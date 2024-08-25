Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.IO

Public Class globVar
    Public Shared hakAkses As String
    Public Shared department As String
    Public Shared failPrint As String
    Public Shared username As String

    Public Shared QRCode_PN As String
    Public Shared QRCode_Qty As String
    Public Shared QRCode_Inv As String
    Public Shared QRCode_Traceability As String
    Public Shared QRCode_Batch As String
    Public Shared QRCode_lot As String
    Public Shared QRCode_Country As String

    Public Shared view As Integer
    Public Shared add As Integer
    Public Shared update As Integer
    Public Shared delete As Integer

    Public Shared shift As String

    Public Shared shift1Awal As New TimeSpan(7, 0, 1)
    Public Shared shift1Akhir As New TimeSpan(15, 0, 0)

    Public Shared shift2Awal As New TimeSpan(15, 0, 1)
    Public Shared shift2Akhir As New TimeSpan(23, 0, 0)

    Public Shared shift3Awal As New TimeSpan(23, 0, 1)
    Public Shared shift3Akhir As New TimeSpan(7, 0, 0)

    Public Shared Function CanAccess(menu As String)
        Dim result As Integer
        If username = "" Then
            result = 0
        Else
            Dim sql As String = "select * from master_access where menu like '%" & menu & ";%'"
            Dim dt As DataTable = Database.GetData(sql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    Dim sqlUsers As String = "select role from users where username='" & globVar.username & "' and role like '%" & dt.Rows(i).Item("name") & ";%'"
                    Dim dtUsers As DataTable = Database.GetData(sqlUsers)
                    If dtUsers.Rows.Count > 0 Then
                        result = 1
                        globVar.view = dt.Rows(i).Item("view")
                        globVar.add = dt.Rows(i).Item("add")
                        globVar.update = dt.Rows(i).Item("update")
                        globVar.delete = dt.Rows(i).Item("delete")
                        Exit For
                    End If
                Next
            Else
                globVar.view = 0
                globVar.add = 0
                globVar.update = 0
                globVar.delete = 0
                result = 0
            End If
        End If

        Return result

    End Function

    Public Shared Sub PingVersion()

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
                Dim SqlUpdate As String = "UPDATE LIST_CONNECTION SET status=1, ping='" & reply.RoundtripTime & "',LAST_UPDATE=getdate(),VERSION='" & Application.ProductVersion & "' FROM LIST_CONNECTION WHERE COMPUTER_NAME='" & hostName & "' and IP_ADDRESS='" & ipAdd & "'"
                Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                cmdUpdate.ExecuteNonQuery()
            Else
                Dim SqlUpdate As String = "UPDATE LIST_CONNECTION SET status=0, ping is null,LAST_UPDATE=getdate(),VERSION='" & Application.ProductVersion & "' FROM LIST_CONNECTION WHERE COMPUTER_NAME='" & hostName & "' and IP_ADDRESS='" & ipAdd & "'"
                Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                cmdUpdate.ExecuteNonQuery()
            End If
        Catch ex As Exception

        End Try

        If read_notepad("\\192.168.0.254\Updater\MES App\_Version\Version.txt") <> Application.ProductVersion Then

            RJMessageBox.Show("New Version is available so this apps exit with automatically.")
            Application.Exit()
            Exit Sub

        End If

    End Sub

    Public Shared Function read_notepad(filePath As String) As String
        Dim fileContents As String = File.ReadAllText(filePath)
        Return fileContents
    End Function

End Class


