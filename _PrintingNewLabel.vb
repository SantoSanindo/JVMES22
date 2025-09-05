Imports System.IO
Imports System.Reflection
Imports NiceLabel.SDK

Public Class _PrintingNewLabel
    Public Shared menu As String = "Label1"

    Dim label As ILabel

    Dim printers As IList(Of IPrinter)
    Dim selected_Printer As IPrinter
    Private Sub _PrintingDefect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializePrintEngine()

        Dim appPath As String = Application.StartupPath()
        label = PrintEngineFactory.PrintEngine.OpenLabel(appPath & "\Label\" & "NewLabel.nlbl")

    End Sub

    Private Sub InitializePrintEngine()
        Try
            Dim sdkFilesPath As String = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..\\SDKFiles")
            'RJMessageBox.Show(sdkFilesPath)

            If My.Computer.FileSystem.DirectoryExists(sdkFilesPath) Then
                PrintEngineFactory.SDKFilesPath = sdkFilesPath
            End If
            PrintEngineFactory.PrintEngine.Initialize()
            'RJMessageBox.Show("test")
        Catch ex As Exception
            RJMessageBox.Show("Initialization of the SDK failed." + Environment.NewLine + Environment.NewLine + ex.ToString())
            Me.Close()
        End Try
    End Sub

    Public Sub btn_Print_Click(sender As Object, e As EventArgs) Handles btn_Print.Click
        Try
            Clear_data()
            isi_data()
            label.Print(1)
            globVar.failPrint = "No"
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
            globVar.failPrint = "Yes"
        End Try
    End Sub
    Private Sub isi_data()
        Application.DoEvents()

        label.Variables("unique_ID").SetValue(txt_Unique_id.Text)

        label.Variables("qr_code").SetValue(txt_QR_Code.Text)
    End Sub
    Private Sub Clear_data()
        Application.DoEvents()

        label.Variables("unique_ID").SetValue("")

        label.Variables("qr_code").SetValue("")
    End Sub

    Private Sub _PrintingDefect_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        PrintEngineFactory.PrintEngine.Shutdown()
    End Sub

    Private Sub btn_refresh_Click(sender As Object, e As EventArgs) Handles btn_refresh.Click
        Dim appPath As String = Application.StartupPath()
        label = PrintEngineFactory.PrintEngine.OpenLabel(appPath & "\Label\" & "NewLabel.nlbl")
    End Sub
End Class