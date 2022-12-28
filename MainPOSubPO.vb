Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports ZXing

Public Class MainPOSubPO
    Private Sub MainPOSubPO_Load(sender As Object, e As EventArgs) Handles Me.Load
        tampilDataComboBox()
        DGV_MainPO_All()
        tampilDataComboBoxLine()
        ComboBox1.SelectedIndex = -1
        ComboBox3.SelectedIndex = -1
    End Sub

    Sub tampilDataComboBox()
        Call Database.koneksi_database()
        Dim dtMasterProcessFlow As DataTable = Database.GetData("select distinct MASTER_FINISH_GOODS_PN from MASTER_PROCESS_FLOW where MASTER_PROCESS IS NOT NULL order by MASTER_FINISH_GOODS_PN")

        ComboBox1.DataSource = dtMasterProcessFlow
        ComboBox1.DisplayMember = "MASTER_FINISH_GOODS_PN"
        ComboBox1.ValueMember = "MASTER_FINISH_GOODS_PN"
    End Sub

    Sub tampilDataComboBoxLine()
        Call Database.koneksi_database()
        Dim dtMasterLine As DataTable = Database.GetData("select NAME from MASTER_LINE where DEPARTEMENT='" & globVar.department & "'")

        ComboBox3.DataSource = dtMasterLine
        ComboBox3.DisplayMember = "NAME"
        ComboBox3.ValueMember = "NAME"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text <> "" And ComboBox1.Text <> "" Then
            Dim sql As String = "select * from main_po where po='" & TextBox1.Text & "' and fg_pn='" & ComboBox1.Text & "' and department='" & globVar.department & "'"
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
        Dim sql As String = "select ID, PO, SUB_PO, SUB_PO_QTY, FG_PN,STATUS,BALANCE,ACTUAL_QTY from main_po where po='" & TextBox1.Text & "' and fg_pn='" & ComboBox1.Text & "' and department='" & globVar.department & "'"
        Dim dtMainPO As DataTable = Database.GetData(sql)
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Call Database.koneksi_database()

        Dim sub_sub_po As DataGridViewButtonColumn = New DataGridViewButtonColumn
        sub_sub_po.Name = "subsubpo"
        sub_sub_po.HeaderText = "Sub-Sub PO"
        sub_sub_po.Width = 50
        sub_sub_po.Text = "Create"
        sub_sub_po.UseColumnTextForButtonValue = True
        DataGridView1.Columns.Insert(0, sub_sub_po)

        DataGridView1.DataSource = dtMainPO

        Dim queryStatus As String = "select status from master_status"
        Dim dtStatus As DataTable = Database.GetData(queryStatus)
        Dim statusDGV As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn
        statusDGV.Name = "statuspo"
        statusDGV.HeaderText = "Status PO"
        statusDGV.DataSource = dtStatus
        statusDGV.DisplayMember = "status"
        statusDGV.ValueMember = "status"
        statusDGV.DataPropertyName = "status"
        DataGridView1.Columns.Add(statusDGV)

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 50
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        DataGridView1.Columns.Add(delete)

        DataGridView1.Columns("status").Visible = False
    End Sub

    Sub DGV_MainPO_All()
        Dim sql As String = "select ID,PO,SUB_PO,SUB_PO_QTY,FG_PN,STATUS,BALANCE,ACTUAL_QTY from main_po where department='" & globVar.department & "'"
        Dim dtMainPO As DataTable = Database.GetData(sql)
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()
        DataGridView1.Rows.Clear()

        Dim sub_sub_po As DataGridViewButtonColumn = New DataGridViewButtonColumn
        sub_sub_po.Name = "subsubpo"
        sub_sub_po.HeaderText = "Sub-Sub PO"
        sub_sub_po.Width = 50
        sub_sub_po.Text = "Create"
        sub_sub_po.UseColumnTextForButtonValue = True
        DataGridView1.Columns.Add(sub_sub_po)

        DataGridView1.DataSource = dtMainPO

        Dim queryStatus As String = "select status from master_status"
        Dim dtStatus As DataTable = Database.GetData(queryStatus)
        Dim statusDGV As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn
        statusDGV.Name = "statuspo"
        statusDGV.HeaderText = "Status PO"
        statusDGV.DataSource = dtStatus
        statusDGV.DisplayMember = "status"
        statusDGV.ValueMember = "status"
        statusDGV.DataPropertyName = "status"
        DataGridView1.Columns.Add(statusDGV)

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 50
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        DataGridView1.Columns.Add(delete)

        DataGridView1.Columns("status").Visible = False
    End Sub

    Sub DGV_MainPO_JustPO()
        Dim sql As String = "select ID, PO, SUB_PO,SUB_PO_QTY,FG_PN,STATUS,BALANCE,ACTUAL_QTY from main_po where po='" & TextBox1.Text & "' and department='" & globVar.department & "'"
        Dim dtMainPO As DataTable = Database.GetData(sql)
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Call Database.koneksi_database()

        Dim sub_sub_po As DataGridViewButtonColumn = New DataGridViewButtonColumn
        sub_sub_po.Name = "subsubpo"
        sub_sub_po.HeaderText = "Sub-Sub PO"
        sub_sub_po.Width = 50
        sub_sub_po.Text = "Create"
        sub_sub_po.UseColumnTextForButtonValue = True

        DataGridView1.DataSource = dtMainPO

        Dim queryStatus As String = "select status from master_status"
        Dim dtStatus As DataTable = Database.GetData(queryStatus)
        Dim statusDGV As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn
        statusDGV.Name = "statuspo"
        statusDGV.HeaderText = "Status PO"
        statusDGV.DataSource = dtStatus
        statusDGV.DisplayMember = "status"
        statusDGV.ValueMember = "status"
        statusDGV.DataPropertyName = "status"
        DataGridView1.Columns.Add(statusDGV)

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 50
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        DataGridView1.Columns.Add(delete)

        DataGridView1.Columns("status").Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sql As String = "select count(*) from main_po where po='" & TextBox1.Text & "' and department='" & globVar.department & "'"
        Dim dtMainPOCount As DataTable = Database.GetData(sql)
        Dim sub_po = TextBox1.Text & "-" & dtMainPOCount.Rows(0).Item(0) + 1

        Dim sqlCheck As String = "select * from main_po where po='" & TextBox1.Text & "' and fg_pn='" & ComboBox1.Text & "' and department='" & globVar.department & "'"
        Dim dtMainPOCheck As DataTable = Database.GetData(sqlCheck)

        If dtMainPOCheck.Rows.Count > 0 Then
            MessageBox.Show("PO & FG Part Number already in DB")
            DGV_MainPO_Spesific()
            'TextBox1.Text = ""
            'TextBox3.Text = ""
            'ComboBox1.Text = ""
            'ComboBox2.Text = ""
        Else
            Try
                Dim sqlInsertMainPOSubPO As String = "INSERT INTO main_po (po, sub_po, sub_po_qty, fg_pn, status, balance, actual_qty,department)
                                    VALUES ('" & TextBox1.Text & "','" & sub_po & "'," & TextBox3.Text & ",'" & ComboBox1.Text & "','Open',0,0,'" & globVar.department & "')"
                Dim cmdInsertMainPOSubPO = New SqlCommand(sqlInsertMainPOSubPO, Database.koneksi)
                If cmdInsertMainPOSubPO.ExecuteNonQuery() Then
                    DGV_MainPO_Spesific()
                    TextBox1.Text = ""
                    TextBox3.Text = ""
                    ComboBox1.Text = ""
                End If
            Catch ex As Exception
                MessageBox.Show("MainPOSubPO-01 : " & ex.Message)
            End Try
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
        'MessageBox.Show(DataGridView1.Columns(e.ColumnIndex).Index)

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
                    MessageBox.Show("MainPOSubPO-02 : " & ex.Message)
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
                MessageBox.Show("Cannot create Sub Sub PO because status PO is close.")
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text <> "" Then
            Dim sql As String = "select * from main_po where po='" & TextBox1.Text & "' and department='" & globVar.department & "'"
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

            Dim sqlcheck As String = "select * from main_po where po='" & DataGridView1.Rows(e.RowIndex).Cells("PO").Value & "' and (balance > 0 or actual_qty > 0) and department='" & globVar.department & "'"
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
                MessageBox.Show("MainPOSubPO-03 : " & ex.Message)
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

            Dim sqlCountSubSubPO As String = "select * from sub_sub_po where main_po=" & TextBox12.Text & " and status='Open'"
            Dim dtCountSubSubPO As DataTable = Database.GetData(sqlCountSubSubPO)
            Dim sub_sub_po = TextBox2.Text & "-" & dtCountSubSubPO.Rows.Count + 1

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

                Try
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
                Catch ex As Exception
                    MessageBox.Show("MainPOSubPO-04 : " & ex.Message)
                End Try

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
        where sp.main_po= mp.id AND mufg.fg_part_number= mp.fg_pn AND sp.status= 'Open' and line = '" & ComboBox3.Text & "' and mp.fg_pn = '" & fg & "' and sp.sub_sub_po='" & sub_sub_po & "' and mp.department='" & globVar.department & "' order by sp.sub_sub_po"
        Dim dtProdDOC As DataTable = Database.GetData(queryProdDOC)

        If dtProdDOC.Rows.Count > 0 Then
            For i As Integer = 0 To dtProdDOC.Rows.Count - 1
                Dim queryCheckProdDOC As String = "select * from prod_doc where sub_sub_po= '" & sub_sub_po & "' AND fg_pn = '" & fg & "' AND component='" & dtProdDOC.Rows(i).Item("component") & "' and line = '" & ComboBox3.Text & "' and department='" & globVar.department & "'"
                Dim dtCheckProdDOC As DataTable = Database.GetData(queryCheckProdDOC)
                If dtCheckProdDOC.Rows.Count = 0 Then
                    Try
                        Dim sqlInsertDOC As String = "INSERT INTO prod_doc (po, sub_sub_po, fg_pn, component, desc_comp, family, usage, line,department)
                                    VALUES ('" & po & "','" & sub_sub_po & "','" & dtProdDOC.Rows(i).Item("fg_pn") & "','" & dtProdDOC.Rows(i).Item("component") & "','" & dtProdDOC.Rows(i).Item("description") & "','" & dtProdDOC.Rows(i).Item("family") & "'," & dtProdDOC.Rows(i).Item("usage").ToString.Replace(",", ".") & ",'" & ComboBox3.Text & "','" & globVar.department & "')"
                        Dim cmdInsertDOC = New SqlCommand(sqlInsertDOC, Database.koneksi)
                        cmdInsertDOC.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show("MainPOSubPO-05 : " & ex.Message)
                    End Try
                End If
            Next
        End If
    End Sub

    Sub Insert_Prod_DOP(fg As String, sub_sub_po As String, po As String)
        Dim queryProdDOP As String = "select mp.po,sp.Sub_Sub_PO,mp.fg_pn,mpf.master_process,mpn.[order]
        from sub_sub_po sp,main_po mp,MASTER_PROCESS_FLOW MPF, master_process_number mpn 
        where sp.main_po = mp.id AND mpf.MASTER_FINISH_GOODS_PN = mp.fg_pn AND sp.status= 'Open' and line = '" & ComboBox3.Text & "' and mp.fg_pn = '" & fg & "' and sp.sub_sub_po='" & sub_sub_po & "' and mpn.process_name=mpf.master_process_number and master_process is not null and mp.department='" & globVar.department & "' order by sp.sub_sub_po"
        Dim dtProdDOP As DataTable = Database.GetData(queryProdDOP)
        If dtProdDOP.Rows.Count > 0 Then
            For i As Integer = 0 To dtProdDOP.Rows.Count - 1
                Dim queryCheckProdDOP As String = "select * from prod_dop where sub_sub_po= '" & sub_sub_po & "' AND fg_pn = '" & fg & "' AND process='" & dtProdDOP.Rows(i).Item("master_process") & "' and line = '" & ComboBox3.Text & "' and department='" & globVar.department & "'"
                Dim dtCheckProdDOP As DataTable = Database.GetData(queryCheckProdDOP)
                If dtCheckProdDOP.Rows.Count = 0 Then
                    Try
                        Dim sqlInsertDOP As String = "INSERT INTO prod_dop (po, sub_sub_po, fg_pn, process, line, process_number,department)
                                    VALUES ('" & po & "','" & sub_sub_po & "','" & dtProdDOP.Rows(i).Item("fg_pn") & "','" & dtProdDOP.Rows(i).Item("master_process") & "','" & ComboBox3.Text & "'," & dtProdDOP.Rows(i).Item("order") & ",'" & globVar.department & "')"
                        Dim cmdInsertDOP = New SqlCommand(sqlInsertDOP, Database.koneksi)
                        cmdInsertDOP.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show("MainPOSubPO-06 : " & ex.Message)
                    End Try
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

        Dim queryStatus As String = "select status from master_status"
        Dim dtStatus As DataTable = Database.GetData(queryStatus)
        Dim statusDGV As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn
        statusDGV.Name = "statuspo"
        statusDGV.HeaderText = "Status PO"
        statusDGV.DataSource = dtStatus
        statusDGV.DisplayMember = "status"
        statusDGV.ValueMember = "status"
        statusDGV.DataPropertyName = "status"
        DataGridView2.Columns.Add(statusDGV)

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 50
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        DataGridView2.Columns.Add(delete)

        Dim sqlSum As String = "select sum(sub_sub_po_qty) from sub_sub_po where main_po=" & TextBox12.Text
        Dim dtSubSubPOSum As DataTable = Database.GetData(sqlSum)
        TextBox11.Text = dtSubSubPOSum.Rows(0).Item(0).ToString

        DataGridView2.Columns("status").Visible = False
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

        Dim queryStatus As String = "select status from master_status"
        Dim dtStatus As DataTable = Database.GetData(queryStatus)
        Dim statusDGV As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn
        statusDGV.Name = "statuspo"
        statusDGV.HeaderText = "Status PO"
        statusDGV.DataSource = dtStatus
        statusDGV.DisplayMember = "status"
        statusDGV.ValueMember = "status"
        statusDGV.DataPropertyName = "status"
        DataGridView2.Columns.Add(statusDGV)

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 50
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        DataGridView2.Columns.Add(delete)

        Dim sqlSum As String = "select sum(sub_sub_po_qty) from sub_sub_po where main_po=" & TextBox12.Text
        Dim dtSubSubPOSum As DataTable = Database.GetData(sqlSum)
        TextBox11.Text = dtSubSubPOSum.Rows(0).Item(0).ToString

        DataGridView2.Columns("status").Visible = False
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
        'MessageBox.Show(DataGridView1.Columns(e.ColumnIndex).Index)

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
                    MessageBox.Show("MainPOSubPO-07 : " & ex.Message)
                End Try
            End If
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
                MessageBox.Show("MainPOSubPO-08 : " & ex.Message)
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
                MessageBox.Show("MainPOSubPO-09 : " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox1.Text = ""
        TextBox3.Text = ""
        TextBox5.Text = ""
        ComboBox1.SelectedIndex = -1
        DGV_MainPO_All()
    End Sub

    Private Sub ComboBox3_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedValueChanged
        If TabControl1.SelectedIndex = 1 Then
            DGV_SubSubPO_FILTERlINE()
        End If
    End Sub

    Private Sub DataGridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing
        If DataGridView1.CurrentCell.ColumnIndex = 10 Then
            Dim combo As DataGridViewComboBoxEditingControl = CType(e.Control, DataGridViewComboBoxEditingControl)

            If (combo IsNot Nothing) Then
                RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf Combo_SelectionChangeCommittedChangeStatusPO)
                AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf Combo_SelectionChangeCommittedChangeStatusPO)
            End If
        End If
    End Sub

    Private Sub Combo_SelectionChangeCommittedChangeStatusPO(ByVal sender As Object, ByVal e As EventArgs)
        Dim combo As DataGridViewComboBoxEditingControl = CType(sender, DataGridViewComboBoxEditingControl)
        If combo.SelectedValue = "Open" Then
            DGV_MainPO_All()
            Exit Sub
        End If

        Try
            Dim Sql As String = "update main_po set status='" & combo.SelectedValue & "' where id=" & DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("ID").Value & " and department='" & globVar.department & "'"
            Dim cmd = New SqlCommand(Sql, Database.koneksi)
            If cmd.ExecuteNonQuery() Then
                DGV_MainPO_All()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView2_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView2.EditingControlShowing
        If DataGridView2.CurrentCell.ColumnIndex = 7 Then
            Dim combo As DataGridViewComboBoxEditingControl = CType(e.Control, DataGridViewComboBoxEditingControl)

            If (combo IsNot Nothing) Then
                RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf Combo_SelectionChangeCommittedChangeStatusPOSubSubPO)
                AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf Combo_SelectionChangeCommittedChangeStatusPOSubSubPO)
            End If
        End If
    End Sub

    Private Sub Combo_SelectionChangeCommittedChangeStatusPOSubSubPO(ByVal sender As Object, ByVal e As EventArgs)
        Dim combo As DataGridViewComboBoxEditingControl = CType(sender, DataGridViewComboBoxEditingControl)
        If combo.SelectedValue = "Open" Then
            DGV_SubSubPO()
            Exit Sub
        End If

        Try
            Dim Sql As String = "update sub_sub_po set status='" & combo.SelectedValue & "' where id=" & DataGridView2.Rows(DataGridView2.CurrentCell.RowIndex).Cells("ID").Value & " and department='" & globVar.department & "'"
            Dim cmd = New SqlCommand(Sql, Database.koneksi)
            If cmd.ExecuteNonQuery() Then
                DGV_SubSubPO()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class