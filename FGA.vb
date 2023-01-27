Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class FGA
    Dim sub_sub_po As String
    Dim pn_fg As String
    Dim qty_po As String
    Dim qtyperlot As String
    Dim line As String
    Dim noflowticket As String

    Private Sub FGA_Load(sender As Object, e As EventArgs) Handles Me.Load
        tampilDataComboBoxInspector()
        tampilDataComboBoxPacker()

        ComboBox1.Enabled = False
        ComboBox2.Enabled = False
        ComboBox3.Enabled = False
        ComboBox4.Enabled = False
        ComboBox5.Enabled = False
        Button1.Enabled = False

        TextBox1.Select()
    End Sub

    Sub tampilDataComboBoxInspector()
        Call Database.koneksi_database()
        Dim dtMasterDepart As DataTable = Database.GetData("select * from users where role='INSPECTOR'")

        ComboBox1.DataSource = dtMasterDepart
        ComboBox1.DisplayMember = "NAME"
        ComboBox1.ValueMember = "NAME"
        ComboBox1.SelectedIndex = -1
    End Sub

    Sub tampilDataComboBoxPacker()
        Call Database.koneksi_database()
        Dim dtMasterDepart As DataTable = Database.GetData("select * from users where role='OPERATOR PACKING'")

        ComboBox2.DataSource = dtMasterDepart
        ComboBox2.DisplayMember = "NAME"
        ComboBox2.ValueMember = "NAME"
        ComboBox2.SelectedIndex = -1

        ComboBox3.DataSource = dtMasterDepart
        ComboBox3.DisplayMember = "NAME"
        ComboBox3.ValueMember = "NAME"
        ComboBox3.SelectedIndex = -1

        ComboBox4.DataSource = dtMasterDepart
        ComboBox4.DisplayMember = "NAME"
        ComboBox4.ValueMember = "NAME"
        ComboBox4.SelectedIndex = -1

        ComboBox5.DataSource = dtMasterDepart
        ComboBox5.DisplayMember = "NAME"
        ComboBox5.ValueMember = "NAME"
        ComboBox5.SelectedIndex = -1

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim queryCheck As String = "select * from fga where FLOW_TICKET='" & TextBox1.Text & "'"
        Dim dtCheck As DataTable = Database.GetData(queryCheck)

        If dtCheck.Rows.Count > 0 Then
            MessageBox.Show("Double Scan")
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox1.Select()

            ComboBox1.Enabled = False
            ComboBox2.Enabled = False
            ComboBox3.Enabled = False
            ComboBox4.Enabled = False
            ComboBox5.Enabled = False
            Button1.Enabled = False

            ComboBox1.SelectedIndex = -1
            ComboBox2.SelectedIndex = -1
            ComboBox3.SelectedIndex = -1
            ComboBox4.SelectedIndex = -1
            ComboBox5.SelectedIndex = -1

            Exit Sub
        End If

        If ComboBox1.Text <> "" And ComboBox2.Text <> "" And TextBox1.Text <> "" Then
            Try
                Dim sqlInsertPrintingRecord As String = "INSERT INTO fga (flow_ticket, inspector, packer1, packer2, packer3, packer4,sub_sub_po,fg_pn,line,no_flowticket,department)
                                    VALUES ('" & TextBox1.Text & "','" & ComboBox1.Text & "','" & ComboBox2.Text & "','" & ComboBox3.Text & "','" & ComboBox4.Text & "','" & ComboBox5.Text & "','" & sub_sub_po & "','" & pn_fg & "','" & line & "','" & noflowticket & "','" & globVar.department & "')"
                Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                If cmdInsertPrintingRecord.ExecuteNonQuery() Then
                    ComboBox1.SelectedIndex = -1
                    ComboBox2.SelectedIndex = -1
                    ComboBox3.SelectedIndex = -1
                    ComboBox4.SelectedIndex = -1
                    ComboBox5.SelectedIndex = -1
                    DGV_FGA()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Sub DGV_FGA()
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Dim queryMasterFinishGoods As String = "select SUB_SUB_PO,FG_PN FG_PART_NUMBER,LINE, NO_FLOWTICKET FLOW_TICKET,INSPECTOR,PACKER1,PACKER2,PACKER3,PACKER4 from FGA where sub_sub_po='" & sub_sub_po & "' and line='" & line & "' and department='" & globVar.department & "'"
        Dim dtMaterialNeed As DataTable = Database.GetData(queryMasterFinishGoods)
        If dtMaterialNeed.Rows.Count > 0 Then
            DataGridView1.DataSource = dtMaterialNeed
        End If
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) And TextBox1.Text <> "" Then
            If TextBox1.Text <> "" Then
                If InStr(TextBox1.Text, ";") = 0 Then
                    TextBox1.Clear()
                    Exit Sub
                End If

                Dim splitFlowTicket = TextBox1.Text.Split(New String() {";"}, StringSplitOptions.None)

                sub_sub_po = splitFlowTicket(0)
                pn_fg = splitFlowTicket(1)
                qty_po = splitFlowTicket(2)
                qtyperlot = splitFlowTicket(3)
                line = splitFlowTicket(4)
                noflowticket = splitFlowTicket(5)

                Dim queryMasterFinishGoods As String = "select * from master_finish_goods where fg_part_number=" & pn_fg
                Dim dtMasterFinishGoods As DataTable = Database.GetData(queryMasterFinishGoods)

                If dtMasterFinishGoods.Rows.Count > 0 Then
                    TextBox2.Text = dtMasterFinishGoods.Rows(0).Item("fg_part_number").ToString
                    TextBox3.Text = dtMasterFinishGoods.Rows(0).Item("description").ToString
                    DGV_FGA()
                End If

                ComboBox1.Enabled = True
                ComboBox2.Enabled = True
                ComboBox3.Enabled = True
                ComboBox4.Enabled = True
                ComboBox5.Enabled = True
                Button1.Enabled = True
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
End Class