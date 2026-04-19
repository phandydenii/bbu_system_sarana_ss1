Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Public Class ViewerFrm
    Sub SelectReport(ByVal report As Integer)
        Select Case report
            Case 1
                lblToolTib.Text = "1-BySchool, 2-ByDate"
                'Dim student_quit As New Students_Quit
                StudentQuit()
            Case 2
                lblToolTib.Text = "1-ByDate"
                'Dim student_quit_resume As New Students_Quit_Resume
                StudentQuitResume()
            Case 3
                lblToolTib.Text = "1-BySchool, 2-ByDate"
                'Dim student_suspend As New Students_Suspend
                StudentSuspend()
            Case 4
                lblToolTib.Text = "1-ByDate"
                Dim student_suspend_resume As New Students_Suspend_Resume
                StudentSuspendResume()
            Case 5
                lblToolTib.Text = "1-BySchool, 2-ByDate"
                Dim student_suppress As New Students_Suppress
                StudentSuppress()

            Case 6
                lblToolTib.Text = "1-ByDate"
                Dim student_Express As New Students_Express
                StudentExpress()

            Case 7
                lblToolTib.Text = "1-ByDate"
                Dim change_group As New Change_Group
                StudentChangeGroup()
            Case 8
                lblToolTib.Text = "1-ByDate"
                Dim chnage_branch_in As New Change_Branch_In
                StudentBranchIn()
            Case 9
                lblToolTib.Text = "1-ByDate"
                Dim chnage_branch_out As New Change_Branch_Out
                StudentBranchOut()
            Case 10
                lblToolTib.Text = "1-ByDate"
                Dim extend_from_other_university As New Extend_From_other_university
                OtherUniversity()
            Case 11
                lblToolTib.Text = "1-ByDate"
                Dim extend_from_other_branch As New Extend_From_other_branch
                OtherBranch()
            Case 12
                lblToolTib.Text = "1-ByDate"
                Dim change_school_field As New Student_Change_School_Field
                ChangeSchoolField()
            Case 13
                lblToolTib.Text = "1-ByDate"
                Dim change_school_fields As New Student_Change_School_Field_FirstTime
                change_school_fields.Dock = DockStyle.Fill
                ChangeSchoolFieldFirstTime()

        End Select
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect

    End Sub


    Private Sub checkPrint_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkPrint.CheckedChanged
        If checkPrint.Checked = True Then
            cboDegree.Enabled = False
            cboSchool.Enabled = False
            cboField.Enabled = False
            cboPromotion.Enabled = False
            cboStage.Enabled = False
            txtYear.Enabled = False
            txtSemester.Enabled = False
            lstGroup.Enabled = False
        Else
            cboDegree.Enabled = True
            cboSchool.Enabled = True
            cboField.Enabled = True
            cboPromotion.Enabled = True
            cboStage.Enabled = True
            txtYear.Enabled = True
            txtSemester.Enabled = True
            lstGroup.Enabled = True
        End If
    End Sub

    Private Sub ViewerFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboDegree.Items.Clear()
        For Each obj As Object In Degrees.GetDegrees.Values
            cboDegree.Items.Add(obj)
        Next
        'BoundCbo(cboDegree, "SELECT DEGREE_ID,DEGREE FROM DEGREE")
        'BoundCbo(cboSchool, "SELECT SCHOOL_ID,SCHOOL_NAME FROM SCHOOL")
        'BoundCbo(cboField, "SELECT FIELD_ID,FIELD_NAME FROM FIELD WHERE DEGREE_ID=" & cboDegree.SelectedValue & " AND SCHOOL_ID=" & cboSchool.SelectedValue & "")
        'BoundCbo(cboPromotion, "SELECT PROMOTION_ID,PROMOTION_NO FROM PROMOTION WHERE DEGREE_ID=" & cboDegree.SelectedValue & " AND SCHOOL_ID=" & cboSchool.SelectedValue & "")
        'BoundCbo(cboStage, "SELECT STAGE_ID,STAGE_NO FROM STAGE WHERE PROMOTION_ID=" & cboPromotion.SelectedValue & "")
        'Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub cboDegree_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDegree.SelectedIndexChanged
        cboSchool.Items.Clear()
        cboPromotion.Items.Clear()
        cboStage.Items.Clear()
        txtYear.Text = ""
        txtSemester.Text = ""
        cboField.Items.Clear()

        If cboDegree.SelectedIndex = -1 Then Exit Sub

        If CType(cboDegree.SelectedItem, Degree).DegreeId = Degree.BACHELOR Then
            For Each obj As Object In Schools.GetSchools().Values
                cboSchool.Items.Add(obj)
            Next
        Else
            For Each obj As Object In Schools.GetSchools().Values
                If Not CType(obj, School).IsFoundationSchool Then
                    cboSchool.Items.Add(obj)
                End If
            Next
        End If

    End Sub

    Private Sub cboSchool_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSchool.SelectedIndexChanged
        cboPromotion.Items.Clear()
        cboStage.Items.Clear()
        txtYear.Text = ""
        txtSemester.Text = ""
        cboField.Items.Clear()
        If cboDegree.SelectedIndex = -1 OrElse cboSchool.SelectedIndex = -1 Then
            Exit Sub
        End If
        For Each obj As Object In Promotions.GetPromotions(CType(cboDegree.SelectedItem, Degree).DegreeId, CType(cboSchool.SelectedItem, School).SchoolId).Values
            cboPromotion.Items.Add(obj)
        Next

        For Each obj As Object In Fields.GetFields(CType(cboSchool.SelectedItem, School).SchoolId, CType(cboDegree.SelectedItem, Degree).DegreeId).Values
            cboField.Items.Add(obj)
        Next
    End Sub

    Private Sub cboPromotion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPromotion.SelectedIndexChanged
        cboStage.Items.Clear()
        lstGroup.Items.Clear()

        If cboPromotion.SelectedIndex = -1 Then Exit Sub

        For Each obj As Object In Stages.GetStages(CType(cboPromotion.SelectedItem, Promotion).PromotionId).Values
            cboStage.Items.Add(obj)
        Next
    End Sub

    Private Sub cboStage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboStage.SelectedIndexChanged
        lstGroup.Items.Clear()

        If cboStage.SelectedIndex = -1 Or txtYear.Text = "" Or txtSemester.Text = "" Then Exit Sub

        If cboField.SelectedIndex <> -1 Then
            For Each obj As Object In Groups.GetGroups(CType(cboPromotion.SelectedItem, Promotion), Term.GetTermNo(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtSemester.Text))).Values
                Dim group As Group = CType(obj, Group)
                If group.StageId = CType(cboStage.SelectedItem, Stage).StageId AndAlso group.FieldId = CType(cboField.SelectedItem, Field).FieldId Then
                    lstGroup.Items.Add(obj)
                End If
            Next
        Else
            For Each obj As Object In Groups.GetGroups(CType(cboPromotion.SelectedItem, Promotion), Term.GetTermNo(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtSemester.Text))).Values
                Dim group As Group = CType(obj, Group)
                If group.StageId = CType(cboStage.SelectedItem, Stage).StageId Then
                    lstGroup.Items.Add(obj)
                End If
            Next
        End If
    End Sub

    Private Sub txtSemester_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSemester.KeyPress, txtYear.KeyPress
        If (Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtYear_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtYear.TextChanged, txtSemester.TextChanged
        lstGroup.Items.Clear()

        If cboDegree.SelectedIndex = -1 Then Exit Sub
        If cboSchool.SelectedIndex = -1 Then Exit Sub
        If cboPromotion.SelectedIndex = -1 Then Exit Sub
        If cboStage.SelectedIndex = -1 Then Exit Sub
        If txtYear.Text = "" Then Exit Sub
        If txtSemester.Text = "" Then Exit Sub

        If cboField.SelectedIndex <> -1 Then
            For Each obj As Object In Groups.GetGroups(CType(cboPromotion.SelectedItem, Promotion), Term.GetTermNo(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtSemester.Text))).Values
                Dim group As Group = CType(obj, Group)
                If group.StageId = CType(cboStage.SelectedItem, Stage).StageId AndAlso group.FieldId = CType(cboField.SelectedItem, Field).FieldId Then
                    lstGroup.Items.Add(obj)
                End If
            Next
        Else
            For Each obj As Object In Groups.GetGroups(CType(cboPromotion.SelectedItem, Promotion), Term.GetTermNo(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtSemester.Text))).Values
                Dim group As Group = CType(obj, Group)
                If group.StageId = CType(cboStage.SelectedItem, Stage).StageId Then
                    lstGroup.Items.Add(obj)
                End If
            Next
        End If
    End Sub

    Private Sub chkAllField_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllField.CheckedChanged
        If chkAllField.Checked = True Then
            cboField.Enabled = False
        Else
            cboField.Enabled = True
        End If
    End Sub

    Private Sub StudentQuit()
        If checkPrint.Checked = True Then
            Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_STUDENT_QUIT WHERE CAST(QUIT_DATE AS DATE) BETWEEN @from AND @to", DbInterface.Connection)
            cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
            cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
            Dim da As New SqlDataAdapter(cmd)
            Dim dtReport As New DataTable
            da.Fill(dtReport)
            Dim pfrom As New ReportParameter("from", dtpFrom.Value)
            Dim pto As New ReportParameter("to", dtpTo.Value)
            Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Students_Quit.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
            Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto, pDes})
            Me.ReportViewer1.RefreshReport()
        Else
            If cboDegree.SelectedValue Is Nothing Then
                Dim groups As String = ""
                For Each obj As Object In lstGroup.SelectedItems
                    groups = groups & CType(obj, Group).GroupId & ", "
                Next
                groups = groups.Substring(0, groups.Length - 2) & ""


                Dim groupName As String = ""
                For Each obj As Object In lstGroup.SelectedItems
                    groupName = groupName & CType(obj, Group).GroupName & ", "
                Next
                groupName = groupName.Substring(0, groupName.Length - 2) & ""
                Dim Str As String
                Dim dtReport As New DataTable
                If chkAllField.Checked = True Then
                    Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_STUDENT_QUIT WHERE DEGREE_ID=@degree_id AND SCHOOL_ID=@school_id  AND PROMOTION_ID=@promotion_id AND STAGE_ID=@stage_id AND GROUP_ID IN (" & groups & ") AND TERM_NO= " & Term.GetTermNo(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtSemester.Text)) & "  AND QUIT_DATE BETWEEN @from AND @to", DbInterface.Connection)
                    cmd.Parameters.Add("@degree_id", SqlDbType.Int).Value = CType(cboDegree.SelectedItem, Degree).DegreeId
                    cmd.Parameters.Add("@school_id", SqlDbType.Int).Value = CType(cboSchool.SelectedItem, School).SchoolId
                    cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = CType(cboPromotion.SelectedItem, Promotion).PromotionId
                    cmd.Parameters.Add("@stage_id", SqlDbType.Int).Value = CType(cboStage.SelectedItem, Stage).StageId
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dtReport)
                    Str = "និស្សិតជំនាន់ " & CStr(cboPromotion.Text) & "​ វគ្គ ​" & CStr(cboStage.Text) & " ឆ្នាំ ​" & CStr(txtYear.Text) & " ឆមាស " & CStr(txtSemester.Text) & " សិក្សាផ្នែក " & CStr(cboSchool.Text) & " ក្រុម " & groupName
                Else
                    Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_STUDENT_QUIT WHERE DEGREE_ID=@degree_id AND SCHOOL_ID=@school_id AND FIELD_ID=@field_id AND PROMOTION_ID=@promotion_id AND STAGE_ID=@stage_id AND GROUP_ID IN (" & groups & ") AND TERM_NO= " & Term.GetTermNo(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtSemester.Text)) & "  AND QUIT_DATE BETWEEN @from AND @to", DbInterface.Connection)
                    cmd.Parameters.Add("@degree_id", SqlDbType.Int).Value = CType(cboDegree.SelectedItem, Degree).DegreeId
                    cmd.Parameters.Add("@school_id", SqlDbType.Int).Value = CType(cboSchool.SelectedItem, School).SchoolId
                    cmd.Parameters.Add("@field_id", SqlDbType.Int).Value = CType(cboField.SelectedItem, Field).FieldId
                    cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = CType(cboPromotion.SelectedItem, Promotion).PromotionId
                    cmd.Parameters.Add("@stage_id", SqlDbType.Int).Value = CType(cboStage.SelectedItem, Stage).StageId
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dtReport)
                    Str = "និស្សិតជំនាន់ " & CStr(cboPromotion.Text) & "​ វគ្គ ​" & CStr(cboStage.Text) & " ឆ្នាំ ​" & CStr(txtYear.Text) & " ឆមាស " & CStr(txtSemester.Text) & " សិក្សាផ្នែក " & CStr(cboSchool.Text) & " ជំនាញ ​" & CStr(cboField.Text) & " ក្រុម " & groupName
                End If

                Dim pfrom As New ReportParameter("from", dtpFrom.Value)
                Dim pto As New ReportParameter("to", dtpTo.Value)
                Dim pDes As New ReportParameter("description", Str)
                Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Students_Quit.rdlc"
                Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
                Me.ReportViewer1.LocalReport.DataSources.Clear()
                Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
                Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto, pDes})
                Me.ReportViewer1.RefreshReport()
            End If

        End If
    End Sub

    Private Sub StudentQuitResume()
        Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_STUDENT_QUIT_RESUME WHERE CAST(DATE_PAYMENT AS DATE) BETWEEN @from AND @to", DbInterface.Connection)
        cmd.Parameters.Add("@from", SqlDbType.VarChar).Value = dtpFrom.Value.Date.ToString()
        cmd.Parameters.Add("@to", SqlDbType.VarChar).Value = dtpTo.Value.Date.ToString()
        Dim da As New SqlDataAdapter(cmd)
        Dim dtReport As New DataTable
        da.Fill(dtReport)
        Dim pfrom As New ReportParameter("from", dtpFrom.Value)
        Dim pto As New ReportParameter("to", dtpTo.Value)
        'Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Students_Quit_Resume.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
        Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto})
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub StudentSuspend()
        If checkPrint.Checked = True Then
            Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_STUDENT_SUSPEND WHERE CAST(FROM_DATE AS DATE) BETWEEN @from AND @to", DbInterface.Connection)
            cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
            cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
            Dim da As New SqlDataAdapter(cmd)
            Dim dtReport As New DataTable
            da.Fill(dtReport)
            Dim pfrom As New ReportParameter("from", dtpFrom.Value)
            Dim pto As New ReportParameter("to", dtpTo.Value)
            Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Students_Suspend.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
            Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto, pDes})
            Me.ReportViewer1.RefreshReport()
        Else
            If cboDegree.SelectedValue Is Nothing Then
                Dim groups As String = ""
                For Each obj As Object In lstGroup.SelectedItems
                    groups = groups & CType(obj, Group).GroupId & ", "
                Next
                groups = groups.Substring(0, groups.Length - 2) & ""


                Dim groupName As String = ""
                For Each obj As Object In lstGroup.SelectedItems
                    groupName = groupName & CType(obj, Group).GroupName & ", "
                Next
                groupName = groupName.Substring(0, groupName.Length - 2) & ""
                Dim Str As String
                Dim dtReport As New DataTable
                If chkAllField.Checked = True Then
                    Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_STUDENT_SUSPEND WHERE DEGREE_ID=@degree_id AND SCHOOL_ID=@school_id  AND PROMOTION_ID=@promotion_id AND STAGE_ID=@stage_id AND GROUP_ID IN (" & groups & ") AND TERM_NO= " & Term.GetTermNo(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtSemester.Text)) & "  AND CAST(FROM_DATE AS DATE) BETWEEN @from AND @to", DbInterface.Connection)

                    cmd.Parameters.Add("@degree_id", SqlDbType.Int).Value = CType(cboDegree.SelectedItem, Degree).DegreeId
                    cmd.Parameters.Add("@school_id", SqlDbType.Int).Value = CType(cboSchool.SelectedItem, School).SchoolId
                    cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = CType(cboPromotion.SelectedItem, Promotion).PromotionId
                    cmd.Parameters.Add("@stage_id", SqlDbType.Int).Value = CType(cboStage.SelectedItem, Stage).StageId
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dtReport)
                    Str = "និស្សិតជំនាន់ " & CStr(cboPromotion.Text) & "​ វគ្គ ​" & CStr(cboStage.Text) & " ឆ្នាំ ​" & CStr(txtYear.Text) & " ឆមាស " & CStr(txtSemester.Text) & " សិក្សាផ្នែក " & CStr(cboSchool.Text) & " ក្រុម " & groupName
                Else
                    Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_STUDENT_SUSPEND WHERE DEGREE_ID=@degree_id AND SCHOOL_ID=@school_id AND FIELD_ID=@field_id AND PROMOTION_ID=@promotion_id AND STAGE_ID=@stage_id AND GROUP_ID IN (" & groups & ") AND TERM_NO= " & Term.GetTermNo(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtSemester.Text)) & "  AND CAST(FROM_DATE AS DATE) BETWEEN @from AND @to", DbInterface.Connection)

                    cmd.Parameters.Add("@degree_id", SqlDbType.Int).Value = CType(cboDegree.SelectedItem, Degree).DegreeId
                    cmd.Parameters.Add("@school_id", SqlDbType.Int).Value = CType(cboSchool.SelectedItem, School).SchoolId
                    cmd.Parameters.Add("@field_id", SqlDbType.Int).Value = CType(cboField.SelectedItem, Field).FieldId
                    cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = CType(cboPromotion.SelectedItem, Promotion).PromotionId
                    cmd.Parameters.Add("@stage_id", SqlDbType.Int).Value = CType(cboStage.SelectedItem, Stage).StageId
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dtReport)
                    Str = "និស្សិតជំនាន់ " & CStr(cboPromotion.Text) & "​ វគ្គ ​" & CStr(cboStage.Text) & " ឆ្នាំ ​" & CStr(txtYear.Text) & " ឆមាស " & CStr(txtSemester.Text) & " សិក្សាផ្នែក " & CStr(cboSchool.Text) & " ជំនាញ ​" & CStr(cboField.Text) & " ក្រុម " & groupName
                End If

                Dim pfrom As New ReportParameter("from", dtpFrom.Value)
                Dim pto As New ReportParameter("to", dtpTo.Value)
                Dim pDes As New ReportParameter("description", Str)
                Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Students_Suspend.rdlc"
                Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
                Me.ReportViewer1.LocalReport.DataSources.Clear()
                Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
                Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto, pDes})
                Me.ReportViewer1.RefreshReport()
            End If

        End If

    End Sub

    Private Sub StudentSuspendResume()

        Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_STUDENT_SUSPEND_RESUME WHERE CAST(DATE_PAYMENT AS DATE) BETWEEN @from AND @to", DbInterface.Connection)

        cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
        cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
        Dim da As New SqlDataAdapter(cmd)
        Dim dtReport As New DataTable
        da.Fill(dtReport)
        Dim pfrom As New ReportParameter("from", dtpFrom.Value)
        Dim pto As New ReportParameter("to", dtpTo.Value)
        'Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Students_Suspend_Resume.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
        Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto})
        Me.ReportViewer1.RefreshReport()


    End Sub

    Sub StudentSuppress()
        If checkPrint.Checked = True Then
            Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_STUDENT_SUPPRESS WHERE CAST(SUPPRESS_DATE AS DATE) BETWEEN @from AND @to", DbInterface.Connection)

            cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
            cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
            Dim dtReport As New DataTable
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dtReport)
            Dim pfrom As New ReportParameter("from", dtpFrom.Value)
            Dim pto As New ReportParameter("to", dtpTo.Value)
            Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Students_Suppress.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
            Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto, pDes})
            Me.ReportViewer1.RefreshReport()

        Else
            If cboDegree.SelectedValue Is Nothing Then
                Dim groups As String = ""
                For Each obj As Object In lstGroup.SelectedItems
                    groups = groups & CType(obj, Group).GroupId & ", "
                Next
                groups = groups.Substring(0, groups.Length - 2) & ""


                Dim groupName As String = ""
                For Each obj As Object In lstGroup.SelectedItems
                    groupName = groupName & CType(obj, Group).GroupName & ", "
                Next
                groupName = groupName.Substring(0, groupName.Length - 2) & ""
                Dim Str As String
                Dim dtReport As New DataTable
                If chkAllField.Checked = True Then
                    Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_STUDENT_SUPPRESS WHERE DEGREE_ID=@degree_id AND SCHOOL_ID=@school_id  AND PROMOTION_ID=@promotion_id AND STAGE_ID=@stage_id AND GROUP_ID IN (" & groups & ") AND TERM_NO= " & Term.GetTermNo(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtSemester.Text)) & "  AND CAST(SUPPRESS_DATE AS DATE) BETWEEN @from AND @to", DbInterface.Connection)

                    cmd.Parameters.Add("@degree_id", SqlDbType.Int).Value = CType(cboDegree.SelectedItem, Degree).DegreeId
                    cmd.Parameters.Add("@school_id", SqlDbType.Int).Value = CType(cboSchool.SelectedItem, School).SchoolId
                    cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = CType(cboPromotion.SelectedItem, Promotion).PromotionId
                    cmd.Parameters.Add("@stage_id", SqlDbType.Int).Value = CType(cboStage.SelectedItem, Stage).StageId
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dtReport)
                    Str = "និស្សិតជំនាន់ " & CStr(cboPromotion.Text) & "​ វគ្គ ​" & CStr(cboStage.Text) & " ឆ្នាំ ​" & CStr(txtYear.Text) & " ឆមាស " & CStr(txtSemester.Text) & " សិក្សាផ្នែក " & CStr(cboSchool.Text) & " ក្រុម " & groupName
                Else
                    Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_STUDENT_SUPPRESS WHERE DEGREE_ID=@degree_id AND SCHOOL_ID=@school_id AND FIELD_ID=@field_id AND PROMOTION_ID=@promotion_id AND STAGE_ID=@stage_id AND GROUP_ID IN (" & groups & ") AND TERM_NO= " & Term.GetTermNo(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtSemester.Text)) & "  AND CAST(SUPPRESS_DATE AS DATE) BETWEEN @from AND @to", DbInterface.Connection)

                    cmd.Parameters.Add("@degree_id", SqlDbType.Int).Value = CType(cboDegree.SelectedItem, Degree).DegreeId
                    cmd.Parameters.Add("@school_id", SqlDbType.Int).Value = CType(cboSchool.SelectedItem, School).SchoolId
                    cmd.Parameters.Add("@field_id", SqlDbType.Int).Value = CType(cboField.SelectedItem, Field).FieldId
                    cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = CType(cboPromotion.SelectedItem, Promotion).PromotionId
                    cmd.Parameters.Add("@stage_id", SqlDbType.Int).Value = CType(cboStage.SelectedItem, Stage).StageId
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dtReport)
                    Str = "និស្សិតជំនាន់ " & CStr(cboPromotion.Text) & "​ វគ្គ ​" & CStr(cboStage.Text) & " ឆ្នាំ ​" & CStr(txtYear.Text) & " ឆមាស " & CStr(txtSemester.Text) & " សិក្សាផ្នែក " & CStr(cboSchool.Text) & " ជំនាញ ​" & CStr(cboField.Text) & " ក្រុម " & groupName
                End If

                Dim pfrom As New ReportParameter("from", dtpFrom.Value)
                Dim pto As New ReportParameter("to", dtpTo.Value)
                Dim pDes As New ReportParameter("description", Str)
                Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Students_Suppress.rdlc"
                Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
                Me.ReportViewer1.LocalReport.DataSources.Clear()
                Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
                Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto, pDes})
                Me.ReportViewer1.RefreshReport()


            End If

        End If
    End Sub

    Sub StudentExpress()
        If checkPrint.Checked = True Then
            Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_STUDENT_EXPRESS WHERE CAST(EXPRESS_DATE AS DATE) BETWEEN @from AND @to", DbInterface.Connection)

            cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
            cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
            Dim da As New SqlDataAdapter(cmd)
            Dim dtReport As New DataTable
            da.Fill(dtReport)
            Dim pfrom As New ReportParameter("from", dtpFrom.Value)
            Dim pto As New ReportParameter("to", dtpTo.Value)
            Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Students_Express.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

            Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto, pDes})
            Me.ReportViewer1.RefreshReport()
        Else
            If cboDegree.SelectedValue Is Nothing Then
                Dim groups As String = ""
                For Each obj As Object In lstGroup.SelectedItems
                    groups = groups & CType(obj, Group).GroupId & ", "
                Next
                groups = groups.Substring(0, groups.Length - 2) & ""


                Dim groupName As String = ""
                For Each obj As Object In lstGroup.SelectedItems
                    groupName = groupName & CType(obj, Group).GroupName & ", "
                Next
                groupName = groupName.Substring(0, groupName.Length - 2) & ""
                Dim Str As String
                Dim dtReport As New DataTable
                If chkAllField.Checked = True Then
                    Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_STUDENT_EXPRESS WHERE  DEGREE_ID=@degree_id AND SCHOOL_ID=@school_id  AND PROMOTION_ID=@promotion_id AND STAGE_ID=@stage_id AND GROUP_ID IN (" & groups & ") AND TERM_NO= " & Term.GetTermNo(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtSemester.Text)) & "  AND EXPRESS_DATE BETWEEN @from AND @to", DbInterface.Connection)

                    cmd.Parameters.Add("@degree_id", SqlDbType.Int).Value = CType(cboDegree.SelectedItem, Degree).DegreeId
                    cmd.Parameters.Add("@school_id", SqlDbType.Int).Value = CType(cboSchool.SelectedItem, School).SchoolId
                    cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = CType(cboPromotion.SelectedItem, Promotion).PromotionId
                    cmd.Parameters.Add("@stage_id", SqlDbType.Int).Value = CType(cboStage.SelectedItem, Stage).StageId
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dtReport)
                    Str = "និស្សិតជំនាន់ " & CStr(cboPromotion.Text) & "​ វគ្គ ​" & CStr(cboStage.Text) & " ឆ្នាំ ​" & CStr(txtYear.Text) & " ឆមាស " & CStr(txtSemester.Text) & " សិក្សាផ្នែក " & CStr(cboSchool.Text) & " ក្រុម " & groupName
                Else
                    Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_STUDENT_EXPRESS WHERE DEGREE_ID=@degree_id AND SCHOOL_ID=@school_id AND FIELD_ID=@field_id AND PROMOTION_ID=@promotion_id AND STAGE_ID=@stage_id AND GROUP_ID IN (" & groups & ") AND TERM_NO= " & Term.GetTermNo(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtSemester.Text)) & "  AND EXPRESS_DATE BETWEEN @from AND @to", DbInterface.Connection)

                    cmd.Parameters.Add("@degree_id", SqlDbType.Int).Value = CType(cboDegree.SelectedItem, Degree).DegreeId
                    cmd.Parameters.Add("@school_id", SqlDbType.Int).Value = CType(cboSchool.SelectedItem, School).SchoolId
                    cmd.Parameters.Add("@field_id", SqlDbType.Int).Value = CType(cboField.SelectedItem, Field).FieldId
                    cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = CType(cboPromotion.SelectedItem, Promotion).PromotionId
                    cmd.Parameters.Add("@stage_id", SqlDbType.Int).Value = CType(cboStage.SelectedItem, Stage).StageId
                    cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
                    cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dtReport)
                    Str = "និស្សិតជំនាន់ " & CStr(cboPromotion.Text) & "​ វគ្គ ​" & CStr(cboStage.Text) & " ឆ្នាំ ​" & CStr(txtYear.Text) & " ឆមាស " & CStr(txtSemester.Text) & " សិក្សាផ្នែក " & CStr(cboSchool.Text) & " ជំនាញ ​" & CStr(cboField.Text) & " ក្រុម " & groupName
                End If

                Dim pfrom As New ReportParameter("from", dtpFrom.Value)
                Dim pto As New ReportParameter("to", dtpTo.Value)
                Dim pDes As New ReportParameter("description", Str)
                Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Students_Express.rdlc"
                Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
                Me.ReportViewer1.LocalReport.DataSources.Clear()
                Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
                Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto, pDes})
                Me.ReportViewer1.RefreshReport()
            End If

        End If
    End Sub

    Sub StudentChangeGroup()
        Dim cmd As New SqlCommand("SELECT * FROM dbo.STUDENT_GROUP_HISTORY_V WHERE CAST(CHANGE_DATE AS DATE)  BETWEEN @from AND @to", DbInterface.Connection)

        cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
        cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
        Dim dtReport As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dtReport)
        Dim pfrom As New ReportParameter("from", dtpFrom.Value)
        Dim pto As New ReportParameter("to", dtpTo.Value)
        'Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Change_Group.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

        Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto})
        Me.ReportViewer1.RefreshReport()
    End Sub

    Sub StudentBranchIn()
        Dim cmd As New SqlCommand("SELECT * FROM CHANGE_BRANCH_V WHERE CAST(RETURN_DATE AS DATE) BETWEEN @from AND @to", DbInterface.Connection)

        cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
        cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
        Dim da As New SqlDataAdapter(cmd)
        Dim dtReport As New DataTable
        da.Fill(dtReport)
        Dim pfrom As New ReportParameter("from", dtpFrom.Value)
        Dim pto As New ReportParameter("to", dtpTo.Value)
        'Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Change_Branch_In.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

        Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto})
        Me.ReportViewer1.RefreshReport()
    End Sub

    Sub StudentBranchOut()
        Dim cmd As New SqlCommand("SELECT * FROM CHANGE_BRANCH_V WHERE CAST(FROM_DATE AS DATE) BETWEEN @from AND @to", DbInterface.Connection)

        cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
        cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
        Dim da As New SqlDataAdapter(cmd)
        Dim dtReport As New DataTable
        da.Fill(dtReport)
        Dim pfrom As New ReportParameter("from", dtpFrom.Value)
        Dim pto As New ReportParameter("to", dtpTo.Value)
        'Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Change_Branch_out.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

        Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto})
        Me.ReportViewer1.RefreshReport()
    End Sub

    Sub OtherUniversity()
        Dim cmd As New SqlCommand("SELECT * FROM EXTEND_FROM_OTHER_UNIVERSITY_V WHERE CAST(EXTEND_DATE AS DATE)  BETWEEN @from AND @to", DbInterface.Connection)

        cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
        cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
        Dim da As New SqlDataAdapter(cmd)
        Dim dtReport As New DataTable
        da.Fill(dtReport)
        Dim pfrom As New ReportParameter("from", dtpFrom.Value)
        Dim pto As New ReportParameter("to", dtpTo.Value)
        'Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Extend_From_other_university.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

        Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto})
        Me.ReportViewer1.RefreshReport()
    End Sub

    Sub OtherBranch()
        Dim cmd As New SqlCommand("SELECT * FROM EXTEND_FROM_OTHER_BRANCH_V WHERE CAST(EXTEND_DATE AS DATE)  BETWEEN @from AND @to", DbInterface.Connection)

        cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
        cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
        Dim da As New SqlDataAdapter(cmd)
        Dim dtReport As New DataTable
        da.Fill(dtReport)
        Dim pfrom As New ReportParameter("from", dtpFrom.Value)
        Dim pto As New ReportParameter("to", dtpTo.Value)
        'Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Extend_From_other_branch.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

        Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto})
        Me.ReportViewer1.RefreshReport()
    End Sub

    Sub ChangeSchoolField()
        Dim cmd As New SqlCommand("SELECT * FROM CHANGE_FIELD_V WHERE CAST(CHANGE_DATE AS DATE)  BETWEEN @from AND @to", DbInterface.Connection)

        cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
        cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
        Dim da As New SqlDataAdapter(cmd)
        Dim dtReport As New DataTable
        da.Fill(dtReport)
        Dim pfrom As New ReportParameter("from", dtpFrom.Value)
        Dim pto As New ReportParameter("to", dtpTo.Value)
        'Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Students_Change_School_Field.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

        Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto})
        Me.ReportViewer1.RefreshReport()
    End Sub
    Sub ChangeSchoolFieldFirstTime()
        Dim cmd As New SqlCommand("SELECT TOP(1)* FROM CHANGE_FIELD_V WHERE CAST(CHANGE_DATE AS DATE)  BETWEEN @from AND @to", DbInterface.Connection)

        cmd.Parameters.Add("@from", SqlDbType.Date).Value = dtpFrom.Value
        cmd.Parameters.Add("@to", SqlDbType.Date).Value = dtpTo.Value
        Dim dtReport As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dtReport)
        Dim pfrom As New ReportParameter("from", dtpFrom.Value)
        Dim pto As New ReportParameter("to", dtpTo.Value)
        'Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Students_Change_School_Field.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

        Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto})
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub TreeView1_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        Try
            Dim i As Integer = CInt(e.Node.Tag)
            If i > 0 Then
                SelectReport(i)
            End If
        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub
End Class