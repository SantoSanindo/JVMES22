Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class MainPOSubPO
    Private Sub MainPOSubPO_Load(sender As Object, e As EventArgs) Handles Me.Load
        tampilDataComboBox()

        DGV_MainPO_All()
    End Sub

    Sub tampilDataComboBox()
        Call Database.koneksi_database()
        Dim dtMasterProcessFlow As DataTable = Database.GetData("select distinct MASTER_FINISH_GOODS_PN from MASTER_PROCESS_FLOW where MASTER_PROCESS IS NOT NULL order by MASTER_FINISH_GOODS_PN")

        ComboBox1.DataSource = dtMasterProcessFlow
        ComboBox1.DisplayMember = "MASTER_FINISH_GOODS_PN"
        ComboBox1.ValueMember = "MASTER_FINISH_GOODS_PN"
        ComboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ComboBox1.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text <> "" And ComboBox1.Text <> "" Then
            Dim sql As String = "select * from main_po where po='" & TextBox1.Text & "' and fg_pn='" & ComboBox1.Text & "'"
            Dim dtMainPO As DataTable = Database.GetData(sql)

            If dtMainPO.Rows.Count > 0 Then
                DGV_MainPO_Spesific()
            Else
                MessageBox.Show("PO & FG Part Number doesn't exist")
            End If
        Else
            MessageBox.Show("Please fill PO Number & FG Part Number")
        End If
    End Sub

    Sub DGV_MainPO_Spesific()
        Dim sql As String = "select ID, PO, FG_PN,SUB_PO,SUB_PO_QTY,STATUS,BALANCE,ACTUAL_QTY from main_po where po='" & TextBox1.Text & "' and fg_pn='" & ComboBox1.Text & "'"
        Dim dtMainPO As DataTable = Database.GetData(sql)
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Call Database.koneksi_database()
        DataGridView1.DataSource = dtMainPO

        DataGridView1.Columns(0).Width = 100
        DataGridView1.Columns(2).Width = 200
        DataGridView1.Columns(5).Width = 100
        DataGridView1.Columns(6).Width = 200
        DataGridView1.Columns(7).Width = 200


        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 50
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True

        DataGridView1.Columns.Insert(8, delete)

        Dim sub_sub_po As DataGridViewButtonColumn = New DataGridViewButtonColumn
        sub_sub_po.Name = "subsubpo"
        sub_sub_po.HeaderText = "Sub-Sub PO"
        sub_sub_po.Width = 50
        sub_sub_po.Text = "Create"
        sub_sub_po.UseColumnTextForButtonValue = True

        DataGridView1.Columns.Insert(0, sub_sub_po)

        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Sub DGV_MainPO_All()
        Dim sql As String = "select ID, PO, FG_PN,SUB_PO,SUB_PO_QTY,STATUS,BALANCE,ACTUAL_QTY from main_po"
        Dim dtMainPO As DataTable = Database.GetData(sql)
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Call Database.koneksi_database()
        DataGridView1.DataSource = dtMainPO

        DataGridView1.Columns(0).Width = 100
        DataGridView1.Columns(2).Width = 200
        DataGridView1.Columns(5).Width = 100
        DataGridView1.Columns(6).Width = 200
        DataGridView1.Columns(7).Width = 200

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 50
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True

        DataGridView1.Columns.Insert(8, delete)

        Dim sub_sub_po As DataGridViewButtonColumn = New DataGridViewButtonColumn
        sub_sub_po.Name = "subsubpo"
        sub_sub_po.HeaderText = "Sub-Sub PO"
        sub_sub_po.Width = 50
        sub_sub_po.Text = "Create"
        sub_sub_po.UseColumnTextForButtonValue = True

        DataGridView1.Columns.Insert(0, sub_sub_po)

        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Sub DGV_MainPO_JustPO()
        Dim sql As String = "select ID, PO, FG_PN,SUB_PO,SUB_PO_QTY,STATUS,BALANCE,ACTUAL_QTY from main_po where po='" & TextBox1.Text & "'"
        Dim dtMainPO As DataTable = Database.GetData(sql)
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Call Database.koneksi_database()
        DataGridView1.DataSource = dtMainPO

        DataGridView1.Columns(0).Width = 100
        DataGridView1.Columns(2).Width = 200
        DataGridView1.Columns(5).Width = 100
        DataGridView1.Columns(6).Width = 200
        DataGridView1.Columns(7).Width = 200

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 50
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True

        DataGridView1.Columns.Insert(8, delete)

        Dim sub_sub_po As DataGridViewButtonColumn = New DataGridViewButtonColumn
        sub_sub_po.Name = "subsubpo"
        sub_sub_po.HeaderText = "Sub-Sub PO"
        sub_sub_po.Width = 50
        sub_sub_po.Text = "Create"
        sub_sub_po.UseColumnTextForButtonValue = True

        DataGridView1.Columns.Insert(0, sub_sub_po)

        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sql As String = "select count(*) from main_po where po='" & TextBox1.Text & "'"
        Dim dtMainPOCount As DataTable = Database.GetData(sql)
        Dim sub_po = TextBox1.Text & dtMainPOCount.Rows(0).Item(0) + 1

        Dim sqlCheck As String = "select * from main_po where po='" & TextBox1.Text & "' and fg_pn='" & ComboBox1.Text & "'"
        Dim dtMainPOCheck As DataTable = Database.GetData(sqlCheck)

        If dtMainPOCheck.Rows.Count > 0 Then
            MessageBox.Show("PO & FG Part Number already in DB")
            DGV_MainPO_Spesific()
            'TextBox1.Text = ""
            'TextBox3.Text = ""
            'ComboBox1.Text = ""
            'ComboBox2.Text = ""
        Else
            Dim sqlInsertMainPOSubPO As String = "INSERT INTO main_po (po, sub_po, sub_po_qty, fg_pn, status, balance, actual_qty)
                                    VALUES ('" & TextBox1.Text & "','" & sub_po & "'," & TextBox3.Text & ",'" & ComboBox1.Text & "','" & ComboBox2.Text & "',0,0)"
            Dim cmdInsertMainPOSubPO = New SqlCommand(sqlInsertMainPOSubPO, Database.koneksi)
            If cmdInsertMainPOSubPO.ExecuteNonQuery() Then
                DGV_MainPO_Spesific()
                TextBox1.Text = ""
                TextBox3.Text = ""
                ComboBox1.Text = ""
                ComboBox2.Text = ""
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
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.Columns(e.ColumnIndex).Name = "delete" Then

            Dim sqlcheck As String = "select * from main_po where po='" & DataGridView1.Rows(e.RowIndex).Cells("PO").Value & "' and (balance > 0 or actual_qty > 0)"
            Dim dtMainPOCheck As DataTable = Database.GetData(sqlcheck)
            If dtMainPOCheck.Rows.Count > 0 Then
                MessageBox.Show("Cannot delete this data")
                Exit Sub
            End If

            Dim result = MessageBox.Show("Are you sure to delete?", "Warning", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from main_po where id=" & DataGridView1.Rows(e.RowIndex).Cells("ID").Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    If cmd.ExecuteNonQuery() Then
                        DGV_MainPO_All()
                    End If
                Catch ex As Exception
                    MessageBox.Show("failed" & ex.Message)
                End Try
            End If
        End If

        If DataGridView1.Columns(e.ColumnIndex).Name = "subsubpo" Then
            TabControl1.SelectedTab = TabPage2
            TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells("SUB_PO").Value
            TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells("SUB_PO_QTY").Value
            TextBox8.Text = DataGridView1.Rows(e.RowIndex).Cells("PO").Value
            TextBox9.Text = DataGridView1.Rows(e.RowIndex).Cells("FG_PN").Value
            TextBox12.Text = DataGridView1.Rows(e.RowIndex).Cells("ID").Value
            DGV_SubSubPO()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text <> "" Then
            Dim sql As String = "select * from main_po where po='" & TextBox1.Text & "'"
            Dim dtMainPO As DataTable = Database.GetData(sql)

            If dtMainPO.Rows.Count > 0 Then
                DGV_MainPO_JustPO()
            Else
                MessageBox.Show("PO & FG Part Number doesn't exist")
            End If
        Else
            MessageBox.Show("Please fill PO Number")
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Call Database.koneksi_database()
        If DataGridView1.Columns(e.ColumnIndex).Name = "SUB_PO_QTY" Then

            Dim sqlcheck As String = "select * from main_po where po='" & DataGridView1.Rows(e.RowIndex).Cells("PO").Value & "' and (balance > 0 or actual_qty > 0)"
            Dim dtMainPOCheck As DataTable = Database.GetData(sqlcheck)
            If dtMainPOCheck.Rows.Count > 0 Then
                MessageBox.Show("Cannot update this data")
                DGV_MainPO_All()
                Exit Sub
            End If

            Try
                Dim Sql As String = "update main_po set SUB_PO_QTY=" & DataGridView1.Rows(e.RowIndex).Cells("SUB_PO_QTY").Value & " where ID=" & DataGridView1.Rows(e.RowIndex).Cells("ID").Value
                Dim cmd = New SqlCommand(Sql, Database.koneksi)
                If cmd.ExecuteNonQuery() Then
                    MessageBox.Show("Success updated data")
                End If
            Catch ex As Exception
                MessageBox.Show("Failed" & ex.Message)
            End Try

        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim sql As String = "select count(*) from sub_sub_po where main_po=" & TextBox12.Text
        Dim dtSubSubPOCount As DataTable = Database.GetData(sql)
        Dim sub_sub_po = TextBox2.Text & dtSubSubPOCount.Rows(0).Item(0) + 1

        Dim sqlCheck As String = "select * from sub_sub_po where main_po=" & TextBox12.Text & " and sub_sub_po='" & sub_sub_po & "'"
        Dim dtMainPOCheck As DataTable = Database.GetData(sqlCheck)

        Dim sqlSum As String = "select sum(sub_sub_po_qty) from sub_sub_po where main_po=" & TextBox12.Text
        Dim dtSubSubPOSum As DataTable = Database.GetData(sqlSum)

        If dtMainPOCheck.Rows.Count > 0 Then
            MessageBox.Show("Sub Sub PO already in DB")
            DGV_SubSubPO()
        Else
            If dtSubSubPOSum.Rows(0).Item(0) >= dtSubSubPOSum.Rows(0).Item(0) + Convert.ToInt32(TextBox10.Text) Then
                Dim sqlInsertSubPO As String = "INSERT INTO sub_sub_po (main_po, sub_sub_po, sub_sub_po_qty, line, status, actual_qty)
                                    VALUES (" & TextBox12.Text & ",'" & sub_sub_po & "'," & TextBox10.Text & ",'" & ComboBox3.Text & "','" & ComboBox4.Text & "',0)"

                Dim cmdInsertSubPO = New SqlCommand(sqlInsertSubPO, Database.koneksi)
                If cmdInsertSubPO.ExecuteNonQuery() Then
                    DGV_SubSubPO()
                    TextBox10.Text = ""
                    ComboBox3.Text = ""
                    ComboBox4.Text = ""
                End If
            Else
                MessageBox.Show("All Sub Sub PO Qty more then Sub Qty")
                TextBox10.Text = ""
                ComboBox3.Text = ""
                ComboBox4.Text = ""
            End If
        End If
    End Sub

    Private Sub TabControl1_Enter(sender As Object, e As EventArgs) Handles TabControl1.Enter
        If TabControl1.SelectedTab.TabIndex = 1 Then
            DGV_SubSubPO()
        End If
    End Sub

    Sub DGV_SubSubPO()
        Dim sql As String = "select ID, SUB_SUB_PO, SUB_SUB_PO_QTY,LINE,STATUS,ACTUAL_QTY from SUB_SUB_PO where MAIN_PO=" & TextBox12.Text
        Dim dtSubSubPO As DataTable = Database.GetData(sql)
        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView2.DataSource = Nothing
        DataGridView2.Rows.Clear()
        DataGridView2.Columns.Clear()
        Call Database.koneksi_database()
        DataGridView2.DataSource = dtSubSubPO

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 50
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True

        DataGridView2.Columns.Insert(6, delete)

        For i As Integer = 0 To DataGridView2.RowCount - 1
            If DataGridView2.Rows(i).Index Mod 2 = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub DataGridView2_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView2.DataBindingComplete
        For i As Integer = 0 To DataGridView2.RowCount - 1
            If DataGridView2.Rows(i).Index Mod 2 = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If DataGridView2.Columns(e.ColumnIndex).Name = "delete" Then

            Dim sqlcheck As String = "select * from SUB_SUB_PO where id='" & DataGridView2.Rows(e.RowIndex).Cells("ID").Value & "' and actual_qty > 0"
            Dim dtMainPOCheck As DataTable = Database.GetData(sqlcheck)
            If dtMainPOCheck.Rows.Count > 0 Then
                MessageBox.Show("Cannot delete this data")
                Exit Sub
            End If

            Dim result = MessageBox.Show("Are you sure to delete?", "Warning", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from SUB_SUB_PO where id=" & DataGridView2.Rows(e.RowIndex).Cells("ID").Value
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    If cmd.ExecuteNonQuery() Then
                        DGV_SubSubPO()
                    End If
                Catch ex As Exception
                    MessageBox.Show("failed" & ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub DataGridView2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellValueChanged
        Call Database.koneksi_database()
        If DataGridView2.Columns(e.ColumnIndex).Name = "SUB_SUB_PO_QTY" Then

            Dim sqlcheck As String = "select * from SUB_SUB_PO where ID='" & DataGridView2.Rows(e.RowIndex).Cells("ID").Value & "' and actual_qty > 0"
            Dim dtMainPOCheck As DataTable = Database.GetData(sqlcheck)
            If dtMainPOCheck.Rows.Count > 0 Then
                MessageBox.Show("Cannot update this data")
                DGV_SubSubPO()
                Exit Sub
            End If

            Try
                Dim Sql As String = "update SUB_SUB_PO set SUB_SUB_PO_QTY=" & DataGridView2.Rows(e.RowIndex).Cells("SUB_PO_QTY").Value & " where ID=" & DataGridView2.Rows(e.RowIndex).Cells("ID").Value
                Dim cmd = New SqlCommand(Sql, Database.koneksi)
                If cmd.ExecuteNonQuery() Then
                    MessageBox.Show("Success updated data")
                End If
            Catch ex As Exception
                MessageBox.Show("Failed" & ex.Message)
            End Try
        End If
    End Sub
End Class