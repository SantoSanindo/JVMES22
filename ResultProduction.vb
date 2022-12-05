Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class ResultProduction
    Private Sub ComboBox2_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedValueChanged
        Dim query As String = "select mp.po,mp.sub_po,mp.fg_pn,ssp.sub_sub_po,mfg.description,ssp.sub_sub_po_qty,mfg.spq
            from sub_sub_po ssp,main_po mp,master_finish_goods mfg 
            where ssp.status='Open' and mp.id=ssp.main_po and mfg.fg_part_number=mp.fg_pn and ssp.line='" & ComboBox2.Text & "'"
        Dim dt As DataTable = Database.GetData(query)

        If dt.Rows.Count > 0 Then
            TextBox2.Text = dt.Rows(0).Item("SUB_SUB_PO").ToString
            TextBox3.Text = dt.Rows(0).Item("FG_PN").ToString

            If RadioButton1.Checked Then
                RadioButton2.Checked = False
                TextBox4.Enabled = True
                TextBox5.Enabled = True
                Button1.Enabled = True

                TextBox6.Enabled = False
                ComboBox1.Enabled = False
                Button2.Enabled = False
            End If

            If RadioButton2.Checked Then
                RadioButton1.Checked = False
                TextBox4.Enabled = False
                TextBox5.Enabled = False
                Button1.Enabled = False

                TextBox6.Enabled = True
                ComboBox1.Enabled = True
                Button2.Enabled = True
            End If

            If TabControl1.SelectedTab.Name = "TabPage1" Then
                DGV_Reject()
            End If
        Else
            DGV_Reject()
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If ComboBox2.Text <> "" Then
            RadioButton2.Checked = False
            TextBox4.Enabled = True
            TextBox5.Enabled = True
            Button1.Enabled = True

            TextBox6.Enabled = False
            ComboBox1.Enabled = False
            Button2.Enabled = False
        Else
            MessageBox.Show("Please Select Line First")
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If ComboBox2.Text <> "" Then
            RadioButton1.Checked = False
            TextBox4.Enabled = False
            TextBox5.Enabled = False
            Button1.Enabled = False

            TextBox6.Enabled = True
            ComboBox1.Enabled = True
            Button2.Enabled = True
        Else
            MessageBox.Show("Please Select Line First")
        End If

    End Sub

    Private Sub TextBox4_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox4.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) And Len(Me.TextBox4.Text) >= 64 Then
            TextBox1.Text = TextBox4.Text

            Dim splitQRCode() As String = TextBox4.Text.Split(New String() {"1P", "12D", "4L", "MLX"}, StringSplitOptions.None)
            Dim splitQRCode1P() As String = splitQRCode(1).Split(New String() {"Q", "S", "13Q", "B"}, StringSplitOptions.None)

            TextBox4.Text = splitQRCode1P(0)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox4.Text <> "" And TextBox5.Text <> "" Then
            Dim adapter As SqlDataAdapter
            Dim ds As New DataSet
            Dim splitQRCode() As String = TextBox1.Text.Split(New String() {"1P", "12D", "4L", "MLX"}, StringSplitOptions.None)
            Dim splitQRCode1P() As String = splitQRCode(1).Split(New String() {"Q", "S", "13Q", "B"}, StringSplitOptions.None)

            Dim sqlCheckProcess As String = "select * from process_prod where line='" & ComboBox2.Text & "' and pn_material = " & splitQRCode1P(0) & " and lot_no=" & splitQRCode1P(3) & " and sub_sub_po=" & TextBox2.Text
            Dim dtCheckProcess As DataTable = Database.GetData(sqlCheckProcess)

            If dtCheckProcess.Rows.Count > 0 Then
                Dim sqlCheckReject As String = "select * from out_prod_reject where line='" & ComboBox2.Text & "' and PART_NUMBER = " & splitQRCode1P(0) & " and lot_no=" & splitQRCode1P(3) & " and fg_pn= " & TextBox3.Text & " and sub_sub_po=" & TextBox2.Text
                Dim dtCheckReject As DataTable = Database.GetData(sqlCheckReject)
                If dtCheckReject.Rows.Count > 0 Then
                    MessageBox.Show("Double Scan Detect")
                    TextBox4.Text = ""
                Else
                    Dim sqlCheckCount As String = "SELECT COUNT(DISTINCT CODE_OUT_PROD_REJECT)+1 as Total FROM OUT_PROD_REJECT WHERE PART_NUMBER=" & splitQRCode1P(0) & " AND LOT_NO=" & splitQRCode1P(3) & " AND SUB_SUB_PO=" & TextBox2.Text & " AND FG_PN=" & TextBox3.Text & " AND LINE='" & ComboBox2.Text & "'"
                    Dim dtCheckCount As DataTable = Database.GetData(sqlCheckCount)

                    Dim sqlProdProcess As String = "INSERT INTO out_prod_reject (code_out_prod_reject, sub_sub_po, fg_pn, part_number, lot_no, traceability,inv_ctrl_date,batch_no,qty,po,line)
                                    VALUES ('RJ-" & dtCheckCount.Rows(0).Item("Total") & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & splitQRCode1P(0) & "','" & splitQRCode1P(3) & "','" & splitQRCode1P(2) & "','" & splitQRCode(2) & "','" & splitQRCode1P(4) & "'," & TextBox5.Text & ",(select po from main_po mp, sub_sub_po ssp where ssp.sub_sub_po=" & TextBox2.Text & " and ssp.main_po=mp.id),'" & ComboBox2.Text & "')"
                    Dim cmdProdProcess = New SqlCommand(sqlProdProcess, Database.koneksi)
                    If cmdProdProcess.ExecuteNonQuery() Then
                        TextBox4.Text = ""
                        TextBox5.Text = ""
                        MessageBox.Show("Refresh DGV Bawah")
                    End If
                End If
            Else
                MessageBox.Show("Sorry this material not for this line.")
                TextBox4.Text = ""
                TextBox4.Select()
            End If
        End If
    End Sub



    Private Sub TabControl1_Click(sender As Object, e As EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedTab.Name = "TabPage1" Then
            DGV_Reject()
        End If
    End Sub

    Sub DGV_Reject()
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Call Database.koneksi_database()
        Dim queryReject As String = "select code_out_prod_reject Code,PO,SUB_SUB_PO,FG_PN,part_number [Part Number],lot_no [Lot No],QTY,process_reject [Process Reject], LINE from out_prod_reject where sub_sub_po='" & TextBox2.Text & "' and fg_pn='" & TextBox3.Text & "' and line='" & ComboBox2.Text & "'"
        Dim dtReject As DataTable = Database.GetData(queryReject)
        DataGridView1.DataSource = dtReject
    End Sub

    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete
        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub
End Class