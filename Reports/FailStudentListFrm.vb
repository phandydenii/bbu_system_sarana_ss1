Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class FailStudentListFrm

    Private Sub FailStudentListFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BoundCbo(cboDegree, "select * from DEGREE")
        BoundCbo(cboSchool, "select * from SCHOOL")
        BoundCbo(cboPromotion, "select 0,PROMOTION_NO from PROMOTION GROUP BY PROMOTION_NO ORDER BY PROMOTION_NO DESC")

    End Sub


    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Dim cmd As New SqlCommand("SELECT * FROM dbo.FAIL_STUDENT_LIST WHERE PROMOTION_NO =@PRONO AND STAGE_NO=@STAGENO AND DEGREE_ID=@DEGREE_ID AND SCHOOL_NAME=@SCHOOL ORDER BY STUDENT_NAME ASC ", DbInterface.Connection)
        Dim da As New SqlDataAdapter(cmd)
        cmd.Parameters.Add("@PRONO", SqlDbType.VarChar).Value = cboPromotion.Text
        cmd.Parameters.Add("@STAGENO", SqlDbType.VarChar).Value = cboStage.Text
        cmd.Parameters.Add("@DEGREE_ID", SqlDbType.VarChar).Value = cboDegree.SelectedValue
        cmd.Parameters.Add("@SCHOOL", SqlDbType.VarChar).Value = cboSchool.Text
        Dim dtReport As New DataTable
        da.Fill(dtReport)
        Dim pro As New ReportParameter("degree", cboDegree.Text)
        Dim stage As New ReportParameter("promotion", cboPromotion.Text)
        Dim degree As New ReportParameter("stage", cboStage.Text)
        Dim school As New ReportParameter("schoolname", cboSchool.Text)

        'Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.FailStudentListDetail.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
        Me.ReportViewer1.LocalReport.SetParameters({pro, stage, degree, school})
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub btnSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSummary.Click
        Dim cmd As New SqlCommand("select DEGREE,PROMOTION_NO,SCHOOL_NAME,COUNT(PROMOTION_NO) as QTY_COURSE from FAIL_STUDENT_LIST  WHERE PROMOTION_NO =@PRONO AND  DEGREE=@DEGREE  group by DEGREE,PROMOTION_NO,SCHOOL_NAME ", DbInterface.Connection)
        Dim da As New SqlDataAdapter(cmd)
        cmd.Parameters.Add("@PRONO", SqlDbType.VarChar).Value = cboPromotion.Text
        'cmd.Parameters.Add("@STAGENO", SqlDbType.VarChar).Value = cboStage.Text
        cmd.Parameters.Add("@DEGREE", SqlDbType.VarChar).Value = cboDegree.Text
        'cmd.Parameters.Add("@SCHOOL", SqlDbType.VarChar).Value = cboSchool.Text
        Dim dtReport As New DataTable
        da.Fill(dtReport)
        Dim pro As New ReportParameter("degree", cboDegree.Text)
        Dim stage As New ReportParameter("promotion", cboPromotion.Text)
        'Dim degree As New ReportParameter("stage", cboStage.Text)
        Dim school As New ReportParameter("schoolname", cboSchool.Text)

        'Dim pDes As New ReportParameter("description", "និស្សិតគ្រប់ជំនាន់គ្រប់មហាវិទ្យាល័យ")
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.FailStudentListSummary.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
        Me.ReportViewer1.LocalReport.SetParameters({pro, stage, school})
        Me.ReportViewer1.RefreshReport()
    End Sub
End Class