<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MaterialUsageFinishGoods
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cb_masterfinishgoods_pn = New System.Windows.Forms.ComboBox()
        Me.txt_masterfinishgoods_usage = New System.Windows.Forms.TextBox()
        Me.txt_masterfinishgoods_comp = New System.Windows.Forms.TextBox()
        Me.txt_masterfinishgoods_family = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.txt_masterfinishgoods_search = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.dgv_masterfinishgoods_atas = New System.Windows.Forms.DataGridView()
        Me.txt_masterfinishgoods_desc = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv_masterfinishgoods_atas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Controls.Add(Me.cb_masterfinishgoods_pn)
        Me.GroupBox1.Controls.Add(Me.txt_masterfinishgoods_usage)
        Me.GroupBox1.Controls.Add(Me.txt_masterfinishgoods_comp)
        Me.GroupBox1.Controls.Add(Me.txt_masterfinishgoods_family)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TreeView1)
        Me.GroupBox1.Controls.Add(Me.txt_masterfinishgoods_search)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.dgv_masterfinishgoods_atas)
        Me.GroupBox1.Controls.Add(Me.txt_masterfinishgoods_desc)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(0, -12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1922, 648)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'cb_masterfinishgoods_pn
        '
        Me.cb_masterfinishgoods_pn.FormattingEnabled = True
        Me.cb_masterfinishgoods_pn.Location = New System.Drawing.Point(200, 27)
        Me.cb_masterfinishgoods_pn.Name = "cb_masterfinishgoods_pn"
        Me.cb_masterfinishgoods_pn.Size = New System.Drawing.Size(247, 37)
        Me.cb_masterfinishgoods_pn.TabIndex = 26
        '
        'txt_masterfinishgoods_usage
        '
        Me.txt_masterfinishgoods_usage.Location = New System.Drawing.Point(1022, 27)
        Me.txt_masterfinishgoods_usage.Name = "txt_masterfinishgoods_usage"
        Me.txt_masterfinishgoods_usage.Size = New System.Drawing.Size(222, 35)
        Me.txt_masterfinishgoods_usage.TabIndex = 25
        '
        'txt_masterfinishgoods_comp
        '
        Me.txt_masterfinishgoods_comp.Location = New System.Drawing.Point(638, 77)
        Me.txt_masterfinishgoods_comp.Name = "txt_masterfinishgoods_comp"
        Me.txt_masterfinishgoods_comp.Size = New System.Drawing.Size(232, 35)
        Me.txt_masterfinishgoods_comp.TabIndex = 24
        '
        'txt_masterfinishgoods_family
        '
        Me.txt_masterfinishgoods_family.Location = New System.Drawing.Point(638, 27)
        Me.txt_masterfinishgoods_family.Name = "txt_masterfinishgoods_family"
        Me.txt_masterfinishgoods_family.Size = New System.Drawing.Size(236, 35)
        Me.txt_masterfinishgoods_family.TabIndex = 23
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(933, 30)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 29)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "Usage"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(494, 80)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(138, 29)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Component"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(494, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 29)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Family"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1628, 580)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 29)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Search"
        '
        'TreeView1
        '
        Me.TreeView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TreeView1.Location = New System.Drawing.Point(6, 129)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(385, 442)
        Me.TreeView1.TabIndex = 18
        '
        'txt_masterfinishgoods_search
        '
        Me.txt_masterfinishgoods_search.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_masterfinishgoods_search.Location = New System.Drawing.Point(1723, 577)
        Me.txt_masterfinishgoods_search.Name = "txt_masterfinishgoods_search"
        Me.txt_masterfinishgoods_search.Size = New System.Drawing.Size(193, 35)
        Me.txt_masterfinishgoods_search.TabIndex = 17
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.Red
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(397, 577)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(239, 43)
        Me.Button2.TabIndex = 16
        Me.Button2.Text = "Delete Multiple Data"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.BackColor = System.Drawing.Color.Green
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Location = New System.Drawing.Point(1782, 25)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(130, 50)
        Me.Button3.TabIndex = 15
        Me.Button3.Text = "Import"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(1342, 26)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(133, 49)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Add"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'dgv_masterfinishgoods_atas
        '
        Me.dgv_masterfinishgoods_atas.AllowUserToAddRows = False
        Me.dgv_masterfinishgoods_atas.AllowUserToDeleteRows = False
        Me.dgv_masterfinishgoods_atas.AllowUserToOrderColumns = True
        Me.dgv_masterfinishgoods_atas.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_masterfinishgoods_atas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_masterfinishgoods_atas.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgv_masterfinishgoods_atas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_masterfinishgoods_atas.GridColor = System.Drawing.SystemColors.Highlight
        Me.dgv_masterfinishgoods_atas.Location = New System.Drawing.Point(397, 129)
        Me.dgv_masterfinishgoods_atas.MultiSelect = False
        Me.dgv_masterfinishgoods_atas.Name = "dgv_masterfinishgoods_atas"
        Me.dgv_masterfinishgoods_atas.ReadOnly = True
        Me.dgv_masterfinishgoods_atas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_masterfinishgoods_atas.Size = New System.Drawing.Size(1519, 442)
        Me.dgv_masterfinishgoods_atas.TabIndex = 4
        '
        'txt_masterfinishgoods_desc
        '
        Me.txt_masterfinishgoods_desc.Location = New System.Drawing.Point(200, 77)
        Me.txt_masterfinishgoods_desc.Name = "txt_masterfinishgoods_desc"
        Me.txt_masterfinishgoods_desc.Size = New System.Drawing.Size(247, 35)
        Me.txt_masterfinishgoods_desc.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(135, 29)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Description"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(188, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "FG Part Number"
        '
        'CheckBox1
        '
        Me.CheckBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.Location = New System.Drawing.Point(1363, 579)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(240, 33)
        Me.CheckBox1.TabIndex = 27
        Me.CheckBox1.Text = "Search From Table"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'MaterialUsageFinishGoods
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1924, 636)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "MaterialUsageFinishGoods"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Material Usage Finish Goods"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgv_masterfinishgoods_atas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button1 As Button
    Friend WithEvents dgv_masterfinishgoods_atas As DataGridView
    Friend WithEvents txt_masterfinishgoods_desc As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents txt_masterfinishgoods_search As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txt_masterfinishgoods_usage As TextBox
    Friend WithEvents txt_masterfinishgoods_comp As TextBox
    Friend WithEvents txt_masterfinishgoods_family As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents cb_masterfinishgoods_pn As ComboBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
End Class
