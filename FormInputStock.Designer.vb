<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormInputStock
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.checkQr = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtmanualQty = New System.Windows.Forms.TextBox()
        Me.txtmanualLot = New System.Windows.Forms.TextBox()
        Me.txtmanualBatch = New System.Windows.Forms.TextBox()
        Me.txtmanualInv = New System.Windows.Forms.TextBox()
        Me.txtmanualTraceability = New System.Windows.Forms.TextBox()
        Me.txtmanualPN = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.Button3 = New System.Windows.Forms.Button()
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
        Me.Label1.Location = New System.Drawing.Point(64, 45)
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
        Me.GroupBox1.Controls.Add(Me.checkQr)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.TreeView1)
        Me.GroupBox1.Controls.Add(Me.Button3)
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
        'checkQr
        '
        Me.checkQr.AutoSize = True
        Me.checkQr.CausesValidation = False
        Me.checkQr.Checked = True
        Me.checkQr.CheckState = System.Windows.Forms.CheckState.Checked
        Me.checkQr.Location = New System.Drawing.Point(728, 93)
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
        Me.GroupBox2.Controls.Add(Me.txtmanualQty)
        Me.GroupBox2.Controls.Add(Me.txtmanualLot)
        Me.GroupBox2.Controls.Add(Me.txtmanualBatch)
        Me.GroupBox2.Controls.Add(Me.txtmanualInv)
        Me.GroupBox2.Controls.Add(Me.txtmanualTraceability)
        Me.GroupBox2.Controls.Add(Me.txtmanualPN)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 126)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1808, 63)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        '
        'txtmanualQty
        '
        Me.txtmanualQty.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.txtmanualQty.Location = New System.Drawing.Point(1645, 19)
        Me.txtmanualQty.Name = "txtmanualQty"
        Me.txtmanualQty.Size = New System.Drawing.Size(156, 35)
        Me.txtmanualQty.TabIndex = 17
        '
        'txtmanualLot
        '
        Me.txtmanualLot.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.txtmanualLot.Location = New System.Drawing.Point(1394, 19)
        Me.txtmanualLot.Name = "txtmanualLot"
        Me.txtmanualLot.Size = New System.Drawing.Size(167, 35)
        Me.txtmanualLot.TabIndex = 16
        '
        'txtmanualBatch
        '
        Me.txtmanualBatch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.txtmanualBatch.Location = New System.Drawing.Point(1097, 19)
        Me.txtmanualBatch.Name = "txtmanualBatch"
        Me.txtmanualBatch.Size = New System.Drawing.Size(155, 35)
        Me.txtmanualBatch.TabIndex = 15
        '
        'txtmanualInv
        '
        Me.txtmanualInv.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.txtmanualInv.Location = New System.Drawing.Point(767, 19)
        Me.txtmanualInv.Name = "txtmanualInv"
        Me.txtmanualInv.Size = New System.Drawing.Size(159, 35)
        Me.txtmanualInv.TabIndex = 14
        '
        'txtmanualTraceability
        '
        Me.txtmanualTraceability.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.txtmanualTraceability.Location = New System.Drawing.Point(427, 19)
        Me.txtmanualTraceability.Name = "txtmanualTraceability"
        Me.txtmanualTraceability.Size = New System.Drawing.Size(157, 35)
        Me.txtmanualTraceability.TabIndex = 13
        '
        'txtmanualPN
        '
        Me.txtmanualPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.txtmanualPN.Location = New System.Drawing.Point(59, 19)
        Me.txtmanualPN.Name = "txtmanualPN"
        Me.txtmanualPN.Size = New System.Drawing.Size(218, 35)
        Me.txtmanualPN.TabIndex = 12
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(1590, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 29)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Qty"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(1283, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 29)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Lot No"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(960, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(111, 29)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Batch No"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(601, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(142, 29)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Inv Ctrl Date"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(283, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(138, 29)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Traceability"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 29)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "PN"
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
        Me.TreeView1.Location = New System.Drawing.Point(6, 195)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(421, 521)
        Me.TreeView1.TabIndex = 9
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(592, 42)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(110, 35)
        Me.Button3.TabIndex = 7
        Me.Button3.Text = "Check"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.Green
        Me.Button2.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Button2.Location = New System.Drawing.Point(1307, 45)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(202, 78)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Lock Permanent"
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
        Me.dgv_forminputstock.Location = New System.Drawing.Point(433, 195)
        Me.dgv_forminputstock.MultiSelect = False
        Me.dgv_forminputstock.Name = "dgv_forminputstock"
        Me.dgv_forminputstock.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.dgv_forminputstock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_forminputstock.Size = New System.Drawing.Size(1381, 521)
        Me.dgv_forminputstock.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(64, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 29)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "QR Code"
        '
        'txt_forminputstock_qrcode
        '
        Me.txt_forminputstock_qrcode.Location = New System.Drawing.Point(239, 91)
        Me.txt_forminputstock_qrcode.Name = "txt_forminputstock_qrcode"
        Me.txt_forminputstock_qrcode.Size = New System.Drawing.Size(463, 35)
        Me.txt_forminputstock_qrcode.TabIndex = 2
        '
        'txt_forminputstock_mts_no
        '
        Me.txt_forminputstock_mts_no.Location = New System.Drawing.Point(239, 42)
        Me.txt_forminputstock_mts_no.Name = "txt_forminputstock_mts_no"
        Me.txt_forminputstock_mts_no.Size = New System.Drawing.Size(332, 35)
        Me.txt_forminputstock_mts_no.TabIndex = 1
        '
        'FormInputStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1819, 707)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FormInputStock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form Input Stock"
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
    Friend WithEvents Button3 As Button
    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents Button1 As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtmanualQty As TextBox
    Friend WithEvents txtmanualLot As TextBox
    Friend WithEvents txtmanualBatch As TextBox
    Friend WithEvents txtmanualInv As TextBox
    Friend WithEvents txtmanualTraceability As TextBox
    Friend WithEvents txtmanualPN As TextBox
    Friend WithEvents checkQr As CheckBox
End Class
