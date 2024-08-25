Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class MasterLine
    Public Shared menu As String = "Master Line"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If globVar.add > 0 Then
            If ComboBox1.Text <> "" And ComboBox2.Text <> "" Then
                Try
                    Dim sqlInsertMasterLine As String = "
                        IF NOT EXISTS (SELECT 1 FROM master_line WHERE name = '" & ComboBox2.Text & "' AND department = '" & ComboBox1.Text & "')
                            BEGIN
                                INSERT INTO master_line (name, department, by_who)
                                VALUES ('" & ComboBox2.Text & "', '" & ComboBox1.Text & "','" & globVar.username & "')
                            END
                        ELSE
                            BEGIN
                                RAISERROR('Data already exists', 16, 1)
                            END"

                    Dim cmdInsertMasterLine = New SqlCommand(sqlInsertMasterLine, Database.koneksi)
                    If cmdInsertMasterLine.ExecuteNonQuery() Then
                        ComboBox1.SelectedIndex = -1
                        ComboBox2.SelectedIndex = -1
                        DGV_MasterLine()
                    End If

                Catch ex As Exception
                    RJMessageBox.Show("Error Master Line - 1 =>" & ex.Message)
                End Try
            Else
                RJMessageBox.Show("Line Name or Department cannot be blank")
            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub MasterLine_Load(sender As Object, e As EventArgs) Handles Me.Load
        If globVar.view > 0 Then
            Call Database.koneksi_database()
            DGV_MasterLine()
            tampilDataComboBoxDepartement()
            tampilDataComboBoxListLine()
            ComboBox1.SelectedIndex = -1
            ComboBox2.SelectedIndex = -1
        End If
    End Sub

    Sub tampilDataComboBoxDepartement()
        Call Database.koneksi_database()
        Dim sql As String
        If globVar.username = "admin" Then
            sql = "select * from department order by department"
        Else
            sql = "select * from department where department='" & globVar.department & "' order by department"
        End If
        Dim dtMasterDepart As DataTable = Database.GetData(sql)

        ComboBox1.DataSource = dtMasterDepart
        ComboBox1.DisplayMember = "department"
        ComboBox1.ValueMember = "department"
    End Sub

    Sub tampilDataComboBoxListLine()
        Call Database.koneksi_database()
        Dim dtMasterDepart As DataTable = Database.GetData("select * from list_line order by line")

        ComboBox2.DataSource = dtMasterDepart
        ComboBox2.DisplayMember = "line"
        ComboBox2.ValueMember = "line"
    End Sub

    Sub DGV_MasterLine()
        Call Database.koneksi_database()
        Dim sql As String = "select ID [#],name [Name Line], department [Department], insert_date [Date Time], by_who [Created By] from master_line where department='" & globVar.department & "' order by name"
        Dim dtMainPO As DataTable = Database.GetData(sql)
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        DataGridView1.DataSource = dtMainPO

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 50
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        DataGridView1.Columns.Insert(5, delete)
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If e.ColumnIndex = -1 Then
            Exit Sub
        End If

        If DataGridView1.Columns(e.ColumnIndex).Name = "delete" Then
            Dim queryCek As String = "SELECT * FROM dbo.main_po mp, dbo.sub_sub_po ssp where mp.department='" & DataGridView1.Rows(e.RowIndex).Cells("Department").Value & "' and ssp.status='Open' and ssp.line='" & DataGridView1.Rows(e.RowIndex).Cells("Name Line").Value & "' and mp.id=ssp.main_po"
            Dim dsexist = New DataSet
            Dim adapterexist = New SqlDataAdapter(queryCek, Database.koneksi)
            adapterexist.Fill(dsexist)

            If dsexist.Tables(0).Rows.Count > 0 Then
                RJMessageBox.Show("Cannot delete. Because this line still used in Main PO / Sub Sub PO")
                Exit Sub
            End If

            'MsgBox(DataGridView1.Rows(e.RowIndex).Cells("NAME").Value)
            If globVar.delete > 0 Then
                Dim result = RJMessageBox.Show("Are you sure to delete?", "Warning", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Try
                        Dim sql As String = "delete from master_line where id=" & DataGridView1.Rows(e.RowIndex).Cells("#").Value
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        If cmd.ExecuteNonQuery() Then
                            RJMessageBox.Show("Delete Success")
                        End If
                        DGV_MasterLine()
                    Catch ex As Exception
                        RJMessageBox.Show("Error Master Line - 2 =>" & ex.Message)
                    End Try
                End If
            Else
                RJMessageBox.Show("Your Access cannot execute this action")
            End If
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