Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class ListPrint
    Public Shared GForm As String = ""
    Public Shared GSubSubPO As String = ""
    Public Shared GLine As String = ""
    Public Shared GDesc As String = ""
    Public Shared GInv As String = ""

    Public Sub New(ByVal nama_form As String, ByVal form As String, ByVal subsubpo As String, Optional ByVal line As String = Nothing, Optional ByVal desc As String = Nothing, Optional ByVal Inv As String = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Label1.Text = nama_form
        GForm = form
        GSubSubPO = subsubpo
        GLine = line
        GDesc = desc
        GInv = Inv

        If GForm = "Flow Ticket" Then
            load_data_print_flow_ticket(subsubpo, line)
        ElseIf GForm = "WIP" Then
            load_data_print_wip(subsubpo, line)
        ElseIf GForm = "ONHOLD" Then
            load_data_print_onhold(subsubpo, line)
        ElseIf GForm = "Defect" Then
            load_data_print_defect(subsubpo, line)
        ElseIf GForm = "Sub Assy" Then
            load_data_print_sa(subsubpo, line)
        ElseIf GForm = "Return" Then
            load_data_print_return(subsubpo, line)
        ElseIf GForm = "Others" Then
            load_data_print_others(subsubpo, line)
        End If

    End Sub
    Sub load_data_print_flow_ticket(ByVal subsubpo As String, ByVal line As String)
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Dim queryCheckFlowTicket As String = "select flow_ticket [Flow Ticket], sub_sub_po [Sub Sub PO],fg [Finish Goods], line Line, qty_per_lot [Qty Per Lot], qty_sub_sub_po [Qty Sub Sub PO] from flow_ticket where sub_sub_po='" & subsubpo & "' and department='" & globVar.department & "' and line='" & line & "'"
        Dim dtCheckFlowTicket As DataTable = Database.GetData(queryCheckFlowTicket)
        If dtCheckFlowTicket.Rows.Count > 0 Then
            DataGridView1.DataSource = dtCheckFlowTicket

            Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
            check.Name = "check"
            check.HeaderText = "Check All"
            check.Width = 100
            check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView1.Columns.Insert(0, check)
        End If
    End Sub

    Sub load_data_print_wip(ByVal subsubpo As String, Optional ByVal line As String = Nothing)
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Dim queryCheckwip As String = "select code_stock_prod_wip [Code],fg_pn [Finish Goods],flow_ticket_no [Flow Ticket],process [Process],pengali [Qty] from stock_prod_wip where sub_sub_po='" & subsubpo & "' group by code_stock_prod_wip,fg_pn,flow_ticket_no,process,pengali"
        Dim dtCheckWIP As DataTable = Database.GetData(queryCheckwip)
        If dtCheckWIP.Rows.Count > 0 Then
            DataGridView1.DataSource = dtCheckWIP

            Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
            check.Name = "check"
            check.HeaderText = "Check All"
            check.Width = 100
            check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView1.Columns.Insert(0, check)
        End If
    End Sub

    Sub load_data_print_onhold(ByVal subsubpo As String, Optional ByVal line As String = Nothing)
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Dim queryCheckonhold As String = "select code_stock_prod_onhold [Code],fg_pn [Finish Goods],flow_ticket_no [Flow Ticket],process [Process],pengali [Qty] from stock_prod_onhold where sub_sub_po='" & subsubpo & "' group by code_stock_prod_onhold,fg_pn,flow_ticket_no,process,pengali"
        Dim dtCheckONHOLD As DataTable = Database.GetData(queryCheckonhold)
        If dtCheckONHOLD.Rows.Count > 0 Then
            DataGridView1.DataSource = dtCheckONHOLD

            Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
            check.Name = "check"
            check.HeaderText = "Check All"
            check.Width = 100
            check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView1.Columns.Insert(0, check)
        End If
    End Sub

    Sub load_data_print_defect(ByVal subsubpo As String, Optional ByVal line As String = Nothing)
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Dim queryCheckdefect As String = "select DISTINCT(CODE_OUT_PROD_DEFECT) [Code],flow_ticket_no [Flow Ticket],fg_pn [Finish Goods], Line from out_prod_DEFECT where sub_sub_po='" & GSubSubPO & "' and line='" & GLine & "'"
        Dim dtCheckDEFECT As DataTable = Database.GetData(queryCheckdefect)
        If dtCheckDEFECT.Rows.Count > 0 Then
            DataGridView1.DataSource = dtCheckDEFECT

            Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
            check.Name = "check"
            check.HeaderText = "Check All"
            check.Width = 100
            check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView1.Columns.Insert(0, check)
        End If
    End Sub

    Sub load_data_print_sa(ByVal subsubpo As String, Optional ByVal line As String = Nothing)
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Dim queryCheckonhold As String = "select code_stock_prod_onhold [Code],fg_pn [Finish Goods],flow_ticket_no [Flow Ticket],process [Process],pengali [Qty] from stock_prod_onhold where sub_sub_po='" & subsubpo & "' group by code_stock_prod_onhold,fg_pn,flow_ticket_no,process,pengali"
        Dim dtCheckONHOLD As DataTable = Database.GetData(queryCheckonhold)
        If dtCheckONHOLD.Rows.Count > 0 Then
            DataGridView1.DataSource = dtCheckONHOLD

            Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
            check.Name = "check"
            check.HeaderText = "Check All"
            check.Width = 100
            check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView1.Columns.Insert(0, check)
        End If
    End Sub

    Sub load_data_print_return(ByVal subsubpo As String, Optional ByVal line As String = Nothing)
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Dim queryCheckonhold As String = "select code_stock_prod_onhold [Code],fg_pn [Finish Goods],flow_ticket_no [Flow Ticket],process [Process],pengali [Qty] from stock_prod_onhold where sub_sub_po='" & subsubpo & "' group by code_stock_prod_onhold,fg_pn,flow_ticket_no,process,pengali"
        Dim dtCheckONHOLD As DataTable = Database.GetData(queryCheckonhold)
        If dtCheckONHOLD.Rows.Count > 0 Then
            DataGridView1.DataSource = dtCheckONHOLD

            Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
            check.Name = "check"
            check.HeaderText = "Check All"
            check.Width = 100
            check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView1.Columns.Insert(0, check)
        End If
    End Sub

    Sub load_data_print_others(ByVal subsubpo As String, Optional ByVal line As String = Nothing)
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Dim queryCheckonhold As String = "select code_stock_prod_onhold [Code],fg_pn [Finish Goods],flow_ticket_no [Flow Ticket],process [Process],pengali [Qty] from stock_prod_onhold where sub_sub_po='" & subsubpo & "' group by code_stock_prod_onhold,fg_pn,flow_ticket_no,process,pengali"
        Dim dtCheckONHOLD As DataTable = Database.GetData(queryCheckonhold)
        If dtCheckONHOLD.Rows.Count > 0 Then
            DataGridView1.DataSource = dtCheckONHOLD

            Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
            check.Name = "check"
            check.HeaderText = "Check All"
            check.Width = 100
            check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView1.Columns.Insert(0, check)
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

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If e.ColumnIndex = -1 Then
            Exit Sub
        End If

        If DataGridView1.Columns(e.ColumnIndex).Name = "check" Then
            If DataGridView1.Rows(e.RowIndex).Cells(0).Value = True Then
                DataGridView1.Rows(e.RowIndex).Cells(0).Value = False
            Else
                DataGridView1.Rows(e.RowIndex).Cells(0).Value = True
            End If
        End If
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        If e.ColumnIndex = 0 Then
            If DataGridView1.Rows(0).Cells(0).Value = True Then
                For i As Integer = 0 To DataGridView1.RowCount - 1
                    DataGridView1.Rows(i).Cells(0).Value = False
                Next
            ElseIf DataGridView1.Rows(0).Cells(0).Value = False Then
                For i As Integer = 0 To DataGridView1.RowCount - 1
                    DataGridView1.Rows(i).Cells(0).Value = True
                Next
            Else
                For i As Integer = 0 To DataGridView1.RowCount - 1
                    DataGridView1.Rows(i).Cells(0).Value = True
                Next
            End If
        End If

        For Each column As DataGridViewColumn In DataGridView1.Columns
            column.SortMode = DataGridViewColumnSortMode.NotSortable
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If GForm = "Flow Ticket" Then
            For i = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(i).Cells("check").Value = True Then
                    globVar.failPrint = ""
                    _PrintingFlowTicket.txt_Lot_No.Text = DataGridView1.Rows(i).Cells("Flow Ticket").Value
                    _PrintingFlowTicket.txt_Quantity_PO.Text = DataGridView1.Rows(i).Cells("Qty Sub Sub PO").Value
                    _PrintingFlowTicket.txt_Qty_per_Lot.Text = DataGridView1.Rows(i).Cells("Qty Per Lot").Value
                    'Subsubpo, fg, qtypo, qtyperlot, line, noflowticket
                    _PrintingFlowTicket.txt_QR_Code.Text = DataGridView1.Rows(i).Cells("Sub Sub PO").Value & ";" & DataGridView1.Rows(i).Cells("Finish Goods").Value & ";" & DataGridView1.Rows(i).Cells("Qty Sub Sub PO").Value & ";" & DataGridView1.Rows(i).Cells("Qty Per Lot").Value & ";" & DataGridView1.Rows(i).Cells("Line").Value & ";" & DataGridView1.Rows(i).Cells("Flow Ticket").Value
                    _PrintingFlowTicket.btn_Print_Click(sender, e)
                    Dim parts As String() = DataGridView1.Rows(i).Cells("Sub Sub PO").Value.Split("-"c)
                    If globVar.failPrint = "No" Then
                        Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, sub_sub_po,department,flow_ticket)
                            VALUES ('" & parts(0) & "','" & DataGridView1.Rows(i).Cells("Finish Goods").Value & "','" & DataGridView1.Rows(i).Cells("Line").Value & "','" & DataGridView1.Rows(i).Cells("Sub Sub PO").Value & "','" & globVar.department & "','" & DataGridView1.Rows(i).Cells("Flow Ticket").Value & "')"
                        Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                        cmdInsertPrintingRecord.ExecuteNonQuery()
                    End If
                End If
            Next i
        ElseIf GForm = "WIP" Then
            For i = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(i).Cells("check").Value = True Then
                    globVar.failPrint = ""
                    Dim parts As String() = GSubSubPO.Split("-"c)

                    _PrintingWIPOnHold.txt_QR_Code.Text = DataGridView1.Rows(i).Cells("Code").Value
                    _PrintingWIPOnHold.txt_jenis_ticket.Text = "WIP"
                    _PrintingWIPOnHold.txt_part_number.Text = DataGridView1.Rows(i).Cells("Finish Goods").Value
                    _PrintingWIPOnHold.txt_Part_Description.Text = GDesc
                    _PrintingWIPOnHold.txt_Process.Text = DataGridView1.Rows(i).Cells("Process").Value
                    _PrintingWIPOnHold.txt_Qty.Text = DataGridView1.Rows(i).Cells("Qty").Value
                    _PrintingWIPOnHold.txt_Traceability.Text = parts(0)
                    _PrintingWIPOnHold.txt_Inv_crtl_date.Text = ""
                    _PrintingWIPOnHold.txt_Unique_id.Text = DataGridView1.Rows(i).Cells("Code").Value
                    _PrintingWIPOnHold.txt_Status.Text = "WIP"
                    _PrintingWIPOnHold.btn_Print_Click(sender, e)

                    If globVar.failPrint = "No" Then
                        Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, remark,sub_sub_po,department,flow_ticket,code_print)
                                VALUES ('" & parts(0) & "','" & DataGridView1.Rows(i).Cells("Finish Goods").Value & "','" & GLine & "','WIP','" & GSubSubPO & "','" & globVar.department & "','" & DataGridView1.Rows(i).Cells("Flow Ticket").Value & "','" & DataGridView1.Rows(i).Cells("Code").Value & "')"
                        Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                        cmdInsertPrintingRecord.ExecuteNonQuery()
                    End If
                End If
            Next i
        ElseIf GForm = "ONHOLD" Then
            For i = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(i).Cells("check").Value = True Then
                    globVar.failPrint = ""
                    Dim parts As String() = GSubSubPO.Split("-"c)

                    _PrintingWIPOnHold.txt_QR_Code.Text = DataGridView1.Rows(i).Cells("Code").Value
                    _PrintingWIPOnHold.txt_jenis_ticket.Text = "ONHOLD"
                    _PrintingWIPOnHold.txt_part_number.Text = DataGridView1.Rows(i).Cells("Finish Goods").Value
                    _PrintingWIPOnHold.txt_Part_Description.Text = GDesc
                    _PrintingWIPOnHold.txt_Process.Text = DataGridView1.Rows(i).Cells("Process").Value
                    _PrintingWIPOnHold.txt_Qty.Text = DataGridView1.Rows(i).Cells("Qty").Value
                    _PrintingWIPOnHold.txt_Traceability.Text = parts(0)
                    _PrintingWIPOnHold.txt_Inv_crtl_date.Text = GInv
                    _PrintingWIPOnHold.txt_Unique_id.Text = DataGridView1.Rows(i).Cells("Code").Value
                    _PrintingWIPOnHold.txt_Status.Text = "ONHOLD"
                    _PrintingWIPOnHold.btn_Print_Click(sender, e)

                    If globVar.failPrint = "No" Then
                        Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, remark,sub_sub_po,department,flow_ticket,code_print)
                                        VALUES ('" & parts(0) & "','" & DataGridView1.Rows(i).Cells("Finish Goods").Value & "','" & GLine & "','OnHold','" & GSubSubPO & "','" & globVar.department & "','" & DataGridView1.Rows(i).Cells("Flow Ticket").Value & "','" & DataGridView1.Rows(i).Cells("Code").Value & "')"
                        Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                        cmdInsertPrintingRecord.ExecuteNonQuery()
                    End If
                End If
            Next i
        ElseIf GForm = "Defect" Then
            For i = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(i).Cells("check").Value = True Then
                    globVar.failPrint = ""
                    Dim parts As String() = GSubSubPO.Split("-"c)

                    _PrintingDefect.txt_QR_Code.Text = DataGridView1.Rows(i).Cells("Code").Value
                    _PrintingDefect.Label2.Text = "Defect Ticket"
                    _PrintingDefect.txt_Unique_id.Text = DataGridView1.Rows(i).Cells("Code").Value
                    _PrintingDefect.txt_part_number.Text = DataGridView1.Rows(i).Cells("Finish Goods").Value
                    _PrintingDefect.txt_Part_Description.Text = GDesc
                    _PrintingDefect.txt_Traceability.Text = parts(0)
                    _PrintingDefect.txt_Inv_crtl_date.Text = GInv
                    _PrintingDefect.btn_Print_Click(sender, e)

                    If globVar.failPrint = "No" Then
                        Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, remark,sub_sub_po,department,flow_ticket,code_print)
                                    VALUES ('" & parts(0) & "','" & DataGridView1.Rows(i).Cells("Finish Goods").Value & "','" & DataGridView1.Rows(i).Cells("Line").Value & "','Defect Material','" & GSubSubPO & "','" & globVar.department & "','" & DataGridView1.Rows(i).Cells("Flow Ticket").Value & "','" & DataGridView1.Rows(i).Cells("Code").Value & "')"
                        Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                        cmdInsertPrintingRecord.ExecuteNonQuery()

                        Dim sqlupdateDefect As String = "update out_prod_defect set [print]=1 where CODE_OUT_PROD_DEFECT='" & DataGridView1.Rows(i).Cells("Code").Value & "'"
                        Dim cmdupdateDefect = New SqlCommand(sqlupdateDefect, Database.koneksi)
                        cmdupdateDefect.ExecuteNonQuery()
                    End If
                End If
            Next i
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub
End Class