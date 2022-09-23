Imports System.Data.SqlClient

Public Class FormReject
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        MainForm.dgv_doc()
        MainForm.dgv_dop()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim queryInsertDOC As String = "insert into reject (po,line,process,qty) values (" & TextBox1.Text & ",'" & ComboBox2.Text & "','" & ComboBox1.Text & "'," & TextBox2.Text & ")"
        Dim cmdInsertDOC = New SqlCommand(queryInsertDOC, Database.koneksi)
        If cmdInsertDOC.ExecuteNonQuery() Then
            DGV_Reject()
        End If
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
        Call Database.koneksi_database()
        Dim adapter As SqlDataAdapter
        Dim ds As New DataSet
        Dim sql As String

        If e.KeyData = Keys.Enter Then
            sql = "SELECT process FROM production_dop where po=" & TextBox1.Text
            adapter = New SqlDataAdapter(sql, Database.koneksi)
            adapter.Fill(ds)
            If ds.Tables(0).Rows.Count > 0 Then
                TextBox2.Enabled = True
                ComboBox1.Enabled = True
                ComboBox2.Enabled = True
                DGV_Reject()

                ComboBox1.Items.Clear()
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    ComboBox1.Items.Add(ds.Tables(0).Rows(i).Item("process").ToString)
                Next

                Dim adapter2 As SqlDataAdapter
                Dim ds2 As New DataSet
                Dim sql2 As String
                sql2 = "select line from sub_po where po_no=" & TextBox1.Text
                adapter2 = New SqlDataAdapter(sql2, Database.koneksi)
                adapter2.Fill(ds2)

                ComboBox2.Items.Clear()
                For i As Integer = 0 To ds2.Tables(0).Rows.Count - 1
                    ComboBox2.Items.Add(ds2.Tables(0).Rows(i).Item("line").ToString)
                Next
            Else
                MessageBox.Show("PO not exist")
                TextBox1.Text = ""
            End If
        End If
    End Sub

    Sub DGV_Reject()
        DataGridView1.DataSource = Nothing
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        If TextBox1.Text <> "" Then
            Call Database.koneksi_database()
            Dim dtDOC As DataTable = Database.GetData("select number DB,po PO,line LINE,process PROC_REJECT,qty QTY_REJECT from reject where po=" & TextBox1.Text)

            DataGridView1.DataSource = dtDOC

            Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
            delete.Name = "delete"
            delete.HeaderText = "Delete"
            delete.Width = 100
            delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            delete.Text = "Delete"
            delete.UseColumnTextForButtonValue = True
            DataGridView1.Columns.Insert(5, delete)

            For i As Integer = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                    DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next i
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.ColumnIndex = 5 Then
            Dim result = MessageBox.Show("Yakin, Hapus Data ini?", "Warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from reject where number=" & DataGridView1.Rows(e.RowIndex).Cells(0).Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()
                    DataGridView1.DataSource = Nothing
                    DGV_Reject()
                    MessageBox.Show("Delete Success.")
                Catch ex As Exception
                    MessageBox.Show("failed" & ex.Message)
                End Try
            End If
        End If
    End Sub
End Class