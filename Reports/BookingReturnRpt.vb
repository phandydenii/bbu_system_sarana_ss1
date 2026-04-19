Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class BookingReturnRpt
    Public bookreturnid As String

    Public braddress As String
    Public brphone As String

    Public dob As String
    Public degree As String
    Public startdate As String
    Public invdate As String
    Public enddate As String
    Public group As String
    Private Sub BookingReturnRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim cmd As New SqlCommand("select * from BOOKING_RETURN_V where BOOKINGRETURN_ID=@bookreturnid", DbInterface.Connection)
            Dim da As New SqlDataAdapter(cmd)
            cmd.Parameters.Add("@bookreturnid", SqlDbType.NVarChar).Value = bookreturnid
            Dim dtReport As New DataTable
            da.Fill(dtReport)

            Dim pbrphone As New ReportParameter("branchaddress", braddress)
            Dim pbraddress As New ReportParameter("branchphone", brphone)

            Dim pdob As New ReportParameter("dob", GetDateKhmer(dob))
            Dim pdegree As New ReportParameter("degree", GetDegreeKh(degree))
            Dim pstartdate As New ReportParameter("startdate", GetDateKhmer(startdate))
            Dim pinvdate As New ReportParameter("invdate", GetDateKhmer(invdate))
            Dim penddate As New ReportParameter("enddate", GetDateKhmer(enddate))
            Dim pgroup As New ReportParameter("group", group)

            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.BookingReturnRpt.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
            Me.ReportViewer1.LocalReport.SetParameters({pbraddress, pbrphone, pdob, pdegree, pstartdate,
                                                        pinvdate, penddate, pgroup})
            'Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Function GetDegreeKh(ByVal degree As String) As String
        Dim degr As String = ""
        degree = degree.Trim
        If degree = "Bachelor" Then
            degr = "បរិញ្ញាបត្រ"
        ElseIf degree = "Associate" Then
            degr = "បរិញ្ញាបត្ររង"
        ElseIf degree = "Master" Then
            degr = "បរិញ្ញាបត្រជាន់ខ្ពស់"
        ElseIf degree = "Doctoral" Then
            degr = "បណ្ឌិត"
        ElseIf degree = "Diploma" Then
            degr = "បាក់ឌុប"
        End If
        Return degr
    End Function
End Class