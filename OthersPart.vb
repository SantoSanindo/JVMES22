Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class OthersPart

    Public Shared menu As String = "Others Part"
    Private Sub DataGridView2_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView2.DataBindingComplete
        For i As Integer = 0 To DataGridView2.RowCount - 1
            If DataGridView2.Rows(i).Index Mod 2 = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub txtLabelOtherPart_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtLabelOtherPart.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
            If globVar.view > 0 Then
                loadDGVOthers()
            Else
                RJMessageBox.Show("Cannot access this menu.")
            End If
        End If
    End Sub

    Private Sub DataGridView4_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView4.DataBindingComplete
        For i As Integer = 0 To DataGridView4.RowCount - 1
            If DataGridView4.Rows(i).Index Mod 2 = 0 Then
                DataGridView4.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView4.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub btnOtherSave_Click(sender As Object, e As EventArgs) Handles btnOtherSave.Click
        If globVar.add > 0 Then
            If DataGridView2.Rows.Count > 0 Then
                Try
                    Dim over As Boolean = False
                    Dim statusSimpan As Integer = 1
                    Dim qtyReject As Double
                    Dim qtyDefect As Double
                    Dim codeOther As String = ""

                    For i = 0 To DataGridView2.Rows.Count - 1
                        If DataGridView2.Rows(i).Cells(10).Value IsNot "" And DataGridView2.Rows(i).Cells(10).Value IsNot Nothing And DataGridView2.Rows(i).Cells(10).Value IsNot DBNull.Value Then
                            If IsNumeric(DataGridView2.Rows(i).Cells(10).Value) Then
                                If DataGridView2.Rows(i).Cells(10).Value > 0 Then
                                    If DataGridView2.Rows(i).Cells(10).Value > DataGridView2.Rows(i).Cells(9).Value Then
                                        over = True
                                        RJMessageBox.Show("The quantity cannot exceed the maximum quantity -> " & DataGridView2.Rows(i).Cells(10).Value)
                                    End If
                                End If
                            Else
                                RJMessageBox.Show("this is not number -> " & DataGridView2.Rows(i).Cells(10).Value & ". Please change with number.")
                            End If
                        End If
                    Next

                    If over Then
                        Exit Sub
                    End If

                    For i = 0 To DataGridView2.Rows.Count - 1
                        Dim queryCode As String = "select top 1 CODE_STOCK_PROD_OTHERS from stock_prod_others ORDER BY cast(replace(CODE_STOCK_PROD_OTHERS,'OT','') as int) desc"
                        Dim dtCode As DataTable = Database.GetData(queryCode)
                        If dtCode.Rows.Count > 0 Then
                            Dim match As Match = Regex.Match(dtCode.Rows(0).Item("CODE_STOCK_PROD_OTHERS").ToString(), "^([A-Z]+)(\d+)$")
                            If match.Success Then
                                Dim prefix As String = match.Groups(1).Value
                                Dim number As Integer = Integer.Parse(match.Groups(2).Value)
                                Dim nextNumber As Integer = number + 1
                                codeOther = prefix & nextNumber.ToString()
                            End If
                        Else
                            codeOther = "OT" & dtCode.Rows.Count + 1
                        End If

                        qtyReject = 0

                        If DataGridView2.Rows(i).Cells(10).Value IsNot "" And DataGridView2.Rows(i).Cells(10).Value IsNot Nothing And DataGridView2.Rows(i).Cells(10).Value IsNot DBNull.Value Then
                            If IsNumeric(DataGridView2.Rows(i).Cells(10).Value) Then
                                If DataGridView2.Rows(i).Cells(10).Value > 0 Then
                                    If DataGridView2.Rows(i).Cells(10).Value <= DataGridView2.Rows(i).Cells(9).Value Then
                                        qtyReject = DataGridView2.Rows(i).Cells(9).Value - DataGridView2.Rows(i).Cells(10).Value

                                        Dim query As String = "select * from stock_prod_others where CODE_OUT_PROD_DEFECT='" & txtLabelOtherPart.Text & "' and part_number='" & DataGridView2.Rows(i).Cells(4).Value & "' and department='" & globVar.department & "'"
                                        Dim dtCheckStockOthers As DataTable = Database.GetData(query)
                                        If dtCheckStockOthers.Rows.Count > 0 Then
                                            Dim sqlUpdateProcessProd As String = "update stock_prod_others set qty=" & DataGridView2.Rows(i).Cells(10).Value.ToString().Replace(",", ".") & " where CODE_OUT_PROD_DEFECT='" & txtLabelOtherPart.Text & "' and part_number='" & DataGridView2.Rows(i).Cells(4).Value & "' and department='" & globVar.department & "'"
                                            Dim cmdUpdateProcessProd = New SqlCommand(sqlUpdateProcessProd, Database.koneksi)
                                            If cmdUpdateProcessProd.ExecuteNonQuery() Then
                                                statusSimpan *= 1
                                            Else
                                                statusSimpan *= 0
                                            End If
                                        Else
                                            Dim sqlInsertOther As String = "INSERT INTO stock_prod_others (CODE_STOCK_PROD_OTHERS, PART_NUMBER, QTY,CODE_OUT_PROD_DEFECT,DEPARTMENT,LOT_NO,TRACEABILITY,INV_CTRL_DATE,BATCH_NO)
                                                values ('" & codeOther & "','" & DataGridView2.Rows(i).Cells(4).Value & "','" & DataGridView2.Rows(i).Cells(10).Value.ToString().Replace(",", ".") & "','" & txtLabelOtherPart.Text & "','" & globVar.department & "','" & DataGridView2.Rows(i).Cells(5).Value & "','" & DataGridView2.Rows(i).Cells(6).Value & "','" & DataGridView2.Rows(i).Cells(8).Value & "','" & DataGridView2.Rows(i).Cells(7).Value & "')"
                                            Dim cmdInsertOther = New SqlCommand(sqlInsertOther, Database.koneksi)
                                            If cmdInsertOther.ExecuteNonQuery() Then
                                                statusSimpan *= 1
                                            Else
                                                statusSimpan *= 0
                                            End If
                                        End If

                                        Dim queryGetReject As String = "select * from out_prod_reject where code_out_prod_defect='" & txtLabelOtherPart.Text & "' and part_number='" & DataGridView2.Rows(i).Cells(4).Value & "' and department='" & globVar.department & "'"
                                        Dim dtcheckGetReject As DataTable = Database.GetData(queryGetReject)

                                        Dim queryGetDefect As String = "select * from out_prod_defect where code_out_prod_defect='" & txtLabelOtherPart.Text & "' and part_number='" & DataGridView2.Rows(i).Cells(4).Value & "' and department='" & globVar.department & "'"
                                        Dim dtcheckGetDefect As DataTable = Database.GetData(queryGetDefect)

                                        If qtyReject = 0 Then
                                            If dtcheckGetReject.Rows.Count > 0 Then
                                                Dim queryDelete As String = "delete from out_prod_reject where id=" & dtcheckGetReject.Rows(0).Item("id")
                                                Dim dtDelete = New SqlCommand(queryDelete, Database.koneksi)
                                                dtDelete.ExecuteNonQuery()
                                            End If

                                            If dtcheckGetDefect.Rows.Count > 0 Then
                                                For iDefect = 0 To dtcheckGetDefect.Rows.Count - 1
                                                    Dim sqlupdateDefect As String = "update out_prod_defect set qty=0,actual_qty=0 where id=" & dtcheckGetDefect.Rows(iDefect).Item("id")
                                                    Dim cmdupdateDefect = New SqlCommand(sqlupdateDefect, Database.koneksi)
                                                    cmdupdateDefect.ExecuteNonQuery()
                                                Next
                                            End If
                                        Else
                                            If dtcheckGetReject.Rows.Count > 0 Then
                                                Dim sqlupdateprocessprod As String = "update out_prod_reject set qty=" & qtyReject.ToString().Replace(",", ".") & " where id=" & dtcheckGetReject.Rows(0).Item("id")
                                                Dim cmdupdateprocessprod = New SqlCommand(sqlupdateprocessprod, Database.koneksi)
                                                If cmdupdateprocessprod.ExecuteNonQuery() Then
                                                    statusSimpan *= 1
                                                Else
                                                    statusSimpan *= 0
                                                End If
                                            Else
                                                Dim sqlinsertother As String = "insert into out_prod_reject (code_out_prod_reject, sub_sub_po, fg_pn,part_number,lot_no,traceability,inv_ctrl_date,batch_no,qty,po,line,department,code_out_prod_defect)
                                                    values('" & codeOther & "','" & DataGridView2.Rows(i).Cells(1).Value & "','" & DataGridView2.Rows(i).Cells(2).Value & "','" & DataGridView2.Rows(i).Cells(4).Value & "','" & DataGridView2.Rows(i).Cells(5).Value & "','" & DataGridView2.Rows(i).Cells(6).Value & "','" & DataGridView2.Rows(i).Cells(8).Value & "','" & DataGridView2.Rows(i).Cells(7).Value & "'," & qtyReject.ToString().Replace(",", ".") & ",'" & DataGridView2.Rows(i).Cells(1).Value.ToString().Split("-")(0) & "','" & DataGridView2.Rows(i).Cells(3).Value & "','" & globVar.department & "','" & txtLabelOtherPart.Text & "')"
                                                Dim cmdinsertother = New SqlCommand(sqlinsertother, Database.koneksi)
                                                If cmdinsertother.ExecuteNonQuery() Then
                                                    statusSimpan *= 1
                                                Else
                                                    statusSimpan *= 0
                                                End If
                                            End If

                                            qtyDefect = DataGridView2.Rows(i).Cells(10).Value

                                            For iDefect = 0 To dtcheckGetDefect.Rows.Count - 1
                                                Dim sqlupdateDefect As String = "update out_prod_defect set qty=0,actual_qty=0 where id=" & dtcheckGetDefect.Rows(iDefect).Item("id")
                                                Dim cmdupdateDefect = New SqlCommand(sqlupdateDefect, Database.koneksi)
                                                cmdupdateDefect.ExecuteNonQuery()
                                            Next
                                        End If
                                    Else
                                        RJMessageBox.Show("The quantity cannot exceed the maximum quantity")
                                    End If
                                Else
                                    Dim queryGetOthers As String = "select * from stock_prod_others where CODE_OUT_PROD_DEFECT='" & txtLabelOtherPart.Text & "' and part_number='" & DataGridView2.Rows(i).Cells(4).Value & "' and department='" & globVar.department & "'"
                                    Dim dtCheckStockOthers As DataTable = Database.GetData(queryGetOthers)
                                    If dtCheckStockOthers.Rows.Count > 0 Then
                                        Dim queryDelete As String = "delete from stock_prod_others where id=" & dtCheckStockOthers.Rows(0).Item("id")
                                        Dim dtDelete = New SqlCommand(queryDelete, Database.koneksi)
                                        dtDelete.ExecuteNonQuery()
                                    End If

                                    Dim queryGetReject As String = "select * from out_prod_reject where code_out_prod_defect='" & txtLabelOtherPart.Text & "' and part_number='" & DataGridView2.Rows(i).Cells(4).Value & "' and department='" & globVar.department & "'"
                                    Dim dtcheckGetReject As DataTable = Database.GetData(queryGetReject)

                                    Dim queryGetDefect As String = "select * from out_prod_defect where code_out_prod_defect='" & txtLabelOtherPart.Text & "' and part_number='" & DataGridView2.Rows(i).Cells(4).Value & "' and department='" & globVar.department & "'"
                                    Dim dtcheckGetDefect As DataTable = Database.GetData(queryGetDefect)

                                    If dtcheckGetReject.Rows.Count > 0 Then
                                        Dim sqlupdateprocessprod As String = "update out_prod_reject set qty=" & DataGridView2.Rows(i).Cells(9).Value.ToString().Replace(",", ".") & " where id=" & dtcheckGetReject.Rows(0).Item("id")
                                        Dim cmdupdateprocessprod = New SqlCommand(sqlupdateprocessprod, Database.koneksi)
                                        If cmdupdateprocessprod.ExecuteNonQuery() Then
                                            statusSimpan *= 1
                                        Else
                                            statusSimpan *= 0
                                        End If
                                    Else
                                        Dim sqlinsertother As String = "insert into out_prod_reject (code_out_prod_reject, sub_sub_po, fg_pn,part_number,lot_no,traceability,inv_ctrl_date,batch_no,qty,po,line,department,code_out_prod_defect)
                                        values('" & codeOther & "','" & DataGridView2.Rows(i).Cells(1).Value & "','" & DataGridView2.Rows(i).Cells(2).Value & "','" & DataGridView2.Rows(i).Cells(4).Value & "','" & DataGridView2.Rows(i).Cells(5).Value & "','" & DataGridView2.Rows(i).Cells(6).Value & "','" & DataGridView2.Rows(i).Cells(8).Value & "','" & DataGridView2.Rows(i).Cells(7).Value & "'," & DataGridView2.Rows(i).Cells(9).Value.ToString().Replace(",", ".") & ",'" & DataGridView2.Rows(i).Cells(1).Value.ToString().Split("-")(0) & "','" & DataGridView2.Rows(i).Cells(3).Value & "','" & globVar.department & "','" & txtLabelOtherPart.Text & "')"
                                        Dim cmdinsertother = New SqlCommand(sqlinsertother, Database.koneksi)
                                        If cmdinsertother.ExecuteNonQuery() Then
                                            statusSimpan *= 1
                                        Else
                                            statusSimpan *= 0
                                        End If
                                    End If

                                    For iDefect = 0 To dtcheckGetDefect.Rows.Count - 1
                                        Dim sqlupdateDefect As String = "update out_prod_defect set qty=0,actual_qty=0 where id=" & dtcheckGetDefect.Rows(iDefect).Item("id")
                                        Dim cmdupdateDefect = New SqlCommand(sqlupdateDefect, Database.koneksi)
                                        cmdupdateDefect.ExecuteNonQuery()
                                    Next
                                End If

                            Else
                                RJMessageBox.Show("this is not number -> " & DataGridView2.Rows(i).Cells(10).Value & ". Please change with number.")
                            End If
                        End If
                    Next

                    If statusSimpan > 0 Then
                        RJMessageBox.Show("Success Save data!!!")
                        loadDGVOthers()
                    Else
                        RJMessageBox.Show("Fail Save data!!!")
                        loadDGVOthers()
                    End If

                Catch ex As Exception
                    RJMessageBox.Show(ex.Message)
                End Try
            End If
        Else
            RJMessageBox.Show("Cannot access this menu.")
        End If
    End Sub

    Private Sub btnPrintOthersPart_Click(sender As Object, e As EventArgs) Handles btnPrintOthersPart.Click
        If globVar.view > 0 Then
            If DataGridView4.Rows.Count > 0 Then
                Dim countPrint = 0
                Try
                    Dim query As String = "select stock_prod_others.*,master_material.name from stock_prod_others, master_material where CODE_OUT_PROD_DEFECT='" & txtLabelOtherPart.Text & "' and department='" & globVar.department & "' and stock_prod_others.part_number=master_material.part_number"
                    Dim dtCheckOthersMaterialBalance As DataTable = Database.GetData(query)
                    If dtCheckOthersMaterialBalance.Rows.Count > 0 Then
                        For i = 0 To dtCheckOthersMaterialBalance.Rows.Count - 1
                            If dtCheckOthersMaterialBalance.Rows(i).Item("print") = 0 Then
                                globVar.failPrint = ""
                                _PrintingSubAssyRawMaterial.txt_Unique_id.Text = dtCheckOthersMaterialBalance.Rows(i).Item("code_stock_prod_others")
                                _PrintingSubAssyRawMaterial.txt_part_number.Text = dtCheckOthersMaterialBalance.Rows(i).Item("part_number")
                                _PrintingSubAssyRawMaterial.txt_Part_Description.Text = dtCheckOthersMaterialBalance.Rows(i).Item("name")
                                _PrintingSubAssyRawMaterial.txt_Qty.Text = dtCheckOthersMaterialBalance.Rows(i).Item("qty")
                                _PrintingSubAssyRawMaterial.txt_jenis_ticket.Text = "Others Material"
                                _PrintingSubAssyRawMaterial.txt_QR_Code.Text = dtCheckOthersMaterialBalance.Rows(i).Item("code_stock_prod_others") & Environment.NewLine
                                _PrintingSubAssyRawMaterial.btn_Print_Click(sender, e)

                                If globVar.failPrint = "No" Then
                                    Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (fg, line, remark,sub_sub_po,department,material,code_print)
                                        VALUES ('" & DataGridView2.Rows(i).Cells("FG PN").Value & "','" & DataGridView2.Rows(i).Cells("Line").Value & "','Others Material','" & DataGridView2.Rows(i).Cells("Sub Sub PO").Value & "','" & globVar.department & "','" & dtCheckOthersMaterialBalance.Rows(i).Item("part_number") & "','" & dtCheckOthersMaterialBalance.Rows(i).Item("code_stock_prod_others") & "')"
                                    Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                                    cmdInsertPrintingRecord.ExecuteNonQuery()
                                End If
                            Else
                                countPrint += 1
                            End If
                        Next
                    Else
                        RJMessageBox.Show("Sorry dont have any others part.")
                    End If
                Catch ex As Exception
                    RJMessageBox.Show(ex.Message)
                End Try
            Else
                RJMessageBox.Show("No data for print.")
            End If
        End If
    End Sub

    Sub loadDGVOthers()
        Try
            Dim query As String = "Select sub_sub_po,fg_pn,line,part_number,sum(qty) qty,lot_no,traceability,batch_no,inv_ctrl_date from OUT_PROD_DEFECT where CODE_OUT_PROD_DEFECT='" & txtLabelOtherPart.Text & "' and department='" & globVar.department & "' GROUP BY sub_sub_po,fg_pn,line,part_number,lot_no,traceability,batch_no,inv_ctrl_date"

            Dim dtOutProd As DataTable = Database.GetData(query)

            With DataGridView2
                .Rows.Clear()

                .DefaultCellStyle.Font = New Font("Tahoma", 14)

                .ColumnCount = 11
                .Columns(0).Name = "No"
                .Columns(1).Name = "Sub Sub PO"
                .Columns(2).Name = "Finish Goods"
                .Columns(3).Name = "Line"
                .Columns(4).Name = "Part Number"
                .Columns(5).Name = "Lot No"
                .Columns(6).Name = "Traceability"
                .Columns(7).Name = "Batch No"
                .Columns(8).Name = "Inv Ctrl Date"
                .Columns(9).Name = "Qty Max"
                .Columns(10).Name = "Qty"

                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .EnableHeadersVisualStyles = False
                With .ColumnHeadersDefaultCellStyle
                    .BackColor = Color.Navy
                    .ForeColor = Color.White
                    .Font = New Font("Tahoma", 13, FontStyle.Bold)
                    .Alignment = HorizontalAlignment.Center
                    .Alignment = ContentAlignment.MiddleCenter
                End With


                If dtOutProd.Rows.Count > 0 Then
                    For i = 0 To dtOutProd.Rows.Count - 1
                        Dim queryOthersStock As String = "select qty from stock_prod_others where CODE_OUT_PROD_DEFECT='" & txtLabelOtherPart.Text & "' and department='" & globVar.department & "' and part_number='" & dtOutProd.Rows(i)("part_number") & "'"
                        Dim dtOthersStock As DataTable = Database.GetData(queryOthersStock)
                        Dim qtyOthers As Integer = 0
                        If dtOthersStock.Rows.Count > 0 Then
                            qtyOthers = dtOthersStock.Rows(0).Item("qty")
                        End If

                        .Rows.Add(1)
                        .Item(0, i).Value = (i + 1).ToString()
                        .Item(1, i).Value = dtOutProd.Rows(i)("sub_sub_po")
                        .Item(2, i).Value = dtOutProd.Rows(i)("fg_pn")
                        .Item(3, i).Value = dtOutProd.Rows(i)("line")
                        .Item(4, i).Value = dtOutProd.Rows(i)("part_number")
                        .Item(5, i).Value = dtOutProd.Rows(i)("lot_no")
                        .Item(6, i).Value = dtOutProd.Rows(i)("traceability")
                        .Item(7, i).Value = dtOutProd.Rows(i)("batch_no")
                        .Item(8, i).Value = dtOutProd.Rows(i)("inv_ctrl_date")
                        .Item(9, i).Value = dtOutProd.Rows(i)("qty")
                        .Item(10, i).Value = qtyOthers
                    Next
                End If

                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            For i As Integer = 0 To DataGridView2.RowCount - 1
                If DataGridView2.Rows(i).Index Mod 2 = 0 Then
                    DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next i

            Dim queryOthers As String = "select * from stock_prod_others where CODE_OUT_PROD_DEFECT='" & txtLabelOtherPart.Text & "' and department='" & globVar.department & "'"
            Dim dtOthers As DataTable = Database.GetData(queryOthers)

            With DataGridView4
                .Rows.Clear()

                .DefaultCellStyle.Font = New Font("Tahoma", 14)

                .ColumnCount = 6
                .Columns(0).Name = "No"
                .Columns(1).Name = "Code Defect"
                .Columns(2).Name = "Code Others"
                .Columns(3).Name = "Part Number"
                .Columns(4).Name = "Lot No"
                .Columns(5).Name = "Qty"

                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .EnableHeadersVisualStyles = False
                With .ColumnHeadersDefaultCellStyle
                    .BackColor = Color.Navy
                    .ForeColor = Color.White
                    .Font = New Font("Tahoma", 13, FontStyle.Bold)
                    .Alignment = HorizontalAlignment.Center
                    .Alignment = ContentAlignment.MiddleCenter
                End With


                If dtOthers.Rows.Count > 0 Then
                    For i = 0 To dtOthers.Rows.Count - 1
                        .Rows.Add(1)
                        .Item(0, i).Value = (i + 1).ToString()
                        .Item(1, i).Value = dtOthers.Rows(i)("CODE_OUT_PROD_DEFECT")
                        .Item(2, i).Value = dtOthers.Rows(i)("code_stock_prod_others")
                        .Item(3, i).Value = dtOthers.Rows(i)("part_number")
                        .Item(4, i).Value = dtOthers.Rows(i)("lot_no")
                        .Item(5, i).Value = dtOthers.Rows(i)("qty")
                    Next
                End If

                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            For i As Integer = 0 To DataGridView4.RowCount - 1
                If DataGridView4.Rows(i).Index Mod 2 = 0 Then
                    DataGridView4.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    DataGridView4.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next i
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

End Class