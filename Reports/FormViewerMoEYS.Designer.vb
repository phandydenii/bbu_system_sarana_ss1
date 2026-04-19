<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormViewerMoEYS
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
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Entrance Exam List")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Conprehensive Exam List")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("MoEYS Official Lists")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("No Entrance Exam List")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("No Comprehensive Exam List")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("No MoEYS Official List")
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Higth School Certificate")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("No Hight School Certificate")
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.lblPromotion = New System.Windows.Forms.Label()
        Me.cboPromotion = New System.Windows.Forms.ComboBox()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.chkAllField = New System.Windows.Forms.CheckBox()
        Me.cboDegree = New System.Windows.Forms.ComboBox()
        Me.lblDegree = New System.Windows.Forms.Label()
        Me.lblSchool = New System.Windows.Forms.Label()
        Me.cboSchool = New System.Windows.Forms.ComboBox()
        Me.lblField = New System.Windows.Forms.Label()
        Me.cboField = New System.Windows.Forms.ComboBox()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(254, 618)
        Me.Splitter1.TabIndex = 1
        Me.Splitter1.TabStop = False
        '
        'lblPromotion
        '
        Me.lblPromotion.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPromotion.Location = New System.Drawing.Point(9, 134)
        Me.lblPromotion.Name = "lblPromotion"
        Me.lblPromotion.Size = New System.Drawing.Size(104, 16)
        Me.lblPromotion.TabIndex = 86
        Me.lblPromotion.Text = "Promotion"
        Me.lblPromotion.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboPromotion
        '
        Me.cboPromotion.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboPromotion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPromotion.Location = New System.Drawing.Point(12, 153)
        Me.cboPromotion.Name = "cboPromotion"
        Me.cboPromotion.Size = New System.Drawing.Size(224, 21)
        Me.cboPromotion.TabIndex = 87
        '
        'TreeView1
        '
        Me.TreeView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TreeView1.Location = New System.Drawing.Point(12, 180)
        Me.TreeView1.Name = "TreeView1"
        TreeNode1.Name = "Node8"
        TreeNode1.Tag = "1"
        TreeNode1.Text = "Entrance Exam List"
        TreeNode2.Name = "Node9"
        TreeNode2.Tag = "2"
        TreeNode2.Text = "Conprehensive Exam List"
        TreeNode3.Name = "Node10"
        TreeNode3.Tag = "3"
        TreeNode3.Text = "MoEYS Official Lists"
        TreeNode4.Name = "Node6"
        TreeNode4.Tag = "4"
        TreeNode4.Text = "No Entrance Exam List"
        TreeNode5.Name = "Node7"
        TreeNode5.Tag = "5"
        TreeNode5.Text = "No Comprehensive Exam List"
        TreeNode6.Name = "Node8"
        TreeNode6.Tag = "6"
        TreeNode6.Text = "No MoEYS Official List"
        TreeNode7.Name = "Node0"
        TreeNode7.Tag = "7"
        TreeNode7.Text = "Higth School Certificate"
        TreeNode8.Name = "Node0"
        TreeNode8.Tag = "8"
        TreeNode8.Text = "No Hight School Certificate"
        Me.TreeView1.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2, TreeNode3, TreeNode4, TreeNode5, TreeNode6, TreeNode7, TreeNode8})
        Me.TreeView1.Size = New System.Drawing.Size(224, 158)
        Me.TreeView1.TabIndex = 85
        '
        'chkAllField
        '
        Me.chkAllField.AutoSize = True
        Me.chkAllField.Location = New System.Drawing.Point(199, 112)
        Me.chkAllField.Name = "chkAllField"
        Me.chkAllField.Size = New System.Drawing.Size(37, 17)
        Me.chkAllField.TabIndex = 84
        Me.chkAllField.Text = "All"
        Me.chkAllField.UseVisualStyleBackColor = True
        '
        'cboDegree
        '
        Me.cboDegree.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboDegree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDegree.Location = New System.Drawing.Point(12, 25)
        Me.cboDegree.Name = "cboDegree"
        Me.cboDegree.Size = New System.Drawing.Size(224, 21)
        Me.cboDegree.TabIndex = 79
        '
        'lblDegree
        '
        Me.lblDegree.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDegree.Location = New System.Drawing.Point(12, 9)
        Me.lblDegree.Name = "lblDegree"
        Me.lblDegree.Size = New System.Drawing.Size(198, 13)
        Me.lblDegree.TabIndex = 78
        Me.lblDegree.Text = "Degree"
        Me.lblDegree.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lblSchool
        '
        Me.lblSchool.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSchool.Location = New System.Drawing.Point(12, 53)
        Me.lblSchool.Name = "lblSchool"
        Me.lblSchool.Size = New System.Drawing.Size(183, 13)
        Me.lblSchool.TabIndex = 80
        Me.lblSchool.Text = "School"
        Me.lblSchool.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboSchool
        '
        Me.cboSchool.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboSchool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSchool.Location = New System.Drawing.Point(12, 69)
        Me.cboSchool.Name = "cboSchool"
        Me.cboSchool.Size = New System.Drawing.Size(224, 21)
        Me.cboSchool.TabIndex = 81
        '
        'lblField
        '
        Me.lblField.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblField.Location = New System.Drawing.Point(12, 94)
        Me.lblField.Name = "lblField"
        Me.lblField.Size = New System.Drawing.Size(116, 13)
        Me.lblField.TabIndex = 82
        Me.lblField.Text = "Field"
        Me.lblField.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboField
        '
        Me.cboField.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboField.Location = New System.Drawing.Point(12, 110)
        Me.cboField.Name = "cboField"
        Me.cboField.Size = New System.Drawing.Size(169, 21)
        Me.cboField.TabIndex = 83
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportViewer1.Location = New System.Drawing.Point(254, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(620, 618)
        Me.ReportViewer1.TabIndex = 88
        '
        'btnPreview
        '
        Me.btnPreview.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.Location = New System.Drawing.Point(161, 344)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(75, 23)
        Me.btnPreview.TabIndex = 89
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'FormViewerMoEYS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(874, 618)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.lblPromotion)
        Me.Controls.Add(Me.cboPromotion)
        Me.Controls.Add(Me.TreeView1)
        Me.Controls.Add(Me.chkAllField)
        Me.Controls.Add(Me.cboDegree)
        Me.Controls.Add(Me.lblDegree)
        Me.Controls.Add(Me.lblSchool)
        Me.Controls.Add(Me.cboSchool)
        Me.Controls.Add(Me.lblField)
        Me.Controls.Add(Me.cboField)
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "FormViewerMoEYS"
        Me.Text = "FormViewerMoEYS"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents lblPromotion As Label
    Friend WithEvents cboPromotion As ComboBox
    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents chkAllField As CheckBox
    Friend WithEvents cboDegree As ComboBox
    Friend WithEvents lblDegree As Label
    Friend WithEvents lblSchool As Label
    Friend WithEvents cboSchool As ComboBox
    Friend WithEvents lblField As Label
    Friend WithEvents cboField As ComboBox
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents btnPreview As Button
End Class
