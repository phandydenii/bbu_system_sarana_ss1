Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class TRANSCRIPT_FORM
    Public studentid As String
    Friend studentIds As String()
    Friend studentNames As String()
    Friend studentNameInKhmers As String()
    Dim i As Integer
    Dim student As New Student

    Private Sub rdoID_CheckedChanged(sender As Object, e As EventArgs) Handles rdoID.CheckedChanged, rdoName.CheckedChanged, rdoNameKh.CheckedChanged
        studentIds = Students.GetAllStudentIds(True)
        studentNames = Students.GetAllStudentNames()
        studentNameInKhmers = Students.GetAllStudentNameInKhmers()

        cboSearchStudent.Items.Clear()
        If rdoID.Checked Then
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

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click

        If cboSearchStudent Is Nothing Then
            Exit Sub
        End If

        Dim student As New Student
        If rdoID.Checked Then
            student = Students.GetStudentById(cboSearchStudent.Text)
        ElseIf rdoName.Checked Then
            Dim vstudent As SortedList = Students.GetStudentByName(cboSearchStudent.Text)
            If vstudent.Count > 0 Then student = vstudent.GetByIndex(0)
        Else
            Dim vstudent As SortedList = Students.GetStudentByNameInKhmer(cboSearchStudent.Text)
            If vstudent.Count > 0 Then student = vstudent.GetByIndex(0)
        End If
        If student Is Nothing Then Return
        student.lastname = student.StudentName.Split(" ")(0)
        student.lastnamekh = student.StudentNameInKhmer.Split(" ")(0)

        student.firstname = student.StudentName.Replace(student.lastname, "").Trim()
        student.firstnamekh = student.StudentNameInKhmer.Replace(student.lastnamekh, "").Trim

        Dim firstname As String = IIf(rdoKhmer.Checked, student.firstnamekh, student.firstname)
        Dim familyname As String = IIf(rdoKhmer.Checked, student.lastnamekh, student.lastname)
        Dim dob As String = IIf(rdoKhmer.Checked, GetDateKhmer(Convert.ToDateTime(student.DateOfBirth)), Convert.ToDateTime(student.DateOfBirth).ToString("d MMM yyyy"))

        Dim degree As String = ""
        Dim school As String = ""
        Dim field As String = ""
        Dim type As String = ""

        Dim degreeid As Integer = 0
        Dim schoolid As Integer = 0
        Dim fieldid As Integer = 0
        Dim promotionno As Integer = 0


        Dim campus As String = txtCampus.Text.Trim
        Dim branchname As String = txtBranchName.Text.Trim
        Dim signature As String = txtSignature.Text.Trim
        Dim short_name As String = txtShortName.Text.Trim

        Dim cmd As New SqlCommand("SELECT * FROM V_STUDENT_FOR_PRINT_CERTIFICATE where  STUDENT_ID='" + student.StudentId + "'", DbInterface.Connection)
        Dim da As New SqlDataAdapter(cmd)
        Dim datatable As New DataTable
        da.Fill(datatable)
        Dim datareader As SqlDataReader = cmd.ExecuteReader
        If datareader.Read Then
            degreeid = CInt(datareader("DEGREE_ID"))
            schoolid = CInt(datareader("SCHOOL_ID"))
            fieldid = CInt(datareader("FIELD_ID"))
            promotionno = CInt(datareader("PROMOTION_NO"))
        End If
        datareader.Close()

        Dim field_cert As FIELD_CERTIFICATE = FIELD_CERTIFICATE.GetFieldCertificate(degreeid, schoolid, fieldid, promotionno)
        If field_cert Is Nothing Then
            MsgBox("Field Certificate invalid data!")
            Exit Sub
        End If

        degree = IIf(rdoKhmer.Checked, field_cert.DEGREE_NAME_KHMER, field_cert.DEGREE_NAME)
        field = IIf(rdoKhmer.Checked, field_cert.FIELD_NAME_KHMER, field_cert.FIELD_NAME)

        Dim dt As New DataTable
        dt.Columns.Add("YEAR")
        dt.Columns.Add("TERM")
        dt.Columns.Add("COURSE_ID")
        dt.Columns.Add("COURSE_NAME")
        dt.Columns.Add("TOTAL")
        dt.Columns.Add("GRADE")
        dt.Columns.Add("CREDIT")
        dt.Columns.Add("GPV")
        dt.Columns.Add("GPE")
        dt.Columns.Add("COURSE_NAME_KHMER")
        dt.Columns.Add("CODE")

        Dim dr As DataRow
        Dim year As Integer = 0
        Dim i As Integer = 1
        Dim gpa As Single = 0
        Dim totalcredit As Single = 0
        Dim totalgradepoints As Single = 0
        Dim term As Integer = 0
        Dim termNo As Integer = 0
        For Each obj As Object In Scores.GetTranscript(student.StudentId).Values
            Dim score As StudentResultScore = CType(obj, StudentResultScore)
            If score.Type = "FINAL" Or score.Type = "PROJECT_PAPER" Or score.Type = "STATE_EXAM" Then
                If score.Term > Convert.ToInt16(lvwTerm.SelectedItems(0).SubItems(2).Text) Then
                    Exit For
                End If
                dr = dt.NewRow
                totalgradepoints += score.Credit * (Grades.GetGrade(score.Total).GetGradePointString)
                totalcredit += score.Credit
                dr("YEAR") = IIf(rdoEnglish.Checked, score.Year.ToString, getKhmerNum(score.Year.ToString, True))
                dr("TERM") = IIf(rdoEnglish.Checked, score.Term.ToString, getKhmerNum(score.Term.ToString, True))
                dr("CODE") = score.CourseCode
                dr("COURSE_ID") = score.CourseID
                dr("COURSE_NAME") = score.CourseFullName
                dr("TOTAL") = score.Total
                dr("GRADE") = Grades.GetGrade(score.Total)
                dr("CREDIT") = score.Credit.ToString
                dr("GPV") = Grades.GetGrade(score.Total).GetGradePointString
                dr("GPE") = (score.Credit * (Grades.GetGrade(score.Total).GetGradePointString)).ToString("#.#0")
                dr("COURSE_NAME_KHMER") = score.CourseFullNameKhmer
                dt.Rows.Add(dr)
                i += 1
            End If
        Next
        gpa = totalgradepoints / totalcredit


        Dim pgpa As New ReportParameter("gpa", gpa.ToString("#.##"))

        Dim pID As New ReportParameter("id", student.StudentId)
        Dim pFirstName As New ReportParameter("firstname", firstname)
        Dim pFamilyName As New ReportParameter("familyname", familyname)
        Dim pDOB As New ReportParameter("dob", dob)
        Dim pDegree As New ReportParameter("degree", degree)
        Dim pfield As New ReportParameter("field", field)
        Dim pyearkhmer As New ReportParameter("yearkm", CKhmerLunaaCalendar.GetKhmerYear())

        Dim pcampus As New ReportParameter("campus", campus)
        Dim pbranch As New ReportParameter("branchname", branchname)
        Dim psignature As New ReportParameter("signature", signature)
        Dim pshortname As New ReportParameter("shortname", short_name)

        If rdoKhmer.Checked Then
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.TRANSCRIPT_KHMER_RPT.rdlc"
        Else
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.TRANSCRIPT_ENGLISH_RPT.rdlc"
        End If
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dt))
        Me.ReportViewer1.LocalReport.SetParameters({pgpa, pID, pFirstName, pFamilyName, pDOB, pDegree,
                                                   pfield, pyearkhmer, pcampus, pbranch, psignature, pshortname})
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub cboSearchStudent_KeyDown(sender As Object, e As KeyEventArgs) Handles cboSearchStudent.KeyDown
        If (e.KeyCode <> Keys.Enter) Then Exit Sub
        If cboSearchStudent Is Nothing Then
            Exit Sub
        End If

        If rdoID.Checked Then
            student = Students.GetStudentById(cboSearchStudent.Text)
        ElseIf rdoName.Checked Then
            Dim vstudent As SortedList = Students.GetStudentByName(cboSearchStudent.Text)
            Dim st As Student = CType(vstudent.GetByIndex(0), Student)
            If vstudent.Count > 0 Then student = Students.GetStudentById(st.StudentId)
        Else
            Dim vstudent As SortedList = Students.GetStudentByNameInKhmer(cboSearchStudent.Text)
            Dim st As Student = CType(vstudent.GetByIndex(0), Student)
            If vstudent.Count > 0 Then student = Students.GetStudentById(st.StudentId)
        End If

        lvwTerm.Items.Clear()
        For Each obj As Object In StudentGroups.GetStudentGroups(student.StudentId).Values
            Dim studentGroup As StudentGroup = CType(obj, StudentGroup)
            Dim group As Group = Groups.GetGroup(studentGroup.GroupId)
            Dim item As ListViewItem = New ListViewItem(studentGroup.StudentGroupId.ToString())
            item.SubItems.Add(studentGroup.StudentId.ToString())
            item.SubItems.Add(studentGroup.TermNo.ToString())
            item.SubItems.Add(group.GroupId.ToString)
            item.SubItems.Add(group.GroupName)
            lvwTerm.Items.Add(item)
        Next
        If lvwTerm.Items.Count > 0 Then
            lvwTerm.Items(lvwTerm.Items.Count - 1).Selected = True
        End If
    End Sub

    Sub Setting()
        Select Case Utilities.BranchName
            Case "PP"
                txtCampus.Text = IIf(rdoKhmer.Checked, "ទីតាំងគោលរាជធានីភ្នំពេញ", "Phnom Penh Main Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "រាជធានីភ្នំពេញ", "Phnom Penh")
                txtSignature.Text = IIf(rdoKhmer.Checked, "ប្រធាន", "Director")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប", "BBU")
            Case "BB"
                txtCampus.Text = IIf(rdoKhmer.Checked, "សាខាខេត្តបាត់ដំបង", "Battambang Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "បាត់ដំបង", "Battambang")
                txtSignature.Text = IIf(rdoKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តបាត់ដំបង", "Director​" & vbCrLf & "Battambang Campus")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប.ប.ប", "BBU.BB")
            Case "TK"
                txtCampus.Text = IIf(rdoKhmer.Checked, "សាខាខេត្តតាកែវ", "Takeo Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "តាកែវ", "Takeo")
                txtSignature.Text = IIf(rdoKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តតាកែវ", "Director" & vbCrLf & "Takeo Campus")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប.ត.ក", "BBU.TK")
            Case "BMC"
                txtCampus.Text = IIf(rdoKhmer.Checked, "សាខាខេត្តបន្ទាយមានជ័យ", "Banteaymeanchey Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "បន្ទាយមានជ័យ", "Banteaymeanchey")
                txtSignature.Text = IIf(rdoKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តព្រះសីហនុ", "Director" & vbCrLf & "Banteaymeanchey Campus")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប.ប.ជ", "BBU.BMC")
            Case "SH"
                txtCampus.Text = IIf(rdoKhmer.Checked, "សាខាខេត្តព្រះសីហនុ", "Sihanouk Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "ព្រះសីហនុ", "Sihanouk")
                txtSignature.Text = IIf(rdoKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តព្រះសីហនុ", "Director" & vbCrLf & "Sihanouk Campus")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប.ស.ហ", "BBU.SH")
            Case "RK"
                txtCampus.Text = IIf(rdoKhmer.Checked, "សាខាខេត្តរតនគិរី", "Ratanakiri Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "", "Ratanakiri")
                txtSignature.Text = IIf(rdoKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តរតនគិរី", "Director" & vbCrLf & "Ratanakiri Campus")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប.រ.គ", "BBU.RK")
            Case "ST"
                txtCampus.Text = IIf(rdoKhmer.Checked, "សាខាខេត្តស្ទឹងត្រែង", "Stung Treng Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "ស្ទឹងត្រែង", "Stung Treng")
                txtSignature.Text = IIf(rdoKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តស្ទឹងត្រែង", "Director" & vbCrLf & "Stung Treng Campus")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប.ស.ត", "BBU.ST")
            Case "TB"
                txtCampus.Text = IIf(rdoKhmer.Checked, "សាខាខេត្តត្បូងឃ្មុំ", "Tboung Khmum Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "ត្បូងឃ្មុំ", "Tboung Khmum")
                txtSignature.Text = IIf(rdoKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តត្បូងឃ្មុំ", "Director" & vbCrLf & "Tboung Khmu Campus")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប.ត.ឃ", "BBU.TB")
            Case "SR"
                txtCampus.Text = IIf(rdoKhmer.Checked, "សាខាខេត្តសៀមរាប", "Siem Reap Campus")
                txtBranchName.Text = IIf(rdoKhmer.Checked, "សៀមរាប", "Siem Reap")
                txtSignature.Text = IIf(rdoKhmer.Checked, "នាយក" & vbCrLf & "សាខាខេត្តសៀមរាប", "Director" & vbCrLf & "Siem Reap Campus")
                txtShortName.Text = IIf(rdoKhmer.Checked, "ស.ប.ប.ស.រ", "BBU.SR")
        End Select
    End Sub

    Private Sub TRANSCRIPT_FORM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Setting()
    End Sub

    Private Sub rdoKhmer_CheckedChanged(sender As Object, e As EventArgs) Handles rdoKhmer.CheckedChanged, rdoEnglish.CheckedChanged
        Setting()
    End Sub
End Class