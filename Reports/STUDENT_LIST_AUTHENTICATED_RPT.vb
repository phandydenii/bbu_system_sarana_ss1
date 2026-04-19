Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Public Class STUDENT_LIST_AUTHENTICATED_RPT

    Public groupid As Integer
    Public termno As Integer

    Private Sub STUDENT_LIST_AUTHENTICATED_RPT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dtReport As New DataTable
        Dim cmd As New SqlCommand()

        'cmd = New SqlCommand("SELECT * FROM V_REPORT_LIST_OF_STUDENT_IS_AUTHENTICATED WHERE GROUP_ID=@group AND TERM_NO=@term_no AND IS_AUTHENTICATED=1 order by STUDENT_NAME", DbInterface.Connection)
        'cmd = New SqlCommand("SELECT * FROM V_REPORT_LIST_OF_STUDENT_IS_AUTHENTICATED WHERE GROUP_ID=@group AND TERM_NO=@term_no   order by STUDENT_NAME", DbInterface.Connection)
        cmd = New SqlCommand("SELECT * FROM V_STUDENT WHERE GROUP_ID=@group AND TERM_NO=@term_no   order by STUDENT_NAME", DbInterface.Connection)

        Dim da As New SqlDataAdapter(cmd) 
        cmd.Parameters.Add("@group", SqlDbType.Int).Value = groupid
        cmd.Parameters.Add("@term_no", SqlDbType.Int).Value = termno
        da.Fill(dtReport)

        Dim count As Integer = 0
        Dim count_male As Integer = 0
        Dim count_female As Integer = 0

        Dim str As String = ""

        For Each dr As DataRow In dtReport.Rows
            count += 1
            If dr("Sex") = "Male" Then
                count_male += 1
            Else
                count_female += 1
            End If
        Next
        str = "បញ្ជីនេះបានបញ្ចប់ត្រឹមនិស្សិតសរុប " & count & "​ នាក់ ក្នុងនោះមាននិស្សិតស្រី " & count_female & " នាក់ និងនិស្សិតប្រុស " & count_male & " នាក់"
        Dim pstring As New ReportParameter("str", str)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.STUDENT_LIST_AUTHENTICATED_RPT.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
        Me.ReportViewer1.LocalReport.SetParameters({pstring})
        Me.ReportViewer1.RefreshReport()
    End Sub
End Class