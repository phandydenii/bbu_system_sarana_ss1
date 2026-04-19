Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class Change_Branch_In

    Private Sub Change_Branch_In_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cmd As New SqlCommand("SELECT * FROM CHANGE_BRANCH_V WHERE CAST(RETURN_DATE AS DATE) BETWEEN @from AND @to", DbInterface.Connection)
        Dim da As New SqlDataAdapter(cmd)
        cmd.Parameters.Add("@From", SqlDbType.Date).Value = ViewerFrm.dtpFrom.Value
        cmd.Parameters.Add("@To", SqlDbType.Date).Value = ViewerFrm.dtpTo.Value
        Dim dtReport As New DataTable
        da.Fill(dtReport)
        Dim pfrom As New ReportParameter("from", ViewerFrm.dtpFrom.Value)
        Dim pto As New ReportParameter("to", ViewerFrm.dtpTo.Value)
        'Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Change_Branch_In.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

        Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto})
        Me.ReportViewer1.RefreshReport()
    End Sub
End Class
