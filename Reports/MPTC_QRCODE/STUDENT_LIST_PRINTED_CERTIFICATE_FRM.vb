Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class STUDENT_LIST_PRINTED_CERTIFICATE_FRM
    Public degreeid As Integer
    Public schoolid As Integer
    Public fieldid As Integer
    Public promotionid As Integer
    Public stageno As String = ""
    Public studytime As String = ""

    Private Sub STUDENT_LIST_PRINTED_CERTIFICATE_FRM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboDegree.Items.Clear()
        For Each obj As Object In Degrees.GetDegrees.Values
            cboDegree.Items.Add(obj)
        Next

        cboGroup.Items.Clear()
        For Each obj As Object In StudyTimes.GetStudyTimes
            cboGroup.Items.Add(obj)
        Next
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Dim degreeid As String = "DEGREE_ID= '" + CType(cboDegree.SelectedItem, Degree).DegreeId.ToString + "'"
        Dim schoolid As String = " AND SCHOOL_ID='" + CType(cboSchool.SelectedItem, School).SchoolId.ToString + "'"
        Dim fieldid As String = " AND FIELD_ID='" + CType(cboField.SelectedItem, Field).FieldId.ToString + "'"
        Dim promotionid As String = " AND PROMOTION_ID='" + CType(cboPromotion.SelectedItem, Promotion).PromotionId.ToString + "'"
        Dim stageno As String = ""
        Dim groupname As String = ""

        If chbAllStage.Checked = False Then
            stageno = " And STAGE_NO ='" + cboStage.Text + "'"
        End If


        If chbAllStage.Checked = False Then
            If chbAllStudyTime.Checked = False Then
                groupname = " AND GROUP_NAME='" + cboGroup.Text + "'"
            End If
        End If
        Dim query As String = degreeid & schoolid & fieldid & promotionid & stageno & groupname & " and STUDENT_ID in (select STUDENT_ID from STUDENT where IS_REQUEST=1) order by CERTIFICATE_CODE"
        Dim cmd As New SqlCommand("select * from QR_CODE_CERTIFICATE where " + query, DbInterface.Connection)
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        If stageno.Trim <> "" Then
            stageno = "វគ្គ" + stageno
        End If
        If studytime.Trim <> "" Then
            studytime = "វេន" + studytime
        End If
        da.Fill(dt)
        Dim pstageno As New ReportParameter("stageno", stageno)
        Dim pstudytime As New ReportParameter("studytime", studytime)

        Dim report As String = IIf(chbShowQR.Checked, "QR_RPT.rdlc", "RPT.rdlc")
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.STUDENT_LIST_PRINTED_CERTIFICATE_" & report
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dt))
        Me.ReportViewer1.LocalReport.SetParameters({pstageno, pstudytime})
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub cboDegree_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDegree.SelectedIndexChanged
        cboSchool.Items.Clear()
        cboPromotion.Items.Clear()
        cboStage.Items.Clear()
        cboField.Items.Clear()
        cboGroup.Items.Clear()
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

    Private Sub cboSchool_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchool.SelectedIndexChanged
        cboPromotion.Items.Clear()
        cboStage.Items.Clear()
        cboField.Items.Clear()
        cboGroup.Items.Clear()
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

    Private Sub cboPromotion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPromotion.SelectedIndexChanged
        cboStage.Items.Clear()
        cboGroup.Items.Clear()
        If cboPromotion.SelectedIndex = -1 Then Exit Sub

        For Each obj As Object In Stages.GetStages(CType(cboPromotion.SelectedItem, Promotion).PromotionId).Values
            cboStage.Items.Add(obj)
        Next
    End Sub

    Private Sub cboStage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStage.SelectedIndexChanged
        cboGroup.Items.Clear()
        For Each obj As Object In Groups.GetGroups(CType(cboStage.SelectedItem, Stage)).Values
            Dim group As Group = CType(obj, Group)
            If group.FieldId = CType(cboField.SelectedItem, Field).FieldId Then
                cboGroup.Items.Add(obj)
            End If
        Next
    End Sub

    Private Sub chbAllStage_CheckedChanged(sender As Object, e As EventArgs) Handles chbAllStage.CheckedChanged
        chbAllStudyTime.Checked = False
        cboStage.SelectedIndex = -1
        If chbAllStage.Checked Then
            cboStage.Enabled = False
            cboGroup.Enabled = False
            chbAllStudyTime.Enabled = False
        Else
            cboStage.Enabled = True
            cboGroup.Enabled = True
            chbAllStudyTime.Enabled = True
        End If
    End Sub

    Private Sub chbAllStudyTime_CheckedChanged(sender As Object, e As EventArgs) Handles chbAllStudyTime.CheckedChanged
        cboGroup.SelectedIndex = -1
        If chbAllStudyTime.Checked Then
            cboGroup.Enabled = False
        Else
            cboGroup.Enabled = True
        End If
    End Sub
End Class