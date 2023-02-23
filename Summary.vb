Imports System.Data.SqlClient
Imports System.Runtime.Remoting
Imports Microsoft.Office.Interop

Public Class Summary


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
        With DGTraceability1
            .DefaultCellStyle.Font = New Font("Tahoma", 14)

            .ColumnCount = 43
            .Columns(0).HeaderText = "Date"
            .Columns(1).HeaderText = "Sub Sub PO"
            .Columns(2).HeaderText = "Line"
            .Columns(3).HeaderText = "FG"
            .Columns(4).HeaderText = "Laser Code"
            .Columns(5).HeaderText = "Inv."
            .Columns(6).HeaderText = "Batch No"
            .Columns(7).HeaderText = "Lot No"
            .Columns(8).HeaderText = "Inspector"
            .Columns(9).HeaderText = "Packer 1"
            .Columns(10).HeaderText = "Packer 2"
            .Columns(11).HeaderText = "Packer 3"
            .Columns(12).HeaderText = "Packer 4"
            .Columns(13).HeaderText = "Process 1"
            .Columns(14).HeaderText = "Process 2"
            .Columns(15).HeaderText = "Process 3"
            .Columns(16).HeaderText = "Process 4"
            .Columns(17).HeaderText = "Process 5"
            .Columns(18).HeaderText = "Process 6"
            .Columns(19).HeaderText = "Process 7"
            .Columns(20).HeaderText = "Process 8"
            .Columns(21).HeaderText = "Process 9"
            .Columns(22).HeaderText = "Process 10"
            .Columns(23).HeaderText = "Process 11"
            .Columns(24).HeaderText = "Process 12"
            .Columns(25).HeaderText = "Process 13"
            .Columns(26).HeaderText = "Process 14"
            .Columns(27).HeaderText = "Process 15"
            .Columns(28).HeaderText = "Process 16"
            .Columns(29).HeaderText = "Process 17"
            .Columns(30).HeaderText = "Process 18"
            .Columns(31).HeaderText = "Process 19"
            .Columns(32).HeaderText = "Process 20"
            .Columns(33).HeaderText = "Process 21"
            .Columns(34).HeaderText = "Process 22"
            .Columns(35).HeaderText = "Process 23"
            .Columns(36).HeaderText = "Process 24"
            .Columns(37).HeaderText = "Process 25"
            .Columns(38).HeaderText = "Process 26"
            .Columns(39).HeaderText = "Process 27"
            .Columns(40).HeaderText = "Process 28"
            .Columns(41).HeaderText = "Process 29"
            .Columns(42).HeaderText = "Process 30"


            For i As Integer = 0 To .ColumnCount - 1
                .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

            Dim sqlStr As String = "SELECT d.DATETIME_INSERT,d.sub_sub_po,d.line,d.fg,d.laser_code,d.INV_CTRL_DATE,d.BATCH_NO,d.LOT_NO,f.inspector,f.packer1,f.packer2,f.packer3,f.packer4 FROM done_fg d left join fga f on d.sub_sub_po=f.sub_sub_po and d.flow_ticket=f.no_flowticket WHERE d.fg= '" & txtTraceability.Text & "'"

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
                    .Item(0, i).Value = dttable.Rows(i).Item("DATETIME_INSERT")
                    .Item(1, i).Value = dttable.Rows(i).Item("sub_sub_po")
                    .Item(2, i).Value = dttable.Rows(i).Item("line")
                    .Item(3, i).Value = dttable.Rows(i).Item("fg")
                    .Item(4, i).Value = dttable.Rows(i).Item("laser_code")
                    .Item(5, i).Value = dttable.Rows(i).Item("INV_CTRL_DATE")
                    .Item(6, i).Value = dttable.Rows(i).Item("BATCH_NO")
                    .Item(7, i).Value = dttable.Rows(i).Item("LOT_NO")
                    .Item(8, i).Value = dttable.Rows(i).Item("inspector")
                    .Item(9, i).Value = dttable.Rows(i).Item("packer1")
                    .Item(10, i).Value = dttable.Rows(i).Item("packer2")
                    .Item(11, i).Value = dttable.Rows(i).Item("packer3")
                    .Item(12, i).Value = dttable.Rows(i).Item("packer4")
                Next
            Else
                .Rows.Clear()
            End If

            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        End With

        For Each col As DataGridViewColumn In DGTraceability1.Columns
            col.MinimumWidth = 300
        Next
        loadTraceability2()

        loadOperator()

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

    Sub loadTraceability11()
        Dim varProcess As String = ""

        Dim sqlStr As String = "SELECT d.DATETIME_INSERT [Date],d.sub_sub_po [Sub Sub PO],d.line [Line],d.fg [FG],d.laser_code [Laser Code],d.INV_CTRL_DATE [Inv.],d.BATCH_NO [Batch No],d.LOT_NO [Lot No],f.inspector [Outgoing Inspector],f.packer1 [Packer 1],f.packer2 [Packer 2],f.packer3 [Packer 3],f.packer4 [Packer 4] FROM done_fg d left join fga f on d.sub_sub_po=f.sub_sub_po and d.flow_ticket=f.no_flowticket WHERE d.fg= '" & txtTraceability.Text & "'"

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

        For k As Integer = 1 To 30
            DGTraceability1.Columns.Add("Process_" & k, "Process " & k)
        Next

        Threading.Thread.Sleep(2000)
        loadOperator()
    End Sub

    Sub loadOperator()
        If DGTraceability1.Rows.Count > 0 Then

            For r As Integer = 0 To DGTraceability1.Rows.Count - 1
                For c As Integer = 0 To DGTraceability1.Columns.Count - 1
                    Dim sqlCheckOperatorDetails As String = "SELECT * FROM prod_dop_details WHERE sub_sub_po= '" & DGTraceability1.Rows(r).Cells(1).Value & "'"
                    Dim dtOperatorDetails As DataTable = Database.GetData(sqlCheckOperatorDetails)
                    If dtOperatorDetails.Rows.Count > 0 Then
                        For l As Integer = 0 To dtOperatorDetails.Rows.Count - 1
                            If DGTraceability1.Columns(c).Index > 12 Then
                                If DGTraceability1.Columns(c).HeaderText = "Process " & dtOperatorDetails.Rows(l).Item("process_number") Then
                                    If r + 1 = dtOperatorDetails.Rows(l).Item("lot_flow_ticket") Then
                                        DGTraceability1(c, r).Value = dtOperatorDetails.Rows(l).Item("process") & "(" & dtOperatorDetails.Rows(l).Item("operator") & ")"
                                    End If
                                End If
                            End If
                        Next
                    Else
                        Dim sqlOperator As String = "SELECT * FROM prod_dop WHERE fg_pn= '" & txtTraceability.Text & "' and sub_sub_po='" & DGTraceability1.Rows(r).Cells(1).Value & "'"
                        Dim dtOperator As DataTable = Database.GetData(sqlOperator)
                        If DGTraceability1.Rows.Count > 0 Then
                            For l As Integer = 0 To dtOperator.Rows.Count - 1
                                If DGTraceability1.Columns(c).Index > 12 Then
                                    If DGTraceability1.Columns(c).HeaderText = "Process " & dtOperator.Rows(l).Item("process_number") Then
                                        DGTraceability1(c, r).Value = dtOperator.Rows(l).Item("process") & "(" & dtOperator.Rows(l).Item("operator_id") & ")"
                                    End If
                                End If
                            Next
                        End If
                    End If
                Next
            Next
        End If
    End Sub

    Sub loadTraceability2()
        With DGTraceability2
            .DefaultCellStyle.Font = New Font("Tahoma", 14)

            .ColumnCount = 9
            .Columns(0).HeaderText = "Line"
            .Columns(1).HeaderText = "Component"
            .Columns(2).HeaderText = "Desc"
            .Columns(3).HeaderText = "Inv."
            .Columns(4).HeaderText = "Batch No"
            .Columns(5).HeaderText = "Lot Component"
            .Columns(6).HeaderText = "Lot FG"
            .Columns(7).HeaderText = "Qty"
            .Columns(8).HeaderText = "Remark"

            For i As Integer = 0 To .ColumnCount - 1
                .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

            Dim sqlStr As String = "select sc.line,sc.material,mm.name,sc.inv_ctrl_date,sc.batch_no,sc.lot_no,sc.flow_ticket,qty,id_level from stock_card sc, master_material mm where sc.status='Production Result' and sc.finish_goods_pn='" & txtTraceability.Text & "' and sc.material=mm.part_number order by sc.line,sc.flow_ticket,sc.material"

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
                    .Item(0, i).Value = dttable.Rows(i).Item("line")
                    .Item(1, i).Value = dttable.Rows(i).Item("material")
                    .Item(2, i).Value = dttable.Rows(i).Item("name")
                    .Item(3, i).Value = dttable.Rows(i).Item("inv_ctrl_date")
                    .Item(4, i).Value = dttable.Rows(i).Item("batch_no")
                    .Item(5, i).Value = dttable.Rows(i).Item("lot_no")
                    .Item(6, i).Value = dttable.Rows(i).Item("flow_ticket")
                    .Item(7, i).Value = dttable.Rows(i).Item("qty")
                    If InStr(dttable.Rows(i).Item("id_level").ToString, "SA") > 0 Or InStr(dttable.Rows(i).Item("id_level").ToString, "WIP") > 0 Or InStr(dttable.Rows(i).Item("id_level").ToString, "OT") > 0 Then
                        .Item(8, i).Value = dttable.Rows(i).Item("id_level")
                    Else
                        .Item(8, i).Value = "Fresh"
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
                DataIndexNow = DGTraceability2.Rows(i).Cells(6).Value
                colorsIndexNow = colorsIndexFirst
            End If

            If DGTraceability2.Rows(i).Cells(6).Value = DataIndexNow Then
                colorsIndexSet = colorsIndexNow
            Else
                If colorsIndexNow = rowIndexMax - 1 Then
                    colorsIndexNow = 0
                Else
                    colorsIndexNow += 1
                End If
            End If

            DataIndexNow = DGTraceability2.Rows(i).Cells(6).Value
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
        exportToExcel(DGSummary)
    End Sub

    Private Sub btn_ExportTrace1_Click(sender As Object, e As EventArgs) Handles btn_ExportTrace1.Click
        exportToExcel(DGTraceability1)
    End Sub

    Private Sub btn_ExportTrace2_Click(sender As Object, e As EventArgs) Handles btn_ExportTrace2.Click
        exportToExcel(DGTraceability2)
    End Sub
End Class