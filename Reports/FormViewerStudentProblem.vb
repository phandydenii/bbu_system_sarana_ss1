
Public Class FormViewerStudentProblem
    Private Sub btnPreviewSP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreviewSP.Click
        'Dim frm As FormViewPrintStudentProblem = New FormViewPrintStudentProblem
        'frm.MdiParent = Me.MdiParent

        'frm.Show()
    End Sub

    Private Sub FormViewerStudentProblem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each obj As Object In Degrees.GetDegrees.Values
            cboDegreeSP.Items.Add(obj)
        Next
    End Sub

    Private Sub cboDegreeSP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDegreeSP.SelectedIndexChanged
        cboSchoolSP.Items.Clear()
        cboFieldSP.Items.Clear()
        cboPromotionSP.Items.Clear()
        cboStageSP.Items.Clear()
        lstGroupSP.Items.Clear()

        If cboDegreeSP.SelectedIndex = -1 Then Exit Sub

        If CType(cboDegreeSP.SelectedItem, Degree).DegreeId = Degree.BACHELOR Then
            For Each obj As Object In Schools.GetSchools().Values
                cboSchoolSP.Items.Add(obj)
            Next
        Else
            For Each obj As Object In Schools.GetSchools().Values
                If Not CType(obj, School).IsFoundationSchool Then
                    cboSchoolSP.Items.Add(obj)
                End If
            Next
        End If
    End Sub

    Private Sub cboSchoolSP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSchoolSP.SelectedIndexChanged
        cboFieldSP.Items.Clear()
        cboPromotionSP.Items.Clear()
        cboStageSP.Items.Clear()
        lstGroupSP.Items.Clear()

        If cboDegreeSP.SelectedIndex = -1 Or cboSchoolSP.SelectedIndex = -1 Then Exit Sub

        For Each obj As Object In Promotions.GetPromotions(CType(cboDegreeSP.SelectedItem, Degree).DegreeId, CType(cboSchoolSP.SelectedItem, School).SchoolId).Values
            cboPromotionSP.Items.Add(obj)
        Next

        For Each obj As Object In Fields.GetFields(CType(cboSchoolSP.SelectedItem, School).SchoolId, CType(cboDegreeSP.SelectedItem, Degree).DegreeId).Values
            cboFieldSP.Items.Add(obj)
        Next
    End Sub

    Private Sub cboFieldSP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFieldSP.SelectedIndexChanged
        lstGroupSP.Items.Clear()

        If cboDegreeSP.SelectedIndex = -1 Or cboSchoolSP.SelectedIndex = -1 Then Exit Sub
        If cboPromotionSP.SelectedIndex = -1 Or cboStageSP.SelectedIndex = -1 Then Exit Sub
        If txtYearSP.Text = "" Or txtSemesterSP.Text = "" Then Exit Sub
        If cboFieldSP.SelectedIndex = -1 Then Exit Sub

        If cboFieldSP.SelectedIndex <> -1 Then
            For Each obj As Object In Groups.GetGroups(CType(cboPromotionSP.SelectedItem, Promotion), Term.GetTermNo(Convert.ToInt32(txtYearSP.Text), Convert.ToInt32(txtSemesterSP.Text))).Values
                Dim group As Group = CType(obj, Group)
                If group.StageId = CType(cboStageSP.SelectedItem, Stage).StageId AndAlso group.FieldId = CType(cboFieldSP.SelectedItem, Field).FieldId Then
                    lstGroupSP.Items.Add(obj)
                End If
            Next
        Else
            For Each obj As Object In Groups.GetGroups(CType(cboPromotionSP.SelectedItem, Promotion), Term.GetTermNo(Convert.ToInt32(txtYearSP.Text), Convert.ToInt32(txtSemesterSP.Text))).Values
                Dim group As Group = CType(obj, Group)
                If group.StageId = CType(cboStageSP.SelectedItem, Stage).StageId Then
                    lstGroupSP.Items.Add(obj)
                End If
            Next
        End If
    End Sub

    Private Sub cboPromotionSP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPromotionSP.SelectedIndexChanged
        cboStageSP.Items.Clear()
        lstGroupSP.Items.Clear()

        If cboPromotionSP.SelectedIndex = -1 Then Exit Sub

        For Each obj As Object In Stages.GetStages(CType(cboPromotionSP.SelectedItem, Promotion).PromotionId).Values
            cboStageSP.Items.Add(obj)
        Next
    End Sub

    Private Sub txtYearSP_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtYearSP.TextChanged
        lstGroupSP.Items.Clear()

        If cboDegreeSP.SelectedIndex = -1 Then Exit Sub
        If cboSchoolSP.SelectedIndex = -1 Then Exit Sub
        If cboPromotionSP.SelectedIndex = -1 Then Exit Sub
        If cboStageSP.SelectedIndex = -1 Then Exit Sub
        If txtYearSP.Text = "" Then Exit Sub
        If txtSemesterSP.Text = "" Then Exit Sub

        If cboFieldSP.SelectedIndex <> -1 Then
            For Each obj As Object In Groups.GetGroups(CType(cboPromotionSP.SelectedItem, Promotion), Term.GetTermNo(Convert.ToInt32(txtYearSP.Text), Convert.ToInt32(txtSemesterSP.Text))).Values
                Dim group As Group = CType(obj, Group)
                If group.StageId = CType(cboStageSP.SelectedItem, Stage).StageId AndAlso group.FieldId = CType(cboFieldSP.SelectedItem, Field).FieldId Then
                    lstGroupSP.Items.Add(obj)
                End If
            Next
        Else
            For Each obj As Object In Groups.GetGroups(CType(cboPromotionSP.SelectedItem, Promotion), Term.GetTermNo(Convert.ToInt32(txtYearSP.Text), Convert.ToInt32(txtSemesterSP.Text))).Values
                Dim group As Group = CType(obj, Group)
                If group.StageId = CType(cboStageSP.SelectedItem, Stage).StageId Then
                    lstGroupSP.Items.Add(obj)
                End If
            Next
        End If
    End Sub

    Private Sub txtSemesterSP_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSemesterSP.TextChanged
        lstGroupSP.Items.Clear()

        If cboDegreeSP.SelectedIndex = -1 Then Exit Sub
        If cboSchoolSP.SelectedIndex = -1 Then Exit Sub
        If cboPromotionSP.SelectedIndex = -1 Then Exit Sub
        If cboStageSP.SelectedIndex = -1 Then Exit Sub
        If txtYearSP.Text = "" Then Exit Sub
        If txtSemesterSP.Text = "" Then Exit Sub

        If cboFieldSP.SelectedIndex <> -1 Then
            For Each obj As Object In Groups.GetGroups(CType(cboPromotionSP.SelectedItem, Promotion), Term.GetTermNo(Convert.ToInt32(txtYearSP.Text), Convert.ToInt32(txtSemesterSP.Text))).Values
                Dim group As Group = CType(obj, Group)
                If group.StageId = CType(cboStageSP.SelectedItem, Stage).StageId AndAlso group.FieldId = CType(cboFieldSP.SelectedItem, Field).FieldId Then
                    lstGroupSP.Items.Add(obj)
                End If
            Next
        Else
            For Each obj As Object In Groups.GetGroups(CType(cboPromotionSP.SelectedItem, Promotion), Term.GetTermNo(Convert.ToInt32(txtYearSP.Text), Convert.ToInt32(txtSemesterSP.Text))).Values
                Dim group As Group = CType(obj, Group)
                If group.StageId = CType(cboStageSP.SelectedItem, Stage).StageId Then
                    lstGroupSP.Items.Add(obj)
                End If
            Next
        End If
    End Sub
End Class