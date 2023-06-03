Imports System.Data.OleDb
Imports System.Data.SqlClient
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
                    Dim querycheck As String = "select * from MASTER_MATERIAL where part_number='" & txt_mastermaterial_pn.Text & "' and upper(department)='" & cb_mastermaterial_dept.Text.ToUpper & "' and upper(family)='" & cb_mastermaterial_family.Text.ToUpper & "'"
                    Dim dtCheck As DataTable = Database.GetData(querycheck)
                    If dtCheck.Rows.Count > 0 Then
                        RJMessageBox.Show("Material Exist")
                    Else
                        Try
                            Dim sql As String = "INSERT INTO MASTER_MATERIAL VALUES ('" & txt_mastermaterial_pn.Text & "','" & txt_pn_name.Text & "'," & txt_mastermaterial_qty.Text & ",'" & cb_mastermaterial_family.Text & "','" & cb_mastermaterial_dept.Text & "')"
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
                    End If
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

    Private Sub DGV_MasterMaterial()
        dgv_material.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_material.DataSource = Nothing
        dgv_material.Rows.Clear()
        dgv_material.Columns.Clear()
        Call Database.koneksi_database()
        Dim dtMasterMaterial As DataTable = Database.GetData("select PART_NUMBER,NAME, STANDARD_QTY, FAMILY, DEPARTMENT from MASTER_MATERIAL")

        dgv_material.DataSource = dtMasterMaterial

        Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
        check.Name = "check"
        check.HeaderText = "Check"
        check.Width = 100
        check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgv_material.Columns.Insert(0, check)

        dgv_material.Columns(0).Width = 100
        dgv_material.Columns(1).Width = 250
        dgv_material.Columns(2).Width = 800

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        dgv_material.Columns.Insert(6, delete)
    End Sub

    Private Sub dgv_material_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_material.CellClick

        If dgv_material.Columns(e.ColumnIndex).Name = "delete" Then
            If globVar.delete > 0 Then
                Dim result = RJMessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then
                    Try
                        Dim sql As String = "delete from master_material where part_number='" & dgv_material.Rows(e.RowIndex).Cells("PART_NUMBER").Value & "' and family='" & dgv_material.Rows(e.RowIndex).Cells("FAMILY").Value & "' and department='" & dgv_material.Rows(e.RowIndex).Cells("DEPARTMENT").Value & "'"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        cmd.ExecuteNonQuery()
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
                        Dim sql As String = "delete from master_material where part_number='" & row.Cells("PART_NUMBER").Value & "' and family='" & row.Cells("FAMILY").Value & "' and department='" & row.Cells("DEPARTMENT").Value & "'"
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
                Dim filePath As String = OpenFileDialog1.FileName
                importOneByOne(filePath)
            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
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
                Dim PN As String
                Dim FamMaterial As String = reader.GetString(3)
                Dim NameMaterial As String = reader.GetString(1)
                Dim SPQMaterial As Double = reader.GetDouble(2)
                Dim DeptMaterial As String = reader.GetString(4)

                Dim PNMaterial As Object = reader.GetValue(0)
                If IsNumeric(SPQMaterial) Then
                    Dim PNMaterialNumeric As Double = CDbl(PNMaterial)
                    PN = PNMaterialNumeric
                Else
                    Dim PNMaterialString As String = CStr(PNMaterial)
                    PN = PNMaterialString
                End If

                Dim existsCmd As New SqlCommand("SELECT COUNT(*) FROM dbo.MASTER_MATERIAL WHERE [part_number] = '" & PN & "' and upper(department)='" & DeptMaterial.ToUpper & "' and upper(family)='" & FamMaterial.ToUpper & "'", Database.koneksi)
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
                    Dim sql As String = "INSERT INTO dbo.MASTER_MATERIAL (part_number,name,standard_qty,family,department) VALUES ('" & PN & "', '" & NameMaterial & "', " & SPQMaterial & ",'" & FamMaterial & "','" & DeptMaterial & "')"

                    Dim insertCmd As New SqlCommand(sql, Database.koneksi)
                    insertCmd.ExecuteNonQuery()
                    totalInsert = totalInsert + 1
                End If
            End While
            DGV_MasterMaterial()
            RJMessageBox.Show("Import Material Success. Total " & totalInsert & " new Material ")
        Catch ex As Exception
            RJMessageBox.Show("Error Master Material - 3 =>" & ex.Message)
            oleCon.Close()
        Finally
            oleCon.Close()
        End Try
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
                        StringToSearch = gRow.Cells("PART_NUMBER").Value.ToString.Trim.ToLower
                        If InStr(1, StringToSearch, LCase(Trim(txt_mastermaterial_search.Text)), vbTextCompare) = 1 Then
                            Dim myCurrentCell As DataGridViewCell = gRow.Cells("PART_NUMBER")
                            Dim myCurrentPosition As DataGridViewCell = gRow.Cells(0)
                            dgv_material.CurrentCell = myCurrentCell
                            CurrentRowIndex = dgv_material.CurrentRow.Index
                            Found = True
                            txt_mastermaterial_search.Clear()
                        End If
                        If Found Then Exit For
                    Next
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
        ExportToExcel()
    End Sub

    Private Sub btn_ex_template_Click(sender As Object, e As EventArgs) Handles btn_ex_template.Click
        If globVar.view > 0 Then
            Dim excelApp As Excel.Application = New Excel.Application()

            'create new workbook
            Dim workbook As Excel.Workbook = excelApp.Workbooks.Add()

            'create new worksheet
            Dim worksheet As Excel.Worksheet = workbook.Worksheets.Add()

            'write data to worksheet
            worksheet.Range("A1").Value = "Part Number"
            worksheet.Range("B1").Value = "Name"
            worksheet.Range("C1").Value = "Standard Qty"
            worksheet.Range("D1").Value = "Family"
            worksheet.Range("E1").Value = "Department"

            'save the workbook
            FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

            If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
                workbook.SaveAs(directoryPath & "\Master Material Template.xlsx")
            End If

            'cleanup
            excelApp.Quit()
            Marshal.ReleaseComObject(excelApp)
            RJMessageBox.Show("Export Template Success !")
        End If
    End Sub
End Class