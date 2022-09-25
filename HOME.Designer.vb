<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HOME
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
        Me.RibbonPanel2 = New System.Windows.Forms.RibbonPanel()
        Me.RibbonTab4 = New System.Windows.Forms.RibbonTab()
        Me.TabControl1 = New MdiTabControl.TabControl()
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
        Me.Ribbon1.TabsMargin = New System.Windows.Forms.Padding(6, 26, 20, 0)
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
        Me.RibbonTab3.Panels.Add(Me.RibbonPanel2)
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
        'RibbonPanel2
        '
        Me.RibbonPanel2.Name = "RibbonPanel2"
        Me.RibbonPanel2.Text = "RibbonPanel2"
        '
        'RibbonTab4
        '
        Me.RibbonTab4.Name = "RibbonTab4"
        Me.RibbonTab4.Text = "Line Process"
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
    Friend WithEvents RibbonPanel2 As RibbonPanel
End Class
