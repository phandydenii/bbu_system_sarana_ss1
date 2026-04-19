Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class BookRpt

    Public bookid As String

    Public braddress As String
    Public brphone As String
    Public group As String

    Public dob As String
    Public startdate As String
    Public invdate As String
    Public enddate As String
    Private Sub BookRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim cmd As New SqlCommand("select * from booking_v where bookingid=@bookingid", DbInterface.Connection)
            Dim da As New SqlDataAdapter(cmd)
            cmd.Parameters.Add("@bookingid", SqlDbType.NVarChar).Value = bookid
            Dim dtReport As New DataTable
            da.Fill(dtReport)

            Dim pbrname As New ReportParameter("brphone", brphone)
            Dim pbrphone As New ReportParameter("branchaddress", braddress)
            Dim pgroup As New ReportParameter("group", group)

            Dim pdob As New ReportParameter("dob", GetDateKhmer(dob))
            Dim pinvoicedate As New ReportParameter("invoicedate", GetDateKhmer(invdate))
            Dim pstartdate As New ReportParameter("startdate", GetDateKhmer(startdate))
            Dim penddate As New ReportParameter("enddate", GetDateKhmer(enddate))

            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.BookRpt.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

            Me.ReportViewer1.LocalReport.SetParameters({pbrname, pbrphone, pgroup, pdob, pinvoicedate, pstartdate, penddate})
            Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class