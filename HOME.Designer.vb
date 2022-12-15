<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HOME
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HOME))
        Me.RibbonTab1 = New System.Windows.Forms.RibbonTab()
        Me.RibbonPanel1 = New System.Windows.Forms.RibbonPanel()
        Me.RibbonTab2 = New System.Windows.Forms.RibbonTab()
        Me.Ribbon1 = New System.Windows.Forms.Ribbon()
        Me.RibbonSeparator1 = New System.Windows.Forms.RibbonSeparator()
        Me.RibbonSeparator2 = New System.Windows.Forms.RibbonSeparator()
        Me.RibbonButtonLogin = New System.Windows.Forms.RibbonButton()
        Me.RibbonTab3 = New System.Windows.Forms.RibbonTab()
        Me.InputStock = New System.Windows.Forms.RibbonPanel()
        Me.InputStockBtn = New System.Windows.Forms.RibbonButton()
        Me.RibStockMinistore = New System.Windows.Forms.RibbonPanel()
        Me.StockMinistoreBtn = New System.Windows.Forms.RibbonButton()
        Me.RibProductionRequest = New System.Windows.Forms.RibbonPanel()
        Me.ProductionRequestBtn = New System.Windows.Forms.RibbonButton()
        Me.RibReturnStock = New System.Windows.Forms.RibbonPanel()
        Me.ReturnStockBtn = New System.Windows.Forms.RibbonButton()
        Me.RibbonTab4 = New System.Windows.Forms.RibbonTab()
        Me.RibStockProd = New System.Windows.Forms.RibbonPanel()
        Me.StockProdBtn = New System.Windows.Forms.RibbonButton()
        Me.RibPO = New System.Windows.Forms.RibbonPanel()
        Me.MainPOSubPOBtn = New System.Windows.Forms.RibbonButton()
        Me.RibProduction = New System.Windows.Forms.RibbonPanel()
        Me.ProductionBtn = New System.Windows.Forms.RibbonButton()
        Me.RibbonTab5 = New System.Windows.Forms.RibbonTab()
        Me.RibMasterLine = New System.Windows.Forms.RibbonPanel()
        Me.MasterLineBtn = New System.Windows.Forms.RibbonButton()
        Me.RibMasterMaterial = New System.Windows.Forms.RibbonPanel()
        Me.MasterMaterialBtn = New System.Windows.Forms.RibbonButton()
        Me.RibMasterProcess = New System.Windows.Forms.RibbonPanel()
        Me.MasterProcessBtn = New System.Windows.Forms.RibbonButton()
        Me.RibMasterFinishGoods = New System.Windows.Forms.RibbonPanel()
        Me.MasterFinishGoodsBtn = New System.Windows.Forms.RibbonButton()
        Me.RibMaterialUsageFinishGoods = New System.Windows.Forms.RibbonPanel()
        Me.MaterialUsageFinishGoodsBtn = New System.Windows.Forms.RibbonButton()
        Me.RibMasterProcessFlow = New System.Windows.Forms.RibbonPanel()
        Me.MasterProcessFlowBtn = New System.Windows.Forms.RibbonButton()
        Me.RibProcessFlowMaterialUsage = New System.Windows.Forms.RibbonPanel()
        Me.ProcessFlowMaterialUsageBtn = New System.Windows.Forms.RibbonButton()
        Me.RibUsers = New System.Windows.Forms.RibbonPanel()
        Me.UsersBtn = New System.Windows.Forms.RibbonButton()
        Me.RibbonPanel3 = New System.Windows.Forms.RibbonPanel()
        Me.TabControl1 = New MdiTabControl.TabControl()
        Me.RibbonButton1 = New System.Windows.Forms.RibbonButton()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.LoginUser = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RibbonTab1
        '
        Me.RibbonTab1.Name = "RibbonTab1"
        Me.RibbonTab1.Panels.Add(Me.RibbonPanel1)
        Me.RibbonTab1.Text = "Mini Store"
        '
        'RibbonPanel1
        '
        Me.RibbonPanel1.Name = "RibbonPanel1"
        Me.RibbonPanel1.Text = "RibbonPanel1"
        '
        'RibbonTab2
        '
        Me.RibbonTab2.Name = "RibbonTab2"
        Me.RibbonTab2.Text = "Line Process"
        '
        'Ribbon1
        '
        Me.Ribbon1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Ribbon1.Location = New System.Drawing.Point(0, 0)
        Me.Ribbon1.Minimized = False
        Me.Ribbon1.Name = "Ribbon1"
        '
        '
        '
        Me.Ribbon1.OrbDropDown.BorderRoundness = 8
        Me.Ribbon1.OrbDropDown.Location = New System.Drawing.Point(0, 0)
        Me.Ribbon1.OrbDropDown.MenuItems.Add(Me.RibbonSeparator1)
        Me.Ribbon1.OrbDropDown.MenuItems.Add(Me.RibbonSeparator2)
        Me.Ribbon1.OrbDropDown.Name = ""
        Me.Ribbon1.OrbDropDown.Size = New System.Drawing.Size(527, 78)
        Me.Ribbon1.OrbDropDown.TabIndex = 0
        Me.Ribbon1.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2010
        Me.Ribbon1.OrbText = ""
        Me.Ribbon1.OrbVisible = False
        '
        '
        '
        Me.Ribbon1.QuickAccessToolbar.DropDownButtonItems.Add(Me.RibbonButtonLogin)
        Me.Ribbon1.RibbonTabFont = New System.Drawing.Font("Trebuchet MS", 9.0!)
        Me.Ribbon1.Size = New System.Drawing.Size(1236, 137)
        Me.Ribbon1.TabIndex = 0
        Me.Ribbon1.Tabs.Add(Me.RibbonTab3)
        Me.Ribbon1.Tabs.Add(Me.RibbonTab4)
        Me.Ribbon1.Tabs.Add(Me.RibbonTab5)
        Me.Ribbon1.TabSpacing = 3
        Me.Ribbon1.Text = "Ribbon1"
        '
        'RibbonSeparator1
        '
        Me.RibbonSeparator1.Name = "RibbonSeparator1"
        '
        'RibbonSeparator2
        '
        Me.RibbonSeparator2.Name = "RibbonSeparator2"
        '
        'RibbonButtonLogin
        '
        Me.RibbonButtonLogin.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.RibbonButtonLogin.Image = CType(resources.GetObject("RibbonButtonLogin.Image"), System.Drawing.Image)
        Me.RibbonButtonLogin.LargeImage = CType(resources.GetObject("RibbonButtonLogin.LargeImage"), System.Drawing.Image)
        Me.RibbonButtonLogin.Name = "RibbonButtonLogin"
        Me.RibbonButtonLogin.SmallImage = CType(resources.GetObject("RibbonButtonLogin.SmallImage"), System.Drawing.Image)
        Me.RibbonButtonLogin.Text = "Login"
        '
        'RibbonTab3
        '
        Me.RibbonTab3.Name = "RibbonTab3"
        Me.RibbonTab3.Panels.Add(Me.InputStock)
        Me.RibbonTab3.Panels.Add(Me.RibStockMinistore)
        Me.RibbonTab3.Panels.Add(Me.RibProductionRequest)
        Me.RibbonTab3.Panels.Add(Me.RibReturnStock)
        Me.RibbonTab3.Text = "Mini Store"
        '
        'InputStock
        '
        Me.InputStock.Items.Add(Me.InputStockBtn)
        Me.InputStock.Name = "InputStock"
        Me.InputStock.Text = "Input Stock  "
        '
        'InputStockBtn
        '
        Me.InputStockBtn.Image = CType(resources.GetObject("InputStockBtn.Image"), System.Drawing.Image)
        Me.InputStockBtn.LargeImage = CType(resources.GetObject("InputStockBtn.LargeImage"), System.Drawing.Image)
        Me.InputStockBtn.MinimumSize = New System.Drawing.Size(70, 0)
        Me.InputStockBtn.Name = "InputStockBtn"
        Me.InputStockBtn.SmallImage = CType(resources.GetObject("InputStockBtn.SmallImage"), System.Drawing.Image)
        '
        'RibStockMinistore
        '
        Me.RibStockMinistore.Items.Add(Me.StockMinistoreBtn)
        Me.RibStockMinistore.Name = "RibStockMinistore"
        Me.RibStockMinistore.Text = "Stock Ministore"
        '
        'StockMinistoreBtn
        '
        Me.StockMinistoreBtn.Image = CType(resources.GetObject("StockMinistoreBtn.Image"), System.Drawing.Image)
        Me.StockMinistoreBtn.LargeImage = CType(resources.GetObject("StockMinistoreBtn.LargeImage"), System.Drawing.Image)
        Me.StockMinistoreBtn.Name = "StockMinistoreBtn"
        Me.StockMinistoreBtn.SmallImage = CType(resources.GetObject("StockMinistoreBtn.SmallImage"), System.Drawing.Image)
        '
        'RibProductionRequest
        '
        Me.RibProductionRequest.Items.Add(Me.ProductionRequestBtn)
        Me.RibProductionRequest.Name = "RibProductionRequest"
        Me.RibProductionRequest.Text = "Production Request"
        '
        'ProductionRequestBtn
        '
        Me.ProductionRequestBtn.Image = CType(resources.GetObject("ProductionRequestBtn.Image"), System.Drawing.Image)
        Me.ProductionRequestBtn.LargeImage = CType(resources.GetObject("ProductionRequestBtn.LargeImage"), System.Drawing.Image)
        Me.ProductionRequestBtn.Name = "ProductionRequestBtn"
        Me.ProductionRequestBtn.SmallImage = CType(resources.GetObject("ProductionRequestBtn.SmallImage"), System.Drawing.Image)
        '
        'RibReturnStock
        '
        Me.RibReturnStock.Items.Add(Me.ReturnStockBtn)
        Me.RibReturnStock.Name = "RibReturnStock"
        Me.RibReturnStock.Text = "Return Stock"
        '
        'ReturnStockBtn
        '
        Me.ReturnStockBtn.Image = CType(resources.GetObject("ReturnStockBtn.Image"), System.Drawing.Image)
        Me.ReturnStockBtn.LargeImage = CType(resources.GetObject("ReturnStockBtn.LargeImage"), System.Drawing.Image)
        Me.ReturnStockBtn.Name = "ReturnStockBtn"
        Me.ReturnStockBtn.SmallImage = CType(resources.GetObject("ReturnStockBtn.SmallImage"), System.Drawing.Image)
        '
        'RibbonTab4
        '
        Me.RibbonTab4.Name = "RibbonTab4"
        Me.RibbonTab4.Panels.Add(Me.RibStockProd)
        Me.RibbonTab4.Panels.Add(Me.RibPO)
        Me.RibbonTab4.Panels.Add(Me.RibProduction)
        Me.RibbonTab4.Text = "Line Process"
        '
        'RibStockProd
        '
        Me.RibStockProd.Items.Add(Me.StockProdBtn)
        Me.RibStockProd.Name = "RibStockProd"
        Me.RibStockProd.Text = "Transit Area"
        '
        'StockProdBtn
        '
        Me.StockProdBtn.Image = CType(resources.GetObject("StockProdBtn.Image"), System.Drawing.Image)
        Me.StockProdBtn.LargeImage = CType(resources.GetObject("StockProdBtn.LargeImage"), System.Drawing.Image)
        Me.StockProdBtn.Name = "StockProdBtn"
        Me.StockProdBtn.SmallImage = CType(resources.GetObject("StockProdBtn.SmallImage"), System.Drawing.Image)
        '
        'RibPO
        '
        Me.RibPO.Items.Add(Me.MainPOSubPOBtn)
        Me.RibPO.Name = "RibPO"
        Me.RibPO.Text = "Main PO / Sub PO / Sub-sub PO"
        '
        'MainPOSubPOBtn
        '
        Me.MainPOSubPOBtn.Image = CType(resources.GetObject("MainPOSubPOBtn.Image"), System.Drawing.Image)
        Me.MainPOSubPOBtn.LargeImage = CType(resources.GetObject("MainPOSubPOBtn.LargeImage"), System.Drawing.Image)
        Me.MainPOSubPOBtn.Name = "MainPOSubPOBtn"
        Me.MainPOSubPOBtn.SmallImage = CType(resources.GetObject("MainPOSubPOBtn.SmallImage"), System.Drawing.Image)
        '
        'RibProduction
        '
        Me.RibProduction.Items.Add(Me.ProductionBtn)
        Me.RibProduction.Name = "RibProduction"
        Me.RibProduction.Text = "Production"
        '
        'ProductionBtn
        '
        Me.ProductionBtn.Image = CType(resources.GetObject("ProductionBtn.Image"), System.Drawing.Image)
        Me.ProductionBtn.LargeImage = CType(resources.GetObject("ProductionBtn.LargeImage"), System.Drawing.Image)
        Me.ProductionBtn.Name = "ProductionBtn"
        Me.ProductionBtn.SmallImage = CType(resources.GetObject("ProductionBtn.SmallImage"), System.Drawing.Image)
        '
        'RibbonTab5
        '
        Me.RibbonTab5.Name = "RibbonTab5"
        Me.RibbonTab5.Panels.Add(Me.RibMasterLine)
        Me.RibbonTab5.Panels.Add(Me.RibMasterMaterial)
        Me.RibbonTab5.Panels.Add(Me.RibMasterProcess)
        Me.RibbonTab5.Panels.Add(Me.RibMasterFinishGoods)
        Me.RibbonTab5.Panels.Add(Me.RibMaterialUsageFinishGoods)
        Me.RibbonTab5.Panels.Add(Me.RibMasterProcessFlow)
        Me.RibbonTab5.Panels.Add(Me.RibProcessFlowMaterialUsage)
        Me.RibbonTab5.Panels.Add(Me.RibUsers)
        Me.RibbonTab5.Text = "Master Data"
        '
        'RibMasterLine
        '
        Me.RibMasterLine.Items.Add(Me.MasterLineBtn)
        Me.RibMasterLine.Name = "RibMasterLine"
        Me.RibMasterLine.Text = "Master Line"
        '
        'MasterLineBtn
        '
        Me.MasterLineBtn.Image = CType(resources.GetObject("MasterLineBtn.Image"), System.Drawing.Image)
        Me.MasterLineBtn.LargeImage = CType(resources.GetObject("MasterLineBtn.LargeImage"), System.Drawing.Image)
        Me.MasterLineBtn.Name = "MasterLineBtn"
        Me.MasterLineBtn.SmallImage = CType(resources.GetObject("MasterLineBtn.SmallImage"), System.Drawing.Image)
        '
        'RibMasterMaterial
        '
        Me.RibMasterMaterial.Items.Add(Me.MasterMaterialBtn)
        Me.RibMasterMaterial.Name = "RibMasterMaterial"
        Me.RibMasterMaterial.Text = "Master Material"
        '
        'MasterMaterialBtn
        '
        Me.MasterMaterialBtn.Image = CType(resources.GetObject("MasterMaterialBtn.Image"), System.Drawing.Image)
        Me.MasterMaterialBtn.LargeImage = CType(resources.GetObject("MasterMaterialBtn.LargeImage"), System.Drawing.Image)
        Me.MasterMaterialBtn.Name = "MasterMaterialBtn"
        Me.MasterMaterialBtn.SmallImage = CType(resources.GetObject("MasterMaterialBtn.SmallImage"), System.Drawing.Image)
        '
        'RibMasterProcess
        '
        Me.RibMasterProcess.Items.Add(Me.MasterProcessBtn)
        Me.RibMasterProcess.Name = "RibMasterProcess"
        Me.RibMasterProcess.Text = "Master Process"
        '
        'MasterProcessBtn
        '
        Me.MasterProcessBtn.Image = CType(resources.GetObject("MasterProcessBtn.Image"), System.Drawing.Image)
        Me.MasterProcessBtn.LargeImage = CType(resources.GetObject("MasterProcessBtn.LargeImage"), System.Drawing.Image)
        Me.MasterProcessBtn.Name = "MasterProcessBtn"
        Me.MasterProcessBtn.SmallImage = CType(resources.GetObject("MasterProcessBtn.SmallImage"), System.Drawing.Image)
        '
        'RibMasterFinishGoods
        '
        Me.RibMasterFinishGoods.Items.Add(Me.MasterFinishGoodsBtn)
        Me.RibMasterFinishGoods.Name = "RibMasterFinishGoods"
        Me.RibMasterFinishGoods.Text = "Master Finish Goods"
        '
        'MasterFinishGoodsBtn
        '
        Me.MasterFinishGoodsBtn.Image = CType(resources.GetObject("MasterFinishGoodsBtn.Image"), System.Drawing.Image)
        Me.MasterFinishGoodsBtn.LargeImage = CType(resources.GetObject("MasterFinishGoodsBtn.LargeImage"), System.Drawing.Image)
        Me.MasterFinishGoodsBtn.Name = "MasterFinishGoodsBtn"
        Me.MasterFinishGoodsBtn.SmallImage = CType(resources.GetObject("MasterFinishGoodsBtn.SmallImage"), System.Drawing.Image)
        '
        'RibMaterialUsageFinishGoods
        '
        Me.RibMaterialUsageFinishGoods.Items.Add(Me.MaterialUsageFinishGoodsBtn)
        Me.RibMaterialUsageFinishGoods.Name = "RibMaterialUsageFinishGoods"
        Me.RibMaterialUsageFinishGoods.Text = "Material Usage Finish Goods"
        '
        'MaterialUsageFinishGoodsBtn
        '
        Me.MaterialUsageFinishGoodsBtn.Image = CType(resources.GetObject("MaterialUsageFinishGoodsBtn.Image"), System.Drawing.Image)
        Me.MaterialUsageFinishGoodsBtn.LargeImage = CType(resources.GetObject("MaterialUsageFinishGoodsBtn.LargeImage"), System.Drawing.Image)
        Me.MaterialUsageFinishGoodsBtn.Name = "MaterialUsageFinishGoodsBtn"
        Me.MaterialUsageFinishGoodsBtn.SmallImage = CType(resources.GetObject("MaterialUsageFinishGoodsBtn.SmallImage"), System.Drawing.Image)
        '
        'RibMasterProcessFlow
        '
        Me.RibMasterProcessFlow.Items.Add(Me.MasterProcessFlowBtn)
        Me.RibMasterProcessFlow.Name = "RibMasterProcessFlow"
        Me.RibMasterProcessFlow.Text = "Master Process Flow"
        '
        'MasterProcessFlowBtn
        '
        Me.MasterProcessFlowBtn.Image = CType(resources.GetObject("MasterProcessFlowBtn.Image"), System.Drawing.Image)
        Me.MasterProcessFlowBtn.LargeImage = CType(resources.GetObject("MasterProcessFlowBtn.LargeImage"), System.Drawing.Image)
        Me.MasterProcessFlowBtn.Name = "MasterProcessFlowBtn"
        Me.MasterProcessFlowBtn.SmallImage = CType(resources.GetObject("MasterProcessFlowBtn.SmallImage"), System.Drawing.Image)
        '
        'RibProcessFlowMaterialUsage
        '
        Me.RibProcessFlowMaterialUsage.Items.Add(Me.ProcessFlowMaterialUsageBtn)
        Me.RibProcessFlowMaterialUsage.Name = "RibProcessFlowMaterialUsage"
        Me.RibProcessFlowMaterialUsage.Text = "Process Flow Material Usage"
        '
        'ProcessFlowMaterialUsageBtn
        '
        Me.ProcessFlowMaterialUsageBtn.Image = CType(resources.GetObject("ProcessFlowMaterialUsageBtn.Image"), System.Drawing.Image)
        Me.ProcessFlowMaterialUsageBtn.LargeImage = CType(resources.GetObject("ProcessFlowMaterialUsageBtn.LargeImage"), System.Drawing.Image)
        Me.ProcessFlowMaterialUsageBtn.Name = "ProcessFlowMaterialUsageBtn"
        Me.ProcessFlowMaterialUsageBtn.SmallImage = CType(resources.GetObject("ProcessFlowMaterialUsageBtn.SmallImage"), System.Drawing.Image)
        '
        'RibUsers
        '
        Me.RibUsers.Items.Add(Me.UsersBtn)
        Me.RibUsers.Name = "RibUsers"
        Me.RibUsers.Text = "Master Users"
        '
        'UsersBtn
        '
        Me.UsersBtn.Image = CType(resources.GetObject("UsersBtn.Image"), System.Drawing.Image)
        Me.UsersBtn.LargeImage = CType(resources.GetObject("UsersBtn.LargeImage"), System.Drawing.Image)
        Me.UsersBtn.Name = "UsersBtn"
        Me.UsersBtn.SmallImage = CType(resources.GetObject("UsersBtn.SmallImage"), System.Drawing.Image)
        '
        'RibbonPanel3
        '
        Me.RibbonPanel3.Name = "RibbonPanel3"
        Me.RibbonPanel3.Text = "RibbonPanel3"
        '
        'TabControl1
        '
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 137)
        Me.TabControl1.MenuRenderer = Nothing
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.Size = New System.Drawing.Size(1236, 415)
        Me.TabControl1.TabCloseButtonImage = Nothing
        Me.TabControl1.TabCloseButtonImageDisabled = Nothing
        Me.TabControl1.TabCloseButtonImageHot = Nothing
        Me.TabControl1.TabIndex = 1
        '
        'RibbonButton1
        '
        Me.RibbonButton1.Image = CType(resources.GetObject("RibbonButton1.Image"), System.Drawing.Image)
        Me.RibbonButton1.LargeImage = CType(resources.GetObject("RibbonButton1.LargeImage"), System.Drawing.Image)
        Me.RibbonButton1.Name = "RibbonButton1"
        Me.RibbonButton1.SmallImage = CType(resources.GetObject("RibbonButton1.SmallImage"), System.Drawing.Image)
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.LoginUser})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 530)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1236, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(69, 17)
        Me.ToolStripStatusLabel1.Text = "Login User :"
        '
        'LoginUser
        '
        Me.LoginUser.Name = "LoginUser"
        Me.LoginUser.Size = New System.Drawing.Size(36, 17)
        Me.LoginUser.Text = "None"
        '
        'HOME
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1236, 552)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Ribbon1)
        Me.KeyPreview = True
        Me.Name = "HOME"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MES Application"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonTab1 As RibbonTab
    Friend WithEvents RibbonPanel1 As RibbonPanel
    Friend WithEvents RibbonTab2 As RibbonTab
    Friend WithEvents Ribbon1 As Ribbon
    Friend WithEvents RibbonTab3 As RibbonTab
    Friend WithEvents RibbonSeparator1 As RibbonSeparator
    Friend WithEvents RibbonSeparator2 As RibbonSeparator
    Friend WithEvents RibbonTab4 As RibbonTab
    Friend WithEvents InputStock As RibbonPanel
    Friend WithEvents InputStockBtn As RibbonButton
    Friend WithEvents TabControl1 As MdiTabControl.TabControl
    '<<<<<<< HEAD
    Friend WithEvents RibbonPanel2 As RibbonPanel
    Friend WithEvents RibbonPanel3 As RibbonPanel
    Friend WithEvents RibbonButton1 As RibbonButton
    '=======
    Friend WithEvents buttontest As RibbonPanel
    Friend WithEvents RibPO As RibbonPanel
    Friend WithEvents RibbonTab5 As RibbonTab
    Friend WithEvents RibMasterMaterial As RibbonPanel
    'Friend WithEvents RibbonButton1 As RibbonButton
    Friend WithEvents MasterMaterialBtn As RibbonButton
    Friend WithEvents RibMasterProcess As RibbonPanel
    Friend WithEvents MasterProcessBtn As RibbonButton
    Friend WithEvents RibMaterialUsageFinishGoods As RibbonPanel
    Friend WithEvents MaterialUsageFinishGoodsBtn As RibbonButton
    Friend WithEvents RibMasterProcessFlow As RibbonPanel
    Friend WithEvents MasterProcessFlowBtn As RibbonButton
    Friend WithEvents RibProcessFlowMaterialUsage As RibbonPanel
    Friend WithEvents ProcessFlowMaterialUsageBtn As RibbonButton
    Friend WithEvents MainPOSubPOBtn As RibbonButton
    Friend WithEvents RibProductionRequest As RibbonPanel
    Friend WithEvents ProductionRequestBtn As RibbonButton
    Friend WithEvents RibMasterFinishGoods As RibbonPanel
    Friend WithEvents MasterFinishGoodsBtn As RibbonButton
    Friend WithEvents RibStockMinistore As RibbonPanel
    Friend WithEvents RibProduction As RibbonPanel
    Friend WithEvents ProductionBtn As RibbonButton
    Friend WithEvents RibStockProd As RibbonPanel
    Friend WithEvents StockProdBtn As RibbonButton
    Friend WithEvents StockMinistoreBtn As RibbonButton
    Friend WithEvents RibUsers As RibbonPanel
    Friend WithEvents UsersBtn As RibbonButton
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents LoginUser As ToolStripStatusLabel
    Friend WithEvents RibbonButtonLogin As RibbonButton
    Friend WithEvents RibMasterLine As RibbonPanel
    Friend WithEvents MasterLineBtn As RibbonButton
    Friend WithEvents RibReturnStock As RibbonPanel
    Friend WithEvents ReturnStockBtn As RibbonButton
    '>>>>>>> Arif
End Class
