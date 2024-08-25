Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar
Imports Microsoft.Office.Interop

Public Class StockProduction
    Public Shared menu As String = "Stock Card Production"

    Private Sub StockMinistore_Load(sender As Object, e As EventArgs) Handles Me.Load
        DateTimePicker1.MaxDate = DateTime.Now

        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"

        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"

        btn_ExportTrace1.Enabled = False

        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub DGV_StockMiniststore()
        Try
            DG_SCMaterial.DataSource = Nothing
            DG_SCMaterial.Rows.Clear()
            DG_SCMaterial.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryInputStockDetail As String = "SELECT [datetime_insert] [Date Time], [STATUS] [Status], [MATERIAL] [Material], [INV_CTRL_DATE] [ICD], [TRACEABILITY] [Trace], [BATCH_NO] [Batch], [LOT_NO] [Lot], [FINISH_GOODS_PN] [FG], [PO], [SUB_PO] [SPO], [SUB_SUB_PO] [SSPO], [LINE] [Line], [QTY] [Qty], [ACTUAL_QTY] [Actual Qty], [FLOW_TICKET] [Flow Ticket], [QRCODE] [QRCode] FROM STOCK_CARD where CAST(datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "' and CAST(datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "' and department='" & globVar.department & "' and status in ('Production Process','Production Result') order by datetime_insert"
            Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
            DG_SCMaterial.DataSource = dtInputStockDetail

            SetEqualColumnWidths(DG_SCMaterial)
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGV_SA()
        Try
            DG_SCSA.DataSource = Nothing
            DG_SCSA.Rows.Clear()
            DG_SCSA.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryInputStockDetail As String = "SELECT [datetime_insert] [Date Time], [CODE_STOCK_PROD_SUB_ASSY] [QRCode], [FG] [Material], [INV_CTRL_DATE] [ICD], [TRACEABILITY] [Trace], [BATCH_NO] [Batch], [LOT_NO] [Lot], [PO], [SUB_SUB_PO] [SSPO], [QTY] [Qty] FROM STOCK_PROD_SUB_ASSY where CAST(datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "' and CAST(datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "' and department='" & globVar.department & "' order by datetime_insert"
            Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
            DG_SCSA.DataSource = dtInputStockDetail

            SetEqualColumnWidths(DG_SCSA)
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGV_Reject()
        Try
            DG_SCReject.DataSource = Nothing
            DG_SCReject.Rows.Clear()
            DG_SCReject.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryInputStockDetail As String = "SELECT [datetime_insert] [Date Time], [part_number] [Material], [INV_CTRL_DATE] [ICD], [TRACEABILITY] [Trace], [BATCH_NO] [Batch], [LOT_NO] [Lot], [FG_PN] [FG], [PO], [SUB_SUB_PO] [SSPO], [LINE] [Line], [QTY] [Qty Reject] FROM OUT_PROD_REJECT where CAST(datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "' and CAST(datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "' and department='" & globVar.department & "' order by datetime_insert"
            Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
            DG_SCReject.DataSource = dtInputStockDetail

            SetEqualColumnWidths(DG_SCReject)
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGV_Others()
        Try
            DG_SCOthers.DataSource = Nothing
            DG_SCOthers.Rows.Clear()
            DG_SCOthers.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryInputStockDetail As String = "SELECT [datetime_insert] [Date Time], [CODE_STOCK_PROD_OTHERS] [QRCode], [PART_NUMBER] [Material], [INV_CTRL_DATE] [ICD], [TRACEABILITY] [Trace], [BATCH_NO] [Batch], [LOT_NO] [Lot], [CODE_OUT_PROD_DEFECT] [CODE DEFECT], [QTY] [Qty] FROM STOCK_PROD_OTHERS where CAST(datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "' and CAST(datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "' and department='" & globVar.department & "' order by datetime_insert"
            Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
            DG_SCOthers.DataSource = dtInputStockDetail

            SetEqualColumnWidths(DG_SCOthers)
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGV_WIP()
        Try
            DG_SCWIPATAS.DataSource = Nothing
            DG_SCWIPATAS.Rows.Clear()
            DG_SCWIPATAS.Columns.Clear()
            Call Database.koneksi_database()
            'Dim queryInputStockDetail As String = "select * from (select DISTINCT [CODE_STOCK_PROD_WIP] [QRCode], [FG_PN] [FINISH GOODS], [PENGALI] [Qty] from STOCK_PROD_WIP where CAST(datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "' and CAST(datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "' and department='" & globVar.department & "') as listofwip ORDER BY cast(replace(QRCode,'WIP','') as int) desc"
            Dim queryInputStockDetail As String = "SELECT
	                                                    [QRCode],
	                                                    [FINISH GOODS],
	                                                    [Qty],
	                                                    CASE
		                                                    WHEN sum_qty > [Qty] THEN [Qty]
		                                                    ELSE 0
	                                                    END AS [Actual Qty]
                                                    FROM
	                                                    (
	                                                    SELECT DISTINCT
		                                                    [CODE_STOCK_PROD_WIP] AS [QRCode],
		                                                    [FG_PN] AS [FINISH GOODS],
		                                                    [PENGALI] AS [Qty],
                                                        SUM(qty) OVER (PARTITION BY [CODE_STOCK_PROD_WIP], [FG_PN], [PENGALI]) AS sum_qty
	                                                    FROM
		                                                    STOCK_PROD_WIP 
	                                                    WHERE
		                                                    CAST(datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "'
		                                                    AND CAST(datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "'
		                                                    AND department = '" & globVar.department & "' 
	                                                    ) AS listofwip 
                                                    ORDER BY
	                                                    CAST(REPLACE([QRCode], 'WIP', '') AS INT) DESC"
            Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
            DG_SCWIPATAS.DataSource = dtInputStockDetail

            Dim CheckStockWIP As DataGridViewButtonColumn = New DataGridViewButtonColumn
            CheckStockWIP.Name = "Check"
            CheckStockWIP.HeaderText = "Check"
            CheckStockWIP.Width = 50
            CheckStockWIP.Text = "Check"
            CheckStockWIP.UseColumnTextForButtonValue = True
            DG_SCWIPATAS.Columns.Insert(0, CheckStockWIP)

            SetEqualColumnWidths(DG_SCWIPATAS)
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGV_WIPBawah(code As String)
        Try
            DG_SCWIPBAWAH.DataSource = Nothing
            DG_SCWIPBAWAH.Rows.Clear()
            DG_SCWIPBAWAH.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryInputStockDetail As String = "select * from STOCK_PROD_WIP where CODE_STOCK_PROD_WIP = '" & code & "' and CAST(datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "' and CAST(datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "' and department='" & globVar.department & "' ORDER BY datetime_insert"
            Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
            DG_SCWIPBAWAH.DataSource = dtInputStockDetail

            SetEqualColumnWidths(DG_SCWIPBAWAH)
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGV_OH()
        Try
            DG_SCOHATAS.DataSource = Nothing
            DG_SCOHATAS.Rows.Clear()
            DG_SCOHATAS.Columns.Clear()
            Call Database.koneksi_database()
            'Dim queryInputStockDetail As String = "select * from (SELECT DISTINCT [CODE_STOCK_PROD_ONHOLD] [QRCode], [FG_PN] [FINISH GOODS], [PENGALI] [Qty] from STOCK_PROD_ONHOLD where CAST(datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "' and CAST(datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "' and department='" & globVar.department & "') as listofoh ORDER BY cast(replace(QRCode,'OH','') as int) desc"
            Dim queryInputStockDetail As String = "SELECT
	                                                    [QRCode],
	                                                    [FINISH GOODS],
	                                                    [Qty],
	                                                    CASE
		                                                    WHEN sum_qty > [Qty] THEN [Qty]
		                                                    ELSE 0
	                                                    END AS [Actual Qty]
                                                    FROM
	                                                    (
	                                                    SELECT DISTINCT
		                                                    [CODE_STOCK_PROD_ONHOLD] AS [QRCode],
		                                                    [FG_PN] AS [FINISH GOODS],
		                                                    [PENGALI] AS [Qty],
                                                        SUM(qty) OVER (PARTITION BY [CODE_STOCK_PROD_WIP], [FG_PN], [PENGALI]) AS sum_qty
	                                                    FROM
		                                                    STOCK_PROD_ONHOLD 
	                                                    WHERE
		                                                    CAST(datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "'
		                                                    AND CAST(datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "'
		                                                    AND department = '" & globVar.department & "' 
	                                                    ) AS listofoh 
                                                    ORDER BY
	                                                    CAST(REPLACE([QRCode], 'OH', '') AS INT) DESC"
            Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
            DG_SCOHATAS.DataSource = dtInputStockDetail

            Dim CheckStockOH As DataGridViewButtonColumn = New DataGridViewButtonColumn
            CheckStockOH.Name = "Check"
            CheckStockOH.HeaderText = "Check"
            CheckStockOH.Width = 50
            CheckStockOH.Text = "Check"
            CheckStockOH.UseColumnTextForButtonValue = True
            DG_SCWIPATAS.Columns.Insert(0, CheckStockOH)

            SetEqualColumnWidths(DG_SCOHATAS)
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGV_OHBawah(code As String)
        Try
            DG_SCOHBAWAH.DataSource = Nothing
            DG_SCOHBAWAH.Rows.Clear()
            DG_SCOHBAWAH.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryInputStockDetail As String = "select * from STOCK_PROD_ONHOLD where CODE_STOCK_PROD_ONHOLD = '" & code & "' and CAST(datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "' and CAST(datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "' and department='" & globVar.department & "' ORDER BY datetime_insert"
            Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
            DG_SCOHBAWAH.DataSource = dtInputStockDetail

            SetEqualColumnWidths(DG_SCOHBAWAH)
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGV_Defect()
        Try
            DG_SCDEFECTATAS.DataSource = Nothing
            DG_SCDEFECTATAS.Rows.Clear()
            DG_SCDEFECTATAS.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryInputStockDetail As String = "SELECT * FROM (SELECT DISTINCT [CODE_OUT_PROD_DEFECT] [QRCode], [sub_sub_po] [SUB SUB PO], FG_PN [FINISH GOODS], [PENGALI] [Qty] FROM out_prod_defect where CAST(datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "' and CAST(datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "' and department='" & globVar.department & "') AS LISTOFDEFECT order by cast(replace(QRCode,'D','') as int) desc"
            Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
            DG_SCDEFECTATAS.DataSource = dtInputStockDetail

            Dim CheckStockDefect As DataGridViewButtonColumn = New DataGridViewButtonColumn
            CheckStockDefect.Name = "Check"
            CheckStockDefect.HeaderText = "Check"
            CheckStockDefect.Width = 50
            CheckStockDefect.Text = "Check"
            CheckStockDefect.UseColumnTextForButtonValue = True
            DG_SCDEFECTATAS.Columns.Insert(0, CheckStockDefect)

            SetEqualColumnWidths(DG_SCDEFECTATAS)
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGV_DefectBawah(code As String)
        Try
            DG_SCDEFECTBAWAH.DataSource = Nothing
            DG_SCDEFECTBAWAH.Rows.Clear()
            DG_SCDEFECTBAWAH.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryInputStockDetail As String = "SELECT * FROM out_prod_defect where CODE_OUT_PROD_DEFECT='" & code & "' and CAST(datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "' and CAST(datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "' and department='" & globVar.department & "' order by datetime_insert"
            Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
            DG_SCDEFECTBAWAH.DataSource = dtInputStockDetail

            SetEqualColumnWidths(DG_SCDEFECTBAWAH)
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGV_Return()
        Try
            DG_SCReturn.DataSource = Nothing
            DG_SCReturn.Rows.Clear()
            DG_SCReturn.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryInputStockDetail As String = "SELECT [datetime_insert] [Date Time], [qrcode] [QRCode], [material] [Material], [INV_CTRL_DATE] [ICD], [TRACEABILITY] [Trace], [BATCH_NO] [Batch], [LOT_NO] [Lot], [PO], [SUB_SUB_PO] [SSPO], [QTY] [Qty], actual_qty [Actual Qty] FROM STOCK_CARD where CAST(datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "' and CAST(datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "' and department='" & globVar.department & "' and status='Return to Mini Store' order by datetime_insert"
            Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
            DG_SCReturn.DataSource = dtInputStockDetail

            SetEqualColumnWidths(DG_SCReturn)
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub SetEqualColumnWidths(ByVal dgv As DataGridView)
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

        Dim totalWidth As Integer = dgv.ClientSize.Width
        Dim columnCount As Integer = dgv.ColumnCount
        Dim equalWidth As Integer = Math.Floor(totalWidth / columnCount)

        For Each column As DataGridViewColumn In dgv.Columns
            column.Width = equalWidth
        Next

        Dim remainingWidth As Integer = totalWidth - (equalWidth * columnCount)
        For i As Integer = 0 To remainingWidth - 1
            dgv.Columns(i).Width += 1
        Next
    End Sub

    Private Sub DG_SCMaterial_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DG_SCMaterial.DataBindingComplete
        For i As Integer = 0 To DG_SCMaterial.RowCount - 1
            If DG_SCMaterial.Rows(i).Index Mod 2 = 0 Then
                DG_SCMaterial.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DG_SCMaterial.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With DG_SCMaterial
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

    Private Sub DG_SCSA_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DG_SCSA.DataBindingComplete
        For i As Integer = 0 To DG_SCSA.RowCount - 1
            If DG_SCSA.Rows(i).Index Mod 2 = 0 Then
                DG_SCSA.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DG_SCSA.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With DG_SCSA
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

    Private Sub DG_SCOthers_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DG_SCOthers.DataBindingComplete
        For i As Integer = 0 To DG_SCOthers.RowCount - 1
            If DG_SCOthers.Rows(i).Index Mod 2 = 0 Then
                DG_SCOthers.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DG_SCOthers.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With DG_SCOthers
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

    Private Sub DG_SCReject_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DG_SCReject.DataBindingComplete
        For i As Integer = 0 To DG_SCReject.RowCount - 1
            If DG_SCReject.Rows(i).Index Mod 2 = 0 Then
                DG_SCReject.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DG_SCReject.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With DG_SCReject
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

    Private Sub DG_SCWIPATAS_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DG_SCWIPATAS.DataBindingComplete
        For i As Integer = 0 To DG_SCWIPATAS.RowCount - 1
            If DG_SCWIPATAS.Rows(i).Index Mod 2 = 0 Then
                DG_SCWIPATAS.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DG_SCWIPATAS.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With DG_SCWIPATAS
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

    Private Sub DG_SCWIPBAWAH_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DG_SCWIPBAWAH.DataBindingComplete
        For i As Integer = 0 To DG_SCWIPBAWAH.RowCount - 1
            If DG_SCWIPBAWAH.Rows(i).Index Mod 2 = 0 Then
                DG_SCWIPBAWAH.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DG_SCWIPBAWAH.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With DG_SCWIPBAWAH
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

    Private Sub DG_SCOHATAS_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DG_SCOHATAS.DataBindingComplete
        For i As Integer = 0 To DG_SCOHATAS.RowCount - 1
            If DG_SCOHATAS.Rows(i).Index Mod 2 = 0 Then
                DG_SCOHATAS.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DG_SCOHATAS.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With DG_SCOHATAS
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

    Private Sub DG_SCOHBAWAH_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DG_SCOHBAWAH.DataBindingComplete
        For i As Integer = 0 To DG_SCOHBAWAH.RowCount - 1
            If DG_SCOHBAWAH.Rows(i).Index Mod 2 = 0 Then
                DG_SCOHBAWAH.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DG_SCOHBAWAH.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With DG_SCOHBAWAH
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

    Private Sub DG_SCDEFECTATAS_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DG_SCDEFECTATAS.DataBindingComplete
        For i As Integer = 0 To DG_SCDEFECTATAS.RowCount - 1
            If DG_SCDEFECTATAS.Rows(i).Index Mod 2 = 0 Then
                DG_SCDEFECTATAS.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DG_SCDEFECTATAS.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With DG_SCDEFECTATAS
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

    Private Sub DG_SCDEFECTBAWAH_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DG_SCDEFECTBAWAH.DataBindingComplete
        For i As Integer = 0 To DG_SCDEFECTBAWAH.RowCount - 1
            If DG_SCDEFECTBAWAH.Rows(i).Index Mod 2 = 0 Then
                DG_SCDEFECTBAWAH.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DG_SCDEFECTBAWAH.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With DG_SCDEFECTBAWAH
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

    Private Sub DG_SCReturn_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DG_SCReturn.DataBindingComplete
        For i As Integer = 0 To DG_SCReturn.RowCount - 1
            If DG_SCReturn.Rows(i).Index Mod 2 = 0 Then
                DG_SCReturn.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DG_SCReturn.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With DG_SCReturn
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

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs)
        If globVar.view > 0 Then
            DGV_StockMiniststore()
        End If
    End Sub

    Private Sub btn_ExportTrace1_Click(sender As Object, e As EventArgs) Handles btn_ExportTrace1.Click
        If globVar.view > 0 Then
            ' Tampilkan ProgressBar
            ProgressBar1.Visible = True
            ProgressBar1.Style = ProgressBarStyle.Marquee
            ProgressBar1.MarqueeAnimationSpeed = 30

            ' Nonaktifkan tombol selama ekspor
            btn_ExportTrace1.Enabled = False

            ' Jalankan backgroundWorker

            If TabControl1.SelectedTab.Text = "Stock Card Sub Assy" Then

                BackgroundWorker1.RunWorkerAsync(DG_SCSA)

            ElseIf TabControl1.SelectedTab.Text = "Stock Card Reject" Then

                BackgroundWorker1.RunWorkerAsync(DG_SCReject)

            ElseIf TabControl1.SelectedTab.Text = "Stock Card Others" Then

                BackgroundWorker1.RunWorkerAsync(DG_SCOthers)

            ElseIf TabControl1.SelectedTab.Text = "Stock Card Return" Then

                BackgroundWorker1.RunWorkerAsync(DG_SCReturn)

            Else

                BackgroundWorker1.RunWorkerAsync(DG_SCMaterial)

            End If

        End If
    End Sub

    Private Sub exportToExcel(ByVal dgv As DataGridView)
        btn_ExportTrace1.Enabled = False

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

        ProgressBar1.Visible = False
        btn_ExportTrace1.Enabled = True
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If globVar.view > 0 Then
            If TabControl1.SelectedTab.Text = "Stock Card Sub Assy" Then

                DGV_SA()

            ElseIf TabControl1.SelectedTab.Text = "Stock Card WIP" Then

                DGV_WIP()

            ElseIf TabControl1.SelectedTab.Text = "Stock Card On Hold" Then

                DGV_OH()

            ElseIf TabControl1.SelectedTab.Text = "Stock Card Defect" Then

                DGV_Defect()

            ElseIf TabControl1.SelectedTab.Text = "Stock Card Reject" Then

                DGV_Reject()

            ElseIf TabControl1.SelectedTab.Text = "Stock Card Others" Then

                DGV_Others()

            Else

                DGV_StockMiniststore()

            End If
            btn_ExportTrace1.Enabled = True
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker2.MinDate = DateTimePicker1.Value
        DateTimePicker2.MaxDate = DateTime.Now.AddDays(1)
        btn_ExportTrace1.Enabled = False

        clearDG()
    End Sub

    Sub clearDG()
        DG_SCMaterial.DataSource = Nothing
        DG_SCMaterial.Rows.Clear()
        DG_SCMaterial.Columns.Clear()

        DG_SCSA.DataSource = Nothing
        DG_SCSA.Rows.Clear()
        DG_SCSA.Columns.Clear()

        DG_SCWIPATAS.DataSource = Nothing
        DG_SCWIPATAS.Rows.Clear()
        DG_SCWIPATAS.Columns.Clear()

        DG_SCWIPBAWAH.DataSource = Nothing
        DG_SCWIPBAWAH.Rows.Clear()
        DG_SCWIPBAWAH.Columns.Clear()

        DG_SCOHATAS.DataSource = Nothing
        DG_SCOHATAS.Rows.Clear()
        DG_SCOHATAS.Columns.Clear()

        DG_SCOHBAWAH.DataSource = Nothing
        DG_SCOHBAWAH.Rows.Clear()
        DG_SCOHBAWAH.Columns.Clear()

        DG_SCDEFECTATAS.DataSource = Nothing
        DG_SCDEFECTATAS.Rows.Clear()
        DG_SCDEFECTATAS.Columns.Clear()

        DG_SCDEFECTBAWAH.DataSource = Nothing
        DG_SCDEFECTBAWAH.Rows.Clear()
        DG_SCDEFECTBAWAH.Columns.Clear()

        DG_SCReject.DataSource = Nothing
        DG_SCReject.Rows.Clear()
        DG_SCReject.Columns.Clear()

        DG_SCOthers.DataSource = Nothing
        DG_SCOthers.Rows.Clear()
        DG_SCOthers.Columns.Clear()
    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        e.Handled = True
    End Sub

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        e.Handled = True
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        btn_ExportTrace1.Enabled = False
        clearDG()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim dgv As DataGridView = CType(e.Argument, DataGridView)

        ' Membuat objek Excel dan workbook baru
        Dim xlApp As New Excel.Application
        Dim xlWorkBook As Excel.Workbook = xlApp.Workbooks.Add
        Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets(1)

        ' Mengisi data ke worksheet
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

        ' Menyimpan workbook ke file Excel
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "Excel Workbook|*.xlsx"
        saveFileDialog1.Title = "Save Excel File"

        ' Gunakan Invoke untuk berinteraksi dengan UI thread
        Dim desktopPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        Dim fileName As String = "Export_Stock_Production_" & DateTime.Now.ToString("yyyyMMdd") & ".xlsx"
        Dim filePath As String = System.IO.Path.Combine(desktopPath, fileName)
        xlWorkBook.SaveAs(filePath)
        RJMessageBox.Show("Success Export Stock Production and save into " & filePath, "Info", MessageBoxButtons.OK)

        ' Membersihkan objek Excel
        xlWorkBook.Close()
        xlApp.Quit()
        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)
    End Sub

    Private Sub backgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        ProgressBar1.Visible = False
        btn_ExportTrace1.Enabled = True
    End Sub

    Private Sub TabControl1_Click(sender As Object, e As EventArgs) Handles TabControl1.Click

        If TabControl1.SelectedTab.Text = "Stock Card Sub Assy" Then

            DGV_SA()

        ElseIf TabControl1.SelectedTab.Text = "Stock Card WIP" Then

            DGV_WIP()

        ElseIf TabControl1.SelectedTab.Text = "Stock Card On Hold" Then

            DGV_OH()

        ElseIf TabControl1.SelectedTab.Text = "Stock Card Defect" Then

            DGV_Defect()

        ElseIf TabControl1.SelectedTab.Text = "Stock Card Reject" Then

            DGV_Reject()

        ElseIf TabControl1.SelectedTab.Text = "Stock Card Others" Then

            DGV_Others()

        ElseIf TabControl1.SelectedTab.Text = "Stock Card Return" Then

            DGV_Return()

        Else

            DGV_StockMiniststore()

        End If
    End Sub

    Private Sub DG_SCWIPATAS_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG_SCWIPATAS.CellClick

        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If e.ColumnIndex = -1 Then
            Exit Sub
        End If

        If DG_SCWIPATAS.Columns(e.ColumnIndex).Name = "Check" Then

            DGV_WIPBawah(DG_SCWIPATAS.Rows(e.RowIndex).Cells("QRCode").Value)

        End If

    End Sub

    Private Sub DG_SCOHATAS_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG_SCOHATAS.CellClick

        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If e.ColumnIndex = -1 Then
            Exit Sub
        End If

        If DG_SCOHATAS.Columns(e.ColumnIndex).Name = "Check" Then

            DGV_OHBawah(DG_SCOHATAS.Rows(e.RowIndex).Cells("QRCode").Value)

        End If

    End Sub

    Private Sub DG_SCDEFECTATAS_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG_SCDEFECTATAS.CellClick

        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If e.ColumnIndex = -1 Then
            Exit Sub
        End If

        If DG_SCDEFECTATAS.Columns(e.ColumnIndex).Name = "Check" Then

            DGV_DefectBawah(DG_SCDEFECTATAS.Rows(e.RowIndex).Cells("QRCode").Value)

        End If

    End Sub
End Class