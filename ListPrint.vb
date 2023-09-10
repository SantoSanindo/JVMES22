Public Class ListPrint
    Public Sub New(ByVal nama_form As String, ByVal form As String, ByVal data As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Label1.Text = nama_form

        If form = "Flow Ticket" Then
            load_data_print_flow_ticket(data)
        End If

    End Sub

    Sub load_data_print_flow_ticket(ByVal data As Integer)
        DataGridView1.Columns.Add("check", "Check For Print")
        DataGridView1.Columns.Add("flowticket", "Flow Ticket")
        DataGridView1.Columns.Add("fg", "FG")
        DataGridView1.Columns.Add("po", "PO")
        DataGridView1.Columns.Add("qtypo", "Qty PO")
        DataGridView1.Columns.Add("qtyperlot", "Qty Per Lot")
        DataGridView1.Columns.Add("line", "Line")

        For i As Integer = 0 To data - 1
            Dim row As New DataGridViewRow()
            Dim checkBox As New DataGridViewCheckBoxCell()
            checkBox.Value = True
            row.Cells.Add(checkBox)

            For j As Integer = 1 To 6 ' Mulai dari kolom ke-1 hingga ke-6
                row.Cells.Add(New DataGridViewTextBoxCell())
            Next

            DataGridView1.Rows.Add(row)
        Next
    End Sub
End Class