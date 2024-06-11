Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class TraceabilityV3
    Public Shared menu As String = "Traceability"

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ShowDGVAtasV2()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Sub ShowDGVAtas()
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()

        Dim queryATAS As String = "WITH LatestData AS (
                SELECT
                    df.SUB_SUB_PO,
                    df.fg,
                    df.line,
                    MAX(df.DATETIME_INSERT) AS LatestInsert
                FROM
                    DONE_FG df
                JOIN PROD_DOP pd ON df.SUB_SUB_PO = pd.SUB_SUB_PO AND df.fg = pd.FG_PN
                WHERE
                    pd.FG_PN = '" & txtFGTraceability.Text & "'
                GROUP BY
                    df.SUB_SUB_PO,
                    df.fg,
                    df.line
            )
            SELECT DISTINCT
                df.SUB_SUB_PO [Sub Sub PO],
                df.fg [Finish Goods],
                df.line [Line],
                df.DATETIME_INSERT [Date Time],
                fga.INSPECTOR [Inspector],
                fga.PACKER1 [Packer 1],
                fga.PACKER2 [Packer 2],
                fga.PACKER3 [Packer 3],
                fga.PACKER4 [Packer 4],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 1 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 1],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 2 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 2],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 3 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 3],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 4 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 4],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 5 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 5],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 6 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 6],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 7 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 7],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 8 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 8],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 9 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 9],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 10 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 10],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 11 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 11],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 12 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 12],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 13 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 13],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 14 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 14],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 15 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 15],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 16 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 16],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 17 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 17],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 18 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 18],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 19 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 19],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 20 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 20],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 21 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 21],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 22 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 22],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 23 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 23],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 24 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 24],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 25 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 25],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 26 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 26],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 27 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 27],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 28 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 28],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 29 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 29],
                MAX(CASE WHEN pd.PROCESS_NUMBER = 30 THEN pd.process + '(' + pd.OPERATOR_ID + ')' ELSE NULL END) AS [Process 30]
            FROM
                LatestData ld
            JOIN DONE_FG df ON ld.SUB_SUB_PO = df.SUB_SUB_PO AND ld.fg = df.fg AND ld.line = df.line AND ld.LatestInsert = df.DATETIME_INSERT
            LEFT JOIN PROD_DOP pd ON df.SUB_SUB_PO = pd.SUB_SUB_PO AND df.fg = pd.FG_PN
            LEFT JOIN FGA fga ON fga.FG_PN = pd.FG_PN AND fga.SUB_SUB_PO = pd.SUB_SUB_PO
            WHERE
                pd.FG_PN = '" & txtFGTraceability.Text & "'
            GROUP BY
                df.SUB_SUB_PO,
                df.fg,
                df.line,
                df.DATETIME_INSERT,
                fga.INSPECTOR,
                fga.PACKER1,
                fga.PACKER2,
                fga.PACKER3,
                fga.PACKER4
            HAVING
                df.SUB_SUB_PO IS NOT NULL
                AND df.fg IS NOT NULL
                AND df.line IS NOT NULL
            ORDER BY
            	df.DATETIME_INSERT DESC;"
        Dim dtATAS As DataTable = Database.GetData(queryATAS)

        DataGridView1.DataSource = dtATAS

        Dim sub_sub_po As DataGridViewButtonColumn = New DataGridViewButtonColumn
        sub_sub_po.Name = "check"
        sub_sub_po.HeaderText = "Check"
        sub_sub_po.Width = 50
        sub_sub_po.Text = "Check"
        sub_sub_po.UseColumnTextForButtonValue = True
        DataGridView1.Columns.Insert(0, sub_sub_po)

        txtFGTraceability.Clear()
    End Sub

    Sub ShowDGVAtasV2()
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Call Database.koneksi_database()
        Dim sql As String = "select date [Date Time], sub_sub_po [Sub Sub PO], fg [Finish Goods], line [Line], INSPECTOR [Inspector],
                packer1 [Packer 1],
                packer2 [Packer 2],
                packer3 [Packer 3],
                packer4 [Packer 4],
                process1 [Process 1],
                process2 [Process 2],
                process3 [Process 3],
                process4 [Process 4],
                process5 [Process 5],
                process6 [Process 6],
                process7 [Process 7],
                process8 [Process 8],
                process9 [Process 9],
                process10 [Process 10],
                process11 [Process 11],
                process12 [Process 12],
                process13 [Process 13],
                process14 [Process 14],
                process15 [Process 15],
                process16 [Process 16],
                process17 [Process 17],
                process18 [Process 18],
                process19 [Process 19],
                process20 [Process 20],
                process21 [Process 21],
                process22 [Process 22],
                process23 [Process 23],
                process24 [Process 24],
                process25 [Process 25],
                process26 [Process 26],
                process27 [Process 27],
                process28 [Process 28],
                process29 [Process 29],
                process30 [Process 30]
            from summary_traceability where fg=" & txtFGTraceability.Text
        Dim dtTraceability As DataTable = Database.GetData(sql)
        DataGridView1.DataSource = dtTraceability

        Dim checkTraceability As DataGridViewButtonColumn = New DataGridViewButtonColumn
        checkTraceability.Name = "check"
        checkTraceability.HeaderText = "Check"
        checkTraceability.Width = 50
        checkTraceability.Text = "Check"
        checkTraceability.UseColumnTextForButtonValue = True
        DataGridView1.Columns.Insert(0, checkTraceability)

        txtFGTraceability.Clear()
    End Sub

    Private Sub txtFGTraceability_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtFGTraceability.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
            ShowDGVAtasV2()
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
            '.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.ColumnIndex >= 0 Then
            If DataGridView1.Columns(e.ColumnIndex).Name = "check" Then
                TextBox1.Clear()
                TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells("Sub Sub PO").Value & " | " & DataGridView1.Rows(e.RowIndex).Cells("Finish Goods").Value
                treeView_show(DataGridView1.Rows(e.RowIndex).Cells("Sub Sub PO").Value, DataGridView1.Rows(e.RowIndex).Cells("Finish Goods").Value)
            End If
        End If
    End Sub

    Private Sub treeView_show(sub_sub_po As String, fg As String)
        TreeView1.Nodes.Clear()
        Dim query As String = "SELECT component,desc_comp FROM prod_doc WHERE fg_pn='" & fg & "' AND sub_sub_po='" & sub_sub_po & "' order by component"
        Dim dt As DataTable = Database.GetData(query)

        TreeView1.Nodes.Add("Finish Goods : " & fg & " - SSP : " & sub_sub_po)

        For i = 0 To dt.Rows.Count - 1
            TreeView1.Nodes(0).Nodes.Add(dt.Rows(i).Item("component").ToString, "PN : " & dt.Rows(i).Item("component").ToString & " - Desc : " & dt.Rows(i).Item("desc_comp").ToString)
        Next
        TreeView1.ExpandAll()
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        If TreeView1.SelectedNode Is Nothing Then
            DataGridView2.DataSource = Nothing
            Exit Sub
        End If

        Dim id As String = TreeView1.SelectedNode.Name

        DGV_Bawah(id)
    End Sub

    Sub DGV_Bawah(material As String)
        If material = "" Then
            Dim VSplit() As String = TextBox1.Text.Split(" | ")
            Dim queryBAWAH As String = "select lot_fg [Lot FG], component [Material], [desc] [Desc], inv [INV], batch_no [Batch No], lot_comp [Lot Material], qty [Qty],remark [Remark] from summary_traceability_comp where sub_sub_po='" & VSplit(0) & "' order by ORDER BY CAST(SUBSTRING(lot_fg, 1, CHARINDEX(' ', lot_fg) - 1) AS INT)"
            Dim dtBAWAH As DataTable = Database.GetData(queryBAWAH)
            DataGridView2.DataSource = dtBAWAH
        Else
            Dim VSplit() As String = TextBox1.Text.Split(" | ")
            Dim queryBAWAH As String = "select lot_fg [Lot FG], component [Material], [desc] [Desc], inv [INV], batch_no [Batch No], lot_comp [Lot Material], qty [Qty],remark [Remark] from summary_traceability_comp where component='" & material & "' and sub_sub_po='" & VSplit(0) & "' ORDER BY CAST(SUBSTRING(lot_fg, 1, CHARINDEX(' ', lot_fg) - 1) AS INT)"
            Dim dtBAWAH As DataTable = Database.GetData(queryBAWAH)
            DataGridView2.DataSource = dtBAWAH
        End If
    End Sub

    Private Sub DataGridView2_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView2.DataBindingComplete
        For i As Integer = 0 To DataGridView2.RowCount - 1
            If DataGridView2.Rows(i).Index Mod 2 = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With DataGridView2
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

            '.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub
End Class