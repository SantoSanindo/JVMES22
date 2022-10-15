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
        Me.RibbonTab3 = New System.Windows.Forms.RibbonTab()
        Me.InputStock = New System.Windows.Forms.RibbonPanel()
        Me.InputStockBtn = New System.Windows.Forms.RibbonButton()
        Me.RibbonTab4 = New System.Windows.Forms.RibbonTab()
        Me.RibbonPanel3 = New System.Windows.Forms.RibbonPanel()
        Me.RibbonTab5 = New System.Windows.Forms.RibbonTab()
        Me.RibMasterMaterial = New System.Windows.Forms.RibbonPanel()
        Me.MasterMaterialBtn = New System.Windows.Forms.RibbonButton()
        Me.RibMasterProcess = New System.Windows.Forms.RibbonPanel()
        Me.MasterProcessBtn = New System.Windows.Forms.RibbonButton()
        Me.TabControl1 = New MdiTabControl.TabControl()
        Me.RibbonButton1 = New System.Windows.Forms.RibbonButton()
        Me.RibMasterFinishGood = New System.Windows.Forms.RibbonPanel()
        Me.MasterFinishGoodsBtn = New System.Windows.Forms.RibbonButton()
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
        Me.Ribbon1.RibbonTabFont = New System.Drawing.Font("Trebuchet MS", 9.0!)
        Me.Ribbon1.Size = New System.Drawing.Size(962, 137)
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
        'RibbonTab3
        '
        Me.RibbonTab3.Name = "RibbonTab3"
        Me.RibbonTab3.Panels.Add(Me.InputStock)
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
        'RibbonTab4
        '
        Me.RibbonTab4.Name = "RibbonTab4"
        Me.RibbonTab4.Panels.Add(Me.RibbonPanel3)
        Me.RibbonTab4.Text = "Line Process"
        '
        'RibbonPanel3
        '
        Me.RibbonPanel3.Name = "RibbonPanel3"
        Me.RibbonPanel3.Text = "RibbonPanel3"
        '
        'RibbonTab5
        '
        Me.RibbonTab5.Name = "RibbonTab5"
        Me.RibbonTab5.Panels.Add(Me.RibMasterMaterial)
        Me.RibbonTab5.Panels.Add(Me.RibMasterProcess)
        Me.RibbonTab5.Panels.Add(Me.RibMasterFinishGood)
        Me.RibbonTab5.Text = "Master Data"
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
        'TabControl1
        '
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 137)
        Me.TabControl1.MenuRenderer = Nothing
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.Size = New System.Drawing.Size(962, 415)
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
        'RibMasterFinishGood
        '
        Me.RibMasterFinishGood.Items.Add(Me.MasterFinishGoodsBtn)
        Me.RibMasterFinishGood.Name = "RibMasterFinishGood"
        Me.RibMasterFinishGood.Text = "Master Finish Goods"
        '
        'MasterFinishGoodsBtn
        '
        Me.MasterFinishGoodsBtn.Image = CType(resources.GetObject("MasterFinishGoodsBtn.Image"), System.Drawing.Image)
        Me.MasterFinishGoodsBtn.LargeImage = CType(resources.GetObject("MasterFinishGoodsBtn.LargeImage"), System.Drawing.Image)
        Me.MasterFinishGoodsBtn.Name = "MasterFinishGoodsBtn"
        Me.MasterFinishGoodsBtn.SmallImage = CType(resources.GetObject("MasterFinishGoodsBtn.SmallImage"), System.Drawing.Image)
        '
        'HOME
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(962, 552)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Ribbon1)
        Me.KeyPreview = True
        Me.Name = "HOME"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MES Application"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

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
    Friend WithEvents buttontest As RibbonPanel
    Friend WithEvents RibbonPanel3 As RibbonPanel
    Friend WithEvents RibbonTab5 As RibbonTab
    Friend WithEvents RibMasterMaterial As RibbonPanel
    Friend WithEvents RibbonButton1 As RibbonButton
    Friend WithEvents MasterMaterialBtn As RibbonButton
    Friend WithEvents RibMasterProcess As RibbonPanel
    Friend WithEvents MasterProcessBtn As RibbonButton
    Friend WithEvents RibMasterFinishGood As RibbonPanel
    Friend WithEvents MasterFinishGoodsBtn As RibbonButton
End Class
