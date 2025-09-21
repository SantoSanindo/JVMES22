Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.Office.Interop

Public Class MasterMaterial
    Public Shared menu As String = "Master Material"

    Dim oleCon As OleDbConnection
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If globVar.add > 0 Then
            Call Database.koneksi_database()
            If txt_mastermaterial_pn.Text <> "" And txt_mastermaterial_qty.Text <> "" And txt_pn_name.Text <> "" And cb_mastermaterial_family.Text <> "" And cb_mastermaterial_dept.Text <> "" Then
                If IsNumeric(txt_mastermaterial_pn.Text) And IsNumeric(txt_mastermaterial_qty.Text) Then
                    Try
                        Dim sql As String = "
                        IF NOT EXISTS (SELECT 1 FROM MASTER_MATERIAL WHERE part_number = '" & txt_mastermaterial_pn.Text & "' AND department = '" & cb_mastermaterial_dept.Text & "' and family = '" & cb_mastermaterial_family.Text & "')
                            BEGIN
                                INSERT INTO MASTER_MATERIAL (part_number,name,STANDARD_QTY, FAMILY, DEPARTMENT, BY_WHO) VALUES ('" & txt_mastermaterial_pn.Text & "','" & txt_pn_name.Text & "'," & txt_mastermaterial_qty.Text & ",'" & cb_mastermaterial_family.Text & "','" & cb_mastermaterial_dept.Text & "','" & globVar.username & "')
                            END
                        ELSE
                            BEGIN
                                RAISERROR('Data already exists', 16, 1)
                            END"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)

                        If cmd.ExecuteNonQuery() Then
                            txt_pn_name.Text = ""
                            txt_mastermaterial_pn.Text = ""
                            txt_mastermaterial_qty.Text = ""
                            cb_mastermaterial_family.SelectedIndex = -1
                            cb_mastermaterial_dept.SelectedIndex = -1
                            txt_mastermaterial_pn.Select()

                            dgv_material.DataSource = Nothing
                            DGV_MasterMaterial()
                        End If
                    Catch ex As Exception
                        RJMessageBox.Show("Error Master Material - 1 =>" & ex.Message)
                    End Try
                Else
                    RJMessageBox.Show("Part Number / Qty must be number.")
                    txt_mastermaterial_pn.Text = ""
                    txt_mastermaterial_qty.Text = ""
                    txt_mastermaterial_pn.Select()
                End If
            Else
                RJMessageBox.Show("Please input all data", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub MasterMaterial_Load(sender As Object, e As EventArgs) Handles Me.Load
        If globVar.view > 0 Then
            txt_mastermaterial_pn.Select()
            DGV_MasterMaterial()
            txt_mastermaterial_search.Text = ""
            tampilDataComboBoxDepartement()
            tampilDataComboBoxFamily()
            cb_mastermaterial_family.SelectedIndex = -1
            cb_mastermaterial_dept.SelectedIndex = -1
        End If
    End Sub

    Sub tampilDataComboBoxFamily()
        Call Database.koneksi_database()
        Dim dtMasterFamily As DataTable = Database.GetData("select family from family where department='" & globVar.department & "' order by family")

        cb_mastermaterial_family.DataSource = dtMasterFamily
        cb_mastermaterial_family.DisplayMember = "family"
        cb_mastermaterial_family.ValueMember = "family"
    End Sub

    Sub tampilDataComboBoxDepartement()
        Call Database.koneksi_database()
        Dim sql As String
        If globVar.username = "admin" Then
            sql = "select * from department order by department"
        Else
            sql = "select * from department where department='" & globVar.department & "' order by department"
        End If
        Dim dtMasterDepartment As DataTable = Database.GetData(sql)

        cb_mastermaterial_dept.DataSource = dtMasterDepartment
        cb_mastermaterial_dept.DisplayMember = "department"
        cb_mastermaterial_dept.ValueMember = "department"
    End Sub

    Private Sub DGV_MasterMaterial()
        dgv_material.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_material.DataSource = Nothing
        dgv_material.Rows.Clear()
        dgv_material.Columns.Clear()
        Call Database.koneksi_database()
        Dim dtMasterMaterial As DataTable = Database.GetData("select PART_NUMBER [Part Number],NAME [Name], STANDARD_QTY [Std Qty], FAMILY [Family], DEPARTMENT [Department], insert_date [Date Time], by_who [Created By] from MASTER_MATERIAL where department='" & globVar.department & "' order by part_number")

        dgv_material.DataSource = dtMasterMaterial

        Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
        check.Name = "check"
        check.HeaderText = "Check For Delete"
        check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgv_material.Columns.Insert(0, check)

        dgv_material.Columns(0).Width = 200
        dgv_material.Columns(1).Width = 200
        dgv_material.Columns(2).Width = 500

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 200
        delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        dgv_material.Columns.Insert(8, delete)
    End Sub

    Private Sub dgv_material_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_material.CellClick
        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If e.ColumnIndex = -1 Then
            Exit Sub
        End If

        If dgv_material.Columns(e.ColumnIndex).Name = "delete" Then
            If globVar.delete > 0 Then
                Dim queryCek As String = "SELECT * FROM dbo.material_usage_finish_goods where component='" & dgv_material.Rows(e.RowIndex).Cells("Part Number").Value & "'"
                Dim dsexist = New DataSet
                Dim adapterexist = New SqlDataAdapter(queryCek, Database.koneksi)
                adapterexist.Fill(dsexist)

                If dsexist.Tables(0).Rows.Count > 0 Then
                    RJMessageBox.Show("Cannot delete. Because this material still used in Material Usage Finish Goods")
                    Exit Sub
                End If

                Dim result = RJMessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then
                    Try
                        Dim sql As String = "delete from master_material where part_number='" & dgv_material.Rows(e.RowIndex).Cells("Part Number").Value & "' and family='" & dgv_material.Rows(e.RowIndex).Cells("Family").Value & "' and department='" & dgv_material.Rows(e.RowIndex).Cells("Department").Value & "'"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        If cmd.ExecuteNonQuery() Then
                            RJMessageBox.Show("Delete Success")
                        End If
                        dgv_material.DataSource = Nothing
                        DGV_MasterMaterial()
                    Catch ex As Exception
                        RJMessageBox.Show("Error Master Material - 2 =>" & ex.Message)
                    End Try
                End If
            Else
                RJMessageBox.Show("Your Access cannot execute this action")
            End If
        End If

        If dgv_material.Columns(e.ColumnIndex).Name = "check" Then
            If dgv_material.Rows(e.RowIndex).Cells(0).Value = True Then
                dgv_material.Rows(e.RowIndex).Cells(0).Value = False
            Else
                dgv_material.Rows(e.RowIndex).Cells(0).Value = True
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If globVar.delete > 0 Then
            Dim hapus As Integer = 0
            Dim result = RJMessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                For Each row As DataGridViewRow In dgv_material.Rows
                    If row.Cells(0).Value = True Then

                        Dim queryCek As String = "SELECT * FROM dbo.material_usage_finish_goods where component='" & row.Cells("Part Number").Value & "'"
                        Dim dsexist = New DataSet
                        Dim adapterexist = New SqlDataAdapter(queryCek, Database.koneksi)
                        adapterexist.Fill(dsexist)

                        If dsexist.Tables(0).Rows.Count > 0 Then
                            RJMessageBox.Show("Cannot delete. Because this material still used in Material Usage Finish Goods")
                            Continue For
                        End If

                        Dim sql As String = "delete from master_material where part_number='" & row.Cells("Part Number").Value & "' and family='" & row.Cells("Family").Value & "' and department='" & row.Cells("Department").Value & "'"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        cmd.ExecuteNonQuery()
                        hapus = hapus + 1
                    End If
                Next
            End If

            DGV_MasterMaterial()
            RJMessageBox.Show("Delete Success " & hapus & " Data.")
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
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

                ' Use only Jet provider (same as working Finish Goods code)
                Dim koneksiExcel As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & excelpath & ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'"
                oleCon = New OleDbConnection(koneksiExcel)
                oleCon.Open()

                Dim queryExcel As String = "SELECT * FROM [" & SheetName & "$]"
                Dim cmd As OleDbCommand = New OleDbCommand(queryExcel, oleCon)
                Dim rd As OleDbDataReader

                Call Database.koneksi_database()

                Dim duplicateRows As New List(Of String)
                Dim validationErrors As New List(Of String)
                Dim totalInserted As Integer = 0

                Try
                    rd = cmd.ExecuteReader()
                    Dim rowNumber As Integer = 1

                    While rd.Read()
                        rowNumber += 1

                        ' Use column names (same approach as working Finish Goods code)
                        Dim partNumber As String = rd("Part Number *").ToString().Trim()
                        Dim materialName As String = rd("Name *").ToString().Trim()
                        Dim standardQtyStr As String = rd("Standard Qty *").ToString().Trim()
                        Dim family As String = rd("Family *").ToString().Trim()

                        ' Validate required fields
                        If String.IsNullOrEmpty(partNumber) Then
                            validationErrors.Add($"Row {rowNumber}: Part Number is required")
                            Continue While
                        End If

                        If String.IsNullOrEmpty(materialName) Then
                            validationErrors.Add($"Row {rowNumber}: Material Name is required")
                            Continue While
                        End If

                        If String.IsNullOrEmpty(family) Then
                            validationErrors.Add($"Row {rowNumber}: Family is required")
                            Continue While
                        End If

                        If String.IsNullOrEmpty(standardQtyStr) OrElse Not IsNumeric(standardQtyStr) Then
                            validationErrors.Add($"Row {rowNumber}: Standard Qty must be a valid number")
                            Continue While
                        End If

                        Dim standardQty As Integer = Convert.ToInt32(standardQtyStr)
                        If standardQty <= 0 Then
                            validationErrors.Add($"Row {rowNumber}: Standard Qty must be greater than 0")
                            Continue While
                        End If

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

                        ' Check if part number already exists
                        Dim checkExistQuery As String = "SELECT COUNT(*) FROM dbo.MASTER_MATERIAL WHERE PART_NUMBER = @partNumber AND DEPARTMENT = @dept AND FAMILY = @family"
                        Using checkCmd As New SqlCommand(checkExistQuery, Database.koneksi)
                            checkCmd.Parameters.AddWithValue("@partNumber", partNumber)
                            checkCmd.Parameters.AddWithValue("@dept", globVar.department)
                            checkCmd.Parameters.AddWithValue("@family", family)

                            Dim existCount As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

                            If existCount > 0 Then
                                duplicateRows.Add($"{partNumber} (Row {rowNumber})")
                                Continue While
                            End If
                        End Using

                        ' Insert new material
                        Dim insertQuery As String = "INSERT INTO dbo.MASTER_MATERIAL (PART_NUMBER, NAME, STANDARD_QTY, FAMILY, DEPARTMENT, INSERT_DATE, BY_WHO) VALUES (@partNumber, @name, @qty, @family, @dept, GETDATE(), @user)"
                        Using insertCmd As New SqlCommand(insertQuery, Database.koneksi)
                            insertCmd.Parameters.AddWithValue("@partNumber", partNumber)
                            insertCmd.Parameters.AddWithValue("@name", materialName)
                            insertCmd.Parameters.AddWithValue("@qty", standardQty)
                            insertCmd.Parameters.AddWithValue("@family", family)
                            insertCmd.Parameters.AddWithValue("@dept", globVar.department)
                            insertCmd.Parameters.AddWithValue("@user", globVar.username)

                            insertCmd.ExecuteNonQuery()
                            totalInserted += 1
                        End Using

                    End While

                    rd.Close()

                    ' Show results (same format as Finish Goods)
                    If validationErrors.Count > 0 Then
                        Dim errorMessage As String = "Import completed with errors:" & Environment.NewLine & Environment.NewLine
                        errorMessage += String.Join(Environment.NewLine, validationErrors.Take(10))

                        If validationErrors.Count > 10 Then
                            errorMessage += Environment.NewLine & $"... and {validationErrors.Count - 10} more errors"
                        End If

                        RJMessageBox.Show(errorMessage, "Import Results", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Else
                        Dim successMessage As String = $"Import Material Success! {totalInserted} records imported."

                        If duplicateRows.Count > 0 Then
                            successMessage += Environment.NewLine & Environment.NewLine & "Duplicates skipped: " & String.Join(", ", duplicateRows)
                        End If

                        RJMessageBox.Show(successMessage, "Import Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    ' Refresh the grid
                    DGV_MasterMaterial()

                Catch ex As Exception
                    RJMessageBox.Show("Error Master Material - Import => " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If rd IsNot Nothing AndAlso Not rd.IsClosed Then rd.Close()
                    If oleCon.State = ConnectionState.Open Then oleCon.Close()
                    xlWorkBook.Close()
                    xlApp.Quit()
                    Marshal.ReleaseComObject(xlApp)
                End Try

            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub txt_mastermaterial_search_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_mastermaterial_search.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            Dim Found As Boolean = False
            Dim StringToSearch As String = ""
            Dim ValueToSearchFor As String = Me.txt_mastermaterial_search.Text.Trim.ToLower
            Dim CurrentRowIndex As Integer = 0
            Try
                If dgv_material.Rows.Count = 0 Then
                    CurrentRowIndex = 0
                Else
                    CurrentRowIndex = dgv_material.CurrentRow.Index + 1
                End If
                If CurrentRowIndex > dgv_material.Rows.Count Then
                    CurrentRowIndex = dgv_material.Rows.Count - 1
                End If
                If dgv_material.Rows.Count > 0 Then
                    For Each gRow As DataGridViewRow In dgv_material.Rows
                        StringToSearch = gRow.Cells("Part Number").Value.ToString.Trim.ToLower
                        If InStr(1, StringToSearch, LCase(Trim(txt_mastermaterial_search.Text)), vbTextCompare) = 1 Then
                            Dim myCurrentCell As DataGridViewCell = gRow.Cells("Part Number")
                            Dim myCurrentPosition As DataGridViewCell = gRow.Cells(0)
                            dgv_material.CurrentCell = myCurrentCell
                            CurrentRowIndex = dgv_material.CurrentRow.Index
                            Found = True
                            txt_mastermaterial_search.Clear()
                        End If
                        If Found Then Exit For
                    Next

                    If Found = False Then
                        RJMessageBox.Show("Data Doesn't exist")
                        txt_mastermaterial_search.Clear()
                    End If
                End If
            Catch ex As Exception
                RJMessageBox.Show("Error Master Material - 4 =>" & ex.Message)
            End Try
        End If
    End Sub

    Private Sub dgv_material_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgv_material.DataBindingComplete
        For i As Integer = 0 To dgv_material.RowCount - 1
            If dgv_material.Rows(i).Index Mod 2 = 0 Then
                dgv_material.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_material.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With dgv_material
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
    Private Sub ExportToExcel()
        If globVar.view > 0 Then
            Dim xlApp As New Excel.Application
            Dim xlWorkBook As Excel.Workbook
            Dim xlWorkSheet As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim i As Integer
            Dim j As Integer
            xlWorkBook = xlApp.Workbooks.Add(misValue)
            xlWorkSheet = xlWorkBook.Sheets("sheet1")

            For i = 0 To dgv_material.RowCount - 1
                For j = 1 To dgv_material.ColumnCount - 1
                    For k As Integer = 1 To dgv_material.Columns.Count
                        xlWorkSheet.Cells(1, k) = dgv_material.Columns(k - 1).HeaderText
                        xlWorkSheet.Cells(i + 2, j + 1) = dgv_material(j, i).Value.ToString()
                    Next
                Next
            Next
            FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

            If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
                xlWorkSheet.SaveAs(directoryPath & "\Master Material.xlsx")
            End If

            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)

            RJMessageBox.Show("Export to Excel Success !")
        End If
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
            For k As Integer = 1 To dgv_material.Columns.Count
                xlWorkSheet.Cells(1, k) = dgv_material.Columns(k - 1).HeaderText
            Next

            ' Menyalin data ke array dua dimensi
            Dim dataArray(dgv_material.RowCount - 1, dgv_material.ColumnCount - 1) As Object
            For i As Integer = 0 To dgv_material.RowCount - 1
                For j As Integer = 0 To dgv_material.ColumnCount - 1
                    dataArray(i, j) = dgv_material(j, i).Value
                Next
            Next

            ' Menyalin array ke lembar kerja Excel
            xlWorkSheet.Range("A2").Resize(dgv_material.RowCount, dgv_material.ColumnCount).Value = dataArray

            ' Mengatur direktori awal untuk dialog
            FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

            ' Memilih folder penyimpanan
            If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
                Dim currentDate As Date = DateTime.Now
                Dim namafile As String = "Master Material Export - " & currentDate.ToString("yyyy-MM-dd HH-mm-ss") & ".xlsx"
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

    Private Sub btn_export_template_Click(sender As Object, e As EventArgs) Handles btn_export_template.Click
        'ExportToExcel()
        ExportToExcelV2()
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
            worksheet.Range("A1").Value = "Part Number *"
            worksheet.Range("B1").Value = "Name *"
            worksheet.Range("C1").Value = "Standard Qty *"
            worksheet.Range("D1").Value = "Family *"

            ' Set column widths for better usability
            worksheet.Columns("A").ColumnWidth = 25
            worksheet.Columns("B").ColumnWidth = 40
            worksheet.Columns("C").ColumnWidth = 15
            worksheet.Columns("D").ColumnWidth = 20

            ' Format header row
            With worksheet.Range("A1:D1")
                .Font.Bold = True
                .Interior.Color = RGB(200, 200, 200)
            End With

            'save the workbook
            FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
                workbook.SaveAs(directoryPath & "\Master Material Template.xlsx")
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

    Private Sub txt_mastermaterial_pn_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_mastermaterial_pn.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt_mastermaterial_qty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_mastermaterial_qty.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt_mastermaterial_pn_TextChanged(sender As Object, e As EventArgs) Handles txt_mastermaterial_pn.TextChanged
        If txt_mastermaterial_pn.Text.StartsWith("0") AndAlso txt_mastermaterial_pn.Text.Length > 1 Then
            txt_mastermaterial_pn.Text = txt_mastermaterial_pn.Text.TrimStart("0"c)
            txt_mastermaterial_pn.SelectionStart = txt_mastermaterial_pn.Text.Length
        End If
    End Sub
End Class