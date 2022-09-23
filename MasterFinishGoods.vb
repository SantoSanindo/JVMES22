Imports System.Data.SqlClient

Public Class MasterFinishGoods

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txt_masterfinishgoods_pn.Text <> "" And txt_masterfinishgoods_qty.Text <> "" Then
            Dim querycheck As String = "select * from MASTER_FINISH_GOODS where PART_NUMBER=" & txt_masterfinishgoods_pn.Text
            Dim dtCheck As DataTable = Database.GetData(querycheck)
            If dtCheck.Rows.Count > 0 Then
                MessageBox.Show("Part Number exist")
            Else
                Try
                    Dim sql As String = "INSERT INTO MASTER_FINISH_GOODS(PART_NUMBER,STANDARD_QTY) VALUES (" & txt_masterfinishgoods_pn.Text & "," & txt_masterfinishgoods_qty.Text & ")"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()

                    txt_masterfinishgoods_pn.Text = ""
                    txt_masterfinishgoods_qty.Text = ""
                    txt_masterfinishgoods_pn.Select()

                    dgv_masterfinishgoods_atas.DataSource = Nothing
                    DGV_Masterfinishgoods_atass()
                Catch ex As Exception
                    MessageBox.Show("Error Insert" & ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub MasterFinishGoods_Load(sender As Object, e As EventArgs) Handles Me.Load
        txt_masterfinishgoods_pn.Select()
        DGV_Masterfinishgoods_atass()
    End Sub

    Private Sub dgv_masterfinishgoods_atas_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_masterfinishgoods_atas.CellClick
        If e.ColumnIndex = 5 Then
            If dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(4).Value = 0 Then
                Dim result = MessageBox.Show("Are you sure delete this data?", "warning", MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then
                    Try
                        Dim sql As String = "delete from master_finish_goods where part_number=" & dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(2).Value
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        cmd.ExecuteNonQuery()

                        dgv_masterfinishgoods_atas.DataSource = Nothing
                        DGV_Masterfinishgoods_atass()
                        MessageBox.Show("Delete Success.")
                    Catch ex As Exception
                        MessageBox.Show("Delete Failed" & ex.Message)
                    End Try
                End If
            Else
                MessageBox.Show("This Data cannot be delete.")
            End If
        End If

        If e.ColumnIndex = 0 Then
            If dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(0).Value = True Then
                dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(0).Value = False
            Else
                dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(0).Value = True
            End If
        End If

        If e.ColumnIndex = 1 Then
            Dim masterfinishgoods2 = New MasterFinishGoods2()
            masterfinishgoods2.TextBox3.Text = dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(2).Value.ToString
            masterfinishgoods2.Show()
            masterfinishgoods2.sub_dgv_masterfinishgoods2(dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(2).Value, dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(3).Value)
            Me.Close()
        End If
    End Sub

    Private Sub DGV_Masterfinishgoods_atass()
        dgv_masterfinishgoods_atas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_masterfinishgoods_atas.Rows.Clear()
        dgv_masterfinishgoods_atas.Columns.Clear()
        Call Database.koneksi_database()
        Dim dtMasterMaterial As DataTable = Database.GetData("select M.part_number as Part_Number_FG, M.standard_qty as Qty,(SELECT COUNT(*) FROM MASTER_FINISH_GOODS WHERE PART_NUMBER=M.part_number AND MATERIAL_PART_NUMBER IS NOT NULL) AS Total_Material from MASTER_FINISH_GOODS M WHERE M.MATERIAL_PART_NUMBER IS NULL ORDER BY TOTAL_MATERIAL DESC")

        dgv_masterfinishgoods_atas.DataSource = dtMasterMaterial

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        dgv_masterfinishgoods_atas.Columns.Insert(3, delete)

        Dim lihat As DataGridViewButtonColumn = New DataGridViewButtonColumn
        lihat.Name = "lihat"
        lihat.HeaderText = "View"
        lihat.Width = 100
        lihat.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        lihat.Text = "View"
        lihat.UseColumnTextForButtonValue = True
        dgv_masterfinishgoods_atas.Columns.Insert(0, lihat)

        Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
        check.Name = "check"
        check.HeaderText = "Check"
        check.Width = 100
        check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgv_masterfinishgoods_atas.Columns.Insert(0, check)

        For i As Integer = 0 To dgv_masterfinishgoods_atas.RowCount - 1
            If dgv_masterfinishgoods_atas.Rows(i).Index Mod 2 = 0 Then
                dgv_masterfinishgoods_atas.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_masterfinishgoods_atas.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Dim hapus As Integer = 0
        Dim result = MessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then
            For Each row As DataGridViewRow In dgv_masterfinishgoods_atas.Rows
                If row.Cells(0).Value = True And row.Cells(4).Value = 0 Then
                    Dim sql As String = "delete from master_finish_goods where part_number=" & row.Cells(2).Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()
                    hapus = hapus + 1
                End If
            Next
        End If

        dgv_masterfinishgoods_atas.DataSource = Nothing
        DGV_Masterfinishgoods_atass()
        MessageBox.Show("Delete Success " & hapus & " Data.")
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_masterfinishgoods_search.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            Dim str As String = txt_masterfinishgoods_search.Text
            Try
                For i As Integer = 0 To dgv_masterfinishgoods_atas.Rows.Count - 1
                    For j As Integer = 0 To dgv_masterfinishgoods_atas.Columns.Count - 1
                        If dgv_masterfinishgoods_atas.Rows(i).Cells(j).Value = str Then
                            dgv_masterfinishgoods_atas.Rows(i).Selected = True
                            dgv_masterfinishgoods_atas.CurrentCell = dgv_masterfinishgoods_atas.Rows(i).Cells(j)
                            Exit Sub
                        End If
                    Next
                Next i
            Catch abc As Exception
            End Try
            MsgBox("Data not found!")
        End If
    End Sub
End Class