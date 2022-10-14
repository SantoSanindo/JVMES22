Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class MasterMaterial

    Dim oleCon As OleDbConnection
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call Database.koneksi_database()
        If txt_mastermaterial_pn.Text <> "" And txt_mastermaterial_qty.Text <> "" And txt_pn_name.Text <> "" Then
            If IsNumeric(txt_mastermaterial_pn.Text) And IsNumeric(txt_mastermaterial_qty.Text) Then
                Dim querycheck As String = "select * from MASTER_MATERIAL where part_number='" & txt_mastermaterial_pn.Text & "'"
                Dim dtCheck As DataTable = Database.GetData(querycheck)
                If dtCheck.Rows.Count > 0 Then
                    MessageBox.Show("Material Exist")
                Else
                    Try
                        Dim sql As String = "INSERT INTO MASTER_MATERIAL VALUES ('" & txt_mastermaterial_pn.Text & "','" & txt_pn_name.Text & "'," & txt_mastermaterial_qty.Text & ",'" & txt_mastermaterial_family.Text & "')"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)

                        If cmd.ExecuteNonQuery() Then
                            txt_pn_name.Text = ""
                            txt_mastermaterial_pn.Text = ""
                            txt_mastermaterial_qty.Text = ""
                            txt_mastermaterial_pn.Select()

                            dgv_material.DataSource = Nothing
                            DGV_MasterMaterial()
                        End If

                    Catch ex As Exception
                        MessageBox.Show("Error Insert" & ex.Message)
                    End Try
                End If
            Else
                MessageBox.Show("Part Number / Qty must be number.")
                txt_mastermaterial_pn.Text = ""
                txt_mastermaterial_qty.Text = ""
                txt_mastermaterial_pn.Select()
            End If
        End If
    End Sub

    Private Sub MasterMaterial_Load(sender As Object, e As EventArgs) Handles Me.Load
        txt_mastermaterial_pn.Select()
        DGV_MasterMaterial()
        txt_mastermaterial_search.Text = ""
    End Sub

    Private Sub DGV_MasterMaterial()
        dgv_material.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_material.Rows.Clear()
        dgv_material.Columns.Clear()
        Call Database.koneksi_database()
        Dim dtMasterMaterial As DataTable = Database.GetData("select PART_NUMBER,NAME, STANDARD_QTY, FAMILY from MASTER_MATERIAL")

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
        dgv_material.Columns.Insert(5, delete)
    End Sub

    Private Sub dgv_material_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_material.CellClick
        'MessageBox.Show(e.ColumnIndex)

        If dgv_material.Columns(e.ColumnIndex).Name = "delete" Then
            Dim result = MessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from master_material where part_number='" & dgv_material.Rows(e.RowIndex).Cells(2).Value & "'"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()
                    dgv_material.DataSource = Nothing
                    DGV_MasterMaterial()
                    MessageBox.Show("Delete Success.")
                Catch ex As Exception
                    MessageBox.Show("failed" & ex.Message)
                End Try
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
        Dim hapus As Integer = 0
        Dim result = MessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then
            For Each row As DataGridViewRow In dgv_material.Rows
                If row.Cells(0).Value = True Then
                    Dim sql As String = "delete from master_material where part_number='" & row.Cells(2).Value & "'"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()
                    hapus = hapus + 1
                End If
            Next
        End If

        dgv_material.DataSource = Nothing
        DGV_MasterMaterial()
        MessageBox.Show("Delete Success " & hapus & " Data.")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
            Dim xlApp As New Microsoft.Office.Interop.Excel.Application
            Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook = xlApp.Workbooks.Open(OpenFileDialog1.FileName)
            Dim SheetName As String = xlWorkBook.Worksheets(1).Name.ToString
            Dim excelpath As String = OpenFileDialog1.FileName
            Dim koneksiExcel As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & excelpath & ";Extended Properties='Excel 8.0;HDR=No;IMEX=1;'"
            oleCon = New OleDbConnection(koneksiExcel)
            oleCon.Open()

            Dim queryExcel As String = "select * from [" & SheetName & "$]"
            Dim cmd As OleDbCommand = New OleDbCommand(queryExcel, oleCon)
            Dim rd As OleDbDataReader

            Call Database.koneksi_database()
            Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(Database.koneksi)
                bulkCopy.DestinationTableName = "dbo.MASTER_MATERIAL"
                Try
                    rd = cmd.ExecuteReader
                    bulkCopy.WriteToServer(rd)
                    rd.Close()

                    dgv_material.DataSource = Nothing
                    DGV_MasterMaterial()
                    MsgBox("Import Material Success")
                Catch ex As Exception
                    MsgBox("Import Material Failed " & ex.Message)
                End Try
            End Using
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
                        StringToSearch = gRow.Cells(2).Value.ToString.Trim.ToLower
                        If InStr(1, StringToSearch, LCase(Trim(txt_mastermaterial_search.Text)), vbTextCompare) = 1 Then
                            Dim myCurrentCell As DataGridViewCell = gRow.Cells(2)
                            Dim myCurrentPosition As DataGridViewCell = gRow.Cells(0)
                            dgv_material.CurrentCell = myCurrentCell
                            CurrentRowIndex = dgv_material.CurrentRow.Index
                            Found = True
                        End If
                        If Found Then Exit For
                    Next
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
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
    End Sub
End Class