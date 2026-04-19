Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class STUDENT_RANK_GRADE_A_ALL_SUBJECT
    Public degreeid As Integer
    Public schoolid As Integer
    Public fieldid As Integer
    Public promotionno As Integer
    Public year As Integer 

    Private Sub STUDENT_RANK_GRADE_A_ALL_SUBJECT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim dtReport As New DataTable
        Dim cmd As New SqlCommand()

        cmd = New SqlCommand("select STUDENT_ID,STUDENT_NAME,STUDENT_NAME_IN_KHMER,SEX,PHONE,DATE_OF_BIRTH,DEGREE_IN_KHMER,
                            SCHOOL_NAME_IN_KHMER,PROMOTION_NO,FIELD_NAME_IN_KHMER,SUM(TOTAL) as TOTAL 
                            FROM V_STUDENT_RESULT WHERE DEGREE_ID=@degreeid and SCHOOL_ID=@schoolid and PROMOTION_ID=@promotionid 
                            and FIELD_ID=@fieldid and TERM_NO between @fromterm and @toterm and TOTAL >= 85 
                            group by STUDENT_ID,STUDENT_NAME,STUDENT_NAME_IN_KHMER,SEX,PHONE,DATE_OF_BIRTH,DEGREE_IN_KHMER,
                            SCHOOL_NAME_IN_KHMER,PROMOTION_NO,FIELD_NAME_IN_KHMER
                            having count(*) = 10
                            order by total desc", DbInterface.Connection)
        cmd.Parameters.Add("@degreeid", SqlDbType.Int).Value = degreeid
        cmd.Parameters.Add("@schoolid", SqlDbType.Int).Value = schoolid
        cmd.Parameters.Add("@fieldid", SqlDbType.Int).Value = fieldid
        cmd.Parameters.Add("@promotionid", SqlDbType.Int).Value = promotionno
        cmd.Parameters.Add("@fromterm", SqlDbType.Int).Value = (year * 2) - 1
        cmd.Parameters.Add("@toterm", SqlDbType.Int).Value = year * 2

        'Dim da As New SqlDataAdapter(cmd)
        'da.Fill(dtReport)

        Dim pfromdate As New ReportParameter("fromdate", degreeid)
        Dim pschool As New ReportParameter("todate", schoolid)
        Dim pfield As New ReportParameter("todate", fieldid)
        Dim ppromotion As New ReportParameter("todate", promotionno)
        Dim pyear As New ReportParameter("year", year)


        Dim dt As New DataTable
        dt.Columns.Add("NAME_ENGLISH")
        dt.Columns.Add("NAME_KHMER")
        dt.Columns.Add("GENDER")
        dt.Columns.Add("DOB")
        dt.Columns.Add("PHONE")
        dt.Columns.Add("SCORE")
        dt.Columns.Add("GRADE")
        dt.Columns.Add("DEGREE")
        dt.Columns.Add("SCHOOL")
        dt.Columns.Add("PROMOTION")

        Dim dr As DataRow
        Dim datareder As SqlDataReader = cmd.ExecuteReader
        Dim i As Integer = 1
        Dim grade As Integer = 0
        Dim total As Decimal = 0
        While datareder.Read()
            dr = dt.NewRow
            dr("NAME_ENGLISH") = datareder("STUDENT_NAME")
            dr("NAME_KHMER") = datareder("STUDENT_NAME_IN_KHMER")
            dr("GENDER") = datareder("SEX")
            dr("DOB") = Convert.ToDateTime(datareder("DATE_OF_BIRTH")).ToString("dd MMM yyyy")
            dr("PHONE") = datareder("PHONE")
            If total = 0 Then
                grade = 1
            End If
            If CDec(datareder("TOTAL")) < total Then
                grade += 1
            Else

            End If
            total = CDec(datareder("TOTAL"))
            dr("SCORE") = total
            dr("GRADE") = grade
            dr("DEGREE") = datareder("DEGREE_IN_KHMER")
            dr("SCHOOL") = datareder("SCHOOL_NAME_IN_KHMER")
            dr("PROMOTION") = datareder("PROMOTION_NO")
            dt.Rows.Add(dr)
            i += 1
        End While
        datareder.Close()
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.STUDENT_RANK_GRADE_A_ALL_SUBJECT.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dt))
        Me.ReportViewer1.LocalReport.SetParameters({pyear})
        Me.ReportViewer1.RefreshReport() 
    End Sub
End Class