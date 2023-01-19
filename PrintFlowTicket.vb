Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports MdiTabControl

Public Class PrintFlowTicket
    Sub DGV_DOC()
        Try
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.DataSource = Nothing
            DataGridView1.Rows.Clear()
            DataGridView1.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryDOC As String = "select component Component,desc_comp Description,Usage from prod_doc where line='" & ComboBox1.Text & "' and sub_sub_po='" & TextBox8.Text & "' and department='" & globVar.department & "'"
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
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub DGV_DOP()
        Try
            DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView2.DataSource = Nothing
            DataGridView2.Rows.Clear()
            DataGridView2.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryDOP As String = "select id_card_no [ID Card], pd.operator_id [Operator Name], pd.Process from prod_dop pd, users u where pd.line='" & ComboBox1.Text & "' and pd.sub_sub_po='" & TextBox8.Text & "' and pd.operator_id=u.name and pd.department='" & globVar.department & "' order by pd.process_number"
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
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DataGridView2.Rows.Count = 0 Then
            MessageBox.Show("Cannot Print When Detail Of Process blank. Please set Operator First.")
            Exit Sub
        End If

        Dim queryDOP As String = "select * from prod_dop where line='" & ComboBox1.Text & "' and sub_sub_po='" & TextBox8.Text & "' and fg_pn='" & TextBox2.Text & "' and operator_id is null and department='" & globVar.department & "'"
        Dim dtDOP As DataTable = Database.GetData(queryDOP)

        _PrintingFlowTicket.txt_fg_part_number.Text = TextBox2.Text
        _PrintingFlowTicket.txt_part_description.Text = TextBox4.Text
        _PrintingFlowTicket.txt_Line_No.Text = ComboBox1.Text
        _PrintingFlowTicket.txt_PO_Number.Text = TextBox8.Text
        _PrintingFlowTicket.txt_Quantity_PO.Text = TextBox6.Text
        _PrintingFlowTicket.txt_Qty_per_Lot.Text = TextBox7.Text

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            _PrintingFlowTicket.DataGridView1.Rows(i).Cells(1).Value = DataGridView1.Rows(i).Cells(0).Value
            _PrintingFlowTicket.DataGridView1.Rows(i).Cells(2).Value = DataGridView1.Rows(i).Cells(1).Value
        Next

        For i As Integer = 0 To DataGridView2.Rows.Count - 1
            _PrintingFlowTicket.DataGridView2.Rows(i).Cells(1).Value = DataGridView2.Rows(i).Cells(1).Value
            _PrintingFlowTicket.DataGridView2.Rows(i).Cells(2).Value = DataGridView2.Rows(i).Cells(0).Value
            _PrintingFlowTicket.DataGridView2.Rows(i).Cells(3).Value = DataGridView2.Rows(i).Cells(2).Value
        Next

        If Val(TextBox6.Text) <= Val(TextBox7.Text) Then
            If CheckBox1.Checked Then
                'MessageBox.Show("Print 1 of 1 Flow Ticket")
                _PrintingFlowTicket.txt_Lot_No.Text = "1 of 1"
                'Subsubpo,fg,qtypo,qtyperlot,line,noflowticket
                _PrintingFlowTicket.txt_QR_Code.Text = TextBox8.Text & ";" & TextBox2.Text & ";" & TextBox6.Text & ";" & TextBox7.Text & ";" & ComboBox1.Text & ";1 of 1"
                _PrintingFlowTicket.btn_Print_Click(sender, e)

                Try
                    Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, lot, of_lot, sub_sub_po,department)
                        VALUES ('" & TextBox5.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "',1,1,'" & TextBox8.Text & "','" & globVar.department & "')"
                    Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                    If cmdInsertPrintingRecord.ExecuteNonQuery() Then
                        Dim sqlInsertFlowTicket As String = "INSERT INTO flow_ticket (sub_sub_po, fg, line, qty_sub_sub_po, qty_per_lot,department,flow_ticket)
                                    VALUES ('" & TextBox8.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "'," & TextBox6.Text & "," & TextBox7.Text & ",'" & globVar.department & "','1 of 1')"
                        Dim cmdInsertFlowTicket = New SqlCommand(sqlInsertFlowTicket, Database.koneksi)
                        cmdInsertFlowTicket.ExecuteNonQuery()
                    End If
                Catch ex As Exception
                    MessageBox.Show("MainPOSubPO-12 : " & ex.Message)
                End Try
            End If

            If CheckBox2.Checked Then
                For i = 1 To Val(TextBox1.Text)
                    _PrintingFlowTicket.txt_Lot_No.Text = "0"
                    _PrintingFlowTicket.txt_QR_Code.Text = TextBox8.Text & ";" & TextBox2.Text & ";" & TextBox6.Text & ";" & TextBox7.Text & ";" & ComboBox1.Text & ";Add" 'Subsubpo,fg,qtypo,qtyperlot,line,noflowticket
                    _PrintingFlowTicket.btn_Print_Click(sender, e)
                    'MessageBox.Show("Print + " & i & " Flow Ticket Additional")
                    Try
                        Dim sqlInsertPrintingRecordAdditional As String = "INSERT INTO record_printing (po, fg, line, lot, of_lot, remark, sub_sub_po,department)
                                    VALUES ('" & TextBox5.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "'," & i & ",0,'Additional','" & TextBox8.Text & "','" & globVar.department & "')"
                        Dim cmdInsertPrintingRecordAdditional = New SqlCommand(sqlInsertPrintingRecordAdditional, Database.koneksi)
                        cmdInsertPrintingRecordAdditional.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show("MainPOSubPO-13 : " & ex.Message)
                    End Try
                Next
            End If
        Else
            If CheckBox1.Checked Then
                If Val(TextBox6.Text) Mod Val(TextBox7.Text) = 0 Then
                    For i = 1 To Val(TextBox6.Text) / Val(TextBox7.Text)
                        Dim NoFlowTicket As String = i & " of " & Val(TextBox6.Text) / Val(TextBox7.Text)
                        _PrintingFlowTicket.txt_Lot_No.Text = NoFlowTicket
                        _PrintingFlowTicket.txt_QR_Code.Text = TextBox8.Text & ";" & TextBox2.Text & ";" & TextBox6.Text & ";" & TextBox7.Text & ";" & ComboBox1.Text & ";" & NoFlowTicket 'Subsubpo,fg,qtypo,qtyperlot,line,noflowticket
                        _PrintingFlowTicket.btn_Print_Click(sender, e)

                        Try
                            Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, lot, of_lot, sub_sub_po,department)
                                VALUES ('" & TextBox5.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "'," & i & "," & Val(TextBox6.Text) / Val(TextBox7.Text) & ",'" & TextBox8.Text & "','" & globVar.department & "')"
                            Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                            If cmdInsertPrintingRecord.ExecuteNonQuery() Then
                                Dim sqlInsertFlowTicket As String = "INSERT INTO flow_ticket (sub_sub_po, fg, line, qty_sub_sub_po, qty_per_lot,department,flow_ticket)
                                    VALUES ('" & TextBox8.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "'," & TextBox6.Text & "," & TextBox7.Text & ",'" & globVar.department & "','" & NoFlowTicket & "')"
                                Dim cmdInsertFlowTicket = New SqlCommand(sqlInsertFlowTicket, Database.koneksi)
                                cmdInsertFlowTicket.ExecuteNonQuery()
                            End If
                        Catch ex As Exception
                            MessageBox.Show("MainPOSubPO-14 : " & ex.Message)
                        End Try


                        'MessageBox.Show("Print " & i & " of " & Val(TextBox6.Text) / Val(TextBox7.Text) & " Label Flow Ticket")
                    Next
                Else
                    For i = 1 To Math.Floor(Val(TextBox6.Text) / Val(TextBox7.Text)) + 1
                        Dim NoFlowTicket As String = i & " of " & Math.Floor(Val(TextBox6.Text) / Val(TextBox7.Text)) + 1

                        _PrintingFlowTicket.txt_Lot_No.Text = NoFlowTicket
                        _PrintingFlowTicket.txt_QR_Code.Text = TextBox8.Text & ";" & TextBox2.Text & ";" & TextBox6.Text & ";" & TextBox7.Text & ";" & ComboBox1.Text & ";" & NoFlowTicket 'Subsubpo,fg,qtypo,qtyperlot,line,noflowticket
                        _PrintingFlowTicket.btn_Print_Click(sender, e)

                        Try
                            Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, lot, of_lot, sub_sub_po,department)
                                    VALUES ('" & TextBox5.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "'," & i & "," & Math.Floor(Val(TextBox6.Text) / Val(TextBox7.Text)) + 1 & ",'" & TextBox8.Text & "','" & globVar.department & "')"
                            Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                            If cmdInsertPrintingRecord.ExecuteNonQuery() Then
                                Dim sqlInsertFlowTicket As String = "INSERT INTO flow_ticket (sub_sub_po, fg, line, qty_sub_sub_po, qty_per_lot,department,flow_ticket)
                                    VALUES ('" & TextBox8.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "'," & TextBox6.Text & "," & TextBox7.Text & ",'" & globVar.department & "','" & NoFlowTicket & "')"
                                Dim cmdInsertFlowTicket = New SqlCommand(sqlInsertFlowTicket, Database.koneksi)
                                cmdInsertFlowTicket.ExecuteNonQuery()
                            End If
                        Catch ex As Exception
                            MessageBox.Show("MainPOSubPO-15 : " & ex.Message)
                        End Try

                        'MessageBox.Show("Print " & i & " of " & Math.Floor(Val(TextBox6.Text) / Val(TextBox7.Text)) + 1 & " Label Flow Ticket")
                    Next
                End If
            End If

            If CheckBox2.Checked Then
                For i = 1 To Val(TextBox1.Text)
                    'MessageBox.Show("Print + " & i & " Flow Ticket Additional")
                    _PrintingFlowTicket.txt_Lot_No.Text = "0"
                    _PrintingFlowTicket.txt_QR_Code.Text = TextBox8.Text & ";" & TextBox2.Text & ";" & TextBox6.Text & ";" & TextBox7.Text & ";" & ComboBox1.Text & ";Add" 'Subsubpo,fg,qtypo,qtyperlot,line,noflowticket
                    _PrintingFlowTicket.btn_Print_Click(sender, e)

                    Try
                        Dim sqlInsertPrintingRecordAdditional As String = "INSERT INTO record_printing (po, fg, line, lot, of_lot, remark, sub_sub_po,department)
                                    VALUES ('" & TextBox5.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "'," & i & ",0,'Additional','" & TextBox8.Text & "','" & globVar.department & "')"
                        Dim cmdInsertPrintingRecordAdditional = New SqlCommand(sqlInsertPrintingRecordAdditional, Database.koneksi)
                        cmdInsertPrintingRecordAdditional.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show("MainPOSubPO-16 : " & ex.Message)
                    End Try
                Next
            End If
        End If
    End Sub

    Private Sub PrintFlowTicket_Load(sender As Object, e As EventArgs) Handles Me.Load
        tampilDataComboBoxLine()
        ComboBox1.SelectedIndex = -1
    End Sub

    Sub tampilDataComboBoxLine()
        Call Database.koneksi_database()
        Dim dtMasterLine As DataTable = Database.GetData("select * from master_line where departement='" & globVar.department & "'")

        ComboBox1.DataSource = dtMasterLine
        ComboBox1.DisplayMember = "name"
        ComboBox1.ValueMember = "name"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim query As String = "select mp.po,mp.sub_po,mp.fg_pn,ssp.sub_sub_po,mfg.description,ssp.sub_sub_po_qty,mfg.spq
            from sub_sub_po ssp,main_po mp,master_finish_goods mfg 
            where ssp.status='Open' and mp.id=ssp.main_po and mfg.fg_part_number=mp.fg_pn and ssp.line='" & ComboBox1.Text & "' and mp.department='" & globVar.department & "'"
            Dim dt As DataTable = Database.GetData(query)
            If dt.Rows.Count > 0 Then
                TextBox2.Text = dt.Rows(0).Item("FG_PN").ToString
                TextBox4.Text = dt.Rows(0).Item("DESCRIPTION").ToString
                TextBox5.Text = dt.Rows(0).Item("PO").ToString
                TextBox6.Text = dt.Rows(0).Item("SUB_SUB_PO_QTY").ToString
                TextBox7.Text = dt.Rows(0).Item("SPQ").ToString
                TextBox8.Text = dt.Rows(0).Item("SUB_SUB_PO").ToString

                Dim queryFam As String = "SELECT DISTINCT(FAMILY) FROM MATERIAL_USAGE_FINISH_GOODS WHERE FG_PART_NUMBER='" & dt.Rows(0).Item("FG_PN").ToString & "'"
                Dim dtFam As DataTable = Database.GetData(queryFam)
                TextBox3.Text = dtFam.Rows(0).Item("family").ToString

                DGV_DOC()
                DGV_DOP()
            Else
                MessageBox.Show("This line no have any PO")
                DGV_DOC()
                DGV_DOP()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class