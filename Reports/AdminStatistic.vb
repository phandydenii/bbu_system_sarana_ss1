Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class AdminStatistic

    Private Sub TotalStudentAdmin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BoundCbo(cboPromotion, "select 0 ID,Promotion_NO from promotion group by promotion_no order by Promotion_no asc")
        BoundCbo(cboPromotionCerti, "select 0 ID,Promotion_NO from promotion group by promotion_no order by Promotion_no asc")
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Try
            Dim cmd As New SqlCommand("STATISTIC_SP", DbInterface.Connection)
            Dim da As New SqlDataAdapter(cmd)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@PROMOTION_NO", SqlDbType.Int).Value = cboPromotion.Text
            cmd.Parameters.Add("@TERM_NO", SqlDbType.Int).Value = cboTerm.Text
            Dim dtReport As New DataTable
            da.Fill(dtReport)

            'Dim staff As New ReportParameter("staffname", DbInterface.LogonUser.UserName)
            'Dim froms As New ReportParameter("from", DateTimePicker1.Value)
            'Dim tos As New ReportParameter("to", DateTimePicker2.Value)

            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.AdminStatistic.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

            'Me.ReportViewer1.LocalReport.SetParameters({staff, froms, tos})
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkAllpro_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllpro.CheckedChanged
        If chkAllpro.Checked = True Then
            cboPromotionCerti.Visible = False
        Else
            cboPromotionCerti.Visible = True
        End If
    End Sub

    Private Sub btnNotYet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNotYet.Click
        Try
            Dim dtReport As New DataTable
            If chkAllpro.Checked Then
                Dim cmd As New SqlCommand(" SELECT * FROM V_STUDENT_NOT_YET_TAKE WHERE IS_accept_certificate=0", DbInterface.Connection)
                Dim da As New SqlDataAdapter(cmd)
                'cmd.Parameters.Add("@PROMOTION_NO", SqlDbType.Int).Value = cboPromotion.Text
                da.Fill(dtReport)

            Else
                Dim cmd As New SqlCommand(" SELECT * FROM V_STUDENT_NOT_YET_TAKE WHERE IS_accept_certificate=0 AND PROMOTION_NO=@PROMOTION_NO", DbInterface.Connection)
                Dim da As New SqlDataAdapter(cmd)
                cmd.Parameters.Add("@PROMOTION_NO", SqlDbType.Int).Value = cboPromotionCerti.Text
                da.Fill(dtReport)
            End If

            'Dim staff As New ReportParameter("staffname", DbInterface.LogonUser.UserName)
            'Dim froms As New ReportParameter("from", DateTimePicker1.Value)
            'Dim tos As New ReportParameter("to", DateTimePicker2.Value)

            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.AdminGraduateNotYet.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

            'Me.ReportViewer1.LocalReport.SetParameters({staff, froms, tos})
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAlready_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAlready.Click
        Try
            Dim dtReport As New DataTable
            If chkAllpro.Checked Then
                Dim cmd As New SqlCommand(" SELECT * FROM V_STUDENT_NOT_YET_TAKE WHERE IS_accept_certificate=1 AND ACCEPT_DATE>=@FROM AND ACCEPT_DATE<=@TO", DbInterface.Connection)
                Dim da As New SqlDataAdapter(cmd)
                cmd.Parameters.Add("@FROM", SqlDbType.Date).Value = dtpFrom.Value
                cmd.Parameters.Add("@TO", SqlDbType.Date).Value = dtpTo.Value
                da.Fill(dtReport)

            Else
                Dim cmd As New SqlCommand(" SELECT * FROM V_STUDENT_NOT_YET_TAKE WHERE IS_accept_certificate=1 AND PROMOTION_NO=@PROMOTION_NO AND ACCEPT_DATE>=@FROM AND ACCEPT_DATE<=@TO", DbInterface.Connection)
                Dim da As New SqlDataAdapter(cmd)
                cmd.Parameters.Add("@PROMOTION_NO", SqlDbType.Int).Value = cboPromotionCerti.Text
                cmd.Parameters.Add("@FROM", SqlDbType.Date).Value = dtpFrom.Value
                cmd.Parameters.Add("@TO", SqlDbType.Date).Value = dtpTo.Value
                da.Fill(dtReport)
            End If

            'Dim staff As New ReportParameter("staffname", DbInterface.LogonUser.UserName)
            'Dim froms As New ReportParameter("from", DateTimePicker1.Value)
            'Dim tos As New ReportParameter("to", DateTimePicker2.Value)

            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.AdminGraduateNotYet.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

            'Me.ReportViewer1.LocalReport.SetParameters({staff, froms, tos})
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception

        End Try
    End Sub
End Class