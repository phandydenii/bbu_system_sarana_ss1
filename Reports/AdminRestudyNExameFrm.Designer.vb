<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminRestudyNExameFrm
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
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.cboTerm = New System.Windows.Forms.ComboBox()
        Me.cboPromotion = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.cboSchool = New System.Windows.Forms.ComboBox()
        Me.chkAllTerm = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Location = New System.Drawing.Point(-1, 25)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(881, 545)
        Me.ReportViewer1.TabIndex = 3
        '
        'btnPreview
        '
        Me.btnPreview.Location = New System.Drawing.Point(528, 0)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(60, 23)
        Me.btnPreview.TabIndex = 12
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'cboTerm
        '
        Me.cboTerm.FormattingEnabled = True
        Me.cboTerm.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"})
        Me.cboTerm.Location = New System.Drawing.Point(224, 2)
        Me.cboTerm.Name = "cboTerm"
        Me.cboTerm.Size = New System.Drawing.Size(50, 21)
        Me.cboTerm.TabIndex = 9
        Me.cboTerm.Text = "1"
        '
        'cboPromotion
        '
        Me.cboPromotion.FormattingEnabled = True
        Me.cboPromotion.Location = New System.Drawing.Point(82, 2)
        Me.cboPromotion.Name = "cboPromotion"
        Me.cboPromotion.Size = New System.Drawing.Size(53, 21)
        Me.cboPromotion.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Promotion :"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Location = New System.Drawing.Point(280, 3)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(37, 17)
        Me.chkAll.TabIndex = 13
        Me.chkAll.Text = "All"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'cboSchool
        '
        Me.cboSchool.FormattingEnabled = True
        Me.cboSchool.Location = New System.Drawing.Point(323, 2)
        Me.cboSchool.Name = "cboSchool"
        Me.cboSchool.Size = New System.Drawing.Size(199, 21)
        Me.cboSchool.TabIndex = 14
        '
        'chkAllTerm
        '
        Me.chkAllTerm.AutoSize = True
        Me.chkAllTerm.Location = New System.Drawing.Point(154, 4)
        Me.chkAllTerm.Name = "chkAllTerm"
        Me.chkAllTerm.Size = New System.Drawing.Size(64, 17)
        Me.chkAllTerm.TabIndex = 15
        Me.chkAllTerm.Text = "All Term"
        Me.chkAllTerm.UseVisualStyleBackColor = True
        '
        'AdminRestudyNExameFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(880, 570)
        Me.Controls.Add(Me.chkAllTerm)
        Me.Controls.Add(Me.cboSchool)
        Me.Controls.Add(Me.chkAll)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.cboTerm)
        Me.Controls.Add(Me.cboPromotion)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Name = "AdminRestudyNExameFrm"
        Me.Text = "AdminRestudyNExameFrm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents cboTerm As System.Windows.Forms.ComboBox
    Friend WithEvents cboPromotion As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
    Friend WithEvents cboSchool As System.Windows.Forms.ComboBox
    Friend WithEvents chkAllTerm As System.Windows.Forms.CheckBox
End Class
