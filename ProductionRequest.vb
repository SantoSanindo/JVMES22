Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Data.SqlClient

Public Class ProductionRequest
    Public Shared menu As String = "Production Request"

    Sub DGV_MaterialNeed()
        Try
            DataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView3.DataSource = Nothing
            DataGridView3.Rows.Clear()
            DataGridView3.Columns.Clear()

            Dim queryMasterFinishGoods As String = "select sp.Sub_Sub_PO,mp.fg_pn FG_Part_Number,mufg.component Component,mufg.description Description,mufg.usage [Usage],sp.sub_sub_po_qty Sub_Sub_Qty,ceiling(( mufg.usage * sp.sub_sub_po_qty ) + ( mufg.usage * sp.sub_sub_po_qty * sp.yield_lose / 100)) AS Total_Need,mp.po,mp.sub_po
                from sub_sub_po sp,main_po mp,material_usage_finish_goods mufg 
                where sp.main_po= mp.id AND mufg.fg_part_number= mp.fg_pn AND sp.status= 'Open' and sp.line = '" & ComboBox1.Text & "' and mp.department='" & globVar.department & "' order by sp.sub_sub_po"
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
                RJMessageBox.Show("Sorry this line not set for Production")
            End If
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
        Try
            Dim ds As New DataSet
            Dim Found As Boolean = False
            Dim StringToSearch As String = ""
            Dim CurrentRowIndex As Integer = 0

            If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) And TextBox1.Text <> "" And ComboBox1.Text <> "" Then
                If Len(TextBox1.Text) >= 64 Then
                    QRCode.Baca(TextBox1.Text)
                Else
                    Dim SplitLabel = TextBox1.Text.Split("-")
                    globVar.QRCode_PN = SplitLabel(0)
                    globVar.QRCode_lot = SplitLabel(1) & "-" & SplitLabel(2)
                End If

                If DataGridView3.Rows.Count > 0 Then
                    For Each gRow As DataGridViewRow In DataGridView3.Rows
                        StringToSearch = gRow.Cells("Component").Value.ToString.Trim.ToLower
                        If InStr(1, StringToSearch, LCase(Trim(globVar.QRCode_PN)), vbTextCompare) = 1 Then
                            Dim myCurrentCell As DataGridViewCell = gRow.Cells("Component")
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
                    Dim sqlCheckStockMinistore As String = "SELECT * FROM stock_card WHERE material = '" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' and department='" & globVar.department & "' and (status='Receive From Main Store' or status='Receive From Production') and actual_qty>0 and [save]=1"
                    Dim dtCheckStockMinistore As DataTable = Database.GetData(sqlCheckStockMinistore)
                    If dtCheckStockMinistore.Rows.Count > 0 Then
                        'If dtCheckStockMinistore.Rows(0).Item("split_material") = 1 Then
                        '    RJMessageBox.Show("Cannot using this QR Code. Please use QR Code Split Qty.")
                        '    Exit Sub
                        'End If
                        Dim sqlCheckInProdFreshMaterial As String = "SELECT * FROM stock_card WHERE material = '" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' and status='Production Request' and department='" & globVar.department & "' and sub_sub_po='" & SubSubPO.Text & "'"
                        Dim dtCheckInProdFreshMaterial As DataTable = Database.GetData(sqlCheckInProdFreshMaterial)
                        If dtCheckInProdFreshMaterial.Rows.Count > 0 Then
                            RJMessageBox.Show("Sorry QR Code already in database production")
                            TextBox1.Text = ""
                        Else
                            Dim sqlCheckSumQtyProdcution As String = "SELECT isnull(sum(sum_qty),0) qty FROM stock_card WHERE sub_sub_po = '" & SubSubPO.Text & "' and material='" & globVar.QRCode_PN & "' AND LINE='" & ComboBox1.Text & "' and department='" & globVar.department & "' and status='Production Request'"
                            Dim dtCheckSumQtyProdcution As DataTable = Database.GetData(sqlCheckSumQtyProdcution)
                            If dtCheckSumQtyProdcution.Rows(0).Item("qty") >= DataGridView3.Rows(CurrentRowIndex).Cells("Total_Need").Value Then
                                RJMessageBox.Show("Cannot add component because Qty more than Qty Need")
                                TextBox1.Text = ""
                                DGV_InProductionMaterial()
                            Else
                                If dtCheckSumQtyProdcution.Rows(0).Item("qty") + dtCheckStockMinistore.Rows(0).Item("qty") > DataGridView3.Rows(CurrentRowIndex).Cells("Total_Need").Value Then
                                    Try
                                        Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,QRCODE,MTS_NO,SUM_QTY,LEVEL,ID_LEVEL)
                                                VALUES ('" & globVar.QRCode_PN & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & dtCheckStockMinistore.Rows(0).Item("inv_ctrl_date") & "','" & dtCheckStockMinistore.Rows(0).Item("traceability") & "','" & globVar.QRCode_lot & "','" & dtCheckStockMinistore.Rows(0).Item("batch_no") & "','" & PO.Text & "','" & SubSubPO.Text & "'," & DataGridView3.Rows(CurrentRowIndex).Cells("FG_Part_Number").Value & "," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "','" & dtCheckStockMinistore.Rows(0).Item("standard_pack") & "','" & dtCheckStockMinistore.Rows(0).Item("qrcode") & "','" & dtCheckStockMinistore.Rows(0).Item("mts_no") & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'Fresh','" & globVar.QRCode_PN & "')"
                                        Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                        If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                                            DGV_InProductionMaterial()

                                            Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE material='" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' AND DEPARTMENT='" & globVar.department & "' AND (STATUS='Receive From Main Store' or STATUS='Receive From Production')"
                                            Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                            cmdUpdate.ExecuteNonQuery()
                                            TextBox1.Clear()
                                        End If
                                    Catch ex As Exception
                                        RJMessageBox.Show("Error Insert" & ex.Message)
                                    End Try
                                Else
                                    Try
                                        Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,QRCODE,MTS_NO,SUM_QTY,LEVEL,ID_LEVEL)
                                    VALUES ('" & globVar.QRCode_PN & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & dtCheckStockMinistore.Rows(0).Item("inv_ctrl_date") & "','" & dtCheckStockMinistore.Rows(0).Item("traceability") & "','" & globVar.QRCode_lot & "','" & dtCheckStockMinistore.Rows(0).Item("batch_no") & "','" & PO.Text & "','" & SubSubPO.Text & "'," & DataGridView3.Rows(CurrentRowIndex).Cells("FG_Part_Number").Value & "," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "','" & dtCheckStockMinistore.Rows(0).Item("standard_pack") & "','" & dtCheckStockMinistore.Rows(0).Item("qrcode") & "','" & dtCheckStockMinistore.Rows(0).Item("mts_no") & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'Fresh','" & globVar.QRCode_PN & "')"
                                        Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                        If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                                            DGV_InProductionMaterial()

                                            Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE material='" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' AND DEPARTMENT='" & globVar.department & "' AND (STATUS='Receive From Main Store' or STATUS='Receive From Production')"
                                            Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                            cmdUpdate.ExecuteNonQuery()
                                            TextBox1.Clear()
                                        End If
                                    Catch ex As Exception
                                        RJMessageBox.Show("Error Insert" & ex.Message)
                                    End Try
                                End If
                            End If
                        End If
                    Else
                        RJMessageBox.Show("This QR Code not available in Stock Ministore. Please goto input stock first or Qty = 0")
                        TextBox1.Text = ""
                    End If
                End If
            End If
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub DGV_InProductionMaterial()
        Try
            DataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView4.DataSource = Nothing
            DataGridView4.Rows.Clear()
            DataGridView4.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryInProdMaterial As String = "select in_mat.MATERIAL,in_mat.LOT_NO,in_mat.TRACEABILITY,in_mat.INV_CTRL_DATE,in_mat.BATCH_NO,in_mat.QTY,in_mat.SUM_QTY [Actual Qty]
            from stock_card in_mat, sub_sub_po sp 
            where sp.sub_sub_po=in_mat.sub_sub_po and sp.line = '" & ComboBox1.Text & "' and in_mat.line= '" & ComboBox1.Text & "' and sp.sub_sub_po='" & SubSubPO.Text & "' and in_mat.sub_sub_po='" & SubSubPO.Text & "' AND DEPARTMENT='" & globVar.department & "' and in_mat.[status]='Production Request' ORDER BY in_mat.DATETIME_INSERT"
            Dim dtInProdMaterial As DataTable = Database.GetData(queryInProdMaterial)

            DataGridView4.DataSource = dtInProdMaterial

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
        tampilDataComboBoxLine()
    End Sub

    Sub tampilDataComboBoxLine()
        Call Database.koneksi_database()
        Dim dtMasterLine As DataTable = Database.GetData("select * from master_line where department='" & globVar.department & "'")

        ComboBox1.DataSource = dtMasterLine
        ComboBox1.DisplayMember = "name"
        ComboBox1.ValueMember = "name"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try


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
                RJMessageBox.Show("Production no need for this Part Number.")
                TextBox1.Text = ""
            Else
                Dim sqlCheckStockMinistore As String = "SELECT * FROM stock_card WHERE material = '" & TextBox2.Text & "' and lot_no='" & TextBox6.Text & "' and department='" & globVar.department & "' and (status='Receive From Main Store' or status='Receive From Production') and actual_qty>0"
                Dim dtCheckStockMinistore As DataTable = Database.GetData(sqlCheckStockMinistore)
                If dtCheckStockMinistore.Rows.Count > 0 Then
                    Dim sqlCheckInProdFreshMaterial As String = "SELECT * FROM stock_card WHERE material = '" & TextBox2.Text & "' and lot_no='" & TextBox6.Text & "' and status='Production Request' and department='" & globVar.department & "' and sub_sub_po='" & SubSubPO.Text & "'"
                    Dim dtCheckInProdFreshMaterial As DataTable = Database.GetData(sqlCheckInProdFreshMaterial)
                    If dtCheckInProdFreshMaterial.Rows.Count > 0 Then
                        RJMessageBox.Show("Sorry QR Code already in database production")
                        TextBox1.Text = ""
                    Else
                        Dim sqlCheckSumQtyProdcution As String = "SELECT isnull(sum(QTY),0) qty FROM stock_card WHERE sub_sub_po = '" & SubSubPO.Text & "' and material='" & TextBox2.Text & "' AND LINE='" & ComboBox1.Text & "' and department='" & globVar.department & "' and status='Production Request'"
                        Dim dtCheckSumQtyProdcution As DataTable = Database.GetData(sqlCheckSumQtyProdcution)
                        If dtCheckSumQtyProdcution.Rows(0).Item("qty") > DataGridView3.Rows(CurrentRowIndex).Cells("Total_Need").Value Then
                            RJMessageBox.Show("Cannot add component because Qty more than Qty Need")
                            TextBox1.Text = ""
                            DGV_InProductionMaterial()
                        Else
                            If dtCheckSumQtyProdcution.Rows(0).Item("qty") + dtCheckStockMinistore.Rows(0).Item("qty") > DataGridView3.Rows(CurrentRowIndex).Cells("Total_Need").Value Then
                                Try
                                    Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTMENT,STANDARD_PACK,QRCODE,MTS_NO,SUM_QTY,LEVEL,ID_LEVEL)
                                    VALUES ('" & TextBox2.Text & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & dtCheckStockMinistore.Rows(0).Item("INV_CTRL_DATE") & "','" & dtCheckStockMinistore.Rows(0).Item("TRACEABILITY") & "','" & TextBox6.Text & "','" & dtCheckStockMinistore.Rows(0).Item("BATCH_NO") & "','" & PO.Text & "','" & SubSubPO.Text & "'," & DataGridView3.Rows(CurrentRowIndex).Cells("FG_Part_Number").Value & "," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "','" & dtCheckStockMinistore.Rows(0).Item("standard_pack") & "','Manual Input','" & dtCheckStockMinistore.Rows(0).Item("mts_no") & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'Fresh','" & TextBox2.Text & "')"
                                    Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)

                                    If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                                        TextBox2.Text = ""
                                        TextBox6.Text = ""
                                        DGV_InProductionMaterial()

                                        Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE material='" & TextBox2.Text & "' and lot_no='" & TextBox6.Text & "' AND DEPARTMENT='" & globVar.department & "' AND (STATUS='Receive From Main Store' or STATUS='Receive From Production')"
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
                                    VALUES ('" & TextBox2.Text & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & dtCheckStockMinistore.Rows(0).Item("INV_CTRL_DATE") & "','" & dtCheckStockMinistore.Rows(0).Item("TRACEABILITY") & "','" & TextBox6.Text & "','" & dtCheckStockMinistore.Rows(0).Item("BATCH_NO") & "','" & PO.Text & "','" & SubSubPO.Text & "'," & DataGridView3.Rows(CurrentRowIndex).Cells("FG_Part_Number").Value & "," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'" & ComboBox1.Text & "','" & SubPO.Text & "','Production Request','" & globVar.department & "','" & dtCheckStockMinistore.Rows(0).Item("standard_pack") & "','Manual Input','" & dtCheckStockMinistore.Rows(0).Item("mts_no") & "'," & dtCheckStockMinistore.Rows(0).Item("qty") & ",'Fresh','" & TextBox2.Text & "')"
                                    Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                    If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                                        TextBox2.Text = ""
                                        TextBox6.Text = ""
                                        DGV_InProductionMaterial()

                                        Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE material='" & TextBox2.Text & "' and lot_no='" & TextBox6.Text & "' AND DEPARTMENT='" & globVar.department & "' AND (STATUS='Receive From Main Store' or STATUS='Receive From Production')"
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
                    TextBox6.Text = ""
                End If
            End If
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DGV_MaterialNeed()
        DGV_InProductionMaterial()
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
End Class