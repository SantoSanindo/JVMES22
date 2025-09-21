Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class HOME
    Private Sub HOME_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Try
        '    Dim SourcePath As String = "\\192.168.0.254\Updater\MES App\_Updater"
        '    Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(SourcePath, Application.StartupPath, True)
        'Catch ex As Exception
        '    RJMessageBox.Show("Updater Not Found")
        'End Try

        'Try
        '    Me.Text = "MES Application v " & Application.ProductVersion & " - " & Environment.MachineName
        '    If read_notepad("\\192.168.0.254\Updater\MES App\_Version\Version.txt") <> Application.ProductVersion Then

        '        Dim result As DialogResult = RJMessageBox.Show(
        '                      "Are you going to update to V " & read_notepad("\\192.168.0.254\Updater\MES App\_Version\Version.txt"), "Newer Version is available.",
        '                      MessageBoxButtons.YesNo)

        '        If result = DialogResult.Yes Then
        '            Process.Start("Updater.exe")
        '            Me.Close()
        '        End If

        '    End If
        'Catch ex As Exception
        '    RJMessageBox.Show(ex.ToString)
        'End Try

        buka_printer()

        'TabControl1.TabPages.Add(FormLogin)
        'TabControl1.TabPages(FormLogin).Select()

        FormLogin.ShowDialog()

        globVar.PingVersion()

    End Sub

    Public Function read_notepad(filePath As String) As String
        Dim fileContents As String = File.ReadAllText(filePath)
        Return fileContents
    End Function

    Private Sub buka_printer()
        TabControl1.TabPages.Add(_PrintingFlowTicket)
        TabControl1.TabPages.Add(_PrintingDefect)
        TabControl1.TabPages.Add(_PrintingSubAssyRawMaterial)
        TabControl1.TabPages.Add(_PrintingWIPOnHold)
        TabControl1.TabPages.Add(_PrintingNewLabel)
        TabControl1.TabPages.Clear()
    End Sub

    ' -----------------------Start Menu Master Data-------------------------'
    Private Sub BtnUsers(sender As Object, e As EventArgs) Handles UsersBtn.Click
        If globVar.CanAccess(Users.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                Application.DoEvents()
                Users.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(Users)
                TabControl1.TabPages(Users).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try

        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnMasterMaterial(sender As Object, e As EventArgs) Handles MasterMaterialBtn.Click
        If globVar.CanAccess(MasterMaterial.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                MasterMaterial.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(MasterMaterial)
                TabControl1.TabPages(MasterMaterial).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnMasterProcess(sender As Object, e As EventArgs) Handles MasterProcessBtn.Click
        If globVar.CanAccess(MasterProcess.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                MasterProcess.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(MasterProcess)
                TabControl1.TabPages(MasterProcess).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnMasterProcessFlow(sender As Object, e As EventArgs) Handles MasterProcessFlowBtn.Click
        If globVar.CanAccess(MasterProcessFlow.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                MasterProcessFlow.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(MasterProcessFlow)
                TabControl1.TabPages(MasterProcessFlow).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnMasterFinishGoods(sender As Object, e As EventArgs) Handles MasterFinishGoodsBtn.Click
        If globVar.CanAccess(MasterFinishGoods.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                MasterFinishGoods.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(MasterFinishGoods)
                TabControl1.TabPages(MasterFinishGoods).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub btnMaterialUsageFinishGoods(sender As Object, e As EventArgs) Handles MaterialUsageFinishGoodsBtn.Click
        If globVar.CanAccess(MaterialUsageFinishGoods.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                MaterialUsageFinishGoods.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(MaterialUsageFinishGoods)
                TabControl1.TabPages(MaterialUsageFinishGoods).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub btnProcessFlowMaterialUsage(sender As Object, e As EventArgs) Handles ProcessFlowMaterialUsageBtn.Click
        If globVar.CanAccess(ProcessFlowMaterialUsage.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                ProcessFlowMaterialUsage.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(ProcessFlowMaterialUsage)
                TabControl1.TabPages(ProcessFlowMaterialUsage).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub BtnMasterLine(sender As Object, e As EventArgs) Handles MasterLineBtn.Click
        If globVar.CanAccess(MasterLine.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                MasterLine.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(MasterLine)
                TabControl1.TabPages(MasterLine).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnAC(sender As Object, e As EventArgs) Handles ACBtn.Click
        If globVar.CanAccess(AccessControll.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                AccessControll.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(AccessControll)
                TabControl1.TabPages(AccessControll).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnDept(sender As Object, e As EventArgs) Handles DeptBtn.Click
        If globVar.CanAccess(MasterDepartment.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                MasterDepartment.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(MasterDepartment)
                TabControl1.TabPages(MasterDepartment).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnFam(sender As Object, e As EventArgs) Handles FamBtn.Click
        If globVar.CanAccess(MasterFamily.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                MasterFamily.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(MasterFamily)
                TabControl1.TabPages(MasterFamily).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' -----------------------End Menu Master Data--------------------------'

    ' -----------------------Start Menu Mini Store-------------------------'
    Private Sub btnInputStock(sender As Object, e As EventArgs) Handles InputStockBtn.Click
        If globVar.CanAccess(FormInputStock.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                FormInputStock.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(FormInputStock)
                TabControl1.TabPages(FormInputStock).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub btnReturnStock(sender As Object, e As EventArgs) Handles ReturnStockBtn.Click
        If globVar.CanAccess(FormReturnStock.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                FormReturnStock.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(FormReturnStock)
                TabControl1.TabPages(FormReturnStock).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnProductionRequest(sender As Object, e As EventArgs) Handles ProductionRequestBtn.Click
        If globVar.CanAccess(ProductionRequest.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                ProductionRequest.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(ProductionRequest)
                TabControl1.TabPages(ProductionRequest).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub BtnStockMinistore(sender As Object, e As EventArgs) Handles StockMinistoreBtn.Click
        If globVar.CanAccess(StockMinistore.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                StockMinistore.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(StockMinistore)
                TabControl1.TabPages(StockMinistore).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnReceiveReturnMaterial(sender As Object, e As EventArgs) Handles ReceiveReturnMaterialBtn.Click
        If globVar.CanAccess(ReceiveReturnMaterial.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                ReceiveReturnMaterial.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(ReceiveReturnMaterial)
                TabControl1.TabPages(ReceiveReturnMaterial).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnSplitQty(sender As Object, e As EventArgs) Handles SplitQtyBtn.Click
        If globVar.CanAccess(SplitMaterial.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                SplitMaterial.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(SplitMaterial)
                TabControl1.TabPages(SplitMaterial).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' -----------------------End Menu Mini Store---------------------------'

    ' -----------------------Start Menu Production-------------------------'
    Private Sub BtnStockProd(sender As Object, e As EventArgs) Handles StockProdBtn.Click
        If globVar.CanAccess(StockProduction.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                StockProduction.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(StockProduction)
                TabControl1.TabPages(StockProduction).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnMainPoSubPO(sender As Object, e As EventArgs) Handles MainPOSubPOBtn.Click
        If globVar.CanAccess(MainPOSubPO.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                MainPOSubPO.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(MainPOSubPO)
                TabControl1.TabPages(MainPOSubPO).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnAddChangeOperator(sender As Object, e As EventArgs) Handles AddChangeOperatorBtn.Click
        If globVar.CanAccess(AddChangeOperator.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                AddChangeOperator.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(AddChangeOperator)
                TabControl1.TabPages(AddChangeOperator).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnPrintFlowTicket(sender As Object, e As EventArgs) Handles PrintFlowTicketBtn.Click
        If globVar.CanAccess(PrintFlowTicket.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                PrintFlowTicket.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(PrintFlowTicket)
                TabControl1.TabPages(PrintFlowTicket).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnStatusFlowTicket(sender As Object, e As EventArgs) Handles StatusFlowTicket.Click
        Dim _formStatusFlowTicket As New StatusFlowTicket()
        _formStatusFlowTicket.ShowDialog()
    End Sub

    Private Sub BtnProduction(sender As Object, e As EventArgs) Handles ProductionBtn.Click
        If globVar.CanAccess(ProductionV2.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                ProductionV2.Close()
                'TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(ProductionV2)
                TabControl1.TabPages(ProductionV2).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnResultProduction(sender As Object, e As EventArgs) Handles ResultProductionBtn.Click
        If globVar.CanAccess(FormDefectiveV2.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                FormDefectiveV2.Close()
                'TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(FormDefectiveV2)
                TabControl1.TabPages(FormDefectiveV2).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnSummaryProduction(sender As Object, e As EventArgs) Handles SummaryProductionBtn.Click
        If globVar.CanAccess(SummaryV2.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                SummaryV2.Close()
                'TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(SummaryV2)
                TabControl1.TabPages(SummaryV2).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnTraceability(sender As Object, e As EventArgs) Handles TraceabilityBtn.Click
        If globVar.CanAccess(TraceabilityV3.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                TraceabilityV3.Close()
                TabControl1.TabPages.Add(TraceabilityV3)
                TabControl1.TabPages(TraceabilityV3).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnOthers(sender As Object, e As EventArgs) Handles OthersBtn.Click
        If globVar.CanAccess(OthersPart.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                OthersPart.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(OthersPart)
                TabControl1.TabPages(OthersPart).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' -----------------------End Menu Production----------------------'

    ' -----------------------Start Menu Setting-------------------------'

    Private Sub RibbonLabelsSettings(sender As Object, e As EventArgs) Handles RibbonButtonPrinterSettings.Click
        If globVar.username = "admin" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(_PrintingFlowTicket)
            TabControl1.TabPages.Add(_PrintingDefect)
            TabControl1.TabPages.Add(_PrintingSubAssyRawMaterial)
            TabControl1.TabPages.Add(_PrintingWIPOnHold)
            TabControl1.TabPages.Add(_PrintingNewLabel)
            TabControl1.TabPages(_PrintingFlowTicket).Select()
            TabControl1.TabPages(_PrintingDefect).Select()
            TabControl1.TabPages(_PrintingSubAssyRawMaterial).Select()
            TabControl1.TabPages(_PrintingWIPOnHold).Select()
            TabControl1.TabPages(_PrintingNewLabel).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' -----------------------End Menu Setting-------------------------'

    Private Sub RibbonLogin(sender As Object, e As EventArgs) Handles RibbonButtonLogin.Click
        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(FormLogin)
        TabControl1.TabPages(FormLogin).Select()
    End Sub

    Private Sub Login_Btn(sender As Object, e As EventArgs) Handles RibbonTab3.PressedChanged
        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(FormLogin)
        TabControl1.TabPages(FormLogin).Select()
    End Sub


    ' -----------------------Start Menu FGA-------------------------'
    Private Sub BtnFGA(sender As Object, e As EventArgs) Handles FGABtn.Click
        If globVar.CanAccess(FGA.menu) Then
            Me.Cursor = Cursors.WaitCursor
            Try
                FGA.Close()
                TabControl1.TabPages.Clear()
                TabControl1.TabPages.Add(FGA)
                TabControl1.TabPages(FGA).Select()
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    ' -----------------------End Menu FGA-------------------------'

    ' -----------------------Start Menu Drop Down-------------------------'
    Private Sub Btn_login_dropdown(sender As Object, e As EventArgs) Handles Login_DropDown.Click
        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(FormLogin)
        TabControl1.TabPages(FormLogin).Select()
    End Sub
    ' -----------------------End Menu Drop Down-------------------------'

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub RibbonButton2_Click(sender As Object, e As EventArgs) Handles RibbonButton2.Click
        Dim filePath As String = Application.StartupPath & "\Help\Help.pdf"

        Process.Start(filePath)
    End Sub

End Class