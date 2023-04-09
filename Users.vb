Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Users
    Public Shared menu As String = "Master Users"

    Dim oleCon As OleDbConnection
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sRole As New System.Text.StringBuilder
        If TextBox1.Text <> "" And TextBox2.Text <> "" Then

            For i = 0 To CheckedListBox1.Items.Count - 1
                If (CheckedListBox1.GetItemChecked(i)) = True Then
                    sRole.Append(CheckedListBox1.Items(i).ToString.Trim)
                    sRole.Append(";")
                End If
            Next

            Dim querycheck As String = "select * from users where id_card_no='" & TextBox1.Text & "'"
            Dim dtCheck As DataTable = Database.GetData(querycheck)
            If dtCheck.Rows.Count > 0 Then
                If globVar.update > 0 Then
                    Try
                        Dim query As String
                        If TextBox4.Text <> "" Then
                            query = "update users set role='" & sRole.ToString & "',department='" & ComboBox1.Text & "',username='" & TextBox3.Text & "',password='" & TextBox4.Text & "' where id_card_no = '" & TextBox1.Text & "'"
                        Else
                            query = "update users set role='" & sRole.ToString & "',department='" & ComboBox1.Text & "',username='" & TextBox3.Text & "' where id_card_no = '" & TextBox1.Text & "'"
                        End If
                        Dim dtUpdate = New SqlCommand(query, Database.koneksi)
                        If dtUpdate.ExecuteNonQuery() Then
                            DGV_Users()

                            TextBox1.ReadOnly = False
                            TextBox2.ReadOnly = False

                            For c1 As Integer = 0 To CheckedListBox1.Items.Count - 1
                                CheckedListBox1.SetItemChecked(c1, False)
                            Next

                            ComboBox1.SelectedIndex = -1
                        End If
                    Catch ex As Exception
                        RJMessageBox.Show("Error Master Users - 1 =>" & ex.Message)
                    End Try
                Else
                    RJMessageBox.Show("Your Access cannot execute this action")
                End If
            Else
                If globVar.add > 0 Then
                    Try
                        Dim sql As String = "INSERT INTO users (id_card_no,name,username,password,department,role) VALUES ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & ComboBox1.Text & "','" & sRole.ToString & "')"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)

                        If cmd.ExecuteNonQuery() Then
                            TextBox1.Text = ""
                            TextBox2.Text = ""
                            TextBox3.Text = ""
                            TextBox4.Text = ""
                            TextBox1.Select()

                            DGV_Users()

                            TextBox1.ReadOnly = False
                            TextBox2.ReadOnly = False

                            For c1 As Integer = 0 To CheckedListBox1.Items.Count - 1
                                CheckedListBox1.SetItemChecked(c1, False)
                            Next

                            ComboBox1.SelectedIndex = -1
                        End If
                    Catch ex As Exception
                        RJMessageBox.Show("Error Master Users - 2 =>" & ex.Message)
                    End Try
                Else
                    RJMessageBox.Show("Your Access cannot execute this action")
                End If
            End If
        End If
    End Sub

    Private Sub Users_Load(sender As Object, e As EventArgs) Handles Me.Load
        If globVar.view > 0 Then
            DGV_Users()

            loadAccessControl()
            tampilDataComboBoxDepartement()

            ComboBox1.SelectedIndex = -1

            For c1 As Integer = 0 To CheckedListBox1.Items.Count - 1
                CheckedListBox1.SetItemChecked(c1, False)
            Next
        End If
    End Sub

    Sub tampilDataComboBoxDepartement()
        Call Database.koneksi_database()
        Dim dtMasterDepart As DataTable = Database.GetData("select * from department")

        ComboBox1.DataSource = dtMasterDepart
        ComboBox1.DisplayMember = "department"
        ComboBox1.ValueMember = "department"
    End Sub

    Sub loadAccessControl()
        CheckedListBox1.Items.Clear()
        Dim query As String = "select name from master_access order by name"
        Dim dt As DataTable = Database.GetData(query)
        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                CheckedListBox1.Items.Add(dt.Rows(i).Item("name").ToString)
            Next
        End If
    End Sub

    Sub DGV_Users()
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Dim dtMasterUsers As DataTable = Database.GetData("select id_card_no [ID Card],name Name,username [Username], role Role, department Department from USERS order by name")

        DataGridView1.DataSource = dtMasterUsers

        Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
        check.Name = "check"
        check.HeaderText = "Check"
        check.Width = 100
        check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        DataGridView1.Columns.Insert(0, check)

        Dim edit As DataGridViewButtonColumn = New DataGridViewButtonColumn
        edit.Name = "edit"
        edit.HeaderText = "Edit"
        edit.Width = 100
        edit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        edit.Text = "Edit"
        edit.UseColumnTextForButtonValue = True
        DataGridView1.Columns.Insert(6, edit)

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        DataGridView1.Columns.Insert(7, delete)
    End Sub

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
                    RJMessageBox.Show("Cannot Search Users couse Users is blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                RJMessageBox.Show("Error Master Users - 3 =>" & ex.Message)
            End Try
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If globVar.delete > 0 Then
            Dim hapus As Integer = 0
            Dim result = RJMessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

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
            RJMessageBox.Show("Delete Success " & hapus & " Data.")
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete
        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With DataGridView1
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
                        RJMessageBox.Show("Import Users Success")
                    Catch ex As Exception
                        RJMessageBox.Show("Error Master Users - 4 =>" & ex.Message)
                    End Try
                End Using
            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.Columns(e.ColumnIndex).Name = "delete" Then
            If globVar.delete > 0 Then
                Dim result = RJMessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then
                    Try
                        Dim sql As String = "delete from users where id_card_no='" & DataGridView1.Rows(e.RowIndex).Cells("ID Card").Value & "'"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        cmd.ExecuteNonQuery()

                        DGV_Users()
                    Catch ex As Exception
                        RJMessageBox.Show("Error Master Users - 5 =>" & ex.Message)
                    End Try
                End If
            Else
                RJMessageBox.Show("Your Access cannot execute this action")
            End If
        End If

        If DataGridView1.Columns(e.ColumnIndex).Name = "edit" Then
            If globVar.update > 0 Then
                For c1 As Integer = 0 To CheckedListBox1.Items.Count - 1
                    CheckedListBox1.SetItemChecked(c1, False)
                Next

                TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells("ID Card").Value
                TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells("Name").Value

                For r1 = 0 To CheckedListBox1.Items.Count - 1
                    Dim resultchildSplit() As String = DataGridView1.Rows(e.RowIndex).Cells("Role").Value.ToString.Split(";")

                    If resultchildSplit.Length > 0 Then
                        For l1 = 0 To resultchildSplit.Length - 1
                            If resultchildSplit(l1) = CheckedListBox1.Items(r1).ToString Then
                                CheckedListBox1.SetItemChecked(r1, True)
                            End If
                        Next
                    End If
                Next

                If IsDBNull(DataGridView1.Rows(e.RowIndex).Cells("Username").Value) Then
                    TextBox3.Text = ""
                Else
                    TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells("Username").Value
                End If

                If IsDBNull(DataGridView1.Rows(e.RowIndex).Cells("Department").Value) Then
                    ComboBox1.SelectedIndex = -1
                Else
                    ComboBox1.SelectedValue = DataGridView1.Rows(e.RowIndex).Cells("Department").Value
                End If

                TextBox1.ReadOnly = True
                TextBox2.ReadOnly = True
            Else
                RJMessageBox.Show("Your Access cannot execute this action")
            End If
        End If

        If e.ColumnIndex = 0 Then
            If DataGridView1.Rows(e.RowIndex).Cells(0).Value = True Then
                DataGridView1.Rows(e.RowIndex).Cells(0).Value = False
            Else
                DataGridView1.Rows(e.RowIndex).Cells(0).Value = True
            End If
        End If
    End Sub
End Class