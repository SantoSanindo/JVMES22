Imports System.Data.SqlClient
Imports System.Net.Mime.MediaTypeNames
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MdiTabControl

Public Class ProductionV2
    Public Shared menu As String = "Production1"

    Sub tampilDataComboBoxLine()
        Call Database.koneksi_database()
        Dim dtMasterLine As DataTable = Database.GetData("select line as name from SUB_SUB_PO ssp LEFT JOIN main_po mp on mp.id=ssp.main_po and mp.department ='" & globVar.department & "' where ssp.status='Open' order by ssp.line")

        ComboBox1.DataSource = dtMasterLine
        ComboBox1.DisplayMember = "NAME"
        ComboBox1.ValueMember = "NAME"
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown

        Dim QrcodeValid As Boolean

        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then

            If globVar.add > 0 Then

                If DataGridView2.Rows.Count = 0 Then

                    RJMessageBox.Show("Cannot process When Detail Of Process blank. Please set Operator First.")
                    TextBox1.Clear()
                    Exit Sub

                End If

                If TextBox1.Text.StartsWith("MX2D") Then 'Normal

                    Try
                        Dim ds As New DataSet
                        Dim yieldlose As Integer = 0
                        Dim usage As Integer = 0
                        Dim targetQty As Integer = 0

                        QrcodeValid = QRCode.Baca(TextBox1.Text)

                        If QrcodeValid = False Then
                            RJMessageBox.Show("QRCode Not Valid")
                            Play_Sound.Wrong()
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        If globVar.QRCode_PN = "" Or globVar.QRCode_lot = "" Or globVar.QRCode_Traceability = "" Or globVar.QRCode_Batch = "" Or globVar.QRCode_Inv = "" Then
                            RJMessageBox.Show("QRCode Not Valid")
                            Play_Sound.Wrong()
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        Dim sqlCheckKecukupanQty As String = "SELECT
	                                                                (
	                                                                SELECT CEILING( SUM
		                                                                ( (mufg.usage * ssp.sub_sub_po_qty) + (mufg.usage * ssp.sub_sub_po_qty * ssp.YIELD_LOSE) / 100 )) 
	                                                                FROM
		                                                                sub_sub_po ssp,
		                                                                MATERIAL_USAGE_FINISH_GOODS mufg,
		                                                                main_po mp 
	                                                                WHERE
		                                                                ssp.sub_sub_po= '" & TextBox11.Text & "' 
		                                                                AND ssp.main_po= mp.id 
		                                                                AND mufg.fg_part_number= mp.fg_pn 
		                                                                AND mufg.COMPONENT= '" & globVar.QRCode_PN & "' 
	                                                                ) total_kebutuhan,
	                                                                isnull( SUM ( actual_qty ), 0 ) total_input 
                                                                FROM
	                                                                stock_card 
                                                                WHERE
	                                                                sub_sub_po = '" & TextBox11.Text & "' 
	                                                                AND status = 'Production Process' 
	                                                                AND material = '" & globVar.QRCode_PN & "'"


                        Dim dtCheckKecukupanQty As DataTable = Database.GetData(sqlCheckKecukupanQty)

                        If dtCheckKecukupanQty.Rows(0).Item("total_kebutuhan") > dtCheckKecukupanQty.Rows(0).Item("total_input") Then

                            Dim sqlCheckStockCard As String = "select * from stock_card where material = '" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' and inv_ctrl_date='" & globVar.QRCode_Inv & "' and traceability='" & globVar.QRCode_Traceability & "' and batch_no='" & globVar.QRCode_Batch & "' and sub_sub_po='" & TextBox11.Text & "' and finish_goods_pn='" & TextBox2.Text & "' and status='Production Request' and actual_qty > 0"
                            Dim dtCheckStockCard As DataTable = Database.GetData(sqlCheckStockCard)

                            If dtCheckStockCard.Rows.Count = 0 Then

                                RJMessageBox.Show("Qty of this material is 0")
                                TextBox1.Text = ""
                                TextBox1.Select()
                                Exit Sub

                            End If

                            If dtCheckStockCard.Rows(0).Item("qrcode").StartsWith("SM") Then

                                RJMessageBox.Show("QRCode not valid")
                                TextBox1.Text = ""
                                TextBox1.Select()
                                Exit Sub

                            End If

                            If dtCheckStockCard.Rows.Count > 1 Then

                                RJMessageBox.Show("The scanned material has more than 1 material. " & globVar.QRCode_PN & " - " & globVar.QRCode_lot)
                                TextBox1.Clear()
                                TextBox1.Select()
                                Exit Sub

                            End If

                            If dtCheckStockCard.Rows(0).Item("actual_qty") <= 0 Then

                                RJMessageBox.Show("Qty of this material is 0")
                                Exit Sub

                            End If

                            Dim sqlCheckInStockNewRecord As String = "select * from stock_card where line='" & ComboBox1.Text & "' and material = '" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' and inv_ctrl_date='" & globVar.QRCode_Inv & "' and batch_no='" & globVar.QRCode_Batch & "' and traceability='" & globVar.QRCode_Traceability & "' and sub_sub_po='" & TextBox11.Text & "' and status='Production Process' and department='" & globVar.department & "'"
                            Dim dtCheckInStockNewRecord As DataTable = Database.GetData(sqlCheckInStockNewRecord)
                            If dtCheckInStockNewRecord.Rows.Count > 0 Then

                                RJMessageBox.Show("Double Scan")
                                TextBox1.Text = ""
                                DGV_DOC()
                                DGV_DOS()

                            Else

                                Dim sqlInsertStockCardProductionProcess As String = "INSERT INTO [dbo].[STOCK_CARD] ([MTS_NO],[DEPARTMENT],[MATERIAL],[STATUS],[STANDARD_PACK],[INV_CTRL_DATE],[TRACEABILITY],
	                                                                            [BATCH_NO],[LOT_NO],[FINISH_GOODS_PN],[PO],[SUB_PO],[SUB_SUB_PO],[LINE],[QRCODE],[QTY],[ACTUAL_QTY],[ID_LEVEL],[LEVEL],[SUM_QTY],[PRODUCTION_PROCESS_WHO],[PRODUCTION_PROCESS_DATETIME]) 
                                                                            SELECT [MTS_NO],[DEPARTMENT],[MATERIAL],'Production Process',[STANDARD_PACK],[INV_CTRL_DATE],[TRACEABILITY],[BATCH_NO],[LOT_NO],[FINISH_GOODS_PN],[PO],[SUB_PO],[SUB_SUB_PO],[LINE],[QRCODE],[QTY],[ACTUAL_QTY],[ID_LEVEL],[LEVEL],[SUM_QTY],'" & globVar.username & "',GETDATE() 
                                                                            FROM stock_card WHERE id =" & dtCheckStockCard.Rows(0).Item("id")
                                Dim cmdInsertStockCardProductionProcess = New SqlCommand(sqlInsertStockCardProductionProcess, Database.koneksi)
                                If cmdInsertStockCardProductionProcess.ExecuteNonQuery() Then

                                    Dim queryUpdateStockCardProdReq As String = "update stock_card set actual_qty = 0 where id=" & dtCheckStockCard.Rows(0).Item("id")
                                    Dim dtUpdateStockCardProdReq = New SqlCommand(queryUpdateStockCardProdReq, Database.koneksi)
                                    dtUpdateStockCardProdReq.ExecuteNonQuery()

                                    TextBox1.Clear()
                                    DGV_DOC()
                                    DGV_DOS()

                                Else

                                    RJMessageBox.Show("Material " & globVar.QRCode_PN & " failed to scan")
                                    TextBox1.Clear()

                                End If

                                For i = 0 To DataGridView1.Rows.Count - 1
                                    If DataGridView1.Rows(i).Cells(1).Value = globVar.QRCode_PN Then
                                        DataGridView1.Rows(i).Cells(3).Selected = True
                                    End If
                                Next

                            End If

                        Else
                            RJMessageBox.Show("Material " & globVar.QRCode_PN & " is full")
                            TextBox1.Text = ""
                        End If

                    Catch ex As Exception
                        RJMessageBox.Show("Error Production - 1 =>" & ex.Message)
                    End Try

                ElseIf TextBox1.Text.StartsWith("SM") AndAlso TextBox1.Text.Length > 2 AndAlso IsNumeric(TextBox1.Text.Substring(2)) Then 'Split Material

                    Try

                        Dim ds As New DataSet
                        Dim yieldlose As Integer = 0
                        Dim usage As Integer = 0
                        Dim targetQty As Integer = 0

                        Dim sqlCheckStockCard As String = "select * from stock_card where qrcode = '" & TextBox1.Text & "' and sub_sub_po='" & TextBox11.Text & "' and finish_goods_pn='" & TextBox2.Text & "' and status='Production Request'"
                        Dim dtCheckStockCard As DataTable = Database.GetData(sqlCheckStockCard)
                        If dtCheckStockCard.Rows.Count = 0 Then
                            RJMessageBox.Show("Sorry this material not for this line.")
                            TextBox1.Text = ""
                            TextBox1.Select()
                            Exit Sub
                        End If

                        If dtCheckStockCard.Rows(0).Item("actual_qty") <= 0 Then
                            RJMessageBox.Show("Qty of this material is 0")
                            TextBox1.Text = ""
                            TextBox1.Select()
                            Exit Sub
                        End If

                        globVar.QRCode_PN = dtCheckStockCard.Rows(0).Item("material")
                        globVar.QRCode_lot = dtCheckStockCard.Rows(0).Item("lot_no")

                        Dim sqlCheckKecukupanQty As String = "SELECT ( select CEILING(sum((mufg.usage * ssp.sub_sub_po_qty) + (mufg.usage * ssp.sub_sub_po_qty * ssp.YIELD_LOSE) / 100)) from sub_sub_po ssp, MATERIAL_USAGE_FINISH_GOODS mufg, main_po mp where ssp.sub_sub_po='" & TextBox11.Text & "' and ssp.main_po=mp.id and mufg.fg_part_number=mp.fg_pn and mufg.COMPONENT='" & globVar.QRCode_PN & "' ) total_kebutuhan, isnull( SUM ( sum_qty ), 0 ) total_input FROM stock_card WHERE sub_sub_po= '" & TextBox11.Text & "' AND status= 'Production Process' AND material= '" & globVar.QRCode_PN & "'"
                        Dim dtCheckKecukupanQty As DataTable = Database.GetData(sqlCheckKecukupanQty)

                        If IsDBNull(dtCheckKecukupanQty.Rows(0).Item("total_kebutuhan")) Then
                            RJMessageBox.Show("Material Doesn't Exists in this Finish Goods")
                            TextBox1.Text = ""
                            Exit Sub
                        End If

                        If dtCheckKecukupanQty.Rows(0).Item("total_kebutuhan") > dtCheckKecukupanQty.Rows(0).Item("total_input") Then

                            Dim sqlCheckInStockNewRecord As String = "select * from stock_card where line='" & ComboBox1.Text & "' and material = '" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' and sub_sub_po='" & TextBox11.Text & "' and status='Production Process' and department='" & globVar.department & "' and qrcode = '" & TextBox1.Text & "'"
                            Dim dtCheckInStockNewRecord As DataTable = Database.GetData(sqlCheckInStockNewRecord)
                            If dtCheckInStockNewRecord.Rows.Count > 0 Then

                                RJMessageBox.Show("Double Scan")
                                TextBox1.Text = ""
                                DGV_DOC()
                                DGV_DOS()

                            Else

                                Dim sqlInsertStockCardProductionProcess As String = "INSERT INTO [dbo].[STOCK_CARD] ([MTS_NO],[DEPARTMENT],[MATERIAL],[STATUS],[STANDARD_PACK],[INV_CTRL_DATE],[TRACEABILITY],
	                                                                            [BATCH_NO],[LOT_NO],[FINISH_GOODS_PN],[PO],[SUB_PO],[SUB_SUB_PO],[LINE],[QRCODE],[QTY],[ACTUAL_QTY],[ID_LEVEL],[LEVEL],[SUM_QTY],[PRODUCTION_PROCESS_WHO],[PRODUCTION_PROCESS_DATETIME]) 
                                                                            SELECT [MTS_NO],[DEPARTMENT],[MATERIAL],'Production Process',[STANDARD_PACK],[INV_CTRL_DATE],[TRACEABILITY],[BATCH_NO],[LOT_NO],[FINISH_GOODS_PN],[PO],[SUB_PO],[SUB_SUB_PO],[LINE],[QRCODE],[QTY],[ACTUAL_QTY],[ID_LEVEL],[LEVEL],[SUM_QTY],'" & globVar.username & "',GETDATE() 
                                                                            FROM stock_card WHERE id =" & dtCheckStockCard.Rows(0).Item("id")
                                Dim cmdInsertStockCardProductionProcess = New SqlCommand(sqlInsertStockCardProductionProcess, Database.koneksi)
                                If cmdInsertStockCardProductionProcess.ExecuteNonQuery() Then

                                    Dim queryUpdateStockCardProdReq As String = "update stock_card set actual_qty = 0 where id=" & dtCheckStockCard.Rows(0).Item("id")
                                    Dim dtUpdateStockCardProdReq = New SqlCommand(queryUpdateStockCardProdReq, Database.koneksi)
                                    dtUpdateStockCardProdReq.ExecuteNonQuery()

                                    TextBox1.Clear()
                                    DGV_DOC()
                                    DGV_DOS()

                                Else

                                    RJMessageBox.Show("Material " & globVar.QRCode_PN & " failed to scan")
                                    TextBox1.Clear()

                                End If

                                For i = 0 To DataGridView1.Rows.Count - 1
                                    If DataGridView1.Rows(i).Cells(1).Value = globVar.QRCode_PN Then
                                        DataGridView1.Rows(i).Cells(3).Selected = True
                                    End If
                                Next

                            End If

                        Else

                            RJMessageBox.Show("Material " & globVar.QRCode_PN & " is full")
                            TextBox1.Text = ""

                        End If


                    Catch ex As Exception

                        RJMessageBox.Show("Error Production - 6 =>" & ex.Message)

                    End Try

                ElseIf TextBox1.Text.StartsWith("B") AndAlso TextBox1.Text.Length > 1 AndAlso IsNumeric(TextBox1.Text.Substring(1)) Then 'Balance

                    Try
                        Dim ds As New DataSet
                        Dim yieldlose As Integer = 0
                        Dim usage As Integer = 0
                        Dim targetQty As Integer = 0

                        Dim sqlCheckBalance As String = "select * from stock_card where qrcode = '" & TextBox1.Text & "' and sub_sub_po='" & TextBox11.Text & "' and finish_goods_pn='" & TextBox2.Text & "' and status='Production Request'"
                        Dim dtCheckBalance As DataTable = Database.GetData(sqlCheckBalance)

                        If dtCheckBalance.Rows.Count = 0 Then
                            RJMessageBox.Show("Sorry this material not for this line.")
                            TextBox1.Text = ""
                            TextBox1.Select()
                            Exit Sub
                        End If

                        If dtCheckBalance.Rows(0).Item("material") <= 0 Then
                            RJMessageBox.Show("Qty of this material is 0")
                            Exit Sub
                        End If

                        globVar.QRCode_PN = dtCheckBalance.Rows(0).Item("material")
                        globVar.QRCode_lot = dtCheckBalance.Rows(0).Item("lot_no")

                        Dim sqlCheckKecukupanQty As String = "SELECT ( select CEILING(sum((mufg.usage * ssp.sub_sub_po_qty) + (mufg.usage * ssp.sub_sub_po_qty * ssp.YIELD_LOSE) / 100)) from sub_sub_po ssp, MATERIAL_USAGE_FINISH_GOODS mufg, main_po mp where ssp.sub_sub_po='" & TextBox11.Text & "' and ssp.main_po=mp.id and mufg.fg_part_number=mp.fg_pn and mufg.COMPONENT='" & globVar.QRCode_PN & "' ) total_kebutuhan, isnull( SUM ( sum_qty ), 0 ) total_input FROM stock_card WHERE sub_sub_po= '" & TextBox11.Text & "' AND status= 'Production Process' AND material= '" & globVar.QRCode_PN & "'"
                        Dim dtCheckKecukupanQty As DataTable = Database.GetData(sqlCheckKecukupanQty)

                        If IsDBNull(dtCheckKecukupanQty.Rows(0).Item("total_kebutuhan")) Then
                            RJMessageBox.Show("Material Doesn't Exists in this Finish Goods")
                            TextBox1.Text = ""
                            Exit Sub
                        End If

                        If dtCheckKecukupanQty.Rows(0).Item("total_kebutuhan") > dtCheckKecukupanQty.Rows(0).Item("total_input") Then

                            Dim sqlCheckInStockNewRecord As String = "select * from stock_card where line='" & ComboBox1.Text & "' and material = '" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' and sub_sub_po='" & TextBox11.Text & "' and status='Production Process' and department='" & globVar.department & "' and qrcode = '" & TextBox1.Text & "'"
                            Dim dtCheckInStockNewRecord As DataTable = Database.GetData(sqlCheckInStockNewRecord)
                            If dtCheckInStockNewRecord.Rows.Count > 0 Then
                                RJMessageBox.Show("Double Scan")
                                TextBox1.Text = ""
                                DGV_DOC()
                                DGV_DOS()
                            Else

                                Dim sqlInsertStockCardProductionProcess As String = "INSERT INTO [dbo].[STOCK_CARD] ([MTS_NO],[DEPARTMENT],[MATERIAL],[STATUS],[STANDARD_PACK],[INV_CTRL_DATE],[TRACEABILITY],
	                                                                            [BATCH_NO],[LOT_NO],[FINISH_GOODS_PN],[PO],[SUB_PO],[SUB_SUB_PO],[LINE],[QRCODE],[QTY],[ACTUAL_QTY],[ID_LEVEL],[LEVEL],[SUM_QTY],[PRODUCTION_PROCESS_WHO],[PRODUCTION_PROCESS_DATETIME]) 
                                                                            SELECT [MTS_NO],[DEPARTMENT],[MATERIAL],'Production Process',[STANDARD_PACK],[INV_CTRL_DATE],[TRACEABILITY],[BATCH_NO],[LOT_NO],[FINISH_GOODS_PN],[PO],[SUB_PO],[SUB_SUB_PO],[LINE],[QRCODE],[QTY],[ACTUAL_QTY],[ID_LEVEL],[LEVEL],[SUM_QTY],'" & globVar.username & "',GETDATE() 
                                                                            FROM stock_card WHERE id =" & dtCheckBalance.Rows(0).Item("id")
                                Dim cmdInsertStockCardProductionProcess = New SqlCommand(sqlInsertStockCardProductionProcess, Database.koneksi)
                                If cmdInsertStockCardProductionProcess.ExecuteNonQuery() Then

                                    Dim queryUpdateStockCardProdReq As String = "update stock_card set actual_qty = 0 where id=" & dtCheckBalance.Rows(0).Item("id")
                                    Dim dtUpdateStockCardProdReq = New SqlCommand(queryUpdateStockCardProdReq, Database.koneksi)
                                    dtUpdateStockCardProdReq.ExecuteNonQuery()

                                    TextBox1.Clear()
                                    DGV_DOC()
                                    DGV_DOS()

                                Else

                                    RJMessageBox.Show("Material " & globVar.QRCode_PN & " failed to scan")
                                    TextBox1.Clear()

                                End If

                                For i = 0 To DataGridView1.Rows.Count - 1
                                    If DataGridView1.Rows(i).Cells(1).Value = globVar.QRCode_PN Then
                                        DataGridView1.Rows(i).Cells(3).Selected = True
                                    End If
                                Next
                            End If

                        Else

                            RJMessageBox.Show("Material " & globVar.QRCode_PN & " is full")
                            TextBox1.Text = ""

                        End If

                    Catch ex As Exception
                        RJMessageBox.Show("Error Production - 1 =>" & ex.Message)
                    End Try

                ElseIf TextBox1.Text.StartsWith("SA") AndAlso TextBox1.Text.Length > 2 AndAlso IsNumeric(TextBox1.Text.Substring(2)) Then 'Sub Assy

                    Try
                        Dim CompExist As String = ""

                        Dim sqlCheckStockSubAssy As String = "select * from STOCK_PROD_SUB_ASSY where CODE_STOCK_PROD_SUB_ASSY='" & TextBox1.Text & "' and department='" & globVar.department & "'"
                        Dim dtCheckStockSubAssy As DataTable = Database.GetData(sqlCheckStockSubAssy)

                        If dtCheckStockSubAssy.Rows.Count = 0 Then
                            RJMessageBox.Show("Sorry this material don't exist in database")
                            TextBox1.Text = ""
                            TextBox1.Select()
                            Exit Sub
                        End If

                        If dtCheckStockSubAssy.Rows(0).Item("qty") <= 0 Then
                            RJMessageBox.Show("Qty of this material is 0")
                            TextBox1.Text = ""
                            TextBox1.Select()
                            Exit Sub
                        End If

                        Dim sqlCheckKecukupanQty As String = "SELECT ( select CEILING(sum((mufg.usage * ssp.sub_sub_po_qty) + (mufg.usage * ssp.sub_sub_po_qty * ssp.YIELD_LOSE) / 100)) from sub_sub_po ssp, MATERIAL_USAGE_FINISH_GOODS mufg, main_po mp where ssp.sub_sub_po='" & TextBox11.Text & "' and ssp.main_po=mp.id and mufg.fg_part_number=mp.fg_pn and mufg.COMPONENT='" & dtCheckStockSubAssy.Rows(0).Item("fg") & "' ) total_kebutuhan, isnull( SUM ( sum_qty ), 0 ) total_input FROM stock_card WHERE sub_sub_po= '" & TextBox11.Text & "' AND status= 'Production Process' AND material= '" & dtCheckStockSubAssy.Rows(0).Item("fg") & "'"
                        Dim dtCheckKecukupanQty As DataTable = Database.GetData(sqlCheckKecukupanQty)

                        If IsDBNull(dtCheckKecukupanQty.Rows(0).Item("total_kebutuhan")) Then
                            RJMessageBox.Show("Material Doesn't Exists in this Finish Goods")
                            TextBox1.Text = ""
                            Exit Sub
                        End If

                        If dtCheckKecukupanQty.Rows(0).Item("total_kebutuhan") > dtCheckKecukupanQty.Rows(0).Item("total_input") Then

                            Dim sqlCheckInStockNewRecord As String = "select * from stock_card where sub_sub_po='" & TextBox11.Text & "' and status='Production Process' and department='" & globVar.department & "' and qrcode = '" & TextBox1.Text & "'"
                            Dim dtCheckInStockNewRecord As DataTable = Database.GetData(sqlCheckInStockNewRecord)

                            If dtCheckInStockNewRecord.Rows.Count > 0 Then

                                RJMessageBox.Show("Double Scan")
                                TextBox1.Text = ""
                                DGV_DOC()
                                DGV_DOS()

                            Else

                                Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,SUM_QTY,LEVEL,ID_LEVEL,QRCODE)
                                            VALUES ('" & dtCheckStockSubAssy.Rows(0).Item("fg") & "'," & dtCheckStockSubAssy.Rows(0).Item("qty").ToString.Replace(",", ".") & ",'" & dtCheckStockSubAssy.Rows(0).Item("INV_CTRL_DATE") & "','" & dtCheckStockSubAssy.Rows(0).Item("TRACEABILITY") & "','" & dtCheckStockSubAssy.Rows(0).Item("lot_no") & "','" & dtCheckStockSubAssy.Rows(0).Item("batch_no") & "','" & TextBox5.Text & "','" & TextBox11.Text & "','" & TextBox2.Text & "'," & dtCheckStockSubAssy.Rows(0).Item("qty").ToString.Replace(",", ".") & ",'" & ComboBox1.Text & "','" & TextBox10.Text & "','Production Process','" & globVar.department & "','YES'," & dtCheckStockSubAssy.Rows(0).Item("qty").ToString.Replace(",", ".") & ",'Fresh','" & dtCheckStockSubAssy.Rows(0).Item("CODE_STOCK_PROD_SUB_ASSY") & "','" & dtCheckStockSubAssy.Rows(0).Item("CODE_STOCK_PROD_SUB_ASSY") & "')"
                                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                cmdInsertInputStockDetail.ExecuteNonQuery()

                                Dim queryUpdateLineSA As String = "update STOCK_PROD_SUB_ASSY set line='" & ComboBox1.Text & "',qty=0 where CODE_STOCK_PROD_SUB_ASSY = '" & TextBox1.Text & "'"
                                Dim dtUpdateLineSA = New SqlCommand(queryUpdateLineSA, Database.koneksi)
                                dtUpdateLineSA.ExecuteNonQuery()

                            End If

                        Else

                            RJMessageBox.Show("Material " & dtCheckStockSubAssy.Rows(0).Item("fg") & " is full")
                            TextBox1.Text = ""

                        End If

                        TextBox1.Clear()
                        DGV_DOC()
                        DGV_DOS()

                    Catch ex As Exception

                        RJMessageBox.Show("Error Production - 5 =>" & ex.Message)

                    End Try

                ElseIf TextBox1.Text.StartsWith("OT") AndAlso TextBox1.Text.Length > 2 AndAlso IsNumeric(TextBox1.Text.Substring(2)) Then 'Others Material

                    Try
                        Dim CompExist As String = ""

                        Dim sqlCheckStockOTHERS As String = "select * from STOCK_PROD_OTHERS where CODE_STOCK_PROD_OTHERS='" & TextBox1.Text & "' and department='" & globVar.department & "'"
                        Dim dtCheckStockOTHERS As DataTable = Database.GetData(sqlCheckStockOTHERS)

                        If dtCheckStockOTHERS.Rows.Count = 0 Then
                            RJMessageBox.Show("Sorry this material don't exist in database")
                            TextBox1.Text = ""
                            TextBox1.Select()
                            Exit Sub
                        End If

                        If dtCheckStockOTHERS.Rows(0).Item("qty") <= 0 Then
                            RJMessageBox.Show("Qty of this material is 0")
                            TextBox1.Text = ""
                            TextBox1.Select()
                            Exit Sub
                        End If

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
                            RJMessageBox.Show("Cannot add OTHERS. Because (" & CompExist & ") Material Qty more than Qty Need")
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        For i = 0 To dtCheckStockOTHERS.Rows.Count - 1
                            Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,SUM_QTY,LEVEL,ID_LEVEL)
                                    VALUES ('" & dtCheckStockOTHERS.Rows(i).Item("part_number") & "'," & dtCheckStockOTHERS.Rows(i).Item("qty").ToString.Replace(",", ".") & ",'" & dtCheckStockOTHERS.Rows(i).Item("INV_CTRL_DATE") & "','" & dtCheckStockOTHERS.Rows(i).Item("TRACEABILITY") & "','" & dtCheckStockOTHERS.Rows(i).Item("lot_no") & "','" & dtCheckStockOTHERS.Rows(i).Item("batch_no") & "','" & TextBox5.Text & "','" & TextBox11.Text & "','" & TextBox2.Text & "',0,'" & ComboBox1.Text & "','" & TextBox10.Text & "','Production Request','" & globVar.department & "','NO'," & dtCheckStockOTHERS.Rows(i).Item("qty").ToString.Replace(",", ".") & ",'OT','" & dtCheckStockOTHERS.Rows(i).Item("CODE_STOCK_PROD_OTHERS") & "')"
                            Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                            cmdInsertInputStockDetail.ExecuteNonQuery()
                        Next

                        For i = 0 To dtCheckStockOTHERS.Rows.Count - 1
                            Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcessOTHERS @sub_sub_po='" & TextBox11.Text & "', @fg='" & TextBox2.Text & "',@line='" & ComboBox1.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtCheckStockOTHERS.Rows(i).Item("qty").ToString.Replace(",", ".") & ",@material='" & dtCheckStockOTHERS.Rows(i).Item("part_number") & "',@lot_material='" & dtCheckStockOTHERS.Rows(i).Item("lot_no") & "',@codeOTHERS='" & dtCheckStockOTHERS.Rows(i).Item("CODE_STOCK_PROD_OTHERS") & "',@po='" & TextBox5.Text & "',@sub_po='" & TextBox10.Text & "'"
                            Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                        Next

                        TextBox1.Text = ""
                        DGV_DOC()
                        DGV_DOS()

                    Catch ex As Exception
                        RJMessageBox.Show("Error Production - 4 =>" & ex.Message)
                    End Try

                ElseIf TextBox1.Text.StartsWith("OH") AndAlso TextBox1.Text.Length > 2 AndAlso IsNumeric(TextBox1.Text.Substring(2)) Then 'OnHold Material

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
                                RJMessageBox.Show("Cannot add ONHOLD. Because (" & CompExist & ") Material Qty more than Qty Need")
                                TextBox1.Clear()
                                Exit Sub
                            End If

                            For i = 0 To dtCheckStockONHOLD.Rows.Count - 1
                                Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,SUM_QTY,LEVEL,ID_LEVEL)
                                    VALUES ('" & dtCheckStockONHOLD.Rows(i).Item("part_number") & "'," & dtCheckStockONHOLD.Rows(i).Item("qty").ToString.Replace(",", ".") & ",'" & dtCheckStockONHOLD.Rows(i).Item("INV_CTRL_DATE") & "','" & dtCheckStockONHOLD.Rows(i).Item("TRACEABILITY") & "','" & dtCheckStockONHOLD.Rows(i).Item("lot_no") & "','" & dtCheckStockONHOLD.Rows(i).Item("batch_no") & "','" & TextBox5.Text & "','" & TextBox11.Text & "','" & TextBox2.Text & "',0,'" & ComboBox1.Text & "','" & TextBox10.Text & "','Production Request','" & globVar.department & "','NO'," & dtCheckStockONHOLD.Rows(i).Item("qty").ToString.Replace(",", ".") & ",'OH','" & dtCheckStockONHOLD.Rows(i).Item("CODE_STOCK_PROD_ONHOLD") & "')"
                                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                cmdInsertInputStockDetail.ExecuteNonQuery()

                                Dim sqlCheckSummaryFG As String = "select * from summary_fg where material = '" & dtCheckStockONHOLD.Rows(i).Item("part_number") & "' and sub_sub_po='" & TextBox11.Text & "' and fg='" & TextBox2.Text & "'"
                                Dim dtCheckSummaryFG As DataTable = Database.GetData(sqlCheckSummaryFG)

                                Dim dtInOnHold As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & TextBox11.Text & "' and material='" & dtCheckStockONHOLD.Rows(i).Item("part_number") & "' and status='Production Request' and [level]='OH'")

                                If dtCheckSummaryFG.Rows.Count > 0 Then
                                    Dim queryUpdateStockCardProdReq As String = "update summary_fg set onhold_in=onhold_in+" & dtInOnHold.Rows(0)(0).ToString.Replace(",", ".") & " where id=" & dtCheckSummaryFG.Rows(0).Item("id")
                                    Dim dtUpdateStockCardProdReq = New SqlCommand(queryUpdateStockCardProdReq, Database.koneksi)
                                    dtUpdateStockCardProdReq.ExecuteNonQuery()
                                Else
                                    Dim sqlInsertSummaryFG As String = "INSERT INTO summary_fg (sub_sub_po, FG ,material,onhold_in) VALUES ('" & TextBox11.Text & "','" & TextBox2.Text & "','" & dtCheckStockONHOLD.Rows(i).Item("part_number") & "'," & dtInOnHold.Rows(0)(0).ToString.Replace(",", ".") & ")"
                                    Dim cmdInsertSummaryFG = New SqlCommand(sqlInsertSummaryFG, Database.koneksi)
                                    cmdInsertSummaryFG.ExecuteNonQuery()
                                End If

                                Dim queryUpdateTotal As String = "update summary_fg set total_in=(select sum(fresh_in+balance_in+others_in+wip_in+onhold_in+sa_in) from summary_fg where material = '" & dtCheckStockONHOLD.Rows(i).Item("part_number") & "' and sub_sub_po='" & TextBox11.Text & "' and fg='" & TextBox2.Text & "') where material = '" & dtCheckStockONHOLD.Rows(i).Item("part_number") & "' and sub_sub_po='" & TextBox11.Text & "' and fg='" & TextBox2.Text & "'"
                                Dim dtUpdateTotal = New SqlCommand(queryUpdateTotal, Database.koneksi)
                                dtUpdateTotal.ExecuteNonQuery()
                            Next

                            For i = 0 To dtCheckStockONHOLD.Rows.Count - 1
                                Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcessONHOLD @sub_sub_po='" & TextBox11.Text & "', @fg='" & TextBox2.Text & "',@line='" & ComboBox1.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtCheckStockONHOLD.Rows(i).Item("qty").ToString.Replace(",", ".") & ",@material='" & dtCheckStockONHOLD.Rows(i).Item("part_number") & "',@lot_material='" & dtCheckStockONHOLD.Rows(i).Item("lot_no") & "',@codeONHOLD='" & dtCheckStockONHOLD.Rows(i).Item("code_stock_prod_onhold") & "',@po='" & TextBox5.Text & "',@sub_po='" & TextBox10.Text & "'"
                                Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                            Next

                            TextBox1.Text = ""
                            DGV_DOC()
                            DGV_DOS()
                        Else
                            RJMessageBox.Show("ONHOLD not Exist in DB")
                            TextBox1.Text = ""
                            DGV_DOC()
                            DGV_DOS()
                        End If
                    Catch ex As Exception
                        RJMessageBox.Show("Error Production - 3 =>" & ex.Message)
                    End Try

                ElseIf TextBox1.Text.StartsWith("WIP") AndAlso TextBox1.Text.Length > 3 AndAlso IsNumeric(TextBox1.Text.Substring(3)) Then 'WIP Material

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
                                RJMessageBox.Show("Cannot add WIP. Because (" & CompExist & ") Material Qty more than Qty Need")
                                TextBox1.Clear()
                                Exit Sub
                            End If

                            For i = 0 To dtCheckStockWIP.Rows.Count - 1
                                Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,SUM_QTY,LEVEL,ID_LEVEL)
                                    VALUES ('" & dtCheckStockWIP.Rows(i).Item("part_number") & "'," & dtCheckStockWIP.Rows(i).Item("qty").ToString.Replace(",", ".") & ",'" & dtCheckStockWIP.Rows(i).Item("INV_CTRL_DATE") & "','" & dtCheckStockWIP.Rows(i).Item("TRACEABILITY") & "','" & dtCheckStockWIP.Rows(i).Item("lot_no") & "','" & dtCheckStockWIP.Rows(i).Item("batch_no") & "','" & TextBox5.Text & "','" & TextBox11.Text & "','" & TextBox2.Text & "',0,'" & ComboBox1.Text & "','" & TextBox10.Text & "','Production Request','" & globVar.department & "','NO'," & dtCheckStockWIP.Rows(i).Item("qty").ToString.Replace(",", ".") & ",'WIP','" & dtCheckStockWIP.Rows(i).Item("CODE_STOCK_PROD_WIP") & "')"
                                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                cmdInsertInputStockDetail.ExecuteNonQuery()

                                Dim sqlCheckSummaryFG As String = "select * from summary_fg where material = '" & dtCheckStockWIP.Rows(i).Item("part_number") & "' and sub_sub_po='" & TextBox11.Text & "' and fg='" & TextBox2.Text & "'"
                                Dim dtCheckSummaryFG As DataTable = Database.GetData(sqlCheckSummaryFG)

                                Dim dtInWIP As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & TextBox11.Text & "' and material='" & dtCheckStockWIP.Rows(i).Item("part_number") & "' and status='Production Request' and [level]='WIP'")

                                If dtCheckSummaryFG.Rows.Count > 0 Then
                                    Dim queryUpdateStockCardProdReq As String = "update summary_fg set wip_in=wip_in+" & dtInWIP.Rows(0)(0).ToString.Replace(",", ".") & " where id=" & dtCheckSummaryFG.Rows(0).Item("id")
                                    Dim dtUpdateStockCardProdReq = New SqlCommand(queryUpdateStockCardProdReq, Database.koneksi)
                                    dtUpdateStockCardProdReq.ExecuteNonQuery()
                                Else
                                    Dim sqlInsertSummaryFG As String = "INSERT INTO summary_fg (sub_sub_po, FG ,material,wip_in) VALUES ('" & TextBox11.Text & "','" & TextBox2.Text & "','" & dtInWIP.Rows(i).Item("part_number") & "'," & dtInWIP.Rows(0)(0).ToString.Replace(",", ".") & ")"
                                    Dim cmdInsertSummaryFG = New SqlCommand(sqlInsertSummaryFG, Database.koneksi)
                                    cmdInsertSummaryFG.ExecuteNonQuery()
                                End If

                                Dim queryUpdateTotal As String = "update summary_fg set total_in=(select sum(fresh_in+balance_in+others_in+wip_in+onhold_in+sa_in) from summary_fg where material = '" & dtCheckStockWIP.Rows(i).Item("part_number") & "' and sub_sub_po='" & TextBox11.Text & "' and fg='" & TextBox2.Text & "') where material = '" & dtCheckStockWIP.Rows(i).Item("part_number") & "' and sub_sub_po='" & TextBox11.Text & "' and fg='" & TextBox2.Text & "'"
                                Dim dtUpdateTotal = New SqlCommand(queryUpdateTotal, Database.koneksi)
                                dtUpdateTotal.ExecuteNonQuery()
                            Next

                            For i = 0 To dtCheckStockWIP.Rows.Count - 1
                                Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcessWIP @sub_sub_po='" & TextBox11.Text & "', @fg='" & TextBox2.Text & "',@line='" & ComboBox1.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtCheckStockWIP.Rows(i).Item("qty").ToString.Replace(",", ".") & ",@material='" & dtCheckStockWIP.Rows(i).Item("part_number") & "',@lot_material='" & dtCheckStockWIP.Rows(i).Item("lot_no") & "',@codeWIP='" & dtCheckStockWIP.Rows(i).Item("code_stock_prod_wip") & "',@po='" & TextBox5.Text & "',@sub_po='" & TextBox10.Text & "'"
                                Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                            Next

                            TextBox1.Text = ""
                            DGV_DOC()
                            DGV_DOS()
                        Else
                            RJMessageBox.Show("WIP not Exist in DB")
                            TextBox1.Text = ""
                            DGV_DOC()
                            DGV_DOS()
                        End If

                    Catch ex As Exception
                        RJMessageBox.Show("Error Production - 2 =>" & ex.Message)
                    End Try

                Else 'error

                    RJMessageBox.Show("QRCode not valid.")
                    Play_Sound.Wrong()
                    TextBox1.Clear()
                    Exit Sub

                End If

            Else
                RJMessageBox.Show("Your Access cannot execute this action")
            End If
        End If
    End Sub

    Sub DGV_DOS()
        Try
            DataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView3.DataSource = Nothing
            DataGridView3.Rows.Clear()
            DataGridView3.Columns.Clear()
            Call Database.koneksi_database()

            Dim queryDOC As String = "SELECT
	                                        desc_comp Description,
	                                        component Component,
	                                        [USAGE],
                                            [usage] * " & Convert.ToInt32(TextBox6.Text) & " as [Qty Need],
	                                        isnull((select sum(qty) from stock_card where material=component and sub_sub_po='" & TextBox11.Text & "' and status='Production Process'),0) [Total IN],
                                            isnull((SELECT SUM(qty) FROM stock_card WHERE material=component AND sub_sub_po='" & TextBox11.Text & "' and status='Production Process'),0) - ISNULL((SELECT SUM(qty) FROM out_prod_reject WHERE part_number = component AND sub_sub_po = '" & TextBox11.Text & "'), 0) - isnull((select sum(qty) from stock_card where material=component and sub_sub_po='" & TextBox11.Text & "' and status ='Return To Mini Store'),0) - isnull((select sum(qty) from out_prod_defect where part_number=component and sub_sub_po='" & TextBox11.Text & "'),0) - isnull((select sum(qty) from stock_card where material=component and sub_sub_po='" & TextBox11.Text & "' and status ='Production Result'),0) AS [Current Qty],
	                                        isnull((select sum(qty) from out_prod_reject where part_number=component and sub_sub_po='" & TextBox11.Text & "'),0) [Total Reject],
                                            isnull((select sum(qty) from out_prod_defect where part_number=component and sub_sub_po='" & TextBox11.Text & "'),0) [Total Defect],
                                            isnull((select sum(qty) from stock_card where material=component and sub_sub_po='" & TextBox11.Text & "' and status ='Return To Mini Store'),0) [Total Return],
                                            isnull(cast((select count(*) from flow_ticket where sub_sub_po='" & TextBox11.Text & "' and department='" & globVar.department & "' and [done] = 1) AS VARCHAR(10) ),0) + ' / ' + isnull(cast((select count(*) from flow_ticket where sub_sub_po='" & TextBox11.Text & "' and department='" & globVar.department & "') AS VARCHAR(10) ),0) [FT Done],
                                            isnull((select sum(qty) from stock_card where material=component and sub_sub_po='" & TextBox11.Text & "' and status ='Production Result'),0) [Qty Done]
                                        FROM
	                                        prod_doc 
                                        WHERE
	                                        line = '" & ComboBox1.Text & "' 
	                                        AND sub_sub_po = '" & TextBox11.Text & "' 
	                                        AND department = '" & globVar.department & "'"

            Dim dtDOC As DataTable = Database.GetData(queryDOC)

            DataGridView3.DataSource = dtDOC

            Dim scan As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
            scan.Name = "Lot Scan"
            scan.HeaderText = "Lot Scan"
            DataGridView3.Columns.Insert(11, scan)

            DataGridView3.Columns(0).Width = 300
            DataGridView3.Columns(1).Width = 100
            DataGridView3.Columns(2).Width = 70
            DataGridView3.Columns(3).Width = 70
            DataGridView3.Columns(4).Width = 70
            DataGridView3.Columns(5).Width = 70
            DataGridView3.Columns(6).Width = 70
            DataGridView3.Columns(7).Width = 70
            DataGridView3.Columns(8).Width = 70
            DataGridView3.Columns(9).Width = 70
            DataGridView3.Columns(10).Width = 70
            DataGridView3.Columns(11).Width = 650
            DataGridView3.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            For rowDataSet As Integer = 0 To dtDOC.Rows.Count - 1
                Dim queryCheck As String = "select DISTINCT lot_no, inv_ctrl_date, traceability, batch_no, id_level, level, qrcode from stock_card where material='" & dtDOC.Rows(rowDataSet).Item("Component").ToString & "' and sub_sub_po='" & TextBox11.Text & "' and line ='" & ComboBox1.Text & "' and status='Production Process' ORDER BY lot_no, inv_ctrl_date, traceability, batch_no, id_level, level, qrcode"
                Dim dtCHECK As DataTable = Database.GetData(queryCheck)
                For i As Integer = 0 To dtCHECK.Rows.Count - 1
                    If dtCHECK.Rows(i).Item("level").ToString = "Fresh" Then
                        If i Mod 8 = 0 Then
                            DataGridView3.Rows(rowDataSet).Cells(11).Value += Environment.NewLine
                        End If

                        If InStr(dtCHECK.Rows(i).Item("qrcode").ToString, "SA") > 0 Then
                            DataGridView3.Rows(rowDataSet).Cells(11).Value += dtCHECK.Rows(i).Item("qrcode").ToString & ", "
                        Else
                            DataGridView3.Rows(rowDataSet).Cells(11).Value += dtCHECK.Rows(i).Item("lot_no").ToString & ", "
                        End If
                    Else
                        If i Mod 4 = 0 Then
                            DataGridView3.Rows(rowDataSet).Cells(11).Value += Environment.NewLine
                        End If
                        DataGridView3.Rows(rowDataSet).Cells(11).Value += dtCHECK.Rows(i).Item("id_level").ToString & ", "
                    End If
                Next
            Next

            For i As Integer = 0 To DataGridView3.RowCount - 1
                If DataGridView3.Rows(i).Index Mod 2 = 0 Then
                    DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next i
        Catch ex As Exception
            RJMessageBox.Show("Error Production - 7 =>" & ex.Message)
        End Try
    End Sub

    Sub DGV_DOC()
        Try
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.DataSource = Nothing
            DataGridView1.Rows.Clear()
            DataGridView1.Columns.Clear()
            Call Database.koneksi_database()

            Dim queryDOC As String = "SELECT
	                                        id [#],
                                            LEFT(qrcode, 15) [QRCode],
                                            material [Material],
                                            lot_no [Lot No],
                                            inv_ctrl_date [Inv Ctrl Date],
                                            traceability [Traceability],
                                            batch_no [Batch No],
                                            qty [Qty],
                                            remark [Remark],
                                            actual_qty [Actual Qty]
                                        FROM
	                                        stock_card 
                                        WHERE
	                                        sub_sub_po = '" & TextBox11.Text & "' 
	                                        AND department = '" & globVar.department & "'
                                            and status = 'Production Process'
                                        order by material"
            Dim dtDOC As DataTable = Database.GetData(queryDOC)

            DataGridView1.DataSource = dtDOC

            Dim deleteProductionRequest As DataGridViewButtonColumn = New DataGridViewButtonColumn
            deleteProductionRequest.Name = "delete"
            deleteProductionRequest.HeaderText = "Delete"
            deleteProductionRequest.Width = 50
            deleteProductionRequest.Text = "Delete"
            deleteProductionRequest.UseColumnTextForButtonValue = True
            DataGridView1.Columns.Insert(10, deleteProductionRequest)

            DataGridView1.Columns(0).Width = 100

            For i As Integer = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                    DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next i
        Catch ex As Exception
            RJMessageBox.Show("Error Production - 7 =>" & ex.Message)
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
            RJMessageBox.Show("Error Production - 8 =>" & ex.Message)
        End Try
    End Sub

    Private Sub Production_Load(sender As Object, e As EventArgs) Handles Me.Load
        tampilDataComboBoxLine()
        TextBox1.ReadOnly = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If globVar.view > 0 Then
            If ComboBox1.Text <> "" Then
                Try
                    Dim query As String = "select mp.po,mp.sub_po,mp.fg_pn,ssp.sub_sub_po,mfg.description,ssp.sub_sub_po_qty,mfg.spq,ssp.yield_lose
                        from sub_sub_po ssp,main_po mp,master_finish_goods mfg 
                        where ssp.status='Open' and mp.id=ssp.main_po and mfg.fg_part_number=mp.fg_pn and ssp.line='" & ComboBox1.Text & "' and mp.department='" & globVar.department & "'"
                    Dim dt As DataTable = Database.GetData(query)
                    If dt.Rows.Count > 0 Then
                        CheckBox1.Enabled = True
                        TextBox1.ReadOnly = False
                        TextBox1.Select()
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
                        DGV_DOS()

                        Button1.Enabled = True
                    Else
                        RJMessageBox.Show("This line no have any PO")
                        DGV_DOC()
                        DGV_DOP()
                        DGV_DOS()
                    End If
                Catch ex As Exception
                    RJMessageBox.Show("Error Production - 9 =>" & ex.Message)
                End Try
            Else
                RJMessageBox.Show("Please select line and fill the Flow Ticket first. ")
            End If
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If Not CheckBox1.Checked Then
            TextBox9.ReadOnly = False
            Button3.Enabled = False
            TextBox1.ReadOnly = True
            ComboBox2.SelectedIndex = -1
            ComboBox2.Enabled = False
            TextBox9.Clear()
        Else
            TextBox9.ReadOnly = True
            Button3.Enabled = False
            TextBox1.ReadOnly = False
            ComboBox2.SelectedIndex = -1
            ComboBox2.Enabled = False
            TextBox1.Clear()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If TextBox2.Text = "" Or ComboBox2.Text = "" Then
            RJMessageBox.Show("Part Number or Lot No Still blank. Please fill first")
            Exit Sub
        End If

        If globVar.add > 0 Then
            Try
                If DataGridView2.Rows.Count = 0 Then
                    RJMessageBox.Show("Cannot process When Detail Of Process blank. Please set Operator First.")
                    TextBox9.Clear()
                    Exit Sub
                End If

                If TextBox9.Text <> "" And ComboBox2.Text <> "" Then

                    Dim allmanualMaterial As String() = ComboBox2.Text.Split("|")
                    Dim lotManualMaterial As String() = allmanualMaterial(0).Split(":")
                    Dim icdManualMaterial As String() = allmanualMaterial(1).Split(":")
                    Dim traceManualMaterial As String() = allmanualMaterial(2).Split(":")
                    Dim batchManualMaterial As String() = allmanualMaterial(3).Split(":")

                    Dim sqlCheckInStock As String = "select in_material.* from sub_sub_po sp, stock_card in_material where in_material.SUB_SUB_PO = sp.sub_sub_po 
                        and sp.status='Open' 
                        and in_material.line='" & ComboBox1.Text & "' 
                        and in_material.material = '" & TextBox9.Text & "' 
                        and in_material.lot_no='" & lotManualMaterial(1) & "' 
                        and in_material.inv_ctrl_date='" & icdManualMaterial(1) & "' 
                        and in_material.traceability='" & traceManualMaterial(1) & "' 
                        and in_material.batch_no='" & batchManualMaterial(1) & "' 
                        and sp.sub_sub_po='" & TextBox11.Text & "' 
                        and in_material.status='Production Request' 
                        and department='" & globVar.department & "'"
                    Dim dtCheckInStock As DataTable = Database.GetData(sqlCheckInStock)
                    If dtCheckInStock.Rows.Count > 0 Then
                        Dim sqlCheckInStockNewRecord As String = "select * from stock_card where line='" & ComboBox1.Text & "' 
                            and material = '" & TextBox9.Text & "' 
                            and lot_no='" & lotManualMaterial(1) & "' 
                            and inv_ctrl_date='" & icdManualMaterial(1) & "' 
                            and traceability='" & traceManualMaterial(1) & "' 
                            and batch_no='" & batchManualMaterial(1) & "' 
                            and sub_sub_po='" & TextBox11.Text & "' 
                            and status='Production Process' 
                            and department='" & globVar.department & "'"
                        Dim dtCheckInStockNewRecord As DataTable = Database.GetData(sqlCheckInStockNewRecord)
                        If dtCheckInStockNewRecord.Rows.Count > 0 Then
                            RJMessageBox.Show("Double Scan")
                            TextBox1.Text = ""
                            DGV_DOC()
                            DGV_DOS()
                        Else
                            Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess_NEW @sub_sub_po='" & TextBox11.Text & "', @fg='" & TextBox2.Text & "',@line='" & ComboBox1.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtCheckInStock.Rows(0).Item("actual_qty") & ",@material='" & TextBox9.Text & "',@lot_material='" & lotManualMaterial(1) & "',@in_icd='" & icdManualMaterial(1) & "',@in_trace='" & traceManualMaterial(1) & "',@in_batch='" & batchManualMaterial(1) & "'"
                            Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)

                            TextBox9.Text = ""
                            ComboBox2.SelectedIndex = -1
                            DGV_DOC()
                            DGV_DOS()

                            For i = 0 To DataGridView1.Rows.Count - 1
                                If DataGridView1.Rows(i).Cells(1).Value = TextBox9.Text Then
                                    DataGridView1.Rows(i).Cells(3).Selected = True
                                End If
                            Next
                        End If

                    Else
                        RJMessageBox.Show("Sorry this material not for this line.")
                        TextBox9.Text = ""
                        ComboBox2.SelectedIndex = -1
                        TextBox9.Select()
                    End If
                Else
                    TextBox9.Clear()
                    ComboBox2.SelectedIndex = -1
                    TextBox9.Select()
                    RJMessageBox.Show("Sorry Comp and Lot No cannot be blank")
                End If
            Catch ex As Exception
                RJMessageBox.Show("Error Production - 10 =>" & ex.Message)
            End Try
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Sub reset()
        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()
        DataGridView1.Rows.Clear()

        DataGridView2.DataSource = Nothing
        DataGridView2.Columns.Clear()
        DataGridView2.Rows.Clear()

        DataGridView3.DataSource = Nothing
        DataGridView3.Columns.Clear()
        DataGridView3.Rows.Clear()

        ComboBox1.SelectedIndex = -1
        TextBox1.Clear()
        TextBox9.Clear()
        ComboBox2.SelectedIndex = -1
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

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox1.ReadOnly = True
        CheckBox1.Enabled = False
        Button1.Enabled = False
    End Sub

    Sub tampilDataComboBoxDataManualMaterial(material As String)
        Call Database.koneksi_database()
        Dim dtMaterial As DataTable = Database.GetData("select lot_no, inv_ctrl_date, traceability, batch_no, qty, qrcode from stock_card where department='" & globVar.department & "' and material='" & material & "' and actual_qty > 0 and sub_sub_po='" & TextBox11.Text & "' and status='Production Request' order by datetime_insert")

        dtMaterial.Columns.Add("DisplayMember", GetType(String))

        For Each row As DataRow In dtMaterial.Rows
            If row("qrcode").ToString() = "Manual Input" Then
                row("DisplayMember") = "Lot No:" & row("lot_no").ToString() & "|ICD:" & row("inv_ctrl_date").ToString() & "|Trace:" & row("traceability").ToString() & "|Batch:" & row("batch_no").ToString() & "|Qty:" & row("qty").ToString() & "|Manual"
            Else
                row("DisplayMember") = "Lot No:" & row("lot_no").ToString() & "|ICD:" & row("inv_ctrl_date").ToString() & "|Trace:" & row("traceability").ToString() & "|Batch:" & row("batch_no").ToString() & "|Qty:" & row("qty").ToString()
            End If
        Next

        ComboBox2.DataSource = dtMaterial
        ComboBox2.DisplayMember = "DisplayMember"
        ComboBox2.ValueMember = "DisplayMember"
        ComboBox2.SelectedIndex = -1
    End Sub

    Private Sub TextBox9_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox9.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            tampilDataComboBoxDataManualMaterial(TextBox9.Text)
            Button3.Enabled = True
            ComboBox2.Enabled = True
        End If
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        If TextBox9.Text.StartsWith("0") AndAlso TextBox9.Text.Length > 1 Then
            TextBox9.Text = TextBox9.Text.TrimStart("0"c)
            TextBox9.SelectionStart = TextBox9.Text.Length
        End If

        Button3.Enabled = False
        ComboBox2.Enabled = False
    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox9.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub DataGridView3_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If e.ColumnIndex = -1 Then
            Exit Sub
        End If

        If DataGridView1.Columns(e.ColumnIndex).Name = "delete" Then

            Dim result = RJMessageBox.Show("Are you sure for delete this material?.", "Are You Sure?", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then

                If DataGridView1.Rows(e.RowIndex).Cells("Qty").Value <> DataGridView1.Rows(e.RowIndex).Cells("Actual Qty").Value Then
                    RJMessageBox.Show("Cannot delete this material because this material already in use")
                    Exit Sub
                End If

                If DataGridView1.Rows(e.RowIndex).Cells("QRCode").Value = "Manual Input" Then
                    Dim queryUpdateSC As String = "update 
                                                        stock_card 
                                                    set 
                                                        actual_qty=qty 
                                                    where 
                                                        status='Production Request' 
                                                        and material = (select material from stock_card where id=" & DataGridView1.Rows(e.RowIndex).Cells("#").Value & ")
                                                        and lot_no = (select lot_no from stock_card where id=" & DataGridView1.Rows(e.RowIndex).Cells("#").Value & ")
                                                        and traceability = (select traceability from stock_card where id=" & DataGridView1.Rows(e.RowIndex).Cells("#").Value & ")
                                                        and inv_ctrl_date = (select inv_ctrl_date from stock_card where id=" & DataGridView1.Rows(e.RowIndex).Cells("#").Value & ")
                                                        and batch_no = (select batch_no from stock_card where id=" & DataGridView1.Rows(e.RowIndex).Cells("#").Value & ")"
                    Dim dtUpdateSC = New SqlCommand(queryUpdateSC, Database.koneksi)
                    If dtUpdateSC.ExecuteNonQuery() Then

                        Dim sql As String = "delete from stock_card where id=" & DataGridView1.Rows(e.RowIndex).Cells("#").Value
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        If cmd.ExecuteNonQuery() Then
                            RJMessageBox.Show("Delete Success")
                            DGV_DOC()
                            DGV_DOS()

                        End If
                    Else

                        RJMessageBox.Show("Delete Failed")

                    End If
                Else
                    Dim queryUpdateSC As String = "update stock_card set actual_qty=qty where status='Production Request' and qrcode = (select qrcode from stock_card where id=" & DataGridView1.Rows(e.RowIndex).Cells("#").Value & ")"
                    Dim dtUpdateSC = New SqlCommand(queryUpdateSC, Database.koneksi)
                    If dtUpdateSC.ExecuteNonQuery() Then

                        Dim sql As String = "delete from stock_card where id=" & DataGridView1.Rows(e.RowIndex).Cells("#").Value
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        If cmd.ExecuteNonQuery() Then
                            RJMessageBox.Show("Delete Success")
                            DGV_DOC()
                            DGV_DOS()

                        End If
                    Else

                        RJMessageBox.Show("Delete Failed")

                    End If

                End If

            End If

        End If

    End Sub

    Private Sub dgReject_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If DataGridView1.Columns(e.ColumnIndex).Name = "Qty" Then
            e.CellStyle.BackColor = Color.Green
            e.CellStyle.ForeColor = Color.White
        End If

        If DataGridView1.Columns(e.ColumnIndex).Name = "Remark" Then
            e.CellStyle.BackColor = Color.Green
            e.CellStyle.ForeColor = Color.White
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

        If e.RowIndex = -1 Then

            Exit Sub

        End If

        If e.ColumnIndex = -1 Then

            Exit Sub

        End If

        If globVar.update = 0 Then

            RJMessageBox.Show("Your Access cannot execute this action")
            Exit Sub

        End If

        If DataGridView1.Columns(e.ColumnIndex).Name = "Qty" Then

            Dim sqlcheck As String = "select * from stock_card where ID=" & DataGridView1.Rows(e.RowIndex).Cells("#").Value
            Dim dtCheck As DataTable = Database.GetData(sqlcheck)

            If dtCheck.Rows(0).Item("qty") <> DataGridView1.Rows(e.RowIndex).Cells("Qty").Value Then

                If DataGridView1.Rows(e.RowIndex).Cells("Remark").Value Is DBNull.Value OrElse DataGridView1.Rows(e.RowIndex).Cells("Remark").Value.ToString() = "" Then

                    RJMessageBox.Show("Please fill the remark")
                    Exit Sub

                End If

                If DataGridView1.Rows(e.RowIndex).Cells("Actual Qty").Value = 0 And dtCheck.Rows(0).Item("qty") > DataGridView1.Rows(e.RowIndex).Cells("Qty").Value Then

                    RJMessageBox.Show("cannot change less than qty when actual qty = 0")
                    DGV_DOC()
                    DGV_DOS()

                Else

                    If dtCheck.Rows(0).Item("qty") > DataGridView1.Rows(e.RowIndex).Cells("Qty").Value Then

                        Dim SumQty = dtCheck.Rows(0).Item("qty") - DataGridView1.Rows(e.RowIndex).Cells("Qty").Value

                        If dtCheck.Rows(0).Item("actual_qty") - SumQty < 0 Then

                            RJMessageBox.Show("cannot change the qty because the addition makes the actual qty smaller than 0")
                            DGV_DOC()
                            DGV_DOS()
                            Exit Sub

                        End If

                        Dim queryUpdateactualQty As String = "update stock_card set qty = qty - " & SumQty & ", actual_qty = actual_qty - " & SumQty & ", remark='" & DataGridView1.Rows(e.RowIndex).Cells("Remark").Value & "' where id=" & DataGridView1.Rows(e.RowIndex).Cells("#").Value
                        Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                        If dtUpdateactualQty.ExecuteNonQuery() Then

                            RJMessageBox.Show("Update Qty Success")
                            DGV_DOC()
                            DGV_DOS()

                        End If

                    Else

                        Dim SumQty = DataGridView1.Rows(e.RowIndex).Cells("Qty").Value - dtCheck.Rows(0).Item("qty")

                        Dim queryUpdateactualQty As String = "update stock_card set qty = qty + " & SumQty & ", actual_qty=actual_qty + " & SumQty & ", remark='" & DataGridView1.Rows(e.RowIndex).Cells("Remark").Value & "' where id=" & DataGridView1.Rows(e.RowIndex).Cells("#").Value
                        Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                        If dtUpdateactualQty.ExecuteNonQuery() Then

                            RJMessageBox.Show("Update Qty Success")
                            DGV_DOC()
                            DGV_DOS()

                        End If

                    End If

                End If

            Else

                RJMessageBox.Show("Old and New qty is same")

            End If

        End If

        If DataGridView1.Columns(e.ColumnIndex).Name = "Remark" Then

            Dim sqlcheck As String = "select * from stock_card where ID=" & DataGridView1.Rows(e.RowIndex).Cells("#").Value
            Dim dtCheck As DataTable = Database.GetData(sqlcheck)

            If dtCheck.Rows(0).Item("qty") <> DataGridView1.Rows(e.RowIndex).Cells("Qty").Value Then

                If DataGridView1.Rows(e.RowIndex).Cells("Remark").Value Is DBNull.Value OrElse DataGridView1.Rows(e.RowIndex).Cells("Remark").Value.ToString() = "" Then

                    RJMessageBox.Show("Please fill the remark")
                    Exit Sub

                End If

                If DataGridView1.Rows(e.RowIndex).Cells("Actual Qty").Value = 0 And dtCheck.Rows(0).Item("qty") > DataGridView1.Rows(e.RowIndex).Cells("Qty").Value Then

                    RJMessageBox.Show("cannot change less than qty when actual qty = 0")
                    DGV_DOC()
                    DGV_DOS()

                Else

                    If dtCheck.Rows(0).Item("qty") > DataGridView1.Rows(e.RowIndex).Cells("Qty").Value Then

                        Dim SumQty = dtCheck.Rows(0).Item("qty") - DataGridView1.Rows(e.RowIndex).Cells("Qty").Value

                        If dtCheck.Rows(0).Item("actual_qty") - SumQty < 0 Then

                            RJMessageBox.Show("cannot change the qty because the addition makes the actual qty smaller than 0")
                            DGV_DOC()
                            DGV_DOS()
                            Exit Sub

                        End If

                        Dim queryUpdateactualQty As String = "update stock_card set qty = qty - " & SumQty & ", actual_qty = actual_qty - " & SumQty & ", remark='" & DataGridView1.Rows(e.RowIndex).Cells("Remark").Value & "' where id=" & DataGridView1.Rows(e.RowIndex).Cells("#").Value
                        Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                        If dtUpdateactualQty.ExecuteNonQuery() Then

                            RJMessageBox.Show("Update Qty Success")
                            DGV_DOC()
                            DGV_DOS()

                        End If

                    Else

                        Dim SumQty = DataGridView1.Rows(e.RowIndex).Cells("Qty").Value - dtCheck.Rows(0).Item("qty")

                        Dim queryUpdateactualQty As String = "update stock_card set qty = qty + " & SumQty & ", actual_qty=actual_qty + " & SumQty & ", remark='" & DataGridView1.Rows(e.RowIndex).Cells("Remark").Value & "' where id=" & DataGridView1.Rows(e.RowIndex).Cells("#").Value
                        Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                        If dtUpdateactualQty.ExecuteNonQuery() Then

                            RJMessageBox.Show("Update Qty Success")
                            DGV_DOC()
                            DGV_DOS()

                        End If

                    End If

                End If

            Else

                RJMessageBox.Show("Old and New qty is same")

            End If

        End If
    End Sub
End Class