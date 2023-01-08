Imports System.Data.SqlClient
Imports System.Net.Mime.MediaTypeNames
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MdiTabControl

Public Class Production
    Sub tampilDataComboBoxLine()
        Call Database.koneksi_database()
        Dim dtMasterLine As DataTable = Database.GetData("select NAME from MASTER_LINE where DEPARTEMENT='" & globVar.department & "'")

        ComboBox1.DataSource = dtMasterLine
        ComboBox1.DisplayMember = "NAME"
        ComboBox1.ValueMember = "NAME"
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
            If Len(Me.TextBox1.Text) >= 64 Then
                Dim ds As New DataSet
                Dim yieldlose As Integer = 0
                Dim usage As Integer = 0
                Dim splitQRCode() As String = TextBox1.Text.Split(New String() {"1P", "12D", "4L", "MLX"}, StringSplitOptions.None)
                Dim splitQRCode1P() As String = splitQRCode(1).Split(New String() {"Q", "S", "13Q", "B"}, StringSplitOptions.None)

                For i = 0 To DataGridView1.Rows.Count - 1
                    If DataGridView1.Rows(i).Cells(1).Value = splitQRCode1P(0) Then
                        usage = DataGridView1.Rows(i).Cells(2).Value
                    End If
                Next

                yieldlose = Math.Ceiling(usage * TextBox6.Text) + (usage * TextBox6.Text * TextBox8.Text / 100)

                Dim sqlCheckInStock As String = "select * from sub_sub_po sp, stock_card in_material where in_material.SUB_SUB_PO = sp.sub_sub_po and sp.status='Open' and in_material.line='" & ComboBox1.Text & "' and in_material.material = '" & splitQRCode1P(0) & "' and in_material.lot_no=" & splitQRCode1P(3) & " and sp.sub_sub_po='" & TextBox11.Text & "' and in_material.status='Production Request' and departement='" & globVar.department & "'"
                Dim dtCheckInStock As DataTable = Database.GetData(sqlCheckInStock)

                If dtCheckInStock.Rows.Count > 0 Then
                    Dim sqlCheckSumQtyProdcution As String = "SELECT isnull(sum(QTY),0) qty FROM process_prod WHERE sub_sub_po = '" & TextBox11.Text & "' and PN_material=" & splitQRCode1P(0) & " AND LINE='" & ComboBox1.Text & "' and department='" & globVar.department & "'"
                    Dim dtCheckSumQtyProdcution As DataTable = Database.GetData(sqlCheckSumQtyProdcution)
                    If dtCheckSumQtyProdcution.Rows(0).Item("qty") > yieldlose Or dtCheckSumQtyProdcution.Rows(0).Item("qty") + dtCheckInStock.Rows(0).Item("QTY") > yieldlose Then
                        MessageBox.Show("Cannot add component because Qty more than Qty Need")
                        TextBox1.Text = ""
                    Else
                        Dim sqlCheckProductionProcess As String = "select * from process_prod where line='" & ComboBox1.Text & "' and PN_MATERIAL = '" & splitQRCode1P(0) & "' and lot_no=" & splitQRCode1P(3) & " and department='" & globVar.department & "'"
                        Dim dtCheckProductionProcess As DataTable = Database.GetData(sqlCheckProductionProcess)
                        If dtCheckProductionProcess.Rows.Count > 0 Then
                            MessageBox.Show("Double Scan Detect")
                            TextBox1.Text = ""
                        Else
                            Dim sqlProdProcess As String = "INSERT INTO process_prod (id_level, level, pn_material, qty, lot_no, batch_no,traceability,inv_ctrl_date,fifo,line,sub_sub_po,department)
                                    VALUES ('" & splitQRCode1P(0) & "','Fresh','" & splitQRCode1P(0) & "','" & dtCheckInStock.Rows(0).Item("QTY") & "','" & splitQRCode1P(3) & "','" & dtCheckInStock.Rows(0).Item("batch_no") & "','" & dtCheckInStock.Rows(0).Item("traceability") & "','" & dtCheckInStock.Rows(0).Item("inv_ctrl_date") & "',(select COUNT(pn_material)+1 fifo from process_prod where pn_material=" & splitQRCode1P(0) & " and level='Fresh' and sub_sub_po='" & TextBox11.Text & "'),'" & ComboBox1.Text & "','" & TextBox11.Text & "','" & globVar.department & "')"
                            Dim cmdProdProcess = New SqlCommand(sqlProdProcess, Database.koneksi)
                            If cmdProdProcess.ExecuteNonQuery() Then

                                Dim sqlInsertInputStockDetail As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, PO, SUB_SUB_PO, Finish_Goods_PN, ACTUAL_QTY,LINE,SUB_PO,STATUS,DEPARTEMENT,STANDARD_PACK)
                                    VALUES (" & splitQRCode1P(0) & "," & dtCheckInStock.Rows(0).Item("QTY") & "," & dtCheckInStock.Rows(0).Item("INV_CTRL_DATE") & "," & dtCheckInStock.Rows(0).Item("TRACEABILITY") & "," & splitQRCode1P(3) & ",'" & dtCheckInStock.Rows(0).Item("BATCH_NO") & "','" & TextBox5.Text & "','" & TextBox11.Text & "'," & TextBox2.Text & "," & dtCheckInStock.Rows(0).Item("qty") & ",'" & ComboBox1.Text & "','" & TextBox10.Text & "','Production Process','" & globVar.department & "','" & dtCheckInStock.Rows(0).Item("standard_pack") & "')"
                                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                                    TextBox1.Text = ""
                                    DGV_DOC()

                                    Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE material='" & splitQRCode1P(0) & "' and lot_no='" & splitQRCode1P(3) & "' AND DEPARTEMENT='" & globVar.department & "' AND STATUS='Production Request' and sub_sub_po='" & TextBox11.Text & "'"
                                    Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                    cmdUpdate.ExecuteNonQuery()
                                End If

                                For i = 0 To DataGridView1.Rows.Count - 1
                                    If DataGridView1.Rows(i).Cells(1).Value = splitQRCode1P(0) Then
                                        DataGridView1.Rows(i).Cells(3).Selected = True
                                    End If
                                Next
                            End If
                        End If
                    End If
                Else
                    MessageBox.Show("Sorry this material not for this line.")
                    TextBox1.Text = ""
                    TextBox1.Select()
                End If
            Else
                If InStr(TextBox1.Text, "WIP") > 0 Then

                    Dim sqlCheckProductionProcess As String = "select * from process_prod where line='" & ComboBox1.Text & "' and id_level = '" & TextBox1.Text & "' and department='" & globVar.department & "' and sub_sub_po='" & TextBox11.Text & "'"
                    Dim dtCheckProductionProcess As DataTable = Database.GetData(sqlCheckProductionProcess)
                    If dtCheckProductionProcess.Rows.Count > 0 Then
                        MessageBox.Show("Double Scan Detect")
                        TextBox1.Clear()
                        Exit Sub
                    End If

                    Dim sqlCheckStockWIP As String = "select * from STOCK_PROD_WIP where CODE_STOCK_PROD_WIP='" & TextBox1.Text & "' and department='" & globVar.department & "'"
                    Dim dtCheckStockWIP As DataTable = Database.GetData(sqlCheckStockWIP)
                    Dim resultCount As Integer = 0
                    Dim yieldlose As Integer = 0
                    Dim CompExist As String = ""

                    For j = 0 To dtCheckStockWIP.Rows.Count - 1
                        For i = 0 To DataGridView1.Rows.Count - 1
                            If DataGridView1.Rows(i).Cells(1).Value = dtCheckStockWIP.Rows(j).Item("part_number") Then
                                resultCount = resultCount + 1
                                yieldlose = Math.Ceiling(DataGridView1.Rows(i).Cells(2).Value * TextBox6.Text) + (DataGridView1.Rows(i).Cells(2).Value * TextBox6.Text * TextBox8.Text / 100)

                                Dim sqlCheckStock As String = "select isnull(sum(QTY),0) qty from process_prod where SUB_SUB_PO='" & TextBox11.Text & "' and department='" & globVar.department & "' AND pn_MATERIAL=" & dtCheckStockWIP.Rows(j).Item("part_number")
                                Dim dtCheckStock As DataTable = Database.GetData(sqlCheckStock)
                                If dtCheckStock.Rows(0).Item("qty") >= yieldlose Then
                                    CompExist += dtCheckStockWIP.Rows(j).Item("part_number") & " "
                                End If
                            End If
                        Next
                    Next

                    If CompExist <> "" Then
                        MessageBox.Show("Cannot add " & CompExist & " because Qty more than Qty Need")
                        TextBox1.Clear()
                        Exit Sub
                    End If

                    If DataGridView1.Rows.Count >= resultCount Then
                        For j = 0 To dtCheckStockWIP.Rows.Count - 1
                            Dim sqlProdProcess As String = "INSERT INTO process_prod (id_level, level, pn_material, qty, lot_no, batch_no,traceability,inv_ctrl_date,fifo,line,sub_sub_po,department)
                                    VALUES ('" & TextBox1.Text & "','WIP','" & dtCheckStockWIP.Rows(j).Item("part_number") & "','" & dtCheckStockWIP.Rows(j).Item("QTY") & "','" & dtCheckStockWIP.Rows(j).Item("lot_no") & "','" & dtCheckStockWIP.Rows(0).Item("batch_no") & "','" & dtCheckStockWIP.Rows(0).Item("traceability") & "','" & dtCheckStockWIP.Rows(0).Item("inv_ctrl_date") & "',0,'" & ComboBox1.Text & "','" & TextBox11.Text & "','" & globVar.department & "')"
                            Dim cmdProdProcess = New SqlCommand(sqlProdProcess, Database.koneksi)
                            If cmdProdProcess.ExecuteNonQuery() Then
                            End If
                        Next
                        TextBox1.Clear()
                        DGV_DOC()
                    End If

                ElseIf InStr(TextBox1.Text, "SUBASSY") > 0 Then

                    Dim sqlCheckProductionProcess As String = "select * from process_prod where line='" & ComboBox1.Text & "' and id_level = '" & TextBox1.Text & "' and department='" & globVar.department & "' and sub_sub_po='" & TextBox11.Text & "'"
                    Dim dtCheckProductionProcess As DataTable = Database.GetData(sqlCheckProductionProcess)
                    If dtCheckProductionProcess.Rows.Count > 0 Then
                        MessageBox.Show("Double Scan Detect")
                        TextBox1.Clear()
                        Exit Sub
                    End If

                    Dim sqlCheckStockWIP As String = "select * from STOCK_PROD_SUB_ASSY where CODE_STOCK_PROD_SUB_ASSY='" & TextBox1.Text & "' and department='" & globVar.department & "'"
                    Dim dtCheckStockWIP As DataTable = Database.GetData(sqlCheckStockWIP)
                    Dim resultCount As Integer = 0
                    Dim yieldlose As Integer = 0
                    Dim CompExist As String = ""

                    For j = 0 To dtCheckStockWIP.Rows.Count - 1
                        For i = 0 To DataGridView1.Rows.Count - 1
                            If DataGridView1.Rows(i).Cells(1).Value = dtCheckStockWIP.Rows(j).Item("part_number") Then
                                resultCount = resultCount + 1
                                yieldlose = Math.Ceiling(DataGridView1.Rows(i).Cells(2).Value * TextBox6.Text) + (DataGridView1.Rows(i).Cells(2).Value * TextBox6.Text * TextBox8.Text / 100)

                                Dim sqlCheckStock As String = "select isnull(sum(QTY),0) qty from process_prod where SUB_SUB_PO='" & TextBox11.Text & "' and department='" & globVar.department & "' AND pn_MATERIAL=" & dtCheckStockWIP.Rows(j).Item("part_number")
                                Dim dtCheckStock As DataTable = Database.GetData(sqlCheckStock)
                                If dtCheckStock.Rows(0).Item("qty") >= yieldlose Then
                                    CompExist += dtCheckStockWIP.Rows(j).Item("part_number") & " "
                                End If
                            End If
                        Next
                    Next

                    If CompExist <> "" Then
                        MessageBox.Show("Cannot add " & CompExist & " because Qty more than Qty Need")
                        TextBox1.Clear()
                        Exit Sub
                    End If

                    If DataGridView1.Rows.Count >= resultCount Then
                        For j = 0 To dtCheckStockWIP.Rows.Count - 1
                            Dim sqlProdProcess As String = "INSERT INTO process_prod (id_level, level, pn_material, qty, lot_no, batch_no,traceability,inv_ctrl_date,fifo,line,sub_sub_po,department)
                                    VALUES ('" & TextBox1.Text & "','SA','" & dtCheckStockWIP.Rows(j).Item("part_number") & "','" & dtCheckStockWIP.Rows(j).Item("QTY") & "','" & dtCheckStockWIP.Rows(j).Item("lot_no") & "','" & dtCheckStockWIP.Rows(0).Item("batch_no") & "','" & dtCheckStockWIP.Rows(0).Item("traceability") & "','" & dtCheckStockWIP.Rows(0).Item("inv_ctrl_date") & "',0,'" & ComboBox1.Text & "','" & TextBox11.Text & "','" & globVar.department & "')"
                            Dim cmdProdProcess = New SqlCommand(sqlProdProcess, Database.koneksi)
                            If cmdProdProcess.ExecuteNonQuery() Then
                            End If
                        Next
                        TextBox1.Clear()
                        DGV_DOC()
                    End If

                    'MessageBox.Show("Sub Assy Bos")
                ElseIf InStr(TextBox1.Text, "ONHOLD") > 0 Then

                    Dim sqlCheckProductionProcess As String = "select * from process_prod where line='" & ComboBox1.Text & "' and id_level = '" & TextBox1.Text & "' and department='" & globVar.department & "' and sub_sub_po='" & TextBox11.Text & "'"
                    Dim dtCheckProductionProcess As DataTable = Database.GetData(sqlCheckProductionProcess)
                    If dtCheckProductionProcess.Rows.Count > 0 Then
                        MessageBox.Show("Double Scan Detect")
                        TextBox1.Clear()
                        Exit Sub
                    End If

                    Dim sqlCheckStockONHOLD As String = "select * from STOCK_PROD_ONHOLD where CODE_STOCK_PROD_ONHOLD='" & TextBox1.Text & "' and department='" & globVar.department & "'"
                    Dim dtCheckStockONHOLD As DataTable = Database.GetData(sqlCheckStockONHOLD)
                    Dim resultCount As Integer = 0
                    Dim yieldlose As Integer = 0
                    Dim CompExist As String = ""

                    For j = 0 To dtCheckStockONHOLD.Rows.Count - 1
                        For i = 0 To DataGridView1.Rows.Count - 1
                            If DataGridView1.Rows(i).Cells(1).Value = dtCheckStockONHOLD.Rows(j).Item("part_number") Then
                                resultCount = resultCount + 1
                                yieldlose = Math.Ceiling(DataGridView1.Rows(i).Cells(2).Value * TextBox6.Text) + (DataGridView1.Rows(i).Cells(2).Value * TextBox6.Text * TextBox8.Text / 100)

                                Dim sqlCheckStock As String = "select isnull(sum(QTY),0) qty from process_prod where SUB_SUB_PO='" & TextBox11.Text & "' and department='" & globVar.department & "' AND pn_MATERIAL=" & dtCheckStockONHOLD.Rows(j).Item("part_number")
                                Dim dtCheckStock As DataTable = Database.GetData(sqlCheckStock)
                                If dtCheckStock.Rows(0).Item("qty") >= yieldlose Then
                                    CompExist += dtCheckStockONHOLD.Rows(j).Item("part_number") & " "
                                End If
                            End If
                        Next
                    Next

                    If CompExist <> "" Then
                        MessageBox.Show("Cannot add " & CompExist & " because Qty more than Qty Need")
                        TextBox1.Clear()
                        Exit Sub
                    End If

                    If DataGridView1.Rows.Count >= resultCount Then
                        For j = 0 To dtCheckStockONHOLD.Rows.Count - 1
                            Dim sqlProdProcess As String = "INSERT INTO process_prod (id_level, level, pn_material, qty, lot_no, batch_no,traceability,inv_ctrl_date,fifo,line,sub_sub_po,department)
                                    VALUES ('" & TextBox1.Text & "','OH','" & dtCheckStockONHOLD.Rows(j).Item("part_number") & "','" & dtCheckStockONHOLD.Rows(j).Item("QTY") & "','" & dtCheckStockONHOLD.Rows(j).Item("lot_no") & "','" & dtCheckStockONHOLD.Rows(0).Item("batch_no") & "','" & dtCheckStockONHOLD.Rows(0).Item("traceability") & "','" & dtCheckStockONHOLD.Rows(0).Item("inv_ctrl_date") & "',0,'" & ComboBox1.Text & "','" & TextBox11.Text & "','" & globVar.department & "')"
                            Dim cmdProdProcess = New SqlCommand(sqlProdProcess, Database.koneksi)
                            If cmdProdProcess.ExecuteNonQuery() Then
                            End If
                        Next
                        TextBox1.Clear()
                        DGV_DOC()
                    End If

                ElseIf InStr(TextBox1.Text, "OTHERS") > 0 Then
                    'MessageBox.Show("Others Bos")

                    Dim sqlCheckProductionProcess As String = "select * from process_prod where line='" & ComboBox1.Text & "' and id_level = '" & TextBox1.Text & "' and department='" & globVar.department & "' and sub_sub_po='" & TextBox11.Text & "'"
                    Dim dtCheckProductionProcess As DataTable = Database.GetData(sqlCheckProductionProcess)
                    If dtCheckProductionProcess.Rows.Count > 0 Then
                        MessageBox.Show("Double Scan Detect")
                        TextBox1.Clear()
                        Exit Sub
                    End If

                    Dim sqlCheckStockONHOLD As String = "select * from STOCK_PROD_OTHERS where CODE_STOCK_PROD_OTHERS='" & TextBox1.Text & "' and department='" & globVar.department & "'"
                    Dim dtCheckStockONHOLD As DataTable = Database.GetData(sqlCheckStockONHOLD)
                    Dim resultCount As Integer = 0
                    Dim yieldlose As Integer = 0
                    Dim CompExist As String = ""

                    For j = 0 To dtCheckStockONHOLD.Rows.Count - 1
                        For i = 0 To DataGridView1.Rows.Count - 1
                            If DataGridView1.Rows(i).Cells(1).Value = dtCheckStockONHOLD.Rows(j).Item("part_number") Then
                                resultCount = resultCount + 1
                                yieldlose = Math.Ceiling(DataGridView1.Rows(i).Cells(2).Value * TextBox6.Text) + (DataGridView1.Rows(i).Cells(2).Value * TextBox6.Text * TextBox8.Text / 100)

                                Dim sqlCheckStock As String = "select isnull(sum(QTY),0) qty from process_prod where SUB_SUB_PO='" & TextBox11.Text & "' and department='" & globVar.department & "' AND pn_MATERIAL=" & dtCheckStockONHOLD.Rows(j).Item("part_number")
                                Dim dtCheckStock As DataTable = Database.GetData(sqlCheckStock)
                                If dtCheckStock.Rows(0).Item("qty") >= yieldlose Then
                                    CompExist += dtCheckStockONHOLD.Rows(j).Item("part_number") & " "
                                End If
                            End If
                        Next
                    Next

                    If CompExist <> "" Then
                        MessageBox.Show("Cannot add " & CompExist & " because Qty more than Qty Need")
                        TextBox1.Clear()
                        Exit Sub
                    End If

                    If DataGridView1.Rows.Count >= resultCount Then
                        For j = 0 To dtCheckStockONHOLD.Rows.Count - 1
                            Dim sqlProdProcess As String = "INSERT INTO process_prod (id_level, level, pn_material, qty, lot_no, batch_no,traceability,inv_ctrl_date,fifo,line,sub_sub_po,department)
                                    VALUES ('" & TextBox1.Text & "','OT','" & dtCheckStockONHOLD.Rows(j).Item("part_number") & "','" & dtCheckStockONHOLD.Rows(j).Item("QTY") & "','" & dtCheckStockONHOLD.Rows(j).Item("lot_no") & "','" & dtCheckStockONHOLD.Rows(0).Item("batch_no") & "','" & dtCheckStockONHOLD.Rows(0).Item("traceability") & "','" & dtCheckStockONHOLD.Rows(0).Item("inv_ctrl_date") & "',0,'" & ComboBox1.Text & "','" & TextBox11.Text & "','" & globVar.department & "')"
                            Dim cmdProdProcess = New SqlCommand(sqlProdProcess, Database.koneksi)
                            If cmdProdProcess.ExecuteNonQuery() Then
                            End If
                        Next
                        TextBox1.Clear()
                        DGV_DOC()
                    End If
                End If
            End If
        End If
    End Sub

    Sub DGV_DOC()
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
            Dim queryCheck As String = "select lot_no,level from process_prod where pn_material=" & dtDOC.Rows(rowDataSet).Item("Component").ToString & " and line='" & ComboBox1.Text & "'"
            Dim dtCHECK As DataTable = Database.GetData(queryCheck)
            For i As Integer = 0 To dtCHECK.Rows.Count - 1
                If dtCHECK.Rows(i).Item("level").ToString = "Fresh" Then
                    DataGridView1.Rows(rowDataSet).Cells(3).Value += dtCHECK.Rows(i).Item("lot_no").ToString & ","
                Else
                    DataGridView1.Rows(rowDataSet).Cells(3).Value += dtCHECK.Rows(i).Item("lot_no").ToString & "(" & dtCHECK.Rows(i).Item("level").ToString & "),"
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
    End Sub

    Sub DGV_DOP()
        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView2.DataSource = Nothing
        DataGridView2.Rows.Clear()
        DataGridView2.Columns.Clear()
        Call Database.koneksi_database()
        Dim queryDOP As String = "select Process, operator_id Operator from prod_dop where line='" & ComboBox1.Text & "' and sub_sub_po='" & TextBox11.Text & "' and department='" & globVar.department & "'"
        Dim dtDOP As DataTable = Database.GetData(queryDOP)

        DataGridView2.DataSource = dtDOP

        For i As Integer = 0 To DataGridView2.RowCount - 1
            If DataGridView2.Rows(i).Index Mod 2 = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub Production_Load(sender As Object, e As EventArgs) Handles Me.Load
        tampilDataComboBoxLine()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
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
    End Sub
End Class