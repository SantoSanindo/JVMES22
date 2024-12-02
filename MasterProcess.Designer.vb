<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MasterProcess
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgv_masterprocess = New Zuby.ADGV.AdvancedDataGridView()
        Me.cb_masterprocess_dept = New System.Windows.Forms.ComboBox()
        Me.cb_masterprocess_family = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btn_ex_template = New System.Windows.Forms.Button()
        Me.btn_export_Master_Process = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.txt_masterprocess_search = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txt_masterprocess_desc = New System.Windows.Forms.TextBox()
        Me.txt_masterprocess_nama = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.process_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.process_desc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv_masterprocess, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.dgv_masterprocess)
        Me.GroupBox1.Controls.Add(Me.cb_masterprocess_dept)
        Me.GroupBox1.Controls.Add(Me.cb_masterprocess_family)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btn_ex_template)
        Me.GroupBox1.Controls.Add(Me.btn_export_Master_Process)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.txt_masterprocess_search)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.txt_masterprocess_desc)
        Me.GroupBox1.Controls.Add(Me.txt_masterprocess_nama)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(-1, -11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1855, 636)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'dgv_masterprocess
        '
        Me.dgv_masterprocess.AllowUserToAddRows = False
        Me.dgv_masterprocess.AllowUserToDeleteRows = False
        Me.dgv_masterprocess.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_masterprocess.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_masterprocess.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_masterprocess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_masterprocess.FilterAndSortEnabled = True
        Me.dgv_masterprocess.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.dgv_masterprocess.GridColor = System.Drawing.SystemColors.Highlight
        Me.dgv_masterprocess.Location = New System.Drawing.Point(11, 122)
        Me.dgv_masterprocess.Name = "dgv_masterprocess"
        Me.dgv_masterprocess.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgv_masterprocess.Size = New System.Drawing.Size(1838, 508)
        Me.dgv_masterprocess.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.dgv_masterprocess.TabIndex = 22
        '
        'cb_masterprocess_dept
        '
        Me.cb_masterprocess_dept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_masterprocess_dept.FormattingEnabled = True
        Me.cb_masterprocess_dept.Location = New System.Drawing.Point(732, 78)
        Me.cb_masterprocess_dept.Name = "cb_masterprocess_dept"
        Me.cb_masterprocess_dept.Size = New System.Drawing.Size(301, 37)
        Me.cb_masterprocess_dept.TabIndex = 21
        '
        'cb_masterprocess_family
        '
        Me.cb_masterprocess_family.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_masterprocess_family.FormattingEnabled = True
        Me.cb_masterprocess_family.Location = New System.Drawing.Point(732, 28)
        Me.cb_masterprocess_family.Name = "cb_masterprocess_family"
        Me.cb_masterprocess_family.Size = New System.Drawing.Size(301, 37)
        Me.cb_masterprocess_family.TabIndex = 20
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(588, 81)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(138, 29)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Department"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(588, 31)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 29)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Family"
        '
        'btn_ex_template
        '
        Me.btn_ex_template.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_ex_template.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_ex_template.ForeColor = System.Drawing.Color.White
        Me.btn_ex_template.Location = New System.Drawing.Point(1432, 24)
        Me.btn_ex_template.Name = "btn_ex_template"
        Me.btn_ex_template.Size = New System.Drawing.Size(210, 42)
        Me.btn_ex_template.TabIndex = 17
        Me.btn_ex_template.Text = "Export Template"
        Me.btn_ex_template.UseVisualStyleBackColor = False
        Me.btn_ex_template.Visible = False
        '
        'btn_export_Master_Process
        '
        Me.btn_export_Master_Process.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_export_Master_Process.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btn_export_Master_Process.ForeColor = System.Drawing.Color.White
        Me.btn_export_Master_Process.Location = New System.Drawing.Point(1656, 74)
        Me.btn_export_Master_Process.Name = "btn_export_Master_Process"
        Me.btn_export_Master_Process.Size = New System.Drawing.Size(193, 42)
        Me.btn_export_Master_Process.TabIndex = 16
        Me.btn_export_Master_Process.Text = "Export to Excel"
        Me.btn_export_Master_Process.UseVisualStyleBackColor = False
        Me.btn_export_Master_Process.Visible = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.Green
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(1696, 26)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(153, 38)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Import"
        Me.Button2.UseVisualStyleBackColor = False
        Me.Button2.Visible = False
        '
        'txt_masterprocess_search
        '
        Me.txt_masterprocess_search.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_masterprocess_search.Location = New System.Drawing.Point(1574, 595)
        Me.txt_masterprocess_search.Name = "txt_masterprocess_search"
        Me.txt_masterprocess_search.Size = New System.Drawing.Size(275, 35)
        Me.txt_masterprocess_search.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1314, 598)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(254, 29)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Search Name Process"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(1075, 26)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(153, 38)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Save"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'txt_masterprocess_desc
        '
        Me.txt_masterprocess_desc.Location = New System.Drawing.Point(215, 78)
        Me.txt_masterprocess_desc.Name = "txt_masterprocess_desc"
        Me.txt_masterprocess_desc.Size = New System.Drawing.Size(327, 35)
        Me.txt_masterprocess_desc.TabIndex = 3
        '
        'txt_masterprocess_nama
        '
        Me.txt_masterprocess_nama.Location = New System.Drawing.Point(215, 28)
        Me.txt_masterprocess_nama.Name = "txt_masterprocess_nama"
        Me.txt_masterprocess_nama.Size = New System.Drawing.Size(327, 35)
        Me.txt_masterprocess_nama.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(181, 29)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Desc Of Proses"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(203, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Name Of Process"
        '
        'process_name
        '
        Me.process_name.HeaderText = "Process Name"
        Me.process_name.Name = "process_name"
        Me.process_name.ReadOnly = True
        '
        'process_desc
        '
        Me.process_desc.HeaderText = "Process Desc"
        Me.process_desc.Name = "process_desc"
        Me.process_desc.ReadOnly = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'MasterProcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1855, 626)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "MasterProcess"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Master Process"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgv_masterprocess, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button1 As Button
    Friend WithEvents txt_masterprocess_desc As TextBox
    Friend WithEvents txt_masterprocess_nama As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_masterprocess_search As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents process_name As DataGridViewTextBoxColumn
    Friend WithEvents process_desc As DataGridViewTextBoxColumn
    Friend WithEvents Button2 As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents btn_ex_template As Button
    Friend WithEvents btn_export_Master_Process As Button
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents cb_masterprocess_dept As ComboBox
    Friend WithEvents cb_masterprocess_family As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents dgv_masterprocess As Zuby.ADGV.AdvancedDataGridView
End Class
