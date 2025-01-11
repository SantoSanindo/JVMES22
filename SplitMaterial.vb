Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.VisualBasic.Logging

Public Class SplitMaterial
    Public Shared menu As String = "Split Label"

    Function SplitMaterialGenerateCode() As String

        Dim balanceCode As String = ""

        Try

            Dim queryCheckCodeSM As String = "SELECT top 1 inner_label FROM SPLIT_LABEL ORDER BY cast(replace(inner_label,'SM','') as int) desc"

            Dim dtCheckCodeSM As DataTable = Database.GetData(queryCheckCodeSM)

            If dtCheckCodeSM.Rows.Count > 0 Then

                Dim match As Match = Regex.Match(dtCheckCodeSM.Rows(0).Item("INNER_LABEL").ToString(), "^([A-Z]+)(\d+)$")

                If match.Success Then

                    Dim prefix As String = match.Groups(1).Value

                    Dim number As Integer = Integer.Parse(match.Groups(2).Value)

                    Dim nextNumber As Integer = number + 1

                    balanceCode = prefix & nextNumber.ToString()

                End If

            End If

        Catch ex As Exception

            RJMessageBox.Show("Error Generate SM Code : " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        Return balanceCode

    End Function

    Private Sub txtOuterLabel_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtOuterLabel.PreviewKeyDown
        Dim QrcodeValid As Boolean
        If (e.KeyData = Keys.Enter Or e.KeyData = Keys.Tab) Then
            If globVar.add > 0 Then
                Try
                    If txtOuterLabel.Text.StartsWith("MX2D") Then

                        QrcodeValid = QRCode.Baca(txtOuterLabel.Text)

                        If QrcodeValid = False Then
                            RJMessageBox.Show("QRCode Not Valid")
                            Play_Sound.Wrong()
                            txtOuterLabel.Clear()
                            Exit Sub
                        End If

                        If globVar.QRCode_PN = "" Or globVar.QRCode_lot = "" Or globVar.QRCode_Traceability = "" Or globVar.QRCode_Batch = "" Or globVar.QRCode_Inv = "" Then
                            RJMessageBox.Show("QRCode Not Valid")
                            Play_Sound.Wrong()
                            txtOuterLabel.Clear()
                            Exit Sub
                        End If

                    ElseIf txtOuterLabel.Text.StartsWith("NQ") Then

                        Dim checkNQ As String = "SELECT * FROM STOCK_CARD where qrcode_new='" & txtOuterLabel.Text & "' 
                                                                    and status = 'Receive From Main Store'
                                                                    and split_material = 0
                                                                    and [save]=1 
                                                                    and actual_qty > 0 "
                        Dim dtCheckNQ As DataTable = Database.GetData(checkNQ)
                        If dtCheckNQ.Rows.Count = 0 Then

                            RJMessageBox.Show("QRCode Does't exist in ministore.")
                            Play_Sound.Wrong()
                            txtOuterLabel.Clear()
                            Exit Sub

                        End If

                        globVar.QRCode_PN = dtCheckNQ.Rows(0).Item("material")
                        globVar.QRCode_lot = dtCheckNQ.Rows(0).Item("lot_no")
                        globVar.QRCode_Traceability = dtCheckNQ.Rows(0).Item("traceability")
                        globVar.QRCode_Batch = dtCheckNQ.Rows(0).Item("batch_no")
                        globVar.QRCode_Inv = dtCheckNQ.Rows(0).Item("inv_ctrl_date")

                        If globVar.QRCode_PN = "" Or globVar.QRCode_lot = "" Or globVar.QRCode_Traceability = "" Or globVar.QRCode_Batch = "" Or globVar.QRCode_Inv = "" Then
                            RJMessageBox.Show("Data NQ not complete.")
                            Play_Sound.Wrong()
                            txtOuterLabel.Clear()
                            Exit Sub
                        End If

                    Else

                        RJMessageBox.Show("QRCode Not Valid")
                        Play_Sound.Wrong()
                        txtOuterLabel.Clear()
                        Exit Sub

                    End If

                    Dim queryCheckInputStockDetail As String = "SELECT * FROM STOCK_CARD where lot_no='" & globVar.QRCode_lot & "' 
                                                                    AND MATERIAL='" & globVar.QRCode_PN & "' 
                                                                    AND batch_no='" & globVar.QRCode_Batch & "' 
                                                                    AND inv_ctrl_date='" & globVar.QRCode_Inv & "' 
                                                                    AND traceability='" & globVar.QRCode_Traceability & "' 
                                                                    and DEPARTMENT='" & globVar.department & "' 
                                                                    and status = 'Receive From Main Store'
                                                                    and split_material = 0
                                                                    and [save]=1 
                                                                    and actual_qty > 0 "
                    Dim dtCheckInputStockDetail As DataTable = Database.GetData(queryCheckInputStockDetail)

                    If dtCheckInputStockDetail.Rows.Count > 0 Then
                        txtOuterLabel.ReadOnly = True
                        txtMatLot.Text = globVar.QRCode_lot
                        txtMatQty.Text = dtCheckInputStockDetail.Rows(0).Item("qty")
                        txtMat.Text = globVar.QRCode_PN

                        Dim queryCheckSplitQty As String = "SELECT * FROM split_label where outer_label='" & txtOuterLabel.Text & "'"
                        Dim dtCheckSplitQty As DataTable = Database.GetData(queryCheckSplitQty)

                        If dtCheckSplitQty.Rows.Count > 0 Then
                            DGV_Atas()
                            Button2.Enabled = True
                        Else
                            Dim q = InputBox("How many packages do you want to split", "Split Outer Label")
                            If q = "" Or q Is Nothing Then
                                Exit Sub
                            End If

                            If IsNumeric(q) = False Then
                                RJMessageBox.Show("Sorry your input not number. Please try again.")
                                txtOuterLabel.Clear()
                            Else
                                Dim Pembagi As Double = Convert.ToInt64(txtMatQty.Text) / q
                                Dim vPembagian As Integer = Math.Round(Pembagi)
                                If CDbl(Pembagi) = CInt(Pembagi) Then
                                    For i As Integer = 1 To q

                                        Dim codeSM = SplitMaterialGenerateCode()

                                        Dim queryCheckForInsert As String = "SELECT * FROM split_label where outer_label='" & txtOuterLabel.Text & "' and inner_lot='" & globVar.QRCode_lot & "-" & i & "'"
                                        Dim dtCheckForInsert As DataTable = Database.GetData(queryCheckForInsert)
                                        If dtCheckForInsert.Rows.Count = 0 Then
                                            Dim sqlInsertInputStockDetail As String = "INSERT INTO split_label (outer_pn, outer_icd, outer_label, outer_qty, inner_label, inner_lot, inner_qty, outer_batch,outer_traceability,outer_lot, by_who)
                                            VALUES ('" & globVar.QRCode_PN & "','" & globVar.QRCode_Inv & "','" & txtOuterLabel.Text & "'," & txtMatQty.Text & ",'" & codeSM & "','" & i & "'," & vPembagian & ",'" & globVar.QRCode_Batch & "','" & globVar.QRCode_Traceability & "','" & globVar.QRCode_lot & "','" & globVar.username & "')"
                                            Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                            cmdInsertInputStockDetail.ExecuteNonQuery()
                                        End If
                                    Next
                                Else
                                    For i As Integer = 1 To q

                                        Dim codeSM = SplitMaterialGenerateCode()

                                        If i = q Then
                                            'MsgBox(Convert.ToInt64(txtMatQty.Text) - (vPembagian * (q - 1)))
                                            Dim queryCheckForInsert As String = "SELECT * FROM split_label where outer_label='" & txtOuterLabel.Text & "' and inner_lot='" & globVar.QRCode_lot & "-" & i & "'"
                                            Dim dtCheckForInsert As DataTable = Database.GetData(queryCheckForInsert)
                                            If dtCheckForInsert.Rows.Count = 0 Then
                                                Dim sqlInsertInputStockDetail As String = "INSERT INTO split_label (outer_pn, outer_icd, outer_label, outer_qty, inner_label, inner_lot, inner_qty, outer_batch,outer_traceability,outer_lot, by_who)
                                            VALUES ('" & globVar.QRCode_PN & "','" & globVar.QRCode_Inv & "','" & txtOuterLabel.Text & "'," & txtMatQty.Text & ",'" & codeSM & "','" & i & "'," & Convert.ToInt64(txtMatQty.Text) - (vPembagian * (q - 1)) & ",'" & globVar.QRCode_Batch & "','" & globVar.QRCode_Traceability & "','" & globVar.QRCode_lot & "','" & globVar.username & "')"
                                                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                                cmdInsertInputStockDetail.ExecuteNonQuery()
                                            End If
                                        Else
                                            'MsgBox(vPembagian & " - " & i)
                                            Dim queryCheckForInsert As String = "SELECT * FROM split_label where outer_label='" & txtOuterLabel.Text & "' and inner_lot='" & globVar.QRCode_lot & "-" & i & "'"
                                            Dim dtCheckForInsert As DataTable = Database.GetData(queryCheckForInsert)
                                            If dtCheckForInsert.Rows.Count = 0 Then
                                                Dim sqlInsertInputStockDetail As String = "INSERT INTO split_label (outer_pn, outer_icd, outer_label, outer_qty, inner_label, inner_lot, inner_qty, outer_batch,outer_traceability,outer_lot, by_who)
                                            VALUES ('" & globVar.QRCode_PN & "','" & globVar.QRCode_Inv & "','" & txtOuterLabel.Text & "'," & txtMatQty.Text & ",'" & codeSM & "','" & i & "'," & vPembagian & ",'" & globVar.QRCode_Batch & "','" & globVar.QRCode_Traceability & "','" & globVar.QRCode_lot & "','" & globVar.username & "')"
                                                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                                cmdInsertInputStockDetail.ExecuteNonQuery()
                                            End If
                                        End If
                                    Next
                                End If

                                Dim SqlUpdateSplitMaterialStockCard As String = "update STOCK_CARD set SPLIT_MATERIAL=1, ACTUAL_QTY=0 where ID=" & dtCheckInputStockDetail.Rows(0).Item("ID")
                                Dim cmdUpdateSplitMaterialStockCard = New SqlCommand(SqlUpdateSplitMaterialStockCard, Database.koneksi)
                                cmdUpdateSplitMaterialStockCard.ExecuteNonQuery()
                            End If

                            Button2.Enabled = True

                            DGV_Atas()

                            txtOuterLabel.ReadOnly = False
                            txtMatLot.Clear()
                            txtMatQty.Clear()
                            txtMat.Clear()
                            txtOuterLabel.Clear()
                        End If
                    Else
                        RJMessageBox.Show("Sorry this material not exists in DB or the QTY = 0.")
                        txtOuterLabel.ReadOnly = False
                        txtMatLot.Clear()
                        txtMatQty.Clear()
                        txtMat.Clear()
                        txtOuterLabel.Clear()
                    End If
                Catch ex As Exception
                    RJMessageBox.Show("Error Split Label - 1 =>" & ex.Message)
                End Try
            Else
                RJMessageBox.Show("Your Access cannot execute this action")
            End If
        End If
    End Sub

    'Sub DGV_Split_Qty(pOuterLabel As String)
    '    Try
    '        DataGridView1.DataSource = Nothing
    '        DataGridView1.Rows.Clear()
    '        DataGridView1.Columns.Clear()

    '        Dim queryCheckSplitQty As String = "SELECT ID [#], outer_pn [OUTER PN], OUTER_ICD [Outer ICD],OUTER_BATCH [Outer Batch], OUTER_TRACEABILITY [Outer Trace], OUTER_LOT [Outer LOT], OUTER_QTY [Outer Qty], INNER_LABEL [Inner Label], INNER_QTY [Inner Qty],datetime_insert [Date Time], by_who [Created By], [print] [Print] FROM split_label where outer_label='" & pOuterLabel & "' order by [print]"
    '        Dim dtCheckSplitQty As DataTable = Database.GetData(queryCheckSplitQty)
    '        DataGridView1.DataSource = dtCheckSplitQty

    '        Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
    '        check.Name = "check"
    '        check.HeaderText = "Check For Print"
    '        check.Width = 200
    '        check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
    '        DataGridView1.Columns.Insert(0, check)

    '        For i As Integer = 0 To DataGridView1.RowCount - 1
    '            If DataGridView1.Rows(i).Index Mod 2 = 0 Then
    '                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
    '            Else
    '                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
    '            End If
    '        Next i
    '    Catch ex As Exception
    '        RJMessageBox.Show("Error Split Label - 2 =>" & ex.Message)
    '    End Try
    'End Sub

    Sub DGV_Split_Qty2(pn As String, lot As String, batch As String, traceability As String, icd As String)
        Try
            DataGridView2.DataSource = Nothing
            DataGridView2.Rows.Clear()
            DataGridView2.Columns.Clear()

            Dim queryCheckSplitQty As String = "SELECT ID [#], outer_pn [OUTER PN], OUTER_LOT [Outer LOT], OUTER_ICD [Outer ICD],OUTER_BATCH [Outer Batch], OUTER_TRACEABILITY [Outer Trace], OUTER_QTY [Outer Qty], INNER_LABEL [Inner Label], INNER_QTY [Inner Qty],datetime_insert [Date Time], by_who [Created By], [print] [Print] FROM split_label where outer_pn='" & pn & "' and outer_lot='" & lot & "' and outer_traceability='" & traceability & "' and outer_batch='" & batch & "' and outer_icd='" & icd & "' order by [print]"
            Dim dtCheckSplitQty As DataTable = Database.GetData(queryCheckSplitQty)
            DataGridView2.DataSource = dtCheckSplitQty

            Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
            check.Name = "check"
            check.HeaderText = "Check All"
            check.Width = 200
            check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView2.Columns.Insert(0, check)

        Catch ex As Exception
            RJMessageBox.Show("Error Split Label - 2 =>" & ex.Message)
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
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End With
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

            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ResetSplitQty()
    End Sub

    Sub ResetSplitQty()
        txtOuterLabel.Clear()
        txtMat.Clear()
        txtMatLot.Clear()
        txtMatQty.Clear()
        txtManual.Clear()
        ComboBox1.SelectedIndex = -1
        SaveManual.Enabled = False
        CheckBox1.CheckState = CheckState.Checked
        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()
        DataGridView2.DataSource = Nothing
        DataGridView2.Columns.Clear()
        txtOuterLabel.ReadOnly = False
        txtOuterLabel.Select()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If globVar.view > 0 Then

            Try

                Dim _check As Integer = 0

                If DataGridView2.Rows.Count > 0 Then

                    For i = 0 To DataGridView2.Rows.Count - 1

                        If DataGridView2.Rows(i).Cells("check").Value = True Then

                            Dim queryCheckSplitLabel As String = "SELECT * FROM SPLIT_LABEL SL, MASTER_MATERIAL MM where SL.ID=" & DataGridView2.Rows(i).Cells("#").Value & " AND SL.OUTER_PN = MM.PART_NUMBER"
                            Dim dtCheckSplitLabel As DataTable = Database.GetData(queryCheckSplitLabel)

                            If dtCheckSplitLabel.Rows.Count > 0 Then

                                Dim queryCheckStockCard As String = "SELECT * FROM STOCK_CARD where qrcode='" & dtCheckSplitLabel.Rows(0).Item("INNER_LABEL") & "' AND DEPARTMENT='" & globVar.department & "' and status='Receive From Main Store'"
                                Dim dtCheckStockCard As DataTable = Database.GetData(queryCheckStockCard)

                                If dtCheckStockCard.Rows.Count = 0 Then

                                    Dim sqlInsertInputStockDetail As String = "INSERT INTO STOCK_CARD (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, QRCODE, MTS_NO,DEPARTMENT, STANDARD_PACK,STATUS,ACTUAL_QTY,ID_FROM_SPLIT,[SAVE])
                                        VALUES ('" & dtCheckSplitLabel.Rows(0).Item("OUTER_PN") & "'," & dtCheckSplitLabel.Rows(0).Item("INNER_QTY") & ",'" & dtCheckSplitLabel.Rows(0).Item("OUTER_ICD") & "','" & dtCheckSplitLabel.Rows(0).Item("OUTER_TRACEABILITY") & "','" & dtCheckSplitLabel.Rows(0).Item("INNER_LOT") & "','" & dtCheckSplitLabel.Rows(0).Item("OUTER_BATCH") & "','" & dtCheckSplitLabel.Rows(0).Item("INNER_LABEL") & "',(SELECT MTS_NO FROM STOCK_CARD WHERE QRCODE='" & dtCheckSplitLabel.Rows(0).Item("OUTER_LABEL") & "' AND STATUS='Receive From Main Store'),'" & globVar.department & "','NO','Receive From Main Store'," & dtCheckSplitLabel.Rows(0).Item("INNER_QTY") & ",(SELECT ID FROM STOCK_CARD WHERE QRCODE='" & dtCheckSplitLabel.Rows(0).Item("OUTER_LABEL") & "' AND STATUS='Receive From Main Store'),1)"

                                    Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)

                                    If cmdInsertInputStockDetail.ExecuteNonQuery() Then

                                        _PrintingSubAssyRawMaterial.txt_jenis_ticket.Text = "Split Material"
                                        _PrintingSubAssyRawMaterial.txt_Unique_id.Text = dtCheckSplitLabel.Rows(0).Item("INNER_LABEL")
                                        _PrintingSubAssyRawMaterial.txt_part_number.Text = dtCheckSplitLabel.Rows(0).Item("OUTER_PN")
                                        _PrintingSubAssyRawMaterial.txt_Part_Description.Text = dtCheckSplitLabel.Rows(0).Item("NAME")
                                        _PrintingSubAssyRawMaterial.txt_Qty.Text = dtCheckSplitLabel.Rows(0).Item("INNER_QTY")
                                        _PrintingSubAssyRawMaterial.txt_Traceability.Text = dtCheckSplitLabel.Rows(0).Item("OUTER_TRACEABILITY")
                                        _PrintingSubAssyRawMaterial.txt_Inv_crtl_date.Text = dtCheckSplitLabel.Rows(0).Item("OUTER_ICD")
                                        _PrintingSubAssyRawMaterial.txt_Batch_no.Text = dtCheckSplitLabel.Rows(0).Item("OUTER_BATCH")
                                        _PrintingSubAssyRawMaterial.txt_Lot_no.Text = dtCheckSplitLabel.Rows(0).Item("INNER_LOT")
                                        _PrintingSubAssyRawMaterial.txt_QR_Code.Text = dtCheckSplitLabel.Rows(0).Item("INNER_LABEL") & Environment.NewLine
                                        _PrintingSubAssyRawMaterial.btn_Print_Click(sender, e)

                                        If globVar.failPrint = "No" Then

                                            Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (remark,department,code_print)
                                            VALUES ('Split Material Print','" & globVar.department & "','" & dtCheckSplitLabel.Rows(0).Item("INNER_LABEL") & "')"
                                            Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                                            cmdInsertPrintingRecord.ExecuteNonQuery()

                                            Dim Sql As String = "update split_label set [print]=1 where ID=" & DataGridView2.Rows(i).Cells("#").Value
                                            Dim cmd = New SqlCommand(Sql, Database.koneksi)
                                            cmd.ExecuteNonQuery()

                                            'DGV_Split_Qty(txtOuterLabel.Text)
                                        End If

                                    End If

                                Else

                                    _PrintingSubAssyRawMaterial.txt_jenis_ticket.Text = "Split Material"
                                    _PrintingSubAssyRawMaterial.txt_Unique_id.Text = dtCheckSplitLabel.Rows(0).Item("INNER_LABEL")
                                    _PrintingSubAssyRawMaterial.txt_part_number.Text = dtCheckSplitLabel.Rows(0).Item("OUTER_PN")
                                    _PrintingSubAssyRawMaterial.txt_Part_Description.Text = dtCheckSplitLabel.Rows(0).Item("NAME")
                                    _PrintingSubAssyRawMaterial.txt_Qty.Text = dtCheckSplitLabel.Rows(0).Item("INNER_QTY")
                                    _PrintingSubAssyRawMaterial.txt_Traceability.Text = dtCheckSplitLabel.Rows(0).Item("OUTER_TRACEABILITY")
                                    _PrintingSubAssyRawMaterial.txt_Inv_crtl_date.Text = dtCheckSplitLabel.Rows(0).Item("OUTER_ICD")
                                    _PrintingSubAssyRawMaterial.txt_Batch_no.Text = dtCheckSplitLabel.Rows(0).Item("OUTER_BATCH")
                                    _PrintingSubAssyRawMaterial.txt_Lot_no.Text = dtCheckSplitLabel.Rows(0).Item("INNER_LOT")
                                    _PrintingSubAssyRawMaterial.txt_QR_Code.Text = dtCheckSplitLabel.Rows(0).Item("INNER_LABEL") & Environment.NewLine
                                    _PrintingSubAssyRawMaterial.btn_Print_Click(sender, e)

                                    If globVar.failPrint = "No" Then

                                        Dim Sql As String = "update split_label set [print]=1 where ID=" & DataGridView2.Rows(i).Cells("#").Value
                                        Dim cmd = New SqlCommand(Sql, Database.koneksi)
                                        cmd.ExecuteNonQuery()

                                        Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (remark,department,code_print)
                                            VALUES ('Split Material Reprint','" & globVar.department & "','" & dtCheckSplitLabel.Rows(0).Item("INNER_LABEL") & "')"
                                        Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                                        cmdInsertPrintingRecord.ExecuteNonQuery()

                                        'DGV_Split_Qty2(dtCheckSplitLabel.Rows(0).Item("OUTER_PN"),
                                        '               dtCheckSplitLabel.Rows(0).Item("OUTER_LOT"),
                                        '               dtCheckSplitLabel.Rows(0).Item("OUTER_BATCH"),
                                        '               dtCheckSplitLabel.Rows(0).Item("OUTER_TRACEABILITY"),
                                        '               dtCheckSplitLabel.Rows(0).Item("OUTER_ICD"))
                                        'DGV_Atas()

                                    End If

                                End If

                            End If

                            _check += 1

                        End If
                    Next

                    If _check <= 0 Then

                        RJMessageBox.Show("Please check first, if you want print")

                    End If

                Else

                    RJMessageBox.Show("No Data")

                End If

            Catch ex As Exception

                RJMessageBox.Show("Error Split Label - 3 =>" & ex.Message)

            End Try

        End If

    End Sub

    Private Sub SplitMaterial_Load(sender As Object, e As EventArgs) Handles Me.Load
        If globVar.view > 0 Then
            txtOuterLabel.Select()
        End If

        DGV_Atas()
    End Sub

    Sub DGV_Atas()
        Try
            DataGridView1.DataSource = Nothing
            DataGridView1.Rows.Clear()
            DataGridView1.Columns.Clear()

            DataGridView2.DataSource = Nothing
            DataGridView2.Rows.Clear()
            DataGridView2.Columns.Clear()

            Dim queryCheckSplitQty As String = "WITH NumberedRows AS (
                                                    SELECT 
                                                        s.outer_pn AS [Part Number],
                                                        s.outer_lot AS [Lot],
                                                        s.outer_icd AS [ICD],
                                                        s.outer_batch AS [Batch],
                                                        s.outer_traceability AS [Traceability],
                                                        s.outer_qty AS [Qty],
                                                        s.ID AS [ID],
                                                        (SELECT SUM(inner_qty) 
                                                         FROM SPLIT_LABEL 
                                                         WHERE outer_pn = s.outer_pn 
                                                           AND outer_lot = s.outer_lot 
                                                           AND outer_icd = s.outer_icd 
                                                           AND outer_batch = s.outer_batch 
                                                           AND outer_traceability = s.outer_traceability) AS [Actual Qty],
                                                        (SELECT COUNT(*) 
                                                         FROM SPLIT_LABEL 
                                                         WHERE outer_pn = s.outer_pn 
                                                           AND outer_lot = s.outer_lot 
                                                           AND outer_icd = s.outer_icd 
                                                           AND outer_batch = s.outer_batch 
                                                           AND outer_traceability = s.outer_traceability) AS [Total Split],
                                                        ROW_NUMBER() OVER (PARTITION BY s.outer_pn, s.outer_lot, s.outer_icd, s.outer_batch, s.outer_traceability ORDER BY s.ID) AS RowNum
                                                    FROM SPLIT_LABEL s
                                                )
                                                SELECT 
                                                    [Part Number],
                                                    [Lot],
                                                    [ICD],
                                                    [Batch],
                                                    [Traceability],
                                                    [Qty],
                                                    [Actual Qty],
                                                    [Total Split]
                                                FROM NumberedRows
                                                WHERE RowNum = 1
                                                ORDER BY [ID] desc
