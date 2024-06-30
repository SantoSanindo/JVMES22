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

            Dim queryMasterFinishGoods As String = "select sp.Sub_Sub_PO [Sub Sub PO],mp.fg_pn [FG Part Number],mufg.component [Comp],mufg.description [Desc],mufg.usage [Usage],sp.sub_sub_po_qty [Qty],ceiling(( mufg.usage * sp.sub_sub_po_qty ) + ( mufg.usage * sp.sub_sub_po_qty * sp.yield_lose / 100)) AS [Total Need],mp.po,mp.sub_po,(select sum(qty) from STOCK_CARD where sub_sub_po=sp.SUB_SUB_PO and material=mufg.component and status='Production Request') [Total Ministore to Production]
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

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
        Dim QrcodeValid As Boolean
        Try
            Dim ds As New DataSet
            Dim Found As Boolean = False
            Dim StringToSearch As String = ""
            Dim CurrentRowIndex As Integer = 0
            Dim sqlCheckStockMinistore As String

            If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) And TextBox1.Text <> "" And ComboBox1.Text <> "" Then
                If globVar.add > 0 Then

                    If TextBox1.Text.StartsWith("B") AndAlso TextBox1.Text.Length > 1 AndAlso IsNumeric(TextBox1.Text.Substring(1)) Then

                        Dim queryCheck As String = "select * from stock_card where id_level='" & TextBox1.Text & "' and status='Receive From Production' and actual_qty > 0 and department='" & globVar.department & "'"
                        Dim dttable As DataTable = Database.GetData(queryCheck)
                        If dttable.Rows.Count > 0 Then
                            globVar.QRCode_PN = dttable.Rows(0).Item("material")
                            globVar.QRCode_lot = dttable.Rows(0).Item("lot_no")
                        End If

                    ElseIf Regex.IsMatch(TextBox1.text, "^\d+-\d+-\d+$") Then

                        Dim SplitLabel = TextBox1.Text.Split("-")
                        globVar.QRCode_PN = SplitLabel(0)
                        globVar.QRCode_lot = SplitLabel(1) & "-" & SplitLabel(2)

                    ElseIf TextBox1.text.StartsWith("MX2D") Then

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

                    Else

                        RJMessageBox.Show("QRCode not valid.")
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
                        RJMessageBox.Show("Production no need for this Part Number.")
                        TextBox1.Text = ""
                    Else
                        If InStr(TextBox1.Text, "B") > 0 And Len(TextBox1.Text) < 10 Then
                            sqlCheckStockMinistore = "SELECT * FROM stock_card WHERE id_level='" & TextBox1.Text & "' and department='" & globVar.department & "' and status='Receive From Production' and actual_qty>0"

                            Dim dtCheckStockMinistore As DataTable = Database.GetData(sqlCheckStockMinistore)
                            If dtCheckStockMinistore.Rows.Count > 0 Then
                                Dim sqlCheckInProdFreshMaterial As String = "SELECT * FROM stock_card WHERE qrcode='" & TextBox1.Text & "' and status='Production Request' and department='" & globVar.department & "' and sub_sub_po='" & SubSubPO.Text & "'"
                                Dim dtCheckInProdFreshMaterial As DataTable = Database.GetData(sqlCheckInProdFreshMaterial)
                                If dtCheckInProdFreshMaterial.Rows.Count > 0 Then
                                    RJMessageBox.Show("Sorry QR Code already in database production")
                                    TextBox1.Text = ""
                                Else
                                    Dim sqlCheckSumQtyProdcution As String = "SELECT isnull(sum(sum_qty),0) qty FROM stock_card WHERE sub_sub_po = '" & SubSubPO.Text & "' and material='" & dtCheckStockMinistore.Rows(0).Item("material") & "' AND LINE='" & ComboBox1.Text & "' and department='" & globVar.department & "' and status='Production Request'"
                                    Dim dtCheckSumQtyProdcution As DataTable = Database.GetData(sqlCheckSumQtyProdcution)
                                    If dtCheckSumQtyProdcution.Rows(0).Item("qty") >= DataGridView3.Rows(CurrentRowIndex).Cells("Total Need").Value Then
                                        RJMessageBox.Show("Cannot add component because Qty more than Qty Need")
                                        TextBox1.Text = ""
                                        DGV_InProductionMaterial()
                                    Else
                                        Try
                                            Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,QRCODE,MTS_NO,SUM_QTY,LEVEL,ID_LEVEL,PRODUCTION_REQUEST_DATETIME,PRODUCTION_REQUEST_WHO)
                                                VALUES ('" & dtCheckStockMinistore.Rows(0).Item("material") & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & dtCheckStockMinistore.Rows(0).Item("inv_ctrl_date") & "','" & dtCheckStockMinistore.Rows(0).Item("traceability") & "','" & dtCheckStockMinistore.Rows(0).Item("lot_no") & "','" & dtCheckStockMinistore.Rows(0).Item("batch_no") & "','" & PO.Text & "','" & SubSubPO.Text & "'," & DataGridView3.Rows(CurrentRowIndex).Cells("FG Part Number").Value & "," & dtCheckStockMinistore.Rows(0).Item("Qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "','" & dtCheckStockMinistore.Rows(0).Item("standard_pack") & "','" & dtCheckStockMinistore.Rows(0).Item("qrcode") & "','" & dtCheckStockMinistore.Rows(0).Item("mts_no") & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'Fresh','" & dtCheckStockMinistore.Rows(0).Item("material") & "',getdate(),'" & globVar.username & "')"
                                            Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                            If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                                                DGV_InProductionMaterial()

                                                Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE qrcode='" & TextBox1.Text & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Receive From Production'"
                                                Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                                cmdUpdate.ExecuteNonQuery()
                                                TextBox1.Clear()
                                            End If
                                        Catch ex As Exception
                                            RJMessageBox.Show("Error Insert" & ex.Message)
                                        End Try
                                    End If
                                End If

                                DGV_MaterialNeed()
                            Else
                                RJMessageBox.Show("This QR Code not available in Stock Ministore. Please goto input stock first or Qty = 0")
                                TextBox1.Text = ""
                            End If
                        Else
                            sqlCheckStockMinistore = "SELECT * FROM stock_card WHERE material = '" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' and batch_no='" & globVar.QRCode_Batch & "' and traceability='" & globVar.QRCode_Traceability & "' and department='" & globVar.department & "' and (status='Receive From Main Store' or status='Receive From Production') and actual_qty>0 and [save]=1"

                            Dim dtCheckStockMinistore As DataTable = Database.GetData(sqlCheckStockMinistore)
                            If dtCheckStockMinistore.Rows.Count > 0 Then
                                Dim sqlCheckInProdFreshMaterial As String = "SELECT * FROM stock_card WHERE batch_no='" & globVar.QRCode_Batch & "' and material = '" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' and status='Production Request' and department='" & globVar.department & "' and sub_sub_po='" & SubSubPO.Text & "'"
                                Dim dtCheckInProdFreshMaterial As DataTable = Database.GetData(sqlCheckInProdFreshMaterial)
                                If dtCheckInProdFreshMaterial.Rows.Count > 0 Then
                                    RJMessageBox.Show("Sorry QR Code already in database production")
                                    TextBox1.Text = ""
                                Else
                                    Dim sqlCheckSumQtyProdcution As String = "SELECT isnull(sum(sum_qty),0) qty FROM stock_card WHERE sub_sub_po = '" & SubSubPO.Text & "' and material='" & globVar.QRCode_PN & "' AND LINE='" & ComboBox1.Text & "' and department='" & globVar.department & "' and status='Production Request'"
                                    Dim dtCheckSumQtyProdcution As DataTable = Database.GetData(sqlCheckSumQtyProdcution)
                                    If dtCheckSumQtyProdcution.Rows(0).Item("qty") >= DataGridView3.Rows(CurrentRowIndex).Cells("Total Need").Value Then
                                        RJMessageBox.Show("Cannot add component because Qty more than Qty Need")
                                        TextBox1.Text = ""
                                        DGV_InProductionMaterial()
                                    Else
                                        Try
                                            Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,QRCODE,MTS_NO,SUM_QTY,LEVEL,ID_LEVEL,PRODUCTION_REQUEST_DATETIME,PRODUCTION_REQUEST_WHO)
                                                VALUES ('" & globVar.QRCode_PN & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & dtCheckStockMinistore.Rows(0).Item("inv_ctrl_date") & "','" & dtCheckStockMinistore.Rows(0).Item("traceability") & "','" & globVar.QRCode_lot & "','" & dtCheckStockMinistore.Rows(0).Item("batch_no") & "','" & PO.Text & "','" & SubSubPO.Text & "'," & DataGridView3.Rows(CurrentRowIndex).Cells("FG Part Number").Value & "," & dtCheckStockMinistore.Rows(0).Item("Qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "','" & dtCheckStockMinistore.Rows(0).Item("standard_pack") & "','" & dtCheckStockMinistore.Rows(0).Item("qrcode") & "','" & dtCheckStockMinistore.Rows(0).Item("mts_no") & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'Fresh','" & globVar.QRCode_PN & "',getdate(),'" & globVar.username & "')"
                                            Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                            If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                                                DGV_InProductionMaterial()

                                                Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE batch_no='" & globVar.QRCode_Batch & "' and material='" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' AND DEPARTMENT='" & globVar.department & "' AND (STATUS='Receive From Main Store' or STATUS='Receive From Production')"
                                                Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                                cmdUpdate.ExecuteNonQuery()
                                                TextBox1.Clear()
                                            End If
                                        Catch ex As Exception
                                            RJMessageBox.Show("Error Insert" & ex.Message)
                                        End Try
                                    End If
                                End If

                                DGV_MaterialNeed()
                            Else
                                RJMessageBox.Show("This QR Code not available in Stock Ministore. Please goto input stock first or Qty = 0")
                                TextBox1.Text = ""
                            End If
                        End If
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
            Dim queryInProdMaterial As String = "select in_mat.MATERIAL [Material],in_mat.LOT_NO [Lot],in_mat.TRACEABILITY [Trace],in_mat.INV_CTRL_DATE [ICD],in_mat.BATCH_NO [Batch],in_mat.QTY [Qty],in_mat.SUM_QTY [Actual Qty], in_mat.[level] [Level Material], in_mat.production_request_datetime [Date Scan],in_mat.production_request_who [Scan By]
            from stock_card in_mat, sub_sub_po sp 
            where sp.sub_sub_po=in_mat.sub_sub_po and sp.line = '" & ComboBox1.Text & "' and in_mat.line= '" & ComboBox1.Text & "' and sp.sub_sub_po='" & SubSubPO.Text & "' and in_mat.sub_sub_po='" & SubSubPO.Text & "' AND DEPARTMENT='" & globVar.department & "' and in_mat.[status]='Production Request' and in_mat.[level] = 'Fresh' ORDER BY in_mat.material, in_mat.lot_no"
            Dim dtInProdMaterial As DataTable = Database.GetData(queryInProdMaterial)

            If dtInProdMaterial.Rows.Count > 0 Then
                DataGridView4.DataSource = dtInProdMaterial
            End If

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
        TextBox2.Enabled = False
        TextBox1.Enabled = True
        Button1.Enabled = False
        ComboBox1.SelectedIndex = -1
        tampilDataComboBoxLine()
        TextBox1.Enabled = False
    End Sub

    Sub tampilDataComboBoxLine()
        Call Database.koneksi_database()
        Dim dtMasterLine As DataTable = Database.GetData("select * from master_line where department='" & globVar.department & "' order by name")

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
                        and (status='Receive From Main Store' or status='Receive From Production') 
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
                                        Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,QRCODE,MTS_NO,SUM_QTY,LEVEL,ID_LEVEL)
                                            VALUES ('" & TextBox2.Text & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & dtCheckStockMinistore.Rows(0).Item("INV_CTRL_DATE") & "','" & dtCheckStockMinistore.Rows(0).Item("TRACEABILITY") & "','" & lotManualMaterial(1) & "','" & dtCheckStockMinistore.Rows(0).Item("BATCH_NO") & "','" & PO.Text & "','" & SubSubPO.Text & "'," & DataGridView3.Rows(CurrentRowIndex).Cells("FG Part Number").Value & "," & dtCheckStockMinistore.Rows(0).Item("Qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "','" & dtCheckStockMinistore.Rows(0).Item("standard_pack") & "','Manual Input','" & dtCheckStockMinistore.Rows(0).Item("mts_no") & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'Fresh','" & TextBox2.Text & "')"
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
                                        Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,QRCODE,MTS_NO,SUM_QTY,LEVEL,ID_LEVEL)
                                            VALUES ('" & TextBox2.Text & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & dtCheckStockMinistore.Rows(0).Item("INV_CTRL_DATE") & "','" & dtCheckStockMinistore.Rows(0).Item("TRACEABILITY") & "','" & dtCheckStockMinistore.Rows(0).Item("LOT_NO") & "','" & dtCheckStockMinistore.Rows(0).Item("BATCH_NO") & "','" & PO.Text & "','" & SubSubPO.Text & "'," & DataGridView3.Rows(CurrentRowIndex).Cells("FG Part Number").Value & "," & dtCheckStockMinistore.Rows(0).Item("Qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "','" & dtCheckStockMinistore.Rows(0).Item("standard_pack") & "','Manual Input','" & dtCheckStockMinistore.Rows(0).Item("mts_no") & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'Fresh','" & TextBox2.Text & "')"
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
                row("DisplayMember") = "Lot No:" & row("lot_no").ToString() & "|ICD:" & row("inv_ctrl_date").ToString() & "|Trace:" & row("traceability").ToString() & "|Batch:" & row("batch_no").ToString() & "|Qty:" & row("qty").ToString() & " - Manual"
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

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Button1.Enabled = False
        ComboBox2.Enabled = False
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
End Class