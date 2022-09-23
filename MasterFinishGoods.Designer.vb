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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txt_masterfinishgoods_search = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.dgv_masterfinishgoods_atas = New System.Windows.Forms.DataGridView()
        Me.txt_masterfinishgoods_qty = New System.Windows.Forms.TextBox()
        Me.txt_masterfinishgoods_pn = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv_masterfinishgoods_atas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txt_masterfinishgoods_search)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.dgv_masterfinishgoods_atas)
        Me.GroupBox1.Controls.Add(Me.txt_masterfinishgoods_qty)
        Me.GroupBox1.Controls.Add(Me.txt_masterfinishgoods_pn)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1092, 615)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'txt_masterfinishgoods_search
        '
        Me.txt_masterfinishgoods_search.Location = New System.Drawing.Point(893, 566)
        Me.txt_masterfinishgoods_search.Name = "txt_masterfinishgoods_search"
        Me.txt_masterfinishgoods_search.Size = New System.Drawing.Size(193, 35)
        Me.txt_masterfinishgoods_search.TabIndex = 17
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Red
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(6, 566)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(239, 43)
        Me.Button2.TabIndex = 16
        Me.Button2.Text = "Delete Multiple Data"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.Green
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Location = New System.Drawing.Point(987, 31)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(99, 87)
        Me.Button3.TabIndex = 15
        Me.Button3.Text = "Import"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(640, 28)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(104, 87)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Save"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'dgv_masterfinishgoods_atas
        '
        Me.dgv_masterfinishgoods_atas.AllowUserToAddRows = False
        Me.dgv_masterfinishgoods_atas.AllowUserToDeleteRows = False
        Me.dgv_masterfinishgoods_atas.AllowUserToOrderColumns = True
        Me.dgv_masterfinishgoods_atas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_masterfinishgoods_atas.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_masterfinishgoods_atas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_masterfinishgoods_atas.GridColor = System.Drawing.SystemColors.Highlight
        Me.dgv_masterfinishgoods_atas.Location = New System.Drawing.Point(6, 139)
        Me.dgv_masterfinishgoods_atas.MultiSelect = False
        Me.dgv_masterfinishgoods_atas.Name = "dgv_masterfinishgoods_atas"
        Me.dgv_masterfinishgoods_atas.ReadOnly = True
        Me.dgv_masterfinishgoods_atas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_masterfinishgoods_atas.Size = New System.Drawing.Size(1080, 421)
        Me.dgv_masterfinishgoods_atas.TabIndex = 4
        '
        'txt_masterfinishgoods_qty
        '
        Me.txt_masterfinishgoods_qty.Location = New System.Drawing.Point(200, 80)
        Me.txt_masterfinishgoods_qty.Name = "txt_masterfinishgoods_qty"
        Me.txt_masterfinishgoods_qty.Size = New System.Drawing.Size(425, 35)
        Me.txt_masterfinishgoods_qty.TabIndex = 3
        '
        'txt_masterfinishgoods_pn
        '
        Me.txt_masterfinishgoods_pn.Location = New System.Drawing.Point(200, 28)
        Me.txt_masterfinishgoods_pn.Name = "txt_masterfinishgoods_pn"
        Me.txt_masterfinishgoods_pn.Size = New System.Drawing.Size(425, 35)
        Me.txt_masterfinishgoods_pn.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(152, 29)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Standard Qty"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(188, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Part Number FG"
        '
        'MasterFinishGoods
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1116, 636)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "MasterFinishGoods"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Master Finish Goods"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgv_masterfinishgoods_atas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button1 As Button
    Friend WithEvents dgv_masterfinishgoods_atas As DataGridView
    Friend WithEvents txt_masterfinishgoods_qty As TextBox
    Friend WithEvents txt_masterfinishgoods_pn As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents txt_masterfinishgoods_search As TextBox
    Friend WithEvents Button2 As Button
End Class
