Imports System.Configuration
Imports System.Data.SqlClient

Public Class Database

    Public Shared koneksi As SqlConnection
    Public Shared database As String

    Public da As SqlDataAdapter

    Public Shared Sub koneksi_database()
        Try
            Dim connectionString As String = ConfigurationManager.ConnectionStrings("Jovan_New.My.MySettings.Production").ConnectionString
            koneksi = New SqlConnection(connectionString)
            If koneksi.State = ConnectionState.Closed Then koneksi.Open() Else koneksi.Close()
        Catch ex As Exception
            RJMessageBox.Show("Please Contact IT Team. This is Database connection Problem -> " + ex.Message)
        End Try
    End Sub

    Public Shared Function GetData(ByVal query As String) As DataTable
        Dim dt As New DataTable
        Try
            koneksi_database()
            Using cmd As SqlCommand = New SqlCommand(query, koneksi)
                Using sda As SqlDataAdapter = New SqlDataAdapter(cmd)
                    sda.Fill(dt)
                End Using
            End Using

            'If koneksi.State = ConnectionState.Open Then
            '    koneksi.Close()
            'End If

            Return dt
        Catch ex As Exception
            RJMessageBox.Show("Error Query ->" + ex.Message)
        End Try
    End Function

    Public Shared Sub close_koneksi()
        Try
            koneksi.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class
