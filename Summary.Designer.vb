﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Summary
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSummarySubSubPO = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTraceability = New System.Windows.Forms.TextBox()
        Me.DGSummary = New Zuby.ADGV.AdvancedDataGridView()
        Me.DGTraceability1 = New Zuby.ADGV.AdvancedDataGridView()
        Me.DGTraceability2 = New Zuby.ADGV.AdvancedDataGridView()
        Me.Btn_Export_DGSummary = New System.Windows.Forms.Button()
        Me.btn_ExportTrace1 = New System.Windows.Forms.Button()
        Me.btn_ExportTrace2 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.DGSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGTraceability1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGTraceability2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DGSummary, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1493, 533)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.4517!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.5483!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 368.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txtSummarySubSubPO, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_Export_DGSummary, 2, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1487, 47)
        Me.TableLayoutPanel2.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(102, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Sub Sub PO"
        '
        'txtSummarySubSubPO
        '
        Me.txtSummarySubSubPO.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtSummarySubSubPO.Location = New System.Drawing.Point(254, 6)
        Me.txtSummarySubSubPO.Name = "txtSummarySubSubPO"
        Me.txtSummarySubSubPO.Size = New System.Drawing.Size(300, 35)
        Me.txtSummarySubSubPO.TabIndex = 1
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1507, 581)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 38)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1499, 539)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Production Summary"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TableLayoutPanel3)
        Me.TabPage2.Location = New System.Drawing.Point(4, 38)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1499, 539)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Traceability"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel4, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.DGTraceability1, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.DGTraceability2, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_ExportTrace2, 0, 3)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 4
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.76471!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.22689!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1493, 533)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 301.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.txtTraceability, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.btn_ExportTrace1, 2, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(1487, 51)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(62, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(290, 29)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Finish Goods Part Number"
        '
        'txtTraceability
        '
        Me.txtTraceability.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtTraceability.Location = New System.Drawing.Point(358, 7)
        Me.txtTraceability.Name = "txtTraceability"
        Me.txtTraceability.Size = New System.Drawing.Size(344, 36)
        Me.txtTraceability.TabIndex = 1
        '
        'DGSummary
        '
        Me.DGSummary.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGSummary.FilterAndSortEnabled = True
        Me.DGSummary.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGSummary.Location = New System.Drawing.Point(3, 56)
        Me.DGSummary.Name = "DGSummary"
        Me.DGSummary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DGSummary.Size = New System.Drawing.Size(1487, 474)
        Me.DGSummary.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGSummary.TabIndex = 3
        '
        'DGTraceability1
        '
        Me.DGTraceability1.AllowUserToAddRows = False
        Me.DGTraceability1.AllowUserToDeleteRows = False
        Me.DGTraceability1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGTraceability1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGTraceability1.FilterAndSortEnabled = True
        Me.DGTraceability1.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGTraceability1.Location = New System.Drawing.Point(3, 60)
        Me.DGTraceability1.Name = "DGTraceability1"
        Me.DGTraceability1.ReadOnly = True
        Me.DGTraceability1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DGTraceability1.Size = New System.Drawing.Size(1487, 200)
        Me.DGTraceability1.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGTraceability1.TabIndex = 3
        '
        'DGTraceability2
        '
        Me.DGTraceability2.AllowUserToAddRows = False
        Me.DGTraceability2.AllowUserToDeleteRows = False
        Me.DGTraceability2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGTraceability2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGTraceability2.FilterAndSortEnabled = True
        Me.DGTraceability2.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGTraceability2.Location = New System.Drawing.Point(3, 266)
        Me.DGTraceability2.Name = "DGTraceability2"
        Me.DGTraceability2.ReadOnly = True
        Me.DGTraceability2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DGTraceability2.Size = New System.Drawing.Size(1487, 219)
        Me.DGTraceability2.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGTraceability2.TabIndex = 4
        '
        'Btn_Export_DGSummary
        '
        Me.Btn_Export_DGSummary.BackColor = System.Drawing.Color.Blue
        Me.Btn_Export_DGSummary.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Btn_Export_DGSummary.Location = New System.Drawing.Point(1121, 3)
        Me.Btn_Export_DGSummary.Name = "Btn_Export_DGSummary"
        Me.Btn_Export_DGSummary.Size = New System.Drawing.Size(363, 41)
        Me.Btn_Export_DGSummary.TabIndex = 2
        Me.Btn_Export_DGSummary.Text = "Export to Excel"
        Me.Btn_Export_DGSummary.UseVisualStyleBackColor = False
        '
        'btn_ExportTrace1
        '
        Me.btn_ExportTrace1.BackColor = System.Drawing.Color.Blue
        Me.btn_ExportTrace1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btn_ExportTrace1.Location = New System.Drawing.Point(1188, 3)
        Me.btn_ExportTrace1.Name = "btn_ExportTrace1"
        Me.btn_ExportTrace1.Size = New System.Drawing.Size(296, 41)
        Me.btn_ExportTrace1.TabIndex = 3
        Me.btn_ExportTrace1.Text = "Export to Excel"
        Me.btn_ExportTrace1.UseVisualStyleBackColor = False
        '
        'btn_ExportTrace2
        '
        Me.btn_ExportTrace2.BackColor = System.Drawing.Color.Blue
        Me.btn_ExportTrace2.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btn_ExportTrace2.Location = New System.Drawing.Point(3, 491)
        Me.btn_ExportTrace2.Name = "btn_ExportTrace2"
        Me.btn_ExportTrace2.Size = New System.Drawing.Size(296, 39)
        Me.btn_ExportTrace2.TabIndex = 5
        Me.btn_ExportTrace2.Text = "Export to Excel"
        Me.btn_ExportTrace2.UseVisualStyleBackColor = False
        '
        'Summary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1507, 581)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Summary"
        Me.Text = "Production Summary"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.DGSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGTraceability1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGTraceability2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents txtSummarySubSubPO As TextBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents Label2 As Label
    Friend WithEvents txtTraceability As TextBox
    Friend WithEvents DGSummary As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents DGTraceability1 As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents DGTraceability2 As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents Btn_Export_DGSummary As Button
    Friend WithEvents btn_ExportTrace1 As Button
    Friend WithEvents btn_ExportTrace2 As Button
End Class
