<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StockMinistoreV2
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.AdvancedDataGridView1 = New Zuby.ADGV.AdvancedDataGridView()
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MTSNODataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DEPARTMENTDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MATERIALDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.STATUSDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.STANDARDPACKDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INVCTRLDATEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TRACEABILITYDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BATCHNODataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LOTNODataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FINISHGOODSPNDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PODataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SUBPODataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SUBSUBPODataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LINEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DATETIMEINSERTDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SAVEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QRCODEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DATETIMESAVEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTYDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ACTUALQTYDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FIFODataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IDLEVELDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LEVELDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FLOWTICKETDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SUMQTYDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.STOCKCARDBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.JOVANDataSet = New Jovan_New.JOVANDataSet()
        Me.STOCK_CARDTableAdapter = New Jovan_New.JOVANDataSetTableAdapters.STOCK_CARDTableAdapter()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.AdvancedDataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.STOCKCARDBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.JOVANDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AdvancedDataGridView1
        '
        Me.AdvancedDataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AdvancedDataGridView1.AutoGenerateColumns = False
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AdvancedDataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.AdvancedDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.AdvancedDataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn, Me.MTSNODataGridViewTextBoxColumn, Me.DEPARTMENTDataGridViewTextBoxColumn, Me.MATERIALDataGridViewTextBoxColumn, Me.STATUSDataGridViewTextBoxColumn, Me.STANDARDPACKDataGridViewTextBoxColumn, Me.INVCTRLDATEDataGridViewTextBoxColumn, Me.TRACEABILITYDataGridViewTextBoxColumn, Me.BATCHNODataGridViewTextBoxColumn, Me.LOTNODataGridViewTextBoxColumn, Me.FINISHGOODSPNDataGridViewTextBoxColumn, Me.PODataGridViewTextBoxColumn, Me.SUBPODataGridViewTextBoxColumn, Me.SUBSUBPODataGridViewTextBoxColumn, Me.LINEDataGridViewTextBoxColumn, Me.DATETIMEINSERTDataGridViewTextBoxColumn, Me.SAVEDataGridViewTextBoxColumn, Me.QRCODEDataGridViewTextBoxColumn, Me.DATETIMESAVEDataGridViewTextBoxColumn, Me.QTYDataGridViewTextBoxColumn, Me.ACTUALQTYDataGridViewTextBoxColumn, Me.FIFODataGridViewTextBoxColumn, Me.IDLEVELDataGridViewTextBoxColumn, Me.LEVELDataGridViewTextBoxColumn, Me.FLOWTICKETDataGridViewTextBoxColumn, Me.SUMQTYDataGridViewTextBoxColumn})
        Me.AdvancedDataGridView1.DataSource = Me.STOCKCARDBindingSource
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.AdvancedDataGridView1.DefaultCellStyle = DataGridViewCellStyle11
        Me.AdvancedDataGridView1.FilterAndSortEnabled = True
        Me.AdvancedDataGridView1.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.AdvancedDataGridView1.Location = New System.Drawing.Point(14, 24)
        Me.AdvancedDataGridView1.Name = "AdvancedDataGridView1"
        Me.AdvancedDataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AdvancedDataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.AdvancedDataGridView1.Size = New System.Drawing.Size(1156, 493)
        Me.AdvancedDataGridView1.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.AdvancedDataGridView1.TabIndex = 1
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
        'MTSNODataGridViewTextBoxColumn
        '
        Me.MTSNODataGridViewTextBoxColumn.DataPropertyName = "MTS_NO"
        Me.MTSNODataGridViewTextBoxColumn.HeaderText = "MTS_NO"
        Me.MTSNODataGridViewTextBoxColumn.MinimumWidth = 22
        Me.MTSNODataGridViewTextBoxColumn.Name = "MTSNODataGridViewTextBoxColumn"
        Me.MTSNODataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'DEPARTMENTDataGridViewTextBoxColumn
        '
        Me.DEPARTMENTDataGridViewTextBoxColumn.DataPropertyName = "DEPARTMENT"
        Me.DEPARTMENTDataGridViewTextBoxColumn.HeaderText = "DEPARTMENT"
        Me.DEPARTMENTDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.DEPARTMENTDataGridViewTextBoxColumn.Name = "DEPARTMENTDataGridViewTextBoxColumn"
        Me.DEPARTMENTDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'MATERIALDataGridViewTextBoxColumn
        '
        Me.MATERIALDataGridViewTextBoxColumn.DataPropertyName = "MATERIAL"
        Me.MATERIALDataGridViewTextBoxColumn.HeaderText = "MATERIAL"
        Me.MATERIALDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.MATERIALDataGridViewTextBoxColumn.Name = "MATERIALDataGridViewTextBoxColumn"
        Me.MATERIALDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'STATUSDataGridViewTextBoxColumn
        '
        Me.STATUSDataGridViewTextBoxColumn.DataPropertyName = "STATUS"
        Me.STATUSDataGridViewTextBoxColumn.HeaderText = "STATUS"
        Me.STATUSDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.STATUSDataGridViewTextBoxColumn.Name = "STATUSDataGridViewTextBoxColumn"
        Me.STATUSDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'STANDARDPACKDataGridViewTextBoxColumn
        '
        Me.STANDARDPACKDataGridViewTextBoxColumn.DataPropertyName = "STANDARD_PACK"
        Me.STANDARDPACKDataGridViewTextBoxColumn.HeaderText = "STANDARD_PACK"
        Me.STANDARDPACKDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.STANDARDPACKDataGridViewTextBoxColumn.Name = "STANDARDPACKDataGridViewTextBoxColumn"
        Me.STANDARDPACKDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'INVCTRLDATEDataGridViewTextBoxColumn
        '
        Me.INVCTRLDATEDataGridViewTextBoxColumn.DataPropertyName = "INV_CTRL_DATE"
        Me.INVCTRLDATEDataGridViewTextBoxColumn.HeaderText = "INV_CTRL_DATE"
        Me.INVCTRLDATEDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.INVCTRLDATEDataGridViewTextBoxColumn.Name = "INVCTRLDATEDataGridViewTextBoxColumn"
        Me.INVCTRLDATEDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'TRACEABILITYDataGridViewTextBoxColumn
        '
        Me.TRACEABILITYDataGridViewTextBoxColumn.DataPropertyName = "TRACEABILITY"
        Me.TRACEABILITYDataGridViewTextBoxColumn.HeaderText = "TRACEABILITY"
        Me.TRACEABILITYDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.TRACEABILITYDataGridViewTextBoxColumn.Name = "TRACEABILITYDataGridViewTextBoxColumn"
        Me.TRACEABILITYDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'BATCHNODataGridViewTextBoxColumn
        '
        Me.BATCHNODataGridViewTextBoxColumn.DataPropertyName = "BATCH_NO"
        Me.BATCHNODataGridViewTextBoxColumn.HeaderText = "BATCH_NO"
        Me.BATCHNODataGridViewTextBoxColumn.MinimumWidth = 22
        Me.BATCHNODataGridViewTextBoxColumn.Name = "BATCHNODataGridViewTextBoxColumn"
        Me.BATCHNODataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'LOTNODataGridViewTextBoxColumn
        '
        Me.LOTNODataGridViewTextBoxColumn.DataPropertyName = "LOT_NO"
        Me.LOTNODataGridViewTextBoxColumn.HeaderText = "LOT_NO"
        Me.LOTNODataGridViewTextBoxColumn.MinimumWidth = 22
        Me.LOTNODataGridViewTextBoxColumn.Name = "LOTNODataGridViewTextBoxColumn"
        Me.LOTNODataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'FINISHGOODSPNDataGridViewTextBoxColumn
        '
        Me.FINISHGOODSPNDataGridViewTextBoxColumn.DataPropertyName = "FINISH_GOODS_PN"
        Me.FINISHGOODSPNDataGridViewTextBoxColumn.HeaderText = "FINISH_GOODS_PN"
        Me.FINISHGOODSPNDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.FINISHGOODSPNDataGridViewTextBoxColumn.Name = "FINISHGOODSPNDataGridViewTextBoxColumn"
        Me.FINISHGOODSPNDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'PODataGridViewTextBoxColumn
        '
        Me.PODataGridViewTextBoxColumn.DataPropertyName = "PO"
        Me.PODataGridViewTextBoxColumn.HeaderText = "PO"
        Me.PODataGridViewTextBoxColumn.MinimumWidth = 22
        Me.PODataGridViewTextBoxColumn.Name = "PODataGridViewTextBoxColumn"
        Me.PODataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'SUBPODataGridViewTextBoxColumn
        '
        Me.SUBPODataGridViewTextBoxColumn.DataPropertyName = "SUB_PO"
        Me.SUBPODataGridViewTextBoxColumn.HeaderText = "SUB_PO"
        Me.SUBPODataGridViewTextBoxColumn.MinimumWidth = 22
        Me.SUBPODataGridViewTextBoxColumn.Name = "SUBPODataGridViewTextBoxColumn"
        Me.SUBPODataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'SUBSUBPODataGridViewTextBoxColumn
        '
        Me.SUBSUBPODataGridViewTextBoxColumn.DataPropertyName = "SUB_SUB_PO"
        Me.SUBSUBPODataGridViewTextBoxColumn.HeaderText = "SUB_SUB_PO"
        Me.SUBSUBPODataGridViewTextBoxColumn.MinimumWidth = 22
        Me.SUBSUBPODataGridViewTextBoxColumn.Name = "SUBSUBPODataGridViewTextBoxColumn"
        Me.SUBSUBPODataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'LINEDataGridViewTextBoxColumn
        '
        Me.LINEDataGridViewTextBoxColumn.DataPropertyName = "LINE"
        Me.LINEDataGridViewTextBoxColumn.HeaderText = "LINE"
        Me.LINEDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.LINEDataGridViewTextBoxColumn.Name = "LINEDataGridViewTextBoxColumn"
        Me.LINEDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'DATETIMEINSERTDataGridViewTextBoxColumn
        '
        Me.DATETIMEINSERTDataGridViewTextBoxColumn.DataPropertyName = "DATETIME_INSERT"
        Me.DATETIMEINSERTDataGridViewTextBoxColumn.HeaderText = "DATETIME_INSERT"
        Me.DATETIMEINSERTDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.DATETIMEINSERTDataGridViewTextBoxColumn.Name = "DATETIMEINSERTDataGridViewTextBoxColumn"
        Me.DATETIMEINSERTDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'SAVEDataGridViewTextBoxColumn
        '
        Me.SAVEDataGridViewTextBoxColumn.DataPropertyName = "SAVE"
        Me.SAVEDataGridViewTextBoxColumn.HeaderText = "SAVE"
        Me.SAVEDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.SAVEDataGridViewTextBoxColumn.Name = "SAVEDataGridViewTextBoxColumn"
        Me.SAVEDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'QRCODEDataGridViewTextBoxColumn
        '
        Me.QRCODEDataGridViewTextBoxColumn.DataPropertyName = "QRCODE"
        Me.QRCODEDataGridViewTextBoxColumn.HeaderText = "QRCODE"
        Me.QRCODEDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.QRCODEDataGridViewTextBoxColumn.Name = "QRCODEDataGridViewTextBoxColumn"
        Me.QRCODEDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'DATETIMESAVEDataGridViewTextBoxColumn
        '
        Me.DATETIMESAVEDataGridViewTextBoxColumn.DataPropertyName = "DATETIME_SAVE"
        Me.DATETIMESAVEDataGridViewTextBoxColumn.HeaderText = "DATETIME_SAVE"
        Me.DATETIMESAVEDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.DATETIMESAVEDataGridViewTextBoxColumn.Name = "DATETIMESAVEDataGridViewTextBoxColumn"
        Me.DATETIMESAVEDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'QTYDataGridViewTextBoxColumn
        '
        Me.QTYDataGridViewTextBoxColumn.DataPropertyName = "QTY"
        Me.QTYDataGridViewTextBoxColumn.HeaderText = "QTY"
        Me.QTYDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.QTYDataGridViewTextBoxColumn.Name = "QTYDataGridViewTextBoxColumn"
        Me.QTYDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'ACTUALQTYDataGridViewTextBoxColumn
        '
        Me.ACTUALQTYDataGridViewTextBoxColumn.DataPropertyName = "ACTUAL_QTY"
        Me.ACTUALQTYDataGridViewTextBoxColumn.HeaderText = "ACTUAL_QTY"
        Me.ACTUALQTYDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.ACTUALQTYDataGridViewTextBoxColumn.Name = "ACTUALQTYDataGridViewTextBoxColumn"
        Me.ACTUALQTYDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'FIFODataGridViewTextBoxColumn
        '
        Me.FIFODataGridViewTextBoxColumn.DataPropertyName = "FIFO"
        Me.FIFODataGridViewTextBoxColumn.HeaderText = "FIFO"
        Me.FIFODataGridViewTextBoxColumn.MinimumWidth = 22
        Me.FIFODataGridViewTextBoxColumn.Name = "FIFODataGridViewTextBoxColumn"
        Me.FIFODataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'IDLEVELDataGridViewTextBoxColumn
        '
        Me.IDLEVELDataGridViewTextBoxColumn.DataPropertyName = "ID_LEVEL"
        Me.IDLEVELDataGridViewTextBoxColumn.HeaderText = "ID_LEVEL"
        Me.IDLEVELDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.IDLEVELDataGridViewTextBoxColumn.Name = "IDLEVELDataGridViewTextBoxColumn"
        Me.IDLEVELDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'LEVELDataGridViewTextBoxColumn
        '
        Me.LEVELDataGridViewTextBoxColumn.DataPropertyName = "LEVEL"
        Me.LEVELDataGridViewTextBoxColumn.HeaderText = "LEVEL"
        Me.LEVELDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.LEVELDataGridViewTextBoxColumn.Name = "LEVELDataGridViewTextBoxColumn"
        Me.LEVELDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'FLOWTICKETDataGridViewTextBoxColumn
        '
        Me.FLOWTICKETDataGridViewTextBoxColumn.DataPropertyName = "FLOW_TICKET"
        Me.FLOWTICKETDataGridViewTextBoxColumn.HeaderText = "FLOW_TICKET"
        Me.FLOWTICKETDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.FLOWTICKETDataGridViewTextBoxColumn.Name = "FLOWTICKETDataGridViewTextBoxColumn"
        Me.FLOWTICKETDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'SUMQTYDataGridViewTextBoxColumn
        '
        Me.SUMQTYDataGridViewTextBoxColumn.DataPropertyName = "SUM_QTY"
        Me.SUMQTYDataGridViewTextBoxColumn.HeaderText = "SUM_QTY"
        Me.SUMQTYDataGridViewTextBoxColumn.MinimumWidth = 22
        Me.SUMQTYDataGridViewTextBoxColumn.Name = "SUMQTYDataGridViewTextBoxColumn"
        Me.SUMQTYDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'STOCKCARDBindingSource
        '
        Me.STOCKCARDBindingSource.DataMember = "STOCK_CARD"
        Me.STOCKCARDBindingSource.DataSource = Me.JOVANDataSet
        '
        'JOVANDataSet
        '
        Me.JOVANDataSet.DataSetName = "JOVANDataSet"
        Me.JOVANDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'STOCK_CARDTableAdapter
        '
        Me.STOCK_CARDTableAdapter.ClearBeforeFill = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.Blue
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button1.Location = New System.Drawing.Point(1013, 523)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(157, 37)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Export To Excel"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'StockMinistoreV2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1182, 669)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.AdvancedDataGridView1)
        Me.Name = "StockMinistoreV2"
        Me.Text = "StockMinistoreV2"
        CType(Me.AdvancedDataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.STOCKCARDBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.JOVANDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AdvancedDataGridView1 As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents JOVANDataSet As JOVANDataSet
    Friend WithEvents STOCKCARDBindingSource As BindingSource
    Friend WithEvents STOCK_CARDTableAdapter As JOVANDataSetTableAdapters.STOCK_CARDTableAdapter
    Friend WithEvents IDDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents MTSNODataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents DEPARTMENTDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents MATERIALDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents STATUSDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents STANDARDPACKDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents INVCTRLDATEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents TRACEABILITYDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents BATCHNODataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents LOTNODataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents FINISHGOODSPNDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PODataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents SUBPODataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents SUBSUBPODataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents LINEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents DATETIMEINSERTDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents SAVEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents QRCODEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents DATETIMESAVEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents QTYDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents ACTUALQTYDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents FIFODataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents IDLEVELDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents LEVELDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents FLOWTICKETDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents SUMQTYDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents Button1 As Button
End Class
