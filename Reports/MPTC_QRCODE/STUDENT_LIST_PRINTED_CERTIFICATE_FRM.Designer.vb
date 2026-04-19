<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class STUDENT_LIST_PRINTED_CERTIFICATE_FRM
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
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.chbAllStudyTime = New System.Windows.Forms.CheckBox()
        Me.chbAllStage = New System.Windows.Forms.CheckBox()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.cboGroup = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboDegree = New System.Windows.Forms.ComboBox()
        Me.cboSchool = New System.Windows.Forms.ComboBox()
        Me.cboPromotion = New System.Windows.Forms.ComboBox()
        Me.cboStage = New System.Windows.Forms.ComboBox()
        Me.cboField = New System.Windows.Forms.ComboBox()
        Me.chbShowQR = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(287, 614)
        Me.Splitter1.TabIndex = 1
        Me.Splitter1.TabStop = False
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportViewer1.Location = New System.Drawing.Point(287, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(744, 614)
        Me.ReportViewer1.TabIndex = 2
        '
        'chbAllStudyTime
        '
        Me.chbAllStudyTime.AutoSize = True
        Me.chbAllStudyTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAllStudyTime.Location = New System.Drawing.Point(221, 311)
        Me.chbAllStudyTime.Name = "chbAllStudyTime"
        Me.chbAllStudyTime.Size = New System.Drawing.Size(41, 18)
        Me.chbAllStudyTime.TabIndex = 138
        Me.chbAllStudyTime.Text = "All"
        Me.chbAllStudyTime.UseVisualStyleBackColor = True
        '
        'chbAllStage
        '
        Me.chbAllStage.AutoSize = True
        Me.chbAllStage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAllStage.Location = New System.Drawing.Point(221, 254)
        Me.chbAllStage.Name = "chbAllStage"
        Me.chbAllStage.Size = New System.Drawing.Size(41, 18)
        Me.chbAllStage.TabIndex = 137
        Me.chbAllStage.Text = "All"
        Me.chbAllStage.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.Location = New System.Drawing.Point(115, 352)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(148, 28)
        Me.btnPreview.TabIndex = 136
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'cboGroup
        '
        Me.cboGroup.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGroup.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cboGroup.Location = New System.Drawing.Point(19, 310)
        Me.cboGroup.Name = "cboGroup"
        Me.cboGroup.Size = New System.Drawing.Size(196, 21)
        Me.cboGroup.TabIndex = 135
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(16, 288)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 14)
        Me.Label7.TabIndex = 134
        Me.Label7.Text = "Group"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(18, 232)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 14)
        Me.Label5.TabIndex = 133
        Me.Label5.Text = "Stage"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(17, 176)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 14)
        Me.Label4.TabIndex = 132
        Me.Label4.Text = "Promotion"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(17, 123)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 14)
        Me.Label3.TabIndex = 131
        Me.Label3.Text = "Field"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(22, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 14)
        Me.Label2.TabIndex = 130
        Me.Label2.Text = "Degree"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(19, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 14)
        Me.Label1.TabIndex = 129
        Me.Label1.Text = "School"
        '
        'cboDegree
        '
        Me.cboDegree.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboDegree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDegree.Location = New System.Drawing.Point(20, 42)
        Me.cboDegree.Name = "cboDegree"
        Me.cboDegree.Size = New System.Drawing.Size(243, 21)
        Me.cboDegree.TabIndex = 124
        '
        'cboSchool
        '
        Me.cboSchool.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboSchool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSchool.Location = New System.Drawing.Point(19, 93)
        Me.cboSchool.Name = "cboSchool"
        Me.cboSchool.Size = New System.Drawing.Size(243, 21)
        Me.cboSchool.TabIndex = 125
        '
        'cboPromotion
        '
        Me.cboPromotion.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboPromotion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPromotion.Location = New System.Drawing.Point(19, 198)
        Me.cboPromotion.Name = "cboPromotion"
        Me.cboPromotion.Size = New System.Drawing.Size(243, 21)
        Me.cboPromotion.TabIndex = 127
        '
        'cboStage
        '
        Me.cboStage.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboStage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStage.Location = New System.Drawing.Point(19, 254)
        Me.cboStage.Name = "cboStage"
        Me.cboStage.Size = New System.Drawing.Size(196, 21)
        Me.cboStage.TabIndex = 128
        '
        'cboField
        '
        Me.cboField.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboField.Location = New System.Drawing.Point(19, 145)
        Me.cboField.Name = "cboField"
        Me.cboField.Size = New System.Drawing.Size(243, 21)
        Me.cboField.TabIndex = 126
        '
        'chbShowQR
        '
        Me.chbShowQR.AutoSize = True
        Me.chbShowQR.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbShowQR.Location = New System.Drawing.Point(19, 358)
        Me.chbShowQR.Name = "chbShowQR"
        Me.chbShowQR.Size = New System.Drawing.Size(83, 18)
        Me.chbShowQR.TabIndex = 139
        Me.chbShowQR.Text = "Show QR"
        Me.chbShowQR.UseVisualStyleBackColor = True
        '
        'STUDENT_LIST_PRINTED_CERTIFICATE_FRM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1031, 614)
        Me.Controls.Add(Me.chbShowQR)
        Me.Controls.Add(Me.chbAllStudyTime)
        Me.Controls.Add(Me.chbAllStage)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.cboGroup)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboDegree)
        Me.Controls.Add(Me.cboSchool)
        Me.Controls.Add(Me.cboPromotion)
        Me.Controls.Add(Me.cboStage)
        Me.Controls.Add(Me.cboField)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "STUDENT_LIST_PRINTED_CERTIFICATE_FRM"
        Me.Text = "STUDENT_LIST_PRINTED_CERTIFICATE"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents chbAllStudyTime As CheckBox
    Friend WithEvents chbAllStage As CheckBox
    Friend WithEvents btnPreview As Button
    Friend WithEvents cboGroup As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cboDegree As ComboBox
    Friend WithEvents cboSchool As ComboBox
    Friend WithEvents cboPromotion As ComboBox
    Friend WithEvents cboStage As ComboBox
    Friend WithEvents cboField As ComboBox
    Friend WithEvents chbShowQR As CheckBox
End Class
