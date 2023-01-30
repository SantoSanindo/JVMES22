Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

Public Class MaterialUsageFinishGoods
    Dim oleCon As OleDbConnection
    Public idP As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If cb_masterfinishgoods_pn.Text <> "" And txt_masterfinishgoods_desc.Text <> "" And txt_masterfinishgoods_family.Text <> "" And txt_masterfinishgoods_comp.Text <> "" And txt_masterfinishgoods_usage.Text <> "" Then
            Dim querycheck As String = "select * from MATERIAL_USAGE_FINISH_GOODS where FG_PART_NUMBER='" & cb_masterfinishgoods_pn.Text & "' and COMPONENT='" & txt_masterfinishgoods_comp.Text & "'"
            Dim dtCheck As DataTable = Database.GetData(querycheck)
            If dtCheck.Rows.Count > 0 Then
                MessageBox.Show("FG Part Number and Comp exist")
            Else
                Try
                    Dim sql As String = "INSERT INTO MATERIAL_USAGE_FINISH_GOODS(FG_PART_NUMBER,DESCRIPTION,FAMILY,COMPONENT,USAGE) VALUES ('" & cb_masterfinishgoods_pn.Text & "','" & txt_masterfinishgoods_desc.Text & "','" & txt_masterfinishgoods_family.Text & "','" & txt_masterfinishgoods_comp.Text & "'," & txt_masterfinishgoods_usage.Text.Replace(",", ".") & ")"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()

                    DGV_Masterfinishgoods_atass(cb_masterfinishgoods_pn.Text)
                    treeView_show()

                    idP = cb_masterfinishgoods_pn.Text

                    cb_masterfinishgoods_pn.Text = ""
                    txt_masterfinishgoods_desc.Text = ""
                    txt_masterfinishgoods_family.Text = ""
                    txt_masterfinishgoods_comp.Text = ""
                    txt_masterfinishgoods_usage.Text = ""
                    cb_masterfinishgoods_pn.Select()
                Catch ex As Exception
                    MessageBox.Show("Error Insert" & ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub MasterFinishGoods_Load(sender As Object, e As EventArgs) Handles Me.Load
        treeView_show()
        idP = ""
        tampilDataComboBox()
    End Sub

    Sub tampilDataComboBox()
        Call Database.koneksi_database()
        Dim dtMasterFinishGoods As DataTable = Database.GetData("select fg_part_number from master_finish_goods order by fg_part_number")

        cb_masterfinishgoods_pn.DataSource = dtMasterFinishGoods
        cb_masterfinishgoods_pn.DisplayMember = "fg_part_number"
        cb_masterfinishgoods_pn.ValueMember = "fg_part_number"
        cb_masterfinishgoods_pn.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cb_masterfinishgoods_pn.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub treeView_show()
        TreeView1.Nodes.Clear()
        Dim queryFinishGoods As String = "select DISTINCT(FG_PART_NUMBER) from MATERIAL_USAGE_FINISH_GOODS order by fg_part_number"
        Dim dtFinishGoods As DataTable = Database.GetData(queryFinishGoods)

        TreeView1.Nodes.Add("Material Usage Finish Goods")

        For i = 0 To dtFinishGoods.Rows.Count - 1
            TreeView1.Nodes(0).Nodes.Add(dtFinishGoods.Rows(i).Item("FG_PART_NUMBER").ToString, "FG PN : " & dtFinishGoods.Rows(i).Item("FG_PART_NUMBER").ToString)
        Next
        TreeView1.ExpandAll()
    End Sub

    Private Sub dgv_masterfinishgoods_atas_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_masterfinishgoods_atas.CellClick
        If dgv_masterfinishgoods_atas.Columns(e.ColumnIndex).Name = "delete" Then

            Dim result = MessageBox.Show("Are you sure delete this data?", "warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from MATERIAL_USAGE_FINISH_GOODS where ID=" & dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(1).Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()
                    DGV_Masterfinishgoods_atass(idP)
                    treeView_show()
                    MessageBox.Show("Delete Success.")
                Catch ex As Exception
                    MessageBox.Show("Delete Failed" & ex.Message)
                End Try
            End If
        End If

        If e.ColumnIndex = 0 Then
            If dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(0).Value = True Then
                dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(0).Value = False
            Else
                dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(0).Value = True
            End If
        End If
    End Sub

    Private Sub DGV_Masterfinishgoods_atass(id As String)
        dgv_masterfinishgoods_atas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_masterfinishgoods_atas.DataSource = Nothing
        dgv_masterfinishgoods_atas.Rows.Clear()
        dgv_masterfinishgoods_atas.Columns.Clear()
        Call Database.koneksi_database()
        Dim queryMasterFinishGoods As String = "select ID,FG_PART_NUMBER,DESCRIPTION,FAMILY,COMPONENT,USAGE from MATERIAL_USAGE_FINISH_GOODS where fg_part_number='" & id & "'"
        Dim dtMasterMaterial As DataTable = Database.GetData(queryMasterFinishGoods)

        dgv_masterfinishgoods_atas.DataSource = dtMasterMaterial

        dgv_masterfinishgoods_atas.Columns(0).Width = 100
        dgv_masterfinishgoods_atas.Columns(1).Width = 300
        dgv_masterfinishgoods_atas.Columns(2).Width = 500
        dgv_masterfinishgoods_atas.Columns(3).Width = 150
        dgv_masterfinishgoods_atas.Columns(5).Width = 150

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        dgv_masterfinishgoods_atas.Columns.Insert(6, delete)

        Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
        check.Name = "check"
        check.HeaderText = "Check"
        check.Width = 100
        check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgv_masterfinishgoods_atas.Columns.Insert(0, check)

        For i As Integer = 0 To dgv_masterfinishgoods_atas.RowCount - 1
            If dgv_masterfinishgoods_atas.Rows(i).Index Mod 2 = 0 Then
                dgv_masterfinishgoods_atas.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_masterfinishgoods_atas.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Dim hapus As Integer = 0
        Dim result = MessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then
            For Each row As DataGridViewRow In dgv_masterfinishgoods_atas.Rows
                If row.Cells(0).Value = True Then
                    Dim sql As String = "delete from MATERIAL_USAGE_FINISH_GOODS where id=" & row.Cells(1).Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()
                    hapus = hapus + 1
                End If
            Next
        End If

        treeView_show()
        DGV_Masterfinishgoods_atass(idP)
        MessageBox.Show("Delete Success " & hapus & " Data.")
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_masterfinishgoods_search.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            If CheckBox1.CheckState = CheckState.Unchecked Then
                Dim FoundTreeview As Boolean = False
                For Each nd As TreeNode In TreeView1.Nodes
                    If nd.Nodes.Count > 0 Then
                        For Each ndChild As TreeNode In nd.Nodes
                            If String.Compare(ndChild.Name, txt_masterfinishgoods_search.Text, True) = 0 Then
                                TreeView1.SelectedNode = ndChild
                                TreeView1.Select()
                                FoundTreeview = True
                                Exit For
                            End If
                        Next
                    End If
                Next
                If FoundTreeview = False Then
                    MessageBox.Show("Data not Found")
                End If
            Else
                Dim Found As Boolean = False
                Dim StringToSearch As String = ""
                Dim ValueToSearchFor As String = Me.txt_masterfinishgoods_search.Text.Trim.ToLower
                Dim CurrentRowIndex As Integer = 0
                Try
                    If dgv_masterfinishgoods_atas.Rows.Count = 0 Then
                        CurrentRowIndex = 0
                        MessageBox.Show("Data not Found")
                        txt_masterfinishgoods_search.Clear()
                    Else
                        CurrentRowIndex = dgv_masterfinishgoods_atas.CurrentRow.Index + 1
                    End If
                    If CurrentRowIndex > dgv_masterfinishgoods_atas.Rows.Count Then
                        CurrentRowIndex = dgv_masterfinishgoods_atas.Rows.Count - 1
                    End If
                    If dgv_masterfinishgoods_atas.Rows.Count > 0 Then
                        For Each gRow As DataGridViewRow In dgv_masterfinishgoods_atas.Rows
                            StringToSearch = gRow.Cells("Component").Value.ToString.Trim.ToLower
                            If InStr(1, StringToSearch, LCase(Trim(txt_masterfinishgoods_search.Text)), vbTextCompare) = 1 Then
                                Dim myCurrentCell As DataGridViewCell = gRow.Cells("Component")
                                Dim myCurrentPosition As DataGridViewCell = gRow.Cells(0)
                                dgv_masterfinishgoods_atas.CurrentCell = myCurrentCell
                                CurrentRowIndex = dgv_masterfinishgoods_atas.CurrentRow.Index
                                Found = True
                                txt_masterfinishgoods_search.Clear()
                                Exit For
                            End If
                        Next
                        If Found = False Then
                            MessageBox.Show("Data not Found")
                        End If
                    End If
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            End If
        End If
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        If TreeView1.SelectedNode Is Nothing Then
            dgv_masterfinishgoods_atas.DataSource = Nothing
            Exit Sub
        End If

        idP = TreeView1.SelectedNode.Name

        DGV_Masterfinishgoods_atass(idP)
    End Sub

    Private Sub dgv_material_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgv_masterfinishgoods_atas.DataBindingComplete
        For i As Integer = 0 To dgv_masterfinishgoods_atas.RowCount - 1
            If dgv_masterfinishgoods_atas.Rows(i).Index Mod 2 = 0 Then
                dgv_masterfinishgoods_atas.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_masterfinishgoods_atas.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
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
                bulkCopy.DestinationTableName = "dbo.MATERIAL_USAGE_FINISH_GOODS"
                Try
                    rd = cmd.ExecuteReader

                    bulkCopy.ColumnMappings.Add(0, 1)
                    bulkCopy.ColumnMappings.Add(1, 2)
                    bulkCopy.ColumnMappings.Add(2, 3)
                    bulkCopy.ColumnMappings.Add(3, 4)
                    bulkCopy.ColumnMappings.Add(4, 5)

                    bulkCopy.WriteToServer(rd)
                    rd.Close()

                    DGV_Masterfinishgoods_atass(cb_masterfinishgoods_pn.Text)
                    treeView_show()

                    idP = cb_masterfinishgoods_pn.Text

                    MsgBox("Import Material Usage Finish Goods Success")
                Catch ex As Exception
                    MsgBox("Import Material Usage Finish Goods Failed " & ex.Message)
                End Try
            End Using
        End If
    End Sub

    Private Sub btn_ex_template_Click(sender As Object, e As EventArgs) Handles btn_ex_template.Click
        Dim excelApp As Excel.Application = New Excel.Application()

        'create new workbook
        Dim workbook As Excel.Workbook = excelApp.Workbooks.Add()

        'create new worksheet
        Dim worksheet As Excel.Worksheet = workbook.Worksheets.Add()

        'write data to worksheet
        worksheet.Range("A1").Value = "Finish Goods Part Number"
        worksheet.Range("B1").Value = "Family"
        worksheet.Range("C1").Value = "Component"
        worksheet.Range("D1").Value = "Description"
        worksheet.Range("E1").Value = "Usage"

        'save the workbook
        FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
            workbook.SaveAs(directoryPath & "\Master Material Usage Finish Goods Template.xlsx")
        End If

        'cleanup
        excelApp.Quit()
        Marshal.ReleaseComObject(excelApp)
    End Sub

    Private Sub btn_export_Master_Usage_Finish_Goods_Click(sender As Object, e As EventArgs) Handles btn_export_Master_Usage_Finish_Goods.Click
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

        For i = 1 To dgv_masterfinishgoods_atas.RowCount - 2
            For j = 1 To dgv_masterfinishgoods_atas.ColumnCount - 2
                For k As Integer = 1 To dgv_masterfinishgoods_atas.Columns.Count
                    xlWorkSheet.Cells(1, k) = dgv_masterfinishgoods_atas.Columns(k - 1).HeaderText
                    xlWorkSheet.Cells(i + 2, j + 1) = dgv_masterfinishgoods_atas(j, i).Value.ToString()
                Next
            Next
        Next
        FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
            xlWorkSheet.SaveAs(directoryPath & "\Master Material Finish Goods.xlsx")
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
End Class