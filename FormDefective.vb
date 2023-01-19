Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Reflection.Emit
Imports System.Runtime.Remoting
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

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
    End Sub

    Sub ReadonlyFormSA(_bool As Boolean)
        txtSAFlowTicket.ReadOnly = _bool
        TextBox6.ReadOnly = _bool
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
            Dim query = "select ID, NAME from MASTER_LINE where DEPARTEMENT='" & dept & "' ORDER BY NAME"
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

                Part = materialList(cbWIPProcess.SelectedIndex).Split(";")
                sFlow_Ticket = txtWIPTicketNo.Text.Split(";")

                'diulang sebanyak part number yg ada
                Call Database.koneksi_database()
                Dim dtsubsubpo As String = WIPGetSubsubPO()
                Dim noCode As String = WIPGenerateCode()

                If WIPcheckExistingData(dtsubsubpo, dept) = False Then
                    For i = 0 To Part.Length - 2
                        Dim dtList As String = WIPGetDataTraceability(Part(i), cbLineNumber.Text, dtsubsubpo, sFlow_Ticket(5))
                        Dim arrdtList() As String
                        arrdtList = dtList.Split(";")

                        Dim tgl As String = arrdtList(11).Replace("/", "-")

                        Dim sql As String = "INSERT INTO STOCK_PROD_WIP(CODE_STOCK_PROD_WIP,SUB_SUB_PO,FG_PN,PART_NUMBER,LOT_NO,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,PROCESS,QTY,PO,FLOW_TICKET_NO,DEPARTMENT) VALUES ('" &
                                        noCode & "','" & dtsubsubpo & "','" & cbFGPN.Text & "','" & Part(i) & "','" & arrdtList(9) & "','" & arrdtList(7) & "','" & arrdtList(6) & "','" & arrdtList(8) & "','" & cbWIPProcess.Text & "'," & WIPGetQtyperPart(Part(i), 0) & ",'" & cbPONumber.Text & "','" & sFlow_Ticket(5) & "','" & dept & "')"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        If cmd.ExecuteNonQuery() Then

                            Dim sqlCheckStock = "select * from STOCK_card where status='Production Request' and sub_sub_po='" & txtSubSubPODefective.Text & "' and material='" & Part(i) & "' and actual_qty>0 and lot_no='" & arrdtList(9) & "'"
                            Dim dtCheckStock As DataTable = Database.GetData(sqlCheckStock)

                            If dtCheckStock.Rows.Count > 0 And dtCheckStock.Rows(0).Item("actual_qty") > WIPGetQtyperPart(Part(i), 0) Then
                                Dim sqlUpdateStock As String = "update STOCK_CARD set ACTUAL_QTY=actual_qty-" & WIPGetQtyperPart(Part(i), 0) & " where id='" & dtCheckStock.Rows(0).Item("ID") & "'"
                                Dim cmdUpdateStock = New SqlCommand(sqlUpdateStock, Database.koneksi)
                                If cmdUpdateStock.ExecuteNonQuery() Then
                                    statusSimpan *= 1
                                Else
                                    statusSimpan *= 0
                                End If
                            Else
                                Dim sqlUpdateStock As String = "update STOCK_CARD set ACTUAL_QTY=actual_qty-" & WIPGetQtyperPart(Part(i), 0) & " where sub_sub_po='" & txtSubSubPODefective.Text & "' and status='Production Process' and material='" & Part(i) & "' and flow_ticket='" & sFlow_Ticket(5) & "' and fifo=(select min(fifo) from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and status='Production Process' and material='" & Part(i) & "' and flow_ticket='" & sFlow_Ticket(5) & "' and actual_qty > 0)"
                                Dim cmdUpdateStock = New SqlCommand(sqlUpdateStock, Database.koneksi)
                                If cmdUpdateStock.ExecuteNonQuery() Then
                                    statusSimpan *= 1
                                Else
                                    statusSimpan *= 0
                                End If
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
                    sqlStr = "select * from STOCK_PROD_WIP wip, sub_sub_po sp where wip.sub_sub_po=sp.sub_sub_po and sp.status='Open' ORDER BY wip.CODE_STOCK_PROD_WIP"
                Else
                    sqlStr = "select * from STOCK_PROD_WIP wip, sub_sub_po sp where wip.sub_sub_po=sp.sub_sub_po and sp.status='Open' and wip.PROCESS='" & proses & "' ORDER BY wip.CODE_STOCK_PROD_WIP"
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

                    Dim sql As String = "update STOCK_PROD_WIP set FLOW_TICKET_NO='" & txtWIPTicketNo.Text & "',QTY='" & WIPGetQtyperPart(matPN, 0) & "' where CODE_STOCK_PROD_WIP='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS='" & processName & "'"
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

                Part = materialList(cbOnHoldProcess.SelectedIndex).Split(";")

                sFlow_Ticket = txtOnHoldTicketNo.Text.Split(";")

                'diulang sebanyak part number yg ada
                Call Database.koneksi_database()
                Dim OnHoldCode As String = ONHOLDGenerateCode()

                For i = 0 To Part.Length - 2
                    Dim dtsubsubpo As String = WIPGetSubsubPO()

                    Dim dtList As String = WIPGetDataTraceability(Part(i), cbLineNumber.Text, dtsubsubpo, sFlow_Ticket(5))
                    Dim arrdtList() As String
                    arrdtList = dtList.Split(";")


                    Dim sql As String = "INSERT INTO STOCK_PROD_ONHOLD(CODE_STOCK_PROD_ONHOLD,SUB_SUB_PO,FG_PN,PART_NUMBER,LOT_NO,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,QTY,PO,PROCESS_ONHOLD,LINE,FLOW_TICKET_NO,DEPARTMENT) VALUES ('" &
                                        OnHoldCode & "','" & dtsubsubpo & "','" & cbFGPN.Text & "','" & Part(i) & "','" & arrdtList(5) & "','" & arrdtList(7) & "','" & arrdtList(8) & "','" & arrdtList(6) & "','" & WIPGetQtyperPart(Part(i), 1) & "','" & cbPONumber.Text & "','" & cbOnHoldProcess.Text & "','" & cbLineNumber.Text & "','" & txtOnHoldTicketNo.Text & "','" & dept & "')"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    If cmd.ExecuteNonQuery() Then
                        statusSimpan *= 1
                    Else
                        statusSimpan *= 0
                    End If
                Next

                If statusSimpan > 0 Then
                    MessageBox.Show("Success save data!!!")
                    LoaddgOnHold("")
                End If
            Catch ex As Exception
                MessageBox.Show("Error Insert : " & ex.Message)
            End Try


        End If
    End Sub

    Private Sub btnOnHoldEdit_Click(sender As Object, e As EventArgs) Handles btnOnHoldEdit.Click
        Dim i As Integer
        Dim result As DialogResult

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

                        Dim sql As String = "update STOCK_PROD_ONHOLD set FLOW_TICKET_NO='" & txtOnHoldTicketNo.Text & "',QTY='" & WIPGetQtyperPart(matPN, 1) & "' where CODE_STOCK_PROD_ONHOLD='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS_ONHOLD='" & processName & "'"
                        'Dim sql As String = "update STOCK_PROD_ONHOLD set QTY='" & WIPGetQtyperPart(matPN, 1) & "' where CODE_STOCK_PROD_ONHOLD='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS_ONHOLD='" & processName & "'"
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
                    sqlStr = "select * from STOCK_PROD_ONHOLD OH, sub_sub_po sp where oh.sub_sub_po=sp.sub_sub_po and sp.status='Open' ORDER BY oh.CODE_STOCK_PROD_ONHOLD"
                Else
                    sqlStr = "select * from STOCK_PROD_ONHOLD oh, sub_sub_po sp where oh.sub_sub_po=sp.sub_sub_po and sp.status='Open' and oh.PROCESS_ONHOLD='" & proses & "' ORDER BY oh.CODE_STOCK_PROD_ONHOLD"
                End If

                Dim dttable As DataTable = Database.GetData(sqlStr)


                If dttable.Rows.Count > 0 Then
                    For i = 0 To dttable.Rows.Count - 1
                        .Rows.Add(1)
                        .Item(0, i).Value = (i + 1).ToString()
                        .Item(1, i).Value = dttable.Rows(i)("CODE_STOCK_PROD_ONHOLD")
                        .Item(2, i).Value = dttable.Rows(i)("PROCESS_ONHOLD")
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

    '************************************* END ON HOLD FUNCTION


    '===================================== BALANCE FUNCTION
    Private Sub btnBalanceAdd_Click(sender As Object, e As EventArgs) Handles btnBalanceAdd.Click
        If txtBalanceBarcode.Text <> "" And txtBalanceQty.Value <= txtBalanceQty.Maximum And txtBalanceQty.Value > 0 Then
            Try
                Call Database.koneksi_database()

                Dim dtsubsubpo As String = WIPGetSubsubPO()
                Dim qtyUpdate As Integer = 0

                Dim dtCekTable As DataTable = Database.GetData("select * from STOCK_CARD where STATUS='Return to Mini Store' AND MATERIAL='" & txtBalanceMaterialPN.Text & "' AND SUB_SUB_PO='" & dtsubsubpo & "' AND LINE='" & cbLineNumber.Text & "' and departement='" & globVar.department & "' ORDER BY LOT_NO")
                If dtCekTable.Rows.Count > 0 Then

                    Dim result = MessageBox.Show("Sorry, the data has been previously recorded. Are you sure for update?.", "Are You Sure?", MessageBoxButtons.YesNo)

                    If result = DialogResult.Yes Then

                        If dtCekTable.Rows(0).Item("actual_qty") > txtBalanceQty.Value Then
                            qtyUpdate = dtCekTable.Rows(0).Item("actual_qty") - txtBalanceQty.Value

                            Dim sqlCheckStockCard As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and status='Production Request' and material='" & txtBalanceMaterialPN.Text & "' and departement='" & globVar.department & "'"
                            Dim dtCheckStockCard As DataTable = Database.GetData(sqlCheckStockCard)

                            If dtCheckStockCard.Rows.Count > 0 Then
                                Dim sqlUpdateStock As String = "update STOCK_CARD set ACTUAL_QTY=actual_qty+" & qtyUpdate & " where ID='" & dtCheckStockCard.Rows(0).Item("ID") & "'"
                                Dim cmdUpdateStock = New SqlCommand(sqlUpdateStock, Database.koneksi)
                                cmdUpdateStock.ExecuteNonQuery()
                            End If
                        Else
                            qtyUpdate = txtBalanceQty.Value - dtCekTable.Rows(0).Item("actual_qty")

                            Dim sqlCheckStockCard As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and status='Production Request' and material='" & txtBalanceMaterialPN.Text & "' and departement='" & globVar.department & "'"
                            Dim dtCheckStockCard As DataTable = Database.GetData(sqlCheckStockCard)

                            If dtCheckStockCard.Rows.Count > 0 Then
                                Dim sqlUpdateStock As String = "update STOCK_CARD set ACTUAL_QTY=actual_qty-" & qtyUpdate & " where ID='" & dtCheckStockCard.Rows(0).Item("ID") & "'"
                                Dim cmdUpdateStock = New SqlCommand(sqlUpdateStock, Database.koneksi)
                                cmdUpdateStock.ExecuteNonQuery()
                            End If
                        End If

                        Dim sqlUpdate As String = "update STOCK_CARD set ACTUAL_QTY=" & txtBalanceQty.Value & " where ID='" & dtCekTable.Rows(0).Item("ID") & "'"
                        Dim cmdUpdate = New SqlCommand(sqlUpdate, Database.koneksi)
                        If cmdUpdate.ExecuteNonQuery() Then
                            MessageBox.Show("Update Success.")
                        End If
                    End If

                Else
                    Dim idData As String = ""

                    Dim querySelectStockCard As String = "select id,MATERIAL,lot_no,inv_ctrl_date,traceability,batch_no,qty,actual_qty from STOCK_CARD where STATUS='Production Request' AND MATERIAL='" & txtBalanceMaterialPN.Text & "' AND SUB_SUB_PO='" & dtsubsubpo & "' AND LINE='" & cbLineNumber.Text & "' and actual_qty > 0 and departement='" & globVar.department & "' ORDER BY LOT_NO"
                    Dim dtTable As DataTable = Database.GetData(querySelectStockCard)
                    If dtTable.Rows.Count > 0 Then
                        idData = dtTable.Rows(0)(0)

                        Dim sqlUpdate As String = "update STOCK_CARD set ACTUAL_QTY=actual_qty-" & txtBalanceQty.Text & " where ID='" & idData & "'"
                        Dim cmdUpdate = New SqlCommand(sqlUpdate, Database.koneksi)
                        If cmdUpdate.ExecuteNonQuery() Then

                            Dim sqlInsert As String = "insert into STOCK_CARD(MTS_NO,DEPARTEMENT,MATERIAL,STATUS,STANDARD_PACK,INV_CTRL_DATE,TRACEABILITY,BATCH_NO,LOT_NO,FINISH_GOODS_PN,PO,SUB_PO,SUB_SUB_PO,LINE,QRCODE,QTY,ACTUAL_QTY)" &
                                                              "select MTS_NO,DEPARTEMENT,MATERIAL,'Return to Mini Store',STANDARD_PACK,INV_CTRL_DATE,TRACEABILITY,BATCH_NO,LOT_NO,FINISH_GOODS_PN,PO,SUB_PO,SUB_SUB_PO,LINE,'" & txtBalanceBarcode.Text & "',QTY," & txtBalanceQty.Text & " from STOCK_CARD where ID='" & idData & "'"
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
        End If
    End Sub

    Private Sub btnBalanceEdit_Click(sender As Object, e As EventArgs) Handles btnBalanceEdit.Click
        'BalanceParsingMaterialPN("MX2D1P1710813000Q000000000270S00202203012213Q0002BWL2041112212D202204114L               ChinaMLX001")

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

                        Dim sql As String = "update STOCK_PROD_ONHOLD set FLOW_TICKET_NO='" & txtOnHoldTicketNo.Text & "',QTY='" & WIPGetQtyperPart(matPN, 1) & "' where CODE_STOCK_PROD_ONHOLD='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS_ONHOLD='" & processName & "'"
                        'Dim sql As String = "update STOCK_PROD_ONHOLD set QTY='" & WIPGetQtyperPart(matPN, 1) & "' where CODE_STOCK_PROD_ONHOLD='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS_ONHOLD='" & processName & "'"
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
    End Sub

    Private Sub txtBalanceBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBalanceBarcode.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtBalanceMaterialPN.Text = BalanceParsingMaterialPN(txtBalanceBarcode.Text)

            Dim queryCheckProductionProcess As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and material='" & txtBalanceMaterialPN.Text & "' and status='Return To Mini Store' and actual_qty > 0 and departement='" & globVar.department & "'"
            Dim dttableProductionProcess As DataTable = Database.GetData(queryCheckProductionProcess)

            If dttableProductionProcess.Rows.Count = 0 Then
                Dim queryCheck As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and material='" & txtBalanceMaterialPN.Text & "' and status='Production Request' and actual_qty > 0 and departement='" & globVar.department & "'"
                Dim dttable As DataTable = Database.GetData(queryCheck)
                txtBalanceQty.Maximum = dttable.Rows(0).Item("actual_qty")
                txtBalanceQty.Value = dttable.Rows(0).Item("actual_qty")
            End If

            SendKeys.Send("{TAB}")
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

    Sub loaddgBalance(material As String)
        Dim i As Integer = 0

        Try
            With dgBalance
                .Rows.Clear()

                Dim sqlStr As String = ""

                If material = "" Then
                    sqlStr = "select row_number() OVER (ORDER BY sc.id) No,sc.id ID,sc.material Material,sc.inv_ctrl_date [Inv. Ctrl Date],sc.traceability Traceability,sc.batch_no [Batch No.],sc.lot_no [Lot No.],sc.qty [Lot Qty],sc.actual_qty [Actual Qty] from STOCK_CARD sc, sub_sub_po sp where sc.DEPARTEMENT='" & dept & "' AND sc.STATUS='Return to Mini Store' and sc.sub_sub_po=sp.sub_sub_po and sp.sub_sub_po='" & txtSubSubPODefective.Text & "' and sp.status='Open' ORDER BY sc.ID"
                Else
                    sqlStr = "select row_number() OVER (ORDER BY sc.id) n,sc.id,sc.material,sc.inv_ctrl_date,sc.traceability,sc.batch_no,sc.lot_no,sc.qty,sc.actual_qty from STOCK_CARD sc, sub_sub_po sp where sc.MATERIAL='" & material & "' AND sc.DEPARTEMENT='" & dept & "' AND sc.STATUS='Return to Mini Store' and sc.sub_sub_po=sp.sub_sub_po and sp.sub_sub_po='" & txtSubSubPODefective.Text & "' and sp.status='Open' ORDER BY sc.ID"
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
            MessageBox.Show("Error Load DGV Balance")
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Dim a As String = "Return to Mini Store"
        'Dim sqlInsert As String = "INSERT INTO STOCK_CARD(MTS_NO,DEPARTEMENT,MATERIAL,STATUS,STANDARD_PACK,INV_CTRL_DATE,TRACEABILITY,BATCH_NO,LOT_NO,FINISH_GOODS_PN,PO,SUB_PO,SUB_SUB_PO,LINE,DATETIME_INSERT,SAVE,QRCODE,DATETIME_SAVE,QTY,ACTUAL_QTY) VALUES ('"
        'For i = 2 To 19
        '    sqlInsert = sqlInsert & "','" & strData(i)
        'Next
        'sqlInsert = sqlInsert & "','" & txtBalanceQty.Text & "')"

        'Dim cmdInsert = New SqlCommand(sqlInsert, Database.koneksi)
        'If cmdInsert.ExecuteNonQuery() Then
        '    statusSimpan *= 1
        'Else
        '    statusSimpan *= 0
        'End If
    End Sub

    'Private Sub txtBalanceMaterialPN_TextChanged(sender As Object, e As EventArgs) Handles txtBalanceMaterialPN.TextChanged
    '    loaddgBalance(txtBalanceMaterialPN.Text)
    'End Sub

    'Private Sub dgBalance_Click(sender As Object, e As EventArgs) Handles dgBalance.Click
    '    idBalanceMaterial = dgBalance.Rows(dgBalance.CurrentCell.RowIndex).Cells(1).Value.ToString()
    '    MessageBox.Show(idBalanceMaterial)

    'End Sub

    '===================================== END BALANCE FUNCTION
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles rbFG.CheckedChanged
        If rbFG.Checked = True Then
            'ReadonlyFormFG(False)
            'ReadonlyFormSA(True)
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
            End If

        ElseIf rbSA.Checked = True Then

            'ReadonlyFormFG(True)
            'ReadonlyFormSA(False)
            Dim dtFG As DataTable = Database.GetData("select DISTINCT(CODE_OUT_PROD_REJECT), input_from_fg from out_prod_reject where SUB_SUB_PO='" & txtSubSubPODefective.Text & "'")

            If dtFG.Rows.Count > 0 Then

                If dtFG.Rows(0).Item("INPUT_FROM_FG") = 0 Then

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
                    DataGridView1.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub txtFGLabel_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtFGLabel.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
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
                    DataGridView1.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If DataGridView1.Rows.Count > 0 Then
            For i = 0 To DataGridView1.Rows.Count - 1
                Dim doWhile As String = ""
                If DataGridView1.Rows(i).Cells(2).Value IsNot "" And DataGridView1.Rows(i).Cells(2).Value IsNot Nothing And DataGridView1.Rows(i).Cells(2).Value IsNot DBNull.Value Then
                    If IsNumeric(DataGridView1.Rows(i).Cells(2).Value) Then
                        Dim query As String = "select * from master_process_flow where master_process='" & DataGridView1.Rows(i).Cells(1).Value & "'"
                        Dim dtMasterProcessFlow As DataTable = Database.GetData(query)
                        Dim numberInt As Integer = dtMasterProcessFlow.Rows(0).Item("ID") - 1
                        If dtMasterProcessFlow.Rows.Count > 0 Then
                            If IsDBNull(dtMasterProcessFlow.Rows(0).Item("MATERIAL_USAGE")) = False Then
                                subInsertRejectFG(dtMasterProcessFlow.Rows(0).Item("MATERIAL_USAGE"), DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(1).Value)
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
                                subInsertRejectFG(doWhile, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(1).Value)
                            End If
                        End If
                    Else
                        MessageBox.Show("this is not number -> " & DataGridView1.Rows(i).Cells(2).Value & ". Please change with number.")
                    End If
                End If
            Next
        End If
    End Sub

    Sub subInsertRejectFG(pn As String, qty As Integer, process As String)
        Dim splitPN() As String = pn.Split(";")
        Dim splitFlowTicket() As String = txtFGFlowTicket.Text.Split(";")

        Dim queryCheckCodeReject As String = "select DISTINCT(CODE_OUT_PROD_REJECT) from OUT_PROD_REJECT"
        Dim dtCheckCodeReject As DataTable = Database.GetData(queryCheckCodeReject)
        Dim codeReject As String = "RJ" & dtCheckCodeReject.Rows.Count + 1

        For i = 0 To splitPN.Length - 1

            Dim queryGetUsage As String = "select * from material_usage_finish_goods where fg_part_number='" & cbFGPN.Text & "' and component='" & splitPN(i) & "'"
            Dim dtGetUsage As DataTable = Database.GetData(queryGetUsage)

            If dtGetUsage.Rows.Count > 0 Then

                Dim queryCheckMaterial As String = "select * from process_prod where level='Fresh' and pn_material='" & splitPN(i) & "' and qty>0 order by fifo"
                Dim dtCheckMaterial As DataTable = Database.GetData(queryCheckMaterial)

                Dim qtyReject = qty * dtGetUsage.Rows(0).Item("USAGE")

                Dim qtyDoneFG = splitFlowTicket(3) * dtGetUsage.Rows(0).Item("USAGE")

                Dim queryCheckRejectPN As String = "select * from out_prod_reject where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and flow_ticket_no='" & splitFlowTicket(5) & "' and part_number='" & splitPN(i) & "' and process_reject='" & process & "' AND DEPARTMENT='" & dept & "'"
                Dim dtCheckRejectPN As DataTable = Database.GetData(queryCheckRejectPN)

                If dtCheckRejectPN.Rows.Count > 0 Then
                    Dim sqlUpdateRejectPN As String = "update out_prod_reject set qty=" & qtyReject & " where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and part_number='" & splitPN(i) & "' and PROCESS_REJECT='" & process & "' and line='" & cbLineNumber.Text & "' and FLOW_TICKET_NO='" & splitFlowTicket(5) & "' AND DEPARTMENT='" & dept & "'"
                    Dim cmdInsertRejectPN = New SqlCommand(sqlUpdateRejectPN, Database.koneksi)
                    cmdInsertRejectPN.ExecuteNonQuery()
                Else
                    Dim queryCheckReject As String = "select * from out_prod_reject where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and flow_ticket_no='" & splitFlowTicket(5) & "' AND DEPARTMENT='" & dept & "'"
                    Dim dtCheckReject As DataTable = Database.GetData(queryCheckReject)

                    If dtCheckReject.Rows.Count > 0 Then

                        'Insert Reject Prod
                        Dim sqlInsertRejectPN As String = "INSERT INTO out_prod_reject (CODE_OUT_PROD_REJECT, sub_sub_po, FG_PN, PART_NUMBER, LOT_NO, TRACEABILITY,INV_CTRL_DATE,BATCH_NO,QTY,PO,PROCESS_REJECT,LINE,FLOW_TICKET_NO,DEPARTMENT,PENGALI,INPUT_FROM_FG)
                            VALUES ('" & dtCheckReject.Rows(0).Item("CODE_OUT_PROD_REJECT") & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & splitPN(i) & "','" & dtCheckMaterial.Rows(0).Item("LOT_NO") & "',
                            '" & dtCheckMaterial.Rows(0).Item("TRACEABILITY") & "','" & dtCheckMaterial.Rows(0).Item("INV_CTRL_DATE") & "','" & dtCheckMaterial.Rows(0).Item("BATCH_NO") & "',
                            " & qtyReject & ",'" & cbPONumber.Text & "','" & process & "','" & cbLineNumber.Text & "','" & splitFlowTicket(5) & "','" & dept & "'," & qty & ",1)"
                        Dim cmdInsertRejectPN = New SqlCommand(sqlInsertRejectPN, Database.koneksi)
                        If cmdInsertRejectPN.ExecuteNonQuery() Then

                            'Update Stock Process Prod
                            Dim sqlUpdateProcessProd As String = "update PROCESS_PROD set qty=qty-" & qtyReject & " where level='fresh' and sub_sub_po='" & txtSubSubPODefective.Text & "' and pn_material='" & splitPN(i) & "' and line='" & cbLineNumber.Text & "' AND DEPARTMENT='" & dept & "' and fifo=(select min(fifo) from PROCESS_PROD where pn_material='" & splitPN(i) & "' and level='Fresh' and qty>0)"
                            Dim cmdUpdateProcessProd = New SqlCommand(sqlUpdateProcessProd, Database.koneksi)
                            cmdUpdateProcessProd.ExecuteNonQuery()

                            'Insert Done FG
                            Dim queryCheckDoneFG As String = "select * from done_fg where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg='" & cbFGPN.Text & "' and flow_ticket='" & splitFlowTicket(5) & "' AND DEPARTMENT='" & dept & "'"
                            Dim dtCheckDoneFG As DataTable = Database.GetData(queryCheckDoneFG)
                            If dtCheckDoneFG.Rows.Count = 0 Then
                                Dim sqlInsertDoneFG As String = "INSERT INTO done_fg (po, sub_sub_po, FG, BATCH_NO,TRACEABILITY,INV_CTRL_DATE,laser_code,line,flow_ticket,QTY,department)
                                    VALUES ('" & cbPONumber.Text & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & txtBatchno.Text & "','" & txtTampungLabel.Text & "',
                                    '" & txtINV.Text & "','" & txtBatchno.Text & "','" & cbLineNumber.Text & "','" & splitFlowTicket(5) & "'," & txtSPQ.Text & ",'" & dept & "')"
                                Dim cmdInsertDoneFG = New SqlCommand(sqlInsertDoneFG, Database.koneksi)
                                cmdInsertDoneFG.ExecuteNonQuery()
                            End If
                        End If
                    Else
                        Dim sqlInsertRejectPN As String = "INSERT INTO out_prod_reject (CODE_OUT_PROD_REJECT, sub_sub_po, FG_PN, PART_NUMBER, LOT_NO, TRACEABILITY,INV_CTRL_DATE,BATCH_NO,QTY,PO,PROCESS_REJECT,LINE,FLOW_TICKET_NO,DEPARTMENT,PENGALI,INPUT_FROM_FG)
                            VALUES ('" & codeReject & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & splitPN(i) & "','" & dtCheckMaterial.Rows(0).Item("LOT_NO") & "',
                            '" & dtCheckMaterial.Rows(0).Item("TRACEABILITY") & "','" & dtCheckMaterial.Rows(0).Item("INV_CTRL_DATE") & "','" & dtCheckMaterial.Rows(0).Item("BATCH_NO") & "',
                            " & qtyReject & ",'" & cbPONumber.Text & "','" & process & "','" & cbLineNumber.Text & "','" & splitFlowTicket(5) & "','" & dept & "'," & qty & ",1)"
                        Dim cmdInsertRejectPN = New SqlCommand(sqlInsertRejectPN, Database.koneksi)

                        If cmdInsertRejectPN.ExecuteNonQuery() Then
                            Dim queryCheckDoneFG As String = "select * from done_fg where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg='" & cbFGPN.Text & "' and flow_ticket='" & splitFlowTicket(5) & "' AND DEPARTMENT='" & dept & "'"
                            Dim dtCheckDoneFG As DataTable = Database.GetData(queryCheckDoneFG)

                            If dtCheckDoneFG.Rows.Count = 0 Then
                                Dim sqlInsertDoneFG As String = "INSERT INTO done_fg (po, sub_sub_po, FG, BATCH_NO,TRACEABILITY,INV_CTRL_DATE,laser_code,line,flow_ticket,QTY,department)
                                    VALUES ('" & cbPONumber.Text & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & txtBatchno.Text & "','" & txtTampungLabel.Text & "',
                                    '" & txtINV.Text & "','" & txtBatchno.Text & "','" & cbLineNumber.Text & "','" & splitFlowTicket(5) & "'," & txtSPQ.Text & ",'" & dept & "')"
                                Dim cmdInsertDoneFG = New SqlCommand(sqlInsertDoneFG, Database.koneksi)
                                cmdInsertDoneFG.ExecuteNonQuery()
                            End If

                            Dim sqlUpdateProcessProd As String = "update PROCESS_PROD set qty=qty-" & qtyReject & " where level='fresh' and sub_sub_po='" & txtSubSubPODefective.Text & "' and pn_material='" & splitPN(i) & "' and line='" & cbLineNumber.Text & "' AND DEPARTMENT='" & dept & "' and fifo=(select min(fifo) from PROCESS_PROD where pn_material='" & splitPN(i) & "' and level='Fresh' and qty>0)"
                            Dim cmdUpdateProcessProd = New SqlCommand(sqlUpdateProcessProd, Database.koneksi)
                            cmdUpdateProcessProd.ExecuteNonQuery()
                        End If
                    End If
                End If
            End If
        Next
    End Sub

    Sub subInsertRejectSA(pn As String, qty As Integer, process As String)
        Dim splitPN() As String = pn.Split(";")
        Dim splitFlowTicket() As String = txtFGFlowTicket.Text.Split(";")

        Dim queryCheckCodeReject As String = "select DISTINCT(CODE_OUT_PROD_REJECT) from OUT_PROD_REJECT"
        Dim dtCheckCodeReject As DataTable = Database.GetData(queryCheckCodeReject)
        Dim codeReject As String = "RJ" & dtCheckCodeReject.Rows.Count + 1

        For i = 0 To splitPN.Length - 1

            Dim queryGetUsage As String = "select * from material_usage_finish_goods where fg_part_number='" & cbFGPN.Text & "' and component='" & splitPN(i) & "'"
            Dim dtGetUsage As DataTable = Database.GetData(queryGetUsage)

            If dtGetUsage.Rows.Count > 0 Then

                Dim queryCheckMaterial As String = "select * from process_prod where level='Fresh' and pn_material='" & splitPN(i) & "' and qty>0 order by fifo"
                Dim dtCheckMaterial As DataTable = Database.GetData(queryCheckMaterial)

                Dim qtyReject = qty * dtGetUsage.Rows(0).Item("USAGE")

                Dim queryCheckRejectPN As String = "select * from out_prod_reject where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and flow_ticket_no='" & splitFlowTicket(5) & "' and part_number='" & splitPN(i) & "' and process_reject='" & process & "' AND DEPARTMENT='" & dept & "'"
                Dim dtCheckRejectPN As DataTable = Database.GetData(queryCheckRejectPN)

                If dtCheckRejectPN.Rows.Count > 0 Then
                    Dim sqlUpdateRejectPN As String = "update out_prod_reject set qty=" & qtyReject & " where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and part_number='" & splitPN(i) & "' and PROCESS_REJECT='" & process & "' and line='" & cbLineNumber.Text & "' and FLOW_TICKET_NO='" & splitFlowTicket(5) & "' AND DEPARTMENT='" & dept & "'"
                    Dim cmdInsertRejectPN = New SqlCommand(sqlUpdateRejectPN, Database.koneksi)
                    cmdInsertRejectPN.ExecuteNonQuery()
                Else
                    Dim queryCheckReject As String = "select * from out_prod_reject where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and flow_ticket_no='" & splitFlowTicket(5) & "' AND DEPARTMENT='" & dept & "'"
                    Dim dtCheckReject As DataTable = Database.GetData(queryCheckReject)

                    If dtCheckReject.Rows.Count > 0 Then
                        Dim sqlInsertRejectPN As String = "INSERT INTO out_prod_reject (CODE_OUT_PROD_REJECT, sub_sub_po, FG_PN, PART_NUMBER, LOT_NO, TRACEABILITY,INV_CTRL_DATE,BATCH_NO,QTY,PO,PROCESS_REJECT,LINE,FLOW_TICKET_NO,DEPARTMENT,PENGALI,INPUT_FROM_FG)
                            VALUES ('" & dtCheckReject.Rows(0).Item("CODE_OUT_PROD_REJECT") & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & splitPN(i) & "','" & dtCheckMaterial.Rows(0).Item("LOT_NO") & "',
                            '" & dtCheckMaterial.Rows(0).Item("TRACEABILITY") & "','" & dtCheckMaterial.Rows(0).Item("INV_CTRL_DATE") & "','" & dtCheckMaterial.Rows(0).Item("BATCH_NO") & "',
                            " & qtyReject & ",'" & cbPONumber.Text & "','" & process & "','" & cbLineNumber.Text & "','" & splitFlowTicket(5) & "','" & dept & "'," & qty & ",1)"
                        Dim cmdInsertRejectPN = New SqlCommand(sqlInsertRejectPN, Database.koneksi)
                        If cmdInsertRejectPN.ExecuteNonQuery() Then
                            Dim queryCheckDoneFG As String = "select * from done_fg where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg='" & cbFGPN.Text & "' and flow_ticket='" & splitFlowTicket(5) & "' AND DEPARTMENT='" & dept & "'"
                            Dim dtCheckDoneFG As DataTable = Database.GetData(queryCheckDoneFG)

                            If dtCheckDoneFG.Rows.Count = 0 Then
                                'insert disini
                            End If

                            Dim sqlUpdateProcessProd As String = "update PROCESS_PROD set qty=qty-" & qtyReject & " where level='fresh' and sub_sub_po='" & txtSubSubPODefective.Text & "' and pn_material='" & splitPN(i) & "' and line='" & cbLineNumber.Text & "' AND DEPARTMENT='" & dept & "' and fifo=(select min(fifo) from PROCESS_PROD where pn_material=1710813000 and level='Fresh' and qty>0)"
                            Dim cmdUpdateProcessProd = New SqlCommand(sqlUpdateProcessProd, Database.koneksi)
                            cmdUpdateProcessProd.ExecuteNonQuery()
                        End If
                    Else
                        Dim sqlInsertRejectPN As String = "INSERT INTO out_prod_reject (CODE_OUT_PROD_REJECT, sub_sub_po, FG_PN, PART_NUMBER, LOT_NO, TRACEABILITY,INV_CTRL_DATE,BATCH_NO,QTY,PO,PROCESS_REJECT,LINE,FLOW_TICKET_NO,DEPARTMENT,PENGALI,INPUT_FROM_FG)
                            VALUES ('" & codeReject & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & splitPN(i) & "','" & dtCheckMaterial.Rows(0).Item("LOT_NO") & "',
                            '" & dtCheckMaterial.Rows(0).Item("TRACEABILITY") & "','" & dtCheckMaterial.Rows(0).Item("INV_CTRL_DATE") & "','" & dtCheckMaterial.Rows(0).Item("BATCH_NO") & "',
                            " & qtyReject & ",'" & cbPONumber.Text & "','" & process & "','" & cbLineNumber.Text & "','" & splitFlowTicket(5) & "','" & dept & "'," & qty & ",1)"
                        Dim cmdInsertRejectPN = New SqlCommand(sqlInsertRejectPN, Database.koneksi)

                        If cmdInsertRejectPN.ExecuteNonQuery() Then
                            Dim queryCheckDoneFG As String = "select * from done_fg where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg='" & cbFGPN.Text & "' and flow_ticket='" & splitFlowTicket(5) & "' AND DEPARTMENT='" & dept & "'"
                            Dim dtCheckDoneFG As DataTable = Database.GetData(queryCheckDoneFG)

                            If dtCheckDoneFG.Rows.Count = 0 Then
                                'insert disini
                            End If

                            Dim sqlUpdateProcessProd As String = "update PROCESS_PROD set qty=qty-" & qtyReject & " where level='fresh' and sub_sub_po='" & txtSubSubPODefective.Text & "' and pn_material='" & splitPN(i) & "' and line='" & cbLineNumber.Text & "' AND DEPARTMENT='" & dept & "' and fifo=(select min(fifo) from PROCESS_PROD where pn_material=1710813000 and level='Fresh' and qty>0)"
                            Dim cmdUpdateProcessProd = New SqlCommand(sqlUpdateProcessProd, Database.koneksi)
                            cmdUpdateProcessProd.ExecuteNonQuery()
                        End If
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If DataGridView1.Rows.Count > 0 Then
            For i = 0 To DataGridView1.Rows.Count - 1
                Dim doWhile As String = ""
                If DataGridView1.Rows(i).Cells(2).Value IsNot "" And DataGridView1.Rows(i).Cells(2).Value IsNot Nothing Then
                    If IsNumeric(DataGridView1.Rows(i).Cells(2).Value) Then
                        Dim query As String = "select * from master_process_flow where master_process='" & DataGridView1.Rows(i).Cells(1).Value & "'"
                        Dim dtMasterProcessFlow As DataTable = Database.GetData(query)
                        Dim numberInt As Integer = dtMasterProcessFlow.Rows(0).Item("ID") - 1
                        If dtMasterProcessFlow.Rows.Count > 0 Then
                            If IsDBNull(dtMasterProcessFlow.Rows(0).Item("MATERIAL_USAGE")) = False Then
                                subInsertRejectSA(dtMasterProcessFlow.Rows(0).Item("MATERIAL_USAGE"), DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(1).Value)
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
                                subInsertRejectSA(doWhile, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(1).Value)
                            End If
                        End If
                    Else
                        MessageBox.Show("this is not number -> " & DataGridView1.Rows(i).Cells(2).Value & ". Please change with number.")
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub txtSAFlowTicket_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtSAFlowTicket.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
            Dim Split() As String = txtSAFlowTicket.Text.Split(";")
            Dim Split1() As String = Split(0).Split("-")

            txtTampungFlow.Text = Split1(0)

            If rbSA.Checked = True Then
                loadSA(cbFGPN.Text, txtSAFlowTicket.Text)
                Button5.Enabled = True
                DataGridView2.Enabled = True
            End If
        End If
    End Sub

    Private Sub txtLabelOtherPart_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtLabelOtherPart.PreviewKeyDown

        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
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
                        .Rows.Add(1)
                        .Item(0, i).Value = (i + 1).ToString()
                        .Item(1, i).Value = dtOutProd.Rows(i)("part_number")
                        .Item(2, i).Value = dtOutProd.Rows(i)("qty")
                        .Item(3, i).Value = ""
                    Next
                End If

                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End If
    End Sub

    Private Sub btnOtherSave_Click(sender As Object, e As EventArgs) Handles btnOtherSave.Click
        If DataGridView2.Rows.Count > 0 Then
            For i = 0 To DataGridView2.Rows.Count - 1

                Dim queryCheckCodeOther As String = "select DISTINCT(CODE_stock_prod_others) from stock_prod_others"
                Dim dtCheckCodeOther As DataTable = Database.GetData(queryCheckCodeOther)
                Dim codeOther As String = "OTHERS" & dtCheckCodeOther.Rows.Count + 1

                If DataGridView2.Rows(i).Cells(3).Value IsNot "" And DataGridView2.Rows(i).Cells(3).Value IsNot Nothing And DataGridView2.Rows(i).Cells(3).Value IsNot DBNull.Value Then
                    If IsNumeric(DataGridView2.Rows(i).Cells(3).Value) Then
                        If DataGridView2.Rows(i).Cells(3).Value <= DataGridView2.Rows(i).Cells(2).Value Then
                            Dim query As String = "select * from stock_prod_others where code_out_prod_reject='" & txtLabelOtherPart.Text & "' and part_number='" & DataGridView2.Rows(i).Cells(1).Value & "' and department='" & dept & "'"
                            Dim dtCheckStockOthers As DataTable = Database.GetData(query)
                            If dtCheckStockOthers.Rows.Count > 0 Then
                                MessageBox.Show("Update")
                                Dim sqlUpdateProcessProd As String = "update stock_prod_others set qty=" & DataGridView2.Rows(i).Cells(3).Value & " where code_out_prod_reject='" & txtLabelOtherPart.Text & "' and part_number='" & DataGridView2.Rows(i).Cells(1).Value & "' and department='" & dept & "'"
                                Dim cmdUpdateProcessProd = New SqlCommand(sqlUpdateProcessProd, Database.koneksi)
                                cmdUpdateProcessProd.ExecuteNonQuery()
                            Else
                                MessageBox.Show("Insert")
                                Dim sqlInsertOther As String = "INSERT INTO stock_prod_others (CODE_STOCK_PROD_OTHERS, PART_NUMBER, LOT_NO, TRACEABILITY,INV_CTRL_DATE,BATCH_NO,QTY,CODE_OUT_PROD_REJECT,DEPARTMENT)
                                    VALUES ('" & codeOther & "','" & DataGridView2.Rows(i).Cells(1).Value & "','0','0','0','0','" & DataGridView2.Rows(i).Cells(3).Value & "','" & txtLabelOtherPart.Text & "','" & dept & "')"
                                Dim cmdInsertOther = New SqlCommand(sqlInsertOther, Database.koneksi)
                                cmdInsertOther.ExecuteNonQuery()
                            End If
                        End If
                    Else
                        MessageBox.Show("this is not number -> " & DataGridView2.Rows(i).Cells(2).Value & ". Please change with number.")
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If txtSubSubPODefective.Text <> "" Then
            Dim query As String = "select sc.id,sc.material,m.name,sc.traceability,sc.inv_ctrl_date,sc.batch_no,sc.lot_no,sc.actual_qty,sc.lot_no from stock_card sc, master_material m where sc.status='Return To Mini Store' and sc.sub_sub_po='" & txtSubSubPODefective.Text & "' and sc.actual_qty>0 and sc.material=m.part_number"
            Dim dtCheckStockBalance As DataTable = Database.GetData(query)
            If dtCheckStockBalance.Rows.Count > 0 Then
                For i = 0 To dtCheckStockBalance.Rows.Count - 1
                    Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, lot, remark,sub_sub_po,department,material)
                                    VALUES ('" & cbPONumber.Text & "','" & cbFGPN.Text & "','" & cbLineNumber.Text & "','" & dtCheckStockBalance.Rows(i).Item("lot_no") & "','Balance Material','" & txtSubSubPODefective.Text & "','" & dept & "','" & dtCheckStockBalance.Rows(i).Item("material") & "')"
                    Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                    If cmdInsertPrintingRecord.ExecuteNonQuery() Then
                        _PrintingSubAssyRawMaterial.txt_Unique_id.Text = dtCheckStockBalance.Rows(i).Item("id")
                        _PrintingSubAssyRawMaterial.txt_part_number.Text = dtCheckStockBalance.Rows(i).Item("material")
                        _PrintingSubAssyRawMaterial.txt_Part_Description.Text = dtCheckStockBalance.Rows(i).Item("name")
                        _PrintingSubAssyRawMaterial.txt_Traceability.Text = dtCheckStockBalance.Rows(i).Item("traceability")
                        _PrintingSubAssyRawMaterial.txt_Inv_crtl_date.Text = dtCheckStockBalance.Rows(i).Item("inv_ctrl_date")
                        _PrintingSubAssyRawMaterial.txt_Batch_no.Text = dtCheckStockBalance.Rows(i).Item("batch_no")
                        _PrintingSubAssyRawMaterial.txt_Lot_no.Text = dtCheckStockBalance.Rows(i).Item("lot_no")
                        _PrintingSubAssyRawMaterial.txt_Qty.Text = dtCheckStockBalance.Rows(i).Item("actual_qty")
                        _PrintingSubAssyRawMaterial.txt_QR_Code.Text = dtCheckStockBalance.Rows(i).Item("material") & ";" & dtCheckStockBalance.Rows(i).Item("lot_no") & ";" & dtCheckStockBalance.Rows(i).Item("traceability") & ";" & dtCheckStockBalance.Rows(i).Item("inv_ctrl_date") & ";" & dtCheckStockBalance.Rows(i).Item("batch_no") & ";" & dtCheckStockBalance.Rows(i).Item("actual_qty") & ";" & dtCheckStockBalance.Rows(i).Item("id")
                        _PrintingSubAssyRawMaterial.btn_Print_Click(sender, e)
                    End If
                Next
            End If
        Else
            MessageBox.Show("Sorry please select Line First.")
        End If
    End Sub
End Class