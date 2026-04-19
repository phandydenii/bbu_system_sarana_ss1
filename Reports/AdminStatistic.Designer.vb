<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminStatistic
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
        Me.cboPromotion = New System.Windows.Forms.ComboBox()
        Me.cboTerm = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnNotYet = New System.Windows.Forms.Button()
        Me.btnAlready = New System.Windows.Forms.Button()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.cboPromotionCerti = New System.Windows.Forms.ComboBox()
        Me.chkAllpro = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Location = New System.Drawing.Point(1, 30)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(816, 558)
        Me.ReportViewer1.TabIndex = 2
        '
        'cboPromotion
        '
        Me.cboPromotion.FormattingEnabled = True
        Me.cboPromotion.Location = New System.Drawing.Point(67, 3)
        Me.cboPromotion.Name = "cboPromotion"
        Me.cboPromotion.Size = New System.Drawing.Size(43, 21)
        Me.cboPromotion.TabIndex = 3
        '
        'cboTerm
        '
        Me.cboTerm.FormattingEnabled = True
        Me.cboTerm.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"})
        Me.cboTerm.Location = New System.Drawing.Point(157, 3)
        Me.cboTerm.Name = "cboTerm"
        Me.cboTerm.Size = New System.Drawing.Size(50, 21)
        Me.cboTerm.TabIndex = 4
        Me.cboTerm.Text = "1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Promotion :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(114, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Term :"
        '
        'btnPreview
        '
        Me.btnPreview.Location = New System.Drawing.Point(213, 1)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(60, 23)
        Me.btnPreview.TabIndex = 7
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnNotYet
        '
        Me.btnNotYet.Location = New System.Drawing.Point(404, 1)
        Me.btnNotYet.Name = "btnNotYet"
        Me.btnNotYet.Size = New System.Drawing.Size(103, 23)
        Me.btnNotYet.TabIndex = 8
        Me.btnNotYet.Text = "Not Yet Certificate"
        Me.btnNotYet.UseVisualStyleBackColor = True
        '
        'btnAlready
        '
        Me.btnAlready.Location = New System.Drawing.Point(701, 1)
        Me.btnAlready.Name = "btnAlready"
        Me.btnAlready.Size = New System.Drawing.Size(114, 23)
        Me.btnAlready.TabIndex = 9
        Me.btnAlready.Text = "Already Certificate"
        Me.btnAlready.UseVisualStyleBackColor = True
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(513, 4)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(89, 20)
        Me.dtpFrom.TabIndex = 10
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd-MMM-yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(608, 3)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(89, 20)
        Me.dtpTo.TabIndex = 11
        '
        'cboPromotionCerti
        '
        Me.cboPromotionCerti.FormattingEnabled = True
        Me.cboPromotionCerti.Location = New System.Drawing.Point(355, 1)
        Me.cboPromotionCerti.Name = "cboPromotionCerti"
        Me.cboPromotionCerti.Size = New System.Drawing.Size(46, 21)
        Me.cboPromotionCerti.TabIndex = 12
        '
        'chkAllpro
        '
        Me.chkAllpro.AutoSize = True
        Me.chkAllpro.Location = New System.Drawing.Point(279, 4)
        Me.chkAllpro.Name = "chkAllpro"
        Me.chkAllpro.Size = New System.Drawing.Size(70, 17)
        Me.chkAllpro.TabIndex = 13
        Me.chkAllpro.Text = "All Promo"
        Me.chkAllpro.UseVisualStyleBackColor = True
        '
        'AdminStatistic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(818, 588)
        Me.Controls.Add(Me.chkAllpro)
        Me.Controls.Add(Me.cboPromotionCerti)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.btnAlready)
        Me.Controls.Add(Me.btnNotYet)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboTerm)
        Me.Controls.Add(Me.cboPromotion)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "AdminStatistic"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TotalStudentAdmin"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents cboPromotion As System.Windows.Forms.ComboBox
    Friend WithEvents cboTerm As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnNotYet As System.Windows.Forms.Button
    Friend WithEvents btnAlready As System.Windows.Forms.Button
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboPromotionCerti As System.Windows.Forms.ComboBox
    Friend WithEvents chkAllpro As System.Windows.Forms.CheckBox
End Class
