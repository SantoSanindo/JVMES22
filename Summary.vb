Imports System.Data.SqlClient
Imports System.Runtime.Remoting

Public Class Summary
    Private Sub Summary_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Sub loadDGV()
        With DGSummary
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

    Sub loadTraceability1()
        Dim varProcess As String = ""

        Dim queryCek As String = "select * from MASTER_PROCESS_NUMBER"
        Dim dsexist = New DataSet
        Dim adapterexist = New SqlDataAdapter(queryCek, Database.koneksi)
        adapterexist.Fill(dsexist)
        For i As Integer = 0 To dsexist.Tables(0).Rows.Count - 1
            If i = 0 Then
                varProcess = "[" + dsexist.Tables(0).Rows(i).Item("PROCESS_NAME").ToString + "]"
            Else
                varProcess = varProcess + ",[" + dsexist.Tables(0).Rows(i).Item("PROCESS_NAME").ToString + "]"
            End If
        Next

        Dim sqlStr As String = "SELECT * FROM ( SELECT d.DATETIME_INSERT [Date],d.line [Line],d.fg [FG],d.laser_code [Laser Code],d.INV_CTRL_DATE [Inv.],d.BATCH_NO [Batch No],d.LOT_NO [Lot No],f.inspector [Outgoing Inspector],f.packer1 [Packer 1],f.packer2 [Packer 2],f.packer3 [Packer 3],f.packer4 [Packer 4],p.process + ' (' + p.operator_id + ')' as process_operator,m.process_name FROM done_fg d LEFT JOIN prod_dop p ON p.sub_sub_po= d.sub_sub_po left join master_process_number m on p.process_number=m.[order] left join fga f on d.sub_sub_po=f.sub_sub_po and d.flow_ticket=f.no_flowticket WHERE d.fg= '" & txtTraceability.Text & "') t PIVOT (MAX (process_operator) FOR process_name IN ( " + varProcess + " )) pivot_table"

        Dim dttable As DataTable = Database.GetData(sqlStr)

        DGTraceability1.DataSource = dttable
        DGTraceability1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        DGTraceability1.EnableHeadersVisualStyles = False
        With DGTraceability1.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Navy
            .ForeColor = Color.White
            .Font = New Font("Tahoma", 13, FontStyle.Bold)
            .Alignment = HorizontalAlignment.Center
            .Alignment = ContentAlignment.MiddleCenter
        End With

        For Each col As DataGridViewColumn In DGTraceability1.Columns
            col.MinimumWidth = 300
        Next
        loadTraceability2()

        Dim rowIndexMax As Integer = 5
        Dim colors As New List(Of Color)({Color.LightBlue, Color.LightGreen, Color.LightGray, Color.LightPink, Color.LightYellow})
        Dim colorsIndexNow = 0

        For i As Integer = 0 To DGTraceability1.RowCount - 1
            If i = 0 Then
                colorsIndexNow = 0
            Else
                If colorsIndexNow = rowIndexMax - 1 Then
                    colorsIndexNow = 0
                Else
                    colorsIndexNow += 1
                End If
            End If
            DGTraceability1.Rows(i).DefaultCellStyle.BackColor = colors(colorsIndexNow)
        Next
    End Sub

    Sub loadTraceability2()
        With DGTraceability2
            .DefaultCellStyle.Font = New Font("Tahoma", 14)

            .ColumnCount = 8
            .Columns(0).HeaderText = "Component"
            .Columns(1).HeaderText = "Desc"
            .Columns(2).HeaderText = "Inv."
            .Columns(3).HeaderText = "Batch No"
            .Columns(4).HeaderText = "Lot Component"
            .Columns(5).HeaderText = "Lot FG"
            .Columns(6).HeaderText = "Qty"
            .Columns(7).HeaderText = "Remark"

            For i As Integer = 0 To .ColumnCount - 1
                .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

            Dim sqlStr As String = "select sc.material,mm.name,sc.inv_ctrl_date,sc.batch_no,sc.lot_no,sc.flow_ticket,qty,id_level from stock_card sc, master_material mm where sc.status='Production Result' and sc.finish_goods_pn='" & txtTraceability.Text & "' and sc.material=mm.part_number order by sc.flow_ticket,sc.material"

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
                    .Rows.Add(1)
                    .Item(0, i).Value = dttable.Rows(i).Item("material")
                    .Item(1, i).Value = dttable.Rows(i).Item("name")
                    .Item(2, i).Value = dttable.Rows(i).Item("inv_ctrl_date")
                    .Item(3, i).Value = dttable.Rows(i).Item("batch_no")
                    .Item(4, i).Value = dttable.Rows(i).Item("lot_no")
                    .Item(5, i).Value = dttable.Rows(i).Item("flow_ticket")
                    .Item(6, i).Value = dttable.Rows(i).Item("qty")
                    If InStr(dttable.Rows(i).Item("id_level").ToString, "SA") > 0 Or InStr(dttable.Rows(i).Item("id_level").ToString, "WIP") > 0 Or InStr(dttable.Rows(i).Item("id_level").ToString, "OT") > 0 Then
                        .Item(7, i).Value = dttable.Rows(i).Item("id_level")
                    Else
                        .Item(7, i).Value = "Fresh"
                    End If
                Next
            Else
                .Rows.Clear()
            End If

            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        Dim rowIndexMax As Integer = 5
        Dim colors As New List(Of Color)({Color.LightBlue, Color.LightGreen, Color.LightGray, Color.LightPink, Color.LightYellow})
        Dim colorsIndexFirst = 0
        Dim DataIndexNow = ""
        Dim colorsIndexNow = 0
        Dim colorsIndexSet = 0

        For i As Integer = 0 To DGTraceability2.RowCount - 1
            If i = 0 Then
                DataIndexNow = DGTraceability2.Rows(i).Cells(5).Value
                colorsIndexNow = colorsIndexFirst
            End If

            If DGTraceability2.Rows(i).Cells(5).Value = DataIndexNow Then
                colorsIndexSet = colorsIndexNow
            Else
                If colorsIndexNow = rowIndexMax - 1 Then
                    colorsIndexNow = 0
                Else
                    colorsIndexNow += 1
                End If
            End If

            DataIndexNow = DGTraceability2.Rows(i).Cells(5).Value
            DGTraceability2.Rows(i).DefaultCellStyle.BackColor = colors(colorsIndexNow)
        Next
    End Sub

    Private Sub txtSummarySubSubPO_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtSummarySubSubPO.PreviewKeyDown
        If (e.KeyData = Keys.Enter) Then
            If txtSummarySubSubPO.Text <> "" Then
                loadDGV()
            Else
                MsgBox("Sorry please fill the sub sub po")
            End If
        End If
    End Sub

    Private Sub txtTraceability_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtTraceability.PreviewKeyDown
        If (e.KeyData = Keys.Enter) Then
            If txtTraceability.Text <> "" Then
                loadTraceability1()
            Else
                MsgBox("Sorry please fill the sub sub po")
            End If
        End If
    End Sub
End Class