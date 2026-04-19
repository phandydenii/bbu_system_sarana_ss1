Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class STUDENT_FEE_COLLECTION_FRM
    Private Sub STUDENT_FEE_COLLECTION_FRM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboDegree.Items.Clear()
        For Each obj As Object In Degrees.GetDegrees.Values
            cboDegree.Items.Add(obj)
        Next
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Dim DEGREE_ID As String = " degree_id=" + CType(cboDegree.SelectedItem, Degree).DegreeId.ToString
        Dim PROMOTIOM_ID As String = " and promotion_no=" + cboPromotion.Text
        Dim STAGE_ID As String = " and stage_no=" + cboStage.Text
        Dim TERM_NO As String = " and term_no=" + cboTermNo.Text
        'Dim GROUP As String = " and GROUP_NAME=" + cboGroup.Text
        Dim START_DATE As String = " and CONVERT(DATE,START_DATE)=" + dtpStartDate.Value.ToString("yyyy-MM-dd")
        'Dim END_DATE As String = " and CONVERT(DATE,END_DATE)=" + dtpEndDate.Value.ToString("yyyy-MM-dd")


        Dim cmd As New SqlCommand("SELECT * FROM V_STUDENT where  " & DEGREE_ID & PROMOTIOM_ID & STAGE_ID & TERM_NO, DbInterface.Connection)
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.STUDENT_FEE_COLLECTION_RPT.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dt))
        'Me.ReportViewer1.LocalReport.SetParameters({pdobKm, ptype, pfield, pfieldKm, pdegreekm, pgraduatedate, pgraduatedateKm, pfieldNew, pfieldKmNew, ppro_year_end, pissuedate, pexpiredate})
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub cboDegree_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDegree.SelectedIndexChanged
        cboPromotion.Items.Clear()
        If cboDegree.SelectedIndex <> -1 Then
            For Each obj As Object In Promotions.GetPromotionNo()
                cboPromotion.Items.Add(obj)
            Next
        End If
    End Sub

    Private Sub cboStage_SelectedIndexChanged(sender As Object, e As EventArgs)
        'cboGroup.Items.Clear()
        ''If cboStage.SelectedIndex = -1 Then Exit Sub
        ''For Each obj As Object In Groups.GetGroups(Convert.ToInt16(cboStage.Text)).Values
        ''    Dim group As Group = CType(obj, Group)

        ''    cboGroup.Items.Add(obj)
        ''Next
        'If cboStage.SelectedIndex = -1 OrElse cboTermNo.SelectedIndex = -1 Then
        '    Exit Sub
        'End If
        'Dim degreeid As Integer = CType(cboDegree.SelectedItem, Degree).DegreeId
        'Dim promotionid As Integer = CType(cboPromotion.SelectedItem, Promotion).PromotionId
        'Dim stageno As Integer = Convert.ToInt16(cboStage.Text)
        'Dim termno As Integer = Convert.ToInt16(cboTermNo.Text)
        'BoundCBODtailItem(cboGroup, "select GROUP_NAME from V_STUDENT where DEGREE_ID=" + degreeid + " and PROMOTION_ID=" + promotionid + " and STAGE_NO=" + stageno + " and TERM_NO=" + termno)
    End Sub


End Class