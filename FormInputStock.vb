Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class FormInputStock
    Public Shared menu As String = "Input Stock"

    Private Sub FormInputStock_Load(sender As Object, e As EventArgs) Handles Me.Load
        globVar.PingVersion()

        If globVar.view > 0 Then

            txt_forminputstock_qrcode.ReadOnly = True
            txtmanualPN.ReadOnly = True
            txtmanualTraceability.ReadOnly = True
            txtmanualInv.ReadOnly = True
            txtmanualBatch.ReadOnly = True
            txtmanualLot.ReadOnly = True
            txtmanualQty.ReadOnly = True

            TreeView1.Nodes.Clear()
            dgv_forminputstock.DataSource = Nothing
            dgv_forminputstock.Columns.Clear()
            dgv_forminputstock.Rows.Clear()

            dgv_forminputstock.ReadOnly = False

            Button2.Enabled = False

            unlock.Enabled = False

            checkQr.Enabled = False

            If globVar.hakAkses.Contains("Administrator") Then
                unlock.Visible = True
            Else
                unlock.Visible = False
            End If
        End If
    End Sub

    Private Sub DGV_InputStock(root As Boolean, id As String)
        Try
            If id = "" Then
                Exit Sub
            End If

            If root Then

                dgv_forminputstock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                dgv_forminputstock.DataSource = Nothing
                dgv_forminputstock.Rows.Clear()
                dgv_forminputstock.Columns.Clear()
                Call Database.koneksi_database()
                Dim queryInputStockDetail As String = "SELECT ID [#],MATERIAL [Material],LOT_NO [Lot],TRACEABILITY [Trace],BATCH_NO [Batch],INV_CTRL_DATE [ICD],QTY [Qty],CASE 
                                                        WHEN qrcode_new is not null
		                                                THEN qrcode_new COLLATE SQL_Latin1_General_CP1_CI_AS 
		                                                ELSE qrcode COLLATE SQL_Latin1_General_CP1_CI_AS  
                                                    END AS QRCode, DATETIME_INSERT [Date Time], INSERT_WHO [Scan By] FROM STOCK_CARD WHERE MTS_NO='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Receive From Main Store'"
                Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
                dgv_forminputstock.DataSource = dtInputStockDetail

                Dim queryCheckLock As String = "SELECT TOP 1 * FROM STOCK_CARD WHERE MTS_NO = '" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Receive From Main Store'"
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
                        unlock.Enabled = False
                    End If
                Else
                    Button2.Enabled = True
                    unlock.Enabled = False
                End If

                dgv_forminputstock.Columns(0).Width = 100
                dgv_forminputstock.Columns(2).Width = 150

                For i As Integer = 0 To dgv_forminputstock.RowCount - 1
                    If dgv_forminputstock.Rows(i).Index Mod 2 = 0 Then
                        dgv_forminputstock.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                    Else
                        dgv_forminputstock.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                    End If

                    Dim queryMaterial As String = "SELECT standard_qty FROM master_material WHERE part_number='" & dgv_forminputstock.Rows(i).Cells(1).Value & "'"
                    Dim dtMaterial As DataTable = Database.GetData(queryMaterial)

                    If dtMaterial.Rows(0).Item(0) = dgv_forminputstock.Rows(i).Cells(6).Value Then
                        dgv_forminputstock.Rows(i).Cells(6).Style.BackColor = Color.Green
                    End If

                Next i

                If dtCheckLock.Rows.Count > 0 Then
                    dgv_forminputstock.ReadOnly = dtCheckLock.Rows(0).Item("SAVE")
                Else
                    dgv_forminputstock.ReadOnly = False
                End If

            Else

                dgv_forminputstock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                dgv_forminputstock.DataSource = Nothing
                dgv_forminputstock.Rows.Clear()
                dgv_forminputstock.Columns.Clear()
                Call Database.koneksi_database()
                Dim queryInputStockDetail As String = "SELECT ID [#],MATERIAL [Material],LOT_NO [Lot],TRACEABILITY [Trace],BATCH_NO [Batch],INV_CTRL_DATE [ICD],QTY [Qty],CASE 
                                                        WHEN qrcode_new is not null
		                                                THEN qrcode_new COLLATE SQL_Latin1_General_CP1_CI_AS 
		                                                ELSE qrcode COLLATE SQL_Latin1_General_CP1_CI_AS  
                                                    END AS QRCode, DATETIME_INSERT [Date Time], INSERT_WHO [Scan By] FROM STOCK_CARD WHERE MTS_NO='" & txt_forminputstock_mts_no.Text & "' and MATERIAL='" & id & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Receive From Main Store'"
                Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
                dgv_forminputstock.DataSource = dtInputStockDetail

                Dim queryCheckLock As String = "SELECT TOP 1 * FROM STOCK_CARD WHERE MTS_NO = '" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Receive From Main Store'"
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
                        unlock.Enabled = False
                    End If
                Else
                    Button2.Enabled = True
                    unlock.Enabled = False
                End If

                dgv_forminputstock.Columns(0).Width = 100
                dgv_forminputstock.Columns(2).Width = 150

                For i As Integer = 0 To dgv_forminputstock.RowCount - 1
                    If dgv_forminputstock.Rows(i).Index Mod 2 = 0 Then
                        dgv_forminputstock.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                    Else
                        dgv_forminputstock.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                    End If

                    Dim queryMaterial As String = "SELECT standard_qty FROM master_material WHERE part_number='" & dgv_forminputstock.Rows(i).Cells(1).Value & "'"
                    Dim dtMaterial As DataTable = Database.GetData(queryMaterial)

                    If dtMaterial.Rows(0).Item(0) = dgv_forminputstock.Rows(i).Cells(6).Value Then
                        dgv_forminputstock.Rows(i).Cells(6).Style.BackColor = Color.Green
                    End If

                Next i

                If dtCheckLock.Rows.Count > 0 Then
                    dgv_forminputstock.ReadOnly = dtCheckLock.Rows(0).Item("SAVE")
                Else
                    dgv_forminputstock.ReadOnly = False
                End If

            End If

        Catch ex As Exception
            RJMessageBox.Show("Error Input Stock - 1 =>" & ex.Message)
        End Try
    End Sub

    Private Sub txt_forminputstock_qrcode_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_forminputstock_qrcode.PreviewKeyDown
        Dim QrcodeValid As Boolean
        Try
            If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
                If globVar.add > 0 Then
                    If txt_forminputstock_qrcode.Text.StartsWith("NQ") And checkQr.Checked Then

                        Dim queryCheckNewQR As String = "SELECT * FROM new_label where qrcode='" & txt_forminputstock_qrcode.Text & "' AND MATERIAL is null"
                        Dim dtCheckNewQR As DataTable = Database.GetData(queryCheckNewQR)

                        If dtCheckNewQR.Rows.Count = 0 Then

                            lbl_Info.Text = "New QRCode already used"
                            Play_Sound.Wrong()
                            txt_forminputstock_qrcode.Clear()
                            Exit Sub

                        End If

                        Dim q = InputBox("Please scan label material double", "Scan Material Double")
                        If q = "" Or q Is Nothing Then
                            Exit Sub
                        End If

                        Call Database.koneksi_database()

                        Dim adapter As SqlDataAdapter
                        Dim ds As New DataTable

                        QrcodeValid = QRCode.Baca(q)

                        If QrcodeValid = False Then
                            lbl_Info.Text = "QRCode Not Valid"
                            Play_Sound.Wrong()
                            txt_forminputstock_qrcode.Clear()
                            Exit Sub
                        End If

                        If globVar.QRCode_PN = "" Or globVar.QRCode_lot = "" Or globVar.QRCode_Traceability = "" Or globVar.QRCode_Batch = "" Or globVar.QRCode_Inv = "" Then
                            lbl_Info.Text = "QRCode Not Valid"
                            Play_Sound.Wrong()
                            txt_forminputstock_qrcode.Clear()
                            Exit Sub
                        End If

                        Dim sql As String = "SELECT * FROM MASTER_MATERIAL where PART_NUMBER='" & globVar.QRCode_PN & "'"
                        adapter = New SqlDataAdapter(sql, Database.koneksi)
                        adapter.Fill(ds)

                        If ds.Rows.Count > 0 Then

                            Dim queryCheckInputStockDetail As String = "SELECT * FROM STOCK_CARD where lot_no='" & globVar.QRCode_lot & "' AND MATERIAL='" & globVar.QRCode_PN & "' and inv_ctrl_date='" & globVar.QRCode_Inv & "' and traceability='" & globVar.QRCode_Traceability & "' and batch_no='" & globVar.QRCode_Batch & "' and mts_no='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' and status='Receive From Main Store' and qrcode_new='" & txt_forminputstock_qrcode.Text & "'"
                            Dim dtCheckInputStockDetail As DataTable = Database.GetData(queryCheckInputStockDetail)

                            If dtCheckInputStockDetail.Rows.Count > 0 Then
                                lbl_Info.Text = "Double Scan!"
                                Play_Sound.Double_scan()


                                txt_forminputstock_qrcode.Text = ""
                                txt_forminputstock_qrcode.Select()

                                dgv_forminputstock.DataSource = Nothing
                                dgv_forminputstock.Rows.Clear()
                                dgv_forminputstock.Columns.Clear()

                                treeView_show()
                            Else
                                Try
                                    Dim StandartPack As String

                                    If ds.Rows(0).Item("STANDARD_QTY") = globVar.QRCode_Qty Then
                                        StandartPack = "YES"
                                    Else
                                        StandartPack = "NO"
                                    End If

                                    Dim sqlInsertInputStockDetail As String = "INSERT INTO STOCK_CARD (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, QRCODE, MTS_NO,DEPARTMENT, STANDARD_PACK,STATUS,ACTUAL_QTY, INSERT_WHO,qrcode_new)
                                    VALUES ('" & globVar.QRCode_PN & "'," & globVar.QRCode_Qty & ",'" & globVar.QRCode_Inv & "','" & globVar.QRCode_Traceability & "','" & globVar.QRCode_lot & "','" & globVar.QRCode_Batch & "','" & q & "','" & txt_forminputstock_mts_no.Text & "','" & globVar.department & "','" & StandartPack & "','Receive From Main Store'," & globVar.QRCode_Qty & ",'" & globVar.username & "','" & txt_forminputstock_qrcode.Text & "')"
                                    Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                    If cmdInsertInputStockDetail.ExecuteNonQuery() Then

                                        Dim SqlUpdateNewLabel As String = "update new_label set material='" & globVar.QRCode_PN & "',QTY=" & globVar.QRCode_Qty & ",INV_CTRL_DATE='" & globVar.QRCode_Inv & "',TRACEABILITY='" & globVar.QRCode_Traceability & "',LOT_NO='" & globVar.QRCode_lot & "',BATCH_NO='" & globVar.QRCode_Batch & "',datetime_update=getdate(),update_who='" & globVar.username & "' where qrcode='" & txt_forminputstock_qrcode.Text & "'"
                                        Dim cmdUpdateNewLabel = New SqlCommand(SqlUpdateNewLabel, Database.koneksi)
                                        cmdUpdateNewLabel.ExecuteNonQuery()

                                        Dim queryCheckReturn As String = "SELECT * FROM STOCK_CARD where lot_no='" & globVar.QRCode_lot & "' AND MATERIAL='" & globVar.QRCode_PN & "' and inv_ctrl_date='" & globVar.QRCode_Inv & "' and traceability='" & globVar.QRCode_Traceability & "' and batch_no='" & globVar.QRCode_Batch & "' AND DEPARTMENT='" & globVar.department & "' and status='Return To Main Store' and actual_qty > 0"
                                        Dim dtCheckReturn As DataTable = Database.GetData(queryCheckReturn)

                                        If dtCheckReturn.Rows.Count > 0 Then
                                            Dim SqlUpdate As String = "update STOCK_CARD set actual_qty=0 where ID=" & dtCheckReturn.Rows(0).Item("id")
                                            Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                            cmdUpdate.ExecuteNonQuery()
                                        End If

                                        txt_forminputstock_qrcode.Text = ""
                                        txt_forminputstock_qrcode.Select()

                                        dgv_forminputstock.DataSource = Nothing
                                        dgv_forminputstock.Rows.Clear()
                                        dgv_forminputstock.Columns.Clear()

                                        treeView_show()

                                        lbl_Info.Text = ""
                                        Play_Sound.correct()
                                    End If
                                Catch ex As Exception
                                    lbl_Info.Text = "Error Insert"
                                    Play_Sound.Wrong()
                                End Try
                            End If
                        Else
                            lbl_Info.Text = "Part Number not in DB"
                            Play_Sound.not_in_database()

                            txt_forminputstock_qrcode.Text = ""
                            txt_forminputstock_qrcode.Select()
                        End If

                    ElseIf txt_forminputstock_qrcode.Text.StartsWith("MX2D") And checkQr.Checked Then

                        Call Database.koneksi_database()

                        Dim adapter As SqlDataAdapter
                        Dim ds As New DataTable

                        QrcodeValid = QRCode.Baca(txt_forminputstock_qrcode.Text)

                        If QrcodeValid = False Then
                            lbl_Info.Text = "QRCode Not Valid"
                            Play_Sound.Wrong()
                            txt_forminputstock_qrcode.Clear()
                            Exit Sub
                        End If

                        If globVar.QRCode_PN = "" Or globVar.QRCode_lot = "" Or globVar.QRCode_Traceability = "" Or globVar.QRCode_Batch = "" Or globVar.QRCode_Inv = "" Then
                            lbl_Info.Text = "QRCode Not Valid"
                            Play_Sound.Wrong()
                            txt_forminputstock_qrcode.Clear()
                            Exit Sub
                        End If

                        Dim sql As String = "SELECT * FROM MASTER_MATERIAL where PART_NUMBER='" & globVar.QRCode_PN & "'"
                        adapter = New SqlDataAdapter(sql, Database.koneksi)
                        adapter.Fill(ds)

                        If ds.Rows.Count > 0 Then

                            'Dim queryCheckInputStockDetail As String = "SELECT * FROM STOCK_CARD where lot_no='" & globVar.QRCode_lot & "' AND MATERIAL='" & globVar.QRCode_PN & "' and inv_ctrl_date='" & globVar.QRCode_Inv & "' and traceability='" & globVar.QRCode_Traceability & "' and batch_no='" & globVar.QRCode_Batch & "' and mts_no='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' and status='Receive From Main Store'"
                            Dim queryCheckInputStockDetail As String = "SELECT * FROM STOCK_CARD where lot_no='" & globVar.QRCode_lot & "' AND MATERIAL='" & globVar.QRCode_PN & "' and inv_ctrl_date='" & globVar.QRCode_Inv & "' and traceability='" & globVar.QRCode_Traceability & "' and batch_no='" & globVar.QRCode_Batch & "' and DEPARTMENT='" & globVar.department & "' and status='Receive From Main Store'"
                            Dim dtCheckInputStockDetail As DataTable = Database.GetData(queryCheckInputStockDetail)

                            If dtCheckInputStockDetail.Rows.Count > 0 Then
                                'RJMessageBox.Show("This QRCode Already Scan")
                                lbl_Info.Text = "Double Scan!"
                                Play_Sound.Double_scan()


                                txt_forminputstock_qrcode.Text = ""
                                txt_forminputstock_qrcode.Select()

                                dgv_forminputstock.DataSource = Nothing
                                dgv_forminputstock.Rows.Clear()
                                dgv_forminputstock.Columns.Clear()

                                treeView_show()
                            Else
                                Try
                                    Dim StandartPack As String

                                    If ds.Rows(0).Item("STANDARD_QTY") = globVar.QRCode_Qty Then
                                        StandartPack = "YES"
                                    Else
                                        StandartPack = "NO"
                                    End If

                                    Dim sqlInsertInputStockDetail As String = "INSERT INTO STOCK_CARD (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, QRCODE, MTS_NO,DEPARTMENT, STANDARD_PACK,STATUS,ACTUAL_QTY, INSERT_WHO)
                                    VALUES ('" & globVar.QRCode_PN & "'," & globVar.QRCode_Qty & ",'" & globVar.QRCode_Inv & "','" & globVar.QRCode_Traceability & "','" & globVar.QRCode_lot & "','" & globVar.QRCode_Batch & "','" & txt_forminputstock_qrcode.Text.Trim & "','" & txt_forminputstock_mts_no.Text & "','" & globVar.department & "','" & StandartPack & "','Receive From Main Store'," & globVar.QRCode_Qty & ",'" & globVar.username & "')"
                                    Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                    If cmdInsertInputStockDetail.ExecuteNonQuery() Then

                                        Dim queryCheckReturn As String = "SELECT * FROM STOCK_CARD where lot_no='" & globVar.QRCode_lot & "' AND MATERIAL='" & globVar.QRCode_PN & "' and inv_ctrl_date='" & globVar.QRCode_Inv & "' and traceability='" & globVar.QRCode_Traceability & "' and batch_no='" & globVar.QRCode_Batch & "' and mts_no='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' and status='Return To Main Store' and actual_qty > 0"
                                        Dim dtCheckReturn As DataTable = Database.GetData(queryCheckReturn)

                                        If dtCheckReturn.Rows.Count > 0 Then
                                            Dim SqlUpdate As String = "update STOCK_CARD set actual_qty=0 where ID=" & dtCheckReturn.Rows(0).Item("id")
                                            Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                            cmdUpdate.ExecuteNonQuery()
                                        End If

                                        txt_forminputstock_qrcode.Text = ""
                                        txt_forminputstock_qrcode.Select()

                                        dgv_forminputstock.DataSource = Nothing
                                        dgv_forminputstock.Rows.Clear()
                                        dgv_forminputstock.Columns.Clear()

                                        treeView_show()

                                        lbl_Info.Text = ""
                                        Play_Sound.correct()
                                    End If
                                Catch ex As Exception
                                    'RJMessageBox.Show("Error Insert" & ex.Message)
                                    lbl_Info.Text = "Error Insert"
                                    Play_Sound.Wrong()
                                End Try
                            End If
                        Else
                            'RJMessageBox.Show("Part Number not in DB")
                            lbl_Info.Text = "Part Number not in DB"
                            Play_Sound.not_in_database()

                            txt_forminputstock_qrcode.Text = ""
                            txt_forminputstock_qrcode.Select()
                        End If
                    Else
                        lbl_Info.Text = "Wrong QRCode"
                        Play_Sound.Wrong()

                        txt_forminputstock_qrcode.Text = ""
                        txt_forminputstock_qrcode.Select()
                    End If
                Else
                    RJMessageBox.Show("Your Access cannot execute this action")
                End If
            End If
        Catch ex As Exception
            RJMessageBox.Show("Error Input Stock - 2 =>" & ex.Message)
        End Try
    End Sub

    Private Sub dgv_forminputstock_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_forminputstock.CellValueChanged
        Call Database.koneksi_database()
        If dgv_forminputstock.Columns(e.ColumnIndex).Name = "Qty" Then
            If globVar.update > 0 Then
                Try
                    Dim queryCheck As String = "SELECT * FROM STOCK_CARD WHERE id=" & dgv_forminputstock.Rows(e.RowIndex).Cells("#").Value & " and actual_qty > 0"
                    Dim dtCheck As DataTable = Database.GetData(queryCheck)
                    If dtCheck.Rows.Count > 0 Then
                        Dim Sql As String = "update STOCK_CARD set QTY=" & dgv_forminputstock.Rows(e.RowIndex).Cells("Qty").Value & ", ACTUAL_QTY=" & dgv_forminputstock.Rows(e.RowIndex).Cells("Qty").Value & " where ID=" & dgv_forminputstock.Rows(e.RowIndex).Cells("#").Value
                        Dim cmd = New SqlCommand(Sql, Database.koneksi)
                        cmd.ExecuteNonQuery()

                        DGV_InputStock(False, TextBox1.Text)
                        treeView_show()
                        RJMessageBox.Show("Success updated data")
                    Else

                        RJMessageBox.Show("Cannot delete this material because this material has been used in production")

                    End If
                Catch ex As Exception
                    RJMessageBox.Show("Error Input Stock - 3 =>" & ex.Message)
                End Try
            Else
                RJMessageBox.Show("Your Access cannot execute this action")
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If globVar.add > 0 Then

            Dim result = RJMessageBox.Show("The data has been saved cannot be changed. Are you sure to save this MTS Data?", "Warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Try
                    Dim Sql As String = "UPDATE STOCK_CARD SET [SAVE]=1, DATETIME_SAVE=GETDATE(), SAVE_WHO='" & globVar.username & "' FROM STOCK_CARD WHERE MTS_NO='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Receive From Main Store'"
                    Dim cmd = New SqlCommand(Sql, Database.koneksi)
                    If (cmd.ExecuteNonQuery() > 0) Then

                        dgv_forminputstock.DataSource = Nothing
                        treeView_show()
                        txt_forminputstock_qrcode.ReadOnly = True
                        checkQr.Enabled = False
                        Button2.Enabled = False
                        unlock.Enabled = True
                        dgv_forminputstock.ReadOnly = True

                        RJMessageBox.Show("Success Save The Data")
                    End If
                Catch ex As Exception
                    RJMessageBox.Show("Error Input Stock - 3 =>" & ex.Message)
                End Try
            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub dgv_forminputstock_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_forminputstock.CellClick
        Try
            If e.RowIndex = -1 Then
                Exit Sub
            End If

            If e.ColumnIndex = -1 Then
                Exit Sub
            End If

            If dgv_forminputstock.Columns(e.ColumnIndex).Name = "delete" Then
                If globVar.delete > 0 Then
                    Dim result = RJMessageBox.Show("Are you sure to delete?", "Warning", MessageBoxButtons.YesNo)
                    If result = DialogResult.Yes Then
                        Try
                            Dim queryCheck As String = "SELECT * FROM STOCK_CARD WHERE id=" & dgv_forminputstock.Rows(e.RowIndex).Cells("#").Value & " and actual_qty > 0"
                            Dim dtCheck As DataTable = Database.GetData(queryCheck)
                            If dtCheck.Rows.Count > 0 Then
                                Dim sql As String = "delete from STOCK_CARD where id=" & dgv_forminputstock.Rows(e.RowIndex).Cells("#").Value
                                Dim cmd = New SqlCommand(sql, Database.koneksi)
                                If cmd.ExecuteNonQuery() Then
                                    If dgv_forminputstock.Rows(e.RowIndex).Cells("QRCode").Value.StartsWith("NQ") Then

                                        Dim SqlUpdateNewLabel As String = "update new_label set material=null,QTY=null,INV_CTRL_DATE=null,TRACEABILITY=null,LOT_NO=null,BATCH_NO=null,datetime_update=null,update_who=null where qrcode='" & dgv_forminputstock.Rows(e.RowIndex).Cells("QRCode").Value & "'"
                                        Dim cmdUpdateNewLabel = New SqlCommand(SqlUpdateNewLabel, Database.koneksi)
                                        cmdUpdateNewLabel.ExecuteNonQuery()

                                    End If

                                    DGV_InputStock(False, dgv_forminputstock.Rows(e.RowIndex).Cells(1).Value)
                                    treeView_show()
                                    RJMessageBox.Show("Success delete.")
                                End If
                            Else
                                RJMessageBox.Show("Cannot delete this material because this material in production request")
                            End If

                        Catch ex As Exception
                            RJMessageBox.Show("Error Input Stock - 4 =>" & ex.Message)
                        End Try
                    End If
                Else
                    RJMessageBox.Show("Your Access cannot execute this action")
                End If
            End If
        Catch ex As Exception
            RJMessageBox.Show("Error Input Stock - 5 =>" & ex.Message)
        End Try
    End Sub

    Private Sub treeView_show()
        TreeView1.Nodes.Clear()
        Dim queryInputStock As String = "SELECT DISTINCT(MATERIAL),SUM(QTY) QTY FROM STOCK_CARD WHERE MTS_NO='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Receive From Main Store' GROUP BY MATERIAL"
        Dim dtInputStock As DataTable = Database.GetData(queryInputStock)

        TreeView1.Nodes.Add(txt_forminputstock_mts_no.Text, "MTS No : " & txt_forminputstock_mts_no.Text)

        For i = 0 To dtInputStock.Rows.Count - 1
            TreeView1.Nodes(0).Nodes.Add(dtInputStock.Rows(i).Item("MATERIAL").ToString, "PN : " & dtInputStock.Rows(i).Item("MATERIAL").ToString & " - Qty : " & dtInputStock.Rows(i).Item("QTY").ToString)
        Next
        TreeView1.ExpandAll()
    End Sub

    Private Sub txt_forminputstock_mts_no_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_forminputstock_mts_no.PreviewKeyDown
        Try
            If e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter Then
                If txt_forminputstock_mts_no.Text = "" Then
                    RJMessageBox.Show("MTS cannot be null.")
                    txt_forminputstock_mts_no.Select()
                Else
                    Dim queryCheck As String = "SELECT * FROM STOCK_CARD WHERE MTS_NO='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT = '" & globVar.department & "' and [save]=1 and status != 'Receive From Main Store'"
                    Dim dtCheck As DataTable = Database.GetData(queryCheck)
                    If dtCheck.Rows.Count > 0 Then
                        RJMessageBox.Show("Sorry MTS Number already in DB")
                        txt_forminputstock_mts_no.Clear()
                        Exit Sub
                    End If

                    txt_forminputstock_mts_no.ReadOnly = True
                    checkQr.Enabled = True

                    If checkQr.Checked Then
                        txt_forminputstock_qrcode.ReadOnly = False
                        txt_forminputstock_qrcode.Select()

                        txtmanualPN.ReadOnly = True
                        txtmanualTraceability.ReadOnly = True
                        txtmanualInv.ReadOnly = True
                        txtmanualBatch.ReadOnly = True
                        txtmanualLot.ReadOnly = True
                        txtmanualQty.ReadOnly = True
                    Else
                        txtmanualPN.ReadOnly = False
                        txtmanualTraceability.ReadOnly = False
                        txtmanualInv.ReadOnly = False
                        txtmanualBatch.ReadOnly = False
                        txtmanualLot.ReadOnly = False
                        txtmanualQty.ReadOnly = False
                        txtmanualPN.Select()
                        txt_forminputstock_qrcode.ReadOnly = True
                    End If

                    treeView_show()

                    Dim queryCheckLock As String = "SELECT TOP 1 * FROM stock_card WHERE MTS_NO = '" & txt_forminputstock_mts_no.Text & "' and department='" & globVar.department & "' and status='Receive From Main Store'"
                    Dim dtCheckLock As DataTable = Database.GetData(queryCheckLock)

                    If dtCheckLock.Rows.Count > 0 Then
                        If dtCheckLock.Rows(0).Item("SAVE") = 0 Then
                            Button2.Enabled = True
                            unlock.Enabled = False
                        Else
                            txt_forminputstock_qrcode.ReadOnly = True
                            checkQr.Enabled = False
                            Button2.Enabled = False
                            unlock.Enabled = True
                            dgv_forminputstock.ReadOnly = True
                        End If
                    Else
                        Button2.Enabled = True
                        unlock.Enabled = False
                    End If
                End If
            End If
        Catch ex As Exception
            RJMessageBox.Show("Error Input Stock - 6 =>" & ex.Message)
        End Try
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        If TreeView1.SelectedNode Is Nothing Then
            dgv_forminputstock.DataSource = Nothing
            Exit Sub
        End If

        Dim id As String = TreeView1.SelectedNode.Name

        TextBox1.Text = id

        If TreeView1.SelectedNode.Name = txt_forminputstock_mts_no.Text Then

            DGV_InputStock(True, id)

        Else

            DGV_InputStock(False, id)

        End If



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        dgv_forminputstock.DataSource = Nothing
        dgv_forminputstock.Rows.Clear()
        dgv_forminputstock.Columns.Clear()

        TreeView1.Nodes.Clear()

        txt_forminputstock_mts_no.ReadOnly = False
        txt_forminputstock_qrcode.ReadOnly = True
        txtmanualPN.ReadOnly = True
        txtmanualTraceability.ReadOnly = True
        txtmanualInv.ReadOnly = True
        txtmanualBatch.ReadOnly = True
        txtmanualLot.ReadOnly = True
        txtmanualQty.ReadOnly = True
        checkQr.Enabled = False

        txt_forminputstock_mts_no.Text = ""
        txt_forminputstock_qrcode.Text = ""

        Button2.Enabled = False
        Button3.Enabled = False
        unlock.Enabled = False

        txtmanualPN.Clear()
        txtmanualBatch.Clear()
        txtmanualInv.Clear()
        txtmanualLot.Clear()
        txtmanualQty.Clear()
        txtmanualTraceability.Clear()
        txt_forminputstock_qrcode.Clear()
    End Sub

    Private Sub checkQr_CheckStateChanged(sender As Object, e As EventArgs) Handles checkQr.CheckStateChanged
        If checkQr.Checked Then
            txt_forminputstock_qrcode.ReadOnly = False
            txt_forminputstock_qrcode.Select()

            txtmanualPN.ReadOnly = True
            txtmanualTraceability.ReadOnly = True
            txtmanualInv.ReadOnly = True
            txtmanualBatch.ReadOnly = True
            txtmanualLot.ReadOnly = True
            txtmanualQty.ReadOnly = True
            Button3.Enabled = False
        Else
            txtmanualPN.ReadOnly = False
            txtmanualTraceability.ReadOnly = False
            txtmanualInv.ReadOnly = False
            txtmanualBatch.ReadOnly = False
            txtmanualLot.ReadOnly = False
            txtmanualQty.ReadOnly = False
            'txtmanualPN.Select()
            txt_forminputstock_qrcode.ReadOnly = True
            Button3.Enabled = True

            Dim q As String

            Do
                q = InputBox("Please scan barcode manual", "Scan barcode manual")

                If q = "" Or q Is Nothing Then
                    Exit Do
                End If

                If q.StartsWith("1P") And q.Contains("Q") = False And q.Contains("B") = False And q.Contains("1T") = False Then
                    Dim resultString = q.Substring(2)
                    txtmanualPN.Text = resultString.TrimStart("0"c)
                ElseIf q.StartsWith("Q") And q.Contains("1P") = False And q.Contains("B") = False And q.Contains("1T") = False Then
                    Dim resultString = q.Substring(1)
                    txtmanualQty.Text = resultString.TrimStart("0"c)
                ElseIf q.StartsWith("B") And q.Contains("Q") = False And q.Contains("1P") = False And q.Contains("1T") = False Then
                    Dim resultString = q.Substring(1)
                    txtmanualBatch.Text = resultString.TrimStart("0"c)
                ElseIf q.StartsWith("S") And q.Contains("Q") = False And q.Contains("1P") = False And q.Contains("1T") = False Then
                    Dim resultString = q.Substring(1)
                    txtmanualBatch.Text = resultString.TrimStart("0"c)
                ElseIf q.StartsWith("1T") And q.Contains("Q") = False And q.Contains("B") = False And q.Contains("1P") = False Then
                    Dim resultString = q.Substring(2)
                    txtmanualTraceability.Text = resultString.TrimStart("0"c)
                Else
                    lbl_Info.Text = "Wrong Barcode"
                    Play_Sound.Wrong()
                End If

            Loop While True



            'Dim q = InputBox("Please scan barcode manual", "Scan barcode manual")
            'If q = "" Or q Is Nothing Then
            '    Exit Sub
            'End If

            'If q.StartsWith("1P") And q.Contains("Q") = False And q.Contains("B") = False And q.Contains("1T") = False Then
            '    Dim resultString = q.Substring(2)
            '    txtmanualPN.Text = resultString.TrimStart("0"c)
            'ElseIf q.StartsWith("Q") And q.Contains("1P") = False And q.Contains("B") = False And q.Contains("1T") = False Then
            '    Dim resultString = q.Substring(1)
            '    txtmanualQty.Text = resultString.TrimStart("0"c)
            'ElseIf q.StartsWith("B") And q.Contains("Q") = False And q.Contains("1P") = False And q.Contains("1T") = False Then
            '    Dim resultString = q.Substring(1)
            '    txtmanualBatch.Text = resultString.TrimStart("0"c)
            'ElseIf q.StartsWith("S") And q.Contains("Q") = False And q.Contains("1P") = False And q.Contains("1T") = False Then
            '    Dim resultString = q.Substring(1)
            '    txtmanualBatch.Text = resultString.TrimStart("0"c)
            'ElseIf q.StartsWith("1T") And q.Contains("Q") = False And q.Contains("B") = False And q.Contains("1P") = False Then
            '    Dim resultString = q.Substring(2)
            '    txtmanualTraceability.Text = resultString.TrimStart("0"c)
            'Else
            '    lbl_Info.Text = "Wrong Barcode"
            '    Play_Sound.Wrong()
            'End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If txtmanualBatch.Text <> "" And txtmanualInv.Text <> "" And txtmanualLot.Text <> "" And txtmanualPN.Text <> "" And txtmanualQty.Text <> "" And txtmanualTraceability.Text <> "" Then
            If globVar.add > 0 Then

                Dim q = InputBox("Please scan New Label", "Scan New Label")
                If q = "" Or q Is Nothing Then
                    Exit Sub
                End If

                Dim queryCheckNewQR As String = "SELECT * FROM new_label where qrcode='" & q & "' AND MATERIAL is null"
                Dim dtCheckNewQR As DataTable = Database.GetData(queryCheckNewQR)

                If dtCheckNewQR.Rows.Count = 0 Then

                    lbl_Info.Text = "New QRCode already used or QRCode doesnt exist in DB"
                    Play_Sound.Wrong()
                    txt_forminputstock_qrcode.Clear()
                    Exit Sub

                End If


                If Not IsNumeric(txtmanualQty.Text.TrimStart("0"c)) Or Not IsNumeric(txtmanualLot.Text.TrimStart("0"c)) Then
                    RJMessageBox.Show("Sorry. Lot No / Qty must be Number.")
                    Exit Sub
                End If

                Dim adapter As SqlDataAdapter
                Dim ds As New DataTable

                Dim sql As String = "SELECT * FROM MASTER_MATERIAL where PART_NUMBER='" & txtmanualPN.Text.TrimStart("0"c) & "'"
                adapter = New SqlDataAdapter(sql, Database.koneksi)
                adapter.Fill(ds)

                If ds.Rows.Count > 0 Then

                    Dim queryCheckInputStockDetail As String = "SELECT * FROM STOCK_CARD where lot_no='" & txtmanualLot.Text.TrimStart("0"c) & "' AND MATERIAL='" & txtmanualPN.Text.TrimStart("0"c) & "' and inv_ctrl_date='" & txtmanualInv.Text.TrimStart("0"c) & "' and traceability='" & txtmanualTraceability.Text.TrimStart("0"c) & "' and batch_no='" & txtmanualBatch.Text & "' and mts_no='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' and status='Receive From Main Store'"
                    Dim dtCheckInputStockDetail As DataTable = Database.GetData(queryCheckInputStockDetail)

                    If dtCheckInputStockDetail.Rows.Count > 0 Then
                        RJMessageBox.Show("Part Number & Lot No already in DB")

                        txtmanualPN.Select()

                        dgv_forminputstock.DataSource = Nothing
                        dgv_forminputstock.Rows.Clear()
                        dgv_forminputstock.Columns.Clear()

                        treeView_show()
                    Else
                        Try
                            Dim StandartPack As String

                            If ds.Rows(0).Item("STANDARD_QTY") = txtmanualQty.Text.TrimStart("0"c) Then
                                StandartPack = "YES"
                            Else
                                StandartPack = "NO"
                            End If

                            Dim sqlInsertInputStockDetail As String = "INSERT INTO STOCK_CARD (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, QRCODE, MTS_NO,DEPARTMENT, STANDARD_PACK,STATUS,ACTUAL_QTY, INSERT_WHO, qrcode_new)
                                    VALUES ('" & txtmanualPN.Text.TrimStart("0"c) & "'," & txtmanualQty.Text.TrimStart("0"c) & ",'" & txtmanualInv.Text.TrimStart("0"c) & "','" & txtmanualTraceability.Text.TrimStart("0"c) & "','" & txtmanualLot.Text.TrimStart("0"c) & "','" & txtmanualBatch.Text.TrimStart("0"c) & "','Manual Input'," & txt_forminputstock_mts_no.Text & ",'" & globVar.department & "','" & StandartPack & "','Receive From Main Store'," & txtmanualQty.Text.TrimStart("0"c) & ",'" & globVar.username & "','" & q & "')"
                            Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)

                            If cmdInsertInputStockDetail.ExecuteNonQuery() Then

                                Dim SqlUpdateNewLabel As String = "update new_label set material='" & txtmanualPN.Text.TrimStart("0"c) & "',QTY=" & txtmanualQty.Text.TrimStart("0"c) & ",INV_CTRL_DATE='" & txtmanualInv.Text.TrimStart("0"c) & "',TRACEABILITY='" & txtmanualTraceability.Text.TrimStart("0"c) & "',LOT_NO='" & txtmanualLot.Text.TrimStart("0"c) & "',BATCH_NO='" & txtmanualBatch.Text.TrimStart("0"c) & "',datetime_update=getdate(),update_who='" & globVar.username & "' where qrcode='" & q & "'"
                                Dim cmdUpdateNewLabel = New SqlCommand(SqlUpdateNewLabel, Database.koneksi)
                                cmdUpdateNewLabel.ExecuteNonQuery()

                                Dim queryCheckReturn As String = "SELECT * FROM STOCK_CARD where lot_no='" & txtmanualLot.Text.TrimStart("0"c) & "' AND MATERIAL='" & txtmanualPN.Text.TrimStart("0"c) & "' and inv_ctrl_date='" & txtmanualInv.Text.TrimStart("0"c) & "' and traceability='" & txtmanualTraceability.Text.TrimStart("0"c) & "' and batch_no='" & txtmanualBatch.Text.TrimStart("0"c) & "' AND DEPARTMENT='" & globVar.department & "' and status='Return To Main Store' and actual_qty > 0"
                                Dim dtCheckReturn As DataTable = Database.GetData(queryCheckReturn)

                                If dtCheckReturn.Rows.Count > 0 Then
                                    Dim SqlUpdate As String = "update STOCK_CARD set actual_qty=0 where ID=" & dtCheckReturn.Rows(0).Item("id")
                                    Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                    cmdUpdate.ExecuteNonQuery()
                                End If

                                txtmanualBatch.Text = ""
                                txtmanualInv.Text = ""
                                txtmanualLot.Text = ""
                                txtmanualPN.Text = ""
                                txtmanualQty.Text = ""
                                txtmanualTraceability.Text = ""
                                txtmanualPN.Select()

                                dgv_forminputstock.DataSource = Nothing
                                dgv_forminputstock.Rows.Clear()
                                dgv_forminputstock.Columns.Clear()

                                treeView_show()
                            End If
                        Catch ex As Exception
                            RJMessageBox.Show("Error Input Stock - 7 =>" & ex.Message)
                        End Try
                    End If
                Else
                    RJMessageBox.Show("Sorry, Part Number not exist in data Master Material.")
                End If
            Else
                RJMessageBox.Show("Your Access cannot execute this action")
            End If
        Else
            RJMessageBox.Show("Please fill all form")
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

    Private Sub unlock_Click(sender As Object, e As EventArgs) Handles unlock.Click
        Dim result = RJMessageBox.Show("The data already saved. Are you sure to edit this MTS Data?", "Warning", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then
            Try
                Dim sqlInsertInputStockDetail As String = "INSERT INTO LOG (MENU, REMARK, WHO) VALUES ('INPUT STOCK','Edit Input Stock After Save','" & globVar.username & "')"
                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                    Dim Sql As String = "UPDATE STOCK_CARD SET [SAVE]=0, DATETIME_SAVE=null FROM STOCK_CARD WHERE MTS_NO='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Receive From Main Store'"
                    Dim cmd = New SqlCommand(Sql, Database.koneksi)
                    If (cmd.ExecuteNonQuery() > 0) Then

                        dgv_forminputstock.DataSource = Nothing
                        treeView_show()
                        txt_forminputstock_qrcode.ReadOnly = False
                        checkQr.Enabled = True
                        Button2.Enabled = True
                        unlock.Enabled = False
                        dgv_forminputstock.ReadOnly = False

                        RJMessageBox.Show("Success Change The Data. You can EDIT now.")
                    End If
                End If
            Catch ex As Exception
                RJMessageBox.Show("Error Input Stock - 8 =>" & ex.Message)
            End Try
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lbl_Info.Visible = Not lbl_Info.Visible
    End Sub

    Private Sub GenQRCode_Click(sender As Object, e As EventArgs) Handles GenQRCode.Click
        Dim ds As New DataTable
        Dim sql As String = "SELECT * FROM new_label"
        Dim adapter = New SqlDataAdapter(sql, Database.koneksi)
        adapter.Fill(ds)

        If ds.Rows.Count > 0 Then

            Dim NewLabelCode = NewLabelGenerateCode()
            Dim sqlInsertNewLabel As String = "INSERT INTO new_label (qrcode, insert_who) VALUES ('" & NewLabelCode & "','" & globVar.username & "')"
            Dim cmdInsertNewLabel = New SqlCommand(sqlInsertNewLabel, Database.koneksi)
            If cmdInsertNewLabel.ExecuteNonQuery() Then

                globVar.failPrint = ""
                _PrintingNewLabel.txt_Unique_id.Text = NewLabelCode
                _PrintingNewLabel.txt_QR_Code.Text = NewLabelCode
                _PrintingNewLabel.btn_Print_Click(sender, e)

                If globVar.failPrint = "No" Then

                    Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (remark,department,code_print)
                                     VALUES ('New Label','" & globVar.username & "','" & NewLabelCode & "')"
                    Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                    cmdInsertPrintingRecord.ExecuteNonQuery()

                End If

            End If

        Else

            Dim QRCodeFirstTime As String = "NQ1"
            Dim sqlInsertNewLabel As String = "INSERT INTO new_label (qrcode, insert_who) VALUES ('" & QRCodeFirstTime & "','" & globVar.username & "')"
            Dim cmdInsertNewLabel = New SqlCommand(sqlInsertNewLabel, Database.koneksi)
            If cmdInsertNewLabel.ExecuteNonQuery() Then

                globVar.failPrint = ""
                _PrintingNewLabel.txt_Unique_id.Text = QRCodeFirstTime
                _PrintingNewLabel.txt_QR_Code.Text = QRCodeFirstTime
                _PrintingNewLabel.btn_Print_Click(sender, e)

                If globVar.failPrint = "No" Then

                    Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (remark,department,code_print)
                                     VALUES ('New Label','" & globVar.username & "','" & QRCodeFirstTime & "')"
                    Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                    cmdInsertPrintingRecord.ExecuteNonQuery()

                End If

            End If
        End If
    End Sub

    Function NewLabelGenerateCode() As String
        Dim NewLabelCode As String = ""
        Try
            Dim queryCheckCode As String = "SELECT top 1 qrcode FROM new_label ORDER BY cast(replace(qrcode,'NQ','') as int) desc"
            Dim dtCheckCode As DataTable = Database.GetData(queryCheckCode)
            If dtCheckCode.Rows.Count > 0 Then
                Dim match As Match = Regex.Match(dtCheckCode.Rows(0).Item("qrcode").ToString(), "^([A-Z]+)(\d+)$")
                If match.Success Then
                    Dim prefix As String = match.Groups(1).Value
                    Dim number As Integer = Integer.Parse(match.Groups(2).Value)
                    Dim nextNumber As Integer = number + 1
                    NewLabelCode = prefix & nextNumber.ToString()
                End If
            End If
        Catch ex As Exception
            RJMessageBox.Show("Error Generate New Label : " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return NewLabelCode
    End Function

    Private Sub checkQr_CheckedChanged(sender As Object, e As EventArgs) Handles checkQr.CheckedChanged
        txt_forminputstock_qrcode.Clear()
        txtmanualPN.Clear()
        txtmanualBatch.Clear()
        txtmanualInv.Clear()
        txtmanualLot.Clear()
        txtmanualQty.Clear()
        txtmanualTraceability.Clear()
    End Sub

    Private Sub txt_forminputstock_mts_no_TextChanged(sender As Object, e As EventArgs) Handles txt_forminputstock_mts_no.TextChanged
        If txt_forminputstock_mts_no.Text.StartsWith("0") AndAlso txt_forminputstock_mts_no.Text.Length > 1 Then
            txt_forminputstock_mts_no.Text = txt_forminputstock_mts_no.Text.TrimStart("0"c)
            txt_forminputstock_mts_no.SelectionStart = txt_forminputstock_mts_no.Text.Length
        End If
    End Sub
End Class
