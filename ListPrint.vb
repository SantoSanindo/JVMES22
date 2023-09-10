Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class ListPrint
    Public Sub New(ByVal nama_form As String, ByVal form As String, ByVal data As Integer, ByVal subsubpo As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Label1.Text = nama_form

        If form = "Flow Ticket" Then
            load_data_print_flow_ticket(data, subsubpo)
        End If

    End Sub
    Sub load_data_print_flow_ticket(ByVal data As Integer, ByVal subsubpo As String)
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Dim queryCheckFlowTicket As String = "select flow_ticket [Flow Ticket], sub_sub_po [Sub Sub PO],fg [Finish Goods], line Line, qty_per_lot [Qty Per Lot], qty_sub_sub_po [Qty Sub Sub PO] from flow_ticket where sub_sub_po='" & subsubpo & "' and department='" & globVar.department & "'"
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
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub
End Class