Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MdiTabControl

Public Class Production
    Private Sub ComboBox1_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedValueChanged
        Dim query As String = "select mp.po,mp.sub_po,mp.fg_pn,ssp.sub_sub_po,mfg.description,ssp.sub_sub_po_qty,mfg.spq
            from sub_sub_po ssp,main_po mp,master_finish_goods mfg 
            where ssp.status='Open' and mp.id=ssp.main_po and mfg.fg_part_number=mp.fg_pn and ssp.line='" & ComboBox1.Text & "'"
        Dim dt As DataTable = Database.GetData(query)
        If dt.Rows.Count > 0 Then
            TextBox2.Text = dt.Rows(0).Item("FG_PN").ToString
            TextBox4.Text = dt.Rows(0).Item("DESCRIPTION").ToString
            TextBox5.Text = dt.Rows(0).Item("PO").ToString
            TextBox6.Text = dt.Rows(0).Item("SUB_SUB_PO_QTY").ToString
            TextBox7.Text = dt.Rows(0).Item("SPQ").ToString
            TextBox8.Text = dt.Rows(0).Item("SUB_SUB_PO").ToString

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

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
            If Len(Me.TextBox1.Text) >= 64 Then
                Dim adapter As SqlDataAdapter
                Dim ds As New DataSet
                Dim splitQRCode() As String = TextBox1.Text.Split(New String() {"1P", "12D", "4L", "MLX"}, StringSplitOptions.None)
                Dim splitQRCode1P() As String = splitQRCode(1).Split(New String() {"Q", "S", "13Q", "B"}, StringSplitOptions.None)

                Dim sqlCheckInStock As String = "select * from sub_sub_po sp, stock_prod_material in_material where in_material.SUB_SUB_PO = sp.sub_sub_po and sp.status='Open' and sp.line='" & ComboBox1.Text & "' and part_number = " & splitQRCode1P(0) & " and lot_no=" & splitQRCode1P(3)
                Dim dtCheckInStock As DataTable = Database.GetData(sqlCheckInStock)

                If dtCheckInStock.Rows.Count > 0 Then
                    Dim sqlCheckProductionProcess As String = "select * from process_prod where line='" & ComboBox1.Text & "' and PN_MATERIAL = " & splitQRCode1P(0) & " and lot_no=" & splitQRCode1P(3)
                    Dim dtCheckProductionProcess As DataTable = Database.GetData(sqlCheckProductionProcess)
                    If dtCheckProductionProcess.Rows.Count > 0 Then
                        MessageBox.Show("Double Scan Detect")
                        TextBox1.Text = ""
                    Else
                        Dim sqlProdProcess As String = "INSERT INTO process_prod (id_level, level, pn_material, qty, lot_no, batch_no,traceability,inv_ctrl_date,fifo,line,sub_sub_po)
                                    VALUES ('" & splitQRCode1P(0) & "','Fresh','" & splitQRCode1P(0) & "','" & dtCheckInStock.Rows(0).Item("QTY") & "','" & splitQRCode1P(3) & "','" & dtCheckInStock.Rows(0).Item("batch_no") & "','" & dtCheckInStock.Rows(0).Item("traceability") & "','" & dtCheckInStock.Rows(0).Item("inv_ctrl_date") & "',(select COUNT(pn_material)+1 fifo from process_prod where pn_material=" & splitQRCode1P(0) & "),'" & ComboBox1.Text & "','" & TextBox8.Text & "')"
                        Dim cmdProdProcess = New SqlCommand(sqlProdProcess, Database.koneksi)
                        If cmdProdProcess.ExecuteNonQuery() Then
                            TextBox1.Text = ""
                            DGV_DOC()
                            For i = 0 To DataGridView1.Rows.Count - 1
                                If DataGridView1.Rows(i).Cells(1).Value = splitQRCode1P(0) Then
                                    DataGridView1.Rows(i).Cells(3).Selected = True
                                End If
                            Next
                        End If
                    End If
                Else
                    MessageBox.Show("Sorry this material not for this line.")
                    TextBox1.Text = ""
                    TextBox1.Select()
                End If
            Else
                MessageBox.Show("On Hold, WIP, Others, Sub Assy")
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'HOME.TabControl1.TabPages.Clear()
        HOME.TabControl1.TabPages.Add(ResultProduction)
        HOME.TabControl1.TabPages(ResultProduction).Select()
    End Sub

    Sub DGV_DOC()
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Call Database.koneksi_database()
        Dim queryDOC As String = "select desc_comp Description,component Component,Usage from prod_doc where line='" & ComboBox1.Text & "' and sub_sub_po='" & TextBox8.Text & "'"
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
            Dim queryCheck As String = "select lot_no from process_prod where pn_material=" & dtDOC.Rows(rowDataSet).Item("Component").ToString & " and line='" & ComboBox1.Text & "'"
            Dim dtCHECK As DataTable = Database.GetData(queryCheck)
            For i As Integer = 0 To dtCHECK.Rows.Count - 1
                DataGridView1.Rows(rowDataSet).Cells(3).Value += dtCHECK.Rows(i).Item("lot_no").ToString & ","
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
        Dim queryDOP As String = "select Process, operator_id Operator from prod_dop where line='" & ComboBox1.Text & "' and sub_sub_po=" & TextBox8.Text
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
End Class