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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.dgv_masterprocess = New System.Windows.Forms.DataGridView()
        Me.txt_masterprocess_desc = New System.Windows.Forms.TextBox()
        Me.txt_masterprocess_nama = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv_masterprocess, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.dgv_masterprocess)
        Me.GroupBox1.Controls.Add(Me.txt_masterprocess_desc)
        Me.GroupBox1.Controls.Add(Me.txt_masterprocess_nama)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1060, 602)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(718, 50)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(153, 64)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Save"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'dgv_masterprocess
        '
        Me.dgv_masterprocess.AllowUserToAddRows = False
        Me.dgv_masterprocess.AllowUserToDeleteRows = False
        Me.dgv_masterprocess.AllowUserToOrderColumns = True
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
        Me.dgv_masterprocess.Location = New System.Drawing.Point(6, 156)
        Me.dgv_masterprocess.MultiSelect = False
        Me.dgv_masterprocess.Name = "dgv_masterprocess"
        Me.dgv_masterprocess.ReadOnly = True
        Me.dgv_masterprocess.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.dgv_masterprocess.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_masterprocess.Size = New System.Drawing.Size(1048, 440)
        Me.dgv_masterprocess.TabIndex = 4
        '
        'txt_masterprocess_desc
        '
        Me.txt_masterprocess_desc.Location = New System.Drawing.Point(260, 89)
        Me.txt_masterprocess_desc.Name = "txt_masterprocess_desc"
        Me.txt_masterprocess_desc.Size = New System.Drawing.Size(359, 35)
        Me.txt_masterprocess_desc.TabIndex = 3
        '
        'txt_masterprocess_nama
        '
        Me.txt_masterprocess_nama.Location = New System.Drawing.Point(260, 37)
        Me.txt_masterprocess_nama.Name = "txt_masterprocess_nama"
        Me.txt_masterprocess_nama.Size = New System.Drawing.Size(359, 35)
        Me.txt_masterprocess_nama.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(181, 29)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Desc Of Proses"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(203, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Name Of Process"
        '
        'MasterProcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1084, 626)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "MasterProcess"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Master Process"
        Me.TopMost = True
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
End Class
