﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StockMinistore
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.DataGridView1 = New Zuby.ADGV.AdvancedDataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ReportDGAtas = New Zuby.ADGV.AdvancedDataGridView()
        Me.ReportDGBawah = New Zuby.ADGV.AdvancedDataGridView()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.btn_ExportTrace1 = New System.Windows.Forms.Button()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.ReportDGAtas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReportDGBawah, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(11, 541)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(115, 38)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Refresh"
        Me.Button1.UseVisualStyleBackColor = False
        Me.Button1.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.TabControl1)
        Me.GroupBox1.Controls.Add(Me.ProgressBar1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox1.Controls.Add(Me.btn_ExportTrace1)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(2, -14)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1298, 586)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(6, 81)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1286, 453)
        Me.TabControl1.TabIndex = 16
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DataGridView1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 38)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1278, 411)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Stock Card"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.FilterAndSortEnabled = True
        Me.DataGridView1.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DataGridView1.Location = New System.Drawing.Point(3, 3)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView1.Size = New System.Drawing.Size(1272, 405)
        Me.DataGridView1.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DataGridView1.TabIndex = 8
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 38)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1278, 411)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Report"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ReportDGAtas, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ReportDGBawah, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.55556!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.44444!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1272, 405)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'ReportDGAtas
        '
        Me.ReportDGAtas.AllowUserToAddRows = False
        Me.ReportDGAtas.AllowUserToDeleteRows = False
        Me.ReportDGAtas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ReportDGAtas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportDGAtas.FilterAndSortEnabled = True
        Me.ReportDGAtas.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.ReportDGAtas.Location = New System.Drawing.Point(3, 3)
        Me.ReportDGAtas.Name = "ReportDGAtas"
        Me.ReportDGAtas.ReadOnly = True
        Me.ReportDGAtas.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ReportDGAtas.Size = New System.Drawing.Size(1266, 138)
        Me.ReportDGAtas.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.ReportDGAtas.TabIndex = 0
        '
        'ReportDGBawah
        '
        Me.ReportDGBawah.AllowUserToAddRows = False
        Me.ReportDGBawah.AllowUserToDeleteRows = False
        Me.ReportDGBawah.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.ReportDGBawah.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ReportDGBawah.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportDGBawah.FilterAndSortEnabled = True
        Me.ReportDGBawah.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.ReportDGBawah.Location = New System.Drawing.Point(3, 147)
        Me.ReportDGBawah.Name = "ReportDGBawah"
        Me.ReportDGBawah.ReadOnly = True
        Me.ReportDGBawah.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ReportDGBawah.Size = New System.Drawing.Size(1266, 255)
        Me.ReportDGBawah.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.ReportDGBawah.TabIndex = 1
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(737, 551)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(253, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 15
        Me.ProgressBar1.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 31)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 29)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Date"
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Green
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(565, 26)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(115, 38)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "Filter"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(298, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(21, 29)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "-"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(335, 26)
        Me.DateTimePicker2.MinDate = New Date(2020, 1, 1, 0, 0, 0, 0)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(200, 35)
        Me.DateTimePicker2.TabIndex = 11
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(83, 26)
        Me.DateTimePicker1.MinDate = New Date(2020, 1, 1, 0, 0, 0, 0)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(200, 35)
        Me.DateTimePicker1.TabIndex = 10
        '
        'btn_ExportTrace1
        '
        Me.btn_ExportTrace1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_ExportTrace1.BackColor = System.Drawing.Color.Blue
        Me.btn_ExportTrace1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btn_ExportTrace1.Location = New System.Drawing.Point(996, 540)
        Me.btn_ExportTrace1.Name = "btn_ExportTrace1"
        Me.btn_ExportTrace1.Size = New System.Drawing.Size(296, 41)
        Me.btn_ExportTrace1.TabIndex = 9
        Me.btn_ExportTrace1.Text = "Export this table to Excel"
        Me.btn_ExportTrace1.UseVisualStyleBackColor = False
        '
        'BackgroundWorker1
        '
        '
        'StockMinistore
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1300, 572)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "StockMinistore"
        Me.Text = "Stock Ministore"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.ReportDGAtas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReportDGBawah, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents DataGridView1 As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents btn_ExportTrace1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ReportDGAtas As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents ReportDGBawah As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
End Class
