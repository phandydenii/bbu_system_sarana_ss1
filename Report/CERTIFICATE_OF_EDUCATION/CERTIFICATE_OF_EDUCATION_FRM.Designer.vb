<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CERTIFICATE_OF_EDUCATION_FRM
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
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.chbKhmer = New System.Windows.Forms.CheckBox()
        Me.rdoDirector = New System.Windows.Forms.RadioButton()
        Me.grbSigner = New System.Windows.Forms.GroupBox()
        Me.rdoVicePrecident = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdoFinish = New System.Windows.Forms.RadioButton()
        Me.rdoBeingStudy = New System.Windows.Forms.RadioButton()
        Me.rdoComplete = New System.Windows.Forms.RadioButton()
        Me.rbtGraduate = New System.Windows.Forms.RadioButton()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.rdoName = New System.Windows.Forms.RadioButton()
        Me.rdoNameKh = New System.Windows.Forms.RadioButton()
        Me.rdoId = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cboSearchStudent = New System.Windows.Forms.ComboBox()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
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
        Me.grbSigner.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grbReportSetting.SuspendLayout()
        Me.SuspendLayout()
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(296, 842)
        Me.Splitter1.TabIndex = 0
        Me.Splitter1.TabStop = False
        '
        'chbKhmer
        '
        Me.chbKhmer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbKhmer.Location = New System.Drawing.Point(12, 301)
        Me.chbKhmer.Name = "chbKhmer"
        Me.chbKhmer.Size = New System.Drawing.Size(104, 24)
        Me.chbKhmer.TabIndex = 25
        Me.chbKhmer.Text = "Khmer"
        '
        'rdoDirector
        '
        Me.rdoDirector.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoDirector.Location = New System.Drawing.Point(6, 18)
        Me.rdoDirector.Name = "rdoDirector"
        Me.rdoDirector.Size = New System.Drawing.Size(81, 24)
        Me.rdoDirector.TabIndex = 1
        Me.rdoDirector.Text = "Director"
        '
        'grbSigner
        '
        Me.grbSigner.Controls.Add(Me.rdoVicePrecident)
        Me.grbSigner.Controls.Add(Me.rdoDirector)
        Me.grbSigner.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grbSigner.Location = New System.Drawing.Point(12, 247)
        Me.grbSigner.Name = "grbSigner"
        Me.grbSigner.Size = New System.Drawing.Size(272, 48)
        Me.grbSigner.TabIndex = 21
        Me.grbSigner.TabStop = False
        Me.grbSigner.Text = "Signer"
        '
        'rdoVicePrecident
        '
        Me.rdoVicePrecident.Checked = True
        Me.rdoVicePrecident.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoVicePrecident.Location = New System.Drawing.Point(146, 18)
        Me.rdoVicePrecident.Name = "rdoVicePrecident"
        Me.rdoVicePrecident.Size = New System.Drawing.Size(117, 24)
        Me.rdoVicePrecident.TabIndex = 2
        Me.rdoVicePrecident.TabStop = True
        Me.rdoVicePrecident.Text = "Vice Precident"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdoFinish)
        Me.GroupBox1.Controls.Add(Me.rdoBeingStudy)
        Me.GroupBox1.Controls.Add(Me.rdoComplete)
        Me.GroupBox1.Controls.Add(Me.rbtGraduate)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 115)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(272, 126)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Status"
        '
        'rdoFinish
        '
        Me.rdoFinish.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoFinish.Location = New System.Drawing.Point(33, 28)
        Me.rdoFinish.Name = "rdoFinish"
        Me.rdoFinish.Size = New System.Drawing.Size(64, 24)
        Me.rdoFinish.TabIndex = 3
        Me.rdoFinish.Text = "Finish"
        '
        'rdoBeingStudy
        '
        Me.rdoBeingStudy.Checked = True
        Me.rdoBeingStudy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoBeingStudy.Location = New System.Drawing.Point(33, 88)
        Me.rdoBeingStudy.Name = "rdoBeingStudy"
        Me.rdoBeingStudy.Size = New System.Drawing.Size(104, 24)
        Me.rdoBeingStudy.TabIndex = 0
        Me.rdoBeingStudy.TabStop = True
        Me.rdoBeingStudy.Text = "Being Study"
        '
        'rdoComplete
        '
        Me.rdoComplete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoComplete.Location = New System.Drawing.Point(33, 58)
        Me.rdoComplete.Name = "rdoComplete"
        Me.rdoComplete.Size = New System.Drawing.Size(88, 24)
        Me.rdoComplete.TabIndex = 1
        Me.rdoComplete.Text = "Complete"
        '
        'rbtGraduate
        '
        Me.rbtGraduate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtGraduate.Location = New System.Drawing.Point(328, 16)
        Me.rbtGraduate.Name = "rbtGraduate"
        Me.rbtGraduate.Size = New System.Drawing.Size(88, 24)
        Me.rbtGraduate.TabIndex = 2
        Me.rbtGraduate.Text = "Complete"
        '
        'btnPreview
        '
        Me.btnPreview.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.Location = New System.Drawing.Point(187, 301)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(97, 24)
        Me.btnPreview.TabIndex = 22
        Me.btnPreview.Text = "&Preview"
        '
        'rdoName
        '
        Me.rdoName.Checked = True
        Me.rdoName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoName.Location = New System.Drawing.Point(64, 28)
        Me.rdoName.Name = "rdoName"
        Me.rdoName.Size = New System.Drawing.Size(65, 21)
        Me.rdoName.TabIndex = 15
        Me.rdoName.TabStop = True
        Me.rdoName.Text = "Name"
        '
        'rdoNameKh
        '
        Me.rdoNameKh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoNameKh.Location = New System.Drawing.Point(135, 28)
        Me.rdoNameKh.Name = "rdoNameKh"
        Me.rdoNameKh.Size = New System.Drawing.Size(128, 21)
        Me.rdoNameKh.TabIndex = 16
        Me.rdoNameKh.Text = "Name in Khmer"
        '
        'rdoId
        '
        Me.rdoId.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoId.Location = New System.Drawing.Point(8, 28)
        Me.rdoId.Name = "rdoId"
        Me.rdoId.Size = New System.Drawing.Size(48, 21)
        Me.rdoId.TabIndex = 14
        Me.rdoId.Text = "ID"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cboSearchStudent)
        Me.GroupBox2.Controls.Add(Me.rdoId)
        Me.GroupBox2.Controls.Add(Me.rdoNameKh)
        Me.GroupBox2.Controls.Add(Me.rdoName)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(272, 97)
        Me.GroupBox2.TabIndex = 22
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Search"
        '
        'cboSearchStudent
        '
        Me.cboSearchStudent.Font = New System.Drawing.Font("Khmer OS Battambang", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSearchStudent.FormattingEnabled = True
        Me.cboSearchStudent.Location = New System.Drawing.Point(8, 55)
        Me.cboSearchStudent.Name = "cboSearchStudent"
        Me.cboSearchStudent.Size = New System.Drawing.Size(255, 32)
        Me.cboSearchStudent.TabIndex = 27
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportViewer1.Location = New System.Drawing.Point(296, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(887, 842)
        Me.ReportViewer1.TabIndex = 26
        '
        'chbReportSetting
        '
        Me.chbReportSetting.AutoSize = True
        Me.chbReportSetting.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbReportSetting.Location = New System.Drawing.Point(12, 344)
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
        Me.grbReportSetting.Location = New System.Drawing.Point(12, 366)
        Me.grbReportSetting.Name = "grbReportSetting"
        Me.grbReportSetting.Size = New System.Drawing.Size(272, 256)
        Me.grbReportSetting.TabIndex = 43
        Me.grbReportSetting.TabStop = False
        '
        'txtCampus
        '
        Me.txtCampus.Font = New System.Drawing.Font("Khmer OS Battambang", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCampus.Location = New System.Drawing.Point(8, 44)
        Me.txtCampus.Name = "txtCampus"
        Me.txtCampus.Size = New System.Drawing.Size(255, 32)
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
        Me.txtBranchName.Size = New System.Drawing.Size(255, 32)
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
        Me.txtSignature.Size = New System.Drawing.Size(255, 32)
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
        Me.txtShortName.Size = New System.Drawing.Size(255, 32)
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
        'CERTIFICATE_OF_EDUCATION_FRM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1183, 842)
        Me.Controls.Add(Me.chbReportSetting)
        Me.Controls.Add(Me.grbReportSetting)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.chbKhmer)
        Me.Controls.Add(Me.grbSigner)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "CERTIFICATE_OF_EDUCATION_FRM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CERTIFICATE_OF_EDUCATION_FRM"
        Me.grbSigner.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.grbReportSetting.ResumeLayout(False)
        Me.grbReportSetting.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents chbKhmer As CheckBox
    Friend WithEvents rdoDirector As RadioButton
    Friend WithEvents grbSigner As GroupBox
    Friend WithEvents rdoVicePrecident As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rdoFinish As RadioButton
    Friend WithEvents rdoBeingStudy As RadioButton
    Friend WithEvents rdoComplete As RadioButton
    Friend WithEvents rbtGraduate As RadioButton
    Friend WithEvents btnPreview As Button
    Friend WithEvents rdoName As RadioButton
    Friend WithEvents rdoNameKh As RadioButton
    Friend WithEvents rdoId As RadioButton
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents cboSearchStudent As ComboBox
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
