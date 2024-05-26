Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

Public Class MaterialUsageFinishGoods
    Public Shared menu As String = "Material Usage & Finish Goods"

    Dim oleCon As OleDbConnection
    Public idP As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If globVar.add > 0 Then
            If txt_masterfinishgoods_usage.Text = "" Then
                RJMessageBox.Show("Usage still blank. Please input first.")
            End If

            If cb_masterfinishgoods_pn.Text <> "" And txt_masterfinishgoods_desc.Text <> "" And txt_masterfinishgoods_family.Text <> "" And cb_masterfinishgoods_component.Text <> "" And txt_masterfinishgoods_usage.Text <> "" Then

                Dim queryMasterFinishGoods As String = "select * from master_material where part_number='" & cb_masterfinishgoods_component.Text & "'"
                Dim dtMasterFG As DataTable = Database.GetData(queryMasterFinishGoods)
                If dtMasterFG.Rows.Count = 0 Then
                    RJMessageBox.Show("Sorry. The material not exist in master material. Please input first.")
                    Exit Sub
                End If

                Try
                    Dim sql As String = "
                        IF NOT EXISTS (SELECT 1 FROM MATERIAL_USAGE_FINISH_GOODS WHERE FG_PART_NUMBER = '" & cb_masterfinishgoods_pn.Text & "' AND COMPONENT = '" & cb_masterfinishgoods_component.Text & "')
                            BEGIN
                                INSERT INTO MATERIAL_USAGE_FINISH_GOODS (FG_PART_NUMBER,DESCRIPTION,FAMILY,COMPONENT,USAGE, BY_WHO) VALUES ('" & cb_masterfinishgoods_pn.Text & "','" & txt_masterfinishgoods_desc.Text & "','" & txt_masterfinishgoods_family.Text & "','" & cb_masterfinishgoods_component.Text & "','" & txt_masterfinishgoods_usage.Text & "','" & globVar.username & "')
                            END
                        ELSE
                            BEGIN
                                RAISERROR('Data already exists', 16, 1)
                            END"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()

                    DGV_Masterfinishgoods_atass(cb_masterfinishgoods_pn.Text)
                    treeView_show()

                    idP = cb_masterfinishgoods_pn.Text

                    txt_masterfinishgoods_desc.Clear()
                    cb_masterfinishgoods_component.SelectedIndex = -1
                    txt_masterfinishgoods_usage.Clear()
                    txt_masterfinishgoods_family.Clear()

                Catch ex As Exception
                    RJMessageBox.Show("Error Master Material Usage Finish Goods - 1 => " & ex.Message)
                End Try
            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub MasterFinishGoods_Load(sender As Object, e As EventArgs) Handles Me.Load
        If globVar.view > 0 Then

            treeView_show()
            idP = ""
            txt_masterfinishgoods_family.Text = ""
            tampilDataComboBox()

            If cb_masterfinishgoods_pn.Items.Count > 0 Then
                cb_masterfinishgoods_pn.SelectedIndex = -1
            End If

        End If
    End Sub

    Sub tampilDataComboBox()
        Call Database.koneksi_database()
        Dim dtMasterFinishGoods As DataTable = Database.GetData("select fg_part_number from master_finish_goods order by fg_part_number")

        cb_masterfinishgoods_pn.DataSource = dtMasterFinishGoods
        cb_masterfinishgoods_pn.DisplayMember = "fg_part_number"
        cb_masterfinishgoods_pn.ValueMember = "fg_part_number"
    End Sub

    Sub tampilDataComboBoxMaterial()
        Call Database.koneksi_database()
        Dim dtMasterMaterial As DataTable = Database.GetData("select part_number from master_material where standard_qty>0 and family='" & txt_masterfinishgoods_family.Text & "' order by part_number")

        cb_masterfinishgoods_component.DataSource = dtMasterMaterial
        cb_masterfinishgoods_component.DisplayMember = "part_number"
        cb_masterfinishgoods_component.ValueMember = "part_number"
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
        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If e.ColumnIndex = -1 Then
            Exit Sub
        End If

        If dgv_masterfinishgoods_atas.Columns(e.ColumnIndex).Name = "delete" Then
            If globVar.delete > 0 Then
                Dim result = RJMessageBox.Show("Are you sure delete this data?", "warning", MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then
                    Try
                        Dim sql As String = "delete from MATERIAL_USAGE_FINISH_GOODS where ID=" & dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(1).Value
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        If cmd.ExecuteNonQuery() Then
                            RJMessageBox.Show("Delete Success")
                        End If
                        DGV_Masterfinishgoods_atass(idP)
                        treeView_show()
                    Catch ex As Exception
                        RJMessageBox.Show("Error Master Material Usage Finish Goods - 2 => " & ex.Message)
                    End Try
                End If
            Else
                RJMessageBox.Show("Your Access cannot execute this action")
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
        Dim queryMasterFinishGoods As String = "select ID [#],FG_PART_NUMBER [FG Part Number],DESCRIPTION [Desc],FAMILY [Family],COMPONENT [Comp],USAGE [Usage], datetime_insert [Date Time], by_who [Created By] from MATERIAL_USAGE_FINISH_GOODS where fg_part_number='" & id & "' order by COMPONENT"
        Dim dtMasterMaterial As DataTable = Database.GetData(queryMasterFinishGoods)

        dgv_masterfinishgoods_atas.DataSource = dtMasterMaterial

        Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
        check.Name = "check"
        check.HeaderText = "Check For Delete"
        check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgv_masterfinishgoods_atas.Columns.Insert(0, check)

        dgv_masterfinishgoods_atas.Columns(0).Width = 100
        dgv_masterfinishgoods_atas.Columns(1).Width = 100
        dgv_masterfinishgoods_atas.Columns(2).Width = 200
        dgv_masterfinishgoods_atas.Columns(3).Width = 300
        dgv_masterfinishgoods_atas.Columns(4).Width = 150
        dgv_masterfinishgoods_atas.Columns(5).Width = 200
        dgv_masterfinishgoods_atas.Columns(6).Width = 100
        dgv_masterfinishgoods_atas.Columns(7).Width = 200

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        dgv_masterfinishgoods_atas.Columns.Insert(9, delete)

        For i As Integer = 0 To dgv_masterfinishgoods_atas.RowCount - 1
            If dgv_masterfinishgoods_atas.Rows(i).Index Mod 2 = 0 Then
                dgv_masterfinishgoods_atas.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_masterfinishgoods_atas.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        If globVar.delete > 0 Then
            Dim hapus As Integer = 0
            Dim result = RJMessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

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
            RJMessageBox.Show("Delete Success " & hapus & " Data.")
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_masterfinishgoods_search.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            If RadioButton1.Checked Then
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
                    RJMessageBox.Show("Data not Found")
                End If
            End If

            If RadioButton2.Checked Then
                Dim Found As Boolean = False
                Dim StringToSearch As String = ""
                Dim ValueToSearchFor As String = Me.txt_masterfinishgoods_search.Text.Trim.ToLower
                Dim CurrentRowIndex As Integer = 0
                Try
                    If dgv_masterfinishgoods_atas.Rows.Count = 0 Then
                        CurrentRowIndex = 0
                        RJMessageBox.Show("Data not Found")
                        txt_masterfinishgoods_search.Clear()
                    Else
                        CurrentRowIndex = dgv_masterfinishgoods_atas.CurrentRow.Index + 1
                    End If

                    If CurrentRowIndex > dgv_masterfinishgoods_atas.Rows.Count Then
                        CurrentRowIndex = dgv_masterfinishgoods_atas.Rows.Count - 1
                    End If
                    If dgv_masterfinishgoods_atas.Rows.Count > 0 Then
                        For Each gRow As DataGridViewRow In dgv_masterfinishgoods_atas.Rows
                            StringToSearch = gRow.Cells("Comp").Value.ToString.Trim.ToLower
                            If InStr(1, StringToSearch, LCase(Trim(txt_masterfinishgoods_search.Text)), vbTextCompare) = 1 Then
                                Dim myCurrentCell As DataGridViewCell = gRow.Cells("Comp")
                                Dim myCurrentPosition As DataGridViewCell = gRow.Cells(0)
                                dgv_masterfinishgoods_atas.CurrentCell = myCurrentCell
                                CurrentRowIndex = dgv_masterfinishgoods_atas.CurrentRow.Index
                                Found = True
                                txt_masterfinishgoods_search.Clear()
                                Exit For
                            End If
                        Next
                        If Found = False Then
                            RJMessageBox.Show("Data not Found")
                        End If
                    End If
                Catch ex As Exception
                    RJMessageBox.Show("Error Master Material Usage Finish Goods - 3 => " & ex.Message)
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

        With dgv_masterfinishgoods_atas
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
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

                        RJMessageBox.Show("Import Material Usage Finish Goods Success")
                    Catch ex As Exception
                        RJMessageBox.Show("Error Master Material Usage Finish Goods - 4 => " & ex.Message)
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
            RJMessageBox.Show("Export Template Success !")
        End If
    End Sub

    Private Sub btn_export_Master_Usage_Finish_Goods_Click(sender As Object, e As EventArgs) Handles btn_export_Master_Usage_Finish_Goods.Click
        If globVar.view > 0 Then

            ExportToExcel()
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

    Private Sub cb_masterfinishgoods_pn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_masterfinishgoods_pn.SelectedIndexChanged
        If cb_masterfinishgoods_pn.Items.Count > 0 Then
            If cb_masterfinishgoods_pn.Text <> "System.Data.DataRowView" And cb_masterfinishgoods_pn.Text <> "" Then
                Dim queryMasterFinishGoods As String = "select * from MASTER_FINISH_GOODS where fg_part_number='" & cb_masterfinishgoods_pn.Text & "'"
                Dim dtMasterFG As DataTable = Database.GetData(queryMasterFinishGoods)
                txt_masterfinishgoods_family.Text = Trim(dtMasterFG.Rows(0).Item("family"))
                tampilDataComboBoxMaterial()
                If cb_masterfinishgoods_component.Items.Count > 0 Then
                    cb_masterfinishgoods_component.SelectedIndex = -1
                End If
            Else
                txt_masterfinishgoods_family.Text = ""
                txt_masterfinishgoods_desc.Text = ""
            End If
        End If
    End Sub

    Private Sub cb_masterfinishgoods_component_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_masterfinishgoods_component.SelectedIndexChanged
        If cb_masterfinishgoods_component.Items.Count > 0 Then
            If cb_masterfinishgoods_component.Text <> "System.Data.DataRowView" And cb_masterfinishgoods_component.Text <> "" Then
                Dim queryMasterFinishGoods As String = "select * from master_material where part_number='" & cb_masterfinishgoods_component.Text & "'"
                Dim dtMasterFG As DataTable = Database.GetData(queryMasterFinishGoods)
                txt_masterfinishgoods_desc.Text = Trim(dtMasterFG.Rows(0).Item("name"))
            Else
                txt_masterfinishgoods_desc.Text = ""
            End If
        End If
    End Sub

    Private Sub txt_masterfinishgoods_usage_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_masterfinishgoods_usage.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

End Class