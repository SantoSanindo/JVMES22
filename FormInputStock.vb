Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class FormInputStock

    Private Sub FormInputStock_Load(sender As Object, e As EventArgs) Handles Me.Load
        txt_forminputstock_qrcode.ReadOnly = True

        TreeView1.Nodes.Clear()
        dgv_forminputstock.DataSource = Nothing
        dgv_forminputstock.Columns.Clear()
        dgv_forminputstock.Rows.Clear()

        dgv_forminputstock.ReadOnly = False

        Button2.Enabled = False
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
        Dim queryInputStockDetail As String = "SELECT ID,PART_NUMBER,LOT_NO,TRACEABILITY,BATCH_NO,INV_CTRL_DATE,QTY FROM input_stock_detail WHERE fk_input_stock=" & id
        Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
        dgv_forminputstock.DataSource = dtInputStockDetail

        Dim queryCheckLock As String = "SELECT TOP 1 * FROM INPUT_STOCK ISS, INPUT_STOCK_DETAIL ISD WHERE ISS.ID = ISD.FK_INPUT_STOCK AND ISS.MTS_NO = " & txt_forminputstock_mts_no.Text
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


            Dim sql As String = "SELECT * FROM MASTER_MATERIAL where [PART_NUMBER]='" & splitQRCode1P(0) & "'"
            adapter = New SqlDataAdapter(sql, Database.koneksi)
            adapter.Fill(ds)

            If ds.Tables(0).Rows.Count > 0 Then

                Dim queryCheckInputStock As String = "SELECT * FROM input_stock where mts_no=" & txt_forminputstock_mts_no.Text & " AND part_number=" & splitQRCode1P(0)
                Dim dtCheckInputStock As DataTable = Database.GetData(queryCheckInputStock)

                If dtCheckInputStock.Rows.Count > 0 Then
                    Dim queryCheckInputStockDetail As String = "SELECT * FROM input_stock_detail isd, input_stock iss where isd.lot_no=" & splitQRCode1P(3) & " AND isd.part_number=" & splitQRCode1P(0) & " and iss.id=isd.fk_input_stock and iss.mts_no=" & txt_forminputstock_mts_no.Text
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
                            Dim sqlUpdateInputStock As String = "UPDATE INPUT_STOCK SET qty=qty+" & splitQRCode1P(1) & " WHERE mts_no=" & txt_forminputstock_mts_no.Text & " AND part_number=" & splitQRCode1P(0)
                            Dim cmd = New SqlCommand(sqlUpdateInputStock, Database.koneksi)
                            If cmd.ExecuteNonQuery() Then

                                Dim sqlInsertInputStockDetail As String = "INSERT INTO INPUT_STOCK_DETAIL (PART_NUMBER, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, COUNTRY, QRCODE, FK_INPUT_STOCK)
                                    VALUES (" & splitQRCode1P(0) & "," & splitQRCode1P(1) & "," & splitQRCode(2) & "," & splitQRCode1P(2) & "," & splitQRCode1P(3) & ",'" & splitQRCode1P(4) & "','" & splitQRCode(3).Trim() & "','" & txt_forminputstock_qrcode.Text.Trim & "'," & dtCheckInputStock.Rows(0).Item(6) & ")"
                                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                                    txt_forminputstock_qrcode.Text = ""
                                    txt_forminputstock_qrcode.Select()

                                    dgv_forminputstock.DataSource = Nothing
                                    dgv_forminputstock.Rows.Clear()
                                    dgv_forminputstock.Columns.Clear()

                                    treeView_show()
                                End If
                            End If
                        Catch ex As Exception
                            MessageBox.Show("Error Insert" & ex.Message)
                        End Try
                    End If
                Else
                    Try
                        Dim sqlInsertInputStock As String = "INSERT INTO INPUT_STOCK (MTS_NO, PART_NUMBER, QTY, INV_CTRL_DATE, TRACEABILITY, BATCH_NO)
                            VALUES (" & txt_forminputstock_mts_no.Text & "," & splitQRCode1P(0) & "," & splitQRCode1P(1) & "," & splitQRCode(2) & "," & splitQRCode1P(2) & ",'" & splitQRCode1P(4) & "');Select @@Identity"
                        Dim cmdInsertInputStock = New SqlCommand(sqlInsertInputStock, Database.koneksi)

                        Dim newID As Integer = cmdInsertInputStock.ExecuteScalar()

                        Dim sqlInsertInputStockDetail As String = "INSERT INTO INPUT_STOCK_DETAIL (PART_NUMBER, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, COUNTRY, QRCODE, FK_INPUT_STOCK)
                            VALUES (" & splitQRCode1P(0) & "," & splitQRCode1P(1) & "," & splitQRCode(2) & "," & splitQRCode1P(2) & "," & splitQRCode1P(3) & ",'" & splitQRCode1P(4) & "','" & splitQRCode(3).Trim() & "','" & txt_forminputstock_qrcode.Text.Trim & "'," & newID & ")"
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
                Dim Sql As String = "update INPUT_STOCK_DETAIL set QTY=" & dgv_forminputstock.Rows(e.RowIndex).Cells("QTY").Value & " where ID=" & dgv_forminputstock.Rows(e.RowIndex).Cells("ID").Value
                Dim cmd = New SqlCommand(Sql, Database.koneksi)
                cmd.ExecuteNonQuery()

                Dim SqlUpdateStock As String = "update INPUT_STOCK set QTY=(SELECT SUM(QTY) QTY FROM INPUT_STOCK_DETAIL WHERE FK_INPUT_STOCK = " & TreeView1.SelectedNode.Name & ") where ID=" & TreeView1.SelectedNode.Name
                Dim cmdUpdateStock = New SqlCommand(SqlUpdateStock, Database.koneksi)
                cmdUpdateStock.ExecuteNonQuery()

                DGV_InputStock(TreeView1.SelectedNode.Name)
                treeView_show()
                MessageBox.Show("Success updated data")
            Catch ex As Exception
                MessageBox.Show("Failed" & ex.Message)
            End Try

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result = MessageBox.Show("Are you sure to lock this MTS Data. The data has been lock cannot be change?", "Warning", MessageBoxButtons.YesNo)

        If dgv_forminputstock.Rows.Count > 0 Then
            If result = DialogResult.Yes Then
                Try
                    Dim Sql As String = "UPDATE ISD SET ISD.LOCK=1, ISD.DATETIME_LOCK=GETDATE() FROM INPUT_STOCK_DETAIL ISD, INPUT_STOCK ISS WHERE ISD.FK_INPUT_STOCK=ISS.ID AND ISS.MTS_NO=" & txt_forminputstock_mts_no.Text
                    Dim cmd = New SqlCommand(Sql, Database.koneksi)
                    If (cmd.ExecuteNonQuery() > 0) Then
                        txt_forminputstock_mts_no.Text = ""
                        txt_forminputstock_mts_no.ReadOnly = False
                        txt_forminputstock_mts_no.Select()

                        txt_forminputstock_qrcode.Text = ""
                        txt_forminputstock_qrcode.ReadOnly = True

                        dgv_forminputstock.ReadOnly = True

                        MessageBox.Show("Success Lock")
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
                    Dim sql As String = "delete from input_stock_detail where id=" & dgv_forminputstock.Rows(e.RowIndex).Cells(0).Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    If cmd.ExecuteNonQuery() Then
                        DGV_InputStock(TreeView1.SelectedNode.Name)
                        MessageBox.Show("Success delete.")

                        Dim SqlUpdateStock As String = "update INPUT_STOCK set QTY=(SELECT SUM(QTY) QTY FROM INPUT_STOCK_DETAIL WHERE FK_INPUT_STOCK = " & TreeView1.SelectedNode.Name & ") where ID=" & TreeView1.SelectedNode.Name
                        Dim cmdUpdateStock = New SqlCommand(SqlUpdateStock, Database.koneksi)
                        cmdUpdateStock.ExecuteNonQuery()

                        treeView_show()
                    End If
                Catch ex As Exception
                    MessageBox.Show("failed" & ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub treeView_show()
        TreeView1.Nodes.Clear()
        Dim queryInputStock As String = "SELECT * FROM input_stock WHERE mts_no=" & txt_forminputstock_mts_no.Text
        Dim dtInputStock As DataTable = Database.GetData(queryInputStock)

        TreeView1.Nodes.Add("MTS No : " & txt_forminputstock_mts_no.Text)

        For i = 0 To dtInputStock.Rows.Count - 1
            TreeView1.Nodes(0).Nodes.Add(dtInputStock.Rows(i).Item("ID").ToString, "PN : " & dtInputStock.Rows(i).Item("part_number").ToString & " - Qty : " & dtInputStock.Rows(i).Item("qty").ToString)
        Next
        TreeView1.ExpandAll()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If txt_forminputstock_mts_no.Text = "" Then
            MessageBox.Show("MTS cannot be null.")
            txt_forminputstock_mts_no.Select()
        Else
            txt_forminputstock_mts_no.ReadOnly = True
            txt_forminputstock_qrcode.ReadOnly = False
            txt_forminputstock_qrcode.Select()

            treeView_show()

            Dim queryCheckLock As String = "SELECT TOP 1 * FROM INPUT_STOCK ISS, INPUT_STOCK_DETAIL ISD WHERE ISS.ID = ISD.FK_INPUT_STOCK AND ISS.MTS_NO = " & txt_forminputstock_mts_no.Text
            Dim dtCheckLock As DataTable = Database.GetData(queryCheckLock)

            If dtCheckLock.Rows.Count > 0 Then
                If dtCheckLock.Rows(0).Item("LOCK") = 0 Then
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

        DGV_InputStock(id)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        dgv_forminputstock.DataSource = Nothing
        dgv_forminputstock.Rows.Clear()
        dgv_forminputstock.Columns.Clear()

        TreeView1.Nodes.Clear()

        txt_forminputstock_mts_no.ReadOnly = False
        txt_forminputstock_qrcode.ReadOnly = True

        txt_forminputstock_mts_no.Text = ""
        txt_forminputstock_qrcode.Text = ""

        Button2.Enabled = False
    End Sub

    Private Sub txt_forminputstock_mts_no_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_forminputstock_mts_no.PreviewKeyDown
        If e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter Then
            If txt_forminputstock_mts_no.Text = "" Then
                MessageBox.Show("MTS cannot be null.")
                txt_forminputstock_mts_no.Select()
            Else
                txt_forminputstock_mts_no.ReadOnly = True
                txt_forminputstock_qrcode.ReadOnly = False
                txt_forminputstock_qrcode.Select()

                treeView_show()
            End If
        End If
    End Sub
End Class