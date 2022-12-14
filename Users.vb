Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class Users
    Dim oleCon As OleDbConnection
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text <> "" And TextBox2.Text <> "" Then
            Dim querycheck As String = "select * from users where id_card_no='" & TextBox1.Text & "'"
            Dim dtCheck As DataTable = Database.GetData(querycheck)
            If dtCheck.Rows.Count > 0 Then
                MessageBox.Show("Users Exist")
            Else
                Try
                    Dim sql As String = "INSERT INTO users (id_card_no,name,username,password,departement,role) VALUES ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & ComboBox2.Text & "','" & ComboBox1.Text & "')"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)

                    If cmd.ExecuteNonQuery() Then
                        TextBox1.Text = ""
                        TextBox2.Text = ""
                        TextBox3.Text = ""
                        TextBox4.Text = ""
                        TextBox1.Select()

                        DGV_Users()
                    End If

                Catch ex As Exception
                    MessageBox.Show("Error Insert" & ex.Message)
                End Try
            End If
        End If
    End Sub

    Sub tampilDataComboBoxDepartement()
        Call Database.koneksi_database()
        Dim dtMasterDepart As DataTable = Database.GetData("select * from departement")

        ComboBox2.DataSource = dtMasterDepart
        ComboBox2.DisplayMember = "departement"
        ComboBox2.ValueMember = "departement"
        ComboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ComboBox2.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub Users_Load(sender As Object, e As EventArgs) Handles Me.Load
        DGV_Users()
        tampilDataComboBoxDepartement()
    End Sub

    Sub DGV_Users()
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Dim dtMasterUsers As DataTable = Database.GetData("select id_card_no [ID Card],name Name,role Role, departement Department from USERS order by name")

        DataGridView1.DataSource = dtMasterUsers

        Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
        check.Name = "check"
        check.HeaderText = "Check"
        check.Width = 100
        check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        DataGridView1.Columns.Insert(0, check)

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        DataGridView1.Columns.Insert(5, delete)
    End Sub

    'Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
    '    If DataGridView1.Columns(e.ColumnIndex).Name = "delete" Then
    '        Dim result = MessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

    '        If result = DialogResult.Yes Then
    '            Try
    '                Dim sql As String = "delete from users where id='" & DataGridView1.Rows(e.RowIndex).Cells("ID").Value & "'"
    '                Dim cmd = New SqlCommand(sql, Database.koneksi)
    '                cmd.ExecuteNonQuery()

    '                DGV_Users()
    '                MessageBox.Show("Delete Success.")
    '            Catch ex As Exception
    '                MessageBox.Show("failed" & ex.Message)
    '            End Try
    '        End If
    '    End If

    '    If e.ColumnIndex = 0 Then
    '        'MessageBox.Show(e.RowIndex)
    '        If DataGridView1.Rows(e.RowIndex).Cells(0).Value = True Then
    '            DataGridView1.Rows(e.RowIndex).Cells(0).Value = False
    '        Else
    '            DataGridView1.Rows(e.RowIndex).Cells(0).Value = True
    '        End If
    '    End If
    'End Sub

    Private Sub TextBox5_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox5.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            Dim Found As Boolean = False
            Dim StringToSearch As String = ""
            Dim ValueToSearchFor As String = Me.TextBox5.Text.Trim.ToLower
            Dim CurrentRowIndex As Integer = 0
            Try
                If DataGridView1.Rows.Count > 0 Then
                    For Each gRow As DataGridViewRow In DataGridView1.Rows
                        StringToSearch = gRow.Cells("NAME").Value.ToString.Trim.ToLower
                        If InStr(1, StringToSearch, LCase(Trim(TextBox5.Text)), vbTextCompare) = 1 Then
                            Dim myCurrentCell As DataGridViewCell = gRow.Cells("NAME")
                            Dim myCurrentPosition As DataGridViewCell = gRow.Cells("NAME")
                            DataGridView1.CurrentCell = myCurrentCell
                            CurrentRowIndex = DataGridView1.CurrentRow.Index
                            Found = True
                            TextBox5.Clear()
                        End If
                        If Found Then Exit For
                    Next
                Else
                    MessageBox.Show("Cannot Search Users couse Users is blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim hapus As Integer = 0
        Dim result = MessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells(0).Value = True Then
                    Dim sql As String = "delete from users where id='" & row.Cells("ID").Value & "'"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()
                    hapus = hapus + 1
                End If
            Next
        End If

        DataGridView1.DataSource = Nothing
        DGV_Users()
        MessageBox.Show("Delete Success " & hapus & " Data.")
    End Sub

    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete
        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
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
                bulkCopy.DestinationTableName = "dbo.USERS"
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

                    DGV_Users()
                    MsgBox("Import Users Success")
                Catch ex As Exception
                    MsgBox("Import Users Failed " & ex.Message)
                End Try
            End Using
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.Columns(e.ColumnIndex).Name = "delete" Then
            Dim result = MessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from users where id='" & DataGridView1.Rows(e.RowIndex).Cells("ID").Value & "'"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()

                    DGV_Users()
                    MessageBox.Show("Delete Success.")
                Catch ex As Exception
                    MessageBox.Show("failed" & ex.Message)
                End Try
            End If
        End If

        If e.ColumnIndex = 0 Then
            'MessageBox.Show(e.RowIndex)
            If DataGridView1.Rows(e.RowIndex).Cells(0).Value = True Then
                DataGridView1.Rows(e.RowIndex).Cells(0).Value = False
            Else
                DataGridView1.Rows(e.RowIndex).Cells(0).Value = True
            End If
        End If
    End Sub
End Class