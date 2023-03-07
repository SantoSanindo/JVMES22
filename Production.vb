﻿Imports System.Data.SqlClient
Imports System.Net.Mime.MediaTypeNames
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MdiTabControl

Public Class Production
    Sub tampilDataComboBoxLine()
        Call Database.koneksi_database()
        Dim dtMasterLine As DataTable = Database.GetData("select NAME from MASTER_LINE where DEPARTMENT='" & globVar.department & "'")

        ComboBox1.DataSource = dtMasterLine
        ComboBox1.DisplayMember = "NAME"
        ComboBox1.ValueMember = "NAME"
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
            If Len(Me.TextBox1.Text) >= 64 Then
                Try
                    If DataGridView2.Rows.Count = 0 Then
                        MessageBox.Show("Cannot process When Detail Of Process blank. Please set Operator First.")
                        TextBox1.Clear()
                        Exit Sub
                    End If

                    Dim ds As New DataSet
                    Dim yieldlose As Integer = 0
                    Dim usage As Integer = 0
                    Dim targetQty As Integer = 0

                    QRCode.Baca(TextBox1.Text)

                    Dim sqlCheckSummaryFG As String = "select * from summary_fg where material = '" & globVar.QRCode_PN & "' and sub_sub_po='" & TextBox11.Text & "' and fg='" & TextBox2.Text & "'"
                    Dim dtCheckSummaryFG As DataTable = Database.GetData(sqlCheckSummaryFG)

                    Dim dtInFresh As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & TextBox11.Text & "' and material='" & globVar.QRCode_PN & "' and status='Production Request' and [level]='Fresh'")

                    If dtCheckSummaryFG.Rows.Count > 0 Then
                        Dim queryUpdateStockCardProdReq As String = "update summary_fg set fresh_in=" & dtInFresh.Rows(0)(0).ToString.Replace(",", ".") & ",total_in=(SELECT isnull(SUM(FRESH_IN+BALANCE_IN+WIP_IN+ONHOLD_IN+OTHERS_IN+SA_IN),0) FROM SUMMARY_FG WHERE id = " & dtCheckSummaryFG.Rows(0).Item("id") & ") where id=" & dtCheckSummaryFG.Rows(0).Item("id")
                        Dim dtUpdateStockCardProdReq = New SqlCommand(queryUpdateStockCardProdReq, Database.koneksi)
                        dtUpdateStockCardProdReq.ExecuteNonQuery()
                    Else
                        Dim sqlInsertSummaryFG As String = "INSERT INTO summary_fg (sub_sub_po, FG ,material,fresh_in,total_in) VALUES ('" & TextBox11.Text & "','" & TextBox2.Text & "','" & globVar.QRCode_PN & "'," & dtInFresh.Rows(0)(0).ToString.Replace(",", ".") & "," & dtInFresh.Rows(0)(0).ToString.Replace(",", ".") & ")"
                        Dim cmdInsertSummaryFG = New SqlCommand(sqlInsertSummaryFG, Database.koneksi)
                        cmdInsertSummaryFG.ExecuteNonQuery()
                    End If

                    Dim sqlCheckInStock As String = "select in_material.* from sub_sub_po sp, stock_card in_material where in_material.SUB_SUB_PO = sp.sub_sub_po and sp.status='Open' and in_material.line='" & ComboBox1.Text & "' and in_material.material = '" & globVar.QRCode_PN & "' and in_material.lot_no=" & globVar.QRCode_lot & " and sp.sub_sub_po='" & TextBox11.Text & "' and in_material.status='Production Request' and department='" & globVar.department & "'"
                    Dim dtCheckInStock As DataTable = Database.GetData(sqlCheckInStock)
                    If dtCheckInStock.Rows.Count > 0 Then
                        Dim sqlCheckInStockNewRecord As String = "select * from stock_card where line='" & ComboBox1.Text & "' and material = '" & globVar.QRCode_PN & "' and lot_no=" & globVar.QRCode_lot & " and sub_sub_po='" & TextBox11.Text & "' and status='Production Process' and department='" & globVar.department & "'"
                        Dim dtCheckInStockNewRecord As DataTable = Database.GetData(sqlCheckInStockNewRecord)
                        If dtCheckInStockNewRecord.Rows.Count > 0 Then
                            MessageBox.Show("Double Scan")
                            TextBox1.Text = ""
                            DGV_DOC()
                        Else
                            Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & TextBox11.Text & "', @fg='" & TextBox2.Text & "',@line='" & ComboBox1.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtCheckInStock.Rows(0).Item("actual_qty").ToString.Replace(",", ".") & ",@material='" & globVar.QRCode_PN & "',@lot_material='" & globVar.QRCode_lot & "'"
                            Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)

                            TextBox1.Text = ""
                            DGV_DOC()

                            For i = 0 To DataGridView1.Rows.Count - 1
                                If DataGridView1.Rows(i).Cells(1).Value = globVar.QRCode_PN Then
                                    DataGridView1.Rows(i).Cells(3).Selected = True
                                End If
                            Next
                        End If

                    Else
                        MessageBox.Show("Sorry this material not for this line.")
                        TextBox1.Text = ""
                        TextBox1.Select()
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Else
                If InStr(TextBox1.Text, "WIP") > 0 Then
                    Try
                        Dim CompExist As String = ""

                        Dim sqlCheckStockWIP As String = "select * from STOCK_PROD_WIP where CODE_STOCK_PROD_WIP='" & TextBox1.Text & "' and department='" & globVar.department & "' and qty>0"
                        Dim dtCheckStockWIP As DataTable = Database.GetData(sqlCheckStockWIP)
                        If dtCheckStockWIP.Rows.Count > 0 Then
                            For i = 0 To dtCheckStockWIP.Rows.Count - 1
                                For j = 0 To DataGridView1.Rows.Count - 1
                                    If DataGridView1.Rows(j).Cells(1).Value = dtCheckStockWIP.Rows(i).Item("part_number") Then
                                        Dim sqlCheckSum As String = "select isnull(sum(qty),0) from stock_card where material='" & dtCheckStockWIP.Rows(i).Item("part_number") & "' and department='" & globVar.department & "' and sub_sub_po='" & TextBox11.Text & "' and line='" & ComboBox1.Text & "' and status='Production Process'"
                                        Dim dtCheckSum As DataTable = Database.GetData(sqlCheckSum)
                                        If Convert.ToDouble(DataGridView1.Rows(j).Cells(2).Value) * Convert.ToInt16(TextBox7.Text) <= dtCheckSum.Rows(0).Item(0) Then
                                            CompExist += dtCheckStockWIP.Rows(j).Item("part_number") & ","
                                        End If
                                    End If
                                Next
                            Next

                            If CompExist <> "" Then
                                MessageBox.Show("Cannot add WIP. Because (" & CompExist & ") Material Qty more than Qty Need")
                                TextBox1.Clear()
                                Exit Sub
                            End If

                            For i = 0 To dtCheckStockWIP.Rows.Count - 1
                                Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,SUM_QTY,LEVEL,ID_LEVEL)
                                    VALUES ('" & dtCheckStockWIP.Rows(i).Item("part_number") & "'," & dtCheckStockWIP.Rows(i).Item("qty").ToString.Replace(",", ".") & ",'" & dtCheckStockWIP.Rows(i).Item("INV_CTRL_DATE") & "','" & dtCheckStockWIP.Rows(i).Item("TRACEABILITY") & "'," & dtCheckStockWIP.Rows(i).Item("lot_no") & ",'" & dtCheckStockWIP.Rows(i).Item("batch_no") & "','" & TextBox5.Text & "','" & TextBox11.Text & "','" & TextBox2.Text & "',0,'" & ComboBox1.Text & "','" & TextBox10.Text & "','Production Request','" & globVar.department & "','NO'," & dtCheckStockWIP.Rows(i).Item("qty").ToString.Replace(",", ".") & ",'WIP','" & dtCheckStockWIP.Rows(i).Item("CODE_STOCK_PROD_WIP") & "')"
                                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                cmdInsertInputStockDetail.ExecuteNonQuery()

                                Dim sqlCheckSummaryFG As String = "select * from summary_fg where material = '" & globVar.QRCode_PN & "' and sub_sub_po='" & TextBox11.Text & "' and fg='" & TextBox2.Text & "'"
                                Dim dtCheckSummaryFG As DataTable = Database.GetData(sqlCheckSummaryFG)

                                Dim dtInWIP As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & TextBox11.Text & "' and material='" & dtCheckStockWIP.Rows(i).Item("part_number") & "' and status='Production Request' and [level]='WIP'")

                                If dtCheckSummaryFG.Rows.Count > 0 Then
                                    Dim queryUpdateStockCardProdReq As String = "update summary_fg set wip_in=" & dtInWIP.Rows(0)(0).ToString.Replace(",", ".") & ",total_in=(SELECT isnull(SUM(FRESH_IN+BALANCE_IN+WIP_IN+ONHOLD_IN+OTHERS_IN+SA_IN),0) FROM SUMMARY_FG WHERE id = " & dtCheckSummaryFG.Rows(0).Item("id") & ") where id=" & dtCheckSummaryFG.Rows(0).Item("id")
                                    Dim dtUpdateStockCardProdReq = New SqlCommand(queryUpdateStockCardProdReq, Database.koneksi)
                                    dtUpdateStockCardProdReq.ExecuteNonQuery()
                                Else
                                    Dim sqlInsertSummaryFG As String = "INSERT INTO summary_fg (sub_sub_po, FG ,material,wip_in,total_in) VALUES ('" & TextBox11.Text & "','" & TextBox2.Text & "','" & globVar.QRCode_PN & "'," & dtInWIP.Rows(0)(0).ToString.Replace(",", ".") & "," & dtInWIP.Rows(0)(0).ToString.Replace(",", ".") & ")"
                                    Dim cmdInsertSummaryFG = New SqlCommand(sqlInsertSummaryFG, Database.koneksi)
                                    cmdInsertSummaryFG.ExecuteNonQuery()
                                End If
                            Next

                            For i = 0 To dtCheckStockWIP.Rows.Count - 1
                                Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcessWIP @sub_sub_po='" & TextBox11.Text & "', @fg='" & TextBox2.Text & "',@line='" & ComboBox1.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtCheckStockWIP.Rows(i).Item("qty").ToString.Replace(",", ".") & ",@material='" & dtCheckStockWIP.Rows(i).Item("part_number") & "',@lot_material='" & dtCheckStockWIP.Rows(i).Item("lot_no") & "',@codeWIP='" & dtCheckStockWIP.Rows(i).Item("code_stock_prod_wip") & "',@po='" & TextBox5.Text & "',@sub_po='" & TextBox10.Text & "'"
                                Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                            Next

                            TextBox1.Text = ""
                            DGV_DOC()
                        Else
                            MsgBox("WIP not Exist in DB")
                            TextBox1.Text = ""
                            DGV_DOC()
                        End If

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                ElseIf InStr(TextBox1.Text, "OH") > 0 Then
                    Try
                        Dim CompExist As String = ""

                        Dim sqlCheckStockONHOLD As String = "select * from STOCK_PROD_ONHOLD where CODE_STOCK_PROD_ONHOLD='" & TextBox1.Text & "' and department='" & globVar.department & "' and qty>0"
                        Dim dtCheckStockONHOLD As DataTable = Database.GetData(sqlCheckStockONHOLD)
                        If dtCheckStockONHOLD.Rows.Count > 0 Then
                            For i = 0 To dtCheckStockONHOLD.Rows.Count - 1
                                For j = 0 To DataGridView1.Rows.Count - 1
                                    If DataGridView1.Rows(j).Cells(1).Value = dtCheckStockONHOLD.Rows(i).Item("part_number") Then
                                        Dim sqlCheckSum As String = "select isnull(sum(qty),0) from stock_card where material='" & dtCheckStockONHOLD.Rows(i).Item("part_number") & "' and department='" & globVar.department & "' and sub_sub_po='" & TextBox11.Text & "' and line='" & ComboBox1.Text & "' and status='Production Process'"
                                        Dim dtCheckSum As DataTable = Database.GetData(sqlCheckSum)
                                        If Convert.ToDouble(DataGridView1.Rows(j).Cells(2).Value) * Convert.ToInt16(TextBox7.Text) <= dtCheckSum.Rows(0).Item(0) Then
                                            CompExist += dtCheckStockONHOLD.Rows(j).Item("part_number") & ","
                                        End If
                                    End If
                                Next
                            Next

                            If CompExist <> "" Then
                                MessageBox.Show("Cannot add ONHOLD. Because (" & CompExist & ") Material Qty more than Qty Need")
                                TextBox1.Clear()
                                Exit Sub
                            End If

                            For i = 0 To dtCheckStockONHOLD.Rows.Count - 1
                                Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,SUM_QTY,LEVEL,ID_LEVEL)
                                    VALUES ('" & dtCheckStockONHOLD.Rows(i).Item("part_number") & "'," & dtCheckStockONHOLD.Rows(i).Item("qty").ToString.Replace(",", ".") & ",'" & dtCheckStockONHOLD.Rows(i).Item("INV_CTRL_DATE") & "','" & dtCheckStockONHOLD.Rows(i).Item("TRACEABILITY") & "'," & dtCheckStockONHOLD.Rows(i).Item("lot_no") & ",'" & dtCheckStockONHOLD.Rows(i).Item("batch_no") & "','" & TextBox5.Text & "','" & TextBox11.Text & "','" & TextBox2.Text & "',0,'" & ComboBox1.Text & "','" & TextBox10.Text & "','Production Request','" & globVar.department & "','NO'," & dtCheckStockONHOLD.Rows(i).Item("qty").ToString.Replace(",", ".") & ",'OH','" & dtCheckStockONHOLD.Rows(i).Item("CODE_STOCK_PROD_ONHOLD") & "')"
                                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                cmdInsertInputStockDetail.ExecuteNonQuery()

                                Dim sqlCheckSummaryFG As String = "select * from summary_fg where material = '" & globVar.QRCode_PN & "' and sub_sub_po='" & TextBox11.Text & "' and fg='" & TextBox2.Text & "'"
                                Dim dtCheckSummaryFG As DataTable = Database.GetData(sqlCheckSummaryFG)

                                Dim dtInOnHold As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & TextBox11.Text & "' and material='" & dtCheckStockONHOLD.Rows(i).Item("part_number") & "' and status='Production Request' and [level]='OH'")

                                If dtCheckSummaryFG.Rows.Count > 0 Then
                                    Dim queryUpdateStockCardProdReq As String = "update summary_fg set onhold_in=" & dtInOnHold.Rows(0)(0).ToString.Replace(",", ".") & ",total_in=(SELECT isnull(SUM(FRESH_IN+BALANCE_IN+WIP_IN+ONHOLD_IN+OTHERS_IN+SA_IN),0) FROM SUMMARY_FG WHERE id = " & dtCheckSummaryFG.Rows(0).Item("id") & ") where id=" & dtCheckSummaryFG.Rows(0).Item("id")
                                    Dim dtUpdateStockCardProdReq = New SqlCommand(queryUpdateStockCardProdReq, Database.koneksi)
                                    dtUpdateStockCardProdReq.ExecuteNonQuery()
                                Else
                                    Dim sqlInsertSummaryFG As String = "INSERT INTO summary_fg (sub_sub_po, FG ,material,onhold_in,total_in) VALUES ('" & TextBox11.Text & "','" & TextBox2.Text & "','" & globVar.QRCode_PN & "'," & dtInOnHold.Rows(0)(0).ToString.Replace(",", ".") & "," & dtInOnHold.Rows(0)(0).ToString.Replace(",", ".") & ")"
                                    Dim cmdInsertSummaryFG = New SqlCommand(sqlInsertSummaryFG, Database.koneksi)
                                    cmdInsertSummaryFG.ExecuteNonQuery()
                                End If
                            Next

                            For i = 0 To dtCheckStockONHOLD.Rows.Count - 1
                                Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcessONHOLD @sub_sub_po='" & TextBox11.Text & "', @fg='" & TextBox2.Text & "',@line='" & ComboBox1.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtCheckStockONHOLD.Rows(i).Item("qty").ToString.Replace(",", ".") & ",@material='" & dtCheckStockONHOLD.Rows(i).Item("part_number") & "',@lot_material='" & dtCheckStockONHOLD.Rows(i).Item("lot_no") & "',@codeONHOLD='" & dtCheckStockONHOLD.Rows(i).Item("code_stock_prod_onhold") & "',@po='" & TextBox5.Text & "',@sub_po='" & TextBox10.Text & "'"
                                Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                            Next

                            TextBox1.Text = ""
                            DGV_DOC()
                        Else
                            MsgBox("ONHOLD not Exist in DB")
                            TextBox1.Text = ""
                            DGV_DOC()
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                ElseIf InStr(TextBox1.Text, "OT") > 0 Then
                    Try
                        Dim CompExist As String = ""

                        Dim sqlCheckStockOTHERS As String = "select * from STOCK_PROD_OTHERS where CODE_STOCK_PROD_OTHERS='" & TextBox1.Text & "' and department='" & globVar.department & "' and qty>0"
                        Dim dtCheckStockOTHERS As DataTable = Database.GetData(sqlCheckStockOTHERS)
                        If dtCheckStockOTHERS.Rows.Count > 0 Then
                            For i = 0 To dtCheckStockOTHERS.Rows.Count - 1
                                For j = 0 To DataGridView1.Rows.Count - 1
                                    If DataGridView1.Rows(j).Cells(1).Value = dtCheckStockOTHERS.Rows(i).Item("part_number") Then
                                        Dim sqlCheckSum As String = "select isnull(sum(qty),0) from stock_card where material='" & dtCheckStockOTHERS.Rows(i).Item("part_number") & "' and department='" & globVar.department & "' and sub_sub_po='" & TextBox11.Text & "' and line='" & ComboBox1.Text & "' and status='Production Process'"
                                        Dim dtCheckSum As DataTable = Database.GetData(sqlCheckSum)
                                        If Convert.ToDouble(DataGridView1.Rows(j).Cells(2).Value) * Convert.ToInt16(TextBox7.Text) <= dtCheckSum.Rows(0).Item(0) Then
                                            CompExist += dtCheckStockOTHERS.Rows(i).Item("part_number") & ","
                                        End If
                                    End If
                                Next
                            Next

                            If CompExist <> "" Then
                                MessageBox.Show("Cannot add OTHERS. Because (" & CompExist & ") Material Qty more than Qty Need")
                                TextBox1.Clear()
                                Exit Sub
                            End If

                            For i = 0 To dtCheckStockOTHERS.Rows.Count - 1
                                Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,SUM_QTY,LEVEL,ID_LEVEL)
                                    VALUES ('" & dtCheckStockOTHERS.Rows(i).Item("part_number") & "'," & dtCheckStockOTHERS.Rows(i).Item("qty").ToString.Replace(",", ".") & ",'" & dtCheckStockOTHERS.Rows(i).Item("INV_CTRL_DATE") & "','" & dtCheckStockOTHERS.Rows(i).Item("TRACEABILITY") & "'," & dtCheckStockOTHERS.Rows(i).Item("lot_no") & ",'" & dtCheckStockOTHERS.Rows(i).Item("batch_no") & "','" & TextBox5.Text & "','" & TextBox11.Text & "','" & TextBox2.Text & "',0,'" & ComboBox1.Text & "','" & TextBox10.Text & "','Production Request','" & globVar.department & "','NO'," & dtCheckStockOTHERS.Rows(i).Item("qty").ToString.Replace(",", ".") & ",'OT','" & dtCheckStockOTHERS.Rows(i).Item("CODE_STOCK_PROD_OTHERS") & "')"
                                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                cmdInsertInputStockDetail.ExecuteNonQuery()

                                Dim sqlCheckSummaryFG As String = "select * from summary_fg where material = '" & globVar.QRCode_PN & "' and sub_sub_po='" & TextBox11.Text & "' and fg='" & TextBox2.Text & "'"
                                Dim dtCheckSummaryFG As DataTable = Database.GetData(sqlCheckSummaryFG)

                                Dim dtInOthers As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & TextBox11.Text & "' and material='" & dtCheckStockOTHERS.Rows(i).Item("part_number") & "' and status='Production Request' and [level]='OT'")

                                If dtCheckSummaryFG.Rows.Count > 0 Then
                                    Dim queryUpdateStockCardProdReq As String = "update summary_fg set onhold_in=" & dtInOthers.Rows(0)(0).ToString.Replace(",", ".") & ",total_in=(SELECT isnull(SUM(FRESH_IN+BALANCE_IN+WIP_IN+ONHOLD_IN+OTHERS_IN+SA_IN),0) FROM SUMMARY_FG WHERE id = " & dtCheckSummaryFG.Rows(0).Item("id") & ") where id=" & dtCheckSummaryFG.Rows(0).Item("id")
                                    Dim dtUpdateStockCardProdReq = New SqlCommand(queryUpdateStockCardProdReq, Database.koneksi)
                                    dtUpdateStockCardProdReq.ExecuteNonQuery()
                                Else
                                    Dim sqlInsertSummaryFG As String = "INSERT INTO summary_fg (sub_sub_po, FG ,material,onhold_in,total_in) VALUES ('" & TextBox11.Text & "','" & TextBox2.Text & "','" & globVar.QRCode_PN & "'," & dtInOthers.Rows(0)(0).ToString.Replace(",", ".") & "," & dtInOthers.Rows(0)(0).ToString.Replace(",", ".") & ")"
                                    Dim cmdInsertSummaryFG = New SqlCommand(sqlInsertSummaryFG, Database.koneksi)
                                    cmdInsertSummaryFG.ExecuteNonQuery()
                                End If
                            Next

                            For i = 0 To dtCheckStockOTHERS.Rows.Count - 1
                                Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcessOTHERS @sub_sub_po='" & TextBox11.Text & "', @fg='" & TextBox2.Text & "',@line='" & ComboBox1.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtCheckStockOTHERS.Rows(i).Item("qty").ToString.Replace(",", ".") & ",@material='" & dtCheckStockOTHERS.Rows(i).Item("part_number") & "',@lot_material='" & dtCheckStockOTHERS.Rows(i).Item("lot_no") & "',@codeOTHERS='" & dtCheckStockOTHERS.Rows(i).Item("CODE_STOCK_PROD_OTHERS") & "',@po='" & TextBox5.Text & "',@sub_po='" & TextBox10.Text & "'"
                                Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                            Next

                            TextBox1.Text = ""
                            DGV_DOC()
                        Else
                            MsgBox("OTHERS not Exist in DB")
                            TextBox1.Text = ""
                            DGV_DOC()
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                ElseIf InStr(TextBox1.Text, "SA") > 0 Then
                    Try
                        Dim CompExist As String = ""

                        Dim sqlCheckStockSubAssy As String = "select * from STOCK_PROD_SUB_ASSY where CODE_STOCK_PROD_SUB_ASSY='" & TextBox1.Text & "' and department='" & globVar.department & "' and qty>0"
                        Dim dtCheckStockSubAssy As DataTable = Database.GetData(sqlCheckStockSubAssy)
                        If dtCheckStockSubAssy.Rows.Count > 0 Then
                            For i = 0 To dtCheckStockSubAssy.Rows.Count - 1
                                For j = 0 To DataGridView1.Rows.Count - 1
                                    If DataGridView1.Rows(j).Cells(1).Value = dtCheckStockSubAssy.Rows(i).Item("fg") Then
                                        Dim sqlCheckSum As String = "select isnull(sum(qty),0) from stock_card where material='" & dtCheckStockSubAssy.Rows(i).Item("fg") & "' and department='" & globVar.department & "' and sub_sub_po='" & TextBox11.Text & "' and line='" & ComboBox1.Text & "' and status='Production Process'"
                                        Dim dtCheckSum As DataTable = Database.GetData(sqlCheckSum)
                                        'If (Convert.ToDouble(DataGridView1.Rows(j).Cells(2).Value) * Convert.ToDouble(TextBox7.Text)) + Math.Round(Convert.ToDouble(DataGridView1.Rows(j).Cells(2).Value) * Convert.ToDouble(TextBox7.Text) * Convert.ToDouble(TextBox8.Text) / 100) <= dtCheckSum.Rows(0).Item(0) Then
                                        If Convert.ToDouble(DataGridView1.Rows(j).Cells(2).Value) * Convert.ToDouble(TextBox7.Text) <= dtCheckSum.Rows(0).Item(0) Then
                                            CompExist += dtCheckStockSubAssy.Rows(i).Item("fg") & ","
                                        End If
                                    End If
                                Next
                            Next

                            If CompExist <> "" Then
                                MessageBox.Show("Cannot add Sub Assy. Because (" & CompExist & ") Material Qty more than Qty Need")
                                TextBox1.Clear()
                                Exit Sub
                            End If

                            For i = 0 To dtCheckStockSubAssy.Rows.Count - 1
                                Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,SUM_QTY,LEVEL,ID_LEVEL)
                                    VALUES ('" & dtCheckStockSubAssy.Rows(i).Item("fg") & "'," & dtCheckStockSubAssy.Rows(i).Item("qty").ToString.Replace(",", ".") & ",'" & dtCheckStockSubAssy.Rows(i).Item("INV_CTRL_DATE") & "','" & dtCheckStockSubAssy.Rows(i).Item("TRACEABILITY") & "'," & dtCheckStockSubAssy.Rows(i).Item("lot_no") & ",'" & dtCheckStockSubAssy.Rows(i).Item("batch_no") & "','" & TextBox5.Text & "','" & TextBox11.Text & "','" & TextBox2.Text & "',0,'" & ComboBox1.Text & "','" & TextBox10.Text & "','Production Request','" & globVar.department & "','YES'," & dtCheckStockSubAssy.Rows(i).Item("qty").ToString.Replace(",", ".") & ",'SA','" & dtCheckStockSubAssy.Rows(i).Item("CODE_STOCK_PROD_SUB_ASSY") & "')"
                                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                cmdInsertInputStockDetail.ExecuteNonQuery()

                                Dim sqlCheckSummaryFG As String = "select * from summary_fg where material = '" & globVar.QRCode_PN & "' and sub_sub_po='" & TextBox11.Text & "' and fg='" & TextBox2.Text & "'"
                                Dim dtCheckSummaryFG As DataTable = Database.GetData(sqlCheckSummaryFG)

                                Dim dtInSA As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & TextBox11.Text & "' and material='" & dtCheckStockSubAssy.Rows(i).Item("fg") & "' and status='Production Request' and [level]='SA'")

                                If dtCheckSummaryFG.Rows.Count > 0 Then
                                    Dim queryUpdateStockCardProdReq As String = "update summary_fg set onhold_in=" & dtInSA.Rows(0)(0).ToString.Replace(",", ".") & ",total_in=(SELECT isnull(SUM(FRESH_IN+BALANCE_IN+WIP_IN+ONHOLD_IN+OTHERS_IN+SA_IN),0) FROM SUMMARY_FG WHERE id = " & dtCheckSummaryFG.Rows(0).Item("id") & ") where id=" & dtCheckSummaryFG.Rows(0).Item("id")
                                    Dim dtUpdateStockCardProdReq = New SqlCommand(queryUpdateStockCardProdReq, Database.koneksi)
                                    dtUpdateStockCardProdReq.ExecuteNonQuery()
                                Else
                                    Dim sqlInsertSummaryFG As String = "INSERT INTO summary_fg (sub_sub_po, FG ,material,onhold_in,total_in) VALUES ('" & TextBox11.Text & "','" & TextBox2.Text & "','" & globVar.QRCode_PN & "'," & dtInSA.Rows(0)(0).ToString.Replace(",", ".") & "," & dtInSA.Rows(0)(0).ToString.Replace(",", ".") & ")"
                                    Dim cmdInsertSummaryFG = New SqlCommand(sqlInsertSummaryFG, Database.koneksi)
                                    cmdInsertSummaryFG.ExecuteNonQuery()
                                End If
                            Next

                            For i = 0 To dtCheckStockSubAssy.Rows.Count - 1
                                Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcessSubAssy @sub_sub_po='" & TextBox11.Text & "', @fg='" & TextBox2.Text & "',@line='" & ComboBox1.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtCheckStockSubAssy.Rows(i).Item("qty").ToString.Replace(",", ".") & ",@material='" & dtCheckStockSubAssy.Rows(i).Item("fg") & "',@lot_material='" & dtCheckStockSubAssy.Rows(i).Item("lot_no") & "',@codeSubAssy='" & dtCheckStockSubAssy.Rows(i).Item("CODE_STOCK_PROD_SUB_ASSY") & "',@po='" & TextBox5.Text & "',@sub_po='" & TextBox10.Text & "'"
                                Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                            Next

                            TextBox1.Text = ""
                            DGV_DOC()
                        Else
                            MsgBox("Sub Assy not Exist in DB")
                            TextBox1.Text = ""
                            DGV_DOC()
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            End If
        End If
    End Sub

    Sub DGV_DOC()
        Try
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.DataSource = Nothing
            DataGridView1.Rows.Clear()
            DataGridView1.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryDOC As String = "select desc_comp Description,component Component,Usage from prod_doc where line='" & ComboBox1.Text & "' and sub_sub_po='" & TextBox11.Text & "' and department='" & globVar.department & "'"
            Dim dtDOC As DataTable = Database.GetData(queryDOC)

            DataGridView1.DataSource = dtDOC

            Dim scan As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
            scan.Name = "Lot Scan"
            scan.HeaderText = "Lot Scan"
            DataGridView1.Columns.Insert(3, scan)

            DataGridView1.Columns(0).Width = 250
            DataGridView1.Columns(1).Width = 70
            DataGridView1.Columns(2).Width = 70
            DataGridView1.Columns(3).Width = 900

            For rowDataSet As Integer = 0 To dtDOC.Rows.Count - 1
                Dim queryCheck As String = "select DISTINCT(lot_no),id_level,level from stock_card where material='" & dtDOC.Rows(rowDataSet).Item("Component").ToString & "' and sub_sub_po='" & TextBox11.Text & "' and line ='" & ComboBox1.Text & "' and status='Production Process'"
                Dim dtCHECK As DataTable = Database.GetData(queryCheck)
                For i As Integer = 0 To dtCHECK.Rows.Count - 1
                    If dtCHECK.Rows(i).Item("level").ToString = "Fresh" Then
                        DataGridView1.Rows(rowDataSet).Cells(3).Value += dtCHECK.Rows(i).Item("lot_no").ToString & ","
                    Else
                        DataGridView1.Rows(rowDataSet).Cells(3).Value += dtCHECK.Rows(i).Item("lot_no").ToString & "(" & dtCHECK.Rows(i).Item("id_level").ToString & "),"
                    End If
                Next
            Next

            For i As Integer = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                    DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next i
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub DGV_DOP()
        Try
            DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView2.DataSource = Nothing
            DataGridView2.Rows.Clear()
            DataGridView2.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryDOP As String = "select Process, operator_id Operator from prod_dop where line='" & ComboBox1.Text & "' and sub_sub_po='" & TextBox11.Text & "' and department='" & globVar.department & "' and operator_id is not null"
            Dim dtDOP As DataTable = Database.GetData(queryDOP)

            DataGridView2.DataSource = dtDOP

            For i As Integer = 0 To DataGridView2.RowCount - 1
                If DataGridView2.Rows(i).Index Mod 2 = 0 Then
                    DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next i
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Production_Load(sender As Object, e As EventArgs) Handles Me.Load
        tampilDataComboBoxLine()
        TextBox1.ReadOnly = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox1.Text <> "" Then
            TextBox1.ReadOnly = False
            Try
                Dim query As String = "select mp.po,mp.sub_po,mp.fg_pn,ssp.sub_sub_po,mfg.description,ssp.sub_sub_po_qty,mfg.spq,ssp.yield_lose
            from sub_sub_po ssp,main_po mp,master_finish_goods mfg 
            where ssp.status='Open' and mp.id=ssp.main_po and mfg.fg_part_number=mp.fg_pn and ssp.line='" & ComboBox1.Text & "' and mp.department='" & globVar.department & "'"
                Dim dt As DataTable = Database.GetData(query)
                If dt.Rows.Count > 0 Then
                    TextBox2.Text = dt.Rows(0).Item("FG_PN").ToString
                    TextBox4.Text = dt.Rows(0).Item("DESCRIPTION").ToString
                    TextBox5.Text = dt.Rows(0).Item("PO").ToString
                    TextBox6.Text = dt.Rows(0).Item("SUB_SUB_PO_QTY").ToString
                    TextBox7.Text = dt.Rows(0).Item("SPQ").ToString
                    TextBox11.Text = dt.Rows(0).Item("SUB_SUB_PO").ToString
                    TextBox10.Text = dt.Rows(0).Item("SUB_PO").ToString
                    TextBox8.Text = dt.Rows(0).Item("YIELD_LOSE").ToString

                    Dim queryFam As String = "SELECT DISTINCT(FAMILY) FROM MATERIAL_USAGE_FINISH_GOODS WHERE FG_PART_NUMBER='" & dt.Rows(0).Item("FG_PN").ToString & "'"
                    Dim dtFam As DataTable = Database.GetData(queryFam)
                    TextBox3.Text = dtFam.Rows(0).Item("family").ToString

                    DGV_DOC()
                    DGV_DOP()
                Else
                    MessageBox.Show("This line no have any PO")
                    DGV_DOC()
                    DGV_DOP()
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MessageBox.Show("Please select line and fill the Flow Ticket first. ")
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If Not CheckBox1.Checked Then
            TextBox9.ReadOnly = False
            TextBox12.ReadOnly = False
            Button3.Enabled = True
            TextBox1.ReadOnly = True
        Else
            TextBox9.ReadOnly = True
            TextBox12.ReadOnly = True
            Button3.Enabled = False
            TextBox1.ReadOnly = False
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If DataGridView2.Rows.Count = 0 Then
                MessageBox.Show("Cannot process When Detail Of Process blank. Please set Operator First.")
                TextBox9.Clear()
                TextBox12.Clear()
                Exit Sub
            End If

            If TextBox9.Text <> "" And TextBox12.Text <> "" Then
                Dim sqlCheckInStock As String = "select in_material.* from sub_sub_po sp, stock_card in_material where in_material.SUB_SUB_PO = sp.sub_sub_po and sp.status='Open' and in_material.line='" & ComboBox1.Text & "' and in_material.material = '" & TextBox9.Text & "' and in_material.lot_no=" & TextBox12.Text & " and sp.sub_sub_po='" & TextBox11.Text & "' and in_material.status='Production Request' and department='" & globVar.department & "'"
                Dim dtCheckInStock As DataTable = Database.GetData(sqlCheckInStock)
                If dtCheckInStock.Rows.Count > 0 Then
                    Dim sqlCheckInStockNewRecord As String = "select * from stock_card where line='" & ComboBox1.Text & "' and material = '" & TextBox9.Text & "' and lot_no=" & TextBox12.Text & " and sub_sub_po='" & TextBox11.Text & "' and status='Production Process' and department='" & globVar.department & "'"
                    Dim dtCheckInStockNewRecord As DataTable = Database.GetData(sqlCheckInStockNewRecord)
                    If dtCheckInStockNewRecord.Rows.Count > 0 Then
                        MessageBox.Show("Double Scan")
                        TextBox1.Text = ""
                        DGV_DOC()
                    Else
                        Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & TextBox11.Text & "', @fg='" & TextBox2.Text & "',@line='" & ComboBox1.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtCheckInStock.Rows(0).Item("actual_qty") & ",@material='" & TextBox9.Text & "',@lot_material='" & TextBox12.Text & "'"
                        Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)

                        TextBox9.Text = ""
                        TextBox12.Clear()
                        DGV_DOC()

                        For i = 0 To DataGridView1.Rows.Count - 1
                            If DataGridView1.Rows(i).Cells(1).Value = TextBox9.Text Then
                                DataGridView1.Rows(i).Cells(3).Selected = True
                            End If
                        Next
                    End If

                Else
                    MessageBox.Show("Sorry this material not for this line.")
                    TextBox9.Text = ""
                    TextBox12.Clear()
                    TextBox9.Select()
                End If
            Else
                TextBox9.Clear()
                TextBox12.Clear()
                TextBox9.Select()
                MessageBox.Show("Sorry Comp and Lot No cannot be blank")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub reset()
        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()
        DataGridView1.Rows.Clear()

        DataGridView2.DataSource = Nothing
        DataGridView2.Columns.Clear()
        DataGridView2.Rows.Clear()

        ComboBox1.SelectedIndex = -1
        TextBox1.Clear()
        TextBox9.Clear()
        TextBox12.Clear()
        TextBox8.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox6.Clear()
        TextBox5.Clear()
        TextBox10.Clear()
        TextBox11.Clear()
        TextBox4.Clear()
        TextBox7.Clear()
        TextBox1.ReadOnly = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        reset()
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

    Private Sub DataGridView2_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView2.DataBindingComplete
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
End Class