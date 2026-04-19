Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class RESTUDY_BY_STUDENT_RPT
    Public studentid As String
    Private Sub RESTUDY_BY_STUDENT_RPT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dtReport As New DataTable
        Dim cmd As New SqlCommand()
        cmd = New SqlCommand("SELECT * FROM V_STUDENT_RESTUDY_RESULT WHERE STUDENT_ID='" + studentid + "' order by TERM_NO", DbInterface.Connection)

        Dim da As New SqlDataAdapter(cmd) 
        da.Fill(dtReport)

        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.RESTUDY_BY_STUDENT_RPT.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
        'Me.ReportViewer1.LocalReport.SetParameters({pyear})
        Me.ReportViewer1.RefreshReport()
    End Sub
End Class