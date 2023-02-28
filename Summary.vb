Imports System.Data.SqlClient
Imports System.Runtime.Remoting
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.Office.Interop

Public Class Summary
    Sub loadDGV()
        With DGSummaryV2
            .DefaultCellStyle.Font = New Font("Tahoma", 14)

            .ColumnCount = 17
            .Columns(0).HeaderText = "Sub Sub PO"
            .Columns(1).HeaderText = "FG"
            .Columns(2).HeaderText = "Mat"
            .Columns(3).HeaderText = "Fresh"
            .Columns(4).HeaderText = "Others"
            .Columns(5).HeaderText = "WIP"
            .Columns(6).HeaderText = "On Hold"
            .Columns(7).HeaderText = "Sub Assy"
            .Columns(8).HeaderText = "Sum Input"
            .Columns(9).HeaderText = "Return"
            .Columns(10).HeaderText = "Defect"
            .Columns(11).HeaderText = "Others"
            .Columns(12).HeaderText = "WIP"
            .Columns(13).HeaderText = "On Hold"
            .Columns(14).HeaderText = "FG"
            .Columns(15).HeaderText = "Balance"
            .Columns(16).HeaderText = "Sum Output"

            For i As Integer = 0 To .ColumnCount - 1
                .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

            Dim sqlStr As String = "select mp.fg_pn, mufg.component from material_usage_finish_goods mufg, main_po mp, sub_sub_po ssp where mufg.fg_part_number=mp.fg_pn and mp.id=ssp.main_po and ssp.sub_sub_po='" & txtSummarySubSubPO.Text & "'"

            .EnableHeadersVisualStyles = False
            With .ColumnHeadersDefaultCellStyle
                .BackColor = Color.Navy
                .ForeColor = Color.White
                .Font = New Font("Tahoma", 13, FontStyle.Bold)
                .Alignment = HorizontalAlignment.Center
                .Alignment = ContentAlignment.MiddleCenter
            End With

            Dim dttable As DataTable = Database.GetData(sqlStr)

            If dttable.Rows.Count > 0 Then
                .Rows.Clear()

                For i = 0 To dttable.Rows.Count - 1
                    Dim dtInFresh As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='Fresh'")
                    Dim dtInOthers As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='OT'")
                    Dim dtInWIP As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='WIP'")
                    Dim dtInOnHold As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='OH'")
                    Dim dtInSA As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='SA'")

                    Dim dtOutReturn As DataTable = Database.GetData("select isnull(sum(qty),0) from out_prod_reject where sub_sub_po='" & txtSummarySubSubPO.Text & "' and part_number='" & dttable.Rows(i).Item("component") & "'")
                    Dim dtOutDefect As DataTable = Database.GetData("select isnull(sum(actual_qty),0) from out_prod_defect where sub_sub_po='" & txtSummarySubSubPO.Text & "' and part_number='" & dttable.Rows(i).Item("component") & "'")
                    Dim dtOutOthers As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_prod_others where code_out_prod_defect in (select DISTINCT(code_out_prod_defect) from out_prod_defect where sub_sub_po='" & txtSummarySubSubPO.Text & "') and part_number='" & dttable.Rows(i).Item("component") & "'")
                    Dim dtOutWIP As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_prod_wip where sub_sub_po='" & txtSummarySubSubPO.Text & "' and part_number='" & dttable.Rows(i).Item("component") & "'")
                    Dim dtOutOnHold As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_prod_onhold where sub_sub_po='" & txtSummarySubSubPO.Text & "' and part_number='" & dttable.Rows(i).Item("component") & "'")
                    Dim dtOutFG As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Result'")
                    Dim dtOutBalance As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Return To Mini Store'")

                    .Rows.Add(1)
                    .Item(0, i).Value = txtSummarySubSubPO.Text
                    .Item(1, i).Value = dttable.Rows(i).Item("fg_pn")
                    .Item(2, i).Value = dttable.Rows(i).Item("component")
                    .Item(3, i).Value = dtInFresh.Rows(0)(0)
                    .Item(4, i).Value = dtInOthers.Rows(0)(0)
                    .Item(5, i).Value = dtInWIP.Rows(0)(0)
                    .Item(6, i).Value = dtInOnHold.Rows(0)(0)
                    .Item(7, i).Value = dtInSA.Rows(0)(0)
                    .Item(8, i).Value = Math.Round(dtInFresh.Rows(0)(0) + dtInOthers.Rows(0)(0) + dtInWIP.Rows(0)(0) + dtInOnHold.Rows(0)(0) + dtInSA.Rows(0)(0))
                    .Item(8, i).Style.BackColor = Color.Green
                    .Item(9, i).Value = dtOutReturn.Rows(0)(0)
                    .Item(10, i).Value = dtOutDefect.Rows(0)(0)
                    .Item(11, i).Value = dtOutOthers.Rows(0)(0)
                    .Item(12, i).Value = dtOutWIP.Rows(0)(0)
                    .Item(13, i).Value = dtOutOnHold.Rows(0)(0)
                    .Item(14, i).Value = dtOutFG.Rows(0)(0)
                    .Item(15, i).Value = dtOutBalance.Rows(0)(0)
                    .Item(16, i).Value = Math.Round(dtOutReturn.Rows(0)(0) + dtOutOthers.Rows(0)(0) + dtOutWIP.Rows(0)(0) + dtOutOnHold.Rows(0)(0) + dtOutFG.Rows(0)(0) + dtOutBalance.Rows(0)(0) + dtOutDefect.Rows(0)(0))
                    If .Item(16, i).Value = .Item(8, i).Value Then
                        .Item(16, i).Style.BackColor = Color.Green
                    Else
                        .Item(16, i).Style.BackColor = Color.Red
                    End If
                Next
            Else
                .Rows.Clear()
            End If

            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Function SummaryFG(sub_sub_po As String)
        Dim _Result As Integer = 0
        Dim sqlStr As String = "select mp.fg_pn, mufg.component from material_usage_finish_goods mufg, main_po mp, sub_sub_po ssp where mufg.fg_part_number=mp.fg_pn and mp.id=ssp.main_po and ssp.sub_sub_po='" & txtSummarySubSubPO.Text & "'"
        Dim dttable As DataTable = Database.GetData(sqlStr)
        For i = 0 To dttable.Rows.Count - 1
            Dim dtInFresh As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & sub_sub_po & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='Fresh'")
            Dim dtInOthers As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & sub_sub_po & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='OT'")
            Dim dtInWIP As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & sub_sub_po & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='WIP'")
            Dim dtInOnHold As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & sub_sub_po & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='OH'")
            Dim dtInSA As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & sub_sub_po & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='SA'")

            Dim dtOutReturn As DataTable = Database.GetData("select isnull(sum(qty),0) from out_prod_reject where sub_sub_po='" & sub_sub_po & "' and part_number='" & dttable.Rows(i).Item("component") & "'")
            Dim dtOutDefect As DataTable = Database.GetData("select isnull(sum(actual_qty),0) from out_prod_defect where sub_sub_po='" & sub_sub_po & "' and part_number='" & dttable.Rows(i).Item("component") & "'")
            Dim dtOutOthers As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_prod_others where code_out_prod_defect in (select DISTINCT(code_out_prod_defect) from out_prod_defect where sub_sub_po='" & sub_sub_po & "') and part_number='" & dttable.Rows(i).Item("component") & "'")
            Dim dtOutWIP As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_prod_wip where sub_sub_po='" & sub_sub_po & "' and part_number='" & dttable.Rows(i).Item("component") & "'")
            Dim dtOutOnHold As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_prod_onhold where sub_sub_po='" & sub_sub_po & "' and part_number='" & dttable.Rows(i).Item("component") & "'")
            Dim dtOutFG As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & sub_sub_po & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Result'")
            Dim dtOutBalance As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & sub_sub_po & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Return To Mini Store'")

            Dim sqlCheckSummaryFG As String = "SELECT * FROM summary_fg WHERE sub_sub_po = '" & sub_sub_po & "' and material='" & dttable.Rows(i).Item("component") & "'"
            Dim dtCheckSummaryFG As DataTable = Database.GetData(sqlCheckSummaryFG)
            If dtCheckSummaryFG.Rows.Count = 0 Then
                Dim sqlInsertSummaryFG As String = "INSERT INTO summary_fg([SUB_SUB_PO], [FG], [MATERIAL], [FRESH_IN], [BALANCE_IN], [OTHERS_IN], [WIP_IN],
                [ONHOLD_IN], [SA_IN], [TOTAL_IN], [RETURN_OUT], [DEFECT_OUT], [OTHERS_OUT], [WIP_OUT], [ONHOLD_OUT], [BALANCE_OUT], [FG_OUT], [TOTAL_OUT]) 
                VALUES ('" & sub_sub_po & "','" & dttable.Rows(i).Item("fg_pn") & "',
                '" & dttable.Rows(i).Item("component") & "'," & dtInFresh.Rows(0)(0).ToString().Replace(",", ".") & ",0," & dtInOthers.Rows(0)(0).ToString().Replace(",", ".") & ",
                " & dtInWIP.Rows(0)(0).ToString().Replace(",", ".") & "," & dtInOnHold.Rows(0)(0).ToString().Replace(",", ".") & "," & dtInSA.Rows(0)(0).ToString().Replace(",", ".") & ",
                " & Math.Round(dtInFresh.Rows(0)(0) + dtInOthers.Rows(0)(0) + dtInWIP.Rows(0)(0) + dtInOnHold.Rows(0)(0) + dtInSA.Rows(0)(0)).ToString().Replace(",", ".") & ",
                " & dtOutReturn.Rows(0)(0).ToString().Replace(",", ".") & "," & dtOutDefect.Rows(0)(0).ToString().Replace(",", ".") & "," & dtOutOthers.Rows(0)(0).ToString().Replace(",", ".") & ",
                " & dtOutWIP.Rows(0)(0).ToString().Replace(",", ".") & "," & dtOutOnHold.Rows(0)(0).ToString().Replace(",", ".") & "," & dtOutBalance.Rows(0)(0).ToString().Replace(",", ".") & ",
                " & dtOutFG.Rows(0)(0).ToString().Replace(",", ".") & "," & Math.Round(dtOutReturn.Rows(0)(0) + dtOutOthers.Rows(0)(0) + dtOutWIP.Rows(0)(0) + dtOutOnHold.Rows(0)(0) + dtOutFG.Rows(0)(0) + dtOutBalance.Rows(0)(0) + dtOutDefect.Rows(0)(0)).ToString().Replace(",", ".") & ")"
                Dim cmdInsertSummaryFG = New SqlCommand(sqlInsertSummaryFG, Database.koneksi)
                cmdInsertSummaryFG.ExecuteNonQuery()
            End If
            _Result = _Result + 1
        Next
        Return _Result
    End Function

    'Sub loadTraceability1()
    '    With DGTraceability1V2
    '        .DefaultCellStyle.Font = New Font("Tahoma", 14)

    '        .ColumnCount = 43
    '        .Columns(0).HeaderText = "Date"
    '        .Columns(1).HeaderText = "Sub Sub PO"
    '        .Columns(2).HeaderText = "Line"
    '        .Columns(3).HeaderText = "FG"
    '        .Columns(4).HeaderText = "Laser Code"
    '        .Columns(5).HeaderText = "Inv."
    '        .Columns(6).HeaderText = "Batch No"
    '        .Columns(7).HeaderText = "Lot No"
    '        .Columns(8).HeaderText = "Inspector"
    '        .Columns(9).HeaderText = "Packer 1"
    '        .Columns(10).HeaderText = "Packer 2"
    '        .Columns(11).HeaderText = "Packer 3"
    '        .Columns(12).HeaderText = "Packer 4"
    '        .Columns(13).HeaderText = "Process 1"
    '        .Columns(14).HeaderText = "Process 2"
    '        .Columns(15).HeaderText = "Process 3"
    '        .Columns(16).HeaderText = "Process 4"
    '        .Columns(17).HeaderText = "Process 5"
    '        .Columns(18).HeaderText = "Process 6"
    '        .Columns(19).HeaderText = "Process 7"
    '        .Columns(20).HeaderText = "Process 8"
    '        .Columns(21).HeaderText = "Process 9"
    '        .Columns(22).HeaderText = "Process 10"
    '        .Columns(23).HeaderText = "Process 11"
    '        .Columns(24).HeaderText = "Process 12"
    '        .Columns(25).HeaderText = "Process 13"
    '        .Columns(26).HeaderText = "Process 14"
    '        .Columns(27).HeaderText = "Process 15"
    '        .Columns(28).HeaderText = "Process 16"
    '        .Columns(29).HeaderText = "Process 17"
    '        .Columns(30).HeaderText = "Process 18"
    '        .Columns(31).HeaderText = "Process 19"
    '        .Columns(32).HeaderText = "Process 20"
    '        .Columns(33).HeaderText = "Process 21"
    '        .Columns(34).HeaderText = "Process 22"
    '        .Columns(35).HeaderText = "Process 23"
    '        .Columns(36).HeaderText = "Process 24"
    '        .Columns(37).HeaderText = "Process 25"
    '        .Columns(38).HeaderText = "Process 26"
    '        .Columns(39).HeaderText = "Process 27"
    '        .Columns(40).HeaderText = "Process 28"
    '        .Columns(41).HeaderText = "Process 29"
    '        .Columns(42).HeaderText = "Process 30"

    '        For i As Integer = 0 To .ColumnCount - 1
    '            .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '        Next

    '        Dim sqlStr As String = "SELECT d.DATETIME_INSERT,d.sub_sub_po,d.line,d.fg,d.laser_code,d.INV_CTRL_DATE,d.BATCH_NO,d.LOT_NO,f.inspector,f.packer1,f.packer2,f.packer3,f.packer4 FROM done_fg d left join fga f on d.sub_sub_po=f.sub_sub_po and d.flow_ticket=f.no_flowticket WHERE d.fg= '" & txtTraceability.Text & "'"

    '        .EnableHeadersVisualStyles = False
    '        With .ColumnHeadersDefaultCellStyle
    '            .BackColor = Color.Navy
    '            .ForeColor = Color.White
    '            .Font = New Font("Tahoma", 13, FontStyle.Bold)
    '            .Alignment = HorizontalAlignment.Center
    '            .Alignment = ContentAlignment.MiddleCenter
    '        End With

    '        Dim dttable As DataTable = Database.GetData(sqlStr)

    '        If dttable.Rows.Count > 0 Then
    '            .Rows.Clear()
    '            For i = 0 To dttable.Rows.Count - 1
    '                .Rows.Add(1)
    '                .Item(0, i).Value = dttable.Rows(i).Item("DATETIME_INSERT")
    '                .Item(1, i).Value = dttable.Rows(i).Item("sub_sub_po")
    '                .Item(2, i).Value = dttable.Rows(i).Item("line")
    '                .Item(3, i).Value = dttable.Rows(i).Item("fg")
    '                .Item(4, i).Value = dttable.Rows(i).Item("laser_code")
    '                .Item(5, i).Value = dttable.Rows(i).Item("INV_CTRL_DATE")
    '                .Item(6, i).Value = dttable.Rows(i).Item("BATCH_NO")
    '                .Item(7, i).Value = dttable.Rows(i).Item("LOT_NO")
    '                .Item(8, i).Value = dttable.Rows(i).Item("inspector")
    '                .Item(9, i).Value = dttable.Rows(i).Item("packer1")
    '                .Item(10, i).Value = dttable.Rows(i).Item("packer2")
    '                .Item(11, i).Value = dttable.Rows(i).Item("packer3")
    '                .Item(12, i).Value = dttable.Rows(i).Item("packer4")
    '            Next
    '        Else
    '            .Rows.Clear()
    '        End If

    '        .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
    '    End With

    '    For Each col As DataGridViewColumn In DGTraceability1V2.Columns
    '        col.MinimumWidth = 300
    '    Next
    '    loadTraceability2()

    '    loadOperator()

    '    Dim rowIndexMax As Integer = 5
    '    Dim colors As New List(Of Color)({Color.LightBlue, Color.LightGreen, Color.LightGray, Color.LightPink, Color.LightYellow})
    '    Dim colorsIndexNow = 0

    '    For i As Integer = 0 To DGTraceability1V2.RowCount - 1
    '        If i = 0 Then
    '            colorsIndexNow = 0
    '        Else
    '            If colorsIndexNow = rowIndexMax - 1 Then
    '                colorsIndexNow = 0
    '            Else
    '                colorsIndexNow += 1
    '            End If
    '        End If
    '        DGTraceability1.Rows(i).DefaultCellStyle.BackColor = colors(colorsIndexNow)
    '    Next
    'End Sub

    Function summaryTraceability(fg)
        Dim _result As Integer = 0
        Dim sqlStr As String = "SELECT d.DATETIME_INSERT,d.sub_sub_po,d.line,d.fg,d.laser_code,d.INV_CTRL_DATE,d.BATCH_NO,d.LOT_NO,f.inspector,f.packer1,f.packer2,f.packer3,f.packer4 FROM done_fg d left join fga f on d.sub_sub_po=f.sub_sub_po and d.flow_ticket=f.no_flowticket WHERE d.fg= '" & fg & "'"
        Dim dttable As DataTable = Database.GetData(sqlStr)
        If dttable.Rows.Count > 0 Then
            For i = 0 To dttable.Rows.Count - 1
                Dim sqlCheckSummaryTraceability As String = "SELECT * FROM summary_traceability WHERE sub_sub_po = '" & dttable.Rows(i).Item("sub_sub_po") & "' and fg='" & fg & "' and lot_no='" & dttable.Rows(i).Item("LOT_NO") & "'"
                Dim dtCheckSummaryTraceability As DataTable = Database.GetData(sqlCheckSummaryTraceability)
                If dtCheckSummaryTraceability.Rows.Count = 0 Then
                    Dim sqlInsertSummaryFG As String = "INSERT INTO SUMMARY_TRACEABILITY([DATE], [SUB_SUB_PO], [LINE], [FG], [LASER_CODE], [INV], [BATCH_NO], [LOT_NO], 
                        [INSPECTOR], [PACKER1], [PACKER2], [PACKER3], [PACKER4]) VALUES (cast(CONVERT(datetime, '" & dttable.Rows(i).Item("DATETIME_INSERT") & "', 105) as varchar(19)), '" & dttable.Rows(i).Item("sub_sub_po") & "', 
                        '" & dttable.Rows(i).Item("line") & "', '" & dttable.Rows(i).Item("fg") & "', '" & dttable.Rows(i).Item("laser_code") & "', '" & dttable.Rows(i).Item("INV_CTRL_DATE") & "', 
                        '" & dttable.Rows(i).Item("BATCH_NO") & "', '" & dttable.Rows(i).Item("LOT_NO") & "', '" & dttable.Rows(i).Item("inspector") & "', '" & dttable.Rows(i).Item("packer1") & "', 
                        '" & dttable.Rows(i).Item("packer2") & "', '" & dttable.Rows(i).Item("packer3") & "', '" & dttable.Rows(i).Item("packer4") & "')"
                    Dim cmdInsertSummaryFG = New SqlCommand(sqlInsertSummaryFG, Database.koneksi)
                    cmdInsertSummaryFG.ExecuteNonQuery()
                End If
                _result = _result + 1
            Next
        End If
        Return _result
    End Function

    'Sub loadOperator()
    '    If DGTraceability1.Rows.Count > 0 Then

    '        For r As Integer = 0 To DGTraceability1.Rows.Count - 1
    '            For c As Integer = 0 To DGTraceability1.Columns.Count - 1
    '                Dim sqlCheckOperatorDetails As String = "SELECT * FROM prod_dop_details WHERE sub_sub_po= '" & DGTraceability1.Rows(r).Cells(1).Value & "'"
    '                Dim dtOperatorDetails As DataTable = Database.GetData(sqlCheckOperatorDetails)
    '                If dtOperatorDetails.Rows.Count > 0 Then
    '                    For l As Integer = 0 To dtOperatorDetails.Rows.Count - 1
    '                        If DGTraceability1.Columns(c).Index > 12 Then
    '                            If DGTraceability1.Columns(c).HeaderText = "Process " & dtOperatorDetails.Rows(l).Item("process_number") Then
    '                                If r + 1 = dtOperatorDetails.Rows(l).Item("lot_flow_ticket") Then
    '                                    DGTraceability1(c, r).Value = dtOperatorDetails.Rows(l).Item("process") & "(" & dtOperatorDetails.Rows(l).Item("operator") & ")"
    '                                End If
    '                            End If
    '                        End If
    '                    Next
    '                Else
    '                    Dim sqlOperator As String = "SELECT * FROM prod_dop WHERE fg_pn= '" & txtTraceability.Text & "' and sub_sub_po='" & DGTraceability1.Rows(r).Cells(1).Value & "'"
    '                    Dim dtOperator As DataTable = Database.GetData(sqlOperator)
    '                    If DGTraceability1.Rows.Count > 0 Then
    '                        For l As Integer = 0 To dtOperator.Rows.Count - 1
    '                            If DGTraceability1.Columns(c).Index > 12 Then
    '                                If DGTraceability1.Columns(c).HeaderText = "Process " & dtOperator.Rows(l).Item("process_number") Then
    '                                    DGTraceability1(c, r).Value = dtOperator.Rows(l).Item("process") & "(" & dtOperator.Rows(l).Item("operator_id") & ")"
    '                                End If
    '                            End If
    '                        Next
    '                    End If
    '                End If
    '            Next
    '        Next
    '    End If
    'End Sub

    'Sub loadTraceability2()
    '    With DGTraceability2
    '        .DefaultCellStyle.Font = New Font("Tahoma", 14)

    '        .ColumnCount = 9
    '        .Columns(0).HeaderText = "Line"
    '        .Columns(1).HeaderText = "Component"
    '        .Columns(2).HeaderText = "Desc"
    '        .Columns(3).HeaderText = "Inv."
    '        .Columns(4).HeaderText = "Batch No"
    '        .Columns(5).HeaderText = "Lot Component"
    '        .Columns(6).HeaderText = "Lot FG"
    '        .Columns(7).HeaderText = "Qty"
    '        .Columns(8).HeaderText = "Remark"

    '        For i As Integer = 0 To .ColumnCount - 1
    '            .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '        Next

    '        Dim sqlStr As String = "select sc.line,sc.material,mm.name,sc.inv_ctrl_date,sc.batch_no,sc.lot_no,sc.flow_ticket,qty,id_level from stock_card sc, master_material mm where sc.status='Production Result' and sc.finish_goods_pn='" & txtTraceability.Text & "' and sc.material=mm.part_number order by sc.line,sc.flow_ticket,sc.material"

    '        .EnableHeadersVisualStyles = False
    '        With .ColumnHeadersDefaultCellStyle
    '            .BackColor = Color.Navy
    '            .ForeColor = Color.White
    '            .Font = New Font("Tahoma", 13, FontStyle.Bold)
    '            .Alignment = HorizontalAlignment.Center
    '            .Alignment = ContentAlignment.MiddleCenter
    '        End With

    '        Dim dttable As DataTable = Database.GetData(sqlStr)

    '        If dttable.Rows.Count > 0 Then
    '            .Rows.Clear()
    '            For i = 0 To dttable.Rows.Count - 1
    '                .Rows.Add(1)
    '                .Item(0, i).Value = dttable.Rows(i).Item("line")
    '                .Item(1, i).Value = dttable.Rows(i).Item("material")
    '                .Item(2, i).Value = dttable.Rows(i).Item("name")
    '                .Item(3, i).Value = dttable.Rows(i).Item("inv_ctrl_date")
    '                .Item(4, i).Value = dttable.Rows(i).Item("batch_no")
    '                .Item(5, i).Value = dttable.Rows(i).Item("lot_no")
    '                .Item(6, i).Value = dttable.Rows(i).Item("flow_ticket")
    '                .Item(7, i).Value = dttable.Rows(i).Item("qty")
    '                If InStr(dttable.Rows(i).Item("id_level").ToString, "SA") > 0 Or InStr(dttable.Rows(i).Item("id_level").ToString, "WIP") > 0 Or InStr(dttable.Rows(i).Item("id_level").ToString, "OT") > 0 Then
    '                    .Item(8, i).Value = dttable.Rows(i).Item("id_level")
    '                Else
    '                    .Item(8, i).Value = "Fresh"
    '                End If
    '            Next
    '        Else
    '            .Rows.Clear()
    '        End If

    '        .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
    '        .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
    '    End With

    '    Dim rowIndexMax As Integer = 5
    '    Dim colors As New List(Of Color)({Color.LightBlue, Color.LightGreen, Color.LightGray, Color.LightPink, Color.LightYellow})
    '    Dim colorsIndexFirst = 0
    '    Dim DataIndexNow = ""
    '    Dim colorsIndexNow = 0
    '    Dim colorsIndexSet = 0

    '    For i As Integer = 0 To DGTraceability2.RowCount - 1
    '        If i = 0 Then
    '            DataIndexNow = DGTraceability2.Rows(i).Cells(6).Value
    '            colorsIndexNow = colorsIndexFirst
    '        End If

    '        If DGTraceability2.Rows(i).Cells(6).Value = DataIndexNow Then
    '            colorsIndexSet = colorsIndexNow
    '        Else
    '            If colorsIndexNow = rowIndexMax - 1 Then
    '                colorsIndexNow = 0
    '            Else
    '                colorsIndexNow += 1
    '            End If
    '        End If

    '        DataIndexNow = DGTraceability2.Rows(i).Cells(6).Value
    '        DGTraceability2.Rows(i).DefaultCellStyle.BackColor = colors(colorsIndexNow)
    '    Next
    'End Sub

    Function summaryTraceabilityMat(fg)
        Dim _result As Integer = 0
        Dim _remark As String = ""
        Dim sqlStr As String = "select sc.line,sc.material,mm.name,sc.inv_ctrl_date,sc.batch_no,sc.lot_no,sc.flow_ticket,qty,id_level from stock_card sc, master_material mm where sc.status='Production Result' and sc.finish_goods_pn='" & fg & "' and sc.material=mm.part_number order by sc.line,sc.flow_ticket,sc.material"
        Dim dttable As DataTable = Database.GetData(sqlStr)
        If dttable.Rows.Count > 0 Then
            For i = 0 To dttable.Rows.Count - 1
                Dim sqlCheckSummaryTraceability As String = "SELECT * FROM summary_traceability_comp WHERE line = '" & dttable.Rows(i).Item("line") & "' and lot_fg='" & dttable.Rows(i).Item("flow_ticket") & "' and component='" & dttable.Rows(i).Item("material") & "'"
                Dim dtCheckSummaryTraceability As DataTable = Database.GetData(sqlCheckSummaryTraceability)
                If dtCheckSummaryTraceability.Rows.Count = 0 Then
                    If InStr(dttable.Rows(i).Item("id_level").ToString, "SA") > 0 Or InStr(dttable.Rows(i).Item("id_level").ToString, "WIP") > 0 Or InStr(dttable.Rows(i).Item("id_level").ToString, "OT") > 0 Then
                        _remark = dttable.Rows(i).Item("id_level")
                    Else
                        _remark = "Fresh"
                    End If
                    Dim sqlInsertSummaryFG As String = "INSERT INTO [SUMMARY_TRACEABILITY_COMP]([LINE], [COMPONENT], [DESC], [INV], [BATCH_NO], [LOT_COMP], [LOT_FG], 
                        [QTY], [REMARK]) VALUES ('" & dttable.Rows(i).Item("line") & "', '" & dttable.Rows(i).Item("material") & "', '" & dttable.Rows(i).Item("name") & "', 
                        '" & dttable.Rows(i).Item("inv_ctrl_date") & "', '" & dttable.Rows(i).Item("batch_no") & "', '" & dttable.Rows(i).Item("lot_no") & "', 
                        '" & dttable.Rows(i).Item("flow_ticket") & "', " & dttable.Rows(i).Item("qty").ToString().Replace(",", ".") & ", '" & _remark & "')"
                    Dim cmdInsertSummaryFG = New SqlCommand(sqlInsertSummaryFG, Database.koneksi)
                    cmdInsertSummaryFG.ExecuteNonQuery()
                End If
                _result = _result + 1
            Next
        End If
        Return _result
    End Function

    Function summaryTraceabilityOperator(fg)
        Dim _result As Integer = 0
        Dim sqlStr As String = "select * from summary_traceability where fg='" & fg & "'"
        Dim dttable As DataTable = Database.GetData(sqlStr)
        If dttable.Rows.Count > 0 Then
            For i = 0 To dttable.Rows.Count - 1
                Dim sqlCheckOperatorDetails As String = "SELECT * FROM prod_dop_details WHERE sub_sub_po= '" & dttable.Rows(i).Item("sub_sub_po") & "' and lot_flow_ticket=" & dttable.Rows(i).Item("lot_no")
                Dim dtOperatorDetails As DataTable = Database.GetData(sqlCheckOperatorDetails)
                If dtOperatorDetails.Rows.Count > 0 Then
                    For l As Integer = 0 To dtOperatorDetails.Rows.Count - 1
                        Dim SqlUpdate As String = "UPDATE summary_traceability SET process" & dtOperatorDetails.Rows(l).Item("process_number") & "='" & dtOperatorDetails.Rows(l).Item("process") & "(" & dtOperatorDetails.Rows(l).Item("operator") & ")' WHERE id=" & dttable.Rows(i).Item("id")
                        Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                        cmdUpdate.ExecuteNonQuery()
                    Next
                Else
                    Dim sqlOperator As String = "SELECT * FROM prod_dop WHERE fg_pn= '" & fg & "' and sub_sub_po='" & dttable.Rows(i).Item("sub_sub_po") & "'"
                    Dim dtOperator As DataTable = Database.GetData(sqlOperator)
                    If dtOperator.Rows.Count > 0 Then
                        For l As Integer = 0 To dtOperator.Rows.Count - 1
                            Dim SqlUpdate As String = "UPDATE summary_traceability SET process" & dtOperator.Rows(l).Item("process_number") & "='" & dtOperator.Rows(l).Item("process") & "(" & dtOperator.Rows(l).Item("operator_id") & ")' WHERE id=" & dttable.Rows(i).Item("id")
                            Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                            cmdUpdate.ExecuteNonQuery()
                        Next
                    End If
                End If
                _result = _result + 1
            Next
        End If
        Return _result
    End Function

    Private Sub txtSummarySubSubPO_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtSummarySubSubPO.PreviewKeyDown
        If (e.KeyData = Keys.Enter) Then
            If txtSummarySubSubPO.Text <> "" Then
                loadDGV()
                SummaryFG(txtSummarySubSubPO.Text)
            Else
                MsgBox("Sorry please fill the sub sub po")
            End If
        End If
    End Sub

    'Private Sub txtTraceability_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtTraceability.PreviewKeyDown
    '    If (e.KeyData = Keys.Enter) Then
    '        If txtTraceability.Text <> "" Then
    '            loadTraceability1()
    '            summaryTraceability(txtTraceability.Text)
    '            summaryTraceabilityMat(txtTraceability.Text)
    '            summaryTraceabilityOperator(txtTraceability.Text)
    '        Else
    '            MsgBox("Sorry please fill the sub sub po")
    '        End If
    '    End If
    'End Sub


    Private Sub exportToExcel(ByVal dgv As DataGridView)

        ' membuat objek Excel dan workbook baru
        Dim xlApp As New Excel.Application
        Dim xlWorkBook As Excel.Workbook = xlApp.Workbooks.Add
        Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets(1)

        ' mengisi data ke worksheet
        Dim dgvColumnIndex As Integer = 0
        For Each column As DataGridViewColumn In dgv.Columns
            dgvColumnIndex += 1
            xlWorkSheet.Cells(1, dgvColumnIndex) = column.HeaderText
        Next
        Dim dgvRowIndex As Integer = 0
        For Each row As DataGridViewRow In dgv.Rows
            dgvRowIndex += 1
            dgvColumnIndex = 0
            For Each cell As DataGridViewCell In row.Cells
                dgvColumnIndex += 1
                xlWorkSheet.Cells(dgvRowIndex + 1, dgvColumnIndex) = cell.Value
            Next
        Next

        ' menyimpan workbook ke file Excel
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "Excel Workbook|*.xlsx"
        saveFileDialog1.Title = "Save Excel File"
        saveFileDialog1.ShowDialog()

        If saveFileDialog1.FileName <> "" Then
            xlWorkBook.SaveAs(saveFileDialog1.FileName)
            MsgBox("File saved successfully.")
        End If

        ' membersihkan objek Excel
        xlWorkBook.Close()
        xlApp.Quit()
        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)

    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Btn_Export_DGSummary_Click(sender As Object, e As EventArgs) Handles Btn_Export_DGSummary.Click
        exportToExcel(DGSummaryV2)
    End Sub

    Private Sub btn_ExportTrace1_Click(sender As Object, e As EventArgs) Handles btn_ExportTrace1.Click
        exportToExcel(DGTraceability1V2)
    End Sub

    Private Sub btn_ExportTrace2_Click(sender As Object, e As EventArgs) Handles btn_ExportTrace2.Click
        exportToExcel(DGTraceability2V2)
    End Sub

    Private Sub Summary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'JOVANDataSet2.SUMMARY_TRACEABILITY_COMP' table. You can move, or remove it, as needed.
        Me.SUMMARY_TRACEABILITY_COMPTableAdapter.Fill(Me.JOVANDataSet2.SUMMARY_TRACEABILITY_COMP)
        'TODO: This line of code loads data into the 'JOVANDataSet1.SUMMARY_TRACEABILITY' table. You can move, or remove it, as needed.
        Me.SUMMARY_TRACEABILITYTableAdapter.Fill(Me.JOVANDataSet1.SUMMARY_TRACEABILITY)
        'TODO: This line of code loads data into the 'JOVANDataSet.SUMMARY_FG' table. You can move, or remove it, as needed.
        Me.SUMMARY_FGTableAdapter.Fill(Me.JOVANDataSet.SUMMARY_FG)

    End Sub

    Private Sub DGTraceability1V2_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DGTraceability1V2.DataBindingComplete
        DGTraceability1V2.DefaultCellStyle.Font = New Font("Tahoma", 14)
        DGTraceability1V2.EnableHeadersVisualStyles = False
        With DGTraceability1V2.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Navy
            .ForeColor = Color.White
            .Font = New Font("Tahoma", 13, FontStyle.Bold)
            .Alignment = HorizontalAlignment.Center
            .Alignment = ContentAlignment.MiddleCenter
        End With
        DGTraceability1V2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders

        For i As Integer = 0 To DGTraceability1V2.ColumnCount - 1
            DGTraceability1V2.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Next

        For Each col As DataGridViewColumn In DGTraceability1V2.Columns
            col.MinimumWidth = 300
        Next

        For i As Integer = 0 To DGTraceability1V2.RowCount - 1
            If DGTraceability1V2.Rows(i).Index Mod 2 = 0 Then
                DGTraceability1V2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DGTraceability1V2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub DGTraceability2V2_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DGTraceability2V2.DataBindingComplete
        With DGTraceability2V2
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

        For i As Integer = 0 To DGTraceability2V2.RowCount - 1
            If DGTraceability2V2.Rows(i).Index Mod 2 = 0 Then
                DGTraceability2V2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DGTraceability2V2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub DGSummaryV2_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DGSummaryV2.DataBindingComplete
        With DGSummaryV2
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

        For i As Integer = 0 To DGSummaryV2.RowCount - 1
            If DGSummaryV2.Rows(i).Index Mod 2 = 0 Then
                DGSummaryV2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DGSummaryV2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub
End Class