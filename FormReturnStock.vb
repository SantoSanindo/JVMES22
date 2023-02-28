Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar

Public Class FormReturnStock
    Private Sub FormInputStock_Load(sender As Object, e As EventArgs) Handles Me.Load
        txt_forminputstock_qrcode.ReadOnly = True
        txtmanualPN.ReadOnly = True
        txtmanualLot.ReadOnly = True

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
        Dim queryInputStockDetail As String = "SELECT ID,MATERIAL,LOT_NO,TRACEABILITY TRACE,BATCH_NO,INV_CTRL_DATE ICD,QTY FROM STOCK_CARD WHERE MTS_NO='" & txt_forminputstock_mts_no.Text & "' and MATERIAL='" & id & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Return To Main Store'"
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
            dgv_forminputstock.ReadOnly = dtCheckLock.Rows(0).Item("SAVE")
        Else
            dgv_forminputstock.ReadOnly = False
        End If
    End Sub

    Private Sub txt_forminputstock_qrcode_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_forminputstock_qrcode.PreviewKeyDown
        Call Database.koneksi_database()

        Dim adapter As SqlDataAdapter
        Dim ds As New DataTable

        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) And Len(Me.txt_forminputstock_qrcode.Text) >= 64 Then
            QRCode.Baca(txt_forminputstock_qrcode.Text)

            Dim sql As String = "SELECT * FROM STOCK_CARD where MATERIAL='" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' AND (STATUS='Receive From Production' or (STATUS='Receive From Main Store' AND [SAVE]=1)) and department='" & globVar.department & "' and actual_qty>0"
            adapter = New SqlDataAdapter(sql, Database.koneksi)
            adapter.Fill(ds)

            If ds.Rows.Count > 0 Then

                Dim queryCheckInputStockDetail As String = "SELECT * FROM STOCK_CARD where lot_no=" & ds.Rows(0).Item("lot_no") & " AND MATERIAL='" & ds.Rows(0).Item("material") & "' and mts_no='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' AND (STATUS='Receive From Production' or (STATUS='Receive From Main Store' AND [SAVE]=1))"
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
                        Dim sqlInsertInputStockDetail As String = "INSERT INTO STOCK_CARD (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, MTS_NO,DEPARTMENT, STANDARD_PACK,STATUS,ACTUAL_QTY)
                                    VALUES (" & ds.Rows(0).Item("material") & ",'" & ds.Rows(0).Item("actual_qty") & "','" & ds.Rows(0).Item("inv_ctrl_date") & "','" & ds.Rows(0).Item("traceability") & "','" & ds.Rows(0).Item("lot_no") & "','" & ds.Rows(0).Item("batch_no") & "'," & txt_forminputstock_mts_no.Text & ",'" & globVar.department & "','" & ds.Rows(0).Item("STANDARD_PACK") & "','Return To Main Store'," & ds.Rows(0).Item("actual_qty") & ")"
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
                MessageBox.Show("Part Number not in DB or qty part number is 0")
                txt_forminputstock_qrcode.Text = ""
                txt_forminputstock_qrcode.Select()
            End If
        End If
    End Sub

    Private Sub dgv_forminputstock_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_forminputstock.CellValueChanged
        Call Database.koneksi_database()
        If dgv_forminputstock.CurrentCell.ColumnIndex = 6 Then
            Try
                Dim Sql As String = "update STOCK_CARD set QTY=" & dgv_forminputstock.Rows(e.RowIndex).Cells("QTY").Value & ",ACTUAL_QTY=" & dgv_forminputstock.Rows(e.RowIndex).Cells("QTY").Value & " where ID=" & dgv_forminputstock.Rows(e.RowIndex).Cells("ID").Value
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

        If TreeView1.Nodes(0).Nodes.Count > 0 Then
            If result = DialogResult.Yes Then
                Try
                    Dim Sql As String = "UPDATE STOCK_CARD SET [SAVE]=1, DATETIME_SAVE=GETDATE() FROM STOCK_CARD WHERE MTS_NO='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Return To Main Store'"
                    Dim cmd = New SqlCommand(Sql, Database.koneksi)
                    If (cmd.ExecuteNonQuery() > 0) Then

                        Dim queryCheck As String = "SELECT * FROM STOCK_CARD WHERE MTS_NO=" & txt_forminputstock_mts_no.Text & " AND DEPARTMENT='" & globVar.department & "' AND STATUS='Return To Main Store'"
                        Dim dtCheck As DataTable = Database.GetData(queryCheck)

                        For i = 0 To dtCheck.Rows.Count - 1
                            Dim SqlUpdateQty As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE DEPARTMENT='" & globVar.department & "' AND  ( STATUS='Receive From Main Store' or STATUS='Receive From Production' ) and material=" & dtCheck.Rows(i).Item("MATERIAL") & " and lot_no=" & dtCheck.Rows(i).Item("LOT_NO") & " and [PO] is null"
                            Dim cmdUpdateQty = New SqlCommand(SqlUpdateQty, Database.koneksi)
                            cmdUpdateQty.ExecuteNonQuery()
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
                    Dim sql As String = "delete from STOCK_CARD where id=" & dgv_forminputstock.Rows(e.RowIndex).Cells(0).Value
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
        Dim queryInputStock As String = "SELECT DISTINCT(MATERIAL),SUM(QTY) QTY FROM STOCK_CARD WHERE MTS_NO=" & txt_forminputstock_mts_no.Text & " AND DEPARTMENT='" & globVar.department & "' AND STATUS='Return To Main Store' GROUP BY MATERIAL"
        Dim dtInputStock As DataTable = Database.GetData(queryInputStock)

        TreeView1.Nodes.Add("MTS No : " & txt_forminputstock_mts_no.Text)

        For i = 0 To dtInputStock.Rows.Count - 1
            TreeView1.Nodes(0).Nodes.Add(dtInputStock.Rows(i).Item("MATERIAL").ToString, "PN : " & dtInputStock.Rows(i).Item("MATERIAL").ToString & " - Qty : " & dtInputStock.Rows(i).Item("QTY").ToString)
        Next
        TreeView1.ExpandAll()
    End Sub

    Private Sub txt_forminputstock_mts_no_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_forminputstock_mts_no.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            If txt_forminputstock_mts_no.Text = "" Then
                MessageBox.Show("MTS cannot be null.")
                txt_forminputstock_mts_no.Select()
            Else
                Dim queryCheck As String = "SELECT * FROM STOCK_CARD WHERE MTS_NO=" & txt_forminputstock_mts_no.Text & " AND DEPARTMENT = '" & globVar.department & "' and [save]=1 and status != 'Return To Main Store'"
                Dim dtCheck As DataTable = Database.GetData(queryCheck)
                If dtCheck.Rows.Count > 0 Then
                    MessageBox.Show("Sorry MTS Number already in DB")
                    txt_forminputstock_mts_no.Clear()
                    Exit Sub
                End If

                txt_forminputstock_mts_no.ReadOnly = True
                checkQr.Enabled = True

                If checkQr.Checked Then
                    txt_forminputstock_qrcode.ReadOnly = False
                    txt_forminputstock_qrcode.Select()

                    txtmanualPN.ReadOnly = True
                    txtmanualLot.ReadOnly = True
                Else
                    txtmanualPN.ReadOnly = False
                    txtmanualLot.ReadOnly = False
                    txtmanualPN.Select()
                    txt_forminputstock_qrcode.ReadOnly = True
                End If

                treeView_show()

                Dim queryCheckLock As String = "SELECT TOP 1 * FROM stock_card WHERE MTS_NO = '" & txt_forminputstock_mts_no.Text & "' and department='" & globVar.department & "' and status='Return To Main Store'"
                Dim dtCheckLock As DataTable = Database.GetData(queryCheckLock)

                If dtCheckLock.Rows.Count > 0 Then
                    If dtCheckLock.Rows(0).Item("SAVE") = 0 Then
                        Button2.Enabled = True
                    Else
                        txt_forminputstock_qrcode.ReadOnly = True
                        checkQr.Enabled = False
                        Button2.Enabled = False
                        dgv_forminputstock.ReadOnly = True
                    End If
                Else
                    Button2.Enabled = True
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
        txtmanualLot.ReadOnly = True
        checkQr.Enabled = False

        txt_forminputstock_mts_no.Text = ""
        txt_forminputstock_qrcode.Text = ""

        Button2.Enabled = False
        Button4.Enabled = False
    End Sub

    Private Sub checkQr_CheckStateChanged(sender As Object, e As EventArgs) Handles checkQr.CheckStateChanged
        If checkQr.Checked Then
            txt_forminputstock_qrcode.ReadOnly = False
            txt_forminputstock_qrcode.Select()

            txtmanualPN.ReadOnly = True
            txtmanualLot.ReadOnly = True
            Button4.Enabled = False
        Else
            txtmanualPN.ReadOnly = False
            txtmanualLot.ReadOnly = False
            txtmanualPN.Select()
            txt_forminputstock_qrcode.ReadOnly = True
            Button4.Enabled = True
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If txtmanualLot.Text <> "" And txtmanualPN.Text <> "" Then
            Dim adapter As SqlDataAdapter
            Dim ds As New DataTable

            Dim sql As String = "SELECT * FROM STOCK_CARD where lot_no=" & txtmanualLot.Text & " AND MATERIAL='" & txtmanualPN.Text & "' AND (STATUS='Receive From Production' or (STATUS='Receive From Main Store' AND [SAVE]=1)) and department='" & globVar.department & "'"
            adapter = New SqlDataAdapter(sql, Database.koneksi)
            adapter.Fill(ds)

            If ds.Rows.Count > 0 Then

                Dim queryCheckInputStockDetail As String = "SELECT * FROM STOCK_CARD where lot_no=" & txtmanualLot.Text & " AND MATERIAL='" & txtmanualPN.Text & "' and mts_no='" & txt_forminputstock_mts_no.Text & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Return To Main Store'"
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
                        Dim sqlInsertInputStockDetail As String = "INSERT INTO STOCK_CARD (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, MTS_NO,DEPARTMENT, STANDARD_PACK,STATUS,ACTUAL_QTY)
                                    VALUES (" & txtmanualPN.Text & "," & ds.Rows(0).Item("QTY") & "," & ds.Rows(0).Item("INV_CTRL_DATE") & "," & ds.Rows(0).Item("TRACEABILITY") & "," & txtmanualLot.Text & ",'" & ds.Rows(0).Item("BATCH_NO") & "'," & txt_forminputstock_mts_no.Text & ",'" & globVar.department & "','" & ds.Rows(0).Item("STANDARD_PACK") & "','Return To Main Store'," & ds.Rows(0).Item("QTY") & ")"
                        Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                        If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                            txtmanualPN.Clear()
                            txtmanualLot.Clear()
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
                MessageBox.Show("Part Number not in DB")
                txt_forminputstock_qrcode.Text = ""
                txt_forminputstock_qrcode.Select()
            End If
        Else
            MessageBox.Show("Please fill all form")
        End If
    End Sub

    Private Sub dgv_forminputstock_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgv_forminputstock.DataBindingComplete
        For i As Integer = 0 To dgv_forminputstock.RowCount - 1
            If dgv_forminputstock.Rows(i).Index Mod 2 = 0 Then
                dgv_forminputstock.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_forminputstock.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

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
End Class
