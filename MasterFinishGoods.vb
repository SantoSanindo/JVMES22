Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.Office.Interop

Public Class MasterFinishGoods
    Dim oleCon As OleDbConnection
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call Database.koneksi_database()
        If txt_dept.Text <> "" And txt_pn.Text <> "" And txt_desc.Text <> "" And txt_level.Text <> "" And txt_spq.Text <> "" Then
            If IsNumeric(txt_spq.Text) Then
                Dim querycheck As String = "select * from MASTER_FINISH_GOODS where FG_PART_NUMBER='" & txt_pn.Text & "'"
                Dim dtCheck As DataTable = Database.GetData(querycheck)
                If dtCheck.Rows.Count > 0 Then
                    MessageBox.Show("Finish Goods Already Exist")
                Else
                    Try
                        Dim sql As String = "INSERT INTO MASTER_FINISH_GOODS (FG_PART_NUMBER,DEPARTMENT,LEVEL,DESCRIPTION,SPQ,LASER_CODE) VALUES ('" & txt_pn.Text & "','" & txt_dept.Text & "','" & txt_level.Text & "','" & txt_desc.Text & "'," & txt_spq.Text & ",'" & txt_laser.Text & "')"
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
                        MessageBox.Show("Error Insert" & ex.Message)
                    End Try
                End If
            Else
                MessageBox.Show("SPQ must be number.")
                txt_dept.Text = ""
                txt_pn.Text = ""
                txt_desc.Text = ""
                txt_level.Text = ""
                txt_spq.Text = ""
                txt_laser.Text = ""
                txt_dept.Select()
            End If
        End If
    End Sub

    Sub tampilDataComboBoxDepartement()
        Call Database.koneksi_database()
        Dim dtMasterDepart As DataTable = Database.GetData("select * from department")

        txt_dept.DataSource = dtMasterDepart
        txt_dept.DisplayMember = "department"
        txt_dept.ValueMember = "department"
        'txt_dept.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        'txt_dept.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub MasterFinishGoods_Load(sender As Object, e As EventArgs) Handles Me.Load
        DGV_MasterFinishGoods()
        txt_dept.Select()
        txt_dept.Text = ""
        tampilDataComboBoxDepartement()
    End Sub

    Sub DGV_MasterFinishGoods()
        dgv_finish_goods.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_finish_goods.Rows.Clear()
        dgv_finish_goods.Columns.Clear()
        Call Database.koneksi_database()
        Dim dtMasterMaterial As DataTable = Database.GetData("select DEPARTMENT,FG_PART_NUMBER,DESCRIPTION,LEVEL, SPQ, LASER_CODE from MASTER_FINISH_GOODS")

        dgv_finish_goods.DataSource = dtMasterMaterial

        Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
        check.Name = "check"
        check.HeaderText = "Check"
        check.Width = 100
        check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgv_finish_goods.Columns.Insert(0, check)

        dgv_finish_goods.Columns(0).Width = 100
        dgv_finish_goods.Columns(3).Width = 800
        dgv_finish_goods.Columns(6).Width = 200

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        dgv_finish_goods.Columns.Insert(7, delete)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
            Dim xlApp As New Microsoft.Office.Interop.Excel.Application
            Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook = xlApp.Workbooks.Open(OpenFileDialog1.FileName)
            Dim SheetName As String = xlWorkBook.Worksheets(1).Name.ToString
            Dim excelpath As String = OpenFileDialog1.FileName
            Dim koneksiExcel As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & excelpath & ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'"
            oleCon = New OLEDBConnection(koneksiExcel)
            oleCon.Open()

            Dim queryExcel As String = "select * from [" & SheetName & "$]"
            Dim cmd As OleDbCommand = New OleDbCommand(queryExcel, oleCon)
            Dim rd As OleDbDataReader

            Call Database.koneksi_database()
            Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(Database.koneksi)
                bulkCopy.DestinationTableName = "dbo.MASTER_FINISH_GOODS"
                Try
                    rd = cmd.ExecuteReader

                    bulkCopy.ColumnMappings.Add(0, 0)
                    bulkCopy.ColumnMappings.Add(1, 1)
                    bulkCopy.ColumnMappings.Add(2, 2)
                    bulkCopy.ColumnMappings.Add(3, 3)
                    bulkCopy.ColumnMappings.Add(4, 4)
                    bulkCopy.ColumnMappings.Add(5, 5)

                    bulkCopy.WriteToServer(rd)
                    rd.Close()

                    dgv_finish_goods.DataSource = Nothing
                    DGV_MasterFinishGoods()
                    MsgBox("Import Finish Goods Success")
                Catch ex As Exception
                    MsgBox("Import Finish Goods Failed " & ex.Message)
                End Try
            End Using
        End If
    End Sub

    Private Sub dgv_finish_goods_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_finish_goods.CellClick
        If dgv_finish_goods.Columns(e.ColumnIndex).Name = "delete" Then
            Dim result = MessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from MASTER_FINISH_GOODS where FG_PART_NUMBER='" & dgv_finish_goods.Rows(e.RowIndex).Cells(2).Value & "'"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()
                    dgv_finish_goods.DataSource = Nothing
                    DGV_MasterFinishGoods()
                    MessageBox.Show("Delete Success.")
                Catch ex As Exception
                    MessageBox.Show("failed" & ex.Message)
                End Try
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
        Dim hapus As Integer = 0
        Dim result = MessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then
            For Each row As DataGridViewRow In dgv_finish_goods.Rows
                If row.Cells(0).Value = True Then
                    Dim sql As String = "delete from MASTER_FINISH_GOODS where FG_PART_NUMBER='" & row.Cells(2).Value & "'"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()
                    hapus = hapus + 1
                End If
            Next
        End If

        dgv_finish_goods.DataSource = Nothing
        DGV_MasterFinishGoods()
        MessageBox.Show("Delete Success " & hapus & " Data.")
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
                        StringToSearch = gRow.Cells("FG_PART_NUMBER").Value.ToString.Trim.ToLower
                        If InStr(1, StringToSearch, LCase(Trim(txt_masterfinishgoods_search.Text)), vbTextCompare) = 1 Then
                            Dim myCurrentCell As DataGridViewCell = gRow.Cells("FG_PART_NUMBER")
                            Dim myCurrentPosition As DataGridViewCell = gRow.Cells("FG_PART_NUMBER")
                            dgv_finish_goods.CurrentCell = myCurrentCell
                            CurrentRowIndex = dgv_finish_goods.CurrentRow.Index
                            Found = True
                            txt_masterfinishgoods_search.Clear()
                        End If
                        If Found Then Exit For
                    Next
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
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
    End Sub

    Private Sub btn_export_template_Click(sender As Object, e As EventArgs) Handles btn_export_template.Click
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

        MsgBox("Export to Excel Success !")
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
        Dim excelApp As Excel.Application = New Excel.Application()

        'create new workbook
        Dim workbook As Excel.Workbook = excelApp.Workbooks.Add()

        'create new worksheet
        Dim worksheet As Excel.Worksheet = workbook.Worksheets.Add()

        'write data to worksheet
        worksheet.Range("A1").Value = "Finish Goods Part Number"
        worksheet.Range("B1").Value = "Department"
        worksheet.Range("C1").Value = "Description"
        worksheet.Range("D1").Value = "Standard Pack"
        worksheet.Range("E1").Value = "Laser Code"

        'save the workbook
        FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
            workbook.SaveAs(directoryPath & "\Master Finish Goods Template.xlsx")
        End If

        'cleanup
        excelApp.Quit()
        Marshal.ReleaseComObject(excelApp)
        MsgBox("Export Template Success !")
    End Sub
End Class