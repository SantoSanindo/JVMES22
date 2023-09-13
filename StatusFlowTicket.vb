Imports System.Security.Cryptography
Imports System.Windows

Public Class StatusFlowTicket
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
        Dim subsubpo As String
        If TextBox1.Text.Contains(";") Then
            Dim parts() As String = TextBox1.Text.Split(";"c)
            subsubpo = parts(0)
        Else
            subsubpo = TextBox1.Text
        End If

        Dim queryStatusFlowTicket As String = "SELECT ft.ID,mp.PO,mp.Sub_po [Sub PO],ft.sub_sub_po [Sub Sub PO],ft.line [Line],ft.flow_ticket [Flow Ticket],MAX(sc.DATETIME_INSERT) AS [Time Finish] FROM flow_ticket ft LEFT JOIN sub_sub_po ssp on ssp.SUB_SUB_PO=ft.SUB_SUB_PO LEFT JOIN main_po mp on ssp.main_po=mp.ID LEFT JOIN stock_card sc on sc.FLOW_TICKET=ft.FLOW_TICKET and sc.SUB_SUB_PO=ft.SUB_SUB_PO WHERE ft.sub_sub_po = '" & subsubpo & "' GROUP BY ft.ID,mp.PO,mp.Sub_po,ft.sub_sub_po,ft.line,ft.flow_ticket ORDER BY ft.id"
        Dim dtStatusFlowTicket As DataTable = Database.GetData(queryStatusFlowTicket)
        If dtStatusFlowTicket.Rows.Count > 0 Then
            DataGridView1.DataSource = dtStatusFlowTicket
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