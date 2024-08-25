<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MasterMaterial
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgv_material = New Zuby.ADGV.AdvancedDataGridView()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cb_mastermaterial_dept = New System.Windows.Forms.ComboBox()
        Me.cb_mastermaterial_family = New System.Windows.Forms.ComboBox()
        Me.btn_ex_template = New System.Windows.Forms.Button()
        Me.btn_export_template = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_pn_name = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_mastermaterial_search = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txt_mastermaterial_qty = New System.Windows.Forms.TextBox()
        Me.txt_mastermaterial_pn = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv_material, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.dgv_material)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cb_mastermaterial_dept)
        Me.GroupBox1.Controls.Add(Me.cb_mastermaterial_family)
        Me.GroupBox1.Controls.Add(Me.btn_ex_template)
        Me.GroupBox1.Controls.Add(Me.btn_export_template)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txt_pn_name)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txt_mastermaterial_search)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.txt_mastermaterial_qty)
        Me.GroupBox1.Controls.Add(Me.txt_mastermaterial_pn)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(-2, -11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1925, 705)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'dgv_material
        '
        Me.dgv_material.AllowUserToAddRows = False
        Me.dgv_material.AllowUserToDeleteRows = False
        Me.dgv_material.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_material.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_material.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_material.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_material.FilterAndSortEnabled = True
        Me.dgv_material.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.dgv_material.GridColor = System.Drawing.SystemColors.Highlight
        Me.dgv_material.Location = New System.Drawing.Point(6, 134)
        Me.dgv_material.MultiSelect = False
        Me.dgv_material.Name = "dgv_material"
        Me.dgv_material.ReadOnly = True
        Me.dgv_material.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgv_material.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.dgv_material.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgv_material.Size = New System.Drawing.Size(1913, 520)
        Me.dgv_material.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.dgv_material.TabIndex = 19
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(738, 31)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(138, 29)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Department"
        '
        'cb_mastermaterial_dept
        '
        Me.cb_mastermaterial_dept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_mastermaterial_dept.FormattingEnabled = True
        Me.cb_mastermaterial_dept.Items.AddRange(New Object() {"zSFP", "zQSFP", "Ten60"})
        Me.cb_mastermaterial_dept.Location = New System.Drawing.Point(882, 28)
        Me.cb_mastermaterial_dept.Name = "cb_mastermaterial_dept"
        Me.cb_mastermaterial_dept.Size = New System.Drawing.Size(157, 37)
        Me.cb_mastermaterial_dept.TabIndex = 17
        '
        'cb_mastermaterial_family
        '
        Me.cb_mastermaterial_family.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_mastermaterial_family.FormattingEnabled = True
        Me.cb_mastermaterial_family.Location = New System.Drawing.Point(546, 81)
        Me.cb_mastermaterial_family.Name = "cb_mastermaterial_family"
        Me.cb_mastermaterial_family.Size = New System.Drawing.Size(151, 37)
        Me.cb_mastermaterial_family.TabIndex = 16
        '
        'btn_ex_template
        '
        Me.btn_ex_template.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_ex_template.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_ex_template.ForeColor = System.Drawing.Color.White
        Me.btn_ex_template.Location = New System.Drawing.Point(1557, 24)
        Me.btn_ex_template.Name = "btn_ex_template"
        Me.btn_ex_template.Size = New System.Drawing.Size(210, 42)
        Me.btn_ex_template.TabIndex = 15
        Me.btn_ex_template.Text = "Export Template"
        Me.btn_ex_template.UseVisualStyleBackColor = False
        '
        'btn_export_template
        '
        Me.btn_export_template.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_export_template.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btn_export_template.ForeColor = System.Drawing.Color.White
        Me.btn_export_template.Location = New System.Drawing.Point(1726, 77)
        Me.btn_export_template.Name = "btn_export_template"
        Me.btn_export_template.Size = New System.Drawing.Size(193, 42)
        Me.btn_export_template.TabIndex = 14
        Me.btn_export_template.Text = "Export to Excel"
        Me.btn_export_template.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(388, 84)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 29)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Family"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(1373, 667)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(323, 29)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Search Material Part Number"
        '
        'txt_pn_name
        '
        Me.txt_pn_name.Location = New System.Drawing.Point(164, 81)
        Me.txt_pn_name.Name = "txt_pn_name"
        Me.txt_pn_name.Size = New System.Drawing.Size(196, 35)
        Me.txt_pn_name.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 29)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "PN Name"
        '
        'txt_mastermaterial_search
        '
        Me.txt_mastermaterial_search.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_mastermaterial_search.Location = New System.Drawing.Point(1702, 664)
        Me.txt_mastermaterial_search.Name = "txt_mastermaterial_search"
        Me.txt_mastermaterial_search.Size = New System.Drawing.Size(217, 35)
        Me.txt_mastermaterial_search.TabIndex = 8
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button3.BackColor = System.Drawing.Color.Red
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Location = New System.Drawing.Point(6, 660)
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
        Me.Button2.Location = New System.Drawing.Point(1796, 25)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(123, 41)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Import"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(1124, 25)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(133, 41)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Save"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'txt_mastermaterial_qty
        '
        Me.txt_mastermaterial_qty.Location = New System.Drawing.Point(546, 28)
        Me.txt_mastermaterial_qty.Name = "txt_mastermaterial_qty"
        Me.txt_mastermaterial_qty.Size = New System.Drawing.Size(151, 35)
        Me.txt_mastermaterial_qty.TabIndex = 3
        '
        'txt_mastermaterial_pn
        '
        Me.txt_mastermaterial_pn.Location = New System.Drawing.Point(164, 28)
        Me.txt_mastermaterial_pn.Name = "txt_mastermaterial_pn"
        Me.txt_mastermaterial_pn.Size = New System.Drawing.Size(196, 35)
        Me.txt_mastermaterial_pn.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(388, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(152, 29)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Standard Qty"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(149, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Part Number"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'MasterMaterial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1924, 696)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "MasterMaterial"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Master Material"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgv_material, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txt_mastermaterial_qty As TextBox
    Friend WithEvents txt_mastermaterial_pn As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents txt_mastermaterial_search As TextBox
    Friend WithEvents txt_pn_name As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents btn_export_template As Button
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents btn_ex_template As Button
    Friend WithEvents cb_mastermaterial_family As ComboBox
    Friend WithEvents cb_mastermaterial_dept As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents dgv_material As Zuby.ADGV.AdvancedDataGridView
End Class
