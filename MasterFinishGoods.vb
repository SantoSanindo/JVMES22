Imports System.Data.SqlClient

Public Class MasterFinishGoods

    Public idP As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txt_masterfinishgoods_pn.Text <> "" And txt_masterfinishgoods_desc.Text <> "" And txt_masterfinishgoods_family.Text <> "" And txt_masterfinishgoods_comp.Text <> "" And txt_masterfinishgoods_usage.Text <> "" Then
            Dim querycheck As String = "select * from MASTER_FINISH_GOODS where FG_PART_NUMBER='" & txt_masterfinishgoods_pn.Text & "' and COMPONENT='" & txt_masterfinishgoods_comp.Text & "'"
            Dim dtCheck As DataTable = Database.GetData(querycheck)
            If dtCheck.Rows.Count > 0 Then
                MessageBox.Show("FG Part Number and Comp exist")
            Else
                Try
                    Dim sql As String = "INSERT INTO MASTER_FINISH_GOODS(FG_PART_NUMBER,DESCRIPTION,FAMILY,COMPONENT,USAGE) VALUES ('" & txt_masterfinishgoods_pn.Text & "','" & txt_masterfinishgoods_desc.Text & "','" & txt_masterfinishgoods_family.Text & "','" & txt_masterfinishgoods_comp.Text & "'," & txt_masterfinishgoods_usage.Text.Replace(",", ".") & ")"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()

                    dgv_masterfinishgoods_atas.DataSource = Nothing

                    DGV_Masterfinishgoods_atass(txt_masterfinishgoods_pn.Text)
                    treeView_show()

                    idP = txt_masterfinishgoods_pn.Text

                    txt_masterfinishgoods_pn.Text = ""
                    txt_masterfinishgoods_desc.Text = ""
                    txt_masterfinishgoods_family.Text = ""
                    txt_masterfinishgoods_comp.Text = ""
                    txt_masterfinishgoods_usage.Text = ""
                    txt_masterfinishgoods_pn.Select()
                Catch ex As Exception
                    MessageBox.Show("Error Insert" & ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub MasterFinishGoods_Load(sender As Object, e As EventArgs) Handles Me.Load
        'txt_masterfinishgoods_pn.Select()
        'DGV_Masterfinishgoods_atass()
        treeView_show()
        idP = ""
    End Sub

    Private Sub treeView_show()
        TreeView1.Nodes.Clear()
        Dim queryFinishGoods As String = "select DISTINCT(FG_PART_NUMBER) from MASTER_FINISH_GOODS order by fg_part_number"
        Dim dtFinishGoods As DataTable = Database.GetData(queryFinishGoods)

        TreeView1.Nodes.Add("Master Finish Goods")

        For i = 0 To dtFinishGoods.Rows.Count - 1
            TreeView1.Nodes(0).Nodes.Add(dtFinishGoods.Rows(i).Item("FG_PART_NUMBER").ToString, "FG PN : " & dtFinishGoods.Rows(i).Item("FG_PART_NUMBER").ToString)
        Next
        TreeView1.ExpandAll()
    End Sub

    Private Sub dgv_masterfinishgoods_atas_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_masterfinishgoods_atas.CellClick
        If dgv_masterfinishgoods_atas.Columns(e.ColumnIndex).Name = "delete" Then

            Dim result = MessageBox.Show("Are you sure delete this data?", "warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from master_finish_goods where ID=" & dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(1).Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()
                    DGV_Masterfinishgoods_atass(idP)
                    treeView_show()
                    MessageBox.Show("Delete Success.")
                Catch ex As Exception
                    MessageBox.Show("Delete Failed" & ex.Message)
                End Try
            End If
        End If

        If e.ColumnIndex = 0 Then
            If dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(0).Value = True Then
                dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(0).Value = False
            Else
                dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(0).Value = True
            End If
        End If
    End Sub

    Private Sub DGV_Masterfinishgoods_atass(id As String)
        dgv_masterfinishgoods_atas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_masterfinishgoods_atas.DataSource = Nothing
        dgv_masterfinishgoods_atas.Rows.Clear()
        dgv_masterfinishgoods_atas.Columns.Clear()
        Call Database.koneksi_database()
        Dim queryMasterFinishGoods As String = "select ID,FG_PART_NUMBER,DESCRIPTION,FAMILY,COMPONENT,USAGE from master_finish_goods where fg_part_number='" & id & "'"
        Dim dtMasterMaterial As DataTable = Database.GetData(queryMasterFinishGoods)

        dgv_masterfinishgoods_atas.DataSource = dtMasterMaterial

        dgv_masterfinishgoods_atas.Columns(0).Width = 100
        dgv_masterfinishgoods_atas.Columns(1).Width = 300
        dgv_masterfinishgoods_atas.Columns(2).Width = 500
        dgv_masterfinishgoods_atas.Columns(3).Width = 150
        dgv_masterfinishgoods_atas.Columns(5).Width = 150

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        dgv_masterfinishgoods_atas.Columns.Insert(6, delete)

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
                If row.Cells(0).Value = True Then
                    Dim sql As String = "delete from master_finish_goods where id=" & row.Cells(1).Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()
                    hapus = hapus + 1
                End If
            Next
        End If

        treeView_show()
        DGV_Masterfinishgoods_atass(idP)
        MessageBox.Show("Delete Success " & hapus & " Data.")
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_masterfinishgoods_search.PreviewKeyDown
        If e.KeyData = Keys.Enter Then

            Dim Found As Boolean = False
            Dim StringToSearch As String = ""
            Dim ValueToSearchFor As String = Me.txt_masterfinishgoods_search.Text.Trim.ToLower
            Dim CurrentRowIndex As Integer = 0
            Try
                If dgv_masterfinishgoods_atas.Rows.Count = 0 Then
                    CurrentRowIndex = 0
                Else
                    CurrentRowIndex = dgv_masterfinishgoods_atas.CurrentRow.Index + 1
                End If
                If CurrentRowIndex > dgv_masterfinishgoods_atas.Rows.Count Then
                    CurrentRowIndex = dgv_masterfinishgoods_atas.Rows.Count - 1
                End If
                If dgv_masterfinishgoods_atas.Rows.Count > 0 Then
                    For Each gRow As DataGridViewRow In dgv_masterfinishgoods_atas.Rows
                        StringToSearch = gRow.Cells(5).Value.ToString.Trim.ToLower
                        If InStr(1, StringToSearch, LCase(Trim(txt_masterfinishgoods_search.Text)), vbTextCompare) = 1 Then
                            Dim myCurrentCell As DataGridViewCell = gRow.Cells(5)
                            Dim myCurrentPosition As DataGridViewCell = gRow.Cells(0)
                            dgv_masterfinishgoods_atas.CurrentCell = myCurrentCell
                            CurrentRowIndex = dgv_masterfinishgoods_atas.CurrentRow.Index
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

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        If TreeView1.SelectedNode Is Nothing Then
            dgv_masterfinishgoods_atas.DataSource = Nothing
            Exit Sub
        End If

        idP = TreeView1.SelectedNode.Name

        DGV_Masterfinishgoods_atass(idP)
    End Sub

    Private Sub dgv_material_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgv_masterfinishgoods_atas.DataBindingComplete
        For i As Integer = 0 To dgv_masterfinishgoods_atas.RowCount - 1
            If dgv_masterfinishgoods_atas.Rows(i).Index Mod 2 = 0 Then
                dgv_masterfinishgoods_atas.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_masterfinishgoods_atas.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub
End Class