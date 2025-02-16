﻿Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.Office.Interop

Public Class MasterProcessFlow
    Public Shared menu As String = "Master Process Flow"

    Dim oleCon As OleDbConnection
    Private Sub MasterProcessFlow_Load(sender As Object, e As EventArgs) Handles Me.Load
        If globVar.view > 0 Then
            'DGV_ProcessFlow()
            tampilDataComboBox()
            loadMasterProcessFlowBawah()
        End If
    End Sub

    Private Sub DGV_ProcessFlow()
        Try
            Dim varProcess As String = ""
            Call Database.koneksi_database()
            dgv_masterprocessflow.Rows.Clear()
            dgv_masterprocessflow.Columns.Clear()

            Dim queryCek As String = "select * from MASTER_PROCESS_NUMBER "
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

            Dim query As String = "SELECT * FROM (SELECT master_finish_goods_pn as FG_Number,master_process_number, master_process FROM dbo.MASTER_PROCESS_FLOW where master_finish_goods_pn='" & cb_masterprocessflow.Text & "') t PIVOT ( max(master_process) FOR master_process_number IN ( " + varProcess + " )) pivot_table"

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

                Dim queryProcess As String = "select process_name from master_process where family=(select family from master_finish_goods where fg_part_number='" & cb_masterprocessflow.Text & "' and department='" & globVar.department & "') order by process_name"
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

                For rowDataSet As Integer = 0 To datasetGas.Tables(0).Rows.Count - 1
                    For colDataSet As Integer = 1 To datasetGas.Tables(0).Columns.Count - 1
                        dgv_masterprocessflow.Rows(rowDataSet).Cells(colDataSet).Value = datasetGas.Tables(0).Rows(rowDataSet).Item(colDataSet).ToString
                    Next
                Next

                Dim btn As New DataGridViewButtonColumn
                btn.Name = "delete"
                btn.HeaderText = "Delete"
                btn.Text = "Delete"
                btn.Width = 100
                btn.UseColumnTextForButtonValue = True
                dgv_masterprocessflow.Columns.Insert(0, btn)

            End If
        Catch ex As Exception
            RJMessageBox.Show("Error Process Flow - 1 =>" & ex.Message)
        End Try
    End Sub

    Sub loadMasterProcessFlowBawah()
        Try
            Dim varProcess As String = ""
            Call Database.koneksi_database()
            dgvBawah.Rows.Clear()
            dgvBawah.Columns.Clear()

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

            Dim query As String = "SELECT * FROM (SELECT mpf.master_finish_goods_pn AS FG_Number, mpf.master_process_number, mpf.master_process FROM dbo.MASTER_PROCESS_FLOW mpf, master_finish_goods mfg where mfg.fg_part_number=mpf.master_finish_goods_pn and mfg.department='" & globVar.department & "') t PIVOT ( max(master_process) FOR master_process_number IN ( " + varProcess + " )) pivot_table"

            Dim adapterGas As SqlDataAdapter
            Dim datasetGas As New DataSet

            adapterGas = New SqlDataAdapter(query, Database.koneksi)
            adapterGas.Fill(datasetGas)

            If datasetGas.Tables(0).Rows.Count > 0 Then
                dgvBawah.ColumnCount = 1
                dgvBawah.Columns(0).Name = "Part Number"
                For r = 0 To datasetGas.Tables(0).Rows.Count - 1
                    Dim row As String() = New String() {datasetGas.Tables(0).Rows(r).Item(0).ToString}
                    dgvBawah.Rows.Add(row)
                Next

                Dim queryProcess As String = "select process_name from master_process order by process_name"
                Dim dsProcess = New DataSet
                Dim adapterProcess = New SqlDataAdapter(queryProcess, Database.koneksi)
                adapterProcess.Fill(dsProcess)

                For i As Integer = 0 To dsexist.Tables(0).Rows.Count - 1
                    Dim txcolumn As New DataGridViewTextBoxColumn
                    txcolumn.HeaderText = dsexist.Tables(0).Rows(i).Item(1).ToString
                    'For iProcess As Integer = 0 To dsProcess.Tables(0).Rows.Count - 1
                    '    cbcolumn.Items.Add(dsProcess.Tables(0).Rows(iProcess).Item("process_name").ToString)
                    'Next

                    dgvBawah.Columns.Add(txcolumn)
                Next

                For rowDataSet As Integer = 0 To datasetGas.Tables(0).Rows.Count - 1
                    For colDataSet As Integer = 1 To datasetGas.Tables(0).Columns.Count - 1
                        dgvBawah.Rows(rowDataSet).Cells(colDataSet).Value = datasetGas.Tables(0).Rows(rowDataSet).Item(colDataSet).ToString
                    Next
                Next

            End If
        Catch ex As Exception
            RJMessageBox.Show("Error Process Flow - 2 =>" & ex.Message)
        End Try
    End Sub

    Sub tampilDataComboBox()
        Call Database.koneksi_database()
        Dim dtMasterFinishGoods As DataTable = Database.GetData("select distinct fg_part_number from master_finish_goods where department='" & globVar.department & "' order by fg_part_number")

        cb_masterprocessflow.DataSource = dtMasterFinishGoods
        cb_masterprocessflow.DisplayMember = "fg_part_number"
        cb_masterprocessflow.ValueMember = "fg_part_number"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If globVar.add > 0 Then
            If cb_masterprocessflow.Text <> "" Then
                Try
                    Dim sql As String = "select * from master_process_number"
                    Dim ds As New DataSet
                    Dim adapter As SqlDataAdapter
                    adapter = New SqlDataAdapter(sql, Database.koneksi)
                    adapter.Fill(ds)
                    For r = 0 To ds.Tables(0).Rows.Count - 1
                        Dim queryCheckProcessFlow As String = "select * from master_process_flow where MASTER_FINISH_GOODS_PN='" & cb_masterprocessflow.Text & "' and MASTER_PROCESS_NUMBER='" & ds.Tables(0).Rows(r).Item("PROCESS_NAME").ToString() & "'"
                        Dim dtCheckProcessFlow As DataTable = Database.GetData(queryCheckProcessFlow)
                        If dtCheckProcessFlow.Rows.Count = 0 Then
                            Dim sql2 As String = "INSERT INTO MASTER_PROCESS_FLOW(MASTER_FINISH_GOODS_PN,MASTER_PROCESS_NUMBER) VALUES ('" & cb_masterprocessflow.Text & "','" & ds.Tables(0).Rows(r).Item("PROCESS_NAME").ToString() & "')"
                            Dim cmd2 = New SqlCommand(sql2, Database.koneksi)
                            cmd2.ExecuteNonQuery()
                        End If
                    Next

                    DGV_ProcessFlow()
                Catch ex As Exception
                    RJMessageBox.Show("Error Process Flow - 3 =>" & ex.Message)
                End Try
            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub dgv_masterprocessflow_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_masterprocessflow.CellClick
        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If e.ColumnIndex = -1 Then
            Exit Sub
        End If

        Call Database.koneksi_database()
        If dgv_masterprocessflow.Columns(e.ColumnIndex).Name = "delete" Then
            If globVar.delete > 0 Then

                Dim queryCek As String = "SELECT * FROM dbo.main_po where fg_pn='" & dgv_masterprocessflow.Rows(e.RowIndex).Cells("Part Number").Value & "' and status='Open'"
                Dim dsexist = New DataSet
                Dim adapterexist = New SqlDataAdapter(queryCek, Database.koneksi)
                adapterexist.Fill(dsexist)

                If dsexist.Tables(0).Rows.Count > 0 Then
                    RJMessageBox.Show("Cannot delete. Because this FG still have Open PO")
                    Exit Sub
                End If

                Dim result = RJMessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then
                    Try
                        Dim sql As String = "delete from master_process_flow where master_finish_goods_pn='" & dgv_masterprocessflow.Rows(e.RowIndex).Cells(1).Value & "'"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        cmd.ExecuteNonQuery()
                        DGV_ProcessFlow()
                    Catch ex As Exception
                        RJMessageBox.Show("Error Process Flow - 4 =>" & ex.Message)
                    End Try
                End If
            Else
                RJMessageBox.Show("Your Access cannot execute this action")
            End If
        End If
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles masterprocessflow_search.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            Dim Found As Boolean = False
            Dim StringToSearch As String = ""
            Dim ValueToSearchFor As String = Me.masterprocessflow_search.Text.Trim.ToLower
            Dim CurrentRowIndex As Integer = 0
            Try
                If dgvBawah.Rows.Count = 0 Then
                    CurrentRowIndex = 0
                Else
                    CurrentRowIndex = dgvBawah.CurrentRow.Index + 1
                End If
                If CurrentRowIndex > dgvBawah.Rows.Count Then
                    CurrentRowIndex = dgvBawah.Rows.Count - 1
                End If
                If dgvBawah.Rows.Count > 0 Then
                    For Each gRow As DataGridViewRow In dgvBawah.Rows
                        StringToSearch = gRow.Cells("Part Number").Value.ToString.Trim.ToLower
                        If InStr(1, StringToSearch, LCase(Trim(masterprocessflow_search.Text)), vbTextCompare) = 1 Then
                            Dim myCurrentCell As DataGridViewCell = gRow.Cells("Part Number")
                            Dim myCurrentPosition As DataGridViewCell = gRow.Cells(0)
                            dgvBawah.CurrentCell = myCurrentCell
                            CurrentRowIndex = dgvBawah.CurrentRow.Index
                            Found = True
                            masterprocessflow_search.Clear()
                        End If
                        If Found Then Exit For
                    Next

                    If Found = False Then
                        RJMessageBox.Show("Data Doesn't exist")
                        masterprocessflow_search.Clear()
                    End If
                End If
            Catch ex As Exception
                RJMessageBox.Show("Error Process Flow - 5 =>" & ex.Message)
            End Try
        End If
    End Sub

    Private Sub dgv_masterprocessflow1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgv_masterprocessflow.CellFormatting
        For i As Integer = 0 To dgv_masterprocessflow.RowCount - 1
            If dgv_masterprocessflow.Rows(i).Index Mod 2 = 0 Then
                dgv_masterprocessflow.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_masterprocessflow.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub dgv_masterprocessflow_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgv_masterprocessflow.EditingControlShowing
        If dgv_masterprocessflow.CurrentCell.ColumnIndex > 1 Then
            Dim combo As DataGridViewComboBoxEditingControl = CType(e.Control, DataGridViewComboBoxEditingControl)

            If (combo IsNot Nothing) Then
                RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf Combo_SelectionChangeCommitted)

                AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf Combo_SelectionChangeCommitted)
            End If
        End If
    End Sub

    Private Sub Combo_SelectionChangeCommitted(ByVal sender As Object, ByVal e As EventArgs)
        If globVar.update > 0 Then
            Dim combo As DataGridViewComboBoxEditingControl = CType(sender, DataGridViewComboBoxEditingControl)
            Dim Sql As String = "update master_process_flow set master_process='" & combo.SelectedItem & "' where master_finish_goods_pn='" & dgv_masterprocessflow.Rows(dgv_masterprocessflow.CurrentCell.RowIndex).Cells(1).Value & "' and master_process_number='" & dgv_masterprocessflow.Columns(dgv_masterprocessflow.CurrentCell.ColumnIndex).HeaderCell.Value & "'"
            Dim cmd = New SqlCommand(Sql, Database.koneksi)
            cmd.ExecuteNonQuery()
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If globVar.add > 0 Then
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                Dim xlApp As New Microsoft.Office.Interop.Excel.Application
                Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook = xlApp.Workbooks.Open(OpenFileDialog1.FileName)
                Dim SheetName As String = xlWorkBook.Worksheets(1).Name.ToString
                Dim excelpath As String = OpenFileDialog1.FileName
                Dim koneksiExcel As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & excelpath & ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'"
                oleCon = New OleDbConnection(koneksiExcel)
                oleCon.Open()

                Dim queryExcel As String = "select * from [" & SheetName & "$]"
                Dim cmd As OleDbCommand = New OleDbCommand(queryExcel, oleCon)
                Dim rd As OleDbDataReader

                Call Database.koneksi_database()
                Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(Database.koneksi)
                    bulkCopy.DestinationTableName = "dbo.MASTER_PROCESS_FLOW"
                    Try
                        rd = cmd.ExecuteReader

                        bulkCopy.ColumnMappings.Add(0, 1)
                        bulkCopy.ColumnMappings.Add(1, 2)
                        bulkCopy.ColumnMappings.Add(2, 3)
                        bulkCopy.ColumnMappings.Add(3, 4)

                        bulkCopy.WriteToServer(rd)
                        rd.Close()

                        DGV_ProcessFlow()
                        RJMessageBox.Show("Import Process Flow & Process Flow material usage Success")
                    Catch ex As Exception
                        RJMessageBox.Show("Error Process Flow - 6 =>" & ex.Message)
                    End Try
                End Using
            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub btn_ex_template_Click(sender As Object, e As EventArgs) Handles btn_ex_template.Click
        If globVar.view > 0 Then
            Dim excelApp As Excel.Application = New Excel.Application()

            'create new workbook
            Dim workbook As Excel.Workbook = excelApp.Workbooks.Add()

            'create new worksheet
            Dim worksheet As Excel.Worksheet = workbook.Worksheets.Add()

            'write data to worksheet
            worksheet.Range("A1").Value = "Finish Goods Part Number *"
            worksheet.Range("B1").Value = "Process Number (1, 2, 3 etc) *"
            worksheet.Range("C1").Value = "Process Name (Must same with Master Process) *"
            worksheet.Range("D1").Value = "Material Usage ( 1, 2, 3 etc ) *"

            'save the workbook
            FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

            If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
                workbook.SaveAs(directoryPath & "\Master Process Flow Template.xlsx")
            End If

            'cleanup
            excelApp.Quit()
            Marshal.ReleaseComObject(excelApp)
            RJMessageBox.Show("Export Template Success !")
        End If
    End Sub

    Private Sub btn_export_Finish_Goods_Click(sender As Object, e As EventArgs) Handles btn_export_Finish_Goods.Click
        If globVar.view > 0 Then
            'ExportToExcel()
            ExportToExcelV2()
        End If
    End Sub
    Private Sub ExportToExcel()
        Dim xlApp As New Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim i As Integer
        Dim j As Integer
        xlWorkBook = xlApp.Workbooks.Add(misValue)
        xlWorkSheet = xlWorkBook.Sheets("sheet1")

        For i = 1 To dgvBawah.RowCount - 2
            For j = 1 To dgvBawah.ColumnCount - 2
                For k As Integer = 1 To dgvBawah.Columns.Count
                    xlWorkSheet.Cells(1, k) = dgvBawah.Columns(k - 1).HeaderText
                    xlWorkSheet.Cells(i + 2, j + 1) = dgvBawah(j, i).Value.ToString()
                Next
            Next
        Next
        FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
            xlWorkSheet.SaveAs(directoryPath & "\Master Process Flow.xlsx")
        End If

        xlWorkBook.Close()
        xlApp.Quit()

        releaseObject(xlWorkSheet)
        releaseObject(xlWorkBook)
        releaseObject(xlApp)

        RJMessageBox.Show("Export to Excel Success !")
    End Sub

    Private Sub ExportToExcelV2()
        If globVar.view > 0 Then
            Dim xlApp As New Excel.Application
            Dim xlWorkBook As Excel.Workbook
            Dim xlWorkSheet As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value

            xlWorkBook = xlApp.Workbooks.Add(misValue)
            xlWorkSheet = xlWorkBook.Sheets("sheet1")

            ' Mengatur header
            For k As Integer = 1 To dgvBawah.Columns.Count
                xlWorkSheet.Cells(1, k) = dgvBawah.Columns(k - 1).HeaderText
            Next

            ' Menyalin data ke array dua dimensi
            Dim dataArray(dgvBawah.RowCount - 1, dgvBawah.ColumnCount - 1) As Object
            For i As Integer = 0 To dgvBawah.RowCount - 1
                For j As Integer = 0 To dgvBawah.ColumnCount - 1
                    dataArray(i, j) = dgvBawah(j, i).Value
                Next
            Next

            ' Menyalin array ke lembar kerja Excel
            xlWorkSheet.Range("A2").Resize(dgvBawah.RowCount, dgvBawah.ColumnCount).Value = dataArray

            ' Mengatur direktori awal untuk dialog
            FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

            ' Memilih folder penyimpanan
            If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
                Dim currentDate As Date = DateTime.Now
                Dim namafile As String = "Master Process Flow Export - " & currentDate.ToString("yyyy-MM-dd HH-mm-ss") & ".xlsx"
                Dim filePath As String = System.IO.Path.Combine(directoryPath, namafile)

                xlWorkSheet.SaveAs(filePath)
                RJMessageBox.Show("Export to Excel Success!" & Environment.NewLine & "Name is " & namafile)
            End If

            ' Membersihkan objek Excel
            xlWorkBook.Close(False)
            xlApp.Quit()

            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
        End If
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub dgv_masterprocessflow_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgv_masterprocessflow.DataBindingComplete
        For i As Integer = 0 To dgv_masterprocessflow.RowCount - 1
            If dgv_masterprocessflow.Rows(i).Index Mod 2 = 0 Then
                dgv_masterprocessflow.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_masterprocessflow.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With dgv_masterprocessflow
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

    Private Sub dgvBawah_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvBawah.DataBindingComplete
        For i As Integer = 0 To dgvBawah.RowCount - 1
            If dgvBawah.Rows(i).Index Mod 2 = 0 Then
                dgvBawah.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgvBawah.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With dgvBawah
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

    Private Sub dgvBawah_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvBawah.CellFormatting
        For i As Integer = 0 To dgvBawah.RowCount - 1
            If dgvBawah.Rows(i).Index Mod 2 = 0 Then
                dgvBawah.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgvBawah.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub
End Class