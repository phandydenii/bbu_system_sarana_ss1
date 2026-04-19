Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class FormViewerMoEYS

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)

    End Sub

    Private Sub FormViewerMoEYS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboDegree.Items.Clear()
        For Each obj As Object In Degrees.GetDegrees.Values
            cboDegree.Items.Add(obj)
        Next
    End Sub


    Private Sub chkAllField_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If chkAllField.Checked = True Then
            cboField.Enabled = False
            cboField.SelectedIndex = -1
        Else
            cboField.Enabled = True
        End If
    End Sub

    Private Sub cboDegree_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles cboDegree.SelectedIndexChanged
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

    Private Sub cboSchool_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles cboSchool.SelectedIndexChanged
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

    Private Sub TreeView1_AfterSelect_1(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect

    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Dim degree As String = " DEGREE_ID=" & CType(cboDegree.SelectedItem, Degree).DegreeId
        Dim school As String = " AND SCHOOL_ID=" & CType(cboSchool.SelectedItem, School).SchoolId
        Dim field As String = If(chkAllField.Checked, "", " AND FIELD_ID=" & CType(cboField.SelectedItem, Field).FieldId)
        Dim promotion As String = " AND PROMOTION_ID=" & CType(cboPromotion.SelectedItem, Promotion).PromotionId
        Dim target As String = ""
        Dim report As String = ""
        Dim title As String = ""

        Select Case TreeView1.SelectedNode.Index
            Case 0
                target = " and documentin !=''"
                report = "bbusystem.EntranceExamList.rdlc"
            Case 1
                target = " AND documentout !=''"
                report = "bbusystem.ComprehensiveExamList.rdlc"
            Case 2
                target = " And documentin !='' and documentout !=''"
                report = "bbusystem.MoEYSOfficialList.rdlc"
            Case 3
                target = " And documentin=''"
                report = "bbusystem.NoEntranceExamList.rdlc"
            Case 4
                target = " And documentout =''"
                report = "bbusystem.NoComprehensiveExamList.rdlc"
            Case 5
                target = " And documentin ='' and documentout =''"
                report = "bbusystem.NoMoEYSOfficialList.rdlc"
            Case 6
                target = " And AUTHENTICATED_NO !=''"
                report = "bbusystem.HIGTH_SCHOOL_CERTIFICATE_RPT.rdlc"
            Case 7
                target = " And AUTHENTICATED_NO =''"
                report = "bbusystem.NO_HIGTH_SCHOOL_CERTIFICATE_RPT.rdlc"
        End Select
        title = "កម្រិត " & cboDegree.Text & "​ មហាវិទ្យាល័យ " & cboSchool.Text & " ជំនាន់ " & cboPromotion.Text & If(chkAllField.Checked, "", " ជំនាញ ​" & cboField.Text)
        Dim cmd As New SqlCommand("SELECT * FROM V_ACADEMIC_OFFICE_REPORT_MOYES_OFFICIAL_LIST  where " & degree & school & field & promotion & target & "  order by STUDENT_NAME", DbInterface.Connection)
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)
        Dim pTitle As New ReportParameter("description", title)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = report
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dt))
        Me.ReportViewer1.LocalReport.SetParameters({pTitle})
        ReportViewer1.RefreshReport()
    End Sub
End Class