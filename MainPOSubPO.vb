Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports ZXing

Public Class MainPOSubPO
    Private Sub MainPOSubPO_Load(sender As Object, e As EventArgs) Handles Me.Load
        tampilDataComboBox()

        DGV_MainPO_All()

        Button6.Enabled = False
    End Sub

    Sub tampilDataComboBox()
        Call Database.koneksi_database()
        Dim dtMasterProcessFlow As DataTable = Database.GetData("select distinct MASTER_FINISH_GOODS_PN from MASTER_PROCESS_FLOW where MASTER_PROCESS IS NOT NULL order by MASTER_FINISH_GOODS_PN")

        ComboBox1.DataSource = dtMasterProcessFlow
        ComboBox1.DisplayMember = "MASTER_FINISH_GOODS_PN"
        ComboBox1.ValueMember = "MASTER_FINISH_GOODS_PN"
        ComboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ComboBox1.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text <> "" And ComboBox1.Text <> "" Then
            Dim sql As String = "select * from main_po where po='" & TextBox1.Text & "' and fg_pn='" & ComboBox1.Text & "'"
            Dim dtMainPO As DataTable = Database.GetData(sql)

            If dtMainPO.Rows.Count > 0 Then
                TextBox5.Text = dtMainPO.Rows.Count
                DGV_MainPO_Spesific()
            Else
                MessageBox.Show("PO & FG Part Number doesn't exist")
            End If
        Else
            MessageBox.Show("Please fill PO Number & FG Part Number")
        End If
    End Sub

    Sub DGV_MainPO_Spesific()
        Dim sql As String = "select ID, PO, FG_PN,SUB_PO,SUB_PO_QTY,STATUS,BALANCE,ACTUAL_QTY from main_po where po='" & TextBox1.Text & "' and fg_pn='" & ComboBox1.Text & "'"
        Dim dtMainPO As DataTable = Database.GetData(sql)
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Call Database.koneksi_database()
        DataGridView1.DataSource = dtMainPO

        DataGridView1.Columns(0).Width = 100
        DataGridView1.Columns(2).Width = 200
        DataGridView1.Columns(5).Width = 100
        DataGridView1.Columns(6).Width = 200
        DataGridView1.Columns(7).Width = 200


        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 50
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True

        DataGridView1.Columns.Insert(8, delete)

        Dim sub_sub_po As DataGridViewButtonColumn = New DataGridViewButtonColumn
        sub_sub_po.Name = "subsubpo"
        sub_sub_po.HeaderText = "Sub-Sub PO"
        sub_sub_po.Width = 50
        sub_sub_po.Text = "Create"
        sub_sub_po.UseColumnTextForButtonValue = True

        DataGridView1.Columns.Insert(0, sub_sub_po)

        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Sub DGV_MainPO_All()
        Dim sql As String = "select ID, PO, FG_PN,SUB_PO,SUB_PO_QTY,STATUS,BALANCE,ACTUAL_QTY from main_po"
        Dim dtMainPO As DataTable = Database.GetData(sql)
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Call Database.koneksi_database()
        DataGridView1.DataSource = dtMainPO

        DataGridView1.Columns(0).Width = 100
        DataGridView1.Columns(2).Width = 200
        DataGridView1.Columns(5).Width = 100
        DataGridView1.Columns(6).Width = 200
        DataGridView1.Columns(7).Width = 200

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 50
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True

        DataGridView1.Columns.Insert(8, delete)

        Dim sub_sub_po As DataGridViewButtonColumn = New DataGridViewButtonColumn
        sub_sub_po.Name = "subsubpo"
        sub_sub_po.HeaderText = "Sub-Sub PO"
        sub_sub_po.Width = 50
        sub_sub_po.Text = "Create"
        sub_sub_po.UseColumnTextForButtonValue = True

        DataGridView1.Columns.Insert(0, sub_sub_po)

        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Sub DGV_MainPO_JustPO()
        Dim sql As String = "select ID, PO, FG_PN,SUB_PO,SUB_PO_QTY,STATUS,BALANCE,ACTUAL_QTY from main_po where po='" & TextBox1.Text & "'"
        Dim dtMainPO As DataTable = Database.GetData(sql)
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Call Database.koneksi_database()
        DataGridView1.DataSource = dtMainPO

        DataGridView1.Columns(0).Width = 100
        DataGridView1.Columns(2).Width = 200
        DataGridView1.Columns(5).Width = 100
        DataGridView1.Columns(6).Width = 200
        DataGridView1.Columns(7).Width = 200

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 50
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True

        DataGridView1.Columns.Insert(8, delete)

        Dim sub_sub_po As DataGridViewButtonColumn = New DataGridViewButtonColumn
        sub_sub_po.Name = "subsubpo"
        sub_sub_po.HeaderText = "Sub-Sub PO"
        sub_sub_po.Width = 50
        sub_sub_po.Text = "Create"
        sub_sub_po.UseColumnTextForButtonValue = True

        DataGridView1.Columns.Insert(0, sub_sub_po)

        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sql As String = "select count(*) from main_po where po='" & TextBox1.Text & "'"
        Dim dtMainPOCount As DataTable = Database.GetData(sql)
        Dim sub_po = TextBox1.Text & "-" & dtMainPOCount.Rows(0).Item(0) + 1

        Dim sqlCheck As String = "select * from main_po where po='" & TextBox1.Text & "' and fg_pn='" & ComboBox1.Text & "'"
        Dim dtMainPOCheck As DataTable = Database.GetData(sqlCheck)

        If dtMainPOCheck.Rows.Count > 0 Then
            MessageBox.Show("PO & FG Part Number already in DB")
            DGV_MainPO_Spesific()
            'TextBox1.Text = ""
            'TextBox3.Text = ""
            'ComboBox1.Text = ""
            'ComboBox2.Text = ""
        Else
            Dim sqlInsertMainPOSubPO As String = "INSERT INTO main_po (po, sub_po, sub_po_qty, fg_pn, status, balance, actual_qty)
                                    VALUES ('" & TextBox1.Text & "','" & sub_po & "'," & TextBox3.Text & ",'" & ComboBox1.Text & "','Open',0,0)"
            Dim cmdInsertMainPOSubPO = New SqlCommand(sqlInsertMainPOSubPO, Database.koneksi)
            If cmdInsertMainPOSubPO.ExecuteNonQuery() Then
                DGV_MainPO_Spesific()
                TextBox1.Text = ""
                TextBox3.Text = ""
                ComboBox1.Text = ""
            End If
        End If
    End Sub

    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete
        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.Columns(e.ColumnIndex).Name = "delete" Then

            Dim sqlcheck As String = "select * from sub_sub_po where main_po='" & DataGridView1.Rows(e.RowIndex).Cells("ID").Value & "'"
            Dim dtMainPOCheck As DataTable = Database.GetData(sqlcheck)
            If dtMainPOCheck.Rows.Count > 0 Then
                MessageBox.Show("Cannot delete this data because Sub Sub PO still exist")
                Exit Sub
            End If

            Dim result = MessageBox.Show("Are you sure to delete?", "Warning", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from main_po where id=" & DataGridView1.Rows(e.RowIndex).Cells("ID").Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    If cmd.ExecuteNonQuery() Then
                        DGV_MainPO_All()
                    End If
                Catch ex As Exception
                    MessageBox.Show("failed" & ex.Message)
                End Try
            End If
        End If

        If DataGridView1.Columns(e.ColumnIndex).Name = "subsubpo" Then
            If DataGridView1.Rows(e.RowIndex).Cells("STATUS").Value = "Open" Then
                Dim sqlGetName As String = "select * from master_finish_goods where fg_part_number='" & DataGridView1.Rows(e.RowIndex).Cells("FG_PN").Value & "'"
                Dim dtGetName As DataTable = Database.GetData(sqlGetName)

                TabControl1.SelectedTab = TabPage2
                TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells("SUB_PO").Value
                TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells("SUB_PO_QTY").Value
                TextBox8.Text = DataGridView1.Rows(e.RowIndex).Cells("PO").Value
                TextBox9.Text = DataGridView1.Rows(e.RowIndex).Cells("FG_PN").Value
                TextBox12.Text = DataGridView1.Rows(e.RowIndex).Cells("ID").Value
                TextBox18.Text = dtGetName.Rows(0).Item("description").ToString
                DGV_SubSubPO()
            Else
                MessageBox.Show("Cannot create Sub Sub PO because status PO already close.")
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text <> "" Then
            Dim sql As String = "select * from main_po where po='" & TextBox1.Text & "'"
            Dim dtMainPO As DataTable = Database.GetData(sql)

            If dtMainPO.Rows.Count > 0 Then
                TextBox5.Text = dtMainPO.Rows.Count
                DGV_MainPO_JustPO()
            Else
                MessageBox.Show("PO & FG Part Number doesn't exist")
            End If
        Else
            MessageBox.Show("Please fill PO Number")
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Call Database.koneksi_database()
        If DataGridView1.Columns(e.ColumnIndex).Name = "SUB_PO_QTY" Then

            Dim sqlcheck As String = "select * from main_po where po='" & DataGridView1.Rows(e.RowIndex).Cells("PO").Value & "' and (balance > 0 or actual_qty > 0)"
            Dim dtMainPOCheck As DataTable = Database.GetData(sqlcheck)
            If dtMainPOCheck.Rows.Count > 0 Then
                MessageBox.Show("Cannot update this data")
                DGV_MainPO_All()
                Exit Sub
            End If

            Try
                Dim Sql As String = "update main_po set SUB_PO_QTY=" & DataGridView1.Rows(e.RowIndex).Cells("SUB_PO_QTY").Value & " where ID=" & DataGridView1.Rows(e.RowIndex).Cells("ID").Value
                Dim cmd = New SqlCommand(Sql, Database.koneksi)
                If cmd.ExecuteNonQuery() Then
                    MessageBox.Show("Success updated data")
                End If
            Catch ex As Exception
                MessageBox.Show("Failed" & ex.Message)
            End Try

        End If
    End Sub



    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim isOpen As Boolean = False

        For i = 0 To DataGridView2.Rows.Count - 1
            If DataGridView2.Rows(i).Cells(4).Value = "Open" Then
                isOpen = True
            End If
        Next

        If isOpen = False Then
            If Convert.ToInt32(TextBox10.Text) > Convert.ToInt32(TextBox4.Text) Then
                MessageBox.Show("Sorry Plan Qty Sub Sub PO more than Qty Sub PO")
                TextBox10.Text = ""
                Exit Sub
            End If

            Dim sql As String = "select * from sub_sub_po where main_po=" & TextBox12.Text & " and line='" & ComboBox3.Text & "' and status='Open'"
            Dim dtSubSubPOCount As DataTable = Database.GetData(sql)
            Dim sub_sub_po = TextBox2.Text & "-" & dtSubSubPOCount.Rows.Count + 1

            If dtSubSubPOCount.Rows.Count > 0 Then
                MessageBox.Show("PO & Line already exists in DB and status still Open")
                DGV_SubSubPO()
                TextBox10.Text = ""
                ComboBox3.Text = ""
            Else
                Dim sqlSum As String = "select COALESCE(sum(sub_sub_po_qty),0) from sub_sub_po where main_po=" & TextBox12.Text
                Dim dtSubSubPOSum As DataTable = Database.GetData(sqlSum)

                If Convert.ToInt32(TextBox4.Text) < Convert.ToInt32(dtSubSubPOSum.Rows(0).Item(0)) + Convert.ToInt32(TextBox10.Text) Then
                    MessageBox.Show("All Sub Sub PO Qty more then Sub Qty")
                    TextBox10.Text = ""
                    ComboBox3.Text = ""
                    Exit Sub
                End If

                Dim sqlInsertSubPO As String = "INSERT INTO sub_sub_po (main_po, sub_sub_po, sub_sub_po_qty, line, status, actual_qty)
                                    VALUES (" & TextBox12.Text & ",'" & sub_sub_po & "'," & TextBox10.Text & ",'" & ComboBox3.Text & "','Open',0)"

                Dim cmdInsertSubPO = New SqlCommand(sqlInsertSubPO, Database.koneksi)
                If cmdInsertSubPO.ExecuteNonQuery() Then
                    DGV_SubSubPO()
                    Insert_Prod_DOC(TextBox9.Text, sub_sub_po, TextBox8.Text)
                    Insert_Prod_DOP(TextBox9.Text, sub_sub_po, TextBox8.Text)
                    TextBox10.Text = ""
                    ComboBox3.Text = ""
                End If
            End If
        Else
            MessageBox.Show("Sorry The line have Open PO")
            ComboBox3.Text = ""
            TextBox10.Text = ""
        End If
    End Sub

    Sub Insert_Prod_DOC(fg As String, sub_sub_po As String, po As String)
        Dim queryProdDOC As String = "select mp.po,sp.Sub_Sub_PO,mp.fg_pn,mufg.component,mufg.description,mufg.usage,mufg.family
        from sub_sub_po sp,main_po mp,material_usage_finish_goods mufg 
        where sp.main_po= mp.id AND mufg.fg_part_number= mp.fg_pn AND sp.status= 'Open' and line = '" & ComboBox3.Text & "' and mp.fg_pn = '" & fg & "' and sp.sub_sub_po='" & sub_sub_po & "' order by sp.sub_sub_po"
        Dim dtProdDOC As DataTable = Database.GetData(queryProdDOC)

        If dtProdDOC.Rows.Count > 0 Then
            For i As Integer = 0 To dtProdDOC.Rows.Count - 1
                Dim queryCheckProdDOC As String = "select * from prod_doc where sub_sub_po= '" & sub_sub_po & "' AND fg_pn = '" & fg & "' AND component='" & dtProdDOC.Rows(i).Item("component") & "' and line = '" & ComboBox3.Text & "'"
                Dim dtCheckProdDOC As DataTable = Database.GetData(queryCheckProdDOC)
                If dtCheckProdDOC.Rows.Count = 0 Then
                    Dim sqlInsertDOC As String = "INSERT INTO prod_doc (po, sub_sub_po, fg_pn, component, desc_comp, family, usage, line)
                                    VALUES ('" & po & "','" & sub_sub_po & "','" & dtProdDOC.Rows(i).Item("fg_pn") & "','" & dtProdDOC.Rows(i).Item("component") & "','" & dtProdDOC.Rows(i).Item("description") & "','" & dtProdDOC.Rows(i).Item("family") & "'," & dtProdDOC.Rows(i).Item("usage").ToString.Replace(",", ".") & ",'" & ComboBox3.Text & "')"
                    Dim cmdInsertDOC = New SqlCommand(sqlInsertDOC, Database.koneksi)
                    cmdInsertDOC.ExecuteNonQuery()
                End If
            Next
        End If
    End Sub

    Sub Insert_Prod_DOP(fg As String, sub_sub_po As String, po As String)
        Dim queryProdDOP As String = "select mp.po,sp.Sub_Sub_PO,mp.fg_pn,mpf.master_process,mpn.[order]
        from sub_sub_po sp,main_po mp,MASTER_PROCESS_FLOW MPF, master_process_number mpn 
        where sp.main_po = mp.id AND mpf.MASTER_FINISH_GOODS_PN = mp.fg_pn AND sp.status= 'Open' and line = '" & ComboBox3.Text & "' and mp.fg_pn = '" & fg & "' and sp.sub_sub_po='" & sub_sub_po & "' and mpn.process_name=mpf.master_process_number and master_process is not null order by sp.sub_sub_po"
        Dim dtProdDOP As DataTable = Database.GetData(queryProdDOP)
        If dtProdDOP.Rows.Count > 0 Then
            For i As Integer = 0 To dtProdDOP.Rows.Count - 1
                Dim queryCheckProdDOP As String = "select * from prod_dop where sub_sub_po= '" & sub_sub_po & "' AND fg_pn = '" & fg & "' AND process='" & dtProdDOP.Rows(i).Item("master_process") & "' and line = '" & ComboBox3.Text & "'"
                Dim dtCheckProdDOP As DataTable = Database.GetData(queryCheckProdDOP)
                If dtCheckProdDOP.Rows.Count = 0 Then
                    Dim sqlInsertDOP As String = "INSERT INTO prod_dop (po, sub_sub_po, fg_pn, process, line, process_number)
                                    VALUES ('" & po & "','" & sub_sub_po & "','" & dtProdDOP.Rows(i).Item("fg_pn") & "','" & dtProdDOP.Rows(i).Item("master_process") & "','" & ComboBox3.Text & "'," & dtProdDOP.Rows(i).Item("order") & ")"
                    Dim cmdInsertDOP = New SqlCommand(sqlInsertDOP, Database.koneksi)
                    cmdInsertDOP.ExecuteNonQuery()
                End If
            Next
        End If
    End Sub

    Private Sub TabControl1_Enter(sender As Object, e As EventArgs) Handles TabControl1.Enter
        If TabControl1.SelectedTab.TabIndex = 1 Then
            DGV_SubSubPO()
        End If
    End Sub

    Sub DGV_SubSubPO()
        Dim sql As String = "select ID, SUB_SUB_PO, SUB_SUB_PO_QTY,LINE,STATUS,ACTUAL_QTY,YIELD_LOSE from SUB_SUB_PO where MAIN_PO=" & TextBox12.Text
        Dim dtSubSubPO As DataTable = Database.GetData(sql)
        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView2.DataSource = Nothing
        DataGridView2.Rows.Clear()
        DataGridView2.Columns.Clear()
        Call Database.koneksi_database()
        DataGridView2.DataSource = dtSubSubPO

        Dim oper As DataGridViewButtonColumn = New DataGridViewButtonColumn
        oper.Name = "operator"
        oper.HeaderText = "Operator"
        oper.Width = 50
        oper.Text = "Add / Change"
        oper.UseColumnTextForButtonValue = True

        DataGridView2.Columns.Insert(7, oper)

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 50
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True

        DataGridView2.Columns.Insert(8, delete)

        For i As Integer = 0 To DataGridView2.RowCount - 1
            If DataGridView2.Rows(i).Index Mod 2 = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        Dim sqlSum As String = "select sum(sub_sub_po_qty) from sub_sub_po where main_po=" & TextBox12.Text
        Dim dtSubSubPOSum As DataTable = Database.GetData(sqlSum)
        TextBox11.Text = dtSubSubPOSum.Rows(0).Item(0).ToString
    End Sub

    Sub DGV_SubSubPO_FILTERlINE()
        Dim sql As String = "select ID, SUB_SUB_PO, SUB_SUB_PO_QTY,LINE,STATUS,ACTUAL_QTY,YIELD_LOSE from SUB_SUB_PO where line='" & ComboBox3.Text & "'"
        Dim dtSubSubPO As DataTable = Database.GetData(sql)
        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView2.DataSource = Nothing
        DataGridView2.Rows.Clear()
        DataGridView2.Columns.Clear()
        Call Database.koneksi_database()
        DataGridView2.DataSource = dtSubSubPO

        Dim oper As DataGridViewButtonColumn = New DataGridViewButtonColumn
        oper.Name = "operator"
        oper.HeaderText = "Operator"
        oper.Width = 50
        oper.Text = "Add / Change"
        oper.UseColumnTextForButtonValue = True

        DataGridView2.Columns.Insert(7, oper)

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 50
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True

        DataGridView2.Columns.Insert(8, delete)

        For i As Integer = 0 To DataGridView2.RowCount - 1
            If DataGridView2.Rows(i).Index Mod 2 = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        Dim sqlSum As String = "select sum(sub_sub_po_qty) from sub_sub_po where main_po=" & TextBox12.Text
        Dim dtSubSubPOSum As DataTable = Database.GetData(sqlSum)
        TextBox11.Text = dtSubSubPOSum.Rows(0).Item(0).ToString
    End Sub

    Private Sub DataGridView2_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView2.DataBindingComplete
        For i As Integer = 0 To DataGridView2.RowCount - 1
            If DataGridView2.Rows(i).Index Mod 2 = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If DataGridView2.Columns(e.ColumnIndex).Name = "delete" Then
            Dim sqlcheck As String = "select * from SUB_SUB_PO where id='" & DataGridView2.Rows(e.RowIndex).Cells("ID").Value & "' and actual_qty > 0"
            Dim dtMainPOCheck As DataTable = Database.GetData(sqlcheck)
            If dtMainPOCheck.Rows.Count > 0 Then
                MessageBox.Show("Cannot delete this data because actual qty more than zero")
                Exit Sub
            End If

            Dim sqlcheckInputMiniStore As String = "select * from stock_prod_material where sub_sub_po='" & DataGridView2.Rows(e.RowIndex).Cells("SUB_SUB_PO").Value & "' and LINE = '" & DataGridView2.Rows(e.RowIndex).Cells("LINE").Value & "'"
            Dim dtInputMiniStore As DataTable = Database.GetData(sqlcheckInputMiniStore)
            If dtInputMiniStore.Rows.Count > 0 Then
                MessageBox.Show("Cannot delete this data because Ministore already provides raw material")
                Exit Sub
            End If

            Dim result = MessageBox.Show("Are you sure to delete?", "Warning", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                Try
                    Dim sqlDeleteDOP As String = "delete from prod_dop where sub_sub_po='" & DataGridView2.Rows(e.RowIndex).Cells("SUB_SUB_PO").Value & "' and line='" & DataGridView2.Rows(e.RowIndex).Cells("LINE").Value & "'"
                    Dim cmdDeleteDOP = New SqlCommand(sqlDeleteDOP, Database.koneksi)
                    cmdDeleteDOP.ExecuteNonQuery()

                    Dim sqlDeleteDOC As String = "delete from prod_doc where sub_sub_po='" & DataGridView2.Rows(e.RowIndex).Cells("SUB_SUB_PO").Value & "' and line='" & DataGridView2.Rows(e.RowIndex).Cells("LINE").Value & "'"
                    Dim cmdDeleteDOC = New SqlCommand(sqlDeleteDOC, Database.koneksi)
                    cmdDeleteDOC.ExecuteNonQuery()

                    Dim sql As String = "delete from SUB_SUB_PO where id=" & DataGridView2.Rows(e.RowIndex).Cells("ID").Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    If cmd.ExecuteNonQuery() Then
                        DGV_SubSubPO()
                    End If
                Catch ex As Exception
                    MessageBox.Show("failed" & ex.Message)
                End Try
            End If
        End If

        If DataGridView2.Columns(e.ColumnIndex).Name = "operator" Then
            If DataGridView2.Rows(e.RowIndex).Cells("STATUS").Value = "Open" Then
                Dim sqlGetName As String = "select * from master_finish_goods where fg_part_number='" & TextBox9.Text & "'"
                Dim dtGetName As DataTable = Database.GetData(sqlGetName)

                TabControl1.SelectedTab = TabPage3
                TextBox13.Text = TextBox9.Text  'FG PN
                TextBox14.Text = dtGetName.Rows(0).Item("description") 'Description
                TextBox15.Text = TextBox8.Text  'PO
                TextBox16.Text = TextBox2.Text  'Sub PO
                TextBox17.Text = DataGridView2.Rows(e.RowIndex).Cells("SUB_SUB_PO").Value 'Sub Sub PO
                ComboBox2.Text = DataGridView2.Rows(e.RowIndex).Cells("LINE").Value
                DGV_Add_Change_Operator()
            Else
                MessageBox.Show("Status PO already close. Please select another Sub Sub PO.")
            End If
        End If
    End Sub

    Sub DGV_Add_Change_Operator()
        'Dim queryDOP As String = "select dp.process_number Number,dp.process Process, dp.operator_id from prod_dop dp, sub_sub_po ssp where dp.line='" & ComboBox2.Text & "' and ssp.sub_sub_po=dp.sub_sub_po and ssp.status='Open' order by dp.process_number"
        Dim queryDOP As String = "select mp.po,mp.sub_po,mp.fg_pn,mfg.description,ssp.sub_sub_po,dp.process_number Number,dp.process Process, dp.operator_id,dp.operator_id2,dp.REMARK,ssp.sub_sub_po_qty,mfg.spq from prod_dop dp, sub_sub_po ssp,main_po mp,master_finish_goods mfg where dp.line='" & ComboBox2.Text & "' and ssp.line=dp.line and ssp.sub_sub_po=dp.sub_sub_po and ssp.status='Open' and mp.id=ssp.main_po and mfg.fg_part_number=mp.fg_pn order by dp.process_number"
        Dim dtDOP As DataTable = Database.GetData(queryDOP)
        DataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView3.DataSource = Nothing
        DataGridView3.Rows.Clear()
        DataGridView3.Columns.Clear()
        If dtDOP.Rows.Count > 0 Then

            DataGridView3.DataSource = dtDOP

            DataGridView3.Columns(0).Visible = False
            DataGridView3.Columns(1).Visible = False
            DataGridView3.Columns(2).Visible = False
            DataGridView3.Columns(3).Visible = False
            DataGridView3.Columns(4).Visible = False

            TextBox19.Text = dtDOP.Rows(0).Item("sub_sub_po_qty")
            TextBox20.Text = dtDOP.Rows(0).Item("spq")

            TextBox13.Text = dtDOP.Rows(0).Item("fg_pn")  'FG PN
            TextBox14.Text = dtDOP.Rows(0).Item("description") 'Description
            TextBox15.Text = dtDOP.Rows(0).Item("PO")  'PO
            TextBox16.Text = dtDOP.Rows(0).Item("SUB_PO")  'Sub PO
            TextBox17.Text = dtDOP.Rows(0).Item("SUB_SUB_PO") 'Sub Sub PO

            Dim name As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn
            name.Name = "name"
            name.HeaderText = "Operator Name 1"
            Dim queryUsers As String = "select id_card_no +' - '+ name id_name,name from users order by name"
            Dim dtUsers As DataTable = Database.GetData(queryUsers)
            name.DataSource = dtUsers
            name.DisplayMember = "id_name"
            name.ValueMember = "name"
            DataGridView3.Columns.Insert(7, name)

            Dim name2 As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn
            name2.Name = "name2"
            name2.HeaderText = "Operator Name 2" 
            name2.DataSource = dtUsers
            name2.DisplayMember = "id_name"
            name2.ValueMember = "name"
            DataGridView3.Columns.Insert(8, name2)

            DataGridView3.Columns(11).ReadOnly = True

            For rowDataSet As Integer = 0 To dtDOP.Rows.Count - 1
                DataGridView3.Rows(rowDataSet).Cells(7).Value = dtDOP.Rows(rowDataSet).Item("operator_id").ToString
            Next

            For rowDataSet As Integer = 0 To dtDOP.Rows.Count - 1
                DataGridView3.Rows(rowDataSet).Cells(8).Value = dtDOP.Rows(rowDataSet).Item("operator_id2").ToString
                DataGridView3.Rows(rowDataSet).Cells(11).ReadOnly = False
            Next

            For i As Integer = 0 To DataGridView3.RowCount - 1
                If DataGridView3.Rows(i).Index Mod 2 = 0 Then
                    DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next i

            DataGridView3.Columns(9).Visible = False
            DataGridView3.Columns(10).Visible = False
            DataGridView3.Columns(12).Visible = False
            DataGridView3.Columns(13).Visible = False
        End If
    End Sub

    Private Sub DataGridView2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellValueChanged
        Call Database.koneksi_database()
        If DataGridView2.Columns(e.ColumnIndex).Name = "SUB_SUB_PO_QTY" Then

            Dim sqlcheck As String = "select * from SUB_SUB_PO where ID='" & DataGridView2.Rows(e.RowIndex).Cells("ID").Value & "' and actual_qty > 0"
            Dim dtMainPOCheck As DataTable = Database.GetData(sqlcheck)
            If dtMainPOCheck.Rows.Count > 0 Then
                MessageBox.Show("Cannot update this data")
                DGV_SubSubPO()
                Exit Sub
            End If

            Try
                Dim Sql As String = "update SUB_SUB_PO set SUB_SUB_PO_QTY=" & DataGridView2.Rows(e.RowIndex).Cells("SUB_PO_QTY").Value & " where ID=" & DataGridView2.Rows(e.RowIndex).Cells("ID").Value
                Dim cmd = New SqlCommand(Sql, Database.koneksi)
                If cmd.ExecuteNonQuery() Then
                    MessageBox.Show("Success updated data")
                End If
            Catch ex As Exception
                MessageBox.Show("Failed" & ex.Message)
            End Try
        End If

        If DataGridView2.Columns(e.ColumnIndex).Name = "YIELD_LOSE" Then
            Try
                Dim Sql As String = "update SUB_SUB_PO set YIELD_LOSE=" & DataGridView2.Rows(e.RowIndex).Cells("YIELD_LOSE").Value & " where ID=" & DataGridView2.Rows(e.RowIndex).Cells("ID").Value
                Dim cmd = New SqlCommand(Sql, Database.koneksi)
                If cmd.ExecuteNonQuery() Then
                    MessageBox.Show("Success updated data")
                End If
            Catch ex As Exception
                MessageBox.Show("Failed" & ex.Message)
            End Try
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox1.Text = ""
        TextBox3.Text = ""
        TextBox5.Text = ""
        DGV_MainPO_All()
    End Sub

    Private Sub DataGridView3_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView3.EditingControlShowing
        If DataGridView3.Columns(DataGridView3.CurrentCell.ColumnIndex).Name = "name" Then
            Dim combo As DataGridViewComboBoxEditingControl = CType(e.Control, DataGridViewComboBoxEditingControl)

            If (combo IsNot Nothing) Then
                RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf Combo_SelectionChangeCommitted)

                AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf Combo_SelectionChangeCommitted)
            End If
        End If

        If DataGridView3.Columns(DataGridView3.CurrentCell.ColumnIndex).Name = "name2" Then
            Dim combo As DataGridViewComboBoxEditingControl = CType(e.Control, DataGridViewComboBoxEditingControl)

            If (combo IsNot Nothing) Then
                RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf Combo_SelectionChangeCommitted2)

                AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf Combo_SelectionChangeCommitted2)
            End If
        End If
    End Sub

    Private Sub Combo_SelectionChangeCommitted(ByVal sender As Object, ByVal e As EventArgs)
        Dim combo As DataGridViewComboBoxEditingControl = CType(sender, DataGridViewComboBoxEditingControl)
        Dim Sql As String = "update prod_dop set operator_id='" & combo.SelectedValue & "' where po='" & TextBox15.Text & "' and sub_sub_po='" & TextBox17.Text & "' and line='" & ComboBox2.Text & "' and process_number=" & DataGridView3.Rows(DataGridView3.CurrentRow.Index).Cells("Number").Value
        Dim cmd = New SqlCommand(Sql, Database.koneksi)
        If cmd.ExecuteNonQuery() Then
            DGV_Add_Change_Operator()
        End If
    End Sub

    Private Sub Combo_SelectionChangeCommitted2(ByVal sender As Object, ByVal e As EventArgs)
        Dim combo As DataGridViewComboBoxEditingControl = CType(sender, DataGridViewComboBoxEditingControl)
        Dim Sql As String = "update prod_dop set operator_id2='" & combo.SelectedValue & "' where po='" & TextBox15.Text & "' and sub_sub_po='" & TextBox17.Text & "' and line='" & ComboBox2.Text & "' and process_number=" & DataGridView3.Rows(DataGridView3.CurrentRow.Index).Cells("Number").Value
        Dim cmd = New SqlCommand(Sql, Database.koneksi)
        If cmd.ExecuteNonQuery() Then
            DGV_Add_Change_Operator()
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If Val(TextBox19.Text) <= Val(TextBox20.Text) Then
            MessageBox.Show("Print 1 of 1 Flow Ticket")
            Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, lot, of_lot, sub_sub_po)
                                    VALUES ('" & TextBox15.Text & "','" & TextBox13.Text & "','" & ComboBox2.Text & "',1,1,'" & TextBox17.Text & "')"
            Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
            cmdInsertPrintingRecord.ExecuteNonQuery()

            For i = 1 To Val(TextBox21.Text)
                MessageBox.Show("Print + " & i & " Flow Ticket")
                Dim sqlInsertPrintingRecordAdditional As String = "INSERT INTO record_printing (po, fg, line, lot, of_lot, remark, sub_sub_po)
                                    VALUES ('" & TextBox15.Text & "','" & TextBox13.Text & "','" & ComboBox2.Text & "'," & i & ",0,'Additional','" & TextBox17.Text & "')"
                Dim cmdInsertPrintingRecordAdditional = New SqlCommand(sqlInsertPrintingRecordAdditional, Database.koneksi)
                cmdInsertPrintingRecordAdditional.ExecuteNonQuery()
            Next
        Else
            If Val(TextBox19.Text) Mod Val(TextBox20.Text) = 0 Then
                For i = 1 To Val(TextBox19.Text) / Val(TextBox20.Text)
                    Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, lot, of_lot, sub_sub_po)
                                    VALUES ('" & TextBox15.Text & "','" & TextBox13.Text & "','" & ComboBox2.Text & "'," & i & "," & Val(TextBox19.Text) / Val(TextBox20.Text) & ",'" & TextBox17.Text & "')"
                    Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                    cmdInsertPrintingRecord.ExecuteNonQuery()

                    MessageBox.Show("Print " & i & " of " & Val(TextBox19.Text) / Val(TextBox20.Text) & " Label Flow Ticket")
                Next
            Else
                For i = 1 To Math.Floor(Val(TextBox19.Text) / Val(TextBox20.Text)) + 1
                    Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, lot, of_lot, sub_sub_po)
                                    VALUES ('" & TextBox15.Text & "','" & TextBox13.Text & "','" & ComboBox2.Text & "'," & i & "," & Math.Floor(Val(TextBox19.Text) / Val(TextBox20.Text)) + 1 & ",'" & TextBox17.Text & "')"
                    Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                    cmdInsertPrintingRecord.ExecuteNonQuery()

                    MessageBox.Show("Print " & i & " of " & Math.Floor(Val(TextBox19.Text) / Val(TextBox20.Text)) + 1 & " Label Flow Ticket")
                Next
            End If

            For i = 1 To Val(TextBox21.Text)
                MessageBox.Show("Print + " & i & " Flow Ticket")
                Dim sqlInsertPrintingRecordAdditional As String = "INSERT INTO record_printing (po, fg, line, lot, of_lot, remark, sub_sub_po)
                                    VALUES ('" & TextBox15.Text & "','" & TextBox13.Text & "','" & ComboBox2.Text & "'," & i & ",0,'Additional','" & TextBox17.Text & "')"
                Dim cmdInsertPrintingRecordAdditional = New SqlCommand(sqlInsertPrintingRecordAdditional, Database.koneksi)
                cmdInsertPrintingRecordAdditional.ExecuteNonQuery()
            Next
        End If
    End Sub

    Private Sub ComboBox2_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedValueChanged
        DGV_Add_Change_Operator()
        If DataGridView3.Rows.Count > 0 Then
            Button6.Enabled = True
        Else
            MessageBox.Show("Sorry this line dont have any PO.")
        End If
    End Sub

    Private Sub ComboBox3_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedValueChanged
        DGV_SubSubPO_FILTERlINE()
    End Sub

    Private Sub DataGridView3_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellValueChanged
        If DataGridView3.Columns(e.ColumnIndex).Name = "REMARK" Then
            Dim Sql As String = "update prod_dop set remark='" & DataGridView3.Rows(e.RowIndex).Cells("REMARK").Value & "' where po='" & TextBox15.Text & "' and sub_sub_po='" & TextBox17.Text & "' and line='" & ComboBox2.Text & "' and process_number=" & DataGridView3.Rows(DataGridView3.CurrentRow.Index).Cells("Number").Value
            Dim cmd = New SqlCommand(Sql, Database.koneksi)
            cmd.ExecuteNonQuery()
        End If
    End Sub

    Private Sub DataGridView3_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView3.CellMouseClick
        If DataGridView3.Columns(e.ColumnIndex).Name = "REMARK" Then
            If DataGridView3.Rows(e.RowIndex).Cells("name2").Value = "" Then
                MessageBox.Show("Please fill the Operator Name 2 first")
            End If
        End If
    End Sub
End Class