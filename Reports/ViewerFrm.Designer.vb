<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ViewerFrm
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
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Students Quit")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Students Quit => Resume")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Students Suspend")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Students Suspend => Resume")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Students Suppress")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Students Express")
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Students Change Group")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Students Return From Other Branch")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Students Change To Other Branch")
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Students Extend From Other University")
        Dim TreeNode11 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Students Extend From Other Branch")
        Dim TreeNode12 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Student Change School & Field")
        Dim TreeNode13 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Student Change School First Time")
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.lstGroup = New System.Windows.Forms.ListBox()
        Me.cboDegree = New System.Windows.Forms.ComboBox()
        Me.lblDegree = New System.Windows.Forms.Label()
        Me.lblSchool = New System.Windows.Forms.Label()
        Me.cboSchool = New System.Windows.Forms.ComboBox()
        Me.cboPromotion = New System.Windows.Forms.ComboBox()
        Me.cboStage = New System.Windows.Forms.ComboBox()
        Me.lblPromotion = New System.Windows.Forms.Label()
        Me.lblToolTib = New System.Windows.Forms.Label()
        Me.chkAllField = New System.Windows.Forms.CheckBox()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.txtSemester = New System.Windows.Forms.TextBox()
        Me.checkPrint = New System.Windows.Forms.CheckBox()
        Me.lblStage = New System.Windows.Forms.Label()
        Me.lblField = New System.Windows.Forms.Label()
        Me.cboField = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblGroup = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.SuspendLayout()
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd-MMM-yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(134, 283)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(104, 20)
        Me.dtpTo.TabIndex = 35
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(12, 283)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(116, 20)
        Me.dtpFrom.TabIndex = 34
        '
        'TreeView1
        '
        Me.TreeView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TreeView1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeView1.Location = New System.Drawing.Point(12, 322)
        Me.TreeView1.Name = "TreeView1"
        TreeNode1.Name = "Node0"
        TreeNode1.Tag = "1"
        TreeNode1.Text = "Students Quit"
        TreeNode2.Name = "Node1"
        TreeNode2.Tag = "2"
        TreeNode2.Text = "Students Quit => Resume"
        TreeNode3.Name = "Node2"
        TreeNode3.Tag = "3"
        TreeNode3.Text = "Students Suspend"
        TreeNode4.Name = "Node3"
        TreeNode4.Tag = "4"
        TreeNode4.Text = "Students Suspend => Resume"
        TreeNode5.Name = "Node4"
        TreeNode5.Tag = "5"
        TreeNode5.Text = "Students Suppress"
        TreeNode6.Name = "Node0"
        TreeNode6.Tag = "6"
        TreeNode6.Text = "Students Express"
        TreeNode7.Name = "Node5"
        TreeNode7.Tag = "7"
        TreeNode7.Text = "Students Change Group"
        TreeNode8.Name = "Node6"
        TreeNode8.Tag = "8"
        TreeNode8.Text = "Students Return From Other Branch"
        TreeNode9.Name = "Node7"
        TreeNode9.Tag = "9"
        TreeNode9.Text = "Students Change To Other Branch"
        TreeNode10.Name = "Node8"
        TreeNode10.Tag = "10"
        TreeNode10.Text = "Students Extend From Other University"
        TreeNode11.Name = "Node0"
        TreeNode11.Tag = "11"
        TreeNode11.Text = "Students Extend From Other Branch"
        TreeNode12.Name = "Node0"
        TreeNode12.Tag = "12"
        TreeNode12.Text = "Student Change School & Field"
        TreeNode13.Name = "Node0"
        TreeNode13.Tag = "13"
        TreeNode13.Text = "Student Change School First Time"
        Me.TreeView1.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2, TreeNode3, TreeNode4, TreeNode5, TreeNode6, TreeNode7, TreeNode8, TreeNode9, TreeNode10, TreeNode11, TreeNode12, TreeNode13})
        Me.TreeView1.Size = New System.Drawing.Size(269, 444)
        Me.TreeView1.TabIndex = 0
        '
        'lstGroup
        '
        Me.lstGroup.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lstGroup.Location = New System.Drawing.Point(189, 160)
        Me.lstGroup.Name = "lstGroup"
        Me.lstGroup.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstGroup.Size = New System.Drawing.Size(92, 95)
        Me.lstGroup.Sorted = True
        Me.lstGroup.TabIndex = 33
        '
        'cboDegree
        '
        Me.cboDegree.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboDegree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDegree.Location = New System.Drawing.Point(12, 30)
        Me.cboDegree.Name = "cboDegree"
        Me.cboDegree.Size = New System.Drawing.Size(269, 21)
        Me.cboDegree.TabIndex = 19
        '
        'lblDegree
        '
        Me.lblDegree.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDegree.Location = New System.Drawing.Point(12, 14)
        Me.lblDegree.Name = "lblDegree"
        Me.lblDegree.Size = New System.Drawing.Size(183, 13)
        Me.lblDegree.TabIndex = 18
        Me.lblDegree.Text = "Degree"
        Me.lblDegree.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lblSchool
        '
        Me.lblSchool.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSchool.Location = New System.Drawing.Point(12, 58)
        Me.lblSchool.Name = "lblSchool"
        Me.lblSchool.Size = New System.Drawing.Size(168, 13)
        Me.lblSchool.TabIndex = 20
        Me.lblSchool.Text = "School"
        Me.lblSchool.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboSchool
        '
        Me.cboSchool.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboSchool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSchool.Location = New System.Drawing.Point(12, 74)
        Me.cboSchool.Name = "cboSchool"
        Me.cboSchool.Size = New System.Drawing.Size(269, 21)
        Me.cboSchool.TabIndex = 21
        '
        'cboPromotion
        '
        Me.cboPromotion.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboPromotion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPromotion.Location = New System.Drawing.Point(12, 160)
        Me.cboPromotion.Name = "cboPromotion"
        Me.cboPromotion.Size = New System.Drawing.Size(168, 21)
        Me.cboPromotion.TabIndex = 25
        '
        'cboStage
        '
        Me.cboStage.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboStage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStage.Location = New System.Drawing.Point(13, 200)
        Me.cboStage.Name = "cboStage"
        Me.cboStage.Size = New System.Drawing.Size(168, 21)
        Me.cboStage.TabIndex = 27
        '
        'lblPromotion
        '
        Me.lblPromotion.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPromotion.Location = New System.Drawing.Point(12, 144)
        Me.lblPromotion.Name = "lblPromotion"
        Me.lblPromotion.Size = New System.Drawing.Size(104, 16)
        Me.lblPromotion.TabIndex = 24
        Me.lblPromotion.Text = "Promotion"
        Me.lblPromotion.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lblToolTib
        '
        Me.lblToolTib.AutoSize = True
        Me.lblToolTib.ForeColor = System.Drawing.Color.Fuchsia
        Me.lblToolTib.Location = New System.Drawing.Point(13, 306)
        Me.lblToolTib.Name = "lblToolTib"
        Me.lblToolTib.Size = New System.Drawing.Size(43, 13)
        Me.lblToolTib.TabIndex = 3
        Me.lblToolTib.Text = "ToolTip"
        '
        'chkAllField
        '
        Me.chkAllField.AutoSize = True
        Me.chkAllField.Location = New System.Drawing.Point(244, 115)
        Me.chkAllField.Name = "chkAllField"
        Me.chkAllField.Size = New System.Drawing.Size(37, 17)
        Me.chkAllField.TabIndex = 41
        Me.chkAllField.Text = "All"
        Me.chkAllField.UseVisualStyleBackColor = True
        '
        'txtYear
        '
        Me.txtYear.Location = New System.Drawing.Point(13, 241)
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(77, 20)
        Me.txtYear.TabIndex = 38
        Me.txtYear.Text = "0"
        Me.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSemester
        '
        Me.txtSemester.Location = New System.Drawing.Point(109, 241)
        Me.txtSemester.Name = "txtSemester"
        Me.txtSemester.Size = New System.Drawing.Size(72, 20)
        Me.txtSemester.TabIndex = 37
        Me.txtSemester.Text = "0"
        Me.txtSemester.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'checkPrint
        '
        Me.checkPrint.AutoSize = True
        Me.checkPrint.Location = New System.Drawing.Point(244, 286)
        Me.checkPrint.Name = "checkPrint"
        Me.checkPrint.Size = New System.Drawing.Size(37, 17)
        Me.checkPrint.TabIndex = 36
        Me.checkPrint.Text = "All"
        Me.checkPrint.UseVisualStyleBackColor = True
        '
        'lblStage
        '
        Me.lblStage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStage.Location = New System.Drawing.Point(13, 184)
        Me.lblStage.Name = "lblStage"
        Me.lblStage.Size = New System.Drawing.Size(104, 16)
        Me.lblStage.TabIndex = 26
        Me.lblStage.Text = "Stage"
        Me.lblStage.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lblField
        '
        Me.lblField.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblField.Location = New System.Drawing.Point(12, 99)
        Me.lblField.Name = "lblField"
        Me.lblField.Size = New System.Drawing.Size(116, 13)
        Me.lblField.TabIndex = 22
        Me.lblField.Text = "Field"
        Me.lblField.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboField
        '
        Me.cboField.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboField.Location = New System.Drawing.Point(12, 115)
        Me.cboField.Name = "cboField"
        Me.cboField.Size = New System.Drawing.Size(226, 21)
        Me.cboField.TabIndex = 23
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(82, 264)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 16)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "To"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 264)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 16)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "From"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(106, 224)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 14)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "Semester"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 222)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 16)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "Year"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lblGroup
        '
        Me.lblGroup.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGroup.Location = New System.Drawing.Point(186, 141)
        Me.lblGroup.Name = "lblGroup"
        Me.lblGroup.Size = New System.Drawing.Size(62, 16)
        Me.lblGroup.TabIndex = 32
        Me.lblGroup.Text = "Groups"
        Me.lblGroup.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(307, 803)
        Me.Splitter1.TabIndex = 3
        Me.Splitter1.TabStop = False
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportViewer1.Location = New System.Drawing.Point(307, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(950, 803)
        Me.ReportViewer1.TabIndex = 42
        '
        'ViewerFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1257, 803)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.TreeView1)
        Me.Controls.Add(Me.lblToolTib)
        Me.Controls.Add(Me.chkAllField)
        Me.Controls.Add(Me.txtYear)
        Me.Controls.Add(Me.cboDegree)
        Me.Controls.Add(Me.txtSemester)
        Me.Controls.Add(Me.lblGroup)
        Me.Controls.Add(Me.checkPrint)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstGroup)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboField)
        Me.Controls.Add(Me.lblDegree)
        Me.Controls.Add(Me.lblField)
        Me.Controls.Add(Me.lblSchool)
        Me.Controls.Add(Me.lblStage)
        Me.Controls.Add(Me.cboSchool)
        Me.Controls.Add(Me.cboStage)
        Me.Controls.Add(Me.lblPromotion)
        Me.Controls.Add(Me.cboPromotion)
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "ViewerFrm"
        Me.Text = "ViewerFrm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents lstGroup As System.Windows.Forms.ListBox
    Friend WithEvents cboDegree As System.Windows.Forms.ComboBox
    Friend WithEvents lblDegree As System.Windows.Forms.Label
    Friend WithEvents lblSchool As System.Windows.Forms.Label
    Friend WithEvents cboSchool As System.Windows.Forms.ComboBox
    Friend WithEvents cboPromotion As System.Windows.Forms.ComboBox
    Friend WithEvents cboStage As System.Windows.Forms.ComboBox
    Friend WithEvents lblPromotion As System.Windows.Forms.Label
    Friend WithEvents lblStage As System.Windows.Forms.Label
    Friend WithEvents lblGroup As System.Windows.Forms.Label
    Friend WithEvents lblField As System.Windows.Forms.Label
    Friend WithEvents cboField As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents checkPrint As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents txtSemester As System.Windows.Forms.TextBox
    Friend WithEvents chkAllField As System.Windows.Forms.CheckBox
    Friend WithEvents lblToolTib As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
End Class
