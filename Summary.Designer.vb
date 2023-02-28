<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Me.Btn_Export_DGSummary = New System.Windows.Forms.Button()
        Me.DGSummaryV2 = New Zuby.ADGV.AdvancedDataGridView()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTraceability = New System.Windows.Forms.TextBox()
        Me.btn_ExportTrace1 = New System.Windows.Forms.Button()
        Me.DGTraceability1V2 = New Zuby.ADGV.AdvancedDataGridView()
        Me.DGTraceability2V2 = New Zuby.ADGV.AdvancedDataGridView()
        Me.btn_ExportTrace2 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.DGSummaryV2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.DGTraceability1V2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGTraceability2V2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DGSummaryV2, 0, 1)
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
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 374.0!))
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
        Me.Label1.Location = New System.Drawing.Point(100, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Sub Sub PO"
        '
        'txtSummarySubSubPO
        '
        Me.txtSummarySubSubPO.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtSummarySubSubPO.Location = New System.Drawing.Point(252, 6)
        Me.txtSummarySubSubPO.Name = "txtSummarySubSubPO"
        Me.txtSummarySubSubPO.Size = New System.Drawing.Size(300, 35)
        Me.txtSummarySubSubPO.TabIndex = 1
        '
        'Btn_Export_DGSummary
        '
        Me.Btn_Export_DGSummary.BackColor = System.Drawing.Color.Blue
        Me.Btn_Export_DGSummary.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Btn_Export_DGSummary.Location = New System.Drawing.Point(1115, 3)
        Me.Btn_Export_DGSummary.Name = "Btn_Export_DGSummary"
        Me.Btn_Export_DGSummary.Size = New System.Drawing.Size(363, 41)
        Me.Btn_Export_DGSummary.TabIndex = 2
        Me.Btn_Export_DGSummary.Text = "Export to Excel"
        Me.Btn_Export_DGSummary.UseVisualStyleBackColor = False
        '
        'DGSummaryV2
        '
        Me.DGSummaryV2.AllowUserToAddRows = False
        Me.DGSummaryV2.AllowUserToDeleteRows = False
        Me.DGSummaryV2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGSummaryV2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGSummaryV2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGSummaryV2.FilterAndSortEnabled = True
        Me.DGSummaryV2.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGSummaryV2.Location = New System.Drawing.Point(3, 56)
        Me.DGSummaryV2.Name = "DGSummaryV2"
        Me.DGSummaryV2.ReadOnly = True
        Me.DGSummaryV2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DGSummaryV2.Size = New System.Drawing.Size(1487, 474)
        Me.DGSummaryV2.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGSummaryV2.TabIndex = 3
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
        Me.TableLayoutPanel3.Controls.Add(Me.DGTraceability1V2, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.DGTraceability2V2, 0, 2)
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
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 307.0!))
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
        Me.Label2.Location = New System.Drawing.Point(61, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(290, 29)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Finish Goods Part Number"
        '
        'txtTraceability
        '
        Me.txtTraceability.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtTraceability.Location = New System.Drawing.Point(357, 7)
        Me.txtTraceability.Name = "txtTraceability"
        Me.txtTraceability.Size = New System.Drawing.Size(344, 36)
        Me.txtTraceability.TabIndex = 1
        '
        'btn_ExportTrace1
        '
        Me.btn_ExportTrace1.BackColor = System.Drawing.Color.Blue
        Me.btn_ExportTrace1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btn_ExportTrace1.Location = New System.Drawing.Point(1183, 3)
        Me.btn_ExportTrace1.Name = "btn_ExportTrace1"
        Me.btn_ExportTrace1.Size = New System.Drawing.Size(296, 41)
        Me.btn_ExportTrace1.TabIndex = 3
        Me.btn_ExportTrace1.Text = "Export to Excel"
        Me.btn_ExportTrace1.UseVisualStyleBackColor = False
        '
        'DGTraceability1V2
        '
        Me.DGTraceability1V2.AllowUserToAddRows = False
        Me.DGTraceability1V2.AllowUserToDeleteRows = False
        Me.DGTraceability1V2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DGTraceability1V2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGTraceability1V2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGTraceability1V2.FilterAndSortEnabled = True
        Me.DGTraceability1V2.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGTraceability1V2.Location = New System.Drawing.Point(3, 60)
        Me.DGTraceability1V2.Name = "DGTraceability1V2"
        Me.DGTraceability1V2.ReadOnly = True
        Me.DGTraceability1V2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DGTraceability1V2.Size = New System.Drawing.Size(1487, 200)
        Me.DGTraceability1V2.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGTraceability1V2.TabIndex = 6
        '
        'DGTraceability2V2
        '
        Me.DGTraceability2V2.AllowUserToAddRows = False
        Me.DGTraceability2V2.AllowUserToDeleteRows = False
        Me.DGTraceability2V2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGTraceability2V2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGTraceability2V2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGTraceability2V2.FilterAndSortEnabled = True
        Me.DGTraceability2V2.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGTraceability2V2.Location = New System.Drawing.Point(3, 266)
        Me.DGTraceability2V2.Name = "DGTraceability2V2"
        Me.DGTraceability2V2.ReadOnly = True
        Me.DGTraceability2V2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DGTraceability2V2.Size = New System.Drawing.Size(1487, 219)
        Me.DGTraceability2V2.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGTraceability2V2.TabIndex = 7
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
        CType(Me.DGSummaryV2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.DGTraceability1V2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGTraceability2V2, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Btn_Export_DGSummary As Button
    Friend WithEvents btn_ExportTrace1 As Button
    Friend WithEvents btn_ExportTrace2 As Button
    Friend WithEvents DGSummaryV2 As Zuby.ADGV.AdvancedDataGridView
    ' Friend WithEvents SUMMARY_FGTableAdapter As JOVANDataSetTableAdapters.SUMMARY_FGTableAdapter
    Friend WithEvents DGTraceability1V2 As Zuby.ADGV.AdvancedDataGridView
    ' Friend WithEvents SUMMARY_TRACEABILITYTableAdapter As JOVANDataSet1TableAdapters.SUMMARY_TRACEABILITYTableAdapter
    Friend WithEvents DGTraceability2V2 As Zuby.ADGV.AdvancedDataGridView
    ' Friend WithEvents SUMMARY_TRACEABILITY_COMPTableAdapter As JOVANDataSet2TableAdapters.SUMMARY_TRACEABILITY_COMPTableAdapter
    Friend WithEvents LINEDataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents COMPONENTDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents DESCDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents INVDataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents BATCHNODataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents LOTCOMPDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents LOTFGDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents QTYDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents REMARKDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents DATEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents SUBSUBPODataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents LINEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents FGDataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents LASERCODEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents INVDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents BATCHNODataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents LOTNODataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents INSPECTORDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PACKER1DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PACKER2DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PACKER3DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PACKER4DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS1DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS2DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS3DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS4DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS5DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS6DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS7DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS8DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS9DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS10DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS11DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS12DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS13DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS14DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS15DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS16DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS17DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS18DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS19DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS20DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS21DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS22DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS23DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS24DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS25DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS26DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS27DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS28DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS29DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PROCESS30DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents SUBSUBPODataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents FGDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents MATERIALDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents FRESHINDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents BALANCEINDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents OTHERSINDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents WIPINDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents ONHOLDINDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents SAINDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents TOTALINDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents RETURNOUTDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents DEFECTOUTDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents OTHERSOUTDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents WIPOUTDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents ONHOLDOUTDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents BALANCEOUTDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents FGOUTDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents TOTALOUTDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
End Class
