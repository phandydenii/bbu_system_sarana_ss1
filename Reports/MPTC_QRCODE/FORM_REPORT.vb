Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports Microsoft.Reporting.WinForms
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Public Class FORM_REPORT
    Friend studentid As String
    Friend autoprint As Boolean
    Public Sub New(ByVal studentid As String)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.studentid = studentid

    End Sub
    Private Sub QRCertificate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmd As New SqlCommand("SELECT * FROM QR_CODE_CERTIFICATE where  STUDENT_ID=@studentid", DbInterface.Connection)
        cmd.Parameters.AddWithValue("@studentid", SqlDbType.VarChar).Value = studentid
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)
        Dim graduatedate As String = ""
        Dim graduatedateKm As String = ""
        Dim dr As SqlDataReader = cmd.ExecuteReader
        Dim field As String = ""
        Dim type As String = ""
        Try

            If dr.Read Then
                If dr("GRADUATE_DATE") Is DBNull.Value Then
                    graduatedate = GetDayOrdinalNumber(DateTime.Now) + " day of " + DateTime.Now.ToString("MMMM") + " in the year " + DateTime.Now.ToString("yyyy")
                    graduatedateKm = SubFunMod.GetDateToTextKm(DateTime.Now)
                Else
                    graduatedate = GetDayOrdinalNumber(Convert.ToDateTime(dr("GRADUATE_DATE"))) + " day of " + Convert.ToDateTime(dr("GRADUATE_DATE")).ToString("MMMM") + " in the year " + Convert.ToDateTime(dr("GRADUATE_DATE")).ToString("yyyy")
                    graduatedateKm = IIf(dr("GRADUATE_DATE") Is DBNull.Value, SubFunMod.GetDateToTextKm(DateTime.Now), SubFunMod.GetDateToTextKm(Convert.ToDateTime(dr("GRADUATE_DATE"))))
                End If
                field = IIf(dr("FIELD_NAME") Is DBNull.Value, "", dr("FIELD_NAME").ToString)
            End If

            If field.Trim = "" Then
                type = "PROFESSIONAL"
            Else
                type = "ACADEMIC"
            End If
            Dim pgraduatedate As New ReportParameter("draduatedate", graduatedate)
            Dim pgraduatedateKm As New ReportParameter("draduatedateKm", graduatedateKm)
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.QR_CERTIFICATE_" + type.ToString.ToUpper + "_" + Utilities.BranchName + ".rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dt))
            Me.ReportViewer1.LocalReport.SetParameters({pgraduatedate, pgraduatedateKm})
            ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            ReportViewer1.RefreshReport()
        Catch exception As Exception
            MessageBox.Show(Me, exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        dr.Close()
    End Sub

    Private Function GetDayOrdinalNumber(ByVal dd As DateTime) As String
        Dim str As String = ""
        If dd.Day.ToString("d") = "1" Then
            str = dd.Day.ToString("d") + "ˢᵗ"
        ElseIf dd.Day.ToString("d") = "2" Then
            str = dd.Day.ToString("d") + "ⁿᵈ"
        ElseIf dd.Day.ToString("d") = "3" Then
            str = dd.Day.ToString("d") + "ʳᵈ"
        ElseIf dd.Day.ToString("d") = "21" Then
            str = dd.Day.ToString("d") + "ˢᵗ"
        ElseIf dd.Day.ToString("d") = "22" Then
            str = dd.Day.ToString("d") + "ⁿᵈ"
        ElseIf dd.Day.ToString("d") = "23" Then
            str = dd.Day.ToString("d") + "ʳᵈ"
        ElseIf dd.Day.ToString("d") = "31" Then
            str = dd.Day.ToString("d") + "ˢᵗ"
        Else
            str = dd.Day.ToString("d") + "ᵗʰ"
        End If
        Return str
    End Function

End Class