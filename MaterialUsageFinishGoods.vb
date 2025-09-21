Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.IO

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

                Dim queryCek As String = "SELECT * FROM dbo.main_po where fg_pn='" & cb_masterfinishgoods_pn.Text & "' and status='Open'"
                Dim dsexist = New DataSet
                Dim adapterexist = New SqlDataAdapter(queryCek, Database.koneksi)
                adapterexist.Fill(dsexist)

                If dsexist.Tables(0).Rows.Count > 0 Then

                    RJMessageBox.Show("Cannot Add. Because this FG still have Open PO")
                    Exit Sub

                End If

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

                    Dim queryUpdatemfg As String = "update MASTER_FINISH_GOODS set STATUS_CHANGE=1 where FG_PART_NUMBER=" & cb_masterfinishgoods_pn.Text
                    Dim dtUpdatemfg = New SqlCommand(queryUpdatemfg, Database.koneksi)
                    dtUpdatemfg.ExecuteNonQuery()

                    Dim queryUpdatempf As String = "update master_process_flow set material_usage=null where master_finish_goods_pn=" & cb_masterfinishgoods_pn.Text
                    Dim dtUpdatempf = New SqlCommand(queryUpdatempf, Database.koneksi)
                    dtUpdatempf.ExecuteNonQuery()

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
        Dim dtMasterFinishGoods As DataTable = Database.GetData("select fg_part_number from master_finish_goods where department='" & globVar.department & "' order by fg_part_number")

        cb_masterfinishgoods_pn.DataSource = dtMasterFinishGoods
        cb_masterfinishgoods_pn.DisplayMember = "fg_part_number"
        cb_masterfinishgoods_pn.ValueMember = "fg_part_number"
    End Sub

    Sub tampilDataComboBoxMaterial()
        Call Database.koneksi_database()
        Dim dtMasterMaterial As DataTable = Database.GetData("select part_number from master_material where standard_qty>0 and family='" & txt_masterfinishgoods_family.Text & "' and department='" & globVar.department & "' order by part_number")

        cb_masterfinishgoods_component.DataSource = dtMasterMaterial
        cb_masterfinishgoods_component.DisplayMember = "part_number"
        cb_masterfinishgoods_component.ValueMember = "part_number"
    End Sub

    Private Sub treeView_show()
        TreeView1.Nodes.Clear()
        Dim queryFinishGoods As String = "select FG_PART_NUMBER from MASTER_FINISH_GOODS where department='" & globVar.department & "'"
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

                Dim queryCek As String = "SELECT * FROM dbo.main_po mp, dbo.sub_sub_po ssp where mp.fg_pn='" & dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells("FG Part Number").Value & "' and mp.id=ssp.main_po and ssp.status='Open'"
                Dim dsexist = New DataSet
                Dim adapterexist = New SqlDataAdapter(queryCek, Database.koneksi)
                adapterexist.Fill(dsexist)

                If dsexist.Tables(0).Rows.Count > 0 Then
                    RJMessageBox.Show("Cannot delete. Because this FG still have Open PO")
                    Exit Sub
                End If

                Dim result = RJMessageBox.Show("Are you sure delete this data?", "warning", MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then
                    Try
                        Dim sql As String = "delete from MATERIAL_USAGE_FINISH_GOODS where ID=" & dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells(1).Value
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        If cmd.ExecuteNonQuery() Then

                            Dim queryUpdatemfg As String = "update MASTER_FINISH_GOODS set STATUS_CHANGE=1 where FG_PART_NUMBER=" & dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells("FG Part Number").Value
                            Dim dtUpdatemfg = New SqlCommand(queryUpdatemfg, Database.koneksi)
                            dtUpdatemfg.ExecuteNonQuery()

                            Dim queryUpdatempf As String = "update master_process_flow set material_usage=null where master_finish_goods_pn=" & dgv_masterfinishgoods_atas.Rows(e.RowIndex).Cells("FG Part Number").Value
                            Dim dtUpdatempf = New SqlCommand(queryUpdatempf, Database.koneksi)
                            dtUpdatempf.ExecuteNonQuery()

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
        Dim queryMasterFinishGoods As String = "select ID [#],FG_PART_NUMBER [FG Part Number],FAMILY [Family],COMPONENT [Comp],DESCRIPTION [Desc],USAGE [Usage], datetime_insert [Date Time], by_who [Created By] from MATERIAL_USAGE_FINISH_GOODS where fg_part_number='" & id & "' order by COMPONENT"
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

                        Dim queryCek As String = "SELECT * FROM dbo.main_po where fg_pn='" & row.Cells("FG Part Number").Value & "' and status='Open'"
                        Dim dsexist = New DataSet
                        Dim adapterexist = New SqlDataAdapter(queryCek, Database.koneksi)
                        adapterexist.Fill(dsexist)

                        If dsexist.Tables(0).Rows.Count > 0 Then
                            RJMessageBox.Show("Cannot delete. Because this FG still have Open PO")
                            Continue For
                        End If

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

                ' Pre-load validation data for performance
                Dim validFinishGoods As New HashSet(Of String)
                Dim validComponents As New HashSet(Of String)
                Dim validFamilies As New HashSet(Of String)
                Dim fgWithOpenPO As New HashSet(Of String)

                ' Load valid Finish Goods
                Dim fgQuery As String = "SELECT FG_PART_NUMBER FROM dbo.MASTER_FINISH_GOODS WHERE DEPARTMENT = @dept"
                Using fgCmd As New SqlCommand(fgQuery, Database.koneksi)
                    fgCmd.Parameters.AddWithValue("@dept", globVar.department)
                    Using fgReader As SqlDataReader = fgCmd.ExecuteReader()
                        While fgReader.Read()
                            validFinishGoods.Add(fgReader("FG_PART_NUMBER").ToString().Trim())
                        End While
                    End Using
                End Using

                ' Load valid Components (from Master Material)
                Dim compQuery As String = "SELECT PART_NUMBER FROM dbo.MASTER_MATERIAL WHERE DEPARTMENT = @dept"
                Using compCmd As New SqlCommand(compQuery, Database.koneksi)
                    compCmd.Parameters.AddWithValue("@dept", globVar.department)
                    Using compReader As SqlDataReader = compCmd.ExecuteReader()
                        While compReader.Read()
                            validComponents.Add(compReader("PART_NUMBER").ToString().Trim())
                        End While
                    End Using
                End Using

                ' Load valid Families
                Dim famQuery As String = "SELECT FAMILY FROM dbo.FAMILY WHERE DEPARTMENT = @dept"
                Using famCmd As New SqlCommand(famQuery, Database.koneksi)
                    famCmd.Parameters.AddWithValue("@dept", globVar.department)
                    Using famReader As SqlDataReader = famCmd.ExecuteReader()
                        While famReader.Read()
                            validFamilies.Add(famReader("FAMILY").ToString().Trim())
                        End While
                    End Using
                End Using

                ' Load Finish Goods with Open PO (CRITICAL ADDITION)
                Dim openPOQuery As String = "SELECT DISTINCT mp.fg_pn FROM dbo.MAIN_PO mp INNER JOIN dbo.SUB_SUB_PO ssp ON mp.id = ssp.main_po WHERE ssp.status = 'Open'"
                Using openPOCmd As New SqlCommand(openPOQuery, Database.koneksi)
                    Using openPOReader As SqlDataReader = openPOCmd.ExecuteReader()
                        While openPOReader.Read()
                            If openPOReader("fg_pn") IsNot DBNull.Value Then
                                fgWithOpenPO.Add(openPOReader("fg_pn").ToString().Trim())
                            End If
                        End While
                    End Using
                End Using

                Dim duplicateComponents As New List(Of String)
                Dim validationErrors As New List(Of String)
                Dim blockedByPO As New List(Of String)
                Dim totalProcessed As Integer = 0
                Dim totalInserted As Integer = 0
                Dim totalSkipped As Integer = 0

                Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(Database.koneksi)
                    bulkCopy.DestinationTableName = "dbo.MATERIAL_USAGE_FINISH_GOODS"

                    Dim dataTable As New DataTable()
                    dataTable.Columns.Add("FG_PART_NUMBER", GetType(String))
                    dataTable.Columns.Add("FAMILY", GetType(String))
                    dataTable.Columns.Add("COMPONENT", GetType(String))
                    dataTable.Columns.Add("DESCRIPTION", GetType(String))
                    dataTable.Columns.Add("USAGE", GetType(Integer))

                    Try
                        rd = cmd.ExecuteReader()
                        Dim rowNumber As Integer = 1

                        While rd.Read()
                            rowNumber += 1
                            totalProcessed += 1

                            ' Read data from Excel
                            Dim fgPartNumber As String = rd("Finish Goods Part Number *").ToString().Trim()
                            Dim family As String = rd("Family *").ToString().Trim()
                            Dim component As String = rd("Component *").ToString().Trim()
                            Dim description As String = rd("Description *").ToString().Trim()
                            Dim usageStr As String = rd("Usage *").ToString().Trim()

                            ' Validate required fields
                            If String.IsNullOrEmpty(fgPartNumber) Then
                                validationErrors.Add($"Row {rowNumber}: Finish Goods Part Number is required")
                                Continue While
                            End If

                            If String.IsNullOrEmpty(family) Then
                                validationErrors.Add($"Row {rowNumber}: Family is required")
                                Continue While
                            End If

                            If String.IsNullOrEmpty(component) Then
                                validationErrors.Add($"Row {rowNumber}: Component is required")
                                Continue While
                            End If

                            If String.IsNullOrEmpty(description) Then
                                validationErrors.Add($"Row {rowNumber}: Description is required")
                                Continue While
                            End If

                            If String.IsNullOrEmpty(usageStr) OrElse Not IsNumeric(usageStr) Then
                                validationErrors.Add($"Row {rowNumber}: Usage must be a valid number")
                                Continue While
                            End If

                            Dim usageValue As Integer = Convert.ToInt32(usageStr)

                            ' Validate Usage minimum is 1
                            If usageValue < 1 Then
                                validationErrors.Add($"Row {rowNumber}: Usage must be at least 1")
                                Continue While
                            End If

                            ' Validate Finish Goods Part Number exists in Master Finish Goods
                            If Not validFinishGoods.Contains(fgPartNumber) Then
                                validationErrors.Add($"Row {rowNumber}: Finish Goods Part Number '{fgPartNumber}' does not exist in Master Finish Goods table")
                                Continue While
                            End If

                            ' CHECK FOR OPEN PO - CRITICAL VALIDATION
                            If fgWithOpenPO.Contains(fgPartNumber) Then
                                blockedByPO.Add($"{fgPartNumber} (Row {rowNumber})")
                                validationErrors.Add($"Row {rowNumber}: Cannot import component for FG '{fgPartNumber}' because it has Open PO")
                                Continue While
                            End If

                            ' Validate Component exists in Master Material
                            If Not validComponents.Contains(component) Then
                                validationErrors.Add($"Row {rowNumber}: Component '{component}' does not exist in Master Material table")
                                Continue While
                            End If

                            ' Validate Family exists in Family table
                            If Not validFamilies.Contains(family) Then
                                validationErrors.Add($"Row {rowNumber}: Family '{family}' does not exist for department '{globVar.department}'")
                                Continue While
                            End If

                            ' Check for duplicates in Material Usage table
                            Dim checkQuery As String = "SELECT COUNT(*) FROM dbo.MATERIAL_USAGE_FINISH_GOODS WHERE FG_PART_NUMBER = @FG_PART_NUMBER AND COMPONENT = @COMPONENT"
                            Dim exists As Integer
                            Using checkCmd As New SqlCommand(checkQuery, Database.koneksi)
                                checkCmd.Parameters.AddWithValue("@FG_PART_NUMBER", fgPartNumber)
                                checkCmd.Parameters.AddWithValue("@COMPONENT", component)
                                exists = Convert.ToInt32(checkCmd.ExecuteScalar())
                            End Using

                            If exists > 0 Then
                                duplicateComponents.Add($"{component} (FG: {fgPartNumber}, Row {rowNumber})")
                                totalSkipped += 1
                            Else
                                ' Add to DataTable for bulk insert
                                Dim row As DataRow = dataTable.NewRow()
                                row("FG_PART_NUMBER") = fgPartNumber
                                row("FAMILY") = family
                                row("COMPONENT") = component
                                row("DESCRIPTION") = description
                                row("USAGE") = usageValue
                                dataTable.Rows.Add(row)
                                totalInserted += 1
                            End If
                        End While

                        rd.Close()

                        ' Show validation errors if any
                        If validationErrors.Count > 0 Then
                            Dim errorMessage As String = $"Import completed with errors:{Environment.NewLine}{Environment.NewLine}"
                            errorMessage += String.Join(Environment.NewLine, validationErrors.Take(15))

                            If validationErrors.Count > 15 Then
                                errorMessage += Environment.NewLine & $"... and {validationErrors.Count - 15} more errors"
                            End If

                            If blockedByPO.Count > 0 Then
                                errorMessage += Environment.NewLine & Environment.NewLine & "Blocked by Open PO: " & String.Join(", ", blockedByPO.Take(5))
                                If blockedByPO.Count > 5 Then
                                    errorMessage += $" ... and {blockedByPO.Count - 5} more"
                                End If
                            End If

                            errorMessage += Environment.NewLine & Environment.NewLine
                            errorMessage += $"Summary:{Environment.NewLine}"
                            errorMessage += $"Total Processed: {totalProcessed}{Environment.NewLine}"
                            errorMessage += $"Successfully Inserted: {totalInserted}{Environment.NewLine}"
                            errorMessage += $"Skipped (Duplicates): {totalSkipped}{Environment.NewLine}"
                            errorMessage += $"Blocked by Open PO: {blockedByPO.Count}{Environment.NewLine}"
                            errorMessage += $"Errors: {validationErrors.Count}"

                            RJMessageBox.Show(errorMessage, "Import Results", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If

                        ' Bulk insert if there are valid rows
                        If dataTable.Rows.Count > 0 Then
                            bulkCopy.ColumnMappings.Add("FG_PART_NUMBER", "FG_PART_NUMBER")
                            bulkCopy.ColumnMappings.Add("FAMILY", "FAMILY")
                            bulkCopy.ColumnMappings.Add("COMPONENT", "COMPONENT")
                            bulkCopy.ColumnMappings.Add("DESCRIPTION", "DESCRIPTION")
                            bulkCopy.ColumnMappings.Add("USAGE", "USAGE")

                            bulkCopy.WriteToServer(dataTable)
                        End If

                        ' Refresh UI
                        DGV_Masterfinishgoods_atass(cb_masterfinishgoods_pn.Text)
                        treeView_show()

                        ' Show success message
                        If validationErrors.Count = 0 Then
                            Dim successMessage As String = $"Import Material Usage Success!{Environment.NewLine}{Environment.NewLine}"
                            successMessage += $"Total Processed: {totalProcessed}{Environment.NewLine}"
                            successMessage += $"Successfully Inserted: {totalInserted}"

                            If duplicateComponents.Count > 0 Then
                                successMessage += Environment.NewLine & Environment.NewLine & "Duplicates skipped: " & String.Join(", ", duplicateComponents.Take(5))
                                If duplicateComponents.Count > 5 Then
                                    successMessage += $" ... and {duplicateComponents.Count - 5} more"
                                End If
                            End If

                            RJMessageBox.Show(successMessage, "Import Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Catch ex As Exception
                        RJMessageBox.Show("Error Master Material Usage Finish Goods - Import => " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            worksheet.Range("B1").Value = "Family *"
            worksheet.Range("C1").Value = "Component *"
            worksheet.Range("D1").Value = "Description *"
            worksheet.Range("E1").Value = "Usage *"

            ' Set column widths for better usability
            worksheet.Columns("A").ColumnWidth = 30
            worksheet.Columns("B").ColumnWidth = 25
            worksheet.Columns("C").ColumnWidth = 25
            worksheet.Columns("D").ColumnWidth = 40
            worksheet.Columns("E").ColumnWidth = 15

            ' Format header row
            With worksheet.Range("A1:E1")
                .Font.Bold = True
                .Interior.Color = RGB(200, 200, 200)
            End With

            'save the workbook
            FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
                workbook.SaveAs(directoryPath & "\Master Material Usage Finish Goods Template.xlsx")
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

    Private Sub btn_export_Master_Usage_Finish_Goods_Click(sender As Object, e As EventArgs) Handles btn_export_Master_Usage_Finish_Goods.Click
        If globVar.view > 0 Then
            ExportToExcel()
        End If
    End Sub

    Private Sub ExportToExcel()
        ' Check if DataGridView has data
        If dgv_masterfinishgoods_atas.RowCount <= 0 Then
            RJMessageBox.Show("Cannot export with empty table", "Export Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim xlApp As Excel.Application = Nothing
        Dim xlWorkBook As Excel.Workbook = Nothing
        Dim xlWorkSheet As Excel.Worksheet = Nothing
        Dim FG As String = ""

        Try
            ' Excluded columns
            Dim excludedColumns As New List(Of String) From {"Check For Delete", "#", "Date Time", "Created By", "Delete"}

            ' Create Excel application
            xlApp = New Excel.Application()
            xlWorkBook = xlApp.Workbooks.Add()
            xlWorkSheet = xlWorkBook.Worksheets(1)

            ' Format entire worksheet as text to prevent data conversion
            xlWorkSheet.Cells.NumberFormat = "@"

            ' Get FG Part Number for filename
            If dgv_masterfinishgoods_atas.RowCount > 0 Then
                For Each column As DataGridViewColumn In dgv_masterfinishgoods_atas.Columns
                    If column.HeaderText.Contains("FG Part Number") OrElse column.HeaderText.Contains("Part Number") Then
                        Dim cellValue = dgv_masterfinishgoods_atas.Rows(0).Cells(column.Index).Value
                        If cellValue IsNot Nothing Then
                            FG = cellValue.ToString().Trim()
                            Exit For
                        End If
                    End If
                Next
            End If

            ' If FG is still empty, use generic name
            If String.IsNullOrEmpty(FG) Then
                FG = "Material_Usage"
            End If

            ' Define custom headers with asterisks
            Dim customHeaders As New List(Of String) From {
                "Finish Goods Part Number *",
                "Family *",
                "Component *",
                "Description *",
                "Usage *"
            }

            Dim colIndex As Integer = 1

            ' Add custom headers
            For Each header As String In customHeaders
                xlWorkSheet.Cells(1, colIndex).Value = header
                colIndex += 1
            Next

            ' Format header row
            Dim headerRange = xlWorkSheet.Range("A1").Resize(1, colIndex - 1)
            With headerRange
                .Font.Bold = True
                .Interior.Color = RGB(200, 200, 200)
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            End With

            ' Add data - only non-excluded columns
            For i As Integer = 0 To dgv_masterfinishgoods_atas.RowCount - 1
                colIndex = 1
                For Each column As DataGridViewColumn In dgv_masterfinishgoods_atas.Columns
                    If Not excludedColumns.Contains(column.HeaderText) Then
                        Dim cellValue = dgv_masterfinishgoods_atas.Rows(i).Cells(column.Index).Value
                        xlWorkSheet.Cells(i + 2, colIndex).Value = If(cellValue IsNot Nothing, cellValue.ToString(), "")
                        colIndex += 1
                    End If
                Next
            Next

            ' Auto-fit columns for better visibility
            xlWorkSheet.Columns.AutoFit()

            ' Set optimal column widths
            For col As Integer = 1 To colIndex - 1
                Dim currentWidth As Double = xlWorkSheet.Columns(col).ColumnWidth
                If currentWidth > 50 Then
                    xlWorkSheet.Columns(col).ColumnWidth = 50 ' Max width
                ElseIf currentWidth < 10 Then
                    xlWorkSheet.Columns(col).ColumnWidth = 15 ' Min width
                End If
            Next

            ' Dialog for selecting save location
            FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

            If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
                ' Sanitize filename - remove invalid characters
                Dim sanitizedFG As String = String.Join("_", FG.Split(System.IO.Path.GetInvalidFileNameChars()))
                Dim fileName As String = $"Export {sanitizedFG} Material Usage.xlsx"
                Dim filePath As String = System.IO.Path.Combine(directoryPath, fileName)

                ' Delete existing file if it exists
                If System.IO.File.Exists(filePath) Then
                    System.IO.File.Delete(filePath)
                End If

                ' Save the workbook
                xlWorkBook.SaveAs(filePath)
                RJMessageBox.Show($"Export to Excel Success!{Environment.NewLine}File saved to: {filePath}", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            RJMessageBox.Show($"Error exporting to Excel: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Cleanup Excel objects properly
            Try
                If xlWorkBook IsNot Nothing Then
                    xlWorkBook.Close(SaveChanges:=False)
                    Marshal.ReleaseComObject(xlWorkBook)
                End If
                If xlApp IsNot Nothing Then
                    xlApp.Quit()
                    Marshal.ReleaseComObject(xlApp)
                End If
                If xlWorkSheet IsNot Nothing Then
                    Marshal.ReleaseComObject(xlWorkSheet)
                End If
            Catch
                ' Ignore cleanup errors
            End Try
        End Try
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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Cursor = Cursors.WaitCursor
        Dim queryMasterFinishGoods As String = "SELECT FG_PART_NUMBER, FAMILY, COMPONENT, DESCRIPTION, [USAGE] FROM MATERIAL_USAGE_FINISH_GOODS ORDER BY FG_PART_NUMBER ASC, COMPONENT DESC"
        Dim dtMasterMaterial As DataTable = Database.GetData(queryMasterFinishGoods)
        DataGridView1.DataSource = dtMasterMaterial

        If globVar.view > 0 Then
            Button4.Enabled = False
            Application.DoEvents()

            ExportToExcelAllFG()
        End If
    End Sub

    Private Sub ExportToExcelAllFG()
        If globVar.view > 0 Then
            If DataGridView1.Rows.Count = 0 Then
                RJMessageBox.Show("No data to export", "Export Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim xlApp As Excel.Application = Nothing
            Dim xlWorkBook As Excel.Workbook = Nothing
            Dim xlWorkSheet As Excel.Worksheet = Nothing

            Try
                ' Create Excel application
                xlApp = New Excel.Application()
                xlWorkBook = xlApp.Workbooks.Add()
                xlWorkSheet = xlWorkBook.Worksheets(1)

                ' Format entire worksheet as text to prevent data conversion
                xlWorkSheet.Cells.NumberFormat = "@"

                ' Define custom headers with your specific format
                Dim customHeaders As New List(Of String) From {
                "Finish Goods Part Number *",
                "Family *",
                "Component *",
                "Description *",
                "Usage *"
            }

                ' Set custom headers
                For k As Integer = 1 To customHeaders.Count
                    xlWorkSheet.Cells(1, k).Value = customHeaders(k - 1)
                Next

                ' Format header row
                With xlWorkSheet.Range("A1").Resize(1, customHeaders.Count)
                    .Font.Bold = True
                    .Interior.Color = RGB(200, 200, 200)
                    .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                End With

                ' Export data row by row to maintain data types
                For i As Integer = 0 To DataGridView1.Rows.Count - 1
                    For j As Integer = 0 To DataGridView1.Columns.Count - 1
                        Dim cellValue As Object = DataGridView1(j, i).Value
                        If cellValue IsNot Nothing Then
                            xlWorkSheet.Cells(i + 2, j + 1).Value = cellValue.ToString()
                        Else
                            xlWorkSheet.Cells(i + 2, j + 1).Value = ""
                        End If
                    Next
                Next

                ' Auto-fit columns
                xlWorkSheet.Columns.AutoFit()

                ' Set column widths for better visibility
                xlWorkSheet.Columns("A").ColumnWidth = 30 ' Finish Goods Part Number
                xlWorkSheet.Columns("B").ColumnWidth = 15 ' Family
                xlWorkSheet.Columns("C").ColumnWidth = 25 ' Component
                xlWorkSheet.Columns("D").ColumnWidth = 40 ' Description
                xlWorkSheet.Columns("E").ColumnWidth = 12 ' Usage

                ' Select folder for saving
                FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

                If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                    Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
                    Dim fileName As String = "Export All Material Usage.xlsx"
                    Dim filePath As String = System.IO.Path.Combine(directoryPath, fileName)

                    ' Delete existing file if it exists
                    If System.IO.File.Exists(filePath) Then
                        System.IO.File.Delete(filePath)
                    End If

                    ' Save the workbook
                    xlWorkBook.SaveAs(filePath)
                    RJMessageBox.Show($"Export to Excel Success!{Environment.NewLine}File saved to: {filePath}", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Catch ex As Exception
                RJMessageBox.Show($"Error exporting to Excel: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally

                Me.Cursor = Cursors.Default
                Button4.Enabled = True

                ' Cleanup Excel objects
                Try
                    If xlWorkBook IsNot Nothing Then
                        xlWorkBook.Close(SaveChanges:=False)
                        Marshal.ReleaseComObject(xlWorkBook)
                    End If
                    If xlApp IsNot Nothing Then
                        xlApp.Quit()
                        Marshal.ReleaseComObject(xlApp)
                    End If
                    If xlWorkSheet IsNot Nothing Then
                        Marshal.ReleaseComObject(xlWorkSheet)
                    End If
                Catch
                    ' Ignore cleanup errors
                End Try
            End Try
        End If
    End Sub
End Class