<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MasterFinishGoods
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btn_ex_template = New System.Windows.Forms.Button()
        Me.btn_export_template = New System.Windows.Forms.Button()
        Me.txt_dept = New System.Windows.Forms.ComboBox()
        Me.txt_laser = New System.Windows.Forms.TextBox()
        Me.txt_spq = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_level = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_pn = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_masterfinishgoods_search = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.dgv_finish_goods = New System.Windows.Forms.DataGridView()
        Me.txt_desc = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv_finish_goods, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btn_ex_template)
        Me.GroupBox1.Controls.Add(Me.btn_export_template)
        Me.GroupBox1.Controls.Add(Me.txt_dept)
        Me.GroupBox1.Controls.Add(Me.txt_laser)
        Me.GroupBox1.Controls.Add(Me.txt_spq)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txt_level)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txt_pn)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txt_masterfinishgoods_search)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.dgv_finish_goods)
        Me.GroupBox1.Controls.Add(Me.txt_desc)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(0, -4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1913, 705)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'btn_ex_template
        '
        Me.btn_ex_template.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_ex_template.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_ex_template.ForeColor = System.Drawing.Color.White
        Me.btn_ex_template.Location = New System.Drawing.Point(1351, 63)
        Me.btn_ex_template.Name = "btn_ex_template"
        Me.btn_ex_template.Size = New System.Drawing.Size(210, 42)
        Me.btn_ex_template.TabIndex = 20
        Me.btn_ex_template.Text = "Export Template"
        Me.btn_ex_template.UseVisualStyleBackColor = False
        '
        'btn_export_template
        '
        Me.btn_export_template.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_export_template.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btn_export_template.ForeColor = System.Drawing.Color.White
        Me.btn_export_template.Location = New System.Drawing.Point(1586, 63)
        Me.btn_export_template.Name = "btn_export_template"
        Me.btn_export_template.Size = New System.Drawing.Size(193, 42)
        Me.btn_export_template.TabIndex = 19
        Me.btn_export_template.Text = "Export to Excel"
        Me.btn_export_template.UseVisualStyleBackColor = False
        '
        'txt_dept
        '
        Me.txt_dept.FormattingEnabled = True
        Me.txt_dept.Location = New System.Drawing.Point(167, 28)
        Me.txt_dept.Name = "txt_dept"
        Me.txt_dept.Size = New System.Drawing.Size(198, 37)
        Me.txt_dept.TabIndex = 18
        '
        'txt_laser
        '
        Me.txt_laser.Location = New System.Drawing.Point(919, 93)
        Me.txt_laser.Name = "txt_laser"
        Me.txt_laser.Size = New System.Drawing.Size(220, 35)
        Me.txt_laser.TabIndex = 17
        '
        'txt_spq
        '
        Me.txt_spq.Location = New System.Drawing.Point(919, 28)
        Me.txt_spq.Name = "txt_spq"
        Me.txt_spq.Size = New System.Drawing.Size(220, 35)
        Me.txt_spq.TabIndex = 16
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(775, 96)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(138, 29)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Laser Code"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(775, 31)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 29)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "SPQ"
        '
        'txt_level
        '
        Me.txt_level.Location = New System.Drawing.Point(498, 90)
        Me.txt_level.Name = "txt_level"
        Me.txt_level.Size = New System.Drawing.Size(227, 35)
        Me.txt_level.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(401, 93)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 29)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Level"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(1598, 638)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 29)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Search"
        '
        'txt_pn
        '
        Me.txt_pn.Location = New System.Drawing.Point(167, 90)
        Me.txt_pn.Name = "txt_pn"
        Me.txt_pn.Size = New System.Drawing.Size(198, 35)
        Me.txt_pn.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(149, 29)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Part Number"
        '
        'txt_masterfinishgoods_search
        '
        Me.txt_masterfinishgoods_search.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_masterfinishgoods_search.Location = New System.Drawing.Point(1690, 635)
        Me.txt_masterfinishgoods_search.Name = "txt_masterfinishgoods_search"
        Me.txt_masterfinishgoods_search.Size = New System.Drawing.Size(217, 35)
        Me.txt_masterfinishgoods_search.TabIndex = 8
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button3.BackColor = System.Drawing.Color.Red
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Location = New System.Drawing.Point(6, 631)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(242, 42)
        Me.Button3.TabIndex = 7
        Me.Button3.Text = "Delete Multiple Data"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.Green
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(1801, 64)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(106, 41)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Import"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(1212, 64)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(102, 41)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Add"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'dgv_finish_goods
        '
        Me.dgv_finish_goods.AllowUserToAddRows = False
        Me.dgv_finish_goods.AllowUserToDeleteRows = False
        Me.dgv_finish_goods.AllowUserToOrderColumns = True
        Me.dgv_finish_goods.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_finish_goods.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_finish_goods.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_finish_goods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_finish_goods.GridColor = System.Drawing.SystemColors.Highlight
        Me.dgv_finish_goods.Location = New System.Drawing.Point(6, 152)
        Me.dgv_finish_goods.MultiSelect = False
        Me.dgv_finish_goods.Name = "dgv_finish_goods"
        Me.dgv_finish_goods.ReadOnly = True
        Me.dgv_finish_goods.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.dgv_finish_goods.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgv_finish_goods.Size = New System.Drawing.Size(1901, 473)
        Me.dgv_finish_goods.TabIndex = 4
        '
        'txt_desc
        '
        Me.txt_desc.Location = New System.Drawing.Point(498, 28)
        Me.txt_desc.Name = "txt_desc"
        Me.txt_desc.Size = New System.Drawing.Size(227, 35)
        Me.txt_desc.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(401, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 29)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Desc"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(138, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Department"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'MasterFinishGoods
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1912, 696)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "MasterFinishGoods"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Master Finish Goods"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgv_finish_goods, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txt_level As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txt_pn As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txt_masterfinishgoods_search As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents dgv_finish_goods As DataGridView
    Friend WithEvents txt_desc As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txt_laser As TextBox
    Friend WithEvents txt_spq As TextBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents txt_dept As ComboBox
    Friend WithEvents btn_ex_template As Button
    Friend WithEvents btn_export_template As Button
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
End Class
