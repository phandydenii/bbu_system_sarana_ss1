<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class STUDENT_NOT_PAYMENT_FRM
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
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.lsbSchool = New System.Windows.Forms.ListBox()
        Me.lsbTerm = New System.Windows.Forms.ListBox()
        Me.lsbPromotion = New System.Windows.Forms.ListBox()
        Me.lsbStatus = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lsbDegree = New System.Windows.Forms.ListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboFrom = New System.Windows.Forms.ComboBox()
        Me.cboTo = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(205, 490)
        Me.Splitter1.TabIndex = 0
        Me.Splitter1.TabStop = False
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportViewer1.Location = New System.Drawing.Point(205, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(572, 490)
        Me.ReportViewer1.TabIndex = 1
        '
        'lsbSchool
        '
        Me.lsbSchool.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lsbSchool.FormattingEnabled = True
        Me.lsbSchool.Location = New System.Drawing.Point(12, 124)
        Me.lsbSchool.Name = "lsbSchool"
        Me.lsbSchool.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lsbSchool.Size = New System.Drawing.Size(174, 69)
        Me.lsbSchool.TabIndex = 2
        '
        'lsbTerm
        '
        Me.lsbTerm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lsbTerm.FormattingEnabled = True
        Me.lsbTerm.Location = New System.Drawing.Point(110, 213)
        Me.lsbTerm.Name = "lsbTerm"
        Me.lsbTerm.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lsbTerm.Size = New System.Drawing.Size(76, 82)
        Me.lsbTerm.TabIndex = 5
        '
        'lsbPromotion
        '
        Me.lsbPromotion.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lsbPromotion.FormattingEnabled = True
        Me.lsbPromotion.Location = New System.Drawing.Point(12, 213)
        Me.lsbPromotion.Name = "lsbPromotion"
        Me.lsbPromotion.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lsbPromotion.Size = New System.Drawing.Size(74, 82)
        Me.lsbPromotion.TabIndex = 4
        '
        'lsbStatus
        '
        Me.lsbStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lsbStatus.FormattingEnabled = True
        Me.lsbStatus.Location = New System.Drawing.Point(12, 318)
        Me.lsbStatus.Name = "lsbStatus"
        Me.lsbStatus.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lsbStatus.Size = New System.Drawing.Size(174, 69)
        Me.lsbStatus.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 105)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 14)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "School"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 14)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Degree"
        '
        'lsbDegree
        '
        Me.lsbDegree.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lsbDegree.FormattingEnabled = True
        Me.lsbDegree.Location = New System.Drawing.Point(12, 30)
        Me.lsbDegree.Name = "lsbDegree"
        Me.lsbDegree.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lsbDegree.Size = New System.Drawing.Size(174, 69)
        Me.lsbDegree.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 196)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 14)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Promotion"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(107, 196)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 14)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Term"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 301)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 14)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Status"
        '
        'cboFrom
        '
        Me.cboFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFrom.FormattingEnabled = True
        Me.cboFrom.Items.AddRange(New Object() {"1", "14", "30", "45", "120"})
        Me.cboFrom.Location = New System.Drawing.Point(12, 407)
        Me.cboFrom.Name = "cboFrom"
        Me.cboFrom.Size = New System.Drawing.Size(74, 21)
        Me.cboFrom.TabIndex = 13
        '
        'cboTo
        '
        Me.cboTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTo.FormattingEnabled = True
        Me.cboTo.Items.AddRange(New Object() {"14", "30", "45", "120", ">120"})
        Me.cboTo.Location = New System.Drawing.Point(112, 407)
        Me.cboTo.Name = "cboTo"
        Me.cboTo.Size = New System.Drawing.Size(74, 21)
        Me.cboTo.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(109, 390)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(22, 14)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "To"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(9, 390)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 14)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "From"
        '
        'btnPreview
        '
        Me.btnPreview.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.Location = New System.Drawing.Point(112, 436)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(74, 23)
        Me.btnPreview.TabIndex = 17
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'STUDENT_NOT_PAYMENT_FRM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(777, 490)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboTo)
        Me.Controls.Add(Me.cboFrom)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lsbDegree)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lsbStatus)
        Me.Controls.Add(Me.lsbTerm)
        Me.Controls.Add(Me.lsbPromotion)
        Me.Controls.Add(Me.lsbSchool)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "STUDENT_NOT_PAYMENT_FRM"
        Me.Text = "STUDENT_NOT_PAYMENT_FRM"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents lsbSchool As System.Windows.Forms.ListBox
    Friend WithEvents lsbTerm As System.Windows.Forms.ListBox
    Friend WithEvents lsbPromotion As System.Windows.Forms.ListBox
    Friend WithEvents lsbStatus As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lsbDegree As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboFrom As System.Windows.Forms.ComboBox
    Friend WithEvents cboTo As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnPreview As System.Windows.Forms.Button
End Class
