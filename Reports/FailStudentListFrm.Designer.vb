<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FailStudentListFrm
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
        Me.btnDetail = New System.Windows.Forms.Button()
        Me.cboSchool = New System.Windows.Forms.ComboBox()
        Me.cboDegree = New System.Windows.Forms.ComboBox()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboPromotion = New System.Windows.Forms.ComboBox()
        Me.cboStage = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnSummary = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnDetail
        '
        Me.btnDetail.Location = New System.Drawing.Point(750, 1)
        Me.btnDetail.Name = "btnDetail"
        Me.btnDetail.Size = New System.Drawing.Size(71, 27)
        Me.btnDetail.TabIndex = 15
        Me.btnDetail.Text = "Detail"
        Me.btnDetail.UseVisualStyleBackColor = True
        '
        'cboSchool
        '
        Me.cboSchool.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSchool.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSchool.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSchool.FormattingEnabled = True
        Me.cboSchool.Location = New System.Drawing.Point(416, 3)
        Me.cboSchool.Name = "cboSchool"
        Me.cboSchool.Size = New System.Drawing.Size(245, 24)
        Me.cboSchool.TabIndex = 13
        '
        'cboDegree
        '
        Me.cboDegree.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDegree.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDegree.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDegree.FormattingEnabled = True
        Me.cboDegree.Location = New System.Drawing.Point(60, 3)
        Me.cboDegree.Name = "cboDegree"
        Me.cboDegree.Size = New System.Drawing.Size(94, 24)
        Me.cboDegree.TabIndex = 12
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 31)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(828, 533)
        Me.ReportViewer1.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Degree"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(370, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "School"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(162, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Promotion"
        '
        'cboPromotion
        '
        Me.cboPromotion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPromotion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPromotion.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPromotion.FormattingEnabled = True
        Me.cboPromotion.Location = New System.Drawing.Point(222, 3)
        Me.cboPromotion.Name = "cboPromotion"
        Me.cboPromotion.Size = New System.Drawing.Size(62, 24)
        Me.cboPromotion.TabIndex = 19
        '
        'cboStage
        '
        Me.cboStage.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStage.FormattingEnabled = True
        Me.cboStage.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7"})
        Me.cboStage.Location = New System.Drawing.Point(708, 3)
        Me.cboStage.Name = "cboStage"
        Me.cboStage.Size = New System.Drawing.Size(39, 24)
        Me.cboStage.TabIndex = 20
        Me.cboStage.Text = "1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(667, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "Stage"
        '
        'btnSummary
        '
        Me.btnSummary.Location = New System.Drawing.Point(293, 1)
        Me.btnSummary.Name = "btnSummary"
        Me.btnSummary.Size = New System.Drawing.Size(71, 27)
        Me.btnSummary.TabIndex = 22
        Me.btnSummary.Text = "Summary"
        Me.btnSummary.UseVisualStyleBackColor = True
        '
        'FailStudentListFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(827, 564)
        Me.Controls.Add(Me.btnSummary)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboStage)
        Me.Controls.Add(Me.cboPromotion)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnDetail)
        Me.Controls.Add(Me.cboSchool)
        Me.Controls.Add(Me.cboDegree)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Name = "FailStudentListFrm"
        Me.Text = "FailStudentListFrm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnDetail As System.Windows.Forms.Button
    Friend WithEvents cboSchool As System.Windows.Forms.ComboBox
    Friend WithEvents cboDegree As System.Windows.Forms.ComboBox
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboPromotion As System.Windows.Forms.ComboBox
    Friend WithEvents cboStage As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnSummary As System.Windows.Forms.Button
End Class
