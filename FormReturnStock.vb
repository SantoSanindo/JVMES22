Imports System.Data.SqlClient

Public Class FormReturnStock

    Private Sub FormReturnStock_Load(sender As Object, e As EventArgs) Handles Me.Load
        txt_forminputstock_mts_no.Text = MainForm.mts_issue_return_stock.Text
        mts_return.Text = MainForm.mts_return_stock.Text
        txt_forminputstock_qrcode.Select()
        MainForm.mts_issue_return_stock.ReadOnly = True
        MainForm.mts_return_stock.ReadOnly = True

        DGV_ReturnStock()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub txt_forminputstock_qrcode_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_forminputstock_qrcode.PreviewKeyDown
        Call Database.koneksi_database()

        Dim adapter As SqlDataAdapter
        Dim ds As New DataSet

        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) And Len(Me.txt_forminputstock_qrcode.Text) >= 64 Then
            Dim splitQRCode() As String = txt_forminputstock_qrcode.Text.Split(New String() {"1P", "4L", "12D", "MLX"}, StringSplitOptions.None)
            Dim splitQRCode2() As String = splitQRCode(1).Split(New String() {"Q", "S", "B"}, StringSplitOptions.None)


            Dim sql As String = "SELECT * FROM MASTER_MATERIAL where [PART_NUMBER]=" & splitQRCode2(0)
            adapter = New SqlDataAdapter(sql, Database.koneksi)
            adapter.Fill(ds)

            If ds.Tables(0).Rows.Count > 0 Then
                Try
                    sql = "INSERT INTO RETURN_STOCK (MTS_ISSUE, MTS_RETURN, PART_NUMBER, QTY) 
                        VALUES (" & txt_forminputstock_mts_no.Text & "," & mts_return.Text & "," & splitQRCode2(0) & ",(SELECT SUM(CURRENT_QTY) FROM INPUT_STOCK WHERE MTS_NO=" & txt_forminputstock_mts_no.Text & " AND PART_NUMBER=" & splitQRCode2(0) & "))"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    If cmd.ExecuteNonQuery() Then
                        txt_forminputstock_qrcode.Text = ""
                        txt_forminputstock_qrcode.Select()
                        DGV_ReturnStock()
                    End If
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

    Private Sub DGV_ReturnStock()
        dgv_forminputstock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_forminputstock.DataSource = Nothing
        dgv_forminputstock.Rows.Clear()
        dgv_forminputstock.Columns.Clear()
        Call Database.koneksi_database()
        Dim query As String = "select DISTINCT(INS.PART_NUMBER),INS.DATE_CODE,INS.COUNTRY,INS.TRACE_NUMBER,RT.QTY from RETURN_STOCK RT, INPUT_STOCK INS where RT.MTS_ISSUE=" & txt_forminputstock_mts_no.Text & " and RT.MTS_RETURN=" & mts_return.Text & " AND RT.MTS_ISSUE=INS.MTS_NO AND RT.PART_NUMBER=INS.PART_NUMBER"
        Dim dtReturntStock As DataTable = Database.GetData(query)

        'Dim lihat As DataGridViewButtonColumn = New DataGridViewButtonColumn
        'lihat.Name = "lihat"
        'lihat.HeaderText = "View"
        'lihat.Width = 100
        'lihat.Text = "View"
        'lihat.UseColumnTextForButtonValue = True
        'dgv_forminputstock.Columns.Insert(0, lihat)

        dgv_forminputstock.DataSource = dtReturntStock

        'Dim qty As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
        'qty.Name = "qty"
        'qty.HeaderText = "Qty"
        'qty.Width = 100
        'dgv_forminputstock.Columns.Insert(2, qty)

        'Dim lot As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
        'lot.Name = "lot"
        'lot.HeaderText = "Total Lot"
        'lot.Width = 200
        'dgv_forminputstock.Columns.Insert(3, lot)

        'Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        'delete.Name = "delete"
        'delete.HeaderText = "Delete"
        'delete.Width = 100
        'delete.Text = "Delete"
        'delete.UseColumnTextForButtonValue = True

        'Dim queryKunci As String = "select DISTINCT KUNCI from INPUT_STOCK where MTS_NO=" & txt_forminputstock_mts_no.Text
        'Dim dsKunci = New DataSet
        'Dim adapterKunci = New SqlDataAdapter(queryKunci, Database.koneksi)
        'adapterKunci.Fill(dsKunci)

        'If dgv_forminputstock.Rows.Count Then
        '    If dsKunci.Tables(0).Rows(0).Item("KUNCI") = 1 Then
        '        delete.Visible = False
        '    End If
        'End If

        'dgv_forminputstock.Columns.Insert(4, delete)

        'If dtInputStock.Rows.Count > 0 Then
        '    For i As Integer = 0 To dgv_forminputstock.Rows.Count - 1
        '        Dim queryCount As String = "select count(*) as count from input_stock where part_number=" & dtInputStock.Rows(i).Item("PART_NUMBER").ToString & " and mts_no=" & txt_forminputstock_mts_no.Text
        '        Dim dsCount = New DataSet
        '        Dim adapterCount = New SqlDataAdapter(queryCount, Database.koneksi)
        '        adapterCount.Fill(dsCount)
        '        dgv_forminputstock.Rows(i).Cells(3).Value = dsCount.Tables(0).Rows(0).Item("count").ToString
        '    Next
        'End If

        'If dtInputStock.Rows.Count > 0 Then
        '    For i As Integer = 0 To dgv_forminputstock.Rows.Count - 1
        '        Dim querySum As String = "select sum(qty) as sum from input_stock where part_number=" & dtInputStock.Rows(i).Item("PART_NUMBER").ToString & " and mts_no=" & txt_forminputstock_mts_no.Text
        '        Dim dsSum = New DataSet
        '        Dim adapterSum = New SqlDataAdapter(querySum, Database.koneksi)
        '        adapterSum.Fill(dsSum)
        '        dgv_forminputstock.Rows(i).Cells(2).Value = dsSum.Tables(0).Rows(0).Item("sum").ToString
        '    Next
        'End If

        For i As Integer = 0 To dgv_forminputstock.RowCount - 1
            If dgv_forminputstock.Rows(i).Index Mod 2 = 0 Then
                dgv_forminputstock.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_forminputstock.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MainForm.mts_issue_return_stock.ReadOnly = False
        MainForm.mts_issue_return_stock.Text = ""
        MainForm.mts_return_stock.ReadOnly = False
        MainForm.mts_return_stock.Text = ""
        MainForm.mts_issue_return_stock.Select()
        Me.Close()
    End Sub
End Class