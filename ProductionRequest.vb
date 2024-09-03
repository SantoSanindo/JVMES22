Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class ProductionRequest
    Public Shared menu As String = "Production Request"

    Sub DGV_MaterialNeed()
        Try
            DataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView3.DataSource = Nothing
            DataGridView3.Rows.Clear()
            DataGridView3.Columns.Clear()

            Dim queryMasterFinishGoods As String = "select sp.Sub_Sub_PO [Sub Sub PO],mp.fg_pn [FG Part Number],mufg.component [Comp],mufg.description [Desc],mufg.usage [Usage],sp.sub_sub_po_qty [Qty],ceiling(( mufg.usage * sp.sub_sub_po_qty ) + ( mufg.usage * sp.sub_sub_po_qty * sp.yield_lose / 100)) AS [Total Need],mp.po,mp.sub_po,isnull((select sum(qty) from STOCK_CARD where sub_sub_po=sp.SUB_SUB_PO and material=mufg.component and status='Production Request'),0) [Total Production Request]
                from sub_sub_po sp,main_po mp,material_usage_finish_goods mufg 
                where sp.main_po= mp.id AND mufg.fg_part_number= mp.fg_pn AND sp.status= 'Open' and sp.line = '" & ComboBox1.Text & "' and mp.department='" & globVar.department & "' order by sp.sub_sub_po"
            Dim dtMaterialNeed As DataTable = Database.GetData(queryMasterFinishGoods)

            If dtMaterialNeed.Rows.Count > 0 Then
                TextBox1.Enabled = True
                TextBox1.Select()
                DataGridView3.DataSource = dtMaterialNeed

                DataGridView3.Columns(0).Width = 150
                DataGridView3.Columns(1).Width = 200
                DataGridView3.Columns(2).Width = 150
                DataGridView3.Columns(3).Width = 400
                DataGridView3.Columns(4).Width = 100
                DataGridView3.Columns(5).Width = 150
                DataGridView3.Columns(6).Width = 150
                DataGridView3.Columns(7).Visible = False
                DataGridView3.Columns(8).Visible = False

                For i As Integer = 0 To DataGridView3.RowCount - 1
                    If DataGridView3.Rows(i).Index Mod 2 = 0 Then
                        DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                    Else
                        DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                    End If
                Next i

                SubSubPO.Text = dtMaterialNeed.Rows(0).Item("Sub Sub PO").ToString
                PO.Text = dtMaterialNeed.Rows(0).Item("PO").ToString
                SubPO.Text = dtMaterialNeed.Rows(0).Item("Sub_PO").ToString
                TextBox3.Text = dtMaterialNeed.Rows(0).Item("FG Part Number").ToString
            Else
                RJMessageBox.Show("Sorry this line not set for Production")
            End If
        Catch ex As Exception
            RJMessageBox.Show("Error Production Request - 1 =>" & ex.Message)
        End Try
    End Sub

    Sub notification(material As String, qty As String)
        NotifyIcon1.Icon = SystemIcons.Information
        NotifyIcon1.Visible = True

        NotifyIcon1.BalloonTipTitle = "Infomation"
        NotifyIcon1.BalloonTipText = "Success input " & material & " with Qty " & qty
        NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info
        NotifyIcon1.ShowBalloonTip(1000)

        Timer1.Interval = 3000
        Timer1.Start()
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
        Dim QrcodeValid As Boolean
        Try
            Dim ds As New DataSet
            Dim Found As Boolean = False
            Dim StringToSearch As String = ""
            Dim CurrentRowIndex As Integer = 0

            If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) And TextBox1.Text <> "" And ComboBox1.Text <> "" Then
                If globVar.add > 0 Then

                    If TextBox1.Text.StartsWith("B") AndAlso TextBox1.Text.Length > 1 AndAlso IsNumeric(TextBox1.Text.Substring(1)) Then

                        Dim queryCheck As String = "select * from stock_card where id_level='" & TextBox1.Text & "' and status='Receive From Production' and department='" & globVar.department & "'"
                        Dim dttable As DataTable = Database.GetData(queryCheck)

                        If dttable.Rows.Count = 0 Then
                            RJMessageBox.Show("Material doesn't exist in Database")
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        If dttable.Rows(0).Item("actual_qty") <= 0 Then
                            RJMessageBox.Show("This Material is 0")
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        globVar.QRCode_PN = dttable.Rows(0).Item("material")
                        globVar.QRCode_lot = dttable.Rows(0).Item("lot_no")

                        If DataGridView3.Rows.Count > 0 Then
                            For Each gRow As DataGridViewRow In DataGridView3.Rows
                                StringToSearch = gRow.Cells("Comp").Value.ToString.Trim.ToLower
                                If InStr(1, StringToSearch, LCase(Trim(globVar.QRCode_PN)), vbTextCompare) = 1 Then
                                    Dim myCurrentCell As DataGridViewCell = gRow.Cells("Comp")
                                    DataGridView3.CurrentCell = myCurrentCell
                                    CurrentRowIndex = DataGridView3.CurrentRow.Index
                                    Found = True
                                End If
                                If Found Then Exit For
                            Next
                        End If

                        If Found = False Then
                            RJMessageBox.Show("Production no need for this material.")
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        Dim sqlCheckInProdFreshMaterial As String = "SELECT * FROM stock_card WHERE qrcode='" & TextBox1.Text & "' and status='Production Request' and department='" & globVar.department & "' and sub_sub_po='" & SubSubPO.Text & "'"
                        Dim dtCheckInProdFreshMaterial As DataTable = Database.GetData(sqlCheckInProdFreshMaterial)
                        If dtCheckInProdFreshMaterial.Rows.Count > 0 Then
                            RJMessageBox.Show("Sorry this material already in database production")
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        Dim sqlCheckSumQtyProdcution As String = "SELECT isnull(sum(sum_qty),0) qty FROM stock_card WHERE sub_sub_po = '" & SubSubPO.Text & "' and material='" & dttable.Rows(0).Item("material") & "' AND LINE='" & ComboBox1.Text & "' and department='" & globVar.department & "' and status='Production Request'"
                        Dim dtCheckSumQtyProdcution As DataTable = Database.GetData(sqlCheckSumQtyProdcution)
                        If dtCheckSumQtyProdcution.Rows(0).Item("qty") >= DataGridView3.Rows(CurrentRowIndex).Cells("Total Need").Value Then
                            RJMessageBox.Show("Cannot add component because Qty more than Qty Need")
                            TextBox1.Text = ""
                            DGV_InProductionMaterial()
                        Else
                            Try


                                Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,QRCODE,MTS_NO,SUM_QTY,LEVEL,ID_LEVEL,insert_who)
                                                VALUES ('" & dttable.Rows(0).Item("material") & "'," & dttable.Rows(0).Item("qty") & ",'" & dttable.Rows(0).Item("inv_ctrl_date") & "','" & dttable.Rows(0).Item("traceability") & "','" & dttable.Rows(0).Item("lot_no") & "','" & dttable.Rows(0).Item("batch_no") & "','" & PO.Text & "','" & SubSubPO.Text & "'," & DataGridView3.Rows(CurrentRowIndex).Cells("FG Part Number").Value & "," & dttable.Rows(0).Item("Qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "','" & dttable.Rows(0).Item("standard_pack") & "','" & dttable.Rows(0).Item("qrcode") & "','" & dttable.Rows(0).Item("mts_no") & "'," & dttable.Rows(0).Item("qty") & ",'Fresh','" & dttable.Rows(0).Item("material") & "','" & globVar.username & "')"
                                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                If cmdInsertInputStockDetail.ExecuteNonQuery() Then

                                    notification(TextBox1.Text, dttable.Rows(0).Item("qty"))

                                    DGV_InProductionMaterial()

                                    Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 WHERE qrcode='" & TextBox1.Text & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Receive From Production'"
                                    Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                    cmdUpdate.ExecuteNonQuery()
                                    TextBox1.Clear()

                                End If
                            Catch ex As Exception

                                RJMessageBox.Show("Error Insert" & ex.Message)

                            End Try
                        End If

                        DGV_MaterialNeed()

                    ElseIf TextBox1.Text.StartsWith("SM") AndAlso TextBox1.Text.Length > 2 AndAlso IsNumeric(TextBox1.Text.Substring(2)) Then

                        Dim sqlCheckStockSM = "SELECT * FROM split_label WHERE inner_label = '" & TextBox1.Text & "'"
                        Dim dtCheckStockSM As DataTable = Database.GetData(sqlCheckStockSM)

                        globVar.QRCode_PN = dtCheckStockSM.Rows(0).Item("outer_pn")
                        globVar.QRCode_lot = dtCheckStockSM.Rows(0).Item("outer_lot")
                        globVar.QRCode_Inv = dtCheckStockSM.Rows(0).Item("outer_icd")
                        globVar.QRCode_Traceability = dtCheckStockSM.Rows(0).Item("outer_traceability")
                        globVar.QRCode_Batch = dtCheckStockSM.Rows(0).Item("outer_batch")

                        If DataGridView3.Rows.Count > 0 Then
                            For Each gRow As DataGridViewRow In DataGridView3.Rows
                                StringToSearch = gRow.Cells("Comp").Value.ToString.Trim.ToLower
                                If InStr(1, StringToSearch, LCase(Trim(globVar.QRCode_PN)), vbTextCompare) = 1 Then
                                    Dim myCurrentCell As DataGridViewCell = gRow.Cells("Comp")
                                    DataGridView3.CurrentCell = myCurrentCell
                                    CurrentRowIndex = DataGridView3.CurrentRow.Index
                                    Found = True
                                End If
                                If Found Then Exit For
                            Next
                        End If

                        If Found = False Then
                            RJMessageBox.Show("Production no need for this material.")
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        If dtCheckStockSM.Rows.Count = 0 Then
                            RJMessageBox.Show("Material doesn't exist in Database")
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        If dtCheckStockSM.Rows(0).Item("inner_qty") <= 0 Then
                            RJMessageBox.Show("Qty this material split is 0")
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        Dim sqlCheckInProdSM As String = "SELECT * FROM stock_card WHERE status='Production Request' and department='" & globVar.department & "' and qrcode='" & TextBox1.Text & "'"
                        Dim dtCheckInProdSM As DataTable = Database.GetData(sqlCheckInProdSM)

                        If dtCheckInProdSM.Rows.Count > 0 Then
                            RJMessageBox.Show("Sorry this material already in database production")
                            TextBox1.Text = ""
                            Exit Sub
                        End If

                        Dim sqlCheckSumQtyProdcution As String = "SELECT isnull(sum(sum_qty),0) qty FROM stock_card WHERE sub_sub_po = '" & SubSubPO.Text & "' and material='" & globVar.QRCode_PN & "' AND LINE='" & ComboBox1.Text & "' and department='" & globVar.department & "' and status='Production Request'"
                        Dim dtCheckSumQtyProdcution As DataTable = Database.GetData(sqlCheckSumQtyProdcution)
                        If dtCheckSumQtyProdcution.Rows(0).Item("qty") >= DataGridView3.Rows(CurrentRowIndex).Cells("Total Need").Value Then
                            RJMessageBox.Show("Cannot add component because Qty more than Qty Need")
                            TextBox1.Text = ""
                            DGV_InProductionMaterial()
                        Else
                            Try
                                Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card ([MATERIAL], [QTY], [INV_CTRL_DATE], [TRACEABILITY], [LOT_NO], [BATCH_NO], [PO], [SUB_SUB_PO], [Finish_Goods_PN], [ACTUAL_QTY],[LINE],[SUB_PO],[STATUS],[DEPARTMENT],[STANDARD_PACK],[QRCODE],[SUM_QTY],[LEVEL],[ID_LEVEL],[insert_who])
                                                VALUES ('" & globVar.QRCode_PN & "'," & dtCheckStockSM.Rows(0).Item("inner_qty") & ",'" & globVar.QRCode_Inv & "','" & globVar.QRCode_Traceability & "','" & globVar.QRCode_lot & "','" & globVar.QRCode_Batch & "','" & PO.Text & "','" & SubSubPO.Text & "','" & DataGridView3.Rows(CurrentRowIndex).Cells("FG Part Number").Value & "'," & dtCheckStockSM.Rows(0).Item("inner_qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "','NO','" & TextBox1.Text & "'," & dtCheckStockSM.Rows(0).Item("inner_qty") & ",'Fresh','" & globVar.QRCode_PN & "','" & globVar.username & "')"
                                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                If cmdInsertInputStockDetail.ExecuteNonQuery() Then

                                    notification(TextBox1.Text, dtCheckStockSM.Rows(0).Item("inner_qty"))

                                    DGV_InProductionMaterial()

                                    Dim SqlUpdate As String = "UPDATE split_label SET inner_qty=0 WHERE inner_label='" & TextBox1.Text & "'"
                                    Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                    cmdUpdate.ExecuteNonQuery()
                                    TextBox1.Clear()

                                End If
                            Catch ex As Exception
                                RJMessageBox.Show("Error Insert" & ex.Message)
                            End Try
                        End If

                        DGV_MaterialNeed()

                    ElseIf TextBox1.Text.StartsWith("NQ") AndAlso TextBox1.Text.Length > 2 AndAlso IsNumeric(TextBox1.Text.Substring(2)) Then

                        Dim sqlCheckStockNQ = "SELECT * FROM new_label WHERE qrcode = '" & TextBox1.Text & "'"
                        Dim dtCheckStockNQ As DataTable = Database.GetData(sqlCheckStockNQ)

                        If IsDBNull(dtCheckStockNQ.Rows(0).Item("material")) OrElse String.IsNullOrEmpty(dtCheckStockNQ.Rows(0).Item("material").ToString()) Then
                            RJMessageBox.Show("New Label doesn't use")
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        globVar.QRCode_PN = dtCheckStockNQ.Rows(0).Item("material")
                        globVar.QRCode_lot = dtCheckStockNQ.Rows(0).Item("lot_no")
                        globVar.QRCode_Inv = dtCheckStockNQ.Rows(0).Item("inv_ctrl_date")
                        globVar.QRCode_Traceability = dtCheckStockNQ.Rows(0).Item("traceability")
                        globVar.QRCode_Batch = dtCheckStockNQ.Rows(0).Item("batch_no")

                        If DataGridView3.Rows.Count > 0 Then
                            For Each gRow As DataGridViewRow In DataGridView3.Rows
                                StringToSearch = gRow.Cells("Comp").Value.ToString.Trim.ToLower
                                If InStr(1, StringToSearch, LCase(Trim(globVar.QRCode_PN)), vbTextCompare) = 1 Then
                                    Dim myCurrentCell As DataGridViewCell = gRow.Cells("Comp")
                                    DataGridView3.CurrentCell = myCurrentCell
                                    CurrentRowIndex = DataGridView3.CurrentRow.Index
                                    Found = True
                                End If
                                If Found Then Exit For
                            Next
                        End If

                        If Found = False Then
                            RJMessageBox.Show("Production no need for this material.")
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        If dtCheckStockNQ.Rows.Count = 0 Then
                            RJMessageBox.Show("Material doesn't exist in Database")
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        If dtCheckStockNQ.Rows(0).Item("qty") <= 0 Then
                            RJMessageBox.Show("Qty this material split is 0")
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        Dim sqlCheckStockMinistore = "SELECT * FROM stock_card WHERE material = '" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' and batch_no='" & globVar.QRCode_Batch & "' and inv_ctrl_date='" & globVar.QRCode_Inv & "' and traceability='" & globVar.QRCode_Traceability & "' and department='" & globVar.department & "' and status='Receive From Main Store' and [save]=1 and actual_qty > 0 and qrcode_new='" & TextBox1.Text & "'"
                        Dim dtCheckStockMinistore As DataTable = Database.GetData(sqlCheckStockMinistore)

                        If dtCheckStockMinistore.Rows.Count = 0 Then
                            RJMessageBox.Show("Material doesn't exist in Database")
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        If dtCheckStockMinistore.Rows.Count > 1 Then
                            RJMessageBox.Show("The scanned material has more than 1 material. " & globVar.QRCode_PN & " - " & globVar.QRCode_lot)
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        Dim sqlCheckInProdSM As String = "SELECT * FROM stock_card WHERE status='Production Request' and department='" & globVar.department & "' and qrcode_new='" & TextBox1.Text & "'"
                        Dim dtCheckInProdSM As DataTable = Database.GetData(sqlCheckInProdSM)

                        If dtCheckInProdSM.Rows.Count > 0 Then
                            RJMessageBox.Show("Sorry this material already in database production")
                            TextBox1.Text = ""
                            Exit Sub
                        End If

                        Dim sqlCheckSumQtyProdcution As String = "SELECT isnull(sum(sum_qty),0) qty FROM stock_card WHERE sub_sub_po = '" & SubSubPO.Text & "' and material='" & globVar.QRCode_PN & "' AND LINE='" & ComboBox1.Text & "' and department='" & globVar.department & "' and status='Production Request'"
                        Dim dtCheckSumQtyProdcution As DataTable = Database.GetData(sqlCheckSumQtyProdcution)
                        If dtCheckSumQtyProdcution.Rows(0).Item("qty") >= DataGridView3.Rows(CurrentRowIndex).Cells("Total Need").Value Then
                            RJMessageBox.Show("Cannot add component because Qty more than Qty Need")
                            TextBox1.Text = ""
                            DGV_InProductionMaterial()
                        Else
                            Try
                                Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (mts_no,[MATERIAL], [QTY], [INV_CTRL_DATE], [TRACEABILITY], [LOT_NO], [BATCH_NO], [PO], [SUB_SUB_PO], [Finish_Goods_PN], [ACTUAL_QTY],[LINE],[SUB_PO],[STATUS],[DEPARTMENT],[STANDARD_PACK],[QRCODE],[SUM_QTY],[LEVEL],[ID_LEVEL],insert_who,qrcode_new)
                                                VALUES ((select mts_no from stock_card where status = 'Receive From Main Store' and qrcode_new='" & dtCheckStockNQ.Rows(0).Item("qrcode") & "'),'" & globVar.QRCode_PN & "'," & dtCheckStockNQ.Rows(0).Item("qty") & ",'" & globVar.QRCode_Inv & "','" & globVar.QRCode_Traceability & "','" & globVar.QRCode_lot & "','" & globVar.QRCode_Batch & "','" & PO.Text & "','" & SubSubPO.Text & "','" & DataGridView3.Rows(CurrentRowIndex).Cells("FG Part Number").Value & "'," & dtCheckStockNQ.Rows(0).Item("qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "',(select standard_pack from stock_card where status = 'Receive From Main Store' and qrcode_new='" & dtCheckStockNQ.Rows(0).Item("qrcode") & "'),(select qrcode from stock_card where status = 'Receive From Main Store' and qrcode_new='" & dtCheckStockNQ.Rows(0).Item("qrcode") & "')," & dtCheckStockNQ.Rows(0).Item("qty") & ",'Fresh','" & globVar.QRCode_PN & "','" & globVar.username & "','" & dtCheckStockNQ.Rows(0).Item("qrcode") & "')"
                                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                If cmdInsertInputStockDetail.ExecuteNonQuery() Then

                                    notification(TextBox1.Text, dtCheckStockNQ.Rows(0).Item("qty"))

                                    DGV_InProductionMaterial()

                                    Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 WHERE qrcode_new='" & TextBox1.Text & "' and STATUS='Receive From Main Store'"
                                    Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                    cmdUpdate.ExecuteNonQuery()
                                    TextBox1.Clear()

                                End If
                            Catch ex As Exception
                                RJMessageBox.Show("Error Insert" & ex.Message)
                            End Try
                        End If

                        DGV_MaterialNeed()

                    ElseIf TextBox1.Text.StartsWith("MX2D") Then

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

                        If DataGridView3.Rows.Count > 0 Then
                            For Each gRow As DataGridViewRow In DataGridView3.Rows
                                StringToSearch = gRow.Cells("Comp").Value.ToString.Trim.ToLower
                                If InStr(1, StringToSearch, LCase(Trim(globVar.QRCode_PN)), vbTextCompare) = 1 Then
                                    Dim myCurrentCell As DataGridViewCell = gRow.Cells("Comp")
                                    DataGridView3.CurrentCell = myCurrentCell
                                    CurrentRowIndex = DataGridView3.CurrentRow.Index
                                    Found = True
                                End If
                                If Found Then Exit For
                            Next
                        End If

                        If Found = False Then
                            RJMessageBox.Show("Production no need for this material.")
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        Dim sqlCheckStockMinistore = "SELECT * FROM stock_card WHERE material = '" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' and batch_no='" & globVar.QRCode_Batch & "' and inv_ctrl_date='" & globVar.QRCode_Inv & "' and traceability='" & globVar.QRCode_Traceability & "' and department='" & globVar.department & "' and status='Receive From Main Store' and [save]=1 and actual_qty > 0 and qrcode_new is null"
                        Dim dtCheckStockMinistore As DataTable = Database.GetData(sqlCheckStockMinistore)

                        If dtCheckStockMinistore.Rows.Count = 0 Then
                            RJMessageBox.Show("Material doesn't exist in Database")
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        If dtCheckStockMinistore.Rows.Count > 1 Then
                            RJMessageBox.Show("The scanned material has more than 1 material. " & globVar.QRCode_PN & " - " & globVar.QRCode_lot)
                            TextBox1.Clear()
                            Exit Sub
                        End If

                        Dim sqlCheckInProdFreshMaterial As String = "SELECT * FROM stock_card WHERE batch_no='" & globVar.QRCode_Batch & "' and material = '" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' and status='Production Request' and department='" & globVar.department & "' and sub_sub_po='" & SubSubPO.Text & "'"
                        Dim dtCheckInProdFreshMaterial As DataTable = Database.GetData(sqlCheckInProdFreshMaterial)

                        If dtCheckInProdFreshMaterial.Rows.Count > 0 Then
                            RJMessageBox.Show("Sorry this material already in database production")
                            TextBox1.Text = ""
                            Exit Sub
                        End If

                        Dim sqlCheckSumQtyProdcution As String = "SELECT isnull(sum(sum_qty),0) qty FROM stock_card WHERE sub_sub_po = '" & SubSubPO.Text & "' and material='" & globVar.QRCode_PN & "' AND LINE='" & ComboBox1.Text & "' and department='" & globVar.department & "' and status='Production Request'"
                        Dim dtCheckSumQtyProdcution As DataTable = Database.GetData(sqlCheckSumQtyProdcution)
                        If dtCheckSumQtyProdcution.Rows(0).Item("qty") >= DataGridView3.Rows(CurrentRowIndex).Cells("Total Need").Value Then
                            RJMessageBox.Show("Cannot add component because Qty more than Qty Need")
                            TextBox1.Text = ""
                            DGV_InProductionMaterial()
                        Else
                            Try
                                Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,QRCODE,MTS_NO,SUM_QTY,LEVEL,ID_LEVEL,insert_who)
                                                VALUES ('" & globVar.QRCode_PN & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & dtCheckStockMinistore.Rows(0).Item("inv_ctrl_date") & "','" & dtCheckStockMinistore.Rows(0).Item("traceability") & "','" & globVar.QRCode_lot & "','" & dtCheckStockMinistore.Rows(0).Item("batch_no") & "','" & PO.Text & "','" & SubSubPO.Text & "'," & DataGridView3.Rows(CurrentRowIndex).Cells("FG Part Number").Value & "," & dtCheckStockMinistore.Rows(0).Item("Qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "','" & dtCheckStockMinistore.Rows(0).Item("standard_pack") & "','" & dtCheckStockMinistore.Rows(0).Item("qrcode") & "','" & dtCheckStockMinistore.Rows(0).Item("mts_no") & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'Fresh','" & globVar.QRCode_PN & "','" & globVar.username & "')"
                                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                If cmdInsertInputStockDetail.ExecuteNonQuery() Then

                                    notification(globVar.QRCode_PN, dtCheckStockMinistore.Rows(0).Item("qty"))

                                    DGV_InProductionMaterial()

                                    Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 WHERE batch_no='" & globVar.QRCode_Batch & "' and material='" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Receive From Main Store' and qrcode_new is null"
                                    Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                    cmdUpdate.ExecuteNonQuery()
                                    TextBox1.Clear()
                                End If
                            Catch ex As Exception
                                RJMessageBox.Show("Error Insert" & ex.Message)
                            End Try
                        End If

                        DGV_MaterialNeed()

                    Else

                        RJMessageBox.Show("QRCode not valid.")
                        Play_Sound.Wrong()
                        TextBox1.Clear()
                        Exit Sub

                    End If

                Else

                    RJMessageBox.Show("Your Access cannot execute this action")

                End If
            End If

        Catch ex As Exception

            RJMessageBox.Show("Error Production Request - 2 =>" & ex.Message)

        End Try
    End Sub

    Sub DGV_InProductionMaterial()
        Try
            DataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView4.DataSource = Nothing
            DataGridView4.Rows.Clear()
            DataGridView4.Columns.Clear()

            Call Database.koneksi_database()
            Dim queryInProdMaterial As String = "select in_mat.id [#], in_mat.datetime_insert [Date Scan], CASE
			                                    WHEN in_mat.qrcode_new IS NULL THEN
					                                    LEFT(in_mat.qrcode COLLATE SQL_Latin1_General_CP1_CI_AS, 16)
			                                    ELSE 
					                                    LEFT(in_mat.qrcode_new COLLATE SQL_Latin1_General_CP1_CI_AS, 16)
	                                        END AS QRCode, in_mat.MATERIAL [Material],in_mat.LOT_NO [Lot],in_mat.TRACEABILITY [Trace],in_mat.INV_CTRL_DATE [ICD],in_mat.BATCH_NO [Batch],in_mat.QTY [Qty],in_mat.actual_qty [Actual Qty],in_mat.insert_who [Scan By]
            from stock_card in_mat, sub_sub_po sp 
            where sp.sub_sub_po=in_mat.sub_sub_po and sp.line = '" & ComboBox1.Text & "' and in_mat.line= '" & ComboBox1.Text & "' and sp.sub_sub_po='" & SubSubPO.Text & "' and in_mat.sub_sub_po='" & SubSubPO.Text & "' AND DEPARTMENT='" & globVar.department & "' and in_mat.[status]='Production Request' and in_mat.[level] = 'Fresh' ORDER BY in_mat.material, in_mat.lot_no"
            Dim dtInProdMaterial As DataTable = Database.GetData(queryInProdMaterial)

            DataGridView4.DataSource = dtInProdMaterial

            Dim deleteProductionRequest As DataGridViewButtonColumn = New DataGridViewButtonColumn
            deleteProductionRequest.Name = "delete"
            deleteProductionRequest.HeaderText = "Delete"
            deleteProductionRequest.Width = 50
            deleteProductionRequest.Text = "Delete"
            deleteProductionRequest.UseColumnTextForButtonValue = True
            DataGridView4.Columns.Insert(11, deleteProductionRequest)

            DataGridView4.Columns(0).Width = 100
            DataGridView4.Columns(2).Width = 300

            For i As Integer = 0 To DataGridView4.RowCount - 1
                If DataGridView4.Rows(i).Index Mod 2 = 0 Then
                    DataGridView4.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    DataGridView4.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next i

        Catch ex As Exception
            RJMessageBox.Show("Error Production Request - 3 =>" & ex.Message)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = CheckState.Unchecked Then
            TextBox2.Enabled = True
            TextBox1.Enabled = False
            Button1.Enabled = False
            ComboBox2.Enabled = False
            TextBox1.Clear()
        Else
            TextBox2.Clear()
            ComboBox2.SelectedIndex = -1
            TextBox2.Enabled = False
            TextBox1.Enabled = True
            Button1.Enabled = False
            ComboBox2.Enabled = False
        End If
    End Sub

    Private Sub ProductionRequest_Load(sender As Object, e As EventArgs) Handles Me.Load
        globVar.PingVersion()
        TextBox2.Enabled = False
        TextBox1.Enabled = True
        Button1.Enabled = False
        ComboBox1.SelectedIndex = -1
        tampilDataComboBoxLine()
        TextBox1.Enabled = False
    End Sub

    Sub tampilDataComboBoxLine()
        Call Database.koneksi_database()
        Dim dtMasterLine As DataTable = Database.GetData("select line as name from SUB_SUB_PO ssp LEFT JOIN main_po mp on mp.id=ssp.main_po and mp.department ='" & globVar.department & "' where ssp.status='Open' order by ssp.line")

        ComboBox1.DataSource = dtMasterLine
        ComboBox1.DisplayMember = "name"
        ComboBox1.ValueMember = "name"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If TextBox2.Text = "" Or ComboBox2.Text = "" Then
                RJMessageBox.Show("Part Number or Lot No Still blank. Please fill first")
                Exit Sub
            End If

            If globVar.add > 0 Then
                Dim StringToSearch As String = ""
                Dim CurrentRowIndex As Integer = 0
                Dim Found As Boolean = False

                If DataGridView3.Rows.Count > 0 Then
                    For Each gRow As DataGridViewRow In DataGridView3.Rows
                        StringToSearch = gRow.Cells("Comp").Value.ToString.Trim.ToLower
                        If InStr(1, StringToSearch, LCase(Trim(TextBox2.Text)), vbTextCompare) = 1 Then
                            Dim myCurrentCell As DataGridViewCell = gRow.Cells("Comp")
                            DataGridView3.CurrentCell = myCurrentCell
                            CurrentRowIndex = DataGridView3.CurrentRow.Index
                            Found = True
                        End If
                        If Found Then Exit For
                    Next
                End If

                If Found = False Then
                    RJMessageBox.Show("Production no need for this Part Number.")
                    TextBox1.Text = ""
                Else

                    Dim allmanualMaterial As String() = ComboBox2.Text.Split("|")
                    Dim lotManualMaterial As String() = allmanualMaterial(0).Split(":")
                    Dim icdManualMaterial As String() = allmanualMaterial(1).Split(":")
                    Dim traceManualMaterial As String() = allmanualMaterial(2).Split(":")
                    Dim batchManualMaterial As String() = allmanualMaterial(3).Split(":")

                    Dim sqlCheckStockMinistore As String = "SELECT * FROM stock_card WHERE material = '" & TextBox2.Text & "' 
                        and lot_no='" & lotManualMaterial(1) & "' 
                        and inv_ctrl_date='" & icdManualMaterial(1) & "' 
                        and traceability='" & traceManualMaterial(1) & "' 
                        and batch_no='" & batchManualMaterial(1) & "' 
                        and department='" & globVar.department & "' 
                        and status='Receive From Main Store' or status='Receive From Production') 
                        and actual_qty>0 and [save]=1"
                    Dim dtCheckStockMinistore As DataTable = Database.GetData(sqlCheckStockMinistore)
                    If dtCheckStockMinistore.Rows.Count > 0 Then
                        Dim sqlCheckInProdFreshMaterial As String = "SELECT * FROM stock_card WHERE material = '" & TextBox2.Text & "' 
                            and lot_no='" & lotManualMaterial(1) & "' 
                            and inv_ctrl_date='" & icdManualMaterial(1) & "' 
                            and traceability='" & traceManualMaterial(1) & "' 
                            and batch_no='" & batchManualMaterial(1) & "' 
                            and status='Production Request' 
                            and department='" & globVar.department & "' 
                            and sub_sub_po='" & SubSubPO.Text & "'"
                        Dim dtCheckInProdFreshMaterial As DataTable = Database.GetData(sqlCheckInProdFreshMaterial)
                        If dtCheckInProdFreshMaterial.Rows.Count > 0 Then
                            RJMessageBox.Show("Sorry QR Code already in database production")
                            TextBox1.Text = ""
                        Else
                            Dim sqlCheckSumQtyProdcution As String = "SELECT isnull(sum(sum_qty),0) qty FROM stock_card WHERE sub_sub_po = '" & SubSubPO.Text & "' and material='" & TextBox2.Text & "' AND LINE='" & ComboBox1.Text & "' and department='" & globVar.department & "' and status='Production Request'"
                            Dim dtCheckSumQtyProdcution As DataTable = Database.GetData(sqlCheckSumQtyProdcution)
                            If dtCheckSumQtyProdcution.Rows(0).Item("qty") > DataGridView3.Rows(CurrentRowIndex).Cells("Total Need").Value Then
                                RJMessageBox.Show("Cannot add component because Qty more than Qty Need")
                                TextBox1.Text = ""
                                DGV_InProductionMaterial()
                            Else
                                If dtCheckSumQtyProdcution.Rows(0).Item("qty") + dtCheckStockMinistore.Rows(0).Item("qty") > DataGridView3.Rows(CurrentRowIndex).Cells("Total Need").Value Then
                                    Try
                                        Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,QRCODE,MTS_NO,SUM_QTY,LEVEL,ID_LEVEL,PRODUCTION_REQUEST_DATETIME,PRODUCTION_REQUEST_WHO)
                                            VALUES ('" & TextBox2.Text & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & dtCheckStockMinistore.Rows(0).Item("INV_CTRL_DATE") & "','" & dtCheckStockMinistore.Rows(0).Item("TRACEABILITY") & "','" & lotManualMaterial(1) & "','" & dtCheckStockMinistore.Rows(0).Item("BATCH_NO") & "','" & PO.Text & "','" & SubSubPO.Text & "'," & DataGridView3.Rows(CurrentRowIndex).Cells("FG Part Number").Value & "," & dtCheckStockMinistore.Rows(0).Item("Qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "','" & dtCheckStockMinistore.Rows(0).Item("standard_pack") & "','Manual Input','" & dtCheckStockMinistore.Rows(0).Item("mts_no") & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'Fresh','" & TextBox2.Text & "',getdate(),'" & globVar.username & "')"
                                        Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)

                                        If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                                            TextBox2.Text = ""
                                            ComboBox2.SelectedIndex = -1
                                            Button1.Enabled = False
                                            DGV_InProductionMaterial()

                                            Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE material='" & TextBox2.Text & "' 
                                                and lot_no='" & lotManualMaterial(1) & "' 
                                                and inv_ctrl_date='" & icdManualMaterial(1) & "' 
                                                and traceability='" & traceManualMaterial(1) & "' 
                                                and batch_no='" & batchManualMaterial(1) & "' 
                                                AND DEPARTMENT='" & globVar.department & "' 
                                                AND (STATUS='Receive From Main Store' or STATUS='Receive From Production')"
                                            Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                            cmdUpdate.ExecuteNonQuery()

                                            RJMessageBox.Show("Add Material Success")
                                        End If
                                    Catch ex As Exception
                                        RJMessageBox.Show("Error Insert" & ex.Message)
                                    End Try
                                Else
                                    Try
                                        Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,QRCODE,MTS_NO,SUM_QTY,LEVEL,ID_LEVEL,PRODUCTION_REQUEST_DATETIME,PRODUCTION_REQUEST_WHO)
                                            VALUES ('" & TextBox2.Text & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & dtCheckStockMinistore.Rows(0).Item("INV_CTRL_DATE") & "','" & dtCheckStockMinistore.Rows(0).Item("TRACEABILITY") & "','" & dtCheckStockMinistore.Rows(0).Item("LOT_NO") & "','" & dtCheckStockMinistore.Rows(0).Item("BATCH_NO") & "','" & PO.Text & "','" & SubSubPO.Text & "'," & DataGridView3.Rows(CurrentRowIndex).Cells("FG Part Number").Value & "," & dtCheckStockMinistore.Rows(0).Item("Qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "','" & dtCheckStockMinistore.Rows(0).Item("standard_pack") & "','Manual Input','" & dtCheckStockMinistore.Rows(0).Item("mts_no") & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'Fresh','" & TextBox2.Text & "',getdate(),'" & globVar.username & "')"
                                        Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                        If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                                            TextBox2.Text = ""
                                            ComboBox2.SelectedIndex = -1
                                            Button1.Enabled = False
                                            DGV_InProductionMaterial()

                                            Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE material='" & TextBox2.Text & "' 
                                                and lot_no='" & lotManualMaterial(1) & "' 
                                                and inv_ctrl_date='" & icdManualMaterial(1) & "' 
                                                and traceability='" & traceManualMaterial(1) & "' 
                                                and batch_no='" & batchManualMaterial(1) & "' 
                                                AND DEPARTMENT='" & globVar.department & "' 
                                                AND (STATUS='Receive From Main Store' or STATUS='Receive From Production')"
                                            Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                            cmdUpdate.ExecuteNonQuery()

                                            RJMessageBox.Show("Add Material Success")
                                        End If
                                    Catch ex As Exception
                                        RJMessageBox.Show("Error Insert" & ex.Message)
                                    End Try
                                End If
                            End If
                        End If
                    Else
                        RJMessageBox.Show("This Material not available in Stock Ministore. Please goto input stock first")
                        TextBox2.Text = ""
                        ComboBox2.SelectedIndex = -1
                    End If
                End If
            Else
                RJMessageBox.Show("Your Access cannot execute this action")
            End If
        Catch ex As Exception
            RJMessageBox.Show("Error Production Request - 4 =>" & ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If globVar.view > 0 Then
            DGV_MaterialNeed()
            DGV_InProductionMaterial()
        End If
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

    Private Sub DataGridView4_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView4.DataBindingComplete
        With DataGridView4
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
        TextBox1.Enabled = False
    End Sub

    Sub tampilDataComboBoxDataManualMaterial(material As String)
        Call Database.koneksi_database()
        Dim dtMaterial As DataTable = Database.GetData("select lot_no, inv_ctrl_date, traceability, batch_no, qty, qrcode from stock_card where department='" & globVar.department & "' and material='" & material & "' and actual_qty > 0 and sub_sub_po is null and status = 'Receive From Main Store' and [save] = 1 order by lot_no, inv_ctrl_date, traceability, batch_no")

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

    Private Sub TextBox2_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox2.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            tampilDataComboBoxDataManualMaterial(TextBox2.Text)
            Button1.Enabled = True
            ComboBox2.Enabled = True
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If TextBox2.Text.StartsWith("0") AndAlso TextBox2.Text.Length > 1 Then
            TextBox2.Text = TextBox2.Text.TrimStart("0"c)
            TextBox2.SelectionStart = TextBox2.Text.Length
        End If

        Button1.Enabled = False
        ComboBox2.Enabled = False
    End Sub

    Private Sub DataGridView4_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView4.CellClick

        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If e.ColumnIndex = -1 Then
            Exit Sub
        End If

        If DataGridView4.Columns(e.ColumnIndex).Name = "delete" Then

            Dim result = RJMessageBox.Show("Are you sure for delete this material?.", "Are You Sure?", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then

                If DataGridView4.Rows(e.RowIndex).Cells("Qty").Value <> DataGridView4.Rows(e.RowIndex).Cells("Actual Qty").Value Then
                    RJMessageBox.Show("Cannot delete this material because this material already scan in production")
                    Exit Sub
                End If

                Dim sqlCheckQRCode As String = "select qrcode from stock_card where id=" & DataGridView4.Rows(e.RowIndex).Cells("#").Value
                Dim dtCheckQRCode As DataTable = Database.GetData(sqlCheckQRCode)
                If dtCheckQRCode.Rows.Count > 0 Then

                    If dtCheckQRCode.Rows(0).Item("qrcode").ToString.StartsWith("NQ") Then

                        Dim queryUpdateSC As String = "update stock_card set actual_qty=qty where (status='Receive From Main Store' or status='Receive From Production') and qrcode_new = '" & dtCheckQRCode.Rows(0).Item("qrcode") & "'"
                        Dim dtUpdateSC = New SqlCommand(queryUpdateSC, Database.koneksi)

                        If dtUpdateSC.ExecuteNonQuery() Then

                            Dim sql As String = "delete from stock_card where id=" & DataGridView4.Rows(e.RowIndex).Cells("#").Value
                            Dim cmd = New SqlCommand(sql, Database.koneksi)
                            If cmd.ExecuteNonQuery() Then
                                RJMessageBox.Show("Delete success")
                                DGV_InProductionMaterial()
                                DGV_MaterialNeed()
                            End If

                        End If

                    Else

                        Dim queryUpdateSC As String = "update stock_card set actual_qty=qty where (status='Receive From Main Store' or status='Receive From Production') and qrcode = '" & dtCheckQRCode.Rows(0).Item("qrcode") & "'"
                        Dim dtUpdateSC = New SqlCommand(queryUpdateSC, Database.koneksi)

                        If dtUpdateSC.ExecuteNonQuery() Then

                            Dim sql As String = "delete from stock_card where id=" & DataGridView4.Rows(e.RowIndex).Cells("#").Value
                            Dim cmd = New SqlCommand(sql, Database.koneksi)
                            If cmd.ExecuteNonQuery() Then
                                RJMessageBox.Show("Delete success")
                                DGV_InProductionMaterial()
                                DGV_MaterialNeed()
                            End If

                        End If

                    End If

                End If

            End If

            End If

    End Sub
End Class