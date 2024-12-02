Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class OthersPart

    Public Shared menu As String = "Others Part"

    Private Sub txtLabelOtherPart_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtLabelOtherPart.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
            If globVar.view > 0 Then
                loadDGVAtas()
                loadDGVBawah()
                btnPrintOthersPart.Enabled = True
                btnOtherSave.Enabled = True
            Else
                RJMessageBox.Show("Cannot access this menu.")
            End If
        End If
    End Sub

    Private Sub DataGridView2_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView2.DataBindingComplete
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

            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Sub DataGridView4_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView4.DataBindingComplete
        With DataGridView4
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

    Private Sub DataGridView2_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView2.CellFormatting
        If DataGridView2.Columns(e.ColumnIndex).Name = "Qty Dismantle" Then
            e.CellStyle.BackColor = Color.Green
            e.CellStyle.ForeColor = Color.White
        End If
    End Sub

    Private Sub btnOtherSave_Click(sender As Object, e As EventArgs) Handles btnOtherSave.Click
        If globVar.add > 0 Then
            If DataGridView2.Rows.Count > 0 Then
                Try
                    Dim over As Boolean = False
                    Dim statusSimpan As Integer = 1
                    Dim codeOther As String = ""

                    For i = 0 To DataGridView2.Rows.Count - 1
                        If IsNumeric(DataGridView2.Rows(i).Cells("Qty Dismantle").Value) Then
                            If DataGridView2.Rows(i).Cells("Qty Dismantle").Value > 0 Then
                                If DataGridView2.Rows(i).Cells("Qty Dismantle").Value > DataGridView2.Rows(i).Cells("Max Qty").Value Then
                                    over = True
                                End If
                            End If
                        Else
                            RJMessageBox.Show("this is not number -> " & DataGridView2.Rows(i).Cells(10).Value & ". Please change with number.")
                        End If
                    Next

                    If over Then
                        RJMessageBox.Show("The quantity cannot exceed the maximum quantity")
                        Exit Sub
                    End If

                    For i = 0 To DataGridView2.Rows.Count - 1

                        Dim queryCode As String = "select top 1 CODE_STOCK_PROD_OTHERS from stock_prod_others ORDER BY cast(replace(CODE_STOCK_PROD_OTHERS,'OT','') as int) desc"
                        Dim dtCode As DataTable = Database.GetData(queryCode)
                        If dtCode.Rows.Count > 0 Then
                            Dim match As Match = Regex.Match(dtCode.Rows(0).Item("CODE_STOCK_PROD_OTHERS").ToString(), "^([A-Z]+)(\d+)$")
                            If match.Success Then
                                Dim prefix As String = match.Groups(1).Value
                                Dim number As Integer = Integer.Parse(match.Groups(2).Value)
                                Dim nextNumber As Integer = number + 1
                                codeOther = prefix & nextNumber.ToString()
                            End If
                        Else
                            codeOther = "OT" & dtCode.Rows.Count + 1
                        End If

                        If DataGridView2.Rows(i).Cells("Qty Dismantle").Value > 0 Then

                            Dim query As String = "select * from stock_prod_others where CODE_OUT_PROD_DEFECT='" & txtLabelOtherPart.Text & "' and part_number='" & DataGridView2.Rows(i).Cells("Material").Value & "' and department='" & globVar.department & "' AND PROCESS_NAME='" & DataGridView2.Rows(i).Cells("Process").Value & "'"
                            Dim dtCheckStockOthers As DataTable = Database.GetData(query)
                            If dtCheckStockOthers.Rows.Count > 0 Then
                                Dim sqlUpdateProcessProd As String = "update stock_prod_others set qty=" & DataGridView2.Rows(i).Cells("Qty Dismantle").Value.ToString().Replace(",", ".") & " where ID=" & dtCheckStockOthers.Rows(0).Item("id")
                                Dim cmdUpdateProcessProd = New SqlCommand(sqlUpdateProcessProd, Database.koneksi)
                                cmdUpdateProcessProd.ExecuteNonQuery()
                            Else
                                Dim sqlInsertOther As String = "INSERT INTO stock_prod_others (CODE_STOCK_PROD_OTHERS, PART_NUMBER, QTY,CODE_OUT_PROD_DEFECT,DEPARTMENT,LOT_NO,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,PROCESS_NAME,insert_who)
                                                values ('" & codeOther & "','" & DataGridView2.Rows(i).Cells("Material").Value & "','" & DataGridView2.Rows(i).Cells("Qty Dismantle").Value.ToString().Replace(",", ".") & "','" & txtLabelOtherPart.Text & "','" & globVar.department & "','" & DataGridView2.Rows(i).Cells("Lot No").Value & "','" & DataGridView2.Rows(i).Cells("Trace").Value & "','" & DataGridView2.Rows(i).Cells("ICD").Value & "','" & DataGridView2.Rows(i).Cells("Batch No").Value & "','" & DataGridView2.Rows(i).Cells("Process").Value & "','" & globVar.department & "')"
                                Dim cmdInsertOther = New SqlCommand(sqlInsertOther, Database.koneksi)
                                cmdInsertOther.ExecuteNonQuery()
                            End If

                            Dim sqlUpdateDefect As String = "update out_prod_defect set qty_out=" & DataGridView2.Rows(i).Cells("Qty Dismantle").Value.ToString().Replace(",", ".") & ",datetime_out=getdate(),who_out='" & globVar.username & "' where ID=" & DataGridView2.Rows(i).Cells("#").Value
                            Dim cmdUpdateDefect = New SqlCommand(sqlUpdateDefect, Database.koneksi)
                            cmdUpdateDefect.ExecuteNonQuery()

                        End If

                    Next

                    loadDGVBawah()

                Catch ex As Exception
                    RJMessageBox.Show(ex.Message)
                End Try
            End If
        Else
            RJMessageBox.Show("Cannot access this menu.")
        End If
    End Sub

    Private Sub btnPrintOthersPart_Click(sender As Object, e As EventArgs) Handles btnPrintOthersPart.Click
        If globVar.view > 0 Then
            If DataGridView4.Rows.Count > 0 Then
                Dim countPrint = 0
                Try
                    Dim query As String = "select stock_prod_others.*,master_material.name from stock_prod_others, master_material where CODE_OUT_PROD_DEFECT='" & txtLabelOtherPart.Text & "' and stock_prod_others.department='" & globVar.department & "' and stock_prod_others.part_number=master_material.part_number"
                    Dim dtCheckOthersMaterialBalance As DataTable = Database.GetData(query)
                    If dtCheckOthersMaterialBalance.Rows.Count > 0 Then
                        For i = 0 To dtCheckOthersMaterialBalance.Rows.Count - 1
                            If dtCheckOthersMaterialBalance.Rows(i).Item("print") = 0 Then
                                globVar.failPrint = ""
                                _PrintingSubAssyRawMaterial.txt_Unique_id.Text = dtCheckOthersMaterialBalance.Rows(i).Item("code_stock_prod_others")
                                _PrintingSubAssyRawMaterial.txt_part_number.Text = dtCheckOthersMaterialBalance.Rows(i).Item("part_number")
                                _PrintingSubAssyRawMaterial.txt_Part_Description.Text = dtCheckOthersMaterialBalance.Rows(i).Item("name")
                                _PrintingSubAssyRawMaterial.txt_Qty.Text = dtCheckOthersMaterialBalance.Rows(i).Item("qty")
                                _PrintingSubAssyRawMaterial.txt_jenis_ticket.Text = "Others Material"
                                _PrintingSubAssyRawMaterial.txt_Traceability.Text = dtCheckOthersMaterialBalance.Rows(i).Item("traceability")
                                _PrintingSubAssyRawMaterial.txt_Inv_crtl_date.Text = dtCheckOthersMaterialBalance.Rows(i).Item("inv_ctrl_date")
                                _PrintingSubAssyRawMaterial.txt_Batch_no.Text = dtCheckOthersMaterialBalance.Rows(i).Item("batch_no")
                                _PrintingSubAssyRawMaterial.txt_Lot_no.Text = dtCheckOthersMaterialBalance.Rows(i).Item("lot_no")
                                _PrintingSubAssyRawMaterial.txt_QR_Code.Text = dtCheckOthersMaterialBalance.Rows(i).Item("code_stock_prod_others") & Environment.NewLine
                                _PrintingSubAssyRawMaterial.btn_Print_Click(sender, e)

                                If globVar.failPrint = "No" Then
                                    Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (fg, remark,sub_sub_po,department,material,code_print)
                                        VALUES ('" & DataGridView2.Rows(i).Cells("FG").Value & "','Others Material','" & DataGridView2.Rows(i).Cells("SSP").Value & "','" & globVar.department & "','" & dtCheckOthersMaterialBalance.Rows(i).Item("part_number") & "','" & dtCheckOthersMaterialBalance.Rows(i).Item("code_stock_prod_others") & "')"
                                    Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                                    cmdInsertPrintingRecord.ExecuteNonQuery()
                                End If
                            Else
                                countPrint += 1
                            End If
                        Next
                    Else
                        RJMessageBox.Show("Sorry dont have any others part.")
                    End If
                Catch ex As Exception
                    RJMessageBox.Show(ex.Message)
                End Try
            Else
                RJMessageBox.Show("No data for print.")
            End If
        End If
    End Sub

    Sub loadDGVAtas()

        Try
            Dim query As String

            query = ""

            If txtLabelOtherPart.Text.StartsWith("D") Then 'Label Defect

                query = "Select id [#], sub_sub_po [SSP],fg_pn [FG],part_number [Material],lot_no [Lot No],traceability [Trace],batch_no [Batch No],inv_ctrl_date [ICD],process_reject [Process], flow_ticket_no [Flow Ticket], qty [Max Qty], qty_out [Qty Dismantle] from OUT_PROD_DEFECT where CODE_OUT_PROD_DEFECT='" & txtLabelOtherPart.Text & "' and department='" & globVar.department & "'"

            ElseIf txtLabelOtherPart.Text.Contains(";") Then 'Label Flow Ticket

                Dim SplitSubSubPO() As String = txtLabelOtherPart.Text.Split(";"c)
                query = "Select id [#], sub_sub_po [SSP],fg_pn [FG],part_number [Material],lot_no [Lot No],traceability [Trace],batch_no [Batch No],inv_ctrl_date [ICD],process_reject [Process], flow_ticket_no [Flow Ticket], qty [Max Qty], qty_out [Qty Dismantle] from OUT_PROD_DEFECT where SUB_SUB_PO='" & SplitSubSubPO(0) & "' and department='" & globVar.department & "'"

            ElseIf txtLabelOtherPart.Text.Contains("-") AndAlso txtLabelOtherPart.Text.Contains(";") = False Then 'Sub sub PO

                query = "Select id [#], sub_sub_po [SSP],fg_pn [FG],part_number [Material],lot_no [Lot No],traceability [Trace],batch_no [Batch No],inv_ctrl_date [ICD],process_reject [Process], flow_ticket_no [Flow Ticket], qty [Max Qty], qty_out [Qty Dismantle] from OUT_PROD_DEFECT where SUB_SUB_PO='" & txtLabelOtherPart.Text & "' and department='" & globVar.department & "'"

            Else

                RJMessageBox.Show("QRCode not valid.")
                Play_Sound.Wrong()
                txtLabelOtherPart.Clear()
                Exit Sub

            End If


            Dim dtOutProd As DataTable = Database.GetData(query)

            DataGridView2.DataSource = dtOutProd

            For i As Integer = 0 To DataGridView2.RowCount - 1
                If DataGridView2.Rows(i).Index Mod 2 = 0 Then
                    DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next i

        Catch ex As Exception

            RJMessageBox.Show(ex.Message)

        End Try

    End Sub

    Sub loadDGVBawah()

        Dim Query As String

        Query = ""

        If txtLabelOtherPart.Text.StartsWith("D") Then 'Label Defect

            Query = "select code_stock_prod_others [Code Other],part_number [Material],lot_no [Lot No],inv_ctrl_date [ICD],batch_no [Batch No],traceability [Traceability],qty [Qty],datetime_insert [Time Save],insert_who [Save By] from stock_prod_others where CODE_OUT_PROD_DEFECT='" & txtLabelOtherPart.Text & "' and department='" & globVar.department & "'"

        ElseIf txtLabelOtherPart.Text.Contains(";") Then 'Label Flow Ticket

            Dim SplitSubSubPO() As String = txtLabelOtherPart.Text.Split(";"c)
            Query = "SELECT
	                    code_stock_prod_others [Code Other],
	                    part_number [Material],
	                    lot_no [Lot No],
	                    inv_ctrl_date [ICD],
	                    batch_no [Batch No],
	                    traceability [Traceability],
	                    qty [Qty],
	                    datetime_insert [Time Save],
	                    insert_who [Save By] 
                    FROM
	                    stock_prod_others 
                    WHERE
	                    CODE_OUT_PROD_DEFECT IN ( SELECT DISTINCT ( CODE_OUT_PROD_DEFECT ) FROM OUT_PROD_DEFECT WHERE SUB_SUB_PO = '" & SplitSubSubPO(0) & "' ) 
	                    AND department = '" & globVar.department & "'"

        ElseIf txtLabelOtherPart.Text.Contains("-") AndAlso txtLabelOtherPart.Text.Contains(";") = False Then 'Sub sub PO

            Query = "SELECT
	                    code_stock_prod_others [Code Other],
	                    part_number [Material],
	                    lot_no [Lot No],
	                    inv_ctrl_date [ICD],
	                    batch_no [Batch No],
	                    traceability [Traceability],
	                    qty [Qty],
	                    datetime_insert [Time Save],
	                    insert_who [Save By] 
                    FROM
	                    stock_prod_others 
                    WHERE
	                    CODE_OUT_PROD_DEFECT IN ( SELECT DISTINCT ( CODE_OUT_PROD_DEFECT ) FROM OUT_PROD_DEFECT WHERE SUB_SUB_PO = '" & txtLabelOtherPart.Text & "' ) 
	                    AND department = '" & globVar.department & "'"

        Else

            RJMessageBox.Show("QRCode not valid.")
            Play_Sound.Wrong()
            txtLabelOtherPart.Clear()
            Exit Sub

        End If

        Dim queryOthers As String = "select code_stock_prod_others [Code Other],part_number [Material],lot_no [Lot No],inv_ctrl_date [ICD],batch_no [Batch No],traceability [Traceability],qty [Qty],datetime_insert [Time Save],insert_who [Save By] from stock_prod_others where CODE_OUT_PROD_DEFECT='" & txtLabelOtherPart.Text & "' and department='" & globVar.department & "'"
        Dim dtOthers As DataTable = Database.GetData(Query)

        DataGridView4.DataSource = dtOthers

        For i As Integer = 0 To DataGridView4.RowCount - 1
            If DataGridView4.Rows(i).Index Mod 2 = 0 Then
                DataGridView4.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView4.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

    End Sub

    Private Sub OthersPart_Load(sender As Object, e As EventArgs) Handles Me.Load

        clear()

        btnPrintOthersPart.Enabled = False
        btnOtherSave.Enabled = False

    End Sub

    Sub clear()

        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView2.DataSource = Nothing
        DataGridView2.Rows.Clear()
        DataGridView2.Columns.Clear()

        DataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView4.DataSource = Nothing
        DataGridView4.Rows.Clear()
        DataGridView4.Columns.Clear()

    End Sub

    Private Sub txtLabelOtherPart_TextChanged(sender As Object, e As EventArgs) Handles txtLabelOtherPart.TextChanged

        clear()

    End Sub
End Class