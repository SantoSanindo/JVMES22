Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.Office.Interop

Public Class MasterProcess
    Public Shared menu As String = "Master Process"

    Dim oleCon As OleDbConnection
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txt_masterprocess_nama.Text <> "" Then
            Dim querycheck As String = "select * from MASTER_PROCESS where upper(PROCESS_NAME)='" & Trim(txt_masterprocess_nama.Text.ToUpper) & "' and upper(department)='" & cb_mastermaterial_dept.Text.ToUpper & "' and upper(family)='" & cb_mastermaterial_family.Text.ToUpper & "'"
            Dim dtCheck As DataTable = Database.GetData(querycheck)
            If dtCheck.Rows.Count > 0 Then
                RJMessageBox.Show("Process Exist")
                txt_masterprocess_nama.Text = ""
                txt_masterprocess_desc.Text = ""
            Else
                Try
                    Dim sql As String = "INSERT INTO MASTER_PROCESS(PROCESS_NAME,PROCESS_DESC,FAMILY,DEPARTMENT) VALUES ('" & Trim(txt_masterprocess_nama.Text) & "','" & Trim(txt_masterprocess_desc.Text) & "','" & cb_mastermaterial_family.Text & "','" & cb_mastermaterial_dept.Text & "')"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()

                    txt_masterprocess_nama.Text = ""
                    txt_masterprocess_desc.Text = ""
                    txt_masterprocess_nama.Select()

                    dgv_masterprocess.DataSource = Nothing
                    DGV_MasterProcesss()
                Catch ex As Exception
                    RJMessageBox.Show("Error Insert" & ex.Message)
                End Try
            End If
        End If
    End Sub

    Sub tampilDataComboBoxFamily()
        Call Database.koneksi_database()
        Dim dtMasterFamily As DataTable = Database.GetData("select * from family")

        cb_mastermaterial_family.DataSource = dtMasterFamily
        cb_mastermaterial_family.DisplayMember = "family"
        cb_mastermaterial_family.ValueMember = "family"
    End Sub

    Sub tampilDataComboBoxDepartement()
        Call Database.koneksi_database()
        Dim dtMasterDepartment As DataTable = Database.GetData("select * from department")

        cb_mastermaterial_dept.DataSource = dtMasterDepartment
        cb_mastermaterial_dept.DisplayMember = "department"
        cb_mastermaterial_dept.ValueMember = "department"
    End Sub

    Private Sub MasterMaterial_Load(sender As Object, e As EventArgs) Handles Me.Load
        txt_masterprocess_nama.Select()
        DGV_MasterProcesss()
        txt_masterprocess_search.Text = ""
        tampilDataComboBoxFamily()
        tampilDataComboBoxDepartement()
        cb_mastermaterial_dept.SelectedIndex = -1
        cb_mastermaterial_family.SelectedIndex = -1
    End Sub

    Private Sub DGV_MasterProcesss()
        dgv_masterprocess.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_masterprocess.DataSource = Nothing
        dgv_masterprocess.Rows.Clear()
        dgv_masterprocess.Columns.Clear()
        Call Database.koneksi_database()
        Dim dtMasterMaterial As DataTable = Database.GetData("select process_name as Name_Process,process_desc as Desc_Process, FAMILY, DEPARTMENT from MASTER_PROCESS")

        dgv_masterprocess.DataSource = dtMasterMaterial

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        dgv_masterprocess.Columns.Insert(4, delete)
    End Sub

    Private Sub dgv_masterprocess_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_masterprocess.CellClick
        If dgv_masterprocess.Columns(e.ColumnIndex).Name = "delete" Then
            Dim result = RJMessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Try
                    Dim querycheck As String = "select * from MASTER_PROCESS_FLOW mpf, MASTER_FINISH_GOODS mfg where mpf.MASTER_FINISH_GOODS_PN=mfg.FG_PART_NUMBER and lower(mpf.MASTER_PROCESS)='" & Trim(dgv_masterprocess.Rows(e.RowIndex).Cells("Name_Process").Value.ToLower) & "' and lower(mfg.family)='" & Trim(dgv_masterprocess.Rows(e.RowIndex).Cells("FAMILY").Value.ToLower) & "' and lower(mfg.department)='" & Trim(dgv_masterprocess.Rows(e.RowIndex).Cells("DEPARTMENT").Value.ToLower) & "'"
                    Dim dtCheck As DataTable = Database.GetData(querycheck)
                    If dtCheck.Rows.Count > 0 Then
                        RJMessageBox.Show("Cannot Delete this data because refrence to process flow.")
                    Else
                        Dim sql As String = "delete from master_process where process_name='" & dgv_masterprocess.Rows(e.RowIndex).Cells("Name_Process").Value & "' and family='" & dgv_masterprocess.Rows(e.RowIndex).Cells("FAMILY").Value & "' and department='" & dgv_masterprocess.Rows(e.RowIndex).Cells("DEPARTMENT").Value & "'"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        cmd.ExecuteNonQuery()
                        dgv_masterprocess.DataSource = Nothing
                        DGV_MasterProcesss()
                    End If
                Catch ex As Exception
                    RJMessageBox.Show("Delete failed" & ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub dgv_material_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgv_masterprocess.DataBindingComplete
        For i As Integer = 0 To dgv_masterprocess.RowCount - 1
            If dgv_masterprocess.Rows(i).Index Mod 2 = 0 Then
                dgv_masterprocess.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_masterprocess.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With dgv_masterprocess
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

    Private Sub txt_masterprocess_search_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_masterprocess_search.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            Dim Found As Boolean = False
            Dim StringToSearch As String = ""
            Dim ValueToSearchFor As String = Me.txt_masterprocess_search.Text.Trim.ToLower
            Dim CurrentRowIndex As Integer = 0
            Try
                If dgv_masterprocess.Rows.Count = 0 Then
                    CurrentRowIndex = 0
                Else
                    CurrentRowIndex = dgv_masterprocess.CurrentRow.Index + 1
                End If
                If CurrentRowIndex > dgv_masterprocess.Rows.Count Then
                    CurrentRowIndex = dgv_masterprocess.Rows.Count - 1
                End If
                If dgv_masterprocess.Rows.Count > 0 Then
                    For Each gRow As DataGridViewRow In dgv_masterprocess.Rows
                        StringToSearch = gRow.Cells("Name_Process").Value.ToString.Trim.ToLower
                        If InStr(1, StringToSearch, LCase(Trim(txt_masterprocess_search.Text)), vbTextCompare) = 1 Then
                            Dim myCurrentCell As DataGridViewCell = gRow.Cells("Name_Process")
                            Dim myCurrentPosition As DataGridViewCell = gRow.Cells(0)
                            dgv_masterprocess.CurrentCell = myCurrentCell
                            CurrentRowIndex = dgv_masterprocess.CurrentRow.Index
                            Found = True
                            txt_masterprocess_search.Clear()
                        End If
                        If Found Then Exit For
                    Next
                End If
            Catch ex As Exception
                RJMessageBox.Show(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog1.Filter = "Excel Files|*.xlsx;*.xls;*.csv"
        If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
            Dim filePath As String = OpenFileDialog1.FileName
            importOneByOne(filePath)
        End If
    End Sub

    Sub importOneByOne(filePath As String)
        Dim xlApp As New Microsoft.Office.Interop.Excel.Application
        Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook = xlApp.Workbooks.Open(filePath)
        Dim SheetName As String = xlWorkBook.Worksheets(1).Name.ToString
        Dim connectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & filePath & ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'"
        oleCon = New OleDbConnection(connectionString)
        Dim totalInsert As Integer = 0

        Call Database.koneksi_database()
        Try
            oleCon.Open()
            Dim cmd As New OleDbCommand("SELECT * FROM [" & SheetName & "$]", oleCon)
            Dim reader As OleDbDataReader = cmd.ExecuteReader()
            While reader.Read()
                Dim ProcessName As String = reader.GetString(0)
                Dim ProcessDesc As String = reader.GetString(1)
                Dim DeptMaterial As String = reader.GetString(3)
                Dim FamMaterial As String = reader.GetString(2)

                Dim existsCmd As New SqlCommand("SELECT COUNT(*) FROM dbo.MASTER_PROCESS WHERE upper([process_name]) = '" & Trim(ProcessName.ToUpper) & "' and upper(department)='" & Trim(DeptMaterial.ToUpper) & "' and upper(family)='" & Trim(FamMaterial.ToUpper) & "'", Database.koneksi)
                Dim count As Integer = existsCmd.ExecuteScalar()

                Dim existsDept As New SqlCommand("SELECT COUNT(*) FROM dbo.DEPARTMENT WHERE [DEPARTMENT] = '" & DeptMaterial & "'", Database.koneksi)
                Dim countDept As Integer = existsDept.ExecuteScalar()
                If countDept = 0 Then
                    RJMessageBox.Show("Sorry Department Wrong Format")
                    Exit Sub
                End If

                Dim existsFam As New SqlCommand("SELECT COUNT(*) FROM dbo.FAMILY WHERE [FAMILY] = '" & FamMaterial & "'", Database.koneksi)
                Dim countFam As Integer = existsFam.ExecuteScalar()
                If countFam = 0 Then
                    RJMessageBox.Show("Sorry Family Wrong Format")
                    Exit Sub
                End If

                If count = 0 Then
                    Dim sql As String = "INSERT INTO MASTER_PROCESS(PROCESS_NAME,PROCESS_DESC,DEPARTMENT,FAMILY) VALUES ('" & Trim(ProcessName) & "','" & Trim(ProcessDesc) & "','" & Trim(DeptMaterial) & "','" & Trim(FamMaterial) & "')"

                    Dim insertCmd As New SqlCommand(sql, Database.koneksi)
                    insertCmd.ExecuteNonQuery()
                    totalInsert = totalInsert + 1
                End If
            End While
            DGV_MasterProcesss()
            RJMessageBox.Show("Import Master Process Success. Total " & totalInsert & " new Material ")
        Catch ex As Exception
            RJMessageBox.Show("Import Master Process Failed " & ex.Message)
        Finally
            oleCon.Close()
        End Try
    End Sub

    Sub ImportBulk(filePath As String)
        Dim xlApp As New Microsoft.Office.Interop.Excel.Application
        Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook = xlApp.Workbooks.Open(filePath)
        Dim SheetName As String = xlWorkBook.Worksheets(1).Name.ToString
        Dim koneksiExcel As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & filePath & ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'"
        oleCon = New OleDbConnection(koneksiExcel)
        oleCon.Open()

        Dim queryExcel As String = "select * from [" & SheetName & "$]"
        Dim cmd As OleDbCommand = New OleDbCommand(queryExcel, oleCon)
        Dim rd As OleDbDataReader

        Call Database.koneksi_database()
        Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(Database.koneksi)
            bulkCopy.DestinationTableName = "dbo.MASTER_PROCESS"
            Try
                rd = cmd.ExecuteReader

                bulkCopy.ColumnMappings.Add(0, 1)
                bulkCopy.ColumnMappings.Add(1, 2)

                bulkCopy.WriteToServer(rd)
                rd.Close()

                DGV_MasterProcesss()
                RJMessageBox.Show("Import Master Process Success")
            Catch ex As Exception
                RJMessageBox.Show("Import Master Process Failed " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btn_ex_template_Click(sender As Object, e As EventArgs) Handles btn_ex_template.Click
        Dim excelApp As Excel.Application = New Excel.Application()

        'create new workbook
        Dim workbook As Excel.Workbook = excelApp.Workbooks.Add()

        'create new worksheet
        Dim worksheet As Excel.Worksheet = workbook.Worksheets.Add()

        'write data to worksheet
        worksheet.Range("A1").Value = "Process Name"
        worksheet.Range("B1").Value = "Process Description"
        worksheet.Range("C1").Value = "Family"
        worksheet.Range("D1").Value = "Department"

        'save the workbook
        FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
            workbook.SaveAs(directoryPath & "\Master Process Template.xlsx")
        End If

        'cleanup
        excelApp.Quit()
        Marshal.ReleaseComObject(excelApp)
        RJMessageBox.Show("Export Template Success !")
    End Sub

    Private Sub btn_export_Master_Process_Click(sender As Object, e As EventArgs) Handles btn_export_Master_Process.Click
        ExportToExcel()
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

        For i = 1 To dgv_masterprocess.RowCount - 2
            For j = 1 To dgv_masterprocess.ColumnCount - 2
                For k As Integer = 1 To dgv_masterprocess.Columns.Count
                    xlWorkSheet.Cells(1, k) = dgv_masterprocess.Columns(k - 1).HeaderText
                    xlWorkSheet.Cells(i + 2, j + 1) = dgv_masterprocess(j, i).Value.ToString()
                Next
            Next
        Next
        FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
            xlWorkSheet.SaveAs(directoryPath & "\Master Process.xlsx")
        End If

        xlWorkBook.Close()
        xlApp.Quit()

        releaseObject(xlWorkSheet)
        releaseObject(xlWorkBook)
        releaseObject(xlApp)

        RJMessageBox.Show("Export to Excel Success !")
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

    Private Sub txt_masterprocess_nama_TextChanged(sender As Object, e As EventArgs) Handles txt_masterprocess_nama.TextChanged
        txt_masterprocess_nama.Text = txt_masterprocess_nama.Text.ToUpper()
        txt_masterprocess_nama.SelectionStart = txt_masterprocess_nama.Text.Length
    End Sub
End Class