Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class STUDENT_PAYMENT_NEW_RPT
    Public fromdate As DateTime
    Public todate As DateTime

    Public all As Boolean = True
    Public online As Boolean = False
    Public offline As Boolean = False
    Public export As Boolean = False
    Private Sub STUDENT_PAYMENT_NEW_RPT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dtReport As New DataTable
        Dim cmd As New SqlCommand()
        If export Then
            cmd = New SqlCommand($"SELECT * FROM INVOICE_V_REPORT_WEBILL WHERE INVOICE_DATE BETWEEN @fromdate AND @todate order by invoice_id desc", DbInterface.Connection)
            Dim pfromdate As New ReportParameter("fromdate", fromdate.ToString("yyyy-MM-dd HH:mm:ss"))
            Dim ptodate As New ReportParameter("todate", todate.ToString("yyyy-MM-dd HH:mm:ss"))

            Dim da As New SqlDataAdapter(cmd)
            cmd.Parameters.Add("@fromdate", SqlDbType.VarChar).Value = fromdate.ToString("yyyy-MM-dd HH:mm:ss")
            cmd.Parameters.Add("@todate", SqlDbType.VarChar).Value = todate.ToString("yyyy-MM-dd HH:mm:ss")
            da.Fill(dtReport)
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.INVOICE_KH_WEBILL_RPT.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
            Me.ReportViewer1.RefreshReport()
        Else
            Dim str As String = ""
            If all Then
                str = ""
            ElseIf offline Then
                str = " and PAY_ON_APP=0"
            ElseIf online Then
                str = " and PAY_ON_APP=1"
            End If

            cmd = New SqlCommand($"SELECT * FROM INVOICE_REP_V WHERE INVOICE_DATE BETWEEN @fromdate AND @todate {str} order by invoice_id desc", DbInterface.Connection)
            Dim pfromdate As New ReportParameter("fromdate", fromdate.ToString("yyyy-MM-dd HH:mm:ss"))
            Dim ptodate As New ReportParameter("todate", todate.ToString("yyyy-MM-dd HH:mm:ss"))

            Dim da As New SqlDataAdapter(cmd)
            cmd.Parameters.Add("@fromdate", SqlDbType.VarChar).Value = fromdate.ToString("yyyy-MM-dd HH:mm:ss")
            cmd.Parameters.Add("@todate", SqlDbType.VarChar).Value = todate.ToString("yyyy-MM-dd HH:mm:ss")
            da.Fill(dtReport)
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.STUDENT_PAYMENT_NEW_RPT.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
            Me.ReportViewer1.LocalReport.SetParameters({pfromdate, ptodate})
            Me.ReportViewer1.RefreshReport()
        End If

    End Sub

End Class