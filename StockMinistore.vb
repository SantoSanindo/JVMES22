Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar
Imports Microsoft.Office.Interop

Public Class StockMinistore
    Public Shared menu As String = "Stock Card Mini Store"

    Private Sub StockMinistore_Load(sender As Object, e As EventArgs) Handles Me.Load
        If globVar.view > 0 Then
            DGV_StockMiniststore("")
            ComboBox1.SelectedIndex = 0
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
                    queryInputStockDetail = "SELECT [MTS_NO], [STATUS],[MATERIAL], [INV_CTRL_DATE], [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [QTY], [ACTUAL_QTY], [FIFO], [LEVEL], [FLOW_TICKET] FROM STOCK_CARD where department='" & globVar.department & "' and status in ('Receive From Main Store','Receive From Production','Production Request','Return To Main Store') order by datetime_insert"
                    queryCB = "SELECT DISTINCT [SUB_SUB_PO] FROM STOCK_CARD where [SUB_SUB_PO] is not null"
                Else
                    queryInputStockDetail = "SELECT [MTS_NO],[STATUS],[MATERIAL], [INV_CTRL_DATE], [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [QTY], [ACTUAL_QTY], [FIFO], [LEVEL], [FLOW_TICKET] FROM STOCK_CARD  WHERE STATUS='" & status & "' and department='" & globVar.department & "' and status in ('Receive From Main Store','Receive From Production','Production Request','Return To Main Store') order by datetime_insert"
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
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGV_StockMiniststore()
        Try
            DataGridView1.DataSource = Nothing
            DataGridView1.Rows.Clear()
            DataGridView1.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryInputStockDetail As String = "SELECT [MTS_NO],[STATUS],[MATERIAL], [INV_CTRL_DATE], [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [QTY], [ACTUAL_QTY], [FIFO], [LEVEL], [FLOW_TICKET] FROM STOCK_CARD where sub_sub_po='" & ComboBox2.Text & "' and department='" & globVar.department & "' and status in ('Receive From Main Store','Receive From Production','Production Request','Return To Main Store') order by datetime_insert"
            Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
            DataGridView1.DataSource = dtInputStockDetail
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
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
            '.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If globVar.view > 0 Then
            If ComboBox1.SelectedIndex = 0 Then
                DGV_StockMiniststore("")
            Else
                DGV_StockMiniststore(ComboBox1.Text)
            End If
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If globVar.view > 0 Then
            DGV_StockMiniststore()
        End If
    End Sub

    Private Sub btn_ExportTrace1_Click(sender As Object, e As EventArgs) Handles btn_ExportTrace1.Click
        If globVar.view > 0 Then
            exportToExcel(DataGridView1)
        End If
    End Sub


    Private Sub exportToExcel(ByVal dgv As DataGridView)

        ' membuat objek Excel dan workbook baru
        Dim xlApp As New Excel.Application
        Dim xlWorkBook As Excel.Workbook = xlApp.Workbooks.Add
        Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets(1)

        ' mengisi data ke worksheet
        Dim dgvColumnIndex As Integer = 0
        For Each column As DataGridViewColumn In dgv.Columns
            dgvColumnIndex += 1
            xlWorkSheet.Cells(1, dgvColumnIndex) = column.HeaderText
        Next
        Dim dgvRowIndex As Integer = 0
        For Each row As DataGridViewRow In dgv.Rows
            dgvRowIndex += 1
            dgvColumnIndex = 0
            For Each cell As DataGridViewCell In row.Cells
                dgvColumnIndex += 1
                xlWorkSheet.Cells(dgvRowIndex + 1, dgvColumnIndex) = cell.Value
            Next
        Next

        ' menyimpan workbook ke file Excel
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "Excel Workbook|*.xlsx"
        saveFileDialog1.Title = "Save Excel File"
        saveFileDialog1.ShowDialog()

        If saveFileDialog1.FileName <> "" Then
            xlWorkBook.SaveAs(saveFileDialog1.FileName)
            RJMessageBox.Show("File saved successfully.")
        End If

        ' membersihkan objek Excel
        xlWorkBook.Close()
        xlApp.Quit()
        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)

    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub
End Class