<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormViewerStudentProblem
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdoFinanceProblem = New System.Windows.Forms.RadioButton()
        Me.rdoAcademicProblem = New System.Windows.Forms.RadioButton()
        Me.btnCancelSP = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboFieldSP = New System.Windows.Forms.ComboBox()
        Me.txtSemesterSP = New System.Windows.Forms.TextBox()
        Me.txtYearSP = New System.Windows.Forms.TextBox()
        Me.btnPreviewSP = New System.Windows.Forms.Button()
        Me.lstGroupSP = New System.Windows.Forms.ListBox()
        Me.cboDegreeSP = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboSchoolSP = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboPromotionSP = New System.Windows.Forms.ComboBox()
        Me.cboStageSP = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.chkGroupSP = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdoFinanceProblem)
        Me.GroupBox1.Controls.Add(Me.rdoAcademicProblem)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 297)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(256, 48)
        Me.GroupBox1.TabIndex = 75
        Me.GroupBox1.TabStop = False
        '
        'rdoFinanceProblem
        '
        Me.rdoFinanceProblem.AutoSize = True
        Me.rdoFinanceProblem.Checked = True
        Me.rdoFinanceProblem.Location = New System.Drawing.Point(18, 18)
        Me.rdoFinanceProblem.Name = "rdoFinanceProblem"
        Me.rdoFinanceProblem.Size = New System.Drawing.Size(63, 17)
        Me.rdoFinanceProblem.TabIndex = 55
        Me.rdoFinanceProblem.TabStop = True
        Me.rdoFinanceProblem.Text = "Finance"
        Me.rdoFinanceProblem.UseVisualStyleBackColor = True
        '
        'rdoAcademicProblem
        '
        Me.rdoAcademicProblem.AutoSize = True
        Me.rdoAcademicProblem.Location = New System.Drawing.Point(152, 18)
        Me.rdoAcademicProblem.Name = "rdoAcademicProblem"
        Me.rdoAcademicProblem.Size = New System.Drawing.Size(72, 17)
        Me.rdoAcademicProblem.TabIndex = 55
        Me.rdoAcademicProblem.Text = "Academic"
        Me.rdoAcademicProblem.UseVisualStyleBackColor = True
        '
        'btnCancelSP
        '
        Me.btnCancelSP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelSP.Location = New System.Drawing.Point(292, 67)
        Me.btnCancelSP.Name = "btnCancelSP"
        Me.btnCancelSP.Size = New System.Drawing.Size(75, 24)
        Me.btnCancelSP.TabIndex = 74
        Me.btnCancelSP.Text = "&Cancel"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(12, 91)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(256, 16)
        Me.Label7.TabIndex = 61
        Me.Label7.Text = "Field"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboFieldSP
        '
        Me.cboFieldSP.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboFieldSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFieldSP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFieldSP.Location = New System.Drawing.Point(12, 107)
        Me.cboFieldSP.Name = "cboFieldSP"
        Me.cboFieldSP.Size = New System.Drawing.Size(256, 22)
        Me.cboFieldSP.TabIndex = 62
        '
        'txtSemesterSP
        '
        Me.txtSemesterSP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSemesterSP.Location = New System.Drawing.Point(12, 267)
        Me.txtSemesterSP.Name = "txtSemesterSP"
        Me.txtSemesterSP.Size = New System.Drawing.Size(112, 22)
        Me.txtSemesterSP.TabIndex = 70
        Me.txtSemesterSP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtYearSP
        '
        Me.txtYearSP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYearSP.Location = New System.Drawing.Point(12, 227)
        Me.txtYearSP.Name = "txtYearSP"
        Me.txtYearSP.Size = New System.Drawing.Size(112, 22)
        Me.txtYearSP.TabIndex = 68
        Me.txtYearSP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnPreviewSP
        '
        Me.btnPreviewSP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreviewSP.Location = New System.Drawing.Point(292, 27)
        Me.btnPreviewSP.Name = "btnPreviewSP"
        Me.btnPreviewSP.Size = New System.Drawing.Size(75, 24)
        Me.btnPreviewSP.TabIndex = 72
        Me.btnPreviewSP.Text = "&Preview"
        '
        'lstGroupSP
        '
        Me.lstGroupSP.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lstGroupSP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstGroupSP.ItemHeight = 14
        Me.lstGroupSP.Location = New System.Drawing.Point(164, 147)
        Me.lstGroupSP.Name = "lstGroupSP"
        Me.lstGroupSP.Size = New System.Drawing.Size(104, 144)
        Me.lstGroupSP.Sorted = True
        Me.lstGroupSP.TabIndex = 71
        '
        'cboDegreeSP
        '
        Me.cboDegreeSP.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboDegreeSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDegreeSP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDegreeSP.Location = New System.Drawing.Point(12, 27)
        Me.cboDegreeSP.Name = "cboDegreeSP"
        Me.cboDegreeSP.Size = New System.Drawing.Size(256, 22)
        Me.cboDegreeSP.TabIndex = 58
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(12, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(256, 16)
        Me.Label8.TabIndex = 57
        Me.Label8.Text = "Degree"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(12, 51)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(256, 16)
        Me.Label9.TabIndex = 59
        Me.Label9.Text = "School"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboSchoolSP
        '
        Me.cboSchoolSP.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboSchoolSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSchoolSP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSchoolSP.Location = New System.Drawing.Point(12, 67)
        Me.cboSchoolSP.Name = "cboSchoolSP"
        Me.cboSchoolSP.Size = New System.Drawing.Size(256, 22)
        Me.cboSchoolSP.TabIndex = 60
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 131)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(104, 16)
        Me.Label10.TabIndex = 63
        Me.Label10.Text = "Promotion"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboPromotionSP
        '
        Me.cboPromotionSP.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboPromotionSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPromotionSP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPromotionSP.Location = New System.Drawing.Point(12, 147)
        Me.cboPromotionSP.Name = "cboPromotionSP"
        Me.cboPromotionSP.Size = New System.Drawing.Size(112, 22)
        Me.cboPromotionSP.TabIndex = 64
        '
        'cboStageSP
        '
        Me.cboStageSP.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboStageSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStageSP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStageSP.Location = New System.Drawing.Point(12, 187)
        Me.cboStageSP.Name = "cboStageSP"
        Me.cboStageSP.Size = New System.Drawing.Size(112, 22)
        Me.cboStageSP.TabIndex = 66
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(12, 171)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(112, 16)
        Me.Label11.TabIndex = 65
        Me.Label11.Text = "Stage"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(12, 211)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(112, 16)
        Me.Label12.TabIndex = 67
        Me.Label12.Text = "Year"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(12, 251)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(112, 16)
        Me.Label13.TabIndex = 69
        Me.Label13.Text = "Semester"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'chkGroupSP
        '
        Me.chkGroupSP.AutoSize = True
        Me.chkGroupSP.Checked = True
        Me.chkGroupSP.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkGroupSP.Location = New System.Drawing.Point(164, 131)
        Me.chkGroupSP.Name = "chkGroupSP"
        Me.chkGroupSP.Size = New System.Drawing.Size(55, 17)
        Me.chkGroupSP.TabIndex = 73
        Me.chkGroupSP.Text = "Group"
        Me.chkGroupSP.UseVisualStyleBackColor = True
        '
        'FormViewerStudentProblem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(419, 408)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCancelSP)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboFieldSP)
        Me.Controls.Add(Me.txtSemesterSP)
        Me.Controls.Add(Me.txtYearSP)
        Me.Controls.Add(Me.btnPreviewSP)
        Me.Controls.Add(Me.lstGroupSP)
        Me.Controls.Add(Me.cboDegreeSP)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cboSchoolSP)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cboPromotionSP)
        Me.Controls.Add(Me.cboStageSP)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.chkGroupSP)
        Me.Name = "FormViewerStudentProblem"
        Me.Text = "FormViewerStudentProblem"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoFinanceProblem As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAcademicProblem As System.Windows.Forms.RadioButton
    Friend WithEvents btnCancelSP As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboFieldSP As System.Windows.Forms.ComboBox
    Friend WithEvents txtSemesterSP As System.Windows.Forms.TextBox
    Friend WithEvents txtYearSP As System.Windows.Forms.TextBox
    Friend WithEvents btnPreviewSP As System.Windows.Forms.Button
    Friend WithEvents lstGroupSP As System.Windows.Forms.ListBox
    Friend WithEvents cboDegreeSP As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboSchoolSP As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboPromotionSP As System.Windows.Forms.ComboBox
    Friend WithEvents cboStageSP As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents chkGroupSP As System.Windows.Forms.CheckBox
End Class
