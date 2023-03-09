Imports System.IO
Public Class HOME
    Private Sub HOME_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = "MES Application v " & Application.ProductVersion
            If read_notepad("\\192.168.0.254\Updater\MES App\_Version\Version.txt") <> Application.ProductVersion Then

                Dim result As DialogResult = RJMessageBox.Show(
                              "Are you going to update to V " & read_notepad("\\192.168.0.254\Updater\MES App\_Version\Version.txt"), "Newer Version is available.",
                              MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then
                    Process.Start("Updater.exe")
                    Me.Close()
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


        buka_printer()

        TabControl1.TabPages.Add(FormLogin)
        TabControl1.TabPages(FormLogin).Select()


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
        TabControl1.TabPages.Clear()
    End Sub

    ' -----------------------Start Menu Master Data-------------------------'
    Private Sub BtnUsers(sender As Object, e As EventArgs) Handles UsersBtn.Click
        If globVar.hakAkses = "ADMIN" Then
            Users.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(Users)
            TabControl1.TabPages(Users).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnMasterMaterial(sender As Object, e As EventArgs) Handles MasterMaterialBtn.Click
        If globVar.hakAkses = "ADMIN" Then
            MasterMaterial.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MasterMaterial)
            TabControl1.TabPages(MasterMaterial).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnMasterProcess(sender As Object, e As EventArgs) Handles MasterProcessBtn.Click
        If globVar.hakAkses = "ADMIN" Then
            MasterProcess.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MasterProcess)
            TabControl1.TabPages(MasterProcess).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnMasterProcessFlow(sender As Object, e As EventArgs) Handles MasterProcessFlowBtn.Click
        If globVar.hakAkses = "ADMIN" Then
            MasterProcessFlow.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MasterProcessFlow)
            TabControl1.TabPages(MasterProcessFlow).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnMasterFinishGoods(sender As Object, e As EventArgs) Handles MasterFinishGoodsBtn.Click
        If globVar.hakAkses = "ADMIN" Then
            MasterFinishGoods.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MasterFinishGoods)
            TabControl1.TabPages(MasterFinishGoods).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub btnMaterialUsageFinishGoods(sender As Object, e As EventArgs) Handles MaterialUsageFinishGoodsBtn.Click
        If globVar.hakAkses = "ADMIN" Then
            MaterialUsageFinishGoods.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MaterialUsageFinishGoods)
            TabControl1.TabPages(MaterialUsageFinishGoods).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub btnProcessFlowMaterialUsage(sender As Object, e As EventArgs) Handles ProcessFlowMaterialUsageBtn.Click
        If globVar.hakAkses = "ADMIN" Then
            ProcessFlowMaterialUsage.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(ProcessFlowMaterialUsage)
            TabControl1.TabPages(ProcessFlowMaterialUsage).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub BtnMasterLine(sender As Object, e As EventArgs) Handles MasterLineBtn.Click
        If globVar.hakAkses = "ADMIN" Then
            MasterLine.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MasterLine)
            TabControl1.TabPages(MasterLine).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' -----------------------End Menu Master Data--------------------------'

    ' -----------------------Start Menu Mini Store-------------------------'
    Private Sub btnInputStock(sender As Object, e As EventArgs) Handles InputStockBtn.Click
        If globVar.hakAkses = "ADMIN" Or globVar.hakAkses = "MINISTORE" Then
            FormInputStock.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(FormInputStock)
            TabControl1.TabPages(FormInputStock).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub btnReturnStock(sender As Object, e As EventArgs) Handles ReturnStockBtn.Click
        If globVar.hakAkses = "ADMIN" Or globVar.hakAkses = "MINISTORE" Then
            FormReturnStock.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(FormReturnStock)
            TabControl1.TabPages(FormReturnStock).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnProductionRequest(sender As Object, e As EventArgs) Handles ProductionRequestBtn.Click
        If globVar.hakAkses = "ADMIN" Or globVar.hakAkses = "MINISTORE" Then
            ProductionRequest.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(ProductionRequest)
            TabControl1.TabPages(ProductionRequest).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub BtnStockMinistore(sender As Object, e As EventArgs) Handles StockMinistoreBtn.Click
        If globVar.hakAkses = "ADMIN" Or globVar.hakAkses = "MINISTORE" Then
            StockMinistore.Close()
            TabControl1.TabPages.Clear()
            'TabControl1.TabPages.Add(StockMinistore)
            'TabControl1.TabPages(StockMinistore).Select()
            TabControl1.TabPages.Add(StockMinistore)
            TabControl1.TabPages(StockMinistore).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnReceiveReturnMaterial(sender As Object, e As EventArgs) Handles ReceiveReturnMaterialBtn.Click
        If globVar.hakAkses = "ADMIN" Or globVar.hakAkses = "MINISTORE" Then
            ReceiveReturnMaterial.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(ReceiveReturnMaterial)
            TabControl1.TabPages(ReceiveReturnMaterial).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' -----------------------End Menu Mini Store---------------------------'

    ' -----------------------Start Menu Production-------------------------'

    Private Sub btnMainPoSubPO(sender As Object, e As EventArgs) Handles MainPOSubPOBtn.Click
        If globVar.hakAkses = "ADMIN" Or globVar.hakAkses = "PRODUCTION" Then
            MainPOSubPO.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MainPOSubPO)
            TabControl1.TabPages(MainPOSubPO).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub BtnProduction(sender As Object, e As EventArgs) Handles ProductionBtn.Click
        If globVar.hakAkses = "ADMIN" Or globVar.hakAkses = "PRODUCTION" Then
            Production.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(Production)
            TabControl1.TabPages(Production).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnStockProd(sender As Object, e As EventArgs) Handles StockProdBtn.Click
        If globVar.hakAkses = "ADMIN" Or globVar.hakAkses = "PRODUCTION" Then
            StockMinistore.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(StockMinistore)
            TabControl1.TabPages(StockMinistore).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnAddChangeOperator(sender As Object, e As EventArgs) Handles AddChangeOperatorBtn.Click
        If globVar.hakAkses = "ADMIN" Or globVar.hakAkses = "PRODUCTION" Then
            AddChangeOperator.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(AddChangeOperator)
            TabControl1.TabPages(AddChangeOperator).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub BtnPrintFlowTicket(sender As Object, e As EventArgs) Handles PrintFlowTicketBtn.Click
        If globVar.hakAkses = "ADMIN" Or globVar.hakAkses = "PRODUCTION" Then
            PrintFlowTicket.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(PrintFlowTicket)
            TabControl1.TabPages(PrintFlowTicket).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnSummaryProduction(sender As Object, e As EventArgs) Handles SummaryProductionBtn.Click
        If globVar.hakAkses = "ADMIN" Or globVar.hakAkses = "PRODUCTION" Then
            Summary.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(Summary)
            TabControl1.TabPages(Summary).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' -----------------------End Menu Production----------------------'

    ' -----------------------Start Menu Setting-------------------------'

    Private Sub RibbonLabelsSettings(sender As Object, e As EventArgs) Handles RibbonButtonPrinterSettings.Click
        If globVar.hakAkses = "ADMIN" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(_PrintingFlowTicket)
            TabControl1.TabPages.Add(_PrintingDefect)
            TabControl1.TabPages.Add(_PrintingSubAssyRawMaterial)
            TabControl1.TabPages.Add(_PrintingWIPOnHold)
            TabControl1.TabPages(_PrintingFlowTicket).Select()
            TabControl1.TabPages(_PrintingDefect).Select()
            TabControl1.TabPages(_PrintingSubAssyRawMaterial).Select()
            TabControl1.TabPages(_PrintingWIPOnHold).Select()
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
        If globVar.hakAkses = "ADMIN" Or globVar.hakAkses = "FGA" Then
            FGA.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(FGA)
            TabControl1.TabPages(FGA).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnResultProduction(sender As Object, e As EventArgs) Handles ResultProductionBtn.Click
        FormDefective.Close()
        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(FormDefective)
        TabControl1.TabPages(FormDefective).Select()
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
End Class