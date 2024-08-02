<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StockProduction
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
        Me.DG_SCMaterial = New Zuby.ADGV.AdvancedDataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DG_SCSA = New Zuby.ADGV.AdvancedDataGridView()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.DG_SCWIPBAWAH = New Zuby.ADGV.AdvancedDataGridView()
        Me.DG_SCWIPATAS = New Zuby.ADGV.AdvancedDataGridView()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.DG_SCOHBAWAH = New Zuby.ADGV.AdvancedDataGridView()
        Me.DG_SCOHATAS = New Zuby.ADGV.AdvancedDataGridView()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.DG_SCDEFECTBAWAH = New Zuby.ADGV.AdvancedDataGridView()
        Me.DG_SCDEFECTATAS = New Zuby.ADGV.AdvancedDataGridView()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.DG_SCReject = New Zuby.ADGV.AdvancedDataGridView()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.DG_SCOthers = New Zuby.ADGV.AdvancedDataGridView()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.btn_ExportTrace1 = New System.Windows.Forms.Button()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DG_SCMaterial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.DG_SCSA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.DG_SCWIPBAWAH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DG_SCWIPATAS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.DG_SCOHBAWAH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DG_SCOHATAS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.DG_SCDEFECTBAWAH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DG_SCDEFECTATAS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage6.SuspendLayout()
        CType(Me.DG_SCReject, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage7.SuspendLayout()
        CType(Me.DG_SCOthers, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox1.Controls.Add(Me.btn_ExportTrace1)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(2, -14)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1526, 586)
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
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Controls.Add(Me.TabPage7)
        Me.TabControl1.Location = New System.Drawing.Point(6, 68)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1520, 467)
        Me.TabControl1.TabIndex = 16
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.DarkGray
        Me.TabPage1.Controls.Add(Me.DG_SCMaterial)
        Me.TabPage1.Location = New System.Drawing.Point(4, 38)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1512, 425)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Stock Card Material"
        '
        'DG_SCMaterial
        '
        Me.DG_SCMaterial.AllowUserToAddRows = False
        Me.DG_SCMaterial.AllowUserToDeleteRows = False
        Me.DG_SCMaterial.AllowUserToResizeRows = False
        Me.DG_SCMaterial.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_SCMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG_SCMaterial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG_SCMaterial.FilterAndSortEnabled = True
        Me.DG_SCMaterial.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCMaterial.Location = New System.Drawing.Point(3, 3)
        Me.DG_SCMaterial.Name = "DG_SCMaterial"
        Me.DG_SCMaterial.ReadOnly = True
        Me.DG_SCMaterial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DG_SCMaterial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DG_SCMaterial.Size = New System.Drawing.Size(1506, 419)
        Me.DG_SCMaterial.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCMaterial.TabIndex = 8
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.DarkGray
        Me.TabPage2.Controls.Add(Me.DG_SCSA)
        Me.TabPage2.Location = New System.Drawing.Point(4, 38)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1512, 425)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Stock Card Sub Assy"
        '
        'DG_SCSA
        '
        Me.DG_SCSA.AllowUserToAddRows = False
        Me.DG_SCSA.AllowUserToDeleteRows = False
        Me.DG_SCSA.AllowUserToResizeRows = False
        Me.DG_SCSA.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_SCSA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG_SCSA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG_SCSA.FilterAndSortEnabled = True
        Me.DG_SCSA.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCSA.Location = New System.Drawing.Point(3, 3)
        Me.DG_SCSA.Name = "DG_SCSA"
        Me.DG_SCSA.ReadOnly = True
        Me.DG_SCSA.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DG_SCSA.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DG_SCSA.Size = New System.Drawing.Size(1506, 419)
        Me.DG_SCSA.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCSA.TabIndex = 9
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.DarkGray
        Me.TabPage3.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 38)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(1512, 425)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Stock Card WIP"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.DG_SCWIPBAWAH, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.DG_SCWIPATAS, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1512, 425)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'DG_SCWIPBAWAH
        '
        Me.DG_SCWIPBAWAH.AllowUserToAddRows = False
        Me.DG_SCWIPBAWAH.AllowUserToDeleteRows = False
        Me.DG_SCWIPBAWAH.AllowUserToResizeRows = False
        Me.DG_SCWIPBAWAH.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_SCWIPBAWAH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG_SCWIPBAWAH.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG_SCWIPBAWAH.FilterAndSortEnabled = True
        Me.DG_SCWIPBAWAH.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCWIPBAWAH.Location = New System.Drawing.Point(3, 258)
        Me.DG_SCWIPBAWAH.Name = "DG_SCWIPBAWAH"
        Me.DG_SCWIPBAWAH.ReadOnly = True
        Me.DG_SCWIPBAWAH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DG_SCWIPBAWAH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DG_SCWIPBAWAH.Size = New System.Drawing.Size(1506, 164)
        Me.DG_SCWIPBAWAH.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCWIPBAWAH.TabIndex = 11
        '
        'DG_SCWIPATAS
        '
        Me.DG_SCWIPATAS.AllowUserToAddRows = False
        Me.DG_SCWIPATAS.AllowUserToDeleteRows = False
        Me.DG_SCWIPATAS.AllowUserToResizeRows = False
        Me.DG_SCWIPATAS.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_SCWIPATAS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG_SCWIPATAS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG_SCWIPATAS.FilterAndSortEnabled = True
        Me.DG_SCWIPATAS.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCWIPATAS.Location = New System.Drawing.Point(3, 3)
        Me.DG_SCWIPATAS.Name = "DG_SCWIPATAS"
        Me.DG_SCWIPATAS.ReadOnly = True
        Me.DG_SCWIPATAS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DG_SCWIPATAS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DG_SCWIPATAS.Size = New System.Drawing.Size(1506, 249)
        Me.DG_SCWIPATAS.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCWIPATAS.TabIndex = 10
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.Color.DarkGray
        Me.TabPage4.Controls.Add(Me.TableLayoutPanel2)
        Me.TabPage4.Location = New System.Drawing.Point(4, 38)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(1512, 425)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Stock Card On Hold"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.DG_SCOHBAWAH, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.DG_SCOHATAS, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1512, 425)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'DG_SCOHBAWAH
        '
        Me.DG_SCOHBAWAH.AllowUserToAddRows = False
        Me.DG_SCOHBAWAH.AllowUserToDeleteRows = False
        Me.DG_SCOHBAWAH.AllowUserToResizeRows = False
        Me.DG_SCOHBAWAH.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_SCOHBAWAH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG_SCOHBAWAH.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG_SCOHBAWAH.FilterAndSortEnabled = True
        Me.DG_SCOHBAWAH.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCOHBAWAH.Location = New System.Drawing.Point(3, 258)
        Me.DG_SCOHBAWAH.Name = "DG_SCOHBAWAH"
        Me.DG_SCOHBAWAH.ReadOnly = True
        Me.DG_SCOHBAWAH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DG_SCOHBAWAH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DG_SCOHBAWAH.Size = New System.Drawing.Size(1506, 164)
        Me.DG_SCOHBAWAH.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCOHBAWAH.TabIndex = 11
        '
        'DG_SCOHATAS
        '
        Me.DG_SCOHATAS.AllowUserToAddRows = False
        Me.DG_SCOHATAS.AllowUserToDeleteRows = False
        Me.DG_SCOHATAS.AllowUserToResizeRows = False
        Me.DG_SCOHATAS.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_SCOHATAS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG_SCOHATAS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG_SCOHATAS.FilterAndSortEnabled = True
        Me.DG_SCOHATAS.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCOHATAS.Location = New System.Drawing.Point(3, 3)
        Me.DG_SCOHATAS.Name = "DG_SCOHATAS"
        Me.DG_SCOHATAS.ReadOnly = True
        Me.DG_SCOHATAS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DG_SCOHATAS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DG_SCOHATAS.Size = New System.Drawing.Size(1506, 249)
        Me.DG_SCOHATAS.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCOHATAS.TabIndex = 10
        '
        'TabPage5
        '
        Me.TabPage5.BackColor = System.Drawing.Color.DarkGray
        Me.TabPage5.Controls.Add(Me.TableLayoutPanel3)
        Me.TabPage5.Location = New System.Drawing.Point(4, 38)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(1512, 425)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Stock Card Defect"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.DG_SCDEFECTBAWAH, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.DG_SCDEFECTATAS, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1512, 425)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'DG_SCDEFECTBAWAH
        '
        Me.DG_SCDEFECTBAWAH.AllowUserToAddRows = False
        Me.DG_SCDEFECTBAWAH.AllowUserToDeleteRows = False
        Me.DG_SCDEFECTBAWAH.AllowUserToResizeRows = False
        Me.DG_SCDEFECTBAWAH.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_SCDEFECTBAWAH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG_SCDEFECTBAWAH.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG_SCDEFECTBAWAH.FilterAndSortEnabled = True
        Me.DG_SCDEFECTBAWAH.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCDEFECTBAWAH.Location = New System.Drawing.Point(3, 258)
        Me.DG_SCDEFECTBAWAH.Name = "DG_SCDEFECTBAWAH"
        Me.DG_SCDEFECTBAWAH.ReadOnly = True
        Me.DG_SCDEFECTBAWAH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DG_SCDEFECTBAWAH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DG_SCDEFECTBAWAH.Size = New System.Drawing.Size(1506, 164)
        Me.DG_SCDEFECTBAWAH.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCDEFECTBAWAH.TabIndex = 11
        '
        'DG_SCDEFECTATAS
        '
        Me.DG_SCDEFECTATAS.AllowUserToAddRows = False
        Me.DG_SCDEFECTATAS.AllowUserToDeleteRows = False
        Me.DG_SCDEFECTATAS.AllowUserToResizeRows = False
        Me.DG_SCDEFECTATAS.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_SCDEFECTATAS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG_SCDEFECTATAS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG_SCDEFECTATAS.FilterAndSortEnabled = True
        Me.DG_SCDEFECTATAS.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCDEFECTATAS.Location = New System.Drawing.Point(3, 3)
        Me.DG_SCDEFECTATAS.Name = "DG_SCDEFECTATAS"
        Me.DG_SCDEFECTATAS.ReadOnly = True
        Me.DG_SCDEFECTATAS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DG_SCDEFECTATAS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DG_SCDEFECTATAS.Size = New System.Drawing.Size(1506, 249)
        Me.DG_SCDEFECTATAS.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCDEFECTATAS.TabIndex = 10
        '
        'TabPage6
        '
        Me.TabPage6.BackColor = System.Drawing.Color.DarkGray
        Me.TabPage6.Controls.Add(Me.DG_SCReject)
        Me.TabPage6.Location = New System.Drawing.Point(4, 38)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(1512, 425)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Stock Card Reject"
        '
        'DG_SCReject
        '
        Me.DG_SCReject.AllowUserToAddRows = False
        Me.DG_SCReject.AllowUserToDeleteRows = False
        Me.DG_SCReject.AllowUserToResizeRows = False
        Me.DG_SCReject.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_SCReject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG_SCReject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG_SCReject.FilterAndSortEnabled = True
        Me.DG_SCReject.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCReject.Location = New System.Drawing.Point(0, 0)
        Me.DG_SCReject.Name = "DG_SCReject"
        Me.DG_SCReject.ReadOnly = True
        Me.DG_SCReject.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DG_SCReject.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DG_SCReject.Size = New System.Drawing.Size(1512, 425)
        Me.DG_SCReject.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCReject.TabIndex = 10
        '
        'TabPage7
        '
        Me.TabPage7.BackColor = System.Drawing.Color.DarkGray
        Me.TabPage7.Controls.Add(Me.DG_SCOthers)
        Me.TabPage7.Location = New System.Drawing.Point(4, 38)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(1512, 425)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "Stock Card Others"
        '
        'DG_SCOthers
        '
        Me.DG_SCOthers.AllowUserToAddRows = False
        Me.DG_SCOthers.AllowUserToDeleteRows = False
        Me.DG_SCOthers.AllowUserToResizeRows = False
        Me.DG_SCOthers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_SCOthers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG_SCOthers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG_SCOthers.FilterAndSortEnabled = True
        Me.DG_SCOthers.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCOthers.Location = New System.Drawing.Point(0, 0)
        Me.DG_SCOthers.Name = "DG_SCOthers"
        Me.DG_SCOthers.ReadOnly = True
        Me.DG_SCOthers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DG_SCOthers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DG_SCOthers.Size = New System.Drawing.Size(1512, 425)
        Me.DG_SCOthers.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DG_SCOthers.TabIndex = 11
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(965, 551)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(253, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 15
        Me.ProgressBar1.Visible = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Green
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(544, 24)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(115, 38)
        Me.Button2.TabIndex = 14
        Me.Button2.Text = "Filter"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(313, 24)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(200, 35)
        Me.DateTimePicker2.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(286, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 29)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "-"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 29)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Date"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(80, 24)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(200, 35)
        Me.DateTimePicker1.TabIndex = 10
        '
        'btn_ExportTrace1
        '
        Me.btn_ExportTrace1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_ExportTrace1.BackColor = System.Drawing.Color.Blue
        Me.btn_ExportTrace1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btn_ExportTrace1.Location = New System.Drawing.Point(1224, 540)
        Me.btn_ExportTrace1.Name = "btn_ExportTrace1"
        Me.btn_ExportTrace1.Size = New System.Drawing.Size(296, 41)
        Me.btn_ExportTrace1.TabIndex = 9
        Me.btn_ExportTrace1.Text = "Export this table to Excel"
        Me.btn_ExportTrace1.UseVisualStyleBackColor = False
        '
        'BackgroundWorker1
        '
        '
        'StockProduction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1528, 572)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "StockProduction"
        Me.Text = "Stock Production"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DG_SCMaterial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DG_SCSA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.DG_SCWIPBAWAH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DG_SCWIPATAS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.DG_SCOHBAWAH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DG_SCOHATAS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.DG_SCDEFECTBAWAH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DG_SCDEFECTATAS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage6.ResumeLayout(False)
        CType(Me.DG_SCReject, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage7.ResumeLayout(False)
        CType(Me.DG_SCOthers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents DG_SCMaterial As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents btn_ExportTrace1 As Button
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Button2 As Button
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents TabPage7 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents DG_SCSA As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents DG_SCReject As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents DG_SCOthers As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents DG_SCWIPATAS As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents DG_SCWIPBAWAH As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents DG_SCOHBAWAH As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents DG_SCOHATAS As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents DG_SCDEFECTBAWAH As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents DG_SCDEFECTATAS As Zuby.ADGV.AdvancedDataGridView
End Class
