Option Explicit On
Option Strict On
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class FormLogin
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim query As String = "select * from users where username='" & txtUname.Text & "' and password='" & txtPass.Text & "'"
        Dim dt As DataTable = Database.GetData(query)

        If dt.Rows.Count > 0 Then
            globVar.hakAkses = dt.Rows(0).Item("ROLE").ToString
            globVar.department = dt.Rows(0).Item("DEPARTMENT").ToString
            txtUname.Clear()
            txtPass.Clear()

            HOME.LoginUser.Text = dt.Rows(0).Item("NAME").ToString & " - " & dt.Rows(0).Item("DEPARTMENT").ToString
        Else
            MessageBox.Show("Login Failed. Please Try Again.")
            txtUname.Clear()
            txtPass.Clear()
            txtUname.Select()
        End If
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        txtUname.Text = ""
        txtPass.Text = ""
    End Sub

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtUname.Select()
        Call Database.koneksi_database()
    End Sub

    Private Sub txtPass_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtPass.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            Dim query As String = "select * from users where username='" & txtUname.Text & "' and password='" & txtPass.Text & "'"
            Dim dt As DataTable = Database.GetData(query)

            If dt.Rows.Count > 0 Then
                globVar.hakAkses = dt.Rows(0).Item("ROLE").ToString
                globVar.department = dt.Rows(0).Item("DEPARTMENT").ToString
                txtUname.Clear()
                txtPass.Clear()

                HOME.LoginUser.Text = dt.Rows(0).Item("NAME").ToString & " - " & dt.Rows(0).Item("DEPARTMENT").ToString
            Else
                MessageBox.Show("Login Failed. Please Try Again.")
                txtUname.Clear()
                txtPass.Clear()
                txtUname.Select()
            End If
        End If
    End Sub
End Class