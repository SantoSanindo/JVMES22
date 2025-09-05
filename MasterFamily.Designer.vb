<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MasterFamily
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
        Me.dgv_masterprocess = New Zuby.ADGV.AdvancedDataGridView()
        Me.cb_family_dept = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txt_family_nama = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.process_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.process_desc = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.GroupBox1.Controls.Add(Me.cb_family_dept)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.txt_family_nama)
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
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_masterprocess.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
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
        'cb_family_dept
        '
        Me.cb_family_dept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_family_dept.FormattingEnabled = True
        Me.cb_family_dept.Location = New System.Drawing.Point(655, 62)
        Me.cb_family_dept.Name = "cb_family_dept"
        Me.cb_family_dept.Size = New System.Drawing.Size(301, 37)
        Me.cb_family_dept.TabIndex = 21
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(511, 65)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(138, 29)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Department"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(1031, 60)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(153, 38)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Save"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'txt_family_nama
        '
        Me.txt_family_nama.Location = New System.Drawing.Point(103, 62)
        Me.txt_family_nama.Name = "txt_family_nama"
        Me.txt_family_nama.Size = New System.Drawing.Size(327, 35)
        Me.txt_family_nama.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Family"
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
        'MasterFamily
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1855, 626)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "MasterFamily"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Master Process"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgv_masterprocess, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button1 As Button
    Friend WithEvents txt_family_nama As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents process_name As DataGridViewTextBoxColumn
    Friend WithEvents process_desc As DataGridViewTextBoxColumn
    Friend WithEvents cb_family_dept As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents dgv_masterprocess As Zuby.ADGV.AdvancedDataGridView
End Class
