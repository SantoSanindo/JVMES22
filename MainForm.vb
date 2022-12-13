Imports ZXing
Imports ZXing.Common
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data.SqlClient
Imports System.Drawing

Public Class MainForm
    Private Sub btn_input_stock_Click(sender As Object, e As EventArgs) Handles btn_input_stock.Click
        If txt_mainform_mts_no.Text = "" Then
            MessageBox.Show("MTS No cannot be null.")
            txt_mainform_mts_no.Select()
        Else
            Dim inputstockform = New FormInputStock()
            inputstockform.Show()
        End If
    End Sub

    Private Sub txt_mainform_mts_no_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_mainform_mts_no.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            If txt_mainform_mts_no.Text = "" Then
                MessageBox.Show("MTS No cannot be null.")
                txt_mainform_mts_no.Select()
            Else
                Dim inputstockform = New FormInputStock()
                inputstockform.Show()
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Dim mastermaterial = New MasterMaterial()
        mastermaterial.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim masterprocess = New MasterProcess()
        masterprocess.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Dim masterfinishgoods = New MaterialUsageFinishGoods()
        masterfinishgoods.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Dim masterprocessflow = New MasterProcessFlow()
        masterprocessflow.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        'Dim praproduction = New PraProduction()
        'praproduction.Show()
    End Sub

    Private Sub TabControl1_Click(sender As Object, e As EventArgs) Handles TabControl1.Click
        tampilDataComboboxLine()
        txt_mainform_production_po.Text = ""

        If TabControl1.SelectedIndex = 0 Then
            If globVar.hakAkses = "operator" Then
                MessageBox.Show("Cannot access this menu")
                TabControl1.SelectedIndex = 1
                scan_comp.Visible = True
                scan_comp.Text = ""
                last_scan_comp.Visible = False
                last_scan_comp.Text = ""
            End If
        End If
    End Sub

    Sub tampilDataComboboxLine()
        cb_mainform_production_line.Items.Clear()
        cb_mainform_production_line.Items.Add("Line 1")
        cb_mainform_production_line.Items.Add("Line 2")
        cb_mainform_production_line.Items.Add("Line 3")
        cb_mainform_production_line.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cb_mainform_production_line.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Public Sub dgv_doc()
        DOC.DataSource = Nothing
        DOC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DOC.Rows.Clear()
        DOC.Columns.Clear()
        If cb_mainform_production_line.Text <> "" And txt_mainform_production_po.Text <> "" Then
            Call Database.koneksi_database()
            Dim dtDOC As DataTable
            Dim query As String = "select comp COMP,pn PN,[check] [CHECK],(select count(number) from reject where po=" & txt_mainform_production_po.Text & " and line='" & cb_mainform_production_line.Text & "' and process in ( SELECT master_process FROM master_finish_goods WHERE material_part_number = pn AND part_number in (select fg_pn from pre_production where no_po=" & txt_mainform_production_po.Text & " ))) as REJECT, last_check LAST_CHECK from production_doc where line='" & cb_mainform_production_line.Text & "' and po=" & txt_mainform_production_po.Text
            'Dim query As String = "select comp COMP,pn PN,[check] [CHECK],reject REJECT, last_check LAST_CHECK from production_doc where line='" & cb_mainform_production_line.Text & "' and po=" & txt_mainform_production_po.Text
            'Dim query As String = "select comp COMP, pn PN,[check] [CHECK],Case When (Select qty from reject where line='" & cb_mainform_production_line.Text & "' and po=" & txt_mainform_production_po.Text & " and process=(select master_process from master_finish_goods where material_part_number=pn))> 0 then (select qty from reject where line='" & cb_mainform_production_line.Text & "' and po=" & txt_mainform_production_po.Text & " and process=(select master_process from master_finish_goods where material_part_number=pn)) else 0 end as REJECT, last_check LAST_CHECK from production_doc where line='" & cb_mainform_production_line.Text & "' and po=" & txt_mainform_production_po.Text

            dtDOC = Database.GetData(query)

            DOC.DataSource = dtDOC

            For i As Integer = 0 To DOC.Rows.Count - 1
                If DOC.Rows(i).Cells(2).Value = 1 Then
                    DOC.Rows(i).Cells(2).Style.BackColor = Color.LightBlue
                End If

                If DOC.Rows(i).Cells(4).Value = 1 Then
                    DOC.Rows(i).Cells(4).Style.BackColor = Color.LightBlue
                End If
            Next i
        End If
    End Sub

    Public Sub dgv_dop()
        DOP.DataSource = Nothing
        DOP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DOP.Rows.Clear()
        DOP.Columns.Clear()
        If cb_mainform_production_line.Text <> "" And txt_mainform_production_po.Text <> "" Then
            Call Database.koneksi_database()
            Dim dtDOP As DataTable = Database.GetData("SELECT process PROCESS,operator OPERATOR,date DATE FROM PRODUCTION_DOP WHERE LINE='" & cb_mainform_production_line.Text & "' and po=" & txt_mainform_production_po.Text)

            DOP.DataSource = dtDOP
        End If
    End Sub

    Private Sub txt_mainform_production_po_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txt_mainform_production_po.PreviewKeyDown
        Call Database.koneksi_database()

        Dim adapter As SqlDataAdapter
        Dim ds As New DataSet

        If e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter Then
            If cb_mainform_production_line.Text <> "" And txt_mainform_production_po.Text <> "" Then
                Dim sql As String = "SELECT * FROM sub_po where po_no=" & txt_mainform_production_po.Text & " and line='" & cb_mainform_production_line.Text & "'"
                adapter = New SqlDataAdapter(sql, Database.koneksi)
                adapter.Fill(ds)

                If ds.Tables(0).Rows.Count > 0 Then
                    qty_po.Text = ds.Tables(0).Rows(0).Item("QTY").ToString
                    Dim adapterCek As SqlDataAdapter
                    Dim dsCek As New DataSet
                    Dim sqlCek As String = "SELECT * FROM production_doc where po=" & txt_mainform_production_po.Text & " and line='" & cb_mainform_production_line.Text & "'"
                    adapterCek = New SqlDataAdapter(sqlCek, Database.koneksi)
                    adapterCek.Fill(dsCek)
                    If dsCek.Tables(0).Rows.Count > 0 Then
                        dgv_doc()
                    Else
                        Dim queryInsertDOC As String = "insert into production_doc (comp,pn,line,po) select mm.name,mfg.material_part_number,sp.line,p.no_po from PRE_PRODUCTION p, master_finish_goods mfg, master_material mm,sub_po sp where p.no_po=" & txt_mainform_production_po.Text & " and p.fg_pn=mfg.part_number and mfg.material_part_number is not null and mm.part_number=mfg.material_part_number and sp.po_no=p.no_po"
                        Dim cmdInsertDOC = New SqlCommand(queryInsertDOC, Database.koneksi)
                        If cmdInsertDOC.ExecuteNonQuery() Then
                            DOC.DataSource = Nothing
                            dgv_doc()
                        End If
                    End If

                    Dim adapterCek2 As SqlDataAdapter
                    Dim dsCek2 As New DataSet
                    Dim sqlCek2 As String = "SELECT * FROM production_dop where po=" & txt_mainform_production_po.Text & " and line='" & cb_mainform_production_line.Text & "'"
                    adapterCek2 = New SqlDataAdapter(sqlCek2, Database.koneksi)
                    adapterCek2.Fill(dsCek2)
                    If dsCek2.Tables(0).Rows.Count > 0 Then
                        dgv_dop()
                    Else
                        Dim queryInsertDOP As String = "insert into production_dop (po,line,process) select p.no_po,sp.line,mpf.master_proc from PRE_PRODUCTION p, master_process_flow mpf, sub_po sp where p.no_po=" & txt_mainform_production_po.Text & " and p.fg_pn=mpf.master_finish_goods and sp.po_no=p.no_po and mpf.need=1"
                        Dim cmdInsertDOP = New SqlCommand(queryInsertDOP, Database.koneksi)
                        If cmdInsertDOP.ExecuteNonQuery() Then
                            DOP.DataSource = Nothing
                            dgv_dop()
                        End If
                    End If
                    btnPrint.Enabled = True

                    scan_comp.Select()
                Else
                    MessageBox.Show("Sorry. PO Not Valid or Wrong Line.")
                    txt_mainform_production_po.Text = ""
                    dgv_dop()
                    dgv_doc()
                End If
            End If
        End If
    End Sub

    Private Sub scan_comp_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles scan_comp.PreviewKeyDown
        If e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter Then

            Dim tampung As String = ""
            For i As Integer = 0 To DOP.Rows.Count - 1
                If IsDBNull(DOP.Rows(i).Cells(1).Value) Then
                    tampung &= i
                End If
            Next i

            If tampung <> "" Then
                MessageBox.Show("Name of Operator is null.")
                scan_comp.Text = ""
            Else
                If scan_comp.Text <> "" Then
                    If IsNumeric(scan_comp.Text) Then
                        Dim adapterCek As SqlDataAdapter
                        Dim adapterCekafterUpdate As SqlDataAdapter
                        Dim dsCek As New DataSet
                        Dim dsCekafterUpdate As New DataSet
                        Dim sqlCek As String = "SELECT * FROM production_doc where po=" & txt_mainform_production_po.Text & " and line='" & cb_mainform_production_line.Text & "' and pn=" & scan_comp.Text
                        adapterCek = New SqlDataAdapter(sqlCek, Database.koneksi)
                        adapterCek.Fill(dsCek)
                        If dsCek.Tables(0).Rows.Count > 0 Then
                            If dsCek.Tables(0).Rows(0).Item("CHECK") = 1 Then
                                MessageBox.Show("Comp already scan")
                                dgv_doc()
                                scan_comp.Text = ""
                            Else
                                Dim queryUpdate As String = "update production_doc set [check]=1,datetime_update=getdate() where number=" & dsCek.Tables(0).Rows(0).Item("NUMBER")
                                Dim cmdUpdate = New SqlCommand(queryUpdate, Database.koneksi)
                                If cmdUpdate.ExecuteNonQuery() Then
                                    Dim sqlCekafterUpdate As String = "SELECT * FROM production_doc where po=" & txt_mainform_production_po.Text & " and line='" & cb_mainform_production_line.Text & "' and [check]=0"
                                    adapterCekafterUpdate = New SqlDataAdapter(sqlCekafterUpdate, Database.koneksi)
                                    adapterCekafterUpdate.Fill(dsCekafterUpdate)
                                    If dsCekafterUpdate.Tables(0).Rows.Count = 0 Then
                                        MessageBox.Show("Automatic Printing")
                                    End If

                                    dgv_doc()

                                    scan_comp.Text = ""
                                End If
                            End If
                        End If
                    Else
                        MessageBox.Show("Wrong Component")
                    End If
                Else
                    MessageBox.Show("Wrong Component")
                End If
            End If
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'Dim tampung As String = ""
        'For i As Integer = 0 To DOP.Rows.Count - 1
        '    If DOP.Rows(i).Cells(1).Value Is Nothing Then
        '        tampung &= i
        '    End If
        'Next i

        'If tampung <> "" Then
        '    MessageBox.Show("Name of Operator is null.")
        'Else
        '    MessageBox.Show("print")
        'End If

        If DOC.Rows.Count > 0 Then
            For i As Integer = 0 To DOC.Rows.Count - 1
                If DOC.Rows(i).Cells(2).Value = 0 Then
                    MessageBox.Show("Cannot Insert Reject before scan comp")
                    Exit Sub
                End If

                If DOC.Rows(i).Cells(4).Value = 1 Then
                    MessageBox.Show("Cannot Insert Reject after last scan")
                    Exit Sub
                End If
            Next i

            Dim reject = New FormReject()
            reject.Show()
        End If
    End Sub

    Private Sub DOP_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DOP.CellValueChanged
        If e.ColumnIndex = 1 Then
            If DOP.Rows(e.RowIndex).Cells(1).Value IsNot Nothing Then
                Dim queryUpdate As String = "update production_dop set operator='" & DOP.Rows(e.RowIndex).Cells(1).Value & "',datetime_update=getdate() where process='" & DOP.Rows(e.RowIndex).Cells(0).Value & "' and po=" & txt_mainform_production_po.Text & " and line='" & cb_mainform_production_line.Text & "'"
                Dim cmdUpdate = New SqlCommand(queryUpdate, Database.koneksi)
                If cmdUpdate.ExecuteNonQuery() Then
                    dgv_dop()
                End If
            End If
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        printData()
    End Sub

    Sub printData()
        Dim appXL As Excel.Application
        Dim wbXl As Excel.Workbook
        Dim shXL As Excel.Worksheet
        Dim raXL As Excel.Range

        ' Start Excel and get Application object.
        appXL = CreateObject("Excel.Application")
        appXL.Visible = False

        ' Add a new workbook.
        wbXl = appXL.Workbooks.Add
        shXL = wbXl.ActiveSheet

        ' Add table headers going cell by cell.
        shXL.Cells(1, 1).Value = "PT. Jovan Technologies"
        formatCell(shXL, 1, 1, 1, 5, Excel.Constants.xlCenter, Excel.Constants.xlCenter)

        shXL.Cells(2, 1).Value = "PRODUCTION FLOW TICKET"
        formatCell(shXL, 2, 1, 1, 5, Excel.Constants.xlCenter, Excel.Constants.xlCenter)

        'shXL.Cells(1, 5).Value = ""
        'formatCell(shXL, 1, 1, 1, 4, Excel.Constants.xlCenter, Excel.Constants.xlCenter)

        shXL.Cells(1, 6).Value = "LINE : " + cb_mainform_production_line.Text
        formatCell(shXL, 1, 6, 2, 2, Excel.Constants.xlLeft, Excel.Constants.xlCenter)

        shXL.Cells(1, 8).Value = "PLUG"
        formatCell(shXL, 1, 8, 1, 2, Excel.Constants.xlCenter, Excel.Constants.xlCenter)

        shXL.Cells(2, 8).Value = "GANG UP"
        formatCell(shXL, 2, 8, 1, 2, Excel.Constants.xlCenter, Excel.Constants.xlCenter)

        shXL.Cells(3, 1).Value = "Lot : "
        formatCell(shXL, 3, 1, 1, 2, Excel.Constants.xlLeft, Excel.Constants.xlCenter)

        shXL.Cells(3, 3).Value = "Of : "
        formatCell(shXL, 3, 3, 1, 1, Excel.Constants.xlLeft, Excel.Constants.xlCenter)

        shXL.Cells(4, 1).Value = "P/N"
        formatCell(shXL, 4, 1, 2, 1, Excel.Constants.xlLeft, Excel.Constants.xlTop)

        shXL.Cells(4, 2).Value = ""     'input PN
        formatCell(shXL, 4, 2, 2, 4, Excel.Constants.xlCenter, Excel.Constants.xlCenter)

        shXL.Cells(3, 5).Value = "PO       "
        formatCell(shXL, 3, 5, 1, 1, Excel.Constants.xlLeft, Excel.Constants.xlTop)

        shXL.Cells(4, 5).Value = txt_mainform_production_po.Text   'input PO
        formatCell(shXL, 4, 5, 2, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)

        shXL.Cells(3, 6).Value = "SR       "
        formatCell(shXL, 3, 6, 3, 1, Excel.Constants.xlLeft, Excel.Constants.xlTop)

        shXL.Cells(3, 7).Value = "LEAD FREE"     'input SR
        formatCell(shXL, 3, 7, 3, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)

        shXL.Cells(3, 8).Value = "Total Qty"
        formatCell(shXL, 3, 8, 1, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)

        shXL.Cells(4, 8).Value = ""     'input Total Qty
        formatCell(shXL, 4, 8, 2, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)

        shXL.Cells(3, 9).Value = "Qty/Lot"
        formatCell(shXL, 3, 9, 1, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)

        shXL.Cells(4, 9).Value = "32"     'input Qty/Lot
        formatCell(shXL, 4, 9, 2, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)

        shXL.Cells(6, 1).Value = "4 Tray + 4 Cover"     'input tray/cover
        formatCell(shXL, 6, 1, 1, 4, Excel.Constants.xlLeft, Excel.Constants.xlCenter)

        shXL.Cells(6, 5).Value = "Date Code : " + DateTime.Now.ToString("yyyy/MM/dd")
        formatCell(shXL, 6, 5, 1, 5, Excel.Constants.xlLeft, Excel.Constants.xlCenter)


        ''''''''''''''tabel 1
        ''''header
        shXL.Range("A9:I9").Interior.Color = Color.LightGray
        shXL.Cells(8, 1).Value = "DETAILS OF COMPONENTS"
        formatCell(shXL, 8, 1, 1, 9, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        Dim rowCount As Int32 = 9
        shXL.Cells(rowCount, 1).Value = "S/N"
        formatCell(shXL, rowCount, 1, 1, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        shXL.Cells(rowCount, 2).Value = "COMP"
        formatCell(shXL, rowCount, 2, 1, 5, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        shXL.Cells(rowCount, 7).Value = "P/N"
        formatCell(shXL, rowCount, 7, 1, 3, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        'shXL.Cells(rowCount, 5).Value = "INV. NO"
        'formatCell(shXL, rowCount, 5, 1, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        'shXL.Cells(rowCount, 6).Value = "ST. NO"
        'formatCell(shXL, rowCount, 6, 1, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        'shXL.Cells(rowCount, 7).Value = "MFG Date"
        'formatCell(shXL, rowCount, 7, 1, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        'shXL.Cells(rowCount, 8).Value = "LOT CONT"
        'formatCell(shXL, rowCount, 8, 1, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        'shXL.Cells(rowCount, 9).Value = "CHECK BY"
        'formatCell(shXL, rowCount, 9, 1, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        ''''data
        Dim i As Int32
        'Dim j As Int32
        'Dim dr As Int32
        rowCount += 1

        For i = 0 To DOC.Rows.Count - 1

            shXL.Cells(rowCount + i, 1).Value = (i + 1).ToString
            formatCell(shXL, rowCount + i, 1, 1, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
            shXL.Cells(rowCount + i, 2).Value = DOC.Rows(i).Cells(0).Value
            formatCell(shXL, rowCount + i, 2, 1, 5, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
            shXL.Cells(rowCount + i, 7).Value = DOC.Rows(i).Cells(1).Value
            formatCell(shXL, rowCount + i, 7, 1, 3, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
            'shXL.Cells(rowCount + i, 5).Value = DOC.Rows(i).Cells(2).Value
            'formatCell(shXL, rowCount + i, 5, 1, 1, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
            'shXL.Cells(rowCount + i, 6).Value = DOC.Rows(i).Cells(3).Value
            'formatCell(shXL, rowCount + i, 6, 1, 1, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
            'shXL.Cells(rowCount + i, 7).Value = DOC.Rows(i).Cells(4).Value
            'formatCell(shXL, rowCount + i, 7, 1, 1, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
            'shXL.Cells(rowCount + i, 8).Value = DOC.Rows(i).Cells(5).Value
            'formatCell(shXL, rowCount + i, 8, 1, 1, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
            'shXL.Cells(rowCount + i, 9).Value = DOC.Rows(i).Cells(6).Value
            'formatCell(shXL, rowCount + i, 9, 1, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        Next

        Dim rowAkhirTabel1 As Int32 = rowCount + i

        ''''''''''''''tabel 2
        '''''header
        rowCount = rowCount + i + 1
        Dim cellCol As String = "A" + (rowCount + 1).ToString + ":I" + (rowCount + 1).ToString
        shXL.Range(cellCol).Interior.Color = Color.LightGray

        shXL.Cells(rowCount, 1).Value = "DETAILS OF PROCESS"
        formatCell(shXL, rowCount, 1, 1, 9, Excel.Constants.xlCenter, Excel.Constants.xlCenter)

        rowCount = rowCount + 1
        shXL.Cells(rowCount, 1).Value = "S/N"
        formatCell(shXL, rowCount, 1, 1, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        shXL.Cells(rowCount, 2).Value = "PROCESS"
        formatCell(shXL, rowCount, 2, 1, 8, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        'shXL.Cells(rowCount, 5).Value = "DATE"
        'formatCell(shXL, rowCount, 5, 1, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        'shXL.Cells(rowCount, 6).Value = "OPERATOR"
        'formatCell(shXL, rowCount, 6, 1, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        'shXL.Cells(rowCount, 7).Value = "DATE CODE LASER"
        'formatCell(shXL, rowCount, 7, 1, 3, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        ''''''data
        rowCount += 1

        For i = 0 To DOP.Rows.Count - 1
            shXL.Cells(rowCount + i, 1).Value = (i + 1).ToString
            formatCell(shXL, rowCount + i, 1, 1, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
            shXL.Cells(rowCount + i, 2).Value = DOP.Rows(i).Cells(0).Value
            formatCell(shXL, rowCount + i, 2, 1, 8, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
            'shXL.Cells(rowCount + i, 5).Value = DOP.Rows(i).Cells(2).Value
            'formatCell(shXL, rowCount + i, 5, 1, 4, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
            'shXL.Cells(rowCount + i, 6).Value = DOP.Rows(i).Cells(1).Value
            'formatCell(shXL, rowCount + i, 6, 1, 1, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
            'shXL.Cells(rowCount + i, 7).Value = ""
            'formatCell(shXL, rowCount + i, 7, 1, 3, Excel.Constants.xlLeft, Excel.Constants.xlCenter)

            'dr = 0
            'For j = 1 To 8
            '    If (j = 2) Then
            '        shXL.Cells(rowCount + i, j).Value = DOP.Rows(i).Cells(dr).Value
            '        formatCell(shXL, rowCount + i, j, 1, 2, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
            '        j += 3
            '    Else
            '        shXL.Cells(rowCount + i, j).Value = DOP.Rows(i).Cells(dr).Value
            '        formatCell(shXL, rowCount + i, j, 1, 1, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
            '    End If
            '    dr += 1
            'Next
        Next

        Dim rowAkhirTabel2 As Int32 = rowCount + i

        ''''''''''''''''''''''''''''''''''''''''''''''''''''
        rowCount = rowCount + i + 1
        shXL.Cells(rowCount, 1).Value = "TECHNICIAN"
        formatCell(shXL, rowCount, 1, 1, 3, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
        shXL.Cells(rowCount, 4).Value = ""
        formatCell(shXL, rowCount, 4, 1, 6, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
        rowCount = rowCount + 1
        shXL.Cells(rowCount, 1).Value = "IPQC"
        formatCell(shXL, rowCount, 1, 1, 3, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
        shXL.Cells(rowCount, 4).Value = ""
        formatCell(shXL, rowCount, 4, 1, 6, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
        rowCount = rowCount + 1
        shXL.Cells(rowCount, 1).Value = "FGA"
        formatCell(shXL, rowCount, 1, 1, 3, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
        shXL.Cells(rowCount, 4).Value = ""
        formatCell(shXL, rowCount, 4, 1, 6, Excel.Constants.xlLeft, Excel.Constants.xlCenter)

        rowCount = rowCount + 1
        shXL.Cells(rowCount, 1).Value = "DISPOSITION FOR ON HOLD"
        formatCell(shXL, rowCount, 1, 1, 9, Excel.Constants.xlLeft, Excel.Constants.xlCenter)

        rowCount = rowCount + 1
        shXL.Cells(rowCount, 1).Value = "Defect of Reject :"
        formatCell(shXL, rowCount, 1, 1, 3, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
        shXL.Cells(rowCount, 4).Value = "FGA :"
        formatCell(shXL, rowCount, 4, 1, 3, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
        shXL.Cells(rowCount, 7).Value = "Prodn Leader :"
        formatCell(shXL, rowCount, 7, 1, 3, Excel.Constants.xlLeft, Excel.Constants.xlCenter)

        rowCount = rowCount + 2
        shXL.Cells(rowCount, 1).Value = "Date : " + DateTime.Now.ToString("yyyy/MM/dd")
        formatCell(shXL, rowCount, 1, 4, 3, Excel.Constants.xlLeft, Excel.Constants.xlTop)
        shXL.Cells(rowCount, 4).Value = ""
        formatCell(shXL, rowCount, 4, 6, 3, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        shXL.Cells(rowCount, 7).Value = "Resorting Qty"
        formatCell(shXL, rowCount, 7, 1, 3, Excel.Constants.xlLeft, Excel.Constants.xlCenter)

        rowCount = rowCount + 1
        shXL.Cells(rowCount, 7).Value = "Rework Qty"
        formatCell(shXL, rowCount, 7, 1, 3, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
        rowCount = rowCount + 1
        shXL.Cells(rowCount, 7).Value = "Accepted Qty"
        formatCell(shXL, rowCount, 7, 1, 3, Excel.Constants.xlLeft, Excel.Constants.xlCenter)
        rowCount = rowCount + 1
        shXL.Cells(rowCount, 7).Value = "Confirmed Qty"
        formatCell(shXL, rowCount, 7, 1, 3, Excel.Constants.xlLeft, Excel.Constants.xlCenter)

        rowCount = rowCount + 1
        shXL.Cells(rowCount, 1).Value = "Name / Signature"
        formatCell(shXL, rowCount, 1, 1, 3, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        rowCount = rowCount + 1
        shXL.Cells(rowCount, 1).Value = "(Production Leader)"
        formatCell(shXL, rowCount, 1, 1, 3, Excel.Constants.xlCenter, Excel.Constants.xlCenter)
        shXL.Cells(rowCount, 7).Value = "Colour : Green"
        formatCell(shXL, rowCount, 7, 1, 3, Excel.Constants.xlCenter, Excel.Constants.xlCenter)


        '''''''''''''''''''''''''''''''''''''''''''''
        '''''setting border
        Dim rangeStr As String
        With shXL
            rangeStr = "A1:I" + rowCount.ToString
            .Range(rangeStr).Borders.Color = System.Drawing.Color.Black
            .Range(rangeStr).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            .Range(rangeStr).Font.Size = 8
            .Range("A1:I6").Font.Size = 11
            .Range("I4:I5").Font.Size = 14
            .Range("I4:I5").Font.Bold = True
            .Range("A2:D2").Font.Underline = True

            .Range("A1:E2").Borders(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlLineStyleNone

            .Range("A3:D5").Borders(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlLineStyleNone
            .Range("A4:D5").Borders(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlLineStyleNone

            .Range("H1:I2").Borders(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlLineStyleNone
            .Range("A7:I8").Borders(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlLineStyleNone
            .Range("A7:I8").Borders(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlLineStyleNone

            rangeStr = "A" + rowAkhirTabel1.ToString + ":I" + (rowAkhirTabel1 + 1).ToString
            .Range(rangeStr).Borders(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlLineStyleNone
            .Range(rangeStr).Borders(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlLineStyleNone

            rangeStr = "A" + rowAkhirTabel2.ToString + ":I" + rowAkhirTabel2.ToString
            .Range(rangeStr).Borders(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlLineStyleNone

            rangeStr = "A" + (rowAkhirTabel2 + 4).ToString + ":I" + (rowAkhirTabel2 + 12).ToString
            .Range(rangeStr).Borders(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlLineStyleNone
            .Range(rangeStr).Borders(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlLineStyleNone

        End With

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''barcode

        Dim barcodeImage As Bitmap
        Dim barcodeCell As Object
        Dim BCrange As Excel.Range

        barcodeImage = bcEncode1D("PN111222333444Q32D" + DateTime.Now.ToString("yyyyMMdd"), BarcodeFormat.CODE_128, 100, 30)
        barcodeCell = "B5"
        BCrange = shXL.Range(barcodeCell)
        Clipboard.SetDataObject(barcodeImage)
        BCrange.Select()
        shXL.Paste()

        '''''''''''''''''''''''''''''''''''''''''''''''
        shXL.Cells.EntireColumn.AutoFit()
        ' Format A1:D1 as bold, vertical alignment = center.
        With shXL.Range("A1", "K2")
            .Font.Bold = True
            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
        End With

        appXL.Visible = True

        shXL.PageSetup.PrintArea = "A1:I" + rowCount.ToString
        'shXL.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, "G:\TesPDF", False, True, False)
        shXL.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperEnvelopePersonal
        shXL.PageSetup.FitToPagesWide = True

        'shXL.PrintOutEx()
        shXL.PrintPreview()

        ' Release object references.
        raXL = Nothing
        shXL = Nothing
        'wbXl = Nothing
        wbXl.Close(SaveChanges:=False)
        appXL.Application.Quit()
        appXL = Nothing


        Exit Sub
Err_Handler:
        MsgBox(Err.Description, vbCritical, "Error: " & Err.Number)
    End Sub

    Sub formatCell(ByVal wsheet As Excel.Worksheet,
                   ByVal rowPos As Int32, ByVal colPos As Int32,
                   ByVal rowResize As Int32, ByVal colResize As Int32,
                   ByVal horAlign As Int32, ByVal verAlign As Int32)

        With wsheet.Cells(rowPos, colPos).Resize(rowResize, colResize)
            .HorizontalAlignment = horAlign
            .VerticalAlignment = verAlign
            .WrapText = True
            .Orientation = 0
            .AddIndent = False
            .IndentLevel = 0
            .ShrinkToFit = False
            '.ReadingOrder = xlContext
            .MergeCells = True
        End With
    End Sub

    Private Sub btnTesCetakLabel_Click(sender As Object, e As EventArgs) Handles btnTesCetakLabel.Click
        printLabel()
    End Sub

    Sub printLabel()
        Dim appXL As Excel.Application
        Dim wbXl As Excel.Workbook
        Dim shXL As Excel.Worksheet
        Dim raXL As Excel.Range

        ' Start Excel and get Application object.
        appXL = CreateObject("Excel.Application")
        appXL.Visible = False

        ' Add a new workbook.
        wbXl = appXL.Workbooks.Add
        shXL = wbXl.ActiveSheet

        Dim barcodeImage As Bitmap
        Dim barcodeCell As Object
        Dim range As Microsoft.Office.Interop.Excel.Range

        With shXL
            .Cells(2, 1).Value = "PART NUMBER"
            .Cells(5, 1).Value = "DESCRIPTION"
            .Cells(5, 6).Value = "ZQSFP+ 2X2 CAGE ASSEMBLY"
            .Cells(8, 1).Value = "QUANTITY"
            .Cells(11, 1).Value = "LOT NO."
            .Cells(14, 1).Value = "TRACEABILITY"
        End With

        barcodeImage = bcEncode1D("1P1717212000")
        barcodeCell = "D2"
        range = shXL.Range(barcodeCell)
        Clipboard.SetDataObject(barcodeImage)
        range.Select()
        shXL.Paste()

        barcodeImage = bcEncode1D("Q000000000120")
        barcodeCell = "D8"
        range = shXL.Range(barcodeCell)
        Clipboard.SetDataObject(barcodeImage)
        range.Select()
        shXL.Paste()

        barcodeImage = bcEncode1D("BF20524011")
        barcodeCell = "D11"
        range = shXL.Range(barcodeCell)
        Clipboard.SetDataObject(barcodeImage)
        range.Select()
        shXL.Paste()

        barcodeImage = bcEncode1D("1T000020220521")
        barcodeCell = "D14"
        range = shXL.Range(barcodeCell)
        Clipboard.SetDataObject(barcodeImage)
        range.Select()
        shXL.Paste()

        barcodeImage = bcEncodePDF417("JOVAN Technologies")
        barcodeCell = "G11"
        range = shXL.Range(barcodeCell)
        Clipboard.SetDataObject(barcodeImage)
        range.Select()
        shXL.Paste()

        appXL.Visible = True

        shXL.PageSetup.PrintArea = "A1:H14"
        'shXL.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, "G:\TesPDF", False, True, False)

        'shXL.PrintOutEx()
        shXL.PrintPreview()

        ' Release object references.
        raXL = Nothing
        shXL = Nothing
        wbXl.Close(SaveChanges:=False)
        appXL.Application.Quit()
        appXL = Nothing

        Exit Sub
Err_Handler:
        MsgBox(Err.Description, vbCritical, "Error: " & Err.Number)
    End Sub

    Function bcEncode1D(ByVal BarcodeData As String) As Bitmap
        Dim writer As New BarcodeWriter
        writer.Format = BarcodeFormat.CODE_128
        Dim bitmap = writer.Write(BarcodeData)

        Return New Bitmap(bitmap, New Size(150, 50))
    End Function

    Function bcEncode1D(ByVal BarcodeData As String, ByVal BCFormat As Integer, ByVal width As Integer, ByVal height As Integer) As Bitmap
        Dim writer As New BarcodeWriter
        writer.Format = BCFormat
        Dim bitmap = writer.Write(BarcodeData)

        Return New Bitmap(bitmap, New Size(width, height))
    End Function

    Function bcEncodePDF417(ByVal BarcodeData As String) As Bitmap
        Dim writer = New BarcodeWriter With {
            .Format = BarcodeFormat.PDF_417,
            .Options = New EncodingOptions With {
                .Width = 0,
                .Height = 0,
                .Margin = 0
            }
        }
        Dim bitmap = writer.Write(BarcodeData)


        Return New Bitmap(bitmap, New Size(300, 100))
    End Function

    Private Sub cbPilDataMaster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPilDataMaster.SelectedIndexChanged
        CbIdxChanged()
    End Sub

    Private Sub btnEntryData_Click(sender As Object, e As EventArgs) Handles btnEntryData.Click
        If (cbPilDataMaster.Text = "Master Process") Then
            Dim masterprocess = New MasterProcess()
            'masterprocess.Show()
            masterprocess.ShowDialog()
            CbIdxChanged()
        ElseIf (cbPilDataMaster.Text = "Master Material") Then
            Dim mastermaterial = New MasterMaterial()
            'mastermaterial.Show()
            mastermaterial.ShowDialog()
            CbIdxChanged()
        ElseIf (cbPilDataMaster.Text = "Master Finish Goods") Then
            Dim masterfinishgoods = New MaterialUsageFinishGoods()
            'masterfinishgoods.Show()
            masterfinishgoods.ShowDialog()
            CbIdxChanged()
        ElseIf (cbPilDataMaster.Text = "Master Process Flow") Then
            Dim masterprocessflow = New MasterProcessFlow()
            'masterprocessflow.Show()
            masterprocessflow.ShowDialog()
            CbIdxChanged()
        ElseIf (cbPilDataMaster.Text = "Pre Production") Then
            'Dim praproduction = New PraProduction()
            'praproduction.Show()
            'praproduction.ShowDialog()
            CbIdxChanged()
        ElseIf (cbPilDataMaster.Text = "Stock") Then
            MessageBox.Show("Sorry. Cannot Entry Stock")
        End If
    End Sub

    Private Sub CbIdxChanged()
        dgDataView.DataSource = Nothing
        dgDataView.Rows.Clear()
        dgDataView.Columns.Clear()

        dgDataView.Refresh()

        dgDataView.ReadOnly = False
        ShowDataGrid()
        dgDataView.ReadOnly = True
    End Sub

    Private Sub View_ProcessFlow()
        Dim varProcess As String = ""
        dgDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Call Database.koneksi_database()

        With dgDataView
            .EnableHeadersVisualStyles = False
            With .ColumnHeadersDefaultCellStyle
                .BackColor = Color.Navy
                .ForeColor = Color.White
                .Font = New Font("Tahoma", 13, FontStyle.Bold)
                .Alignment = HorizontalAlignment.Center
                .Alignment = ContentAlignment.MiddleCenter
            End With
        End With

        Dim queryCek As String = "select * from MASTER_PROCESS"
        Dim dsexist = New DataSet
        Dim adapterexist = New SqlDataAdapter(queryCek, Database.koneksi)
        adapterexist.Fill(dsexist)
        For i As Integer = 0 To dsexist.Tables(0).Rows.Count - 1
            If i = 0 Then
                varProcess = "[" + dsexist.Tables(0).Rows(i).Item("PROCESS_NAME").ToString + "]"
            Else
                varProcess = varProcess + ",[" + dsexist.Tables(0).Rows(i).Item("PROCESS_NAME").ToString + "]"
            End If
        Next

        Dim query As String = "SELECT * FROM ( SELECT master_finish_goods as FG_Number,master_proc, need FROM dbo.MASTER_PROCESS_FLOW where need=1) t PIVOT ( count(need) FOR master_proc IN ( " + varProcess + " )) pivot_table"

        Dim adapterGas As SqlDataAdapter
        Dim datasetGas As New DataSet

        adapterGas = New SqlDataAdapter(query, Database.koneksi)
        adapterGas.Fill(datasetGas)

        If datasetGas.Tables(0).Rows.Count > 0 Then
            dgDataView.ColumnCount = 1
            dgDataView.Columns(0).Name = "Part Number"
            For r = 0 To datasetGas.Tables(0).Rows.Count - 1
                Dim row As String() = New String() {datasetGas.Tables(0).Rows(r).Item(0).ToString}
                dgDataView.Rows.Add(row)
            Next

            For i As Integer = 0 To dsexist.Tables(0).Rows.Count - 1
                Dim chk As New DataGridViewCheckBoxColumn
                chk.HeaderText = dsexist.Tables(0).Rows(i).Item(1)
                dgDataView.Columns.Add(chk)
            Next

            For r = 0 To datasetGas.Tables(0).Rows.Count - 1
                For c = 1 To datasetGas.Tables(0).Columns.Count - 1
                    If datasetGas.Tables(0).Rows(r).Item(c) = 1 Then
                        dgDataView.Rows(r).Cells(c).Value = True
                    Else
                        dgDataView.Rows(r).Cells(c).Value = False
                    End If
                Next
            Next
        End If

        For i As Integer = 0 To dgDataView.RowCount - 1
            If dgDataView.Rows(i).Index Mod 2 = 0 Then
                dgDataView.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgDataView.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        datasetGas.Clear()
    End Sub

    Sub ShowDataGrid()
        Call Database.koneksi_database()
        Dim dtTable As DataTable = Nothing

        Dim i, j As Int32

        Try
            With dgDataView
                .DefaultCellStyle.Font = New Font("Tahoma", 11)

                If (cbPilDataMaster.Text = "Master Process") Then
                    dtTable = Database.GetData("select process_name as Process_Name,process_desc as Desc_Process from MASTER_PROCESS")

                    .ColumnCount = 3
                    .Columns(0).Name = "No"
                    .Columns(1).Name = "Process Name"
                    .Columns(2).Name = "Process Description"

                    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(0).Width = Int(.Width * 0.05)
                    .Columns(1).Width = Int(.Width * 0.3)
                    .Columns(2).Width = Int(.Width * 0.65)
                ElseIf (cbPilDataMaster.Text = "Master Material") Then
                    dtTable = Database.GetData("select * from MASTER_MATERIAL")

                    .ColumnCount = 4
                    .Columns(0).Name = "No"
                    .Columns(1).Name = "Part Number"
                    .Columns(2).Name = "Name"
                    .Columns(3).Name = "Standart Qty"

                    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(0).Width = Int(.Width * 0.05)
                    .Columns(1).Width = Int(.Width * 0.3)
                    .Columns(2).Width = Int(.Width * 0.5)
                    .Columns(3).Width = Int(.Width * 0.15)
                ElseIf (cbPilDataMaster.Text = "Master Finish Goods") Then
                    dtTable = Database.GetData("select M.part_number as Part_Number_FG, M.standard_qty as Qty,(SELECT COUNT(*) FROM MASTER_FINISH_GOODS WHERE PART_NUMBER=M.part_number AND MATERIAL_PART_NUMBER IS NOT NULL) AS Total_Material from MASTER_FINISH_GOODS M WHERE M.MATERIAL_PART_NUMBER IS NULL ORDER BY TOTAL_MATERIAL DESC")

                    .ColumnCount = 4
                    .Columns(0).Name = "No"
                    .Columns(1).Name = "Part Number Finish Good"
                    .Columns(2).Name = "Quantity"
                    .Columns(3).Name = "Total Material"

                    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(0).Width = Int(.Width * 0.05)
                    .Columns(1).Width = Int(.Width * 0.65)
                    .Columns(2).Width = Int(.Width * 0.15)
                    .Columns(3).Width = Int(.Width * 0.15)
                ElseIf (cbPilDataMaster.Text = "Master Process Flow") Then
                    View_ProcessFlow()
                    Exit Sub
                ElseIf (cbPilDataMaster.Text = "Pre Production") Then
                    'View_PraProduction()

                    dtTable = Database.GetData("select NO_PO,FG_PN,MASTER_QTY,CURRENT_QTY,STATUS from pre_PRODUCTION")

                    .ColumnCount = 7
                    .Columns(0).Name = "No"
                    .Columns(1).Name = "PO"
                    .Columns(2).Name = "FG_PN"
                    .Columns(3).Name = "Quantity"
                    .Columns(4).Name = "Current Quantity"
                    .Columns(5).Name = "Total PO"
                    .Columns(6).Name = "Status"

                    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(0).Width = Int(.Width * 0.05)
                    .Columns(1).Width = Int(.Width * 0.2)
                    .Columns(2).Width = Int(.Width * 0.15)
                    .Columns(3).Width = Int(.Width * 0.15)
                    .Columns(4).Width = Int(.Width * 0.15)
                    .Columns(5).Width = Int(.Width * 0.15)
                    .Columns(6).Width = Int(.Width * 0.15)
                ElseIf (cbPilDataMaster.Text = "Stock") Then
                    dtTable = Database.GetData("select part_number, stock, datetime_update from stock")

                    .ColumnCount = 4
                    .Columns(0).Name = "No"
                    .Columns(1).Name = "Part Number"
                    .Columns(2).Name = "Stock"
                    .Columns(3).Name = "Last Update"

                    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(0).Width = Int(.Width * 0.05)
                    .Columns(1).Width = Int(.Width * 0.45)
                    .Columns(2).Width = Int(.Width * 0.25)
                    .Columns(3).Width = Int(.Width * 0.25)
                End If

                .EnableHeadersVisualStyles = False
                With .ColumnHeadersDefaultCellStyle
                    .BackColor = Color.Navy
                    .ForeColor = Color.White
                    .Font = New Font("Tahoma", 16, FontStyle.Bold)
                    .Alignment = HorizontalAlignment.Center
                    .Alignment = ContentAlignment.MiddleCenter
                End With

                If (dtTable.Rows.Count > 0) Then
                    For i = 0 To dtTable.Rows.Count - 1
                        dgDataView.Rows.Add(1)
                        .Item(0, i).Value = (i + 1).ToString
                        For j = 0 To dtTable.Columns.Count - 1
                            If (cbPilDataMaster.Text = "Pre Production") Then
                                If (j = 4) Then
                                    If (dtTable.Rows(i).Item(j) = "1") Then
                                        .Item(j + 2, i).Value = "Active"
                                    Else
                                        .Item(j + 2, i).Value = "Non-Active"
                                    End If

                                    Dim queryCount As String = "select count(*) as count from SUB_PO where PO_NO=" & dgDataView.Rows(i).Cells("PO").Value
                                    Dim dsCount = New DataSet
                                    Dim adapterCount = New SqlDataAdapter(queryCount, Database.koneksi)
                                    adapterCount.Fill(dsCount)
                                    dgDataView.Rows(i).Cells(5).Value = dsCount.Tables(0).Rows(0).Item("count").ToString
                                Else
                                    .Item(j + 1, i).Value = dtTable.Rows(i).Item(j)
                                End If
                            Else
                                .Item(j + 1, i).Value = dtTable.Rows(i).Item(j)
                            End If
                        Next
                    Next
                End If
            End With

            For i = 0 To dgDataView.RowCount - 1
                If dgDataView.Rows(i).Index Mod 2 = 0 Then
                    dgDataView.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    dgDataView.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next i

            For i = 0 To dgDataView.Columns.Count - 1
                dgDataView.Columns(i).DefaultCellStyle.Font = New Font("Tahoma", 16, FontStyle.Regular)
            Next

            Call Database.close_koneksi()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GroupBox1.Width = pAtas.Width / 2
        GroupBox4.Width = pAtas.Width / 2

        If globVar.hakAkses = "operator" Then
            TabControl1.SelectedIndex = 1
            scan_comp.Visible = True
            scan_comp.Text = ""
            last_scan_comp.Visible = False
            last_scan_comp.Text = ""
        Else
            scan_comp.Visible = True
            scan_comp.Text = ""
            last_scan_comp.Visible = True
            last_scan_comp.Text = ""
        End If
    End Sub



    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Call Database.koneksi_database()
        If mts_issue_return_stock.Text <> "" And mts_return_stock.Text <> "" Then
            Dim adapterCek As SqlDataAdapter
            Dim dsCek As New DataSet
            Dim sqlCek As String = "SELECT * FROM input_stock where mts_no=" & mts_issue_return_stock.Text
            adapterCek = New SqlDataAdapter(sqlCek, Database.koneksi)
            adapterCek.Fill(dsCek)
            If dsCek.Tables(0).Rows.Count > 0 Then
                Dim adapterCek2 As SqlDataAdapter
                Dim dsCek2 As New DataSet
                Dim sqlCek2 As String = "SELECT * FROM input_stock where mts_no=" & mts_return_stock.Text
                adapterCek2 = New SqlDataAdapter(sqlCek2, Database.koneksi)
                adapterCek2.Fill(dsCek2)
                If dsCek2.Tables(0).Rows.Count > 0 Then
                    MessageBox.Show("MTS Return already in database.")
                Else
                    Dim FormReturnStock = New OldFormReturnStock()
                    FormReturnStock.Show()
                End If
            Else
                MessageBox.Show("MTS Issue not in Database.")
            End If
        End If
    End Sub

    Private Sub mts_return_stock_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles mts_return_stock.PreviewKeyDown
        Call Database.koneksi_database()
        If e.KeyData = Keys.Enter Then
            If mts_issue_return_stock.Text <> "" And mts_return_stock.Text <> "" Then
                Dim adapterCek As SqlDataAdapter
                Dim dsCek As New DataSet
                Dim sqlCek As String = "SELECT * FROM input_stock where mts_no=" & mts_issue_return_stock.Text
                adapterCek = New SqlDataAdapter(sqlCek, Database.koneksi)
                adapterCek.Fill(dsCek)
                If dsCek.Tables(0).Rows.Count > 0 Then
                    Dim adapterCek2 As SqlDataAdapter
                    Dim dsCek2 As New DataSet
                    Dim sqlCek2 As String = "SELECT * FROM input_stock where mts_no=" & mts_return_stock.Text
                    adapterCek2 = New SqlDataAdapter(sqlCek2, Database.koneksi)
                    adapterCek2.Fill(dsCek2)
                    If dsCek2.Tables(0).Rows.Count > 0 Then
                        MessageBox.Show("MTS Return already in database.")
                    Else
                        Dim FormReturnStock = New OldFormReturnStock()
                        FormReturnStock.Show()
                    End If
                Else
                    MessageBox.Show("MTS Issue not in Database.")
                End If
            End If
        End If
    End Sub

    Private Sub txtSearch_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtSearch.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            Dim str As String = txtSearch.Text
            Try
                For i As Integer = 0 To dgDataView.Rows.Count - 2
                    For j As Integer = 0 To dgDataView.Columns.Count - 1
                        If (dgDataView.Rows(i).Cells(j).Value.ToString = str) Then
                            If (dgDataView.CurrentCell.RowIndex = i) Then
                                Continue For
                            Else
                                dgDataView.Rows(i).Selected = True
                                dgDataView.CurrentCell = dgDataView.Rows(i).Cells(j)
                                MessageBox.Show(dgDataView.CurrentCell.RowIndex.ToString + " -> " + i.ToString + ", " + j.ToString)
                                Exit Sub
                            End If
                        End If
                    Next
                Next i
            Catch abc As Exception
            End Try
            MsgBox("Data not found!")
        End If
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        FormLogin.Show()
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        FormLogin.Show()
    End Sub

    Private Sub MainForm_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        FormLogin.Close()
    End Sub

    Private Sub last_scan_comp_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles last_scan_comp.PreviewKeyDown
        If e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter Then
            If last_scan_comp.Text <> "" Then
                If IsNumeric(last_scan_comp.Text) Then
                    Dim adapterCek As SqlDataAdapter
                    Dim adapterCekafterUpdate As SqlDataAdapter
                    Dim adapterCekStock As SqlDataAdapter
                    Dim dsCek As New DataSet
                    Dim dsCekafterUpdate As New DataSet
                    Dim dsCekStock As New DataSet
                    Dim sqlCek As String = "SELECT * FROM production_doc where po=" & txt_mainform_production_po.Text & " and line='" & cb_mainform_production_line.Text & "' and pn=" & last_scan_comp.Text
                    adapterCek = New SqlDataAdapter(sqlCek, Database.koneksi)
                    adapterCek.Fill(dsCek)
                    If dsCek.Tables(0).Rows.Count > 0 Then
                        If dsCek.Tables(0).Rows(0).Item("LAST_CHECK") = 1 Then
                            MessageBox.Show("Comp already scan")
                            dgv_doc()
                            last_scan_comp.Text = ""
                        Else
                            Dim queryUpdate As String = "update production_doc set [last_check]=1,datetime_update=getdate() where number=" & dsCek.Tables(0).Rows(0).Item("NUMBER")
                            Dim cmdUpdate = New SqlCommand(queryUpdate, Database.koneksi)
                            If cmdUpdate.ExecuteNonQuery() Then
                                Dim sqlCekafterUpdate As String = "SELECT * FROM production_doc where po=" & txt_mainform_production_po.Text & " and line='" & cb_mainform_production_line.Text & "' and [check]=0"
                                adapterCekafterUpdate = New SqlDataAdapter(sqlCekafterUpdate, Database.koneksi)
                                adapterCekafterUpdate.Fill(dsCekafterUpdate)
                                If dsCekafterUpdate.Tables(0).Rows.Count = 0 Then
                                    Dim sqlCekStock As String = "select * from stock where part_number='" & last_scan_comp.Text & "'"
                                    adapterCekStock = New SqlDataAdapter(sqlCekStock, Database.koneksi)
                                    adapterCekStock.Fill(dsCekStock)
                                    Dim Reject As Integer = 0

                                    For i As Integer = 0 To DOC.Rows.Count - 1
                                        If DOC.Rows(i).Cells(4).Value > Reject Then
                                            Reject = DOC.Rows(i).Cells(4).Value
                                        End If
                                    Next

                                    If dsCekStock.Tables(0).Rows.Count > 0 Then
                                        Dim queryUpdateStock As String = "update stock set stock=" & Int(qty_po.Text) - Reject & ",datetime_update=getdate() where part_number='" & last_scan_comp.Text & "'"
                                        Dim cmdUpdateStock = New SqlCommand(queryUpdateStock, Database.koneksi)
                                        cmdUpdateStock.ExecuteNonQuery()
                                    Else
                                        Dim queryInsertStock As String = "insert into stock (part_number,stock,datetime_update) values ('" & last_scan_comp.Text & "'," & Int(qty_po.Text) - Reject & ",getdate())"
                                        Dim cmdInsertStock = New SqlCommand(queryInsertStock, Database.koneksi)
                                        cmdInsertStock.ExecuteNonQuery()
                                    End If
                                End If

                                dgv_doc()
                                last_scan_comp.Text = ""
                            End If
                        End If
                    End If
                Else
                    MessageBox.Show("Wrong Component")
                End If
            Else
                MessageBox.Show("Wrong Component")
            End If
        End If
    End Sub
End Class
