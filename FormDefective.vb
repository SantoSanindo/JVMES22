Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Reflection.Emit
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class FormDefective
    Dim dept As String = "ZQSFP"
    Dim idLine As New List(Of Integer)
    Dim materialList As New List(Of String)

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
            Dim dtSubPO As DataTable = Database.GetData("select distinct MAIN_PO from SUB_SUB_PO where LINE='" & cbLineNumber.Text & "'")

            Dim listIDPO As New List(Of String)

            cbPONumber.Items.Clear()

            If dtSubPO.Rows.Count > 0 Then
                For i As Integer = 0 To dtSubPO.Rows.Count - 1
                    listIDPO.Add(dtSubPO.Rows(i)(0))
                    'cbPONumber.Items.Add(dtSubPO.Rows(i)(0))

                    Dim dtIdPO As DataTable = Database.GetData("select distinct PO from MAIN_PO where ID='" & dtSubPO.Rows(i)(0) & "'")

                    If dtIdPO.Rows.Count > 0 Then
                        If cbPONumber.Items.Contains(dtIdPO.Rows(0)(0)) Then
                            Continue For
                        End If
                        cbPONumber.Items.Add(dtIdPO.Rows(0)(0))
                    End If
                Next
            End If
            'cbPONumber.DataSource = dtMasterMaterial
            'cbPONumber.DisplayMember = "PO_NO"
            'cbPONumber.ValueMember = "PO_NO"

            'cbPONumber.Text = ""
        Catch ex As Exception
            MessageBox.Show("error load PO_NO")
        End Try
    End Sub

    Private Sub cbPONumber_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPONumber.SelectedIndexChanged
        If cbPONumber.Text <> "" And cbLineNumber.Text <> "" Then
            Try
                Call Database.koneksi_database()
                Dim dtFG_PN As DataTable = Database.GetData("select distinct FG_PN from MAIN_PO where PO='" & cbPONumber.Text & "'")

                cbFGPN.Items.Clear()

                If dtFG_PN.Rows.Count > 0 Then
                    For i As Integer = 0 To dtFG_PN.Rows.Count - 1
                        cbFGPN.Items.Add(dtFG_PN.Rows(i)(0))
                    Next
                End If

                'cbFGPN.DataSource = dtMasterMaterial
                'cbFGPN.DisplayMember = "FG_PN"
                'cbFGPN.ValueMember = "FG_PN"

                'cbFGPN.Text = ""
            Catch ex As Exception
                MessageBox.Show("error load FG_PN")
            End Try
        End If
    End Sub

    Private Sub cbFGPN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFGPN.SelectedIndexChanged
        'If cbFGPN.Text <> "" Then
        loadCBDefPartProcess(cbFGPN.Text)
        'End If
        'MessageBox.Show(cbFGPN.SelectedIndex.ToString())

        enableStatusInputInput(True)
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
                MessageBox.Show("error load Process flow")
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
            MessageBox.Show("error load ShowToDGView")
        End Try


    End Sub

    Private Sub FormDefective_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadCBLine()

        enableStatusInputInput(False)
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
            Call Database.koneksi_database()
            Dim dtLine As DataTable = Database.GetData("select distinct ID, NAME from MASTER_LINE where DEPARTEMENT='" & dept & "' ORDER BY NAME")

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
            MessageBox.Show("error load Process flow")
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
                Dim i As Integer
                Dim statusSimpan As Integer = 1

                Part = materialList(cbWIPProcess.SelectedIndex).Split(";")

                'diulang sebanyak part number yg ada
                Call Database.koneksi_database()
                For i = 0 To Part.Length - 2
                    Dim dtsubsubpo As String = WIPGetSubsubPO()

                    Dim dtList As String = WIPGetDataTraceability(Part(i), cbLineNumber.Text, dtsubsubpo)
                    Dim arrdtList() As String
                    arrdtList = dtList.Split(";")


                    Dim sql As String = "INSERT INTO STOCK_PROD_WIP(CODE_STOCK_PROD_WIP,SUB_SUB_PO,FG_PN,PART_NUMBER,LOT_NO,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,PROCESS,QTY,DATETIME_INSERT,PO) VALUES ('" &
                                        WIPGenerateCode() & "','" & dtsubsubpo & "','" & cbFGPN.Text & "','" & Part(i) & "','" & txtWIPTicketNo.Text & "','" & arrdtList(7) & "','" & arrdtList(8) & "','" & arrdtList(6) & "','" & cbWIPProcess.Text & "','" & WIPGetQtyperPart(Part(i), 0) & "','" & arrdtList(11) & "','" & cbPONumber.Text & "')"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    If cmd.ExecuteNonQuery() Then
                        statusSimpan *= 1
                    Else
                        statusSimpan *= 0
                    End If
                Next

                If statusSimpan > 0 Then
                    MessageBox.Show("Success save data!!!")
                    LoaddgWIP("")
                End If
            Catch ex As Exception
                MessageBox.Show("Error Insert : " & ex.Message)
            End Try


        End If

    End Sub

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

                .ColumnCount = 10
                .Columns(0).Name = "No"
                .Columns(1).Name = "ID"
                .Columns(2).Name = "Process Name"
                .Columns(3).Name = "Ticket No."
                .Columns(4).Name = "Material PN"
                .Columns(5).Name = "Inv No."
                .Columns(6).Name = "MFG Date"
                .Columns(7).Name = "Lot Code"
                .Columns(8).Name = "Lot No."
                .Columns(9).Name = "Qty"

                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

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
                .Columns(2).Width = Int(.Width * 0.2)
                .Columns(3).Width = Int(.Width * 0.08)
                .Columns(4).Width = Int(.Width * 0.08)
                .Columns(5).Width = Int(.Width * 0.1)
                .Columns(6).Width = Int(.Width * 0.15)
                .Columns(7).Width = Int(.Width * 0.08)
                .Columns(8).Width = Int(.Width * 0.08)
                .Columns(9).Width = Int(.Width * 0.08)


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
                        .Item(1, i).Value = dttable.Rows(i)(1)
                        .Item(2, i).Value = dttable.Rows(i)(9)
                        .Item(3, i).Value = dttable.Rows(i)(5)
                        .Item(4, i).Value = dttable.Rows(i)(4)
                        .Item(5, i).Value = dttable.Rows(i)(7)
                        .Item(6, i).Value = dttable.Rows(i)(6)
                        .Item(7, i).Value = "Lot Code"
                        .Item(8, i).Value = "Lot No."
                        .Item(9, i).Value = dttable.Rows(i)(10)

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
            MessageBox.Show("error load ShowToDGView")
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

                wipCode = "WIP" + dt
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

    Function WIPGetDataTraceability(pn_no As String, lineNo As String, subsubpo As String) As String
        Dim dataTrace As String
        Dim i As Integer

        dataTrace = ""
        Try
            Call Database.koneksi_database()

            'get id PO dari MAIN_PO
            Dim idPO As String = ""
            Dim dtCode As DataTable = Database.GetData("select * from PROCESS_PROD where PN_MATERIAL='" & pn_no & "' AND LINE='" & lineNo & "' AND SUB_SUB_PO='" & subsubpo & "' ORDER BY FIFO DESC")
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

                    'Dim sql As String = "update STOCK_PROD_WIP set TICKET_NO='" & txtWIPTicketNo.Text & "',QTY='" & WIPGetQtyperPart(matPN) & "' where CODE_STOCK_PROD_WIP='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS='" & processName & "'"
                    Dim sql As String = "update STOCK_PROD_WIP set QTY='" & WIPGetQtyperPart(matPN, 0) & "' where CODE_STOCK_PROD_WIP='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS='" & processName & "'"
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
                Dim i As Integer
                Dim statusSimpan As Integer = 1

                Part = materialList(cbOnHoldProcess.SelectedIndex).Split(";")

                'diulang sebanyak part number yg ada
                Call Database.koneksi_database()
                For i = 0 To Part.Length - 2
                    Dim dtsubsubpo As String = WIPGetSubsubPO()

                    Dim dtList As String = WIPGetDataTraceability(Part(i), cbLineNumber.Text, dtsubsubpo)
                    Dim arrdtList() As String
                    arrdtList = dtList.Split(";")


                    Dim sql As String = "INSERT INTO STOCK_PROD_ONHOLD(CODE_STOCK_PROD_ONHOLD,SUB_SUB_PO,FG_PN,PART_NUMBER,LOT_NO,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,QTY,DATETIME_INSERT,PO,PROCESS_ONHOLD,LINE) VALUES ('" &
                                        ONHOLDGenerateCode() & "','" & dtsubsubpo & "','" & cbFGPN.Text & "','" & Part(i) & "','" & txtOnHoldTicketNo.Text & "','" & arrdtList(7) & "','" & arrdtList(8) & "','" & arrdtList(6) & "','" & WIPGetQtyperPart(Part(i), 1) & "','" & arrdtList(11) & "','" & cbPONumber.Text & "','" & cbOnHoldProcess.Text & "','" & cbLineNumber.Text & "')"
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

                        'Dim sql As String = "update STOCK_PROD_ONHOLD set TICKET_NO='" & txtWIPTicketNo.Text & "',QTY='" & WIPGetQtyperPart(matPN) & "' where CODE_STOCK_PROD_WIP='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS='" & processName & "'"
                        Dim sql As String = "update STOCK_PROD_ONHOLD set QTY='" & WIPGetQtyperPart(matPN, 1) & "' where CODE_STOCK_PROD_ONHOLD='" & id & "' AND PART_NUMBER='" & matPN & "' AND PROCESS_ONHOLD='" & processName & "'"
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
                .Columns(7).Name = "Lot Code"
                .Columns(8).Name = "Lot No."
                .Columns(9).Name = "Qty"

                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(0).Width = Int(.Width * 0.05)
                .Columns(1).Width = Int(.Width * 0.08)
                .Columns(2).Width = Int(.Width * 0.2)
                .Columns(3).Width = Int(.Width * 0.08)
                .Columns(4).Width = Int(.Width * 0.08)
                .Columns(5).Width = Int(.Width * 0.1)
                .Columns(6).Width = Int(.Width * 0.15)
                .Columns(7).Width = Int(.Width * 0.08)
                .Columns(8).Width = Int(.Width * 0.08)
                .Columns(9).Width = Int(.Width * 0.08)


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
                    sqlStr = "select * from STOCK_PROD_ONHOLD ORDER BY CODE_STOCK_PROD_ONHOLD"
                Else
                    sqlStr = "select * from STOCK_PROD_ONHOLD where PROCESS_ONHOLD='" & proses & "' ORDER BY CODE_STOCK_PROD_ONHOLD"
                End If

                Dim dttable As DataTable = Database.GetData(sqlStr)


                If dttable.Rows.Count > 0 Then
                    For i = 0 To dttable.Rows.Count - 1
                        .Rows.Add(1)
                        .Item(0, i).Value = (i + 1).ToString()
                        .Item(1, i).Value = dttable.Rows(i)("CODE_STOCK_PROD_ONHOLD")
                        .Item(2, i).Value = dttable.Rows(i)("PROCESS_ONHOLD")
                        .Item(3, i).Value = dttable.Rows(i)("LOT_NO")
                        .Item(4, i).Value = dttable.Rows(i)("PART_NUMBER")
                        .Item(5, i).Value = dttable.Rows(i)("INV_CTRL_DATE")
                        .Item(6, i).Value = dttable.Rows(i)("TRACEABILITY")
                        .Item(7, i).Value = "Lot Code"
                        .Item(8, i).Value = "Lot No."
                        .Item(9, i).Value = dttable.Rows(i)("QTY")

                    Next
                End If
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            End With



        Catch ex As Exception
            MessageBox.Show("error load ShowToDGView")
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

                wipCode = "ONHOLD" + dt
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
        If txtBalanceBarcode.Text <> "" And txtBalanceQty.Text <> "" Then

            Try
                Dim i As Integer
                Dim statusSimpan As Integer = 1

                Call Database.koneksi_database()

                Dim idData As String = ""
                Dim strData(21) As String

                Dim dtTable As DataTable = Database.GetData("select * from STOCK_CARD where STATUS='Production Process' AND MATERIAL='" & txtBalanceMaterialPN.Text & "' AND SUB_SUB_PO='" & WIPGetSubsubPO() & "' AND LINE='" & cbLineNumber.Text & "' ORDER BY LOT_NO")
                If dtTable.Rows.Count > 0 Then
                    idData = dtTable.Rows(0)(0)
                    For i = 0 To dtTable.Columns.Count - 1
                        If (IsDBNull(dtTable.Rows(0)(i)) = True) Then
                            strData(i) = ""
                        Else
                            strData(i) = dtTable.Rows(0)(i)
                        End If
                    Next
                End If

                'proses update data
                'Dim sqlUpdate As String = "update STOCK_CARD set ACTUAL_QTY='" & txtBalanceQty.Text & "' where ID='" & idData & "'"
                'Dim cmdUpdate = New SqlCommand(sqlUpdate, Database.koneksi)
                'If cmdUpdate.ExecuteNonQuery() Then
                '    statusSimpan *= 1
                'Else
                '    statusSimpan *= 0
                'End If

                'proses simpan data
                strData(4) = "Return to Mini Store"
                'Dim sqlInsert As String = "INSERT INTO STOCK_CARD(MTS_NO,DEPARTEMENT,MATERIAL,STATUS,STANDARD_PACK,INV_CTRL_DATE,TRACEABILITY,BATCH_NO,LOT_NO,FINISH_GOODS_PN,PO,SUB_PO,SUB_SUB_PO,LINE,DATETIME_INSERT,SAVE,QRCODE,DATETIME_SAVE,QTY,ACTUAL_QTY) VALUES ('"
                'For i = 2 To 19
                '    sqlInsert = sqlInsert & "','" & strData(i)
                'Next
                'sqlInsert = sqlInsert & "','" & txtBalanceQty.Text & "')"

                Dim sqlInsert As String = "insert into STOCK_CARD(MTS_NO,DEPARTEMENT,MATERIAL,STATUS,STANDARD_PACK,INV_CTRL_DATE,TRACEABILITY,BATCH_NO,LOT_NO,FINISH_GOODS_PN,PO,SUB_PO,SUB_SUB_PO,LINE,DATETIME_INSERT,SAVE,QRCODE,DATETIME_SAVE,QTY,ACTUAL_QTY) select MTS_NO,DEPARTEMENT,MATERIAL,STATUS,STANDARD_PACK,INV_CTRL_DATE,TRACEABILITY,BATCH_NO,LOT_NO,FINISH_GOODS_PN,PO,SUB_PO,SUB_SUB_PO,LINE,DATETIME_INSERT,SAVE,QRCODE,DATETIME_SAVE,QTY,ACTUAL_QTY from STOCK_CARD where ID='90'"

                Dim cmdInsert = New SqlCommand(sqlInsert, Database.koneksi)
                If cmdInsert.ExecuteNonQuery() Then
                    statusSimpan *= 1
                Else
                    statusSimpan *= 0
                End If

                'For i = 0 To Part.Length - 2
                '    Dim dtsubsubpo As String = WIPGetSubsubPO()

                '    Dim dtList As String = WIPGetDataTraceability(Part(i), cbLineNumber.Text, dtsubsubpo)
                '    Dim arrdtList() As String
                '    arrdtList = dtList.Split(";")


                '    Dim sql As String = "INSERT INTO STOCK_PROD_ONHOLD(CODE_STOCK_PROD_ONHOLD,SUB_SUB_PO,FG_PN,PART_NUMBER,LOT_NO,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,QTY,DATETIME_INSERT,PO,PROCESS_ONHOLD,LINE) VALUES ('" &
                '                        ONHOLDGenerateCode() & "','" & dtsubsubpo & "','" & cbFGPN.Text & "','" & Part(i) & "','" & txtOnHoldTicketNo.Text & "','" & arrdtList(7) & "','" & arrdtList(8) & "','" & arrdtList(6) & "','" & WIPGetQtyperPart(Part(i), 1) & "','" & arrdtList(11) & "','" & cbPONumber.Text & "','" & cbOnHoldProcess.Text & "','" & cbLineNumber.Text & "')"
                '    Dim cmd = New SqlCommand(sql, Database.koneksi)
                '    If cmd.ExecuteNonQuery() Then
                '        statusSimpan *= 1
                '    Else
                '        statusSimpan *= 0
                '    End If
                'Next

                If statusSimpan > 0 Then
                    MessageBox.Show("Success save data!!!")
                    'LoaddgOnHold("")
                End If
            Catch ex As Exception
                MessageBox.Show("Error Insert : " & ex.Message)
            End Try


        End If
    End Sub

    Private Sub btnBalanceEdit_Click(sender As Object, e As EventArgs) Handles btnBalanceEdit.Click
        BalanceParsingMaterialPN("MX2D1P1710813000Q000000000270S00202203012213Q0002BWL2041112212D202204114L               ChinaMLX001")
    End Sub

    Private Sub txtBalanceBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBalanceBarcode.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtBalanceMaterialPN.Text = BalanceParsingMaterialPN(txtBalanceBarcode.Text)
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
    '===================================== END BALANCE FUNCTION
End Class