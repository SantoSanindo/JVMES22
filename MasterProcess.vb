Imports System.Data.SqlClient

Public Class MasterProcess

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txt_masterprocess_nama.Text <> "" Then
            Dim querycheck As String = "select * from MASTER_PROCESS where PROCESS_NAME='" & txt_masterprocess_nama.Text & "'"
            Dim dtCheck As DataTable = Database.GetData(querycheck)
            If dtCheck.Rows.Count > 0 Then
                MessageBox.Show("Process Exist")
                txt_masterprocess_nama.Text = ""
                txt_masterprocess_desc.Text = ""
            Else
                Try
                    Dim sql As String = "INSERT INTO MASTER_PROCESS(PROCESS_NAME,PROCESS_DESC) VALUES ('" & txt_masterprocess_nama.Text & "','" & txt_masterprocess_desc.Text & "')"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()

                    txt_masterprocess_nama.Text = ""
                    txt_masterprocess_desc.Text = ""
                    txt_masterprocess_nama.Select()

                    dgv_masterprocess.DataSource = Nothing
                    DGV_MasterProcesss()
                Catch ex As Exception
                    MessageBox.Show("Error Insert" & ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub MasterMaterial_Load(sender As Object, e As EventArgs) Handles Me.Load
        txt_masterprocess_nama.Select()
        DGV_MasterProcesss()
    End Sub

    Private Sub DGV_MasterProcesss()
        dgv_masterprocess.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_masterprocess.Rows.Clear()
        dgv_masterprocess.Columns.Clear()
        Call Database.koneksi_database()
        Dim dtMasterMaterial As DataTable = Database.GetData("select process_name as Name_Process,process_desc as Desc_Process from MASTER_PROCESS")

        dgv_masterprocess.DataSource = dtMasterMaterial

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        dgv_masterprocess.Columns.Insert(2, delete)

        For i As Integer = 0 To dgv_masterprocess.RowCount - 1
            If dgv_masterprocess.Rows(i).Index Mod 2 = 0 Then
                dgv_masterprocess.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_masterprocess.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub dgv_masterprocess_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_masterprocess.CellClick
        If e.ColumnIndex = 2 Then
            Dim result = MessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from master_process where process_name='" & dgv_masterprocess.Rows(e.RowIndex).Cells(1).Value & "'"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()

                    Dim sql1 As String = "delete from master_process_flow where master_proc='" & dgv_masterprocess.Rows(e.RowIndex).Cells(1).Value & "'"
                    Dim cmd1 = New SqlCommand(sql1, Database.koneksi)
                    cmd1.ExecuteNonQuery()

                    dgv_masterprocess.DataSource = Nothing
                    DGV_MasterProcesss()
                    MessageBox.Show("Delete Success.")
                Catch ex As Exception
                    MessageBox.Show("Delete failed" & ex.Message)
                End Try
            End If
        End If
    End Sub
End Class