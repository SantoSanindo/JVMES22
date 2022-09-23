Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class FormInputStock

    Private rowIndex As Integer = 0
    Private rowIndexBawah As Integer = 0
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MainForm.txt_mainform_mts_no.ReadOnly = False
        MainForm.txt_mainform_mts_no.Text = ""
        MainForm.txt_mainform_mts_no.Select()
        Me.Close()
    End Sub

    Private Sub FormInputStock_Load(sender As Object, e As EventArgs) Handles Me.Load
        txt_forminputstock_mts_no.Text = MainForm.txt_mainform_mts_no.Text
        txt_forminputstock_qrcode.Select()
        MainForm.txt_mainform_mts_no.ReadOnly = True
        DGV_InputStock()

        Dim queryKunci As String = "select DISTINCT KUNCI from INPUT_STOCK where MTS_NO=" & txt_forminputstock_mts_no.Text
        Dim dsKunci = New DataSet
        Dim adapterKunci = New SqlDataAdapter(queryKunci, Database.koneksi)
        adapterKunci.Fill(dsKunci)

        If dgv_forminputstock.Rows.Count > 0 Then
            If dsKunci.Tables(0).Rows(0).Item("KUNCI") = 1 Then
                txt_forminputstock_qrcode.ReadOnly = True
                Button2.Enabled = False
            Else
                txt_forminputstock_qrcode.ReadOnly = False
                Button2.Enabled = True
            End If
        End If
    End Sub

    Private Sub DGV_InputStock()
        dgv_forminputstock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_forminputstock.DataSource = Nothing
        dgv_forminputstock.Rows.Clear()
        dgv_forminputstock.Columns.Clear()
        Call Database.koneksi_database()
        Dim query As String = "select DISTINCT PART_NUMBER from INPUT_STOCK where MTS_NO=" & txt_forminputstock_mts_no.Text
        Dim dtInputStock As DataTable = Database.GetData(query)

        Dim lihat As DataGridViewButtonColumn = New DataGridViewButtonColumn
        lihat.Name = "lihat"
        lihat.HeaderText = "View"
        lihat.Width = 100
        lihat.Text = "View"
        lihat.UseColumnTextForButtonValue = True
        dgv_forminputstock.Columns.Insert(0, lihat)

        dgv_forminputstock.DataSource = dtInputStock

        Dim qty As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
        qty.Name = "qty"
        qty.HeaderText = "Qty"
        qty.Width = 100
        dgv_forminputstock.Columns.Insert(2, qty)

        Dim lot As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
        lot.Name = "lot"
        lot.HeaderText = "Total Lot"
        lot.Width = 200
        dgv_forminputstock.Columns.Insert(3, lot)

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True

        Dim queryKunci As String = "select DISTINCT KUNCI from INPUT_STOCK where MTS_NO=" & txt_forminputstock_mts_no.Text
        Dim dsKunci = New DataSet
        Dim adapterKunci = New SqlDataAdapter(queryKunci, Database.koneksi)
        adapterKunci.Fill(dsKunci)

        If dgv_forminputstock.Rows.Count Then
            If dsKunci.Tables(0).Rows(0).Item("KUNCI") = 1 Then
                delete.Visible = False
            End If
        End If

        dgv_forminputstock.Columns.Insert(4, delete)

        'Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
        'check.Name = "check"
        'check.HeaderText = "Check"
        'check.Width = 100
        'check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        'dgv_forminputstock.Columns.Insert(0, check)

        If dtInputStock.Rows.Count > 0 Then
            For i As Integer = 0 To dgv_forminputstock.Rows.Count - 1
                Dim queryCount As String = "select count(*) as count from input_stock where part_number=" & dtInputStock.Rows(i).Item("PART_NUMBER").ToString & " and mts_no=" & txt_forminputstock_mts_no.Text
                Dim dsCount = New DataSet
                Dim adapterCount = New SqlDataAdapter(queryCount, Database.koneksi)
                adapterCount.Fill(dsCount)
                dgv_forminputstock.Rows(i).Cells(3).Value = dsCount.Tables(0).Rows(0).Item("count").ToString
            Next
        End If

        If dtInputStock.Rows.Count > 0 Then
            For i As Integer = 0 To dgv_forminputstock.Rows.Count - 1
                Dim querySum As String = "select sum(qty) as sum from input_stock where part_number=" & dtInputStock.Rows(i).Item("PART_NUMBER").ToString & " and mts_no=" & txt_forminputstock_mts_no.Text
                Dim dsSum = New DataSet
                Dim adapterSum = New SqlDataAdapter(querySum, Database.koneksi)
                adapterSum.Fill(dsSum)
                dgv_forminputstock.Rows(i).Cells(2).Value = dsSum.Tables(0).Rows(0).Item("sum").ToString
            Next
        End If

        For i As Integer = 0 To dgv_forminputstock.RowCount - 1
            If dgv_forminputstock.Rows(i).Index Mod 2 = 0 Then
                dgv_forminputstock.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_forminputstock.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub txt_forminputstock_qrcode_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_forminputstock_qrcode.PreviewKeyDown
        Call Database.koneksi_database()

        Dim adapter As SqlDataAdapter
        Dim ds As New DataSet

        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) And Len(Me.txt_forminputstock_qrcode.Text) >= 64 Then
            Dim splitQRCode() As String = txt_forminputstock_qrcode.Text.Split(New String() {"1P", "4L", "12D", "MLX"}, StringSplitOptions.None)
            Dim splitQRCode2() As String = splitQRCode(1).Split(New String() {"Q", "S", "B"}, StringSplitOptions.None)


            Dim sql As String = "SELECT * FROM MASTER_MATERIAL where [PART_NUMBER]='" & splitQRCode2(0) & "'"
            adapter = New SqlDataAdapter(sql, Database.koneksi)
            adapter.Fill(ds)

            If ds.Tables(0).Rows.Count > 0 Then
                Try
                    sql = "INSERT INTO INPUT_STOCK (MTS_NO, PART_NUMBER, QTY, DATE_CODE, COUNTRY, LOT_NO, QRCODE, TRACE_NUMBER, LOT_CODE, CURRENT_QTY) 
                        VALUES (" & txt_forminputstock_mts_no.Text & "," & splitQRCode2(0) & "," & splitQRCode2(1) & ",'" & splitQRCode(2) & "','" & splitQRCode(3).Trim() & "','" & splitQRCode2(3) & "','" & txt_forminputstock_qrcode.Text.Trim & "'," & splitQRCode2(2) & ",'" & splitQRCode2(4) & "'," & splitQRCode2(1) & ")"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()
                    txt_forminputstock_qrcode.Text = ""
                    txt_forminputstock_qrcode.Select()
                    DGV_InputStock()
                    sub_DGV_Bawah(rowIndexBawah)
                Catch ex As Exception
                    MessageBox.Show("Error Insert" & ex.Message)
                End Try
            Else
                MessageBox.Show("Part Number not in DB")
                txt_forminputstock_qrcode.Text = ""
                txt_forminputstock_qrcode.Select()
            End If
        End If
    End Sub

    Private Sub dgv_forminputstock_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DGVBawah.CellValueChanged
        Call Database.koneksi_database()
        If DGVBawah.CurrentCell.ColumnIndex = 2 Then

            Try
                Dim Sql As String = "update INPUT_STOCK set QTY=" & DGVBawah.Rows(e.RowIndex).Cells("QTY").Value & " where NUMBER=" & DGVBawah.Rows(e.RowIndex).Cells("DB").Value
                Dim cmd = New SqlCommand(Sql, Database.koneksi)
                cmd.ExecuteNonQuery()
                DGV_InputStock()
                sub_DGV_Bawah(rowIndexBawah)
                MessageBox.Show("Success updated data")
            Catch ex As Exception
                MessageBox.Show("Failed" & ex.Message)
            End Try

        End If
    End Sub

    Private Sub dgv_forminputstock_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles dgv_forminputstock.PreviewKeyDown
        If e.KeyData = Keys.Delete Then
            Dim result = MessageBox.Show("Are you sure to delete?", "Warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from input_stock where number=" & Me.rowIndex
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()
                    DGV_InputStock()
                    sub_DGV_Bawah(rowIndexBawah)
                    MessageBox.Show("Success deleted.")
                Catch ex As Exception
                    MessageBox.Show("failed" & ex.Message)
                End Try
            Else
                Me.Close()
            End If
        End If
    End Sub

    Private Sub dgv_forminputstock_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGVBawah.CellMouseDoubleClick
        If e.ColumnIndex = 2 Then
            If e.Button = MouseButtons.Left Then
                DGVBawah.Rows(e.RowIndex).Selected = True
                Me.rowIndex = DGVBawah.Rows(e.RowIndex).Cells("QTY").Value
                If globVar.hakAkses <> "leader" Then
                    Dim fLogin As New FormLogin
                    fLogin.ShowDialog()
                End If

                If globVar.hakAkses = "leader" Then
                    DGVBawah.CurrentCell = DGVBawah.Item(2, DGVBawah.CurrentRow.Index)
                    DGVBawah.BeginEdit(True)
                End If
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result = MessageBox.Show("Are you sure to lock. The data has been lock cannot be change?", "Warning", MessageBoxButtons.YesNo)

        If dgv_forminputstock.Rows.Count > 0 Then
            If result = DialogResult.Yes Then
                Try
                    Dim Sql As String = "update INPUT_STOCK set kunci=1 where mts_no=" & txt_forminputstock_mts_no.Text
                    Dim cmd = New SqlCommand(Sql, Database.koneksi)
                    If (cmd.ExecuteNonQuery() > 0) Then
                        MessageBox.Show("Success Lock")

                        Dim adapter As SqlDataAdapter
                        Dim ds As New DataSet
                        Dim sqlCek As String = "select part_number,sum(qty) stock from input_stock where mts_no=" & txt_forminputstock_mts_no.Text & " GROUP BY part_number"
                        adapter = New SqlDataAdapter(sqlCek, Database.koneksi)
                        adapter.Fill(ds)
                        If ds.Tables(0).Rows.Count > 0 Then
                            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                                Dim adapterCheckForInsert As SqlDataAdapter
                                Dim dsCheckForInsert As New DataSet
                                Dim sqlCheckForInsert As String = "select * from stock where part_number=" & ds.Tables(0).Rows(i).Item("part_number")
                                adapterCheckForInsert = New SqlDataAdapter(sqlCheckForInsert, Database.koneksi)
                                adapterCheckForInsert.Fill(dsCheckForInsert)
                                If dsCheckForInsert.Tables(0).Rows.Count > 0 Then
                                    Dim SqlUpdate As String = "update STOCK set STOCK=STOCK+" & ds.Tables(0).Rows(i).Item("stock") & " where part_number=" & ds.Tables(0).Rows(i).Item("part_number")
                                    Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                                    cmdUpdate.ExecuteNonQuery()
                                Else
                                    Dim SqlInsert = "INSERT INTO STOCK (PART_NUMBER, STOCK) VALUES (" & ds.Tables(0).Rows(i).Item("part_number") & "," & ds.Tables(0).Rows(i).Item("stock") & ")"
                                    Dim cmdInsert = New SqlCommand(SqlInsert, Database.koneksi)
                                    cmdInsert.ExecuteNonQuery()
                                End If
                            Next
                        End If
                        MainForm.txt_mainform_mts_no.ReadOnly = False
                        MainForm.txt_mainform_mts_no.Text = ""
                        MainForm.txt_mainform_mts_no.Select()
                        Me.Close()
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
        If e.ColumnIndex = 0 Then
            DGVBawah.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DGVBawah.DataSource = Nothing
            DGVBawah.Rows.Clear()
            DGVBawah.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryDGVBawah As String = "select PART_NUMBER, QTY, DATE_CODE, COUNTRY, TRACE_NUMBER, LOT_NO, LOT_CODE, KUNCI from INPUT_STOCK where MTS_NO=" & txt_forminputstock_mts_no.Text & " and part_number=" & dgv_forminputstock.Rows(e.RowIndex).Cells(1).Value
            Dim dtDGVBawah As DataTable = Database.GetData(queryDGVBawah)

            DGVBawah.DataSource = dtDGVBawah

            sub_DGV_Bawah(e.RowIndex)
        End If

        If e.ColumnIndex = 4 Then
            Dim result = MessageBox.Show("Are you sure to delete?", "Warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from input_stock where part_number=" & dgv_forminputstock.Rows(e.RowIndex).Cells(1).Value & " and mts_no=" & txt_forminputstock_mts_no.Text
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()
                    sub_DGV_Bawah(rowIndexBawah)
                    DGV_InputStock()
                    MessageBox.Show("Success delete.")
                Catch ex As Exception
                    MessageBox.Show("failed" & ex.Message)
                End Try
            End If
        End If
    End Sub

    Sub sub_DGV_Bawah(row As String)
        rowIndexBawah = Int(row)
        DGVBawah.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DGVBawah.DataSource = Nothing
        DGVBawah.Rows.Clear()
        DGVBawah.Columns.Clear()
        Call Database.koneksi_database()
        Dim queryDGVBawah As String = "select NUMBER DB, PART_NUMBER PN, QTY, DATE_CODE DC, COUNTRY, TRACE_NUMBER TRACE_NO, LOT_NO, LOT_CODE from INPUT_STOCK where MTS_NO=" & txt_forminputstock_mts_no.Text & " and part_number=" & dgv_forminputstock.Rows(row).Cells(1).Value
        Dim dtDGVBawah As DataTable = Database.GetData(queryDGVBawah)

        DGVBawah.DataSource = dtDGVBawah

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.Text = "Delete"

        Dim queryKunci As String = "select DISTINCT KUNCI from INPUT_STOCK where MTS_NO=" & txt_forminputstock_mts_no.Text
        Dim dsKunci = New DataSet
        Dim adapterKunci = New SqlDataAdapter(queryKunci, Database.koneksi)
        adapterKunci.Fill(dsKunci)

        If dsKunci.Tables(0).Rows(0).Item("KUNCI") = 1 Then
            delete.Visible = False
        End If

        delete.UseColumnTextForButtonValue = True
        DGVBawah.Columns.Insert(DGVBawah.ColumnCount, delete)

        DGVBawah.Columns(0).Width = 100
        DGVBawah.Columns(2).Width = 100
        DGVBawah.Columns(6).Width = 120

        For i As Integer = 0 To DGVBawah.RowCount - 1
            If DGVBawah.Rows(i).Index Mod 2 = 0 Then
                DGVBawah.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DGVBawah.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        dgv_forminputstock.Rows(0).Cells(4).Value = Nothing
    End Sub

    Private Sub DGVBawah_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVBawah.CellClick
        If e.ColumnIndex = 8 Then
            Dim result = MessageBox.Show("Are you sure to delete?", "Warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from input_stock where number=" & DGVBawah.Rows(e.RowIndex).Cells(0).Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()
                    sub_DGV_Bawah(rowIndexBawah)
                    DGV_InputStock()
                    MessageBox.Show("Delete Success.")
                Catch ex As Exception
                    MessageBox.Show("failed" & ex.Message)
                End Try
            End If
        End If
    End Sub
End Class