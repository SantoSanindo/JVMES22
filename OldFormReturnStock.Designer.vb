<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OldFormReturnStock
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
        Me.mts_return = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.dgv_forminputstock = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_forminputstock_qrcode = New System.Windows.Forms.TextBox()
        Me.txt_forminputstock_mts_no = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv_forminputstock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.mts_return)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.dgv_forminputstock)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txt_forminputstock_qrcode)
        Me.GroupBox1.Controls.Add(Me.txt_forminputstock_mts_no)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1377, 683)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'mts_return
        '
        Me.mts_return.Location = New System.Drawing.Point(239, 88)
        Me.mts_return.Name = "mts_return"
        Me.mts_return.ReadOnly = True
        Me.mts_return.Size = New System.Drawing.Size(463, 35)
        Me.mts_return.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(67, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(142, 29)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "MTS Return"
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Green
        Me.Button2.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Button2.Location = New System.Drawing.Point(858, 68)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(202, 75)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Save"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Button1.Location = New System.Drawing.Point(1205, 68)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(130, 75)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Exit"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'dgv_forminputstock
        '
        Me.dgv_forminputstock.AllowUserToAddRows = False
        Me.dgv_forminputstock.AllowUserToDeleteRows = False
        Me.dgv_forminputstock.AllowUserToOrderColumns = True
        Me.dgv_forminputstock.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_forminputstock.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_forminputstock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_forminputstock.GridColor = System.Drawing.SystemColors.Highlight
        Me.dgv_forminputstock.Location = New System.Drawing.Point(6, 213)
        Me.dgv_forminputstock.MultiSelect = False
        Me.dgv_forminputstock.Name = "dgv_forminputstock"
        Me.dgv_forminputstock.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.dgv_forminputstock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_forminputstock.Size = New System.Drawing.Size(1365, 464)
        Me.dgv_forminputstock.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(67, 143)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 29)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "QR Code"
        '
        'txt_forminputstock_qrcode
        '
        Me.txt_forminputstock_qrcode.Location = New System.Drawing.Point(239, 140)
        Me.txt_forminputstock_qrcode.Name = "txt_forminputstock_qrcode"
        Me.txt_forminputstock_qrcode.Size = New System.Drawing.Size(463, 35)
        Me.txt_forminputstock_qrcode.TabIndex = 2
        '
        'txt_forminputstock_mts_no
        '
        Me.txt_forminputstock_mts_no.Location = New System.Drawing.Point(239, 34)
        Me.txt_forminputstock_mts_no.Name = "txt_forminputstock_mts_no"
        Me.txt_forminputstock_mts_no.ReadOnly = True
        Me.txt_forminputstock_mts_no.Size = New System.Drawing.Size(463, 35)
        Me.txt_forminputstock_mts_no.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(67, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(128, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "MTS Issue"
        '
        'FormReturnStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1401, 707)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FormReturnStock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form Return Stock"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgv_forminputstock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents dgv_forminputstock As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_forminputstock_qrcode As TextBox
    Friend WithEvents txt_forminputstock_mts_no As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents mts_return As TextBox
    Friend WithEvents Label3 As Label
End Class
