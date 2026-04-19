Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class DOCTORAL_CONTRACT_FRM

    Private Sub DOCTORAL_CONTRACT_FRM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboDegree.Items.Clear()
        For Each obj As Object In Degrees.GetDegrees.Values
            cboDegree.Items.Add(obj)
        Next
        cboDegree.SelectedIndex = 4
        cboSchool.Text = "Doctoral Studies"
    End Sub

    Private Sub cboDegree_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDegree.SelectedIndexChanged
        cboSchool.Items.Clear()
        cboPromotion.Items.Clear()
        cboStage.Items.Clear()
        txtYear.Text = ""
        txtSemester.Text = ""
        cboField.Items.Clear()

        If cboDegree.SelectedIndex = -1 Then Exit Sub

        If CType(cboDegree.SelectedItem, Degree).DegreeId = Degree.BACHELOR Then
            For Each obj As Object In Schools.GetSchools().Values
                cboSchool.Items.Add(obj)
            Next
        Else
            For Each obj As Object In Schools.GetSchools().Values
                If Not CType(obj, School).IsFoundationSchool Then
                    cboSchool.Items.Add(obj)
                End If
            Next
        End If
    End Sub

    Private Sub cboSchool_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSchool.SelectedIndexChanged
        cboPromotion.Items.Clear()
        cboStage.Items.Clear()
        txtYear.Text = ""
        txtSemester.Text = ""
        cboField.Items.Clear()
        If cboDegree.SelectedIndex = -1 OrElse cboSchool.SelectedIndex = -1 Then
            Exit Sub
        End If
        For Each obj As Object In Promotions.GetPromotions(CType(cboDegree.SelectedItem, Degree).DegreeId, CType(cboSchool.SelectedItem, School).SchoolId).Values
            cboPromotion.Items.Add(obj)
        Next

        For Each obj As Object In Fields.GetFields(CType(cboSchool.SelectedItem, School).SchoolId, CType(cboDegree.SelectedItem, Degree).DegreeId).Values
            cboField.Items.Add(obj)
        Next
    End Sub

    Private Sub cboPromotion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPromotion.SelectedIndexChanged
        cboStage.Items.Clear()
        lstGroup.Items.Clear()

        If cboPromotion.SelectedIndex = -1 Then Exit Sub

        For Each obj As Object In Stages.GetStages(CType(cboPromotion.SelectedItem, Promotion).PromotionId).Values
            cboStage.Items.Add(obj)
        Next
    End Sub

    Private Sub cboStage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboStage.SelectedIndexChanged
        lstGroup.Items.Clear()

        If cboStage.SelectedIndex = -1 Or txtYear.Text = "" Or txtSemester.Text = "" Then Exit Sub

        If cboField.SelectedIndex <> -1 Then
            For Each obj As Object In Groups.GetGroups(CType(cboPromotion.SelectedItem, Promotion), Term.GetTermNo(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtSemester.Text))).Values
                Dim group As Group = CType(obj, Group)
                If group.StageId = CType(cboStage.SelectedItem, Stage).StageId AndAlso group.FieldId = CType(cboField.SelectedItem, Field).FieldId Then
                    lstGroup.Items.Add(obj)
                End If
            Next
        Else
            For Each obj As Object In Groups.GetGroups(CType(cboPromotion.SelectedItem, Promotion), Term.GetTermNo(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtSemester.Text))).Values
                Dim group As Group = CType(obj, Group)
                If group.StageId = CType(cboStage.SelectedItem, Stage).StageId Then
                    lstGroup.Items.Add(obj)
                End If
            Next
        End If
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim cmd As New SqlCommand("SELECT * FROM V_DOCTORAL_CONTRACT WHERE DEGREE_ID=@degree_id AND SCHOOL_ID=@school_id AND FIELD_ID=@field_id AND PROMOTION_ID=@promotion_id AND STAGE_NO=@stage_no AND TERM_NO=@term AND GROUP_NAME=@group", DbInterface.Connection)
        Dim da As New SqlDataAdapter(cmd)
        cmd.Parameters.Add("@degree_id", SqlDbType.Int).Value = CType(cboDegree.SelectedItem, Degree).DegreeId
        cmd.Parameters.Add("@school_id", SqlDbType.Int).Value = CType(cboSchool.SelectedItem, School).SchoolId
        cmd.Parameters.Add("@field_id", SqlDbType.Int).Value = CType(cboField.SelectedItem, Field).FieldId
        cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = CType(cboPromotion.SelectedItem, Promotion).PromotionId
        cmd.Parameters.Add("@stage_no", SqlDbType.Int).Value = CInt(cboStage.Text)
        cmd.Parameters.Add("@term", SqlDbType.Int).Value = Term.GetTermNo(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtSemester.Text))
        cmd.Parameters.Add("@group", SqlDbType.VarChar).Value = CType(lstGroup.SelectedItem, Group).GroupId
        Dim dtReport As New DataTable
        da.Fill(dtReport)
        'Dim pfrom As New ReportParameter("from", ViewerFrm.dtpFrom.Value)
        'Dim pto As New ReportParameter("to", ViewerFrm.dtpTo.Value)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.DOCTORAL_CONTRACT_RPT.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

        'Me.ReportViewer1.LocalReport.SetParameters({pfrom, pto})
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
End Class