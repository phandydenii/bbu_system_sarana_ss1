Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class STUDENT_NOT_PAYMENT_FRM
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Try
            InitializeEntry()
            InitializeData()
        Catch exception As Exception
            MessageBox.Show(Me, exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InitializeEntry()

    End Sub

    Private Sub InitializeData()
        lsbDegree.Items.Clear()
        lsbDegree.Items.Add("All")
        For Each obj As Object In Degrees.GetDegrees.Values
            lsbDegree.Items.Add(obj)
        Next
        If lsbDegree.Items.Count > 0 Then
            lsbDegree.SelectedIndex = 0
        End If
        lsbSchool.Items.Clear()
        lsbSchool.Items.Add("All")
        For Each obj As Object In Schools.GetSchools.Values
            lsbSchool.Items.Add(obj)
        Next
        If lsbSchool.Items.Count > 0 Then
            lsbSchool.SelectedIndex = 0
        End If

        lsbPromotion.Items.Clear()
        lsbPromotion.Items.Add("All")
        For Each obj As Object In Promotions.GetPromotionNo() 
            lsbPromotion.Items.Add(obj)
        Next
        If lsbPromotion.Items.Count > 0 Then
            lsbPromotion.SelectedIndex = 0
        End If

        lsbTerm.Items.Clear()
        lsbTerm.Items.Add("All")
        For i As Integer = 1 To 10
            lsbTerm.Items.Add(i.ToString())
        Next
        If lsbTerm.Items.Count > 0 Then
            lsbTerm.SelectedIndex = 0
        End If

        lsbStatus.Items.Clear()
        lsbStatus.Items.Add("All")
        For Each obj As Object In Students.GetStatusStudent()
            Dim student As Student = CType(obj, Student)
            lsbStatus.Items.Add(student.Status)
        Next

        If lsbStatus.Items.Count > 0 Then
            lsbStatus.SelectedIndex = 1
        End If
    End Sub
    Private Sub STUDENT_NOT_PAYMENT_FRM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.ReportViewer1.RefreshReport()
    End Sub


    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim deg As String = "'"
        If lsbDegree.SelectedIndex = 0 Then
            For Each obj As Object In lsbDegree.Items 
                deg += obj.ToString() + "','"
            Next
        Else
            For Each obj As Object In lsbDegree.SelectedItems 
                deg += obj.ToString() + "','"
            Next
        End If
        deg += "'"

        Dim sch As String = "'"
        If lsbSchool.SelectedIndex = 0 Then
            For Each obj As Object In lsbSchool.Items
                sch += obj.ToString() + "','"
            Next
        Else
            For Each obj As Object In lsbSchool.SelectedItems
                Dim school As School = CType(obj, School)
                sch += school.SchoolName + "','"
            Next
        End If
        sch += "'"


        Dim pro As String = "'"
        If lsbPromotion.SelectedIndex = 0 Then
            For Each obj As Object In lsbPromotion.Items
                pro += obj.ToString() + "','"
            Next
        Else
            For Each obj As Object In lsbPromotion.SelectedItems
                Dim promotion As Promotion = CType(obj, Promotion)
                pro += promotion.PromotionNo.ToString() + "','"
            Next
        End If
        pro += "'"


        Dim termno As String = "'"
        If lsbTerm.SelectedIndex = 0 Then
            For Each t As String In lsbTerm.Items
                termno += t + "','"
            Next
        Else
            For Each t As String In lsbTerm.SelectedItems
                termno += t + "','"
            Next
        End If
        termno += "'"

        Dim st As String = "'"
        If lsbStatus.SelectedIndex = 0 Then
            For Each obj As Object In lsbStatus.Items
                st += obj.ToString() + "','"
            Next
        Else
            For Each obj As Object In lsbStatus.SelectedItems 
                st += obj.ToString() + "','"
            Next
        End If
        st += "'"


        Dim cmd As New SqlCommand("SELECT * FROM V_ADMIN_REPORT_LIST_OF_STUDENT_NOT_PAYMENT_NEW WHERE DEGREE IN (" + deg + ") AND SCHOOL_NAME IN (" + sch + ") AND PROMOTION_NO IN (" + pro + ")  AND TERM_NO IN (" + termno + ") AND STATUS IN (" + st + ") ", DbInterface.Connection)
        Dim da As New SqlDataAdapter(cmd)
        cmd.Parameters.Add("@degree", SqlDbType.VarChar).Value = deg.Replace(",''", "")
        cmd.Parameters.Add("@school", SqlDbType.VarChar).Value = sch.Replace(",''", "")
        cmd.Parameters.Add("@promotion", SqlDbType.VarChar).Value = pro.Replace(",''", "")
        cmd.Parameters.Add("@term", SqlDbType.VarChar).Value = termno.Replace(",''", "")
        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = st.Replace(",''", "")
        cmd.Parameters.Add("@from", SqlDbType.VarChar).Value = cboFrom.Text
        cmd.Parameters.Add("@to", SqlDbType.VarChar).Value = cboTo.Text
        Dim dtReport As New DataTable
        da.Fill(dtReport)
        'Dim pfrom As New ReportParameter("from", ViewerFrm.dtpFrom.Value)
        'Dim pto As New ReportParameter("to", ViewerFrm.dtpTo.Value) 
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.STUDENT_NOT_PAYMENT_RPT.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
        'Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto})
        Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        Me.ReportViewer1.RefreshReport()
    End Sub
End Class