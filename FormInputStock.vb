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

        checkQr.Enabled = False
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
        Dim queryInputStockDetail As String = "SELECT ID,PART_NUMBER [P.NUMBER],LOT_NO,TRACEABILITY TRACE,BATCH_NO,INV_CTRL_DATE ICD,QTY FROM IN_MINISTORE WHERE MTS_NO=" & txt_forminputstock_mts_no.Text & " and part_number=" & id
        Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
        dgv_forminputstock.DataSource = dtInputStockDetail

        Dim queryCheckLock As String = "SELECT TOP 1 * FROM IN_MINISTORE WHERE MTS_NO = " & txt_forminputstock_mts_no.Text
        Dim dtCheckLock As DataTable = Database.GetData(queryCheckLock)

        If dtCheckLock.Rows.Count > 0 Then
            If dtCheckLock.Rows(0).Item("LOCK") = 0 Then
                Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
                delete.Name = "delete"
                delete.HeaderText = "Delete"
                delete.Width = 50
                delete.Text = "Delete"
                delete.UseColumnTextForButtonValue = True

                dgv_forminputstock.Columns.Insert(7, delete)

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
            dgv_forminputstock.ReadOnly = dtCheckLock.Rows(0).Item("LOCK")
        Else
            dgv_forminputstock.ReadOnly = False
        End If
    End Sub

    Private Sub txt_forminputstock_qrcode_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_forminputstock_qrcode.PreviewKeyDown
        Call Database.koneksi_database()

        Dim adapter As SqlDataAdapter
        Dim ds As New DataSet

        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) And Len(Me.txt_forminputstock_qrcode.Text) >= 64 Then
            Dim splitQRCode() As String = txt_forminputstock_qrcode.Text.Split(New String() {"1P", "12D", "4L", "MLX"}, StringSplitOptions.None)
            Dim splitQRCode1P() As String = splitQRCode(1).Split(New String() {"Q", "S", "13Q", "B"}, StringSplitOptions.None)

            Dim sql As String = "SELECT * FROM MASTER_MATERIAL where PART_NUMBER='" & splitQRCode1P(0) & "'"
            adapter = New SqlDataAdapter(sql, Database.koneksi)
            adapter.Fill(ds)

            If ds.Tables(0).Rows.Count > 0 Then

                Dim queryCheckInputStockDetail As String = "SELECT * FROM IN_MINISTORE where lot_no=" & splitQRCode1P(3) & " AND part_number=" & splitQRCode1P(0) & " and mts_no=" & txt_forminputstock_mts_no.Text
                Dim dtCheckInputStockDetail As DataTable = Database.GetData(queryCheckInputStockDetail)

                If dtCheckInputStockDetail.Rows.Count > 0 Then
                    MessageBox.Show("This QRCode Already Scan")

                    txt_forminputstock_qrcode.Text = ""
                    txt_forminputstock_qrcode.Select()

                    dgv_forminputstock.DataSource = Nothing
                    dgv_forminputstock.Rows.Clear()
                    dgv_forminputstock.Columns.Clear()

                    treeView_show()
                Else
                    Try
                        Dim sqlInsertInputStockDetail As String = "INSERT INTO IN_MINISTORE (PART_NUMBER, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, COUNTRY, QRCODE, MTS_NO)
                                    VALUES (" & splitQRCode1P(0) & "," & splitQRCode1P(1) & "," & splitQRCode(2) & "," & splitQRCode1P(2) & "," & splitQRCode1P(3) & ",'" & splitQRCode1P(4) & "','" & splitQRCode(3).Trim() & "','" & txt_forminputstock_qrcode.Text.Trim & "'," & txt_forminputstock_mts_no.Text & ")"
                        Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                        If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                            txt_forminputstock_qrcode.Text = ""
                            txt_forminputstock_qrcode.Select()

                            dgv_forminputstock.DataSource = Nothing
                            dgv_forminputstock.Rows.Clear()
                            dgv_forminputstock.Columns.Clear()

                            treeView_show()
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error Insert" & ex.Message)
                    End Try
                End If
            Else
                MessageBox.Show("Part Number not in DB")
                txt_forminputstock_qrcode.Text = ""
                txt_forminputstock_qrcode.Select()
            End If
        End If
    End Sub

    Private Sub dgv_forminputstock_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_forminputstock.CellValueChanged
        Call Database.koneksi_database()
        If dgv_forminputstock.CurrentCell.ColumnIndex = 6 Then
            Try
                Dim Sql As String = "update IN_MINISTORE set QTY=" & dgv_forminputstock.Rows(e.RowIndex).Cells("QTY").Value & " where ID=" & dgv_forminputstock.Rows(e.RowIndex).Cells("ID").Value
                Dim cmd = New SqlCommand(Sql, Database.koneksi)
                cmd.ExecuteNonQuery()

                DGV_InputStock(TextBox1.Text)
                treeView_show()
                MessageBox.Show("Success updated data")
            Catch ex As Exception
                MessageBox.Show("Failed" & ex.Message)
            End Try

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result = MessageBox.Show("The data has been saved cannot be changed. Are you sure to save this MTS Data?", "Warning", MessageBoxButtons.YesNo)

        If dgv_forminputstock.Rows.Count > 0 Then
            If result = DialogResult.Yes Then
                Try
                    Dim Sql As String = "UPDATE IN_MINISTORE SET LOCK=1, DATETIME_LOCK=GETDATE() FROM IN_MINISTORE WHERE MTS_NO=" & txt_forminputstock_mts_no.Text
                    Dim cmd = New SqlCommand(Sql, Database.koneksi)
                    If (cmd.ExecuteNonQuery() > 0) Then

                        Dim queryCheckinMiniStore As String = "select * from IN_MINISTORE where MTS_NO=" & txt_forminputstock_mts_no.Text
                        Dim dtCheckinMiniStore As DataTable = Database.GetData(queryCheckinMiniStore)

                        For i = 0 To dtCheckinMiniStore.Rows.Count - 1
                            Dim queryCheckStockMiniStore As String = "select * from STOCK_MINISTORE where PART_NUMBER=" & dtCheckinMiniStore.Rows(i).Item("PART_NUMBER").ToString & " AND LOT_NO=" & dtCheckinMiniStore.Rows(i).Item("LOT_NO").ToString
                            Dim dtCheckStockMiniStore As DataTable = Database.GetData(queryCheckStockMiniStore)
                            If dtCheckStockMiniStore.Rows.Count > 0 Then
                                Dim sqlInsertStockMinistore As String = "UPDATE STOCK_MINISTORE SET QTY=" & dtCheckinMiniStore.Rows(i).Item("QTY") & " FROM STOCK_MINISTORE WHERE PART_NUMBER='" & dtCheckinMiniStore.Rows(i).Item("PART_NUMBER").ToString & "' AND LOT_NO='" & dtCheckinMiniStore.Rows(i).Item("LOT_NO").ToString & "'"
                                Dim cmdInsertStockMinistore = New SqlCommand(sqlInsertStockMinistore, Database.koneksi)
                                cmdInsertStockMinistore.ExecuteNonQuery()
                            Else
                                Dim sqlInsertStockMinistore As String = "INSERT INTO STOCK_MINISTORE(PART_NUMBER,QTY,TRACEABILITY,LOT_NO,INV_CTRL_DATE,BATCH_NO) 
