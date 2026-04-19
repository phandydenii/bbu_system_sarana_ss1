<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminAllStudentsViewer
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvDegree = New System.Windows.Forms.DataGridView()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dgvState = New System.Windows.Forms.DataGridView()
        Me.dgvPromotion = New System.Windows.Forms.DataGridView()
        Me.chkTerm = New System.Windows.Forms.CheckBox()
        Me.cboTerm = New System.Windows.Forms.ComboBox()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgvDegree, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvPromotion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(782, 536)
        Me.ReportViewer1.TabIndex = 3
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboTerm)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkTerm)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvDegree)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnPreview)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvState)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvPromotion)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.ReportViewer1)
        Me.SplitContainer1.Size = New System.Drawing.Size(979, 536)
        Me.SplitContainer1.SplitterDistance = 193
        Me.SplitContainer1.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "DEGREE"
        '
        'dgvDegree
        '
        Me.dgvDegree.AllowUserToAddRows = False
        Me.dgvDegree.AllowUserToDeleteRows = False
        Me.dgvDegree.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDegree.Location = New System.Drawing.Point(9, 25)
        Me.dgvDegree.Name = "dgvDegree"
        Me.dgvDegree.ReadOnly = True
        Me.dgvDegree.Size = New System.Drawing.Size(151, 115)
        Me.dgvDegree.TabIndex = 13
        '
        'btnPreview
        '
        Me.btnPreview.Location = New System.Drawing.Point(8, 434)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(152, 39)
        Me.btnPreview.TabIndex = 12
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 319)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "STATE"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 147)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "PROMOTION"
        '
        'dgvState
        '
        Me.dgvState.AllowUserToAddRows = False
        Me.dgvState.AllowUserToDeleteRows = False
        Me.dgvState.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvState.Location = New System.Drawing.Point(7, 335)
        Me.dgvState.Name = "dgvState"
        Me.dgvState.ReadOnly = True
        Me.dgvState.Size = New System.Drawing.Size(100, 93)
        Me.dgvState.TabIndex = 9
        '
        'dgvPromotion
        '
        Me.dgvPromotion.AllowUserToAddRows = False
        Me.dgvPromotion.AllowUserToDeleteRows = False
        Me.dgvPromotion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPromotion.Location = New System.Drawing.Point(9, 163)
        Me.dgvPromotion.Name = "dgvPromotion"
        Me.dgvPromotion.ReadOnly = True
        Me.dgvPromotion.Size = New System.Drawing.Size(151, 149)
        Me.dgvPromotion.TabIndex = 8
        '
        'chkTerm
        '
        Me.chkTerm.AutoSize = True
        Me.chkTerm.Location = New System.Drawing.Point(109, 335)
        Me.chkTerm.Name = "chkTerm"
        Me.chkTerm.Size = New System.Drawing.Size(50, 17)
        Me.chkTerm.TabIndex = 15
        Me.chkTerm.Text = "Term"
        Me.chkTerm.UseVisualStyleBackColor = True
        '
        'cboTerm
        '
        Me.cboTerm.FormattingEnabled = True
        Me.cboTerm.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cboTerm.Location = New System.Drawing.Point(109, 358)
        Me.cboTerm.Name = "cboTerm"
        Me.cboTerm.Size = New System.Drawing.Size(51, 21)
        Me.cboTerm.TabIndex = 16
        Me.cboTerm.Text = "1"
        '
        'AdminAllStudentsViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(979, 536)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "AdminAllStudentsViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AdminAllStudentsViewer"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgvDegree, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvPromotion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dgvState As System.Windows.Forms.DataGridView
    Friend WithEvents dgvPromotion As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvDegree As System.Windows.Forms.DataGridView
    Friend WithEvents chkTerm As System.Windows.Forms.CheckBox
    Friend WithEvents cboTerm As System.Windows.Forms.ComboBox
End Class
