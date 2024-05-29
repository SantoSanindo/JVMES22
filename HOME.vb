Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class HOME
    Private Sub HOME_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Dim SourcePath As String = "\\192.168.0.254\Updater\MES App\_Updater"
            Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(SourcePath, Application.StartupPath, True)
        Catch ex As Exception
            RJMessageBox.Show("Updater Not Found")
        End Try

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
            RJMessageBox.Show(ex.ToString)
        End Try

        buka_printer()

        TabControl1.TabPages.Add(FormLogin)
        TabControl1.TabPages(FormLogin).Select()

        Dim hostName As String = Dns.GetHostName()
        Dim ipAddresses As IPAddress() = Dns.GetHostAddresses(hostName)

        Dim ipAdd As String

        For Each ip As IPAddress In ipAddresses
            If ip.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                If ip.ToString().Contains("192.168.0") Then
                    ipAdd = ip.ToString()
                End If
            End If
        Next

        Try
            Dim sqlInsertMasterLine As String = "
            IF NOT EXISTS (SELECT 1 FROM LIST_CONNECTION WHERE COMPUTER_NAME = '" & hostName & "' AND IP_ADDRESS = '" & ipAdd & "')
                BEGIN
                    INSERT INTO LIST_CONNECTION (COMPUTER_NAME, IP_ADDRESS, STATUS)
                    VALUES ('" & hostName & "', '" & ipAdd & "','1')
                END
            ELSE
                BEGIN
                    RAISERROR('Data already exists', 16, 1)
                END"

            Dim cmdInsertMasterLine = New SqlCommand(sqlInsertMasterLine, Database.koneksi)
            cmdInsertMasterLine.ExecuteNonQuery()
        Catch ex As Exception

        End Try

        Dim sql As String = "select ip_address from LIST_CONNECTION where server=1"
        Dim dtServer As DataTable = Database.GetData(sql)

        Dim pingSender As New Ping()
        Try
            Dim reply As PingReply = pingSender.Send(dtServer.Rows(0).Item("ip_address").ToString)

            If reply.Status = IPStatus.Success Then
                Dim SqlUpdate As String = "UPDATE LIST_CONNECTION SET status=1, ping='" & reply.RoundtripTime & "',LAST_UPDATE=getdate() FROM LIST_CONNECTION WHERE COMPUTER_NAME='" & hostName & "' and IP_ADDRESS='" & ipAdd & "'"
                Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                cmdUpdate.ExecuteNonQuery()
            Else
                Dim SqlUpdate As String = "UPDATE LIST_CONNECTION SET status=0, ping is null,LAST_UPDATE=getdate() FROM LIST_CONNECTION WHERE COMPUTER_NAME='" & hostName & "' and IP_ADDRESS='" & ipAdd & "'"
                Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                cmdUpdate.ExecuteNonQuery()
            End If
        Catch ex As Exception

        End Try

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
        If globVar.CanAccess(Users.menu) Then
            Users.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(Users)
            TabControl1.TabPages(Users).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnMasterMaterial(sender As Object, e As EventArgs) Handles MasterMaterialBtn.Click
        If globVar.CanAccess(MasterMaterial.menu) Then
            MasterMaterial.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MasterMaterial)
            TabControl1.TabPages(MasterMaterial).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnMasterProcess(sender As Object, e As EventArgs) Handles MasterProcessBtn.Click
        If globVar.CanAccess(MasterProcess.menu) Then
            MasterProcess.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MasterProcess)
            TabControl1.TabPages(MasterProcess).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnMasterProcessFlow(sender As Object, e As EventArgs) Handles MasterProcessFlowBtn.Click
        If globVar.CanAccess(MasterProcessFlow.menu) Then
            MasterProcessFlow.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MasterProcessFlow)
            TabControl1.TabPages(MasterProcessFlow).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnMasterFinishGoods(sender As Object, e As EventArgs) Handles MasterFinishGoodsBtn.Click
        If globVar.CanAccess(MasterFinishGoods.menu) Then
            MasterFinishGoods.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MasterFinishGoods)
            TabControl1.TabPages(MasterFinishGoods).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub btnMaterialUsageFinishGoods(sender As Object, e As EventArgs) Handles MaterialUsageFinishGoodsBtn.Click
        If globVar.CanAccess(MaterialUsageFinishGoods.menu) Then
            MaterialUsageFinishGoods.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MaterialUsageFinishGoods)
            TabControl1.TabPages(MaterialUsageFinishGoods).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub btnProcessFlowMaterialUsage(sender As Object, e As EventArgs) Handles ProcessFlowMaterialUsageBtn.Click
        If globVar.CanAccess(ProcessFlowMaterialUsage.menu) Then
            ProcessFlowMaterialUsage.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(ProcessFlowMaterialUsage)
            TabControl1.TabPages(ProcessFlowMaterialUsage).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub BtnMasterLine(sender As Object, e As EventArgs) Handles MasterLineBtn.Click
        If globVar.CanAccess(MasterLine.menu) Then
            MasterLine.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MasterLine)
            TabControl1.TabPages(MasterLine).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnAC(sender As Object, e As EventArgs) Handles ACBtn.Click
        If globVar.CanAccess(AccessControll.menu) Then
            AccessControll.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(AccessControll)
            TabControl1.TabPages(AccessControll).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' -----------------------End Menu Master Data--------------------------'

    ' -----------------------Start Menu Mini Store-------------------------'
    Private Sub btnInputStock(sender As Object, e As EventArgs) Handles InputStockBtn.Click
        If globVar.CanAccess(FormInputStock.menu) Then
            FormInputStock.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(FormInputStock)
            TabControl1.TabPages(FormInputStock).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub btnReturnStock(sender As Object, e As EventArgs) Handles ReturnStockBtn.Click
        If globVar.CanAccess(FormReturnStock.menu) Then
            FormReturnStock.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(FormReturnStock)
            TabControl1.TabPages(FormReturnStock).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnProductionRequest(sender As Object, e As EventArgs) Handles ProductionRequestBtn.Click
        If globVar.CanAccess(ProductionRequest.menu) Then
            ProductionRequest.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(ProductionRequest)
            TabControl1.TabPages(ProductionRequest).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub BtnStockMinistore(sender As Object, e As EventArgs) Handles StockMinistoreBtn.Click
        If globVar.CanAccess(StockMinistore.menu) Then
            StockMinistore.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(StockMinistore)
            TabControl1.TabPages(StockMinistore).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnReceiveReturnMaterial(sender As Object, e As EventArgs) Handles ReceiveReturnMaterialBtn.Click
        If globVar.CanAccess(ReceiveReturnMaterial.menu) Then
            ReceiveReturnMaterial.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(ReceiveReturnMaterial)
            TabControl1.TabPages(ReceiveReturnMaterial).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnSplitQty(sender As Object, e As EventArgs) Handles SplitQtyBtn.Click
        If globVar.CanAccess(SplitMaterial.menu) Then
            SplitMaterial.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(SplitMaterial)
            TabControl1.TabPages(SplitMaterial).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' -----------------------End Menu Mini Store---------------------------'

    ' -----------------------Start Menu Production-------------------------'

    Private Sub btnMainPoSubPO(sender As Object, e As EventArgs) Handles MainPOSubPOBtn.Click
        If globVar.CanAccess(MainPOSubPO.menu) Then
            MainPOSubPO.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MainPOSubPO)
            TabControl1.TabPages(MainPOSubPO).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub BtnProduction(sender As Object, e As EventArgs) Handles ProductionBtn.Click
        If globVar.CanAccess(Production.menu) Then
            Production.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(Production)
            TabControl1.TabPages(Production).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnStockProd(sender As Object, e As EventArgs) Handles StockProdBtn.Click
        If globVar.CanAccess(StockProduction.menu) Then
            StockProduction.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(StockProduction)
            TabControl1.TabPages(StockProduction).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnAddChangeOperator(sender As Object, e As EventArgs) Handles AddChangeOperatorBtn.Click
        If globVar.CanAccess(AddChangeOperator.menu) Then
            AddChangeOperator.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(AddChangeOperator)
            TabControl1.TabPages(AddChangeOperator).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub BtnPrintFlowTicket(sender As Object, e As EventArgs) Handles PrintFlowTicketBtn.Click
        If globVar.CanAccess(PrintFlowTicket.menu) Then
            PrintFlowTicket.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(PrintFlowTicket)
            TabControl1.TabPages(PrintFlowTicket).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnSummaryProduction(sender As Object, e As EventArgs) Handles SummaryProductionBtn.Click
        If globVar.CanAccess(Summary.menu) Then
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
        If globVar.username = "admin" Then
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
        If globVar.CanAccess(FGA.menu) Then
            FGA.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(FGA)
            TabControl1.TabPages(FGA).Select()
        Else
            RJMessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnResultProduction(sender As Object, e As EventArgs) Handles ResultProductionBtn.Click
        If globVar.CanAccess(FormDefective.menu) Then
            FormDefective.Close()
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(FormDefective)
            TabControl1.TabPages(FormDefective).Select()
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

    Private Sub BtnStatusFlowTicket(sender As Object, e As EventArgs) Handles StatusFlowTicket.Click
        Dim _formStatusFlowTicket As New StatusFlowTicket()
        _formStatusFlowTicket.ShowDialog()
    End Sub

    Private Sub BtnTraceability(sender As Object, e As EventArgs) Handles TraceabilityBtn.Click
        TabControl1.TabPages.Add(TraceabilityV3)
        TabControl1.TabPages(TraceabilityV3).Select()
    End Sub
End Class