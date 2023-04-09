Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class AccessControll
    Public Shared menu As String = "Access Control"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim TotalCheckMenu As Integer = 0
        Dim TotalCheckAccess As Integer = 0
        Dim sMenu As New System.Text.StringBuilder
        Dim sDept As New System.Text.StringBuilder
        Dim view As Integer = 0
        Dim add As Integer = 0
        Dim update As Integer = 0
        Dim delete As Integer = 0
        If txtName.Text <> "" Then
            For iT1 = 0 To CheckedListBox1.Items.Count - 1
                If (CheckedListBox1.GetItemChecked(iT1)) = True Then
                    TotalCheckMenu += 1
                End If
            Next

            If TotalCheckMenu = 0 Then
                RJMessageBox.Show("Sorry. Please select minimal 1 menu. if want to save.")
                Exit Sub
            End If

            For iT2 = 0 To CheckedListBox2.Items.Count - 1
                If (CheckedListBox2.GetItemChecked(iT2)) = True Then
                    TotalCheckAccess += 1
                End If
            Next

            If TotalCheckAccess = 0 Then
                RJMessageBox.Show("Sorry. Please select minimal 1 Action. if want to save.")
                Exit Sub
            End If

            For iT3 = 0 To CheckedListBox3.Items.Count - 1
                If (CheckedListBox3.GetItemChecked(iT3)) = True Then
                    TotalCheckAccess += 1
                End If
            Next

            If TotalCheckAccess = 0 Then
                RJMessageBox.Show("Sorry. Please select minimal 1 Action. if want to save.")
                Exit Sub
            End If

            For i = 0 To CheckedListBox1.Items.Count - 1
                If (CheckedListBox1.GetItemChecked(i)) = True Then
                    sMenu.Append(CheckedListBox1.Items(i).ToString.Trim)
                    sMenu.Append(";")
                End If
            Next

            For i = 0 To CheckedListBox2.Items.Count - 1
                If (CheckedListBox2.GetItemChecked(i)) = True Then
                    If CheckedListBox2.Items(i).ToString = "View" Then
                        view = 1
                    End If
                    If CheckedListBox2.Items(i).ToString = "Add" Then
                        add = 1
                    End If
                    If CheckedListBox2.Items(i).ToString = "Update" Then
                        update = 1
                    End If
                    If CheckedListBox2.Items(i).ToString = "Delete" Then
                        delete = 1
                    End If
                End If
            Next

            For i = 0 To CheckedListBox3.Items.Count - 1
                If (CheckedListBox3.GetItemChecked(i)) = True Then
                    sDept.Append(CheckedListBox3.Items(i).ToString.Trim)
                    sDept.Append(";")
                End If
            Next

            Dim querycheck As String = "select * from master_access where name='" & txtName.Text & "'"
            Dim dtCheck As DataTable = Database.GetData(querycheck)
            If dtCheck.Rows.Count > 0 Then
                If globVar.update > 0 Then
                    Try
                        Dim query As String = "update master_access set menu='" & sMenu.ToString & "',view='" & view & "',add='" & add & "',update='" & update & "',delete='" & delete & "' where name = '" & txtName.Text & "'"
                        Dim dtUpdate = New SqlCommand(query, Database.koneksi)
                        If dtUpdate.ExecuteNonQuery() Then
                            RJMessageBox.Show("Update Access Control Success")
                            txtName.Clear()
                            loadMenu()
                            loadAction()
                            loadDepartment()
                            DGV_Access_Control()
                            txtName.ReadOnly = False
                        End If
                    Catch ex As Exception
                        RJMessageBox.Show("Error Access Control - 1 =>" & ex.Message)
                    End Try
                End If
            Else
                If globVar.add > 0 Then
                    Try
                        Dim insertQuery As String = "INSERT INTO master_access ([name], [menu], [view], [add],[update],[delete],[user_add],[department])
                        VALUES ('" & txtName.Text & "','" & sMenu.ToString & "'," & view & "," & add & "," & update & "," & delete & ",'" & globVar.username & "','" & sDept.ToString & "')"
                        Dim cmdInsertQuery = New SqlCommand(insertQuery, Database.koneksi)
                        If cmdInsertQuery.ExecuteNonQuery() Then
                            RJMessageBox.Show("Add Access Control Success")
                            txtName.Clear()
                            loadMenu()
                            loadAction()
                            loadDepartment()
                            DGV_Access_Control()
                        End If
                    Catch ex As Exception
                        RJMessageBox.Show("Error Access Control - 2 =>" & ex.Message)
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub AccessControll_Load(sender As Object, e As EventArgs) Handles Me.Load
        If globVar.view > 0 Then
            loadMenu()
            loadAction()
            loadDepartment()
            DGV_Access_Control()
        End If
    End Sub

    Sub loadMenu()
        CheckedListBox1.Items.Clear()
        Dim query As String = "select menu from master_menu order by id"
        Dim dtMenu As DataTable = Database.GetData(query)
        If dtMenu.Rows.Count > 0 Then
            For i = 0 To dtMenu.Rows.Count - 1
                CheckedListBox1.Items.Add(dtMenu.Rows(i).Item("MENU").ToString)
            Next
        End If
    End Sub

    Sub loadAction()
        CheckedListBox2.Items.Clear()
        Dim query As String = "select menu from master_menu order by id"
        Dim dtMenu As DataTable = Database.GetData(query)
        If dtMenu.Rows.Count > 0 Then
            CheckedListBox2.Items.Add("View").ToString()
            CheckedListBox2.Items.Add("Add").ToString()
            CheckedListBox2.Items.Add("Update").ToString()
            CheckedListBox2.Items.Add("Delete").ToString()
        End If
    End Sub

    Sub loadDepartment()
        CheckedListBox3.Items.Clear()
        Dim query As String = "select department from department"
        Dim dtDepartment As DataTable = Database.GetData(query)
        If dtDepartment.Rows.Count > 0 Then
            For i = 0 To dtDepartment.Rows.Count - 1
                CheckedListBox3.Items.Add(dtDepartment.Rows(i).Item("department").ToString)
            Next
        End If
    End Sub

    Sub DGV_Access_Control()
        Dim sql As String = "select [id] ID,[name] Name,[menu] Menu, [department] Department, [view] [View],[add] [Add],[update] [Update],[delete] [Delete],[user_add] [User Add] from master_access"
        Dim dtMainPO As DataTable = Database.GetData(sql)
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        DataGridView1.DataSource = dtMainPO

        Dim edit As DataGridViewButtonColumn = New DataGridViewButtonColumn
        edit.Name = "edit"
        edit.HeaderText = "Edit"
        edit.Width = 50
        edit.Text = "Edit"
        edit.UseColumnTextForButtonValue = True
        DataGridView1.Columns.Insert(9, edit)

        Dim delete2 As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete2.Name = "delete2"
        delete2.HeaderText = "Delete"
        delete2.Width = 50
        delete2.Text = "Delete"
        delete2.UseColumnTextForButtonValue = True
        DataGridView1.Columns.Insert(10, delete2)

        DataGridView1.Columns(0).Width = 50
        DataGridView1.Columns(1).Width = 200
        DataGridView1.Columns(2).Width = 500
        DataGridView1.Columns(3).Width = 200
        DataGridView1.Columns(4).Width = 50
        DataGridView1.Columns(5).Width = 50
        DataGridView1.Columns(6).Width = 50
        DataGridView1.Columns(7).Width = 50
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.Columns(e.ColumnIndex).Name = "delete2" Then
            If globVar.delete > 0 Then
                Dim result = RJMessageBox.Show("Are you sure to delete?", "Warning", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Try
                        Dim sql As String = "delete from master_access where id=" & DataGridView1.Rows(e.RowIndex).Cells("ID").Value
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        If cmd.ExecuteNonQuery() Then
                            DGV_Access_Control()
                        End If
                    Catch ex As Exception
                        RJMessageBox.Show("MasterAccess-01 : " & ex.Message)
                    End Try
                End If
            End If
        End If

        If DataGridView1.Columns(e.ColumnIndex).Name = "edit" Then
            If globVar.update > 0 Then
                For c1 As Integer = 0 To CheckedListBox1.Items.Count - 1
                    CheckedListBox1.SetItemChecked(c1, False)
                Next
                For c2 As Integer = 0 To CheckedListBox2.Items.Count - 1
                    CheckedListBox2.SetItemChecked(c2, False)
                Next
                For c3 As Integer = 0 To CheckedListBox3.Items.Count - 1
                    CheckedListBox3.SetItemChecked(c3, False)
                Next

                If globVar.update > 0 Then
                    txtName.Text = DataGridView1.Rows(e.RowIndex).Cells("Name").Value
                    txtName.ReadOnly = True
                    For r1 = 0 To CheckedListBox1.Items.Count - 1
                        Dim resultchildSplit() As String = DataGridView1.Rows(e.RowIndex).Cells("Menu").Value.ToString.Split(";")

                        For l1 = 0 To resultchildSplit.Length - 1
                            If resultchildSplit(l1) = CheckedListBox1.Items(r1).ToString Then
                                CheckedListBox1.SetItemChecked(r1, True)
                            End If
                        Next
                    Next

                    For r2 = 0 To CheckedListBox2.Items.Count - 1
                        If DataGridView1.Rows(e.RowIndex).Cells("View").Value = 1 Then
                            If CheckedListBox2.Items(r2).ToString = "View" Then
                                CheckedListBox2.SetItemChecked(r2, True)
                            End If
                        End If

                        If DataGridView1.Rows(e.RowIndex).Cells("Add").Value = 1 Then
                            If CheckedListBox2.Items(r2).ToString = "Add" Then
                                CheckedListBox2.SetItemChecked(r2, True)
                            End If
                        End If

                        If DataGridView1.Rows(e.RowIndex).Cells("Update").Value = 1 Then
                            If CheckedListBox2.Items(r2).ToString = "Update" Then
                                CheckedListBox2.SetItemChecked(r2, True)
                            End If
                        End If

                        If DataGridView1.Rows(e.RowIndex).Cells("Delete").Value = 1 Then
                            If CheckedListBox2.Items(r2).ToString = "Delete" Then
                                CheckedListBox2.SetItemChecked(r2, True)
                            End If
                        End If
                    Next

                    For r3 = 0 To CheckedListBox3.Items.Count - 1
                        Dim resultchildSplit() As String = DataGridView1.Rows(e.RowIndex).Cells("Department").Value.ToString.Split(";")

                        For l3 = 0 To resultchildSplit.Length - 1
                            If resultchildSplit(l3) = CheckedListBox3.Items(r3).ToString Then
                                CheckedListBox3.SetItemChecked(r3, True)
                            End If
                        Next
                    Next
                End If
            End If
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
End Class