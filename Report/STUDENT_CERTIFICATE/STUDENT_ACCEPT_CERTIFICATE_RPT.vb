Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Public Class STUDENT_ACCEPT_AND_NOT_CERTIFICATE_RPT

    Private Sub STUDENT_ACCEPT_AND_NOT_CERTIFICATE_RPT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboDegree.Items.Clear()
        cboDegree.Items.Add("All Degree")
        For Each obj As Object In Degrees.GetDegrees.Values
            cboDegree.Items.Add(obj)
        Next
    End Sub

    Private Sub cboDegree_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDegree.SelectedIndexChanged
        cboSchool.Items.Clear()
        cboFromPromotion.Items.Clear()
        cboToPromotion.Items.Clear()

        If cboDegree.SelectedIndex = -1 Then Exit Sub
        If cboDegree.SelectedIndex >= 1 Then
            cboSchool.Items.Add("All School")
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
        End If


        For Each obj As Object In Promotions.GetPromotionNo()
            cboFromPromotion.Items.Add(obj)
        Next

        For Each obj As Object In Promotions.GetPromotionNo()
            cboToPromotion.Items.Add(obj)
        Next
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Me.Enabled = False
        If cboDegree.SelectedIndex = -1 Then
            Exit Sub
        End If

        If cboFromPromotion.SelectedIndex = -1 Then
            Exit Sub
        End If

        If cboToPromotion.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim frompro As String = cboFromPromotion.Text
        Dim topro As String = cboToPromotion.Text

        Dim degreename As String = IIf(cboDegree.SelectedIndex = 0, " DEGREE IN ('Associate','Bachelor','Master')", " DEGREE='" + cboDegree.Text + "'")
        Dim school As String = ""
        If cboSchool.SelectedIndex > 0 Then
            school = " AND SCHOOL_ID =" + CType(cboSchool.SelectedItem, School).SchoolId.ToString
        End If

        Dim promotion As String = " AND PROMOTION_NO BETWEEN '" + cboFromPromotion.Text + "' AND '" + cboToPromotion.Text + "'"
        Dim accept As String = IIf(rdoAccept.Checked, " AND IS_ACCEPT_CERTIFICATE=1", " AND IS_ACCEPT_CERTIFICATE=0")
        Dim accept_date As String = IIf(rdoNotAccept.Checked, "", " AND CONVERT(date,ACCEPT_DATE) BETWEEN '" + dtpFromDate.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpToDate.Value.ToString("yyyy-MM-dd") + "'")


        Dim status As String = IIf(rdoAllStatus.Checked, "", IIf(rdoGraduated.Checked, " and STUDENT_ID IN (SELECT STUDENT_ID FROM V_STUDENT_GRADUATED where " + degreename + school + promotion + accept_date + accept + ")", " and STUDENT_ID IN (SELECT STUDENT_ID FROM V_STUDENT_COMPLETED where " + degreename + school + promotion + accept_date + accept + ")"))

        Dim dtReport As New DataTable
        dtReport.Columns.Add("PROMOTION_NO")
        dtReport.Columns.Add("DEGREE")
        dtReport.Columns.Add("TOTAL")

        Dim dr As DataRow
        Dim cmd As New SqlCommand()
        cmd = New SqlCommand("SELECT PROMOTION_NO,DEGREE,Count(*) as TOTAL FROM V_STUDENT WHERE " + degreename + status + school + promotion + accept_date + accept + " group by PROMOTION_NO,DEGREE", DbInterface.Connection)
        Dim total As Integer = 0
        If rdoAccept.Checked Then
            Dim pfrompro As New ReportParameter("frompro", frompro)
            Dim ptopro As New ReportParameter("topro", topro)
            Dim pfromdate As New ReportParameter("fromdate", dtpFromDate.Value.ToString("dd-MMM-yyyy"))
            Dim ptodate As New ReportParameter("todate", dtpToDate.Value.ToString("dd-MMM-yyyy"))

            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read
                dr = dtReport.NewRow
                dr("PROMOTION_NO") = reader("PROMOTION_NO")
                dr("DEGREE") = reader("DEGREE")
                dr("TOTAL") = reader("TOTAL")
                dtReport.Rows.Add(dr)
                total += CInt(reader("TOTAL"))
            End While
            reader.Close()

            'Dim da As New SqlDataAdapter(cmd)
            'da.Fill(dtReport)



            Dim ptotal As New ReportParameter("total", total)
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.STUDENT_ACCEPT_CERTIFICATE_RPT.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
            Me.ReportViewer1.LocalReport.SetParameters({pfrompro, ptopro, pfromdate, ptodate, ptotal})
            Me.ReportViewer1.RefreshReport()
        Else
            Dim pfrompro As New ReportParameter("frompro", frompro)
            Dim ptopro As New ReportParameter("topro", topro)

            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read
                dr = dtReport.NewRow
                dr("PROMOTION_NO") = reader("PROMOTION_NO")
                dr("DEGREE") = reader("DEGREE")
                dr("TOTAL") = reader("TOTAL")
                dtReport.Rows.Add(dr)
                total += CInt(reader("TOTAL"))
            End While
            reader.Close()
            Dim ptotal As New ReportParameter("total", total)
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.STUDENT_NOT_ACCEPT_CERTIFICATE_RPT.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
            Me.ReportViewer1.LocalReport.SetParameters({pfrompro, ptopro, ptotal})
            Me.ReportViewer1.RefreshReport()
        End If
        Me.Enabled = True
    End Sub

    Private Sub rdoNotAccept_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoNotAccept.CheckedChanged
        If rdoNotAccept.Checked = True Then
            dtpFromDate.Enabled = False
            dtpToDate.Enabled = False
        Else
            dtpFromDate.Enabled = True
            dtpToDate.Enabled = True
        End If
    End Sub

    Private Sub btnViewList_Click(sender As Object, e As EventArgs) Handles btnViewList.Click
        Me.Enabled = False
        If cboDegree.SelectedIndex = -1 Then
            Exit Sub
        End If

        If cboFromPromotion.SelectedIndex = -1 Then
            Exit Sub
        End If

        If cboToPromotion.SelectedIndex = -1 Then
            Exit Sub
        End If
        Dim frompro As String = cboFromPromotion.Text
        Dim topro As String = cboToPromotion.Text
        Dim title As String = IIf(rdoAllStatus.Checked, "", IIf(rdoGraduated.Checked, " និងជាប់ជាស្ថាពរ", " និងមានមុខវិជ្ជាធ្លាក់"))

        Dim degreename As String = IIf(cboDegree.SelectedIndex = 0, " DEGREE IN ('Associate','Bachelor','Master')", " DEGREE='" + cboDegree.Text + "'")
        Dim school As String = ""
        If cboSchool.SelectedIndex > 0 Then
            school = " AND SCHOOL_ID =" + CType(cboSchool.SelectedItem, School).SchoolId.ToString
        End If
        Dim promotion As String = " AND PROMOTION_NO BETWEEN '" + cboFromPromotion.Text + "' AND '" + cboToPromotion.Text + "'"
        Dim accept As String = IIf(rdoAccept.Checked, " AND IS_ACCEPT_CERTIFICATE=1", " AND IS_ACCEPT_CERTIFICATE=0")
        Dim status As String = IIf(rdoAllStatus.Checked, "", IIf(rdoGraduated.Checked, " STUDENT_ID IN (SELECT STUDENT_ID FROM V_STUDENT_GRADUATED where " + degreename + school + promotion + ")", " STUDENT_ID IN (SELECT STUDENT_ID FROM V_STUDENT_COMPLETED where " + degreename + school + promotion + ")"))
        Dim accept_date As String = IIf(rdoNotAccept.Checked, "", " AND CONVERT(date,ACCEPT_DATE) BETWEEN '" + dtpFromDate.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpToDate.Value.ToString("yyyy-MM-dd") + "'")

        Dim dtReport As New DataTable
        Dim cmd As New SqlCommand()
        'cmd = New SqlCommand("SELECT * FROM V_STUDENT WHERE " + degreename + school + promotion + status + accept_date + accept, DbInterface.Connection)
        cmd = New SqlCommand("SELECT * FROM V_STUDENT WHERE " + status + " and " + degreename + school + promotion + accept_date + accept, DbInterface.Connection)
        If rdoAccept.Checked Then
            Dim pfrompro As New ReportParameter("frompro", frompro)
            Dim ptopro As New ReportParameter("topro", topro)
            Dim ptitle As New ReportParameter("title", title)
            Dim pfromdate As New ReportParameter("fromdate", dtpFromDate.Value.ToString("dd-MMM-yyyy"))
            Dim ptodate As New ReportParameter("todate", dtpToDate.Value.ToString("dd-MMM-yyyy"))

            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dtReport)
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.STUDENT_LIST_ACCEPT_CERTIFICATE_RPT.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
            Me.ReportViewer1.LocalReport.SetParameters({pfrompro, ptopro, pfromdate, ptodate, ptitle})
            Me.ReportViewer1.RefreshReport()
        Else
            Dim pfrompro As New ReportParameter("frompro", frompro)
            Dim ptopro As New ReportParameter("topro", topro)
            Dim ptitle As New ReportParameter("title", title)

            Dim da As New SqlDataAdapter(cmd)

            da.Fill(dtReport)
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.STUDENT_LIST_NOT_ACCEPT_CERTIFICATE_RPT.rdlc"
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
            Me.ReportViewer1.LocalReport.SetParameters({pfrompro, ptopro, ptitle})
            Me.ReportViewer1.RefreshReport()
        End If
        Me.Enabled = True
    End Sub
End Class