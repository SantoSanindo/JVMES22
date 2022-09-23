Imports System.Data.SqlClient

Public Class MasterProcessFlow
    Private Sub MasterProcessFlow_Load(sender As Object, e As EventArgs) Handles Me.Load
        DGV_ProcessFlow()
        tampilDataComboBox()
    End Sub

    Private Sub DGV_ProcessFlow()
        Try
            Dim varProcess As String = ""
            'dgv_masterprocessflow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Call Database.koneksi_database()
            dgv_masterprocessflow.Rows.Clear()
            dgv_masterprocessflow.Columns.Clear()

            Dim queryCek As String = "select * from MASTER_PROCESS_NUMBER"
            Dim dsexist = New DataSet
            Dim adapterexist = New SqlDataAdapter(queryCek, Database.koneksi)
            adapterexist.Fill(dsexist)
            For i As Integer = 0 To dsexist.Tables(0).Rows.Count - 1
                If i = 0 Then
                    varProcess = "[" + dsexist.Tables(0).Rows(i).Item("PROCESS_NAME").ToString + "]"
                Else
                    varProcess = varProcess + ",[" + dsexist.Tables(0).Rows(i).Item("PROCESS_NAME").ToString + "]"
                End If
            Next

            Dim query As String = "SELECT * FROM (SELECT master_finish_goods as FG_Number,master_process_number, master_process FROM dbo.MASTER_PROCESS_FLOW) t PIVOT ( max(master_process) FOR master_process_number IN ( " + varProcess + " )) pivot_table"

            Dim adapterGas As SqlDataAdapter
            Dim datasetGas As New DataSet

            adapterGas = New SqlDataAdapter(query, Database.koneksi)
            adapterGas.Fill(datasetGas)

            If datasetGas.Tables(0).Rows.Count > 0 Then
                dgv_masterprocessflow.ColumnCount = 1
                dgv_masterprocessflow.Columns(0).Name = "Part Number"
                For r = 0 To datasetGas.Tables(0).Rows.Count - 1
                    Dim row As String() = New String() {datasetGas.Tables(0).Rows(r).Item(0).ToString}
                    dgv_masterprocessflow.Rows.Add(row)
                Next

                Dim queryProcess As String = "select process_name from master_process order by process_name"
                Dim dsProcess = New DataSet
                Dim adapterProcess = New SqlDataAdapter(queryProcess, Database.koneksi)
                adapterProcess.Fill(dsProcess)

                For i As Integer = 0 To dsexist.Tables(0).Rows.Count - 1
                    Dim cbcolumn As New DataGridViewComboBoxColumn
                    cbcolumn.HeaderText = dsexist.Tables(0).Rows(i).Item(1).ToString
                    For iProcess As Integer = 0 To dsProcess.Tables(0).Rows.Count - 1
                        cbcolumn.Items.Add(dsProcess.Tables(0).Rows(iProcess).Item("process_name").ToString)
                    Next

                    dgv_masterprocessflow.Columns.Add(cbcolumn)
                Next


                'For Each rows As DataGridViewRow In dgv_masterprocessflow.Rows
                '    Dim comboBoxCell As DataGridViewComboBoxCell = CType(rows.Cells(2), DataGridViewComboBoxCell)


                For rowDataSet As Integer = 0 To datasetGas.Tables(0).Rows.Count - 1
                    For colDataSet As Integer = 1 To datasetGas.Tables(0).Columns.Count - 1
                        dgv_masterprocessflow.Rows(rowDataSet).Cells(colDataSet).Value = datasetGas.Tables(0).Rows(rowDataSet).Item(colDataSet).ToString
                    Next
                Next
                'Next

                'For r = 0 To datasetGas.Tables(0).Rows.Count - 1
                '    For c = 1 To datasetGas.Tables(0).Columns.Count - 1
                '        dgv_masterprocessflow.Rows(0).Cells(1).Value = datasetGas.Tables(0).Rows(0).Item(1).ToString
                '    Next
                'Next

                Dim btn As New DataGridViewButtonColumn
                btn.HeaderText = "Delete"
                btn.Text = "Delete"
                btn.Width = 100
                btn.UseColumnTextForButtonValue = True
                dgv_masterprocessflow.Columns.Insert(0, btn)

            End If

            For i As Integer = 0 To dgv_masterprocessflow.RowCount - 1
                If dgv_masterprocessflow.Rows(i).Index Mod 2 = 0 Then
                    dgv_masterprocessflow.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    dgv_masterprocessflow.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next i
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub tampilDataComboBox()
        Call Database.koneksi_database()
        Dim dtMasterMaterial As DataTable = Database.GetData("select distinct part_number from master_finish_goods where material_part_number is not null")

        cb_masterprocessflow.DataSource = dtMasterMaterial
        cb_masterprocessflow.DisplayMember = "part_number"
        cb_masterprocessflow.ValueMember = "part_number"
        cb_masterprocessflow.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cb_masterprocessflow.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If cb_masterprocessflow.Text <> "" Then
            Try
                Dim sql As String = "select * from master_process_number"
                Dim ds As New DataSet
                Dim adapter As SqlDataAdapter
                adapter = New SqlDataAdapter(sql, Database.koneksi)
                adapter.Fill(ds)
                For r = 0 To ds.Tables(0).Rows.Count - 1
                    Dim sql2 As String = "INSERT INTO MASTER_PROCESS_FLOW(MASTER_FINISH_GOODS,MASTER_PROCESS_NUMBER) VALUES (" & cb_masterprocessflow.Text & ",'" & ds.Tables(0).Rows(r).Item("PROCESS_NAME").ToString() & "')"
                    Dim cmd2 = New SqlCommand(sql2, Database.koneksi)
                    cmd2.ExecuteNonQuery()
                Next

                'Dim sql1 As String = "insert into master_process_flow (master_finish_goods, master_proc, need) select DISTINCT MASTER_FINISH_GOODS,'" & txt_masterprocess_nama.Text & "', 0  from master_process_flow where [master_proc] != '" & txt_masterprocess_nama.Text & "'"
                'Dim cmd1 = New SqlCommand(sql1, Database.koneksi)
                'cmd1.ExecuteNonQuery()

                cb_masterprocessflow.Text = ""

                DGV_ProcessFlow()
            Catch ex As Exception
                MessageBox.Show("Error Insert" & ex.Message)
            End Try
        End If
    End Sub

    Private Sub dgv_masterprocessflow_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_masterprocessflow.CellClick
        Call Database.koneksi_database()
        If e.ColumnIndex = 0 Then
            Dim result = MessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from master_process_flow where master_finish_goods=" & dgv_masterprocessflow.Rows(e.RowIndex).Cells(1).Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()
                    DGV_ProcessFlow()
                    MessageBox.Show("Success delete.")
                Catch ex As Exception
                    MessageBox.Show("Failed delete" & ex.Message)
                End Try
            End If
        End If

        'If e.ColumnIndex > 1 Then
        '    If dgv_masterprocessflow.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True Then
        '        Dim Sql As String = "update master_process_flow set need=0 where master_finish_goods=" & dgv_masterprocessflow.Rows(e.RowIndex).Cells(1).Value & " and master_proc='" & dgv_masterprocessflow.Columns(e.ColumnIndex).HeaderCell.Value & "'"
        '        Dim cmd = New SqlCommand(Sql, Database.koneksi)
        '        cmd.ExecuteNonQuery()
        '        dgv_masterprocessflow.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False
        '    Else
        '        Dim Sql As String = "update master_process_flow set need=1 where master_finish_goods=" & dgv_masterprocessflow.Rows(e.RowIndex).Cells(1).Value & " and master_proc='" & dgv_masterprocessflow.Columns(e.ColumnIndex).HeaderCell.Value & "'"
        '        Dim cmd = New SqlCommand(Sql, Database.koneksi)
        '        cmd.ExecuteNonQuery()
        '        dgv_masterprocessflow.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True
        '    End If
        'End If
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            Dim str As String = TextBox1.Text
            Try
                For i As Integer = 0 To dgv_masterprocessflow.Rows.Count - 1
                    If dgv_masterprocessflow.Rows(i).Cells(1).Value = str Then
                        dgv_masterprocessflow.Rows(i).Selected = True
                        Exit Sub
                    End If
                Next i
            Catch abc As Exception
            End Try
            MsgBox("Data not found!")
        End If
    End Sub
End Class