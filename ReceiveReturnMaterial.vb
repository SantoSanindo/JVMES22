Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class ReceiveReturnMaterial
    Private Sub ReceiveReturnMaterial_Load(sender As Object, e As EventArgs) Handles Me.Load
        DGV_ReceiveFromProduction()
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) And TextBox1.Text <> "" Then
            Dim splitQRCode() As String = Nothing
            splitQRCode = TextBox1.Text.Split(New String() {";"}, StringSplitOptions.None)
            Dim sqlCheckReturnMaterial As String = "SELECT * FROM stock_card WHERE material = '" & splitQRCode(0) & "' and lot_no=" & splitQRCode(2) & " and department='" & globVar.department & "' and status='Return To Mini Store' and actual_qty>0"
            Dim dtCheckReturnMaterial As DataTable = Database.GetData(sqlCheckReturnMaterial)
            If dtCheckReturnMaterial.Rows.Count > 0 Then
                Dim sqlCheckStockCard As String = "SELECT * FROM stock_card WHERE material = '" & splitQRCode(0) & "' and lot_no=" & splitQRCode(2) & " and department='" & globVar.department & "' and status='Receive From Production'"
                Dim dtCheckStockCard As DataTable = Database.GetData(sqlCheckStockCard)
                If dtCheckStockCard.Rows.Count > 0 Then
                    MessageBox.Show("Sorry. Double Scan")
                Else
                    Dim sqlInsertReceiveFromProduction As String = "INSERT INTO stock_card (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, ACTUAL_QTY,STATUS,DEPARTMENT,STANDARD_PACK)
                    VALUES (" & splitQRCode(0) & "," & splitQRCode(1) & "," & splitQRCode(3) & "," & splitQRCode(4) & "," & splitQRCode(2) & ",'" & splitQRCode(5) & "'," & splitQRCode(1) & ",'Receive From Production','" & globVar.department & "','" & splitQRCode(6) & "')"
                    Dim cmdInsertReceiveFromProduction = New SqlCommand(sqlInsertReceiveFromProduction, Database.koneksi)
                    If cmdInsertReceiveFromProduction.ExecuteNonQuery() Then
                        Dim SqlUpdate As String = "UPDATE STOCK_CARD SET actual_qty=0 FROM STOCK_CARD WHERE material='" & splitQRCode(0) & "' and lot_no='" & splitQRCode(2) & "' AND DEPARTMENT='" & globVar.department & "' AND STATUS='Return To Mini Store' and actual_qty > 0"
                        Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                        cmdUpdate.ExecuteNonQuery()

                        DGV_ReceiveFromProduction()
                        TextBox1.Clear()
                    End If
                End If
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
End Class