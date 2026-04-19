<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class STUDENT_ACCEPT_AND_NOT_CERTIFICATE_RPT
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboDegree = New System.Windows.Forms.ComboBox()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboSchool = New System.Windows.Forms.ComboBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboToPromotion = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboFromPromotion = New System.Windows.Forms.ComboBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.rdoNotAccept = New System.Windows.Forms.RadioButton()
        Me.rdoAccept = New System.Windows.Forms.RadioButton()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.btnViewList = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdoAllStatus = New System.Windows.Forms.RadioButton()
        Me.rdoGraduated = New System.Windows.Forms.RadioButton()
        Me.rdoCopleted = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(275, 571)
        Me.Splitter1.TabIndex = 91
        Me.Splitter1.TabStop = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(117, 312)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 16)
        Me.Label2.TabIndex = 104
        Me.Label2.Text = "To Date"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboDegree
        '
        Me.cboDegree.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboDegree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDegree.Location = New System.Drawing.Point(12, 25)
        Me.cboDegree.Name = "cboDegree"
        Me.cboDegree.Size = New System.Drawing.Size(257, 21)
        Me.cboDegree.TabIndex = 94
        '
        'btnPreview
        '
        Me.btnPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.Location = New System.Drawing.Point(12, 367)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(102, 23)
        Me.btnPreview.TabIndex = 101
        Me.btnPreview.Text = "View Total"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 312)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 16)
        Me.Label3.TabIndex = 92
        Me.Label3.Text = "From Date"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboSchool
        '
        Me.cboSchool.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboSchool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSchool.Location = New System.Drawing.Point(12, 69)
        Me.cboSchool.Name = "cboSchool"
        Me.cboSchool.Size = New System.Drawing.Size(257, 21)
        Me.cboSchool.TabIndex = 96
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(120, 331)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(103, 20)
        Me.dtpToDate.TabIndex = 103
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 53)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(168, 13)
        Me.Label4.TabIndex = 95
        Me.Label4.Text = "School"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(12, 331)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(102, 20)
        Me.dtpFromDate.TabIndex = 102
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(183, 13)
        Me.Label5.TabIndex = 93
        Me.Label5.Text = "Degree"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.cboToPromotion)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.cboFromPromotion)
        Me.GroupBox4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(12, 100)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(257, 75)
        Me.GroupBox4.TabIndex = 99
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = " Promotion"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(131, 21)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 16)
        Me.Label8.TabIndex = 61
        Me.Label8.Text = "To"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboToPromotion
        '
        Me.cboToPromotion.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboToPromotion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboToPromotion.Location = New System.Drawing.Point(134, 40)
        Me.cboToPromotion.Name = "cboToPromotion"
        Me.cboToPromotion.Size = New System.Drawing.Size(115, 22)
        Me.cboToPromotion.TabIndex = 62
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(11, 21)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(68, 16)
        Me.Label9.TabIndex = 51
        Me.Label9.Text = "From"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboFromPromotion
        '
        Me.cboFromPromotion.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboFromPromotion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFromPromotion.Location = New System.Drawing.Point(11, 40)
        Me.cboFromPromotion.Name = "cboFromPromotion"
        Me.cboFromPromotion.Size = New System.Drawing.Size(117, 22)
        Me.cboFromPromotion.Sorted = True
        Me.cboFromPromotion.TabIndex = 52
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox6.Controls.Add(Me.rdoNotAccept)
        Me.GroupBox6.Controls.Add(Me.rdoAccept)
        Me.GroupBox6.Location = New System.Drawing.Point(12, 183)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(257, 50)
        Me.GroupBox6.TabIndex = 97
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = " "
        '
        'rdoNotAccept
        '
        Me.rdoNotAccept.AutoSize = True
        Me.rdoNotAccept.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoNotAccept.Location = New System.Drawing.Point(31, 19)
        Me.rdoNotAccept.Name = "rdoNotAccept"
        Me.rdoNotAccept.Size = New System.Drawing.Size(89, 17)
        Me.rdoNotAccept.TabIndex = 3
        Me.rdoNotAccept.Text = "Not Accept"
        Me.rdoNotAccept.UseVisualStyleBackColor = True
        '
        'rdoAccept
        '
        Me.rdoAccept.AutoSize = True
        Me.rdoAccept.Checked = True
        Me.rdoAccept.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoAccept.Location = New System.Drawing.Point(165, 19)
        Me.rdoAccept.Name = "rdoAccept"
        Me.rdoAccept.Size = New System.Drawing.Size(65, 17)
        Me.rdoAccept.TabIndex = 2
        Me.rdoAccept.TabStop = True
        Me.rdoAccept.Text = "Accept"
        Me.rdoAccept.UseVisualStyleBackColor = True
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportViewer1.Location = New System.Drawing.Point(275, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(817, 571)
        Me.ReportViewer1.TabIndex = 105
        '
        'btnViewList
        '
        Me.btnViewList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewList.Location = New System.Drawing.Point(120, 367)
        Me.btnViewList.Name = "btnViewList"
        Me.btnViewList.Size = New System.Drawing.Size(103, 23)
        Me.btnViewList.TabIndex = 106
        Me.btnViewList.Text = "View List"
        Me.btnViewList.UseVisualStyleBackColor = True
        Me.btnViewList.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.rdoAllStatus)
        Me.GroupBox1.Controls.Add(Me.rdoGraduated)
        Me.GroupBox1.Controls.Add(Me.rdoCopleted)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 239)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(257, 50)
        Me.GroupBox1.TabIndex = 98
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Student Status"
        '
        'rdoAllStatus
        '
        Me.rdoAllStatus.AutoSize = True
        Me.rdoAllStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoAllStatus.Location = New System.Drawing.Point(14, 20)
        Me.rdoAllStatus.Name = "rdoAllStatus"
        Me.rdoAllStatus.Size = New System.Drawing.Size(39, 17)
        Me.rdoAllStatus.TabIndex = 4
        Me.rdoAllStatus.Text = "All"
        Me.rdoAllStatus.UseVisualStyleBackColor = True
        '
        'rdoGraduated
        '
        Me.rdoGraduated.AutoSize = True
        Me.rdoGraduated.Checked = True
        Me.rdoGraduated.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoGraduated.Location = New System.Drawing.Point(165, 20)
        Me.rdoGraduated.Name = "rdoGraduated"
        Me.rdoGraduated.Size = New System.Drawing.Size(84, 17)
        Me.rdoGraduated.TabIndex = 3
        Me.rdoGraduated.TabStop = True
        Me.rdoGraduated.Text = "Graduated"
        Me.rdoGraduated.UseVisualStyleBackColor = True
        '
        'rdoCopleted
        '
        Me.rdoCopleted.AutoSize = True
        Me.rdoCopleted.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoCopleted.Location = New System.Drawing.Point(75, 20)
        Me.rdoCopleted.Name = "rdoCopleted"
        Me.rdoCopleted.Size = New System.Drawing.Size(84, 17)
        Me.rdoCopleted.TabIndex = 2
        Me.rdoCopleted.Text = "Completed"
        Me.rdoCopleted.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Khmer OS Battambang", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(9, 416)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(252, 28)
        Me.Label1.TabIndex = 107
        Me.Label1.Text = "*Completed គឺនិស្សិតមិនទាន់គ្រប់លក្ខណ្ឌ"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Khmer OS Battambang", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(9, 444)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(252, 24)
        Me.Label6.TabIndex = 108
        Me.Label6.Text = "*Graduated គឺនិស្សិតគ្រប់លក្ខណ្ឌ"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'STUDENT_ACCEPT_AND_NOT_CERTIFICATE_RPT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1092, 571)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnViewList)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboDegree)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboSchool)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtpFromDate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "STUDENT_ACCEPT_AND_NOT_CERTIFICATE_RPT"
        Me.Text = "STUDENT_ACCEPT_AND_NOT_CERTIFICATE"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboDegree As System.Windows.Forms.ComboBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboSchool As System.Windows.Forms.ComboBox
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboToPromotion As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboFromPromotion As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoNotAccept As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAccept As System.Windows.Forms.RadioButton
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents btnViewList As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rdoGraduated As RadioButton
    Friend WithEvents rdoCopleted As RadioButton
    Friend WithEvents rdoAllStatus As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents Label6 As Label
End Class
