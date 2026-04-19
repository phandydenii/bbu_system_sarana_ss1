<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PROVISIONAL_CERTIFICATE_FRM
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.dtpExamDate = New System.Windows.Forms.DateTimePicker()
        Me.rbtSearchByName = New System.Windows.Forms.RadioButton()
        Me.rbtSearchByNameInKhmer = New System.Windows.Forms.RadioButton()
        Me.rbtSearchById = New System.Windows.Forms.RadioButton()
        Me.cboSerachStudent = New System.Windows.Forms.ComboBox()
        Me.lblExamDate = New System.Windows.Forms.Label()
        Me.rbtEnglishKhmer = New System.Windows.Forms.RadioButton()
        Me.rdoKhmer = New System.Windows.Forms.RadioButton()
        Me.rdoEnglish = New System.Windows.Forms.RadioButton()
        Me.grbReport = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.dtpIssuedDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpExpireDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCampus = New System.Windows.Forms.TextBox()
        Me.txtBranchName = New System.Windows.Forms.TextBox()
        Me.txtShortName = New System.Windows.Forms.TextBox()
        Me.txtSignature = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.grbReportSetting = New System.Windows.Forms.GroupBox()
        Me.txtKhmerDate = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtKhmerLunarDate = New System.Windows.Forms.TextBox()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chbNotFill = New System.Windows.Forms.CheckBox()
        Me.chbReportSetting = New System.Windows.Forms.CheckBox()
        Me.grbReport.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grbReportSetting.SuspendLayout()
        Me.SuspendLayout()
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportViewer1.Location = New System.Drawing.Point(367, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(880, 830)
        Me.ReportViewer1.TabIndex = 0
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(367, 830)
        Me.Splitter1.TabIndex = 1
        Me.Splitter1.TabStop = False
        '
        'dtpExamDate
        '
        Me.dtpExamDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpExamDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpExamDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExamDate.Location = New System.Drawing.Point(98, 114)
        Me.dtpExamDate.Name = "dtpExamDate"
        Me.dtpExamDate.Size = New System.Drawing.Size(170, 22)
        Me.dtpExamDate.TabIndex = 23
        '
        'rbtSearchByName
        '
        Me.rbtSearchByName.Checked = True
        Me.rbtSearchByName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtSearchByName.Location = New System.Drawing.Point(60, 19)
        Me.rbtSearchByName.Name = "rbtSearchByName"
        Me.rbtSearchByName.Size = New System.Drawing.Size(65, 21)
        Me.rbtSearchByName.TabIndex = 17
        Me.rbtSearchByName.TabStop = True
        Me.rbtSearchByName.Text = "Name"
        '
        'rbtSearchByNameInKhmer
        '
        Me.rbtSearchByNameInKhmer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtSearchByNameInKhmer.Location = New System.Drawing.Point(131, 19)
        Me.rbtSearchByNameInKhmer.Name = "rbtSearchByNameInKhmer"
        Me.rbtSearchByNameInKhmer.Size = New System.Drawing.Size(121, 21)
        Me.rbtSearchByNameInKhmer.TabIndex = 18
        Me.rbtSearchByNameInKhmer.Text = "Name in Khmer"
        '
        'rbtSearchById
        '
        Me.rbtSearchById.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtSearchById.Location = New System.Drawing.Point(6, 19)
        Me.rbtSearchById.Name = "rbtSearchById"
        Me.rbtSearchById.Size = New System.Drawing.Size(48, 21)
        Me.rbtSearchById.TabIndex = 16
        Me.rbtSearchById.Text = "ID"
        '
        'cboSerachStudent
        '
        Me.cboSerachStudent.Font = New System.Drawing.Font("Khmer OS Battambang", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSerachStudent.Location = New System.Drawing.Point(6, 46)
        Me.cboSerachStudent.Name = "cboSerachStudent"
        Me.cboSerachStudent.Size = New System.Drawing.Size(306, 32)
        Me.cboSerachStudent.TabIndex = 19
        '
        'lblExamDate
        '
        Me.lblExamDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExamDate.Location = New System.Drawing.Point(12, 113)
        Me.lblExamDate.Name = "lblExamDate"
        Me.lblExamDate.Size = New System.Drawing.Size(80, 24)
        Me.lblExamDate.TabIndex = 22
        Me.lblExamDate.Text = "Exam Date"
        Me.lblExamDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rbtEnglishKhmer
        '
        Me.rbtEnglishKhmer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtEnglishKhmer.Location = New System.Drawing.Point(8, 113)
        Me.rbtEnglishKhmer.Name = "rbtEnglishKhmer"
        Me.rbtEnglishKhmer.Size = New System.Drawing.Size(112, 16)
        Me.rbtEnglishKhmer.TabIndex = 0
        Me.rbtEnglishKhmer.Text = "English Khmer"
        Me.rbtEnglishKhmer.Visible = False
        '
        'rdoKhmer
        '
        Me.rdoKhmer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoKhmer.Location = New System.Drawing.Point(51, 27)
        Me.rdoKhmer.Name = "rdoKhmer"
        Me.rdoKhmer.Size = New System.Drawing.Size(112, 16)
        Me.rdoKhmer.TabIndex = 0
        Me.rdoKhmer.Text = "Khmer"
        '
        'rdoEnglish
        '
        Me.rdoEnglish.Checked = True
        Me.rdoEnglish.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoEnglish.Location = New System.Drawing.Point(191, 27)
        Me.rdoEnglish.Name = "rdoEnglish"
        Me.rdoEnglish.Size = New System.Drawing.Size(96, 16)
        Me.rdoEnglish.TabIndex = 2
        Me.rdoEnglish.TabStop = True
        Me.rdoEnglish.Text = "English"
        '
        'grbReport
        '
        Me.grbReport.Controls.Add(Me.rdoEnglish)
        Me.grbReport.Controls.Add(Me.rdoKhmer)
        Me.grbReport.Controls.Add(Me.rbtEnglishKhmer)
        Me.grbReport.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grbReport.Location = New System.Drawing.Point(12, 140)
        Me.grbReport.Name = "grbReport"
        Me.grbReport.Size = New System.Drawing.Size(349, 58)
        Me.grbReport.TabIndex = 25
        Me.grbReport.TabStop = False
        Me.grbReport.Text = "Report"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cboSerachStudent)
        Me.GroupBox1.Controls.Add(Me.rbtSearchByNameInKhmer)
        Me.GroupBox1.Controls.Add(Me.rbtSearchById)
        Me.GroupBox1.Controls.Add(Me.rbtSearchByName)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(349, 86)
        Me.GroupBox1.TabIndex = 27
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search"
        '
        'btnPreview
        '
        Me.btnPreview.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.Location = New System.Drawing.Point(268, 260)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(87, 30)
        Me.btnPreview.TabIndex = 28
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'dtpIssuedDate
        '
        Me.dtpIssuedDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpIssuedDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpIssuedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpIssuedDate.Location = New System.Drawing.Point(12, 230)
        Me.dtpIssuedDate.Name = "dtpIssuedDate"
        Me.dtpIssuedDate.Size = New System.Drawing.Size(163, 22)
        Me.dtpIssuedDate.TabIndex = 29
        '
        'dtpExpireDate
        '
        Me.dtpExpireDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpExpireDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpExpireDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpireDate.Location = New System.Drawing.Point(181, 230)
        Me.dtpExpireDate.Name = "dtpExpireDate"
        Me.dtpExpireDate.Size = New System.Drawing.Size(174, 22)
        Me.dtpExpireDate.TabIndex = 30
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 203)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 24)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "Issued Date"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(178, 203)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 24)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "Expire Date"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCampus
        '
        Me.txtCampus.Font = New System.Drawing.Font("Khmer OS Battambang", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCampus.Location = New System.Drawing.Point(8, 88)
        Me.txtCampus.Name = "txtCampus"
        Me.txtCampus.Size = New System.Drawing.Size(335, 32)
        Me.txtCampus.TabIndex = 33
        '
        'txtBranchName
        '
        Me.txtBranchName.Font = New System.Drawing.Font("Khmer OS Battambang", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBranchName.Location = New System.Drawing.Point(8, 141)
        Me.txtBranchName.Name = "txtBranchName"
        Me.txtBranchName.Size = New System.Drawing.Size(335, 32)
        Me.txtBranchName.TabIndex = 34
        '
        'txtShortName
        '
        Me.txtShortName.Font = New System.Drawing.Font("Khmer OS Battambang", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShortName.Location = New System.Drawing.Point(8, 250)
        Me.txtShortName.Name = "txtShortName"
        Me.txtShortName.Size = New System.Drawing.Size(335, 32)
        Me.txtShortName.TabIndex = 36
        '
        'txtSignature
        '
        Me.txtSignature.Font = New System.Drawing.Font("Khmer OS Battambang", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSignature.Location = New System.Drawing.Point(8, 195)
        Me.txtSignature.Name = "txtSignature"
        Me.txtSignature.Size = New System.Drawing.Size(335, 32)
        Me.txtSignature.TabIndex = 35
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 16)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "Campus"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(5, 122)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 16)
        Me.Label4.TabIndex = 38
        Me.Label4.Text = "Branch Name"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(5, 176)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 16)
        Me.Label5.TabIndex = 39
        Me.Label5.Text = "Signature"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(5, 231)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 16)
        Me.Label6.TabIndex = 40
        Me.Label6.Text = "Short Name"
        '
        'grbReportSetting
        '
        Me.grbReportSetting.Controls.Add(Me.txtKhmerDate)
        Me.grbReportSetting.Controls.Add(Me.Label8)
        Me.grbReportSetting.Controls.Add(Me.txtKhmerLunarDate)
        Me.grbReportSetting.Controls.Add(Me.txtTitle)
        Me.grbReportSetting.Controls.Add(Me.Label7)
        Me.grbReportSetting.Controls.Add(Me.txtCampus)
        Me.grbReportSetting.Controls.Add(Me.Label6)
        Me.grbReportSetting.Controls.Add(Me.txtBranchName)
        Me.grbReportSetting.Controls.Add(Me.Label5)
        Me.grbReportSetting.Controls.Add(Me.txtSignature)
        Me.grbReportSetting.Controls.Add(Me.Label4)
        Me.grbReportSetting.Controls.Add(Me.txtShortName)
        Me.grbReportSetting.Controls.Add(Me.Label3)
        Me.grbReportSetting.Controls.Add(Me.chbNotFill)
        Me.grbReportSetting.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grbReportSetting.Location = New System.Drawing.Point(12, 296)
        Me.grbReportSetting.Name = "grbReportSetting"
        Me.grbReportSetting.Size = New System.Drawing.Size(349, 390)
        Me.grbReportSetting.TabIndex = 41
        Me.grbReportSetting.TabStop = False
        '
        'txtKhmerDate
        '
        Me.txtKhmerDate.Font = New System.Drawing.Font("Khmer OS Battambang", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKhmerDate.Location = New System.Drawing.Point(6, 345)
        Me.txtKhmerDate.Name = "txtKhmerDate"
        Me.txtKhmerDate.Size = New System.Drawing.Size(337, 32)
        Me.txtKhmerDate.TabIndex = 47
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 288)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(84, 16)
        Me.Label8.TabIndex = 46
        Me.Label8.Text = "Khmer Date"
        '
        'txtKhmerLunarDate
        '
        Me.txtKhmerLunarDate.Font = New System.Drawing.Font("Khmer OS Battambang", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKhmerLunarDate.Location = New System.Drawing.Point(6, 307)
        Me.txtKhmerLunarDate.Name = "txtKhmerLunarDate"
        Me.txtKhmerLunarDate.Size = New System.Drawing.Size(337, 32)
        Me.txtKhmerLunarDate.TabIndex = 45
        '
        'txtTitle
        '
        Me.txtTitle.Font = New System.Drawing.Font("Khmer OS Battambang", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(8, 32)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(335, 32)
        Me.txtTitle.TabIndex = 43
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(5, 13)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 16)
        Me.Label7.TabIndex = 44
        Me.Label7.Text = "Title"
        '
        'chbNotFill
        '
        Me.chbNotFill.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbNotFill.Location = New System.Drawing.Point(275, 285)
        Me.chbNotFill.Name = "chbNotFill"
        Me.chbNotFill.Size = New System.Drawing.Size(68, 24)
        Me.chbNotFill.TabIndex = 48
        Me.chbNotFill.Text = "Not fill"
        '
        'chbReportSetting
        '
        Me.chbReportSetting.AutoSize = True
        Me.chbReportSetting.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbReportSetting.Location = New System.Drawing.Point(9, 275)
        Me.chbReportSetting.Name = "chbReportSetting"
        Me.chbReportSetting.Size = New System.Drawing.Size(123, 20)
        Me.chbReportSetting.TabIndex = 42
        Me.chbReportSetting.Text = "Report Setting"
        Me.chbReportSetting.UseVisualStyleBackColor = True
        '
        'PROVISIONAL_CERTIFICATE_FRM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1247, 830)
        Me.Controls.Add(Me.chbReportSetting)
        Me.Controls.Add(Me.grbReportSetting)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpExpireDate)
        Me.Controls.Add(Me.dtpIssuedDate)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grbReport)
        Me.Controls.Add(Me.dtpExamDate)
        Me.Controls.Add(Me.lblExamDate)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "PROVISIONAL_CERTIFICATE_FRM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PROVISIONAL_CERTIFICATE_FRM"
        Me.grbReport.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.grbReportSetting.ResumeLayout(False)
        Me.grbReportSetting.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents dtpExamDate As DateTimePicker
    Friend WithEvents rbtSearchByName As RadioButton
    Friend WithEvents rbtSearchByNameInKhmer As RadioButton
    Friend WithEvents rbtSearchById As RadioButton
    Friend WithEvents cboSerachStudent As ComboBox
    Friend WithEvents lblExamDate As Label
    Friend WithEvents rbtEnglishKhmer As RadioButton
    Friend WithEvents rdoKhmer As RadioButton
    Friend WithEvents rdoEnglish As RadioButton
    Friend WithEvents grbReport As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnPreview As Button
    Friend WithEvents dtpIssuedDate As DateTimePicker
    Friend WithEvents dtpExpireDate As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtCampus As TextBox
    Friend WithEvents txtBranchName As TextBox
    Friend WithEvents txtShortName As TextBox
    Friend WithEvents txtSignature As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents grbReportSetting As GroupBox
    Friend WithEvents chbReportSetting As CheckBox
    Friend WithEvents txtTitle As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents txtKhmerLunarDate As TextBox
    Friend WithEvents txtKhmerDate As TextBox
    Friend WithEvents chbNotFill As CheckBox
End Class
