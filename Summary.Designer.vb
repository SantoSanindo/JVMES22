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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSummarySubSubPO = New System.Windows.Forms.TextBox()
        Me.Btn_Export_DGSummary = New System.Windows.Forms.Button()
        Me.DGSummaryV2 = New Zuby.ADGV.AdvancedDataGridView()
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SUBSUBPODataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FGDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MATERIALDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FRESHINDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BALANCEINDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OTHERSINDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WIPINDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ONHOLDINDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SAINDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TOTALINDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RETURNOUTDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DEFECTOUTDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OTHERSOUTDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WIPOUTDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ONHOLDOUTDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BALANCEOUTDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FGOUTDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TOTALOUTDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SUMMARYFGBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.JOVANDataSet = New Jovan_New.JOVANDataSet()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTraceability = New System.Windows.Forms.TextBox()
        Me.btn_ExportTrace1 = New System.Windows.Forms.Button()
        Me.btn_ExportTrace2 = New System.Windows.Forms.Button()
        Me.DGTraceability1V2 = New Zuby.ADGV.AdvancedDataGridView()
        Me.IDDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DATEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SUBSUBPODataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LINEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FGDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LASERCODEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INVDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BATCHNODataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LOTNODataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INSPECTORDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PACKER1DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PACKER2DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PACKER3DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PACKER4DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS1DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS2DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS3DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS4DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS5DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS6DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS7DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS8DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS9DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS10DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS11DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS12DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS13DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS14DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS15DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS16DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS17DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS18DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS19DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS20DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS21DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS22DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS23DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS24DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS25DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS26DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS27DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS28DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS29DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROCESS30DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SUMMARYTRACEABILITYBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.JOVANDataSet1 = New Jovan_New.JOVANDataSet1()
        Me.DGTraceability2V2 = New Zuby.ADGV.AdvancedDataGridView()
        Me.IDDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LINEDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COMPONENTDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DESCDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INVDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BATCHNODataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LOTCOMPDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LOTFGDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTYDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.REMARKDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SUMMARYTRACEABILITYCOMPBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.JOVANDataSet2 = New Jovan_New.JOVANDataSet2()
        Me.SUMMARY_FGTableAdapter = New Jovan_New.JOVANDataSetTableAdapters.SUMMARY_FGTableAdapter()
        Me.JOVANDataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SUMMARY_TRACEABILITYTableAdapter = New Jovan_New.JOVANDataSet1TableAdapters.SUMMARY_TRACEABILITYTableAdapter()
        Me.SUMMARY_TRACEABILITY_COMPTableAdapter = New Jovan_New.JOVANDataSet2TableAdapters.SUMMARY_TRACEABILITY_COMPTableAdapter()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.DGSummaryV2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SUMMARYFGBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.JOVANDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.DGTraceability1V2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SUMMARYTRACEABILITYBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.JOVANDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGTraceability2V2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SUMMARYTRACEABILITYCOMPBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.JOVANDataSet2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.JOVANDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 370.0!))
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
        Me.Label1.Location = New System.Drawing.Point(101, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Sub Sub PO"
        '
        'txtSummarySubSubPO
        '
        Me.txtSummarySubSubPO.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtSummarySubSubPO.Location = New System.Drawing.Point(253, 6)
        Me.txtSummarySubSubPO.Name = "txtSummarySubSubPO"
        Me.txtSummarySubSubPO.Size = New System.Drawing.Size(300, 35)
        Me.txtSummarySubSubPO.TabIndex = 1
        '
        'Btn_Export_DGSummary
        '
        Me.Btn_Export_DGSummary.BackColor = System.Drawing.Color.Blue
        Me.Btn_Export_DGSummary.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Btn_Export_DGSummary.Location = New System.Drawing.Point(1119, 3)
        Me.Btn_Export_DGSummary.Name = "Btn_Export_DGSummary"
        Me.Btn_Export_DGSummary.Size = New System.Drawing.Size(363, 41)
        Me.Btn_Export_DGSummary.TabIndex = 2
        Me.Btn_Export_DGSummary.Text = "Export to Excel"
        Me.Btn_Export_DGSummary.UseVisualStyleBackColor = False
        '
        'DGSummaryV2
        '
        Me.DGSummaryV2.AutoGenerateColumns = False
        Me.DGSummaryV2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGSummaryV2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.DGSummaryV2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGSummaryV2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn, Me.SUBSUBPODataGridViewTextBoxColumn, Me.FGDataGridViewTextBoxColumn, Me.MATERIALDataGridViewTextBoxColumn, Me.FRESHINDataGridViewTextBoxColumn, Me.BALANCEINDataGridViewTextBoxColumn, Me.OTHERSINDataGridViewTextBoxColumn, Me.WIPINDataGridViewTextBoxColumn, Me.ONHOLDINDataGridViewTextBoxColumn, Me.SAINDataGridViewTextBoxColumn, Me.TOTALINDataGridViewTextBoxColumn, Me.RETURNOUTDataGridViewTextBoxColumn, Me.DEFECTOUTDataGridViewTextBoxColumn, Me.OTHERSOUTDataGridViewTextBoxColumn, Me.WIPOUTDataGridViewTextBoxColumn, Me.ONHOLDOUTDataGridViewTextBoxColumn, Me.BALANCEOUTDataGridViewTextBoxColumn, Me.FGOUTDataGridViewTextBoxColumn, Me.TOTALOUTDataGridViewTextBoxColumn})
        Me.DGSummaryV2.DataSource = Me.SUMMARYFGBindingSource
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGSummaryV2.DefaultCellStyle = DataGridViewCellStyle8
        Me.DGSummaryV2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGSummaryV2.FilterAndSortEnabled = True
        Me.DGSummaryV2.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGSummaryV2.Location = New System.Drawing.Point(3, 56)
        Me.DGSummaryV2.Name = "DGSummaryV2"
        Me.DGSummaryV2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DGSummaryV2.Size = New System.Drawing.Size(1487, 474)
        Me.DGSummaryV2.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGSummaryV2.TabIndex = 3
        '
        'IDDataGridViewTextBoxColumn
        '
        Me.IDDataGridViewTextBoxColumn.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.IDDataGridViewTextBoxColumn.Name = "IDDataGridViewTextBoxColumn"
        Me.IDDataGridViewTextBoxColumn.ReadOnly = True
        Me.IDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'SUBSUBPODataGridViewTextBoxColumn
        '
        Me.SUBSUBPODataGridViewTextBoxColumn.DataPropertyName = "SUB_SUB_PO"
        Me.SUBSUBPODataGridViewTextBoxColumn.HeaderText = "SUB_SUB_PO"
        Me.SUBSUBPODataGridViewTextBoxColumn.MinimumWidth = 22
        Me.SUBSUBPODataGridViewTextBoxColumn.Name = "SUBSUBPODataGridViewTextBoxColumn"
        Me.SUBSUBPODataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'FGDataGridViewTextBoxColumn
        '
        Me.FGDataGridViewTextBoxColumn.DataPropertyName = "FG"
        Me.FGDataGridViewTextBoxColumn.HeaderText = "FG"
        Me.FGDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.FGDataGridViewTextBoxColumn.Name = "FGDataGridViewTextBoxColumn"
        Me.FGDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'MATERIALDataGridViewTextBoxColumn
        '
        Me.MATERIALDataGridViewTextBoxColumn.DataPropertyName = "MATERIAL"
        Me.MATERIALDataGridViewTextBoxColumn.HeaderText = "MATERIAL"
        Me.MATERIALDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.MATERIALDataGridViewTextBoxColumn.Name = "MATERIALDataGridViewTextBoxColumn"
        Me.MATERIALDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'FRESHINDataGridViewTextBoxColumn
        '
        Me.FRESHINDataGridViewTextBoxColumn.DataPropertyName = "FRESH_IN"
        Me.FRESHINDataGridViewTextBoxColumn.HeaderText = "FRESH_IN"
        Me.FRESHINDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.FRESHINDataGridViewTextBoxColumn.Name = "FRESHINDataGridViewTextBoxColumn"
        Me.FRESHINDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'BALANCEINDataGridViewTextBoxColumn
        '
        Me.BALANCEINDataGridViewTextBoxColumn.DataPropertyName = "BALANCE_IN"
        Me.BALANCEINDataGridViewTextBoxColumn.HeaderText = "BALANCE_IN"
        Me.BALANCEINDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.BALANCEINDataGridViewTextBoxColumn.Name = "BALANCEINDataGridViewTextBoxColumn"
        Me.BALANCEINDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'OTHERSINDataGridViewTextBoxColumn
        '
        Me.OTHERSINDataGridViewTextBoxColumn.DataPropertyName = "OTHERS_IN"
        Me.OTHERSINDataGridViewTextBoxColumn.HeaderText = "OTHERS_IN"
        Me.OTHERSINDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.OTHERSINDataGridViewTextBoxColumn.Name = "OTHERSINDataGridViewTextBoxColumn"
        Me.OTHERSINDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'WIPINDataGridViewTextBoxColumn
        '
        Me.WIPINDataGridViewTextBoxColumn.DataPropertyName = "WIP_IN"
        Me.WIPINDataGridViewTextBoxColumn.HeaderText = "WIP_IN"
        Me.WIPINDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.WIPINDataGridViewTextBoxColumn.Name = "WIPINDataGridViewTextBoxColumn"
        Me.WIPINDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'ONHOLDINDataGridViewTextBoxColumn
        '
        Me.ONHOLDINDataGridViewTextBoxColumn.DataPropertyName = "ONHOLD_IN"
        Me.ONHOLDINDataGridViewTextBoxColumn.HeaderText = "ONHOLD_IN"
        Me.ONHOLDINDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.ONHOLDINDataGridViewTextBoxColumn.Name = "ONHOLDINDataGridViewTextBoxColumn"
        Me.ONHOLDINDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'SAINDataGridViewTextBoxColumn
        '
        Me.SAINDataGridViewTextBoxColumn.DataPropertyName = "SA_IN"
        Me.SAINDataGridViewTextBoxColumn.HeaderText = "SA_IN"
        Me.SAINDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.SAINDataGridViewTextBoxColumn.Name = "SAINDataGridViewTextBoxColumn"
        Me.SAINDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'TOTALINDataGridViewTextBoxColumn
        '
        Me.TOTALINDataGridViewTextBoxColumn.DataPropertyName = "TOTAL_IN"
        Me.TOTALINDataGridViewTextBoxColumn.HeaderText = "TOTAL_IN"
        Me.TOTALINDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.TOTALINDataGridViewTextBoxColumn.Name = "TOTALINDataGridViewTextBoxColumn"
        Me.TOTALINDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'RETURNOUTDataGridViewTextBoxColumn
        '
        Me.RETURNOUTDataGridViewTextBoxColumn.DataPropertyName = "RETURN_OUT"
        Me.RETURNOUTDataGridViewTextBoxColumn.HeaderText = "RETURN_OUT"
        Me.RETURNOUTDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.RETURNOUTDataGridViewTextBoxColumn.Name = "RETURNOUTDataGridViewTextBoxColumn"
        Me.RETURNOUTDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'DEFECTOUTDataGridViewTextBoxColumn
        '
        Me.DEFECTOUTDataGridViewTextBoxColumn.DataPropertyName = "DEFECT_OUT"
        Me.DEFECTOUTDataGridViewTextBoxColumn.HeaderText = "DEFECT_OUT"
        Me.DEFECTOUTDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.DEFECTOUTDataGridViewTextBoxColumn.Name = "DEFECTOUTDataGridViewTextBoxColumn"
        Me.DEFECTOUTDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'OTHERSOUTDataGridViewTextBoxColumn
        '
        Me.OTHERSOUTDataGridViewTextBoxColumn.DataPropertyName = "OTHERS_OUT"
        Me.OTHERSOUTDataGridViewTextBoxColumn.HeaderText = "OTHERS_OUT"
        Me.OTHERSOUTDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.OTHERSOUTDataGridViewTextBoxColumn.Name = "OTHERSOUTDataGridViewTextBoxColumn"
        Me.OTHERSOUTDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'WIPOUTDataGridViewTextBoxColumn
        '
        Me.WIPOUTDataGridViewTextBoxColumn.DataPropertyName = "WIP_OUT"
        Me.WIPOUTDataGridViewTextBoxColumn.HeaderText = "WIP_OUT"
        Me.WIPOUTDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.WIPOUTDataGridViewTextBoxColumn.Name = "WIPOUTDataGridViewTextBoxColumn"
        Me.WIPOUTDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'ONHOLDOUTDataGridViewTextBoxColumn
        '
        Me.ONHOLDOUTDataGridViewTextBoxColumn.DataPropertyName = "ONHOLD_OUT"
        Me.ONHOLDOUTDataGridViewTextBoxColumn.HeaderText = "ONHOLD_OUT"
        Me.ONHOLDOUTDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.ONHOLDOUTDataGridViewTextBoxColumn.Name = "ONHOLDOUTDataGridViewTextBoxColumn"
        Me.ONHOLDOUTDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'BALANCEOUTDataGridViewTextBoxColumn
        '
        Me.BALANCEOUTDataGridViewTextBoxColumn.DataPropertyName = "BALANCE_OUT"
        Me.BALANCEOUTDataGridViewTextBoxColumn.HeaderText = "BALANCE_OUT"
        Me.BALANCEOUTDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.BALANCEOUTDataGridViewTextBoxColumn.Name = "BALANCEOUTDataGridViewTextBoxColumn"
        Me.BALANCEOUTDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'FGOUTDataGridViewTextBoxColumn
        '
        Me.FGOUTDataGridViewTextBoxColumn.DataPropertyName = "FG_OUT"
        Me.FGOUTDataGridViewTextBoxColumn.HeaderText = "FG_OUT"
        Me.FGOUTDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.FGOUTDataGridViewTextBoxColumn.Name = "FGOUTDataGridViewTextBoxColumn"
        Me.FGOUTDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'TOTALOUTDataGridViewTextBoxColumn
        '
        Me.TOTALOUTDataGridViewTextBoxColumn.DataPropertyName = "TOTAL_OUT"
        Me.TOTALOUTDataGridViewTextBoxColumn.HeaderText = "TOTAL_OUT"
        Me.TOTALOUTDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.TOTALOUTDataGridViewTextBoxColumn.Name = "TOTALOUTDataGridViewTextBoxColumn"
        Me.TOTALOUTDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'SUMMARYFGBindingSource
        '
        Me.SUMMARYFGBindingSource.DataMember = "SUMMARY_FG"
        Me.SUMMARYFGBindingSource.DataSource = Me.JOVANDataSet
        '
        'JOVANDataSet
        '
        Me.JOVANDataSet.DataSetName = "JOVANDataSet"
        Me.JOVANDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
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
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 303.0!))
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
        'btn_ExportTrace1
        '
        Me.btn_ExportTrace1.BackColor = System.Drawing.Color.Blue
        Me.btn_ExportTrace1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btn_ExportTrace1.Location = New System.Drawing.Point(1186, 3)
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
        'DGTraceability1V2
        '
        Me.DGTraceability1V2.AutoGenerateColumns = False
        Me.DGTraceability1V2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGTraceability1V2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.DGTraceability1V2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGTraceability1V2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn1, Me.DATEDataGridViewTextBoxColumn, Me.SUBSUBPODataGridViewTextBoxColumn1, Me.LINEDataGridViewTextBoxColumn, Me.FGDataGridViewTextBoxColumn1, Me.LASERCODEDataGridViewTextBoxColumn, Me.INVDataGridViewTextBoxColumn, Me.BATCHNODataGridViewTextBoxColumn, Me.LOTNODataGridViewTextBoxColumn, Me.INSPECTORDataGridViewTextBoxColumn, Me.PACKER1DataGridViewTextBoxColumn, Me.PACKER2DataGridViewTextBoxColumn, Me.PACKER3DataGridViewTextBoxColumn, Me.PACKER4DataGridViewTextBoxColumn, Me.PROCESS1DataGridViewTextBoxColumn, Me.PROCESS2DataGridViewTextBoxColumn, Me.PROCESS3DataGridViewTextBoxColumn, Me.PROCESS4DataGridViewTextBoxColumn, Me.PROCESS5DataGridViewTextBoxColumn, Me.PROCESS6DataGridViewTextBoxColumn, Me.PROCESS7DataGridViewTextBoxColumn, Me.PROCESS8DataGridViewTextBoxColumn, Me.PROCESS9DataGridViewTextBoxColumn, Me.PROCESS10DataGridViewTextBoxColumn, Me.PROCESS11DataGridViewTextBoxColumn, Me.PROCESS12DataGridViewTextBoxColumn, Me.PROCESS13DataGridViewTextBoxColumn, Me.PROCESS14DataGridViewTextBoxColumn, Me.PROCESS15DataGridViewTextBoxColumn, Me.PROCESS16DataGridViewTextBoxColumn, Me.PROCESS17DataGridViewTextBoxColumn, Me.PROCESS18DataGridViewTextBoxColumn, Me.PROCESS19DataGridViewTextBoxColumn, Me.PROCESS20DataGridViewTextBoxColumn, Me.PROCESS21DataGridViewTextBoxColumn, Me.PROCESS22DataGridViewTextBoxColumn, Me.PROCESS23DataGridViewTextBoxColumn, Me.PROCESS24DataGridViewTextBoxColumn, Me.PROCESS25DataGridViewTextBoxColumn, Me.PROCESS26DataGridViewTextBoxColumn, Me.PROCESS27DataGridViewTextBoxColumn, Me.PROCESS28DataGridViewTextBoxColumn, Me.PROCESS29DataGridViewTextBoxColumn, Me.PROCESS30DataGridViewTextBoxColumn})
        Me.DGTraceability1V2.DataSource = Me.SUMMARYTRACEABILITYBindingSource
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGTraceability1V2.DefaultCellStyle = DataGridViewCellStyle10
        Me.DGTraceability1V2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGTraceability1V2.FilterAndSortEnabled = True
        Me.DGTraceability1V2.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGTraceability1V2.Location = New System.Drawing.Point(3, 60)
        Me.DGTraceability1V2.Name = "DGTraceability1V2"
        Me.DGTraceability1V2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DGTraceability1V2.Size = New System.Drawing.Size(1487, 200)
        Me.DGTraceability1V2.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGTraceability1V2.TabIndex = 6
        '
        'IDDataGridViewTextBoxColumn1
        '
        Me.IDDataGridViewTextBoxColumn1.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn1.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn1.MinimumWidth = 22
        Me.IDDataGridViewTextBoxColumn1.Name = "IDDataGridViewTextBoxColumn1"
        Me.IDDataGridViewTextBoxColumn1.ReadOnly = True
        Me.IDDataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.IDDataGridViewTextBoxColumn1.Width = 51
        '
        'DATEDataGridViewTextBoxColumn
        '
        Me.DATEDataGridViewTextBoxColumn.DataPropertyName = "DATE"
        Me.DATEDataGridViewTextBoxColumn.HeaderText = "DATE"
        Me.DATEDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.DATEDataGridViewTextBoxColumn.Name = "DATEDataGridViewTextBoxColumn"
        Me.DATEDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.DATEDataGridViewTextBoxColumn.Width = 75
        '
        'SUBSUBPODataGridViewTextBoxColumn1
        '
        Me.SUBSUBPODataGridViewTextBoxColumn1.DataPropertyName = "SUB_SUB_PO"
        Me.SUBSUBPODataGridViewTextBoxColumn1.HeaderText = "SUB_SUB_PO"
        Me.SUBSUBPODataGridViewTextBoxColumn1.MinimumWidth = 22
        Me.SUBSUBPODataGridViewTextBoxColumn1.Name = "SUBSUBPODataGridViewTextBoxColumn1"
        Me.SUBSUBPODataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.SUBSUBPODataGridViewTextBoxColumn1.Width = 131
        '
        'LINEDataGridViewTextBoxColumn
        '
        Me.LINEDataGridViewTextBoxColumn.DataPropertyName = "LINE"
        Me.LINEDataGridViewTextBoxColumn.HeaderText = "LINE"
        Me.LINEDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.LINEDataGridViewTextBoxColumn.Name = "LINEDataGridViewTextBoxColumn"
        Me.LINEDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.LINEDataGridViewTextBoxColumn.Width = 68
        '
        'FGDataGridViewTextBoxColumn1
        '
        Me.FGDataGridViewTextBoxColumn1.DataPropertyName = "FG"
        Me.FGDataGridViewTextBoxColumn1.HeaderText = "FG"
        Me.FGDataGridViewTextBoxColumn1.MinimumWidth = 22
        Me.FGDataGridViewTextBoxColumn1.Name = "FGDataGridViewTextBoxColumn1"
        Me.FGDataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.FGDataGridViewTextBoxColumn1.Width = 53
        '
        'LASERCODEDataGridViewTextBoxColumn
        '
        Me.LASERCODEDataGridViewTextBoxColumn.DataPropertyName = "LASER_CODE"
        Me.LASERCODEDataGridViewTextBoxColumn.HeaderText = "LASER_CODE"
        Me.LASERCODEDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.LASERCODEDataGridViewTextBoxColumn.Name = "LASERCODEDataGridViewTextBoxColumn"
        Me.LASERCODEDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.LASERCODEDataGridViewTextBoxColumn.Width = 132
        '
        'INVDataGridViewTextBoxColumn
        '
        Me.INVDataGridViewTextBoxColumn.DataPropertyName = "INV"
        Me.INVDataGridViewTextBoxColumn.HeaderText = "INV"
        Me.INVDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.INVDataGridViewTextBoxColumn.Name = "INVDataGridViewTextBoxColumn"
        Me.INVDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.INVDataGridViewTextBoxColumn.Width = 61
        '
        'BATCHNODataGridViewTextBoxColumn
        '
        Me.BATCHNODataGridViewTextBoxColumn.DataPropertyName = "BATCH_NO"
        Me.BATCHNODataGridViewTextBoxColumn.HeaderText = "BATCH_NO"
        Me.BATCHNODataGridViewTextBoxColumn.MinimumWidth = 22
        Me.BATCHNODataGridViewTextBoxColumn.Name = "BATCHNODataGridViewTextBoxColumn"
        Me.BATCHNODataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.BATCHNODataGridViewTextBoxColumn.Width = 117
        '
        'LOTNODataGridViewTextBoxColumn
        '
        Me.LOTNODataGridViewTextBoxColumn.DataPropertyName = "LOT_NO"
        Me.LOTNODataGridViewTextBoxColumn.HeaderText = "LOT_NO"
        Me.LOTNODataGridViewTextBoxColumn.MinimumWidth = 22
        Me.LOTNODataGridViewTextBoxColumn.Name = "LOTNODataGridViewTextBoxColumn"
        Me.LOTNODataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.LOTNODataGridViewTextBoxColumn.Width = 96
        '
        'INSPECTORDataGridViewTextBoxColumn
        '
        Me.INSPECTORDataGridViewTextBoxColumn.DataPropertyName = "INSPECTOR"
        Me.INSPECTORDataGridViewTextBoxColumn.HeaderText = "INSPECTOR"
        Me.INSPECTORDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.INSPECTORDataGridViewTextBoxColumn.Name = "INSPECTORDataGridViewTextBoxColumn"
        Me.INSPECTORDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.INSPECTORDataGridViewTextBoxColumn.Width = 120
        '
        'PACKER1DataGridViewTextBoxColumn
        '
        Me.PACKER1DataGridViewTextBoxColumn.DataPropertyName = "PACKER1"
        Me.PACKER1DataGridViewTextBoxColumn.HeaderText = "PACKER1"
        Me.PACKER1DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PACKER1DataGridViewTextBoxColumn.Name = "PACKER1DataGridViewTextBoxColumn"
        Me.PACKER1DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PACKER1DataGridViewTextBoxColumn.Width = 101
        '
        'PACKER2DataGridViewTextBoxColumn
        '
        Me.PACKER2DataGridViewTextBoxColumn.DataPropertyName = "PACKER2"
        Me.PACKER2DataGridViewTextBoxColumn.HeaderText = "PACKER2"
        Me.PACKER2DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PACKER2DataGridViewTextBoxColumn.Name = "PACKER2DataGridViewTextBoxColumn"
        Me.PACKER2DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PACKER2DataGridViewTextBoxColumn.Width = 101
        '
        'PACKER3DataGridViewTextBoxColumn
        '
        Me.PACKER3DataGridViewTextBoxColumn.DataPropertyName = "PACKER3"
        Me.PACKER3DataGridViewTextBoxColumn.HeaderText = "PACKER3"
        Me.PACKER3DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PACKER3DataGridViewTextBoxColumn.Name = "PACKER3DataGridViewTextBoxColumn"
        Me.PACKER3DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PACKER3DataGridViewTextBoxColumn.Width = 101
        '
        'PACKER4DataGridViewTextBoxColumn
        '
        Me.PACKER4DataGridViewTextBoxColumn.DataPropertyName = "PACKER4"
        Me.PACKER4DataGridViewTextBoxColumn.HeaderText = "PACKER4"
        Me.PACKER4DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PACKER4DataGridViewTextBoxColumn.Name = "PACKER4DataGridViewTextBoxColumn"
        Me.PACKER4DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PACKER4DataGridViewTextBoxColumn.Width = 101
        '
        'PROCESS1DataGridViewTextBoxColumn
        '
        Me.PROCESS1DataGridViewTextBoxColumn.DataPropertyName = "PROCESS1"
        Me.PROCESS1DataGridViewTextBoxColumn.HeaderText = "PROCESS1"
        Me.PROCESS1DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS1DataGridViewTextBoxColumn.Name = "PROCESS1DataGridViewTextBoxColumn"
        Me.PROCESS1DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS1DataGridViewTextBoxColumn.Width = 111
        '
        'PROCESS2DataGridViewTextBoxColumn
        '
        Me.PROCESS2DataGridViewTextBoxColumn.DataPropertyName = "PROCESS2"
        Me.PROCESS2DataGridViewTextBoxColumn.HeaderText = "PROCESS2"
        Me.PROCESS2DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS2DataGridViewTextBoxColumn.Name = "PROCESS2DataGridViewTextBoxColumn"
        Me.PROCESS2DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS2DataGridViewTextBoxColumn.Width = 111
        '
        'PROCESS3DataGridViewTextBoxColumn
        '
        Me.PROCESS3DataGridViewTextBoxColumn.DataPropertyName = "PROCESS3"
        Me.PROCESS3DataGridViewTextBoxColumn.HeaderText = "PROCESS3"
        Me.PROCESS3DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS3DataGridViewTextBoxColumn.Name = "PROCESS3DataGridViewTextBoxColumn"
        Me.PROCESS3DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS3DataGridViewTextBoxColumn.Width = 111
        '
        'PROCESS4DataGridViewTextBoxColumn
        '
        Me.PROCESS4DataGridViewTextBoxColumn.DataPropertyName = "PROCESS4"
        Me.PROCESS4DataGridViewTextBoxColumn.HeaderText = "PROCESS4"
        Me.PROCESS4DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS4DataGridViewTextBoxColumn.Name = "PROCESS4DataGridViewTextBoxColumn"
        Me.PROCESS4DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS4DataGridViewTextBoxColumn.Width = 111
        '
        'PROCESS5DataGridViewTextBoxColumn
        '
        Me.PROCESS5DataGridViewTextBoxColumn.DataPropertyName = "PROCESS5"
        Me.PROCESS5DataGridViewTextBoxColumn.HeaderText = "PROCESS5"
        Me.PROCESS5DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS5DataGridViewTextBoxColumn.Name = "PROCESS5DataGridViewTextBoxColumn"
        Me.PROCESS5DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS5DataGridViewTextBoxColumn.Width = 111
        '
        'PROCESS6DataGridViewTextBoxColumn
        '
        Me.PROCESS6DataGridViewTextBoxColumn.DataPropertyName = "PROCESS6"
        Me.PROCESS6DataGridViewTextBoxColumn.HeaderText = "PROCESS6"
        Me.PROCESS6DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS6DataGridViewTextBoxColumn.Name = "PROCESS6DataGridViewTextBoxColumn"
        Me.PROCESS6DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS6DataGridViewTextBoxColumn.Width = 111
        '
        'PROCESS7DataGridViewTextBoxColumn
        '
        Me.PROCESS7DataGridViewTextBoxColumn.DataPropertyName = "PROCESS7"
        Me.PROCESS7DataGridViewTextBoxColumn.HeaderText = "PROCESS7"
        Me.PROCESS7DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS7DataGridViewTextBoxColumn.Name = "PROCESS7DataGridViewTextBoxColumn"
        Me.PROCESS7DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS7DataGridViewTextBoxColumn.Width = 111
        '
        'PROCESS8DataGridViewTextBoxColumn
        '
        Me.PROCESS8DataGridViewTextBoxColumn.DataPropertyName = "PROCESS8"
        Me.PROCESS8DataGridViewTextBoxColumn.HeaderText = "PROCESS8"
        Me.PROCESS8DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS8DataGridViewTextBoxColumn.Name = "PROCESS8DataGridViewTextBoxColumn"
        Me.PROCESS8DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS8DataGridViewTextBoxColumn.Width = 111
        '
        'PROCESS9DataGridViewTextBoxColumn
        '
        Me.PROCESS9DataGridViewTextBoxColumn.DataPropertyName = "PROCESS9"
        Me.PROCESS9DataGridViewTextBoxColumn.HeaderText = "PROCESS9"
        Me.PROCESS9DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS9DataGridViewTextBoxColumn.Name = "PROCESS9DataGridViewTextBoxColumn"
        Me.PROCESS9DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS9DataGridViewTextBoxColumn.Width = 111
        '
        'PROCESS10DataGridViewTextBoxColumn
        '
        Me.PROCESS10DataGridViewTextBoxColumn.DataPropertyName = "PROCESS10"
        Me.PROCESS10DataGridViewTextBoxColumn.HeaderText = "PROCESS10"
        Me.PROCESS10DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS10DataGridViewTextBoxColumn.Name = "PROCESS10DataGridViewTextBoxColumn"
        Me.PROCESS10DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS10DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS11DataGridViewTextBoxColumn
        '
        Me.PROCESS11DataGridViewTextBoxColumn.DataPropertyName = "PROCESS11"
        Me.PROCESS11DataGridViewTextBoxColumn.HeaderText = "PROCESS11"
        Me.PROCESS11DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS11DataGridViewTextBoxColumn.Name = "PROCESS11DataGridViewTextBoxColumn"
        Me.PROCESS11DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS11DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS12DataGridViewTextBoxColumn
        '
        Me.PROCESS12DataGridViewTextBoxColumn.DataPropertyName = "PROCESS12"
        Me.PROCESS12DataGridViewTextBoxColumn.HeaderText = "PROCESS12"
        Me.PROCESS12DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS12DataGridViewTextBoxColumn.Name = "PROCESS12DataGridViewTextBoxColumn"
        Me.PROCESS12DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS12DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS13DataGridViewTextBoxColumn
        '
        Me.PROCESS13DataGridViewTextBoxColumn.DataPropertyName = "PROCESS13"
        Me.PROCESS13DataGridViewTextBoxColumn.HeaderText = "PROCESS13"
        Me.PROCESS13DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS13DataGridViewTextBoxColumn.Name = "PROCESS13DataGridViewTextBoxColumn"
        Me.PROCESS13DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS13DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS14DataGridViewTextBoxColumn
        '
        Me.PROCESS14DataGridViewTextBoxColumn.DataPropertyName = "PROCESS14"
        Me.PROCESS14DataGridViewTextBoxColumn.HeaderText = "PROCESS14"
        Me.PROCESS14DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS14DataGridViewTextBoxColumn.Name = "PROCESS14DataGridViewTextBoxColumn"
        Me.PROCESS14DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS14DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS15DataGridViewTextBoxColumn
        '
        Me.PROCESS15DataGridViewTextBoxColumn.DataPropertyName = "PROCESS15"
        Me.PROCESS15DataGridViewTextBoxColumn.HeaderText = "PROCESS15"
        Me.PROCESS15DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS15DataGridViewTextBoxColumn.Name = "PROCESS15DataGridViewTextBoxColumn"
        Me.PROCESS15DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS15DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS16DataGridViewTextBoxColumn
        '
        Me.PROCESS16DataGridViewTextBoxColumn.DataPropertyName = "PROCESS16"
        Me.PROCESS16DataGridViewTextBoxColumn.HeaderText = "PROCESS16"
        Me.PROCESS16DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS16DataGridViewTextBoxColumn.Name = "PROCESS16DataGridViewTextBoxColumn"
        Me.PROCESS16DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS16DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS17DataGridViewTextBoxColumn
        '
        Me.PROCESS17DataGridViewTextBoxColumn.DataPropertyName = "PROCESS17"
        Me.PROCESS17DataGridViewTextBoxColumn.HeaderText = "PROCESS17"
        Me.PROCESS17DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS17DataGridViewTextBoxColumn.Name = "PROCESS17DataGridViewTextBoxColumn"
        Me.PROCESS17DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS17DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS18DataGridViewTextBoxColumn
        '
        Me.PROCESS18DataGridViewTextBoxColumn.DataPropertyName = "PROCESS18"
        Me.PROCESS18DataGridViewTextBoxColumn.HeaderText = "PROCESS18"
        Me.PROCESS18DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS18DataGridViewTextBoxColumn.Name = "PROCESS18DataGridViewTextBoxColumn"
        Me.PROCESS18DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS18DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS19DataGridViewTextBoxColumn
        '
        Me.PROCESS19DataGridViewTextBoxColumn.DataPropertyName = "PROCESS19"
        Me.PROCESS19DataGridViewTextBoxColumn.HeaderText = "PROCESS19"
        Me.PROCESS19DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS19DataGridViewTextBoxColumn.Name = "PROCESS19DataGridViewTextBoxColumn"
        Me.PROCESS19DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS19DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS20DataGridViewTextBoxColumn
        '
        Me.PROCESS20DataGridViewTextBoxColumn.DataPropertyName = "PROCESS20"
        Me.PROCESS20DataGridViewTextBoxColumn.HeaderText = "PROCESS20"
        Me.PROCESS20DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS20DataGridViewTextBoxColumn.Name = "PROCESS20DataGridViewTextBoxColumn"
        Me.PROCESS20DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS20DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS21DataGridViewTextBoxColumn
        '
        Me.PROCESS21DataGridViewTextBoxColumn.DataPropertyName = "PROCESS21"
        Me.PROCESS21DataGridViewTextBoxColumn.HeaderText = "PROCESS21"
        Me.PROCESS21DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS21DataGridViewTextBoxColumn.Name = "PROCESS21DataGridViewTextBoxColumn"
        Me.PROCESS21DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS21DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS22DataGridViewTextBoxColumn
        '
        Me.PROCESS22DataGridViewTextBoxColumn.DataPropertyName = "PROCESS22"
        Me.PROCESS22DataGridViewTextBoxColumn.HeaderText = "PROCESS22"
        Me.PROCESS22DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS22DataGridViewTextBoxColumn.Name = "PROCESS22DataGridViewTextBoxColumn"
        Me.PROCESS22DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS22DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS23DataGridViewTextBoxColumn
        '
        Me.PROCESS23DataGridViewTextBoxColumn.DataPropertyName = "PROCESS23"
        Me.PROCESS23DataGridViewTextBoxColumn.HeaderText = "PROCESS23"
        Me.PROCESS23DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS23DataGridViewTextBoxColumn.Name = "PROCESS23DataGridViewTextBoxColumn"
        Me.PROCESS23DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS23DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS24DataGridViewTextBoxColumn
        '
        Me.PROCESS24DataGridViewTextBoxColumn.DataPropertyName = "PROCESS24"
        Me.PROCESS24DataGridViewTextBoxColumn.HeaderText = "PROCESS24"
        Me.PROCESS24DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS24DataGridViewTextBoxColumn.Name = "PROCESS24DataGridViewTextBoxColumn"
        Me.PROCESS24DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS24DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS25DataGridViewTextBoxColumn
        '
        Me.PROCESS25DataGridViewTextBoxColumn.DataPropertyName = "PROCESS25"
        Me.PROCESS25DataGridViewTextBoxColumn.HeaderText = "PROCESS25"
        Me.PROCESS25DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS25DataGridViewTextBoxColumn.Name = "PROCESS25DataGridViewTextBoxColumn"
        Me.PROCESS25DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS25DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS26DataGridViewTextBoxColumn
        '
        Me.PROCESS26DataGridViewTextBoxColumn.DataPropertyName = "PROCESS26"
        Me.PROCESS26DataGridViewTextBoxColumn.HeaderText = "PROCESS26"
        Me.PROCESS26DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS26DataGridViewTextBoxColumn.Name = "PROCESS26DataGridViewTextBoxColumn"
        Me.PROCESS26DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS26DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS27DataGridViewTextBoxColumn
        '
        Me.PROCESS27DataGridViewTextBoxColumn.DataPropertyName = "PROCESS27"
        Me.PROCESS27DataGridViewTextBoxColumn.HeaderText = "PROCESS27"
        Me.PROCESS27DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS27DataGridViewTextBoxColumn.Name = "PROCESS27DataGridViewTextBoxColumn"
        Me.PROCESS27DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS27DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS28DataGridViewTextBoxColumn
        '
        Me.PROCESS28DataGridViewTextBoxColumn.DataPropertyName = "PROCESS28"
        Me.PROCESS28DataGridViewTextBoxColumn.HeaderText = "PROCESS28"
        Me.PROCESS28DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS28DataGridViewTextBoxColumn.Name = "PROCESS28DataGridViewTextBoxColumn"
        Me.PROCESS28DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS28DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS29DataGridViewTextBoxColumn
        '
        Me.PROCESS29DataGridViewTextBoxColumn.DataPropertyName = "PROCESS29"
        Me.PROCESS29DataGridViewTextBoxColumn.HeaderText = "PROCESS29"
        Me.PROCESS29DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS29DataGridViewTextBoxColumn.Name = "PROCESS29DataGridViewTextBoxColumn"
        Me.PROCESS29DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS29DataGridViewTextBoxColumn.Width = 120
        '
        'PROCESS30DataGridViewTextBoxColumn
        '
        Me.PROCESS30DataGridViewTextBoxColumn.DataPropertyName = "PROCESS30"
        Me.PROCESS30DataGridViewTextBoxColumn.HeaderText = "PROCESS30"
        Me.PROCESS30DataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PROCESS30DataGridViewTextBoxColumn.Name = "PROCESS30DataGridViewTextBoxColumn"
        Me.PROCESS30DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PROCESS30DataGridViewTextBoxColumn.Width = 120
        '
        'SUMMARYTRACEABILITYBindingSource
        '
        Me.SUMMARYTRACEABILITYBindingSource.DataMember = "SUMMARY_TRACEABILITY"
        Me.SUMMARYTRACEABILITYBindingSource.DataSource = Me.JOVANDataSet1
        '
        'JOVANDataSet1
        '
        Me.JOVANDataSet1.DataSetName = "JOVANDataSet1"
        Me.JOVANDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DGTraceability2V2
        '
        Me.DGTraceability2V2.AutoGenerateColumns = False
        Me.DGTraceability2V2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGTraceability2V2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.DGTraceability2V2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGTraceability2V2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn2, Me.LINEDataGridViewTextBoxColumn1, Me.COMPONENTDataGridViewTextBoxColumn, Me.DESCDataGridViewTextBoxColumn, Me.INVDataGridViewTextBoxColumn1, Me.BATCHNODataGridViewTextBoxColumn1, Me.LOTCOMPDataGridViewTextBoxColumn, Me.LOTFGDataGridViewTextBoxColumn, Me.QTYDataGridViewTextBoxColumn, Me.REMARKDataGridViewTextBoxColumn})
        Me.DGTraceability2V2.DataSource = Me.SUMMARYTRACEABILITYCOMPBindingSource
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGTraceability2V2.DefaultCellStyle = DataGridViewCellStyle12
        Me.DGTraceability2V2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGTraceability2V2.FilterAndSortEnabled = True
        Me.DGTraceability2V2.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGTraceability2V2.Location = New System.Drawing.Point(3, 266)
        Me.DGTraceability2V2.Name = "DGTraceability2V2"
        Me.DGTraceability2V2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DGTraceability2V2.Size = New System.Drawing.Size(1487, 219)
        Me.DGTraceability2V2.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DGTraceability2V2.TabIndex = 7
        '
        'IDDataGridViewTextBoxColumn2
        '
        Me.IDDataGridViewTextBoxColumn2.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn2.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn2.MinimumWidth = 22
        Me.IDDataGridViewTextBoxColumn2.Name = "IDDataGridViewTextBoxColumn2"
        Me.IDDataGridViewTextBoxColumn2.ReadOnly = True
        Me.IDDataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.IDDataGridViewTextBoxColumn2.Width = 51
        '
        'LINEDataGridViewTextBoxColumn1
        '
        Me.LINEDataGridViewTextBoxColumn1.DataPropertyName = "LINE"
        Me.LINEDataGridViewTextBoxColumn1.HeaderText = "LINE"
        Me.LINEDataGridViewTextBoxColumn1.MinimumWidth = 22
        Me.LINEDataGridViewTextBoxColumn1.Name = "LINEDataGridViewTextBoxColumn1"
        Me.LINEDataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.LINEDataGridViewTextBoxColumn1.Width = 68
        '
        'COMPONENTDataGridViewTextBoxColumn
        '
        Me.COMPONENTDataGridViewTextBoxColumn.DataPropertyName = "COMPONENT"
        Me.COMPONENTDataGridViewTextBoxColumn.HeaderText = "COMPONENT"
        Me.COMPONENTDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.COMPONENTDataGridViewTextBoxColumn.Name = "COMPONENTDataGridViewTextBoxColumn"
        Me.COMPONENTDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.COMPONENTDataGridViewTextBoxColumn.Width = 130
        '
        'DESCDataGridViewTextBoxColumn
        '
        Me.DESCDataGridViewTextBoxColumn.DataPropertyName = "DESC"
        Me.DESCDataGridViewTextBoxColumn.HeaderText = "DESC"
        Me.DESCDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.DESCDataGridViewTextBoxColumn.Name = "DESCDataGridViewTextBoxColumn"
        Me.DESCDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.DESCDataGridViewTextBoxColumn.Width = 73
        '
        'INVDataGridViewTextBoxColumn1
        '
        Me.INVDataGridViewTextBoxColumn1.DataPropertyName = "INV"
        Me.INVDataGridViewTextBoxColumn1.HeaderText = "INV"
        Me.INVDataGridViewTextBoxColumn1.MinimumWidth = 22
        Me.INVDataGridViewTextBoxColumn1.Name = "INVDataGridViewTextBoxColumn1"
        Me.INVDataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.INVDataGridViewTextBoxColumn1.Width = 61
        '
        'BATCHNODataGridViewTextBoxColumn1
        '
        Me.BATCHNODataGridViewTextBoxColumn1.DataPropertyName = "BATCH_NO"
        Me.BATCHNODataGridViewTextBoxColumn1.HeaderText = "BATCH_NO"
        Me.BATCHNODataGridViewTextBoxColumn1.MinimumWidth = 22
        Me.BATCHNODataGridViewTextBoxColumn1.Name = "BATCHNODataGridViewTextBoxColumn1"
        Me.BATCHNODataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.BATCHNODataGridViewTextBoxColumn1.Width = 117
        '
        'LOTCOMPDataGridViewTextBoxColumn
        '
        Me.LOTCOMPDataGridViewTextBoxColumn.DataPropertyName = "LOT_COMP"
        Me.LOTCOMPDataGridViewTextBoxColumn.HeaderText = "LOT_COMP"
        Me.LOTCOMPDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.LOTCOMPDataGridViewTextBoxColumn.Name = "LOTCOMPDataGridViewTextBoxColumn"
        Me.LOTCOMPDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.LOTCOMPDataGridViewTextBoxColumn.Width = 116
        '
        'LOTFGDataGridViewTextBoxColumn
        '
        Me.LOTFGDataGridViewTextBoxColumn.DataPropertyName = "LOT_FG"
        Me.LOTFGDataGridViewTextBoxColumn.HeaderText = "LOT_FG"
        Me.LOTFGDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.LOTFGDataGridViewTextBoxColumn.Name = "LOTFGDataGridViewTextBoxColumn"
        Me.LOTFGDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.LOTFGDataGridViewTextBoxColumn.Width = 92
        '
        'QTYDataGridViewTextBoxColumn
        '
        Me.QTYDataGridViewTextBoxColumn.DataPropertyName = "QTY"
        Me.QTYDataGridViewTextBoxColumn.HeaderText = "QTY"
        Me.QTYDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.QTYDataGridViewTextBoxColumn.Name = "QTYDataGridViewTextBoxColumn"
        Me.QTYDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.QTYDataGridViewTextBoxColumn.Width = 66
        '
        'REMARKDataGridViewTextBoxColumn
        '
        Me.REMARKDataGridViewTextBoxColumn.DataPropertyName = "REMARK"
        Me.REMARKDataGridViewTextBoxColumn.HeaderText = "REMARK"
        Me.REMARKDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.REMARKDataGridViewTextBoxColumn.Name = "REMARKDataGridViewTextBoxColumn"
        Me.REMARKDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.REMARKDataGridViewTextBoxColumn.Width = 95
        '
        'SUMMARYTRACEABILITYCOMPBindingSource
        '
        Me.SUMMARYTRACEABILITYCOMPBindingSource.DataMember = "SUMMARY_TRACEABILITY_COMP"
        Me.SUMMARYTRACEABILITYCOMPBindingSource.DataSource = Me.JOVANDataSet2
        '
        'JOVANDataSet2
        '
        Me.JOVANDataSet2.DataSetName = "JOVANDataSet2"
        Me.JOVANDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'SUMMARY_FGTableAdapter
        '
        Me.SUMMARY_FGTableAdapter.ClearBeforeFill = True
        '
        'JOVANDataSetBindingSource
        '
        Me.JOVANDataSetBindingSource.DataSource = Me.JOVANDataSet
        Me.JOVANDataSetBindingSource.Position = 0
        '
        'SUMMARY_TRACEABILITYTableAdapter
        '
        Me.SUMMARY_TRACEABILITYTableAdapter.ClearBeforeFill = True
        '
        'SUMMARY_TRACEABILITY_COMPTableAdapter
        '
        Me.SUMMARY_TRACEABILITY_COMPTableAdapter.ClearBeforeFill = True
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
        CType(Me.SUMMARYFGBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.JOVANDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.DGTraceability1V2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SUMMARYTRACEABILITYBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.JOVANDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGTraceability2V2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SUMMARYTRACEABILITYCOMPBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.JOVANDataSet2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.JOVANDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents JOVANDataSet As JOVANDataSet
    Friend WithEvents SUMMARYFGBindingSource As BindingSource
    Friend WithEvents SUMMARY_FGTableAdapter As JOVANDataSetTableAdapters.SUMMARY_FGTableAdapter
    Friend WithEvents IDDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
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
    Friend WithEvents DGTraceability1V2 As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents JOVANDataSetBindingSource As BindingSource
    Friend WithEvents JOVANDataSet1 As JOVANDataSet1
    Friend WithEvents SUMMARYTRACEABILITYBindingSource As BindingSource
    Friend WithEvents SUMMARY_TRACEABILITYTableAdapter As JOVANDataSet1TableAdapters.SUMMARY_TRACEABILITYTableAdapter
    Friend WithEvents IDDataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
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
    Friend WithEvents DGTraceability2V2 As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents JOVANDataSet2 As JOVANDataSet2
    Friend WithEvents SUMMARYTRACEABILITYCOMPBindingSource As BindingSource
    Friend WithEvents SUMMARY_TRACEABILITY_COMPTableAdapter As JOVANDataSet2TableAdapters.SUMMARY_TRACEABILITY_COMPTableAdapter
    Friend WithEvents IDDataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents LINEDataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents COMPONENTDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents DESCDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents INVDataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents BATCHNODataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents LOTCOMPDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents LOTFGDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents QTYDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents REMARKDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
End Class
