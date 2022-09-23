<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MasterMaterial
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
        Me.txt_pn_name = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_mastermaterial_search = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.dgv_material = New System.Windows.Forms.DataGridView()
        Me.txt_mastermaterial_qty = New System.Windows.Forms.TextBox()
        Me.txt_mastermaterial_pn = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv_material, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txt_pn_name)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txt_mastermaterial_search)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.dgv_material)
        Me.GroupBox1.Controls.Add(Me.txt_mastermaterial_qty)
        Me.GroupBox1.Controls.Add(Me.txt_mastermaterial_pn)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(961, 672)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txt_pn_name
        '
        Me.txt_pn_name.Location = New System.Drawing.Point(164, 77)
        Me.txt_pn_name.Name = "txt_pn_name"
        Me.txt_pn_name.Size = New System.Drawing.Size(503, 35)
        Me.txt_pn_name.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 29)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "PN Name"
        '
        'txt_mastermaterial_search
        '
        Me.txt_mastermaterial_search.Location = New System.Drawing.Point(738, 628)
        Me.txt_mastermaterial_search.Name = "txt_mastermaterial_search"
        Me.txt_mastermaterial_search.Size = New System.Drawing.Size(217, 35)
        Me.txt_mastermaterial_search.TabIndex = 8
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.Red
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Location = New System.Drawing.Point(6, 624)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(242, 42)
        Me.Button3.TabIndex = 7
        Me.Button3.Text = "Delete Multiple Data"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Green
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(811, 28)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(144, 139)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Import"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(164, 178)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(340, 41)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Save"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'dgv_material
        '
        Me.dgv_material.AllowUserToAddRows = False
        Me.dgv_material.AllowUserToDeleteRows = False
        Me.dgv_material.AllowUserToOrderColumns = True
        Me.dgv_material.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_material.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_material.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_material.GridColor = System.Drawing.SystemColors.Highlight
        Me.dgv_material.Location = New System.Drawing.Point(6, 239)
        Me.dgv_material.MultiSelect = False
        Me.dgv_material.Name = "dgv_material"
        Me.dgv_material.ReadOnly = True
        Me.dgv_material.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.dgv_material.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_material.Size = New System.Drawing.Size(949, 379)
        Me.dgv_material.TabIndex = 4
        '
        'txt_mastermaterial_qty
        '
        Me.txt_mastermaterial_qty.Location = New System.Drawing.Point(164, 129)
        Me.txt_mastermaterial_qty.Name = "txt_mastermaterial_qty"
        Me.txt_mastermaterial_qty.Size = New System.Drawing.Size(503, 35)
        Me.txt_mastermaterial_qty.TabIndex = 3
        '
        'txt_mastermaterial_pn
        '
        Me.txt_mastermaterial_pn.Location = New System.Drawing.Point(164, 28)
        Me.txt_mastermaterial_pn.Name = "txt_mastermaterial_pn"
        Me.txt_mastermaterial_pn.Size = New System.Drawing.Size(503, 35)
        Me.txt_mastermaterial_pn.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 135)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(152, 29)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Standard Qty"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(149, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Part Number"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'MasterMaterial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(985, 696)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "MasterMaterial"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Master Material"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgv_material, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents dgv_material As DataGridView
    Friend WithEvents txt_mastermaterial_qty As TextBox
    Friend WithEvents txt_mastermaterial_pn As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents txt_mastermaterial_search As TextBox
    Friend WithEvents txt_pn_name As TextBox
    Friend WithEvents Label3 As Label
End Class
