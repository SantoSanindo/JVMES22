<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormReturnStock
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.checkQr = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.txtmanualLot = New System.Windows.Forms.TextBox()
        Me.txtmanualPN = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.dgv_forminputstock = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_forminputstock_qrcode = New System.Windows.Forms.TextBox()
        Me.txt_forminputstock_mts_no = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgv_forminputstock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "MTS No."
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.checkQr)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.TreeView1)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.dgv_forminputstock)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txt_forminputstock_qrcode)
        Me.GroupBox1.Controls.Add(Me.txt_forminputstock_mts_no)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(0, -12)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Size = New System.Drawing.Size(1821, 719)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(1058, 95)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(43, 35)
        Me.TextBox1.TabIndex = 13
        Me.TextBox1.Visible = False
        '
        'checkQr
        '
        Me.checkQr.AutoSize = True
        Me.checkQr.CausesValidation = False
        Me.checkQr.Checked = True
        Me.checkQr.CheckState = System.Windows.Forms.CheckState.Checked
        Me.checkQr.Location = New System.Drawing.Point(1032, 37)
        Me.checkQr.Name = "checkQr"
        Me.checkQr.Size = New System.Drawing.Size(133, 33)
        Me.checkQr.TabIndex = 12
        Me.checkQr.Text = "QR Code"
        Me.checkQr.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox2.Controls.Add(Me.Button4)
        Me.GroupBox2.Controls.Add(Me.txtmanualLot)
        Me.GroupBox2.Controls.Add(Me.txtmanualPN)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 76)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(997, 68)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.Green
        Me.Button4.ForeColor = System.Drawing.Color.White
        Me.Button4.Location = New System.Drawing.Point(791, 19)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(169, 42)
        Me.Button4.TabIndex = 17
        Me.Button4.Text = "Add Manual"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'txtmanualLot
        '
        Me.txtmanualLot.Location = New System.Drawing.Point(534, 23)
        Me.txtmanualLot.Name = "txtmanualLot"
        Me.txtmanualLot.Size = New System.Drawing.Size(210, 35)
        Me.txtmanualLot.TabIndex = 16
        '
        'txtmanualPN
        '
        Me.txtmanualPN.Location = New System.Drawing.Point(161, 23)
        Me.txtmanualPN.Name = "txtmanualPN"
        Me.txtmanualPN.Size = New System.Drawing.Size(218, 35)
        Me.txtmanualPN.TabIndex = 12
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(392, 29)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 29)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Lot No"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(149, 29)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Part Number"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Button1.Location = New System.Drawing.Point(1694, 45)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(113, 78)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Reset"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'TreeView1
        '
        Me.TreeView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TreeView1.Location = New System.Drawing.Point(6, 150)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(387, 566)
        Me.TreeView1.TabIndex = 9
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.Green
        Me.Button2.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Button2.Location = New System.Drawing.Point(1252, 45)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(143, 78)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "SAVE"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'dgv_forminputstock
        '
        Me.dgv_forminputstock.AllowUserToAddRows = False
        Me.dgv_forminputstock.AllowUserToDeleteRows = False
        Me.dgv_forminputstock.AllowUserToOrderColumns = True
        Me.dgv_forminputstock.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_forminputstock.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_forminputstock.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_forminputstock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_forminputstock.GridColor = System.Drawing.SystemColors.Highlight
        Me.dgv_forminputstock.Location = New System.Drawing.Point(399, 150)
        Me.dgv_forminputstock.MultiSelect = False
        Me.dgv_forminputstock.Name = "dgv_forminputstock"
        Me.dgv_forminputstock.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.dgv_forminputstock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_forminputstock.Size = New System.Drawing.Size(1415, 566)
        Me.dgv_forminputstock.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(398, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 29)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "QR Code"
        '
        'txt_forminputstock_qrcode
        '
        Me.txt_forminputstock_qrcode.Location = New System.Drawing.Point(540, 35)
        Me.txt_forminputstock_qrcode.Name = "txt_forminputstock_qrcode"
        Me.txt_forminputstock_qrcode.Size = New System.Drawing.Size(463, 35)
        Me.txt_forminputstock_qrcode.TabIndex = 2
        '
        'txt_forminputstock_mts_no
        '
        Me.txt_forminputstock_mts_no.Location = New System.Drawing.Point(167, 35)
        Me.txt_forminputstock_mts_no.Name = "txt_forminputstock_mts_no"
        Me.txt_forminputstock_mts_no.Size = New System.Drawing.Size(218, 35)
        Me.txt_forminputstock_mts_no.TabIndex = 1
        '
        'FormReturnStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1819, 707)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FormReturnStock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form Return Stock"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgv_forminputstock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_forminputstock_qrcode As TextBox
    Friend WithEvents txt_forminputstock_mts_no As TextBox
    Friend WithEvents dgv_forminputstock As DataGridView
    Friend WithEvents Button2 As Button
    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents Button1 As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtmanualLot As TextBox
    Friend WithEvents txtmanualPN As TextBox
    Friend WithEvents checkQr As CheckBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button4 As Button
End Class
