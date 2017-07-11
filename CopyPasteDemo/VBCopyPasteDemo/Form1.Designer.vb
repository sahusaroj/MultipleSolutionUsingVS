<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmContract
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
        Me.lstContractList = New System.Windows.Forms.ListView()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtContract = New System.Windows.Forms.TextBox()
        Me.groupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnPreClose = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnRemoveAll = New System.Windows.Forms.Button()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.groupBox1.SuspendLayout()
        Me.groupBox3.SuspendLayout()
        Me.groupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstContractList
        '
        Me.lstContractList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstContractList.FullRowSelect = True
        Me.lstContractList.GridLines = True
        Me.lstContractList.Location = New System.Drawing.Point(3, 16)
        Me.lstContractList.Name = "lstContractList"
        Me.lstContractList.ShowItemToolTips = True
        Me.lstContractList.Size = New System.Drawing.Size(385, 244)
        Me.lstContractList.TabIndex = 0
        Me.lstContractList.UseCompatibleStateImageBehavior = False
        '
        'groupBox1
        '
        Me.groupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.groupBox1.Controls.Add(Me.lstContractList)
        Me.groupBox1.Location = New System.Drawing.Point(10, 62)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(391, 263)
        Me.groupBox1.TabIndex = 6
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Contract List"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(209, 19)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(78, 23)
        Me.btnAdd.TabIndex = 3
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtContract
        '
        Me.txtContract.Location = New System.Drawing.Point(3, 20)
        Me.txtContract.Name = "txtContract"
        Me.txtContract.Size = New System.Drawing.Size(200, 20)
        Me.txtContract.TabIndex = 2
        '
        'groupBox3
        '
        Me.groupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.groupBox3.Controls.Add(Me.btnPreClose)
        Me.groupBox3.Controls.Add(Me.btnRemove)
        Me.groupBox3.Controls.Add(Me.btnRemoveAll)
        Me.groupBox3.Location = New System.Drawing.Point(407, 62)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Size = New System.Drawing.Size(87, 263)
        Me.groupBox3.TabIndex = 8
        Me.groupBox3.TabStop = False
        '
        'btnPreClose
        '
        Me.btnPreClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreClose.Location = New System.Drawing.Point(4, 234)
        Me.btnPreClose.Name = "btnPreClose"
        Me.btnPreClose.Size = New System.Drawing.Size(78, 23)
        Me.btnPreClose.TabIndex = 6
        Me.btnPreClose.Text = "Pre-Close"
        Me.btnPreClose.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(4, 16)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(78, 23)
        Me.btnRemove.TabIndex = 5
        Me.btnRemove.Text = "Remove"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnRemoveAll
        '
        Me.btnRemoveAll.Location = New System.Drawing.Point(4, 45)
        Me.btnRemoveAll.Name = "btnRemoveAll"
        Me.btnRemoveAll.Size = New System.Drawing.Size(78, 23)
        Me.btnRemoveAll.TabIndex = 4
        Me.btnRemoveAll.Text = "Remove All"
        Me.btnRemoveAll.UseVisualStyleBackColor = True
        '
        'groupBox2
        '
        Me.groupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.groupBox2.Controls.Add(Me.btnAdd)
        Me.groupBox2.Controls.Add(Me.txtContract)
        Me.groupBox2.Location = New System.Drawing.Point(10, 6)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(484, 50)
        Me.groupBox2.TabIndex = 7
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = "Add Single Contract"
        '
        'frmContract
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(504, 330)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.groupBox3)
        Me.Controls.Add(Me.groupBox2)
        Me.Name = "frmContract"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox3.ResumeLayout(False)
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents lstContractList As ListView
    Private WithEvents groupBox1 As GroupBox
    Private WithEvents btnAdd As Button
    Private WithEvents txtContract As TextBox
    Private WithEvents groupBox3 As GroupBox
    Private WithEvents btnPreClose As Button
    Private WithEvents btnRemove As Button
    Private WithEvents btnRemoveAll As Button
    Private WithEvents groupBox2 As GroupBox
End Class
