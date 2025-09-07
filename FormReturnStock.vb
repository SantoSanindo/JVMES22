Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar

Public Class FormReturnStock
    Public Shared menu As String = "Return To Main Store"

    Private Sub FormInputStock_Load(sender As Object, e As EventArgs) Handles Me.Load
        If globVar.view > 0 Then

            txt_forminputstock_qrcode.Enabled = False
            txtmanualPN.Enabled = False

            TreeView1.Nodes.Clear()
            dgv_forminputstock.DataSource = Nothing
            dgv_forminputstock.Columns.Clear()
            dgv_forminputstock.Rows.Clear()

            dgv_forminputstock.ReadOnly = False

            Button2.Enabled = False

            Unlock.Enabled = False

            checkQr.Enabled = False

            If globVar.hakAkses.Contains("Administrator") Then
                Unlock.Visible = True
            Else
                Unlock.Visible = False
            End If

        End If
    End Sub

    Private Sub DGV_InputStock(id As String)
        If id = "" Then
            Exit Sub
        End If
        dgv_forminputstock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_forminputstock.DataSource = Nothing
        dgv_forminputstock.Rows.Clear()
        dgv_forminputstock.Columns.Clear()
        Call Database.koneksi_database()
        Dim queryInputStockDetail As String = "SELECT ID [#],MATERIAL [Material],LOT_NO [Lot],TRACEABILITY [Trace],BATCH_NO [Batch],INV_CTRL_DATE [ICD],QTY [Qty], CASE 
                                                        WHEN qrcode_new is not null
		                                                THEN qrcode_new COLLATE SQL_Latin1_General_CP1_CI_AS 
		                                                ELSE qrcode COLLATE SQL_Latin1_General_CP1_CI_AS  
                                                    END AS QRCode, RETURN_MAINSTORE_DATETIME [Date Time], RETURN_MAINSTORE_WHO [Return By] FROM STOCK_CARD WHERE MTS_NO='" & txt_forminputstock_mts_no.Text & "' and MATERIAL='" & id & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Return To Main Store'"
        Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
        dgv_forminputstock.DataSource = dtInputStockDetail

        Dim queryCheckLock As String = "SELECT TOP 1 * FROM STOCK_CARD WHERE MTS_NO = '" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Return To Main Store'"
        Dim dtCheckLock As DataTable = Database.GetData(queryCheckLock)

        If dtCheckLock.Rows.Count > 0 Then
            If dtCheckLock.Rows(0).Item("SAVE") = 0 Then
                Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
                delete.Name = "delete"
                delete.HeaderText = "Delete"
                delete.Width = 50
                delete.Text = "Delete"
                delete.UseColumnTextForButtonValue = True

                dgv_forminputstock.Columns.Insert(10, delete)

                Button2.Enabled = True
            End If
        Else
            Button2.Enabled = True
        End If

        dgv_forminputstock.Columns(0).Width = 100
        dgv_forminputstock.Columns(2).Width = 150

        For i As Integer = 0 To dgv_forminputstock.RowCount - 1
            If dgv_forminputstock.Rows(i).Index Mod 2 = 0 Then
                dgv_forminputstock.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_forminputstock.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        If dtCheckLock.Rows.Count > 0 Then
            dgv_forminputstock.ReadOnly = dtCheckLock.Rows(0).Item("SAVE")
        Else
            dgv_forminputstock.ReadOnly = False
        End If
    End Sub

    Private Sub txt_forminputstock_qrcode_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_forminputstock_qrcode.PreviewKeyDown
        Dim QrcodeValid As Boolean
        Call Database.koneksi_database()

        Dim adapter As SqlDataAdapter
        Dim ds As New DataTable
        If globVar.add = 0 Then
            RJMessageBox.Show("Your Access cannot execute this action")
            Exit Sub
        End If

        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then

            If txt_forminputstock_qrcode.Text.StartsWith("B") Then

                Dim sqlCheckBalance As String = "select * from stock_card where qrcode = '" & txt_forminputstock_qrcode.Text & "' AND (STATUS='Receive From Production' or (STATUS='Receive From Main Store' AND [SAVE]=1)) and actual_qty > 0 and department='" & globVar.department & "'"
                Dim dtCheckBalance As DataTable = Database.GetData(sqlCheckBalance)
                If dtCheckBalance.Rows.Count > 0 Then

                    globVar.QRCode_PN = dtCheckBalance.Rows(0).Item("material")
                    globVar.QRCode_lot = dtCheckBalance.Rows(0).Item("lot_no")
                    Dim queryCheckInputStockDetail As String = "SELECT * FROM STOCK_CARD where qrcode='" & txt_forminputstock_qrcode.Text & "' and lot_no='" & dtCheckBalance.Rows(0).Item("lot_no") & "' AND MATERIAL='" & dtCheckBalance.Rows(0).Item("material") & "' and mts_no='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' AND status='Return To Main Store'"
                    Dim dtCheckInputStockDetail As DataTable = Database.GetData(queryCheckInputStockDetail)

                    If dtCheckInputStockDetail.Rows.Count > 0 Then
                        RJMessageBox.Show("This QRCode Already Scan")

                        txt_forminputstock_qrcode.Text = ""
                        txt_forminputstock_qrcode.Select()

                        treeView_show()
                    Else
                        Try
                            Dim sqlInsertInputStockDetail As String = "INSERT INTO STOCK_CARD (ID_LEVEL,LEVEL,QRCODE, MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, MTS_NO,DEPARTMENT, STANDARD_PACK,STATUS,ACTUAL_QTY,RETURN_MAINSTORE_DATETIME,RETURN_MAINSTORE_WHO)
                                    VALUES ('" & dtCheckBalance.Rows(0).Item("level") & "','" & dtCheckBalance.Rows(0).Item("id_level") & "','" & txt_forminputstock_qrcode.Text & "'," & dtCheckBalance.Rows(0).Item("material") & ",'" & dtCheckBalance.Rows(0).Item("actual_qty") & "','" & dtCheckBalance.Rows(0).Item("inv_ctrl_date") & "','" & dtCheckBalance.Rows(0).Item("traceability") & "','" & dtCheckBalance.Rows(0).Item("lot_no") & "','" & dtCheckBalance.Rows(0).Item("batch_no") & "'," & txt_forminputstock_mts_no.Text & ",'" & globVar.department & "','" & dtCheckBalance.Rows(0).Item("STANDARD_PACK") & "','Return To Main Store'," & dtCheckBalance.Rows(0).Item("actual_qty") & ",getdate(),'" & globVar.username & "')"
                            Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                            If cmdInsertInputStockDetail.ExecuteNonQuery() Then

                                Dim SqlUpdateQty As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE id=" & dtCheckBalance.Rows(0).Item("id")
                                Dim cmdUpdateQty = New SqlCommand(SqlUpdateQty, Database.koneksi)
                                If cmdUpdateQty.ExecuteNonQuery() Then
                                    txt_forminputstock_qrcode.Text = ""
                                    txt_forminputstock_qrcode.Select()

                                    dgv_forminputstock.DataSource = Nothing
                                    dgv_forminputstock.Rows.Clear()
                                    dgv_forminputstock.Columns.Clear()
                                End If
                                treeView_show()
                            End If
                        Catch ex As Exception
                            RJMessageBox.Show("Error Return Stock - 2 =>" & ex.Message)
                        End Try
                    End If
                Else
                    RJMessageBox.Show("Sorry this material qty is zero or this material already return.")
                    txt_forminputstock_qrcode.Text = ""
                    txt_forminputstock_qrcode.Select()
                End If

            ElseIf txt_forminputstock_qrcode.Text.StartsWith("MX2D") Then

                QrcodeValid = QRCode.Baca(txt_forminputstock_qrcode.Text)

                If QrcodeValid = False Then
                    RJMessageBox.Show("QRCode Not Valid")
                    Play_Sound.Wrong()
                    txt_forminputstock_qrcode.Clear()
                    Exit Sub
                End If

                If globVar.QRCode_PN = "" Or globVar.QRCode_lot = "" Or globVar.QRCode_Traceability = "" Or globVar.QRCode_Batch = "" Or globVar.QRCode_Inv = "" Then
                    RJMessageBox.Show("QRCode Not Valid")
                    Play_Sound.Wrong()
                    txt_forminputstock_qrcode.Clear()
                    Exit Sub
                End If

                Dim sql As String = "SELECT * FROM STOCK_CARD where qrcode='" & txt_forminputstock_qrcode.Text & "' and MATERIAL='" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' AND (STATUS='Receive From Production' or (STATUS='Receive From Main Store' AND [SAVE]=1)) and department='" & globVar.department & "' and actual_qty>0"
                adapter = New SqlDataAdapter(sql, Database.koneksi)
                adapter.Fill(ds)

                If ds.Rows.Count > 0 Then

                    Dim queryCheckInputStockDetail As String = "SELECT * FROM STOCK_CARD where qrcode='" & txt_forminputstock_qrcode.Text & "' and lot_no='" & ds.Rows(0).Item("lot_no") & "' AND MATERIAL='" & ds.Rows(0).Item("material") & "' and mts_no='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' AND status='Return To Main Store'"
                    Dim dtCheckInputStockDetail As DataTable = Database.GetData(queryCheckInputStockDetail)

                    If dtCheckInputStockDetail.Rows.Count > 0 Then
                        RJMessageBox.Show("This QRCode Already Scan")

                        txt_forminputstock_qrcode.Text = ""
                        txt_forminputstock_qrcode.Select()

                        dgv_forminputstock.DataSource = Nothing
                        dgv_forminputstock.Rows.Clear()
                        dgv_forminputstock.Columns.Clear()

                        treeView_show()
                    Else
                        Try
                            Dim sqlInsertInputStockDetail As String = "INSERT INTO STOCK_CARD (qrcode,MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, MTS_NO,DEPARTMENT, STANDARD_PACK,STATUS,ACTUAL_QTY,RETURN_MAINSTORE_DATETIME,RETURN_MAINSTORE_WHO)
                                    VALUES ('" & txt_forminputstock_qrcode.Text & "'," & ds.Rows(0).Item("material") & ",'" & ds.Rows(0).Item("actual_qty") & "','" & ds.Rows(0).Item("inv_ctrl_date") & "','" & ds.Rows(0).Item("traceability") & "','" & ds.Rows(0).Item("lot_no") & "','" & ds.Rows(0).Item("batch_no") & "'," & txt_forminputstock_mts_no.Text & ",'" & globVar.department & "','" & ds.Rows(0).Item("STANDARD_PACK") & "','Return To Main Store'," & ds.Rows(0).Item("actual_qty") & ",getdate(),'" & globVar.username & "')"
                            Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                            If cmdInsertInputStockDetail.ExecuteNonQuery() Then

                                Dim SqlUpdateQty As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE id=" & ds.Rows(0).Item("id")
                                Dim cmdUpdateQty = New SqlCommand(SqlUpdateQty, Database.koneksi)
                                If cmdUpdateQty.ExecuteNonQuery() Then
                                    txt_forminputstock_qrcode.Text = ""
                                    txt_forminputstock_qrcode.Select()

                                    dgv_forminputstock.DataSource = Nothing
                                    dgv_forminputstock.Rows.Clear()
                                    dgv_forminputstock.Columns.Clear()
                                End If
                                treeView_show()
                            End If
                        Catch ex As Exception
                            RJMessageBox.Show("Error Return Stock - 1 =>" & ex.Message)
                        End Try
                    End If
                Else
                    RJMessageBox.Show("Sorry this material qty is zero or this material already return.")
                    txt_forminputstock_qrcode.Text = ""
                    txt_forminputstock_qrcode.Select()
                End If

            ElseIf txt_forminputstock_qrcode.Text.StartsWith("NQ") Then

                Dim sqlCheckStockNQ = "SELECT * FROM new_label WHERE qrcode = '" & txt_forminputstock_qrcode.Text & "'"
                Dim dtCheckStockNQ As DataTable = Database.GetData(sqlCheckStockNQ)

                If dtCheckStockNQ.Rows.Count = 0 Then
                    RJMessageBox.Show("Material doesn't exist in Database")
                    Play_Sound.Wrong()
                    txt_forminputstock_qrcode.Clear()
                    Exit Sub
                End If

                globVar.QRCode_PN = dtCheckStockNQ.Rows(0).Item("material")
                globVar.QRCode_lot = dtCheckStockNQ.Rows(0).Item("lot_no")
                globVar.QRCode_Inv = dtCheckStockNQ.Rows(0).Item("inv_ctrl_date")
                globVar.QRCode_Traceability = dtCheckStockNQ.Rows(0).Item("traceability")
                globVar.QRCode_Batch = dtCheckStockNQ.Rows(0).Item("batch_no")

                If dtCheckStockNQ.Rows(0).Item("qty") <= 0 Then
                    RJMessageBox.Show("Qty this material is 0")
                    Play_Sound.Wrong()
                    txt_forminputstock_qrcode.Clear()
                    Exit Sub
                End If

                Dim sqlCheckStockMinistore = "SELECT * FROM stock_card WHERE department='" & globVar.department & "' and (STATUS='Receive From Production' or (STATUS='Receive From Main Store' AND [SAVE]=1)) and actual_qty > 0 and ( qrcode_new='" & txt_forminputstock_qrcode.Text & "' or qrcode = '" & txt_forminputstock_qrcode.Text & "')"
                adapter = New SqlDataAdapter(sqlCheckStockMinistore, Database.koneksi)
                adapter.Fill(ds)

                If ds.Rows.Count = 0 Then

                    RJMessageBox.Show("Sorry this material qty is zero or this material already return.")
                    txt_forminputstock_qrcode.Text = ""
                    txt_forminputstock_qrcode.Select()
                    Exit Sub

                End If

                Dim queryCheckInputStockDetail As String = "SELECT * FROM STOCK_CARD where qrcode_new='" & txt_forminputstock_qrcode.Text & "' and lot_no='" & ds.Rows(0).Item("lot_no") & "' AND MATERIAL='" & ds.Rows(0).Item("material") & "' and mts_no='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' AND status='Return To Main Store'"
                Dim dtCheckInputStockDetail As DataTable = Database.GetData(queryCheckInputStockDetail)

                If dtCheckInputStockDetail.Rows.Count > 0 Then

                    RJMessageBox.Show("This QRCode Already Scan")

                    txt_forminputstock_qrcode.Text = ""
                    txt_forminputstock_qrcode.Select()

                    dgv_forminputstock.DataSource = Nothing
                    dgv_forminputstock.Rows.Clear()
                    dgv_forminputstock.Columns.Clear()

                    treeView_show()

                Else
                    Try
                        Dim sqlInsertInputStockDetail As String = "INSERT INTO STOCK_CARD (qrcode,MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, MTS_NO,DEPARTMENT, STANDARD_PACK,STATUS,ACTUAL_QTY,RETURN_MAINSTORE_DATETIME,RETURN_MAINSTORE_WHO,qrcode_new)
                                    VALUES ('Manual Input'," & ds.Rows(0).Item("material") & ",'" & ds.Rows(0).Item("actual_qty") & "','" & ds.Rows(0).Item("inv_ctrl_date") & "','" & ds.Rows(0).Item("traceability") & "','" & ds.Rows(0).Item("lot_no") & "','" & ds.Rows(0).Item("batch_no") & "'," & txt_forminputstock_mts_no.Text & ",'" & globVar.department & "','" & ds.Rows(0).Item("STANDARD_PACK") & "','Return To Main Store'," & ds.Rows(0).Item("actual_qty") & ",getdate(),'" & globVar.username & "','" & txt_forminputstock_qrcode.Text & "')"
                        Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                        If cmdInsertInputStockDetail.ExecuteNonQuery() Then

                            Dim SqlUpdateQty As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE id=" & ds.Rows(0).Item("id")
                            Dim cmdUpdateQty = New SqlCommand(SqlUpdateQty, Database.koneksi)
                            If cmdUpdateQty.ExecuteNonQuery() Then
                                txt_forminputstock_qrcode.Text = ""
                                txt_forminputstock_qrcode.Select()

                                dgv_forminputstock.DataSource = Nothing
                                dgv_forminputstock.Rows.Clear()
                                dgv_forminputstock.Columns.Clear()
                            End If
                            treeView_show()
                        End If
                    Catch ex As Exception
                        RJMessageBox.Show("Error Return Stock - 1 =>" & ex.Message)
                    End Try
                End If

            ElseIf txt_forminputstock_qrcode.Text.StartsWith("SM") Then

                Dim sqlCheckStockSM = "SELECT * FROM stock_card WHERE qrcode = '" & txt_forminputstock_qrcode.Text & "' and actual_qty > 0 and status='Receive From Main Store' and [save] = 1 and department='" & globVar.department & "'"
                Dim dtCheckStockSM As DataTable = Database.GetData(sqlCheckStockSM)

                If dtCheckStockSM.Rows.Count = 0 Then

                    RJMessageBox.Show("Sorry this material qty is zero or this material already return.")
                    txt_forminputstock_qrcode.Text = ""
                    txt_forminputstock_qrcode.Select()
                    Exit Sub

                End If

                If dtCheckStockSM.Rows.Count > 0 Then

                    Dim queryCheckInputStockDetail As String = "SELECT * FROM STOCK_CARD where qrcode='" & txt_forminputstock_qrcode.Text & "' and mts_no='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' AND status='Return To Main Store'"
                    Dim dtCheckInputStockDetail As DataTable = Database.GetData(queryCheckInputStockDetail)

                    If dtCheckInputStockDetail.Rows.Count > 0 Then
                        RJMessageBox.Show("This QRCode Already Scan")

                        txt_forminputstock_qrcode.Text = ""
                        txt_forminputstock_qrcode.Select()

                        dgv_forminputstock.DataSource = Nothing
                        dgv_forminputstock.Rows.Clear()
                        dgv_forminputstock.Columns.Clear()

                        treeView_show()
                    Else
                        Try
                            Dim sqlInsertInputStockDetail As String = "INSERT INTO STOCK_CARD (qrcode,MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, MTS_NO,DEPARTMENT, STANDARD_PACK,STATUS,ACTUAL_QTY,RETURN_MAINSTORE_DATETIME,RETURN_MAINSTORE_WHO)
                                    VALUES ('" & txt_forminputstock_qrcode.Text & "'," & dtCheckStockSM.Rows(0).Item("material") & ",'" & dtCheckStockSM.Rows(0).Item("actual_qty") & "','" & dtCheckStockSM.Rows(0).Item("inv_ctrl_date") & "','" & dtCheckStockSM.Rows(0).Item("traceability") & "','" & dtCheckStockSM.Rows(0).Item("lot_no") & "','" & dtCheckStockSM.Rows(0).Item("batch_no") & "'," & txt_forminputstock_mts_no.Text & ",'" & globVar.department & "','" & dtCheckStockSM.Rows(0).Item("STANDARD_PACK") & "','Return To Main Store'," & dtCheckStockSM.Rows(0).Item("actual_qty") & ",getdate(),'" & globVar.username & "')"
                            Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                            If cmdInsertInputStockDetail.ExecuteNonQuery() Then

                                Dim SqlUpdateQty As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE id=" & dtCheckStockSM.Rows(0).Item("id")
                                Dim cmdUpdateQty = New SqlCommand(SqlUpdateQty, Database.koneksi)
                                If cmdUpdateQty.ExecuteNonQuery() Then
                                    txt_forminputstock_qrcode.Text = ""
                                    txt_forminputstock_qrcode.Select()

                                    dgv_forminputstock.DataSource = Nothing
                                    dgv_forminputstock.Rows.Clear()
                                    dgv_forminputstock.Columns.Clear()
                                End If
                                treeView_show()
                            End If
                        Catch ex As Exception
                            RJMessageBox.Show("Error Return Stock - 2 =>" & ex.Message)
                        End Try
                    End If

                End If

            Else

                RJMessageBox.Show("QRCode Not Valid")
                Play_Sound.Wrong()
                txt_forminputstock_qrcode.Clear()
                Exit Sub

            End If

        End If
    End Sub

    Private Sub dgv_forminputstock_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_forminputstock.CellValueChanged
        Call Database.koneksi_database()
        If dgv_forminputstock.CurrentCell.ColumnIndex = 6 Then
            If globVar.update > 0 Then

                Try
                    Dim Sql As String = "update STOCK_CARD set QTY=" & dgv_forminputstock.Rows(e.RowIndex).Cells("QTY").Value & ",ACTUAL_QTY=" & dgv_forminputstock.Rows(e.RowIndex).Cells("QTY").Value & " where ID=" & dgv_forminputstock.Rows(e.RowIndex).Cells("#").Value
                    Dim cmd = New SqlCommand(Sql, Database.koneksi)
                    cmd.ExecuteNonQuery()

                    DGV_InputStock(TextBox1.Text)
                    treeView_show()
                    RJMessageBox.Show("Success updated data")
                Catch ex As Exception
                    RJMessageBox.Show("Error Return Stock - 2 =>" & ex.Message)
                End Try
            Else
                RJMessageBox.Show("Your Access cannot execute this action")
            End If

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If globVar.add > 0 Then
            Dim result = RJMessageBox.Show("The data has been saved cannot be changed. Are you sure to save this MTS Data?", "Warning", MessageBoxButtons.YesNo)

            If TreeView1.Nodes(0).Nodes.Count > 0 Then
                If result = DialogResult.Yes Then
                    Try
                        Dim Sql As String = "UPDATE STOCK_CARD SET [SAVE]=1, DATETIME_SAVE=GETDATE() FROM STOCK_CARD WHERE MTS_NO='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Return To Main Store'"
                        Dim cmd = New SqlCommand(Sql, Database.koneksi)
                        If cmd.ExecuteNonQuery() Then

                            dgv_forminputstock.DataSource = Nothing
                            dgv_forminputstock.Columns.Clear()
                            treeView_show()
                            txt_forminputstock_qrcode.ReadOnly = True
                            checkQr.Enabled = False
                            Button2.Enabled = False
                            Unlock.Enabled = True
                            dgv_forminputstock.ReadOnly = True

                            RJMessageBox.Show("Success Save The Data")
                        End If
                    Catch ex As Exception
                        RJMessageBox.Show("Error Return Stock - 3 =>" & ex.Message)
                    End Try
                End If
            Else
                RJMessageBox.Show("Cannot save with 0 Record")
            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub dgv_forminputstock_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_forminputstock.CellClick
        If dgv_forminputstock.Columns(e.ColumnIndex).Name = "delete" Then
            If globVar.delete > 0 Then
                Dim result = RJMessageBox.Show("Are you sure to delete?", "Warning", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Try

                        Dim sqlCheck As String = "select * from stock_card where actual_qty>0 and id = " & dgv_forminputstock.Rows(e.RowIndex).Cells("#").Value
                        Dim dtCheck As DataTable = Database.GetData(sqlCheck)

                        If dtCheck.Rows.Count > 0 Then

                            If dgv_forminputstock.Rows(e.RowIndex).Cells("QRCode").Value.ToString.StartsWith("NQ") Then

                                Dim SqlUpdateQty As String = "UPDATE STOCK_CARD SET actual_qty=qty FROM STOCK_CARD WHERE 
                                    status = 'Receive From Main Store' and department='" & globVar.department & "'
                                    and qrcode_new='" & dtCheck.Rows(0).Item("qrcode_new") & "'"
                                Dim cmdUpdateQty = New SqlCommand(SqlUpdateQty, Database.koneksi)

                                If cmdUpdateQty.ExecuteNonQuery() Then

                                    Dim sql As String = "delete from STOCK_CARD where id=" & dgv_forminputstock.Rows(e.RowIndex).Cells("#").Value
                                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                                    If cmd.ExecuteNonQuery() Then
                                        DGV_InputStock(dgv_forminputstock.Rows(e.RowIndex).Cells(1).Value)
                                        treeView_show()
                                        RJMessageBox.Show("Success delete.")
                                    End If

                                Else
                                    RJMessageBox.Show("Failed delete.")
                                End If

                            ElseIf dgv_forminputstock.Rows(e.RowIndex).Cells("QRCode").Value.ToString.StartsWith("B") Then

                                Dim SqlUpdateQty As String = "UPDATE STOCK_CARD SET actual_qty=qty FROM STOCK_CARD WHERE 
                                        status = 'Receive From Production' and department='" & globVar.department & "'
                                        and qrcode='" & dtCheck.Rows(0).Item("qrcode") & "'"
                                Dim cmdUpdateQty = New SqlCommand(SqlUpdateQty, Database.koneksi)

                                If cmdUpdateQty.ExecuteNonQuery() Then

                                    Dim sql As String = "delete from STOCK_CARD where id=" & dgv_forminputstock.Rows(e.RowIndex).Cells("#").Value
                                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                                    If cmd.ExecuteNonQuery() Then
                                        DGV_InputStock(dgv_forminputstock.Rows(e.RowIndex).Cells(1).Value)
                                        treeView_show()
                                        RJMessageBox.Show("Success delete.")
                                    End If

                                Else
                                    RJMessageBox.Show("Failed delete.")
                                End If

                            Else

                                Dim SqlUpdateQty As String = "UPDATE STOCK_CARD SET actual_qty=qty FROM STOCK_CARD WHERE 
                                        status = 'Receive From Main Store' and department='" & globVar.department & "'
                                        and qrcode='" & dtCheck.Rows(0).Item("qrcode") & "'"
                                Dim cmdUpdateQty = New SqlCommand(SqlUpdateQty, Database.koneksi)

                                If cmdUpdateQty.ExecuteNonQuery() Then

                                    Dim sql As String = "delete from STOCK_CARD where id=" & dgv_forminputstock.Rows(e.RowIndex).Cells("#").Value
                                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                                    If cmd.ExecuteNonQuery() Then
                                        DGV_InputStock(dgv_forminputstock.Rows(e.RowIndex).Cells(1).Value)
                                        treeView_show()
                                        RJMessageBox.Show("Success delete.")
                                    End If

                                Else
                                    RJMessageBox.Show("Failed delete.")
                                End If

                            End If

                        Else

                            RJMessageBox.Show("This material cannot be deleted because it has already been reused.")

                        End If
                    Catch ex As Exception
                        RJMessageBox.Show("Error Return Stock - 4 =>" & ex.Message)
                    End Try
                End If
            Else
                RJMessageBox.Show("Your Access cannot execute this action")
            End If
        End If
    End Sub

    Private Sub treeView_show()
        TreeView1.Nodes.Clear()
        Dim queryInputStock As String = "SELECT DISTINCT(MATERIAL),SUM(QTY) QTY FROM STOCK_CARD WHERE MTS_NO=" & txt_forminputstock_mts_no.Text & " AND DEPARTMENT='" & globVar.department & "' AND STATUS='Return To Main Store' GROUP BY MATERIAL"
        Dim dtInputStock As DataTable = Database.GetData(queryInputStock)

        TreeView1.Nodes.Add("MTS No : " & txt_forminputstock_mts_no.Text)

        For i = 0 To dtInputStock.Rows.Count - 1
            TreeView1.Nodes(0).Nodes.Add(dtInputStock.Rows(i).Item("MATERIAL").ToString, "PN : " & dtInputStock.Rows(i).Item("MATERIAL").ToString & " - Qty : " & dtInputStock.Rows(i).Item("QTY").ToString)
        Next
        TreeView1.ExpandAll()
    End Sub

    Private Sub txt_forminputstock_mts_no_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_forminputstock_mts_no.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt_forminputstock_mts_no_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_forminputstock_mts_no.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            If globVar.view > 0 Then
                If txt_forminputstock_mts_no.Text = "" Then
                    RJMessageBox.Show("MTS cannot be null.")
                    txt_forminputstock_mts_no.Select()
                Else
                    Dim queryCheck As String = "SELECT * FROM STOCK_CARD WHERE MTS_NO='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT = '" & globVar.department & "' and [save]=1 and status='Receive From Main Store'"
                    Dim dtCheck As DataTable = Database.GetData(queryCheck)
                    If dtCheck.Rows.Count > 0 Then
                        RJMessageBox.Show("Sorry This MTS Number already use in input stock")
                        txt_forminputstock_mts_no.Clear()
                        Exit Sub
                    End If

                    txt_forminputstock_mts_no.ReadOnly = True
                    checkQr.Enabled = True

                    If checkQr.Checked Then
                        txt_forminputstock_qrcode.Enabled = True
                        txt_forminputstock_qrcode.Select()

                        txtmanualPN.Enabled = False
                    Else
                        txtmanualPN.Enabled = True
                        txtmanualPN.Select()
                        txt_forminputstock_qrcode.Enabled = False
                    End If

                    treeView_show()

                    Dim queryCheckLock As String = "SELECT TOP 1 * FROM stock_card WHERE MTS_NO = '" & txt_forminputstock_mts_no.Text & "' and department='" & globVar.department & "' and status='Return To Main Store'"
                    Dim dtCheckLock As DataTable = Database.GetData(queryCheckLock)

                    If dtCheckLock.Rows.Count > 0 Then
                        If dtCheckLock.Rows(0).Item("SAVE") = 0 Then
                            Button2.Enabled = True
                            Unlock.Enabled = False
                        Else
                            txt_forminputstock_qrcode.ReadOnly = True
                            checkQr.Enabled = False
                            Button2.Enabled = False
                            dgv_forminputstock.ReadOnly = True
                            Unlock.Enabled = True
                        End If
                    Else
                        Button2.Enabled = True
                        Unlock.Enabled = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        If TreeView1.SelectedNode Is Nothing Then
            dgv_forminputstock.DataSource = Nothing
            Exit Sub
        End If

        If TreeView1.SelectedNode.Name = txt_forminputstock_mts_no.Text Then
            dgv_forminputstock.DataSource = Nothing
            Exit Sub
        End If

        Dim id As String = TreeView1.SelectedNode.Name

        TextBox1.Text = id

        DGV_InputStock(id)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        dgv_forminputstock.DataSource = Nothing
        dgv_forminputstock.Rows.Clear()
        dgv_forminputstock.Columns.Clear()

        TreeView1.Nodes.Clear()

        txt_forminputstock_mts_no.Enabled = True
        txt_forminputstock_qrcode.Enabled = False
        txtmanualPN.Enabled = False
        checkQr.Enabled = False

        txt_forminputstock_mts_no.ReadOnly = False

        txt_forminputstock_mts_no.Select()

        txt_forminputstock_mts_no.Text = ""
        txt_forminputstock_qrcode.Text = ""

        Button2.Enabled = False
        Button4.Enabled = False
    End Sub

    Private Sub checkQr_CheckStateChanged(sender As Object, e As EventArgs) Handles checkQr.CheckStateChanged
        If checkQr.Checked Then
            txt_forminputstock_qrcode.Select()
            txt_forminputstock_qrcode.Enabled = True
            txtmanualPN.Enabled = False
            Button4.Enabled = False
            txtmanualPN.Clear()
        Else
            txtmanualPN.Enabled = True
            txt_forminputstock_qrcode.Enabled = False
            Button4.Enabled = False
            txtmanualPN.Select()
            txt_forminputstock_qrcode.Clear()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If globVar.add > 0 Then

            If txtmanualPN.Text <> "" Then
                Dim adapter As SqlDataAdapter
                Dim ds As New DataTable

                Dim allmanualMaterial As String() = ComboBox1.Text.Split("|")
                Dim lotManualMaterial As String() = allmanualMaterial(0).Split(":")
                Dim icdManualMaterial As String() = allmanualMaterial(1).Split(":")
                Dim traceManualMaterial As String() = allmanualMaterial(2).Split(":")
                Dim batchManualMaterial As String() = allmanualMaterial(3).Split(":")

                Dim sql As String = "SELECT * FROM STOCK_CARD where lot_no='" & lotManualMaterial(1) & "' 
                    AND MATERIAL='" & txtmanualPN.Text & "' 
                    AND inv_ctrl_date='" & icdManualMaterial(1) & "' 
                    AND traceability='" & traceManualMaterial(1) & "' 
                    AND batch_no='" & batchManualMaterial(1) & "' 
                    AND (STATUS='Receive From Production' or (STATUS='Receive From Main Store' AND [SAVE]=1)) 
                    and department='" & globVar.department & "'"
                adapter = New SqlDataAdapter(sql, Database.koneksi)
                adapter.Fill(ds)

                If ds.Rows.Count > 0 Then

                    Dim queryCheckInputStockDetail As String = "SELECT * FROM STOCK_CARD where lot_no='" & lotManualMaterial(1) & "' 
                        AND MATERIAL='" & txtmanualPN.Text & "' 
                        AND inv_ctrl_date='" & icdManualMaterial(1) & "' 
                        AND traceability='" & traceManualMaterial(1) & "' 
                        AND batch_no='" & batchManualMaterial(1) & "' 
                        and mts_no='" & txt_forminputstock_mts_no.Text & "' 
                        AND DEPARTMENT='" & globVar.department & "' 
                        AND STATUS='Return To Main Store'"
                    Dim dtCheckInputStockDetail As DataTable = Database.GetData(queryCheckInputStockDetail)

                    If dtCheckInputStockDetail.Rows.Count > 0 Then
                        RJMessageBox.Show("This QRCode Already Scan")

                        txt_forminputstock_qrcode.Text = ""
                        txt_forminputstock_qrcode.Select()

                        dgv_forminputstock.DataSource = Nothing
                        dgv_forminputstock.Rows.Clear()
                        dgv_forminputstock.Columns.Clear()

                        treeView_show()
                    Else
                        Try
                            Dim sqlInsertInputStockDetail As String = "INSERT INTO STOCK_CARD (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, MTS_NO,DEPARTMENT, STANDARD_PACK,STATUS,ACTUAL_QTY,RETURN_MAINSTORE_DATETIME,RETURN_MAINSTORE_WHO,QRCODE)
                                    VALUES ('" & txtmanualPN.Text & "'," & ds.Rows(0).Item("QTY") & ",'" & ds.Rows(0).Item("INV_CTRL_DATE") & "','" & ds.Rows(0).Item("TRACEABILITY") & "','" & lotManualMaterial(1) & "','" & ds.Rows(0).Item("BATCH_NO") & "'," & txt_forminputstock_mts_no.Text & ",'" & globVar.department & "','" & ds.Rows(0).Item("STANDARD_PACK") & "','Return To Main Store'," & ds.Rows(0).Item("QTY") & ",getdate(),'" & globVar.username & "','Manual Input')"
                            Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                            If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                                txtmanualPN.Clear()
                                txtmanualPN.Select()

                                dgv_forminputstock.DataSource = Nothing
                                dgv_forminputstock.Rows.Clear()
                                dgv_forminputstock.Columns.Clear()

                                treeView_show()
                            End If
                        Catch ex As Exception
                            RJMessageBox.Show("Error Return Stock - 5 =>" & ex.Message)
                        End Try
                    End If
                Else
                    RJMessageBox.Show("Part Number not in DB")
                    txt_forminputstock_qrcode.Text = ""
                    txt_forminputstock_qrcode.Select()
                End If
            Else
                RJMessageBox.Show("Please fill all form")
            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub dgv_forminputstock_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgv_forminputstock.DataBindingComplete
        With dgv_forminputstock
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

    Private Sub txtmanualPN_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtmanualPN.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            tampilDataComboBoxDataManualMaterial(txtmanualPN.Text)
            Button4.Enabled = True
            ComboBox1.Enabled = True
        End If
    End Sub

    Sub tampilDataComboBoxDataManualMaterial(material As String)
        Call Database.koneksi_database()
        Dim dtMaterial As DataTable = Database.GetData("select lot_no, inv_ctrl_date, traceability, batch_no from stock_card where department='" & globVar.department & "' and material='" & material & "' and qrcode='Manual Input' and actual_qty > 0 and status = 'Receive From Main Store' order by datetime_insert")

        dtMaterial.Columns.Add("DisplayMember", GetType(String))

        For Each row As DataRow In dtMaterial.Rows
            row("DisplayMember") = "Lot No:" & row("lot_no").ToString() & "|ICD:" & row("inv_ctrl_date").ToString() & "|Trace:" & row("traceability").ToString() & "|Batch:" & row("batch_no").ToString()
        Next

        ComboBox1.DataSource = dtMaterial
        ComboBox1.DisplayMember = "DisplayMember"
        ComboBox1.ValueMember = "DisplayMember"
        ComboBox1.SelectedIndex = -1
    End Sub

    Private Sub txtmanualPN_TextChanged(sender As Object, e As EventArgs) Handles txtmanualPN.TextChanged
        Button4.Enabled = False
        ComboBox1.Enabled = False
    End Sub

    Private Sub txtmanualPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtmanualPN.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt_forminputstock_mts_no_TextChanged(sender As Object, e As EventArgs) Handles txt_forminputstock_mts_no.TextChanged
        If txt_forminputstock_mts_no.Text.StartsWith("0") AndAlso txt_forminputstock_mts_no.Text.Length > 1 Then
            txt_forminputstock_mts_no.Text = txt_forminputstock_mts_no.Text.TrimStart("0"c)
            txt_forminputstock_mts_no.SelectionStart = txt_forminputstock_mts_no.Text.Length
        End If
    End Sub

    Private Sub Unlock_Click(sender As Object, e As EventArgs) Handles Unlock.Click

        Dim result = RJMessageBox.Show("Data return already saved. Are you sure to edit this MTS Data?", "Warning", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then
            Try
                Dim sqlInsertInputStockDetail As String = "INSERT INTO LOG (MENU, REMARK, WHO) VALUES ('INPUT STOCK','Edit Return Stock After Save','" & globVar.username & "')"
                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                    Dim Sql As String = "UPDATE STOCK_CARD SET [SAVE]=0, DATETIME_SAVE=null FROM STOCK_CARD WHERE MTS_NO='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Return To Main Store'"
                    Dim cmd = New SqlCommand(Sql, Database.koneksi)
                    If (cmd.ExecuteNonQuery() > 0) Then

                        dgv_forminputstock.DataSource = Nothing
                        treeView_show()
                        txt_forminputstock_qrcode.ReadOnly = False
                        checkQr.Enabled = True
                        Button2.Enabled = True
                        Unlock.Enabled = False
                        dgv_forminputstock.ReadOnly = False

                        RJMessageBox.Show("Success Change The Data. You can EDIT now.")
                    End If
                End If
            Catch ex As Exception
                RJMessageBox.Show("Error Return Stock - 8 =>" & ex.Message)
            End Try
        End If

    End Sub
End Class
