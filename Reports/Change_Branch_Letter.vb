
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Public Class Change_Branch_Letter
    Public studentid As String
    Public changebranchid As String
    Dim studentname As String
    Dim degree As String
    Dim sex As String
    Dim dob As String
    Dim school As String
    Dim field As String
    Dim pro As String
    Dim stage As String
    Dim year As String
    Dim semester As String
    Dim group As String
    Dim branch As New Branch
    Public Sub setdatabr(ByVal brid As String, ByVal yr As String, ByVal sem As String, ByVal grp As String)
        changebranchid = brid
        year = yr
        semester = sem
        group = grp
    End Sub

    Private Sub Change_Branch_Letter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            branch = Branches.GetBranchByShortName(Utilities.BranchName)
            Dim br As String = branch.BranchNameInKhmer
            Dim province As String = branch.BranchShortName
            Dim cmd As New SqlCommand("SELECT * FROM CHANGE_BRANCH_V WHERE CHANGE_BRANCH_ID=@CHANGE_BRANCH_ID", DbInterface.Connection)
            Dim da As New SqlDataAdapter(cmd)
            cmd.Parameters.Add("@CHANGE_BRANCH_ID", SqlDbType.VarChar).Value = changebranchid

            Dim dtReport As New DataTable
            da.Fill(dtReport)
            Using dr As SqlDataReader = cmd.ExecuteReader()
                dr.Read()
                If dr.HasRows Then
                    degree = dr("DEGREE_ID").ToString()
                    dob = dr("DATE_OF_BIRTH").ToString()
                    sex = dr("SEX").ToString()
                    pro = dr("PROMOTION_ID").ToString()
                    stage = dr("STAGE_ID").ToString()
                End If
            End Using


            Dim pdegree As New ReportParameter("degree", GetDegreeKh(degree))
            Dim pdob As New ReportParameter("dob", GetDateKhmer(dob))
            Dim psex As New ReportParameter("sex", GetGenderKh(sex))
            Dim pfrombr As New ReportParameter("frombr", br)
            Dim pprovince As New ReportParameter("province", GetProvinceName(province))
            Dim pyear As New ReportParameter("year", ConvertToKhmerNumber(year))
            Dim psemester As New ReportParameter("semester", ConvertToKhmerNumber(semester))
            Dim pgroup As New ReportParameter("group", group)
            Dim ppro As New ReportParameter("pro", ConvertToKhmerNumber(pro))
            Dim pstage As New ReportParameter("stage", ConvertToKhmerNumber(stage))

            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.Change_Branch_Letter.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
            Me.ReportViewer1.LocalReport.SetParameters({pdegree, psex, pdob, pfrombr, pyear, psemester, pgroup, pprovince, ppro, pstage})
            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception

        End Try

    End Sub

    Private Function GetDateKhmer(ByVal pdtpdate As DateTime) As String

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
        Return ConvertToKhmerNumber(day) + " " + monthnamekhmer + " " + ConvertToKhmerNumber(year)
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

    Private Function GetProvinceName(ByVal province As String) As String
        Dim pro As String = ""
        If province = "PP" Then
            pro = "រាជធានីភ្នំពេញ"
        ElseIf province = "TK" Then
            pro = "តាកែវ"
        ElseIf province = "SR" Then
            pro = "សៀមរាប"
        ElseIf province = "BB" Then
            pro = "បាត់ដំបង"
        ElseIf province = "BMC" Then
            pro = "បន្ទាយមានជ័យ"
        ElseIf province = "RK" Then
            pro = "រតនគិរី"
        ElseIf province = "SH" Then
            pro = "ព្រះសីហនុ"
        ElseIf province = "TB" Then
            pro = "ត្បូងឃ្មុំ"
        ElseIf province = "ST" Then
            pro = "ស្ទឹងត្រែង"
        End If
        Return pro
    End Function

    Private Function ConvertToKhmerNumber(ByVal value As String) As String
        Dim arrEnglish() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"}
        Dim arrKhmer() As String = {"០", "១", "២", "៣", "៤", "៥", "៦", "៧", "៨", "៩"}
        Dim TextInput As String = value
        Dim result As String = ""
        For i As Integer = 0 To arrKhmer.Length() - 1
            result = TextInput.Replace(arrEnglish(i), arrKhmer(i))
            TextInput = result
        Next
        Return TextInput
    End Function
End Class