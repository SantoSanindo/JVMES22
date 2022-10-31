Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class ProductionRequest
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox1.Text <> "" Then
            TreeView3.Nodes.Clear()
            Dim sql As String = "SELECT sp.sub_sub_po,mp.fg_pn FROM sub_sub_po sp,main_po mp WHERE line = '" & ComboBox1.Text & "' And sp.status = 'Open' AND sp.main_po= mp.id"
            Dim dtSubSubPO As DataTable = Database.GetData(sql)

            If dtSubSubPO.Rows.Count > 0 Then
                For i = 0 To dtSubSubPO.Rows.Count - 1
                    TreeView3.Nodes.Add("Sub Sub PO : " & dtSubSubPO.Rows(i).Item("SUB_SUB_PO").ToString)
                    Dim level2 = TreeView3.Nodes(i).Nodes.Add(dtSubSubPO.Rows(i).Item("FG_PN").ToString, dtSubSubPO.Rows(i).Item("FG_PN").ToString)

                    level2.Nodes.Add("Material", "Material")
                    level2.Nodes.Add("FG", "Finish Goods")
                    level2.Nodes.Add("OnHold", "On Hold")
                    level2.Nodes.Add("WIP", "WIP")
                    level2.Nodes.Add("SubAssy", "Sub Assy")
                Next
            Else
                MessageBox.Show("Cannot find Sub Sub PO for This Line")
            End If
        Else
            MessageBox.Show("Please select Line first")
        End If
    End Sub

    Private Sub TreeView3_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView3.AfterSelect
        If TreeView3.SelectedNode.Name <> "" Then
            DGV_MaterialNeed(TreeView3.SelectedNode.Name)
        End If
    End Sub

    Sub DGV_MaterialNeed(fg As String)
        DataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView3.DataSource = Nothing
        DataGridView3.Rows.Clear()
        DataGridView3.Columns.Clear()
        Call Database.koneksi_database()
        Dim queryMasterFinishGoods As String = "select mp.fg_pn FG_Part_Number,mufg.component Component,mufg.description Description,mufg.usage Usage,sp.sub_sub_po_qty Sub_Sub_Qty,mufg.usage * sp.sub_sub_po_qty as Total_Need
        from sub_sub_po sp,main_po mp,material_usage_finish_goods mufg 
        where sp.main_po= mp.id AND mufg.fg_part_number= mp.fg_pn AND sp.status= 'Open' and line = '" & ComboBox1.Text & "' and mp.fg_pn = '" & fg & "'"
        Dim dtMaterialNeed As DataTable = Database.GetData(queryMasterFinishGoods)

        DataGridView3.DataSource = dtMaterialNeed

        DataGridView3.Columns(0).Width = 200
        DataGridView3.Columns(1).Width = 150
        DataGridView3.Columns(2).Width = 500
        DataGridView3.Columns(3).Width = 150
        DataGridView3.Columns(4).Width = 150
        DataGridView3.Columns(5).Width = 150

        For i As Integer = 0 To DataGridView3.RowCount - 1
            If DataGridView3.Rows(i).Index Mod 2 = 0 Then
                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

End Class