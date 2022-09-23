Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class MasterFinishGoods2

    Dim oleCon As OleDbConnection
    Dim v1 As String = ""
    Dim v2 As String = ""
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call Database.koneksi_database()
        If ComboBox1.SelectedValue.ToString <> "" And TextBox1.Text <> "" Then
            If IsNumeric(ComboBox1.SelectedValue) And IsNumeric(TextBox1.Text) Then
                Dim queryFG As String = "select * from master_finish_goods where part_number=" + v1 + " and material_part_number=" + ComboBox1.SelectedValue.ToString
                Dim dsexistFG = New DataSet
                Dim adapterexistFG = New SqlDataAdapter(queryFG, Database.koneksi)
                adapterexistFG.Fill(dsexistFG)
                If dsexistFG.Tables(0).Rows.Count > 0 Then
                    MessageBox.Show("Material exist.")
                Else
                    Try
                        Dim sql As String = "INSERT INTO MASTER_FINISH_GOODS(PART_NUMBER,STANDARD_QTY,MATERIAL_PART_NUMBER,qty_need,master_process) VALUES (" & v1 & "," & v2 & "," & ComboBox1.SelectedValue & "," & TextBox1.Text & ",'" & ComboBox2.SelectedValue & "')"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        cmd.ExecuteNonQuery()

                        ComboBox1.Text = ""
                        TextBox1.Text = ""
                        ComboBox1.Select()

                        dgv_masterfinishgoods2.DataSource = Nothing
                        sub_dgv_masterfinishgoods2(v1, v2)
                    Catch ex As Exception
                        MessageBox.Show("Error Insert" & ex.Message)
                    End Try
                End If
            Else
                MessageBox.Show("Part Number / Qty must be number.")
                ComboBox1.Text = ""
                TextBox1.Text = ""
                ComboBox1.Select()
            End If
        End If
    End Sub

    Private Sub MasterFinishGoods2_Load(sender As Object, e As EventArgs) Handles Me.Load
        tampilDataComboBox()
        tampilDataComboBoxdtMasterProcess()
    End Sub

    Sub tampilDataComboBox()
        Call Database.koneksi_database()
        Dim dtMasterMaterial As DataTable = Database.GetData("select part_number, concat(part_number,' (',stock,')') as display_partnumber from stock")

        ComboBox1.DataSource = dtMasterMaterial
        ComboBox1.DisplayMember = "display_partnumber"
        ComboBox1.ValueMember = "part_number"
        ComboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ComboBox1.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Sub tampilDataComboBoxdtMasterProcess()
        Call Database.koneksi_database()
        Dim dtMasterProcess As DataTable = Database.GetData("select process_name from master_process")

        ComboBox2.DataSource = dtMasterProcess
        ComboBox2.DisplayMember = "process_name"
        ComboBox2.ValueMember = "process_name"
        ComboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ComboBox2.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Public Sub sub_dgv_masterfinishgoods2(partnumber As String, qty As String)
        v1 = partnumber
        v2 = qty
        dgv_masterfinishgoods2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_masterfinishgoods2.Rows.Clear()
        dgv_masterfinishgoods2.Columns.Clear()
        Call Database.koneksi_database()
        Dim query As String = "select M.part_number as Part_Number, M.material_part_number as Material, M.qty_need as Qty_Need,master_process as Process from MASTER_FINISH_GOODS M where M.part_number=" + partnumber + " and M.material_part_number is not null order by m.qty_need desc"
        Dim dtMasterMaterial As DataTable = Database.GetData(query)

        dgv_masterfinishgoods2.DataSource = dtMasterMaterial

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        dgv_masterfinishgoods2.Columns.Insert(4, delete)

        Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
        check.Name = "check"
        check.HeaderText = "Check"
        check.Width = 100
        check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgv_masterfinishgoods2.Columns.Insert(0, check)

        For i As Integer = 0 To dgv_masterfinishgoods2.RowCount - 1
            If dgv_masterfinishgoods2.Rows(i).Index Mod 2 = 0 Then
                dgv_masterfinishgoods2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_masterfinishgoods2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
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
                    dgv_masterfinishgoods2.DataSource = Nothing
                    sub_dgv_masterfinishgoods2(v1, v2)
                    MsgBox("Import Material Berhasil")
                Catch ex As Exception
                    MsgBox("Import Material Gagal " & ex.Message)
                End Try
            End Using
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim hapus As Integer = 0
        Dim result = MessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then
            For Each row As DataGridViewRow In dgv_masterfinishgoods2.Rows
                If row.Cells(0).Value = True Then
                    Dim sql As String = "delete from master_finish_goods where part_number=" & row.Cells(1).Value & " and material_part_number=" & row.Cells(2).Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()
                    hapus = hapus + 1
                End If
            Next
        End If

        dgv_masterfinishgoods2.DataSource = Nothing
        sub_dgv_masterfinishgoods2(v1, v2)
        MessageBox.Show("Delete Success " & hapus & " Data.")
    End Sub

    Private Sub TextBox2_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox2.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            Dim str As String = TextBox2.Text
            Try
                For i As Integer = 0 To dgv_masterfinishgoods2.Rows.Count - 1
                    For j As Integer = 0 To dgv_masterfinishgoods2.Columns.Count - 1
                        If dgv_masterfinishgoods2.Rows(i).Cells(j).Value = str Then
                            dgv_masterfinishgoods2.Rows(i).Selected = True
                            dgv_masterfinishgoods2.CurrentCell = dgv_masterfinishgoods2.Rows(i).Cells(j)
                            Exit Sub
                        End If
                    Next
                Next i
            Catch abc As Exception
            End Try
            MsgBox("Data not found!")
        End If
    End Sub

    Private Sub dgv_masterfinishgoods2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_masterfinishgoods2.CellClick
        If e.ColumnIndex = 5 Then

            Dim result = MessageBox.Show("Are you sure delete this data?", "warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from master_finish_goods where part_number=" & dgv_masterfinishgoods2.Rows(e.RowIndex).Cells(1).Value & " and material_part_number=" & dgv_masterfinishgoods2.Rows(e.RowIndex).Cells(2).Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()

                    dgv_masterfinishgoods2.DataSource = Nothing
                    sub_dgv_masterfinishgoods2(v1, v2)
                    MessageBox.Show("Delete Success.")
                Catch ex As Exception
                    MessageBox.Show("Delete Failed" & ex.Message)
                End Try
            End If
        End If

        If e.ColumnIndex = 0 Then
            If dgv_masterfinishgoods2.Rows(e.RowIndex).Cells(0).Value = True Then
                dgv_masterfinishgoods2.Rows(e.RowIndex).Cells(0).Value = False
            Else
                dgv_masterfinishgoods2.Rows(e.RowIndex).Cells(0).Value = True
            End If
        End If
    End Sub

    Private Sub MasterFinishGoods2_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Dim FGForm = New MasterFinishGoods()

        FGForm.Show()
    End Sub
End Class