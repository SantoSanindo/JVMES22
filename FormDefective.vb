Imports System.CodeDom
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Reflection.Emit
Imports System.Runtime.Remoting
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports ZXing

Public Class FormDefective
    'Dim dept As String = "zQSFP"
    Dim dept As String = globVar.department
    Dim idLine As New List(Of Integer)
    Dim materialList As New List(Of String)
    Dim idBalanceMaterial As String

    Private Sub txtPONumber_KeyDown(sender As Object, e As KeyEventArgs)
        'If (e.KeyCode = Keys.Enter) Then
        '    If cbLineNumber.Text <> "" Then
        '        Try
        '            Call Database.koneksi_database()

        '            Dim adapter As SqlDataAdapter
        '            Dim ds As New DataSet

        '            'Dim sql As String = "SELECT * FROM SUB_PO where PO_NO=" & txtPONumber.Text & " and line='" & cbLineNumber.Text & "'"
        '            Dim sql As String = "SELECT * FROM PRE_PRODUCTION where NO_PO='" & txtPONumber.Text & "'"
        '            adapter = New SqlDataAdapter(sql, Database.koneksi)
        '            adapter.Fill(ds)

        '            Dim data As String
        '            data = ds.Tables(0).Rows(0).Item(1).ToString
        '            txtFinishGoodsPN.Text = data
        '        Catch ex As Exception
        '            MessageBox.Show("Error Insert" & ex.Message)
        '        End Try
        '    End If
        'End If
    End Sub

    Private Sub cbLineNumber_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLineNumber.SelectedIndexChanged
        Try
            Call Database.koneksi_database()
            Dim dtSubPO As DataTable = Database.GetData("select * from SUB_SUB_PO sp,main_po mp,master_finish_goods mfg where LINE='" & cbLineNumber.Text & "' and mp.id=sp.main_po and mfg.fg_part_number=mp.fg_pn and sp.status='Open'")

            txtSubSubPODefective.Text = dtSubPO.Rows(0).Item("SUB_SUB_PO")

            cbPONumber.Text = dtSubPO.Rows(0).Item("PO")

            cbFGPN.Text = dtSubPO.Rows(0).Item("FG_PN")

            txtDescDefective.Text = dtSubPO.Rows(0).Item("DESCRIPTION")

            txtSPQ.Text = dtSubPO.Rows(0).Item("SPQ")

            If cbFGPN.Text <> "" Then
                loadCBDefPartProcess(cbFGPN.Text)
            End If

            enableStatusInputInput(True)
            LoaddgWIP("")
            LoaddgOnHold("")
            loaddgBalance("")

            If dtSubPO.Rows(0).Item("level") = "FG" Then
                rbFG.Checked = True
                rbSA.Checked = False
                rbSA.Enabled = False
            Else
                rbSA.Checked = True
                rbFG.Checked = False
                rbFG.Enabled = False
            End If

            TableLayoutPanel14.Enabled = False
            TableLayoutPanel7.Enabled = False

        Catch ex As Exception
            MessageBox.Show("Error Load PO_NO")
        End Try
    End Sub

    Sub loadCBDefPartProcess(str As String)
        If str <> "System.Data.DataRowView" Then
            Try
                Call Database.koneksi_database()
                'Dim dtProcess As DataTable = Database.GetData("select distinct MASTER_PROCESS from MASTER_PROCESS_FLOW where MASTER_FINISH_GOODS_PN='" & str & "' ORDER BY ID")
                Dim dtProcess As DataTable = Database.GetData("select * from MASTER_PROCESS_FLOW where MASTER_FINISH_GOODS_PN='" & str & "' ORDER BY ID")

                'cbDefProcess1.Items.Clear()
                'cbDefProcess2.Items.Clear()
                cbWIPProcess.Items.Clear()
                materialList.Clear()

                If dtProcess.Rows.Count > 0 Then
                    For i As Integer = 0 To dtProcess.Rows.Count - 1
                        'cbDefProcess1.Items.Add(dtProcess.Rows(i)(0))
                        'cbDefProcess2.Items.Add(dtProcess.Rows(i)(0))
                        cbWIPProcess.Items.Add(dtProcess.Rows(i)(3))
                        cbOnHoldProcess.Items.Add(dtProcess.Rows(i)(3))
                        'Dim astr As String = dtProcess.Rows(i)(4)
                        If (IsDBNull(dtProcess.Rows(i)(4)) = True) Then
                            If i > 0 Then
                                materialList.Add(materialList(i - 1))
                            Else
                                materialList.Add("")
                            End If
                        Else
                            materialList.Add(dtProcess.Rows(i)(4))
                        End If
                    Next
                End If
                'cbDef.DataSource = dtMasterMaterial
                'cbDef.DisplayMember = "MASTER_PROCESS"
                'cbDef.ValueMember = "MASTER_PROCESS"

                'cbDef.Text = ""
            Catch ex As Exception
                MessageBox.Show("Error Load Process flow")
            End Try
        End If
    End Sub

    Private Sub rbDefMatLabel_CheckedChanged(sender As Object, e As EventArgs)
        'If rbDefMatLabel.Checked Then
        '    cbDefProcess1.Enabled = True
        '    txtDefMatLabel.Enabled = True
        '    btnDefectiveScan.Enabled = True
        'Else
        '    cbDefProcess1.Enabled = False
        '    txtDefMatLabel.Enabled = False
        '    btnDefectiveScan.Enabled = False
        'End If
    End Sub

    Private Sub rbDefProcess_CheckedChanged(sender As Object, e As EventArgs)
        'If rbDefProcess.Checked Then
        '    cbDefProcess2.Enabled = True
        '    txtDefTicketNoProcess.Enabled = True
        '    txtDefQtyProcess.Enabled = True
        '    btnDefConfirmProcess.Enabled = True
        'Else
        '    cbDefProcess2.Enabled = False
        '    txtDefTicketNoProcess.Enabled = False
        '    txtDefQtyProcess.Enabled = False
        '    btnDefConfirmProcess.Enabled = False
        'End If
    End Sub

    Private Sub cbDefProcess1_SelectedIndexChanged(sender As Object, e As EventArgs)
        'showToDGView(dgDefectiveListMaterial, cbDefProcess1.Text)
    End Sub

    Sub showToDGView(dg As DataGridView, strKey As String)
        Dim i As Integer

        Try
            Call Database.koneksi_database()
            Dim dtMaterialUsage As DataTable = Database.GetData("select distinct MATERIAL_USAGE from _OLD_MASTER_PROCESS_FLOW where MASTER_PROCESS='" & strKey & "' AND MASTER_FINISH_GOODS_PN='" & cbFGPN.Text & "'")
            'Dim dtMaterialInfo As DataTable = Database.GetData("select distinct MATERIAL_USAGE from MASTER_PROCESS_FLOW where MASTER_PROCESS='" & strKey & "'")

            Dim matUsage As String = dtMaterialUsage.Rows(i)(0).ToString()
            Dim matList() As String = matUsage.Split(";")

            With dg
                .Rows.Clear()

                .DefaultCellStyle.Font = New Font("Tahoma", 14)

                .ColumnCount = 4
                .Columns(0).Name = "No"
                .Columns(1).Name = "Part Number"
                .Columns(2).Name = "Name"
                .Columns(3).Name = "Check"

                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'For i = 0 To 7
                '    If ((i = 0) Or (i = 3) Or (i = 7)) Then
                '        .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '    Else
                '        .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                '    End If

                '    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable

                '    .Columns(i).ReadOnly = True
                '    'If (i <> 1) Then
                '    '    .Columns(i).ReadOnly = True
                '    'Else
                '    '    .Columns(i).ReadOnly = False
                '    'End If
                'Next

                .Columns(0).Width = Int(.Width * 0.05)
                .Columns(1).Width = Int(.Width * 0.15)
                .Columns(2).Width = Int(.Width * 0.68)
                .Columns(3).Width = Int(.Width * 0.1)


                .EnableHeadersVisualStyles = False
                With .ColumnHeadersDefaultCellStyle
                    .BackColor = Color.Navy
                    .ForeColor = Color.White
                    .Font = New Font("Tahoma", 13, FontStyle.Bold)
                    .Alignment = HorizontalAlignment.Center
                    .Alignment = ContentAlignment.MiddleCenter
                End With



                ''''''''''''''''''''''''''''''''''''''''''''
                For i = 0 To matList.Length - 1
                    If matList(i) = "" Then
                        Continue For
                    End If

                    Dim dtMaterialInfo As DataTable = Database.GetData("select distinct NAME from _OLD_MASTER_MATERIAL where PART_NUMBER='" & matList(i) & "'")

                    If dtMaterialInfo.Rows.Count > 0 Then
                        .Rows.Add(1)
                        .Item(0, i).Value = (i + 1).ToString()
                        .Item(1, i).Value = matList(i)
                        .Item(2, i).Value = dtMaterialInfo.Rows(0)(0)
                        .Rows(i).Cells(3) = New DataGridViewCheckBoxCell()
                        .Rows(i).Cells(3).Value = False
                    End If
                Next

                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            End With
        Catch ex As Exception
            MessageBox.Show("Error Load DGV")
        End Try
    End Sub

    Sub ReadonlyFormFG(_bool As Boolean)
        txtFGLabel.ReadOnly = _bool
        txtFGFlowTicket.ReadOnly = _bool
        TextBox3.ReadOnly = _bool
        txtFGLabel.Clear()
        txtFGFlowTicket.Clear()
        TextBox3.Clear()
    End Sub

    Sub ReadonlyFormSA(_bool As Boolean)
        txtSAFlowTicket.ReadOnly = _bool
        txtSABatchNo.ReadOnly = _bool
        TextBox6.ReadOnly = _bool
        txtSAFlowTicket.Clear()
        txtSABatchNo.Clear()
        TextBox6.Clear()
    End Sub

    Private Sub FormDefective_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadCBLine()

        ReadonlyFormSA(True)
        ReadonlyFormFG(True)

        'enableStatusInputInput(False)

        'TableLayoutPanel8.Enabled = False
        'TableLayoutPanel10.Enabled = False
        'DataGridView1.Enabled = False
        'DataGridView3.Enabled = False
    End Sub

    Sub enableStatusInputInput(statusEnable As Boolean)
        txtBalanceBarcode.Enabled = statusEnable
        txtBalanceQty.Enabled = statusEnable

        cbWIPProcess.Enabled = statusEnable
        txtWIPQuantity.Enabled = statusEnable
        txtWIPTicketNo.Enabled = statusEnable

        cbOnHoldProcess.Enabled = statusEnable
        txtOnHoldQty.Enabled = statusEnable
        txtOnHoldTicketNo.Enabled = statusEnable
    End Sub

    Sub loadCBLine()
        Try
            Dim query = "select ID, NAME from MASTER_LINE where DEPARTMENT='" & dept & "' ORDER BY NAME"
            Dim dtLine As DataTable = Database.GetData(query)

            cbLineNumber.Items.Clear()
            idLine.Clear()

            If dtLine.Rows.Count > 0 Then
                For i As Integer = 0 To dtLine.Rows.Count - 1
                    'cbDefProcess1.Items.Add(dtProcess.Rows(i)(0))
                    'cbDefProcess2.Items.Add(dtProcess.Rows(i)(0))
                    idLine.Add(dtLine.Rows(i)(0))
                    cbLineNumber.Items.Add(dtLine.Rows(i)(1))
                Next
            End If
            'cbDef.DataSource = dtMasterMaterial
            'cbDef.DisplayMember = "MASTER_PROCESS"
            'cbDef.ValueMember = "MASTER_PROCESS"

            'cbDef.Text = ""
        Catch ex As Exception
            MessageBox.Show("Error Load Line ->" & ex.Message)
        End Try
    End Sub


    ''''''''''''''''''''''''''''''''''''''' WIP FUNCTION

    Private Sub cbWIPProcess_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbWIPProcess.SelectedIndexChanged
        LoaddgWIP(cbWIPProcess.Text)
    End Sub

    Private Sub btnWIPAdd_Click(sender As Object, e As EventArgs) Handles btnWIPAdd.Click
        If cbWIPProcess.Text <> "" And txtWIPTicketNo.Text <> "" And txtWIPQuantity.Text <> "" Then
            Try
                Dim Part As String() = Nothing
                Dim sFlow_Ticket As String() = Nothing
                Dim i As Integer
                Dim statusSimpan As Integer = 1
                Dim sumQty As Double = 0

                Part = materialList(cbWIPProcess.SelectedIndex).Split(";")
                sFlow_Ticket = txtWIPTicketNo.Text.Split(";")

                'diulang sebanyak part number yg ada
                Call Database.koneksi_database()
                Dim noCode As String = WIPGenerateCode()

                If WIPcheckExistingData(txtSubSubPODefective.Text, dept) = False Then
                    For i = 0 To Part.Length - 2
                        Dim dtList As String = WIPGetDataTraceability(Part(i), cbLineNumber.Text, txtSubSubPODefective.Text, sFlow_Ticket(5))
                        Dim arrdtList() As String
                        arrdtList = dtList.Split(";")

                        Dim tgl As String = arrdtList(11).Replace("/", "-")

                        Dim sql As String = "INSERT INTO STOCK_PROD_WIP(CODE_STOCK_PROD_WIP,SUB_SUB_PO,FG_PN,PART_NUMBER,LOT_NO,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,PROCESS,QTY,PO,FLOW_TICKET_NO,DEPARTMENT,LINE,PENGALI) VALUES ('" &
                                        noCode & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & Part(i) & "','" & arrdtList(9) & "','" & arrdtList(7) & "','" & arrdtList(6) & "','" & arrdtList(8) & "','" & cbWIPProcess.Text & "'," & WIPGetQtyperPart(Part(i), 0).ToString.Replace(",", ".") & ",'" & cbPONumber.Text & "','" & sFlow_Ticket(5) & "','" & dept & "','" & cbLineNumber.Text & "'," & txtWIPQuantity.Text & ")"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        If cmd.ExecuteNonQuery() Then

                            Dim queryInsertToTemp As String = "insert into stock_card_temporary([MTS_NO], [DEPARTMENT], [MATERIAL], [STATUS], [STANDARD_PACK], [INV_CTRL_DATE], 
                                [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [DATETIME_INSERT], [SAVE], [QRCODE], [DATETIME_SAVE], 
                                [QTY], [ACTUAL_QTY], [FIFO], [ID_LEVEL], [LEVEL], [FLOW_TICKET]) 
                                select [MTS_NO], [DEPARTMENT], [MATERIAL], [STATUS], [STANDARD_PACK], [INV_CTRL_DATE], [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], 
                                [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [DATETIME_INSERT], [SAVE], [QRCODE], [DATETIME_SAVE], [QTY], [ACTUAL_QTY], [FIFO], [ID_LEVEL], [LEVEL], [FLOW_TICKET] 
                                from stock_card where status='Production Process' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                            Dim dtInsertTemp = New SqlCommand(queryInsertToTemp, Database.koneksi)
                            If dtInsertTemp.ExecuteNonQuery() Then
                                Dim queryDeleteStockCard As String = "delete from stock_card where status='Production Process' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                                Dim dtDeleteStockCard = New SqlCommand(queryDeleteStockCard, Database.koneksi)
                                If dtDeleteStockCard.ExecuteNonQuery() Then
                                    Dim queryCheckFlowTicketinTemp As String = "select * from stock_card_temporary where status='Production Process' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and flow_ticket='" & sFlow_Ticket(5) & "'"
                                    Dim dtCheckFlowTicketinTemp As DataTable = Database.GetData(queryCheckFlowTicketinTemp)

                                    Dim queryCheckSumUsage As String = "select * from stock_card where status='Production Request' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no=" & dtCheckFlowTicketinTemp.Rows(0).Item("lot_no")
                                    Dim dtCheckSumUsage As DataTable = Database.GetData(queryCheckSumUsage)

                                    sumQty = dtCheckSumUsage.Rows(0).Item("sum_qty") - WIPGetQtyperPart(Part(i), 0)

                                    Dim queryUpdateToStockCardqty As String = "update stock_card set actual_qty=" & sumQty.ToString.Replace(",", ".") & ",sum_qty=" & sumQty.ToString.Replace(",", ".") & " where status='Production Request' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no=" & dtCheckFlowTicketinTemp.Rows(0).Item("lot_no")
                                    Dim dtUpdateToStockCardqty = New SqlCommand(queryUpdateToStockCardqty, Database.koneksi)
                                    If dtUpdateToStockCardqty.ExecuteNonQuery() Then
                                        Dim queryDistintLotNoinTemp As String = "select DISTINCT(lot_no) from stock_card_temporary where status='Production Process' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                                        Dim dtDistintLotNoinTemp As DataTable = Database.GetData(queryDistintLotNoinTemp)
                                        If dtDistintLotNoinTemp.Rows.Count > 0 Then
                                            For iDistint = 0 To dtDistintLotNoinTemp.Rows.Count - 1
                                                If dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") = dtCheckFlowTicketinTemp.Rows(0).Item("lot_no") Then
                                                    Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & sumQty.ToString.Replace(",", ".") & ",@material='" & Part(i) & "',@lot_material='" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") & "'"
                                                    Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                                                Else
                                                    Dim sqlQtyLot = "select * from stock_card where status='Production Request' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no=" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no")
                                                    Dim dtQtyLot As DataTable = Database.GetData(sqlQtyLot)
                                                    Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtQtyLot.Rows(0).Item("sum_qty").ToString.Replace(",", ".") & ",@material='" & Part(i) & "',@lot_material='" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") & "'"
                                                    Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                                                End If
                                            Next
                                        End If
                                    End If
                                End If
                            End If

                            Dim queryDeleteStockCardTemporary As String = "delete from stock_card_temporary where status='Production Process' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                            Dim dtDeleteStockCardTemporary = New SqlCommand(queryDeleteStockCardTemporary, Database.koneksi)
                            If dtDeleteStockCardTemporary.ExecuteNonQuery() Then
                                statusSimpan *= 1
                            Else
                                statusSimpan *= 0
                            End If
                        Else
                            statusSimpan *= 0
                        End If
                    Next

                    If statusSimpan > 0 Then
                        MessageBox.Show("Success save data!!!")
                        LoaddgWIP("")
                        cbWIPProcess.SelectedIndex = -1
                        txtWIPTicketNo.Clear()
                        txtWIPQuantity.Clear()
                    End If
                Else
                    MessageBox.Show("Sorry, the data has been previously recorded. If you want to change it, you can click EDIT button.")
                End If
            Catch ex As Exception
                MessageBox.Show("Error Insert : " & ex.Message)
            End Try


        End If

    End Sub

    Function WIPcheckExistingData(subsubpo As String, department As String) As Boolean
        Dim data As Boolean = False

        Try
            Call Database.koneksi_database()
            Dim dttable As DataTable = Database.GetData("select distinct ID from STOCK_PROD_WIP where SUB_SUB_PO='" & subsubpo & "' AND FG_PN='" & cbFGPN.Text & "' AND PROCESS='" & cbWIPProcess.Text & "' AND DEPARTMENT='" & department & "'")

            If dttable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return True
        End Try

        Return data
    End Function

    Sub LoaddgWIP(proses As String)
        Dim i As Integer = 0

        Try
            'Call Database.koneksi_database()
            'Dim dtMaterialUsage As DataTable = Database.GetData("select distinct MATERIAL_USAGE from _OLD_MASTER_PROCESS_FLOW where MASTER_PROCESS='" & strKey & "' AND MASTER_FINISH_GOODS_PN='" & cbFGPN.Text & "'")
            ''Dim dtMaterialInfo As DataTable = Database.GetData("select distinct MATERIAL_USAGE from MASTER_PROCESS_FLOW where MASTER_PROCESS='" & strKey & "'")

            'Dim matUsage As String = dtMaterialUsage.Rows(i)(0).ToString()
            'Dim matList() As String = matUsage.Split(";")

            With dgWIP
                .Rows.Clear()

                .DefaultCellStyle.Font = New Font("Tahoma", 14)

                .ColumnCount = 9
                .Columns(0).Name = "No"
                .Columns(1).Name = "ID"
                .Columns(2).Name = "Process Name"
                .Columns(3).Name = "Ticket No."
                .Columns(4).Name = "Material PN"
                .Columns(5).Name = "Inv No."
                .Columns(6).Name = "MFG Date"
                '.Columns(7).Name = "Flow Ticket No."
                .Columns(7).Name = "Lot No."
                .Columns(8).Name = "Qty"

                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'For i = 0 To 7
                '    If ((i = 0) Or (i = 3) Or (i = 7)) Then
                '        .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '    Else
                '        .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                '    End If

                '    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable

                '    .Columns(i).ReadOnly = True
                '    'If (i <> 1) Then
                '    '    .Columns(i).ReadOnly = True
                '    'Else
                '    '    .Columns(i).ReadOnly = False
                '    'End If
                'Next

                .Columns(0).Width = Int(.Width * 0.05)
                .Columns(1).Width = Int(.Width * 0.08)
                .Columns(2).Width = Int(.Width * 0.26)
                .Columns(3).Width = Int(.Width * 0.08)
                .Columns(4).Width = Int(.Width * 0.1)
                .Columns(5).Width = Int(.Width * 0.1)
                .Columns(6).Width = Int(.Width * 0.15)
                .Columns(7).Width = Int(.Width * 0.08)
                .Columns(8).Width = Int(.Width * 0.08)
                '.Columns(9).Width = Int(.Width * 0.08)


                .EnableHeadersVisualStyles = False
                With .ColumnHeadersDefaultCellStyle
                    .BackColor = Color.Navy
                    .ForeColor = Color.White
                    .Font = New Font("Tahoma", 13, FontStyle.Bold)
                    .Alignment = HorizontalAlignment.Center
                    .Alignment = ContentAlignment.MiddleCenter
                End With



                ''''''''''''''''''''''''''''''''''''''''''''
                Dim sqlStr As String = ""

                If proses = "" Then
                    sqlStr = "select * from STOCK_PROD_WIP wip, sub_sub_po sp where wip.sub_sub_po=sp.sub_sub_po and sp.status='Open' and wip.line='" & cbLineNumber.Text & "' ORDER BY wip.CODE_STOCK_PROD_WIP"
                Else
                    sqlStr = "select * from STOCK_PROD_WIP wip, sub_sub_po sp where wip.sub_sub_po=sp.sub_sub_po and sp.status='Open' and wip.PROCESS='" & proses & "' and wip.line='" & cbLineNumber.Text & "' ORDER BY wip.CODE_STOCK_PROD_WIP"
                End If

                Dim dttable As DataTable = Database.GetData(sqlStr)


                If dttable.Rows.Count > 0 Then
                    For i = 0 To dttable.Rows.Count - 1
                        .Rows.Add(1)
                        .Item(0, i).Value = (i + 1).ToString()
                        .Item(1, i).Value = dttable.Rows(i)("CODE_STOCK_PROD_WIP")
                        .Item(2, i).Value = dttable.Rows(i)("PROCESS")
                        .Item(3, i).Value = dttable.Rows(i)("FLOW_TICKET_NO")
                        .Item(4, i).Value = dttable.Rows(i)("PART_NUMBER")
                        .Item(5, i).Value = dttable.Rows(i)("INV_CTRL_DATE")
                        .Item(6, i).Value = dttable.Rows(i)("TRACEABILITY")
                        '.Item(7, i).Value = dttable.Rows(i)("FLOW_TICKET_NO")
                        .Item(7, i).Value = dttable.Rows(i)("LOT_NO")
                        .Item(8, i).Value = dttable.Rows(i)("QTY")

                    Next
                End If
                'For i = 0 To matList.Length - 1
                '    If matList(i) = "" Then
                '        Continue For
                '    End If

                '    Dim dtMaterialInfo As DataTable = Database.GetData("select distinct NAME from _OLD_MASTER_MATERIAL where PART_NUMBER='" & matList(i) & "'")

                '    If dtMaterialInfo.Rows.Count > 0 Then
                '        .Rows.Add(1)
                '        .Item(0, i).Value = (i + 1).ToString()
                '        .Item(1, i).Value = matList(i)
                '        .Item(2, i).Value = dtMaterialInfo.Rows(0)(0)
                '        .Rows(i).Cells(3) = New DataGridViewCheckBoxCell()
                '        .Rows(i).Cells(3).Value = False
                '    End If
                'Next

                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            End With



        Catch ex As Exception
            MessageBox.Show("Error Load DGV WIP(Process)")
        End Try
    End Sub

    Function WIPGenerateCode() As String
        Dim wipCode As String = ""

        Try
            Call Database.koneksi_database()
            Dim dtCode As DataTable = Database.GetData("select distinct CODE_STOCK_PROD_WIP from STOCK_PROD_WIP ORDER BY CODE_STOCK_PROD_WIP DESC")

            If dtCode.Rows.Count > 0 Then
                Dim str As String = dtCode.Rows(0)(0)
                Dim dt As String = str.Substring(3, str.Length - 3) 'nomor id setelah WIP

                wipCode = "WIP" + (Convert.ToInt32(dt) + 1).ToString()
            Else
                wipCode = "WIP1"
            End If
        Catch ex As Exception
            MessageBox.Show("Error Insert WIP : " & ex.Message)
        End Try

        Return wipCode
    End Function

    Function WIPGetSubsubPO() As String
        Dim SubsubPO As String = ""

        Try
            Call Database.koneksi_database()

            'get id PO dari MAIN_PO
            Dim idPO As String = ""
            Dim dtCode As DataTable = Database.GetData("select distinct ID from MAIN_PO where PO='" & cbPONumber.Text & "'")
            If dtCode.Rows.Count > 0 Then
                idPO = dtCode.Rows(0)(0)
            End If
            dtCode.Clear()

            'get sub_sub_po dari SUB_SUB_PO
            dtCode = Database.GetData("select SUB_SUB_PO from SUB_SUB_PO where MAIN_PO='" & idPO & "' AND LINE='" & cbLineNumber.Text & "' AND STATUS='Open' ORDER BY ID")
            If dtCode.Rows.Count > 0 Then
                SubsubPO = dtCode.Rows(0)(0)
            End If

        Catch ex As Exception
            MessageBox.Show("Error Insert" & ex.Message)
        End Try



        Return SubsubPO
    End Function

    Function WIPGetQtyperPart(strComponent As String, idx As Integer) As Double
        Dim qty As Double = 0

        Try
            Call Database.koneksi_database()

            'get qty dari MATERIAL_USAGE_FINISH_GOODS
            Dim dtQty As String = ""
            Dim dtCode As DataTable = Database.GetData("select USAGE from MATERIAL_USAGE_FINISH_GOODS where FG_PART_NUMBER='" & cbFGPN.Text & "' AND COMPONENT='" & strComponent & "'")
            If dtCode.Rows.Count > 0 Then
                dtQty = dtCode.Rows(0)(0)
            End If
            dtCode.Clear()

            Double.TryParse(dtQty, qty)
        Catch ex As Exception
            MessageBox.Show("Error Insert" & ex.Message)
        End Try

        Dim dt As Double
        If idx = 0 Then
            Double.TryParse(txtWIPQuantity.Text, dt)
        ElseIf idx = 1 Then
            Double.TryParse(txtOnHoldQty.Text, dt)
        End If

        qty = qty * dt

        Return qty
    End Function

    Function WIPGetDataTraceability(pn_no As String, lineNo As String, subsubpo As String, _flow_ticket As String) As String
        Dim dataTrace As String
        Dim i As Integer

        dataTrace = ""
        Try
            Call Database.koneksi_database()

            'get id PO dari MAIN_PO
            Dim idPO As String = ""
            Dim dtCode As DataTable = Database.GetData("select * from stock_card where material='" & pn_no & "' AND LINE='" & lineNo & "' AND SUB_SUB_PO='" & subsubpo & "' and status='Production Process' and flow_ticket='" & _flow_ticket & "' ORDER BY FIFO DESC")
            If dtCode.Rows.Count > 0 Then
                For i = 0 To dtCode.Columns.Count - 1
                    dataTrace = dataTrace + IIf(IsDBNull(dtCode.Rows(0)(i).ToString()) = True, "", dtCode.Rows(0)(i).ToString()) + ";"
                Next
            End If

        Catch ex As Exception
            MessageBox.Show("Error Insert" & ex.Message)
        End Try

        Return dataTrace
    End Function

    Private Sub btnWIPEdit_Click(sender As Object, e As EventArgs) Handles btnWIPEdit.Click
        'MessageBox.Show((WIPGetQtyperPart("1717213000") * 10).ToString())
        Try
            Dim i As Integer
            Dim result As DialogResult

            If Convert.ToInt16(txtWIPQuantity.Text) > 0 Then
                result = MessageBox.Show("Are you sure want to update the ticket no. and quantity?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    Dim statusSimpan As Integer = 1

                    For i = 0 To dgWIP.RowCount - 2
                        Dim id As String = dgWIP.Rows(i).Cells(1).Value.ToString()
                        Dim processName As String = dgWIP.Rows(i).Cells(2).Value.ToString()
                        Dim matPN As String = dgWIP.Rows(i).Cells(4).Value.ToString()

                        Call Database.koneksi_database()

                        Dim sql As String = "update STOCK_PROD_WIP set FLOW_TICKET_NO='" & txtWIPTicketNo.Text & "',QTY='" & WIPGetQtyperPart(matPN, 0) & "',PENGALI=" & txtWIPQuantity.Text & " where CODE_STOCK_PROD_WIP='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS='" & processName & "'"
                        'Dim sql As String = "update STOCK_PROD_WIP set QTY='" & WIPGetQtyperPart(matPN, 0) & "' where CODE_STOCK_PROD_WIP='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS='" & processName & "'"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)

                        If cmd.ExecuteNonQuery() Then
                            statusSimpan *= 1
                        Else
                            statusSimpan *= 0
                        End If
                    Next

                    If statusSimpan > 0 Then
                        MessageBox.Show("Successfully update data!")
                        LoaddgWIP("")
                    End If
                End If
            Else
                result = MessageBox.Show("Are you sure want to delete WIP data?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    Dim statusSimpan As Integer = 1
                    Dim id As String = dgWIP.Rows(i).Cells(1).Value.ToString()

                    Call Database.koneksi_database()

                    'Dim sql As String = "update STOCK_PROD_WIP set TICKET_NO='" & txtWIPTicketNo.Text & "',QTY='" & WIPGetQtyperPart(matPN) & "' where CODE_STOCK_PROD_WIP='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS='" & processName & "'"
                    Dim sql As String = "delete from STOCK_PROD_WIP where CODE_STOCK_PROD_WIP='" & id & "'"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)

                    If cmd.ExecuteNonQuery() Then
                        statusSimpan *= 1
                    Else
                        statusSimpan *= 0
                    End If

                    If statusSimpan > 0 Then
                        MessageBox.Show("Successfully update data!")
                        LoaddgWIP("")
                    End If
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    '''''''''''''''''''''''''''''''''''''' END WIP FUNCTION


    '************************************* ON HOLD FUNCTION
    Private Sub btnOnHoldAdd_Click(sender As Object, e As EventArgs) Handles btnOnHoldAdd.Click
        If cbOnHoldProcess.Text <> "" And txtOnHoldTicketNo.Text <> "" And txtOnHoldQty.Text <> "" Then

            Try
                Dim Part As String() = Nothing
                Dim sFlow_Ticket As String() = Nothing
                Dim i As Integer
                Dim statusSimpan As Integer = 1
                Dim sumQty As Double = 0

                Part = materialList(cbOnHoldProcess.SelectedIndex).Split(";")

                sFlow_Ticket = txtOnHoldTicketNo.Text.Split(";")

                'diulang sebanyak part number yg ada
                Call Database.koneksi_database()
                Dim OnHoldCode As String = ONHOLDGenerateCode()

                For i = 0 To Part.Length - 2
                    Dim dtList As String = WIPGetDataTraceability(Part(i), cbLineNumber.Text, txtSubSubPODefective.Text, sFlow_Ticket(5))
                    Dim arrdtList() As String
                    arrdtList = dtList.Split(";")


                    Dim sql As String = "INSERT INTO STOCK_PROD_ONHOLD(CODE_STOCK_PROD_ONHOLD,SUB_SUB_PO,FG_PN,PART_NUMBER,LOT_NO,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,QTY,PO,PROCESS,FLOW_TICKET_NO,DEPARTMENT,LINE,PENGALI) VALUES ('" &
                                        OnHoldCode & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & Part(i) & "','" & arrdtList(9) & "','" & arrdtList(7) & "','" & arrdtList(6) & "','" & arrdtList(8) & "','" & WIPGetQtyperPart(Part(i), 1).ToString.Replace(",", ".") & "','" & cbPONumber.Text & "','" & cbOnHoldProcess.Text & "','" & sFlow_Ticket(5) & "','" & dept & "','" & cbLineNumber.Text & "'," & txtOnHoldQty.Text & ")"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    If cmd.ExecuteNonQuery() Then

                        Dim queryInsertToTemp As String = "insert into stock_card_temporary([MTS_NO], [DEPARTMENT], [MATERIAL], [STATUS], [STANDARD_PACK], [INV_CTRL_DATE], 
                                [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [DATETIME_INSERT], [SAVE], [QRCODE], [DATETIME_SAVE], 
                                [QTY], [ACTUAL_QTY], [FIFO], [ID_LEVEL], [LEVEL], [FLOW_TICKET]) 
                                select [MTS_NO], [DEPARTMENT], [MATERIAL], [STATUS], [STANDARD_PACK], [INV_CTRL_DATE], [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], 
                                [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [DATETIME_INSERT], [SAVE], [QRCODE], [DATETIME_SAVE], [QTY], [ACTUAL_QTY], [FIFO], [ID_LEVEL], [LEVEL], [FLOW_TICKET] 
                                from stock_card where status='Production Process' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                        Dim dtInsertTemp = New SqlCommand(queryInsertToTemp, Database.koneksi)
                        If dtInsertTemp.ExecuteNonQuery() Then
                            Dim queryDeleteStockCard As String = "delete from stock_card where status='Production Process' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                            Dim dtDeleteStockCard = New SqlCommand(queryDeleteStockCard, Database.koneksi)
                            If dtDeleteStockCard.ExecuteNonQuery() Then
                                Dim queryCheckFlowTicketinTemp As String = "select * from stock_card_temporary where status='Production Process' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and flow_ticket='" & sFlow_Ticket(5) & "'"
                                Dim dtCheckFlowTicketinTemp As DataTable = Database.GetData(queryCheckFlowTicketinTemp)

                                Dim queryCheckSumUsage As String = "select * from stock_card where status='Production Request' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no=" & dtCheckFlowTicketinTemp.Rows(0).Item("lot_no")
                                Dim dtCheckSumUsage As DataTable = Database.GetData(queryCheckSumUsage)

                                sumQty = dtCheckSumUsage.Rows(0).Item("sum_qty") - WIPGetQtyperPart(Part(i), 1)

                                Dim queryUpdateToStockCardqty As String = "update stock_card set actual_qty=" & sumQty.ToString.Replace(",", ".") & ",sum_qty=" & sumQty.ToString.Replace(",", ".") & " where status='Production Request' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no=" & dtCheckFlowTicketinTemp.Rows(0).Item("lot_no")
                                Dim dtUpdateToStockCardqty = New SqlCommand(queryUpdateToStockCardqty, Database.koneksi)
                                If dtUpdateToStockCardqty.ExecuteNonQuery() Then
                                    Dim queryDistintLotNoinTemp As String = "select DISTINCT(lot_no) from stock_card_temporary where status='Production Process' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                                    Dim dtDistintLotNoinTemp As DataTable = Database.GetData(queryDistintLotNoinTemp)
                                    If dtDistintLotNoinTemp.Rows.Count > 0 Then
                                        For iDistint = 0 To dtDistintLotNoinTemp.Rows.Count - 1
                                            If dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") = dtCheckFlowTicketinTemp.Rows(0).Item("lot_no") Then
                                                Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & sumQty.ToString.Replace(",", ".") & ",@material='" & Part(i) & "',@lot_material='" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") & "'"
                                                Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                                            Else
                                                Dim sqlQtyLot = "select * from stock_card where status='Production Request' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no=" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no")
                                                Dim dtQtyLot As DataTable = Database.GetData(sqlQtyLot)
                                                Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtQtyLot.Rows(0).Item("sum_qty").ToString.Replace(",", ".") & ",@material='" & Part(i) & "',@lot_material='" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") & "'"
                                                Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                                            End If
                                        Next
                                    End If
                                End If
                            End If
                        End If

                        Dim queryDeleteStockCardTemporary As String = "delete from stock_card_temporary where status='Production Process' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                        Dim dtDeleteStockCardTemporary = New SqlCommand(queryDeleteStockCardTemporary, Database.koneksi)
                        If dtDeleteStockCardTemporary.ExecuteNonQuery() Then
                            statusSimpan *= 1
                        Else
                            statusSimpan *= 0
                        End If
                    Else
                        statusSimpan *= 0
                    End If
                Next

                If statusSimpan > 0 Then
                    MessageBox.Show("Success save data!!!")
                    LoaddgOnHold("")
                    cbOnHoldProcess.SelectedIndex = -1
                    txtOnHoldTicketNo.Clear()
                    txtOnHoldQty.Clear()
                End If
            Catch ex As Exception
                MessageBox.Show("Error Insert : " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnOnHoldEdit_Click(sender As Object, e As EventArgs) Handles btnOnHoldEdit.Click
        Dim i As Integer
        Dim result As DialogResult

        Try
            If dgOnHold.Rows.Count > 1 Then
                If Convert.ToInt16(txtOnHoldQty.Text) > 0 Then
                    result = MessageBox.Show("Are you sure want to update the ticket no. and quantity?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If result = DialogResult.Yes Then
                        Dim statusSimpan As Integer = 1

                        For i = 0 To dgOnHold.RowCount - 2
                            Dim id As String = dgOnHold.Rows(i).Cells(1).Value.ToString()
                            Dim processName As String = dgOnHold.Rows(i).Cells(2).Value.ToString()
                            Dim matPN As String = dgOnHold.Rows(i).Cells(4).Value.ToString()

                            Call Database.koneksi_database()

                            Dim sql As String = "update STOCK_PROD_ONHOLD set FLOW_TICKET_NO='" & txtOnHoldTicketNo.Text & "',QTY='" & WIPGetQtyperPart(matPN, 1) & "',PENGALI=" & txtWIPQuantity.Text & " where CODE_STOCK_PROD_ONHOLD='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS='" & processName & "'"
                            'Dim sql As String = "update STOCK_PROD_ONHOLD set QTY='" & WIPGetQtyperPart(matPN, 1) & "' where CODE_STOCK_PROD_ONHOLD='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS='" & processName & "'"
                            Dim cmd = New SqlCommand(sql, Database.koneksi)

                            If cmd.ExecuteNonQuery() Then
                                statusSimpan *= 1
                            Else
                                statusSimpan *= 0
                            End If
                        Next

                        If statusSimpan > 0 Then
                            MessageBox.Show("Successfully update data!")
                            LoaddgOnHold("")
                        End If
                    End If
                Else
                    result = MessageBox.Show("Are you sure want to delete ON HOLD data?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If result = DialogResult.Yes Then
                        Dim statusSimpan As Integer = 1
                        Dim id As String = dgOnHold.Rows(i).Cells(1).Value.ToString()

                        Call Database.koneksi_database()

                        'Dim sql As String = "update STOCK_PROD_WIP set TICKET_NO='" & txtWIPTicketNo.Text & "',QTY='" & WIPGetQtyperPart(matPN) & "' where CODE_STOCK_PROD_WIP='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS='" & processName & "'"
                        Dim sql As String = "delete from STOCK_PROD_ONHOLD where CODE_STOCK_PROD_ONHOLD='" & id & "'"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)

                        If cmd.ExecuteNonQuery() Then
                            statusSimpan *= 1
                        Else
                            statusSimpan *= 0
                        End If

                        If statusSimpan > 0 Then
                            MessageBox.Show("Successfully update data!")
                            LoaddgOnHold("")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub LoaddgOnHold(proses As String)
        Dim i As Integer = 0

        Try
            With dgOnHold
                .Rows.Clear()

                .DefaultCellStyle.Font = New Font("Tahoma", 14)

                .ColumnCount = 10
                .Columns(0).Name = "No"
                .Columns(1).Name = "ID"
                .Columns(2).Name = "Process Name"
                .Columns(3).Name = "Ticket No."
                .Columns(4).Name = "Material PN"
                .Columns(5).Name = "Inv No."
                .Columns(6).Name = "MFG Date"
                '.Columns(7).Name = "Flow Ticket No."
                .Columns(7).Name = "Lot No."
                .Columns(8).Name = "Qty"

                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(0).Width = Int(.Width * 0.05)
                .Columns(1).Width = Int(.Width * 0.08)
                .Columns(2).Width = Int(.Width * 0.26)
                .Columns(3).Width = Int(.Width * 0.08)
                .Columns(4).Width = Int(.Width * 0.1)
                .Columns(5).Width = Int(.Width * 0.1)
                .Columns(6).Width = Int(.Width * 0.15)
                .Columns(7).Width = Int(.Width * 0.08)
                .Columns(8).Width = Int(.Width * 0.08)
                '.Columns(9).Width = Int(.Width * 0.08)


                .EnableHeadersVisualStyles = False
                With .ColumnHeadersDefaultCellStyle
                    .BackColor = Color.Navy
                    .ForeColor = Color.White
                    .Font = New Font("Tahoma", 13, FontStyle.Bold)
                    .Alignment = HorizontalAlignment.Center
                    .Alignment = ContentAlignment.MiddleCenter
                End With



                ''''''''''''''''''''''''''''''''''''''''''''
                Dim sqlStr As String = ""

                If proses = "" Then
                    sqlStr = "select * from STOCK_PROD_ONHOLD OH, sub_sub_po sp where oh.sub_sub_po=sp.sub_sub_po and sp.status='Open' and oh.line='" & cbLineNumber.Text & "' ORDER BY oh.CODE_STOCK_PROD_ONHOLD"
                Else
                    sqlStr = "select * from STOCK_PROD_ONHOLD oh, sub_sub_po sp where oh.sub_sub_po=sp.sub_sub_po and sp.status='Open' and oh.PROCESS='" & proses & "' and oh.line='" & cbLineNumber.Text & "' ORDER BY oh.CODE_STOCK_PROD_ONHOLD"
                End If

                Dim dttable As DataTable = Database.GetData(sqlStr)


                If dttable.Rows.Count > 0 Then
                    For i = 0 To dttable.Rows.Count - 1
                        .Rows.Add(1)
                        .Item(0, i).Value = (i + 1).ToString()
                        .Item(1, i).Value = dttable.Rows(i)("CODE_STOCK_PROD_ONHOLD")
                        .Item(2, i).Value = dttable.Rows(i)("PROCESS")
                        .Item(3, i).Value = dttable.Rows(i)("FLOW_TICKET_NO")
                        .Item(4, i).Value = dttable.Rows(i)("PART_NUMBER")
                        .Item(5, i).Value = dttable.Rows(i)("INV_CTRL_DATE")
                        .Item(6, i).Value = dttable.Rows(i)("TRACEABILITY")
                        '.Item(7, i).Value = dttable.Rows(i)("FLOW_TICKET_NO")
                        .Item(7, i).Value = dttable.Rows(i)("LOT_NO")
                        .Item(8, i).Value = dttable.Rows(i)("QTY")

                    Next
                End If
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            End With

        Catch ex As Exception
            MessageBox.Show("Error Load DGV On Hold (Process)")
        End Try
    End Sub

    Private Sub cbOnHoldProcess_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbOnHoldProcess.SelectedIndexChanged
        LoaddgOnHold(cbOnHoldProcess.Text)
    End Sub

    Function ONHOLDGenerateCode() As String
        Dim wipCode As String = ""

        Try
            Call Database.koneksi_database()
            Dim dtCode As DataTable = Database.GetData("select distinct CODE_STOCK_PROD_ONHOLD from STOCK_PROD_ONHOLD ORDER BY CODE_STOCK_PROD_ONHOLD DESC")

            If dtCode.Rows.Count > 0 Then
                Dim str As String = dtCode.Rows(0)(0)
                Dim dt As String = str.Substring(6, str.Length - 6) 'nomor id setelah ONHOLD

                wipCode = "ONHOLD" + (Convert.ToInt16(dt) + 1).ToString
            Else
                wipCode = "ONHOLD1"
            End If
        Catch ex As Exception
            MessageBox.Show("Error Insert ONHOLD : " & ex.Message)
        End Try

        Return wipCode
    End Function

    Function BalanceMaterialGenerateCode() As String
        Dim balanceCode As String = ""
        Try
            Dim queryCheckCodeBalanceMaterial As String = "select DISTINCT(ID_LEVEL) from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and status='Return To Mini Store'"
            Dim dtCheckCodeBalanceMAterial As DataTable = Database.GetData(queryCheckCodeBalanceMaterial)
            If dtCheckCodeBalanceMAterial.Rows.Count > 0 Then
                balanceCode = dtCheckCodeBalanceMAterial.Rows(0).Item("ID_LEVEL")
            Else
                balanceCode = "OB" & dtCheckCodeBalanceMAterial.Rows.Count + 1
            End If
        Catch ex As Exception
            MessageBox.Show("Error Insert Balance Material : " & ex.Message)
        End Try
        Return balanceCode
    End Function

    '************************************* END ON HOLD FUNCTION


    '===================================== BALANCE FUNCTION
    Private Sub btnBalanceAdd_Click(sender As Object, e As EventArgs) Handles btnBalanceAdd.Click
        If txtBalanceBarcode.Text <> "" And Convert.ToInt16(txtBalanceQty.Text) > 0 Then
            Try
                Call Database.koneksi_database()

                Dim dtsubsubpo As String = WIPGetSubsubPO()
                Dim qtyUpdate As Integer = 0

                Dim codeBalance As String = BalanceMaterialGenerateCode()

                Dim dtCekTable As DataTable = Database.GetData("select * from STOCK_CARD where STATUS='Return to Mini Store' AND MATERIAL='" & txtBalanceMaterialPN.Text & "' AND SUB_SUB_PO='" & dtsubsubpo & "' AND LINE='" & cbLineNumber.Text & "' and department='" & globVar.department & "' ORDER BY LOT_NO")
                If dtCekTable.Rows.Count > 0 Then

                    Dim result = MessageBox.Show("Sorry, the data has been previously recorded. Are you sure for update?.", "Are You Sure?", MessageBoxButtons.YesNo)

                    If result = DialogResult.Yes Then

                        If dtCekTable.Rows(0).Item("actual_qty") > Convert.ToInt16(txtBalanceQty.Text) Then
                            qtyUpdate = dtCekTable.Rows(0).Item("actual_qty") - Convert.ToInt16(txtBalanceQty.Text)

                            Dim sqlCheckStockCard As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and status='Production Request' and material='" & txtBalanceMaterialPN.Text & "' and department='" & globVar.department & "'"
                            Dim dtCheckStockCard As DataTable = Database.GetData(sqlCheckStockCard)

                            If dtCheckStockCard.Rows.Count > 0 Then
                                Dim sqlUpdateStock As String = "update STOCK_CARD set ACTUAL_QTY=actual_qty+" & qtyUpdate & " where ID='" & dtCheckStockCard.Rows(0).Item("ID") & "'"
                                Dim cmdUpdateStock = New SqlCommand(sqlUpdateStock, Database.koneksi)
                                cmdUpdateStock.ExecuteNonQuery()
                            End If
                        Else
                            qtyUpdate = Convert.ToInt16(txtBalanceQty.Text) - dtCekTable.Rows(0).Item("actual_qty")

                            Dim sqlCheckStockCard As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and status='Production Request' and material='" & txtBalanceMaterialPN.Text & "' and department='" & globVar.department & "'"
                            Dim dtCheckStockCard As DataTable = Database.GetData(sqlCheckStockCard)

                            If dtCheckStockCard.Rows.Count > 0 Then
                                Dim sqlUpdateStock As String = "update STOCK_CARD set ACTUAL_QTY=actual_qty-" & qtyUpdate & " where ID='" & dtCheckStockCard.Rows(0).Item("ID") & "'"
                                Dim cmdUpdateStock = New SqlCommand(sqlUpdateStock, Database.koneksi)
                                cmdUpdateStock.ExecuteNonQuery()
                            End If
                        End If

                        Dim sqlUpdate As String = "update STOCK_CARD set ACTUAL_QTY=" & Convert.ToInt16(txtBalanceQty.Text) & " where ID='" & dtCekTable.Rows(0).Item("ID") & "'"
                        Dim cmdUpdate = New SqlCommand(sqlUpdate, Database.koneksi)
                        If cmdUpdate.ExecuteNonQuery() Then
                            MessageBox.Show("Update Success.")
                        End If
                    End If

                Else
                    Dim idData As String = ""

                    Dim querySelectStockCard As String = "select id,MATERIAL,lot_no,inv_ctrl_date,traceability,batch_no,qty,actual_qty from STOCK_CARD where STATUS='Production Request' AND MATERIAL='" & txtBalanceMaterialPN.Text & "' AND SUB_SUB_PO='" & dtsubsubpo & "' AND LINE='" & cbLineNumber.Text & "' and actual_qty > 0 and department='" & globVar.department & "' ORDER BY LOT_NO"
                    Dim dtTable As DataTable = Database.GetData(querySelectStockCard)
                    If dtTable.Rows.Count > 0 Then
                        idData = dtTable.Rows(0)(0)

                        Dim sqlUpdate As String = "update STOCK_CARD set ACTUAL_QTY=actual_qty-" & txtBalanceQty.Text & ",SUM_QTY=SUM_QTY-" & txtBalanceQty.Text & " where ID='" & idData & "'"
                        Dim cmdUpdate = New SqlCommand(sqlUpdate, Database.koneksi)
                        If cmdUpdate.ExecuteNonQuery() Then

                            Dim sqlInsert As String = "insert into STOCK_CARD(MTS_NO,DEPARTMENT,MATERIAL,STATUS,STANDARD_PACK,INV_CTRL_DATE,TRACEABILITY,BATCH_NO,LOT_NO,FINISH_GOODS_PN,PO,SUB_PO,SUB_SUB_PO,LINE,QRCODE,QTY,ACTUAL_QTY,ID_LEVEL,LEVEL)" &
                                                        "select MTS_NO,DEPARTMENT,MATERIAL,'Return to Mini Store',STANDARD_PACK,INV_CTRL_DATE,TRACEABILITY,BATCH_NO,LOT_NO,FINISH_GOODS_PN,PO,SUB_PO,SUB_SUB_PO,LINE,'" & txtBalanceBarcode.Text & "',QTY," & txtBalanceQty.Text & ",'" & codeBalance & "','OB' from STOCK_CARD where ID='" & idData & "'"
                            Dim cmdInsert = New SqlCommand(sqlInsert, Database.koneksi)

                            If cmdInsert.ExecuteNonQuery() Then
                                MessageBox.Show("Success save data!!!")
                                loaddgBalance("")
                            End If

                        End If

                    Else
                        MessageBox.Show("This Material qty=0 or this material not exist in DB")
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("Error Insert : " & ex.Message)
            End Try
        Else
            MessageBox.Show("Sorry please fill the blank or Qty Return more than maximum value")
        End If
    End Sub

    Private Sub dgBalance_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgBalance.CellValueChanged
        If dgBalance.Columns(e.ColumnIndex).Name = "Actual Qty" Then
            Try
                Dim qtyUpdate As Integer = 0
                Dim sqlCheckQty As String = "select * from stock_card where id='" & dgBalance.Rows(e.RowIndex).Cells("id").Value & "'"
                Dim dtCheckQtye As DataTable = Database.GetData(sqlCheckQty)
                If dtCheckQtye.Rows(0).Item("actual_qty") > dgBalance.Rows(e.RowIndex).Cells("Actual Qty").Value Then
                    qtyUpdate = dtCheckQtye.Rows(0).Item("actual_qty") - dgBalance.Rows(e.RowIndex).Cells("Actual Qty").Value

                    Dim sqlCheckStockCard As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and status='Production Request' and material='" & dgBalance.Rows(e.RowIndex).Cells("Material").Value & "'"
                    Dim dtCheckStockCard As DataTable = Database.GetData(sqlCheckStockCard)

                    If dtCheckStockCard.Rows.Count > 0 Then
                        Dim sqlUpdateStock As String = "update STOCK_CARD set ACTUAL_QTY=actual_qty+" & qtyUpdate & " where ID='" & dtCheckStockCard.Rows(0).Item("ID") & "'"
                        Dim cmdUpdateStock = New SqlCommand(sqlUpdateStock, Database.koneksi)
                        cmdUpdateStock.ExecuteNonQuery()
                    End If
                Else
                    qtyUpdate = dgBalance.Rows(e.RowIndex).Cells("Actual Qty").Value - dtCheckQtye.Rows(0).Item("actual_qty")

                    Dim sqlCheckStockCard As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and status='Production Request' and material='" & dgBalance.Rows(e.RowIndex).Cells("Material").Value & "'"
                    Dim dtCheckStockCard As DataTable = Database.GetData(sqlCheckStockCard)

                    If dtCheckStockCard.Rows.Count > 0 Then
                        Dim sqlUpdateStock As String = "update STOCK_CARD set ACTUAL_QTY=actual_qty-" & qtyUpdate & " where ID='" & dtCheckStockCard.Rows(0).Item("ID") & "'"
                        Dim cmdUpdateStock = New SqlCommand(sqlUpdateStock, Database.koneksi)
                        cmdUpdateStock.ExecuteNonQuery()
                    End If
                End If

                Dim sqlUpdate As String = "update STOCK_CARD set ACTUAL_QTY=" & dgBalance.Rows(e.RowIndex).Cells("Actual Qty").Value & " where ID='" & dgBalance.Rows(e.RowIndex).Cells("ID").Value & "'"
                Dim cmdUpdate = New SqlCommand(sqlUpdate, Database.koneksi)
                If cmdUpdate.ExecuteNonQuery() Then
                    MessageBox.Show("Update Success.")
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnBalanceEdit_Click(sender As Object, e As EventArgs) Handles btnBalanceEdit.Click
        'BalanceParsingMaterialPN("MX2D1P1710813000Q000000000270S00202203012213Q0002BWL2041112212D202204114L               ChinaMLX001")

        Try


            Dim i As Integer
            Dim result As DialogResult

            If dgBalance.Rows.Count > 1 Then
                If Convert.ToInt16(txtBalanceQty.Text) > 0 Then
                    result = MessageBox.Show("Are you sure want to update quantity?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If result = DialogResult.Yes Then
                        Dim statusSimpan As Integer = 1
                        'masih belum-+

                        For i = 0 To dgOnHold.RowCount - 2
                            Dim id As String = dgOnHold.Rows(i).Cells(1).Value.ToString()
                            Dim processName As String = dgOnHold.Rows(i).Cells(2).Value.ToString()
                            Dim matPN As String = dgOnHold.Rows(i).Cells(4).Value.ToString()

                            Call Database.koneksi_database()

                            Dim sql As String = "update STOCK_PROD_ONHOLD set FLOW_TICKET_NO='" & txtOnHoldTicketNo.Text & "',QTY='" & WIPGetQtyperPart(matPN, 1) & "' where CODE_STOCK_PROD_ONHOLD='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS='" & processName & "'"
                            'Dim sql As String = "update STOCK_PROD_ONHOLD set QTY='" & WIPGetQtyperPart(matPN, 1) & "' where CODE_STOCK_PROD_ONHOLD='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS='" & processName & "'"
                            Dim cmd = New SqlCommand(sql, Database.koneksi)

                            If cmd.ExecuteNonQuery() Then
                                statusSimpan *= 1
                            Else
                                statusSimpan *= 0
                            End If
                        Next

                        If statusSimpan > 0 Then
                            MessageBox.Show("Successfully update data!")
                            LoaddgOnHold("")
                        End If
                    End If
                Else
                    result = MessageBox.Show("Are you sure want to delete ON HOLD data?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If result = DialogResult.Yes Then
                        Dim statusSimpan As Integer = 1
                        Dim id As String = dgOnHold.Rows(i).Cells(1).Value.ToString()

                        Call Database.koneksi_database()

                        'Dim sql As String = "update STOCK_PROD_WIP set TICKET_NO='" & txtWIPTicketNo.Text & "',QTY='" & WIPGetQtyperPart(matPN) & "' where CODE_STOCK_PROD_WIP='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS='" & processName & "'"
                        Dim sql As String = "delete from STOCK_PROD_ONHOLD where CODE_STOCK_PROD_ONHOLD='" & id & "'"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)

                        If cmd.ExecuteNonQuery() Then
                            statusSimpan *= 1
                        Else
                            statusSimpan *= 0
                        End If

                        If statusSimpan > 0 Then
                            MessageBox.Show("Successfully update data!")
                            LoaddgOnHold("")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtBalanceBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBalanceBarcode.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                Dim splitQRCode() As String = txtBalanceBarcode.Text.Split(New String() {"1P", "12D", "4L", "MLX"}, StringSplitOptions.None)
                Dim splitQRCode1P() As String = splitQRCode(1).Split(New String() {"Q", "S", "13Q", "B"}, StringSplitOptions.None)

                txtBalanceMaterialPN.Text = BalanceParsingMaterialPN(txtBalanceBarcode.Text)

                Dim queryCheckProductionProcess As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and material='" & txtBalanceMaterialPN.Text & "' and status='Return To Mini Store' and actual_qty > 0 and department='" & globVar.department & "'"
                Dim dttableProductionProcess As DataTable = Database.GetData(queryCheckProductionProcess)

                If dttableProductionProcess.Rows.Count = 0 Then
                    Dim queryCheck As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and material='" & txtBalanceMaterialPN.Text & "' and status='Production Request' and actual_qty > 0 and department='" & globVar.department & "' and lot_no=" & splitQRCode1P(3)
                    Dim dttable As DataTable = Database.GetData(queryCheck)
                    If dttable.Rows.Count > 0 Then
                        txtBalanceQty.Text = dttable.Rows(0).Item("actual_qty")
                    Else
                        MsgBox("This material Qty = 0 or this material not exist in DB.")
                        txtBalanceBarcode.Clear()
                        txtBalanceMaterialPN.Clear()
                    End If
                Else
                    MsgBox("Double Scan")
                    txtBalanceBarcode.Clear()
                    txtBalanceMaterialPN.Clear()
                End If

                SendKeys.Send("{TAB}")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Function BalanceParsingMaterialPN(strBarcode As String)
        Dim dtReturn As String = ""

        Dim idxStart As Integer = strBarcode.IndexOf("1P")
        Dim idxStop As Integer = strBarcode.IndexOf("Q")
        dtReturn = strBarcode.Substring(idxStart + 2, (idxStop - idxStart - 2))

        'MessageBox.Show(dtReturn)

        Return dtReturn
    End Function

    Function BalanceParsingMaterialLotNo(strBarcode As String)
        Dim dtReturn As String = ""

        Dim idxStart As Integer = strBarcode.IndexOf("1P")
        Dim idxStop As Integer = strBarcode.IndexOf("Q")
        dtReturn = strBarcode.Substring(idxStart + 2, (idxStop - idxStart - 2))

        'MessageBox.Show(dtReturn)

        Return dtReturn
    End Function

    Sub loaddgBalance(material As String)
        Dim i As Integer = 0

        Try
            With dgBalance
                If .Rows.Count > 0 Then
                    .Rows.Clear()
                End If

                Dim sqlStr As String = ""

                If material = "" Then
                    sqlStr = "select row_number() OVER (ORDER BY sc.id) No,sc.id_level ID,sc.material Material,sc.inv_ctrl_date [Inv. Ctrl Date],sc.traceability Traceability,sc.batch_no [Batch No.],sc.lot_no [Lot No.],sc.qty [Lot Qty],sc.actual_qty [Actual Qty] from STOCK_CARD sc, sub_sub_po sp where sc.DEPARTMENT='" & dept & "' AND sc.STATUS='Return to Mini Store' and sc.sub_sub_po=sp.sub_sub_po and sp.sub_sub_po='" & txtSubSubPODefective.Text & "' and sp.status='Open' and sc.line='" & cbLineNumber.Text & "' ORDER BY sc.ID"
                Else
                    sqlStr = "select row_number() OVER (ORDER BY sc.id) No,sc.id_level ID,sc.material Material,sc.inv_ctrl_date [Inv. Ctrl Date],sc.traceability Traceability,sc.batch_no [Batch No.],sc.lot_no [Lot No.],sc.qty [Lot Qty],sc.actual_qty [Actual Qty] from STOCK_CARD sc, sub_sub_po sp where sc.MATERIAL='" & material & "' AND sc.DEPARTMENT='" & dept & "' AND sc.STATUS='Return to Mini Store' and sc.sub_sub_po=sp.sub_sub_po and sp.sub_sub_po='" & txtSubSubPODefective.Text & "' and sp.status='Open' and sc.line='" & cbLineNumber.Text & "' ORDER BY sc.ID"
                End If

                Dim dttable As DataTable = Database.GetData(sqlStr)

                .DataSource = dttable

                .DefaultCellStyle.Font = New Font("Tahoma", 14)

                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(0).Width = Int(.Width * 0.05)
                .Columns(1).Width = Int(.Width * 0.08)
                .Columns(2).Width = Int(.Width * 0.2)
                .Columns(3).Width = Int(.Width * 0.14)
                .Columns(4).Width = Int(.Width * 0.1)
                .Columns(5).Width = Int(.Width * 0.1)
                .Columns(6).Width = Int(.Width * 0.11)
                .Columns(7).Width = Int(.Width * 0.1)
                .Columns(8).Width = Int(.Width * 0.1)
                '.Columns(9).Width = Int(.Width * 0.08)


                .EnableHeadersVisualStyles = False
                With .ColumnHeadersDefaultCellStyle
                    .BackColor = Color.Navy
                    .ForeColor = Color.White
                    .Font = New Font("Tahoma", 13, FontStyle.Bold)
                    .Alignment = HorizontalAlignment.Center
                    .Alignment = ContentAlignment.MiddleCenter
                End With

                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            End With
        Catch ex As Exception
            MessageBox.Show("Error Load DGV Balance" & ex.Message)
        End Try
    End Sub

    '===================================== END BALANCE FUNCTION
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles rbFG.CheckedChanged
        If rbFG.Checked = True Then
            'ReadonlyFormFG(False)
            'ReadonlyFormSA(True)
            Dim dtFG As DataTable = Database.GetData("select DISTINCT(CODE_OUT_PROD_REJECT), input_from_fg from out_prod_reject where SUB_SUB_PO='" & txtSubSubPODefective.Text & "' and input_from_fg=1")

            If dtFG.Rows.Count > 0 Then

                ReadonlyFormFG(False)
                ReadonlyFormSA(True)

                rbSA.Checked = False
                DataGridView1.Enabled = False
                TableLayoutPanel8.Enabled = True


                DataGridView3.Enabled = False
                TableLayoutPanel9.Enabled = False
                DataGridView3.DataSource = Nothing
                DataGridView3.Rows.Clear()
                DataGridView3.Columns.Clear()

                Button5.Enabled = False
                Button4.Enabled = False
                btnPrintFGReject.Enabled = False
                btnPrintSAReject.Enabled = False

                'loadFG(cbFGPN.Text)
            Else
                ReadonlyFormFG(False)
                ReadonlyFormSA(True)

                rbSA.Checked = False
                DataGridView1.Enabled = False
                TableLayoutPanel8.Enabled = True
                DataGridView1.DataSource = Nothing
                DataGridView1.Rows.Clear()
                DataGridView1.Columns.Clear()

                DataGridView3.Enabled = False
                TableLayoutPanel9.Enabled = False

                Button5.Enabled = False
                Button4.Enabled = False
                btnPrintFGReject.Enabled = False
                btnPrintSAReject.Enabled = False
            End If

        ElseIf rbSA.Checked = True Then

            'ReadonlyFormFG(True)
            'ReadonlyFormSA(False)
            Dim dtSA As DataTable = Database.GetData("select DISTINCT(CODE_OUT_PROD_REJECT), input_from_fg from out_prod_reject where SUB_SUB_PO='" & txtSubSubPODefective.Text & "' and input_from_fg=0")

            If dtSA.Rows.Count > 0 Then

                ReadonlyFormFG(True)
                ReadonlyFormSA(False)

                rbFG.Checked = False
                DataGridView3.Enabled = True
                TableLayoutPanel9.Enabled = True

                DataGridView1.Enabled = False
                TableLayoutPanel8.Enabled = False
                DataGridView1.DataSource = Nothing
                DataGridView1.Rows.Clear()
                DataGridView1.Columns.Clear()

                'loadSA(cbFGPN.Text)

                Button4.Enabled = False
                Button5.Enabled = False
                btnPrintFGReject.Enabled = False
                btnPrintSAReject.Enabled = False

            Else
                ReadonlyFormFG(True)
                ReadonlyFormSA(False)

                rbFG.Checked = False
                DataGridView3.Enabled = False
                TableLayoutPanel9.Enabled = True
                DataGridView3.DataSource = Nothing
                DataGridView3.Rows.Clear()
                DataGridView3.Columns.Clear()

                DataGridView1.Enabled = False
                TableLayoutPanel8.Enabled = False

                'loadSA(cbFGPN.Text)

                Button4.Enabled = False
                Button5.Enabled = False
                btnPrintFGReject.Enabled = False
                btnPrintSAReject.Enabled = False
            End If
        End If
    End Sub

    Private Sub rbSA_CheckedChanged(sender As Object, e As EventArgs) Handles rbSA.CheckedChanged
        If rbSA.Checked = True Then
            Dim dtSA As DataTable = Database.GetData("select DISTINCT(CODE_OUT_PROD_REJECT), input_from_fg from out_prod_reject where SUB_SUB_PO='" & txtSubSubPODefective.Text & "'")

            If dtSA.Rows.Count > 0 Then

                If dtSA.Rows(0).Item("INPUT_FROM_FG") = 0 Then

                    ReadonlyFormFG(True)
                    ReadonlyFormSA(False)

                    rbFG.Checked = False
                    DataGridView3.Enabled = True
                    TableLayoutPanel9.Enabled = True

                    DataGridView1.Enabled = False
                    TableLayoutPanel8.Enabled = False

                    'loadSA(cbFGPN.Text)

                    Button4.Enabled = False
                    Button5.Enabled = False
                    btnPrintFGReject.Enabled = False
                    btnPrintSAReject.Enabled = False
                End If

            Else
                ReadonlyFormFG(True)
                ReadonlyFormSA(False)

                rbFG.Checked = False
                DataGridView3.Enabled = False
                TableLayoutPanel9.Enabled = True

                DataGridView1.Enabled = False
                TableLayoutPanel8.Enabled = False

                'loadSA(cbFGPN.Text)

                Button4.Enabled = False
                Button5.Enabled = False
                btnPrintFGReject.Enabled = False
                btnPrintSAReject.Enabled = False
            End If
        ElseIf rbFG.Checked = True Then
            Dim dtFG As DataTable = Database.GetData("select DISTINCT(CODE_OUT_PROD_REJECT), input_from_fg from out_prod_reject where SUB_SUB_PO='" & txtSubSubPODefective.Text & "'")

            If dtFG.Rows.Count > 0 Then

                If dtFG.Rows(0).Item("INPUT_FROM_FG") = 1 Then

                    ReadonlyFormFG(False)
                    ReadonlyFormSA(True)

                    rbSA.Checked = False
                    DataGridView1.Enabled = False
                    TableLayoutPanel8.Enabled = True

                    DataGridView3.Enabled = False
                    TableLayoutPanel9.Enabled = False

                    Button5.Enabled = False
                    Button4.Enabled = False
                    btnPrintFGReject.Enabled = False
                    btnPrintSAReject.Enabled = False

                    'loadFG(cbFGPN.Text)
                End If
            Else
                ReadonlyFormFG(False)
                ReadonlyFormSA(True)

                rbSA.Checked = False
                DataGridView1.Enabled = False
                TableLayoutPanel8.Enabled = True

                DataGridView3.Enabled = False
                TableLayoutPanel9.Enabled = False

                Button5.Enabled = False
                Button4.Enabled = False
                btnPrintFGReject.Enabled = False
                btnPrintSAReject.Enabled = False
            End If
        End If
    End Sub

    Sub loadFG(str As String, flowticket As String)
        If str <> "System.Data.DataRowView" Then
            Dim splitFlowTicket() As String = flowticket.Split(";")
            Try
                DataGridView3.Rows.Clear()
                DataGridView3.Columns.Clear()
                DataGridView1.Rows.Clear()
                DataGridView1.Columns.Clear()
                Call Database.koneksi_database()
                'Dim dtProcess As DataTable = Database.GetData("select * from MASTER_PROCESS_FLOW where MASTER_FINISH_GOODS_PN='" & str & "' ORDER BY ID")

                Dim dtProcess As DataTable = Database.GetData("select mpf.id,mpf.master_process,opr.pengali from MASTER_PROCESS_FLOW mpf left join out_prod_reject opr on opr.process_reject=mpf.master_process and opr.sub_sub_po='" & txtSubSubPODefective.Text & "' and department='" & dept & "' AND opr.flow_ticket_no='" & splitFlowTicket(5) & "' where mpf.MASTER_FINISH_GOODS_PN='" & str & "' GROUP BY mpf.id,mpf.master_process,opr.pengali ORDER BY mpf.ID")
                Dim dtFG As DataTable = Database.GetData("select * from MASTER_FINISH_GOODS where FG_PART_NUMBER='" & str & "'")

                TextBox3.Text = dtFG.Rows(0).Item("LASER_CODE").ToString

                With DataGridView1
                    .Rows.Clear()

                    .DefaultCellStyle.Font = New Font("Tahoma", 14)

                    .ColumnCount = 3
                    .Columns(0).Name = "No"
                    .Columns(1).Name = "Process Name"
                    .Columns(2).Name = "Defact qty"

                    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .EnableHeadersVisualStyles = False
                    With .ColumnHeadersDefaultCellStyle
                        .BackColor = Color.Navy
                        .ForeColor = Color.White
                        .Font = New Font("Tahoma", 13, FontStyle.Bold)
                        .Alignment = HorizontalAlignment.Center
                        .Alignment = ContentAlignment.MiddleCenter
                    End With

                    If dtProcess.Rows.Count > 0 Then
                        For i = 0 To dtProcess.Rows.Count - 1
                            .Rows.Add(1)
                            .Item(0, i).Value = (i + 1).ToString()
                            .Item(1, i).Value = dtProcess.Rows(i)("MASTER_PROCESS")
                            .Item(2, i).Value = dtProcess.Rows(i)("PENGALI")
                        Next
                    End If

                    .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
                End With

            Catch ex As Exception
                MessageBox.Show("Error Load Data Process Flow")
            End Try
        End If
    End Sub

    Sub loadSA(str As String, flowticket As String)
        If str <> "System.Data.DataRowView" Then
            Dim splitFlowTicket() As String = flowticket.Split(";")
            Try
                DataGridView3.Rows.Clear()
                DataGridView3.Columns.Clear()
                DataGridView1.Rows.Clear()
                DataGridView1.Columns.Clear()
                Call Database.koneksi_database()
                Dim dtProcess As DataTable = Database.GetData("select mpf.id,mpf.master_process,opr.pengali from MASTER_PROCESS_FLOW mpf left join out_prod_reject opr on opr.process_reject=mpf.master_process and opr.sub_sub_po='" & txtSubSubPODefective.Text & "' and department='" & dept & "' AND opr.flow_ticket_no='" & splitFlowTicket(5) & "' where mpf.MASTER_FINISH_GOODS_PN='" & str & "' GROUP BY mpf.id,mpf.master_process,opr.pengali ORDER BY mpf.ID")
                'Dim dtProcess As DataTable = Database.GetData("select * from MASTER_PROCESS_FLOW where MASTER_FINISH_GOODS_PN='" & str & "' ORDER BY ID")

                Dim dtFG As DataTable = Database.GetData("select * from MASTER_FINISH_GOODS where FG_PART_NUMBER='" & str & "'")

                TextBox6.Text = dtFG.Rows(0).Item("LASER_CODE").ToString

                With DataGridView3
                    .Rows.Clear()

                    .DefaultCellStyle.Font = New Font("Tahoma", 14)

                    .ColumnCount = 3
                    .Columns(0).Name = "No"
                    .Columns(1).Name = "Process Name"
                    .Columns(2).Name = "Defact qty"

                    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .EnableHeadersVisualStyles = False
                    With .ColumnHeadersDefaultCellStyle
                        .BackColor = Color.Navy
                        .ForeColor = Color.White
                        .Font = New Font("Tahoma", 13, FontStyle.Bold)
                        .Alignment = HorizontalAlignment.Center
                        .Alignment = ContentAlignment.MiddleCenter
                    End With

                    If dtProcess.Rows.Count > 0 Then
                        For i = 0 To dtProcess.Rows.Count - 1
                            .Rows.Add(1)
                            .Item(0, i).Value = (i + 1).ToString()
                            .Item(1, i).Value = dtProcess.Rows(i)("MASTER_PROCESS")
                            .Item(2, i).Value = dtProcess.Rows(i)("PENGALI")
                        Next
                    End If

                    .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
                End With

            Catch ex As Exception
                MessageBox.Show("Error Load Data Process Flow")
            End Try
        End If
    End Sub

    Sub LoaddgvFG(proses As String)
        Dim i As Integer = 0

        Try
            'Call Database.koneksi_database()
            'Dim dtMaterialUsage As DataTable = Database.GetData("select distinct MATERIAL_USAGE from _OLD_MASTER_PROCESS_FLOW where MASTER_PROCESS='" & strKey & "' AND MASTER_FINISH_GOODS_PN='" & cbFGPN.Text & "'")
            ''Dim dtMaterialInfo As DataTable = Database.GetData("select distinct MATERIAL_USAGE from MASTER_PROCESS_FLOW where MASTER_PROCESS='" & strKey & "'")

            'Dim matUsage As String = dtMaterialUsage.Rows(i)(0).ToString()
            'Dim matList() As String = matUsage.Split(";")

            With dgWIP
                .Rows.Clear()

                .DefaultCellStyle.Font = New Font("Tahoma", 14)

                .ColumnCount = 9
                .Columns(0).Name = "No"
                .Columns(1).Name = "ID"
                .Columns(2).Name = "Process Name"
                .Columns(3).Name = "Ticket No."
                .Columns(4).Name = "Material PN"
                .Columns(5).Name = "Inv No."
                .Columns(6).Name = "MFG Date"
                '.Columns(7).Name = "Flow Ticket No."
                .Columns(7).Name = "Lot No."
                .Columns(8).Name = "Qty"

                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'For i = 0 To 7
                '    If ((i = 0) Or (i = 3) Or (i = 7)) Then
                '        .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '    Else
                '        .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                '    End If

                '    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable

                '    .Columns(i).ReadOnly = True
                '    'If (i <> 1) Then
                '    '    .Columns(i).ReadOnly = True
                '    'Else
                '    '    .Columns(i).ReadOnly = False
                '    'End If
                'Next

                .Columns(0).Width = Int(.Width * 0.05)
                .Columns(1).Width = Int(.Width * 0.08)
                .Columns(2).Width = Int(.Width * 0.26)
                .Columns(3).Width = Int(.Width * 0.08)
                .Columns(4).Width = Int(.Width * 0.1)
                .Columns(5).Width = Int(.Width * 0.1)
                .Columns(6).Width = Int(.Width * 0.15)
                .Columns(7).Width = Int(.Width * 0.08)
                .Columns(8).Width = Int(.Width * 0.08)
                '.Columns(9).Width = Int(.Width * 0.08)


                .EnableHeadersVisualStyles = False
                With .ColumnHeadersDefaultCellStyle
                    .BackColor = Color.Navy
                    .ForeColor = Color.White
                    .Font = New Font("Tahoma", 13, FontStyle.Bold)
                    .Alignment = HorizontalAlignment.Center
                    .Alignment = ContentAlignment.MiddleCenter
                End With



                ''''''''''''''''''''''''''''''''''''''''''''
                Dim sqlStr As String = ""

                If proses = "" Then
                    sqlStr = "select * from STOCK_PROD_WIP ORDER BY CODE_STOCK_PROD_WIP"
                Else
                    sqlStr = "select * from STOCK_PROD_WIP where PROCESS='" & proses & "' ORDER BY CODE_STOCK_PROD_WIP"
                End If

                Dim dttable As DataTable = Database.GetData(sqlStr)


                If dttable.Rows.Count > 0 Then
                    For i = 0 To dttable.Rows.Count - 1
                        .Rows.Add(1)
                        .Item(0, i).Value = (i + 1).ToString()
                        .Item(1, i).Value = dttable.Rows(i)("CODE_STOCK_PROD_WIP")
                        .Item(2, i).Value = dttable.Rows(i)("PROCESS")
                        .Item(3, i).Value = dttable.Rows(i)("FLOW_TICKET_NO")
                        .Item(4, i).Value = dttable.Rows(i)("PART_NUMBER")
                        .Item(5, i).Value = dttable.Rows(i)("INV_CTRL_DATE")
                        .Item(6, i).Value = dttable.Rows(i)("TRACEABILITY")
                        '.Item(7, i).Value = dttable.Rows(i)("FLOW_TICKET_NO")
                        .Item(7, i).Value = dttable.Rows(i)("LOT_NO")
                        .Item(8, i).Value = dttable.Rows(i)("QTY")

                    Next
                End If
                'For i = 0 To matList.Length - 1
                '    If matList(i) = "" Then
                '        Continue For
                '    End If

                '    Dim dtMaterialInfo As DataTable = Database.GetData("select distinct NAME from _OLD_MASTER_MATERIAL where PART_NUMBER='" & matList(i) & "'")

                '    If dtMaterialInfo.Rows.Count > 0 Then
                '        .Rows.Add(1)
                '        .Item(0, i).Value = (i + 1).ToString()
                '        .Item(1, i).Value = matList(i)
                '        .Item(2, i).Value = dtMaterialInfo.Rows(0)(0)
                '        .Rows(i).Cells(3) = New DataGridViewCheckBoxCell()
                '        .Rows(i).Cells(3).Value = False
                '    End If
                'Next

                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            End With
        Catch ex As Exception
            MessageBox.Show("Error Load DGV FG")
        End Try
    End Sub

    Private Sub TextBox4_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtFGFlowTicket.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
            Dim Split() As String = txtFGFlowTicket.Text.Split(";")
            Dim Split1() As String = Split(0).Split("-")

            txtTampungFlow.Text = Split1(0)

            If Split1(0) = txtTampungLabel.Text Then
                If rbFG.Checked = True Then
                    loadFG(cbFGPN.Text, txtFGFlowTicket.Text)
                    Button4.Enabled = True
                    btnPrintFGReject.Enabled = True
                    DataGridView1.Enabled = True
                    txtFGLabel.ReadOnly = True
                    txtFGFlowTicket.ReadOnly = True
                End If
            End If
        End If
    End Sub

    Private Sub txtFGLabel_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtFGLabel.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
            Try
                Dim splitQRCode() As String = txtFGLabel.Text.Split(New String() {"1P", "12D", "4L", "MLX"}, StringSplitOptions.None)
                Dim splitQRCode1P() As String = splitQRCode(1).Split(New String() {"Q", "S", "13Q", "B"}, StringSplitOptions.None)

                Dim intValue As Integer = 0
                Integer.TryParse(splitQRCode1P(2), intValue)
                txtTampungLabel.Text = intValue
                txtINV.Text = splitQRCode(2)
                txtBatchno.Text = splitQRCode1P(4)

                If intValue.ToString = txtTampungFlow.Text Then
                    If rbFG.Checked = True Then
                        loadFG(cbFGPN.Text, txtFGFlowTicket.Text)
                        Button4.Enabled = True
                        btnPrintFGReject.Enabled = True
                        DataGridView1.Enabled = True
                        txtFGLabel.ReadOnly = True
                        txtFGFlowTicket.ReadOnly = True
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If DataGridView1.Rows.Count > 0 Then
            Try
                For i = 0 To DataGridView1.Rows.Count - 1
                    Dim doWhile As String = ""
                    If DataGridView1.Rows(i).Cells(2).Value IsNot "" And DataGridView1.Rows(i).Cells(2).Value IsNot Nothing And DataGridView1.Rows(i).Cells(2).Value IsNot DBNull.Value Then
                        If IsNumeric(DataGridView1.Rows(i).Cells(2).Value) Then
                            Dim query As String = "select * from master_process_flow where master_process='" & DataGridView1.Rows(i).Cells(1).Value & "'"
                            Dim dtMasterProcessFlow As DataTable = Database.GetData(query)
                            Dim numberInt As Integer = dtMasterProcessFlow.Rows(0).Item("ID") - 1
                            If dtMasterProcessFlow.Rows.Count > 0 Then
                                If IsDBNull(dtMasterProcessFlow.Rows(0).Item("MATERIAL_USAGE")) = False Then
                                    subInsertReject(dtMasterProcessFlow.Rows(0).Item("MATERIAL_USAGE"), DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(1).Value, 1, txtFGFlowTicket.Text)
                                Else
                                    Do While doWhile Is ""
                                        Dim queryKosong As String = "select * from master_process_flow where ID=" & numberInt
                                        Dim dtKosong As DataTable = Database.GetData(queryKosong)
                                        If IsDBNull(dtKosong.Rows(0).Item("MATERIAL_USAGE")) = False Then
                                            doWhile = dtKosong.Rows(0).Item("MATERIAL_USAGE")
                                        Else
                                            numberInt = dtKosong.Rows(0).Item("ID") - 1
                                        End If
                                    Loop
                                    subInsertReject(doWhile, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(1).Value, 1, txtFGFlowTicket.Text)
                                End If
                            End If
                        Else
                            MessageBox.Show("this is not number -> " & DataGridView1.Rows(i).Cells(2).Value & ". Please change with number.")
                        End If
                    End If
                Next

            Catch ex As Exception
                MsgBox("22" & ex.Message)
            End Try
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If DataGridView3.Rows.Count > 0 Then
            Try
                For i = 0 To DataGridView3.Rows.Count - 1
                    Dim doWhile As String = ""
                    If DataGridView3.Rows(i).Cells(2).Value IsNot "" And DataGridView3.Rows(i).Cells(2).Value IsNot Nothing And DataGridView3.Rows(i).Cells(2).Value IsNot DBNull.Value Then
                        If IsNumeric(DataGridView3.Rows(i).Cells(2).Value) Then
                            Dim query As String = "select * from master_process_flow where master_process='" & DataGridView3.Rows(i).Cells(1).Value & "'"
                            Dim dtMasterProcessFlow As DataTable = Database.GetData(query)
                            Dim numberInt As Integer = dtMasterProcessFlow.Rows(0).Item("ID") - 1
                            If dtMasterProcessFlow.Rows.Count > 0 Then
                                If IsDBNull(dtMasterProcessFlow.Rows(0).Item("MATERIAL_USAGE")) = False Then
                                    subInsertReject(dtMasterProcessFlow.Rows(0).Item("MATERIAL_USAGE"), DataGridView3.Rows(i).Cells(2).Value, DataGridView3.Rows(i).Cells(1).Value, 0, txtSAFlowTicket.Text)
                                Else
                                    Do While doWhile Is ""
                                        Dim queryKosong As String = "select * from master_process_flow where ID=" & numberInt
                                        Dim dtKosong As DataTable = Database.GetData(queryKosong)
                                        If IsDBNull(dtKosong.Rows(0).Item("MATERIAL_USAGE")) = False Then
                                            doWhile = dtKosong.Rows(0).Item("MATERIAL_USAGE")
                                        Else
                                            numberInt = dtKosong.Rows(0).Item("ID") - 1
                                        End If
                                    Loop
                                    subInsertReject(doWhile, DataGridView3.Rows(i).Cells(2).Value, DataGridView3.Rows(i).Cells(1).Value, 0, txtSAFlowTicket.Text)
                                End If
                            End If
                        Else
                            MessageBox.Show("this is not number -> " & DataGridView3.Rows(i).Cells(2).Value & ". Please change with number.")
                        End If
                    End If
                Next
            Catch ex As Exception
                MsgBox("11" & ex.Message)
            End Try
        End If
    End Sub

    Public Function GetStockCard(pn As String, qty As Integer) As String
        Dim splitPN() As String = pn.Split(";")
        Dim qtyReject As Double
        Dim i As Integer = 0
        Dim sReturnValue As String = ""
        For i = 0 To splitPN.Length - 2
            qtyReject = 0
            Dim queryGetUsage As String = "select * from material_usage_finish_goods where fg_part_number='" & cbFGPN.Text & "' and component='" & splitPN(i) & "'"
            Dim dtGetUsage As DataTable = Database.GetData(queryGetUsage)

            qtyReject = qty * dtGetUsage.Rows(0).Item("USAGE")

            Dim querySelectMaxFlowTicket As String = "select max(id) id from stock_card where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
            Dim dtSelectMaxFlowTicket As DataTable = Database.GetData(querySelectMaxFlowTicket)

            If IsDBNull(dtSelectMaxFlowTicket.Rows(0).Item("id")) = False Then
                Dim querySelectLotNo As String = "select * from stock_card where id=" & dtSelectMaxFlowTicket.Rows(0).Item("id")
                Dim dtSelectLotNo As DataTable = Database.GetData(querySelectLotNo)

                Dim querySelectQtyExist As String = "select * from stock_card where status='Production Request' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtSelectLotNo.Rows(0).Item("lot_no") & "'"
                Dim dtSelectQtyExist As DataTable = Database.GetData(querySelectQtyExist)

                If dtSelectQtyExist.Rows(0).Item("actual_qty") < qtyReject Then
                    sReturnValue += splitPN(i) & ","
                End If
            Else
                sReturnValue = "Kosong"
            End If

        Next
        Return sReturnValue
    End Function

    Sub subInsertReject(pn As String, qty As Integer, process As String, input_from_fg As Integer, sFlowTicket As String)
        Try
            Dim sTampung As String = GetStockCard(pn, qty)
            If sTampung <> "" And sTampung <> "Kosong" Then
                MessageBox.Show("Sorry qty this material " & sTampung & " is not enough. Please input in menu production")
            ElseIf sTampung = "Kosong" Then
                MessageBox.Show("Sorry some material not exist in DB. Please input in menu production.")
            Else
                Dim splitPN() As String = pn.Split(";")
                Dim splitFlowTicket() As String = sFlowTicket.Split(";")

                Dim qtyReject As Double
                Dim sumQty As Double
                Dim i As Integer = 0
                Dim result As Integer = 1

                Dim queryCheckCodeReject As String = "select DISTINCT(CODE_OUT_PROD_REJECT) from OUT_PROD_REJECT"
                Dim dtCheckCodeReject As DataTable = Database.GetData(queryCheckCodeReject)
                Dim codeReject As String = "RJ" & dtCheckCodeReject.Rows.Count + 1

                For i = 0 To splitPN.Length - 2

                    qtyReject = 0
                    sumQty = 0

                    Dim queryGetUsage As String = "select * from material_usage_finish_goods where fg_part_number='" & cbFGPN.Text & "' and component='" & splitPN(i) & "'"
                    Dim dtGetUsage As DataTable = Database.GetData(queryGetUsage)

                    If dtGetUsage.Rows.Count > 0 Then
                        qtyReject = qty * dtGetUsage.Rows(0).Item("USAGE")

                        Dim queryInsertToTemp As String = "insert into stock_card_temporary([MTS_NO], [DEPARTMENT], [MATERIAL], [STATUS], [STANDARD_PACK], [INV_CTRL_DATE], 
                            [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [DATETIME_INSERT], [SAVE], [QRCODE], [DATETIME_SAVE], 
                            [QTY], [ACTUAL_QTY], [FIFO], [ID_LEVEL], [LEVEL], [FLOW_TICKET]) 
                            select [MTS_NO], [DEPARTMENT], [MATERIAL], [STATUS], [STANDARD_PACK], [INV_CTRL_DATE], [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], 
                            [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [DATETIME_INSERT], [SAVE], [QRCODE], [DATETIME_SAVE], [QTY], [ACTUAL_QTY], [FIFO], [ID_LEVEL], [LEVEL], [FLOW_TICKET] 
                            from stock_card where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                        Dim dtInsertTemp = New SqlCommand(queryInsertToTemp, Database.koneksi)
                        If dtInsertTemp.ExecuteNonQuery() Then
                            Dim queryDeleteStockCard As String = "delete from stock_card where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                            Dim dtDeleteStockCard = New SqlCommand(queryDeleteStockCard, Database.koneksi)
                            If dtDeleteStockCard.ExecuteNonQuery() Then
                                Dim queryCheckFlowTicketinTemp As String = "select * from stock_card_temporary where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and flow_ticket='" & splitFlowTicket(5) & "'"
                                Dim dtCheckFlowTicketinTemp As DataTable = Database.GetData(queryCheckFlowTicketinTemp)

                                Dim queryCheckSumUsage As String = "select * from stock_card where status='Production Request' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no=" & dtCheckFlowTicketinTemp.Rows(0).Item("lot_no")
                                Dim dtCheckSumUsage As DataTable = Database.GetData(queryCheckSumUsage)

                                sumQty = dtCheckSumUsage.Rows(0).Item("sum_qty") - qtyReject

                                Dim queryUpdateToStockCardqty As String = "update stock_card set actual_qty=" & sumQty.ToString.Replace(",", ".") & ",sum_qty=" & sumQty.ToString.Replace(",", ".") & " where status='Production Request' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no=" & dtCheckFlowTicketinTemp.Rows(0).Item("lot_no")
                                Dim dtUpdateToStockCardqty = New SqlCommand(queryUpdateToStockCardqty, Database.koneksi)
                                If dtUpdateToStockCardqty.ExecuteNonQuery() Then
                                    Dim queryDistintLotNoinTemp As String = "select DISTINCT(lot_no) from stock_card_temporary where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                                    Dim dtDistintLotNoinTemp As DataTable = Database.GetData(queryDistintLotNoinTemp)
                                    If dtDistintLotNoinTemp.Rows.Count > 0 Then
                                        For iDistint = 0 To dtDistintLotNoinTemp.Rows.Count - 1
                                            If dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") = dtCheckFlowTicketinTemp.Rows(0).Item("lot_no") Then
                                                Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & sumQty.ToString.Replace(",", ".") & ",@material='" & splitPN(i) & "',@lot_material='" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") & "'"
                                                Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                                            Else
                                                Dim sqlQtyLot = "select * from stock_card where status='Production Request' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no=" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no")
                                                Dim dtQtyLot As DataTable = Database.GetData(sqlQtyLot)
                                                Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtQtyLot.Rows(0).Item("sum_qty").ToString.Replace(",", ".") & ",@material='" & splitPN(i) & "',@lot_material='" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") & "'"
                                                Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                                            End If
                                        Next

                                        Dim sqlInsertRejectPN As String = "INSERT INTO out_prod_reject (CODE_OUT_PROD_REJECT, sub_sub_po, FG_PN, PART_NUMBER, LOT_NO, TRACEABILITY,INV_CTRL_DATE,BATCH_NO,QTY,PO,PROCESS_REJECT,LINE,FLOW_TICKET_NO,DEPARTMENT,PENGALI,INPUT_FROM_FG)
                                            VALUES ('" & codeReject & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & splitPN(i) & "','" & dtCheckFlowTicketinTemp.Rows(0).Item("lot_no") & "',
                                            '" & dtCheckFlowTicketinTemp.Rows(0).Item("TRACEABILITY") & "','" & dtCheckFlowTicketinTemp.Rows(0).Item("INV_CTRL_DATE") & "','" & dtCheckFlowTicketinTemp.Rows(0).Item("BATCH_NO") & "',
                                            " & qtyReject.ToString.Replace(",", ".") & ",'" & cbPONumber.Text & "','" & process & "','" & cbLineNumber.Text & "','" & splitFlowTicket(5) & "','" & dept & "'," & qty & ",'" & input_from_fg & "')"
                                        Dim cmdInsertRejectPN = New SqlCommand(sqlInsertRejectPN, Database.koneksi)
                                        If cmdInsertRejectPN.ExecuteNonQuery() Then
                                            Dim queryDeleteStockCardTemporary As String = "delete from stock_card_temporary where status='Production Process' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                                            Dim dtDeleteStockCardTemporary = New SqlCommand(queryDeleteStockCardTemporary, Database.koneksi)
                                            If dtDeleteStockCardTemporary.ExecuteNonQuery() Then
                                                result *= 1
                                            Else
                                                result *= 0
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next

                If result > 0 Then
                    MessageBox.Show("Success Save!")
                Else
                    MessageBox.Show("Fail Save!")
                End If
            End If
        Catch ex As Exception
            MsgBox("asd" & ex.Message)
        End Try
    End Sub

    Private Sub txtSAFlowTicket_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtSAFlowTicket.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
            If txtSAFlowTicket.Text <> "" Then
                loadSA(cbFGPN.Text, txtSAFlowTicket.Text)
                Button5.Enabled = True
                btnPrintSAReject.Enabled = True
                DataGridView3.Enabled = True
                txtSAFlowTicket.ReadOnly = True
            End If
        End If
    End Sub

    Sub loadDGVOthers()
        Try
            Dim query As String = "select part_number,sum(qty) qty from OUT_PROD_REJECT where code_OUT_PROD_REJECT='" & txtLabelOtherPart.Text & "' and department='" & dept & "' GROUP BY part_number"
            Dim dtOutProd As DataTable = Database.GetData(query)

            With DataGridView2
                .Rows.Clear()

                .DefaultCellStyle.Font = New Font("Tahoma", 14)

                .ColumnCount = 4
                .Columns(0).Name = "No"
                .Columns(1).Name = "Part Number"
                .Columns(2).Name = "Qty Maximal"
                .Columns(3).Name = "Qty"

                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .EnableHeadersVisualStyles = False
                With .ColumnHeadersDefaultCellStyle
                    .BackColor = Color.Navy
                    .ForeColor = Color.White
                    .Font = New Font("Tahoma", 13, FontStyle.Bold)
                    .Alignment = HorizontalAlignment.Center
                    .Alignment = ContentAlignment.MiddleCenter
                End With


                If dtOutProd.Rows.Count > 0 Then
                    For i = 0 To dtOutProd.Rows.Count - 1
                        Dim queryOthersStock As String = "select qty from stock_prod_others where code_OUT_PROD_REJECT='" & txtLabelOtherPart.Text & "' and department='" & dept & "' and part_number='" & dtOutProd.Rows(i)("part_number") & "'"
                        Dim dtOthersStock As DataTable = Database.GetData(queryOthersStock)
                        Dim qtyOthers As Integer = 0
                        If dtOthersStock.Rows.Count > 0 Then
                            qtyOthers = dtOthersStock.Rows(0).Item("qty")
                        End If

                        .Rows.Add(1)
                        .Item(0, i).Value = (i + 1).ToString()
                        .Item(1, i).Value = dtOutProd.Rows(i)("part_number")
                        .Item(2, i).Value = dtOutProd.Rows(i)("qty")
                        .Item(3, i).Value = qtyOthers
                    Next
                End If

                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            Dim queryOthers As String = "select * from stock_prod_others where code_OUT_PROD_REJECT='" & txtLabelOtherPart.Text & "' and department='" & dept & "'"
            Dim dtOthers As DataTable = Database.GetData(queryOthers)

            With DataGridView4
                .Rows.Clear()

                .DefaultCellStyle.Font = New Font("Tahoma", 14)

                .ColumnCount = 9
                .Columns(0).Name = "No"
                .Columns(1).Name = "Code Defect"
                .Columns(2).Name = "Code Others"
                .Columns(3).Name = "Part Number"
                .Columns(4).Name = "Lot No"
                .Columns(5).Name = "Traceability"
                .Columns(6).Name = "INV CTRL Date"
                .Columns(7).Name = "Batch No"
                .Columns(8).Name = "Qty"

                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .EnableHeadersVisualStyles = False
                With .ColumnHeadersDefaultCellStyle
                    .BackColor = Color.Navy
                    .ForeColor = Color.White
                    .Font = New Font("Tahoma", 13, FontStyle.Bold)
                    .Alignment = HorizontalAlignment.Center
                    .Alignment = ContentAlignment.MiddleCenter
                End With


                If dtOthers.Rows.Count > 0 Then
                    For i = 0 To dtOthers.Rows.Count - 1
                        .Rows.Add(1)
                        .Item(0, i).Value = (i + 1).ToString()
                        .Item(1, i).Value = dtOthers.Rows(i)("code_OUT_PROD_REJECT")
                        .Item(2, i).Value = dtOthers.Rows(i)("code_stock_prod_others")
                        .Item(3, i).Value = dtOthers.Rows(i)("part_number")
                        .Item(4, i).Value = dtOthers.Rows(i)("lot_no")
                        .Item(5, i).Value = dtOthers.Rows(i)("traceability")
                        .Item(6, i).Value = dtOthers.Rows(i)("inv_ctrl_date")
                        .Item(7, i).Value = dtOthers.Rows(i)("batch_no")
                        .Item(8, i).Value = dtOthers.Rows(i)("qty")
                    Next
                End If

                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtLabelOtherPart_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtLabelOtherPart.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
            loadDGVOthers()
        End If
    End Sub

    Private Sub btnOtherSave_Click(sender As Object, e As EventArgs) Handles btnOtherSave.Click
        If DataGridView2.Rows.Count > 0 Then
            Try
                For i = 0 To DataGridView2.Rows.Count - 1
                    Dim queryCheckCodeOther As String = "select DISTINCT(CODE_stock_prod_others) from stock_prod_others"
                    Dim dtCheckCodeOther As DataTable = Database.GetData(queryCheckCodeOther)
                    Dim codeOther As String = "OTHERS" & dtCheckCodeOther.Rows.Count + 1

                    If DataGridView2.Rows(i).Cells(3).Value IsNot "" And DataGridView2.Rows(i).Cells(3).Value IsNot Nothing And DataGridView2.Rows(i).Cells(3).Value IsNot DBNull.Value Then
                        If IsNumeric(DataGridView2.Rows(i).Cells(3).Value) Then
                            If DataGridView2.Rows(i).Cells(3).Value > 0 Then
                                If DataGridView2.Rows(i).Cells(3).Value <= DataGridView2.Rows(i).Cells(2).Value Then
                                    Dim query As String = "select * from stock_prod_others where code_out_prod_reject='" & txtLabelOtherPart.Text & "' and part_number='" & DataGridView2.Rows(i).Cells(1).Value & "' and department='" & dept & "'"
                                    Dim dtCheckStockOthers As DataTable = Database.GetData(query)
                                    If dtCheckStockOthers.Rows.Count > 0 Then
                                        MessageBox.Show("Update")
                                        Dim sqlUpdateProcessProd As String = "update stock_prod_others set qty=" & DataGridView2.Rows(i).Cells(3).Value & " where code_out_prod_reject='" & txtLabelOtherPart.Text & "' and part_number='" & DataGridView2.Rows(i).Cells(1).Value & "' and department='" & dept & "'"
                                        Dim cmdUpdateProcessProd = New SqlCommand(sqlUpdateProcessProd, Database.koneksi)
                                        cmdUpdateProcessProd.ExecuteNonQuery()
                                    Else
                                        Dim sqlInsertOther As String = "INSERT INTO stock_prod_others (CODE_STOCK_PROD_OTHERS, PART_NUMBER, LOT_NO, TRACEABILITY,INV_CTRL_DATE,BATCH_NO,QTY,CODE_OUT_PROD_REJECT,DEPARTMENT)
                                        select '" & codeOther & "',part_number,lot_no,TRACEABILITY,INV_CTRL_DATE,BATCH_NO, '" & DataGridView2.Rows(i).Cells(3).Value & "', CODE_OUT_PROD_REJECT,DEPARTMENT FROM OUT_PROD_REJECT WHERE CODE_OUT_PROD_REJECT='" & txtLabelOtherPart.Text & "' and part_number='" & DataGridView2.Rows(i).Cells(1).Value & "'"
                                        Dim cmdInsertOther = New SqlCommand(sqlInsertOther, Database.koneksi)
                                        cmdInsertOther.ExecuteNonQuery()
                                    End If
                                Else
                                    MsgBox("The quantity cannot exceed the maximum quantity")
                                End If
                            End If
                        Else
                            MessageBox.Show("this is line not number -> " & DataGridView2.Rows(i).Cells(3).Value & ". Please change with number.")
                        End If
                    End If
                Next
                loadDGVOthers()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnPrintBalance_Click(sender As Object, e As EventArgs) Handles btnPrintBalance.Click
        If txtSubSubPODefective.Text <> "" Then
            Try
                Dim query As String = "select sc.id_level,sc.material,m.name,sc.traceability,sc.inv_ctrl_date,sc.batch_no,sc.lot_no,sc.actual_qty,sc.lot_no from stock_card sc, master_material m where sc.status='Return To Mini Store' and sc.sub_sub_po='" & txtSubSubPODefective.Text & "' and sc.actual_qty>0 and sc.material=m.part_number"
                Dim dtCheckStockBalance As DataTable = Database.GetData(query)
                If dtCheckStockBalance.Rows.Count > 0 Then
                    For i = 0 To dtCheckStockBalance.Rows.Count - 1

                        globVar.failPrint = ""
                        _PrintingSubAssyRawMaterial.txt_Unique_id.Text = dtCheckStockBalance.Rows(i).Item("id_level")
                        _PrintingSubAssyRawMaterial.txt_part_number.Text = dtCheckStockBalance.Rows(i).Item("material")
                        _PrintingSubAssyRawMaterial.txt_Part_Description.Text = dtCheckStockBalance.Rows(i).Item("name")
                        _PrintingSubAssyRawMaterial.txt_Traceability.Text = dtCheckStockBalance.Rows(i).Item("traceability")
                        _PrintingSubAssyRawMaterial.txt_Inv_crtl_date.Text = dtCheckStockBalance.Rows(i).Item("inv_ctrl_date")
                        _PrintingSubAssyRawMaterial.txt_Batch_no.Text = dtCheckStockBalance.Rows(i).Item("batch_no")
                        _PrintingSubAssyRawMaterial.txt_Lot_no.Text = dtCheckStockBalance.Rows(i).Item("lot_no")
                        _PrintingSubAssyRawMaterial.txt_Qty.Text = dtCheckStockBalance.Rows(i).Item("actual_qty")
                        _PrintingSubAssyRawMaterial.txt_jenis_ticket.Text = "Balance Material"
                        _PrintingSubAssyRawMaterial.txt_QR_Code.Text = dtCheckStockBalance.Rows(i).Item("id_level")
                        _PrintingSubAssyRawMaterial.btn_Print_Click(sender, e)

                        If globVar.failPrint = "No" Then
                            Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, lot, remark,sub_sub_po,department,material,code_print)
                                VALUES ('" & cbPONumber.Text & "','" & cbFGPN.Text & "','" & cbLineNumber.Text & "','" & dtCheckStockBalance.Rows(i).Item("lot_no") & "','Balance Material','" & txtSubSubPODefective.Text & "','" & dept & "','" & dtCheckStockBalance.Rows(i).Item("material") & "','" & dtCheckStockBalance.Rows(i).Item("id_level") & "')"
                            Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                            cmdInsertPrintingRecord.ExecuteNonQuery()
                        End If
                    Next
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MessageBox.Show("Sorry please select Line First.")
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles btnPrintFGReject.Click
        If txtSubSubPODefective.Text <> "" Then
            If txtFGFlowTicket.Text <> "" Then
                Dim split() As String = txtFGFlowTicket.Text.Split(";")
                Dim query As String = "select DISTINCT(CODE_OUT_PROD_REJECT) from out_prod_reject where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and line='" & cbLineNumber.Text & "' and flow_ticket_no='" & split(5) & "' and input_from_fg=1"
                Dim dtCheckStockReject As DataTable = Database.GetData(query)
                If dtCheckStockReject.Rows.Count > 0 Then
                    For i = 0 To dtCheckStockReject.Rows.Count - 1
                        globVar.failPrint = ""
                        _PrintingDefect.txt_QR_Code.Text = dtCheckStockReject.Rows(i).Item("CODE_OUT_PROD_REJECT")
                        _PrintingDefect.Label2.Text = "Defect Ticket"
                        _PrintingDefect.txt_Unique_id.Text = dtCheckStockReject.Rows(i).Item("CODE_OUT_PROD_REJECT")
                        _PrintingDefect.txt_part_number.Text = cbFGPN.Text
                        _PrintingDefect.txt_Part_Description.Text = txtDescDefective.Text
                        _PrintingDefect.txt_Traceability.Text = txtTampungLabel.Text
                        _PrintingDefect.txt_Inv_crtl_date.Text = txtINV.Text
                        _PrintingDefect.btn_Print_Click(sender, e)

                        If globVar.failPrint = "No" Then
                            Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, remark,sub_sub_po,department,flow_ticket,code_print)
                                VALUES ('" & cbPONumber.Text & "','" & cbFGPN.Text & "','" & cbLineNumber.Text & "','Reject Material','" & txtSubSubPODefective.Text & "','" & dept & "','" & split(5) & "','" & dtCheckStockReject.Rows(i).Item("CODE_OUT_PROD_REJECT") & "')"
                            Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                            cmdInsertPrintingRecord.ExecuteNonQuery()
                        End If
                    Next
                Else
                    MsgBox("Dont have Reject Data")
                End If
            Else
                MessageBox.Show("Sorry please input flow ticket first.")
            End If
        Else
            MessageBox.Show("Sorry please select Line First.")
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles btnPrintSAReject.Click
        If txtSubSubPODefective.Text <> "" Then
            If txtSAFlowTicket.Text <> "" Then
                Dim split() As String = txtSAFlowTicket.Text.Split(";")
                Dim query As String = "select DISTINCT(CODE_OUT_PROD_REJECT) from out_prod_reject where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and line='" & cbLineNumber.Text & "' and flow_ticket_no='" & split(5) & "' and input_from_fg=1"
                Dim dtCheckStockReject As DataTable = Database.GetData(query)
                If dtCheckStockReject.Rows.Count > 0 Then
                    For i = 0 To dtCheckStockReject.Rows.Count - 1
                        globVar.failPrint = ""
                        _PrintingDefect.txt_QR_Code.Text = dtCheckStockReject.Rows(i).Item("CODE_OUT_PROD_REJECT")
                        _PrintingDefect.Label2.Text = "Defect Ticket"
                        _PrintingDefect.txt_Unique_id.Text = dtCheckStockReject.Rows(i).Item("CODE_OUT_PROD_REJECT")
                        _PrintingDefect.txt_part_number.Text = cbFGPN.Text
                        _PrintingDefect.txt_Part_Description.Text = txtDescDefective.Text
                        _PrintingDefect.txt_Traceability.Text = txtTampungLabel.Text
                        _PrintingDefect.txt_Inv_crtl_date.Text = txtINV.Text
                        _PrintingDefect.btn_Print_Click(sender, e)

                        If globVar.failPrint = "No" Then
                            Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, remark,sub_sub_po,department,flow_ticket,code_print)
                            VALUES ('" & cbPONumber.Text & "','" & cbFGPN.Text & "','" & cbLineNumber.Text & "','Reject Material','" & txtSubSubPODefective.Text & "','" & dept & "','" & split(5) & "','" & dtCheckStockReject.Rows(i).Item("CODE_OUT_PROD_REJECT") & "')"
                            Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                            cmdInsertPrintingRecord.ExecuteNonQuery()
                        End If
                    Next
                Else
                    MsgBox("Dont have Reject Data")
                End If
            Else
                MessageBox.Show("Sorry please input flow ticket first.")
            End If
        Else
            MessageBox.Show("Sorry please select Line First.")
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ClearInputFG()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        ClearInputFG()
    End Sub

    Sub ClearInputFG()
        txtSPQ.Clear()
        txtBatchno.Clear()
        txtINV.Clear()
        txtTampungLabel.Clear()
        txtTampungFlow.Clear()
        txtFGFlowTicket.Clear()
        txtFGLabel.Clear()
        TextBox3.Clear()
        txtSAFlowTicket.Clear()
        txtSABatchNo.Clear()
        TextBox6.Clear()
        txtFGFlowTicket.ReadOnly = False
        txtFGLabel.ReadOnly = False
        txtSAFlowTicket.ReadOnly = False
        txtSABatchNo.ReadOnly = False
        DataGridView1.Rows.Clear()
        DataGridView3.Rows.Clear()
    End Sub

    Private Sub btnPrintWIP_Click(sender As Object, e As EventArgs) Handles btnPrintWIP.Click
        If txtSubSubPODefective.Text <> "" Then
            If txtWIPTicketNo.Text <> "" Then
                Dim split() As String = txtWIPTicketNo.Text.Split(";")
                Dim query As String = "select code_stock_prod_wip,flow_ticket_no,process,pengali from stock_prod_wip where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and line='" & cbLineNumber.Text & "' and flow_ticket_no='" & split(5) & "' group by code_stock_prod_wip,flow_ticket_no,process,pengali"
                Dim dt As DataTable = Database.GetData(query)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        globVar.failPrint = ""
                        _PrintingWIPOnHold.txt_QR_Code.Text = dt.Rows(i).Item("code_stock_prod_wip")
                        _PrintingWIPOnHold.txt_jenis_ticket.Text = "WIP"
                        _PrintingWIPOnHold.txt_part_number.Text = cbFGPN.Text
                        _PrintingWIPOnHold.txt_Part_Description.Text = txtDescDefective.Text
                        _PrintingWIPOnHold.txt_Process.Text = dt.Rows(i).Item("process")
                        _PrintingWIPOnHold.txt_Qty.Text = dt.Rows(i).Item("pengali")
                        _PrintingWIPOnHold.txt_Traceability.Text = cbPONumber.Text
                        _PrintingWIPOnHold.txt_Inv_crtl_date.Text = ""
                        _PrintingWIPOnHold.txt_Unique_id.Text = dt.Rows(i).Item("code_stock_prod_wip")
                        _PrintingWIPOnHold.txt_Status.Text = "WIP"
                        _PrintingWIPOnHold.btn_Print_Click(sender, e)

                        If globVar.failPrint = "No" Then
                            Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, remark,sub_sub_po,department,flow_ticket,code_print)
                                VALUES ('" & cbPONumber.Text & "','" & cbFGPN.Text & "','" & cbLineNumber.Text & "','WIP','" & txtSubSubPODefective.Text & "','" & dept & "','" & split(5) & "','" & dt.Rows(i).Item("code_stock_prod_wip") & "')"
                            Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                            cmdInsertPrintingRecord.ExecuteNonQuery()
                        End If
                    Next
                Else
                    MsgBox("Dont have WIP Data")
                End If
            Else
                Dim query As String = "select code_stock_prod_wip,flow_ticket_no,process,pengali from stock_prod_wip where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and line='" & cbLineNumber.Text & "' group by code_stock_prod_wip,flow_ticket_no,process,pengali"
                Dim dt As DataTable = Database.GetData(query)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        globVar.failPrint = ""
                        _PrintingWIPOnHold.txt_QR_Code.Text = dt.Rows(i).Item("code_stock_prod_wip")
                        _PrintingWIPOnHold.txt_jenis_ticket.Text = "WIP"
                        _PrintingWIPOnHold.txt_part_number.Text = cbFGPN.Text
                        _PrintingWIPOnHold.txt_Part_Description.Text = txtDescDefective.Text
                        _PrintingWIPOnHold.txt_Process.Text = dt.Rows(i).Item("process")
                        _PrintingWIPOnHold.txt_Qty.Text = dt.Rows(i).Item("pengali")
                        _PrintingWIPOnHold.txt_Traceability.Text = cbPONumber.Text
                        _PrintingWIPOnHold.txt_Inv_crtl_date.Text = ""
                        _PrintingWIPOnHold.txt_Unique_id.Text = dt.Rows(i).Item("code_stock_prod_wip")
                        _PrintingWIPOnHold.txt_Status.Text = "WIP"
                        _PrintingWIPOnHold.btn_Print_Click(sender, e)

                        If globVar.failPrint = "No" Then
                            Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, remark,sub_sub_po,department,flow_ticket,code_print)
                                VALUES ('" & cbPONumber.Text & "','" & cbFGPN.Text & "','" & cbLineNumber.Text & "','WIP','" & txtSubSubPODefective.Text & "','" & dept & "','" & dt.Rows(i).Item("flow_ticket_no") & "','" & dt.Rows(i).Item("code_stock_prod_wip") & "')"
                            Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                            cmdInsertPrintingRecord.ExecuteNonQuery()
                        End If
                    Next
                Else
                    MsgBox("Dont have WIP Data")
                End If
            End If
        Else
            MessageBox.Show("Sorry please select Line First.")
        End If
    End Sub

    Private Sub btnPrintOnhold_Click(sender As Object, e As EventArgs) Handles btnPrintOnhold.Click
        If txtSubSubPODefective.Text <> "" Then
            If txtOnHoldTicketNo.Text <> "" Then
                Dim split() As String = txtOnHoldTicketNo.Text.Split(";")
                Dim query As String = "select code_stock_prod_onhold,flow_ticket_no,process,pengali from stock_prod_onhold where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and line='" & cbLineNumber.Text & "' and flow_ticket_no='" & split(5) & "' group by code_stock_prod_onhold,flow_ticket_no,process,pengali"
                Dim dt As DataTable = Database.GetData(query)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1

                        globVar.failPrint = ""
                        _PrintingWIPOnHold.txt_QR_Code.Text = dt.Rows(i).Item("code_stock_prod_onhold")
                        _PrintingWIPOnHold.txt_jenis_ticket.Text = "ONHOLD"
                        _PrintingWIPOnHold.txt_part_number.Text = cbFGPN.Text
                        _PrintingWIPOnHold.txt_Part_Description.Text = txtDescDefective.Text
                        _PrintingWIPOnHold.txt_Process.Text = dt.Rows(i).Item("process")
                        _PrintingWIPOnHold.txt_Qty.Text = dt.Rows(i).Item("pengali")
                        _PrintingWIPOnHold.txt_Traceability.Text = cbPONumber.Text
                        _PrintingWIPOnHold.txt_Inv_crtl_date.Text = ""
                        _PrintingWIPOnHold.txt_Unique_id.Text = dt.Rows(i).Item("code_stock_prod_onhold")
                        _PrintingWIPOnHold.txt_Status.Text = "ONHOLD"
                        _PrintingWIPOnHold.btn_Print_Click(sender, e)

                        If globVar.failPrint = "No" Then
                            Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, remark,sub_sub_po,department,flow_ticket,code_print)
                                VALUES ('" & cbPONumber.Text & "','" & cbFGPN.Text & "','" & cbLineNumber.Text & "','OnHold','" & txtSubSubPODefective.Text & "','" & dept & "','" & split(5) & "','" & dt.Rows(i).Item("code_stock_prod_onhold") & "')"
                            Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                            cmdInsertPrintingRecord.ExecuteNonQuery()
                        End If

                    Next
                Else
                    MsgBox("Dont have On Hold Data")
                End If
            Else
                Dim query As String = "select code_stock_prod_onhold,flow_ticket_no,process,pengali from stock_prod_onhold where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and line='" & cbLineNumber.Text & "' group by code_stock_prod_onhold,flow_ticket_no,process,pengali"
                Dim dt As DataTable = Database.GetData(query)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1

                        globVar.failPrint = ""
                        _PrintingWIPOnHold.txt_QR_Code.Text = dt.Rows(i).Item("code_stock_prod_onhold")
                        _PrintingWIPOnHold.txt_jenis_ticket.Text = "ONHOLD"
                        _PrintingWIPOnHold.txt_part_number.Text = cbFGPN.Text
                        _PrintingWIPOnHold.txt_Part_Description.Text = txtDescDefective.Text
                        _PrintingWIPOnHold.txt_Process.Text = dt.Rows(i).Item("process")
                        _PrintingWIPOnHold.txt_Qty.Text = dt.Rows(i).Item("pengali")
                        _PrintingWIPOnHold.txt_Traceability.Text = cbPONumber.Text
                        _PrintingWIPOnHold.txt_Inv_crtl_date.Text = ""
                        _PrintingWIPOnHold.txt_Unique_id.Text = dt.Rows(i).Item("code_stock_prod_onhold")
                        _PrintingWIPOnHold.txt_Status.Text = "ONHOLD"
                        _PrintingWIPOnHold.btn_Print_Click(sender, e)

                        If globVar.failPrint = "No" Then
                            Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, remark,sub_sub_po,department,flow_ticket,code_print)
                                VALUES ('" & cbPONumber.Text & "','" & cbFGPN.Text & "','" & cbLineNumber.Text & "','OnHold','" & txtSubSubPODefective.Text & "','" & dept & "','" & dt.Rows(i).Item("flow_ticket_no") & "','" & dt.Rows(i).Item("code_stock_prod_onhold") & "')"
                            Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                            cmdInsertPrintingRecord.ExecuteNonQuery()
                        End If

                    Next
                Else
                    MsgBox("Dont have On Hold Data")
                End If
            End If
        Else
            MessageBox.Show("Sorry please select Line First.")
        End If
    End Sub

    Private Sub btnPrintOthersPart_Click(sender As Object, e As EventArgs) Handles btnPrintOthersPart.Click
        If txtSubSubPODefective.Text <> "" Then
            Try
                Dim query As String = "select stock_prod_others.*,master_material.name from stock_prod_others, master_material where code_out_prod_reject='" & txtLabelOtherPart.Text & "' and department='" & globVar.department & "' and stock_prod_others.part_number=master_material.part_number"
                Dim dtCheckOthersMaterialBalance As DataTable = Database.GetData(query)
                If dtCheckOthersMaterialBalance.Rows.Count > 0 Then
                    For i = 0 To dtCheckOthersMaterialBalance.Rows.Count - 1

                        globVar.failPrint = ""
                        _PrintingSubAssyRawMaterial.txt_Unique_id.Text = dtCheckOthersMaterialBalance.Rows(i).Item("code_stock_prod_others")
                        _PrintingSubAssyRawMaterial.txt_part_number.Text = dtCheckOthersMaterialBalance.Rows(i).Item("part_number")
                        _PrintingSubAssyRawMaterial.txt_Part_Description.Text = dtCheckOthersMaterialBalance.Rows(i).Item("name")
                        _PrintingSubAssyRawMaterial.txt_Traceability.Text = dtCheckOthersMaterialBalance.Rows(i).Item("traceability")
                        _PrintingSubAssyRawMaterial.txt_Inv_crtl_date.Text = dtCheckOthersMaterialBalance.Rows(i).Item("inv_ctrl_date")
                        _PrintingSubAssyRawMaterial.txt_Batch_no.Text = dtCheckOthersMaterialBalance.Rows(i).Item("batch_no")
                        _PrintingSubAssyRawMaterial.txt_Lot_no.Text = dtCheckOthersMaterialBalance.Rows(i).Item("lot_no")
                        _PrintingSubAssyRawMaterial.txt_Qty.Text = dtCheckOthersMaterialBalance.Rows(i).Item("qty")
                        _PrintingSubAssyRawMaterial.txt_jenis_ticket.Text = "Others Material"
                        _PrintingSubAssyRawMaterial.txt_QR_Code.Text = dtCheckOthersMaterialBalance.Rows(i).Item("code_stock_prod_others")
                        _PrintingSubAssyRawMaterial.btn_Print_Click(sender, e)

                        If globVar.failPrint = "No" Then
                            Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, lot, remark,sub_sub_po,department,material,code_print)
                                VALUES ('" & cbPONumber.Text & "','" & cbFGPN.Text & "','" & cbLineNumber.Text & "','" & dtCheckOthersMaterialBalance.Rows(i).Item("lot_no") & "','Others Material','" & txtSubSubPODefective.Text & "','" & dept & "','" & dtCheckOthersMaterialBalance.Rows(i).Item("part_number") & "','" & dtCheckOthersMaterialBalance.Rows(i).Item("code_stock_prod_others") & "')"
                            Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                            cmdInsertPrintingRecord.ExecuteNonQuery()
                        End If
                    Next
                Else
                    MessageBox.Show("Sorry dont have any others part.")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MessageBox.Show("Sorry please select Line First.")
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.CheckState = CheckState.Unchecked Then
            TableLayoutPanel14.Enabled = True
            TableLayoutPanel7.Enabled = True
            txtBalanceBarcode.Enabled = False
        Else
            TableLayoutPanel14.Enabled = False
            TableLayoutPanel7.Enabled = False
            txtBalanceBarcode.Enabled = True
        End If
        txtBalanceBarcode.Clear()
        txtBalanceMaterialPN.Clear()
        TextBox1.Clear()
        TextBox2.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim queryCheckProductionProcess As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and material='" & TextBox1.Text & "' and status='Return To Mini Store' and actual_qty > 0 and department='" & globVar.department & "'"
            Dim dttableProductionProcess As DataTable = Database.GetData(queryCheckProductionProcess)

            If dttableProductionProcess.Rows.Count = 0 Then
                Dim queryCheck As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and material='" & TextBox1.Text & "' and status='Production Request' and actual_qty > 0 and department='" & globVar.department & "' and lot_no='" & TextBox2.Text & "'"
                Dim dttable As DataTable = Database.GetData(queryCheck)
                If dttable.Rows.Count > 0 Then
                    txtBalanceQty.Text = dttable.Rows(0).Item("actual_qty")
                Else
                    MsgBox("This material Qty = 0 or this material not exist in DB.")
                    txtBalanceBarcode.Clear()
                    txtBalanceMaterialPN.Clear()
                End If
            Else
                MsgBox("Double Scan")
                TextBox1.Clear()
                TextBox2.Clear()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class