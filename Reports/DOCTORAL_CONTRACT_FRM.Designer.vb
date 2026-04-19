<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DOCTORAL_CONTRACT_FRM
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
        Me.lstGroup = New System.Windows.Forms.ListBox()
        Me.cboDegree = New System.Windows.Forms.ComboBox()
        Me.lblDegree = New System.Windows.Forms.Label()
        Me.lblSchool = New System.Windows.Forms.Label()
        Me.cboSchool = New System.Windows.Forms.ComboBox()
        Me.lblPromotion = New System.Windows.Forms.Label()
        Me.cboPromotion = New System.Windows.Forms.ComboBox()
        Me.cboStage = New System.Windows.Forms.ComboBox()
        Me.lblStage = New System.Windows.Forms.Label()
        Me.lblField = New System.Windows.Forms.Label()
        Me.cboField = New System.Windows.Forms.ComboBox()
        Me.lblGroup = New System.Windows.Forms.Label()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.txtSemester = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(234, 455)
        Me.Splitter1.TabIndex = 0
        Me.Splitter1.TabStop = False
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportViewer1.Location = New System.Drawing.Point(234, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(561, 455)
        Me.ReportViewer1.TabIndex = 1
        '
        'lstGroup
        '
        Me.lstGroup.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lstGroup.Location = New System.Drawing.Point(114, 252)
        Me.lstGroup.Name = "lstGroup"
        Me.lstGroup.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstGroup.Size = New System.Drawing.Size(89, 121)
        Me.lstGroup.Sorted = True
        Me.lstGroup.TabIndex = 55
        '
        'cboDegree
        '
        Me.cboDegree.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboDegree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDegree.Location = New System.Drawing.Point(12, 25)
        Me.cboDegree.Name = "cboDegree"
        Me.cboDegree.Size = New System.Drawing.Size(191, 21)
        Me.cboDegree.TabIndex = 43
        '
        'lblDegree
        '
        Me.lblDegree.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDegree.Location = New System.Drawing.Point(12, 9)
        Me.lblDegree.Name = "lblDegree"
        Me.lblDegree.Size = New System.Drawing.Size(183, 13)
        Me.lblDegree.TabIndex = 42
        Me.lblDegree.Text = "Degree"
        Me.lblDegree.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lblSchool
        '
        Me.lblSchool.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSchool.Location = New System.Drawing.Point(12, 53)
        Me.lblSchool.Name = "lblSchool"
        Me.lblSchool.Size = New System.Drawing.Size(168, 13)
        Me.lblSchool.TabIndex = 44
        Me.lblSchool.Text = "School"
        Me.lblSchool.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboSchool
        '
        Me.cboSchool.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboSchool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSchool.Location = New System.Drawing.Point(12, 69)
        Me.cboSchool.Name = "cboSchool"
        Me.cboSchool.Size = New System.Drawing.Size(191, 21)
        Me.cboSchool.TabIndex = 45
        '
        'lblPromotion
        '
        Me.lblPromotion.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPromotion.Location = New System.Drawing.Point(12, 139)
        Me.lblPromotion.Name = "lblPromotion"
        Me.lblPromotion.Size = New System.Drawing.Size(104, 16)
        Me.lblPromotion.TabIndex = 48
        Me.lblPromotion.Text = "Promotion"
        Me.lblPromotion.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboPromotion
        '
        Me.cboPromotion.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboPromotion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPromotion.Location = New System.Drawing.Point(12, 155)
        Me.cboPromotion.Name = "cboPromotion"
        Me.cboPromotion.Size = New System.Drawing.Size(191, 21)
        Me.cboPromotion.TabIndex = 49
        '
        'cboStage
        '
        Me.cboStage.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboStage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStage.Location = New System.Drawing.Point(13, 195)
        Me.cboStage.Name = "cboStage"
        Me.cboStage.Size = New System.Drawing.Size(190, 21)
        Me.cboStage.TabIndex = 51
        '
        'lblStage
        '
        Me.lblStage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStage.Location = New System.Drawing.Point(13, 179)
        Me.lblStage.Name = "lblStage"
        Me.lblStage.Size = New System.Drawing.Size(104, 16)
        Me.lblStage.TabIndex = 50
        Me.lblStage.Text = "Stage"
        Me.lblStage.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lblField
        '
        Me.lblField.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblField.Location = New System.Drawing.Point(12, 94)
        Me.lblField.Name = "lblField"
        Me.lblField.Size = New System.Drawing.Size(116, 13)
        Me.lblField.TabIndex = 46
        Me.lblField.Text = "Field"
        Me.lblField.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboField
        '
        Me.cboField.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboField.Location = New System.Drawing.Point(12, 110)
        Me.cboField.Name = "cboField"
        Me.cboField.Size = New System.Drawing.Size(191, 21)
        Me.cboField.TabIndex = 47
        '
        'lblGroup
        '
        Me.lblGroup.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGroup.Location = New System.Drawing.Point(111, 233)
        Me.lblGroup.Name = "lblGroup"
        Me.lblGroup.Size = New System.Drawing.Size(62, 16)
        Me.lblGroup.TabIndex = 54
        Me.lblGroup.Text = "Groups"
        Me.lblGroup.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'btnPreview
        '
        Me.btnPreview.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.Location = New System.Drawing.Point(126, 396)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(77, 23)
        Me.btnPreview.TabIndex = 64
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(28, 396)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(77, 23)
        Me.btnClose.TabIndex = 65
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 217)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 16)
        Me.Label3.TabIndex = 61
        Me.Label3.Text = "Year"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(13, 278)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 14)
        Me.Label4.TabIndex = 62
        Me.Label4.Text = "Semester"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtYear
        '
        Me.txtYear.Location = New System.Drawing.Point(13, 236)
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(72, 20)
        Me.txtYear.TabIndex = 60
        Me.txtYear.Text = "0"
        Me.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSemester
        '
        Me.txtSemester.Location = New System.Drawing.Point(16, 295)
        Me.txtSemester.Name = "txtSemester"
        Me.txtSemester.Size = New System.Drawing.Size(72, 20)
        Me.txtSemester.TabIndex = 59
        Me.txtSemester.Text = "0"
        Me.txtSemester.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DOCTORAL_CONTRACT_FRM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(795, 455)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.txtYear)
        Me.Controls.Add(Me.txtSemester)
        Me.Controls.Add(Me.lstGroup)
        Me.Controls.Add(Me.cboDegree)
        Me.Controls.Add(Me.lblDegree)
        Me.Controls.Add(Me.lblSchool)
        Me.Controls.Add(Me.cboSchool)
        Me.Controls.Add(Me.lblPromotion)
        Me.Controls.Add(Me.cboPromotion)
        Me.Controls.Add(Me.cboStage)
        Me.Controls.Add(Me.lblStage)
        Me.Controls.Add(Me.lblField)
        Me.Controls.Add(Me.cboField)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblGroup)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "DOCTORAL_CONTRACT_FRM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DOCTORAL_CONTRACT_FRM"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents lstGroup As System.Windows.Forms.ListBox
    Friend WithEvents cboDegree As System.Windows.Forms.ComboBox
    Friend WithEvents lblDegree As System.Windows.Forms.Label
    Friend WithEvents lblSchool As System.Windows.Forms.Label
    Friend WithEvents cboSchool As System.Windows.Forms.ComboBox
    Friend WithEvents lblPromotion As System.Windows.Forms.Label
    Friend WithEvents cboPromotion As System.Windows.Forms.ComboBox
    Friend WithEvents cboStage As System.Windows.Forms.ComboBox
    Friend WithEvents lblStage As System.Windows.Forms.Label
    Friend WithEvents lblField As System.Windows.Forms.Label
    Friend WithEvents cboField As System.Windows.Forms.ComboBox
    Friend WithEvents lblGroup As System.Windows.Forms.Label
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents txtSemester As System.Windows.Forms.TextBox
End Class
