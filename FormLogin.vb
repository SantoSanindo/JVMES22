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
            globVar.username = txtUname.Text

            txtUname.Clear()
            txtPass.Clear()

            HOME.LoginUser.Text = dt.Rows(0).Item("NAME").ToString & " - " & globVar.department

            If globVar.username = "admin" Then
                ComboBox1.Enabled = True
            End If
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
        ComboBox1.Enabled = False
    End Sub

    Private Sub txtPass_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtPass.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            Dim query As String = "select * from users where username='" & txtUname.Text & "' and password='" & txtPass.Text & "'"
            Dim dt As DataTable = Database.GetData(query)

            If dt.Rows.Count > 0 Then
                globVar.hakAkses = dt.Rows(0).Item("ROLE").ToString
                globVar.department = dt.Rows(0).Item("DEPARTMENT").ToString
                globVar.username = txtUname.Text
                txtUname.Clear()
                txtPass.Clear()

                HOME.LoginUser.Text = dt.Rows(0).Item("NAME").ToString & " - " & globVar.department
                If globVar.username = "admin" Then
                    ComboBox1.Enabled = True
                End If
            Else
                MessageBox.Show("Login Failed. Please Try Again.")
                txtUname.Clear()
                txtPass.Clear()
                txtUname.Select()
            End If
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        globVar.department = ComboBox1.Text
        HOME.LoginUser.Text = "Administrator - " & globVar.department
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
            QRCode.Baca(TextBox1.Text)
        End If
    End Sub
End Class