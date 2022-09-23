Option Explicit On
Option Strict On

Public Class FormLogin
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If txtUname.Text = "leader" And txtPass.Text = "leader" Then
            globVar.hakAkses = "leader"

            Me.Hide()

            MainForm.Show()

            txtUname.Clear()
            txtPass.Clear()
        ElseIf txtUname.Text = "operator" And txtPass.Text = "operator" Then
            globVar.hakAkses = "operator"

            Me.Hide()

            MainForm.Show()

            txtUname.Clear()
            txtPass.Clear()
        ElseIf txtUname.Text = "admin" And txtPass.Text = "admin" Then
            globVar.hakAkses = "admin"

            Me.Hide()

            MainForm.Show()

            txtUname.Clear()
            txtPass.Clear()
        Else
            MessageBox.Show("Maaf, hak akses anda belum terdaftar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtUname.Clear()
            txtPass.Clear()
            txtUname.Select()
        End If
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Me.Close()
    End Sub

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtUname.Select()
    End Sub

    Private Sub txtPass_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtPass.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            If txtUname.Text = "leader" And txtPass.Text = "leader" Then
                globVar.hakAkses = "leader"

                Me.Hide()

                MainForm.Show()

                txtUname.Clear()
                txtPass.Clear()
            ElseIf txtUname.Text = "operator" And txtPass.Text = "operator" Then
                globVar.hakAkses = "operator"

                Me.Hide()

                MainForm.Show()

                txtUname.Clear()
                txtPass.Clear()
            ElseIf txtUname.Text = "admin" And txtPass.Text = "admin" Then
                globVar.hakAkses = "admin"

                Me.Hide()

                MainForm.Show()

                txtUname.Clear()
                txtPass.Clear()
            Else
                MessageBox.Show("Maaf, hak akses anda belum terdaftar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtUname.Clear()
                txtPass.Clear()
                txtUname.Select()
            End If
        End If
    End Sub
End Class