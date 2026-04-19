Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class PrintScoreSheetMasterDoctor
    Dim pro As String
    Dim stage As String
    Dim school As String
    Dim field As String
    Dim termNo As Integer
    Dim groupno As String
    Dim schoolname As String
    Dim fieldname As String
    Dim groupname As String
    Public Sub SentData(ByVal Pros As String, ByVal Stages As String, ByVal Schools As String, ByVal Fields As String, ByVal Termnos As String, ByVal Groupnos As String, ByVal schoolnames As String, ByVal fieldnames As String, ByVal groupnames As String)
        pro = Pros
        stage = Stages
        school = Schools
        field = Fields
        termNo = Termnos
        groupno = Groupnos
        schoolname = schoolnames
        fieldname = fieldnames
        groupname = groupnames
    End Sub

    Private Sub PrintScoreSheetMasterDoctor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim groups As String = "["
        'For Each obj As Object In FormPrintScoreSheet.lstGroup.SelectedItems
        '    groups = groups & CType(obj, Group).GroupId & ", "
        'Next
        'groups = groups.Substring(0, groups.Length - 2) & "]"

        'Dim pro As String = FormPrintScoreSheet.cboPromotion.Text
        'Dim stage As String = FormPrintScoreSheet.cboStage.Text
        'Dim school As String = CType(FormPrintScoreSheet.cboSchool.SelectedItem, School).SchoolId
        'Dim field As String = CType(FormPrintScoreSheet.cboField.SelectedItem, Field).FieldId
        'Dim termNo As Integer = Term.GetTermNo(FormPrintScoreSheet.txtYear.Text, FormPrintScoreSheet.txtSemester.Text)

        Dim cmd As New SqlCommand("select * from V_ACADEMIC_OFFICE_REPORT_LIST_OF_STUDENT_SUPPRESS WHERE PROMOTION_NO=@PRO AND STAGE_NO=@STAGE AND SCHOOL_ID=@SCHOOLID AND FIELD_ID=@FIELDID AND TERM_NO=@TERM AND GROUP_ID=@GROUPID order by STUDENT_NAME", DbInterface.Connection)
        Dim da As New SqlDataAdapter(cmd)

        cmd.Parameters.Add("@PRO", SqlDbType.VarChar).Value = pro
        cmd.Parameters.Add("@STAGE", SqlDbType.VarChar).Value = stage
        cmd.Parameters.Add("@SCHOOLID", SqlDbType.VarChar).Value = school
        cmd.Parameters.Add("@FIELDID", SqlDbType.VarChar).Value = field
        cmd.Parameters.Add("@GROUPID", SqlDbType.VarChar).Value = groupno
        cmd.Parameters.Add("@TERM", SqlDbType.VarChar).Value = termNo

        Dim dtReport As New DataTable
        da.Fill(dtReport)

        Dim ppro As New ReportParameter("pro", pro)
        Dim pstage As New ReportParameter("stage", stage)
        Dim pschool As New ReportParameter("school", schoolname)
        Dim pfield As New ReportParameter("field", fieldname)
        Dim ptermno As New ReportParameter("term", termNo)
        Dim pgroup As New ReportParameter("group", groupname)

        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "bbusystem.PrintScoreSheetMasterDoctor.rdlc"
        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dtReport))

        Me.ReportViewer1.LocalReport.SetParameters({ppro, pstage, pschool, pfield, ptermno, pgroup})
        Me.ReportViewer1.RefreshReport()
    End Sub
End Class