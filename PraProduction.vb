Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles
Imports System.Drawing
Imports System.Windows.Forms

Public Class PraProduction

    Public Shared PO As String = ""
    Public Shared QTY As String = ""
    Public Shared cQTY As String = ""
    Public Shared FG As String = ""

    Private Sub PraProduction_Load(sender As Object, e As EventArgs) Handles Me.Load
        tampilDataComboboxFG()
        DGV_PraProduction()
    End Sub

    Sub tampilDataComboboxFG()
        cb_masterfinishgoods.Items.Clear()

        Call Database.koneksi_database()
        Dim dtPN As DataTable = Database.GetData("select distinct part_number from master_finish_goods where material_part_number is not null")

        cb_masterfinishgoods.DataSource = dtPN
        cb_masterfinishgoods.DisplayMember = "part_number"
        cb_masterfinishgoods.ValueMember = "part_number"
        cb_masterfinishgoods.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cb_masterfinishgoods.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If cb_masterfinishgoods.Text <> "" And txt_pra_po_no.Text <> "" And txt_pra_qty_po.Text <> "" Then
            Dim querycheck As String = "select * from pre_PRODUCTION where no_po=" & txt_pra_po_no.Text & " and fg_pn='" & cb_masterfinishgoods.Text & "'"
            Dim dtCheck As DataTable = Database.GetData(querycheck)
            If dtCheck.Rows.Count > 0 Then
                MessageBox .Show ("PO and FG exist")
            Else
                Try
                    Dim sql As String = "INSERT INTO pre_PRODUCTION(no_po,fg_pn,master_qty,current_qty) VALUES (" & txt_pra_po_no.Text & "," & cb_masterfinishgoods.Text & "," & txt_pra_qty_po.Text & "," & txt_pra_qty_po.Text & ")"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()

                    cb_masterfinishgoods.Text = ""
                    txt_pra_po_no.Text = ""
                    txt_pra_qty_po.Text = ""

                    DGV_PraProduction()
                Catch ex As Exception
                    MessageBox.Show("Error Insert" & ex.Message)
                End Try
            End If
        End If
    End Sub

    Public Sub DGV_PraProduction()
        dgv_pra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_pra.DataSource = Nothing
        dgv_pra.Rows.Clear()
        dgv_pra.Columns.Clear()
        Call Database.koneksi_database()
        Dim query As String = "select NO_PO,FG_PN,MASTER_QTY Qty, CURRENT_QTY Curr_Qty from pre_PRODUCTION"
        Dim dtpreProd As DataTable = Database.GetData(query)

        Dim lihat As DataGridViewButtonColumn = New DataGridViewButtonColumn
        lihat.Name = "lihat"
        lihat.HeaderText = "View"
        lihat.Width = 100
        lihat.Text = "View"
        lihat.UseColumnTextForButtonValue = True
        dgv_pra.Columns.Insert(0, lihat)

        dgv_pra.DataSource = dtpreProd

        Dim lot As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
        lot.Name = "lot"
        lot.HeaderText = "Total PO"
        lot.Width = 200
        dgv_pra.Columns.Insert(5, lot)

        Dim cbAktif As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn
        cbAktif.Name = "cbAktif"
        cbAktif.HeaderText = "Status"
        cbAktif.Width = 100
        cbAktif.MaxDropDownItems = 2
        cbAktif.Items.Add("Active")
        cbAktif.Items.Add("Non-Active")
        dgv_pra.Columns.Insert(6, cbAktif)

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        dgv_pra.Columns.Insert(7, delete)

        'Dim queryKunci As String = "select DISTINCT KUNCI from INPUT_STOCK where MTS_NO=" & txt_forminputstock_mts_no.Text
        'Dim dsKunci = New DataSet
        'Dim adapterKunci = New SqlDataAdapter(queryKunci, Database.koneksi)
        'adapterKunci.Fill(dsKunci)

        'If dsKunci.Tables(0).Rows(0).Item("KUNCI") = 1 Then
        '    delete.Visible = False
        'End If

        'Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
        'check.Name = "check"
        'check.HeaderText = "Check"
        'check.Width = 100
        'check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        'dgv_forminputstock.Columns.Insert(0, check)

        If dgv_pra.Rows.Count > 0 Then
            For i As Integer = 0 To dgv_pra.Rows.Count - 1
                Dim queryCount As String = "select count(*) as count from SUB_PO where PO_NO=" & dgv_pra.Rows(i).Cells("NO_PO").Value
                Dim dsCount = New DataSet
                Dim adapterCount = New SqlDataAdapter(queryCount, Database.koneksi)
                adapterCount.Fill(dsCount)
                dgv_pra.Rows(i).Cells(5).Value = dsCount.Tables(0).Rows(0).Item("count").ToString
            Next
        End If

        If dgv_pra.Rows.Count > 0 Then
            For i As Integer = 0 To dgv_pra.Rows.Count - 1
                Dim queryStatus As String = "select status from pre_production where no_po=" & dgv_pra.Rows(i).Cells(1).Value & " and fg_pn=" & dgv_pra.Rows(i).Cells(2).Value
                Dim dsStatus = New DataSet
                Dim adapterStatus = New SqlDataAdapter(queryStatus, Database.koneksi)
                adapterStatus.Fill(dsStatus)
                If dsStatus.Tables(0).Rows(0).Item("status").ToString = 1 Then
                    dgv_pra.Rows(i).Cells(6).Value = "Active"
                Else
                    dgv_pra.Rows(i).Cells(6).Value = "Non-Active"
                End If
            Next
        End If



        'If dgv_pra.Rows.Count > 0 Then
        '    For i As Integer = 0 To dgv_pra.Rows.Count - 1
        '        Dim queryStatus As String = "select status,master_qty,current_qty from pre_production where no_po=" & dgv_pra.Rows(i).Cells(1).Value & " and fg_pn=" & dgv_pra.Rows(i).Cells(2).Value
        '        Dim dsStatus = New DataSet
        '        Dim adapterStatus = New SqlDataAdapter(queryStatus, Database.koneksi)
        '        adapterStatus.Fill(dsStatus)
        '        Dim buttonHapusDisable As DataGridViewDisableButtonCell = CType(dgv_pra.Rows(i).Cells("delete"), DataGridViewDisableButtonCell)

        '        If dsStatus.Tables(0).Rows(0).Item("status").ToString = 1 Then
        '            If dsStatus.Tables(0).Rows(0).Item("current_qty").ToString <> dsStatus.Tables(0).Rows(0).Item("master_qty").ToString Then
        '                buttonHapusDisable.Enabled = Not CType(buttonHapusDisable.Value, [Boolean])
        '            End If
        '        Else
        '            buttonHapusDisable.Enabled = Not CType(buttonHapusDisable.Value, [Boolean])
        '        End If

        '        dgv_pra.Invalidate()
        '    Next
        'End If

        For i As Integer = 0 To dgv_pra.RowCount - 1
            If dgv_pra.Rows(i).Index Mod 2 = 0 Then
                dgv_pra.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_pra.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub dgv_pra_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_pra.CellClick
        If e.ColumnIndex = 0 Then
            Dim subPOForm = New SubPO()

            subPOForm.po_pre_production = dgv_pra.Rows(e.RowIndex).Cells(1).Value
            subPOForm.fg_pn_pre_production = dgv_pra.Rows(e.RowIndex).Cells(2).Value
            subPOForm.curr_qty_pre_production = dgv_pra.Rows(e.RowIndex).Cells(4).Value

            subPOForm.Show()
            Me.Close()
        End If

        If e.ColumnIndex = 7 Then
            If dgv_pra.Rows(e.RowIndex).Cells(5).Value = 0 Then
                Dim result = MessageBox.Show("Are you sure delete this data??", "Warning", MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then
                    Try
                        Dim sql As String = "delete from pre_production where no_po=" & dgv_pra.Rows(e.RowIndex).Cells(1).Value & " and fg_pn=" & dgv_pra.Rows(e.RowIndex).Cells(2).Value
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        cmd.ExecuteNonQuery()
                        DGV_PraProduction()
                        MessageBox.Show("Delete success.")
                    Catch ex As Exception
                        MessageBox.Show("Delete failed" & ex.Message)
                    End Try
                End If
            Else
                MessageBox.Show("Cannot delete this data because SUB PO still exist.")
            End If
        End If
    End Sub
End Class