"
            Dim dtCheckSplitQty As DataTable = Database.GetData(queryCheckSplitQty)
            DataGridView1.DataSource = dtCheckSplitQty

            Dim sub_sub_po As DataGridViewButtonColumn = New DataGridViewButtonColumn
            sub_sub_po.Name = "check"
            sub_sub_po.HeaderText = "Check"
            sub_sub_po.Width = 50
            sub_sub_po.Text = "Check"
            sub_sub_po.UseColumnTextForButtonValue = True
            DataGridView1.Columns.Insert(0, sub_sub_po)

            Dim cancelSplit As DataGridViewButtonColumn = New DataGridViewButtonColumn
            cancelSplit.Name = "cancel"
            cancelSplit.HeaderText = "Cancel Split"
            cancelSplit.Width = 50
            cancelSplit.Text = "Cancel"
            cancelSplit.UseColumnTextForButtonValue = True
            DataGridView1.Columns.Insert(9, cancelSplit)

            For i As Integer = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                    DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next i
        Catch ex As Exception
            RJMessageBox.Show("Error Split Label - 2 =>" & ex.Message)
        End Try
    End Sub

    'Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
    '    If DataGridView1.CurrentCell.ColumnIndex = 9 Then
    '        If globVar.update > 0 Then
    '            Try
    '                If IsNumeric(DataGridView1.Rows(e.RowIndex).Cells("INNER QTY").Value) = True Then
    '                    Dim Sql As String = "update split_label set INNER_QTY=" & DataGridView1.Rows(e.RowIndex).Cells("INNER QTY").Value & " where ID=" & DataGridView1.Rows(e.RowIndex).Cells("ID").Value
    '                    Dim cmd = New SqlCommand(Sql, Database.koneksi)
    '                    cmd.ExecuteNonQuery()

    '                    DGV_Split_Qty(txtOuterLabel.Text)
    '                    RJMessageBox.Show("Success updated data")
    '                Else
    '                    RJMessageBox.Show("Your Value not number. Please change the value.")
    '                End If
    '            Catch ex As Exception
    '                RJMessageBox.Show("Error Split Label - 4 =>" & ex.Message)
    '            End Try
    '        Else
    '            RJMessageBox.Show("Your Access cannot execute this action")
    '        End If
    '    End If
    'End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If e.ColumnIndex = -1 Then
            Exit Sub
        End If

        If DataGridView1.Columns(e.ColumnIndex).Name = "check" Then
            DGV_Split_Qty2(DataGridView1.Rows(e.RowIndex).Cells("Part Number").Value,
                           DataGridView1.Rows(e.RowIndex).Cells("Lot").Value,
                           DataGridView1.Rows(e.RowIndex).Cells("Batch").Value,
                           DataGridView1.Rows(e.RowIndex).Cells("Traceability").Value,
                           DataGridView1.Rows(e.RowIndex).Cells("ICD").Value)
            Button1.Enabled = True
        End If

        If DataGridView1.Columns(e.ColumnIndex).Name = "cancel" Then
            If globVar.hakAkses.Contains("Administrator") = False Then
                RJMessageBox.Show("Cannot Access this action.")
                Exit Sub
            End If

            If DataGridView1.Rows(e.RowIndex).Cells("Actual Qty").Value < DataGridView1.Rows(e.RowIndex).Cells("Qty").Value Then
                RJMessageBox.Show("Cannot cancel this split because one of the split already in use in the PO")
                Exit Sub
            End If

            Dim result = RJMessageBox.Show("Are you sure to delete?", "Warning", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from split_label where outer_pn='" & DataGridView1.Rows(e.RowIndex).Cells("Part Number").Value & "' 
                                            and outer_lot='" & DataGridView1.Rows(e.RowIndex).Cells("Lot").Value & "' 
                                            and outer_icd='" & DataGridView1.Rows(e.RowIndex).Cells("ICD").Value & "' 
                                            and outer_batch='" & DataGridView1.Rows(e.RowIndex).Cells("Batch").Value & "' 
                                            and outer_traceability='" & DataGridView1.Rows(e.RowIndex).Cells("Traceability").Value & "'"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    If cmd.ExecuteNonQuery() Then
                        Dim SqlUpdate As String = "UPDATE STOCK_CARD SET split_material=0, actual_qty=qty FROM STOCK_CARD WHERE material='" & DataGridView1.Rows(e.RowIndex).Cells("Part Number").Value & "' 
                                                        and lot_no='" & DataGridView1.Rows(e.RowIndex).Cells("Lot").Value & "' 
                                                        and inv_ctrl_date='" & DataGridView1.Rows(e.RowIndex).Cells("ICD").Value & "' 
                                                        and batch_no='" & DataGridView1.Rows(e.RowIndex).Cells("Batch").Value & "' 
                                                        and traceability='" & DataGridView1.Rows(e.RowIndex).Cells("Traceability").Value & "'"
                        Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                        cmdUpdate.ExecuteNonQuery()

                        DGV_Atas()
                        RJMessageBox.Show("Success Cancel Split Label.")
                    End If
                Catch ex As Exception
                    RJMessageBox.Show("Error Split Material - 5 =>" & ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        If txtOuterLabel.Text <> "" Then
            Dim result = RJMessageBox.Show("Are you sure to delete?", "Warning", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                Try
                    Dim sql As String = "delete from split_label where outer_label='" & txtOuterLabel.Text & "'"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    If cmd.ExecuteNonQuery() Then
                        Dim SqlUpdate As String = "UPDATE STOCK_CARD SET split_material=0, actual_qty=qty FROM STOCK_CARD WHERE qrcode='" & txtOuterLabel.Text & "'"
                        Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                        cmdUpdate.ExecuteNonQuery()

                        ResetSplitQty()
                        RJMessageBox.Show("Success Cancel Split Label.")
                    End If
                Catch ex As Exception
                    RJMessageBox.Show("Error Split Material - 5 =>" & ex.Message)
                End Try
            End If
        Else
            RJMessageBox.Show("Outer label blank")
        End If
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        If e.ColumnIndex = 0 Then
            If DataGridView1.Rows(0).Cells(0).Value = True Then
                For i As Integer = 0 To DataGridView1.RowCount - 1
                    DataGridView1.Rows(i).Cells(0).Value = False
                Next
            ElseIf DataGridView1.Rows(0).Cells(0).Value = False Then
                For i As Integer = 0 To DataGridView1.RowCount - 1
                    DataGridView1.Rows(i).Cells(0).Value = True
                Next
            Else
                For i As Integer = 0 To DataGridView1.RowCount - 1
                    DataGridView1.Rows(i).Cells(0).Value = True
                Next
            End If
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            txtOuterLabel.Enabled = True
            SaveManual.Enabled = False
            txtManual.Enabled = False
            ComboBox1.Enabled = False
            txtManual.Clear()
            ComboBox1.SelectedIndex = -1
        Else
            SaveManual.Enabled = True
            txtManual.Enabled = True
            ComboBox1.Enabled = False
            txtOuterLabel.Enabled = False
            txtOuterLabel.Clear()
        End If
    End Sub

    Sub tampilDataComboBoxDataManualMaterial(material As String)
        Call Database.koneksi_database()
        Dim dtMaterial As DataTable = Database.GetData("select lot_no, inv_ctrl_date, traceability, batch_no, qty, qrcode from stock_card where department='" & globVar.department & "' and material='" & material & "' and actual_qty > 0 and sub_sub_po is null and status = 'Receive From Main Store' and [save] = 1 order by lot_no, inv_ctrl_date, traceability, batch_no")

        dtMaterial.Columns.Add("DisplayMember", GetType(String))

        For Each row As DataRow In dtMaterial.Rows
            If row("qrcode").ToString() = "Manual Input" Then
                row("DisplayMember") = "Lot No:" & row("lot_no").ToString() & "|ICD:" & row("inv_ctrl_date").ToString() & "|Trace:" & row("traceability").ToString() & "|Batch:" & row("batch_no").ToString() & "|Qty:" & row("qty").ToString() & "|Manual"
            Else
                row("DisplayMember") = "Lot No:" & row("lot_no").ToString() & "|ICD:" & row("inv_ctrl_date").ToString() & "|Trace:" & row("traceability").ToString() & "|Batch:" & row("batch_no").ToString() & "|Qty:" & row("qty").ToString()
            End If
        Next

        ComboBox1.DataSource = dtMaterial
        ComboBox1.DisplayMember = "DisplayMember"
        ComboBox1.ValueMember = "DisplayMember"
        ComboBox1.SelectedIndex = -1
    End Sub

    Private Sub txtManual_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtManual.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            tampilDataComboBoxDataManualMaterial(txtManual.Text)
            SaveManual.Enabled = True
            ComboBox1.Enabled = True
        End If
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If e.ColumnIndex = -1 Then
            Exit Sub
        End If

        If DataGridView2.Columns(e.ColumnIndex).Name = "check" Then
            If DataGridView2.Rows(e.RowIndex).Cells(0).Value = True Then
                DataGridView2.Rows(e.RowIndex).Cells(0).Value = False
            Else
                DataGridView2.Rows(e.RowIndex).Cells(0).Value = True
            End If
        End If
    End Sub

    Private Sub DataGridView2_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.ColumnHeaderMouseClick
        If e.ColumnIndex = 0 Then
            If DataGridView2.Rows(0).Cells(0).Value = True Then
                For i As Integer = 0 To DataGridView2.RowCount - 1
                    DataGridView2.Rows(i).Cells(0).Value = False
                Next
            ElseIf DataGridView2.Rows(0).Cells(0).Value = False Then
                For i As Integer = 0 To DataGridView2.RowCount - 1
                    DataGridView2.Rows(i).Cells(0).Value = True
                Next
            Else
                For i As Integer = 0 To DataGridView2.RowCount - 1
                    DataGridView2.Rows(i).Cells(0).Value = True
                Next
            End If
        End If

        For Each column As DataGridViewColumn In DataGridView2.Columns
            column.SortMode = DataGridViewColumnSortMode.NotSortable
        Next
    End Sub

    Private Sub SaveManual_Click(sender As Object, e As EventArgs) Handles SaveManual.Click
        If txtManual.Text = "" Or ComboBox1.SelectedIndex = -1 Then
            RJMessageBox.Show("Part Number blank. Please input first")
        End If

        If globVar.add > 0 Then
            Dim allmanualMaterial As String() = ComboBox1.Text.Split("|")
            Dim lotManualMaterial As String() = allmanualMaterial(0).Split(":")
            Dim icdManualMaterial As String() = allmanualMaterial(1).Split(":")
            Dim traceManualMaterial As String() = allmanualMaterial(2).Split(":")
            Dim batchManualMaterial As String() = allmanualMaterial(3).Split(":")
            Dim QtyManualMaterial As String() = allmanualMaterial(4).Split(":")

            Dim queryCheckInputStockDetail As String = "SELECT * FROM STOCK_CARD where lot_no='" & lotManualMaterial(1) & "' 
                                                            AND MATERIAL='" & txtManual.Text & "' 
                                                            AND batch_no='" & batchManualMaterial(1) & "' 
                                                            AND inv_ctrl_date='" & icdManualMaterial(1) & "' 
                                                            AND traceability='" & traceManualMaterial(1) & "' 
                                                            and DEPARTMENT='" & globVar.department & "' 
                                                            and status = 'Receive From Main Store'
                                                            and split_material = 0
                                                            and [save]=1 
                                                            and actual_qty > 0 "
            Dim dtCheckInputStockDetail As DataTable = Database.GetData(queryCheckInputStockDetail)

            If dtCheckInputStockDetail.Rows.Count > 0 Then
                txtMatLot.Text = lotManualMaterial(1)
                txtMatQty.Text = QtyManualMaterial(1)
                txtMat.Text = txtManual.Text

                Dim queryCheckSplitQty As String = "SELECT * FROM split_label where outer_pn='" & txtManual.Text & "' 
                                                        and outer_lot='" & lotManualMaterial(1) & "' 
                                                        and outer_batch='" & batchManualMaterial(1) & "' 
                                                        and outer_icd='" & icdManualMaterial(1) & "' 
                                                        and outer_traceability='" & traceManualMaterial(1) & "'"
                Dim dtCheckSplitQty As DataTable = Database.GetData(queryCheckSplitQty)

                If dtCheckSplitQty.Rows.Count > 0 Then
                    DGV_Atas()
                    Button2.Enabled = True
                Else
                    Dim q = InputBox("How many packages do you want to split", "Split Outer Label")
                    If q = "" Or q Is Nothing Then
                        Exit Sub
                    End If

                    If IsNumeric(q) = False Then
                        RJMessageBox.Show("Sorry your input not number. Please try again.")
                    Else
                        Dim Pembagi As Double = Convert.ToInt64(txtMatQty.Text) / q
                        Dim vPembagian As Integer = Math.Round(Pembagi)
                        If CDbl(Pembagi) = CInt(Pembagi) Then
                            For i As Integer = 1 To q
                                Dim queryCheckForInsert As String = "SELECT * FROM split_label where outer_pn='" & txtManual.Text & "' 
                                                                        and outer_batch='" & batchManualMaterial(1) & "' 
                                                                        and outer_icd='" & icdManualMaterial(1) & "' 
                                                                        and outer_traceability='" & traceManualMaterial(1) & "'
                                                                        and inner_lot='" & lotManualMaterial(1) & "-" & i & "'"
                                Dim dtCheckForInsert As DataTable = Database.GetData(queryCheckForInsert)
                                If dtCheckForInsert.Rows.Count = 0 Then
                                    Dim sqlInsertInputStockDetail As String = "INSERT INTO split_label (outer_pn, outer_icd, outer_label, outer_qty, inner_label, inner_lot, inner_qty, outer_batch,outer_traceability,outer_lot, by_who)
                                            VALUES ('" & txtManual.Text & "','" & icdManualMaterial(1) & "','Manual Input'," & txtMatQty.Text & ",'" & txtManual.Text & "-" & lotManualMaterial(1) & "-" & i & "','" & lotManualMaterial(1) & "-" & i & "'," & vPembagian & ",'" & batchManualMaterial(1) & "','" & traceManualMaterial(1) & "','" & lotManualMaterial(1) & "','" & globVar.username & "')"
                                    Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                    cmdInsertInputStockDetail.ExecuteNonQuery()
                                End If
                            Next
                        Else
                            For i As Integer = 1 To q
                                If i = q Then
                                    'MsgBox(Convert.ToInt64(txtMatQty.Text) - (vPembagian * (q - 1)))
                                    Dim queryCheckForInsert As String = "SELECT * FROM split_label where outer_label='" & txtOuterLabel.Text & "' and inner_lot='" & globVar.QRCode_lot & "-" & i & "'"
                                    Dim dtCheckForInsert As DataTable = Database.GetData(queryCheckForInsert)
                                    If dtCheckForInsert.Rows.Count = 0 Then
                                        Dim sqlInsertInputStockDetail As String = "INSERT INTO split_label (outer_pn, outer_icd, outer_label, outer_qty, inner_label, inner_lot, inner_qty, outer_batch,outer_traceability,outer_lot, by_who)
                                            VALUES ('" & txtManual.Text & "','" & icdManualMaterial(1) & "','Manual Input'," & txtMatQty.Text & ",'" & txtManual.Text & "-" & lotManualMaterial(1) & "-" & i & "','" & lotManualMaterial(1) & "-" & i & "'," & Convert.ToInt64(txtMatQty.Text) - (vPembagian * (q - 1)) & ",'" & batchManualMaterial(1) & "','" & traceManualMaterial(1) & "','" & lotManualMaterial(1) & "','" & globVar.username & "')"
                                        Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                        cmdInsertInputStockDetail.ExecuteNonQuery()
                                    End If
                                Else
                                    'MsgBox(vPembagian & " - " & i)
                                    Dim queryCheckForInsert As String = "SELECT * FROM split_label where outer_label='" & txtOuterLabel.Text & "' and inner_lot='" & globVar.QRCode_lot & "-" & i & "'"
                                    Dim dtCheckForInsert As DataTable = Database.GetData(queryCheckForInsert)
                                    If dtCheckForInsert.Rows.Count = 0 Then
                                        Dim sqlInsertInputStockDetail As String = "INSERT INTO split_label (outer_pn, outer_icd, outer_label, outer_qty, inner_label, inner_lot, inner_qty, outer_batch,outer_traceability,outer_lot, by_who)
                                            VALUES ('" & txtManual.Text & "','" & icdManualMaterial(1) & "','Manual Input'," & txtMatQty.Text & ",'" & txtManual.Text & "-" & lotManualMaterial(1) & "-" & i & "','" & lotManualMaterial(1) & "-" & i & "'," & vPembagian & ",'" & batchManualMaterial(1) & "','" & traceManualMaterial(1) & "','" & lotManualMaterial(1) & "','" & globVar.username & "')"
                                        Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                        cmdInsertInputStockDetail.ExecuteNonQuery()
                                    End If
                                End If
                            Next
                        End If

                        Dim SqlUpdateSplitMaterialStockCard As String = "update STOCK_CARD set SPLIT_MATERIAL=1, ACTUAL_QTY=0 where ID=" & dtCheckInputStockDetail.Rows(0).Item("ID")
                        Dim cmdUpdateSplitMaterialStockCard = New SqlCommand(SqlUpdateSplitMaterialStockCard, Database.koneksi)
                        cmdUpdateSplitMaterialStockCard.ExecuteNonQuery()
                    End If

                    Button2.Enabled = True

                    DGV_Atas()

                    txtMatLot.Clear()
                    txtMatQty.Clear()
                    txtMat.Clear()
                    txtManual.Clear()
                    ComboBox1.SelectedIndex = -1
                End If
            Else
                RJMessageBox.Show("Sorry this material not exists in DB or the QTY = 0.")
                txtOuterLabel.ReadOnly = False
                txtMatLot.Clear()
                txtMatQty.Clear()
                txtMat.Clear()
                txtOuterLabel.Clear()
            End If
        Else
            RJMessageBox.Show("Your access cannot execute this action")
        End If

    End Sub

    Private Sub txtManual_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtManual.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtManual_TextChanged(sender As Object, e As EventArgs) Handles txtManual.TextChanged
        If txtManual.Text.StartsWith("0") AndAlso txtManual.Text.Length > 1 Then
            txtManual.Text = txtManual.Text.TrimStart("0"c)
            txtManual.SelectionStart = txtManual.Text.Length
        End If

        SaveManual.Enabled = False
        ComboBox1.Enabled = False
    End Sub
End Class