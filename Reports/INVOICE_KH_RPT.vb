Imports Microsoft.Reporting.WinForms
Imports System.Data.SqlClient

Public Class INVOICE_KH_RPT
    Public invoice_id As String
    Public sex As String
    Public dob As String
    Public phone As String
    Public studytime As String
    Public degree As String
    Public pro As String
    Public stage As String
    Public group As String
    Public semester As String

    Public invoicedate As String
    Public startdate As String
    Public enddate As String
    Public exrate As String
    Public description As String
    Public br_address As String
    Public br_phone As String
    Public br_shortname As String


    Public totaldollar As String
    Public totalriel As String
    Public totalbath As String
    Public totaldistotalother As String
    Public totalgranddollar As String
    Public Totalgrandriel As String
    Public totalgrandbath As String
    Public totalpaydollar As String
    Public totalpayriel As String
    Public totalPayBath As String
    Public amountR As String
    Dim price As String
    Dim qty As String
    Dim return_amount As String
    Dim owe As String
    Dim owe_khr As String
    Dim amount As String
    Dim invoiceno As Integer
    Dim year As Integer
    Dim invno As String
    Dim owe_reason As String = ""

    Private Sub INVOICE_KH_RPT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Dim cmd As New SqlCommand("SELECT * FROM INVOICE_V_REPORT WHERE INVOICE_ID=@INVOICE_ID", DbInterface.Connection)
            Dim da As New SqlDataAdapter(cmd)
            cmd.Parameters.Add("@INVOICE_ID", SqlDbType.VarChar).Value = invoice_id
            Dim dtReport As New DataTable
            da.Fill(dtReport)
            Using dr As SqlDataReader = cmd.ExecuteReader()
                dr.Read()
                If dr.HasRows Then
                    price = dr("PRICE").ToString()
                    qty = dr("QTY").ToString()
                    owe = dr("TOTAL_OWE").ToString()
                    owe_khr = dr("TOTAL_OWE_KHR").ToString()
                    owe_reason = dr("OWE_REASON").ToString()
                    return_amount = dr("RETURN_AMOUNT")
                    invoiceno = CInt(dr("INVOICE_NO"))
                    year = CInt(dr("YEAR_NUMBER"))
                End If
            End Using
            amount = IIf(CDec(owe) > 0, (CInt(qty) * CDec(price)) - CDec(owe), (CInt(qty) * CDec(price)) - CDec(return_amount))
            owe_reason = IIf(owe_reason = "", "ជំពាក់", owe_reason)
            invno = br_shortname + "CI" + invoiceno.ToString("000000") + "/" + DateTime.Now.ToString("yy")
            Dim pbraddress As New ReportParameter("braddress", br_address)
            Dim pbrphone As New ReportParameter("brphone", br_phone)
            Dim pbrshotname As New ReportParameter("brshortname", br_shortname)

            Dim staff As New ReportParameter("staffname", DbInterface.LogonUser.UserName)
            Dim pinvoiceno As New ReportParameter("invoiceno", invno.ToUpper)
            Dim pincoiedate As New ReportParameter("invoicedate", GetDateKhmer(invoicedate))
            Dim psex As New ReportParameter("sex", GetGenderKh(sex))
            Dim pdob As New ReportParameter("dob", GetDateKhmer(dob))
            Dim pphone As New ReportParameter("phone", phone)
            Dim pstudeytime As New ReportParameter("studytime", studytime)
            Dim pdegree As New ReportParameter("degree", GetDegreeKh(degree))
            Dim ppro As New ReportParameter("promotion", pro)
            Dim pstage As New ReportParameter("stage", stage)
            Dim pgroup As New ReportParameter("group", group)
            Dim pexrate As New ReportParameter("exchangerate", exrate)
            Dim pstartdate As New ReportParameter("startdate", GetDateKhmer(startdate)) 
            Dim penddate As New ReportParameter("enddate", GetDateKhmer(enddate))
            Dim powereason As New ReportParameter("owereson", owe_reason)
            Dim pdescription As New ReportParameter("description", description)
            Dim pamount As New ReportParameter("amount", amount)


            Dim ptotaldiscount As New ReportParameter("totaldiscount", totaldistotalother)
            Dim ptotalgranddollar As New ReportParameter("grandtotaldollar", totalgranddollar)
            Dim grandtotalreil As Decimal = Math.Round(Totalgrandriel / 100) * 100

            Dim ptotalgrandriel As New ReportParameter("grandtotalreil", grandtotalreil.ToString("###,000.00R"))
            Dim ptotalpaydollar As New ReportParameter("paydollar", totalpaydollar)
            Dim payreil As Decimal = Math.Round(totalpayriel / 100) * 100
            Dim ptotalpayreil As New ReportParameter("payreil", payreil.ToString("###,000.00R"))

            Dim report As String = "bbusystem.INVOICE_KH_RPT.rdlc"
            If CDec(owe) > 0 OrElse CDec(owe_khr) > 0 Then
                report = "bbusystem.INVOICE_KH_OWE_RPT.rdlc"
            End If
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = report
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
            Me.ReportViewer1.LocalReport.SetParameters({staff, pinvoiceno, pincoiedate, psex, pdob, pphone, pstudeytime, pdegree, ppro,
                                                       pstage, pgroup, penddate, pstartdate, pexrate, pbraddress, pbrphone,
                                                       pbrshotname, powereason, pdescription, pamount, ptotaldiscount,
                                                       ptotalgranddollar, ptotalgrandriel, ptotalpaydollar, ptotalpayreil})
            Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function GetDateKhmer(ByVal pdtpdate As Date) As String

        Dim day As String = pdtpdate.ToString("dd")
        Dim monthnaneenglish As String = pdtpdate.ToString("MMM")
        Dim year As String = pdtpdate.ToString("yyyy")

        Dim monthnamekhmer As String = ""
        Select Case monthnaneenglish
            Case "Jan"
                monthnamekhmer = "មករា"
            Case "Feb"
                monthnamekhmer = "កុម្ភៈ"
            Case "Mar"
                monthnamekhmer = "មីនា"
            Case "Apr"
                monthnamekhmer = "មេសា"
            Case "May"
                monthnamekhmer = "ឧសភា"
            Case "Jun"
                monthnamekhmer = "មិថុនា"
            Case "Jul"
                monthnamekhmer = "កក្កដា"
            Case "Aug"
                monthnamekhmer = "សីហា"
            Case "Sep"
                monthnamekhmer = "កញ្ញា"
            Case "Oct"
                monthnamekhmer = "តុលា"
            Case "Nov"
                monthnamekhmer = "វិច្ឆិកា"
            Case "Dec"
                monthnamekhmer = "ធ្នូ"
        End Select
        Return day + " " + monthnamekhmer + " " + year
    End Function
    Private Function GetGenderKh(ByVal sex As String) As String
        Dim gender As String
        If sex = "Male" Then
            gender = "ប្រុស"
        Else
            gender = "ស្រី"
        End If
        Return gender
    End Function

    Private Function GetDegreeKh(ByVal degree As String) As String
        Dim degr As String = ""
        If degree = "Bachelor" Then
            degr = "បរិញ្ញាបត្រ"
        ElseIf degree = "Associate" Then
            degr = "បរិញ្ញាបត្ររង"
        ElseIf degree = "Master" Then
            degr = "បរិញ្ញាបត្រជាន់ខ្ពស់"
        ElseIf degree = "Doctor" Then
            degr = "បណ្ឌិត"
        Else
            degr = "បាក់ឌុប"
        End If
        Return degr
    End Function
End Class