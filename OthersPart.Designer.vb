<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OthersPart
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.pDefective1 = New System.Windows.Forms.Panel()
        Me.txtLabelOtherPart = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnPrintOthersPart = New System.Windows.Forms.Button()
        Me.btnOtherSave = New System.Windows.Forms.Button()
        Me.DataGridView4 = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pDefective1.SuspendLayout()
        CType(Me.DataGridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridView2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.pDefective1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnOtherSave, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridView4, 0, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(7)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.25641!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41.04348!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.869565!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.17391!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1137, 694)
        Me.TableLayoutPanel1.TabIndex = 21
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView2.Location = New System.Drawing.Point(7, 77)
        Me.DataGridView2.Margin = New System.Windows.Forms.Padding(7)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(1123, 269)
        Me.DataGridView2.TabIndex = 2
        '
        'pDefective1
        '
        Me.pDefective1.Controls.Add(Me.txtLabelOtherPart)
        Me.pDefective1.Controls.Add(Me.Label7)
        Me.pDefective1.Controls.Add(Me.btnPrintOthersPart)
        Me.pDefective1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pDefective1.Location = New System.Drawing.Point(7, 7)
        Me.pDefective1.Margin = New System.Windows.Forms.Padding(7)
        Me.pDefective1.Name = "pDefective1"
        Me.pDefective1.Size = New System.Drawing.Size(1123, 56)
        Me.pDefective1.TabIndex = 18
        '
        'txtLabelOtherPart
        '
        Me.txtLabelOtherPart.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtLabelOtherPart.Location = New System.Drawing.Point(208, 11)
        Me.txtLabelOtherPart.Margin = New System.Windows.Forms.Padding(7)
        Me.txtLabelOtherPart.Name = "txtLabelOtherPart"
        Me.txtLabelOtherPart.Size = New System.Drawing.Size(426, 35)
        Me.txtLabelOtherPart.TabIndex = 22
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(46, 14)
        Me.Label7.Margin = New System.Windows.Forms.Padding(7, 0, 7, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(148, 29)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Label Defect"
        '
        'btnPrintOthersPart
        '
        Me.btnPrintOthersPart.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnPrintOthersPart.BackColor = System.Drawing.Color.SkyBlue
        Me.btnPrintOthersPart.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPrintOthersPart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnPrintOthersPart.Location = New System.Drawing.Point(939, 0)
        Me.btnPrintOthersPart.Margin = New System.Windows.Forms.Padding(7)
        Me.btnPrintOthersPart.Name = "btnPrintOthersPart"
        Me.btnPrintOthersPart.Size = New System.Drawing.Size(184, 56)
        Me.btnPrintOthersPart.TabIndex = 20
        Me.btnPrintOthersPart.Text = "Print"
        Me.btnPrintOthersPart.UseVisualStyleBackColor = False
        '
        'btnOtherSave
        '
        Me.btnOtherSave.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnOtherSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnOtherSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnOtherSave.Location = New System.Drawing.Point(379, 360)
        Me.btnOtherSave.Margin = New System.Windows.Forms.Padding(7)
        Me.btnOtherSave.Name = "btnOtherSave"
        Me.btnOtherSave.Size = New System.Drawing.Size(378, 47)
        Me.btnOtherSave.TabIndex = 1
        Me.btnOtherSave.Text = "Save"
        Me.btnOtherSave.UseVisualStyleBackColor = False
        '
        'DataGridView4
        '
        Me.DataGridView4.AllowUserToAddRows = False
        Me.DataGridView4.AllowUserToDeleteRows = False
        Me.DataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView4.Location = New System.Drawing.Point(7, 421)
        Me.DataGridView4.Margin = New System.Windows.Forms.Padding(7)
        Me.DataGridView4.Name = "DataGridView4"
        Me.DataGridView4.ReadOnly = True
        Me.DataGridView4.Size = New System.Drawing.Size(1123, 266)
        Me.DataGridView4.TabIndex = 19
        '
        'OthersPart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(14.0!, 29.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1137, 694)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(7)
        Me.Name = "OthersPart"
        Me.Text = "Others Part"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pDefective1.ResumeLayout(False)
        Me.pDefective1.PerformLayout()
        CType(Me.DataGridView4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents pDefective1 As Panel
    Friend WithEvents txtLabelOtherPart As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents btnPrintOthersPart As Button
    Friend WithEvents btnOtherSave As Button
    Friend WithEvents DataGridView4 As DataGridView
End Class
