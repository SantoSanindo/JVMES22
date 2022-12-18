Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.Office.Interop.Excel

Public Class ProductionRequest
    Sub Insert_Prod_DOC(fg As String, sub_sub_po As String)
        Dim queryProdDOC As String = "select mp.po,sp.Sub_Sub_PO,mp.fg_pn,mufg.component,mufg.description,mufg.usage,mufg.family
        from sub_sub_po sp,main_po mp,material_usage_finish_goods mufg 
        where sp.main_po= mp.id AND mufg.fg_part_number= mp.fg_pn AND sp.status= 'Open' and line = '" & ComboBox1.Text & "' and mp.fg_pn = '" & fg & "' and sp.sub_sub_po='" & sub_sub_po & "' order by sp.sub_sub_po"
        Dim dtProdDOC As DataTable = Database.GetData(queryProdDOC)

        If dtProdDOC.Rows.Count > 0 Then
            For i As Integer = 0 To dtProdDOC.Rows.Count - 1
                Dim queryCheckProdDOC As String = "select * from prod_doc where sub_sub_po= '" & sub_sub_po & "' AND fg_pn = '" & fg & "' AND component='" & dtProdDOC.Rows(i).Item("component") & "' and line = '" & ComboBox1.Text & "'"
                Dim dtCheckProdDOC As DataTable = Database.GetData(queryCheckProdDOC)
                If dtCheckProdDOC.Rows.Count = 0 Then
                    Dim sqlInsertDOC As String = "INSERT INTO prod_doc (po, sub_sub_po, fg_pn, component, desc_comp, family, usage, line)
                                    VALUES ('" & PO.Text & "','" & SubSubPO.Text & "','" & dtProdDOC.Rows(i).Item("fg_pn") & "','" & dtProdDOC.Rows(i).Item("component") & "','" & dtProdDOC.Rows(i).Item("description") & "','" & dtProdDOC.Rows(i).Item("family") & "'," & dtProdDOC.Rows(i).Item("usage").ToString.Replace(",", ".") & ",'" & ComboBox1.Text & "')"
                    Dim cmdInsertDOC = New SqlCommand(sqlInsertDOC, Database.koneksi)
                    cmdInsertDOC.ExecuteNonQuery()
                End If
            Next
        End If
    End Sub

    Sub Insert_Prod_DOP(fg As String, sub_sub_po As String)
        Dim queryProdDOP As String = "select mp.po,sp.Sub_Sub_PO,mp.fg_pn,mpf.master_process
        from sub_sub_po sp,main_po mp,MASTER_PROCESS_FLOW MPF 
        where sp.main_po = mp.id AND mpf.MASTER_FINISH_GOODS_PN = mp.fg_pn AND sp.status= 'Open' and line = '" & ComboBox1.Text & "' and mp.fg_pn = '" & fg & "' and sp.sub_sub_po='" & sub_sub_po & "' order by sp.sub_sub_po"
        Dim dtProdDOP As DataTable = Database.GetData(queryProdDOP)

        If dtProdDOP.Rows.Count > 0 Then
            For i As Integer = 0 To dtProdDOP.Rows.Count - 1
                Dim queryCheckProdDOP As String = "select * from prod_dop where sub_sub_po= '" & sub_sub_po & "' AND fg_pn = '" & fg & "' AND process='" & dtProdDOP.Rows(i).Item("master_process") & "' and line = '" & ComboBox1.Text & "'"
                Dim dtCheckProdDOP As DataTable = Database.GetData(queryCheckProdDOP)
                If dtCheckProdDOP.Rows.Count = 0 Then
                    Dim sqlInsertDOP As String = "INSERT INTO prod_dop (po, sub_sub_po, fg_pn, process, line)
                                    VALUES ('" & PO.Text & "','" & SubSubPO.Text & "','" & dtProdDOP.Rows(i).Item("fg_pn") & "','" & dtProdDOP.Rows(i).Item("master_process") & "','" & ComboBox1.Text & "')"
                    Dim cmdInsertDOP = New SqlCommand(sqlInsertDOP, Database.koneksi)
                    cmdInsertDOP.ExecuteNonQuery()
                End If
            Next
        End If
    End Sub

    Sub DGV_MaterialNeed()
        DataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView3.DataSource = Nothing
        DataGridView3.Rows.Clear()
        DataGridView3.Columns.Clear()
        Dim queryMasterFinishGoods As String = "select sp.Sub_Sub_PO,mp.fg_pn FG_Part_Number,mufg.component Component,mufg.description Description,mufg.usage [Usage],sp.sub_sub_po_qty Sub_Sub_Qty,ceiling(( mufg.usage * sp.sub_sub_po_qty ) + ( mufg.usage * sp.sub_sub_po_qty * sp.yield_lose / 100)) AS Total_Need,mp.po,mp.sub_po
        from sub_sub_po sp,main_po mp,material_usage_finish_goods mufg 
        where sp.main_po= mp.id AND mufg.fg_part_number= mp.fg_pn AND sp.status= 'Open' and line = '" & ComboBox1.Text & "' and department='" & globVar.department & "' order by sp.sub_sub_po"
        Dim dtMaterialNeed As DataTable = Database.GetData(queryMasterFinishGoods)

        If dtMaterialNeed.Rows.Count > 0 Then
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

            SubSubPO.Text = dtMaterialNeed.Rows(0).Item("Sub_Sub_PO").ToString
            PO.Text = dtMaterialNeed.Rows(0).Item("PO").ToString
            SubPO.Text = dtMaterialNeed.Rows(0).Item("Sub_PO").ToString
            TextBox3.Text = dtMaterialNeed.Rows(0).Item("FG_Part_Number").ToString
        Else
            MessageBox.Show("Sorry this line not set for Production")
        End If
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
        Dim ds As New DataSet
        Dim Found As Boolean = False
        Dim StringToSearch As String = ""
        Dim CurrentRowIndex As Integer = 0

        Dim splitQRCode() As String = Nothing
        Dim splitQRCode1P() As String = Nothing

        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) And TextBox1.Text <> "" And ComboBox1.Text <> "" Then
            If Len(TextBox1.Text) >= 64 Then
                splitQRCode = TextBox1.Text.Split(New String() {"1P", "12D", "4L", "MLX"}, StringSplitOptions.None)
                splitQRCode1P = splitQRCode(1).Split(New String() {"Q", "S", "13Q", "B"}, StringSplitOptions.None)
            End If

            If DataGridView3.Rows.Count > 0 Then
                For Each gRow As DataGridViewRow In DataGridView3.Rows
                    StringToSearch = gRow.Cells("Component").Value.ToString.Trim.ToLower
                    If InStr(1, StringToSearch, LCase(Trim(splitQRCode1P(0))), vbTextCompare) = 1 Then
                        Dim myCurrentCell As DataGridViewCell = gRow.Cells("Component")
                        DataGridView3.CurrentCell = myCurrentCell
                        CurrentRowIndex = DataGridView3.CurrentRow.Index
                        Found = True
                    End If
                    If Found Then Exit For
                Next
            End If

            If Found = False Then
                MessageBox.Show("Production no need for this Part Number.")
                TextBox1.Text = ""
            Else
                Dim sqlCheckStockMinistore As String = "SELECT * FROM stock_card WHERE material = '" & splitQRCode1P(0) & "' and lot_no=" & splitQRCode1P(3) & " and departement='" & globVar.department & "' and (status='Receive From Main Store' or status='Receive From Production') and actual_qty>0"
                Dim dtCheckStockMinistore As DataTable = Database.GetData(sqlCheckStockMinistore)
                If dtCheckStockMinistore.Rows.Count > 0 Then
                    Dim sqlCheckInProdFreshMaterial As String = "SELECT * FROM stock_card WHERE material = '" & splitQRCode1P(0) & "' and lot_no=" & splitQRCode1P(3) & " and status='Production Request' and departement='" & globVar.department & "' and sub_sub_po='" & SubSubPO.Text & "'"
                    Dim dtCheckInProdFreshMaterial As DataTable = Database.GetData(sqlCheckInProdFreshMaterial)
                    If dtCheckInProdFreshMaterial.Rows.Count > 0 Then
                        MessageBox.Show("Sorry QR Code already in database production")
                        TextBox1.Text = ""
                    Else
                        Dim sqlCheckSumQtyProdcution As String = "SELECT isnull(sum(QTY),0) qty FROM stock_card WHERE sub_sub_po = '" & SubSubPO.Text & "' and material=" & splitQRCode1P(0) & " AND LINE='" & ComboBox1.Text & "' and departement='" & globVar.department & "'"
                        Dim dtCheckSumQtyProdcution As DataTable = Database.GetData(sqlCheckSumQtyProdcution)
                        If dtCheckSumQtyProdcution.Rows(0).Item("qty") > DataGridView3.Rows(CurrentRowIndex).Cells("Total_Need").Value Then
                            MessageBox.Show("Cannot add component because Qty more than Qty Need")
                            TextBox1.Text = ""
                            DGV_InProductionMaterial()
                        Else
                            If dtCheckSumQtyProdcution.Rows(0).Item("qty") + dtCheckStockMinistore.Rows(0).Item("qty") > DataGridView3.Rows(CurrentRowIndex).Cells("Total_Need").Value Then
                                If MessageBox.Show("Qty More than Total Need Production. Are You Sure for Add this Material?", "Important Question", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                                    Try
                                        Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTEMENT,STANDARD_PACK)
                                    VALUES (" & splitQRCode1P(0) & "," & dtCheckStockMinistore.Rows(0).Item("qty") & "," & splitQRCode(2) & "," & splitQRCode1P(2) & "," & splitQRCode1P(3) & ",'" & splitQRCode1P(4) & "','" & PO.Text & "','" & SubSubPO.Text & "'," & DataGridView3.Rows(CurrentRowIndex).Cells("FG_Part_Number").Value & "," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "','" & dtCheckStockMinistore.Rows(0).Item("standard_pack") & "')"
                                        Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                        If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                                            TextBox1.Text = ""
                                            DGV_InProductionMaterial()

                                            Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE material='" & splitQRCode1P(0) & "' and lot_no='" & splitQRCode1P(3) & "' AND DEPARTEMENT='" & globVar.department & "' AND (STATUS='Receive From Main Store' or STATUS='Receive From Production')"
                                            Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                            cmdUpdate.ExecuteNonQuery()

                                            MessageBox.Show("Add Material Success")
                                        End If
                                    Catch ex As Exception
                                        MessageBox.Show("Error Insert" & ex.Message)
                                    End Try
                                Else
                                    TextBox1.Text = ""
                                End If
                            Else
                                Try
                                    Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTEMENT,STANDARD_PACK)
                                    VALUES (" & splitQRCode1P(0) & "," & dtCheckStockMinistore.Rows(0).Item("qty") & "," & splitQRCode(2) & "," & splitQRCode1P(2) & "," & splitQRCode1P(3) & ",'" & splitQRCode1P(4) & "','" & PO.Text & "','" & SubSubPO.Text & "'," & DataGridView3.Rows(CurrentRowIndex).Cells("FG_Part_Number").Value & "," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "','" & dtCheckStockMinistore.Rows(0).Item("standard_pack") & "')"
                                    Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                    If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                                        TextBox1.Text = ""
                                        DGV_InProductionMaterial()

                                        Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE material='" & splitQRCode1P(0) & "' and lot_no='" & splitQRCode1P(3) & "' AND DEPARTEMENT='" & globVar.department & "' AND (STATUS='Receive From Main Store' or STATUS='Receive From Production')"
                                        Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                        cmdUpdate.ExecuteNonQuery()

                                        MessageBox.Show("Add Material Success")
                                    End If
                                Catch ex As Exception
                                    MessageBox.Show("Error Insert" & ex.Message)
                                End Try
                            End If
                        End If
                    End If
                Else
                    MessageBox.Show("This QR Code not available in Stock Ministore. Please goto input stock first")
                    TextBox1.Text = ""
                End If
            End If
        End If
    End Sub

    Sub DGV_InProductionMaterial()
        DataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView4.DataSource = Nothing
        DataGridView4.Rows.Clear()
        DataGridView4.Columns.Clear()
        Call Database.koneksi_database()
        Dim queryInProdMaterial As String = "select in_mat.MATERIAL,in_mat.LOT_NO,in_mat.TRACEABILITY,in_mat.INV_CTRL_DATE,in_mat.BATCH_NO,in_mat.QTY
            from stock_card in_mat, sub_sub_po sp 
            where sp.sub_sub_po=in_mat.sub_sub_po and sp.line = '" & ComboBox1.Text & "' and in_mat.line= '" & ComboBox1.Text & "' AND DEPARTEMENT='" & globVar.department & "'ORDER BY in_mat.DATETIME_INSERT"
        Dim dtInProdMaterial As DataTable = Database.GetData(queryInProdMaterial)

        DataGridView4.DataSource = dtInProdMaterial

        For i As Integer = 0 To DataGridView4.RowCount - 1
            If DataGridView4.Rows(i).Index Mod 2 = 0 Then
                DataGridView4.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView4.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = CheckState.Unchecked Then
            TextBox2.Enabled = True
            TextBox6.Enabled = True
            TextBox1.Enabled = False
            Button1.Enabled = True
        Else
            TextBox2.Enabled = False
            TextBox6.Enabled = False
            TextBox1.Enabled = True
            Button1.Enabled = False
        End If
    End Sub

    Private Sub ProductionRequest_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBox2.Enabled = False
        TextBox6.Enabled = False
        TextBox1.Enabled = True
        Button1.Enabled = False
        ComboBox1.SelectedIndex = -1
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim StringToSearch As String = ""
        Dim CurrentRowIndex As Integer = 0
        Dim Found As Boolean = False

        If DataGridView3.Rows.Count > 0 Then
            For Each gRow As DataGridViewRow In DataGridView3.Rows
                StringToSearch = gRow.Cells("Component").Value.ToString.Trim.ToLower
                If InStr(1, StringToSearch, LCase(Trim(TextBox2.Text)), vbTextCompare) = 1 Then
                    Dim myCurrentCell As DataGridViewCell = gRow.Cells("Component")
                    DataGridView3.CurrentCell = myCurrentCell
                    CurrentRowIndex = DataGridView3.CurrentRow.Index
                    Found = True
                End If
                If Found Then Exit For
            Next
        End If

        If Found = False Then
            MessageBox.Show("Production no need for this Part Number.")
            TextBox1.Text = ""
        Else
            Dim sqlCheckStockMinistore As String = "SELECT * FROM stock_card WHERE material = '" & TextBox2.Text & "' and lot_no=" & TextBox6.Text & " and departement='" & globVar.department & "' and (status='Receive From Main Store' or status='Receive From Production') and actual_qty>0"
            Dim dtCheckStockMinistore As DataTable = Database.GetData(sqlCheckStockMinistore)
            If dtCheckStockMinistore.Rows.Count > 0 Then
                Dim sqlCheckInProdFreshMaterial As String = "SELECT * FROM stock_card WHERE material = '" & TextBox2.Text & "' and lot_no=" & TextBox6.Text & " and status='Production Request' and departement='" & globVar.department & "' and sub_sub_po='" & SubSubPO.Text & "'"
                Dim dtCheckInProdFreshMaterial As DataTable = Database.GetData(sqlCheckInProdFreshMaterial)
                If dtCheckInProdFreshMaterial.Rows.Count > 0 Then
                    MessageBox.Show("Sorry QR Code already in database production")
                    TextBox1.Text = ""
                Else
                    Dim sqlCheckSumQtyProdcution As String = "SELECT isnull(sum(QTY),0) qty FROM stock_card WHERE sub_sub_po = '" & SubSubPO.Text & "' and material=" & TextBox2.Text & " AND LINE='" & ComboBox1.Text & "' and departement='" & globVar.department & "'"
                    Dim dtCheckSumQtyProdcution As DataTable = Database.GetData(sqlCheckSumQtyProdcution)
                    If dtCheckSumQtyProdcution.Rows(0).Item("qty") > DataGridView3.Rows(CurrentRowIndex).Cells("Total_Need").Value Then
                        MessageBox.Show("Cannot add component because Qty more than Qty Need")
                        TextBox1.Text = ""
                        DGV_InProductionMaterial()
                    Else
                        If dtCheckSumQtyProdcution.Rows(0).Item("qty") + dtCheckStockMinistore.Rows(0).Item("qty") > DataGridView3.Rows(CurrentRowIndex).Cells("Total_Need").Value Then
                            If MessageBox.Show("Qty More than Total Need Production. Are You Sure for Add this Material?", "Important Question", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                                Try
                                    Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTEMENT,STANDARD_PACK)
                                    VALUES (" & TextBox2.Text & "," & dtCheckStockMinistore.Rows(0).Item("qty") & "," & dtCheckStockMinistore.Rows(0).Item("INV_CTRL_DATE") & "," & dtCheckStockMinistore.Rows(0).Item("TRACEABILITY") & "," & TextBox6.Text & ",'" & dtCheckStockMinistore.Rows(0).Item("BATCH_NO") & "','" & PO.Text & "','" & SubSubPO.Text & "'," & DataGridView3.Rows(CurrentRowIndex).Cells("FG_Part_Number").Value & "," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "','" & dtCheckStockMinistore.Rows(0).Item("standard_pack") & "')"
                                    Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                    If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                                        TextBox1.Text = ""
                                        DGV_InProductionMaterial()

                                        Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE material='" & TextBox2.Text & "' and lot_no='" & TextBox6.Text & "' AND DEPARTEMENT='" & globVar.department & "' AND (STATUS='Receive From Main Store' or STATUS='Receive From Production')"
                                        Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                        cmdUpdate.ExecuteNonQuery()

                                        MessageBox.Show("Add Material Success")
                                    End If
                                Catch ex As Exception
                                    MessageBox.Show("Error Insert" & ex.Message)
                                End Try
                            Else
                                TextBox1.Text = ""
                            End If
                        Else
                            Try
                                Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTEMENT,STANDARD_PACK)
                                    VALUES (" & TextBox2.Text & "," & dtCheckStockMinistore.Rows(0).Item("qty") & "," & dtCheckStockMinistore.Rows(0).Item("INV_CTRL_DATE") & "," & dtCheckStockMinistore.Rows(0).Item("TRACEABILITY") & "," & TextBox6.Text & ",'" & dtCheckStockMinistore.Rows(0).Item("BATCH_NO") & "','" & PO.Text & "','" & SubSubPO.Text & "'," & DataGridView3.Rows(CurrentRowIndex).Cells("FG_Part_Number").Value & "," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "','" & dtCheckStockMinistore.Rows(0).Item("standard_pack") & "')"
                                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                                    TextBox1.Text = ""
                                    DGV_InProductionMaterial()

                                    Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE material='" & TextBox2.Text & "' and lot_no='" & TextBox6.Text & "' AND DEPARTEMENT='" & globVar.department & "' AND (STATUS='Receive From Main Store' or STATUS='Receive From Production')"
                                    Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                    cmdUpdate.ExecuteNonQuery()

                                    MessageBox.Show("Add Material Success")
                                End If
                            Catch ex As Exception
                                MessageBox.Show("Error Insert" & ex.Message)
                            End Try
                        End If
                    End If
                End If
            Else
                MessageBox.Show("This QR Code not available in Stock Ministore. Please goto input stock first")
                TextBox1.Text = ""
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DGV_MaterialNeed()
        DGV_InProductionMaterial()
    End Sub
End Class