<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TRANSCRIPT_FORM
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lvwTerm = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.cboSearchStudent = New System.Windows.Forms.ComboBox()
        Me.rdoID = New System.Windows.Forms.RadioButton()
        Me.rdoName = New System.Windows.Forms.RadioButton()
        Me.rdoNameKh = New System.Windows.Forms.RadioButton()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.rdoKhmer = New System.Windows.Forms.RadioButton()
        Me.rdoEnglish = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chbReportSetting = New System.Windows.Forms.CheckBox()
        Me.grbReportSetting = New System.Windows.Forms.GroupBox()
        Me.txtCampus = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtBranchName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSignature = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtShortName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grbReportSetting.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvwTerm
        '
        Me.lvwTerm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lvwTerm.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader9, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.lvwTerm.FullRowSelect = True
        Me.lvwTerm.GridLines = True
        Me.lvwTerm.HideSelection = False
        Me.lvwTerm.Location = New System.Drawing.Point(14, 110)
        Me.lvwTerm.MultiSelect = False
        Me.lvwTerm.Name = "lvwTerm"
        Me.lvwTerm.Size = New System.Drawing.Size(256, 212)
        Me.lvwTerm.TabIndex = 1
        Me.lvwTerm.UseCompatibleStateImageBehavior = False
        Me.lvwTerm.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "StudentGroupId"
        Me.ColumnHeader1.Width = 0
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "STUDENT ID"
        Me.ColumnHeader9.Width = 0
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "TERM NO"
        Me.ColumnHeader2.Width = 100
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "GROUP_ID"
        Me.ColumnHeader3.Width = 80
        '
        'btnPreview
        '
        Me.btnPreview.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.Location = New System.Drawing.Point(167, 387)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(103, 29)
        Me.btnPreview.TabIndex = 2
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'cboSearchStudent
        '
        Me.cboSearchStudent.FormattingEnabled = True
        Me.cboSearchStudent.Location = New System.Drawing.Point(7, 58)
        Me.cboSearchStudent.Name = "cboSearchStudent"
        Me.cboSearchStudent.Size = New System.Drawing.Size(243, 24)
        Me.cboSearchStudent.TabIndex = 3
        '
        'rdoID
        '
        Me.rdoID.AutoSize = True
        Me.rdoID.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoID.Location = New System.Drawing.Point(6, 32)
        Me.rdoID.Name = "rdoID"
        Me.rdoID.Size = New System.Drawing.Size(40, 20)
        Me.rdoID.TabIndex = 4
        Me.rdoID.Text = "ID"
        Me.rdoID.UseVisualStyleBackColor = True
        '
        'rdoName
        '
        Me.rdoName.AutoSize = True
        Me.rdoName.Checked = True
        Me.rdoName.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoName.Location = New System.Drawing.Point(52, 32)
        Me.rdoName.Name = "rdoName"
        Me.rdoName.Size = New System.Drawing.Size(61, 20)
        Me.rdoName.TabIndex = 5
        Me.rdoName.TabStop = True
        Me.rdoName.Text = "Name"
        Me.rdoName.UseVisualStyleBackColor = True
        '
        'rdoNameKh
        '
        Me.rdoNameKh.AutoSize = True
        Me.rdoNameKh.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoNameKh.Location = New System.Drawing.Point(119, 32)
        Me.rdoNameKh.Name = "rdoNameKh"
        Me.rdoNameKh.Size = New System.Drawing.Size(106, 20)
        Me.rdoNameKh.TabIndex = 6
        Me.rdoNameKh.Text = "Name Khmer"
        Me.rdoNameKh.UseVisualStyleBackColor = True
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportViewer1.Location = New System.Drawing.Point(287, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(897, 842)
        Me.ReportViewer1.TabIndex = 8
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(287, 842)
        Me.Splitter1.TabIndex = 9
        Me.Splitter1.TabStop = False
        '
        'rdoKhmer
        '
        Me.rdoKhmer.AutoSize = True
        Me.rdoKhmer.Checked = True
        Me.rdoKhmer.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoKhmer.Location = New System.Drawing.Point(28, 22)
        Me.rdoKhmer.Name = "rdoKhmer"
        Me.rdoKhmer.Size = New System.Drawing.Size(67, 20)
        Me.rdoKhmer.TabIndex = 10
        Me.rdoKhmer.TabStop = True
        Me.rdoKhmer.Text = "Khmer"
        Me.rdoKhmer.UseVisualStyleBackColor = True
        '
        'rdoEnglish
        '
        Me.rdoEnglish.AutoSize = True
        Me.rdoEnglish.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoEnglish.Location = New System.Drawing.Point(140, 22)
        Me.rdoEnglish.Name = "rdoEnglish"
        Me.rdoEnglish.Size = New System.Drawing.Size(70, 20)
        Me.rdoEnglish.TabIndex = 11
        Me.rdoEnglish.Text = "English"
        Me.rdoEnglish.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdoKhmer)
        Me.GroupBox1.Controls.Add(Me.rdoEnglish)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(14, 328)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(256, 53)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Report"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cboSearchStudent)
        Me.GroupBox2.Controls.Add(Me.rdoID)
        Me.GroupBox2.Controls.Add(Me.rdoName)
        Me.GroupBox2.Controls.Add(Me.rdoNameKh)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(14, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(256, 92)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Search"
        '
        'chbReportSetting
        '
        Me.chbReportSetting.AutoSize = True
        Me.chbReportSetting.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbReportSetting.Location = New System.Drawing.Point(14, 417)
        Me.chbReportSetting.Name = "chbReportSetting"
        Me.chbReportSetting.Size = New System.Drawing.Size(123, 20)
        Me.chbReportSetting.TabIndex = 44
        Me.chbReportSetting.Text = "Report Setting"
        Me.chbReportSetting.UseVisualStyleBackColor = True
        '
        'grbReportSetting
        '
        Me.grbReportSetting.Controls.Add(Me.txtCampus)
        Me.grbReportSetting.Controls.Add(Me.Label6)
        Me.grbReportSetting.Controls.Add(Me.txtBranchName)
        Me.grbReportSetting.Controls.Add(Me.Label5)
        Me.grbReportSetting.Controls.Add(Me.txtSignature)
        Me.grbReportSetting.Controls.Add(Me.Label4)
        Me.grbReportSetting.Controls.Add(Me.txtShortName)
        Me.grbReportSetting.Controls.Add(Me.Label3)
        Me.grbReportSetting.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grbReportSetting.Location = New System.Drawing.Point(14, 439)
        Me.grbReportSetting.Name = "grbReportSetting"
        Me.grbReportSetting.Size = New System.Drawing.Size(256, 256)
        Me.grbReportSetting.TabIndex = 43
        Me.grbReportSetting.TabStop = False
        '
        'txtCampus
        '
        Me.txtCampus.Font = New System.Drawing.Font("Khmer OS Battambang", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCampus.Location = New System.Drawing.Point(8, 44)
        Me.txtCampus.Name = "txtCampus"
        Me.txtCampus.Size = New System.Drawing.Size(242, 32)
        Me.txtCampus.TabIndex = 33
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(5, 184)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 16)
        Me.Label6.TabIndex = 40
        Me.Label6.Text = "Short Name"
        '
        'txtBranchName
        '
        Me.txtBranchName.Font = New System.Drawing.Font("Khmer OS Battambang", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBranchName.Location = New System.Drawing.Point(8, 96)
        Me.txtBranchName.Name = "txtBranchName"
        Me.txtBranchName.Size = New System.Drawing.Size(242, 32)
        Me.txtBranchName.TabIndex = 34
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(5, 131)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 16)
        Me.Label5.TabIndex = 39
        Me.Label5.Text = "Signature"
        '
        'txtSignature
        '
        Me.txtSignature.Font = New System.Drawing.Font("Khmer OS Battambang", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSignature.Location = New System.Drawing.Point(8, 150)
        Me.txtSignature.Name = "txtSignature"
        Me.txtSignature.Size = New System.Drawing.Size(242, 32)
        Me.txtSignature.TabIndex = 35
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(5, 77)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 16)
        Me.Label4.TabIndex = 38
        Me.Label4.Text = "Branch Name"
        '
        'txtShortName
        '
        Me.txtShortName.Font = New System.Drawing.Font("Khmer OS Battambang", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShortName.Location = New System.Drawing.Point(8, 203)
        Me.txtShortName.Name = "txtShortName"
        Me.txtShortName.Size = New System.Drawing.Size(242, 32)
        Me.txtShortName.TabIndex = 36
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 16)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "Campus"
        '
        'TRANSCRIPT_FORM
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1184, 842)
        Me.Controls.Add(Me.chbReportSetting)
        Me.Controls.Add(Me.grbReportSetting)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.lvwTerm)
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "TRANSCRIPT_FORM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TRANSCRIPT_FORM"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grbReportSetting.ResumeLayout(False)
        Me.grbReportSetting.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lvwTerm As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents btnPreview As Button
    Friend WithEvents cboSearchStudent As ComboBox
    Friend WithEvents rdoID As RadioButton
    Friend WithEvents rdoName As RadioButton
    Friend WithEvents rdoNameKh As RadioButton
    Friend WithEvents ColumnHeader9 As ColumnHeader
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents rdoKhmer As RadioButton
    Friend WithEvents rdoEnglish As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents chbReportSetting As CheckBox
    Friend WithEvents grbReportSetting As GroupBox
    Friend WithEvents txtCampus As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtBranchName As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtSignature As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtShortName As TextBox
    Friend WithEvents Label3 As Label
End Class
