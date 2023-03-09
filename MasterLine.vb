Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class MasterLine
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text <> "" And ComboBox1.Text <> "" Then
            Dim splitLine() As String = TextBox1.Text.Split(" ")

            If splitLine(0) <> "Line" Then
                MsgBox("Wrong Format")
                Exit Sub
            End If

            If IsNumeric(splitLine(1)) Then
                If splitLine(1) = 0 Then
                    MsgBox("Wrong Format")
                    Exit Sub
                End If
            Else
                MsgBox("Wrong Format")
                Exit Sub
            End If

            Dim sqlInsertMasterLine As String = "INSERT INTO master_line (name, department) VALUES ('" & TextBox1.Text & "','" & ComboBox1.Text & "')"
            Dim cmdInsertMasterLine = New SqlCommand(sqlInsertMasterLine, Database.koneksi)
            If cmdInsertMasterLine.ExecuteNonQuery() Then
                TextBox1.Text = ""
                ComboBox1.SelectedIndex = -1
                DGV_MasterLine()
            End If
        Else
            RJMessageBox.Show("Line Name or Department cannot be blank")
        End If
    End Sub

    Private Sub MasterLine_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call Database.koneksi_database()
        DGV_MasterLine()
        tampilDataComboBoxDepartement()
        ComboBox1.SelectedIndex = -1
    End Sub

    Sub tampilDataComboBoxDepartement()
        Call Database.koneksi_database()
        Dim dtMasterDepart As DataTable = Database.GetData("select * from department")

        ComboBox1.DataSource = dtMasterDepart
        ComboBox1.DisplayMember = "department"
        ComboBox1.ValueMember = "department"
        'ComboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        'ComboBox1.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Sub DGV_MasterLine()
        Call Database.koneksi_database()
        Dim sql As String = "select * from master_line"
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
        DataGridView1.Columns.Insert(4, delete)
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.Columns(e.ColumnIndex).Name = "delete" Then
            Dim result = RJMessageBox.Show("Are you sure to delete?", "Warning", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from master_line where id=" & DataGridView1.Rows(e.RowIndex).Cells("ID").Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    If cmd.ExecuteNonQuery() Then
                        DGV_MasterLine()
                    End If
                Catch ex As Exception
                    RJMessageBox.Show("MasterLine-01 : " & ex.Message)
                End Try
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