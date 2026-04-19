Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class AdminAllStudentsViewer
    Dim de As New Degrees
    Dim pro As New Promotions
    Dim st As New Stages
    Private Sub AdminAllStudentsViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dgvDegree.DataSource = de.SelectRecords().Tables(0)
        dgvDegree.Columns("DEGREE_ID").Visible = False
    End Sub
    Private Sub dgvDegree_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDegree.CellClick

       

        Dim row As String = ""
        Dim rows As String = "["
        For Each d As DataGridViewRow In dgvDegree.Rows
            row = d.Cells("DEGREE_ID").Value '& ","
            If rows.Length <= 1 Then
                rows = rows & "" & row
            Else
                rows = rows & "," & row
            End If
        Next
        rows = rows.Substring(0, rows.Length - 2) & "]"



        dgvPromotion.DataSource = pro.SelectRecords(dgvDegree.CurrentRow.Cells("DEGREE_ID").Value).Tables(0)
    End Sub

    Private Sub dgvPromotion_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPromotion.CellClick
        dgvState.DataSource = st.SelectRecords(dgvPromotion.CurrentRow.Cells("PROMOTION_NO").Value, dgvDegree.CurrentRow.Cells("DEGREE_ID").Value).Tables(0)
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If chkTerm.Checked Then
            Dim cmd As New SqlCommand("SELECT COUNT(CASE WHEN SEX='Male' THEN 1 END) AS MALE,COUNT(CASE WHEN SEX='Female' THEN 1 END) AS FEMALE,COUNT(*) AS TOTAL,SCHOOL_NAME FROM dbo.V_ADMIN_REPORT_LIST_OF_STUDENT WHERE PROMOTION_NO =@PRONO AND STAGE_NO=@STAGENO AND DEGREE_ID=@DEGREE_ID AND TERM_NO=@TERM_NO GROUP BY SCHOOL_NAME", DbInterface.Connection)
            Dim da As New SqlDataAdapter(cmd)
            cmd.Parameters.Add("@PRONO", SqlDbType.Int).Value = dgvPromotion.CurrentRow.Cells("PROMOTION_NO").Value
            cmd.Parameters.Add("@STAGENO", SqlDbType.Int).Value = dgvState.CurrentRow.Cells("STAGE_NO").Value
            cmd.Parameters.Add("@DEGREE_ID", SqlDbType.Int).Value = dgvDegree.CurrentRow.Cells("DEGREE_ID").Value
            cmd.Parameters.Add("@TERM_NO", SqlDbType.Int).Value = cboTerm.Text
            Dim dtReport As New DataTable
            da.Fill(dtReport)
            'Dim pfrom As New ReportParameter("from", ViewerFrm.dtpFrom.Value)
            'Dim pto As New ReportParameter("to", ViewerFrm.dtpTo.Value)
            'Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.AdminAllStudents.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
            'Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto})
            Me.ReportViewer1.RefreshReport()
        Else
            Dim cmd As New SqlCommand("SELECT COUNT(CASE WHEN SEX='Male' THEN 1 END) AS MALE,COUNT(CASE WHEN SEX='Female' THEN 1 END) AS FEMALE,COUNT(*) AS TOTAL,SCHOOL_NAME FROM dbo.V_ADMIN_REPORT_LIST_OF_STUDENT WHERE PROMOTION_NO =@PRONO AND STAGE_NO=@STAGENO AND DEGREE_ID=@DEGREE_ID GROUP BY SCHOOL_NAME", DbInterface.Connection)
            Dim da As New SqlDataAdapter(cmd)
            cmd.Parameters.Add("@PRONO", SqlDbType.Int).Value = dgvPromotion.CurrentRow.Cells("PROMOTION_NO").Value
            cmd.Parameters.Add("@STAGENO", SqlDbType.Int).Value = dgvState.CurrentRow.Cells("STAGE_NO").Value
            cmd.Parameters.Add("@DEGREE_ID", SqlDbType.Int).Value = dgvDegree.CurrentRow.Cells("DEGREE_ID").Value
            Dim dtReport As New DataTable
            da.Fill(dtReport)
            'Dim pfrom As New ReportParameter("from", ViewerFrm.dtpFrom.Value)
            'Dim pto As New ReportParameter("to", ViewerFrm.dtpTo.Value)
            'Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.AdminAllStudents.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
            'Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto})
            Me.ReportViewer1.RefreshReport()
        End If
    End Sub

   
End Class