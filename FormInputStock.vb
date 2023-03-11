Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class FormInputStock
    Private Sub FormInputStock_Load(sender As Object, e As EventArgs) Handles Me.Load
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

        If globVar.hakAkses = "ADMIN" Then
            unlock.Visible = True
        Else
            unlock.Visible = False
        End If
    End Sub

    Private Sub DGV_InputStock(id As String)
        Try
            If id = "" Then
                Exit Sub
            End If
            dgv_forminputstock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            dgv_forminputstock.DataSource = Nothing
            dgv_forminputstock.Rows.Clear()
            dgv_forminputstock.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryInputStockDetail As String = "SELECT ID,MATERIAL,LOT_NO,TRACEABILITY TRACE,BATCH_NO,INV_CTRL_DATE ICD,QTY FROM STOCK_CARD WHERE MTS_NO='" & txt_forminputstock_mts_no.Text & "' and MATERIAL='" & id & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Receive From Main Store'"
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

                    dgv_forminputstock.Columns.Insert(7, delete)

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
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txt_forminputstock_qrcode_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_forminputstock_qrcode.PreviewKeyDown
        Try
            If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
                Call Database.koneksi_database()

                Dim adapter As SqlDataAdapter
                Dim ds As New DataTable

                'If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
                QRCode.Baca(txt_forminputstock_qrcode.Text)

                Dim sql As String = "SELECT * FROM MASTER_MATERIAL where PART_NUMBER='" & globVar.QRCode_PN & "'"
                adapter = New SqlDataAdapter(sql, Database.koneksi)
                adapter.Fill(ds)

                If ds.Rows.Count > 0 Then

                    Dim queryCheckInputStockDetail As String = "SELECT * FROM STOCK_CARD where lot_no=" & globVar.QRCode_lot & " AND MATERIAL='" & globVar.QRCode_PN & "' and mts_no='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "'"
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

                            Dim sqlInsertInputStockDetail As String = "INSERT INTO STOCK_CARD (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, QRCODE, MTS_NO,DEPARTMENT, STANDARD_PACK,STATUS,ACTUAL_QTY)
                                    VALUES ('" & globVar.QRCode_PN & "'," & globVar.QRCode_Qty & "," & globVar.QRCode_Inv & "," & globVar.QRCode_Traceability & "," & globVar.QRCode_lot & ",'" & globVar.QRCode_Batch & "','" & txt_forminputstock_qrcode.Text.Trim & "'," & txt_forminputstock_mts_no.Text & ",'" & globVar.department & "','" & StandartPack & "','Receive From Main Store'," & globVar.QRCode_Qty & ")"
                            Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                            If cmdInsertInputStockDetail.ExecuteNonQuery() Then
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
            End If
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgv_forminputstock_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_forminputstock.CellValueChanged
        Call Database.koneksi_database()
        If dgv_forminputstock.CurrentCell.ColumnIndex = 6 Then
            Try
                Dim Sql As String = "update STOCK_CARD set QTY=" & dgv_forminputstock.Rows(e.RowIndex).Cells("QTY").Value & ", ACTUAL_QTY=" & dgv_forminputstock.Rows(e.RowIndex).Cells("QTY").Value & " where ID=" & dgv_forminputstock.Rows(e.RowIndex).Cells("ID").Value
                Dim cmd = New SqlCommand(Sql, Database.koneksi)
                cmd.ExecuteNonQuery()

                DGV_InputStock(TextBox1.Text)
                treeView_show()
                RJMessageBox.Show("Success updated data")
            Catch ex As Exception
                RJMessageBox.Show("Failed" & ex.Message)
            End Try

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result = RJMessageBox.Show("The data has been saved cannot be changed. Are you sure to save this MTS Data?", "Warning", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then
            Try
                Dim Sql As String = "UPDATE STOCK_CARD SET [SAVE]=1, DATETIME_SAVE=GETDATE() FROM STOCK_CARD WHERE MTS_NO='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Receive From Main Store'"
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
                RJMessageBox.Show("failed" & ex.Message)
            End Try
        End If
    End Sub

    Private Sub dgv_forminputstock_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_forminputstock.CellClick
        If dgv_forminputstock.Columns(e.ColumnIndex).Name = "delete" Then
            Dim result = RJMessageBox.Show("Are you sure to delete?", "Warning", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from STOCK_CARD where id=" & dgv_forminputstock.Rows(e.RowIndex).Cells("ID").Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    If cmd.ExecuteNonQuery() Then
                        DGV_InputStock(dgv_forminputstock.Rows(e.RowIndex).Cells(1).Value)
                        treeView_show()
                        RJMessageBox.Show("Success delete.")
                    End If
                Catch ex As Exception
                    RJMessageBox.Show("failed" & ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub treeView_show()
        TreeView1.Nodes.Clear()
        Dim queryInputStock As String = "SELECT DISTINCT(MATERIAL),SUM(QTY) QTY FROM STOCK_CARD WHERE MTS_NO=" & txt_forminputstock_mts_no.Text & " AND DEPARTMENT='" & globVar.department & "' AND STATUS='Receive From Main Store' GROUP BY MATERIAL"
        Dim dtInputStock As DataTable = Database.GetData(queryInputStock)

        TreeView1.Nodes.Add("MTS No : " & txt_forminputstock_mts_no.Text)

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
                    Dim queryCheck As String = "SELECT * FROM STOCK_CARD WHERE MTS_NO=" & txt_forminputstock_mts_no.Text & " AND DEPARTMENT = '" & globVar.department & "' and [save]=1 and status != 'Receive From Main Store'"
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
            RJMessageBox.Show(ex.Message)
        End Try
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
            txtmanualPN.Select()
            txt_forminputstock_qrcode.ReadOnly = True
            Button3.Enabled = True
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If txtmanualBatch.Text <> "" And txtmanualInv.Text <> "" And txtmanualLot.Text <> "" And txtmanualPN.Text <> "" And txtmanualQty.Text <> "" And txtmanualTraceability.Text <> "" Then
            If Not IsNumeric(txtmanualQty.Text) Or Not IsNumeric(txtmanualLot.Text) Then
                RJMessageBox.Show("Sorry. Lot No / Qty must be Number.")
                Exit Sub
            End If

            Dim adapter As SqlDataAdapter
            Dim ds As New DataTable

            Dim sql As String = "SELECT * FROM MASTER_MATERIAL where PART_NUMBER='" & txtmanualPN.Text & "'"
            adapter = New SqlDataAdapter(sql, Database.koneksi)
            adapter.Fill(ds)

            If ds.Rows.Count > 0 Then

                Dim queryCheckInputStockDetail As String = "SELECT * FROM STOCK_CARD where lot_no=" & txtmanualLot.Text & " AND MATERIAL='" & txtmanualPN.Text & "' and mts_no='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "'"
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

                        If ds.Rows(0).Item("STANDARD_QTY") = txtmanualQty.Text Then
                            StandartPack = "YES"
                        Else
                            StandartPack = "NO"
                        End If

                        Dim sqlInsertInputStockDetail As String = "INSERT INTO STOCK_CARD (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, QRCODE, MTS_NO,DEPARTMENT, STANDARD_PACK,STATUS,ACTUAL_QTY)
                                    VALUES ('" & txtmanualPN.Text & "'," & txtmanualQty.Text & ",'" & txtmanualInv.Text & "','" & txtmanualTraceability.Text & "'," & txtmanualLot.Text & ",'" & txtmanualBatch.Text & "','Manual Input'," & txt_forminputstock_mts_no.Text & ",'" & globVar.department & "','" & StandartPack & "','Receive From Main Store'," & txtmanualQty.Text & ")"
                        Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)

                        If cmdInsertInputStockDetail.ExecuteNonQuery() Then
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
                        RJMessageBox.Show("Error Insert" & ex.Message)
                    End Try
                End If
            Else
                RJMessageBox.Show("Sorry, Part Number not exist in data Master Material.")
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
                RJMessageBox.Show("failed" & ex.Message)
            End Try
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lbl_Info.Visible = Not lbl_Info.Visible
    End Sub
End Class
