Imports System.Data.SqlClient
Imports System.Windows
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports MdiTabControl

Public Class PrintFlowTicket
    Public Shared menu As String = "Print Flow Ticket"
    Sub DGV_DOC()
        Try
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.DataSource = Nothing
            DataGridView1.Rows.Clear()
            DataGridView1.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryDOC As String = "select component [Comp],desc_comp [Desc],Usage [Usage] from prod_doc where line='" & ComboBox1.Text & "' and sub_sub_po='" & TextBox8.Text & "' and department='" & globVar.department & "'"
            Dim dtDOC As DataTable = Database.GetData(queryDOC)

            DataGridView1.DataSource = dtDOC

            For i As Integer = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                    DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next i
        Catch ex As Exception
            RJMessageBox.Show("Error Flow Ticket - 1 =>" & ex.Message)
        End Try
    End Sub

    Sub DGV_DOP()
        Try
            DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView2.DataSource = Nothing
            DataGridView2.Rows.Clear()
            DataGridView2.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryDOP As String = "select id_card_no [ID Card], pd.operator_id [Operator Name], pd.Process [Process] from prod_dop pd, users u where pd.line='" & ComboBox1.Text & "' and pd.sub_sub_po='" & TextBox8.Text & "' and pd.operator_id=u.name and pd.department='" & globVar.department & "' order by pd.process_number"
            Dim dtDOP As DataTable = Database.GetData(queryDOP)

            DataGridView2.DataSource = dtDOP

            For i As Integer = 0 To DataGridView2.RowCount - 1
                If DataGridView2.Rows(i).Index Mod 2 = 0 Then
                    DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next i

        Catch ex As Exception
            RJMessageBox.Show("Error Flow Ticket - 2 =>" & ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If globVar.view > 0 Then
            If DataGridView2.Rows.Count = 0 Then
                RJMessageBox.Show("Cannot Print When Detail Of Process / Detail Of Component blank. Please set First.")
                Exit Sub
            End If

            Dim queryDOP As String = "select * from prod_dop where line='" & ComboBox1.Text & "' and sub_sub_po='" & TextBox8.Text & "' and fg_pn='" & TextBox2.Text & "' and operator_id is null and department='" & globVar.department & "'"
            Dim dtDOP As DataTable = Database.GetData(queryDOP)

            _PrintingFlowTicket.txt_fg_part_number.Text = TextBox2.Text
            _PrintingFlowTicket.txt_part_description.Text = TextBox4.Text
            _PrintingFlowTicket.txt_Line_No.Text = ComboBox1.Text
            _PrintingFlowTicket.txt_PO_Number.Text = TextBox8.Text
            _PrintingFlowTicket.DataGridView2.Rows.Clear()

            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                _PrintingFlowTicket.DataGridView1.Rows(i).Cells(1).Value = DataGridView1.Rows(i).Cells(0).Value
                _PrintingFlowTicket.DataGridView1.Rows(i).Cells(2).Value = DataGridView1.Rows(i).Cells(1).Value
            Next

            Dim stringSub As String
            For i As Integer = 0 To DataGridView2.Rows.Count - 1
                If DataGridView2.Rows(i).Cells(1).Value.ToString().Length > 4 Then
                    stringSub = DataGridView2.Rows(i).Cells(1).Value.ToString.Substring(0, 4)
                Else
                    stringSub = DataGridView2.Rows(i).Cells(1).Value.ToString
                End If
                Dim row As String() = New String() {i + 1.ToString, stringSub, DataGridView2.Rows(i).Cells(0).Value, DataGridView2.Rows(i).Cells(2).Value}
                _PrintingFlowTicket.DataGridView2.Rows.Add(row)
            Next

            If Val(TextBox6.Text) <= Val(TextBox7.Text) Then
                Dim NoFlowTicket = "1 of 1"

                Try
                    Dim queryCheckFlowTicket As String = "select * from flow_ticket where line='" & ComboBox1.Text & "' and sub_sub_po='" & TextBox8.Text & "' and fg='" & TextBox2.Text & "' and department='" & globVar.department & "' and flow_ticket='" & NoFlowTicket & "'"
                    Dim dtCheckFlowTicket As DataTable = Database.GetData(queryCheckFlowTicket)
                    If dtCheckFlowTicket.Rows.Count = 0 Then
                        Dim sqlInsertFlowTicket As String = "INSERT INTO flow_ticket (sub_sub_po, fg, line, qty_sub_sub_po, qty_per_lot,department,flow_ticket)
                                VALUES ('" & TextBox8.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "'," & TextBox6.Text & "," & TextBox7.Text & ",'" & globVar.department & "','" & NoFlowTicket & "')"
                        Dim cmdInsertFlowTicket = New SqlCommand(sqlInsertFlowTicket, Database.koneksi)
                        cmdInsertFlowTicket.ExecuteNonQuery()
                    End If
                Catch ex As Exception
                    RJMessageBox.Show("Error Flow Ticket - 3 =>" & ex.Message)
                End Try
            Else
                If Val(TextBox6.Text) Mod Val(TextBox7.Text) = 0 Then
                    For i = 1 To Val(TextBox6.Text) / Val(TextBox7.Text)
                        Dim NoFlowTicket As String = i & " of " & Val(TextBox6.Text) / Val(TextBox7.Text)

                        Try
                            'Dim queryCheckFlowTicket As String = "select * from flow_ticket where line='" & ComboBox1.Text & "' and sub_sub_po='" & TextBox8.Text & "' and fg='" & TextBox2.Text & "' and department='" & globVar.department & "' and flow_ticket='" & NoFlowTicket & "'"
                            Dim queryCheckFlowTicket As String = "select * from flow_ticket where line='" & ComboBox1.Text & "' and sub_sub_po='" & TextBox8.Text & "' and fg='" & TextBox2.Text & "' and department='" & globVar.department & "' and flow_ticket like '" & i & " of %'"
                            Dim dtCheckFlowTicket As DataTable = Database.GetData(queryCheckFlowTicket)
                            If dtCheckFlowTicket.Rows.Count = 0 Then

                                Dim sqlInsertFlowTicket As String = "INSERT INTO flow_ticket (sub_sub_po, fg, line, qty_sub_sub_po, qty_per_lot,department,flow_ticket)
                                            VALUES ('" & TextBox8.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "'," & TextBox6.Text & "," & TextBox7.Text & ",'" & globVar.department & "','" & NoFlowTicket & "')"
                                Dim cmdInsertFlowTicket = New SqlCommand(sqlInsertFlowTicket, Database.koneksi)
                                cmdInsertFlowTicket.ExecuteNonQuery()
                            Else

                                Dim SqlUpdateFlowTicket As String = "update flow_ticket set qty_sub_sub_po=" & TextBox6.Text & ", qty_per_lot=" & TextBox7.Text & ", flow_ticket='" & NoFlowTicket & "' where ID=" & dtCheckFlowTicket.Rows(0).Item("id")
                                Dim cmdUpdateFlowTicket = New SqlCommand(SqlUpdateFlowTicket, Database.koneksi)
                                cmdUpdateFlowTicket.ExecuteNonQuery()

                            End If
                        Catch ex As Exception
                            RJMessageBox.Show("Error Flow Ticket - 5 =>" & ex.Message)
                        End Try

                    Next

                    Dim sqlDelete As String = "delete from flow_ticket where sub_sub_po='" & TextBox8.Text & "' and qty_sub_sub_po != '" & TextBox6.Text & "'"
                    Dim cmdDelete = New SqlCommand(sqlDelete, Database.koneksi)
                    cmdDelete.ExecuteNonQuery()
                Else
                    For i = 1 To Math.Floor(Val(TextBox6.Text) / Val(TextBox7.Text)) + 1
                        Dim NoFlowTicket As String = i & " of " & Math.Floor(Val(TextBox6.Text) / Val(TextBox7.Text)) + 1

                        Try
                            'Dim queryCheckFlowTicket As String = "select * from flow_ticket where line='" & ComboBox1.Text & "' and sub_sub_po='" & TextBox8.Text & "' and fg='" & TextBox2.Text & "' and department='" & globVar.department & "' and flow_ticket='" & NoFlowTicket & "'"
                            Dim queryCheckFlowTicket As String = "select * from flow_ticket where line='" & ComboBox1.Text & "' and sub_sub_po='" & TextBox8.Text & "' and fg='" & TextBox2.Text & "' and department='" & globVar.department & "' and flow_ticket like '" & i & " of %'"
                            Dim dtCheckFlowTicket As DataTable = Database.GetData(queryCheckFlowTicket)
                            If dtCheckFlowTicket.Rows.Count = 0 Then

                                Dim sqlInsertFlowTicket As String = "INSERT INTO flow_ticket (sub_sub_po, fg, line, qty_sub_sub_po, qty_per_lot,department,flow_ticket)
                                    VALUES ('" & TextBox8.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "'," & TextBox6.Text & "," & TextBox7.Text & ",'" & globVar.department & "','" & NoFlowTicket & "')"
                                Dim cmdInsertFlowTicket = New SqlCommand(sqlInsertFlowTicket, Database.koneksi)
                                cmdInsertFlowTicket.ExecuteNonQuery()
                            Else

                                Dim SqlUpdateFlowTicket As String = "update flow_ticket set qty_sub_sub_po=" & TextBox6.Text & ", qty_per_lot=" & TextBox7.Text & ", flow_ticket='" & NoFlowTicket & "' where ID=" & dtCheckFlowTicket.Rows(0).Item("id")
                                Dim cmdUpdateFlowTicket = New SqlCommand(SqlUpdateFlowTicket, Database.koneksi)
                                cmdUpdateFlowTicket.ExecuteNonQuery()

                            End If
                        Catch ex As Exception
                            RJMessageBox.Show("Error Flow Ticket - 6 =>" & ex.Message)
                        End Try

                        'rjMessageBox.Show("Print " & i & " of " & Math.Floor(Val(TextBox6.Text) / Val(TextBox7.Text)) + 1 & " Label Flow Ticket")
                    Next

                    Dim sqlDelete As String = "delete from flow_ticket where sub_sub_po='" & TextBox8.Text & "' and qty_sub_sub_po != '" & TextBox6.Text & "'"
                    Dim cmdDelete = New SqlCommand(sqlDelete, Database.koneksi)
                    cmdDelete.ExecuteNonQuery()
                End If
            End If

            Dim _formListPrint As New ListPrint("Print Flow Ticket", "Flow Ticket", TextBox8.Text, ComboBox1.Text)
            _formListPrint.ShowDialog()
        End If
    End Sub

    Private Sub PrintFlowTicket_Load(sender As Object, e As EventArgs) Handles Me.Load
        tampilDataComboBoxLine()
        ComboBox1.SelectedIndex = -1
        Button2.Enabled = False
        TextBox1.Enabled = False
        Button3.Enabled = False
    End Sub

    Sub tampilDataComboBoxLine()
        Call Database.koneksi_database()
        Dim dtMasterLine As DataTable = Database.GetData("select * from master_line where department='" & globVar.department & "' order by name")

        ComboBox1.DataSource = dtMasterLine
        ComboBox1.DisplayMember = "name"
        ComboBox1.ValueMember = "name"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If globVar.view > 0 Then
            clear()
            _PrintingFlowTicket._refresh()
            Try
                Dim query As String = "select mp.po,mp.sub_po,mp.fg_pn,ssp.sub_sub_po,mfg.description,ssp.sub_sub_po_qty,mfg.spq
                    from sub_sub_po ssp,main_po mp,master_finish_goods mfg 
                    where ssp.status='Open' and mp.id=ssp.main_po and mfg.fg_part_number=mp.fg_pn and ssp.line='" & ComboBox1.Text & "' and mp.department='" & globVar.department & "'"
                Dim dt As DataTable = Database.GetData(query)
                If dt.Rows.Count > 0 Then
                    Button2.Enabled = True
                    TextBox1.Enabled = True
                    Button3.Enabled = True
                    TextBox2.Text = dt.Rows(0).Item("FG_PN").ToString
                    TextBox4.Text = dt.Rows(0).Item("DESCRIPTION").ToString
                    TextBox5.Text = dt.Rows(0).Item("PO").ToString
                    TextBox6.Text = dt.Rows(0).Item("SUB_SUB_PO_QTY").ToString
                    TextBox7.Text = dt.Rows(0).Item("SPQ").ToString
                    TextBox8.Text = dt.Rows(0).Item("SUB_SUB_PO").ToString

                    Dim queryFam As String = "SELECT DISTINCT(FAMILY) FROM MATERIAL_USAGE_FINISH_GOODS WHERE FG_PART_NUMBER='" & dt.Rows(0).Item("FG_PN").ToString & "'"
                    Dim dtFam As DataTable = Database.GetData(queryFam)
                    TextBox3.Text = dtFam.Rows(0).Item("family").ToString

                    DGV_DOP()
                    DGV_DOC()
                Else
                    RJMessageBox.Show("This line no have any PO")
                    DGV_DOC()
                    DGV_DOP()
                End If
            Catch ex As Exception
                RJMessageBox.Show("Error Flow Ticket - 8 =>" & ex.Message)
            End Try
        End If
    End Sub

    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete
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

    Private Sub DataGridView2_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView2.DataBindingComplete
        With DataGridView2
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

    Sub clear()
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        DataGridView2.DataSource = Nothing
        DataGridView2.Rows.Clear()
        DataGridView2.Columns.Clear()
    End Sub

    Sub DGV_DOP_Param(Lot As String)
        Try
            DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView2.DataSource = Nothing
            DataGridView2.Rows.Clear()
            DataGridView2.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryDOP As String = "select id_card_no [ID Card], pd.operator [Operator Name], pd.Process from prod_dop_details pd, users u where pd.sub_sub_po='" & TextBox8.Text & "' and pd.operator=u.name and pd.lot_flow_ticket='" & Lot & "' order by pd.process_number"
            Dim dtDOP As DataTable = Database.GetData(queryDOP)

            DataGridView2.DataSource = dtDOP

            For i As Integer = 0 To DataGridView2.RowCount - 1
                If DataGridView2.Rows(i).Index Mod 2 = 0 Then
                    DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next i

        Catch ex As Exception
            RJMessageBox.Show("Error Flow Ticket - 9 =>" & ex.Message)
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Button2.Enabled = False
        TextBox1.Enabled = False
        Button3.Enabled = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If globVar.view > 0 Then

            If DataGridView2.Rows.Count = 0 Then
                RJMessageBox.Show("Cannot Print When Detail Of Process / Detail Of Component blank. Please set First.")
                Exit Sub
            End If

            If Val(TextBox1.Text) > 0 Then
                _PrintingFlowTicket.txt_fg_part_number.Text = TextBox2.Text
                _PrintingFlowTicket.txt_part_description.Text = TextBox4.Text
                _PrintingFlowTicket.txt_Line_No.Text = ComboBox1.Text
                _PrintingFlowTicket.txt_PO_Number.Text = TextBox8.Text
                _PrintingFlowTicket.txt_Quantity_PO.Text = TextBox6.Text
                _PrintingFlowTicket.txt_Qty_per_Lot.Text = TextBox7.Text
                _PrintingFlowTicket.DataGridView2.Rows.Clear()

                For i As Integer = 0 To DataGridView1.Rows.Count - 1
                    _PrintingFlowTicket.DataGridView1.Rows(i).Cells(1).Value = DataGridView1.Rows(i).Cells(0).Value
                    _PrintingFlowTicket.DataGridView1.Rows(i).Cells(2).Value = DataGridView1.Rows(i).Cells(1).Value
                Next

                Dim stringSub As String
                For i As Integer = 0 To DataGridView2.Rows.Count - 1
                    If DataGridView2.Rows(i).Cells(1).Value.ToString().Length > 4 Then
                        stringSub = DataGridView2.Rows(i).Cells(1).Value.ToString.Substring(0, 4)
                    Else
                        stringSub = DataGridView2.Rows(i).Cells(1).Value.ToString
                    End If
                    Dim row As String() = New String() {i + 1.ToString, stringSub, DataGridView2.Rows(i).Cells(0).Value, DataGridView2.Rows(i).Cells(2).Value}
                    _PrintingFlowTicket.DataGridView2.Rows.Add(row)
                Next

                For i = 1 To Val(TextBox1.Text)
                    Dim NoFlowTicket = i & " of 0"
                    globVar.failPrint = ""
                    _PrintingFlowTicket.txt_Lot_No.Text = "0"
                    _PrintingFlowTicket.txt_QR_Code.Text = TextBox8.Text & ";" & TextBox2.Text & ";" & TextBox6.Text & ";" & TextBox7.Text & ";" & ComboBox1.Text & ";Additional" 'Subsubpo,fg,qtypo,qtyperlot,line,noflowticket
                    _PrintingFlowTicket.btn_Print_Click(sender, e)

                    Try
                        If globVar.failPrint = "No" Then
                            Dim sqlInsertPrintingRecordAdditional As String = "INSERT INTO record_printing (po, fg, line, remark, sub_sub_po,department,flow_ticket)
                            VALUES ('" & TextBox5.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "','Additional','" & TextBox8.Text & "','" & globVar.department & "','" & NoFlowTicket & "')"
                            Dim cmdInsertPrintingRecordAdditional = New SqlCommand(sqlInsertPrintingRecordAdditional, Database.koneksi)
                            cmdInsertPrintingRecordAdditional.ExecuteNonQuery()
                        End If
                    Catch ex As Exception
                        RJMessageBox.Show("Error Flow Ticket - 7 =>" & ex.Message)
                    End Try
                Next
            Else
                RJMessageBox.Show("Cannot print because qty extra < 1")
            End If
        End If
    End Sub
End Class