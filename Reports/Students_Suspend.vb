Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class Students_Suspend
    Private Sub ReportView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If ViewerFrm.checkPrint.Checked = True Then
            Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_STUDENT_SUSPEND WHERE CAST(FROM_DATE AS DATE) BETWEEN @from AND @to", DbInterface.Connection)
            Dim da As New SqlDataAdapter(cmd)
            cmd.Parameters.Add("@From", SqlDbType.Date).Value = ViewerFrm.dtpFrom.Value
            cmd.Parameters.Add("@To", SqlDbType.Date).Value = ViewerFrm.dtpTo.Value
            Dim dtReport As New DataTable
            da.Fill(dtReport)
            Dim pfrom As New ReportParameter("from", ViewerFrm.dtpFrom.Value)
            Dim pto As New ReportParameter("to", ViewerFrm.dtpTo.Value)
            Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Students_Suspend.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
            Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto, pDes})
            Me.ReportViewer1.RefreshReport()
        Else
            If ViewerFrm.cboDegree.SelectedValue Is Nothing Then
                Dim groups As String = ""
                For Each obj As Object In ViewerFrm.lstGroup.SelectedItems
                    groups = groups & CType(obj, Group).GroupId & ", "
                Next
                groups = groups.Substring(0, groups.Length - 2) & ""


                Dim groupName As String = ""
                For Each obj As Object In ViewerFrm.lstGroup.SelectedItems
                    groupName = groupName & CType(obj, Group).GroupName & ", "
                Next
                groupName = groupName.Substring(0, groupName.Length - 2) & ""
                Dim Str As String
                Dim dtReport As New DataTable
                If ViewerFrm.chkAllField.Checked = True Then
                    Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_STUDENT_SUSPEND WHERE DEGREE_ID=@degree_id AND SCHOOL_ID=@school_id  AND PROMOTION_ID=@promotion_id AND STAGE_ID=@stage_id AND GROUP_ID IN (" & groups & ") AND TERM_NO= " & Term.GetTermNo(Convert.ToInt32(ViewerFrm.txtYear.Text), Convert.ToInt32(ViewerFrm.txtSemester.Text)) & "  AND CAST(FROM_DATE AS DATE) BETWEEN @from AND @to", DbInterface.Connection)
                    Dim da As New SqlDataAdapter(cmd)
                    cmd.Parameters.Add("@degree_id", SqlDbType.Int).Value = CType(ViewerFrm.cboDegree.SelectedItem, Degree).DegreeId
                    cmd.Parameters.Add("@school_id", SqlDbType.Int).Value = CType(ViewerFrm.cboSchool.SelectedItem, School).SchoolId
                    cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = CType(ViewerFrm.cboPromotion.SelectedItem, Promotion).PromotionId
                    cmd.Parameters.Add("@stage_id", SqlDbType.Int).Value = CType(ViewerFrm.cboStage.SelectedItem, Stage).StageId
                    cmd.Parameters.Add("@From", SqlDbType.Date).Value = ViewerFrm.dtpFrom.Value
                    cmd.Parameters.Add("@To", SqlDbType.Date).Value = ViewerFrm.dtpTo.Value
                    da.Fill(dtReport)
                    Str = "និស្សិតជំនាន់ " & CStr(ViewerFrm.cboPromotion.Text) & "​ វគ្គ ​" & CStr(ViewerFrm.cboStage.Text) & " ឆ្នាំ ​" & CStr(ViewerFrm.txtYear.Text) & " ឆមាស " & CStr(ViewerFrm.txtSemester.Text) & " សិក្សាផ្នែក " & CStr(ViewerFrm.cboSchool.Text) & " ក្រុម " & groupName
                Else
                    Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_STUDENT_SUSPEND WHERE DEGREE_ID=@degree_id AND SCHOOL_ID=@school_id AND FIELD_ID=@field_id AND PROMOTION_ID=@promotion_id AND STAGE_ID=@stage_id AND GROUP_ID IN (" & groups & ") AND TERM_NO= " & Term.GetTermNo(Convert.ToInt32(ViewerFrm.txtYear.Text), Convert.ToInt32(ViewerFrm.txtSemester.Text)) & "  AND CAST(FROM_DATE AS DATE) BETWEEN @from AND @to", DbInterface.Connection)
                    Dim da As New SqlDataAdapter(cmd)
                    cmd.Parameters.Add("@degree_id", SqlDbType.Int).Value = CType(ViewerFrm.cboDegree.SelectedItem, Degree).DegreeId
                    cmd.Parameters.Add("@school_id", SqlDbType.Int).Value = CType(ViewerFrm.cboSchool.SelectedItem, School).SchoolId
                    cmd.Parameters.Add("@field_id", SqlDbType.Int).Value = CType(ViewerFrm.cboField.SelectedItem, Field).FieldId
                    cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = CType(ViewerFrm.cboPromotion.SelectedItem, Promotion).PromotionId
                    cmd.Parameters.Add("@stage_id", SqlDbType.Int).Value = CType(ViewerFrm.cboStage.SelectedItem, Stage).StageId
                    cmd.Parameters.Add("@From", SqlDbType.Date).Value = ViewerFrm.dtpFrom.Value
                    cmd.Parameters.Add("@To", SqlDbType.Date).Value = ViewerFrm.dtpTo.Value
                    da.Fill(dtReport)
                    Str = "និស្សិតជំនាន់ " & CStr(ViewerFrm.cboPromotion.Text) & "​ វគ្គ ​" & CStr(ViewerFrm.cboStage.Text) & " ឆ្នាំ ​" & CStr(ViewerFrm.txtYear.Text) & " ឆមាស " & CStr(ViewerFrm.txtSemester.Text) & " សិក្សាផ្នែក " & CStr(ViewerFrm.cboSchool.Text) & " ជំនាញ ​" & CStr(ViewerFrm.cboField.Text) & " ក្រុម " & groupName
                End If

                Dim pfrom As New ReportParameter("from", ViewerFrm.dtpFrom.Value)
                Dim pto As New ReportParameter("to", ViewerFrm.dtpTo.Value)
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
End Class
