Imports System.Data.SqlClient

Public Class Database

    Public Shared koneksi As SqlConnection
    Public Shared database As String

    Public da As SqlDataAdapter

    Public Shared Sub koneksi_database()
        Try
            database = "Data Source=10.14.3.51;
            initial catalog=JOVAN;
            Persist Security Info=True;
            User ID=sa;
            Password=jovan123;
            Max Pool Size=5000;
            Pooling=True"
            koneksi = New SqlConnection(database)
            If koneksi.State = ConnectionState.Closed Then koneksi.Open() Else koneksi.Close()

            'database = "Data Source=10.13.8.203;
            'initial catalog=JOVAN;
            'Persist Security Info=True;
            'User ID=sa;
            'Password=jovan123;
            'Max Pool Size=5000;
            'Pooling=True"
            'koneksi = New SqlConnection(database)
            'If koneksi.State = ConnectionState.Closed Then koneksi.Open() Else koneksi.Close()
        Catch ex As Exception
            MsgBox("Please Contact IT Team. This is Database connection Problem -> " + ex.Message)
        End Try
    End Sub

    Public Shared Function GetData(ByVal query As String) As DataTable
        Try
            koneksi_database()
            Using cmd As SqlCommand = New SqlCommand(query, koneksi)
                Using sda As SqlDataAdapter = New SqlDataAdapter(cmd)
                    Using dt As DataTable = New DataTable
                        sda.Fill(dt)
                        Return dt
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error Query ->" + ex.Message)
        End Try
    End Function

    Public Shared Sub close_koneksi()
        Try
            koneksi.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class
