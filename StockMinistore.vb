﻿Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar

Public Class StockMinistore
    Private Sub StockMinistore_Load(sender As Object, e As EventArgs) Handles Me.Load
        DGV_StockMiniststore("")
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
        If e.KeyData = Keys.Enter And TextBox1.Text <> "" Then
            Dim Found As Boolean = False
            Dim StringToSearch As String = ""
            Dim ValueToSearchFor As String = Me.TextBox1.Text.Trim.ToLower
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
                        StringToSearch = gRow.Cells(0).Value.ToString.Trim.ToLower
                        If InStr(1, StringToSearch, LCase(Trim(TextBox1.Text)), vbTextCompare) = 1 Then
                            Dim myCurrentCell As DataGridViewCell = gRow.Cells(0)
                            Dim myCurrentPosition As DataGridViewCell = gRow.Cells(0)
                            DataGridView1.CurrentCell = myCurrentCell
                            CurrentRowIndex = DataGridView1.CurrentRow.Index
                            Found = True
                        End If
                        If Found Then Exit For
                    Next
                End If
                If Found = False Then
                    MessageBox.Show("Data not found")
                    TextBox1.Text = ""
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub DGV_StockMiniststore(status As String)
        Try
            If status = "All" Then
                DGV_StockMiniststore("")
            Else
                DataGridView1.DataSource = Nothing
                DataGridView1.Rows.Clear()
                DataGridView1.Columns.Clear()
                Call Database.koneksi_database()
                Dim queryInputStockDetail As String
                Dim queryCB As String
                If status = "" Then
                    queryInputStockDetail = "SELECT [MTS_NO], [DEPARTMENT], [MATERIAL], [INV_CTRL_DATE], [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [QTY], [ACTUAL_QTY], [FIFO], [LEVEL], [FLOW_TICKET] FROM STOCK_CARD order by datetime_insert"
                    queryCB = "SELECT DISTINCT [SUB_SUB_PO] FROM STOCK_CARD where [SUB_SUB_PO] is not null"
                Else
                    queryInputStockDetail = "SELECT [MTS_NO], [DEPARTMENT], [MATERIAL], [INV_CTRL_DATE], [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [QTY], [ACTUAL_QTY], [FIFO], [LEVEL], [FLOW_TICKET] FROM STOCK_CARD  WHERE STATUS='" & status & "' and actual_qty > 0 order by datetime_insert"
                    queryCB = "SELECT DISTINCT [SUB_SUB_PO] FROM STOCK_CARD  WHERE STATUS='" & status & "' and [SUB_SUB_PO] is not null"
                End If

                Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
                DataGridView1.DataSource = dtInputStockDetail

                Dim dtMasterDepart As DataTable = Database.GetData(queryCB)

                For i As Integer = 0 To dtMasterDepart.Rows.Count - 1
                    ComboBox2.Items.Add(dtMasterDepart.Rows(i).Item(0))
                Next


            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGV_StockMiniststore()
        Try
            DataGridView1.DataSource = Nothing
            DataGridView1.Rows.Clear()
            DataGridView1.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryInputStockDetail As String = "SELECT [MTS_NO], [DEPARTMENT], [MATERIAL], [INV_CTRL_DATE], [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [QTY], [ACTUAL_QTY], [FIFO], [LEVEL], [FLOW_TICKET] FROM STOCK_CARD where sub_sub_po='" & ComboBox2.Text & "' order by datetime_insert"
            Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
            DataGridView1.DataSource = dtInputStockDetail
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 0 Then
            DGV_StockMiniststore("")
        Else
            DGV_StockMiniststore(ComboBox1.Text)
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        DGV_StockMiniststore()
    End Sub
End Class