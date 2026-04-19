Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class StudentLetterRpt

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Try
            Dim cmd As New SqlCommand("select * from student_letter_v where issued_date>=@from and issued_date<=@to", DbInterface.Connection)
            Dim da As New SqlDataAdapter(cmd)
            cmd.Parameters.Add("@from", SqlDbType.Date).Value = DateTimePicker1.Value
            cmd.Parameters.Add("@to", SqlDbType.Date).Value = DateTimePicker2.Value
          
            Dim dtReport As New DataTable
            da.Fill(dtReport)

            Dim staff As New ReportParameter("staffname", DbInterface.LogonUser.UserName)
            Dim froms As New ReportParameter("from", DateTimePicker1.Value)
            Dim tos As New ReportParameter("to", DateTimePicker2.Value)


            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.StudentLetterRpt.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

            Me.ReportViewer1.LocalReport.SetParameters({staff, froms, tos})
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdTotal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTotal.Click
        Dim cmd As New SqlCommand("SELECT LETTER_ID,LETTER_NAME,COUNT(LETTER_ID) TOTAL from STUDENT_LETTER_V WHERE CAST(ISSUED_DATE as DATE)>=@FROM AND CAST(ISSUED_DATE as DATE)<=@TO GROUP BY  LETTER_ID,LETTER_NAME ORDER BY LETTER_ID ASC", DbInterface.Connection)
        Dim da As New SqlDataAdapter(cmd)
        cmd.Parameters.Add("@FROM", SqlDbType.Date).Value = DateTimePicker1.Value
        cmd.Parameters.Add("@TO", SqlDbType.Date).Value = DateTimePicker2.Value

        Dim dtReport As New DataTable
        da.Fill(dtReport)

        Dim staff As New ReportParameter("staffname", DbInterface.LogonUser.UserName)
        Dim froms As New ReportParameter("from", DateTimePicker1.Value)
        Dim tos As New ReportParameter("to", DateTimePicker2.Value)


        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.StudentLetterTotalRpt.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

        Me.ReportViewer1.LocalReport.SetParameters({staff, froms, tos})
        Me.ReportViewer1.RefreshReport()
    End Sub
End Class