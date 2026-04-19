Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class DailyBookingRpt

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker2.ValueChanged
        LoadReport()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        LoadReport()
    End Sub
    Private Sub LoadReport()
        Try
            Dim cmd As New SqlCommand("select * from booking_v where bookingdate>=@from and bookingdate<=@to", DbInterface.Connection)
            Dim da As New SqlDataAdapter(cmd)
            cmd.Parameters.Add("@from", SqlDbType.Date).Value = DateTimePicker1.Value
            cmd.Parameters.Add("@to", SqlDbType.Date).Value = DateTimePicker2.Value
            Dim dtReport As New DataTable
            da.Fill(dtReport)


            Dim staff As New ReportParameter("staffname", DbInterface.LogonUser.UserName)
            Dim froms As New ReportParameter("from", FullDateTimeKhmer.GetFullDateTimeKhmer(DateTimePicker1.Value))
            Dim tos As New ReportParameter("to", FullDateTimeKhmer.GetFullDateTimeKhmer(DateTimePicker2.Value))

            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.DailyBookingRpt.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

            Me.ReportViewer1.LocalReport.SetParameters({staff, froms, tos})
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DailyBookingRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadReport()
        BoundCbo(cboDegree, "select degree_id,degree from degree")
        BoundCbo(cboSchool, "select school_id,school_name from school")
        cboDegree.Enabled = False
        cboSchool.Enabled = False
    End Sub

    
    Private Sub LoadReportFilter()
        Try
            Dim cmd As New SqlCommand("select * from booking_v where bookingdate>=@from and bookingdate<=@to  and degree=@degree and school_name=@school", DbInterface.Connection)
            Dim da As New SqlDataAdapter(cmd)
            cmd.Parameters.Add("@from", SqlDbType.Date).Value = DateTimePicker1.Value
            cmd.Parameters.Add("@to", SqlDbType.Date).Value = DateTimePicker2.Value
            cmd.Parameters.Add("@degree", SqlDbType.NVarChar).Value = cboDegree.Text
            cmd.Parameters.Add("@school", SqlDbType.NVarChar).Value = cboSchool.Text
            Dim dtReport As New DataTable
            da.Fill(dtReport)



            Dim staff As New ReportParameter("staffname", DbInterface.LogonUser.UserName)
            Dim froms As New ReportParameter("from", FullDateTimeKhmer.GetFullDateTimeKhmer(DateTimePicker1.Value))
            Dim tos As New ReportParameter("to", FullDateTimeKhmer.GetFullDateTimeKhmer(DateTimePicker2.Value))


            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.DailyBookingRpt.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

            Me.ReportViewer1.LocalReport.SetParameters({staff, froms, tos})
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        LoadReportFilter()
    End Sub

    Private Sub chkBy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBy.CheckedChanged
        If chkBy.Checked = True Then
            cboDegree.Enabled = True
            cboSchool.Enabled = True
        Else
            cboDegree.Enabled = False
            cboSchool.Enabled = False
        End If
    End Sub

    Private Sub btnSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSummary.Click
        Try
            Dim cmd As New SqlCommand("SELECT DEGREE,SCHOOL_ID,SCHOOL_NAME,COUNT(BOOKINGID)as TOTAL FROM BOOKING_RPT GROUP BY DEGREE,SCHOOL_ID,SCHOOL_NAME ORDER BY DEGREE,SCHOOL_NAME ASC ", DbInterface.Connection)
            Dim da As New SqlDataAdapter(cmd)
            'cmd.Parameters.Add("@from", SqlDbType.Date).Value = DateTimePicker1.Value
            'cmd.Parameters.Add("@to", SqlDbType.Date).Value = DateTimePicker2.Value
            'cmd.Parameters.Add("@degree", SqlDbType.NVarChar).Value = cboDegree.Text
            'cmd.Parameters.Add("@school", SqlDbType.NVarChar).Value = cboSchool.Text
            Dim dtReport As New DataTable
            da.Fill(dtReport)

            Dim staff As New ReportParameter("staffname", DbInterface.LogonUser.UserName)
            'Dim froms As New ReportParameter("from", DateTimePicker1.Value)
            'Dim tos As New ReportParameter("to", DateTimePicker2.Value)


            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.DailyBookingSummaryRpt.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

            Me.ReportViewer1.LocalReport.SetParameters({staff})
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub
End Class