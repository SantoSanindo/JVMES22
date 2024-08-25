Imports System.Data.SqlClient
Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class SummaryV2
    Public Shared menu As String = "Summary"

    Dim selectdata As String = ""
    Dim selectComp As String = ""


    Private Sub SummaryV2_Load(sender As Object, e As EventArgs) Handles Me.Load
        globVar.PingVersion()
        tampilDataComboBoxLine()
        ComboBox1.SelectedIndex = -1
    End Sub

    Sub tampilDataComboBoxLine()
        Call Database.koneksi_database()
        Dim dtMasterLine As DataTable = Database.GetData("select line from SUB_SUB_PO where status='Open' order by line")

        ComboBox1.DataSource = dtMasterLine
        ComboBox1.DisplayMember = "line"
        ComboBox1.ValueMember = "line"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If globVar.view > 0 Then
            TreeView1.Nodes.Clear()

            DataGridView2.DataSource = Nothing
            DataGridView2.Rows.Clear()
            DataGridView2.Columns.Clear()

            LoadDataTXT()

            LoadData()

        Else
            RJMessageBox.Show("Cannot access this menu.")
        End If
    End Sub

    Private Sub LoadData()
        Call Database.koneksi_database()
        Dim query As String = "exec V_SUMMARY @Line='" + ComboBox1.Text + "',@department='" & globVar.department & "',@fg='" & txtFG.Text & "',@subsubpo='" & txtSubSubPO.Text & "'"
        Dim dtSummary As DataTable = Database.GetData(query)

        DGV_Summary()
    End Sub

    Private Sub DGV_Summary()
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Call Database.koneksi_database()
        Dim dtSummary As DataTable = Database.GetData("select comp [Comp], fresh_in [Fresh In], sub_assy_in [SA In], onhold_in [OnHold In], wip_in [WIP In], others_in [Others In], total_in [Total In], reject [Reject], [return] [Return], defect [Defect Out], wip_out [WIP Out], onhold_out [OnHold Out], sa_out [SA Out], fg_out [FG Out], total_out [Total Out] from summary_fg where sub_sub_po=(select sub_sub_po from sub_sub_po where line='" + ComboBox1.Text + "' and status='Open') and line='" + ComboBox1.Text + "'order by comp")

        DataGridView1.DataSource = dtSummary

        DataGridView1.Columns(1).Width = 100
        DataGridView1.Columns(6).Width = 100
        DataGridView1.Columns(14).Width = 100

        Dim checkSummary As DataGridViewButtonColumn = New DataGridViewButtonColumn
        checkSummary.Name = "check"
        checkSummary.HeaderText = "Check"
        checkSummary.Width = 50
        checkSummary.Text = "Check"
        checkSummary.UseColumnTextForButtonValue = True
        DataGridView1.Columns.Insert(0, checkSummary)
    End Sub

    Sub LoadDataTXT()
        Call Database.koneksi_database()
        Dim dtSummary As DataTable = Database.GetData("select * from sub_sub_po ssp, main_po mp where ssp.sub_sub_po=(select sub_sub_po from sub_sub_po where line='" + ComboBox1.Text + "' and status='Open') and ssp.line='" + ComboBox1.Text + "' and mp.id=ssp.main_po")
        If dtSummary.Rows.Count > 0 Then
            txtSubSubPO.Text = dtSummary.Rows(0).Item("sub_sub_po")
            txtSubSubPOQty.Text = dtSummary.Rows(0).Item("sub_sub_po_qty")
            txtPO.Text = dtSummary.Rows(0).Item("po")
            txtSubPO.Text = dtSummary.Rows(0).Item("sub_po")
            txtFG.Text = dtSummary.Rows(0).Item("fg_pn")
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try

            If e.ColumnIndex >= 0 Then
                If DataGridView1.Columns(e.ColumnIndex).Name = "check" Then
                    treeView_show(txtSubSubPO.Text, DataGridView1.Rows(e.RowIndex).Cells("Comp").Value)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub treeView_show(sub_sub_po As String, comp As String)
        TreeView1.Nodes.Clear()
        Dim query As String = "SELECT comp,fresh_in,sub_assy_in,onhold_in,wip_in,others_in,total_in,reject,[return],defect,wip_out,onhold_out,sa_out,fg_out,total_out FROM summary_fg WHERE comp='" & comp & "' AND sub_sub_po='" & sub_sub_po & "' order by comp"
        Dim dt As DataTable = Database.GetData(query)

        TreeView1.Nodes.Add("root", "SSP : " & sub_sub_po & " | Comp : " & comp)
        selectComp = comp

        If dt.Rows.Count > 0 Then
            TreeView1.Nodes(0).Nodes.Add("fresh_in", "Fresh In (" & dt.Rows(0).Item("fresh_in").ToString & ")")
            TreeView1.Nodes(0).Nodes.Add("sub_assy_in", "Sub Assy In (" & dt.Rows(0).Item("sub_assy_in").ToString & ")")
            TreeView1.Nodes(0).Nodes.Add("onhold_in", "On Hold In (" & dt.Rows(0).Item("onhold_in").ToString & ")")
            TreeView1.Nodes(0).Nodes.Add("wip_in", "WIP In (" & dt.Rows(0).Item("wip_in").ToString & ")")
            TreeView1.Nodes(0).Nodes.Add("others_in", "Others In (" & dt.Rows(0).Item("others_in").ToString & ")")
            TreeView1.Nodes(0).Nodes.Add("reject", "Reject (" & dt.Rows(0).Item("reject").ToString & ")")
            TreeView1.Nodes(0).Nodes.Add("return", "Return (" & dt.Rows(0).Item("return").ToString & ")")
            TreeView1.Nodes(0).Nodes.Add("defect", "Defect (" & dt.Rows(0).Item("defect").ToString & ")")
            TreeView1.Nodes(0).Nodes.Add("onhold_out", "On Hold Out (" & dt.Rows(0).Item("onhold_out").ToString & ")")
            TreeView1.Nodes(0).Nodes.Add("wip_out", "WIP Out (" & dt.Rows(0).Item("wip_out").ToString & ")")
            TreeView1.Nodes(0).Nodes.Add("sa_out", "SA Out (" & dt.Rows(0).Item("sa_out").ToString & ")")
            TreeView1.Nodes(0).Nodes.Add("fg_out", "FG Out (" & dt.Rows(0).Item("fg_out").ToString & ")")
        End If

        TreeView1.ExpandAll()
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        If TreeView1.SelectedNode Is Nothing Then
            DataGridView2.DataSource = Nothing
            Exit Sub
        End If

        Dim id As String = TreeView1.SelectedNode.Name

        selectdata = id

        If TreeView1.SelectedNode.Name IsNot "root" Then
            Dim parent As String() = TreeView1.SelectedNode.Parent.Text.Split(" | ")
            DGV_Bawah(id, parent(6))
        End If

    End Sub

    Sub DGV_Bawah(key As String, comp As String)
        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView2.DataSource = Nothing
        DataGridView2.Rows.Clear()
        DataGridView2.Columns.Clear()
        Call Database.koneksi_database()

        Dim query As String

        If key = "fresh_in" Then
            query = "SELECT 
                id [#],
                qrcode [QRCode],
	            mts_no [MTS NO],
	            material [Material],
	            INV_CTRL_DATE [ICD] ,
	            TRACEABILITY [Traceability],
	            BATCH_NO [Batch No],
	            LOT_NO [Lot No],
                qrcode_new [New Label],
	            qty [QTY],
	            remark [Remark]
            FROM
	            STOCK_CARD 
            WHERE
	            status = 'Production Process' 
	            AND LEVEL = 'Fresh' 
	            AND (QRCODE NOT LIKE 'SA%' or QRCODE_NEW IS NOT NULL) 
	            AND material = '" & comp & "'
	            AND line = '" & ComboBox1.Text & "'
	            AND SUB_SUB_PO = '" & txtSubSubPO.Text & "' 
	            AND department ='" & globVar.department & "'
            order by LOT_NO "
        ElseIf key = "sub_assy_in" Then
            query = "SELECT 
                id [#],
	            qrcode [QRCode],
	            material [Material],
	            INV_CTRL_DATE [ICD],
	            TRACEABILITY [Traceability],
	            BATCH_NO [Batch No],
	            LOT_NO [Lot No],
	            qty [QTY],
                qrcode [QRCode],
	            remark [Remark]
            FROM
                STOCK_CARD
            WHERE
                Status = 'Production Process' 
                And QRCode Like 'SA%' 
	            And material = '" & comp & "'
	            And line = '" & ComboBox1.Text & "'
	            And SUB_SUB_PO = '" & txtSubSubPO.Text & "' 
	            And department ='" & globVar.department & "'
            order by LOT_NO "
        ElseIf key = "onhold_in" Then
            query = "SELECT 
                id [#],
	            qrcode [QRCode],
	            material [Material],
	            INV_CTRL_DATE [ICD],
	            TRACEABILITY [Traceability],
	            BATCH_NO [Batch No],
	            LOT_NO [Lot No],
                qrcode_new [New Label],
	            actual_qty [QTY],
	            remark [Remark]
            FROM
                STOCK_CARD
            WHERE
                Status = 'Production Process' 
                And QRCode Like 'OH%' 
	            And material = '" & comp & "'
	            And line = '" & ComboBox1.Text & "'
	            And SUB_SUB_PO = '" & txtSubSubPO.Text & "' 
	            And department ='" & globVar.department & "'
            order by LOT_NO "
        ElseIf key = "wip_in" Then
            query = "SELECT 
                id [#],
	            qrcode [QRCode],
	            material [Material],
	            INV_CTRL_DATE [ICD],
	            TRACEABILITY [Traceability],
	            BATCH_NO [Batch No],
                qrcode_new [New Label],
	            LOT_NO [Lot No],
	            actual_qty [QTY],
	            remark [Remark]
            FROM
                STOCK_CARD
            WHERE
                Status = 'Production Process' 
                And QRCode Like 'WIP%' 
	            And material = '" & comp & "'
	            And line = '" & ComboBox1.Text & "'
	            And SUB_SUB_PO = '" & txtSubSubPO.Text & "' 
	            And department ='" & globVar.department & "'
            order by LOT_NO "
        ElseIf key = "others_in" Then
            query = "SELECT 
                id [#],
	            qrcode [QRCode],
	            material [Material],
	            INV_CTRL_DATE [ICD],
	            TRACEABILITY [Traceability],
	            BATCH_NO [Batch No],
	            LOT_NO [Lot No],
	            qty [QTY],
                qrcode [QRCode],
	            remark [Remark]
            FROM
                STOCK_CARD
            WHERE
                Status = 'Production Process' 
                AND LEVEL = 'OT' 
	            And material = '" & comp & "'
	            And line = '" & ComboBox1.Text & "'
	            And SUB_SUB_PO = '" & txtSubSubPO.Text & "' 
	            And department ='" & globVar.department & "'
            order by LOT_NO "
        ElseIf key = "reject" Then
            query = "SELECT 
                     CODE_OUT_PROD_REJECT [QRCode], 
                     PART_NUMBER [Material],
                     INV_CTRL_DATE [ICD],
                     TRACEABILITY [Traceability],
                     BATCH_NO [Batch No],
                     LOT_NO [Lot No],
                     QTY [QTY],
                     SUB_ASSY [Notes]
                    FROM
                     OUT_PROD_REJECT 
                    WHERE
                     SUB_SUB_PO = '" & txtSubSubPO.Text & "' 
                     AND line = '" & ComboBox1.Text & "' 
                     AND PART_NUMBER = '" & comp & "'"
        ElseIf key = "return" Then
            query = "SELECT 
                     mts_no [MTS NO],
                     material [Material],
                     INV_CTRL_DATE [ICD],
                     TRACEABILITY [Traceability],
                     BATCH_NO [Batch No],
                     LOT_NO [Lot No],
                     qty [QTY],
                     qrcode [Notes]
                    FROM
                     STOCK_CARD 
                    WHERE
                     status = 'Return to Mini Store' 
                     AND LEVEL = 'B' 
                     and SUB_SUB_PO = '" & txtSubSubPO.Text & "' 
                     AND line = '" & ComboBox1.Text & "' 
                     And department ='" & globVar.department & "'
                     AND material = '" & comp & "'"
        ElseIf key = "defect" Then
            query = "SELECT 
                     CODE_OUT_PROD_DEFECT [QRCode], 
                     PART_NUMBER [Material],
                     INV_CTRL_DATE [ICD],
                     TRACEABILITY [Traceability],
                     BATCH_NO [Batch No],
                     LOT_NO [Lot No],
                     FLOW_TICKET_NO [Flow Ticket],
                     QTY [QTY]
                    FROM
                     OUT_PROD_DEFECT 
                    WHERE
                     SUB_SUB_PO = '" & txtSubSubPO.Text & "' 
                     AND line = '" & ComboBox1.Text & "' 
                     And department ='" & globVar.department & "'
                     AND PART_NUMBER = '" & comp & "'"
        ElseIf key = "onhold_out" Then
            query = "SELECT 
                     CODE_STOCK_PROD_ONHOLD [QRCode], 
                     PART_NUMBER [Material],
                     INV_CTRL_DATE [ICD],
                     TRACEABILITY [Traceability],
                     BATCH_NO [Batch No],
                     LOT_NO [Lot No],
                     QTY [QTY]
                    FROM
                     stock_prod_onhold 
                    WHERE
                     SUB_SUB_PO = '" & txtSubSubPO.Text & "' 
                     AND line = '" & ComboBox1.Text & "' 
                     And department ='" & globVar.department & "'
                     AND PART_NUMBER = '" & comp & "'"
        ElseIf key = "wip_out" Then
            query = "SELECT 
                     CODE_STOCK_PROD_WIP [QRCode], 
                     PART_NUMBER [Material],
                     INV_CTRL_DATE [ICD],
                     TRACEABILITY [Traceability],
                     BATCH_NO [Batch No],
                     LOT_NO [Lot No],
                     QTY [QTY]
                    FROM
                     stock_prod_wip 
                    WHERE
                     SUB_SUB_PO = '" & txtSubSubPO.Text & "' 
                     AND line = '" & ComboBox1.Text & "' 
                     And department ='" & globVar.department & "'
                     AND PART_NUMBER = '" & comp & "'"
        ElseIf key = "sa_out" Then
            query = "SELECT 
                     CODE_STOCK_PROD_SUB_ASSY [QRCode], 
                     FG [SA],
                     INV_CTRL_DATE [ICD],
                     TRACEABILITY [Traceability],
                     BATCH_NO [Batch No],
                     LOT_NO [Lot No],
                     FLOW_TICKET,
                     QTY [QTY]
                    FROM
                     stock_prod_sub_assy 
                    WHERE
                     SUB_SUB_PO = '" & txtSubSubPO.Text & "' 
                     AND line = '" & ComboBox1.Text & "' 
                     And department ='" & globVar.department & "'"
        ElseIf key = "fg_out" Then
            query = "SELECT 
                     material [Material],
                     INV_CTRL_DATE [ICD],
                     TRACEABILITY [Traceability],
                     BATCH_NO [Batch No],
                     LOT_NO [Lot No],
                     FLOW_TICKET,
                     qty [QTY]
                    FROM
                     STOCK_CARD 
                    WHERE
                     SUB_SUB_PO = '" & txtSubSubPO.Text & "' 
                     AND line = '" & ComboBox1.Text & "' 
                     And department ='" & globVar.department & "'
                     AND material = '" & comp & "'
                     AND STATUS='Production Result'
                    order by cast(replace(flow_ticket,' of ','') as int) asc"

        End If

        Dim dtSummary As DataTable = Database.GetData(query)
        DataGridView2.DataSource = dtSummary
    End Sub

    Private Sub DataGridView2_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView2.DataBindingComplete
        For i As Integer = 0 To DataGridView2.RowCount - 1
            If DataGridView2.Rows(i).Index Mod 2 = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With DataGridView2
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

    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete
        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If globVar.add > 0 Then
            Dim tidakBalance As Boolean = False

            Dim result = RJMessageBox.Show("Are you sure for close this Sub Sub PO?.", "Are You Sure?", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                For i = 0 To DataGridView1.Rows.Count - 1
                    If DataGridView1.Rows(i).Cells("Total In").Value <> DataGridView1.Rows(i).Cells("Total Out").Value Then
                        tidakBalance = True
                    End If
                Next

                If tidakBalance Then
                    RJMessageBox.Show("Please check TOTAL IN and TOTAL OUT must be balance.")
                Else
                    SaveTraceability()

                    Dim sqlStr As String = "SELECT mp.id, mp.sub_po_qty,mp.actual_qty,mfg.level FROM sub_sub_po ssp, main_po mp, MASTER_FINISH_GOODS mfg where ssp.sub_sub_po='" & txtSubSubPO.Text & "' and ssp.main_po = mp.id and mfg.FG_PART_NUMBER=mp.fg_pn"
                    Dim dttable As DataTable = Database.GetData(sqlStr)

                    If dttable.Rows(0).Item("sub_po_qty") = dttable.Rows(0).Item("actual_qty") Then

                        Dim queryUpdateSubsubpo As String = "update sub_sub_po set status='Closed',datetime_closed=getdate(),closed_who='" & globVar.username & "' where sub_sub_po='" & txtSubSubPO.Text & "';update main_po set status='Closed',datetime_closed=getdate(),closed_who='" & globVar.username & "' where id=" & dttable.Rows(0).Item("id")
                        Dim dtUpdateSubsubpo = New SqlCommand(queryUpdateSubsubpo, Database.koneksi)
                        If dtUpdateSubsubpo.ExecuteNonQuery() Then
                            RJMessageBox.Show("Success Close Sub Sub PO")
                        End If

                        clearAll()

                    Else

                        If dttable.Rows(0).Item("level") = "Sub Assy" Then

                            Dim queryUpdateSubsubpo As String = "update sub_sub_po set status='Closed',datetime_closed=getdate(),closed_who='" & globVar.username & "' where sub_sub_po='" & txtSubSubPO.Text & "';update main_po set status='Closed',datetime_closed=getdate(),closed_who='" & globVar.username & "' where id=" & dttable.Rows(0).Item("id")
                            Dim dtUpdateSubsubpo = New SqlCommand(queryUpdateSubsubpo, Database.koneksi)
                            If dtUpdateSubsubpo.ExecuteNonQuery() Then
                                RJMessageBox.Show("Success Close Sub Sub PO")
                            End If

                        Else

                            Dim queryUpdateSubsubpo As String = "update sub_sub_po set status='Closed',datetime_closed=getdate(),closed_who='" & globVar.username & "' where sub_sub_po='" & txtSubSubPO.Text & "'"
                            Dim dtUpdateSubsubpo = New SqlCommand(queryUpdateSubsubpo, Database.koneksi)
                            If dtUpdateSubsubpo.ExecuteNonQuery() Then
                                RJMessageBox.Show("Success Close Sub Sub PO")
                            End If

                        End If

                        clearAll()

                    End If
                End If
            End If
        Else
            RJMessageBox.Show("Cannot access this menu.")
        End If
    End Sub

    Sub SaveTraceability()

        Dim sqlCheckMasterFG As String = "SELECT * FROM master_finish_goods where fg_part_number= '" & txtFG.Text & "'"
        Dim dtCheckMasterFG As DataTable = Database.GetData(sqlCheckMasterFG)

        If dtCheckMasterFG.Rows(0).Item("level") = "Sub Assy" Then

            Dim sqlStr As String = "SELECT d.DATETIME_INSERT,d.sub_sub_po,d.line,d.fg,d.laser_code,d.INV_CTRL_DATE,d.BATCH_NO,d.LOT_NO,d.traceability,f.inspector,f.packer1,f.packer2,f.packer3,f.packer4 FROM STOCK_PROD_SUB_ASSY d left join fga f on d.sub_sub_po=f.sub_sub_po and d.flow_ticket=f.no_flowticket WHERE d.fg= '" & txtFG.Text & "' and d.sub_sub_po='" & txtSubSubPO.Text & "'"
            Dim dttable As DataTable = Database.GetData(sqlStr)
            If dttable.Rows.Count > 0 Then
                For u = 0 To dttable.Rows.Count - 1
                    Dim sqlCheckSummaryTraceability As String = "SELECT * FROM summary_traceability WHERE sub_sub_po = '" & dttable.Rows(u).Item("sub_sub_po") & "' and fg='" & txtFG.Text & "' and lot_no='" & dttable.Rows(u).Item("LOT_NO") & "'"
                    Dim dtCheckSummaryTraceability As DataTable = Database.GetData(sqlCheckSummaryTraceability)
                    If dtCheckSummaryTraceability.Rows.Count = 0 Then
                        Dim sqlInsertSummaryFG As String = "INSERT INTO SUMMARY_TRACEABILITY([DATE], [SUB_SUB_PO], [LINE], [FG], [LASER_CODE], [INV], [BATCH_NO], [LOT_NO], [TRACEABILITY],
                                            [INSPECTOR], [PACKER1], [PACKER2], [PACKER3], [PACKER4]) VALUES (getdate(), '" & dttable.Rows(u).Item("sub_sub_po") & "', 
                                            '" & dttable.Rows(u).Item("line") & "', '" & dttable.Rows(u).Item("fg") & "', '" & dttable.Rows(u).Item("laser_code") & "', '" & dttable.Rows(u).Item("INV_CTRL_DATE") & "', 
                                            '" & dttable.Rows(u).Item("BATCH_NO") & "', '" & dttable.Rows(u).Item("LOT_NO") & "', '" & dttable.Rows(u).Item("TRACEABILITY") & "', '" & dttable.Rows(u).Item("inspector") & "', 
                                            '" & dttable.Rows(u).Item("packer1") & "', '" & dttable.Rows(u).Item("packer2") & "', '" & dttable.Rows(u).Item("packer3") & "', '" & dttable.Rows(u).Item("packer4") & "')"
                        Dim cmdInsertSummaryFG = New SqlCommand(sqlInsertSummaryFG, Database.koneksi)
                        cmdInsertSummaryFG.ExecuteNonQuery()
                    End If
                Next
            End If

        Else

            Dim sqlStr As String = "SELECT d.DATETIME_INSERT,d.sub_sub_po,d.line,d.fg,d.laser_code,d.INV_CTRL_DATE,d.BATCH_NO,d.LOT_NO,d.traceability,f.inspector,f.packer1,f.packer2,f.packer3,f.packer4 FROM done_fg d left join fga f on d.sub_sub_po=f.sub_sub_po and d.flow_ticket=f.no_flowticket WHERE d.fg= '" & txtFG.Text & "' and d.sub_sub_po='" & txtSubSubPO.Text & "'"
            Dim dttable As DataTable = Database.GetData(sqlStr)
            If dttable.Rows.Count > 0 Then
                For u = 0 To dttable.Rows.Count - 1
                    Dim sqlCheckSummaryTraceability As String = "SELECT * FROM summary_traceability WHERE sub_sub_po = '" & dttable.Rows(u).Item("sub_sub_po") & "' and fg='" & txtFG.Text & "' and lot_no='" & dttable.Rows(u).Item("LOT_NO") & "'"
                    Dim dtCheckSummaryTraceability As DataTable = Database.GetData(sqlCheckSummaryTraceability)
                    If dtCheckSummaryTraceability.Rows.Count = 0 Then
                        Dim sqlInsertSummaryFG As String = "INSERT INTO SUMMARY_TRACEABILITY([DATE], [SUB_SUB_PO], [LINE], [FG], [LASER_CODE], [INV], [BATCH_NO], [LOT_NO], [TRACEABILITY],
                                            [INSPECTOR], [PACKER1], [PACKER2], [PACKER3], [PACKER4]) VALUES (getdate(), '" & dttable.Rows(u).Item("sub_sub_po") & "', 
                                            '" & dttable.Rows(u).Item("line") & "', '" & dttable.Rows(u).Item("fg") & "', '" & dttable.Rows(u).Item("laser_code") & "', '" & dttable.Rows(u).Item("INV_CTRL_DATE") & "', 
                                            '" & dttable.Rows(u).Item("BATCH_NO") & "', '" & dttable.Rows(u).Item("LOT_NO") & "', '" & dttable.Rows(u).Item("TRACEABILITY") & "', '" & dttable.Rows(u).Item("inspector") & "', 
                                            '" & dttable.Rows(u).Item("packer1") & "', '" & dttable.Rows(u).Item("packer2") & "', '" & dttable.Rows(u).Item("packer3") & "', '" & dttable.Rows(u).Item("packer4") & "')"
                        Dim cmdInsertSummaryFG = New SqlCommand(sqlInsertSummaryFG, Database.koneksi)
                        cmdInsertSummaryFG.ExecuteNonQuery()
                    End If
                Next
            End If

        End If

        Dim sqlStrZ As String = "select * from summary_traceability where fg='" & txtFG.Text & "' and sub_sub_po='" & txtSubSubPO.Text & "'"
        Dim dttableZ As DataTable = Database.GetData(sqlStrZ)
        If dttableZ.Rows.Count > 0 Then
            For z = 0 To dttableZ.Rows.Count - 1
                Dim sqlCheckOperatorDetails As String = "SELECT * FROM prod_dop_details WHERE sub_sub_po= '" & dttableZ.Rows(z).Item("sub_sub_po") & "' and lot_flow_ticket=" & dttableZ.Rows(z).Item("lot_no")
                Dim dtOperatorDetails As DataTable = Database.GetData(sqlCheckOperatorDetails)
                If dtOperatorDetails.Rows.Count > 0 Then
                    For l As Integer = 0 To dtOperatorDetails.Rows.Count - 1
                        Dim SqlUpdate As String = "UPDATE summary_traceability SET process" & dtOperatorDetails.Rows(l).Item("process_number") & "='" & dtOperatorDetails.Rows(l).Item("process") & "(" & dtOperatorDetails.Rows(l).Item("operator") & ")' WHERE id=" & dttableZ.Rows(z).Item("id")
                        Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                        cmdUpdate.ExecuteNonQuery()
                    Next
                Else
                    Dim sqlOperator As String = "SELECT * FROM prod_dop WHERE fg_pn= '" & txtFG.Text & "' and sub_sub_po='" & dttableZ.Rows(z).Item("sub_sub_po") & "'"
                    Dim dtOperator As DataTable = Database.GetData(sqlOperator)
                    If dtOperator.Rows.Count > 0 Then
                        For l As Integer = 0 To dtOperator.Rows.Count - 1
                            Dim SqlUpdate As String = "UPDATE summary_traceability SET process" & dtOperator.Rows(l).Item("process_number") & "='" & dtOperator.Rows(l).Item("process") & "(" & dtOperator.Rows(l).Item("operator_id") & ")' WHERE id=" & dttableZ.Rows(z).Item("id")
                            Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                            cmdUpdate.ExecuteNonQuery()
                        Next
                    End If
                End If
            Next
        End If

        Dim sqlStrMat As String = "select sc.line,sc.material,mm.name,sc.inv_ctrl_date,sc.batch_no,sc.lot_no,sc.flow_ticket,sc.traceability,sc.remark,qty,qrcode, qrcode_sa from stock_card sc, master_material mm where sc.status='Production Result' and sc.finish_goods_pn='" & txtFG.Text & "' and sc.sub_sub_po='" & txtSubSubPO.Text & "' and sc.material=mm.part_number order by sc.line,sc.flow_ticket,sc.material"
        Dim dttableMat As DataTable = Database.GetData(sqlStrMat)
        If dttableMat.Rows.Count > 0 Then
            For m = 0 To dttableMat.Rows.Count - 1
                Dim sqlCheckSummaryTraceability As String = "SELECT * FROM summary_traceability_comp WHERE lot_fg='" & dttableMat.Rows(m).Item("flow_ticket") & "' and component='" & dttableMat.Rows(m).Item("material") & "' and sub_sub_po='" & txtSubSubPO.Text & "' and lot_comp='" & dttableMat.Rows(m).Item("lot_no") & "'"
                Dim dtCheckSummaryTraceability As DataTable = Database.GetData(sqlCheckSummaryTraceability)
                If dtCheckSummaryTraceability.Rows.Count = 0 Then

                    Dim sqlInsertSummaryFG As String = "INSERT INTO [SUMMARY_TRACEABILITY_COMP]([LINE], [COMPONENT], [DESC], [INV], [BATCH_NO], [LOT_COMP], [TRACEABILITY], [QRCODE], [LOT_FG], [QTY], [SUB_SUB_PO], [REMARK]) 
                                            VALUES ('" & dttableMat.Rows(m).Item("line") & "', '" & dttableMat.Rows(m).Item("material") & "', '" & dttableMat.Rows(m).Item("name") & "', 
                                            '" & dttableMat.Rows(m).Item("inv_ctrl_date") & "', '" & dttableMat.Rows(m).Item("batch_no") & "', '" & dttableMat.Rows(m).Item("lot_no") & "', 
                                            '" & dttableMat.Rows(m).Item("traceability") & "', '" & dttableMat.Rows(m).Item("qrcode") & "', 
                                            '" & dttableMat.Rows(m).Item("flow_ticket") & "', " & dttableMat.Rows(m).Item("qty").ToString().Replace(",", ".") & ", '" & txtSubSubPO.Text & "','" & dttableMat.Rows(m).Item("qrcode_sa") & "')"
                    Dim cmdInsertSummaryFG = New SqlCommand(sqlInsertSummaryFG, Database.koneksi)
                    cmdInsertSummaryFG.ExecuteNonQuery()
                End If
            Next
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Try
            Call Database.koneksi_database()
            If DataGridView1.Columns(e.ColumnIndex).Name = "Remark" Then
                If globVar.update > 0 Then
                    If Not IsDBNull(DataGridView1.Rows(e.RowIndex).Cells("Remark").Value) AndAlso Not String.IsNullOrEmpty(DataGridView1.Rows(e.RowIndex).Cells("Remark").Value) Then
                        Dim Sql As String = "update summary_fg set remark='" & DataGridView1.Rows(e.RowIndex).Cells("Remark").Value & "' where sub_sub_po='" & txtSubSubPO.Text & "' and line='" & ComboBox1.Text & "' and comp='" & DataGridView1.Rows(e.RowIndex).Cells("COMP").Value & "'"
                        Dim cmd = New SqlCommand(Sql, Database.koneksi)
                        If cmd.ExecuteNonQuery() Then
                            Dim SqlRemark As String = "update sub_sub_po set remark=1 where sub_sub_po='" & txtSubSubPO.Text & "' and line='" & ComboBox1.Text & "'"
                            Dim cmdRemark = New SqlCommand(SqlRemark, Database.koneksi)
                            cmdRemark.ExecuteNonQuery()
                            RJMessageBox.Show("Success updated data")
                        End If
                    End If
                Else
                    RJMessageBox.Show("Your Access cannot execute this action")
                End If
            End If
        Catch ex As Exception
            RJMessageBox.Show("Error Summary - 1 =>" & ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If DataGridView1.Columns(e.ColumnIndex).Name = "Total In" Then
            e.CellStyle.BackColor = Color.Yellow
        End If

        If DataGridView1.Columns(e.ColumnIndex).Name = "Total Out" Then
            e.CellStyle.BackColor = Color.Yellow
        End If
    End Sub

    Private Sub DataGridView2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellValueChanged
        Try
            Call Database.koneksi_database()
            If DataGridView2.Columns(e.ColumnIndex).Name = "QTY Adjust" Or DataGridView2.Columns(e.ColumnIndex).Name = "Remark" Then

                If IsDBNull(DataGridView2.Rows(e.RowIndex).Cells("Remark").Value) OrElse String.IsNullOrEmpty(DataGridView2.Rows(e.RowIndex).Cells("Remark").Value.ToString()) Then
                    RJMessageBox.Show("Please input remark first")
                    Exit Sub
                End If

                If selectdata = "fresh_in" Or selectdata = "sub_assy_in" Or selectdata = "others_in" Then
                    If globVar.update > 0 Then
                        Dim Sql As String = "update stock_card set qty_adjust='" & DataGridView2.Rows(e.RowIndex).Cells("QTY Adjust").Value & "', remark_qty_adjust='" & DataGridView2.Rows(e.RowIndex).Cells("Remark").Value & "' where id='" & DataGridView2.Rows(e.RowIndex).Cells("#").Value & "'"
                        Dim cmd = New SqlCommand(Sql, Database.koneksi)
                        If cmd.ExecuteNonQuery() Then

                            LoadData()

                            treeView_show(txtSubSubPO.Text, selectComp)

                            RJMessageBox.Show("Success updated data")

                        End If
                    Else
                        RJMessageBox.Show("Your Access cannot execute this action")
                    End If
                    'ElseIf selectdata = "reject" Then
                    '    If globVar.update > 0 Then
                    '        Dim Sql As String = "update out_prod_reject set qty='" & DataGridView2.Rows(e.RowIndex).Cells("QTY").Value & "' where id='" & DataGridView2.Rows(e.RowIndex).Cells("#").Value & "'"
                    '        Dim cmd = New SqlCommand(Sql, Database.koneksi)
                    '        If cmd.ExecuteNonQuery() Then
                    '            LoadData()
                    '            RJMessageBox.Show("Success updated data")
                    '        End If
                    '    Else
                    '        RJMessageBox.Show("Your Access cannot execute this action")
                    '    End If
                    'ElseIf selectdata = "return" Then
                    '    If globVar.update > 0 Then
                    '        Dim Sql As String = "update stock_card set qty='" & DataGridView2.Rows(e.RowIndex).Cells("QTY").Value & "' where id='" & DataGridView2.Rows(e.RowIndex).Cells("#").Value & "'"
                    '        Dim cmd = New SqlCommand(Sql, Database.koneksi)
                    '        If cmd.ExecuteNonQuery() Then
                    '            LoadData()
                    '            RJMessageBox.Show("Success updated data")
                    '        End If
                    '    Else
                    '        RJMessageBox.Show("Your Access cannot execute this action")
                    '    End If
                End If
            End If
        Catch ex As Exception
            RJMessageBox.Show("Error Summary - 1 =>" & ex.Message)
        End Try
    End Sub

    Private Sub DataGridView2_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridView2.DataError
        ' Handle the data error event to provide custom error messages
        RJMessageBox.Show("QTY Adjust must be number", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        e.ThrowException = False
    End Sub

    Sub clearAll()
        tampilDataComboBoxLine()
        txtFG.Clear()
        txtPO.Clear()
        txtSubPO.Clear()
        txtSubSubPO.Clear()
        txtSubSubPOQty.Clear()

        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()

        DataGridView2.DataSource = Nothing
        DataGridView2.Rows.Clear()
        DataGridView2.Columns.Clear()

        TreeView1.Nodes.Clear()
    End Sub
End Class