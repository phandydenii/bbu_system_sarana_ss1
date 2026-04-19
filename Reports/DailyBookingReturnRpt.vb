Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class DailyBookingReturnRpt

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker2.ValueChanged
        LoadReport()
    End Sub
    Private Sub LoadReport()
        Try
            Dim cmd As New SqlCommand("select * from BOOKINGRETURN_RPT where return_date>=@from and return_date<=@to", DbInterface.Connection)
            Dim da As New SqlDataAdapter(cmd)
            cmd.Parameters.Add("@from", SqlDbType.Date).Value = DateTimePicker1.Value
            cmd.Parameters.Add("@to", SqlDbType.Date).Value = DateTimePicker2.Value
            Dim dtReport As New DataTable
            da.Fill(dtReport)

            Dim staff As New ReportParameter("staffname", DbInterface.LogonUser.UserName)
            Dim froms As New ReportParameter("from", FullDateTimeKhmer.GetFullDateTimeKhmer(DateTimePicker1.Value))
            Dim tos As New ReportParameter("to", FullDateTimeKhmer.GetFullDateTimeKhmer(DateTimePicker2.Value))

            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.DailyBookingReturnRpt.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

            Me.ReportViewer1.LocalReport.SetParameters({staff, froms, tos})
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        LoadReport()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim cmd As New SqlCommand("select * from BOOKINGRETURN_RPT where return_date>=@from and return_date<=@to and BOOKINGID not in (SELECT BOOKINGID from BOOKINGDETAIL_TBL)", DbInterface.Connection)
            Dim da As New SqlDataAdapter(cmd)
            cmd.Parameters.Add("@from", SqlDbType.Date).Value = DateTimePicker1.Value
            cmd.Parameters.Add("@to", SqlDbType.Date).Value = DateTimePicker2.Value
            Dim dtReport As New DataTable
            da.Fill(dtReport)

            Dim staff As New ReportParameter("staffname", DbInterface.LogonUser.UserName)
            Dim froms As New ReportParameter("from", FullDateTimeKhmer.GetFullDateTimeKhmer(DateTimePicker1.Value))
            Dim tos As New ReportParameter("to", FullDateTimeKhmer.GetFullDateTimeKhmer(DateTimePicker2.Value))

            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.DailyBookingReturnRpt.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

            Me.ReportViewer1.LocalReport.SetParameters({staff, froms, tos})
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception

        End Try
    End Sub
End Class