Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Public Class INVOICE_KH_ENG_FRM
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
    Public invoiceno As String
    Public invoicedate As String
    Public startdate As String
    Public enddate As String
    Public exrate As String
    Public owe_reason As String
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
    Dim amount As String

    Private Sub FormPrintInvioceNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
                    owe = dr("OWE").ToString()
                    return_amount = dr("RETURN_AMOUNT")
                End If
            End Using
            amount = IIf(CDec(owe) > 0, (CInt(qty) * CDec(price)) - CDec(owe), (CInt(qty) * CDec(price)) - CDec(return_amount))

            Dim staff As New ReportParameter("staffname", DbInterface.LogonUser.UserName)
            Dim pinvoiceno As New ReportParameter("InvoiceNo", invoiceno)
            Dim pincoiedate As New ReportParameter("InvoiceDate", GetDateKhmer(invoicedate))
            Dim pincoiedatee As New ReportParameter("InvoiceDateE", invoicedate)
            Dim psex As New ReportParameter("Sex", GetGenderKh(sex))
            Dim psexe As New ReportParameter("SexE", sex)
            Dim pdob As New ReportParameter("DoB", GetDateKhmer(dob))
            Dim pdobe As New ReportParameter("DoBE", dob)
            Dim pphone As New ReportParameter("Phone", phone)
            Dim pstudeytime As New ReportParameter("StudyTime", studytime)
            Dim pdegree As New ReportParameter("Degree", GetDegreeKh(degree))
            Dim pdegreeE As New ReportParameter("DegreeE", degree)
            Dim ppro As New ReportParameter("Promotion", pro)
            Dim pstage As New ReportParameter("Stage", stage)
            Dim pgroup As New ReportParameter("Group", group)
            Dim psemester As New ReportParameter("Semester", semester)
            Dim penddate As New ReportParameter("EndDate", GetDateKhmer(enddate))
            Dim penddatee As New ReportParameter("EndDateE", enddate)
            Dim pstartdate As New ReportParameter("StartDate", GetDateKhmer(startdate))
            Dim pstartdatee As New ReportParameter("StartDateE", startdate)
            Dim pexrate As New ReportParameter("ExRate", exrate)
            Dim pbraddress As New ReportParameter("BrAddress", br_address)
            Dim pbrphone As New ReportParameter("BrPhone", br_phone)
            Dim pbrshotname As New ReportParameter("BrShortName", br_shortname)
            Dim powereason As New ReportParameter("OweReason", owe_reason)
            Dim pdescription As New ReportParameter("Description", description)
            Dim pamount As New ReportParameter("Amount", amount)


            Dim ptotaldollar As New ReportParameter("TOTALDOLLAR", totaldollar)
            Dim ptotalriel As New ReportParameter("TOTALRIEL", totalriel)
            Dim ptotalbart As New ReportParameter("TOTALBART", totalbath)
            Dim ptotaldiscount As New ReportParameter("TOTALDISCOUNT", totaldistotalother)
            Dim ptotalgranddollar As New ReportParameter("TOTAL_GRAND_DOLLAR", totalgranddollar)
            Dim ptotalgrandriel As New ReportParameter("TOTAL_GRAND_RAIL", Totalgrandriel)
            Dim ptotalgrandbart As New ReportParameter("TOTAL_GRAND_BATH", totalgrandbath)
            Dim ptotalpaydollar As New ReportParameter("TOTALPAYDOLLAR", totalpaydollar)
            Dim ptotalpayreil As New ReportParameter("TOTALPAYREIL", totalpayriel)
            Dim ptotalpaybath As New ReportParameter("TOTALPAYBATH", totalPayBath)

            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.INVOICE_KH_ENG_RPT.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
            Me.ReportViewer1.LocalReport.SetParameters({staff, pinvoiceno, pincoiedate, pincoiedatee, psex, psexe, pdob, pdobe, pphone, pstudeytime, pdegree, pdegreeE, ppro,
                                                        pstage, pgroup, psemester, penddate, penddatee, pstartdate, pstartdatee, pexrate, pbraddress, pbrphone,
                                                        pbrshotname, powereason, pdescription, pamount, ptotaldollar, ptotalbart, ptotalriel, ptotaldiscount,
                                                        ptotalgranddollar, ptotalgrandbart, ptotalgrandriel, ptotalpaybath, ptotalpaydollar, ptotalpayreil})
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception

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