﻿Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.Office.Interop

Public Class MasterFinishGoods
    Public Shared menu As String = "Master Finish Goods"

    Dim oleCon As OleDbConnection
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If globVar.add > 0 Then

            Call Database.koneksi_database()
            If txt_dept.Text <> "" And txt_pn.Text <> "" And txt_desc.Text <> "" And txt_level.Text <> "" And txt_spq.Text <> "" And cb_family.Text <> "" Then
                If IsNumeric(txt_spq.Text) Then
                    Try
                        Dim sql As String = "
                            IF NOT EXISTS (SELECT 1 FROM MASTER_FINISH_GOODS WHERE FG_PART_NUMBER = '" & txt_pn.Text & "')
                                BEGIN
                                    INSERT INTO MASTER_FINISH_GOODS (FG_PART_NUMBER,DEPARTMENT,LEVEL,DESCRIPTION,SPQ,LASER_CODE,family, BY_WHO) VALUES ('" & txt_pn.Text & "','" & txt_dept.Text & "','" & txt_level.Text & "','" & txt_desc.Text & "','" & txt_spq.Text & "','" & txt_laser.Text & "','" & cb_family.Text & "','" & globVar.username & "')
                                END
                            ELSE
                                BEGIN
                                    RAISERROR('Data already exists', 16, 1)
                                END"

                        Dim cmd = New SqlCommand(sql, Database.koneksi)

                        If cmd.ExecuteNonQuery() Then
                            txt_dept.Text = ""
                            txt_pn.Text = ""
                            txt_desc.Text = ""
                            txt_level.Text = ""
                            txt_spq.Text = ""
                            txt_laser.Text = ""
                            txt_dept.Select()

                            dgv_finish_goods.DataSource = Nothing
                            DGV_MasterFinishGoods()
                        End If

                    Catch ex As Exception
                        RJMessageBox.Show("Error Master Finish Goods - 1 => " & ex.Message)
                    End Try
                Else
                    RJMessageBox.Show("SPQ must be number.")
                    txt_dept.Text = ""
                    txt_pn.Text = ""
                    txt_desc.Text = ""
                    txt_level.Text = ""
                    txt_spq.Text = ""
                    txt_laser.Text = ""
                    txt_dept.Select()
                End If
            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Sub tampilDataComboBoxDepartement()
        Call Database.koneksi_database()
        Dim sql As String
        If globVar.username = "admin" Then
            sql = "select * from department order by department"
        Else
            sql = "select * from department where department='" & globVar.department & "' order by department"
        End If

        Dim dtMasterDepart As DataTable = Database.GetData(sql)

        txt_dept.DataSource = dtMasterDepart
        txt_dept.DisplayMember = "department"
        txt_dept.ValueMember = "department"
    End Sub

    Sub tampilDataComboBoxFamily()
        Call Database.koneksi_database()
        Dim dtMasterFamily As DataTable = Database.GetData("select family from family where department='" & globVar.department & "' order by family")

        cb_family.DataSource = dtMasterFamily
        cb_family.DisplayMember = "family"
        cb_family.ValueMember = "family"
    End Sub

    Private Sub MasterFinishGoods_Load(sender As Object, e As EventArgs) Handles Me.Load
        If globVar.view > 0 Then

            DGV_MasterFinishGoods()
            txt_dept.Select()
            txt_dept.Text = ""
            tampilDataComboBoxDepartement()
            tampilDataComboBoxFamily()
            txt_dept.SelectedIndex = -1
            txt_level.SelectedIndex = -1
            cb_family.SelectedIndex = -1
        End If
    End Sub

    Sub DGV_MasterFinishGoods()
        dgv_finish_goods.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_finish_goods.Rows.Clear()
        dgv_finish_goods.Columns.Clear()
        Call Database.koneksi_database()
        Dim dtMasterMaterial As DataTable = Database.GetData("select DEPARTMENT [Department],FG_PART_NUMBER [FG Part Number],DESCRIPTION [Desc],LEVEL [Level], SPQ [Spq], LASER_CODE [Laser Code],FAMILY [Family], datetime_insert [Date Time], by_who [Created By] from MASTER_FINISH_GOODS where department='" & globVar.department & "' order by FG_PART_NUMBER")

        dgv_finish_goods.DataSource = dtMasterMaterial

        Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
        check.Name = "check"
        check.HeaderText = "Check For Delete"
        check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgv_finish_goods.Columns.Insert(0, check)

        dgv_finish_goods.Columns(0).Width = 200
        dgv_finish_goods.Columns(1).Width = 150
        dgv_finish_goods.Columns(2).Width = 300
        dgv_finish_goods.Columns(3).Width = 300
        dgv_finish_goods.Columns(4).Width = 150
        dgv_finish_goods.Columns(5).Width = 100
        dgv_finish_goods.Columns(6).Width = 200
        dgv_finish_goods.Columns(7).Width = 100
        dgv_finish_goods.Columns(8).Width = 250
        dgv_finish_goods.Columns(9).Width = 150

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        dgv_finish_goods.Columns.Insert(10, delete)
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

                Dim queryExcel As String = "SELECT * FROM [" & SheetName & "$]"
                Dim cmd As OleDbCommand = New OleDbCommand(queryExcel, oleCon)
                Dim rd As OleDbDataReader

                Call Database.koneksi_database()

                Dim duplicateRows As New List(Of String)
                Dim dataTable As New DataTable()

                dataTable.Columns.Add("FG_PART_NUMBER", GetType(String))
                dataTable.Columns.Add("DEPARTMENT", GetType(String))
                dataTable.Columns.Add("LEVEL", GetType(String))
                dataTable.Columns.Add("DESCRIPTION", GetType(String))
                dataTable.Columns.Add("SPQ", GetType(Int32))
                dataTable.Columns.Add("FAMILY", GetType(String))
                dataTable.Columns.Add("LASER_CODE", GetType(String))

                Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(Database.koneksi)
                    bulkCopy.DestinationTableName = "dbo.MASTER_FINISH_GOODS"
                    Try
                        rd = cmd.ExecuteReader()

                        While rd.Read()
                            Dim fgPartNumber As String = rd("Finish Goods Part Number *").ToString().Trim()
                            Dim _dept As String = rd("Department (zQSFP, Ten60 etc) *").ToString().Trim()

                            If _dept = globVar.department Then

                                Dim cekQuery As String = "SELECT COUNT(*) FROM dbo.MASTER_FINISH_GOODS WHERE FG_PART_NUMBER = @FG_PART_NUMBER"
                                Using cekCmd As New SqlCommand(cekQuery, Database.koneksi)
                                    cekCmd.Parameters.AddWithValue("@FG_PART_NUMBER", fgPartNumber)

                                    Dim exists As Integer = Convert.ToInt32(cekCmd.ExecuteScalar())
                                    If exists > 0 Then
                                        duplicateRows.Add(fgPartNumber)
                                    Else
                                        Dim row As DataRow = dataTable.NewRow()
                                        row("FG_PART_NUMBER") = fgPartNumber
                                        row("DEPARTMENT") = _dept
                                        row("LEVEL") = rd("Level (Sub Assy, FG etc) *").ToString().Trim()
                                        row("DESCRIPTION") = rd("Description *").ToString().Trim()
                                        row("SPQ") = Convert.ToInt32(rd("Standard Pack *"))
                                        row("FAMILY") = rd("Family (zSFP, zQSFP etc) *").ToString().Trim()
                                        row("LASER_CODE") = If(rd.IsDBNull(rd.GetOrdinal("Laser Code")), DBNull.Value, rd("Laser Code").ToString().Trim())
                                        dataTable.Rows.Add(row)
                                    End If
                                End Using

                            End If

                        End While

                        If dataTable.Rows.Count > 0 Then
                            bulkCopy.ColumnMappings.Add("FG_PART_NUMBER", "FG_PART_NUMBER")
                            bulkCopy.ColumnMappings.Add("DEPARTMENT", "DEPARTMENT")
                            bulkCopy.ColumnMappings.Add("LEVEL", "LEVEL")
                            bulkCopy.ColumnMappings.Add("DESCRIPTION", "DESCRIPTION")
                            bulkCopy.ColumnMappings.Add("SPQ", "SPQ")
                            bulkCopy.ColumnMappings.Add("FAMILY", "FAMILY")
                            bulkCopy.ColumnMappings.Add("LASER_CODE", "LASER_CODE")
                            bulkCopy.WriteToServer(dataTable)
                        End If

                        If duplicateRows.Count > 0 Then
                            RJMessageBox.Show("Import Success. But, some data is duplicate: " & String.Join(", ", duplicateRows))
                        Else
                            RJMessageBox.Show("Import Finish Goods Success.")
                        End If

                        rd.Close()
                        dgv_finish_goods.DataSource = Nothing
                        DGV_MasterFinishGoods()

                    Catch ex As Exception
                        RJMessageBox.Show("Error Master Finish Goods - 2 => " & ex.Message)
                    End Try
                End Using
            End If


        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub dgv_finish_goods_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_finish_goods.CellClick
        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If e.ColumnIndex = -1 Then
            Exit Sub
        End If

        If dgv_finish_goods.Columns(e.ColumnIndex).Name = "delete" Then
            If globVar.delete > 0 Then

                Dim queryCek2 As String = "select * from dbo.master_process_flow where master_finish_goods_pn='" & dgv_finish_goods.Rows(e.RowIndex).Cells("FG Part Number").Value & "'"
                Dim dsexist2 = New DataSet
                Dim adapterexist2 = New SqlDataAdapter(queryCek2, Database.koneksi)
                adapterexist2.Fill(dsexist2)

                If dsexist2.Tables(0).Rows.Count > 0 Then
                    RJMessageBox.Show("Cannot delete. Because this FG still used in master process flow")
                    Exit Sub
                End If

                Dim queryCek3 As String = "select * from dbo.material_usage_finish_goods where fg_part_number='" & dgv_finish_goods.Rows(e.RowIndex).Cells("FG Part Number").Value & "'"
                Dim dsexist3 = New DataSet
                Dim adapterexist3 = New SqlDataAdapter(queryCek3, Database.koneksi)
                adapterexist3.Fill(dsexist3)

                If dsexist3.Tables(0).Rows.Count > 0 Then
                    RJMessageBox.Show("Cannot delete. Because this FG still used in material usage finish goods")
                    Exit Sub
                End If

                Dim result = RJMessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then
                    Try
                        Dim sql As String = "delete from MASTER_FINISH_GOODS where FG_PART_NUMBER='" & dgv_finish_goods.Rows(e.RowIndex).Cells("FG Part Number").Value & "'"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        If cmd.ExecuteNonQuery() Then
                            RJMessageBox.Show("Delete Success")
                        End If
                        dgv_finish_goods.DataSource = Nothing
                        DGV_MasterFinishGoods()
                    Catch ex As Exception
                        RJMessageBox.Show("Error Master Finish Goods - 3 => " & ex.Message)
                    End Try
                End If
            Else
                RJMessageBox.Show("Your Access cannot execute this action")
            End If
        End If

        If dgv_finish_goods.Columns(e.ColumnIndex).Name = "check" Then
            If dgv_finish_goods.Rows(e.RowIndex).Cells(0).Value = True Then
                dgv_finish_goods.Rows(e.RowIndex).Cells(0).Value = False
            Else
                dgv_finish_goods.Rows(e.RowIndex).Cells(0).Value = True
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If globVar.delete > 0 Then

            Dim hapus As Integer = 0
            Dim result = RJMessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                For Each row As DataGridViewRow In dgv_finish_goods.Rows
                    If row.Cells(0).Value = True Then

                        Dim queryCek2 As String = "select * from dbo.master_process_flow where master_finish_goods_pn='" & row.Cells("FG Part Number").Value & "'"
                        Dim dsexist2 = New DataSet
                        Dim adapterexist2 = New SqlDataAdapter(queryCek2, Database.koneksi)
                        adapterexist2.Fill(dsexist2)

                        If dsexist2.Tables(0).Rows.Count > 0 Then
                            RJMessageBox.Show("Cannot delete. Because this FG still used in master process flow")
                            Continue For
                        End If

                        Dim sql As String = "delete from MASTER_FINISH_GOODS where FG_PART_NUMBER='" & row.Cells("FG Part Number").Value & "'"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        cmd.ExecuteNonQuery()
                        hapus = hapus + 1
                    End If
                Next
            End If

            dgv_finish_goods.DataSource = Nothing
            DGV_MasterFinishGoods()
            RJMessageBox.Show("Delete Success " & hapus & " Data.")
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub txt_masterfinishgoods_search_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_masterfinishgoods_search.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            Dim Found As Boolean = False
            Dim StringToSearch As String = ""
            Dim ValueToSearchFor As String = Me.txt_masterfinishgoods_search.Text.Trim.ToLower
            Dim CurrentRowIndex As Integer = 0
            Try
                If dgv_finish_goods.Rows.Count = 0 Then
                    CurrentRowIndex = 0
                Else
                    CurrentRowIndex = dgv_finish_goods.CurrentRow.Index + 1
                End If
                If CurrentRowIndex > dgv_finish_goods.Rows.Count Then
                    CurrentRowIndex = dgv_finish_goods.Rows.Count - 1
                End If
                If dgv_finish_goods.Rows.Count > 0 Then
                    For Each gRow As DataGridViewRow In dgv_finish_goods.Rows
                        StringToSearch = gRow.Cells("FG Part Number").Value.ToString.Trim.ToLower
                        If InStr(1, StringToSearch, LCase(Trim(txt_masterfinishgoods_search.Text)), vbTextCompare) = 1 Then
                            Dim myCurrentCell As DataGridViewCell = gRow.Cells("FG Part Number")
                            Dim myCurrentPosition As DataGridViewCell = gRow.Cells("FG Part Number")
                            dgv_finish_goods.CurrentCell = myCurrentCell
                            CurrentRowIndex = dgv_finish_goods.CurrentRow.Index
                            Found = True
                            txt_masterfinishgoods_search.Clear()
                        End If
                        If Found Then Exit For
                    Next

                    If Found = False Then
                        RJMessageBox.Show("Data Doesn't exist")
                        txt_masterfinishgoods_search.Clear()
                    End If
                End If
            Catch ex As Exception
                RJMessageBox.Show(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub dgv_finish_goods_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgv_finish_goods.DataBindingComplete
        For i As Integer = 0 To dgv_finish_goods.RowCount - 1
            If dgv_finish_goods.Rows(i).Index Mod 2 = 0 Then
                dgv_finish_goods.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_finish_goods.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With dgv_finish_goods
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

    Private Sub btn_export_template_Click(sender As Object, e As EventArgs) Handles btn_export_template.Click
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

        For i = 1 To dgv_finish_goods.RowCount - 2
            For j = 1 To dgv_finish_goods.ColumnCount - 2
                For k As Integer = 1 To dgv_finish_goods.Columns.Count
                    xlWorkSheet.Cells(1, k) = dgv_finish_goods.Columns(k - 1).HeaderText
                    xlWorkSheet.Cells(i + 2, j + 1) = dgv_finish_goods(j, i).Value.ToString()
                Next
            Next
        Next
        FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
            xlWorkSheet.SaveAs(directoryPath & "\Master Finish Goods.xlsx")
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
            For k As Integer = 1 To dgv_finish_goods.Columns.Count
                xlWorkSheet.Cells(1, k) = dgv_finish_goods.Columns(k - 1).HeaderText
            Next

            ' Menyalin data ke array dua dimensi
            Dim dataArray(dgv_finish_goods.RowCount - 1, dgv_finish_goods.ColumnCount - 1) As Object
            For i As Integer = 0 To dgv_finish_goods.RowCount - 1
                For j As Integer = 0 To dgv_finish_goods.ColumnCount - 1
                    dataArray(i, j) = dgv_finish_goods(j, i).Value
                Next
            Next

            ' Menyalin array ke lembar kerja Excel
            xlWorkSheet.Range("A2").Resize(dgv_finish_goods.RowCount, dgv_finish_goods.ColumnCount).Value = dataArray

            ' Mengatur direktori awal untuk dialog
            FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

            ' Memilih folder penyimpanan
            If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
                Dim currentDate As Date = DateTime.Now
                Dim namafile As String = "Master Finish Goods Export - " & currentDate.ToString("yyyy-MM-dd HH-mm-ss") & ".xlsx"
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

    Private Sub btn_ex_template_Click(sender As Object, e As EventArgs) Handles btn_ex_template.Click
        If globVar.view > 0 Then

            Dim excelApp As Excel.Application = New Excel.Application()

            'create new workbook
            Dim workbook As Excel.Workbook = excelApp.Workbooks.Add()

            'create new worksheet
            Dim worksheet As Excel.Worksheet = workbook.Worksheets.Add()

            'write data to worksheet
            worksheet.Range("A1").Value = "Finish Goods Part Number *"
            worksheet.Range("B1").Value = "Department (zQSFP, Ten60 etc) *"
            worksheet.Range("C1").Value = "Level (Sub Assy, FG etc) *"
            worksheet.Range("D1").Value = "Description *"
            worksheet.Range("E1").Value = "Standard Pack *"
            worksheet.Range("F1").Value = "Family (zSFP, zQSFP etc) *"
            worksheet.Range("G1").Value = "Laser Code"

            'save the workbook
            FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

            If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
                workbook.SaveAs(directoryPath & "\Master Finish Goods Template.xlsx")
            End If

            'cleanup
            excelApp.Quit()
            Marshal.ReleaseComObject(excelApp)
            RJMessageBox.Show("Export Template Success !")
        End If
    End Sub

    Private Sub txt_pn_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_pn.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt_spq_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_spq.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt_pn_TextChanged(sender As Object, e As EventArgs) Handles txt_pn.TextChanged
        If txt_pn.Text.StartsWith("0") AndAlso txt_pn.Text.Length > 1 Then
            txt_pn.Text = txt_pn.Text.TrimStart("0"c)
            txt_pn.SelectionStart = txt_pn.Text.Length
        End If
    End Sub
End Class