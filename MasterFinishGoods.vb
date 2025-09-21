Imports System.Data.OleDb
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
            OpenFileDialog1.Filter = "Excel Files|*.xlsx;*.xls;*.csv"
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
                Dim validationErrors As New List(Of String)
                Dim dataTable As New DataTable()

                dataTable.Columns.Add("FG_PART_NUMBER", GetType(String))
                dataTable.Columns.Add("LEVEL", GetType(String))
                dataTable.Columns.Add("DESCRIPTION", GetType(String))
                dataTable.Columns.Add("SPQ", GetType(Int32))
                dataTable.Columns.Add("FAMILY", GetType(String))
                dataTable.Columns.Add("LASER_CODE", GetType(String))
                dataTable.Columns.Add("DEPARTMENT", GetType(String))

                Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(Database.koneksi)
                    bulkCopy.DestinationTableName = "dbo.MASTER_FINISH_GOODS"
                    Try
                        rd = cmd.ExecuteReader()
                        Dim rowNumber As Integer = 1

                        While rd.Read()
                            rowNumber += 1
                            Dim fgPartNumber As String = rd("Finish Goods Part Number *").ToString().Trim()
                            Dim level As String = rd("Level (Sub Assy or FG) *").ToString().Trim()
                            Dim description As String = rd("Description *").ToString().Trim()
                            Dim standardPack As String = rd("Standard Pack *").ToString().Trim()
                            Dim family As String = rd("Family *").ToString().Trim()
                            Dim laserCode As String = If(rd.IsDBNull(rd.GetOrdinal("Laser Code")), "", rd("Laser Code").ToString().Trim())

                            ' Validate required fields
                            If String.IsNullOrEmpty(fgPartNumber) Then
                                validationErrors.Add($"Row {rowNumber}: Finish Goods Part Number is required")
                                Continue While
                            End If

                            If String.IsNullOrEmpty(level) Then
                                validationErrors.Add($"Row {rowNumber}: Level is required")
                                Continue While
                            End If

                            If String.IsNullOrEmpty(description) Then
                                validationErrors.Add($"Row {rowNumber}: Description is required")
                                Continue While
                            End If

                            If String.IsNullOrEmpty(standardPack) OrElse Not IsNumeric(standardPack) Then
                                validationErrors.Add($"Row {rowNumber}: Standard Pack must be a valid number")
                                Continue While
                            End If

                            If String.IsNullOrEmpty(family) Then
                                validationErrors.Add($"Row {rowNumber}: Family is required")
                                Continue While
                            End If

                            ' Normalize and validate level
                            Dim normalizedLevel As String = ""
                            Select Case level.ToLower().Replace(" ", "")
                                Case "fg", "finishedgoods", "finishgoods"
                                    normalizedLevel = "FG"
                                Case "subassy", "subassembly", "sub-assy", "sub-assembly"
                                    normalizedLevel = "Sub Assy"
                                Case Else
                                    validationErrors.Add($"Row {rowNumber}: Level '{level}' is invalid. Must be 'FG' or 'Sub Assy'")
                                    Continue While
                            End Select

                            ' Check if FG Part Number already exists in MASTER_FINISH_GOODS
                            Dim cekQuery As String = "SELECT COUNT(*) FROM dbo.MASTER_FINISH_GOODS WHERE FG_PART_NUMBER = @FG_PART_NUMBER"
                            Using cekCmd As New SqlCommand(cekQuery, Database.koneksi)
                                cekCmd.Parameters.AddWithValue("@FG_PART_NUMBER", fgPartNumber)
                                Dim exists As Integer = Convert.ToInt32(cekCmd.ExecuteScalar())

                                If exists > 0 Then
                                    duplicateRows.Add($"{fgPartNumber} (Row {rowNumber})")
                                    Continue While
                                End If
                            End Using

                            ' Check if family exists in FAMILY table for current department
                            Dim cekFamily As String = "SELECT COUNT(*) FROM dbo.FAMILY WHERE FAMILY = @family AND DEPARTMENT = @department"
                            Using cekCmdFam As New SqlCommand(cekFamily, Database.koneksi)
                                cekCmdFam.Parameters.AddWithValue("@family", family)
                                cekCmdFam.Parameters.AddWithValue("@department", globVar.department)

                                Dim famExists As Integer = Convert.ToInt32(cekCmdFam.ExecuteScalar())
                                If famExists = 0 Then
                                    validationErrors.Add($"Row {rowNumber}: Family '{family}' does not exist for department '{globVar.department}'")
                                    Continue While
                                End If
                            End Using

                            ' If level is Sub Assy, check if part number exists in MASTER_MATERIAL
                            If normalizedLevel = "Sub Assy" Then
                                Dim cekMaterial As String = "SELECT COUNT(*) FROM dbo.MASTER_MATERIAL WHERE PART_NUMBER = @part_number AND DEPARTMENT = @department"
                                Using cekCmdMat As New SqlCommand(cekMaterial, Database.koneksi)
                                    cekCmdMat.Parameters.AddWithValue("@part_number", fgPartNumber)
                                    cekCmdMat.Parameters.AddWithValue("@department", globVar.department)

                                    Dim matExists As Integer = Convert.ToInt32(cekCmdMat.ExecuteScalar())
                                    If matExists = 0 Then
                                        validationErrors.Add($"Row {rowNumber}: Sub Assy '{fgPartNumber}' must exist in Master Material table first")
                                        Continue While
                                    End If
                                End Using
                            End If

                            ' If all validations pass, add to DataTable
                            Dim row As DataRow = dataTable.NewRow()
                            row("FG_PART_NUMBER") = fgPartNumber
                            row("LEVEL") = normalizedLevel
                            row("DESCRIPTION") = description
                            row("SPQ") = Convert.ToInt32(standardPack)
                            row("FAMILY") = family
                            row("LASER_CODE") = If(String.IsNullOrEmpty(laserCode), DBNull.Value, laserCode)
                            row("DEPARTMENT") = globVar.department
                            dataTable.Rows.Add(row)
                        End While

                        rd.Close()

                        ' Show validation errors if any
                        If validationErrors.Count > 0 Then
                            Dim errorMessage As String = "Import failed due to validation errors:" & Environment.NewLine & Environment.NewLine
                            errorMessage += String.Join(Environment.NewLine, validationErrors.Take(10)) ' Show first 10 errors

                            If validationErrors.Count > 10 Then
                                errorMessage += Environment.NewLine & $"... and {validationErrors.Count - 10} more errors"
                            End If

                            RJMessageBox.Show(errorMessage, "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        ' Bulk insert if there are valid rows
                        If dataTable.Rows.Count > 0 Then
                            bulkCopy.ColumnMappings.Add("FG_PART_NUMBER", "FG_PART_NUMBER")
                            bulkCopy.ColumnMappings.Add("DEPARTMENT", "DEPARTMENT")
                            bulkCopy.ColumnMappings.Add("LEVEL", "LEVEL")
                            bulkCopy.ColumnMappings.Add("DESCRIPTION", "DESCRIPTION")
                            bulkCopy.ColumnMappings.Add("SPQ", "SPQ")
                            bulkCopy.ColumnMappings.Add("FAMILY", "FAMILY")
                            bulkCopy.ColumnMappings.Add("LASER_CODE", "LASER_CODE")

                            bulkCopy.WriteToServer(dataTable)

                            Dim successMessage As String = $"Import successful! {dataTable.Rows.Count} records imported."

                            If duplicateRows.Count > 0 Then
                                successMessage += Environment.NewLine & Environment.NewLine & "Duplicates skipped: " & String.Join(", ", duplicateRows)
                            End If

                            RJMessageBox.Show(successMessage, "Import Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            RJMessageBox.Show("No valid records found to import.", "Import Result", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If

                        dgv_finish_goods.DataSource = Nothing
                        DGV_MasterFinishGoods()

                    Catch ex As Exception

                        RJMessageBox.Show("Error Master Finish Goods - Import => " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    Finally
                        If rd IsNot Nothing AndAlso Not rd.IsClosed Then rd.Close()
                        If oleCon.State = ConnectionState.Open Then oleCon.Close()
                        xlWorkBook.Close()
                        xlApp.Quit()
                        Marshal.ReleaseComObject(xlApp)
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

            ' Format entire worksheet as text before adding data
            worksheet.Cells.NumberFormat = "@"

            'write data to worksheet
            worksheet.Range("A1").Value = "Finish Goods Part Number *"
            worksheet.Range("B1").Value = "Level (Sub Assy or FG) *"
            worksheet.Range("C1").Value = "Description *"
            worksheet.Range("D1").Value = "Standard Pack *"
            worksheet.Range("E1").Value = "Family *"
            worksheet.Range("F1").Value = "Laser Code"

            ' Set column widths for better usability
            worksheet.Columns("A").ColumnWidth = 30
            worksheet.Columns("B").ColumnWidth = 25
            worksheet.Columns("C").ColumnWidth = 40
            worksheet.Columns("D").ColumnWidth = 15
            worksheet.Columns("E").ColumnWidth = 20
            worksheet.Columns("F").ColumnWidth = 20

            ' Format header row
            With worksheet.Range("A1:F1")
                .Font.Bold = True
                .Interior.Color = RGB(200, 200, 200)
            End With

            'save the workbook
            FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
                workbook.SaveAs(directoryPath & "\Master Finish Goods Template.xlsx")
            End If
            'cleanup
            workbook.Close()
            excelApp.Quit()
            Marshal.ReleaseComObject(workbook)
            Marshal.ReleaseComObject(worksheet)
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