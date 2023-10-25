Public Class TraceabilityV2
    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtInputFG.PreviewKeyDown
        If (e.KeyData = Keys.Enter) Then
            LoadDGVAtas(txtInputFG.Text)
            LoadTreeView(txtInputFG.Text)
        End If
    End Sub

    Sub LoadDGVAtas(inputFG As String)
        Try
            DataGridViewAtas.DataSource = Nothing
            DataGridViewAtas.Rows.Clear()
            DataGridViewAtas.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryInputStockDetail As String

            queryInputStockDetail = "SELECT * FROM summary_traceability WHERE fg='" & inputFG & "' order by line,lot_no"

            Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
            DataGridViewAtas.DataSource = dtInputStockDetail
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub LoadTreeView(inputFG As String)
        Try
            TreeView1.Nodes.Clear()

            Dim queryCountFG As String = "select DISTINCT(sub_sub_po) from SUMMARY_TRACEABILITY where fg='" & inputFG & "'"
            Dim dtQueryCountFG As DataTable = Database.GetData(queryCountFG)

            If dtQueryCountFG.Rows.Count > 0 Then
                For i = 0 To dtQueryCountFG.Rows.Count - 1
                    TreeView1.Nodes.Add(dtQueryCountFG.Rows(i).Item("SUB_SUB_PO").ToString, "Sub Sub PO : " & dtQueryCountFG.Rows(i).Item("SUB_SUB_PO").ToString)

                    Dim queryComp As String = "select * from SUMMARY_TRACEABILITY_COMP where sub_sub_po='" & dtQueryCountFG.Rows(i).Item("SUB_SUB_PO").ToString & "'"
                    Dim dtComp As DataTable = Database.GetData(queryComp)

                    If dtComp.Rows.Count > 0 Then
                        For j = 0 To dtComp.Rows.Count - 1
                            If dtComp.Rows(j).Item("REMARK").ToString <> "Fresh" Then
                                Dim level3 = TreeView1.Nodes(i).Nodes.Add(dtComp.Rows(j).Item("COMPONENT").ToString, dtComp.Rows(j).Item("COMPONENT").ToString & " - " & dtComp.Rows(j).Item("LOT_FG").ToString)
                            End If
                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        If (e.Node.Parent IsNot Nothing) Then
            DataGridViewBawah.DataSource = Nothing
            DataGridViewBawah.Rows.Clear()
            DataGridViewBawah.Columns.Clear()
            Dim resultSplit() As String = e.Node.Text.ToString.Split(" - ")
            Dim queryShowSA As String = "select MATERIAL,INV_CTRL_DATE INV,BATCH_NO,LOT_NO LOT_COMP,FLOW_TICKET LOT_FG,QTY from STOCK_CARD where sub_sub_po='" & e.Node.Parent.Name & "' and material='" & resultSplit(0) & "' and flow_ticket='" & resultSplit(2) & " " & resultSplit(3) & " " & resultSplit(4) & "' and status='Production Result'"
            Dim dtShowSA As DataTable = Database.GetData(queryShowSA)
            DataGridViewBawah.DataSource = dtShowSA
        Else
            DataGridViewBawah.DataSource = Nothing
            DataGridViewBawah.Rows.Clear()
            DataGridViewBawah.Columns.Clear()
            Dim queryShowFG As String = "select COMPONENT,[DESC],INV,BATCH_NO,LOT_COMP,LOT_FG,QTY from SUMMARY_TRACEABILITY_COMP where sub_sub_po='" & e.Node.Name & "'"
            Dim dtShowFG As DataTable = Database.GetData(queryShowFG)
            DataGridViewBawah.DataSource = dtShowFG
        End If
    End Sub

    Private Sub DataGridViewBawah_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridViewBawah.DataBindingComplete
        DataGridViewBawah.DefaultCellStyle.Font = New Font("Tahoma", 14)
        DataGridViewBawah.EnableHeadersVisualStyles = False
        With DataGridViewBawah.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Navy
            .ForeColor = Color.White
            .Font = New Font("Tahoma", 13, FontStyle.Bold)
            .Alignment = HorizontalAlignment.Center
            .Alignment = ContentAlignment.MiddleCenter
        End With
        DataGridViewBawah.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders

        For i As Integer = 0 To DataGridViewBawah.ColumnCount - 1
            DataGridViewBawah.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Next

        For Each col As DataGridViewColumn In DataGridViewBawah.Columns
            col.MinimumWidth = 300
        Next

        For i As Integer = 0 To DataGridViewBawah.RowCount - 1
            If DataGridViewBawah.Rows(i).Index Mod 2 = 0 Then
                DataGridViewBawah.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridViewBawah.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub DataGridViewAtas_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridViewAtas.DataBindingComplete
        DataGridViewAtas.DefaultCellStyle.Font = New Font("Tahoma", 14)
        DataGridViewAtas.EnableHeadersVisualStyles = False
        With DataGridViewAtas.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Navy
            .ForeColor = Color.White
            .Font = New Font("Tahoma", 13, FontStyle.Bold)
            .Alignment = HorizontalAlignment.Center
            .Alignment = ContentAlignment.MiddleCenter
        End With
        DataGridViewAtas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders

        For i As Integer = 0 To DataGridViewAtas.ColumnCount - 1
            DataGridViewAtas.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Next

        For Each col As DataGridViewColumn In DataGridViewAtas.Columns
            col.MinimumWidth = 300
        Next

        For i As Integer = 0 To DataGridViewAtas.RowCount - 1
            If DataGridViewAtas.Rows(i).Index Mod 2 = 0 Then
                DataGridViewAtas.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridViewAtas.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub
End Class