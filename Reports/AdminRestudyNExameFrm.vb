Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class AdminRestudyNExameFrm

    Private Sub AdminRestudyNExameFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BoundCbo(cboPromotion, "select 0 ID,Promotion_NO from promotion group by promotion_no order by Promotion_no asc")
        BoundCbo(cboSchool, "select school_id,school_name from school")
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If chkAllTerm.Checked = True Then
            If chkAll.Checked = True Then
                Try
                    Dim cmd As New SqlCommand("SELECT * FROM  STUDENT_RESTUY_REEXAM_V WHERE  PROMOTION_NO=@PROMOTION_NO ORDER BY TERM_NO", DbInterface.Connection)
                    Dim da As New SqlDataAdapter(cmd)
                    'cmd.CommandType = CommandType.StoredProcedure
                    'cmd.Parameters.Add("@SCHOOL_ID", SqlDbType.Int).Value = cboSchool.SelectedValue
                    'cmd.Parameters.Add("@TERM_NO", SqlDbType.Int).Value = cboTerm.Text
                    cmd.Parameters.Add("@PROMOTION_NO", SqlDbType.Int).Value = cboPromotion.Text
                    Dim dtReport As New DataTable
                    da.Fill(dtReport)

                    Dim school As New ReportParameter("school", "គ្រប់មហាវិទ្យាល័យ")
                    Dim term As New ReportParameter("term", "ALL")
                    'Dim tos As New ReportParameter("to", DateTimePicker2.Value)

                    Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.AdminRestudyNReexam.rdlc"
                    Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
                    Me.ReportViewer1.LocalReport.DataSources.Clear()
                    Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
                    Me.ReportViewer1.LocalReport.SetParameters({school, term})
                    Me.ReportViewer1.RefreshReport()
                Catch ex As Exception

                End Try
            Else
                Try
                    Dim cmd As New SqlCommand("SELECT * FROM  STUDENT_RESTUY_REEXAM_V WHERE SCHOOL_ID=@SCHOOL_ID AND  PROMOTION_NO=@PROMOTION_NO ORDER BY TERM_NO", DbInterface.Connection)
                    Dim da As New SqlDataAdapter(cmd)
                    cmd.Parameters.Add("@SCHOOL_ID", SqlDbType.Int).Value = cboSchool.SelectedValue
                    'cmd.Parameters.Add("@TERM_NO", SqlDbType.Int).Value = cboTerm.Text
                    cmd.Parameters.Add("@PROMOTION_NO", SqlDbType.Int).Value = cboPromotion.Text
                    Dim dtReport As New DataTable
                    da.Fill(dtReport)

                    Dim school As New ReportParameter("school", cboSchool.Text)
                    Dim term As New ReportParameter("term", "ALL")
                    Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.AdminRestudyNReexam.rdlc"
                    Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
                    Me.ReportViewer1.LocalReport.DataSources.Clear()
                    Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
                    Me.ReportViewer1.LocalReport.SetParameters({school, term})
                    Me.ReportViewer1.RefreshReport()
                Catch ex As Exception

                End Try
            End If

        Else
            If chkAll.Checked = True Then
                Try
                    Dim cmd As New SqlCommand("SELECT * FROM  STUDENT_RESTUY_REEXAM_V WHERE TERM_NO=@TERM_NO AND PROMOTION_NO=@PROMOTION_NO ORDER BY TERM_NO", DbInterface.Connection)
                    Dim da As New SqlDataAdapter(cmd)
                    'cmd.CommandType = CommandType.StoredProcedure
                    'cmd.Parameters.Add("@SCHOOL_ID", SqlDbType.Int).Value = cboSchool.SelectedValue
                    cmd.Parameters.Add("@TERM_NO", SqlDbType.Int).Value = cboTerm.Text
                    cmd.Parameters.Add("@PROMOTION_NO", SqlDbType.Int).Value = cboPromotion.Text
                    Dim dtReport As New DataTable
                    da.Fill(dtReport)

                    Dim school As New ReportParameter("school", "គ្រប់មហាវិទ្យាល័យ")
                    Dim term As New ReportParameter("term", cboTerm.Text)
                    'Dim tos As New ReportParameter("to", DateTimePicker2.Value)

                    Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.AdminRestudyNReexam.rdlc"
                    Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
                    Me.ReportViewer1.LocalReport.DataSources.Clear()
                    Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
                    Me.ReportViewer1.LocalReport.SetParameters({school, term})
                    Me.ReportViewer1.RefreshReport()
                Catch ex As Exception

                End Try
            Else
                Try
                    Dim cmd As New SqlCommand("SELECT * FROM  STUDENT_RESTUY_REEXAM_V WHERE SCHOOL_ID=@SCHOOL_ID AND TERM_NO=@TERM_NO AND PROMOTION_NO=@PROMOTION_NO ORDER BY TERM_NO", DbInterface.Connection)
                    Dim da As New SqlDataAdapter(cmd)
                    cmd.Parameters.Add("@SCHOOL_ID", SqlDbType.Int).Value = cboSchool.SelectedValue
                    cmd.Parameters.Add("@TERM_NO", SqlDbType.Int).Value = cboTerm.Text
                    cmd.Parameters.Add("@PROMOTION_NO", SqlDbType.Int).Value = cboPromotion.Text
                    Dim dtReport As New DataTable
                    da.Fill(dtReport)
                    Dim school As New ReportParameter("school", cboSchool.Text)
                    Dim term As New ReportParameter("term", cboTerm.Text)
                    Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.AdminRestudyNReexam.rdlc"
                    Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
                    Me.ReportViewer1.LocalReport.DataSources.Clear()
                    Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))
                    Me.ReportViewer1.LocalReport.SetParameters({school, term})
                    Me.ReportViewer1.RefreshReport()
                Catch ex As Exception

                End Try
            End If

        End If

        

        
    End Sub

    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        If chkAll.Checked = True Then
            cboSchool.Enabled = False
        Else
            cboSchool.Enabled = True
        End If
    End Sub

    Private Sub chkAllTerm_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllTerm.CheckedChanged
        If chkAllTerm.Checked = True Then
            cboTerm.Enabled = False
        Else
            cboTerm.Enabled = True
        End If
    End Sub
End Class