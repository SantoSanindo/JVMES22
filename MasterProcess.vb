Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles

Public Class MasterProcess

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txt_masterprocess_nama.Text <> "" Then
            Dim querycheck As String = "select * from MASTER_PROCESS where lower(PROCESS_NAME)='" & Trim(txt_masterprocess_nama.Text.ToLower) & "'"
            Dim dtCheck As DataTable = Database.GetData(querycheck)
            If dtCheck.Rows.Count > 0 Then
                MessageBox.Show("Process Exist")
                txt_masterprocess_nama.Text = ""
                txt_masterprocess_desc.Text = ""
            Else
                Try
                    Dim sql As String = "INSERT INTO MASTER_PROCESS(PROCESS_NAME,PROCESS_DESC) VALUES ('" & Trim(txt_masterprocess_nama.Text) & "','" & Trim(txt_masterprocess_desc.Text) & "')"
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
        txt_masterprocess_search.Text = ""
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
    End Sub

    Private Sub dgv_masterprocess_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_masterprocess.CellClick
        If dgv_masterprocess.Columns(e.ColumnIndex).Name = "delete" Then
            Dim result = MessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Try
                    Dim querycheck As String = "select * from MASTER_PROCESS_FLOW where lower(MASTER_PROCESS)='" & Trim(dgv_masterprocess.Rows(e.RowIndex).Cells(1).Value.ToLower) & "'"
                    Dim dtCheck As DataTable = Database.GetData(querycheck)
                    If dtCheck.Rows.Count > 0 Then
                        MessageBox.Show("Cannot Delete this data because refrence to process flow.")
                    Else
                        Dim sql As String = "delete from master_process where process_name='" & dgv_masterprocess.Rows(e.RowIndex).Cells(1).Value & "'"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        cmd.ExecuteNonQuery()
                        dgv_masterprocess.DataSource = Nothing
                        DGV_MasterProcesss()
                        MessageBox.Show("Delete Success.")
                    End If
                Catch ex As Exception
                    MessageBox.Show("Delete failed" & ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub dgv_material_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgv_masterprocess.DataBindingComplete
        For i As Integer = 0 To dgv_masterprocess.RowCount - 1
            If dgv_masterprocess.Rows(i).Index Mod 2 = 0 Then
                dgv_masterprocess.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_masterprocess.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub txt_masterprocess_search_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_masterprocess_search.PreviewKeyDown
        If e.KeyData = Keys.Enter Then

            Dim Found As Boolean = False
            Dim StringToSearch As String = ""
            Dim ValueToSearchFor As String = Me.txt_masterprocess_search.Text.Trim.ToLower
            Dim CurrentRowIndex As Integer = 0
            Try
                If dgv_masterprocess.Rows.Count = 0 Then
                    CurrentRowIndex = 0
                Else
                    CurrentRowIndex = dgv_masterprocess.CurrentRow.Index + 1
                End If
                If CurrentRowIndex > dgv_masterprocess.Rows.Count Then
                    CurrentRowIndex = dgv_masterprocess.Rows.Count - 1
                End If
                If dgv_masterprocess.Rows.Count > 0 Then
                    For Each gRow As DataGridViewRow In dgv_masterprocess.Rows
                        StringToSearch = gRow.Cells(1).Value.ToString.Trim.ToLower
                        If InStr(1, StringToSearch, LCase(Trim(txt_masterprocess_search.Text)), vbTextCompare) = 1 Then
                            Dim myCurrentCell As DataGridViewCell = gRow.Cells(1)
                            Dim myCurrentPosition As DataGridViewCell = gRow.Cells(0)
                            dgv_masterprocess.CurrentCell = myCurrentCell
                            CurrentRowIndex = dgv_masterprocess.CurrentRow.Index
                            Found = True
                        End If
                        If Found Then Exit For
                    Next
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub
End Class