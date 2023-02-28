Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class ReceiveReturnMaterial
    Private Sub ReceiveReturnMaterial_Load(sender As Object, e As EventArgs) Handles Me.Load
        DGV_ReceiveFromProduction()
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) And TextBox1.Text <> "" Then

            Dim sqlCheckReturnMaterial As String = "SELECT * FROM stock_card WHERE id_level='" & TextBox1.Text & "' and department='" & globVar.department & "' and status='Return To Mini Store'"
            Dim dtCheckReturnMaterial As DataTable = Database.GetData(sqlCheckReturnMaterial)
            If dtCheckReturnMaterial.Rows.Count > 0 Then
                For i = 0 To dtCheckReturnMaterial.Rows.Count - 1
                    Dim sqlCheckStockCard As String = "SELECT * FROM stock_card WHERE material = '" & dtCheckReturnMaterial.Rows(i).Item("material") & "' and lot_no=" & dtCheckReturnMaterial.Rows(i).Item("lot_no") & " and department='" & globVar.department & "' and status='Receive From Production'"
                    Dim dtCheckStockCard As DataTable = Database.GetData(sqlCheckStockCard)
                    If dtCheckStockCard.Rows.Count > 0 Then

                    Else
                        Dim sql = "insert into stock_card([MTS_NO], [DEPARTMENT], [MATERIAL], [STATUS], [STANDARD_PACK], [INV_CTRL_DATE], [TRACEABILITY], [BATCH_NO], [LOT_NO], 
                        [FINISH_GOODS_PN], [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [QTY], [ACTUAL_QTY]) select [MTS_NO], [DEPARTMENT], [MATERIAL], 'Receive From Production', [STANDARD_PACK], 
                        [INV_CTRL_DATE], [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [QTY], [ACTUAL_QTY] from stock_card 
                        where id=" & dtCheckReturnMaterial.Rows(i).Item("id")

                        Dim cmdInsertReceiveFromProduction = New SqlCommand(sql, Database.koneksi)
                        If cmdInsertReceiveFromProduction.ExecuteNonQuery() Then
                            Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE id=" & dtCheckReturnMaterial.Rows(i).Item("id")
                            Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                            If cmdUpdate.ExecuteNonQuery() Then
                                DGV_ReceiveFromProduction()
                                TextBox1.Clear()
                            End If
                        End If
                    End If
                Next
            End If
        End If
    End Sub

    Sub DGV_ReceiveFromProduction()
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Dim queryDGV As String = "select MATERIAL,lot_no [LOT NO],INV_CTRL_DATE ICD,TRACEABILITY,batch_no [BATCH NO],qty [QTY LOT],actual_qty [ACTUAL QTY] from stock_card where status='Receive From Production'"
        Dim dtDGV As DataTable = Database.GetData(queryDGV)

        DataGridView1.DataSource = dtDGV

        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub TextBox2_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox2.PreviewKeyDown
        If e.KeyData = Keys.Enter And TextBox2.Text <> "" Then
            Dim Found As Boolean = False
            Dim StringToSearch As String = ""
            Dim ValueToSearchFor As String = Me.TextBox2.Text.Trim.ToLower
            Dim CurrentRowIndex As Integer = 0
            Try
                If DataGridView1.Rows.Count = 0 Then
                    CurrentRowIndex = 0
                Else
                    CurrentRowIndex = DataGridView1.CurrentRow.Index + 1
                End If
                If CurrentRowIndex > DataGridView1.Rows.Count Then
                    CurrentRowIndex = DataGridView1.Rows.Count - 1
                End If
                If DataGridView1.Rows.Count > 0 Then
                    For Each gRow As DataGridViewRow In DataGridView1.Rows
                        StringToSearch = gRow.Cells("MATERIAL").Value.ToString.Trim.ToLower
                        If InStr(1, StringToSearch, LCase(Trim(TextBox2.Text)), vbTextCompare) = 1 Then
                            Dim myCurrentCell As DataGridViewCell = gRow.Cells("MATERIAL")
                            Dim myCurrentPosition As DataGridViewCell = gRow.Cells(0)
                            DataGridView1.CurrentCell = myCurrentCell
                            CurrentRowIndex = DataGridView1.CurrentRow.Index
                            Found = True
                            TextBox2.Clear()
                        End If
                        If Found Then Exit For
                    Next
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
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