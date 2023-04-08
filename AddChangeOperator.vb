Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
'Imports ZXing

Public Class AddChangeOperator
    Public Shared menu As String = "Add / Change Operator"
    Private Sub AddChangeOperator_Load(sender As Object, e As EventArgs) Handles Me.Load
        Button1.Enabled = False
        tampilDataComboBoxLine()
        ComboBox2.SelectedIndex = -1
    End Sub

    Sub tampilDataComboBoxLine()
        Call Database.koneksi_database()
        Dim dtMasterLine As DataTable = Database.GetData("select * from master_line where department='" & globVar.department & "'")

        ComboBox2.DataSource = dtMasterLine
        ComboBox2.DisplayMember = "name"
        ComboBox2.ValueMember = "name"
    End Sub

    Sub sudahPrint()
        Dim queryRecordPrinting As String = "select * from record_printing where line='" & ComboBox2.Text & "' and sub_sub_po='" & TextBox17.Text & "' and fg='" & TextBox13.Text & "' and department='" & globVar.department & "'"
        Dim dtPrinting As DataTable = Database.GetData(queryRecordPrinting)

        If dtPrinting.Rows.Count > 0 Then
            CheckBox2.CheckState = CheckState.Checked
            Button1.Enabled = True
        Else
            CheckBox2.CheckState = CheckState.Unchecked
            Button1.Enabled = False
        End If
    End Sub

    Sub DGV_Add_Change_Operator()
        Try
            Dim queryDOP As String = "select mp.po,mp.sub_po,mp.fg_pn,mfg.description,ssp.sub_sub_po,dp.process_number Number,dp.process Process,dp.operator_id,ssp.sub_sub_po_qty,mfg.spq from prod_dop dp, sub_sub_po ssp,main_po mp,master_finish_goods mfg where dp.line='" & ComboBox2.Text & "' and ssp.line=dp.line and ssp.sub_sub_po=dp.sub_sub_po and ssp.status='Open' and mp.id=ssp.main_po and mfg.fg_part_number=mp.fg_pn and mp.department='" & globVar.department & "' order by dp.process_number"
            Dim dtDOP As DataTable = Database.GetData(queryDOP)
            DataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView3.DataSource = Nothing
            DataGridView3.Rows.Clear()
            DataGridView3.Columns.Clear()
            If dtDOP.Rows.Count > 0 Then

                TextBox19.Text = dtDOP.Rows(0).Item("sub_sub_po_qty")
                TextBox20.Text = dtDOP.Rows(0).Item("spq")

                TextBox13.Text = dtDOP.Rows(0).Item("fg_pn")  'FG PN
                TextBox14.Text = dtDOP.Rows(0).Item("description") 'Description
                TextBox15.Text = dtDOP.Rows(0).Item("PO")  'PO
                TextBox16.Text = dtDOP.Rows(0).Item("SUB_PO")  'Sub PO
                TextBox17.Text = dtDOP.Rows(0).Item("SUB_SUB_PO") 'Sub Sub PO

                sudahPrint()

                If CheckBox2.Checked = False Then
                    DataGridView3.DataSource = dtDOP

                    DataGridView3.Columns(0).Visible = False
                    DataGridView3.Columns(1).Visible = False
                    DataGridView3.Columns(2).Visible = False
                    DataGridView3.Columns(3).Visible = False
                    DataGridView3.Columns(4).Visible = False
                    DataGridView3.Columns(7).Visible = False
                    DataGridView3.Columns(8).Visible = False
                    DataGridView3.Columns(9).Visible = False

                    Dim name As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn
                    name.Name = "name"
                    name.HeaderText = "Operator Name"
                    Dim queryUsers As String = "select id_card_no +' - '+ name id_name,name from users where role='PRODUCTION' and department='" & globVar.department & "'order by name"
                    Dim dtUsers As DataTable = Database.GetData(queryUsers)
                    name.DataSource = dtUsers
                    name.DisplayMember = "id_name"
                    name.ValueMember = "name"
                    DataGridView3.Columns.Insert(7, name)

                    For rowDataSet As Integer = 0 To dtDOP.Rows.Count - 1
                        DataGridView3.Rows(rowDataSet).Cells(7).Value = dtDOP.Rows(rowDataSet).Item("operator_id").ToString
                    Next

                    For i As Integer = 0 To DataGridView3.RowCount - 1
                        If DataGridView3.Rows(i).Index Mod 2 = 0 Then
                            DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                        Else
                            DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                        End If
                    Next i
                Else
                    DataGridView3.DataSource = dtDOP

                    DataGridView3.Columns(0).Visible = False
                    DataGridView3.Columns(1).Visible = False
                    DataGridView3.Columns(2).Visible = False
                    DataGridView3.Columns(3).Visible = False
                    DataGridView3.Columns(4).Visible = False
                    DataGridView3.Columns(7).HeaderText = "Operator Name"
                    DataGridView3.Columns(8).Visible = False
                    DataGridView3.Columns(9).Visible = False

                    For rowDataSet As Integer = 0 To dtDOP.Rows.Count - 1
                        DataGridView3.Rows(rowDataSet).Cells(7).Value = dtDOP.Rows(rowDataSet).Item("operator_id").ToString
                    Next

                    For i As Integer = 0 To DataGridView3.RowCount - 1
                        If DataGridView3.Rows(i).Index Mod 2 = 0 Then
                            DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                        Else
                            DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                        End If
                    Next i
                End If
            End If
        Catch ex As Exception
            RJMessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub DataGridView3_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView3.EditingControlShowing
        If DataGridView3.Columns(DataGridView3.CurrentCell.ColumnIndex).Name = "name" Then
            Dim combo As DataGridViewComboBoxEditingControl = CType(e.Control, DataGridViewComboBoxEditingControl)

            If (combo IsNot Nothing) Then
                RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf Combo_SelectionChangeCommitted)

                AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf Combo_SelectionChangeCommitted)
            End If
        End If
    End Sub

    Private Sub Combo_SelectionChangeCommitted(ByVal sender As Object, ByVal e As EventArgs)
        Dim combo As DataGridViewComboBoxEditingControl = CType(sender, DataGridViewComboBoxEditingControl)
        If combo.SelectedValue IsNot Nothing Then
            If DataGridView3.Rows(DataGridView3.CurrentRow.Index).Cells("Number").Value IsNot Nothing Then
                Try
                    Dim Sql As String = "update prod_dop set operator_id='" & combo.SelectedValue & "' where po='" & TextBox15.Text & "' and sub_sub_po='" & TextBox17.Text & "' and line='" & ComboBox2.Text & "' and process_number=" & DataGridView3.Rows(DataGridView3.CurrentRow.Index).Cells("Number").Value & " AND DEPARTMENT='" & globVar.department & "'"
                    Dim cmd = New SqlCommand(Sql, Database.koneksi)
                    If cmd.ExecuteNonQuery() Then
                        DGV_Add_Change_Operator()
                    End If
                Catch ex As Exception
                    RJMessageBox.Show("MainPOSubPO-10 : " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim queryDOP As String = "select * from prod_dop where line='" & ComboBox2.Text & "' and sub_sub_po='" & TextBox17.Text & "' AND DEPARTMENT='" & globVar.department & "'"
            Dim dtDOP As DataTable = Database.GetData(queryDOP)

            Dim queryProdDOP As String = "select mp.po,sp.Sub_Sub_PO,mp.fg_pn,mpf.master_process,mpn.[order],pd.operator_id
                from sub_sub_po sp,main_po mp,MASTER_PROCESS_FLOW MPF, master_process_number mpn,prod_dop pd 
                where sp.main_po = mp.id AND mpf.MASTER_FINISH_GOODS_PN = mp.fg_pn AND sp.status= 'Open' and sp.line = '" & ComboBox2.Text & "' and mp.fg_pn = '" & TextBox13.Text & "' and sp.sub_sub_po='" & TextBox17.Text & "' and mpn.process_name=mpf.master_process_number and master_process is not null and pd.line=sp.line and pd.fg_pn=mp.fg_pn and pd.sub_sub_po=sp.sub_sub_po and pd.process=mpf.master_process AND MP.DEPARTMENT='" & globVar.department & "' order by sp.sub_sub_po"
            Dim dtProdDOP As DataTable = Database.GetData(queryProdDOP)
            If dtProdDOP.Rows.Count > 0 Then
                Dim queryCount As String = "select count(*) from prod_dop where line='" & ComboBox2.Text & "' and sub_sub_po='" & TextBox17.Text & "' and fg_pn=" & TextBox13.Text & " and operator_id is null AND DEPARTMENT='" & globVar.department & "'"
                Dim dtCount As DataTable = Database.GetData(queryCount)
                If dtCount.Rows(0).Item(0) > 0 Then
                    RJMessageBox.Show("Please fill Operator first!",
                                       "",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim totalLot As Integer = 0

                If Val(TextBox19.Text) <= Val(TextBox20.Text) Then
                    totalLot = 1
                Else
                    If Val(TextBox19.Text) Mod Val(TextBox20.Text) = 0 Then
                        totalLot = Val(TextBox19.Text) / Val(TextBox20.Text)
                    Else
                        totalLot = Math.Floor(Val(TextBox19.Text) / Val(TextBox20.Text)) + 1
                    End If
                End If

                Dim queryDOPDetailCount As String = "select count(*) from prod_dop_details where sub_sub_po='" & TextBox17.Text & "' AND DEPARTMENT='" & globVar.department & "'"
                Dim dtDOPDetailCount As DataTable = Database.GetData(queryDOPDetailCount)
                If dtDOPDetailCount.Rows(0).Item(0) = 0 Then
                    For i As Integer = 0 To dtProdDOP.Rows.Count - 1
                        For j As Integer = 0 To totalLot - 1
                            Dim sqlInsertDOPDetails As String = "INSERT INTO prod_dop_details (sub_sub_po, process, operator, lot_flow_ticket, DEPARTMENT, PROCESS_NUMBER)
                    VALUES ('" & TextBox17.Text & "','" & dtProdDOP.Rows(i).Item("master_process") & "','" & dtProdDOP.Rows(i).Item("operator_id") & "'," & j + 1 & ",'" & globVar.department & "'," & dtProdDOP.Rows(i).Item("order") & ")"
                            Dim cmdInsertDOPDetails = New SqlCommand(sqlInsertDOPDetails, Database.koneksi)
                            If cmdInsertDOPDetails.ExecuteNonQuery() Then
                                TabControl1.SelectedTab = TabPage1
                            End If
                        Next
                    Next
                Else
                    TabControl1.SelectedTab = TabPage1
                End If

            End If
            TabControl1.SelectedIndex = 0
            TabControl1.SelectedIndex = 1
        Catch ex As Exception
            RJMessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Try
            If TabControl1.SelectedIndex = 1 Then
                DGV_Bawah
            End If
        Catch ex As Exception
            RJMessageBox.Show(ex.ToString)
        End Try
    End Sub

    Sub DGV_Bawah()
        If CheckBox2.CheckState = CheckState.Unchecked Then
            TabControl1.SelectedIndex = 0
            Exit Sub
        End If

        If DataGridView3.Rows.Count > 0 Then
            ComboBox1.Text = ComboBox2.Text
            TextBox5.Text = TextBox13.Text
            TextBox4.Text = TextBox14.Text
            TextBox3.Text = TextBox15.Text
            TextBox2.Text = TextBox16.Text
            TextBox1.Text = TextBox17.Text

            Dim varProcess As String = ""
            DataGridView1.Rows.Clear()
            DataGridView1.Columns.Clear()

            Dim queryCek As String = "select process from prod_dop_details where sub_sub_po='" & TextBox1.Text & "' AND DEPARTMENT='" & globVar.department & "' group by process order by max(datetime_insert)"
            Dim dsexist = New DataSet
            Dim adapterexist = New SqlDataAdapter(queryCek, Database.koneksi)
            adapterexist.Fill(dsexist)
            For i As Integer = 0 To dsexist.Tables(0).Rows.Count - 1
                If i = 0 Then
                    varProcess = "[" + dsexist.Tables(0).Rows(i).Item("PROCESS").ToString + "]"
                Else
                    varProcess = varProcess + ",[" + dsexist.Tables(0).Rows(i).Item("PROCESS").ToString + "]"
                End If
            Next

            If varProcess = "" Then
                RJMessageBox.Show("If you want to change the operator, please use the 'change operator' button.")
                TabControl1.SelectedIndex = 0
                Exit Sub
            End If

            Dim query As String = "SELECT * FROM (SELECT lot_flow_ticket, process, operator FROM dbo.prod_dop_details where sub_sub_po='" & TextBox1.Text & "') t PIVOT ( max(operator) FOR process IN ( " + varProcess + " )) pivot_table order by cast(lot_flow_ticket as int)"
            Dim dtDOP As DataTable = Database.GetData(query)

            If dtDOP.Rows.Count > 0 Then
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader
                DataGridView1.ColumnCount = 1
                DataGridView1.Columns(0).Name = "Lot Flow Ticket"
                For r = 0 To dtDOP.Rows.Count - 1
                    Dim row As String() = New String() {
                        dtDOP.Rows(r).Item(0).ToString
                    }
                    DataGridView1.Rows.Add(row)
                Next

                Dim queryOperator As String = "select name from users where role='PRODUCTION' AND department='" & globVar.department & "' order by name"
                Dim dsOperator = New DataSet
                Dim adapterOperator = New SqlDataAdapter(queryOperator, Database.koneksi)
                adapterOperator.Fill(dsOperator)

                For i As Integer = 0 To dsexist.Tables(0).Rows.Count - 1
                    Dim cbcolumn As New DataGridViewTextBoxColumn
                    cbcolumn.HeaderText = dsexist.Tables(0).Rows(i).Item(0).ToString
                    'For iProcess As Integer = 0 To dsOperator.Tables(0).Rows.Count - 1
                    '    cbcolumn.Items.Add(dsOperator.Tables(0).Rows(iProcess).Item("name").ToString)
                    'Next

                    DataGridView1.Columns.Add(cbcolumn)
                Next

                For rowDataSet As Integer = 0 To dtDOP.Rows.Count - 1
                    For colDataSet As Integer = 1 To dtDOP.Columns.Count - 1
                        DataGridView1.Rows(rowDataSet).Cells(colDataSet).Value = dtDOP.Rows(rowDataSet).Item(colDataSet).ToString
                    Next
                Next
            End If

            tampilDataComboBox(TextBox1.Text)
            cbLot.SelectedIndex = -1
        End If
    End Sub

    Sub tampilDataComboBox(sub_sub_po As String)
        Call Database.koneksi_database()
        Dim dtLot As DataTable = Database.GetData("SELECT lot_flow_ticket
            FROM (
              SELECT lot_flow_ticket, ROW_NUMBER() OVER (PARTITION BY lot_flow_ticket ORDER BY lot_flow_ticket) AS row_num
              FROM prod_dop_details
              WHERE sub_sub_po = '" & sub_sub_po & "'
            ) AS subquery
            WHERE subquery.row_num = 1
            ORDER BY CAST(subquery.lot_flow_ticket AS INT) ASC")

        cbLot.DataSource = dtLot
        cbLot.DisplayMember = "lot_flow_ticket"
        cbLot.ValueMember = "lot_flow_ticket"
    End Sub

    Sub ChangeOperator()
        Try
            If TabControl1.SelectedIndex = 1 Then
                If CheckBox2.CheckState = CheckState.Unchecked Then
                    TabControl1.SelectedIndex = 0
                    Exit Sub
                End If

                If DataGridView3.Rows.Count > 0 Then
                    ComboBox1.Text = ComboBox2.Text
                    TextBox5.Text = TextBox13.Text
                    TextBox4.Text = TextBox14.Text
                    TextBox3.Text = TextBox15.Text
                    TextBox2.Text = TextBox16.Text
                    TextBox1.Text = TextBox17.Text

                    Dim varProcess As String = ""
                    DataGridView2.Rows.Clear()
                    DataGridView2.Columns.Clear()

                    Dim queryCek As String = "select process from prod_dop_details where sub_sub_po='" & TextBox1.Text & "' AND DEPARTMENT='" & globVar.department & "' group by process order by max(datetime_insert)"
                    Dim dsexist = New DataSet
                    Dim adapterexist = New SqlDataAdapter(queryCek, Database.koneksi)
                    adapterexist.Fill(dsexist)
                    For i As Integer = 0 To dsexist.Tables(0).Rows.Count - 1
                        If i = 0 Then
                            varProcess = "[" + dsexist.Tables(0).Rows(i).Item("PROCESS").ToString + "]"
                        Else
                            varProcess = varProcess + ",[" + dsexist.Tables(0).Rows(i).Item("PROCESS").ToString + "]"
                        End If
                    Next

                    If varProcess = "" Then
                        RJMessageBox.Show("If you want to change the operator, please use the 'change operator' button.")
                        TabControl1.SelectedIndex = 0
                        Exit Sub
                    End If

                    Dim query As String = "SELECT * FROM (SELECT lot_flow_ticket, process, operator FROM dbo.prod_dop_details where sub_sub_po='" & TextBox1.Text & "' and lot_flow_ticket='" & cbLot.Text & "') t PIVOT ( max(operator) FOR process IN ( " + varProcess + " )) pivot_table"
                    Dim dtDOP As DataTable = Database.GetData(query)

                    If dtDOP.Rows.Count > 0 Then
                        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader
                        DataGridView2.ColumnCount = 1
                        DataGridView2.Columns(0).Name = "Lot Flow Ticket"
                        For r = 0 To dtDOP.Rows.Count - 1
                            Dim row As String() = New String() {
                                dtDOP.Rows(r).Item(0).ToString
                            }
                            DataGridView2.Rows.Add(row)
                        Next

                        Dim queryOperator As String = "select name from users where role='PRODUCTION' AND department='" & globVar.department & "' order by name"
                        Dim dsOperator = New DataSet
                        Dim adapterOperator = New SqlDataAdapter(queryOperator, Database.koneksi)
                        adapterOperator.Fill(dsOperator)

                        For i As Integer = 0 To dsexist.Tables(0).Rows.Count - 1
                            Dim cbcolumn As New DataGridViewComboBoxColumn
                            cbcolumn.HeaderText = dsexist.Tables(0).Rows(i).Item(0).ToString
                            For iProcess As Integer = 0 To dsOperator.Tables(0).Rows.Count - 1
                                cbcolumn.Items.Add(dsOperator.Tables(0).Rows(iProcess).Item("name").ToString)
                            Next

                            DataGridView2.Columns.Add(cbcolumn)
                        Next

                        For rowDataSet As Integer = 0 To dtDOP.Rows.Count - 1
                            For colDataSet As Integer = 1 To dtDOP.Columns.Count - 1
                                DataGridView2.Rows(rowDataSet).Cells(colDataSet).Value = dtDOP.Rows(rowDataSet).Item(colDataSet).ToString
                            Next
                        Next
                    End If
                End If

            End If
        Catch ex As Exception
            RJMessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub DataGridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView2.EditingControlShowing
        If DataGridView2.CurrentCell.ColumnIndex > 0 Then
            Dim combo As DataGridViewComboBoxEditingControl = CType(e.Control, DataGridViewComboBoxEditingControl)

            If (combo IsNot Nothing) Then
                RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf Combo_SelectionChangeCommittedChangeOperator)

                AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf Combo_SelectionChangeCommittedChangeOperator)
            End If
        End If
    End Sub

    Private Sub Combo_SelectionChangeCommittedChangeOperator(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim combo As DataGridViewComboBoxEditingControl = CType(sender, DataGridViewComboBoxEditingControl)
            Dim Sql As String = "update prod_dop_details set operator='" & combo.SelectedItem & "' where sub_sub_po='" & TextBox1.Text & "' and cast(lot_flow_ticket as int)>=" & DataGridView2.Rows(DataGridView2.CurrentCell.RowIndex).Cells(0).Value & " and process='" & DataGridView2.Columns(DataGridView2.CurrentCell.ColumnIndex).HeaderCell.Value & "' and department='" & globVar.department & "'"
            Dim cmd = New SqlCommand(Sql, Database.koneksi)
            If cmd.ExecuteNonQuery() Then
                DGV_Bawah()
            End If
        Catch ex As Exception
            RJMessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub DataGridView2_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView2.CellFormatting
        For i As Integer = 0 To DataGridView2.RowCount - 1
            If DataGridView2.Rows(i).Index Mod 2 = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ClearAll()
        DGV_Add_Change_Operator()
    End Sub

    Sub ClearAll()
        TextBox13.Clear()
        TextBox14.Clear()
        TextBox15.Clear()
        TextBox16.Clear()
        TextBox17.Clear()
        CheckBox2.CheckState = CheckState.Unchecked

        TextBox5.Clear()
        TextBox4.Clear()
        TextBox3.Clear()
        TextBox2.Clear()
        TextBox1.Clear()

        DataGridView1.Columns.Clear()
        DataGridView1.Rows.Clear()

        DataGridView2.Columns.Clear()
        DataGridView2.Rows.Clear()
    End Sub

    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete
        With DataGridView1
            .DefaultCellStyle.Font = New Font("Tahoma", 14)

            For i As Integer = 0 To .ColumnCount - 1
                .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

            .EnableHeadersVisualStyles = False
            With .ColumnHeadersDefaultCellStyle
                .BackColor = Color.Navy
                .ForeColor = Color.White
                .Font = New Font("Tahoma", 13, FontStyle.Bold)
                .Alignment = HorizontalAlignment.Center
                .Alignment = ContentAlignment.MiddleCenter
            End With

            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Sub DataGridView3_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView3.DataBindingComplete
        With DataGridView3
            .DefaultCellStyle.Font = New Font("Tahoma", 14)

            For i As Integer = 0 To .ColumnCount - 1
                .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

            .EnableHeadersVisualStyles = False
            With .ColumnHeadersDefaultCellStyle
                .BackColor = Color.Navy
                .ForeColor = Color.White
                .Font = New Font("Tahoma", 13, FontStyle.Bold)
                .Alignment = HorizontalAlignment.Center
                .Alignment = ContentAlignment.MiddleCenter
            End With

            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Sub btn_check_Click(sender As Object, e As EventArgs) Handles btn_check.Click
        ChangeOperator()
    End Sub
End Class