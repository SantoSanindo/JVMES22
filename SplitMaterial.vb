Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class SplitMaterial
    Public Shared menu As String = "Split Label"

    Private Sub txtOuterLabel_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtOuterLabel.PreviewKeyDown
        If (e.KeyData = Keys.Enter Or e.KeyData = Keys.Tab) Then
            If globVar.add > 0 Then
                Try
                    QRCode.Baca(txtOuterLabel.Text)

                    Dim queryCheckInputStockDetail As String = "SELECT * FROM STOCK_CARD where lot_no='" & globVar.QRCode_lot & "' AND MATERIAL='" & globVar.QRCode_PN & "' and DEPARTMENT='" & globVar.department & "' and [save]=1"
                    Dim dtCheckInputStockDetail As DataTable = Database.GetData(queryCheckInputStockDetail)

                    If dtCheckInputStockDetail.Rows.Count > 0 Then
                        txtOuterLabel.ReadOnly = True
                        txtMatLot.Text = globVar.QRCode_lot
                        txtMatQty.Text = dtCheckInputStockDetail.Rows(0).Item("qty")
                        txtMat.Text = globVar.QRCode_PN

                        Dim queryCheckSplitQty As String = "SELECT * FROM split_label where outer_label='" & txtOuterLabel.Text & "'"
                        Dim dtCheckSplitQty As DataTable = Database.GetData(queryCheckSplitQty)

                        If dtCheckSplitQty.Rows.Count > 0 Then
                            DGV_Split_Qty(txtOuterLabel.Text)
                            Button1.Enabled = True
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
                                        Dim queryCheckForInsert As String = "SELECT * FROM split_label where outer_label='" & txtOuterLabel.Text & "' and outer_lot='" & globVar.QRCode_lot & "-" & i & "'"
                                        Dim dtCheckForInsert As DataTable = Database.GetData(queryCheckForInsert)
                                        If dtCheckForInsert.Rows.Count = 0 Then
                                            Dim sqlInsertInputStockDetail As String = "INSERT INTO split_label (outer_pn, outer_icd, outer_label, outer_qty, inner_label, inner_lot, inner_qty, outer_batch,outer_traceability,outer_lot)
                                            VALUES ('" & globVar.QRCode_PN & "','" & globVar.QRCode_Inv & "','" & txtOuterLabel.Text & "'," & txtMatQty.Text & ",'" & globVar.QRCode_PN & "-" & globVar.QRCode_lot & "-" & i & "','" & globVar.QRCode_lot & "-" & i & "'," & vPembagian & ",'" & globVar.QRCode_Batch & "','" & globVar.QRCode_Traceability & "','" & globVar.QRCode_lot & "')"
                                            Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                            cmdInsertInputStockDetail.ExecuteNonQuery()
                                        End If
                                    Next
                                Else
                                    For i As Integer = 1 To q
                                        If i = q Then
                                            'MsgBox(Convert.ToInt64(txtMatQty.Text) - (vPembagian * (q - 1)))
                                            Dim queryCheckForInsert As String = "SELECT * FROM split_label where outer_label='" & txtOuterLabel.Text & "' and outer_lot='" & globVar.QRCode_lot & "-" & i & "'"
                                            Dim dtCheckForInsert As DataTable = Database.GetData(queryCheckForInsert)
                                            If dtCheckForInsert.Rows.Count = 0 Then
                                                Dim sqlInsertInputStockDetail As String = "INSERT INTO split_label (outer_pn, outer_icd, outer_label, outer_qty, inner_label, inner_lot, inner_qty, outer_batch,outer_traceability,outer_lot)
                                            VALUES ('" & globVar.QRCode_PN & "','" & globVar.QRCode_Inv & "','" & txtOuterLabel.Text & "'," & txtMatQty.Text & ",'" & globVar.QRCode_PN & "-" & globVar.QRCode_lot & "-" & i & "','" & globVar.QRCode_lot & "-" & i & "'," & Convert.ToInt64(txtMatQty.Text) - (vPembagian * (q - 1)) & ",'" & globVar.QRCode_Batch & "','" & globVar.QRCode_Traceability & "','" & globVar.QRCode_lot & "')"
                                                Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                                cmdInsertInputStockDetail.ExecuteNonQuery()
                                            End If
                                        Else
                                            'MsgBox(vPembagian & " - " & i)
                                            Dim queryCheckForInsert As String = "SELECT * FROM split_label where outer_label='" & txtOuterLabel.Text & "' and outer_lot='" & globVar.QRCode_lot & "-" & i & "'"
                                            Dim dtCheckForInsert As DataTable = Database.GetData(queryCheckForInsert)
                                            If dtCheckForInsert.Rows.Count = 0 Then
                                                Dim sqlInsertInputStockDetail As String = "INSERT INTO split_label (outer_pn, outer_icd, outer_label, outer_qty, inner_label, inner_lot, inner_qty, outer_batch,outer_traceability,outer_lot)
                                            VALUES ('" & globVar.QRCode_PN & "','" & globVar.QRCode_Inv & "','" & txtOuterLabel.Text & "'," & txtMatQty.Text & ",'" & globVar.QRCode_PN & "-" & globVar.QRCode_lot & "-" & i & "','" & globVar.QRCode_lot & "-" & i & "'," & vPembagian & ",'" & globVar.QRCode_Batch & "','" & globVar.QRCode_Traceability & "','" & globVar.QRCode_lot & "')"
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

                            Button1.Enabled = True
                            Button2.Enabled = True

                            DGV_Split_Qty(txtOuterLabel.Text)
                        End If
                    Else
                        RJMessageBox.Show("Sorry this material not exists in DB. Please input in stock card.")
                    End If
                Catch ex As Exception
                    RJMessageBox.Show("Error Split Label - 1 =>" & ex.Message)
                End Try
            Else
                RJMessageBox.Show("Your Access cannot execute this action")
            End If
        End If
    End Sub

    Sub DGV_Split_Qty(pOuterLabel As String)
        Try
            DataGridView1.DataSource = Nothing
            DataGridView1.Rows.Clear()
            DataGridView1.Columns.Clear()

            Dim queryCheckSplitQty As String = "SELECT ID, outer_pn [OUTER PN], OUTER_ICD [OUTER ICD],OUTER_BATCH [OUTER BATCH], OUTER_TRACEABILITY [OUTER TRACE], OUTER_LOT [OUTER LOT], OUTER_QTY [OUTER QTY], INNER_LABEL [INNER LABEL], INNER_QTY [INNER QTY], [print] [PRINT] FROM split_label where outer_label='" & pOuterLabel & "' order by [print]"
            Dim dtCheckSplitQty As DataTable = Database.GetData(queryCheckSplitQty)
            DataGridView1.DataSource = dtCheckSplitQty

            Dim check As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn
            check.Name = "check"
            check.HeaderText = "Check For Print"
            check.Width = 100
            check.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView1.Columns.Insert(0, check)

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

    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ResetSplitQty()
    End Sub

    Sub ResetSplitQty()
        txtOuterLabel.Clear()
        txtMat.Clear()
        txtMatLot.Clear()
        txtMatQty.Clear()
        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()
        txtOuterLabel.ReadOnly = False
        txtOuterLabel.Select()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If globVar.view > 0 Then
            Try
                Dim nocheck As Integer = 0
                If DataGridView1.Rows.Count > 0 Then
                    For i = 0 To DataGridView1.Rows.Count - 1
                        If DataGridView1.Rows(i).Cells("check").Value = True Then
                            Dim queryCheckSplitLabel As String = "SELECT * FROM SPLIT_LABEL where ID=" & DataGridView1.Rows(i).Cells("ID").Value
                            Dim dtCheckSplitLabel As DataTable = Database.GetData(queryCheckSplitLabel)
                            If dtCheckSplitLabel.Rows.Count > 0 Then
                                Dim queryCheckStockCard As String = "SELECT * FROM STOCK_CARD where lot_no='" & dtCheckSplitLabel.Rows(0).Item("INNER_LOT") & "' AND MATERIAL='" & dtCheckSplitLabel.Rows(0).Item("OUTER_PN") & "' AND DEPARTMENT='" & globVar.department & "' and status='Receive From Main Store'"
                                Dim dtCheckStockCard As DataTable = Database.GetData(queryCheckStockCard)
                                If dtCheckStockCard.Rows.Count = 0 Then
                                    Dim sqlInsertInputStockDetail As String = "INSERT INTO STOCK_CARD (MATERIAL, QTY, INV_CTRL_DATE, TRACEABILITY, LOT_NO, BATCH_NO, QRCODE, MTS_NO,DEPARTMENT, STANDARD_PACK,STATUS,ACTUAL_QTY,ID_FROM_SPLIT,[SAVE])
                                        VALUES ('" & dtCheckSplitLabel.Rows(0).Item("OUTER_PN") & "'," & dtCheckSplitLabel.Rows(0).Item("INNER_QTY") & ",'" & dtCheckSplitLabel.Rows(0).Item("OUTER_ICD") & "','" & dtCheckSplitLabel.Rows(0).Item("OUTER_TRACEABILITY") & "','" & dtCheckSplitLabel.Rows(0).Item("INNER_LOT") & "','" & dtCheckSplitLabel.Rows(0).Item("OUTER_BATCH") & "','" & dtCheckSplitLabel.Rows(0).Item("INNER_LABEL") & "',(SELECT MTS_NO FROM STOCK_CARD WHERE QRCODE='" & dtCheckSplitLabel.Rows(0).Item("OUTER_LABEL") & "' AND STATUS='Receive From Main Store'),'" & globVar.department & "','NO','Receive From Main Store'," & dtCheckSplitLabel.Rows(0).Item("INNER_QTY") & ",(SELECT ID FROM STOCK_CARD WHERE QRCODE='" & dtCheckSplitLabel.Rows(0).Item("OUTER_LABEL") & "' AND STATUS='Receive From Main Store'),1)"
                                    Dim cmdInsertInputStockDetail = New SqlCommand(sqlInsertInputStockDetail, Database.koneksi)
                                    If cmdInsertInputStockDetail.ExecuteNonQuery() Then
                                        _PrintingSubAssyRawMaterial.txt_jenis_ticket.Text = "Split Material"
                                        _PrintingSubAssyRawMaterial.txt_Unique_id.Text = dtCheckSplitLabel.Rows(0).Item("INNER_LABEL")
                                        _PrintingSubAssyRawMaterial.txt_QR_Code.Text = dtCheckSplitLabel.Rows(0).Item("INNER_LABEL") & Environment.NewLine
                                        _PrintingSubAssyRawMaterial.btn_Print_Click(sender, e)

                                        If globVar.failPrint = "No" Then
                                            Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (remark,department,code_print)
                                            VALUES ('Split Material Print','" & globVar.department & "','" & dtCheckSplitLabel.Rows(0).Item("INNER_LABEL") & "')"
                                            Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                                            cmdInsertPrintingRecord.ExecuteNonQuery()

                                            Dim Sql As String = "update split_label set [print]=1 where ID=" & DataGridView1.Rows(i).Cells("ID").Value
                                            Dim cmd = New SqlCommand(Sql, Database.koneksi)
                                            cmd.ExecuteNonQuery()

                                            DGV_Split_Qty(txtOuterLabel.Text)
                                        End If
                                    End If
                                Else
                                    _PrintingSubAssyRawMaterial.txt_jenis_ticket.Text = "Split Material"
                                    _PrintingSubAssyRawMaterial.txt_Unique_id.Text = dtCheckSplitLabel.Rows(0).Item("INNER_LABEL")
                                    _PrintingSubAssyRawMaterial.txt_QR_Code.Text = dtCheckSplitLabel.Rows(0).Item("INNER_LABEL") & Environment.NewLine
                                    _PrintingSubAssyRawMaterial.btn_Print_Click(sender, e)

                                    If globVar.failPrint = "No" Then
                                        Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (remark,department,code_print)
                                            VALUES ('Split Material Reprint','" & globVar.department & "','" & dtCheckSplitLabel.Rows(0).Item("INNER_LABEL") & "')"
                                        Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                                        cmdInsertPrintingRecord.ExecuteNonQuery()

                                        DGV_Split_Qty(txtOuterLabel.Text)
                                    End If
                                End If
                            End If
                        Else
                            nocheck += 1
                        End If
                    Next

                    If nocheck >= DataGridView1.Rows.Count Then
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
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If DataGridView1.CurrentCell.ColumnIndex = 9 Then
            If globVar.update > 0 Then
                Try
                    If IsNumeric(DataGridView1.Rows(e.RowIndex).Cells("INNER QTY").Value) = True Then
                        Dim Sql As String = "update split_label set INNER_QTY=" & DataGridView1.Rows(e.RowIndex).Cells("INNER QTY").Value & " where ID=" & DataGridView1.Rows(e.RowIndex).Cells("ID").Value
                        Dim cmd = New SqlCommand(Sql, Database.koneksi)
                        cmd.ExecuteNonQuery()

                        DGV_Split_Qty(txtOuterLabel.Text)
                        RJMessageBox.Show("Success updated data")
                    Else
                        RJMessageBox.Show("Your Value not number. Please change the value.")
                    End If
                Catch ex As Exception
                    RJMessageBox.Show("Error Split Label - 4 =>" & ex.Message)
                End Try
            Else
                RJMessageBox.Show("Your Access cannot execute this action")
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If e.ColumnIndex = -1 Then
            Exit Sub
        End If

        If DataGridView1.Columns(e.ColumnIndex).Name = "check" Then
            If DataGridView1.Rows(e.RowIndex).Cells(0).Value = True Then
                DataGridView1.Rows(e.RowIndex).Cells(0).Value = False
            Else
                DataGridView1.Rows(e.RowIndex).Cells(0).Value = True
            End If
        End If
    End Sub
End Class