Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Public Class STUDENT_CARD_FRM
    Public studentid As String = ""
    Private Sub STUDENT_CARD_FRM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmd As New SqlCommand("SELECT * FROM V_STUDENT_CARD where  STUDENT_ID=@id", DbInterface.Connection)
        cmd.Parameters.AddWithValue("@id", studentid)
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)
        Dim dd As String = getKhmerNum(Now.Day, True)
        Dim mm As String = GetMonthKhmer(Now)
        Dim yy As String = getKhmerNum(Now.Year, True)

        Dim dobkm As String = ""
        Dim dr As SqlDataReader = cmd.ExecuteReader
        If dr.Read Then
            dobkm = GetDateKhmer(Convert.ToDateTime(dr("DATE_OF_BIRTH")))
        End If

        Dim branch_and_date As String = $"{GetBranchName(Utilities.BranchName, True)}, ថ្ងៃទី{dd} ខែ{mm} ឆ្នាំ{yy}"


        Dim pdobkm As New ReportParameter("dobkm", dobkm)
        Dim pbranch_and_print_date As New ReportParameter("branch_and_print_date", branch_and_date)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.STUDENT_CARD_RPT.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dt))
        Me.ReportViewer1.LocalReport.SetParameters({pbranch_and_print_date, pdobkm})
        ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.RefreshReport()
        Me.ReportViewer1.RefreshReport()
    End Sub
End Class