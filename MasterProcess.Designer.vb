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
        Me.Button2 = New System.Windows.Forms.Button()
        Me.txt_masterprocess_search = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.dgv_masterprocess = New System.Windows.Forms.DataGridView()
        Me.txt_masterprocess_desc = New System.Windows.Forms.TextBox()
        Me.txt_masterprocess_nama = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.process_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.process_desc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv_masterprocess, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.txt_masterprocess_search)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.dgv_masterprocess)
        Me.GroupBox1.Controls.Add(Me.txt_masterprocess_desc)
        Me.GroupBox1.Controls.Add(Me.txt_masterprocess_nama)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(-1, -11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1316, 636)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.Green
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(1157, 25)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(153, 38)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Import"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'txt_masterprocess_search
        '
        Me.txt_masterprocess_search.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_masterprocess_search.Location = New System.Drawing.Point(1035, 567)
        Me.txt_masterprocess_search.Name = "txt_masterprocess_search"
        Me.txt_masterprocess_search.Size = New System.Drawing.Size(275, 35)
        Me.txt_masterprocess_search.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(931, 570)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 29)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Search"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(917, 26)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(153, 38)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Add"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'dgv_masterprocess
        '
        Me.dgv_masterprocess.AllowUserToAddRows = False
        Me.dgv_masterprocess.AllowUserToDeleteRows = False
        Me.dgv_masterprocess.AllowUserToOrderColumns = True
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
        Me.dgv_masterprocess.GridColor = System.Drawing.SystemColors.Highlight
        Me.dgv_masterprocess.Location = New System.Drawing.Point(6, 79)
        Me.dgv_masterprocess.MultiSelect = False
        Me.dgv_masterprocess.Name = "dgv_masterprocess"
        Me.dgv_masterprocess.ReadOnly = True
        Me.dgv_masterprocess.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.dgv_masterprocess.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgv_masterprocess.Size = New System.Drawing.Size(1304, 482)
        Me.dgv_masterprocess.TabIndex = 4
        '
        'txt_masterprocess_desc
        '
        Me.txt_masterprocess_desc.Location = New System.Drawing.Point(666, 28)
        Me.txt_masterprocess_desc.Name = "txt_masterprocess_desc"
        Me.txt_masterprocess_desc.Size = New System.Drawing.Size(219, 35)
        Me.txt_masterprocess_desc.TabIndex = 3
        '
        'txt_masterprocess_nama
        '
        Me.txt_masterprocess_nama.Location = New System.Drawing.Point(215, 28)
        Me.txt_masterprocess_nama.Name = "txt_masterprocess_nama"
        Me.txt_masterprocess_nama.Size = New System.Drawing.Size(208, 35)
        Me.txt_masterprocess_nama.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(466, 31)
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
        Me.ClientSize = New System.Drawing.Size(1314, 626)
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
    Friend WithEvents dgv_masterprocess As DataGridView
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
End Class
