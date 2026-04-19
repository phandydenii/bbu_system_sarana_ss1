Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class ENTRANCE_EXAM_PASS_FRM
    Private Sub ENTRANCE_EXAM_PASS_FRM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboDegree.Items.Clear()
        For Each obj As Object In Degrees.GetDegrees.Values
            cboDegree.Items.Add(obj)
        Next
        txtExamDate.Text = "ថ្ងៃសៅរ៍ ១០រោច​ខែមាឃ ឆ្នាំរោង ឆស័ក ព.ស.២៥៦៩ ត្រូវនឹងថ្ងៃទី២២ ខែកុម្ភ៖ ឆ្នាំ២០២៥"

    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        If cboDegree.SelectedIndex = -1 Then
            Exit Sub
        End If
        If cboSchool.SelectedIndex = -1 Then
            Exit Sub
        End If

        If cboField.SelectedIndex = -1 Then
            Exit Sub
        End If
        If cboPromotion.SelectedIndex = -1 Then
            Exit Sub
        End If
        Dim school As School = Schools.GetSchool(CType(cboSchool.SelectedItem, School).SchoolId)
        Dim field As Field = Fields.GetField(CType(cboField.SelectedItem, Field).FieldId)
        Dim promotion As Promotion = Promotions.GetPromotion(CType(cboPromotion.SelectedItem, Promotion).PromotionId)

        Dim cmd As New SqlCommand("SELECT * FROM V_STUDENT_ENTRANCE_PASS where PROMOTION_NO=@prono and FIELD_ID=@filedid order by student_id desc", DbInterface.Connection)
        cmd.Parameters.AddWithValue("@prono", SqlDbType.VarChar).Value = promotion.PromotionNo
        cmd.Parameters.AddWithValue("@filedid", SqlDbType.VarChar).Value = field.FieldId
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)


        Dim pBranch As New ReportParameter("branch", BranchNameKhmer())
        Dim pTitle As New ReportParameter("title", txtTitle.Text.Trim)
        Dim pSchool As New ReportParameter("school", school.SchoolNameInKhmer)
        Dim pExamDate As New ReportParameter("examdate", txtExamDate.Text.Trim)
        Dim pKhmerLunar As New ReportParameter("khmerlunar", CKhmerLunaaCalendar.GetKhmerYear)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.ENTRANCE_EXAM_PASS_RPT.rdlc"
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dt))
        Me.ReportViewer1.LocalReport.SetParameters({pBranch, pTitle, pExamDate, pSchool, pKhmerLunar})
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub cboDegree_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDegree.SelectedIndexChanged

        cboSchool.Items.Clear()
        cboPromotion.Items.Clear()
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

    Private Sub cboSchool_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchool.SelectedIndexChanged
        cboPromotion.Items.Clear()
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


    Function BranchNameKhmer() As String
        Select Case Utilities.BranchName
            Case "PP"
                Return "រាជធានីភ្នំពេញ"
            Case "BB"
                Return "សាខាខេត្តបាត់ដំបង"
            Case "TK"
                Return "សាខាខេត្តតាកែវ"
            Case "BMC"
                Return "សាខាខេត្តបន្ទាយមានជ័យ"
            Case "SH"
                Return "សាខាខេត្តព្រះសីហនុ"
            Case "RK"
                Return "សាខាខេត្តរតនគិរី"
            Case "ST"
                Return "សាខាខេត្តស្ទឹងត្រែង"
            Case "TB"
                Return "សាខាខេត្តត្បូងឃ្មុំ"
            Case "SR"
                Return "សាខាខេត្តសៀមរាប"
        End Select
        Return ""
    End Function

    Private Sub cboPromotion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPromotion.SelectedIndexChanged
        If cboPromotion.SelectedIndex = -1 Then
            Exit Sub
        End If
        Dim promotion As Promotion = Promotions.GetPromotion(CType(cboPromotion.SelectedItem, Promotion).PromotionId)
        Dim pro As String = $"{getKhmerNum(promotion.AcademicYearStart, 0)}-{getKhmerNum(promotion.AcademicYearStart + 1, 0)}"

        txtTitle.Text = $"ការអប់រំឧត្តមសិក្សា ជំនាន់ទី{getKhmerNum(cboPromotion.Text, 0)} សម្រាប់ឆ្នាំសិក្សា{pro}"
    End Sub
End Class