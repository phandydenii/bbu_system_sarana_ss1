Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class CERTIFICATE_OF_EDUCATION_FRM
    Dim student As New Student
    Friend studentIds As String()
    Friend studentNames As String()
    Friend studentNameInKhmers As String()
    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Dim dob As String = ""
        Dim type As String = ""
        Dim degree As String = ""
        Dim field As String = ""
        Dim pro_year_end As String = ""
        Dim year As String = ""
        Dim semester As String = ""

        Dim degreeid As Integer = 0
        Dim schoolid As Integer = 0
        Dim fieldid As Integer = 0
        Dim promotionid As Integer = 0

        Dim title As String = ""

        If rdoBeingStudy.Checked Then
            title = IIf(chbKhmer.Checked, "កំពុងសិក្សាកម្រិត", "is currently pursuing")
        Else
            title = IIf(chbKhmer.Checked, "បានបញ្ចប់ការសិក្សាកម្រិត", "successfully completed the following program")
        End If

        Dim campus As String = txtCampus.Text.Trim()
        Dim branchname As String = txtBranchName.Text.Trim()
        Dim signature As String = txtSignature.Text.Trim()
        Dim short_name As String = txtShortName.Text.Trim()

        Dim iskhmer As String = IIf(chbKhmer.Checked, "_KHMER_RPT.rdlc", "_ENGLISH_RPT.rdlc")
        Dim REPORT As String = ""

        Dim cmd As New SqlCommand("SELECT * FROM V_STUDENT_FOR_PRINT_CERTIFICATE where  STUDENT_ID='" + student.StudentId + "'", DbInterface.Connection)
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)
        Dim dr As SqlDataReader = cmd.ExecuteReader
        If dr.Read Then
            dob = IIf(chbKhmer.Checked, GetDateKhmer(Convert.ToDateTime(dr("DATE_OF_BIRTH"))), Convert.ToDateTime(dr("DATE_OF_BIRTH")).ToString("d MMM yyyy"))
            If chbKhmer.Checked Then
                year = getKhmerNum(Term.GetYear(Convert.ToInt16(dr("TERM_NO"))).ToString(), False)
                semester = getKhmerNum(Term.GetSemester(Convert.ToInt16(dr("TERM_NO"))).ToString(), False)
            Else
                year = Term.GetYear(Convert.ToInt16(dr("TERM_NO")))
                semester = Term.GetSemester(Convert.ToInt16(dr("TERM_NO")))
            End If

            If rdoFinish.Checked Then
                pro_year_end = IIf(chbKhmer.Checked, "ដោយជោគជ័យ", "")
            Else
                pro_year_end = IIf(chbKhmer.Checked, $"ក្នុងឆ្នាំទី​ {year} ឆមាសទី​ {semester}", $"in Year {year} Semester {semester}")
            End If

            degreeid = CInt(dr("DEGREE_ID"))
            schoolid = CInt(dr("SCHOOL_ID"))
            fieldid = CInt(dr("FIELD_ID"))
            promotionid = CInt(dr("PROMOTION_NO"))
            'type = IIf(CBool(dr("TYPE")), IIf(chbKhmer.Checked, "ជំនាញ", "in"), IIf(chbKhmer.Checked, "ឯកទេស", "in"))
        End If
        dr.Close()

        Dim field_cert As FIELD_CERTIFICATE = FIELD_CERTIFICATE.GetFieldCertificate(degreeid, schoolid, fieldid, promotionid)
        If field_cert Is Nothing Then
            MsgBox("Field Certificate invalid data!")
            Exit Sub
        End If
        If Fields.GetField(field_cert.FIELD_ID).Type = False Then
            REPORT = "PROFESSIONAL"
        Else
            REPORT = "ACADEMIC"
        End If

        degree = IIf(chbKhmer.Checked, field_cert.DEGREE_NAME_KHMER, field_cert.DEGREE_NAME)
        field = IIf(chbKhmer.Checked, field_cert.FIELD_NAME_KHMER, field_cert.FIELD_NAME)
        type = IIf(chbKhmer.Checked, field_cert.TypeKhmer, field_cert.Type)

        Dim pdob As New ReportParameter("dob", dob)
        Dim ptype As New ReportParameter("type", type)
        Dim pdegree As New ReportParameter("degree", degree)
        Dim pfield As New ReportParameter("field", field.Trim)
        Dim ppro_year_end As New ReportParameter("pro_year_end", pro_year_end)
        Dim ptitle As New ReportParameter("title", title)

        Dim psigner As New ReportParameter("signer", Signature)
        Dim pcampus As New ReportParameter("campus", campus)
        Dim pbranch As New ReportParameter("branchname", branchname)
        Dim pshortname As New ReportParameter("shortname", short_name)
        Dim pyearkhmer As New ReportParameter("yearkm", CKhmerLunaaCalendar.GetKhmerYear())

        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.CERTIFICATE_OF_EDUCATION_" & REPORT + iskhmer
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dt))
        Me.ReportViewer1.LocalReport.SetParameters({pdob, ptype, pdegree, pfield, ppro_year_end, ptitle,
                                                        psigner, pcampus, pbranch, pshortname, pyearkhmer})
        Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.ZoomMode = ZoomMode.Percent
        Me.ReportViewer1.ZoomPercent = 100
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub CERTIFICATE_OF_EDUCATION_FRM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReportSetting()
        grbReportSetting.Enabled = chbReportSetting.Checked
    End Sub
    Sub ReportSetting()
        Select Case Utilities.BranchName
            Case "PP"
                txtCampus.Text = IIf(chbKhmer.Checked, "ទីតាំងគោលរាជធានីភ្នំពេញ", "Phnom Penh Main Campus")
                txtBranchName.Text = IIf(chbKhmer.Checked, "រាជធានីភ្នំពេញ", "Phnom Penh")
                If rdoDirector.Checked Then
                    txtSignature.Text = IIf(chbKhmer.Checked, "ប្រធាន", "Director")
                Else
                    txtSignature.Text = IIf(chbKhmer.Checked, "ជ.សាកលវិទ្យាធិការរងជាន់ខ្ពស់" & vbCrLf & "សាកលវិទ្យាធិការរង", "Vice President")
                End If
                txtShortName.Text = IIf(chbKhmer.Checked, "ស.ប.ប", "BBU")
            Case "BB"
                txtCampus.Text = IIf(chbKhmer.Checked, "សាខាខេត្តបាត់ដំបង", "Battambang Campus")
                txtBranchName.Text = IIf(chbKhmer.Checked, "បាត់ដំបង", "Battambang")
                txtSignature.Text = IIf(chbKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តបាត់ដំបង", "Director​" & vbCrLf & "Battambang Campus")
                txtShortName.Text = IIf(chbKhmer.Checked, "ស.ប.ប.ប.ប", "BBU.BB")
            Case "TK"
                txtCampus.Text = IIf(chbKhmer.Checked, "សាខាខេត្តតាកែវ", "Takeo Campus")
                txtBranchName.Text = IIf(chbKhmer.Checked, "តាកែវ", "Takeo")
                txtSignature.Text = IIf(chbKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តតាកែវ", "Director" & vbCrLf & "Takeo Campus")
                txtShortName.Text = IIf(chbKhmer.Checked, "ស.ប.ប.ត.ក", "BBU.TK")
            Case "BMC"
                txtCampus.Text = IIf(chbKhmer.Checked, "សាខាខេត្តបន្ទាយមានជ័យ", "Banteaymeanchey Campus")
                txtBranchName.Text = IIf(chbKhmer.Checked, "បន្ទាយមានជ័យ", "Banteaymeanchey")
                txtSignature.Text = IIf(chbKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តព្រះសីហនុ", "Director" & vbCrLf & "Banteaymeanchey Campus")
                txtShortName.Text = IIf(chbKhmer.Checked, "ស.ប.ប.ប.ជ", "BBU.BMC")
            Case "SH"
                txtCampus.Text = IIf(chbKhmer.Checked, "សាខាខេត្តព្រះសីហនុ", "Sihanouk Campus")
                txtBranchName.Text = IIf(chbKhmer.Checked, "ព្រះសីហនុ", "Sihanouk")
                txtSignature.Text = IIf(chbKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តព្រះសីហនុ", "Director" & vbCrLf & "Sihanouk Campus")
                txtShortName.Text = IIf(chbKhmer.Checked, "ស.ប.ប.ស.ហ", "BBU.SH")
            Case "RK"
                txtCampus.Text = IIf(chbKhmer.Checked, "សាខាខេត្តរតនគិរី", "Ratanakiri Campus")
                txtBranchName.Text = IIf(chbKhmer.Checked, "", "Ratanakiri")
                txtSignature.Text = IIf(chbKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តរតនគិរី", "Director" & vbCrLf & "Ratanakiri Campus")
                txtShortName.Text = IIf(chbKhmer.Checked, "ស.ប.ប.រ.គ", "BBU.RK")
            Case "ST"
                txtCampus.Text = IIf(chbKhmer.Checked, "សាខាខេត្តស្ទឹងត្រែង", "Stung Treng Campus")
                txtBranchName.Text = IIf(chbKhmer.Checked, "ស្ទឹងត្រែង", "Stung Treng")
                txtSignature.Text = IIf(chbKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តស្ទឹងត្រែង", "Director" & vbCrLf & "Stung Treng Campus")
                txtShortName.Text = IIf(chbKhmer.Checked, "ស.ប.ប.ស.ត", "BBU.ST")
            Case "TB"
                txtCampus.Text = IIf(chbKhmer.Checked, "សាខាខេត្តត្បូងឃ្មុំ", "Tboung Khmum Campus")
                txtBranchName.Text = IIf(chbKhmer.Checked, "ត្បូងឃ្មុំ", "Tboung Khmum")
                txtSignature.Text = IIf(chbKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តត្បូងឃ្មុំ", "Director" & vbCrLf & "Tboung Khmu Campus")
                txtShortName.Text = IIf(chbKhmer.Checked, "ស.ប.ប.ត.ឃ", "BBU.TB")
            Case "SR"
                txtCampus.Text = IIf(chbKhmer.Checked, "សាខាខេត្តសៀមរាប", "Siem Reap Campus")
                txtBranchName.Text = IIf(chbKhmer.Checked, "សៀមរាប", "Siem Reap")
                txtSignature.Text = IIf(chbKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តសៀមរាប", "Director" & vbCrLf & "Siem Reap Campus")
                txtShortName.Text = IIf(chbKhmer.Checked, "ស.ប.ប.ស.រ", "BBU.SR")
        End Select
    End Sub
    Private Sub rbtSearchById_CheckedChanged(sender As Object, e As EventArgs) Handles rdoId.CheckedChanged, rdoName.CheckedChanged, rdoNameKh.CheckedChanged
        studentIds = Students.GetAllStudentIds(True)
        studentNames = Students.GetAllStudentNames()
        studentNameInKhmers = Students.GetAllStudentNameInKhmers()
        Dim i As Integer
        cboSearchStudent.Items.Clear()
        If rdoId.Checked Then
            For i = 0 To studentIds.Length - 1
                cboSearchStudent.Items.Add(studentIds(i))
            Next i
        ElseIf rdoName.Checked Then
            For i = 0 To studentNames.Length - 1
                cboSearchStudent.Items.Add(studentNames(i))
            Next i
        Else
            For i = 0 To studentNameInKhmers.Length - 1
                cboSearchStudent.Items.Add(studentNameInKhmers(i))
            Next i
        End If
    End Sub

    Private Sub cboSearchStudent_KeyDown(sender As Object, e As KeyEventArgs) Handles cboSearchStudent.KeyDown
        If (e.KeyCode <> Keys.Enter) Then Exit Sub
        If cboSearchStudent Is Nothing Then
            Exit Sub
        End If

        If rdoId.Checked Then
            student = Students.GetStudentById(cboSearchStudent.Text)
        ElseIf rdoName.Checked Then
            Dim vstudent As SortedList = Students.GetStudentByName(cboSearchStudent.Text)
            If vstudent.Count > 0 Then student = vstudent.GetByIndex(0)
        Else
            Dim vstudent As SortedList = Students.GetStudentByNameInKhmer(cboSearchStudent.Text)
            If vstudent.Count > 0 Then student = vstudent.GetByIndex(0)
        End If
    End Sub

    Private Sub chbKhmer_CheckedChanged(sender As Object, e As EventArgs) Handles chbKhmer.CheckedChanged
        ReportSetting()
    End Sub

    Private Sub chbReportSetting_CheckedChanged(sender As Object, e As EventArgs) Handles chbReportSetting.CheckedChanged
        grbReportSetting.Enabled = chbReportSetting.Checked
    End Sub

    Private Sub rdoDirector_CheckedChanged(sender As Object, e As EventArgs) Handles rdoDirector.CheckedChanged, rdoVicePrecident.CheckedChanged
        ReportSetting()
    End Sub
End Class