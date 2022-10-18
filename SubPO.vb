Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class SubPO

    Public po_pre_production As String
    Public fg_pn_pre_production As String
    Public curr_qty_pre_production As Integer

    Public curr_qty_sub_po As Integer
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If cb_lineproduction.Text <> "" And qty_sub_po.Text <> "" Then
            If Int(qty_sub_po.Text) <= curr_qty_sub_po Then
                Dim querycheck As String = "select * from sub_po where po_no=" & po_pre_production & " and line='" & cb_lineproduction.Text & "'"
                Dim dtCheck As DataTable = Database.GetData(querycheck)
                If dtCheck.Rows.Count > 0 Then
                    MessageBox.Show("PO and Line exist")
                Else
                    Dim queryCheckQty As String = "select mfg.material_part_number,mfg.qty_need,s.stock from pre_production pp, master_finish_goods mfg, stock s where pp.no_po=" & txt_pra_po_no.Text & " and mfg.part_number=pp.fg_pn and mfg.material_part_number is not null and s.part_number=mfg.material_part_number"
                    Dim dtCheckQty As DataTable = Database.GetData(queryCheckQty)

                    For i As Integer = 0 To dtCheckQty.Rows.Count - 1
                        Dim stock_for_prod As Integer = 0
                        stock_for_prod = Int(dtCheckQty.Rows(i).Item("qty_need")) * Int(qty_sub_po.Text)

                        If Int(dtCheckQty.Rows(i).Item("stock")) < stock_for_prod Then
                            MessageBox.Show("Stock " & dtCheckQty.Rows(i).Item("material_part_number").ToString & " not enough")
                            Exit Sub
                        End If
                    Next

                    Try
                        Dim sql As String = "INSERT INTO SUB_PO(po_no,qty,line,current_qty) VALUES (" & txt_pra_po_no.Text & "," & qty_sub_po.Text & ",'" & cb_lineproduction.Text & "'," & qty_sub_po.Text & ")"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        If cmd.ExecuteNonQuery() Then
                            curr_qty_sub_po = Int(curr_qty_sub_po) - Int(qty_sub_po.Text)
                            Dim sqlUpdate As String = "update pre_production set current_qty=" & curr_qty_sub_po & "  where no_po=" & po_pre_production & " and fg_pn=" & fg_pn_pre_production
                            Dim cmdUpdate = New SqlCommand(sqlUpdate, Database.koneksi)
                            cmdUpdate.ExecuteNonQuery()

                            For i As Integer = 0 To dtCheckQty.Rows.Count - 1
                                Dim stock_for_prod_stock As Integer = 0
                                stock_for_prod_stock = Int(dtCheckQty.Rows(i).Item("qty_need")) * Int(qty_sub_po.Text)

                                Dim sqlUpdateStock As String = "update stock set stock=" & Int(dtCheckQty.Rows(i).Item("stock")) - stock_for_prod_stock & ", datetime_update=getdate() where part_number='" & dtCheckQty.Rows(i).Item("material_part_number") & "'"
                                Dim cmdUpdateStock = New SqlCommand(sqlUpdateStock, Database.koneksi)
                                cmdUpdateStock.ExecuteNonQuery()
                            Next

                            txt_pra_qty_po.Text = curr_qty_sub_po

                            'Dim praProdForm = New PraProduction()
                            'praProdForm.DGV_PraProduction()

                            qty_sub_po.Text = ""
                            cb_lineproduction.Text = ""
                            DGV_SubPO()
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error Insert" & ex.Message)
                    End Try

                End If
            Else
                MessageBox.Show("Qty more than master Qty")
            End If
        End If
    End Sub

    Sub tampilDataComboboxLine()
        cb_lineproduction.Items.Clear()
        cb_lineproduction.Items.Add("Line 1")
        cb_lineproduction.Items.Add("Line 2")
        cb_lineproduction.Items.Add("Line 3")

        cb_lineproduction.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cb_lineproduction.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub PreProduction2_Load(sender As Object, e As EventArgs) Handles Me.Load
        curr_qty_sub_po = curr_qty_pre_production
        tampilDataComboboxLine()
        DGV_SubPO()
        txt_pra_po_no.Text = po_pre_production
        txt_pra_qty_po.Text = curr_qty_sub_po
        fg_pn.Text = fg_pn_pre_production
    End Sub

    Private Sub DGV_SubPO()
        dgv_pra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_pra.DataSource = Nothing
        dgv_pra.Rows.Clear()
        dgv_pra.Columns.Clear()
        Call Database.koneksi_database()
        Dim query As String = "select NUMBER DB,PO_NO,QTY,LINE,CURRENT_QTY CURR_QTY from sub_po where po_no=" & po_pre_production
        Dim dtsubPO As DataTable = Database.GetData(query)

        Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
        check.Name = "check"
        check.HeaderText = "Check"
        check.Width = 100
        check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgv_pra.Columns.Insert(0, check)

        dgv_pra.DataSource = dtsubPO

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        dgv_pra.Columns.Insert(6, delete)

        For i As Integer = 0 To dgv_pra.RowCount - 1
            If dgv_pra.Rows(i).Index Mod 2 = 0 Then
                dgv_pra.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_pra.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub dgv_pra_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_pra.CellClick
        If e.ColumnIndex = 6 Then
            Dim result = MessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Call Database.koneksi_database()
                Try
                    Dim sql As String = "delete from sub_po where number=" & dgv_pra.Rows(e.RowIndex).Cells(1).Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    If cmd.ExecuteNonQuery() > 0 Then
                        curr_qty_sub_po = Int(curr_qty_sub_po) + Int(dgv_pra.Rows(e.RowIndex).Cells(3).Value)
                        Dim sqlUpdate As String = "update pre_production set current_qty=" & curr_qty_sub_po & "  where no_po=" & po_pre_production & " and fg_pn=" & fg_pn_pre_production
                        Dim cmdUpdate = New SqlCommand(sqlUpdate, Database.koneksi)
                        If cmdUpdate.ExecuteNonQuery() Then
                            txt_pra_qty_po.Text = curr_qty_sub_po

                            Dim queryCheckQty As String = "select mfg.material_part_number,mfg.qty_need,s.stock from pre_production pp, master_finish_goods mfg, stock s where pp.no_po=" & txt_pra_po_no.Text & " and mfg.part_number=pp.fg_pn and mfg.material_part_number is not null and s.part_number=mfg.material_part_number"
                            Dim dtCheckQty As DataTable = Database.GetData(queryCheckQty)

                            For i As Integer = 0 To dtCheckQty.Rows.Count - 1
                                Dim stock_for_prod_stock As Integer = 0
                                stock_for_prod_stock = Int(dtCheckQty.Rows(i).Item("qty_need")) * Int(dgv_pra.Rows(e.RowIndex).Cells(3).Value)

                                Dim sqlUpdateStock As String = "update stock set stock=" & stock_for_prod_stock & ", datetime_update=getdate()  where part_number='" & dtCheckQty.Rows(i).Item("material_part_number") & "'"
                                Dim cmdUpdateStock = New SqlCommand(sqlUpdateStock, Database.koneksi)
                                cmdUpdateStock.ExecuteNonQuery()
                            Next
                        End If

                        'Dim praProdForm = New PraProduction()
                        'praProdForm.DGV_PraProduction()

                        DGV_SubPO()
                        MessageBox.Show("Delete Success.")
                    End If
                Catch ex As Exception
                    MessageBox.Show("Delete Failed" & ex.Message)
                End Try
            End If
        End If

        If e.ColumnIndex = 0 Then
            If dgv_pra.Rows(e.RowIndex).Cells(0).Value = True Then
                dgv_pra.Rows(e.RowIndex).Cells(0).Value = False
            Else
                dgv_pra.Rows(e.RowIndex).Cells(0).Value = True
            End If
        End If
    End Sub

    Private Sub SubPO_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        'Dim praProd = New PraProduction()

        'praProd.Show()
    End Sub
End Class