﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.txt_masterfinishgoods_family = New System.Windows.Forms.TextBox()
        Me.cb_masterfinishgoods_component = New System.Windows.Forms.ComboBox()
        Me.btn_ex_template = New System.Windows.Forms.Button()
        Me.btn_export_Master_Usage_Finish_Goods = New System.Windows.Forms.Button()
        Me.cb_masterfinishgoods_pn = New System.Windows.Forms.ComboBox()
        Me.txt_masterfinishgoods_usage = New System.Windows.Forms.TextBox()
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
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv_masterfinishgoods_atas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.DataGridView1)
        Me.GroupBox1.Controls.Add(Me.Button4)
        Me.GroupBox1.Controls.Add(Me.RadioButton2)
        Me.GroupBox1.Controls.Add(Me.RadioButton1)
        Me.GroupBox1.Controls.Add(Me.txt_masterfinishgoods_family)
        Me.GroupBox1.Controls.Add(Me.cb_masterfinishgoods_component)
        Me.GroupBox1.Controls.Add(Me.btn_ex_template)
        Me.GroupBox1.Controls.Add(Me.btn_export_Master_Usage_Finish_Goods)
        Me.GroupBox1.Controls.Add(Me.cb_masterfinishgoods_pn)
        Me.GroupBox1.Controls.Add(Me.txt_masterfinishgoods_usage)
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
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(888, 75)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(144, 34)
        Me.DataGridView1.TabIndex = 37
        Me.DataGridView1.Visible = False
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button4.ForeColor = System.Drawing.Color.White
        Me.Button4.Location = New System.Drawing.Point(1449, 71)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(210, 42)
        Me.Button4.TabIndex = 36
        Me.Button4.Text = "Export All FG"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'RadioButton2
        '
        Me.RadioButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(1190, 608)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(370, 33)
        Me.RadioButton2.TabIndex = 35
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Search Component From Table"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(843, 608)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(252, 33)
        Me.RadioButton1.TabIndex = 34
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Search FG From List"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'txt_masterfinishgoods_family
        '
        Me.txt_masterfinishgoods_family.Location = New System.Drawing.Point(619, 27)
        Me.txt_masterfinishgoods_family.Name = "txt_masterfinishgoods_family"
        Me.txt_masterfinishgoods_family.ReadOnly = True
        Me.txt_masterfinishgoods_family.Size = New System.Drawing.Size(232, 35)
        Me.txt_masterfinishgoods_family.TabIndex = 32
        '
        'cb_masterfinishgoods_component
        '
        Me.cb_masterfinishgoods_component.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_masterfinishgoods_component.FormattingEnabled = True
        Me.cb_masterfinishgoods_component.Location = New System.Drawing.Point(200, 75)
        Me.cb_masterfinishgoods_component.Name = "cb_masterfinishgoods_component"
        Me.cb_masterfinishgoods_component.Size = New System.Drawing.Size(247, 37)
        Me.cb_masterfinishgoods_component.TabIndex = 31
        '
        'btn_ex_template
        '
        Me.btn_ex_template.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_ex_template.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_ex_template.ForeColor = System.Drawing.Color.White
        Me.btn_ex_template.Location = New System.Drawing.Point(1449, 20)
        Me.btn_ex_template.Name = "btn_ex_template"
        Me.btn_ex_template.Size = New System.Drawing.Size(210, 42)
        Me.btn_ex_template.TabIndex = 29
        Me.btn_ex_template.Text = "Export Template"
        Me.btn_ex_template.UseVisualStyleBackColor = False
        '
        'btn_export_Master_Usage_Finish_Goods
        '
        Me.btn_export_Master_Usage_Finish_Goods.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_export_Master_Usage_Finish_Goods.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btn_export_Master_Usage_Finish_Goods.ForeColor = System.Drawing.Color.White
        Me.btn_export_Master_Usage_Finish_Goods.Location = New System.Drawing.Point(1680, 71)
        Me.btn_export_Master_Usage_Finish_Goods.Name = "btn_export_Master_Usage_Finish_Goods"
        Me.btn_export_Master_Usage_Finish_Goods.Size = New System.Drawing.Size(236, 42)
        Me.btn_export_Master_Usage_Finish_Goods.TabIndex = 28
        Me.btn_export_Master_Usage_Finish_Goods.Text = "Export Selected FG"
        Me.btn_export_Master_Usage_Finish_Goods.UseVisualStyleBackColor = False
        '
        'cb_masterfinishgoods_pn
        '
        Me.cb_masterfinishgoods_pn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_masterfinishgoods_pn.FormattingEnabled = True
        Me.cb_masterfinishgoods_pn.Location = New System.Drawing.Point(200, 27)
        Me.cb_masterfinishgoods_pn.Name = "cb_masterfinishgoods_pn"
        Me.cb_masterfinishgoods_pn.Size = New System.Drawing.Size(247, 37)
        Me.cb_masterfinishgoods_pn.TabIndex = 26
        '
        'txt_masterfinishgoods_usage
        '
        Me.txt_masterfinishgoods_usage.Location = New System.Drawing.Point(972, 24)
        Me.txt_masterfinishgoods_usage.Name = "txt_masterfinishgoods_usage"
        Me.txt_masterfinishgoods_usage.Size = New System.Drawing.Size(222, 35)
        Me.txt_masterfinishgoods_usage.TabIndex = 25
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(883, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 29)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "Usage"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 77)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(138, 29)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Component"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(475, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 29)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Family"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1628, 610)
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
        Me.TreeView1.Size = New System.Drawing.Size(385, 468)
        Me.TreeView1.TabIndex = 18
        '
        'txt_masterfinishgoods_search
        '
        Me.txt_masterfinishgoods_search.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_masterfinishgoods_search.Location = New System.Drawing.Point(1723, 607)
        Me.txt_masterfinishgoods_search.Name = "txt_masterfinishgoods_search"
        Me.txt_masterfinishgoods_search.Size = New System.Drawing.Size(193, 35)
        Me.txt_masterfinishgoods_search.TabIndex = 17
        Me.txt_masterfinishgoods_search.Tag = "asd"
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.Red
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(397, 603)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(239, 43)
        Me.Button2.TabIndex = 16
        Me.Button2.Text = "Delete Multiple Data"
        Me.Button2.UseVisualStyleBackColor = False
        Me.Button2.Visible = False
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.BackColor = System.Drawing.Color.Green
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Location = New System.Drawing.Point(1786, 16)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(130, 50)
        Me.Button3.TabIndex = 15
        Me.Button3.Text = "Import"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(1220, 17)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(133, 49)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Save"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'dgv_masterfinishgoods_atas
        '
        Me.dgv_masterfinishgoods_atas.AllowUserToAddRows = False
        Me.dgv_masterfinishgoods_atas.AllowUserToDeleteRows = False
        Me.dgv_masterfinishgoods_atas.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_masterfinishgoods_atas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_masterfinishgoods_atas.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_masterfinishgoods_atas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_masterfinishgoods_atas.GridColor = System.Drawing.SystemColors.Highlight
        Me.dgv_masterfinishgoods_atas.Location = New System.Drawing.Point(397, 129)
        Me.dgv_masterfinishgoods_atas.MultiSelect = False
        Me.dgv_masterfinishgoods_atas.Name = "dgv_masterfinishgoods_atas"
        Me.dgv_masterfinishgoods_atas.ReadOnly = True
        Me.dgv_masterfinishgoods_atas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgv_masterfinishgoods_atas.Size = New System.Drawing.Size(1519, 468)
        Me.dgv_masterfinishgoods_atas.TabIndex = 4
        '
        'txt_masterfinishgoods_desc
        '
        Me.txt_masterfinishgoods_desc.Location = New System.Drawing.Point(620, 74)
        Me.txt_masterfinishgoods_desc.Name = "txt_masterfinishgoods_desc"
        Me.txt_masterfinishgoods_desc.ReadOnly = True
        Me.txt_masterfinishgoods_desc.Size = New System.Drawing.Size(232, 35)
        Me.txt_masterfinishgoods_desc.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(475, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(139, 29)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Desc Comp"
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
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents cb_masterfinishgoods_pn As ComboBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents btn_ex_template As Button
    Friend WithEvents btn_export_Master_Usage_Finish_Goods As Button
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents cb_masterfinishgoods_component As ComboBox
    Friend WithEvents txt_masterfinishgoods_family As TextBox
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents Button4 As Button
    Friend WithEvents DataGridView1 As DataGridView
End Class
