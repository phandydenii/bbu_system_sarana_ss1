Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class FormViewStudentProblem
    Dim pro As String
    Dim stage As String
    Dim school As String
    Dim field As String
    Dim termNo As Integer
    Dim groupno As String
    Dim schoolname As String
    Dim fieldname As String
    Dim groupname As String
    Dim chkallgroup As Boolean
    Dim radofinance As Boolean
    Dim radoacademic As Boolean
    Dim year As String
    Dim semester As String
    Public Sub SentData(ByVal Pros As String, ByVal Stages As String, ByVal Schools As String, ByVal Fields As String, ByVal Termnos As String, ByVal Groupnos As String, ByVal Schoolnames As String, ByVal Fieldnames As String, ByVal Groupnames As String, ByVal chkGroups As Boolean, ByVal rdoFinances As Boolean, ByVal rdoAcademics As Boolean, ByVal years As String, ByVal semesters As String)
        pro = Pros
        stage = Stages
        school = Schools
        field = Fields
        termNo = Termnos
        groupno = Groupnos
        schoolname = Schoolnames
        fieldname = Fieldnames
        groupname = Groupnames
        chkallgroup = chkGroups
        radofinance = rdoFinances
        radoacademic = rdoAcademics
        year = years
        semester = semesters
    End Sub

    Private Sub FormViewStudentProblem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim str As String
        Dim pro As String
        Dim stage As String
        Dim dtReport As New DataTable
        Dim drfield As String
        Dim drschool As String
        Dim drgroupname As String
        Dim drstage As String
        Dim drpro As String

        Try
            If radofinance = True Then
                If chkallgroup = True Then

                    Dim cmd As New SqlCommand("SELECT * FROM V_STUDENT_PROBLEM where GROUP_ID=@GROUPID AND TERM_NO=@TERM", DbInterface.Connection)
                    Dim da As New SqlDataAdapter(cmd)
                    Dim yr As String
                    Dim sem As String

                    cmd.Parameters.Add("@GROUPID", SqlDbType.VarChar).Value = groupno
                    cmd.Parameters.Add("@TERM", SqlDbType.VarChar).Value = termNo

                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        dr.Read()
                        If dr.HasRows Then
                            drfield = dr("FIELD_NAME_IN_KHMER").ToString()
                            drschool = dr("SCHOOL_NAME_IN_KHMER").ToString()
                            drgroupname = dr("GROUP_NAME").ToString()
                            drstage = dr("STAGE_NO").ToString()
                            drpro = dr("PROMOTION_ID").ToString()
                            yr = year
                            sem = semester
                        End If
                    End Using

                    str = "ជំនាន់ " & drpro & "​ វគ្គ " & drstage & "​ មហាវិទ្យាល័យ " & drschool & " ជំនាញ ​" & drfield & "​ ឆ្នាំទី​ " & yr & " ឆមាសទី​ " & sem & " ក្រុម​ " & drgroupname & ""
                    da.Fill(dtReport)

                Else
                    Dim cmd As New SqlCommand("SELECT * FROM V_STUDENT_PROBLEM where TERM_NO=@TERM", DbInterface.Connection)
                    Dim da As New SqlDataAdapter(cmd)

                    Dim yr As String
                    Dim sem As String

                    cmd.Parameters.Add("@TERM", SqlDbType.VarChar).Value = termNo

                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        dr.Read()
                        If dr.HasRows Then
                            drfield = dr("FIELD_NAME_IN_KHMER").ToString()
                            drschool = dr("SCHOOL_NAME_IN_KHMER").ToString()
                            drstage = dr("STAGE_NO").ToString()
                            drpro = dr("PROMOTION_ID").ToString()
                            yr = year
                            sem = semester
                        End If
                    End Using

                    str = "ជំនាន់ " & drpro & "​ វគ្គ " & drstage & "​ មហាវិទ្យាល័យ " & drschool & " ជំនាញ ​" & drfield & " ឆ្នាំទី​ " & yr & " ឆមាសទី​ " & sem & ""
                    da.Fill(dtReport)
                End If
                Dim pstr As New ReportParameter("description", str)
                Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.StudentProblemWithFinance.rdlc"
                Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
                Me.ReportViewer1.LocalReport.DataSources.Clear()
                Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
                Me.ReportViewer1.LocalReport.SetParameters({pstr})
                Me.ReportViewer1.RefreshReport()

            ElseIf radoacademic = True Then
                If chkallgroup = True Then
                    Dim cmd As New SqlCommand("SELECT * FROM V_STUDENT_PROBLEM where GROUP_ID=@GROUPID AND TERM_NO=@TERM", DbInterface.Connection)
                    Dim da As New SqlDataAdapter(cmd)

                    Dim yr As String
                    Dim sem As String

                    cmd.Parameters.Add("@GROUPID", SqlDbType.VarChar).Value = groupno
                    cmd.Parameters.Add("@TERM", SqlDbType.VarChar).Value = termNo

                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        dr.Read()
                        If dr.HasRows Then
                            drfield = dr("FIELD_NAME_IN_KHMER").ToString()
                            drschool = dr("SCHOOL_NAME_IN_KHMER").ToString()
                            drgroupname = dr("GROUP_NAME").ToString()
                            drstage = dr("STAGE_NO").ToString()
                            drpro = dr("PROMOTION_ID").ToString()
                            yr = year
                            sem = semester
                        End If
                    End Using

                    str = "ជំនាន់ " & drpro & "​ វគ្គ " & drstage & "​ មហាវិទ្យាល័យ " & drschool & " ជំនាញ ​" & drfield & " ឆ្នាំទី​ " & yr & " ឆមាសទី​ " & sem & " ក្រុម​ " & drgroupname & ""
                    da.Fill(dtReport)

                Else
                    Dim cmd As New SqlCommand("SELECT * FROM V_STUDENT_PROBLEM where TERM_NO=@TERM", DbInterface.Connection)
                    Dim da As New SqlDataAdapter(cmd)

                    Dim yr As String
                    Dim sem As String
                    
                    cmd.Parameters.Add("@TERM", SqlDbType.VarChar).Value = termNo

                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        dr.Read()
                        If dr.HasRows Then
                            drfield = dr("FIELD_NAME_IN_KHMER").ToString()
                            drschool = dr("SCHOOL_NAME_IN_KHMER").ToString()
                            drstage = dr("STAGE_NO").ToString()
                            drpro = dr("PROMOTION_ID").ToString()
                            yr = year
                            sem = semester

                        End If
                    End Using

                    str = "ជំនាន់ " & drpro & "​ វគ្គ " & drstage & "​ មហាវិទ្យាល័យ " & drschool & " ជំនាញ ​" & drfield & " ឆ្នាំទី​ " & yr & " ឆមាសទី​ " & sem & ""
                    da.Fill(dtReport)
                End If
                Dim pstr As New ReportParameter("description", str)
                Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.StudentProblemWithAcademic.rdlc"
                Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
                Me.ReportViewer1.LocalReport.DataSources.Clear()
                Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
                Me.ReportViewer1.LocalReport.SetParameters({pstr})
                Me.ReportViewer1.RefreshReport()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        Me.ReportViewer1.RefreshReport()
    End Sub
End Class