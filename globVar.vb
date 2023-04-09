Imports System.Data.SqlClient
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
                result = 0
            End If
        End If

        Return result

    End Function

End Class


