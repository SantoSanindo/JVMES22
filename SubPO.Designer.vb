<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SubPO
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
        Me.fg_pn = New System.Windows.Forms.TextBox()
        Me.qty_sub_po = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dgv_pra = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cb_lineproduction = New System.Windows.Forms.ComboBox()
        Me.txt_pra_po_no = New System.Windows.Forms.TextBox()
        Me.txt_pra_qty_po = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv_pra, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.fg_pn)
        Me.GroupBox1.Controls.Add(Me.qty_sub_po)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.dgv_pra)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.cb_lineproduction)
        Me.GroupBox1.Controls.Add(Me.txt_pra_po_no)
        Me.GroupBox1.Controls.Add(Me.txt_pra_qty_po)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1179, 647)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'fg_pn
        '
        Me.fg_pn.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fg_pn.Location = New System.Drawing.Point(233, 101)
        Me.fg_pn.Name = "fg_pn"
        Me.fg_pn.ReadOnly = True
        Me.fg_pn.Size = New System.Drawing.Size(447, 35)
        Me.fg_pn.TabIndex = 12
        '
        'qty_sub_po
        '
        Me.qty_sub_po.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.qty_sub_po.Location = New System.Drawing.Point(233, 292)
        Me.qty_sub_po.Name = "qty_sub_po"
        Me.qty_sub_po.Size = New System.Drawing.Size(447, 35)
        Me.qty_sub_po.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(30, 295)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(139, 29)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Qty Sub PO"
        '
        'dgv_pra
        '
        Me.dgv_pra.AllowUserToAddRows = False
        Me.dgv_pra.AllowUserToDeleteRows = False
        Me.dgv_pra.AllowUserToOrderColumns = True
        Me.dgv_pra.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_pra.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_pra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_pra.GridColor = System.Drawing.SystemColors.Highlight
        Me.dgv_pra.Location = New System.Drawing.Point(6, 344)
        Me.dgv_pra.MultiSelect = False
        Me.dgv_pra.Name = "dgv_pra"
        Me.dgv_pra.ReadOnly = True
        Me.dgv_pra.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_pra.Size = New System.Drawing.Size(1167, 297)
        Me.dgv_pra.TabIndex = 9
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(793, 135)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(174, 88)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Save"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'cb_lineproduction
        '
        Me.cb_lineproduction.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_lineproduction.FormattingEnabled = True
        Me.cb_lineproduction.Location = New System.Drawing.Point(233, 245)
        Me.cb_lineproduction.Name = "cb_lineproduction"
        Me.cb_lineproduction.Size = New System.Drawing.Size(447, 37)
        Me.cb_lineproduction.TabIndex = 7
        '
        'txt_pra_po_no
        '
        Me.txt_pra_po_no.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_pra_po_no.Location = New System.Drawing.Point(233, 42)
        Me.txt_pra_po_no.Name = "txt_pra_po_no"
        Me.txt_pra_po_no.ReadOnly = True
        Me.txt_pra_po_no.Size = New System.Drawing.Size(447, 35)
        Me.txt_pra_po_no.TabIndex = 6
        '
        'txt_pra_qty_po
        '
        Me.txt_pra_qty_po.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_pra_qty_po.Location = New System.Drawing.Point(233, 162)
        Me.txt_pra_qty_po.Name = "txt_pra_qty_po"
        Me.txt_pra_qty_po.ReadOnly = True
        Me.txt_pra_qty_po.Size = New System.Drawing.Size(447, 35)
        Me.txt_pra_qty_po.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(30, 248)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(181, 29)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Line Production"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(30, 165)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(134, 29)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Current Qty"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(30, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 29)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "PO NO"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(30, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(156, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Finish Goods"
        '
        'SubPO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1205, 671)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "SubPO"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sub PO"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgv_pra, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents dgv_pra As DataGridView
    Friend WithEvents Button1 As Button
    Friend WithEvents cb_lineproduction As ComboBox
    Friend WithEvents txt_pra_po_no As TextBox
    Friend WithEvents txt_pra_qty_po As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents qty_sub_po As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents fg_pn As TextBox
End Class
