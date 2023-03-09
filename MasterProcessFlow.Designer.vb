<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MasterProcessFlow
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgvBawah = New System.Windows.Forms.DataGridView()
        Me.btn_ex_template = New System.Windows.Forms.Button()
        Me.btn_export_Finish_Goods = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.masterprocessflow_search = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cb_masterprocessflow = New System.Windows.Forms.ComboBox()
        Me.dgv_masterprocessflow = New System.Windows.Forms.DataGridView()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvBawah, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv_masterprocessflow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.dgvBawah)
        Me.GroupBox1.Controls.Add(Me.btn_ex_template)
        Me.GroupBox1.Controls.Add(Me.btn_export_Finish_Goods)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.masterprocessflow_search)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cb_masterprocessflow)
        Me.GroupBox1.Controls.Add(Me.dgv_masterprocessflow)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(1, -12)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Size = New System.Drawing.Size(1534, 714)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'dgvBawah
        '
        Me.dgvBawah.AllowUserToAddRows = False
        Me.dgvBawah.AllowUserToDeleteRows = False
        Me.dgvBawah.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvBawah.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvBawah.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvBawah.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvBawah.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBawah.GridColor = System.Drawing.SystemColors.Highlight
        Me.dgvBawah.Location = New System.Drawing.Point(3, 197)
        Me.dgvBawah.MultiSelect = False
        Me.dgvBawah.Name = "dgvBawah"
        Me.dgvBawah.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.dgvBawah.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvBawah.Size = New System.Drawing.Size(1528, 446)
        Me.dgvBawah.TabIndex = 20
        '
        'btn_ex_template
        '
        Me.btn_ex_template.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_ex_template.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_ex_template.ForeColor = System.Drawing.Color.White
        Me.btn_ex_template.Location = New System.Drawing.Point(943, 21)
        Me.btn_ex_template.Name = "btn_ex_template"
        Me.btn_ex_template.Size = New System.Drawing.Size(210, 42)
        Me.btn_ex_template.TabIndex = 19
        Me.btn_ex_template.Text = "Export Template"
        Me.btn_ex_template.UseVisualStyleBackColor = False
        '
        'btn_export_Finish_Goods
        '
        Me.btn_export_Finish_Goods.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_export_Finish_Goods.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btn_export_Finish_Goods.ForeColor = System.Drawing.Color.White
        Me.btn_export_Finish_Goods.Location = New System.Drawing.Point(1178, 21)
        Me.btn_export_Finish_Goods.Name = "btn_export_Finish_Goods"
        Me.btn_export_Finish_Goods.Size = New System.Drawing.Size(193, 42)
        Me.btn_export_Finish_Goods.TabIndex = 18
        Me.btn_export_Finish_Goods.Text = "Export to Excel"
        Me.btn_export_Finish_Goods.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(1199, 652)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 29)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Search"
        '
        'masterprocessflow_search
        '
        Me.masterprocessflow_search.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.masterprocessflow_search.Location = New System.Drawing.Point(1294, 649)
        Me.masterprocessflow_search.Name = "masterprocessflow_search"
        Me.masterprocessflow_search.Size = New System.Drawing.Size(240, 35)
        Me.masterprocessflow_search.TabIndex = 9
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.Green
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(1396, 21)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(135, 42)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Import"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(618, 21)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(135, 42)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Check"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(196, 29)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "PN Finish Goods"
        '
        'cb_masterprocessflow
        '
        Me.cb_masterprocessflow.FormattingEnabled = True
        Me.cb_masterprocessflow.Location = New System.Drawing.Point(241, 25)
        Me.cb_masterprocessflow.Name = "cb_masterprocessflow"
        Me.cb_masterprocessflow.Size = New System.Drawing.Size(330, 37)
        Me.cb_masterprocessflow.TabIndex = 5
        '
        'dgv_masterprocessflow
        '
        Me.dgv_masterprocessflow.AllowUserToAddRows = False
        Me.dgv_masterprocessflow.AllowUserToDeleteRows = False
        Me.dgv_masterprocessflow.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_masterprocessflow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgv_masterprocessflow.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_masterprocessflow.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_masterprocessflow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_masterprocessflow.GridColor = System.Drawing.SystemColors.Highlight
        Me.dgv_masterprocessflow.Location = New System.Drawing.Point(3, 72)
        Me.dgv_masterprocessflow.MultiSelect = False
        Me.dgv_masterprocessflow.Name = "dgv_masterprocessflow"
        Me.dgv_masterprocessflow.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.dgv_masterprocessflow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_masterprocessflow.Size = New System.Drawing.Size(1528, 119)
        Me.dgv_masterprocessflow.TabIndex = 4
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'MasterProcessFlow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1538, 701)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "MasterProcessFlow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Master Process Flow"
        Me.TopMost = True
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvBawah, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv_masterprocessflow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents dgv_masterprocessflow As DataGridView
    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents cb_masterprocessflow As ComboBox
    Friend WithEvents Button2 As Button
    Friend WithEvents masterprocessflow_search As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents btn_ex_template As Button
    Friend WithEvents btn_export_Finish_Goods As Button
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents dgvBawah As DataGridView
End Class