VALUES ('" & dtCheckinMiniStore.Rows(i).Item("PART_NUMBER") & "'," & dtCheckinMiniStore.Rows(i).Item("QTY") & ",'" & dtCheckinMiniStore.Rows(i).Item("TRACEABILITY") & "','" & dtCheckinMiniStore.Rows(i).Item("LOT_NO") & "','" & dtCheckinMiniStore.Rows(i).Item("INV_CTRL_DATE") & "','" & dtCheckinMiniStore.Rows(i).Item("BATCH_NO") & "')"
                                Dim cmdInsertStockMinistore = New SqlCommand(sqlInsertStockMinistore, Database.koneksi)
                                cmdInsertStockMinistore.ExecuteNonQuery()
                            End If
                        Next

                        dgv_forminputstock.DataSource = Nothing
                        treeView_show()
                        txt_forminputstock_qrcode.ReadOnly = True
                        checkQr.Enabled = False
                        Button2.Enabled = False
                        dgv_forminputstock.ReadOnly = True

                        MessageBox.Show("Success Save The Data")
                    End If
                Catch ex As Exception
                    MessageBox.Show("failed" & ex.Message)
                End Try
            End If
        Else
            MessageBox.Show("Cannot save with 0 Record")
        End If
    End Sub

    Private Sub dgv_forminputstock_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_forminputstock.CellClick
        If dgv_forminputstock.Columns(e.ColumnIndex).Name = "delete" Then
            Dim result = MessageBox.Show("Are you sure to delete?", "Warning", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from IN_MINISTORE where id=" & dgv_forminputstock.Rows(e.RowIndex).Cells(0).Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    If cmd.ExecuteNonQuery() Then
                        DGV_InputStock(dgv_forminputstock.Rows(e.RowIndex).Cells(1).Value)
                        treeView_show()
                        MessageBox.Show("Success delete.")
                    End If
                Catch ex As Exception
                    MessageBox.Show("failed" & ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub treeView_show()
        TreeView1.Nodes.Clear()
        Dim queryInputStock As String = "SELECT DISTINCT(PART_NUMBER),SUM(QTY) QTY FROM IN_MINISTORE WHERE MTS_NO=" & txt_forminputstock_mts_no.Text & " GROUP BY PART_NUMBER"
        Dim dtInputStock As DataTable = Database.GetData(queryInputStock)

        TreeView1.Nodes.Add("MTS No : " & txt_forminputstock_mts_no.Text)

        For i = 0 To dtInputStock.Rows.Count - 1
            TreeView1.Nodes(0).Nodes.Add(dtInputStock.Rows(i).Item("PART_NUMBER").ToString, "PN : " & dtInputStock.Rows(i).Item("PART_NUMBER").ToString & " - Qty : " & dtInputStock.Rows(i).Item("QTY").ToString)
        Next
        TreeView1.ExpandAll()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If txt_forminputstock_mts_no.Text = "" Then
            MessageBox.Show("MTS cannot be null.")
            txt_forminputstock_mts_no.Select()
        Else
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

            Dim queryCheckLock As String = "SELECT TOP 1 * FROM IN_MINISTORE WHERE MTS_NO = " & txt_forminputstock_mts_no.Text
            Dim dtCheckLock As DataTable = Database.GetData(queryCheckLock)

            If dtCheckLock.Rows.Count > 0 Then
                If dtCheckLock.Rows(0).Item("LOCK") = 0 Then
                    Button2.Enabled = True
                Else
                    txt_forminputstock_qrcode.ReadOnly = True
                    checkQr.Enabled = False
                    Button2.Enabled = False
                    dgv_forminputstock.ReadOnly = True
                End If
            End If

        End If
    End Sub

    'Private Sub txt_forminputstock_mts_no_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_forminputstock_mts_no.PreviewKeyDown
    '    If e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter Then
    '        If txt_forminputstock_mts_no.Text = "" Then
    '            MessageBox.Show("MTS cannot be null.")
    '            txt_forminputstock_mts_no.Select()
    '        Else
    '            txt_forminputstock_mts_no.ReadOnly = True
    '            checkQr.Enabled = True

    '            If checkQr.Checked Then
    '                txt_forminputstock_qrcode.ReadOnly = False
    '                txt_forminputstock_qrcode.Select()

    '                txtmanualPN.ReadOnly = True
    '                txtmanualTraceability.ReadOnly = True
    '                txtmanualInv.ReadOnly = True
    '                txtmanualBatch.ReadOnly = True
    '                txtmanualLot.ReadOnly = True
    '                txtmanualQty.ReadOnly = True
    '            Else
    '                txtmanualPN.ReadOnly = False
    '                txtmanualTraceability.ReadOnly = False
    '                txtmanualInv.ReadOnly = False
    '                txtmanualBatch.ReadOnly = False
    '                txtmanualLot.ReadOnly = False
    '                txtmanualQty.ReadOnly = False
    '                txtmanualPN.Select()
    '                txt_forminputstock_qrcode.ReadOnly = True
    '            End If

    '            treeView_show()

    '            Dim queryCheckLock As String = "SELECT TOP 1 * FROM IN_MINISTORE WHERE MTS_NO = " & txt_forminputstock_mts_no.Text
    '            Dim dtCheckLock As DataTable = Database.GetData(queryCheckLock)

    '            If dtCheckLock.Rows.Count > 0 Then
    '                If dtCheckLock.Rows(0).Item("LOCK") = 0 Then
    '                    Button2.Enabled = True
    '                Else
    '                    txt_forminputstock_qrcode.ReadOnly = True
    '                    checkQr.Enabled = False
    '                    Button2.Enabled = False
    '                    dgv_forminputstock.ReadOnly = True
    '                End If
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub txt_forminputstock_mts_no_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_forminputstock_mts_no.PreviewKeyDown
        If e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter Then
            If txt_forminputstock_mts_no.Text = "" Then
                MessageBox.Show("MTS cannot be null.")
                txt_forminputstock_mts_no.Select()
            Else
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

                Dim queryCheckLock As String = "SELECT TOP 1 * FROM IN_MINISTORE WHERE MTS_NO = " & txt_forminputstock_mts_no.Text
                Dim dtCheckLock As DataTable = Database.GetData(queryCheckLock)

                If dtCheckLock.Rows.Count > 0 Then
                    If dtCheckLock.Rows(0).Item("LOCK") = 0 Then
                        Button2.Enabled = True
                    Else
                        txt_forminputstock_qrcode.ReadOnly = True
                        checkQr.Enabled = False
                        Button2.Enabled = False
                        dgv_forminputstock.ReadOnly = True
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
    End Sub

    Private Sub txtmanualQty_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtmanualQty.PreviewKeyDown

        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
            If txtmanualBatch.Text <> "" And txtmanualInv.Text <> "" And txtmanualLot.Text <> "" And txtmanualPN.Text <> "" And txtmanualQty.Text <> "" And txtmanualTraceability.Text <> "" Then
                Dim adapter As SqlDataAdapter
                Dim ds As New DataSet

                Dim sql As String = "SELECT * FROM MASTER_MATERIAL where PART_NUMBER='" & txtmanualPN.Text & "'"
                adapter = New SqlDataAdapter(sql, Database.koneksi)
                adapter.Fill(ds)

                If ds.Tables(0).Rows.Count > 0 Then

                    Dim queryCheckInputStockDetail As String = "SELECT * FROM IN_MINISTORE where lot_no=" & txtmanualLot.Text & " AND part_number=" & txtmanualPN.Text & " and mts_no=" & txt_forminputstock_mts_no.Text
                    Dim dtCheckInputStockDetail As DataTable = Database.GetData(queryCheckInputStockDetail)

                    If dtCheckInputStockDetail.Rows.Count > 0 Then
                        MessageBox.Show("Part Number & Lot No already in DB")

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
                    Else
                        Try
                            Dim sqlInsertInputStockDetail As String = "INSERT INTO IN_MINISTORE (PART_NUMBER, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, MTS_NO)
                                    VALUES (" & txtmanualPN.Text & "," & txtmanualQty.Text & "," & txtmanualInv.Text & "," & txtmanualTraceability.Text & "," & txtmanualLot.Text & ",'" & txtmanualBatch.Text & "'," & txt_forminputstock_mts_no.Text & ")"
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
                            MessageBox.Show("Error Insert" & ex.Message)
                        End Try
                    End If
                Else
                    MessageBox.Show("Sorry, Part Number not exist in data Master Material.")
                End If
            Else
                MessageBox.Show("Please fill all form")
            End If
        End If
    End Sub
End Class
