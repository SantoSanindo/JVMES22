Imports System.Data.OleDb
Imports System.Data.SqlClient

Imports Excel = Microsoft.Office.Interop.Excel

Public Class MasterMaterial

    Dim oleCon As OleDbConnection
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call Database.koneksi_database()
        If txt_mastermaterial_pn.Text <> "" And txt_mastermaterial_qty.Text <> "" And txt_pn_name.Text <> "" Then
            If IsNumeric(txt_mastermaterial_pn.Text) And IsNumeric(txt_mastermaterial_qty.Text) Then
                Dim querycheck As String = "select * from MASTER_MATERIAL where part_number=" & txt_mastermaterial_pn.Text
                Dim dtCheck As DataTable = Database.GetData(querycheck)
                If dtCheck.Rows.Count > 0 Then
                    MessageBox.Show("Material Exist")
                Else
                    Try
                        Dim sql As String = "INSERT INTO MASTER_MATERIAL VALUES (" & txt_mastermaterial_pn.Text & ",'" & txt_pn_name.Text & "'," & txt_mastermaterial_qty.Text & ")"
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
    End Sub

    Private Sub DGV_MasterMaterial()
        dgv_material.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_material.Rows.Clear()
        dgv_material.Columns.Clear()
        Call Database.koneksi_database()
        Dim dtMasterMaterial As DataTable = Database.GetData("select part_number as Part_Number,name as PN_Name, standard_qty as Qty from MASTER_MATERIAL")

        dgv_material.DataSource = dtMasterMaterial

        Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
        check.Name = "check"
        check.HeaderText = "Check"
        check.Width = 100
        check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgv_material.Columns.Insert(0, check)

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        dgv_material.Columns.Insert(4, delete)

        For i As Integer = 0 To dgv_material.RowCount - 1
            If dgv_material.Rows(i).Index Mod 2 = 0 Then
                dgv_material.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_material.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub dgv_material_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_material.CellClick
        If e.ColumnIndex = 4 Then
            Dim result = MessageBox.Show("Yakin, Hapus Data ini?", "Warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from master_material where part_number=" & dgv_material.Rows(e.RowIndex).Cells(1).Value
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

        If e.ColumnIndex = 0 Then
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
                    Dim sql As String = "delete from master_material where part_number=" & row.Cells(1).Value
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
            Dim str As String = txt_mastermaterial_search.Text
            Try
                For i As Integer = 0 To dgv_material.Rows.Count - 1
                    For j As Integer = 0 To dgv_material.Columns.Count - 1
                        If dgv_material.Rows(i).Cells(j).Value = str Then
                            dgv_material.Rows(i).Selected = True
                            dgv_material.CurrentCell = dgv_material.Rows(i).Cells(j)
                            Exit Sub
                        End If
                    Next
                Next i
            Catch abc As Exception
            End Try
            MsgBox("Data not found!")
        End If
    End Sub
End Class