Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class PROVISIONAL_CERTIFICATE_FRM
    Public iskm As Boolean
    Public studentid As String
    Friend studentIds As String()
    Friend studentNames As String()
    Friend studentNameInKhmers As String()
    Dim i As Integer

    Private Sub PROVISIONAL_CERTIFICATE_FRM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'cboSerachStudent.Items.Clear()
        'For Each obj As Object In BoundCBODtailItem1().Values
        '    Dim s As Student = CType(obj, Student)
        '    cboSerachStudent.Items.Add(s)
        'Next
        dtpExpireDate.Value = DateTime.Now.AddYears(1)
        Setting()
        grbReportSetting.Enabled = False
    End Sub
    Sub Setting()
        Select Case Utilities.BranchName
            Case "PP"
                txtTitle.Text = IIf(rdoKhmer.Checked, "ការិយាល័យកណ្តាលកិច្ចការនិងធនធានសិក្សា", "Central Office of Academic Affairs and Learning Resources")
                txtCampus.Text = IIf(rdoKhmer.Checked, "ទីតាំងគោលរាជធានីភ្នំពេញ", "Phnom Penh Main Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "រាជធានីភ្នំពេញ", "Phnom Penh")
                txtSignature.Text = IIf(rdoKhmer.Checked, "ប្រធាន", "Director")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប", "BBU")
            Case "BB"
                txtTitle.Text = IIf(rdoKhmer.Checked, "ការិយាល័យកិច្ចការនិងធនធានសិក្សា", "Academic Affairs and Learning Resources")
                txtCampus.Text = IIf(rdoKhmer.Checked, "សាខាខេត្តបាត់ដំបង", "Battambang Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "បាត់ដំបង", "Battambang")
                txtSignature.Text = IIf(rdoKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តបាត់ដំបង", "Director​" & vbCrLf & "Battambang Campus")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប.ប.ប", "BBU.BB")
            Case "TK"
                txtTitle.Text = IIf(rdoKhmer.Checked, "ការិយាល័យកិច្ចការនិងធនធានសិក្សា", "Academic Affairs and Learning Resources")
                txtCampus.Text = IIf(rdoKhmer.Checked, "សាខាខេត្តតាកែវ", "Takeo Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "តាកែវ", "Takeo")
                txtSignature.Text = IIf(rdoKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តតាកែវ", "Director" & vbCrLf & "Takeo Campus")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប.ត.ក", "BBU.TK")
            Case "BMC"
                txtTitle.Text = IIf(rdoKhmer.Checked, "ការិយាល័យកិច្ចការនិងធនធានសិក្សា", "Academic Affairs and Learning Resources")
                txtCampus.Text = IIf(rdoKhmer.Checked, "សាខាខេត្តបន្ទាយមានជ័យ", "Banteaymeanchey Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "បន្ទាយមានជ័យ", "Banteaymeanchey")
                txtSignature.Text = IIf(rdoKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តព្រះសីហនុ", "Director" & vbCrLf & "Banteaymeanchey Campus")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប.ប.ជ", "BBU.BMC")
            Case "SH"
                txtTitle.Text = IIf(rdoKhmer.Checked, "ការិយាល័យកិច្ចការនិងធនធានសិក្សា", "Academic Affairs and Learning Resources")
                txtCampus.Text = IIf(rdoKhmer.Checked, "សាខាខេត្តព្រះសីហនុ", "Sihanouk Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "ព្រះសីហនុ", "Sihanouk")
                txtSignature.Text = IIf(rdoKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តព្រះសីហនុ", "Director" & vbCrLf & "Sihanouk Campus")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប.ស.ហ", "BBU.SH")
            Case "RK"
                txtTitle.Text = IIf(rdoKhmer.Checked, "ការិយាល័យកិច្ចការនិងធនធានសិក្សា", "Academic Affairs and Learning Resources")
                txtCampus.Text = IIf(rdoKhmer.Checked, "សាខាខេត្តរតនគិរី", "Ratanakiri Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "", "Ratanakiri")
                txtSignature.Text = IIf(rdoKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តរតនគិរី", "Director" & vbCrLf & "Ratanakiri Campus")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប.រ.គ", "BBU.RK")
            Case "ST"
                txtTitle.Text = IIf(rdoKhmer.Checked, "ការិយាល័យកិច្ចការនិងធនធានសិក្សា", "Academic Affairs and Learning Resources")
                txtCampus.Text = IIf(rdoKhmer.Checked, "សាខាខេត្តស្ទឹងត្រែង", "Stung Treng Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "ស្ទឹងត្រែង", "Stung Treng")
                txtSignature.Text = IIf(rdoKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តស្ទឹងត្រែង", "Director" & vbCrLf & "Stung Treng Campus")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប.ស.ត", "BBU.ST")
            Case "TB"
                txtTitle.Text = IIf(rdoKhmer.Checked, "ការិយាល័យកិច្ចការនិងធនធានសិក្សា", "Academic Affairs and Learning Resources")
                txtCampus.Text = IIf(rdoKhmer.Checked, "សាខាខេត្តត្បូងឃ្មុំ", "Tboung Khmum Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "ត្បូងឃ្មុំ", "Tboung Khmum")
                txtSignature.Text = IIf(rdoKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តត្បូងឃ្មុំ", "Director" & vbCrLf & "Tboung Khmu Campus")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប.ត.ឃ", "BBU.TB")
            Case "SR"
                txtTitle.Text = IIf(rdoKhmer.Checked, "ការិយាល័យកិច្ចការនិងធនធានសិក្សា", "Academic Affairs and Learning Resources")
                txtCampus.Text = IIf(rdoKhmer.Checked, "សាខាខេត្តសៀមរាប", "Siem Reap Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "សៀមរាប", "Siem Reap")
                txtSignature.Text = IIf(rdoKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តសៀមរាប", "Director" & vbCrLf & "Siem Reap Campus")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប.ស.រ", "BBU.SR")
        End Select
        txtKhmerLunarDate.Text = IIf(rdoKhmer.Checked, $"ថ្ងៃអង្គារ ៦រោច ខែកក្ដិក {CKhmerLunaaCalendar.GetKhmerYear()}", "")
        txtKhmerDate.Text = txtBranchName.Text.Trim() + IIf(rdoKhmer.Checked, $" ថ្ងៃទី {getKhmerNum(DateTime.Now.Day, 0)} ខែ {GetMonthKhmer(DateTime.Now)}​ ឆ្នាំ {getKhmerNum(DateTime.Now.Year, 0)}", $", issued on ............................................{Now.Year}")
    End Sub
    Private Sub rbtSearchById_CheckedChanged(sender As Object, e As EventArgs) Handles rbtSearchById.CheckedChanged, rbtSearchByName.CheckedChanged, rbtSearchByNameInKhmer.CheckedChanged
        studentIds = Students.GetAllStudentIds(True)
        studentNames = Students.GetAllStudentNames()
        studentNameInKhmers = Students.GetAllStudentNameInKhmers()
        'BoundCBODtailItem(cboSerachStudent, "Select STUDENT_ID,STUDENT_ID+' '+STUDENT_NAME FROM STUDENT")
        'If rbtSearchById.Checked Then
        '    BoundCBODtailItem(cboSerachStudent, "SELECT STUDENT_NAME,STUDENT_ID+' '+STUDENT_NAME AS STUDENT_NAME FROM STUDENT")
        'ElseIf rbtSearchByName.Checked Then
        '    BoundCBODtailItem(cboSerachStudent, "SELECT STUDENT_NAME,STUDENT_NAME+' '+STUDENT_ID AS STUDENT_NAME FROM STUDENT")
        'Else
        '    BoundCBODtailItem(cboSerachStudent, "SELECT STUDENT_NAME,STUDENT_NAME_IN_KHMER+' '+STUDENT_ID AS STUDENT_NAME FROM STUDENT")
        'End If
        cboSerachStudent.Items.Clear()
        cboSerachStudent.Text = ""
        If rbtSearchById.Checked Then
            For i = 0 To studentIds.Length - 1
                cboSerachStudent.Items.Add(studentIds(i))
            Next i
        ElseIf rbtSearchByName.Checked Then
            For i = 0 To studentNames.Length - 1
                cboSerachStudent.Items.Add(studentNames(i))
            Next i
        Else
            For i = 0 To studentNameInKhmers.Length - 1
                cboSerachStudent.Items.Add(studentNameInKhmers(i))
            Next i
        End If

    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        If cboSerachStudent Is Nothing Then
            Exit Sub
        End If

        Dim student As New Student
        If rbtSearchById.Checked Then
            student = Students.GetStudentById(cboSerachStudent.Text)
        ElseIf rbtSearchByName.Checked Then
            Dim vstudent As SortedList = Students.GetStudentByName(cboSerachStudent.Text)
            If vstudent.Count > 0 Then student = vstudent.GetByIndex(0)
        Else
            Dim vstudent As SortedList = Students.GetStudentByNameInKhmer(cboSerachStudent.Text)
            If vstudent.Count > 0 Then student = vstudent.GetByIndex(0)
        End If
        If student Is Nothing Then Return

        Dim degreeid As Integer = 0
        Dim schoolid As Integer = 0
        Dim fieldid As Integer = 0
        Dim promotionid As Integer = 0

        Dim degree As String = ""
        Dim school As String = ""
        Dim field As String = ""
        Dim type As String = ""
        Dim pro_year_end As String = ""

        Dim dob As String = ""


        Dim issue_date As String = IIf(rdoKhmer.Checked, GetDateKhmer(dtpIssuedDate.Value), dtpIssuedDate.Value.ToString("d MMM yyyy"))
        Dim expire_date As String = IIf(rdoKhmer.Checked, GetDateKhmer(dtpExpireDate.Value), dtpExpireDate.Value.ToString("d MMM yyyy"))

        Dim iskhmer As String = IIf(rdoEnglish.Checked, "_ENGLISH_RPT.rdlc", "_KHMER_RPT.rdlc")
        Dim REPORT As String = ""

        Dim campus As String = txtCampus.Text.Trim

        Dim signature As String = txtSignature.Text.Trim
        Dim short_name As String = txtShortName.Text.Trim

        Dim cmd As New SqlCommand("SELECT * FROM V_PROVISSIONAL_CERTIFICATE where  STUDENT_ID='" + student.StudentId + "'", DbInterface.Connection)
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)
        Dim dr As SqlDataReader = cmd.ExecuteReader
        Try
            If dr.Read Then
                dob = IIf(rdoKhmer.Checked, GetDateKhmer(Convert.ToDateTime(dr("DATE_OF_BIRTH"))), Convert.ToDateTime(dr("DATE_OF_BIRTH")).ToString("d MMM yyyy"))

                If rdoKhmer.Checked Then
                    pro_year_end = getKhmerNum(dr("PROMOTION_YEAR_END").ToString(), False)
                Else
                    pro_year_end = dr("PROMOTION_YEAR_END").ToString()
                End If

                degreeid = CInt(dr("DEGREE_ID"))
                schoolid = CInt(dr("SCHOOL_ID"))
                fieldid = CInt(dr("FIELD_ID"))
                promotionid = CInt(dr("PROMOTION_NO"))
                'type = IIf(CBool(dr("TYPE")), IIf(rdoKhmer.Checked, "ជំនាញ", "in"), IIf(rdoKhmer.Checked, "ឯកទេស", "in"))
            Else
                dr.Close()
                MsgBox("Invalid student information!")
                Exit Sub
            End If
            dr.Close()

            Dim field_cert As FIELD_CERTIFICATE = FIELD_CERTIFICATE.GetFieldCertificate(degreeid, schoolid, fieldid, promotionid)
            If field_cert Is Nothing Then
                MsgBox("Field Certificate invalid data!")
                Exit Sub
            End If
            If field_cert.FIELD_NAME.Trim = "" Then
                REPORT = "PROFESSIONAL"
            Else
                REPORT = "ACADEMIC"
            End If

            degree = IIf(rdoKhmer.Checked, field_cert.DEGREE_NAME_KHMER, field_cert.DEGREE_NAME)
            field = IIf(rdoKhmer.Checked, field_cert.FIELD_NAME_KHMER, field_cert.FIELD_NAME)
            type = IIf(rdoKhmer.Checked, field_cert.TypeKhmer, field_cert.Type)

            Dim parameters As ReportParameter() = {
                New ReportParameter("report_title", txtTitle.Text),
                New ReportParameter("dob", dob),
                New ReportParameter("degree", degree),
                New ReportParameter("field", field.Trim),
                New ReportParameter("pro_year_end", pro_year_end),
                New ReportParameter("issue_date", issue_date),
                New ReportParameter("expire_date", expire_date),
                New ReportParameter("campus", campus),
                New ReportParameter("branchname", txtKhmerDate.Text.Trim),
                New ReportParameter("signature", signature),
                New ReportParameter("shortname", short_name),
                New ReportParameter("yearkm", txtKhmerLunarDate.Text),
                New ReportParameter("type", type)
            }

            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.PROVISIONAL_CERTIFICATE_" & REPORT + iskhmer
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dt))
            Me.ReportViewer1.LocalReport.SetParameters(parameters)
            ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            ReportViewer1.RefreshReport()
        Catch exception As Exception
            MessageBox.Show(Me, exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub rdoKhmer_CheckedChanged(sender As Object, e As EventArgs) Handles rdoKhmer.CheckedChanged, rdoEnglish.CheckedChanged
        Setting()
    End Sub

    Private Sub chbReportSetting_CheckedChanged(sender As Object, e As EventArgs) Handles chbReportSetting.CheckedChanged
        grbReportSetting.Enabled = chbReportSetting.Checked
    End Sub

    Private Sub chbNotFill_CheckedChanged(sender As Object, e As EventArgs) Handles chbNotFill.CheckedChanged
        If chbNotFill.Checked Then
            txtKhmerLunarDate.Text = IIf(rdoKhmer.Checked, $"ថ្ងៃ........................ទី...................ឆ្នាំ......", "")
            txtKhmerDate.Text = txtBranchName.Text.Trim() + IIf(rdoKhmer.Checked, $" ថ្ងៃទី.........................ខែ.............................ឆ្នាំ........", $", issued on ............................................{Now.Year}")
        Else
            txtKhmerLunarDate.Text = IIf(rdoKhmer.Checked, $"ថ្ងៃអង្គារ ៦រោច ខែកក្ដិក {CKhmerLunaaCalendar.GetKhmerYear()}", "")
            txtKhmerDate.Text = txtBranchName.Text.Trim() + IIf(rdoKhmer.Checked, $" ថ្ងៃទី {getKhmerNum(DateTime.Now.Day, 0)} ខែ {GetMonthKhmer(DateTime.Now)}​ ឆ្នាំ {getKhmerNum(DateTime.Now.Year, 0)}", $", issued on {Now.ToString("dd-MMMM-yyyy")}")
        End If
    End Sub
End Class