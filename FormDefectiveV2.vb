Imports System.CodeDom
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Reflection
Imports System.Reflection.Emit
Imports System.Runtime.Remoting
Imports System.Security.Cryptography
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.VisualBasic.Logging

Public Class FormDefectiveV2
    Public Shared menu As String = "Production Result"

    'Dim dept As String = "zQSFP"
    Dim dept As String = globVar.department
    Dim idLine As New List(Of Integer)
    Dim materialList As New List(Of String)
    Dim idBalanceMaterial As String
    Dim FGkah As Boolean

    Private Sub txtSubSubPODefective_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtSubSubPODefective.PreviewKeyDown
        If (e.KeyData = Keys.Enter) Then
            If globVar.view > 0 Then
                Try

                    If txtSubSubPODefective.Text.Contains(";") Then
                        Dim SplitSubSubPO() As String = txtSubSubPODefective.Text.Split(";"c)
                        txtSubSubPODefective.Text = SplitSubSubPO(0)
                    End If

                    If InStr(txtSubSubPODefective.Text, ";") Then
                        Dim sSubSubPO As String() = txtSubSubPODefective.Text.Split(";")
                        txtSubSubPODefective.Text = sSubSubPO(0)
                    End If

                    Call Database.koneksi_database()
                    Dim dtSubPO As DataTable = Database.GetData("select sp.line,mp.po,mp.fg_pn,mfg.spq,mfg.description,mfg.[level],sp.status from SUB_SUB_PO sp,main_po mp,master_finish_goods mfg where mp.id=sp.main_po and mfg.fg_part_number=mp.fg_pn and sp.sub_sub_po='" & txtSubSubPODefective.Text & "'")

                    If dtSubPO.Rows.Count > 0 Then

                        GroupBox2.Enabled = True

                        cbLineNumber.Text = dtSubPO.Rows(0).Item("line")

                        cbPONumber.Text = dtSubPO.Rows(0).Item("PO")

                        cbFGPN.Text = dtSubPO.Rows(0).Item("FG_PN")

                        txtDescDefective.Text = dtSubPO.Rows(0).Item("DESCRIPTION")

                        txtSPQ.Text = dtSubPO.Rows(0).Item("SPQ")

                        txtStatusSubSubPO.Text = dtSubPO.Rows(0).Item("status")

                        txtBalanceBarcode.Clear()

                        btnBalanceAdd.Enabled = False

                        btnRejectSave.Enabled = False

                        btnWIPAdd.Enabled = False

                        btnOnHoldSave.Enabled = False

                        If cbFGPN.Text <> "" Then
                            loadCBDefPartProcess(cbFGPN.Text)
                        End If

                        enableStatusInputInput(True)
                        LoaddgWIP("")
                        LoaddgOnHold("")
                        loaddgBalance("")
                        loaddgReject("")

                        PrintDefect.Enabled = True

                        If dtSubPO.Rows(0).Item("level") = "FG" Then
                            rbFG.Checked = True
                            rbSA.Checked = False
                            rbSA.Enabled = False
                            btnPrintSA.Enabled = False
                            CheckBox5.Enabled = False
                            FGkah = True
                        Else
                            rbSA.Checked = True
                            rbFG.Checked = False
                            rbFG.Enabled = False
                            btnPrintSA.Enabled = True
                            CheckBox5.Enabled = True
                            FGkah = False
                        End If

                        btnListPrintOnHold.Enabled = True
                        btnListPrintWIP.Enabled = True
                        btnListPrintReturn.Enabled = True
                        btnListPrintOthers.Enabled = True

                        txtRejectBarcode.Select()

                        TableLayoutPanel14.Enabled = False
                        TableLayoutPanel7.Enabled = False

                        DataGridView1.Rows.Clear()
                        DataGridView1.Columns.Clear()
                        DataGridView1.DataSource = Nothing

                        DataGridView3.Rows.Clear()
                        DataGridView3.Columns.Clear()
                        DataGridView3.DataSource = Nothing
                    End If
                Catch ex As Exception
                    RJMessageBox.Show("Error Load PO_NO", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

    Sub loadCBDefPartProcess(str As String)
        If str <> "System.Data.DataRowView" Then
            Try
                Call Database.koneksi_database()
                'Dim dtProcess As DataTable = Database.GetData("select distinct MASTER_PROCESS from MASTER_PROCESS_FLOW where MASTER_FINISH_GOODS_PN='" & str & "' ORDER BY ID")
                Dim dtProcess As DataTable = Database.GetData("select * from MASTER_PROCESS_FLOW where MASTER_FINISH_GOODS_PN='" & str & "' and master_process is not null ORDER BY ID")

                'cbDefProcess1.Items.Clear()
                'cbDefProcess2.Items.Clear()
                cbWIPProcess.Items.Clear()
                cbOnHoldProcess.Items.Clear()
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
                'rjMessageBox.Show("Error Load Process flow")
                RJMessageBox.Show("Error Load Process flow", "", MessageBoxButtons.OK,
                                      MessageBoxIcon.Error)
            End Try
        End If
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
        globVar.PingVersion()

        loadCBLine()

        ReadonlyFormSA(True)
        ReadonlyFormFG(True)

        txtSubSubPODefective.Select()

        GroupBox2.Enabled = False

        btnListPrintOnHold.Enabled = False
        btnListPrintWIP.Enabled = False
        btnListPrintReturn.Enabled = False
        btnListPrintOthers.Enabled = False

        txtRejectQty.Enabled = False
        txtBalanceQty.Enabled = False
    End Sub

    Sub enableStatusInputInput(statusEnable As Boolean)
        txtBalanceBarcode.Enabled = statusEnable

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
                    idLine.Add(dtLine.Rows(i)(0))
                    cbLineNumber.Items.Add(dtLine.Rows(i)(1))
                Next
            End If

        Catch ex As Exception
            RJMessageBox.Show("Error Load Line", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ''''''''''''''''''''''''''''''''''''''' WIP FUNCTION

    Private Sub cbWIPProcess_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbWIPProcess.SelectedIndexChanged
        LoaddgWIP(cbWIPProcess.Text)
        txtWIPTicketNo.Select()
    End Sub

    Private Sub btnWIPAdd_Click(sender As Object, e As EventArgs) Handles btnWIPAdd.Click

        Dim spasiFlowTicket = txtWIPTicketNo.Text.Replace(ChrW(160), " ")
        Dim sFlowTicket = spasiFlowTicket.Split(";")
        Dim sFlowTicketSplitOf = sFlowTicket(5).Split(" of ")
        Dim showMessageBox As Boolean = True
        Dim codeWIP As String = ""

        If globVar.add = 0 Then
            RJMessageBox.Show("Cannot access this menu")
            Exit Sub
        End If

        If cbWIPProcess.Text = "" Or txtWIPTicketNo.Text = "" Or txtWIPQuantity.Text = "" Then
            RJMessageBox.Show("Please fill the Process / Flow Ticket / Qty")
            Exit Sub
        End If

        Dim queryMPF As String = "select * from master_process_flow where master_finish_goods_pn='" & cbFGPN.Text & "' and master_process='" & cbWIPProcess.Text & "'"
        Dim dtMPF As DataTable = Database.GetData(queryMPF)

        If dtMPF.Rows.Count = 0 Then
            RJMessageBox.Show("Data Process Flow still Empty")
            Exit Sub
        End If

        Dim queryCode As String = "SELECT ISNULL(
                                        (SELECT TOP 1 CODE_STOCK_PROD_WIP 
                                         FROM stock_prod_wip 
                                         ORDER BY CAST(REPLACE(CODE_STOCK_PROD_WIP,'WIP','') AS INT) DESC),
                                        'OH0') CODE_STOCK_PROD_WIP
                                    "
        Dim dtCode As DataTable = Database.GetData(queryCode)
        Dim match As Match = Regex.Match(dtCode.Rows(0).Item("CODE_STOCK_PROD_WIP").ToString(), "^([A-Z]+)(\d+)$")
        If match.Success Then
            Dim prefix As String = match.Groups(1).Value
            Dim number As Integer = Integer.Parse(match.Groups(2).Value)
            Dim nextNumber As Integer = number + 1
            codeWIP = prefix & nextNumber.ToString()
        End If

        Dim separatedMaterial As String() = dtMPF.Rows(0).Item("material_usage").Split(";"c)

        For Each material As String In separatedMaterial 'Looping material yang dimakan

            Dim TotalQtyCheck As Integer

            If Not String.IsNullOrEmpty(material) Then

                Dim queryMUFG As String = "select * from MATERIAL_USAGE_FINISH_GOODS where FG_PART_NUMBER='" & cbFGPN.Text & "' and component='" & material & "'"
                Dim dtMUFG As DataTable = Database.GetData(queryMUFG)

                If dtMUFG.Rows.Count = 0 Then

                    RJMessageBox.Show("Data Material Usage Finish Goods still Empty")
                    Exit Sub

                End If

                TotalQtyCheck = dtMUFG.Rows(0).Item("usage") * txtWIPQuantity.Text

                Dim querySelectSC As String = "select 
                                                sum(actual_qty) total
                                            from 
                                                stock_card 
                                            where 
                                                status = 'Production Process' 
                                                AND department ='" & globVar.department & "'
                                                AND material = '" & material & "' 
                                                AND sub_sub_po ='" & txtSubSubPODefective.Text & "'"
                Dim dtSelectSC As DataTable = Database.GetData(querySelectSC)

                If dtSelectSC.Rows(0).Item("total") < TotalQtyCheck Then

                    RJMessageBox.Show("Qty this material " & material & " is not enough ")
                    Exit Sub

                End If

            End If

        Next

        For Each material As String In separatedMaterial 'Looping material yang dimakan

            Dim TotalQtySave As Integer

            If Not String.IsNullOrEmpty(material) Then

                Dim queryMUFG As String = "select * from MATERIAL_USAGE_FINISH_GOODS where FG_PART_NUMBER='" & cbFGPN.Text & "' and component='" & material & "'"
                Dim dtMUFG As DataTable = Database.GetData(queryMUFG)

                If dtMUFG.Rows.Count = 0 Then

                    RJMessageBox.Show("Data Material Usage Finish Goods still Empty")
                    Exit Sub

                End If

                Dim querySelectSC As String = "select 
                                                    * 
                                                from 
                                                    stock_card 
                                                where 
                                                    status = 'Production Process' 
                                                    AND department ='" & globVar.department & "'
                                                    AND material = '" & material & "' 
                                                    AND sub_sub_po ='" & txtSubSubPODefective.Text & "' 
                                                    and actual_qty > 0
                                                order by id"
                Dim dtSelectSC As DataTable = Database.GetData(querySelectSC)

                TotalQtySave = dtMUFG.Rows(0).Item("usage") * txtWIPQuantity.Text

                For i = 0 To dtSelectSC.Rows.Count - 1 'looping material yang ada di stock card 

                    Dim querySelectResult As String = "select 
                                                            isnull(sum(qty),0) totalResult
                                                        from 
                                                            stock_card 
                                                        where 
                                                            status = 'Production Result' 
                                                            AND department ='" & globVar.department & "'
                                                            AND material = '" & material & "' 
                                                            AND sub_sub_po ='" & txtSubSubPODefective.Text & "'
                                                            and flow_ticket = '" & sFlowTicket(5) & "'"

                    Dim dtSelectResult As DataTable = Database.GetData(querySelectResult)

                    If dtSelectResult.Rows(0).Item("totalResult") <> TotalQtySave Then

                        Dim totalPenguranganYangAda = TotalQtySave - dtSelectResult.Rows(0).Item("totalResult")

                        Dim totalPenguranganSCProcess = dtSelectSC.Rows(i).Item("actual_qty") - totalPenguranganYangAda

                        If dtSelectSC.Rows(i).Item("actual_qty") > totalPenguranganYangAda Then

                            Dim sqlInsertWIP As String

                            'insert WIP
                            If IsDBNull(dtSelectSC.Rows(i).Item("qrcode_new")) = False Then

                                sqlInsertWIP = "INSERT INTO stock_prod_wip (code_stock_prod_wip, po, sub_sub_po, FG_PN ,FLOW_TICKET_NO,DEPARTMENT,part_number, LOT_NO, qty,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,LINE,PROCESS,PENGALI,qrcode,INSERT_WHO)
                                                VALUES ('" & codeWIP & "','" & cbPONumber.Text & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & sFlowTicket(5) & "','" & globVar.department & "',
                                                '" & material & "','" & dtSelectSC.Rows(i).Item("lot_no") & "'," & TotalQtySave & ",'" & dtSelectSC.Rows(i).Item("traceability") & "','" & dtSelectSC.Rows(i).Item("inv_ctrl_date") & "','" & dtSelectSC.Rows(i).Item("batch_no") & "','" & dtSelectSC.Rows(i).Item("line") & "','" & cbWIPProcess.Text & "'," & txtWIPQuantity.Text & ",'" & dtSelectSC.Rows(i).Item("qrcode_new") & "','" & globVar.username & "')"
                            Else

                                sqlInsertWIP = "INSERT INTO stock_prod_wip (code_stock_prod_wip, po, sub_sub_po, FG_PN ,FLOW_TICKET_NO,DEPARTMENT,part_number, LOT_NO, qty,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,LINE,PROCESS,PENGALI,INSERT_WHO)
                                                VALUES ('" & codeWIP & "','" & cbPONumber.Text & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & sFlowTicket(5) & "','" & globVar.department & "',
                                                '" & material & "','" & dtSelectSC.Rows(i).Item("lot_no") & "'," & TotalQtySave & ",'" & dtSelectSC.Rows(i).Item("traceability") & "','" & dtSelectSC.Rows(i).Item("inv_ctrl_date") & "','" & dtSelectSC.Rows(i).Item("batch_no") & "','" & dtSelectSC.Rows(i).Item("line") & "','" & cbWIPProcess.Text & "'," & txtWIPQuantity.Text & ",'" & globVar.username & "')"

                            End If


                            Dim cmdInsertWIP = New SqlCommand(sqlInsertWIP, Database.koneksi)
                            If cmdInsertWIP.ExecuteNonQuery() Then

                                Dim queryUpdateSCProductionProcess As String = "update stock_card set actual_qty=" & totalPenguranganSCProcess & " where id = " & dtSelectSC.Rows(i).Item("id")
                                Dim dtUpdateSCProductionProcess = New SqlCommand(queryUpdateSCProductionProcess, Database.koneksi)
                                dtUpdateSCProductionProcess.ExecuteNonQuery()

                                If showMessageBox Then

                                    RJMessageBox.Show("Success Save WIP!!!")

                                    showMessageBox = False

                                End If

                                Exit For

                            End If

                        Else

                            Dim sqlInsertWIP As String

                            'insert WIP
                            If IsDBNull(dtSelectSC.Rows(i).Item("qrcode_new")) = False Then

                                sqlInsertWIP = "INSERT INTO stock_prod_wip (code_stock_prod_wip, po, sub_sub_po, FG_PN ,FLOW_TICKET_NO,DEPARTMENT,part_number, LOT_NO, qty,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,LINE,PROCESS,PENGALI,qrcode,INSERT_WHO)
                                                VALUES ('" & codeWIP & "','" & cbPONumber.Text & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & sFlowTicket(5) & "','" & globVar.department & "',
                                                '" & material & "','" & dtSelectSC.Rows(i).Item("lot_no") & "'," & TotalQtySave & ",'" & dtSelectSC.Rows(i).Item("traceability") & "','" & dtSelectSC.Rows(i).Item("inv_ctrl_date") & "','" & dtSelectSC.Rows(i).Item("batch_no") & "','" & dtSelectSC.Rows(i).Item("line") & "','" & cbWIPProcess.Text & "'," & txtWIPQuantity.Text & ",'" & dtSelectSC.Rows(i).Item("qrcode_new") & "','" & globVar.username & "')"

                            Else

                                sqlInsertWIP = "INSERT INTO stock_prod_wip (code_stock_prod_wip, po, sub_sub_po, FG_PN ,FLOW_TICKET_NO,DEPARTMENT,part_number, LOT_NO, qty,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,LINE,PROCESS,PENGALI,INSERT_WHO)
                                                VALUES ('" & codeWIP & "','" & cbPONumber.Text & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & sFlowTicket(5) & "','" & globVar.department & "',
                                                '" & material & "','" & dtSelectSC.Rows(i).Item("lot_no") & "'," & TotalQtySave & ",'" & dtSelectSC.Rows(i).Item("traceability") & "','" & dtSelectSC.Rows(i).Item("inv_ctrl_date") & "','" & dtSelectSC.Rows(i).Item("batch_no") & "','" & dtSelectSC.Rows(i).Item("line") & "','" & cbWIPProcess.Text & "'," & txtWIPQuantity.Text & ",'" & globVar.username & "')"

                            End If


                            Dim cmdInsertWIP = New SqlCommand(sqlInsertWIP, Database.koneksi)
                            If cmdInsertWIP.ExecuteNonQuery() Then

                                Dim queryUpdateSCProductionProcess As String = "update stock_card set actual_qty=0 where id = " & dtSelectSC.Rows(i).Item("id")
                                Dim dtUpdateSCProductionProcess = New SqlCommand(queryUpdateSCProductionProcess, Database.koneksi)
                                dtUpdateSCProductionProcess.ExecuteNonQuery()

                                If showMessageBox Then

                                    RJMessageBox.Show("Success Save WIP!!!")

                                    showMessageBox = False

                                End If

                            End If

                        End If

                    End If

                Next

            End If

        Next

        If showMessageBox = False Then

            LoaddgWIP("")
            cbWIPProcess.SelectedIndex = -1
            txtWIPTicketNo.Clear()
            txtWIPQuantity.Clear()

        End If

    End Sub

    Private Sub btnWIPDelete_Click(sender As Object, e As EventArgs) Handles btnWIPDelete.Click
        If globVar.delete > 0 Then
            Try
                If cbWIPProcess.Text <> "" And txtWIPTicketNo.Text <> "" Then

                    If txtStatusSubSubPO.Text = "Closed" Then
                        RJMessageBox.Show("Sorry This Sub Sub PO status is closed. Cannot use this menu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    Dim Part As String() = Nothing
                    Dim sFlow_Ticket As String() = Nothing
                    Dim i As Integer
                    Dim statusSimpan As Integer = 1
                    Dim addQty As Double = 0

                    Part = materialList(cbWIPProcess.SelectedIndex).Split(";")
                    Dim spasiFlowTicket = txtWIPTicketNo.Text.Replace(ChrW(160), " ")
                    sFlow_Ticket = spasiFlowTicket.Split(";")

                    'diulang sebanyak part number yg ada
                    Call Database.koneksi_database()

                    If WIPcheckExistingData(txtSubSubPODefective.Text, dept) = True Then
                        Dim result = RJMessageBox.Show("Are you sure you want to delete this item?.", "Deleting !", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                        If result = DialogResult.Yes Then
                            For i = 0 To Part.Length - 2

                                Dim queryCheckWIP As String = "select * from STOCK_PROD_WIP where part_number='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FG_PN='" & cbFGPN.Text & "' and process='" & cbWIPProcess.Text & "' and flow_ticket_no='" & sFlow_Ticket(5) & "'"
                                Dim dtCheckWIP As DataTable = Database.GetData(queryCheckWIP)

                                Dim queryCheckAdd As String = "select * from stock_card where status='Production Request' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckWIP.Rows(0).Item("lot_no") & "' AND [LEVEL]='Fresh'"
                                Dim dtCheckAdd As DataTable = Database.GetData(queryCheckAdd)

                                addQty = dtCheckAdd.Rows(0).Item("sum_qty") + WIPGetQtyDB(Part(i), 0)

                                Dim queryUpdateAdd As String = "update stock_card set actual_qty=" & addQty.ToString.Replace(",", ".") & ",sum_qty=" & addQty.ToString.Replace(",", ".") & " where status='Production Request' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckWIP.Rows(0).Item("lot_no") & "' AND [LEVEL]='Fresh'"
                                Dim dtUpdateAdd = New SqlCommand(queryUpdateAdd, Database.koneksi)
                                If dtUpdateAdd.ExecuteNonQuery() Then

                                    Dim queryDeleteStockCard As String = "delete from stock_card where status='Production Process' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' AND [LEVEL]='Fresh'"
                                    Dim dtDeleteStockCard = New SqlCommand(queryDeleteStockCard, Database.koneksi)
                                    If dtDeleteStockCard.ExecuteNonQuery() Then

                                        Dim queryDistintLotNoinTemp As String = "select * from stock_card where status='Production Request' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                                        Dim dtDistintLotNoinTemp As DataTable = Database.GetData(queryDistintLotNoinTemp)
                                        If dtDistintLotNoinTemp.Rows.Count > 0 Then
                                            For iDistint = 0 To dtDistintLotNoinTemp.Rows.Count - 1
                                                If dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") = dtCheckWIP.Rows(0).Item("lot_no") Then
                                                    Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & addQty.ToString.Replace(",", ".") & ",@material='" & Part(i) & "',@lot_material='" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") & "'"
                                                    Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                                                Else
                                                    Dim queryUpdateactualQty As String = "update stock_card set actual_qty=sum_qty where status='Production Request' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") & "' AND [LEVEL]='Fresh'"
                                                    Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                                                    If dtUpdateactualQty.ExecuteNonQuery() Then
                                                        Dim sqlQtyLot = "select * from stock_card where status='Production Request' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") & "' AND [LEVEL]='Fresh'"
                                                        Dim dtQtyLot As DataTable = Database.GetData(sqlQtyLot)
                                                        Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtQtyLot.Rows(0).Item("sum_qty").ToString.Replace(",", ".") & ",@material='" & Part(i) & "',@lot_material='" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") & "'"
                                                        Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If

                                    Dim queryDelete As String = "delete from STOCK_PROD_WIP where DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FG_PN='" & cbFGPN.Text & "' and process='" & cbWIPProcess.Text & "' and flow_ticket_no='" & sFlow_Ticket(5) & "' and part_number='" & Part(i) & "' and lot_no='" & dtCheckWIP.Rows(0).Item("lot_no") & "'"
                                    Dim dtqueryDelete = New SqlCommand(queryDelete, Database.koneksi)
                                    If dtqueryDelete.ExecuteNonQuery() Then
                                        statusSimpan *= 1
                                    Else
                                        statusSimpan *= 0
                                    End If
                                End If

                                'Dim queryUpdateStockCardProdReq As String = "update summary_fg set wip_out=0 where sub_sub_po='" & txtSubSubPODefective.Text & "' and material='" & Part(i) & "'"
                                'Dim dtUpdateStockCardProdReq = New SqlCommand(queryUpdateStockCardProdReq, Database.koneksi)
                                'dtUpdateStockCardProdReq.ExecuteNonQuery()
                            Next

                            If statusSimpan > 0 Then
                                RJMessageBox.Show("Success Delete data!!!")
                                LoaddgWIP("")
                                cbWIPProcess.SelectedIndex = -1
                                txtWIPTicketNo.Clear()
                                txtWIPQuantity.Clear()
                            Else
                                RJMessageBox.Show("Fail to Delete data!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                LoaddgWIP("")
                                cbWIPProcess.SelectedIndex = -1
                                txtWIPTicketNo.Clear()
                                txtWIPQuantity.Clear()
                            End If
                        End If
                    Else
                        RJMessageBox.Show("Sorry the data not exist in DB. Cannot be deleted.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End If
            Catch ex As Exception
                RJMessageBox.Show("Error Delete WIP : " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
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

    Function ONHOLDcheckExistingData(subsubpo As String, department As String) As Boolean
        Dim data As Boolean = False

        Try
            Call Database.koneksi_database()
            Dim sql As String = ""
            Dim dttable As DataTable = Database.GetData("select distinct ID from STOCK_PROD_ONHOLD where SUB_SUB_PO='" & subsubpo & "' AND FG_PN='" & cbFGPN.Text & "' AND PROCESS='" & cbOnHoldProcess.Text & "' AND DEPARTMENT='" & department & "'")

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

        dgWIP.DataSource = Nothing
        dgWIP.Rows.Clear()
        dgWIP.Columns.Clear()

        Dim i As Integer = 0

        Try

            With dgWIP
                Dim sqlStr As String = ""

                If proses = "" Then
                    sqlStr = "select [CODE_STOCK_PROD_wip] [Code], [PROCESS] [Process], [PART_NUMBER] [Material], [LOT_NO] [Lot No], [TRACEABILITY] [Trace], [INV_CTRL_DATE] [ICD], [BATCH_NO] [Batch No], [FLOW_TICKET_NO] [Ticket], [PENGALI] [Qty], [QTY] [Sum Qty], oh.[DATETIME_INSERT] [Time Save], oh.[INSERT_WHO] [Scan By] from STOCK_PROD_wip OH, sub_sub_po sp where oh.sub_sub_po=sp.sub_sub_po and oh.line='" & cbLineNumber.Text & "' and sp.sub_sub_po='" & txtSubSubPODefective.Text & "' ORDER BY oh.CODE_STOCK_PROD_wip"
                Else
                    sqlStr = "select [CODE_STOCK_PROD_wip] [Code], [PROCESS] [Process], [PART_NUMBER] [Material], [LOT_NO] [Lot No], [TRACEABILITY] [Trace], [INV_CTRL_DATE] [ICD], [BATCH_NO] [Batch No], [FLOW_TICKET_NO] [Ticket], [PENGALI] [Qty], [QTY] [Sum Qty], oh.[DATETIME_INSERT] [Time Save], oh.[INSERT_WHO] [Scan By] from STOCK_PROD_wip oh, sub_sub_po sp where oh.sub_sub_po=sp.sub_sub_po and oh.PROCESS='" & proses & "' and oh.line='" & cbLineNumber.Text & "' and sp.sub_sub_po='" & txtSubSubPODefective.Text & "' ORDER BY oh.CODE_STOCK_PROD_wip"
                End If

                Dim dttable As DataTable = Database.GetData(sqlStr)

                If dttable.Rows.Count > 0 Then
                    .DataSource = dttable

                    .DefaultCellStyle.Font = New Font("Tahoma", 14)

                    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(0).Width = Int(.Width * 0.03)
                    .Columns(1).Width = Int(.Width * 0.12)
                    .Columns(2).Width = Int(.Width * 0.05)
                    .Columns(3).Width = Int(.Width * 0.05)
                    .Columns(4).Width = Int(.Width * 0.05)
                    .Columns(5).Width = Int(.Width * 0.05)
                    .Columns(6).Width = Int(.Width * 0.05)
                    .Columns(7).Width = Int(.Width * 0.05)
                    .Columns(8).Width = Int(.Width * 0.05)
                    .Columns(9).Width = Int(.Width * 0.05)
                    .Columns(10).Width = Int(.Width * 0.1)
                    .Columns(11).Width = Int(.Width * 0.08)

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

                    Dim deleteProductionRequest As DataGridViewButtonColumn = New DataGridViewButtonColumn
                    deleteProductionRequest.Name = "delete"
                    deleteProductionRequest.HeaderText = "Delete"
                    deleteProductionRequest.Text = "Delete"
                    deleteProductionRequest.UseColumnTextForButtonValue = True
                    .Columns.Insert(12, deleteProductionRequest)
                Else
                    .DataSource = Nothing
                End If
            End With

            For j As Integer = 0 To dgWIP.RowCount - 1
                If dgWIP.Rows(j).Index Mod 2 = 0 Then
                    dgWIP.Rows(j).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    dgWIP.Rows(j).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next
        Catch ex As Exception
            RJMessageBox.Show("Error Load Data WIP " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            RJMessageBox.Show("Error Insert WIP : " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return wipCode
    End Function

    Function RejectGenerateCode() As String
        Dim rCode As String = ""

        Try
            Call Database.koneksi_database()
            Dim queryCheckReject As String = "select top 1 code_out_prod_reject from out_prod_reject where sub_sub_po='" & txtSubSubPODefective.Text & "' ORDER BY ID DESC"
            Dim dtReject As DataTable = Database.GetData(queryCheckReject)
            If dtReject.Rows.Count > 0 Then
                rCode = dtReject.Rows(0).Item("code_out_prod_reject").ToString()
            Else
                Dim queryCheckRejectLast As String = "select top 1 code_out_prod_reject from out_prod_reject ORDER BY cast(replace(code_out_prod_reject,'R','') as int) desc"
                Dim dtRejectLast As DataTable = Database.GetData(queryCheckRejectLast)
                Dim match As Match = Regex.Match(dtRejectLast.Rows(0).Item("code_out_prod_reject").ToString(), "^([A-Z]+)(\d+)$")
                If match.Success Then
                    Dim prefix As String = match.Groups(1).Value
                    Dim number As Integer = Integer.Parse(match.Groups(2).Value)
                    Dim nextNumber As Integer = number + 1
                    rCode = prefix & nextNumber.ToString()
                End If
            End If
        Catch ex As Exception
            RJMessageBox.Show("Error Generate Code Reject : " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return rCode
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
            dtCode = Database.GetData("select SUB_SUB_PO from SUB_SUB_PO where MAIN_PO='" & idPO & "' AND LINE='" & cbLineNumber.Text & "' ORDER BY ID")
            If dtCode.Rows.Count > 0 Then
                SubsubPO = dtCode.Rows(0)(0)
            End If

        Catch ex As Exception
            RJMessageBox.Show("Error Insert" & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            RJMessageBox.Show("Error Insert" & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Function WIPGetQtyDB(strComponent As String, idx As Integer) As Double
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
            RJMessageBox.Show("Error Insert" & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Dim dt As Double
        If idx = 0 Then
            Double.TryParse(TextBox11.Text, dt)
        ElseIf idx = 1 Then
            Double.TryParse(TextBox12.Text, dt)
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
            RJMessageBox.Show("Error Insert" & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dataTrace
    End Function



    '''''''''''''''''''''''''''''''''''''' END WIP FUNCTION


    '************************************* ON HOLD FUNCTION
    Private Sub btnOnHoldAdd_Click(sender As Object, e As EventArgs) Handles btnOnHoldSave.Click

        Dim spasiFlowTicket = txtOnHoldTicketNo.Text.Replace(ChrW(160), " ")
        Dim sFlowTicket = spasiFlowTicket.Split(";")
        Dim sFlowTicketSplitOf = sFlowTicket(5).Split(" of ")
        Dim showMessageBox As Boolean = True
        Dim codeOH As String = ""

        If globVar.add = 0 Then
            RJMessageBox.Show("Cannot access this menu")
            Exit Sub
        End If

        If cbOnHoldProcess.Text = "" Or txtOnHoldTicketNo.Text = "" Or txtOnHoldQty.Text = "" Then
            RJMessageBox.Show("Please fill the Process / Flow Ticket / Qty")
            Exit Sub
        End If

        Dim queryMPF As String = "select * from master_process_flow where master_finish_goods_pn='" & cbFGPN.Text & "' and master_process='" & cbOnHoldProcess.Text & "'"
        Dim dtMPF As DataTable = Database.GetData(queryMPF)

        If dtMPF.Rows.Count = 0 Then
            RJMessageBox.Show("Data Process Flow still Empty")
            Exit Sub
        End If

        Dim queryCode As String = "SELECT ISNULL(
                                        (SELECT TOP 1 CODE_STOCK_PROD_ONHOLD 
                                         FROM stock_prod_onhold 
                                         ORDER BY CAST(REPLACE(CODE_STOCK_PROD_ONHOLD,'OH','') AS INT) DESC),
                                        'OH0') CODE_STOCK_PROD_ONHOLD
                                    "
        Dim dtCode As DataTable = Database.GetData(queryCode)
        Dim match As Match = Regex.Match(dtCode.Rows(0).Item("CODE_STOCK_PROD_ONHOLD").ToString(), "^([A-Z]+)(\d+)$")
        If match.Success Then
            Dim prefix As String = match.Groups(1).Value
            Dim number As Integer = Integer.Parse(match.Groups(2).Value)
            Dim nextNumber As Integer = number + 1
            codeOH = prefix & nextNumber.ToString()
        End If

        Dim separatedMaterial As String() = dtMPF.Rows(0).Item("material_usage").Split(";"c)

        For Each material As String In separatedMaterial 'Looping material yang dimakan

            Dim TotalQtyCheck As Integer

            If Not String.IsNullOrEmpty(material) Then

                Dim queryMUFG As String = "select * from MATERIAL_USAGE_FINISH_GOODS where FG_PART_NUMBER='" & cbFGPN.Text & "' and component='" & material & "'"
                Dim dtMUFG As DataTable = Database.GetData(queryMUFG)

                If dtMUFG.Rows.Count = 0 Then

                    RJMessageBox.Show("Data Material Usage Finish Goods still Empty")
                    Exit Sub

                End If

                TotalQtyCheck = dtMUFG.Rows(0).Item("usage") * txtOnHoldQty.Text

                Dim querySelectSC As String = "select 
                                                sum(actual_qty) total
                                            from 
                                                stock_card 
                                            where 
                                                status = 'Production Process' 
                                                AND department ='" & globVar.department & "'
                                                AND material = '" & material & "' 
                                                AND sub_sub_po ='" & txtSubSubPODefective.Text & "'"
                Dim dtSelectSC As DataTable = Database.GetData(querySelectSC)

                If dtSelectSC.Rows(0).Item("total") < TotalQtyCheck Then

                    RJMessageBox.Show("Qty this material " & material & " is not enough ")
                    Exit Sub

                End If

            End If

        Next

        For Each material As String In separatedMaterial 'Looping material yang dimakan

            Dim TotalQtySave As Integer

            If Not String.IsNullOrEmpty(material) Then

                Dim queryMUFG As String = "select * from MATERIAL_USAGE_FINISH_GOODS where FG_PART_NUMBER='" & cbFGPN.Text & "' and component='" & material & "'"
                Dim dtMUFG As DataTable = Database.GetData(queryMUFG)

                If dtMUFG.Rows.Count = 0 Then

                    RJMessageBox.Show("Data Material Usage Finish Goods still Empty")
                    Exit Sub

                End If

                Dim querySelectSC As String = "select 
                                                    * 
                                                from 
                                                    stock_card 
                                                where 
                                                    status = 'Production Process' 
                                                    AND department ='" & globVar.department & "'
                                                    AND material = '" & material & "' 
                                                    AND sub_sub_po ='" & txtSubSubPODefective.Text & "' 
                                                    and actual_qty > 0
                                                order by id"
                Dim dtSelectSC As DataTable = Database.GetData(querySelectSC)

                TotalQtySave = dtMUFG.Rows(0).Item("usage") * txtOnHoldQty.Text

                For i = 0 To dtSelectSC.Rows.Count - 1 'looping material yang ada di stock card 

                    Dim querySelectResult As String = "select 
                                                            isnull(sum(qty),0) totalResult
                                                        from 
                                                            stock_card 
                                                        where 
                                                            status = 'Production Result' 
                                                            AND department ='" & globVar.department & "'
                                                            AND material = '" & material & "' 
                                                            AND sub_sub_po ='" & txtSubSubPODefective.Text & "'
                                                            and flow_ticket = '" & sFlowTicket(5) & "'"

                    Dim dtSelectResult As DataTable = Database.GetData(querySelectResult)

                    If dtSelectResult.Rows(0).Item("totalResult") <> TotalQtySave Then

                        Dim totalPenguranganYangAda = TotalQtySave - dtSelectResult.Rows(0).Item("totalResult")

                        Dim totalPenguranganSCProcess = dtSelectSC.Rows(i).Item("actual_qty") - totalPenguranganYangAda

                        If dtSelectSC.Rows(i).Item("actual_qty") > totalPenguranganYangAda Then

                            Dim sqlInsertOnHold As String

                            'insert on hold
                            If IsDBNull(dtSelectSC.Rows(i).Item("qrcode_new")) = False Then

                                sqlInsertOnHold = "INSERT INTO stock_prod_onhold (code_stock_prod_onhold, po, sub_sub_po, FG_PN ,FLOW_TICKET_NO,DEPARTMENT,part_number, LOT_NO, qty,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,LINE,PROCESS,PENGALI,qrcode,INSERT_WHO)
                                                VALUES ('" & codeOH & "','" & cbPONumber.Text & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & sFlowTicket(5) & "','" & globVar.department & "',
                                                '" & material & "','" & dtSelectSC.Rows(i).Item("lot_no") & "'," & TotalQtySave & ",'" & dtSelectSC.Rows(i).Item("traceability") & "','" & dtSelectSC.Rows(i).Item("inv_ctrl_date") & "','" & dtSelectSC.Rows(i).Item("batch_no") & "','" & dtSelectSC.Rows(i).Item("line") & "','" & cbOnHoldProcess.Text & "'," & txtOnHoldQty.Text & ",'" & dtSelectSC.Rows(i).Item("qrcode_new") & "','" & globVar.username & "')"
                            Else

                                sqlInsertOnHold = "INSERT INTO stock_prod_onhold (code_stock_prod_onhold, po, sub_sub_po, FG_PN ,FLOW_TICKET_NO,DEPARTMENT,part_number, LOT_NO, qty,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,LINE,PROCESS,PENGALI,INSERT_WHO)
                                                VALUES ('" & codeOH & "','" & cbPONumber.Text & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & sFlowTicket(5) & "','" & globVar.department & "',
                                                '" & material & "','" & dtSelectSC.Rows(i).Item("lot_no") & "'," & TotalQtySave & ",'" & dtSelectSC.Rows(i).Item("traceability") & "','" & dtSelectSC.Rows(i).Item("inv_ctrl_date") & "','" & dtSelectSC.Rows(i).Item("batch_no") & "','" & dtSelectSC.Rows(i).Item("line") & "','" & cbOnHoldProcess.Text & "'," & txtOnHoldQty.Text & ",'" & globVar.username & "')"

                            End If


                            Dim cmdInsertOnHold = New SqlCommand(sqlInsertOnHold, Database.koneksi)
                            If cmdInsertOnHold.ExecuteNonQuery() Then

                                Dim queryUpdateSCProductionProcess As String = "update stock_card set actual_qty=" & totalPenguranganSCProcess & " where id = " & dtSelectSC.Rows(i).Item("id")
                                Dim dtUpdateSCProductionProcess = New SqlCommand(queryUpdateSCProductionProcess, Database.koneksi)
                                dtUpdateSCProductionProcess.ExecuteNonQuery()

                                If showMessageBox Then

                                    RJMessageBox.Show("Success Save On Hold!!!")

                                    showMessageBox = False

                                End If

                                Exit For

                            End If

                        Else

                            Dim sqlInsertOnHold As String

                            'insert on hold
                            If IsDBNull(dtSelectSC.Rows(i).Item("qrcode_new")) = False Then

                                sqlInsertOnHold = "INSERT INTO stock_prod_onhold (code_stock_prod_sub_assy, po, sub_sub_po, FG_PN ,FLOW_TICKET_NO,DEPARTMENT,part_number, LOT_NO, qty,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,LINE,PROCESS,PENGALI,qrcode,INSERT_WHO)
                                                VALUES ('" & codeOH & "','" & cbPONumber.Text & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & sFlowTicket(5) & "','" & globVar.department & "',
                                                '" & material & "','" & dtSelectSC.Rows(i).Item("lot_no") & "'," & TotalQtySave & ",'" & dtSelectSC.Rows(i).Item("traceability") & "','" & dtSelectSC.Rows(i).Item("inv_ctrl_date") & "','" & dtSelectSC.Rows(i).Item("batch_no") & "','" & dtSelectSC.Rows(i).Item("line") & "','" & cbOnHoldProcess.Text & "'," & txtOnHoldQty.Text & ",'" & dtSelectSC.Rows(i).Item("qrcode_new") & "','" & globVar.username & "')"

                            Else

                                sqlInsertOnHold = "INSERT INTO stock_prod_onhold (code_stock_prod_sub_assy, po, sub_sub_po, FG_PN ,FLOW_TICKET_NO,DEPARTMENT,part_number, LOT_NO, qty,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,LINE,PROCESS,PENGALI,INSERT_WHO)
                                                VALUES ('" & codeOH & "','" & cbPONumber.Text & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & sFlowTicket(5) & "','" & globVar.department & "',
                                                '" & material & "','" & dtSelectSC.Rows(i).Item("lot_no") & "'," & TotalQtySave & ",'" & dtSelectSC.Rows(i).Item("traceability") & "','" & dtSelectSC.Rows(i).Item("inv_ctrl_date") & "','" & dtSelectSC.Rows(i).Item("batch_no") & "','" & dtSelectSC.Rows(i).Item("line") & "','" & cbOnHoldProcess.Text & "'," & txtOnHoldQty.Text & ",'" & globVar.username & "')"

                            End If


                            Dim cmdInsertOnHold = New SqlCommand(sqlInsertOnHold, Database.koneksi)
                            If cmdInsertOnHold.ExecuteNonQuery() Then

                                Dim queryUpdateSCProductionProcess As String = "update stock_card set actual_qty=0 where id = " & dtSelectSC.Rows(i).Item("id")
                                Dim dtUpdateSCProductionProcess = New SqlCommand(queryUpdateSCProductionProcess, Database.koneksi)
                                dtUpdateSCProductionProcess.ExecuteNonQuery()

                                If showMessageBox Then

                                    RJMessageBox.Show("Success Save On Hold!!!")

                                    showMessageBox = False

                                End If

                            End If

                        End If

                    End If

                Next

            End If

        Next

        If showMessageBox = False Then

            LoaddgOnHold("")
            cbOnHoldProcess.SelectedIndex = -1
            txtOnHoldTicketNo.Clear()
            txtOnHoldQty.Clear()

        End If

    End Sub

    Private Sub btnOnHoldDelete_Click(sender As Object, e As EventArgs) Handles btnOnHoldDelete.Click
        If globVar.delete > 0 Then
            If cbOnHoldProcess.Text <> "" And txtOnHoldTicketNo.Text <> "" Then
                Try
                    If txtStatusSubSubPO.Text = "Closed" Then
                        RJMessageBox.Show("Sorry This Sub Sub PO status is closed. Cannot use this menu.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    Dim Part As String() = Nothing
                    Dim sFlow_Ticket As String() = Nothing
                    Dim i As Integer
                    Dim statusSimpan As Integer = 1
                    Dim sumQty As Double = 0
                    Dim addQty As Double = 0

                    Part = materialList(cbOnHoldProcess.SelectedIndex).Split(";")
                    Dim spasiFlowTicket = txtOnHoldTicketNo.Text.Replace(ChrW(160), " ")
                    sFlow_Ticket = spasiFlowTicket.Split(";")

                    'diulang sebanyak part number yg ada
                    Call Database.koneksi_database()

                    If ONHOLDcheckExistingData(txtSubSubPODefective.Text, dept) = True Then
                        Dim result = RJMessageBox.Show("Are you sure to delete?.", "Deleting", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                        If result = DialogResult.Yes Then
                            For i = 0 To Part.Length - 2

                                Dim queryCheckOnhold As String = "select * from STOCK_PROD_ONHOLD where part_number='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FG_PN='" & cbFGPN.Text & "' and process='" & cbOnHoldProcess.Text & "' and flow_ticket_no='" & sFlow_Ticket(5) & "'"
                                Dim dtCheckOnhold As DataTable = Database.GetData(queryCheckOnhold)

                                For onholdDelete = 0 To dtCheckOnhold.Rows.Count - 1

                                    Dim queryCheckAdd As String = "select * from stock_card where status='Production Request' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckOnhold.Rows(onholdDelete).Item("lot_no") & "' AND [LEVEL]='Fresh'"
                                    Dim dtCheckAdd As DataTable = Database.GetData(queryCheckAdd)

                                    addQty = dtCheckAdd.Rows(0).Item("sum_qty") + dtCheckOnhold.Rows(onholdDelete).Item("qty")

                                    Dim queryUpdateAdd As String = "update stock_card set actual_qty=" & addQty.ToString.Replace(",", ".") & ",sum_qty=" & addQty.ToString.Replace(",", ".") & " where id=" & dtCheckAdd.Rows(0).Item("id")
                                    Dim dtUpdateAdd = New SqlCommand(queryUpdateAdd, Database.koneksi)

                                    If dtUpdateAdd.ExecuteNonQuery() Then

                                        Dim queryDeleteStockCard As String = "delete from stock_card where status='Production Process' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' AND [LEVEL]='Fresh' and flow_ticket='" & sFlow_Ticket(5) & "'"
                                        Dim dtDeleteStockCard = New SqlCommand(queryDeleteStockCard, Database.koneksi)

                                        If dtDeleteStockCard.ExecuteNonQuery() Then

                                            Dim queryCheckSumUsage As String = "select * from stock_card where status='Production Request' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' AND [LEVEL]='Fresh'"
                                            Dim dtCheckSumUsage As DataTable = Database.GetData(queryCheckSumUsage)

                                            If dtCheckSumUsage.Rows.Count > 0 Then

                                                For iDistint = 0 To dtCheckSumUsage.Rows.Count - 1

                                                    If dtCheckSumUsage.Rows(iDistint).Item("lot_no") = dtCheckOnhold.Rows(onholdDelete).Item("lot_no") Then

                                                        Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & addQty.ToString.Replace(",", ".") & ",@material='" & Part(i) & "',@lot_material='" & dtCheckSumUsage.Rows(iDistint).Item("lot_no") & "'"
                                                        Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)

                                                    Else

                                                        Dim queryUpdateactualQty As String = "update stock_card set actual_qty=sum_qty where status='Production Request' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckSumUsage.Rows(iDistint).Item("lot_no") & "' AND [LEVEL]='Fresh'"
                                                        Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                                                        If dtUpdateactualQty.ExecuteNonQuery() Then

                                                            Dim sqlQtyLot = "select * from stock_card where status='Production Request' and material='" & Part(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckSumUsage.Rows(iDistint).Item("lot_no") & "' AND [LEVEL]='Fresh'"
                                                            Dim dtQtyLot As DataTable = Database.GetData(sqlQtyLot)

                                                            Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtQtyLot.Rows(0).Item("sum_qty").ToString.Replace(",", ".") & ",@material='" & Part(i) & "',@lot_material='" & dtCheckSumUsage.Rows(iDistint).Item("lot_no") & "'"
                                                            Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)

                                                        End If


                                                    End If

                                                Next

                                            End If

                                        End If

                                        Dim queryDelete As String = "delete from STOCK_PROD_ONHOLD where DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FG_PN='" & cbFGPN.Text & "' and process='" & cbOnHoldProcess.Text & "' and flow_ticket_no='" & sFlow_Ticket(5) & "' and part_number='" & Part(i) & "' and lot_no='" & dtCheckOnhold.Rows(onholdDelete).Item("lot_no") & "'"
                                        Dim dtqueryDelete = New SqlCommand(queryDelete, Database.koneksi)
                                        If dtqueryDelete.ExecuteNonQuery() Then
                                            statusSimpan *= 1
                                        Else
                                            statusSimpan *= 0
                                        End If

                                    End If

                                Next

                                'Dim queryUpdateStockCardProdReq As String = "update summary_fg set onhold_out=0 where sub_sub_po='" & txtSubSubPODefective.Text & "' and material='" & Part(i) & "'"
                                'Dim dtUpdateStockCardProdReq = New SqlCommand(queryUpdateStockCardProdReq, Database.koneksi)
                                'dtUpdateStockCardProdReq.ExecuteNonQuery()
                            Next

                            If statusSimpan > 0 Then
                                RJMessageBox.Show("Success Delete data!!!")
                                LoaddgOnHold("")
                                cbOnHoldProcess.SelectedIndex = -1
                                txtOnHoldTicketNo.Clear()
                                txtOnHoldQty.Clear()
                            Else
                                RJMessageBox.Show("Fail Delete data!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                LoaddgOnHold("")
                                cbOnHoldProcess.SelectedIndex = -1
                                txtOnHoldTicketNo.Clear()
                                txtOnHoldQty.Clear()
                            End If
                        End If
                    Else
                        RJMessageBox.Show("Sorry the data not exist in DB. Cannot delete.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                Catch ex As Exception
                    RJMessageBox.Show("Error Delete On Hold : " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Sub LoaddgOnHold(proses As String)

        dgOnHold.DataSource = Nothing
        dgOnHold.Rows.Clear()
        dgOnHold.Columns.Clear()

        Dim i As Integer = 0

        Try

            With dgOnHold
                Dim sqlStr As String = ""

                If proses = "" Then
                    sqlStr = "select [CODE_STOCK_PROD_ONHOLD] [Code], [PROCESS] [Process], [PART_NUMBER] [Material], [LOT_NO] [Lot No], [TRACEABILITY] [Trace], [INV_CTRL_DATE] [ICD], [BATCH_NO] [Batch No], [FLOW_TICKET_NO] [Ticket], [PENGALI] [Qty], [QTY] [Sum Qty], oh.[DATETIME_INSERT] [Time Save], oh.[INSERT_WHO] [Scan By] from STOCK_PROD_ONHOLD OH, sub_sub_po sp where oh.sub_sub_po=sp.sub_sub_po and oh.line='" & cbLineNumber.Text & "' and sp.sub_sub_po='" & txtSubSubPODefective.Text & "' ORDER BY oh.CODE_STOCK_PROD_ONHOLD"
                Else
                    sqlStr = "select [CODE_STOCK_PROD_ONHOLD] [Code], [PROCESS] [Process], [PART_NUMBER] [Material], [LOT_NO] [Lot No], [TRACEABILITY] [Trace], [INV_CTRL_DATE] [ICD], [BATCH_NO] [Batch No], [FLOW_TICKET_NO] [Ticket], [PENGALI] [Qty], [QTY] [Sum Qty], oh.[DATETIME_INSERT] [Time Save], oh.[INSERT_WHO] [Scan By] from STOCK_PROD_ONHOLD oh, sub_sub_po sp where oh.sub_sub_po=sp.sub_sub_po and oh.PROCESS='" & proses & "' and oh.line='" & cbLineNumber.Text & "' and sp.sub_sub_po='" & txtSubSubPODefective.Text & "' ORDER BY oh.CODE_STOCK_PROD_ONHOLD"
                End If

                Dim dttable As DataTable = Database.GetData(sqlStr)

                If dttable.Rows.Count > 0 Then
                    .DataSource = dttable

                    .DefaultCellStyle.Font = New Font("Tahoma", 14)

                    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(0).Width = Int(.Width * 0.03)
                    .Columns(1).Width = Int(.Width * 0.12)
                    .Columns(2).Width = Int(.Width * 0.05)
                    .Columns(3).Width = Int(.Width * 0.05)
                    .Columns(4).Width = Int(.Width * 0.05)
                    .Columns(5).Width = Int(.Width * 0.05)
                    .Columns(6).Width = Int(.Width * 0.05)
                    .Columns(7).Width = Int(.Width * 0.05)
                    .Columns(8).Width = Int(.Width * 0.05)
                    .Columns(9).Width = Int(.Width * 0.05)
                    .Columns(10).Width = Int(.Width * 0.1)
                    .Columns(11).Width = Int(.Width * 0.08)

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

                    Dim deleteProductionRequest As DataGridViewButtonColumn = New DataGridViewButtonColumn
                    deleteProductionRequest.Name = "delete"
                    deleteProductionRequest.HeaderText = "Delete"
                    deleteProductionRequest.Text = "Delete"
                    deleteProductionRequest.UseColumnTextForButtonValue = True
                    .Columns.Insert(12, deleteProductionRequest)
                Else
                    .DataSource = Nothing
                End If
            End With

            For j As Integer = 0 To dgOnHold.RowCount - 1
                If dgOnHold.Rows(j).Index Mod 2 = 0 Then
                    dgOnHold.Rows(j).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    dgOnHold.Rows(j).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next
        Catch ex As Exception
            RJMessageBox.Show("Error Load Data On Hold " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cbOnHoldProcess_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbOnHoldProcess.SelectedIndexChanged
        LoaddgOnHold(cbOnHoldProcess.Text)
        txtOnHoldTicketNo.Select()
    End Sub

    Function ONHOLDGenerateCode() As String
        Dim wipCode As String = ""

        Try
            Call Database.koneksi_database()
            Dim dtCode As DataTable = Database.GetData("select distinct (CODE_STOCK_PROD_ONHOLD) from STOCK_PROD_ONHOLD WHERE PROCESS='" & cbOnHoldProcess.Text & "' and sub_sub_po='" & txtSubSubPODefective.Text & "'")

            If dtCode.Rows.Count > 0 Then
                wipCode = dtCode.Rows(0).Item("CODE_STOCK_PROD_ONHOLD")
            Else
                Dim dtOnHold As DataTable = Database.GetData("select distinct (CODE_STOCK_PROD_ONHOLD) from STOCK_PROD_ONHOLD")
                wipCode = "OH" & dtOnHold.Rows.Count + 1
            End If
        Catch ex As Exception
            RJMessageBox.Show("Error Insert ONHOLD : " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return wipCode
    End Function

    Function BalanceMaterialGenerateCode() As String
        Dim balanceCode As String = ""
        Try
            Dim queryCheckCodeBalanceMaterial As String = "SELECT top 1 id_level FROM stock_card where [level]='B' ORDER BY cast(replace(id_level,'B','') as int) desc"
            Dim dtCheckCodeBalanceMAterial As DataTable = Database.GetData(queryCheckCodeBalanceMaterial)
            If dtCheckCodeBalanceMAterial.Rows.Count > 0 Then
                Dim match As Match = Regex.Match(dtCheckCodeBalanceMAterial.Rows(0).Item("ID_LEVEL").ToString(), "^([A-Z]+)(\d+)$")
                If match.Success Then
                    Dim prefix As String = match.Groups(1).Value
                    Dim number As Integer = Integer.Parse(match.Groups(2).Value)
                    Dim nextNumber As Integer = number + 1
                    balanceCode = prefix & nextNumber.ToString()
                End If
            End If
        Catch ex As Exception
            RJMessageBox.Show("Error Insert Balance Material : " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return balanceCode
    End Function

    '************************************* END ON HOLD FUNCTION


    '===================================== BALANCE FUNCTION
    Private Sub btnBalanceAdd_Click(sender As Object, e As EventArgs) Handles btnBalanceAdd.Click
        If globVar.add > 0 Then
            Try
                If (CheckBox2.CheckState = CheckState.Checked And txtBalanceBarcode.Text <> "" And txtBalanceQty.Text <> "") Or (CheckBox2.CheckState = CheckState.Unchecked And txtReturnMaterialManual.Text <> "" And ComboBox2.Text <> "" And Convert.ToDouble(txtBalanceQty.Text) > 0) Then

                    Dim result = RJMessageBox.Show("Are you sure for save?.", "Are You Sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If result = DialogResult.No Then
                        Exit Sub
                    End If

                    Call Database.koneksi_database()

                    Dim ds As New DataTable
                    Dim dsCheck As New DataTable
                    Dim StandartPack As String

                    Dim codeBalance As String = BalanceMaterialGenerateCode()

                    Dim sqlCheckTable As String = "select * from STOCK_CARD where STATUS='Return to Mini Store' AND MATERIAL='" & txtReturnMaterialPN.Text & "' AND SUB_SUB_PO='" & txtSubSubPODefective.Text & "' AND LINE='" & cbLineNumber.Text & "' and department='" & globVar.department & "' and lot_no='" & TextBox10.Text & "' and traceability=(select traceability from stock_card where id=" & tampungIDMaterialReturnMaterial.Text & ") and batch_no=(select batch_no from stock_card where id=" & tampungIDMaterialReturnMaterial.Text & ") and inv_ctrl_date=(select inv_ctrl_date from stock_card where id=" & tampungIDMaterialReturnMaterial.Text & ")"
                    Dim dtCekTable As DataTable = Database.GetData(sqlCheckTable)

                    StandartPack = "NO"

                    If dtCekTable.Rows.Count > 0 Then

                        RJMessageBox.Show("Sorry this material already return. if want to change please using table below.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        loaddgBalance("")
                        txtReturnMaterialManual.Clear()
                        TextBox2.Clear()
                        txtBalanceBarcode.Clear()
                        txtBalanceMaterialPN.Clear()
                        txtBalanceQty.Clear()
                        txtReturnMaterialPN.Clear()
                        txtBalanceQty.Enabled = False
                        TextBox10.Clear()
                        tampungIDMaterialReturnMaterial.Clear()
                        ComboBox2.SelectedIndex = -1

                    Else
                        Dim idData As String = ""
                        Dim querySelectStockCard As String

                        querySelectStockCard = "select * from STOCK_CARD where id=" & tampungIDMaterialReturnMaterial.Text
                        Dim dtTable As DataTable = Database.GetData(querySelectStockCard)

                        If dtTable.Rows.Count <= 0 Then

                            RJMessageBox.Show("This material not exist in Database", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            loaddgBalance("")
                            txtReturnMaterialManual.Clear()
                            TextBox2.Clear()
                            txtBalanceBarcode.Clear()
                            txtBalanceMaterialPN.Clear()
                            txtBalanceQty.Clear()
                            txtReturnMaterialPN.Clear()
                            Exit Sub

                        End If


                        If dtTable.Rows(0).Item("actual_qty") >= Convert.ToDouble(txtBalanceQty.Text) Then

                            Dim sqlUpdate As String = "update STOCK_CARD set ACTUAL_QTY=actual_qty-" & txtBalanceQty.Text.Replace(",", ".") & " where ID=" & dtTable.Rows(0).Item("id")
                            Dim cmdUpdate = New SqlCommand(sqlUpdate, Database.koneksi)
                            If cmdUpdate.ExecuteNonQuery() Then

                                Dim sqlInsert As String = "insert into STOCK_CARD(MTS_NO,DEPARTMENT,MATERIAL,STATUS,STANDARD_PACK,INV_CTRL_DATE,TRACEABILITY,BATCH_NO,LOT_NO,FINISH_GOODS_PN,PO,SUB_PO,SUB_SUB_PO,LINE,QRCODE,QTY,ACTUAL_QTY,ID_LEVEL,LEVEL,REMARK)" &
                                                "select MTS_NO,DEPARTMENT,MATERIAL,'Return to Mini Store','" & StandartPack & "',INV_CTRL_DATE,TRACEABILITY,BATCH_NO,LOT_NO,FINISH_GOODS_PN,PO,SUB_PO,SUB_SUB_PO,LINE,'" & codeBalance & "'," & txtBalanceQty.Text.Replace(",", ".") & "," & txtBalanceQty.Text.Replace(",", ".") & ",'" & codeBalance & "','B','Balance Material' from STOCK_CARD where ID=" & dtTable.Rows(0).Item("id")
                                Dim cmdInsert = New SqlCommand(sqlInsert, Database.koneksi)

                                If cmdInsert.ExecuteNonQuery() Then

                                    RJMessageBox.Show("Success saving data!!!")
                                    loaddgBalance("")
                                    txtReturnMaterialManual.Clear()
                                    TextBox2.Clear()
                                    txtBalanceBarcode.Clear()
                                    txtBalanceMaterialPN.Clear()
                                    txtBalanceQty.Clear()
                                    txtReturnMaterialPN.Clear()
                                    txtBalanceQty.Enabled = False
                                    TextBox10.Clear()
                                    tampungIDMaterialReturnMaterial.Clear()

                                Else

                                    RJMessageBox.Show("Failed Save Balance Material!!!")

                                End If

                            End If
                        Else
                            loaddgBalance("")
                            txtReturnMaterialManual.Clear()
                            TextBox2.Clear()
                            txtBalanceBarcode.Clear()
                            txtBalanceMaterialPN.Clear()
                            txtBalanceQty.Clear()
                            txtReturnMaterialPN.Clear()

                            RJMessageBox.Show("Qty for this material is not enough")
                        End If

                    End If
                Else
                    RJMessageBox.Show("Sorry please fill the blank", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Catch ex As Exception
                RJMessageBox.Show("Error Insert : " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub btnBalanceEdit_Click(sender As Object, e As EventArgs) Handles btnBalanceEdit.Click
        Try
            Dim qtyUpdate As Integer = 0
            Dim dtCekTable As DataTable = Database.GetData("select * from STOCK_CARD where STATUS='Return to Mini Store' AND MATERIAL='" & txtBalanceMaterialPN.Text & "' AND SUB_SUB_PO='" & txtSubSubPODefective.Text & "' AND LINE='" & cbLineNumber.Text & "' and department='" & globVar.department & "' and lot_no='" & txtReturnMaterialPN.Text & "' ORDER BY LOT_NO")
            If dtCekTable.Rows.Count > 0 Then
                Dim result = RJMessageBox.Show("Are you sure for edit?.", "Editing", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If result = DialogResult.Yes Then

                    Dim sqlCheckStockCard As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and status='Production Request' and material='" & txtBalanceMaterialPN.Text & "' and department='" & globVar.department & "' and finish_goods_pn='" & cbFGPN.Text & "' and lot_no='" & dtCekTable.Rows(0).Item("lot_no") & "' AND [LEVEL]='Fresh'"
                    Dim dtCheckStockCard As DataTable = Database.GetData(sqlCheckStockCard)

                    If dtCheckStockCard.Rows.Count > 0 Then
                        Dim sqlUpdateStock As String = "update STOCK_CARD set ACTUAL_QTY=actual_qty-" & Convert.ToDouble(txtBalanceQty.Text) & " where ID='" & dtCekTable.Rows(0).Item("ID") & "'"
                        Dim cmdUpdateStock = New SqlCommand(sqlUpdateStock, Database.koneksi)
                        If cmdUpdateStock.ExecuteNonQuery() Then
                            Dim sqlUpdate As String = "update STOCK_CARD set ACTUAL_QTY=ACTUAL_QTY+" & Convert.ToDouble(txtBalanceQty.Text) & ",sum_qty=sum_qty+" & Convert.ToDouble(txtBalanceQty.Text) & " where ID='" & dtCheckStockCard.Rows(0).Item("ID") & "'"
                            Dim cmdUpdate = New SqlCommand(sqlUpdate, Database.koneksi)
                            If cmdUpdate.ExecuteNonQuery() Then
                                RJMessageBox.Show("Update Success.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                loaddgBalance("")
                                txtReturnMaterialManual.Clear()
                                TextBox2.Clear()
                                txtBalanceBarcode.Clear()
                                txtBalanceMaterialPN.Clear()
                                txtBalanceQty.Clear()
                                txtReturnMaterialPN.Clear()
                            End If
                        End If
                    End If
                End If
            Else
                RJMessageBox.Show("Sorry, the data not exist in DB. Please use Save Button.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                loaddgBalance("")
                txtReturnMaterialManual.Clear()
                TextBox2.Clear()
                txtBalanceBarcode.Clear()
                txtBalanceMaterialPN.Clear()
                txtBalanceQty.Clear()
                txtReturnMaterialPN.Clear()
            End If
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBalanceDelete_Click(sender As Object, e As EventArgs) Handles btnBalanceDelete.Click
        If globVar.delete > 0 Then
            Dim sqlUpdate As String

            If (CheckBox2.CheckState = CheckState.Checked And txtBalanceBarcode.Text <> "") Or (CheckBox2.CheckState = CheckState.Unchecked And txtReturnMaterialManual.Text <> "" And TextBox2.Text <> "") Then
                Dim result = RJMessageBox.Show("Are you sure for delete?.", "Deleting", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If result = DialogResult.Yes Then

                    Dim dtCekTable As DataTable = Database.GetData("select * from STOCK_CARD where STATUS='Return to Mini Store' AND MATERIAL='" & txtBalanceMaterialPN.Text & "' AND SUB_SUB_PO='" & txtSubSubPODefective.Text & "' AND LINE='" & cbLineNumber.Text & "' and department='" & globVar.department & "' and finish_goods_pn='" & cbFGPN.Text & "' and lot_no='" & txtReturnMaterialPN.Text & "' ORDER BY LOT_NO")
                    If dtCekTable.Rows.Count > 0 Then
                        'If InStr(txtBalanceBarcode.Text, "SA") > 0 Then
                        '    sqlUpdate = "update STOCK_CARD set ACTUAL_QTY=actual_qty+" & dtCekTable.Rows(0).Item("actual_qty") & ",SUM_QTY=SUM_QTY+" & dtCekTable.Rows(0).Item("actual_qty") & " where STATUS='Production Request' AND MATERIAL='" & txtBalanceMaterialPN.Text & "' AND SUB_SUB_PO='" & txtSubSubPODefective.Text & "' AND LINE='" & cbLineNumber.Text & "' and department='" & globVar.department & "' and finish_goods_pn='" & cbFGPN.Text & "' and lot_no='" & dtCekTable.Rows(0).Item("lot_no") & "' AND [LEVEL]='SA'"
                        'Else
                        sqlUpdate = "update STOCK_CARD set ACTUAL_QTY=actual_qty+" & dtCekTable.Rows(0).Item("actual_qty") & ",SUM_QTY=SUM_QTY+" & dtCekTable.Rows(0).Item("actual_qty") & " where STATUS='Production Request' AND MATERIAL='" & txtBalanceMaterialPN.Text & "' AND SUB_SUB_PO='" & txtSubSubPODefective.Text & "' AND LINE='" & cbLineNumber.Text & "' and department='" & globVar.department & "' and finish_goods_pn='" & cbFGPN.Text & "' and lot_no='" & dtCekTable.Rows(0).Item("lot_no") & "' AND [LEVEL]='Fresh'"
                        'End If

                        Dim cmdUpdate = New SqlCommand(sqlUpdate, Database.koneksi)

                        If cmdUpdate.ExecuteNonQuery() Then
                            Dim queryDelete As String = "delete from stock_card where id=" & dtCekTable.Rows(0).Item("id")
                            Dim dtDelete = New SqlCommand(queryDelete, Database.koneksi)
                            If dtDelete.ExecuteNonQuery() Then
                                RJMessageBox.Show("Success Delete data!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                loaddgBalance("")
                                txtReturnMaterialManual.Clear()
                                TextBox2.Clear()
                                txtBalanceBarcode.Clear()
                                txtBalanceMaterialPN.Clear()
                                txtBalanceQty.Clear()
                                txtReturnMaterialPN.Clear()
                            Else
                                RJMessageBox.Show("Fail Delete data!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                loaddgBalance("")
                                txtReturnMaterialManual.Clear()
                                TextBox2.Clear()
                                txtBalanceBarcode.Clear()
                                txtBalanceMaterialPN.Clear()
                                txtBalanceQty.Clear()
                                txtReturnMaterialPN.Clear()
                            End If
                        End If
                    Else
                        RJMessageBox.Show("Sorry. This Material not exist in DB. Cannot delete.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        loaddgBalance("")
                        txtReturnMaterialManual.Clear()
                        TextBox2.Clear()
                        txtBalanceBarcode.Clear()
                        txtBalanceMaterialPN.Clear()
                        txtBalanceQty.Clear()
                        txtReturnMaterialPN.Clear()
                    End If
                End If
            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub dgBalance_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgBalance.CellValueChanged

        If e.RowIndex = -1 Then

            Exit Sub

        End If

        If e.ColumnIndex = -1 Then

            Exit Sub

        End If

        If globVar.update = 0 Then

            RJMessageBox.Show("Your Access cannot execute this action")
            Exit Sub

        End If

        If dgBalance.Columns(e.ColumnIndex).Name = "Return Qty" Then

            Dim sqlcheck As String = "select * from stock_card where ID=" & dgBalance.Rows(e.RowIndex).Cells("#").Value
            Dim dtCheck As DataTable = Database.GetData(sqlcheck)
            If dtCheck.Rows.Count > 0 Then

                If dtCheck.Rows(0).Item("qty") <> dgBalance.Rows(e.RowIndex).Cells("Return Qty").Value Then

                    Dim sqlcheckSCProcess As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and material='" & dgBalance.Rows(e.RowIndex).Cells("Material").Value & "' and lot_no='" & dgBalance.Rows(e.RowIndex).Cells("Lot No.").Value & "' and inv_ctrl_date='" & dgBalance.Rows(e.RowIndex).Cells("Inv. Ctrl Date").Value & "' and traceability='" & dgBalance.Rows(e.RowIndex).Cells("Traceability").Value & "' and batch_no='" & dgBalance.Rows(e.RowIndex).Cells("Batch No.").Value & "' and status='Production Process' order by id"
                    Dim dtCheckSCProcess As DataTable = Database.GetData(sqlcheckSCProcess)

                    If dtCheckSCProcess.Rows(0).Item("actual_qty") - dgBalance.Rows(e.RowIndex).Cells("Return Qty").Value < 0 Then

                        RJMessageBox.Show("cannot change the qty because the addition makes the actual qty smaller than 0")
                        loaddgBalance("")
                        Exit Sub

                    End If

                    If dtCheck.Rows(0).Item("qty") > dgBalance.Rows(e.RowIndex).Cells("Return Qty").Value Then

                        Dim SumQty = dtCheck.Rows(0).Item("qty") - dgBalance.Rows(e.RowIndex).Cells("Return Qty").Value

                        Dim queryUpdateactualQty As String = "update stock_card set actual_qty=actual_qty + " & SumQty & " where id=" & dtCheckSCProcess.Rows(0).Item("id")
                        Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                        If dtUpdateactualQty.ExecuteNonQuery() Then

                            Dim queryUpdateReturn As String = "update stock_card set qty=" & dgBalance.Rows(e.RowIndex).Cells("Return Qty").Value & " where id=" & dgBalance.Rows(e.RowIndex).Cells("#").Value
                            Dim dtUpdateReturn = New SqlCommand(queryUpdateReturn, Database.koneksi)
                            If dtUpdateReturn.ExecuteNonQuery() Then

                                RJMessageBox.Show("Update Qty Success")
                                loaddgBalance("")

                            End If

                        End If

                    Else

                        Dim SumQty = dgBalance.Rows(e.RowIndex).Cells("Return Qty").Value - dtCheck.Rows(0).Item("qty")

                        Dim queryUpdateactualQty As String = "update stock_card set actual_qty=actual_qty - " & SumQty & " where id=" & dtCheckSCProcess.Rows(0).Item("id")
                        Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                        If dtUpdateactualQty.ExecuteNonQuery() Then

                            Dim queryUpdateReturn As String = "update stock_card set qty=" & dgBalance.Rows(e.RowIndex).Cells("Return Qty").Value & " where id=" & dgBalance.Rows(e.RowIndex).Cells("#").Value
                            Dim dtUpdateReturn = New SqlCommand(queryUpdateReturn, Database.koneksi)
                            If dtUpdateReturn.ExecuteNonQuery() Then

                                RJMessageBox.Show("Update Qty Success")
                                loaddgBalance("")

                            End If

                        End If

                    End If

                Else

                    RJMessageBox.Show("Old and New qty is same")

                End If

            End If

        End If

        'If dgBalance.Columns(e.ColumnIndex).Name = "Actual Qty" Then
        '    Try
        '        Dim qtyUpdate As Integer = 0
        '        Dim sqlCheckQty As String = "select * from stock_card where id='" & dgBalance.Rows(e.RowIndex).Cells("id").Value & "'"
        '        Dim dtCheckQtye As DataTable = Database.GetData(sqlCheckQty)
        '        If dtCheckQtye.Rows(0).Item("actual_qty") > dgBalance.Rows(e.RowIndex).Cells("Actual Qty").Value Then
        '            qtyUpdate = dtCheckQtye.Rows(0).Item("actual_qty") - dgBalance.Rows(e.RowIndex).Cells("Actual Qty").Value

        '            Dim sqlCheckStockCard As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and status='Production Request' and material='" & dgBalance.Rows(e.RowIndex).Cells("Material").Value & "'"
        '            Dim dtCheckStockCard As DataTable = Database.GetData(sqlCheckStockCard)

        '            If dtCheckStockCard.Rows.Count > 0 Then
        '                Dim sqlUpdateStock As String = "update STOCK_CARD set ACTUAL_QTY=actual_qty+" & qtyUpdate & " where ID='" & dtCheckStockCard.Rows(0).Item("ID") & "'"
        '                Dim cmdUpdateStock = New SqlCommand(sqlUpdateStock, Database.koneksi)
        '                cmdUpdateStock.ExecuteNonQuery()
        '            End If
        '        Else
        '            qtyUpdate = dgBalance.Rows(e.RowIndex).Cells("Actual Qty").Value - dtCheckQtye.Rows(0).Item("actual_qty")

        '            Dim sqlCheckStockCard As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and status='Production Request' and material='" & dgBalance.Rows(e.RowIndex).Cells("Material").Value & "'"
        '            Dim dtCheckStockCard As DataTable = Database.GetData(sqlCheckStockCard)

        '            If dtCheckStockCard.Rows.Count > 0 Then
        '                Dim sqlUpdateStock As String = "update STOCK_CARD set ACTUAL_QTY=actual_qty-" & qtyUpdate & " where ID='" & dtCheckStockCard.Rows(0).Item("ID") & "'"
        '                Dim cmdUpdateStock = New SqlCommand(sqlUpdateStock, Database.koneksi)
        '                cmdUpdateStock.ExecuteNonQuery()
        '            End If
        '        End If

        '        Dim sqlUpdate As String = "update STOCK_CARD set ACTUAL_QTY=" & dgBalance.Rows(e.RowIndex).Cells("Actual Qty").Value & " where ID='" & dgBalance.Rows(e.RowIndex).Cells("ID").Value & "'"
        '        Dim cmdUpdate = New SqlCommand(sqlUpdate, Database.koneksi)
        '        If cmdUpdate.ExecuteNonQuery() Then
        '            RJMessageBox.Show("Update Success.")
        '        End If

        '    Catch ex As Exception
        '        RJMessageBox.Show(ex.Message)
        '    End Try
        'End If
    End Sub

    Private Sub txtBalanceBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBalanceBarcode.KeyDown
        Dim QrcodeValid As Boolean
        If e.KeyCode = Keys.Enter Then
            Try

                If txtSubSubPODefective.Text <> "" Then

                    If txtBalanceBarcode.Text.StartsWith("B") AndAlso txtBalanceBarcode.Text.Length > 1 AndAlso IsNumeric(txtBalanceBarcode.Text.Substring(1)) Then 'Balance

                        Dim queryCheck As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and qrcode='" & txtBalanceBarcode.Text & "' and status='Production Process' and department='" & globVar.department & "'"
                        Dim dttable As DataTable = Database.GetData(queryCheck)
                        If dttable.Rows.Count > 0 Then
                            globVar.QRCode_PN = dttable.Rows(0).Item("material")
                            globVar.QRCode_lot = dttable.Rows(0).Item("lot_no")

                            txtReturnMaterialPN.Text = globVar.QRCode_PN
                            TextBox10.Text = globVar.QRCode_lot

                            tampungIDMaterialReturnMaterial.Text = dttable.Rows(0).Item("id")

                            If dttable.Rows(0).Item("actual_qty") = 0 Then
                                txtBalanceBarcode.Clear()
                                RJMessageBox.Show("This material qty is 0.")
                                Exit Sub
                            End If

                            txtBalanceQty.Enabled = True
                            txtBalanceQty.Text = dttable.Rows(0).Item("actual_qty")
                            txtBalanceQty.Select()
                            btnBalanceAdd.Enabled = True
                        Else
                            RJMessageBox.Show("This material not exist in Database.")
                            txtBalanceBarcode.Clear()
                            txtBalanceMaterialPN.Clear()
                            txtReturnMaterialPN.Clear()
                            Exit Sub
                        End If

                    ElseIf txtBalanceBarcode.Text.StartsWith("SM") AndAlso txtBalanceBarcode.Text.Length > 2 AndAlso IsNumeric(txtBalanceBarcode.Text.Substring(2)) Then

                        Dim queryCheck As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and qrcode='" & txtBalanceBarcode.Text & "' and status='Production Process' and department='" & globVar.department & "'"
                        Dim dttable As DataTable = Database.GetData(queryCheck)
                        If dttable.Rows.Count > 0 Then
                            globVar.QRCode_PN = dttable.Rows(0).Item("material")
                            globVar.QRCode_lot = dttable.Rows(0).Item("lot_no")

                            txtReturnMaterialPN.Text = globVar.QRCode_PN
                            TextBox10.Text = globVar.QRCode_lot

                            tampungIDMaterialReturnMaterial.Text = dttable.Rows(0).Item("id")

                            If dttable.Rows(0).Item("actual_qty") = 0 Then
                                txtBalanceBarcode.Clear()
                                RJMessageBox.Show("This material qty is 0.")
                                Exit Sub
                            End If

                            txtBalanceQty.Enabled = True
                            txtBalanceQty.Text = dttable.Rows(0).Item("actual_qty")
                            txtBalanceQty.Select()
                            btnBalanceAdd.Enabled = True
                        Else
                            RJMessageBox.Show("This material not exist in Database.")
                            txtBalanceBarcode.Clear()
                            txtBalanceMaterialPN.Clear()
                            txtReturnMaterialPN.Clear()
                            Exit Sub
                        End If

                    ElseIf txtBalanceBarcode.Text.StartsWith("NQ") AndAlso txtBalanceBarcode.Text.Length > 2 AndAlso IsNumeric(txtBalanceBarcode.Text.Substring(2)) Then

                        Dim queryCheck As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and qrcode_new='" & txtBalanceBarcode.Text & "' and status='Production Process' and department='" & globVar.department & "'"
                        Dim dttable As DataTable = Database.GetData(queryCheck)
                        If dttable.Rows.Count > 0 Then
                            globVar.QRCode_PN = dttable.Rows(0).Item("material")
                            globVar.QRCode_lot = dttable.Rows(0).Item("lot_no")

                            txtReturnMaterialPN.Text = globVar.QRCode_PN
                            TextBox10.Text = globVar.QRCode_lot

                            tampungIDMaterialReturnMaterial.Text = dttable.Rows(0).Item("id")

                            If dttable.Rows(0).Item("actual_qty") = 0 Then
                                txtBalanceBarcode.Clear()
                                RJMessageBox.Show("This material qty is 0.")
                                Exit Sub
                            End If

                            txtBalanceQty.Enabled = True
                            txtBalanceQty.Text = dttable.Rows(0).Item("actual_qty")
                            txtBalanceQty.Select()
                            btnBalanceAdd.Enabled = True
                        Else
                            RJMessageBox.Show("This material not exist in Database.")
                            txtBalanceBarcode.Clear()
                            txtBalanceMaterialPN.Clear()
                            txtReturnMaterialPN.Clear()
                            Exit Sub
                        End If

                    ElseIf txtBalanceBarcode.Text.StartsWith("MX2D") Then 'Normal

                        QrcodeValid = QRCode.Baca(txtBalanceBarcode.Text)

                        If QrcodeValid = False Then
                            RJMessageBox.Show("QRCode Not Valid")
                            Play_Sound.Wrong()
                            txtBalanceBarcode.Clear()
                            Exit Sub
                        End If

                        If globVar.QRCode_PN = "" Or globVar.QRCode_lot = "" Or globVar.QRCode_Traceability = "" Or globVar.QRCode_Batch = "" Or globVar.QRCode_Inv = "" Then
                            RJMessageBox.Show("QRCode Not Valid")
                            Play_Sound.Wrong()
                            txtBalanceBarcode.Clear()
                            Exit Sub
                        End If

                        Dim queryCheck As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and material='" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' and traceability='" & globVar.QRCode_Traceability & "' and inv_ctrl_date='" & globVar.QRCode_Inv & "' and batch_no='" & globVar.QRCode_Batch & "' and status='Production Process' and department='" & globVar.department & "'"
                        Dim dttable As DataTable = Database.GetData(queryCheck)
                        If dttable.Rows.Count > 0 Then
                            globVar.QRCode_PN = dttable.Rows(0).Item("material")
                            globVar.QRCode_lot = dttable.Rows(0).Item("lot_no")

                            txtReturnMaterialPN.Text = globVar.QRCode_PN
                            TextBox10.Text = globVar.QRCode_lot

                            tampungIDMaterialReturnMaterial.Text = dttable.Rows(0).Item("id")

                            If dttable.Rows(0).Item("actual_qty") = 0 Then
                                txtBalanceBarcode.Clear()
                                RJMessageBox.Show("This material qty is 0.")
                                Exit Sub
                            End If

                            txtBalanceQty.Enabled = True
                            txtBalanceQty.Text = dttable.Rows(0).Item("actual_qty")
                            txtBalanceQty.Select()
                            btnBalanceAdd.Enabled = True
                        Else
                            RJMessageBox.Show("This material not exist in DB.")
                            txtBalanceBarcode.Clear()
                            txtBalanceMaterialPN.Clear()
                            txtReturnMaterialPN.Clear()
                            Exit Sub
                        End If

                    ElseIf txtBalanceBarcode.Text.StartsWith("SA") AndAlso txtBalanceBarcode.Text.Length > 2 AndAlso IsNumeric(txtBalanceBarcode.Text.Substring(2)) Then

                        Dim queryCheck As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and id_level='" & txtBalanceBarcode.Text & "' and status='Production Process' and department='" & globVar.department & "'"
                        Dim dttable As DataTable = Database.GetData(queryCheck)
                        If dttable.Rows.Count > 0 Then
                            globVar.QRCode_PN = dttable.Rows(0).Item("material")
                            globVar.QRCode_lot = dttable.Rows(0).Item("lot_no")

                            txtReturnMaterialPN.Text = globVar.QRCode_PN
                            TextBox10.Text = globVar.QRCode_lot

                            tampungIDMaterialReturnMaterial.Text = dttable.Rows(0).Item("id")

                            If dttable.Rows(0).Item("actual_qty") = 0 Then
                                txtBalanceBarcode.Clear()
                                RJMessageBox.Show("This material qty is 0.")
                                Exit Sub
                            End If

                            txtBalanceQty.Enabled = True
                            txtBalanceQty.Text = dttable.Rows(0).Item("actual_qty")
                            txtBalanceQty.Select()
                            btnBalanceAdd.Enabled = True
                        Else
                            RJMessageBox.Show("This material not exist in DB.")
                            txtBalanceBarcode.Clear()
                            txtBalanceMaterialPN.Clear()
                            txtReturnMaterialPN.Clear()
                            Exit Sub
                        End If

                    Else

                        RJMessageBox.Show("QRCode not valid.")
                        Play_Sound.Wrong()
                        txtBalanceBarcode.Clear()
                        Exit Sub

                    End If

                Else
                    RJMessageBox.Show("Sorry, please input Sub Sub PO First")
                    txtBalanceBarcode.Clear()
                    txtBalanceMaterialPN.Clear()
                End If
            Catch ex As Exception
                RJMessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Sub loaddgBalance(material As String)

        dgBalance.DataSource = Nothing
        dgBalance.Rows.Clear()
        dgBalance.Columns.Clear()

        Dim i As Integer = 0

        Try
            With dgBalance
                Dim sqlStr As String = ""

                If material = "" Then
                    sqlStr = "select sc.id [#],sc.material Material,sc.id_level Qrcode,sc.inv_ctrl_date [Inv. Ctrl Date],sc.traceability Traceability,sc.batch_no [Batch No.],sc.lot_no [Lot No.],sc.qty [Return Qty] from STOCK_CARD sc, sub_sub_po sp where sc.DEPARTMENT='" & dept & "' AND sc.STATUS='Return to Mini Store' and sc.sub_sub_po=sp.sub_sub_po and sp.sub_sub_po='" & txtSubSubPODefective.Text & "' and sc.line='" & cbLineNumber.Text & "' ORDER BY sc.ID"
                Else
                    sqlStr = "select sc.id [#],sc.material Material,sc.id_level Qrcode,sc.inv_ctrl_date [Inv. Ctrl Date],sc.traceability Traceability,sc.batch_no [Batch No.],sc.lot_no [Lot No.],sc.qty [Return Qty] from STOCK_CARD sc, sub_sub_po sp where sc.MATERIAL='" & material & "' AND sc.DEPARTMENT='" & dept & "' AND sc.STATUS='Return to Mini Store' and sc.sub_sub_po=sp.sub_sub_po and sp.sub_sub_po='" & txtSubSubPODefective.Text & "'  and sc.line='" & cbLineNumber.Text & "' ORDER BY sc.ID"
                End If

                Dim dttable As DataTable = Database.GetData(sqlStr)

                If dttable.Rows.Count > 0 Then
                    .DataSource = dttable

                    .DefaultCellStyle.Font = New Font("Tahoma", 14)

                    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(0).Width = Int(.Width * 0.05)
                    .Columns(1).Width = Int(.Width * 0.08)
                    .Columns(2).Width = Int(.Width * 0.2)
                    .Columns(3).Width = Int(.Width * 0.14)
                    .Columns(4).Width = Int(.Width * 0.1)
                    .Columns(5).Width = Int(.Width * 0.1)
                    .Columns(6).Width = Int(.Width * 0.11)
                    .Columns(7).Width = Int(.Width * 0.1)

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

                    Dim deleteProductionRequest As DataGridViewButtonColumn = New DataGridViewButtonColumn
                    deleteProductionRequest.Name = "delete"
                    deleteProductionRequest.HeaderText = "Delete"
                    deleteProductionRequest.Width = 50
                    deleteProductionRequest.Text = "Delete"
                    deleteProductionRequest.UseColumnTextForButtonValue = True
                    .Columns.Insert(8, deleteProductionRequest)
                Else
                    .DataSource = Nothing
                End If
            End With

            For j As Integer = 0 To dgBalance.RowCount - 1
                If dgBalance.Rows(j).Index Mod 2 = 0 Then
                    dgBalance.Rows(j).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    dgBalance.Rows(j).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next
        Catch ex As Exception
            RJMessageBox.Show("Error Load DGV Balance " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgReject_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgReject.CellFormatting
        If dgReject.Columns(e.ColumnIndex).Name = "Reject Qty" Then
            e.CellStyle.BackColor = Color.Green
            e.CellStyle.ForeColor = Color.White
        End If
    End Sub

    Private Sub dgBalance_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgBalance.CellFormatting
        If dgBalance.Columns(e.ColumnIndex).Name = "Return Qty" Then
            e.CellStyle.BackColor = Color.Green
            e.CellStyle.ForeColor = Color.White
        End If
    End Sub

    Sub loaddgReject(material As String)

        dgReject.DataSource = Nothing
        dgReject.Rows.Clear()
        dgReject.Columns.Clear()

        Dim i As Integer = 0

        Try

            With dgReject
                Dim sqlStr As String = ""

                If material = "" Then
                    sqlStr = "select ID [#], sc.part_number[Material], CASE WHEN sc.sub_assy IS NULL OR sc.sub_assy = '' THEN sc.part_number ELSE sc.sub_assy END AS Qrcode,sc.inv_ctrl_date [Inv. Ctrl Date],sc.traceability Traceability,sc.batch_no [Batch No.],sc.lot_no [Lot No.],sc.qty [Reject Qty] from out_prod_reject sc where sc.DEPARTMENT='" & dept & "' AND sc.sub_sub_po='" & txtSubSubPODefective.Text & "' and sc.line='" & cbLineNumber.Text & "' ORDER BY sc.ID"
                Else
                    sqlStr = "select ID [#], sc.part_number[Material], CASE WHEN sc.sub_assy IS NULL OR sc.sub_assy = '' THEN sc.part_number ELSE sc.sub_assy END AS Qrcode,sc.inv_ctrl_date [Inv. Ctrl Date],sc.traceability Traceability,sc.batch_no [Batch No.],sc.lot_no [Lot No.],sc.qty [Reject Qty] from out_prod_reject sc where sc.part_number='" & material & "' AND sc.DEPARTMENT='" & dept & "' AND sc.sub_sub_po='" & txtSubSubPODefective.Text & "' and sc.line='" & cbLineNumber.Text & "' ORDER BY sc.ID"
                End If

                Dim dttable As DataTable = Database.GetData(sqlStr)

                If dttable.Rows.Count > 0 Then
                    .DataSource = dttable

                    .DefaultCellStyle.Font = New Font("Tahoma", 14)

                    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(0).Width = Int(.Width * 0.1)
                    .Columns(1).Width = Int(.Width * 0.1)
                    .Columns(2).Width = Int(.Width * 0.1)
                    .Columns(3).Width = Int(.Width * 0.2)
                    .Columns(4).Width = Int(.Width * 0.2)
                    .Columns(5).Width = Int(.Width * 0.2)
                    .Columns(6).Width = Int(.Width * 0.1)
                    .Columns(7).Width = Int(.Width * 0.1)

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

                    Dim deleteProductionRequest As DataGridViewButtonColumn = New DataGridViewButtonColumn
                    deleteProductionRequest.Name = "delete"
                    deleteProductionRequest.HeaderText = "Delete"
                    deleteProductionRequest.Width = 50
                    deleteProductionRequest.Text = "Delete"
                    deleteProductionRequest.UseColumnTextForButtonValue = True
                    .Columns.Insert(8, deleteProductionRequest)
                Else
                    .DataSource = Nothing
                End If
            End With

            For j As Integer = 0 To dgReject.RowCount - 1
                If dgReject.Rows(j).Index Mod 2 = 0 Then
                    dgReject.Rows(j).DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    dgReject.Rows(j).DefaultCellStyle.BackColor = Color.LemonChiffon
                End If
            Next
        Catch ex As Exception
            RJMessageBox.Show("Error Load DGV Reject " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '===================================== END BALANCE FUNCTION
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles rbFG.CheckedChanged
        If rbFG.Checked = True Then
            'ReadonlyFormFG(False)
            'ReadonlyFormSA(True)
            Dim dtFG As DataTable = Database.GetData("select DISTINCT(CODE_OUT_PROD_DEFECT), input_from_fg from out_prod_defect where SUB_SUB_PO='" & txtSubSubPODefective.Text & "' and input_from_fg=1")

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

                btnSaveSADefect.Enabled = False
                btnSaveSA.Enabled = False
                btnSaveFGDefect.Enabled = False
                btnSaveFG.Enabled = False
                btnResetFG.Enabled = False
                btnResetSA.Enabled = False
                'btnPrintFGDefect.Enabled = False
                'btnPrintSADefect.Enabled = False

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

                btnSaveSADefect.Enabled = False
                btnSaveSA.Enabled = False
                btnSaveFGDefect.Enabled = False
                btnSaveFG.Enabled = False
                btnResetFG.Enabled = False
                btnResetSA.Enabled = False
                'btnPrintFGDefect.Enabled = False
                'btnPrintSADefect.Enabled = False
            End If

        ElseIf rbSA.Checked = True Then

            'ReadonlyFormFG(True)
            'ReadonlyFormSA(False)
            Dim dtSA As DataTable = Database.GetData("select DISTINCT(CODE_OUT_PROD_DEFECT), input_from_fg from out_prod_defect where SUB_SUB_PO='" & txtSubSubPODefective.Text & "' and input_from_fg=0")

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

                btnSaveFGDefect.Enabled = False
                btnSaveFG.Enabled = False
                btnResetFG.Enabled = False
                btnSaveSADefect.Enabled = False
                btnSaveSA.Enabled = False
                btnResetSA.Enabled = False
                'btnPrintFGDefect.Enabled = False
                'btnPrintSADefect.Enabled = False

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

                btnSaveFGDefect.Enabled = False
                btnSaveFG.Enabled = False
                btnSaveSADefect.Enabled = False
                btnSaveSA.Enabled = False
                btnResetFG.Enabled = False
                btnResetSA.Enabled = False
                'btnPrintFGDefect.Enabled = False
                'btnPrintSADefect.Enabled = False
            End If
        End If
    End Sub

    Private Sub rbSA_CheckedChanged(sender As Object, e As EventArgs) Handles rbSA.CheckedChanged
        If rbSA.Checked = True Then
            Dim dtSA As DataTable = Database.GetData("select DISTINCT(CODE_OUT_PROD_DEFECT), input_from_fg from out_prod_defect where SUB_SUB_PO='" & txtSubSubPODefective.Text & "'")

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

                    btnSaveFGDefect.Enabled = False
                    btnSaveFG.Enabled = False
                    btnSaveSADefect.Enabled = False
                    btnSaveSA.Enabled = False
                    btnResetFG.Enabled = False
                    btnResetSA.Enabled = False
                    'btnPrintFGDefect.Enabled = False
                    'btnPrintSADefect.Enabled = False
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

                btnSaveFGDefect.Enabled = False
                btnSaveFG.Enabled = False
                btnSaveSADefect.Enabled = False
                btnSaveSA.Enabled = False
                btnResetFG.Enabled = False
                btnResetSA.Enabled = False
                'btnPrintFGDefect.Enabled = False
                'btnPrintSADefect.Enabled = False
            End If
        ElseIf rbFG.Checked = True Then
            Dim dtFG As DataTable = Database.GetData("select DISTINCT(CODE_OUT_PROD_DEFECT), input_from_fg from out_prod_defect where SUB_SUB_PO='" & txtSubSubPODefective.Text & "'")

            If dtFG.Rows.Count > 0 Then

                If dtFG.Rows(0).Item("INPUT_FROM_FG") = 1 Then

                    ReadonlyFormFG(False)
                    ReadonlyFormSA(True)

                    rbSA.Checked = False
                    DataGridView1.Enabled = False
                    TableLayoutPanel8.Enabled = True

                    DataGridView3.Enabled = False
                    TableLayoutPanel9.Enabled = False

                    btnSaveSADefect.Enabled = False
                    btnSaveSA.Enabled = False
                    btnSaveFGDefect.Enabled = False
                    btnSaveFG.Enabled = False
                    btnResetFG.Enabled = False
                    btnResetSA.Enabled = False
                    'btnPrintFGDefect.Enabled = False
                    'btnPrintSADefect.Enabled = False

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

                btnSaveSADefect.Enabled = False
                btnSaveSA.Enabled = False
                btnSaveFGDefect.Enabled = False
                btnSaveFG.Enabled = False
                btnResetFG.Enabled = False
                btnResetSA.Enabled = False
                'btnPrintFGDefect.Enabled = False
                'btnPrintSADefect.Enabled = False
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

                Dim dtProcess As DataTable = Database.GetData("select mpf.id,mpf.master_process,isnull(opr.pengali,0) pengali from MASTER_PROCESS_FLOW mpf left join out_prod_defect opr on opr.process_reject=mpf.master_process and opr.sub_sub_po='" & txtSubSubPODefective.Text & "' and department='" & dept & "' AND opr.flow_ticket_no='" & splitFlowTicket(5) & "' where mpf.MASTER_FINISH_GOODS_PN='" & str & "' and mpf.master_process is not null GROUP BY mpf.id,mpf.master_process,opr.pengali ORDER BY mpf.ID")
                Dim dtFG As DataTable = Database.GetData("select * from MASTER_FINISH_GOODS where FG_PART_NUMBER='" & str & "'")

                TextBox3.Text = dtFG.Rows(0).Item("LASER_CODE").ToString

                With DataGridView1

                    .DefaultCellStyle.Font = New Font("Tahoma", 14)

                    .ColumnCount = 3
                    .Columns(0).Name = "No"
                    .Columns(1).Name = "Process Name"
                    .Columns(2).Name = "Defect Qty"

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
                    Else
                        .Rows.Clear()
                    End If

                    .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
                End With

                For i As Integer = 0 To DataGridView1.RowCount - 1
                    If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                        DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                    Else
                        DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                    End If
                Next i

            Catch ex As Exception
                RJMessageBox.Show("Error Load Data Process Flow", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

                Dim dtProcess As DataTable = Database.GetData("select mpf.id,mpf.master_process,isnull(opr.pengali,0) pengali from MASTER_PROCESS_FLOW mpf left join out_prod_defect opr on opr.process_reject=mpf.master_process and opr.sub_sub_po='" & txtSubSubPODefective.Text & "' and department='" & dept & "' AND opr.flow_ticket_no='" & splitFlowTicket(5) & "' where mpf.MASTER_FINISH_GOODS_PN='" & str & "' and mpf.master_process is not null GROUP BY mpf.id,mpf.master_process,opr.pengali ORDER BY mpf.ID")
                Dim dtFG As DataTable = Database.GetData("select * from MASTER_FINISH_GOODS where FG_PART_NUMBER='" & str & "'")
                Dim dtMat As DataTable = Database.GetData("select * from master_material where PART_NUMBER='" & cbFGPN.Text & "'")

                TextBox6.Text = dtFG.Rows(0).Item("LASER_CODE").ToString
                txtLossQty.Text = dtMat.Rows(0).Item("standard_qty").ToString

                With DataGridView3
                    .DefaultCellStyle.Font = New Font("Tahoma", 14)

                    .ColumnCount = 3
                    .Columns(0).Name = "No"
                    .Columns(1).Name = "Process Name"
                    .Columns(2).Name = "Defect Qty"

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
                    Else
                        .Rows.Clear()
                    End If

                    .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
                End With

                For i As Integer = 0 To DataGridView3.RowCount - 1
                    If DataGridView3.Rows(i).Index Mod 2 = 0 Then
                        DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                    Else
                        DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                    End If
                Next i

            Catch ex As Exception
                RJMessageBox.Show("Error Load Data Process Flow," & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    'Sub LoaddgvFG(proses As String)
    '    Dim i As Integer = 0

    '    Try
    '        'Call Database.koneksi_database()
    '        'Dim dtMaterialUsage As DataTable = Database.GetData("select distinct MATERIAL_USAGE from _OLD_MASTER_PROCESS_FLOW where MASTER_PROCESS='" & strKey & "' AND MASTER_FINISH_GOODS_PN='" & cbFGPN.Text & "'")
    '        ''Dim dtMaterialInfo As DataTable = Database.GetData("select distinct MATERIAL_USAGE from MASTER_PROCESS_FLOW where MASTER_PROCESS='" & strKey & "'")

    '        'Dim matUsage As String = dtMaterialUsage.Rows(i)(0).ToString()
    '        'Dim matList() As String = matUsage.Split(";")

    '        With dgWIP
    '            .Rows.Clear()

    '            .DefaultCellStyle.Font = New Font("Tahoma", 14)

    '            .ColumnCount = 10
    '            .Columns(0).Name = "No"
    '            .Columns(1).Name = "ID"
    '            .Columns(2).Name = "Process Name"
    '            .Columns(3).Name = "Ticket No."
    '            .Columns(4).Name = "Material PN"
    '            .Columns(5).Name = "Inv No."
    '            .Columns(6).Name = "MFG Date"
    '            .Columns(7).Name = "Lot No."
    '            .Columns(8).Name = "Qty x Usage"
    '            .Columns(9).Name = "Qty"

    '            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    '            .Columns(0).Width = Int(.Width * 0.05)
    '            .Columns(1).Width = Int(.Width * 0.08)
    '            .Columns(2).Width = Int(.Width * 0.2)
    '            .Columns(3).Width = Int(.Width * 0.08)
    '            .Columns(4).Width = Int(.Width * 0.1)
    '            .Columns(5).Width = Int(.Width * 0.1)
    '            .Columns(6).Width = Int(.Width * 0.15)
    '            .Columns(7).Width = Int(.Width * 0.08)
    '            .Columns(8).Width = Int(.Width * 0.08)
    '            .Columns(9).Width = Int(.Width * 0.06)


    '            .EnableHeadersVisualStyles = False
    '            With .ColumnHeadersDefaultCellStyle
    '                .BackColor = Color.Navy
    '                .ForeColor = Color.White
    '                .Font = New Font("Tahoma", 13, FontStyle.Bold)
    '                .Alignment = HorizontalAlignment.Center
    '                .Alignment = ContentAlignment.MiddleCenter
    '            End With



    '            ''''''''''''''''''''''''''''''''''''''''''''
    '            Dim sqlStr As String = ""

    '            If proses = "" Then
    '                sqlStr = "select * from STOCK_PROD_WIP ORDER BY CODE_STOCK_PROD_WIP"
    '            Else
    '                sqlStr = "select * from STOCK_PROD_WIP where PROCESS='" & proses & "' ORDER BY CODE_STOCK_PROD_WIP"
    '            End If

    '            Dim dttable As DataTable = Database.GetData(sqlStr)


    '            If dttable.Rows.Count > 0 Then
    '                For i = 0 To dttable.Rows.Count - 1
    '                    .Rows.Add(1)
    '                    .Item(0, i).Value = (i + 1).ToString()
    '                    .Item(1, i).Value = dttable.Rows(i)("CODE_STOCK_PROD_WIP")
    '                    .Item(2, i).Value = dttable.Rows(i)("PROCESS")
    '                    .Item(3, i).Value = dttable.Rows(i)("FLOW_TICKET_NO")
    '                    .Item(4, i).Value = dttable.Rows(i)("PART_NUMBER")
    '                    .Item(5, i).Value = dttable.Rows(i)("INV_CTRL_DATE")
    '                    .Item(6, i).Value = dttable.Rows(i)("TRACEABILITY")
    '                    .Item(7, i).Value = dttable.Rows(i)("LOT_NO")
    '                    .Item(8, i).Value = dttable.Rows(i)("QTY")
    '                    .Item(9, i).Value = dttable.Rows(i)("PENGALI")
    '                Next
    '            End If

    '            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
    '        End With
    '    Catch ex As Exception
    '        RJMessageBox.Show("Error Load DGV FG", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub TextBox4_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtFGFlowTicket.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) And txtFGFlowTicket.Text IsNot "" Then
            Try
                Dim Split() As String = txtFGFlowTicket.Text.Split(";")
                Dim Split1() As String = Split(0).Split("-")

                txtTampungFlow.Text = Split1(0)
                If txtFGLabel.Text IsNot "" Then
                    If Split1(0) = txtTampungLabel.Text Then
                        If rbFG.Checked = True Then
                            loadFG(cbFGPN.Text, txtFGFlowTicket.Text)
                            btnSaveFGDefect.Enabled = True
                            btnSaveFG.Enabled = True
                            'btnPrintFGDefect.Enabled = True
                            DataGridView1.Enabled = True
                            txtFGLabel.ReadOnly = True
                            txtFGFlowTicket.ReadOnly = True
                            btnResetFG.Enabled = True
                            TextBox3.Select()
                        End If
                    Else
                        RJMessageBox.Show("Sorry. QR Code SAP and QR Code Flow Ticket are different. Please double check.")
                    End If
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub txtFGLabel_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtFGLabel.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) And txtFGLabel.Text IsNot "" Then
            Try
                QRCode.Baca(txtFGLabel.Text)

                Dim intValue As Integer = 0
                Integer.TryParse(globVar.QRCode_Traceability, intValue)
                txtTampungLabel.Text = intValue
                txtINV.Text = globVar.QRCode_Inv
                txtBatchno.Text = globVar.QRCode_Batch

                If txtFGFlowTicket.Text IsNot "" Then
                    If intValue.ToString = txtTampungFlow.Text Then
                        If rbFG.Checked = True Then
                            loadFG(cbFGPN.Text, txtFGFlowTicket.Text)
                            btnSaveFGDefect.Enabled = True
                            btnSaveFG.Enabled = True
                            btnPrintFGDefect.Enabled = True
                            DataGridView1.Enabled = True
                            txtFGLabel.ReadOnly = True
                            txtFGFlowTicket.ReadOnly = True
                            btnResetFG.Enabled = True
                            txtFGFlowTicket.Select()
                        End If
                    Else
                        RJMessageBox.Show("Sorry. QR Code SAP and QR Code Flow Ticket are different. Please double check.")
                    End If
                End If
            Catch ex As Exception
                RJMessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Function SaveFGDefact()
        If globVar.add > 0 Then
            If DataGridView1.Rows.Count > 0 Then
                Try
                    If txtStatusSubSubPO.Text = "Closed" Then
                        RJMessageBox.Show("Sorry This Sub Sub PO status is closed. Cannot use this menu.")
                        Return "not_access"
                        Exit Function
                    End If

                    Dim sResult As Integer = 0
                    Dim spasiFlowTicket = txtFGFlowTicket.Text.Replace(ChrW(160), " ")
                    Dim sFlowTicket = spasiFlowTicket.Split(";")
                    Dim sFlow_Ticket5 = sFlowTicket(5).Split(" of ")

                    Dim queryCheckDoneFG As String = "select * from done_fg where DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and fg='" & cbFGPN.Text & "' and flow_ticket='" & sFlowTicket(5) & "' and lot_no='" & sFlow_Ticket5(0) & "'"
                    Dim dtCheckDoneFG As DataTable = Database.GetData(queryCheckDoneFG)
                    If dtCheckDoneFG.Rows.Count > 0 Then
                        RJMessageBox.Show("Sorry this Flow Ticket already Done. Cannot Save.")
                        ClearInputFG()
                        Exit Function
                    End If

                    For i = 0 To DataGridView1.Rows.Count - 1
                        Dim doWhile As String = ""
                        If DataGridView1.Rows(i).Cells(2).Value IsNot "" And DataGridView1.Rows(i).Cells(2).Value IsNot Nothing And DataGridView1.Rows(i).Cells(2).Value IsNot DBNull.Value Then
                            If IsNumeric(DataGridView1.Rows(i).Cells(2).Value) Then
                                If Convert.ToInt32(DataGridView1.Rows(i).Cells(2).Value) > 0 Then
                                    Dim query As String = "select * from master_process_flow where master_finish_goods_pn='" & cbFGPN.Text & "' and master_process='" & DataGridView1.Rows(i).Cells(1).Value & "'"
                                    Dim dtMasterProcessFlow As DataTable = Database.GetData(query)
                                    Dim numberInt As Integer = dtMasterProcessFlow.Rows(0).Item("ID")
                                    If dtMasterProcessFlow.Rows.Count > 0 Then
                                        If IsDBNull(dtMasterProcessFlow.Rows(0).Item("MATERIAL_USAGE")) = False Then
                                            sResult = funcInsertReject(dtMasterProcessFlow.Rows(0).Item("MATERIAL_USAGE"), DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(1).Value, 1, txtFGFlowTicket.Text)
                                        Else
                                            Dim queryKosong As String = "select * from master_process_flow where master_finish_goods_pn='" & cbFGPN.Text & "' order by id"
                                            Dim dtKosong As DataTable = Database.GetData(queryKosong)
                                            Do While doWhile Is ""
                                                Dim queryGetMaterialUsage As String = "select * from master_process_flow where ID=" & numberInt
                                                Dim dtKosongGetMaterialUsage As DataTable = Database.GetData(queryGetMaterialUsage)
                                                If numberInt <= dtKosong.Rows(0).Item("ID") Then
                                                    doWhile = "Kosong-" & DataGridView1.Rows(i).Cells(1).Value
                                                    DataGridView1.Rows(i).Cells(2).Value = 0
                                                Else
                                                    If IsDBNull(dtKosongGetMaterialUsage.Rows(0).Item("MATERIAL_USAGE")) = False Then
                                                        doWhile = dtKosongGetMaterialUsage.Rows(0).Item("MATERIAL_USAGE")
                                                    Else
                                                        numberInt = dtKosongGetMaterialUsage.Rows(0).Item("ID") - 1
                                                    End If
                                                End If
                                            Loop
                                            sResult = funcInsertReject(doWhile, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(1).Value, 1, txtFGFlowTicket.Text)
                                        End If
                                    End If
                                Else
                                    Dim queryCheckDefect As String = "select * from out_prod_defect where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and process_reject='" & DataGridView1.Rows(i).Cells(1).Value & "' and flow_ticket_no='" & sFlowTicket(5) & "' and department='" & globVar.department & "' and pengali > 0 and input_from_fg=1"
                                    Dim dtCheckDefect As DataTable = Database.GetData(queryCheckDefect)
                                    If dtCheckDefect.Rows.Count > 0 Then
                                        For d = 0 To dtCheckDefect.Rows.Count - 1
                                            sResult = funcDeleteReject(dtCheckDefect.Rows(d).Item("id"), dtCheckDefect.Rows(d).Item("part_number"), dtCheckDefect.Rows(d).Item("lot_no"), dtCheckDefect.Rows(d).Item("qty"), sFlowTicket(5))

                                            If sResult > 0 Then
                                                Dim queryUpdateOutDefect As String = "delete from out_prod_defect where id=" & dtCheckDefect.Rows(d).Item("id")
                                                Dim dtUpdateOutDefect = New SqlCommand(queryUpdateOutDefect, Database.koneksi)
                                                If dtUpdateOutDefect.ExecuteNonQuery() Then
                                                    Dim queryDeleteStockCardTemporary As String = "delete from stock_card_temporary where status='Production Process' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                                                    Dim dtDeleteStockCardTemporary = New SqlCommand(queryDeleteStockCardTemporary, Database.koneksi)
                                                    dtDeleteStockCardTemporary.ExecuteNonQuery()
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            Else
                                RJMessageBox.Show("this is not number -> " & DataGridView1.Rows(i).Cells(2).Value & ". Please change with number.")
                            End If
                        End If
                    Next

                    'If sResult > 0 Then
                    '    RJMessageBox.Show("Success Save data!!!")
                    '    loadFG(cbFGPN.Text, txtFGFlowTicket.Text)
                    'Else
                    '    RJMessageBox.Show("Fail Save data!!!")
                    '    loadFG(cbFGPN.Text, txtFGFlowTicket.Text)
                    'End If
                Catch ex As Exception
                    RJMessageBox.Show("ERDEF1-" & ex.Message)
                End Try
            End If
            'Return sResult
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If

        Return 0
    End Function

    Private Sub btnSaveFGDefect_Click(sender As Object, e As EventArgs) Handles btnSaveFGDefect.Click
        SaveFGDefact()
    End Sub

    Private Sub btnSaveFG_Click(sender As Object, e As EventArgs) Handles btnSaveFG.Click

        Try

            Dim spasiFlowTicket = txtFGFlowTicket.Text.Replace(ChrW(160), " ")
            Dim sFlowTicket = spasiFlowTicket.Split(";")
            Dim sFlowTicketSplitOf = sFlowTicket(5).Split(" of ")
            Dim AdaDefectFG = False
            Dim codeDefect As String = ""
            Dim showMessageBox As Boolean = True

            If globVar.add = 0 Then
                RJMessageBox.Show("Your Access cannot execute this action")
                Exit Sub
            End If

            If txtStatusSubSubPO.Text = "Closed" Then
                RJMessageBox.Show("Sorry This Sub Sub PO status is closed.")
                Exit Sub
            End If

            Dim queryCekFlowTicketDone As String = "select * from flow_ticket where done=1 and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and fg='" & cbFGPN.Text & "' and flow_ticket='" & sFlowTicket(5) & "'"
            Dim dtCekFlowTicketDone As DataTable = Database.GetData(queryCekFlowTicketDone)
            If dtCekFlowTicketDone.Rows.Count > 0 Then
                RJMessageBox.Show("Sorry this flow ticket already finish.")
                Exit Sub
            End If

            Dim result = RJMessageBox.Show("Are you sure for save Finish Goods. Cannot Change after Save?.", "Are You Sure?", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then

                Dim tampungQty As New Dictionary(Of String, Integer)()

                If DataGridView1.RowCount > 0 Then

                    For defectFG = 0 To DataGridView1.RowCount - 1

                        Dim TotalQtyCheck As Integer

                        If DataGridView1.Rows(defectFG).Cells("Defect Qty").Value <> 0 Then

                            AdaDefectFG = True

                            Dim queryMPF As String = "select * from master_process_flow where master_finish_goods_pn='" & cbFGPN.Text & "' and master_process='" & DataGridView1.Rows(defectFG).Cells(1).Value & "'"
                            Dim dtMPF As DataTable = Database.GetData(queryMPF)

                            Dim separatedMaterial As String() = dtMPF.Rows(0).Item("material_usage").Split(";"c)

                            For Each material As String In separatedMaterial

                                If Not String.IsNullOrEmpty(material) Then

                                    Dim queryMUFGCheck As String = "select * from MATERIAL_USAGE_FINISH_GOODS where FG_PART_NUMBER='" & cbFGPN.Text & "' and component='" & material & "'"
                                    Dim dtMUFGCheck As DataTable = Database.GetData(queryMUFGCheck)

                                    If dtMUFGCheck.Rows.Count = 0 Then
                                        RJMessageBox.Show("In this FG " & cbFGPN.Text & " cannot find Material " & material & ". Please check material usage finish goods.")
                                        Exit Sub
                                    End If

                                    TotalQtyCheck = dtMUFGCheck.Rows(0).Item("usage") * DataGridView1.Rows(defectFG).Cells("Defect Qty").Value

                                    If tampungQty.ContainsKey(material) Then

                                        tampungQty(material) += TotalQtyCheck

                                    Else

                                        tampungQty.Add(material, TotalQtyCheck)

                                    End If

                                End If

                            Next

                        End If

                    Next

                End If

                Dim queryMUFGCheck1 As String = "select * from MATERIAL_USAGE_FINISH_GOODS where FG_PART_NUMBER='" & cbFGPN.Text & "'"
                Dim dtMUFGCheck1 As DataTable = Database.GetData(queryMUFGCheck1)

                For i = 0 To dtMUFGCheck1.Rows.Count - 1

                    Dim TotalQtySaveFG = dtMUFGCheck1.Rows(i).Item("usage") * txtSPQ.Text
                    Dim TotalQty As Integer

                    If tampungQty.ContainsKey(dtMUFGCheck1.Rows(i).Item("component")) Then

                        TotalQty = TotalQtySaveFG + tampungQty(dtMUFGCheck1.Rows(i).Item("component"))

                    Else

                        TotalQty = TotalQtySaveFG

                    End If

                    Dim querySelectSC As String = "select 
                                                    sum(actual_qty) total
                                                from 
                                                    stock_card 
                                                where 
                                                    status = 'Production Process' 
                                                    AND department ='" & globVar.department & "'
                                                    AND material = '" & dtMUFGCheck1.Rows(i).Item("component") & "' 
                                                    AND sub_sub_po ='" & txtSubSubPODefective.Text & "'"
                    Dim dtSelectSC As DataTable = Database.GetData(querySelectSC)

                    If dtSelectSC.Rows(0).Item("total") < TotalQty Then

                        RJMessageBox.Show("Qty this material " & dtMUFGCheck1.Rows(i).Item("component") & " is not enough ")
                        Exit Sub

                    End If

                Next

                Dim queryMUFG As String = "select * from MATERIAL_USAGE_FINISH_GOODS where FG_PART_NUMBER='" & cbFGPN.Text & "'"
                Dim dtMUFG As DataTable = Database.GetData(queryMUFG)

                If dtMUFG.Rows.Count = 0 Then

                    RJMessageBox.Show("Data This Finish Goods Doesn't exist in Material Usage Finish Goods")
                    Exit Sub

                End If

                If AdaDefectFG Then

                    Dim queryCode As String = "select top 1 CODE_OUT_PROD_DEFECT from OUT_PROD_DEFECT ORDER BY cast(replace(CODE_OUT_PROD_DEFECT,'D','') as int) desc"
                    Dim dtCode As DataTable = Database.GetData(queryCode)
                    Dim match As Match = Regex.Match(dtCode.Rows(0).Item("CODE_OUT_PROD_DEFECT").ToString(), "^([A-Z]+)(\d+)$")
                    If match.Success Then
                        Dim prefix As String = match.Groups(1).Value
                        Dim number As Integer = Integer.Parse(match.Groups(2).Value)
                        Dim nextNumber As Integer = number + 1
                        codeDefect = prefix & nextNumber.ToString()
                    End If

                    For defectFG = 0 To DataGridView1.RowCount - 1

                        If DataGridView1.Rows(defectFG).Cells("Defect Qty").Value <> 0 Then

                            If IsNumeric(DataGridView1.Rows(defectFG).Cells("Defect Qty").Value) Then

                                If Convert.ToInt32(DataGridView1.Rows(defectFG).Cells("Defect Qty").Value) > 0 Then

                                    Dim query As String = "select * from master_process_flow where master_finish_goods_pn='" & cbFGPN.Text & "' and master_process='" & DataGridView1.Rows(defectFG).Cells(1).Value & "'"
                                    Dim dtMasterProcessFlow As DataTable = Database.GetData(query)
                                    Dim numberInt As Integer = dtMasterProcessFlow.Rows(0).Item("ID")
                                    If dtMasterProcessFlow.Rows.Count > 0 Then

                                        Dim materialUsageRAWDATA() As String = dtMasterProcessFlow.Rows(0).Item("MATERIAL_USAGE").Split(";"c)

                                        For Each materialUsage As String In materialUsageRAWDATA

                                            If Not String.IsNullOrEmpty(materialUsage) Then

                                                Dim queryGetUsage As String = "select [usage] from material_usage_finish_goods where fg_part_number='" & cbFGPN.Text & "' and component='" & materialUsage & "'"
                                                Dim dtGetUsage As DataTable = Database.GetData(queryGetUsage)

                                                Dim TotalUsage = dtGetUsage.Rows(0).Item("usage") * DataGridView1.Rows(defectFG).Cells("Defect Qty").Value

                                                Dim querySelectSC As String = "select 
                                                                                    * 
                                                                                from 
                                                                                    stock_card 
                                                                                where 
                                                                                    status = 'Production Process' 
	                                                                                AND department ='" & globVar.department & "'
	                                                                                AND material = '" & materialUsage & "' 
	                                                                                AND sub_sub_po ='" & txtSubSubPODefective.Text & "' 
                                                                                    and actual_qty > 0
                                                                                order by id"
                                                Dim dtSelectSC As DataTable = Database.GetData(querySelectSC)

                                                For i = 0 To dtSelectSC.Rows.Count - 1

                                                    Dim queryCheckSumDefect As String = "select isnull(sum(qty),0) as total from out_prod_defect where sub_sub_po='" & cbFGPN.Text & "' and part_number='" & materialUsage & "' and flow_ticket_no='" & sFlowTicket(5) & "'"
                                                    Dim dtCheckSumDefect As DataTable = Database.GetData(queryCheckSumDefect)

                                                    If TotalUsage <> dtCheckSumDefect.Rows(0).Item("total") Then

                                                        If dtSelectSC.Rows(i).Item("actual_qty") >= TotalUsage Then

                                                            Dim queryUpdateSCDefect As String = "update stock_card set actual_qty=actual_qty-" & TotalUsage & " where ID=" & dtSelectSC.Rows(i).Item("id")
                                                            Dim dtUpdateSCDefect = New SqlCommand(queryUpdateSCDefect, Database.koneksi)
                                                            dtUpdateSCDefect.ExecuteNonQuery()

                                                            Dim sqlInsertRejectPN As String = "INSERT INTO out_prod_defect (CODE_OUT_PROD_DEFECT, sub_sub_po, FG_PN, PART_NUMBER, LOT_NO, TRACEABILITY,INV_CTRL_DATE,
                                                            BATCH_NO,QTY,PO,PROCESS_REJECT,LINE,FLOW_TICKET_NO,DEPARTMENT,PENGALI,INPUT_FROM_FG,ACTUAL_QTY)
                                                            VALUES ('" & codeDefect & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & materialUsage & "','" & dtSelectSC.Rows(i).Item("lot_no") & "',
                                                            '" & dtSelectSC.Rows(i).Item("TRACEABILITY") & "','" & dtSelectSC.Rows(i).Item("INV_CTRL_DATE") & "','" & dtSelectSC.Rows(i).Item("BATCH_NO") & "',
                                                            " & TotalUsage & ",'" & cbPONumber.Text & "','" & DataGridView1.Rows(defectFG).Cells(1).Value & "','" & cbLineNumber.Text & "','" & sFlowTicket(5) & "',
                                                            '" & globVar.department & "'," & DataGridView1.Rows(defectFG).Cells("Defect Qty").Value & ",1," & TotalUsage & ")"
                                                            Dim cmdInsertRejectPN = New SqlCommand(sqlInsertRejectPN, Database.koneksi)
                                                            cmdInsertRejectPN.ExecuteNonQuery()

                                                            Exit For

                                                        Else

                                                            Dim queryUpdateSCDefect As String = "update stock_card set actual_qty=0 where ID=" & dtSelectSC.Rows(i).Item("id")
                                                            Dim dtUpdateSCDefect = New SqlCommand(queryUpdateSCDefect, Database.koneksi)
                                                            dtUpdateSCDefect.ExecuteNonQuery()

                                                            Dim sqlInsertRejectPN As String = "INSERT INTO out_prod_defect (CODE_OUT_PROD_DEFECT, sub_sub_po, FG_PN, PART_NUMBER, LOT_NO, TRACEABILITY,INV_CTRL_DATE,
                                                            BATCH_NO,QTY,PO,PROCESS_REJECT,LINE,FLOW_TICKET_NO,DEPARTMENT,PENGALI,INPUT_FROM_FG,ACTUAL_QTY)
                                                            VALUES ('" & codeDefect & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & materialUsage & "','" & dtSelectSC.Rows(i).Item("lot_no") & "',
                                                            '" & dtSelectSC.Rows(i).Item("TRACEABILITY") & "','" & dtSelectSC.Rows(i).Item("INV_CTRL_DATE") & "','" & dtSelectSC.Rows(i).Item("BATCH_NO") & "',
                                                            " & TotalUsage & ",'" & cbPONumber.Text & "','" & DataGridView1.Rows(defectFG).Cells(1).Value & "','" & cbLineNumber.Text & "','" & sFlowTicket(5) & "',
                                                            '" & globVar.department & "'," & DataGridView1.Rows(defectFG).Cells("Defect Qty").Value & ",1," & TotalUsage & ")"
                                                            Dim cmdInsertRejectPN = New SqlCommand(sqlInsertRejectPN, Database.koneksi)
                                                            cmdInsertRejectPN.ExecuteNonQuery()

                                                        End If

                                                    End If

                                                Next

                                            End If

                                        Next

                                    End If

                                End If

                            Else

                                RJMessageBox.Show("this is not number -> " & DataGridView1.Rows(defectFG).Cells(2).Value & ". Please change with number.")

                            End If

                        End If

                    Next

                    If CheckBoxFGDefect.CheckState = CheckState.Checked Then

                        Dim queryCheckdefect As String = "select DISTINCT(CODE_OUT_PROD_DEFECT) [Code],flow_ticket_no [Flow Ticket],fg_pn [Finish Goods], Line from out_prod_DEFECT where sub_sub_po='" & txtSubSubPODefective.Text & "'"
                        Dim dtCheckDEFECT As DataTable = Database.GetData(queryCheckdefect)
                        If dtCheckDEFECT.Rows.Count > 0 Then

                            For i = 0 To dtCheckDEFECT.Rows.Count - 1

                                globVar.failPrint = ""
                                Dim parts As String() = txtSubSubPODefective.Text.Split("-"c)

                                _PrintingDefect.txt_QR_Code.Text = dtCheckDEFECT.Rows(i).Item("Code")
                                _PrintingDefect.Label2.Text = "Defect Ticket"
                                _PrintingDefect.txt_Unique_id.Text = dtCheckDEFECT.Rows(i).Item("Code")
                                _PrintingDefect.txt_part_number.Text = dtCheckDEFECT.Rows(i).Item("Finish Goods")
                                _PrintingDefect.txt_Part_Description.Text = txtDescDefective.Text
                                _PrintingDefect.txt_Traceability.Text = parts(0)
                                _PrintingDefect.txt_Inv_crtl_date.Text = txtINV.Text
                                _PrintingDefect.btn_Print_Click(sender, e)

                                If globVar.failPrint = "No" Then

                                    Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, remark,sub_sub_po,department,flow_ticket,code_print)
                                    VALUES ('" & parts(0) & "','" & dtCheckDEFECT.Rows(i).Item("Finish Goods") & "','" & dtCheckDEFECT.Rows(i).Item("Line") & "','Defect Material','" & txtSubSubPODefective.Text & "','" & globVar.department & "','" & dtCheckDEFECT.Rows(i).Item("Flow Ticket") & "','" & dtCheckDEFECT.Rows(i).Item("Code") & "')"
                                    Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                                    cmdInsertPrintingRecord.ExecuteNonQuery()

                                    Dim sqlupdateDefect As String = "update out_prod_defect set [print]=1 where CODE_OUT_PROD_DEFECT='" & dtCheckDEFECT.Rows(i).Item("Code") & "'"
                                    Dim cmdupdateDefect = New SqlCommand(sqlupdateDefect, Database.koneksi)
                                    cmdupdateDefect.ExecuteNonQuery()

                                End If

                            Next

                        End If

                    End If

                End If

                For ii = 0 To dtMUFG.Rows.Count - 1

                    Dim TotalQtySave = dtMUFG.Rows(ii).Item("usage") * Convert.ToInt64(txtSPQ.Text)

                    Dim querySelectSC As String = "select 
                                                    * 
                                                from 
                                                    stock_card 
                                                where 
                                                    status = 'Production Process' 
	                                                AND department ='" & globVar.department & "'
	                                                AND material = '" & dtMUFG.Rows(ii).Item("component") & "' 
	                                                AND sub_sub_po ='" & txtSubSubPODefective.Text & "' 
                                                    and actual_qty > 0
                                                order by id"
                    Dim dtSelectSC As DataTable = Database.GetData(querySelectSC)

                    For i = 0 To dtSelectSC.Rows.Count - 1

                        Dim querySelectResult As String = "select 
                                                                isnull(sum(qty),0) totalResult
                                                            from 
                                                                stock_card 
                                                            where 
                                                                status = 'Production Result' 
	                                                            AND department ='" & globVar.department & "'
	                                                            AND material = '" & dtMUFG.Rows(ii).Item("component") & "' 
	                                                            AND sub_sub_po ='" & txtSubSubPODefective.Text & "'
                                                                and flow_ticket = '" & sFlowTicket(5) & "'"

                        Dim dtSelectResult As DataTable = Database.GetData(querySelectResult)

                        If dtSelectResult.Rows(0).Item("totalResult") <> TotalQtySave Then

                            Dim totalPenguranganYangAda = TotalQtySave - dtSelectResult.Rows(0).Item("totalResult")

                            Dim totalPenguranganSCProcess = dtSelectSC.Rows(i).Item("actual_qty") - totalPenguranganYangAda

                            If dtSelectSC.Rows(i).Item("actual_qty") > totalPenguranganYangAda Then

                                Dim sqlInsertSubAssyResult As String = "insert into stock_card(
                                                                [MTS_NO],[DEPARTMENT],[MATERIAL],[STATUS],[STANDARD_PACK],
                                                                [INV_CTRL_DATE],[TRACEABILITY],[BATCH_NO],[LOT_NO],[FINISH_GOODS_PN],
                                                                [PO],[SUB_PO],[SUB_SUB_PO],[LINE],[QRCODE],[QRCODE_NEW],[QTY],[ACTUAL_QTY],
                                                                [ID_LEVEL],[LEVEL],[FLOW_TICKET], [INSERT_WHO]) 
                                                            select top 1
                                                                [MTS_NO],[DEPARTMENT],[MATERIAL],'Production Result',[STANDARD_PACK],
                                                                [INV_CTRL_DATE],[TRACEABILITY],[BATCH_NO],[LOT_NO],[FINISH_GOODS_PN],
                                                                [PO],[SUB_PO],[SUB_SUB_PO],[LINE],[QRCODE],[QRCODE_NEW]," & totalPenguranganYangAda & ",
                                                                " & totalPenguranganYangAda & ",[ID_LEVEL],'FG','" & sFlowTicket(5) & "','" & globVar.username & "' 
                                                            from 
                                                                stock_card 
                                                            where 
                                                                id = " & dtSelectSC.Rows(i).Item("ID")

                                Dim cmdInsertSubAssyResult = New SqlCommand(sqlInsertSubAssyResult, Database.koneksi)
                                If cmdInsertSubAssyResult.ExecuteNonQuery() Then

                                    Dim queryUpdateSCProductionProcess As String = "update stock_card set actual_qty=" & totalPenguranganSCProcess & " where id=" & dtSelectSC.Rows(i).Item("id")

                                    Dim dtUpdateSCProductionProcess = New SqlCommand(queryUpdateSCProductionProcess, Database.koneksi)
                                    dtUpdateSCProductionProcess.ExecuteNonQuery()

                                    Dim queryUpdateFlowTicket As String = "update flow_ticket set done=1,datetime_done=getdate() where DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and fg='" & cbFGPN.Text & "' and flow_ticket='" & sFlowTicket(5) & "'"
                                    Dim dtUpdateFlowTicket = New SqlCommand(queryUpdateFlowTicket, Database.koneksi)
                                    If dtUpdateFlowTicket.ExecuteNonQuery() Then

                                        If showMessageBox Then

                                            Dim sqlInsertDoneFG As String = "INSERT INTO done_fg (po, sub_sub_po, FG ,FLOW_TICKET,DEPARTMENT,laser_code, LOT_NO, qty,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,line)
                                            VALUES ('" & cbPONumber.Text & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & sFlowTicket(5) & "','" & globVar.department & "',
                                            '" & TextBox3.Text & "','" & sFlowTicketSplitOf(0) & "'," & txtSPQ.Text & ",'" & txtTampungLabel.Text & "','" & txtINV.Text & "','" & txtBatchno.Text & "','" & cbLineNumber.Text & "')"
                                            Dim cmdInsertDoneFG = New SqlCommand(sqlInsertDoneFG, Database.koneksi)
                                            cmdInsertDoneFG.ExecuteNonQuery()

                                            RJMessageBox.Show("Success Save Finish Goods data!!!")
                                            showMessageBox = False

                                        End If

                                        ClearInputFG()
                                        UpdateQtySubSubPO()

                                        Exit For

                                    Else

                                        RJMessageBox.Show("Fail Save Finish Goods data!")

                                    End If

                                End If

                            Else

                                Dim sqlInsertSubAssyResult As String = "insert into stock_card(
                                                                [MTS_NO],[DEPARTMENT],[MATERIAL],[STATUS],[STANDARD_PACK],
                                                                [INV_CTRL_DATE],[TRACEABILITY],[BATCH_NO],[LOT_NO],[FINISH_GOODS_PN],
                                                                [PO],[SUB_PO],[SUB_SUB_PO],[LINE],[QRCODE],[QRCODE_NEW],[QTY],[ACTUAL_QTY],
                                                                [ID_LEVEL],[LEVEL],[FLOW_TICKET], [INSERT_WHO]) 
                                                            select top 1
                                                                [MTS_NO],[DEPARTMENT],[MATERIAL],'Production Result',[STANDARD_PACK],
                                                                [INV_CTRL_DATE],[TRACEABILITY],[BATCH_NO],[LOT_NO],[FINISH_GOODS_PN],
                                                                [PO],[SUB_PO],[SUB_SUB_PO],[LINE],[QRCODE],[QRCODE_NEW]," & dtSelectSC.Rows(i).Item("actual_qty") & ",
                                                                " & dtSelectSC.Rows(i).Item("actual_qty") & ",[ID_LEVEL],'FG','" & sFlowTicket(5) & "','" & globVar.username & "' 
                                                            from 
                                                                stock_card 
                                                            where 
                                                                id = " & dtSelectSC.Rows(i).Item("ID")

                                Dim cmdInsertSubAssyResult = New SqlCommand(sqlInsertSubAssyResult, Database.koneksi)
                                If cmdInsertSubAssyResult.ExecuteNonQuery() Then

                                    Dim queryUpdateSCProductionProcess As String = "update stock_card set actual_qty=0 where id=" & dtSelectSC.Rows(i).Item("id")
                                    Dim dtUpdateSCProductionProcess = New SqlCommand(queryUpdateSCProductionProcess, Database.koneksi)
                                    dtUpdateSCProductionProcess.ExecuteNonQuery()

                                End If

                            End If

                        End If

                    Next

                Next

            End If

        Catch ex As Exception

            RJMessageBox.Show("Error Save Finish Goods " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Sub UpdateQtySubSubPO()
        Try
            If rbFG.Checked = True Then
                Dim queryCheckDoneFG As String = "select isnull(sum(qty),0) sum_qty from done_fg where DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and fg='" & cbFGPN.Text & "'"
                Dim dtCheckDoneFG As DataTable = Database.GetData(queryCheckDoneFG)
                If dtCheckDoneFG.Rows.Count > 0 Then
                    Dim querySelectSubsubpo As String = "select * from sub_sub_po where sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "'"
                    Dim dtSelectSubsubpo As DataTable = Database.GetData(querySelectSubsubpo)
                    If dtSelectSubsubpo.Rows.Count > 0 Then

                        Dim queryUpdateSubsubpo As String = "update sub_sub_po set actual_qty=" & dtCheckDoneFG.Rows(0).Item("sum_qty") & " where id=" & dtSelectSubsubpo.Rows(0).Item("id")
                        Dim dtUpdateSubsubpo = New SqlCommand(queryUpdateSubsubpo, Database.koneksi)
                        dtUpdateSubsubpo.ExecuteNonQuery()

                        Dim queryCheckQtySubsubpo As String = "select isnull(sum(actual_qty),0) sum_qty from sub_sub_po where main_po=" & dtSelectSubsubpo.Rows(0).Item("main_po")
                        Dim dtCheckQtySubsubpo As DataTable = Database.GetData(queryCheckQtySubsubpo)
                        If dtCheckQtySubsubpo.Rows.Count > 0 Then
                            Dim querySelectMainPO As String = "select * from main_po where id=" & dtSelectSubsubpo.Rows(0).Item("main_po")
                            Dim dtSelectMainPO As DataTable = Database.GetData(querySelectMainPO)
                            If dtSelectMainPO.Rows.Count > 0 Then
                                If Convert.ToInt16(dtSelectMainPO.Rows(0).Item("sub_po_qty")) <= Convert.ToInt16(dtCheckQtySubsubpo.Rows(0).Item("sum_qty")) Then
                                    'Dim queryUpdateMainPO As String = "update main_po set actual_qty=" & dtCheckDoneFG.Rows(0).Item("sum_qty") & ", status='Closed' where id=" & dtSelectSubsubpo.Rows(0).Item("main_po")
                                    Dim queryUpdateMainPO As String = "update main_po set actual_qty=" & dtCheckDoneFG.Rows(0).Item("sum_qty") & " where id=" & dtSelectSubsubpo.Rows(0).Item("main_po")
                                    Dim dtUpdateMainPO = New SqlCommand(queryUpdateMainPO, Database.koneksi)
                                    dtUpdateMainPO.ExecuteNonQuery()
                                Else
                                    Dim queryUpdateMainPO As String = "update main_po set actual_qty=" & dtCheckQtySubsubpo.Rows(0).Item("sum_qty") & " where id=" & dtSelectSubsubpo.Rows(0).Item("main_po")
                                    Dim dtUpdateMainPO = New SqlCommand(queryUpdateMainPO, Database.koneksi)
                                    dtUpdateMainPO.ExecuteNonQuery()
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            If rbSA.Checked = True Then
                Dim queryCheckDoneFG As String = "select isnull(sum(qty),0) sum_qty from stock_prod_sub_assy where DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and fg='" & cbFGPN.Text & "'"
                Dim dtCheckDoneFG As DataTable = Database.GetData(queryCheckDoneFG)
                If dtCheckDoneFG.Rows.Count > 0 Then
                    Dim querySelectSubsubpo As String = "select * from sub_sub_po where sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "'"
                    Dim dtSelectSubsubpo As DataTable = Database.GetData(querySelectSubsubpo)
                    If dtSelectSubsubpo.Rows.Count > 0 Then

                        Dim queryUpdateSubsubpo As String = "update sub_sub_po set actual_qty=" & dtCheckDoneFG.Rows(0).Item("sum_qty") & " where id=" & dtSelectSubsubpo.Rows(0).Item("id")
                        Dim dtUpdateSubsubpo = New SqlCommand(queryUpdateSubsubpo, Database.koneksi)
                        dtUpdateSubsubpo.ExecuteNonQuery()

                        Dim queryCheckQtySubsubpo As String = "select isnull(sum(actual_qty),0) sum_qty from sub_sub_po where main_po=" & dtSelectSubsubpo.Rows(0).Item("main_po")
                        Dim dtCheckQtySubsubpo As DataTable = Database.GetData(queryCheckQtySubsubpo)
                        If dtCheckQtySubsubpo.Rows.Count > 0 Then
                            Dim querySelectMainPO As String = "select * from main_po where id=" & dtSelectSubsubpo.Rows(0).Item("main_po")
                            Dim dtSelectMainPO As DataTable = Database.GetData(querySelectMainPO)
                            If dtSelectMainPO.Rows.Count > 0 Then

                                Dim queryUpdateMainPO As String = "update main_po set actual_qty=" & dtCheckQtySubsubpo.Rows(0).Item("sum_qty") & " where id=" & dtSelectSubsubpo.Rows(0).Item("main_po")
                                Dim dtUpdateMainPO = New SqlCommand(queryUpdateMainPO, Database.koneksi)
                                dtUpdateMainPO.ExecuteNonQuery()

                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSaveSADefect_Click(sender As Object, e As EventArgs) Handles btnSaveSADefect.Click
        If globVar.add > 0 Then
            If DataGridView3.Rows.Count > 0 Then
                If txtStatusSubSubPO.Text = "Closed" Then
                    RJMessageBox.Show("Sorry This Sub Sub PO status is closed. Cannot use this menu.")
                    Exit Sub
                End If

                Dim sResult As Integer = 0
                Dim sFlowTicket = txtSAFlowTicket.Text.Split(";")
                Dim sFlow_Ticket5 = sFlowTicket(5).Split(" of ")

                Dim queryCheckDoneFG As String = "select * from STOCK_PROD_SUB_ASSY where DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and fg='" & cbFGPN.Text & "' and flow_ticket='" & sFlowTicket(5) & "' and lot_no='" & sFlow_Ticket5(0) & "'"
                Dim dtCheckDoneFG As DataTable = Database.GetData(queryCheckDoneFG)
                If dtCheckDoneFG.Rows.Count > 0 Then
                    RJMessageBox.Show("Sorry this Flow Ticket already Done. Cannot Save.")
                    ClearInputFG()
                    Exit Sub
                End If

                Try
                    For i = 0 To DataGridView3.Rows.Count - 1
                        Dim doWhile As String = ""
                        If DataGridView3.Rows(i).Cells(2).Value IsNot "" And DataGridView3.Rows(i).Cells(2).Value IsNot Nothing And DataGridView3.Rows(i).Cells(2).Value IsNot DBNull.Value Then
                            If IsNumeric(DataGridView3.Rows(i).Cells(2).Value) Then
                                If Convert.ToInt32(DataGridView3.Rows(i).Cells(2).Value) > 0 Then
                                    Dim query As String = "select * from master_process_flow where master_process='" & DataGridView3.Rows(i).Cells(1).Value & "' and master_finish_goods_pn='" & cbFGPN.Text & "'"
                                    Dim dtMasterProcessFlow As DataTable = Database.GetData(query)
                                    Dim numberInt As Integer = dtMasterProcessFlow.Rows(0).Item("ID")
                                    If dtMasterProcessFlow.Rows.Count > 0 Then
                                        If IsDBNull(dtMasterProcessFlow.Rows(0).Item("MATERIAL_USAGE")) = False Then
                                            sResult = funcInsertReject(dtMasterProcessFlow.Rows(0).Item("MATERIAL_USAGE"), DataGridView3.Rows(i).Cells(2).Value, DataGridView3.Rows(i).Cells(1).Value, 0, txtSAFlowTicket.Text)
                                        Else
                                            Dim queryKosong As String = "select * from master_process_flow where master_finish_goods_pn='" & cbFGPN.Text & "' order by id"
                                            Dim dtKosong As DataTable = Database.GetData(queryKosong)
                                            Do While doWhile Is ""
                                                Dim queryGetMaterialUsage As String = "select * from master_process_flow where ID=" & numberInt
                                                Dim dtKosongGetMaterialUsage As DataTable = Database.GetData(queryGetMaterialUsage)
                                                If numberInt <= dtKosong.Rows(0).Item("ID") Then
                                                    doWhile = "Kosong-" & DataGridView3.Rows(i).Cells(1).Value
                                                    DataGridView3.Rows(i).Cells(2).Value = 0
                                                Else
                                                    If IsDBNull(dtKosongGetMaterialUsage.Rows(0).Item("MATERIAL_USAGE")) = False Then
                                                        doWhile = dtKosongGetMaterialUsage.Rows(0).Item("MATERIAL_USAGE")
                                                    Else
                                                        numberInt = dtKosongGetMaterialUsage.Rows(0).Item("ID") - 1
                                                    End If
                                                End If
                                            Loop
                                            sResult = funcInsertReject(doWhile, DataGridView3.Rows(i).Cells(2).Value, DataGridView3.Rows(i).Cells(1).Value, 0, txtSAFlowTicket.Text)
                                        End If
                                    End If
                                Else
                                    Dim queryCheckDefect As String = "select * from out_prod_defect where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and process_reject='" & DataGridView3.Rows(i).Cells(1).Value & "' and flow_ticket_no='" & sFlowTicket(5) & "' and department='" & globVar.department & "' and pengali > 0 and input_from_fg=0"
                                    Dim dtCheckDefect As DataTable = Database.GetData(queryCheckDefect)
                                    If dtCheckDefect.Rows.Count > 0 Then
                                        For d = 0 To dtCheckDefect.Rows.Count - 1
                                            sResult = funcDeleteReject(dtCheckDefect.Rows(d).Item("id"), dtCheckDefect.Rows(d).Item("part_number"), dtCheckDefect.Rows(d).Item("lot_no"), dtCheckDefect.Rows(d).Item("qty"), sFlowTicket(5))

                                            If sResult > 0 Then
                                                Dim queryUpdateOutDefect As String = "delete from out_prod_defect where id=" & dtCheckDefect.Rows(d).Item("id")
                                                Dim dtUpdateOutDefect = New SqlCommand(queryUpdateOutDefect, Database.koneksi)
                                                If dtUpdateOutDefect.ExecuteNonQuery() Then
                                                    Dim queryDeleteStockCardTemporary As String = "delete from stock_card_temporary where status='Production Process' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                                                    Dim dtDeleteStockCardTemporary = New SqlCommand(queryDeleteStockCardTemporary, Database.koneksi)
                                                    dtDeleteStockCardTemporary.ExecuteNonQuery()
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            Else
                                RJMessageBox.Show("this is not number -> " & DataGridView3.Rows(i).Cells(2).Value & ". Please change with number.")
                            End If
                        End If
                    Next

                    If sResult > 0 Then
                        RJMessageBox.Show("Success Save data!!!")
                        loadSA(cbFGPN.Text, txtSAFlowTicket.Text)
                    Else
                        RJMessageBox.Show("Fail Save data!!!")
                    End If

                Catch ex As Exception
                    RJMessageBox.Show("Error - ERDEF2" & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Public Sub BandingkanJam(ByVal jamSekarang As TimeSpan)
        If jamSekarang >= globVar.shift1Awal AndAlso jamSekarang <= globVar.shift1Akhir Then
            globVar.shift = "1"
        ElseIf jamSekarang >= globVar.shift2Awal AndAlso jamSekarang <= globVar.shift2Akhir Then
            globVar.shift = "2"
        ElseIf jamSekarang >= globVar.shift3Awal Or jamSekarang <= globVar.shift3Akhir Then
            globVar.shift = "3"
        End If
    End Sub

    Private Sub btnSaveSA_Click(sender As Object, e As EventArgs) Handles btnSaveSA.Click

        Try

            Dim spasiFlowTicket = txtSAFlowTicket.Text.Replace(ChrW(160), " ")
            Dim sFlowTicket = spasiFlowTicket.Split(";")
            Dim sFlowTicketSplitOf = sFlowTicket(5).Split(" of ")
            Dim AdaDefectSA = False
            Dim codeSubassy As String = ""
            Dim IlostQty As Integer = 0
            Dim showMessageBox As Boolean = True

            If globVar.add = 0 Then
                RJMessageBox.Show("Your Access cannot execute this action")
                Exit Sub
            End If

            If txtStatusSubSubPO.Text = "Closed" Then
                RJMessageBox.Show("Sorry This Sub Sub PO status is closed.")
                Exit Sub
            End If

            Dim queryCekFlowTicketDone As String = "select * from flow_ticket where done=1 and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and fg='" & cbFGPN.Text & "' and flow_ticket='" & sFlowTicket(5) & "'"
            Dim dtCekFlowTicketDone As DataTable = Database.GetData(queryCekFlowTicketDone)
            If dtCekFlowTicketDone.Rows.Count > 0 Then
                RJMessageBox.Show("Sorry this flow ticket already finish.")
                Exit Sub
            End If

            If DataGridView3.RowCount > 0 Then
                For defectSA = 0 To DataGridView3.RowCount - 1
                    If DataGridView3.Rows(defectSA).Cells("Defect Qty").Value <> 0 Then
                        AdaDefectSA = True
                    End If
                Next
            End If

            If AdaDefectSA Then
                RJMessageBox.Show("Sorry Save Defect Sub Assy??")
                Exit Sub
            End If

            IlostQty = txtLossQty.Text

            Dim queryCheckmaterial_usage_finish_goods As String = "select * from material_usage_finish_goods where fg_part_number='" & cbFGPN.Text & "'"
            Dim dtqueryCheckmaterial_usage_finish_goods As DataTable = Database.GetData(queryCheckmaterial_usage_finish_goods)

            If dtqueryCheckmaterial_usage_finish_goods.Rows.Count = 0 Then

                RJMessageBox.Show("Material Usage Finish Goods still empty")
                Exit Sub

            End If

            Dim TotalQtyCheck As Integer

            For i = 0 To dtqueryCheckmaterial_usage_finish_goods.Rows.Count - 1

                TotalQtyCheck = dtqueryCheckmaterial_usage_finish_goods.Rows(i).Item("usage") * txtLossQty.Text

                Dim querySelectSC As String = "select 
                                                sum(actual_qty) total
                                            from 
                                                stock_card 
                                            where 
                                                status = 'Production Process' 
                                                AND department ='" & globVar.department & "'
                                                AND material = '" & dtqueryCheckmaterial_usage_finish_goods.Rows(i).Item("component") & "' 
                                                AND sub_sub_po ='" & txtSubSubPODefective.Text & "'"
                Dim dtSelectSC As DataTable = Database.GetData(querySelectSC)

                If dtSelectSC.Rows(0).Item("total") < TotalQtyCheck Then

                    RJMessageBox.Show("Qty is not enough")
                    Exit Sub

                End If

            Next

            Dim queryCode As String = "select top 1 CODE_STOCK_PROD_SUB_ASSY from stock_prod_sub_assy ORDER BY cast(replace(CODE_STOCK_PROD_SUB_ASSY,'SA','') as int) desc"
            Dim dtCode As DataTable = Database.GetData(queryCode)
            Dim match As Match = Regex.Match(dtCode.Rows(0).Item("CODE_STOCK_PROD_SUB_ASSY").ToString(), "^([A-Z]+)(\d+)$")
            If match.Success Then
                Dim prefix As String = match.Groups(1).Value
                Dim number As Integer = Integer.Parse(match.Groups(2).Value)
                Dim nextNumber As Integer = number + 1
                codeSubassy = prefix & nextNumber.ToString()
            End If

            Dim dateString As String = Now.ToString("yyyyMMdd")

            Dim queryCheckDoneFG As String = "select * from stock_prod_sub_assy where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg='" & cbFGPN.Text & "' and FLOW_TICKET='" & sFlowTicket(5) & "' and department='" & globVar.department & "'"
            Dim dtCheckDoneFG As DataTable = Database.GetData(queryCheckDoneFG)
            If dtCheckDoneFG.Rows.Count = 0 Then

                Dim queryMUFG As String = "select * from MATERIAL_USAGE_FINISH_GOODS where FG_PART_NUMBER='" & cbFGPN.Text & "'"
                Dim dtMUFG As DataTable = Database.GetData(queryMUFG)

                If dtMUFG.Rows.Count = 0 Then
                    RJMessageBox.Show("Data This Finish Goods Doesn't exist in Material Usage Finish Goods")
                    Exit Sub
                End If

                If dtMUFG.Rows.Count = 1 Then

                    Dim querySelectSC As String = "select 
                                                        * 
                                                    from 
                                                        stock_card 
                                                    where 
                                                        status = 'Production Process' 
	                                                    AND department ='" & globVar.department & "'
	                                                    AND material = '" & dtMUFG.Rows(0).Item("component") & "' 
	                                                    AND sub_sub_po ='" & txtSubSubPODefective.Text & "' 
                                                        and actual_qty > 0
                                                    order by id"

                    Dim dtSelectSC As DataTable = Database.GetData(querySelectSC)

                    For i = 0 To dtSelectSC.Rows.Count - 1

                        Dim querySelectResult As String = "select 
                                                                isnull(sum(qty),0) totalResult
                                                            from 
                                                                stock_card 
                                                            where 
                                                                status = 'Production Result' 
	                                                            AND department ='" & globVar.department & "'
	                                                            AND material = '" & dtMUFG.Rows(0).Item("component") & "' 
	                                                            AND sub_sub_po ='" & txtSubSubPODefective.Text & "'
                                                                and flow_ticket = '" & sFlowTicket(5) & "'"

                        Dim dtSelectResult As DataTable = Database.GetData(querySelectResult)

                        If dtSelectResult.Rows(0).Item("totalResult") <> IlostQty Then

                            Dim totalPenguranganYangAda = IlostQty - dtSelectResult.Rows(0).Item("totalResult")

                            Dim totalPenguranganSCProcess = dtSelectSC.Rows(i).Item("actual_qty") - totalPenguranganYangAda

                            If dtSelectSC.Rows(i).Item("actual_qty") > totalPenguranganYangAda Then

                                Dim sqlInsertSubAssyResult As String = "insert into stock_card(
                                                                [MTS_NO],[DEPARTMENT],[MATERIAL],[STATUS],[STANDARD_PACK],
                                                                [INV_CTRL_DATE],[TRACEABILITY],[BATCH_NO],[LOT_NO],[FINISH_GOODS_PN],
                                                                [PO],[SUB_PO],[SUB_SUB_PO],[LINE],[QRCODE],[QRCODE_SA],[QTY],[ACTUAL_QTY],
                                                                [ID_LEVEL],[LEVEL],[FLOW_TICKET], [INSERT_WHO]) 
                                                            select top 1
                                                                [MTS_NO],[DEPARTMENT],[MATERIAL],'Production Result',[STANDARD_PACK],
                                                                [INV_CTRL_DATE],[TRACEABILITY],[BATCH_NO],[LOT_NO],[FINISH_GOODS_PN],
                                                                [PO],[SUB_PO],[SUB_SUB_PO],[LINE],[QRCODE],'" & codeSubassy & "'," & totalPenguranganYangAda & ",
                                                                " & totalPenguranganYangAda & ",[ID_LEVEL],'SA','" & sFlowTicket(5) & "','" & globVar.username & "' 
                                                            from 
                                                                stock_card 
                                                            where 
                                                                id = " & dtSelectSC.Rows(i).Item("ID")

                                Dim cmdInsertSubAssyResult = New SqlCommand(sqlInsertSubAssyResult, Database.koneksi)
                                If cmdInsertSubAssyResult.ExecuteNonQuery() Then

                                    Dim queryCheckSA As String = "select * from stock_prod_sub_assy where code_stock_prod_sub_assy='" & codeSubassy & "'"
                                    Dim dtCheckSA As DataTable = Database.GetData(queryCheckSA)
                                    If dtCheckSA.Rows.Count = 0 Then

                                        Dim sqlInsertSubAssy As String = "INSERT INTO stock_prod_sub_assy (code_stock_prod_sub_assy, po, sub_sub_po, FG ,FLOW_TICKET,DEPARTMENT,laser_code, LOT_NO, qty,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,LINE)
                                                VALUES ('" & codeSubassy & "','" & cbPONumber.Text & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & sFlowTicket(5) & "','" & globVar.department & "',
                                                '" & TextBox6.Text & "','" & sFlowTicketSplitOf(0) & "'," & IlostQty & ",'" & cbPONumber.Text & "','" & dateString & "','" & txtSABatchNo.Text & "','" & cbLineNumber.Text & "')"
                                        Dim cmdInsertSubAssy = New SqlCommand(sqlInsertSubAssy, Database.koneksi)
                                        cmdInsertSubAssy.ExecuteNonQuery()

                                    End If


                                    Dim queryUpdateSCProductionProcess As String = "update stock_card set actual_qty=" & totalPenguranganSCProcess & " where id = " & dtSelectSC.Rows(i).Item("id")
                                    Dim dtUpdateSCProductionProcess = New SqlCommand(queryUpdateSCProductionProcess, Database.koneksi)
                                    dtUpdateSCProductionProcess.ExecuteNonQuery()

                                    Dim queryUpdateFlowTicket As String = "update flow_ticket set done=1,datetime_done=getdate() where DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and fg='" & cbFGPN.Text & "' and flow_ticket='" & sFlowTicket(5) & "'"
                                    Dim dtUpdateFlowTicket = New SqlCommand(queryUpdateFlowTicket, Database.koneksi)
                                    If dtUpdateFlowTicket.ExecuteNonQuery() Then

                                        If showMessageBox Then
                                            RJMessageBox.Show("Success Save Sub Assy data!!!")
                                            showMessageBox = False
                                        End If

                                        ClearInputFG()
                                        UpdateQtySubSubPO()

                                    Else

                                        RJMessageBox.Show("Fail Save Sub Assy data!")

                                    End If

                                End If

                            Else

                                Dim sqlInsertSubAssyResult As String = "insert into stock_card(
                                                                [MTS_NO],[DEPARTMENT],[MATERIAL],[STATUS],[STANDARD_PACK],
                                                                [INV_CTRL_DATE],[TRACEABILITY],[BATCH_NO],[LOT_NO],[FINISH_GOODS_PN],
                                                                [PO],[SUB_PO],[SUB_SUB_PO],[LINE],[QRCODE],[QRCODE_SA],[QTY],[ACTUAL_QTY],
                                                                [ID_LEVEL],[LEVEL],[FLOW_TICKET], [INSERT_WHO]) 
                                                            select top 1
                                                                [MTS_NO],[DEPARTMENT],[MATERIAL],'Production Result',[STANDARD_PACK],
                                                                [INV_CTRL_DATE],[TRACEABILITY],[BATCH_NO],[LOT_NO],[FINISH_GOODS_PN],
                                                                [PO],[SUB_PO],[SUB_SUB_PO],[LINE],[QRCODE],'" & codeSubassy & "'," & dtSelectSC.Rows(i).Item("actual_qty") & ",
                                                                " & dtSelectSC.Rows(i).Item("actual_qty") & ",[ID_LEVEL],'SA','" & sFlowTicket(5) & "','" & globVar.username & "' 
                                                            from 
                                                                stock_card 
                                                            where 
                                                                id = " & dtSelectSC.Rows(i).Item("ID")

                                Dim cmdInsertSubAssyResult = New SqlCommand(sqlInsertSubAssyResult, Database.koneksi)
                                If cmdInsertSubAssyResult.ExecuteNonQuery() Then

                                    Dim queryCheckSA As String = "select * from stock_prod_sub_assy where code_stock_prod_sub_assy='" & codeSubassy & "'"
                                    Dim dtCheckSA As DataTable = Database.GetData(queryCheckSA)
                                    If dtCheckSA.Rows.Count = 0 Then

                                        Dim sqlInsertSubAssy As String = "INSERT INTO stock_prod_sub_assy (code_stock_prod_sub_assy, po, sub_sub_po, FG ,FLOW_TICKET,DEPARTMENT,laser_code, LOT_NO, qty,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,LINE)
                                                VALUES ('" & codeSubassy & "','" & cbPONumber.Text & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & sFlowTicket(5) & "','" & globVar.department & "',
                                                '" & TextBox6.Text & "','" & sFlowTicketSplitOf(0) & "'," & IlostQty & ",'" & cbPONumber.Text & "','" & dateString & "','" & txtSABatchNo.Text & "','" & cbLineNumber.Text & "')"
                                        Dim cmdInsertSubAssy = New SqlCommand(sqlInsertSubAssy, Database.koneksi)
                                        cmdInsertSubAssy.ExecuteNonQuery()

                                    End If

                                    Dim queryUpdateSCProductionProcess As String = "update stock_card set actual_qty=0 where id = " & dtSelectSC.Rows(i).Item("id")
                                    Dim dtUpdateSCProductionProcess = New SqlCommand(queryUpdateSCProductionProcess, Database.koneksi)
                                    dtUpdateSCProductionProcess.ExecuteNonQuery()

                                    Dim queryUpdateFlowTicket As String = "update flow_ticket set done=1 where DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and fg='" & cbFGPN.Text & "' and flow_ticket='" & sFlowTicket(5) & "'"
                                    Dim dtUpdateFlowTicket = New SqlCommand(queryUpdateFlowTicket, Database.koneksi)
                                    If dtUpdateFlowTicket.ExecuteNonQuery() Then

                                        If showMessageBox Then
                                            RJMessageBox.Show("Success Save Sub Assy data!!!")
                                            showMessageBox = False
                                        End If

                                        ClearInputFG()
                                        UpdateQtySubSubPO()

                                    Else

                                        RJMessageBox.Show("Fail Save Sub Assy data!")

                                    End If

                                End If

                            End If

                        End If

                    Next

                Else

                    RJMessageBox.Show("Sub Assy MUFG more then 1 Material??")

                End If

            Else

                RJMessageBox.Show("This Flow ticket already finish")

            End If


            If CheckBox5.CheckState = CheckState.Checked Then
                Dim query As String = "select DISTINCT(CODE_STOCK_PROD_SUB_ASSY),[print],traceability,inv_ctrl_date,batch_no,lot_no,qty from STOCK_PROD_SUB_ASSY where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg='" & cbFGPN.Text & "' and flow_ticket='" & sFlowTicket(5) & "'"
                Dim dtCheckStockSA As DataTable = Database.GetData(query)
                If dtCheckStockSA.Rows.Count > 0 Then
                    For sa = 0 To dtCheckStockSA.Rows.Count - 1
                        globVar.failPrint = ""
                        _PrintingSubAssyRawMaterial.txt_jenis_ticket.Text = "Sub Assy"
                        _PrintingSubAssyRawMaterial.txt_Unique_id.Text = dtCheckStockSA.Rows(sa).Item("CODE_STOCK_PROD_SUB_ASSY")
                        _PrintingSubAssyRawMaterial.txt_QR_Code.Text = dtCheckStockSA.Rows(sa).Item("CODE_STOCK_PROD_SUB_ASSY")
                        _PrintingSubAssyRawMaterial.txt_part_number.Text = cbFGPN.Text
                        _PrintingSubAssyRawMaterial.txt_Part_Description.Text = txtDescDefective.Text
                        _PrintingSubAssyRawMaterial.txt_Qty.Text = dtCheckStockSA.Rows(sa).Item("QTY")
                        _PrintingSubAssyRawMaterial.txt_Traceability.Text = dtCheckStockSA.Rows(sa).Item("TRACEABILITY")
                        _PrintingSubAssyRawMaterial.txt_Inv_crtl_date.Text = dtCheckStockSA.Rows(sa).Item("INV_CTRL_DATE")
                        _PrintingSubAssyRawMaterial.txt_Batch_no.Text = dtCheckStockSA.Rows(sa).Item("BATCH_NO")
                        _PrintingSubAssyRawMaterial.txt_Lot_no.Text = dtCheckStockSA.Rows(sa).Item("LOT_NO")
                        _PrintingSubAssyRawMaterial.btn_Print_Click(sender, e)

                        If globVar.failPrint = "No" Then
                            Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, remark,sub_sub_po,department,flow_ticket,code_print)
                                     VALUES ('" & cbPONumber.Text & "','" & cbFGPN.Text & "','" & cbLineNumber.Text & "','Sub Assy','" & txtSubSubPODefective.Text & "','" & dept & "','" & sFlowTicket(5) & "','" & dtCheckStockSA.Rows(sa).Item("CODE_STOCK_PROD_SUB_ASSY") & "')"
                            Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                            cmdInsertPrintingRecord.ExecuteNonQuery()

                            Dim sqlupdateDefect As String = "update STOCK_PROD_SUB_ASSY set [print]=1 where CODE_STOCK_PROD_SUB_ASSY='" & dtCheckStockSA.Rows(sa).Item("CODE_STOCK_PROD_SUB_ASSY") & "'"
                            Dim cmdupdateDefect = New SqlCommand(sqlupdateDefect, Database.koneksi)
                            cmdupdateDefect.ExecuteNonQuery()
                        End If
                    Next
                End If
            End If

        Catch ex As Exception

            RJMessageBox.Show("Error Save Sub Assy " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try





        'If globVar.add > 0 Then
        '    Dim sResult As Integer = 1
        '    Dim spasiFlowTicket = txtSAFlowTicket.Text.Replace(ChrW(160), " ")
        '    Dim sFlowTicket = spasiFlowTicket.Split(";")
        '    Dim sFlowTicketSplitOf = sFlowTicket(5).Split(" of ")
        '    Dim IlostQty As Integer = 0
        '    Dim queryInsertResultProduction As String

        '    Dim queryCekFlowTicketDone As String = "select * from flow_ticket where done=1 and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and fg='" & cbFGPN.Text & "' and flow_ticket='" & sFlowTicket(5) & "'"
        '    Dim dtCekFlowTicketDone As DataTable = Database.GetData(queryCekFlowTicketDone)
        '    If dtCekFlowTicketDone.Rows.Count > 0 Then
        '        RJMessageBox.Show("Sorry this flow ticket already done.")
        '        Exit Sub
        '    End If

        '    Dim queryMPF As String = "select * from master_process_flow where master_finish_goods_pn='" & cbFGPN.Text & "' and master_process is not null"
        '    Dim dtMasterProcessFlow As DataTable = Database.GetData(queryMPF)

        '    For iGas = 0 To dtMasterProcessFlow.Rows.Count - 1

        '        Dim sTampung As String = GetStockCard2(dtMasterProcessFlow.Rows(iGas).Item("material_usage"), sFlowTicket(5))

        '        If ckLossQty.Checked = True Then
        '            sTampung = ""
        '        End If

        '        If sTampung <> "" Then

        '            RJMessageBox.Show("Sorry qty this material " & sTampung & " is not enough. Please input in menu Production")
        '            Exit Sub

        '        Else

        '            Dim sLine = cbLineNumber.Text.Split(" ")
        '            Dim codeSubassy As String = ""
        '            Dim materialKurang As String = ""

        '            If txtStatusSubSubPO.Text = "Closed" Then
        '                RJMessageBox.Show("Sorry This Sub Sub PO status is closed. Cannot use this menu.")
        '                Exit Sub
        '            End If

        '            Dim result = RJMessageBox.Show("Are you sure for save Sub Assy. Cannot Change after Save?.", "Are You Sure?", MessageBoxButtons.YesNo)

        '            Dim queryCode As String = "select top 1 CODE_STOCK_PROD_SUB_ASSY from stock_prod_sub_assy ORDER BY cast(replace(CODE_STOCK_PROD_SUB_ASSY,'SA','') as int) desc"
        '            Dim dtCode As DataTable = Database.GetData(queryCode)
        '            Dim match As Match = Regex.Match(dtCode.Rows(0).Item("CODE_STOCK_PROD_SUB_ASSY").ToString(), "^([A-Z]+)(\d+)$")
        '            If match.Success Then
        '                Dim prefix As String = match.Groups(1).Value
        '                Dim number As Integer = Integer.Parse(match.Groups(2).Value)
        '                Dim nextNumber As Integer = number + 1
        '                codeSubassy = prefix & nextNumber.ToString()
        '            End If

        '            Dim now As DateTime = DateTime.Now
        '            Dim dateString As String = now.ToString("yyyyMMdd")

        '            If result = DialogResult.Yes Then

        '                Dim queryCheck1 As String = "select * from stock_card where status='Production Process' and sub_sub_po='" & txtSubSubPODefective.Text & "' and flow_ticket='" & sFlowTicket(5) & "'"
        '                Dim dtCheck1 As DataTable = Database.GetData(queryCheck1)
        '                If dtCheck1.Rows.Count = 0 Then
        '                    RJMessageBox.Show("Sorry one or more material not exist in DB. Please input in menu Production.")
        '                    Exit Sub
        '                End If

        '                Dim queryCheckDoneFG As String = "select * from stock_prod_sub_assy where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg='" & cbFGPN.Text & "' and FLOW_TICKET='" & sFlowTicket(5) & "' and department='" & globVar.department & "'"
        '                Dim dtCheckDoneFG As DataTable = Database.GetData(queryCheckDoneFG)
        '                If dtCheckDoneFG.Rows.Count = 0 Then

        '                    Dim queryCheckProductionProcess As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and finish_goods_pn='" & cbFGPN.Text & "' and flow_ticket='" & sFlowTicket(5) & "' and department='" & globVar.department & "' and status='Production Process' and line='" & cbLineNumber.Text & "'"
        '                    Dim dtCheckProductionProcess As DataTable = Database.GetData(queryCheckProductionProcess)
        '                    If dtCheckProductionProcess.Rows.Count > 0 Then

        '                        For i = 0 To dtCheckProductionProcess.Rows.Count - 1

        '                            If ckLossQty.Checked = True Then

        '                                IlostQty = txtLossQty.Text

        '                                queryInsertResultProduction = "insert into stock_card([MTS_NO],[DEPARTMENT],[MATERIAL],[STATUS],[STANDARD_PACK],[INV_CTRL_DATE],[TRACEABILITY],[BATCH_NO],[LOT_NO],
        '                                    [FINISH_GOODS_PN],[PO],[SUB_PO],[SUB_SUB_PO],[LINE],[QRCODE],[QTY],[ACTUAL_QTY],[ID_LEVEL],[LEVEL],[FLOW_TICKET]) 
        '                                    select [MTS_NO],[DEPARTMENT],[MATERIAL],'Production Result',[STANDARD_PACK],'" & dateString & "','" & cbPONumber.Text & "','" & txtSABatchNo.Text & "','" & sFlowTicketSplitOf(0) & "',
        '                                    [FINISH_GOODS_PN],[PO],[SUB_PO],[SUB_SUB_PO],[LINE],'" & codeSubassy & "'," & IlostQty & "," & IlostQty & ",[ID_LEVEL],'SA',[FLOW_TICKET] 
        '                                    from stock_card where id=" & dtCheckProductionProcess.Rows(i).Item("id")
        '                            Else

        '                                IlostQty = dtCheckProductionProcess.Rows(i).Item("actual_qty").ToString.Replace(",", ".")

        '                                queryInsertResultProduction = "insert into stock_card([MTS_NO],[DEPARTMENT],[MATERIAL],[STATUS],[STANDARD_PACK],[INV_CTRL_DATE],[TRACEABILITY],[BATCH_NO],[LOT_NO],
        '                                    [FINISH_GOODS_PN],[PO],[SUB_PO],[SUB_SUB_PO],[LINE],[QRCODE],[QTY],[ACTUAL_QTY],[ID_LEVEL],[LEVEL],[FLOW_TICKET]) 
        '                                    select [MTS_NO],[DEPARTMENT],[MATERIAL],'Production Result',[STANDARD_PACK],'" & dateString & "','" & cbPONumber.Text & "','" & txtSABatchNo.Text & "','" & sFlowTicketSplitOf(0) & "',
        '                                    [FINISH_GOODS_PN],[PO],[SUB_PO],[SUB_SUB_PO],[LINE],'" & codeSubassy & "'," & IlostQty & "," & IlostQty & ",[ID_LEVEL],'SA',[FLOW_TICKET] 
        '                                    from stock_card where id=" & dtCheckProductionProcess.Rows(i).Item("id")
        '                            End If

        '                            Dim queryCheckSubAssy As String = "select * from stock_prod_sub_assy where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg='" & cbFGPN.Text & "' and FLOW_TICKET='" & sFlowTicket(5) & "' and department='" & globVar.department & "'"
        '                            Dim dtCheckSubAssy As DataTable = Database.GetData(queryCheckSubAssy)
        '                            If dtCheckSubAssy.Rows.Count = 0 Then
        '                                'Insert SA Done
        '                                Dim dtInsertResultProduction = New SqlCommand(queryInsertResultProduction, Database.koneksi)
        '                                'Insert SA Done

        '                                If dtInsertResultProduction.ExecuteNonQuery() Then

        '                                    'Insert stock prod sub assy
        '                                    Dim sqlInsertSubAssy As String = "INSERT INTO stock_prod_sub_assy (code_stock_prod_sub_assy, po, sub_sub_po, FG ,FLOW_TICKET,DEPARTMENT,laser_code, LOT_NO, qty,TRACEABILITY,INV_CTRL_DATE,BATCH_NO)
        '                                        VALUES ('" & codeSubassy & "','" & cbPONumber.Text & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & sFlowTicket(5) & "','" & globVar.department & "',
        '                                        '" & TextBox6.Text & "','" & sFlowTicketSplitOf(0) & "'," & IlostQty & ",'" & cbPONumber.Text & "','" & dateString & "','" & txtSABatchNo.Text & "')"
        '                                    Dim cmdInsertSubAssy = New SqlCommand(sqlInsertSubAssy, Database.koneksi)
        '                                    cmdInsertSubAssy.ExecuteNonQuery()
        '                                    'Insert stock prod sub assy

        '                                    'Update production prequest
        '                                    Dim queryUpdateStockCardProdReq As String = "update stock_card set sum_qty=sum_qty-" & IlostQty & " where status='Production Request' and material='" & dtCheckProductionProcess.Rows(i).Item("material") & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckProductionProcess.Rows(i).Item("lot_no") & "' AND ID_LEVEL='" & dtCheckProductionProcess.Rows(i).Item("ID_LEVEL") & "'"
        '                                    Dim dtUpdateStockCardProdReq = New SqlCommand(queryUpdateStockCardProdReq, Database.koneksi)
        '                                    dtUpdateStockCardProdReq.ExecuteNonQuery()
        '                                    'Update production prequest
        '                                End If
        '                            End If

        '                            'Delete production Process
        '                            Dim queryDeleteStockCardProdProcess As String = "delete from stock_card where id=" & dtCheckProductionProcess.Rows(i).Item("id")
        '                            Dim dtDeleteStockCardProdProcess = New SqlCommand(queryDeleteStockCardProdProcess, Database.koneksi)
        '                            dtDeleteStockCardProdProcess.ExecuteNonQuery()
        '                            'Delete production Process

        '                            'Update flowticket done
        '                            Dim queryUpdateFlowTicket As String = "update flow_ticket set done=1 where DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and fg='" & cbFGPN.Text & "' and flow_ticket='" & sFlowTicket(5) & "'"
        '                            Dim dtUpdateFlowTicket = New SqlCommand(queryUpdateFlowTicket, Database.koneksi)
        '                            If dtUpdateFlowTicket.ExecuteNonQuery() Then
        '                                sResult *= 1
        '                            Else
        '                                sResult *= 0
        '                            End If
        '                            'Update flowticket done

        '                        Next

        '                    End If

        '                Else
        '                    RJMessageBox.Show("Sorry this Flow Ticket already Done. Cannot Save.")
        '                    ClearInputFG()
        '                    UpdateQtySubSubPO()
        '                End If
        '            Else
        '                sResult *= 0
        '            End If

        '        End If
        '    Next

        '    If CheckBox5.CheckState = CheckState.Checked Then
        '        Dim query As String = "select DISTINCT(CODE_STOCK_PROD_SUB_ASSY),[print],traceability,inv_ctrl_date,batch_no,lot_no,qty from STOCK_PROD_SUB_ASSY where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg='" & cbFGPN.Text & "' and flow_ticket='" & sFlowTicket(5) & "'"
        '        Dim dtCheckStockSA As DataTable = Database.GetData(query)
        '        If dtCheckStockSA.Rows.Count > 0 Then
        '            For sa = 0 To dtCheckStockSA.Rows.Count - 1
        '                globVar.failPrint = ""
        '                _PrintingSubAssyRawMaterial.txt_jenis_ticket.Text = "Sub Assy"
        '                _PrintingSubAssyRawMaterial.txt_Unique_id.Text = dtCheckStockSA.Rows(sa).Item("CODE_STOCK_PROD_SUB_ASSY")
        '                _PrintingSubAssyRawMaterial.txt_QR_Code.Text = dtCheckStockSA.Rows(sa).Item("CODE_STOCK_PROD_SUB_ASSY")
        '                _PrintingSubAssyRawMaterial.txt_part_number.Text = cbFGPN.Text
        '                _PrintingSubAssyRawMaterial.txt_Part_Description.Text = txtDescDefective.Text
        '                _PrintingSubAssyRawMaterial.txt_Qty.Text = dtCheckStockSA.Rows(sa).Item("QTY")
        '                _PrintingSubAssyRawMaterial.txt_Traceability.Text = dtCheckStockSA.Rows(sa).Item("TRACEABILITY")
        '                _PrintingSubAssyRawMaterial.txt_Inv_crtl_date.Text = dtCheckStockSA.Rows(sa).Item("INV_CTRL_DATE")
        '                _PrintingSubAssyRawMaterial.txt_Batch_no.Text = dtCheckStockSA.Rows(sa).Item("BATCH_NO")
        '                _PrintingSubAssyRawMaterial.txt_Lot_no.Text = dtCheckStockSA.Rows(sa).Item("LOT_NO")
        '                _PrintingSubAssyRawMaterial.btn_Print_Click(sender, e)

        '                If globVar.failPrint = "No" Then
        '                    Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, remark,sub_sub_po,department,flow_ticket,code_print)
        '                         VALUES ('" & cbPONumber.Text & "','" & cbFGPN.Text & "','" & cbLineNumber.Text & "','Sub Assy','" & txtSubSubPODefective.Text & "','" & dept & "','" & sFlowTicket(5) & "','" & dtCheckStockSA.Rows(sa).Item("CODE_STOCK_PROD_SUB_ASSY") & "')"
        '                    Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
        '                    cmdInsertPrintingRecord.ExecuteNonQuery()

        '                    Dim sqlupdateDefect As String = "update STOCK_PROD_SUB_ASSY set [print]=1 where CODE_STOCK_PROD_SUB_ASSY='" & dtCheckStockSA.Rows(sa).Item("CODE_STOCK_PROD_SUB_ASSY") & "'"
        '                    Dim cmdupdateDefect = New SqlCommand(sqlupdateDefect, Database.koneksi)
        '                    cmdupdateDefect.ExecuteNonQuery()
        '                End If
        '            Next
        '        End If
        '    End If

        '    If sResult > 0 Then

        '        RJMessageBox.Show("Success Save Sub Assy data!!!")
        '        ClearInputFG()
        '        UpdateQtySubSubPO()

        '    Else

        '        RJMessageBox.Show("Fail Save Sub Assy data!!!")

        '    End If
        'Else
        '    RJMessageBox.Show("Your Access cannot execute this action")
        'End If

    End Sub

    Public Function GetStockCard(pn As String) As String
        Dim splitPN() As String = pn.Split(";")
        Dim i As Integer
        Dim sReturnValue As String = ""

        For i = 0 To splitPN.Length - 2

            Dim querySelectSumFlowTicket As String = "select isnull(sum(ft.qty_per_lot*mufg.[usage]),0) qty from material_usage_finish_goods mufg, flow_ticket ft where mufg.fg_part_number='" & cbFGPN.Text & "' and mufg.component='" & splitPN(i) & "' and ft.fg=mufg.fg_part_number and ft.sub_sub_po='" & txtSubSubPODefective.Text & "' and ft.done=0"
            Dim dtSelectSumFlowTicket As DataTable = Database.GetData(querySelectSumFlowTicket)

            Dim querySelectSumMaterial As String = "select isnull(sum(sum_qty),0) qty from stock_card where status='Production Request' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no in (select DISTINCT(lot_no) from stock_card where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "')"
            Dim dtSelectSumMaterial As DataTable = Database.GetData(querySelectSumMaterial)

            If dtSelectSumMaterial.Rows(0).Item("qty") < dtSelectSumFlowTicket.Rows(0).Item("qty") Then
                sReturnValue += splitPN(i) & ","
            End If

        Next

        Return sReturnValue
    End Function

    Public Function GetStockCardNew(fg As String, flow_ticket As String) As String

        Dim queryMUFG As String = "select * from material_usage_finish_goods where fg_part_number='" & fg & "'"
        Dim dtMUFG As DataTable = Database.GetData(queryMUFG)

        Dim sReturnValue As String = ""

        For i = 0 To dtMUFG.Rows().Count - 1

            Dim querySelectSumFlowTicket As String = "SELECT
	                isnull( ( ft.qty_per_lot * " & dtMUFG.Rows(i).Item("usage") & " ), 0 ) qty 
                FROM
	                flow_ticket ft 
                WHERE
	                ft.sub_sub_po= '" & txtSubSubPODefective.Text & "' 
	                AND ft.done=0
                    AND ft.FLOW_TICKET = '" & flow_ticket & "'"
            Dim dtSelectSumFlowTicket As DataTable = Database.GetData(querySelectSumFlowTicket)

            Dim queryActualQtyProcess As String = "SELECT
	                isnull(SUM ( ACTUAL_QTY ),0) ACTUAL_QTY 
                FROM
	                STOCK_CARD sc
                WHERE
	                sc.status = 'Production Process' 
	                AND sc.material = '" & dtMUFG.Rows(i).Item("component") & "' 
	                AND sc.DEPARTMENT = '" & globVar.department & "' 
	                AND sc.sub_sub_po = '" & txtSubSubPODefective.Text & "' 
	                AND sc.FINISH_GOODS_PN = '" & cbFGPN.Text & "'
	                AND sc.FLOW_TICKET = '" & flow_ticket & "'"
            Dim dtActualQtyProcess As DataTable = Database.GetData(queryActualQtyProcess)

            If dtActualQtyProcess.Rows(0).Item("actual_qty") < dtSelectSumFlowTicket.Rows(0).Item("qty") Then
                sReturnValue += dtMUFG.Rows(i).Item("component") & ","
            End If
        Next
        Return sReturnValue
    End Function

    Public Function GetStockCard2(pn As String, lot_flow_ticket As String) As String
        Dim splitPN() As String = pn.Split(";")
        Dim i As Integer
        Dim sReturnValue As String = ""
        For i = 0 To splitPN.Length - 2

            Dim querySelectSumFlowTicket As String = "select isnull(sum(ft.qty_per_lot*mufg.[usage]),0) qty from material_usage_finish_goods mufg, flow_ticket ft where mufg.fg_part_number='" & cbFGPN.Text & "' and mufg.component='" & splitPN(i) & "' and ft.fg=mufg.fg_part_number and ft.sub_sub_po='" & txtSubSubPODefective.Text & "' and ft.done=0 and ft.flow_ticket='" & lot_flow_ticket & "'"
            Dim dtSelectSumFlowTicket As DataTable = Database.GetData(querySelectSumFlowTicket)

            Dim querySelectSumMaterial As String = "select isnull(sum(sum_qty),0) qty from stock_card where status='Production Request' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no in (select DISTINCT(lot_no) from stock_card where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "')"
            Dim dtSelectSumMaterial As DataTable = Database.GetData(querySelectSumMaterial)

            Dim queryCheckStockMaterial As String = "select DISTINCT(lot_no) from stock_card where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
            Dim dtCheckStockMaterial As DataTable = Database.GetData(queryCheckStockMaterial)

            If dtCheckStockMaterial.Rows.Count > 0 Then
                If dtSelectSumMaterial.Rows(0).Item("qty") < dtSelectSumFlowTicket.Rows(0).Item("qty") Then
                    sReturnValue += splitPN(i) & ","
                End If
            Else
                sReturnValue += splitPN(i) & ","
            End If
        Next
        Return sReturnValue
    End Function

    Function CheckStockSaveSA(qty As Integer) As String

        Dim sReturnValue As String = ""

        Dim queryCheckmaterial_usage_finish_goods As String = "select * from material_usage_finish_goods where fg_part_number='" & cbFGPN.Text & "'"
        Dim dtqueryCheckmaterial_usage_finish_goods As DataTable = Database.GetData(queryCheckmaterial_usage_finish_goods)

        If dtqueryCheckmaterial_usage_finish_goods.Rows.Count > 0 Then
            For i = 0 To dtqueryCheckmaterial_usage_finish_goods.Rows.Count - 1
                Dim queryCheckQtyProductionProcess As String = "select * from stock_card where material = '" & dtqueryCheckmaterial_usage_finish_goods.Rows(i).Item("component") & "' and status='Production Process' and sub_sub_po='" & txtSubSubPODefective.Text & "' and actual_qty > " & qty
                Dim dtCheckQtyProductionProcess As DataTable = Database.GetData(queryCheckQtyProductionProcess)
                If dtCheckQtyProductionProcess.Rows.Count = 0 Then
                    sReturnValue += dtqueryCheckmaterial_usage_finish_goods.Rows(i).Item("component") & ","
                Else
                    sReturnValue = ""
                End If
            Next
        Else
            sReturnValue = "Kosong"
        End If
    End Function

    Function funcDeleteReject(id As Integer, pn As String, lot As String, qty As Double, sFlowTicket As String)
        Try
            Dim sResult As Integer = 1
            Dim i As Integer = 0
            Dim addQty As Double

            Dim queryCheckAdd As String = "select * from stock_card where status='Production Request' and material='" & pn & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & lot & "' AND [LEVEL]='Fresh'"
            Dim dtCheckAdd As DataTable = Database.GetData(queryCheckAdd)
            If dtCheckAdd.Rows.Count > 0 Then
                addQty = dtCheckAdd.Rows(0).Item("sum_qty") + qty

                Dim queryUpdateAdd As String = "update stock_card set actual_qty=" & addQty.ToString.Replace(",", ".") & ",sum_qty=" & addQty.ToString.Replace(",", ".") & " where status='Production Request' and material='" & pn & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & lot & "' AND [LEVEL]='Fresh'"
                Dim dtUpdateAdd = New SqlCommand(queryUpdateAdd, Database.koneksi)
                If dtUpdateAdd.ExecuteNonQuery() Then
                    Dim queryDeleteStockCard As String = "delete from stock_card where status='Production Process' and material='" & pn & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' AND [LEVEL]='Fresh'" ' and lot_no='" & lot & "'"
                    Dim dtDeleteStockCard = New SqlCommand(queryDeleteStockCard, Database.koneksi)
                    If dtDeleteStockCard.ExecuteNonQuery() Then
                        Dim queryCheckStockCardRequest As String = "select * from stock_card where status='Production Request' and material='" & pn & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' AND [LEVEL]='Fresh' and sum_qty > 0 order by id"
                        Dim dtCheckStockCardRequest As DataTable = Database.GetData(queryCheckStockCardRequest)
                        If dtCheckStockCardRequest.Rows.Count > 0 Then
                            For iDistint = 0 To dtCheckStockCardRequest.Rows.Count - 1
                                If dtCheckStockCardRequest.Rows(iDistint).Item("lot_no") = lot Then
                                    Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & addQty.ToString.Replace(",", ".") & ",@material='" & pn & "',@lot_material='" & dtCheckStockCardRequest.Rows(iDistint).Item("lot_no") & "'"
                                    Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                                Else
                                    Dim queryUpdateactualQty As String = "update stock_card set actual_qty=sum_qty where status='Production Request' and material='" & pn & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckStockCardRequest.Rows(iDistint).Item("lot_no") & "' AND [LEVEL]='Fresh'"
                                    Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                                    If dtUpdateactualQty.ExecuteNonQuery() Then
                                        Dim sqlQtyLot = "select * from stock_card where status='Production Request' and material='" & pn & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckStockCardRequest.Rows(iDistint).Item("lot_no") & "' AND [LEVEL]='Fresh' and sum_qty > 0 order by id"
                                        Dim dtQtyLot As DataTable = Database.GetData(sqlQtyLot)
                                        Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtQtyLot.Rows(0).Item("sum_qty").ToString.Replace(",", ".") & ",@material='" & pn & "',@lot_material='" & dtCheckStockCardRequest.Rows(iDistint).Item("lot_no") & "'"
                                        Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If
                sResult *= 1
            Else
                sResult *= 0
                RJMessageBox.Show("Sorry. Material not exist ind Stock Card")
            End If

            'Dim queryUpdateStockCardProdReq As String = "update summary_fg set defect_out=0 where sub_sub_po='" & txtSubSubPODefective.Text & "' and material='" & pn & "'"
            'Dim dtUpdateStockCardProdReq = New SqlCommand(queryUpdateStockCardProdReq, Database.koneksi)
            'dtUpdateStockCardProdReq.ExecuteNonQuery()

            Return sResult
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Function

    Function funcInsertReject(pn As String, qty As Integer, process As String, input_from_fg As Integer, sFlowTicket As String)
        Try

            If pn.Split("-")(0) = "Kosong" Then
                RJMessageBox.Show("Sorry, the " & pn.Split("-")(1) & " does not use any material")
                Return 0
                Exit Function
            End If

            Dim sResult As Integer = 1
            Dim sTampung As String = GetStockCard(pn)

            If sTampung <> "" Then
                RJMessageBox.Show("Sorry qty this material " & sTampung & " is not enough. Please input in menu Production")
                Return 0
            Else
                Dim splitPN() As String = pn.Split(";")
                Dim splitFlowTicket() As String = sFlowTicket.Split(";")

                Dim qtyUpdate As Double
                Dim qtyReject As Double
                Dim qtyRejectLoop As Double
                Dim sumQty As Double
                Dim i As Integer = 0
                Dim addQty As Double
                Dim fifo As Integer
                Dim lot As String

                Dim queryGetExistingDB As String = "select * from out_prod_defect where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and process_reject='" & process & "' and flow_ticket_no='" & splitFlowTicket(5) & "' and department='" & globVar.department & "' and input_from_fg=" & input_from_fg
                Dim dtGetExistingDB As DataTable = Database.GetData(queryGetExistingDB)

                If dtGetExistingDB.Rows.Count > 0 Then
                    If dtGetExistingDB.Rows(0).Item("pengali") <> qty Then
                        For i = 0 To splitPN.Length - 2
                            qtyUpdate = 0
                            qtyReject = 0
                            sumQty = 0
                            qtyRejectLoop = 0
                            fifo = 1
                            lot = ""

                            'Get List Material Lot Production Request
                            Dim queryCheckTotalMaterial As String = "select * from stock_card where status='Production Request' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                            Dim dtCheckTotalMaterial As DataTable = Database.GetData(queryCheckTotalMaterial)
                            'Get List Material Lot Production Request

                            For TotalMaterial = 0 To dtCheckTotalMaterial.Rows.Count - 1

                                'Get Qty From Defect
                                Dim queryGetDefect As String = "select * from out_prod_defect where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and part_number='" & splitPN(i) & "' and process_reject='" & process & "' and flow_ticket_no='" & splitFlowTicket(5) & "' and department='" & globVar.department & "' and lot_no='" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") & "' and input_from_fg=" & input_from_fg
                                Dim dtGetDefect As DataTable = Database.GetData(queryGetDefect)
                                'Get Qty From Defect

                                'Get Qty From Stock Card
                                Dim queryCheckAdd As String = "select * from stock_card where status='Production Request' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") & "'" ' & " AND [LEVEL]='Fresh'"
                                Dim dtCheckAdd As DataTable = Database.GetData(queryCheckAdd)
                                'Get Qty From Stock Card

                                If dtGetDefect.Rows.Count > 0 Then
                                    addQty = dtCheckAdd.Rows(0).Item("sum_qty") + dtGetDefect.Rows(0).Item("qty")
                                Else
                                    addQty = dtCheckAdd.Rows(0).Item("sum_qty")
                                End If

                                'Update Stock Card
                                Dim queryUpdateAdd As String = "update stock_card set actual_qty=" & addQty.ToString.Replace(",", ".") & ",sum_qty=" & addQty.ToString.Replace(",", ".") & " where status='Production Request' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") & "'" ' & " AND [LEVEL]='Fresh'"
                                Dim dtUpdateAdd = New SqlCommand(queryUpdateAdd, Database.koneksi)
                                'Update Stock Card

                                If dtUpdateAdd.ExecuteNonQuery() Then

                                    'Get Code Untuk Insert di Defect
                                    Dim queryCheckCodeReject As String = "select DISTINCT(CODE_OUT_PROD_DEFECT) from OUT_PROD_DEFECT where sub_sub_po='" & txtSubSubPODefective.Text & "'"
                                    Dim dtCheckCodeReject As DataTable = Database.GetData(queryCheckCodeReject)
                                    Dim codeReject As String = ""
                                    If dtCheckCodeReject.Rows.Count > 0 Then
                                        codeReject = dtCheckCodeReject.Rows(0).Item(0)
                                    Else
                                        Dim queryCount As String = "select DISTINCT(CODE_OUT_PROD_DEFECT) from OUT_PROD_DEFECT"
                                        Dim dtCount As DataTable = Database.GetData(queryCount)
                                        codeReject = "D" & dtCount.Rows.Count + 1
                                    End If
                                    'Get Code Untuk Insert di Defect

                                    'Get Usage Per Material
                                    Dim queryGetUsage As String = "select [usage] from material_usage_finish_goods where fg_part_number='" & cbFGPN.Text & "' and component='" & splitPN(i) & "'"
                                    Dim dtGetUsage As DataTable = Database.GetData(queryGetUsage)
                                    'Get Usage Per Material

                                    If dtGetUsage.Rows.Count > 0 Then

                                        'Hitung Reject
                                        qtyReject = qty * dtGetUsage.Rows(0).Item("USAGE")
                                        'Hitung Reject

                                        'Delete Defect
                                        If dtGetDefect.Rows.Count > 0 Then
                                            Dim queryDeleteDefect As String = "delete from out_prod_defect where part_number='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FG_PN='" & cbFGPN.Text & "' and input_from_fg=" & input_from_fg & " and flow_ticket_no='" & splitFlowTicket(5) & "' and process_reject='" & process & "'"
                                            Dim dtDeleteDefect = New SqlCommand(queryDeleteDefect, Database.koneksi)
                                            dtDeleteDefect.ExecuteNonQuery()
                                        End If
                                        'Delete Defect

                                        'Get Qty di Table Defect
                                        Dim queryCheckDefectQty As String = "select isnull(sum(qty),0) from out_prod_defect where part_number='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FG_PN='" & cbFGPN.Text & "' and input_from_fg=" & input_from_fg & " and flow_ticket_no='" & splitFlowTicket(5) & "' and process_reject='" & process & "'"
                                        Dim dtCheckSumDefectQty As DataTable = Database.GetData(queryCheckDefectQty)
                                        'Get Qty di Table Defect

                                        'Insert ke temporary untuk production Process
                                        Dim queryInsertToTemp As String = "insert into stock_card_temporary([MTS_NO], [DEPARTMENT], [MATERIAL], [STATUS], [STANDARD_PACK], [INV_CTRL_DATE], 
                                            [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [DATETIME_INSERT], [SAVE], [QRCODE], [DATETIME_SAVE], 
                                            [QTY], [ACTUAL_QTY], [FIFO], [ID_LEVEL], [LEVEL], [FLOW_TICKET]) 
                                            select [MTS_NO], [DEPARTMENT], [MATERIAL], [STATUS], [STANDARD_PACK], [INV_CTRL_DATE], [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], 
                                            [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [DATETIME_INSERT], [SAVE], [QRCODE], [DATETIME_SAVE], [QTY], [ACTUAL_QTY], [FIFO], [ID_LEVEL], [LEVEL], [FLOW_TICKET] 
                                            from stock_card where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'and lot_no='" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") & "'"
                                        Dim dtInsertTemp = New SqlCommand(queryInsertToTemp, Database.koneksi)
                                        'Insert ke temporary untuk production Process

                                        If dtInsertTemp.ExecuteNonQuery() Then

                                            'Delete stock card
                                            Dim queryDeleteStockCard As String = "delete from stock_card where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") & "'"
                                            Dim dtDeleteStockCard = New SqlCommand(queryDeleteStockCard, Database.koneksi)
                                            'Delete stock card

                                            If dtDeleteStockCard.ExecuteNonQuery() Then

                                                'Get Stock Card Temporary berdasarkan input flow ticket
                                                Dim queryCheckFlowTicketinTemp As String = "select * from stock_card_temporary where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and flow_ticket='" & splitFlowTicket(5) & "'"
                                                Dim dtCheckFlowTicketinTemp As DataTable = Database.GetData(queryCheckFlowTicketinTemp)
                                                'Get Stock Card Temporary berdasarkan input flow ticket

                                                'Perhitungan qty reject per lot dari setiap material
                                                If dtCheckTotalMaterial.Rows(TotalMaterial).Item("sum_qty") <= qtyReject Then
                                                    sumQty = 0
                                                    qtyRejectLoop = dtCheckTotalMaterial.Rows(TotalMaterial).Item("sum_qty")
                                                Else
                                                    qtyUpdate = qtyReject - dtCheckSumDefectQty.Rows(0).Item(0)
                                                    qtyRejectLoop = qtyUpdate
                                                    sumQty = dtCheckTotalMaterial.Rows(TotalMaterial).Item("sum_qty") - qtyUpdate
                                                End If
                                                'Perhitungan qty reject per lot dari setiap material

                                                'Update perhitungan Qty Reject dari stock card production request 
                                                Dim queryUpdateToStockCardqty As String = "update stock_card set actual_qty=" & sumQty.ToString.Replace(",", ".") & ",sum_qty=" & sumQty.ToString.Replace(",", ".") & " where status='Production Request' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") & "'"
                                                Dim dtUpdateToStockCardqty = New SqlCommand(queryUpdateToStockCardqty, Database.koneksi)
                                                'Update perhitungan Qty Reject dari stock card production request 

                                                If dtUpdateToStockCardqty.ExecuteNonQuery() Then

                                                    'Generate Production Process
                                                    If dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") = dtCheckFlowTicketinTemp.Rows(0).Item("lot_no") Then
                                                        Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & sumQty.ToString.Replace(",", ".") & ",@material='" & splitPN(i) & "',@lot_material='" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") & "'"
                                                        Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                                                    Else
                                                        Dim sqlQtyLot = "select * from stock_card where status='Production Request' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") & "'"
                                                        Dim dtQtyLot As DataTable = Database.GetData(sqlQtyLot)
                                                        Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtQtyLot.Rows(0).Item("sum_qty").ToString.Replace(",", ".") & ",@material='" & splitPN(i) & "',@lot_material='" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") & "'"
                                                        Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                                                    End If
                                                    'Generate Production Process

                                                    'insert ke Defect
                                                    If dtGetDefect.Rows.Count > 0 Then

                                                        'insert ke Defect
                                                        Dim sqlInsertRejectPN As String = "INSERT INTO out_prod_defect (CODE_OUT_PROD_DEFECT, sub_sub_po, FG_PN, PART_NUMBER, LOT_NO, TRACEABILITY,INV_CTRL_DATE,BATCH_NO,QTY,PO,PROCESS_REJECT,LINE,FLOW_TICKET_NO,DEPARTMENT,PENGALI,INPUT_FROM_FG,ACTUAL_QTY)
                                                            VALUES ('" & codeReject & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & splitPN(i) & "','" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") & "',
                                                            '" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("TRACEABILITY") & "','" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("INV_CTRL_DATE") & "','" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("BATCH_NO") & "',
                                                            " & qtyRejectLoop.ToString.Replace(",", ".") & ",'" & cbPONumber.Text & "','" & process & "','" & cbLineNumber.Text & "','" & splitFlowTicket(5) & "','" & dept & "'," & qty & ",'" & input_from_fg & "'," & qtyRejectLoop.ToString.Replace(",", ".") & ")"
                                                        Dim cmdInsertRejectPN = New SqlCommand(sqlInsertRejectPN, Database.koneksi)
                                                        cmdInsertRejectPN.ExecuteNonQuery()
                                                        'insert ke Defect
                                                    End If
                                                    'insert ke Defect
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            Next

                            'Update Fifo
                            Dim queryCheckProductionProcess As String = "select * from stock_card where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' order by id"
                            Dim dtCheckProductionProcess As DataTable = Database.GetData(queryCheckProductionProcess)
                            For production_proc = 0 To dtCheckProductionProcess.Rows.Count - 1
                                If lot <> dtCheckProductionProcess.Rows(production_proc).Item("lot_no").ToString Then
                                    Dim queryUpdateFifo As String = "update stock_card set fifo=" & fifo & " where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckProductionProcess.Rows(production_proc).Item("lot_no") & "'"
                                    Dim dtUpdateFifo = New SqlCommand(queryUpdateFifo, Database.koneksi)

                                    If dtUpdateFifo.ExecuteNonQuery() Then
                                        lot = dtCheckProductionProcess.Rows(production_proc).Item("lot_no")
                                        fifo = fifo + 1
                                    End If
                                End If
                            Next
                            'Update Fifo

                            'delete temporary data
                            Dim queryDeleteStockCardTemporary As String = "delete from stock_card_temporary where status='Production Process' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                            Dim dtDeleteStockCardTemporary = New SqlCommand(queryDeleteStockCardTemporary, Database.koneksi)
                            If dtDeleteStockCardTemporary.ExecuteNonQuery() Then
                                sResult *= 1
                            Else
                                sResult *= 0
                            End If
                            'delete temporary data

                            'Kebutuhan traceabiity
                            'Dim dtOutDefect As DataTable = Database.GetData("select isnull(sum(actual_qty),0) from out_prod_defect where sub_sub_po='" & txtSubSubPODefective.Text & "' and part_number='" & splitPN(i) & "'")

                            'Dim queryUpdateStockCardProdReq As String = "update summary_fg set defect_out=" & dtOutDefect.Rows(0)(0).ToString.Replace(",", ".") & " where sub_sub_po='" & txtSubSubPODefective.Text & "' and material='" & splitPN(i) & "'"
                            'Dim dtUpdateStockCardProdReq = New SqlCommand(queryUpdateStockCardProdReq, Database.koneksi)
                            'dtUpdateStockCardProdReq.ExecuteNonQuery()
                            'Kebutuhan traceabiity
                        Next
                    End If
                Else
                    For i = 0 To splitPN.Length - 2
                        qtyUpdate = 0
                        qtyReject = 0
                        sumQty = 0
                        qtyRejectLoop = 0
                        fifo = 1
                        lot = ""

                        'Get List Material Lot Production Request
                        Dim queryCheckTotalMaterial As String = "select * from stock_card where status='Production Request' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                        Dim dtCheckTotalMaterial As DataTable = Database.GetData(queryCheckTotalMaterial)
                        'Get List Material Lot Production Request

                        For TotalMaterial = 0 To dtCheckTotalMaterial.Rows.Count - 1

                            'Get Code Untuk Insert di Defect
                            Dim queryCheckCodeReject As String = "select DISTINCT(CODE_OUT_PROD_DEFECT) from OUT_PROD_DEFECT where sub_sub_po='" & txtSubSubPODefective.Text & "'"
                            Dim dtCheckCodeReject As DataTable = Database.GetData(queryCheckCodeReject)
                            Dim codeReject As String = ""
                            If dtCheckCodeReject.Rows.Count > 0 Then
                                codeReject = dtCheckCodeReject.Rows(0).Item(0)
                            Else
                                Dim queryCount As String = "select DISTINCT(CODE_OUT_PROD_DEFECT) from OUT_PROD_DEFECT"
                                Dim dtCount As DataTable = Database.GetData(queryCount)
                                codeReject = "D" & dtCount.Rows.Count + 1
                            End If
                            'Get Code Untuk Insert di Defect

                            'Get Usage Per Material
                            Dim queryGetUsage As String = "select [usage] from material_usage_finish_goods where fg_part_number='" & cbFGPN.Text & "' and component='" & splitPN(i) & "'"
                            Dim dtGetUsage As DataTable = Database.GetData(queryGetUsage)
                            'Get Usage Per Material

                            If dtGetUsage.Rows.Count > 0 Then

                                'Hitung Reject
                                qtyReject = qty * dtGetUsage.Rows(0).Item("USAGE")
                                'Hitung Reject

                                'Get Qty di Table Defect
                                Dim queryCheckDefectQty As String = "select isnull(sum(qty),0) from out_prod_defect where part_number='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FG_PN='" & cbFGPN.Text & "' and input_from_fg=" & input_from_fg & " and flow_ticket_no='" & splitFlowTicket(5) & "' and process_reject='" & process & "'"
                                Dim dtCheckSumDefectQty As DataTable = Database.GetData(queryCheckDefectQty)
                                'Get Qty di Table Defect

                                'Insert ke temporary untuk production Process
                                Dim queryInsertToTemp As String = "insert into stock_card_temporary([MTS_NO], [DEPARTMENT], [MATERIAL], [STATUS], [STANDARD_PACK], [INV_CTRL_DATE], 
                                    [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [DATETIME_INSERT], [SAVE], [QRCODE], [DATETIME_SAVE], 
                                    [QTY], [ACTUAL_QTY], [FIFO], [ID_LEVEL], [LEVEL], [FLOW_TICKET]) 
                                    select [MTS_NO], [DEPARTMENT], [MATERIAL], [STATUS], [STANDARD_PACK], [INV_CTRL_DATE], [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], 
                                    [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [DATETIME_INSERT], [SAVE], [QRCODE], [DATETIME_SAVE], [QTY], [ACTUAL_QTY], [FIFO], [ID_LEVEL], [LEVEL], [FLOW_TICKET] 
                                    from stock_card where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'and lot_no='" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") & "'"
                                Dim dtInsertTemp = New SqlCommand(queryInsertToTemp, Database.koneksi)
                                'Insert ke temporary untuk production Process

                                If dtInsertTemp.ExecuteNonQuery() Then

                                    'Delete stock card
                                    Dim queryDeleteStockCard As String = "delete from stock_card where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") & "'"
                                    Dim dtDeleteStockCard = New SqlCommand(queryDeleteStockCard, Database.koneksi)
                                    'Delete stock card

                                    If dtDeleteStockCard.ExecuteNonQuery() Then

                                        'Get Stock Card Temporary berdasarkan input flow ticket
                                        Dim queryCheckFlowTicketinTemp As String = "select * from stock_card_temporary where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and flow_ticket='" & splitFlowTicket(5) & "'"
                                        Dim dtCheckFlowTicketinTemp As DataTable = Database.GetData(queryCheckFlowTicketinTemp)
                                        'Get Stock Card Temporary berdasarkan input flow ticket

                                        'Perhitungan qty reject per lot dari setiap material
                                        If dtCheckTotalMaterial.Rows(TotalMaterial).Item("sum_qty") <= qtyReject Then
                                            sumQty = 0
                                            qtyRejectLoop = dtCheckTotalMaterial.Rows(TotalMaterial).Item("sum_qty")
                                        Else
                                            qtyUpdate = qtyReject - dtCheckSumDefectQty.Rows(0).Item(0)
                                            qtyRejectLoop = qtyUpdate
                                            sumQty = dtCheckTotalMaterial.Rows(TotalMaterial).Item("sum_qty") - qtyUpdate
                                        End If
                                        'Perhitungan qty reject per lot dari setiap material

                                        'Update perhitungan Qty Reject dari stock card production request 
                                        Dim queryUpdateToStockCardqty As String = "update stock_card set actual_qty=" & sumQty.ToString.Replace(",", ".") & ",sum_qty=" & sumQty.ToString.Replace(",", ".") & " where status='Production Request' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") & "'"
                                        Dim dtUpdateToStockCardqty = New SqlCommand(queryUpdateToStockCardqty, Database.koneksi)
                                        'Update perhitungan Qty Reject dari stock card production request 

                                        If dtUpdateToStockCardqty.ExecuteNonQuery() Then

                                            'Generate Production Process
                                            If dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") = dtCheckFlowTicketinTemp.Rows(0).Item("lot_no") Then
                                                Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & sumQty.ToString.Replace(",", ".") & ",@material='" & splitPN(i) & "',@lot_material='" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") & "'"
                                                Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                                            Else
                                                Dim sqlQtyLot = "select * from stock_card where status='Production Request' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") & "'"
                                                Dim dtQtyLot As DataTable = Database.GetData(sqlQtyLot)
                                                Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtQtyLot.Rows(0).Item("sum_qty").ToString.Replace(",", ".") & ",@material='" & splitPN(i) & "',@lot_material='" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") & "'"
                                                Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                                            End If
                                            'Generate Production Process

                                            If dtCheckSumDefectQty.Rows(0).Item(0) < qtyReject Then

                                                'insert ke Defect
                                                Dim sqlInsertRejectPN As String = "INSERT INTO out_prod_defect (CODE_OUT_PROD_DEFECT, sub_sub_po, FG_PN, PART_NUMBER, LOT_NO, TRACEABILITY,INV_CTRL_DATE,BATCH_NO,QTY,PO,PROCESS_REJECT,LINE,FLOW_TICKET_NO,DEPARTMENT,PENGALI,INPUT_FROM_FG,ACTUAL_QTY)
                                                    VALUES ('" & codeReject & "','" & txtSubSubPODefective.Text & "','" & cbFGPN.Text & "','" & splitPN(i) & "','" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("lot_no") & "',
                                                    '" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("TRACEABILITY") & "','" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("INV_CTRL_DATE") & "','" & dtCheckTotalMaterial.Rows(TotalMaterial).Item("BATCH_NO") & "',
                                                    " & qtyRejectLoop.ToString.Replace(",", ".") & ",'" & cbPONumber.Text & "','" & process & "','" & cbLineNumber.Text & "','" & splitFlowTicket(5) & "','" & dept & "'," & qty & ",'" & input_from_fg & "'," & qtyRejectLoop.ToString.Replace(",", ".") & ")"
                                                Dim cmdInsertRejectPN = New SqlCommand(sqlInsertRejectPN, Database.koneksi)
                                                cmdInsertRejectPN.ExecuteNonQuery()
                                                'insert ke Defect
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Next

                        'Update Fifo
                        Dim queryCheckProductionProcess As String = "select * from stock_card where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' order by id"
                        Dim dtCheckProductionProcess As DataTable = Database.GetData(queryCheckProductionProcess)
                        For production_proc = 0 To dtCheckProductionProcess.Rows.Count - 1
                            If lot <> dtCheckProductionProcess.Rows(production_proc).Item("lot_no").ToString Then
                                Dim queryUpdateFifo As String = "update stock_card set fifo=" & fifo & " where status='Production Process' and material='" & splitPN(i) & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtCheckProductionProcess.Rows(production_proc).Item("lot_no") & "'"
                                Dim dtUpdateFifo = New SqlCommand(queryUpdateFifo, Database.koneksi)

                                If dtUpdateFifo.ExecuteNonQuery() Then
                                    lot = dtCheckProductionProcess.Rows(production_proc).Item("lot_no")
                                    fifo = fifo + 1
                                End If
                            End If
                        Next
                        'Update Fifo

                        'delete temporary data
                        Dim queryDeleteStockCardTemporary As String = "delete from stock_card_temporary where status='Production Process' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                        Dim dtDeleteStockCardTemporary = New SqlCommand(queryDeleteStockCardTemporary, Database.koneksi)
                        If dtDeleteStockCardTemporary.ExecuteNonQuery() Then
                            sResult *= 1
                        Else
                            sResult *= 0
                        End If
                        'delete temporary data

                        'Kebutuhan traceabiity
                        'Dim dtOutDefect As DataTable = Database.GetData("select isnull(sum(actual_qty),0) from out_prod_defect where sub_sub_po='" & txtSubSubPODefective.Text & "' and part_number='" & splitPN(i) & "'")

                        'Dim queryUpdateStockCardProdReq As String = "update summary_fg set defect_out=" & dtOutDefect.Rows(0)(0).ToString.Replace(",", ".") & " where sub_sub_po='" & txtSubSubPODefective.Text & "' and material='" & splitPN(i) & "'"
                        'Dim dtUpdateStockCardProdReq = New SqlCommand(queryUpdateStockCardProdReq, Database.koneksi)
                        'dtUpdateStockCardProdReq.ExecuteNonQuery()
                        'Kebutuhan traceabiity
                    Next
                End If
            End If

            Return sResult
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try

    End Function

    Private Sub txtSAFlowTicket_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtSAFlowTicket.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
            If txtSAFlowTicket.Text <> "" Then
                Dim sLine = cbLineNumber.Text.Split(" ")

                BandingkanJam(DateTime.Now.TimeOfDay)

                Dim tahun As Integer = DateTime.Now.Year
                Dim digitTerakhir As Integer = tahun Mod 10

                Dim batchFormat = "JV" & digitTerakhir & Now.ToString("MMdd") & sLine(1) & globVar.shift

                txtSABatchNo.Text = batchFormat
                txtSABatchNo.ReadOnly = True
                TextBox6.Select()
                txtLossQty.ReadOnly = True

                loadSA(cbFGPN.Text, txtSAFlowTicket.Text)
                btnSaveSADefect.Enabled = True
                'btnPrintSADefect.Enabled = True
                DataGridView3.Enabled = True
                txtSAFlowTicket.ReadOnly = True
                btnSaveSA.Enabled = True
                btnResetSA.Enabled = True
            End If
        End If
    End Sub

    Private Sub btnPrintBalance_Click(sender As Object, e As EventArgs) Handles btnPrintBalance.Click
        If globVar.view > 0 Then

            If txtSubSubPODefective.Text <> "" Then
                Try
                    Dim query As String = "select sc.id_level,sc.material,m.name,sc.traceability,sc.inv_ctrl_date,sc.batch_no,sc.lot_no,sc.actual_qty,sc.lot_no from stock_card sc, master_material m where sc.status='Return To Mini Store' and sc.sub_sub_po='" & txtSubSubPODefective.Text & "' and sc.actual_qty>0 and sc.material=m.part_number"
                    Dim dtCheckStockBalance As DataTable = Database.GetData(query)
                    If dtCheckStockBalance.Rows.Count > 0 Then
                        globVar.failPrint = ""
                        _PrintingSubAssyRawMaterial.txt_Unique_id.Text = dtCheckStockBalance.Rows(0).Item("id_level")
                        For i = 0 To dtCheckStockBalance.Rows.Count - 1
                            _PrintingSubAssyRawMaterial.txt_part_number.Text += dtCheckStockBalance.Rows(i).Item("material") & "(" & dtCheckStockBalance.Rows(i).Item("lot_no") & "),"
                        Next
                        _PrintingSubAssyRawMaterial.txt_jenis_ticket.Text = "Balance Material"
                        _PrintingSubAssyRawMaterial.txt_QR_Code.Text = dtCheckStockBalance.Rows(0).Item("id_level") & Environment.NewLine
                        _PrintingSubAssyRawMaterial.btn_Print_Click(sender, e)

                        If globVar.failPrint = "No" Then
                            Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, remark,sub_sub_po,department,code_print)
                                VALUES ('" & cbPONumber.Text & "','" & cbFGPN.Text & "','" & cbLineNumber.Text & "','Balance Material','" & txtSubSubPODefective.Text & "','" & dept & "','" & dtCheckStockBalance.Rows(0).Item("id_level") & "')"
                            Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                            cmdInsertPrintingRecord.ExecuteNonQuery()
                        End If
                    End If
                Catch ex As Exception
                    RJMessageBox.Show(ex.Message)
                End Try
            Else
                RJMessageBox.Show("Sorry please input Sub Sub PO First.")
            End If
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles btnPrintFGDefect.Click
        If txtSubSubPODefective.Text <> "" Then
            Dim countPrint = 0
            Dim query As String = "select DISTINCT(CODE_OUT_PROD_DEFECT),[print],flow_ticket_no from out_prod_DEFECT where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and line='" & cbLineNumber.Text & "' and input_from_fg=1"
            Dim dtCheckStockReject As DataTable = Database.GetData(query)
            If dtCheckStockReject.Rows.Count > 0 Then
                For i = 0 To dtCheckStockReject.Rows.Count - 1
                    If dtCheckStockReject.Rows(i).Item("print") = 0 Then
                        globVar.failPrint = ""
                        _PrintingDefect.txt_QR_Code.Text = dtCheckStockReject.Rows(i).Item("CODE_OUT_PROD_DEFECT") & Environment.NewLine
                        _PrintingDefect.Label2.Text = "Defect Ticket"
                        _PrintingDefect.txt_Unique_id.Text = dtCheckStockReject.Rows(i).Item("CODE_OUT_PROD_DEFECT")
                        _PrintingDefect.txt_part_number.Text = cbFGPN.Text
                        _PrintingDefect.txt_Part_Description.Text = txtDescDefective.Text
                        _PrintingDefect.txt_Traceability.Text = txtTampungLabel.Text
                        _PrintingDefect.txt_Inv_crtl_date.Text = txtINV.Text
                        _PrintingDefect.btn_Print_Click(sender, e)

                        If globVar.failPrint = "No" Then
                            Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, remark,sub_sub_po,department,flow_ticket,code_print)
                                VALUES ('" & cbPONumber.Text & "','" & cbFGPN.Text & "','" & cbLineNumber.Text & "','Reject Material','" & txtSubSubPODefective.Text & "','" & dept & "','" & dtCheckStockReject.Rows(i).Item("flow_ticket_no") & "','" & dtCheckStockReject.Rows(i).Item("CODE_OUT_PROD_DEFECT") & "')"
                            Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                            cmdInsertPrintingRecord.ExecuteNonQuery()

                            Dim sqlupdateDefect As String = "update out_prod_defect set [print]=1 where CODE_OUT_PROD_DEFECT='" & dtCheckStockReject.Rows(i).Item("CODE_OUT_PROD_DEFECT") & "'"
                            Dim cmdupdateDefect = New SqlCommand(sqlupdateDefect, Database.koneksi)
                            cmdupdateDefect.ExecuteNonQuery()
                        End If
                    Else
                        countPrint += 1
                    End If
                Next

                If dtCheckStockReject.Rows.Count = countPrint Then
                    Dim result As DialogResult = RJMessageBox.Show("All defects have been printed. Are you sure you want to print again?", "Attention", MessageBoxButtons.YesNo)
                    If result = DialogResult.Yes Then
                        For i = 0 To dtCheckStockReject.Rows.Count - 1
                            globVar.failPrint = ""
                            _PrintingDefect.txt_QR_Code.Text = dtCheckStockReject.Rows(i).Item("CODE_OUT_PROD_DEFECT") & Environment.NewLine
                            _PrintingDefect.Label2.Text = "Defect Ticket"
                            _PrintingDefect.txt_Unique_id.Text = dtCheckStockReject.Rows(i).Item("CODE_OUT_PROD_DEFECT")
                            _PrintingDefect.txt_part_number.Text = cbFGPN.Text
                            _PrintingDefect.txt_Part_Description.Text = txtDescDefective.Text
                            _PrintingDefect.txt_Traceability.Text = txtTampungLabel.Text
                            _PrintingDefect.txt_Inv_crtl_date.Text = txtINV.Text
                            _PrintingDefect.btn_Print_Click(sender, e)

                            If globVar.failPrint = "No" Then
                                Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, remark,sub_sub_po,department,flow_ticket,code_print)
                                        VALUES ('" & cbPONumber.Text & "','" & cbFGPN.Text & "','" & cbLineNumber.Text & "','Reject Material (Again)','" & txtSubSubPODefective.Text & "','" & dept & "','" & dtCheckStockReject.Rows(i).Item("flow_ticket_no") & "','" & dtCheckStockReject.Rows(i).Item("CODE_OUT_PROD_DEFECT") & "')"
                                Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                                cmdInsertPrintingRecord.ExecuteNonQuery()
                            End If
                        Next
                    End If
                End If
            Else
                RJMessageBox.Show("This Sub Sub PO dont have Defect Data")
            End If
        Else
            RJMessageBox.Show("Sorry please input Sub Sub PO First.")
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles btnListPrintWIP.Click
        If txtSubSubPODefective.Text <> "" Then
            Dim _formListPrint As New ListPrint("Print WIP", "WIP", txtSubSubPODefective.Text, cbLineNumber.Text, txtDescDefective.Text, txtINV.Text)
            _formListPrint.ShowDialog()
        Else
            RJMessageBox.Show("Sorry, please input Sub Sub PO First.")
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles btnResetFG.Click
        ClearInputFG()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles btnResetSA.Click
        ClearInputFG()
    End Sub

    Sub ClearInputFG()
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
        btnResetFG.Enabled = False
        'btnPrintFGDefect.Enabled = False
        btnSaveFGDefect.Enabled = False
        btnSaveFG.Enabled = False
        'btnPrintSADefect.Enabled = False
        btnSaveFGDefect.Enabled = False
        btnSaveSA.Enabled = False
        btnSaveSADefect.Enabled = False
        btnResetSA.Enabled = False
        ckLossQty.CheckState = CheckState.Unchecked
        txtLossQty.Clear()
        If rbSA.Checked = True Then
            txtSAFlowTicket.Select()
        End If

        If rbFG.Checked = True Then
            txtFGLabel.Select()
        End If
    End Sub

    Public Sub btnPrintWIP_Click(sender As Object, e As EventArgs) Handles btnPrintWIP.Click
        If globVar.view > 0 Then

            Try
                Dim queryDoneFG As String = "select * from done_fg where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg='" & cbFGPN.Text & "' and line='" & cbLineNumber.Text & "'"
                Dim dtDoneFG As DataTable = Database.GetData(queryDoneFG)

                Dim queryFT As String = "select * from flow_ticket where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg='" & cbFGPN.Text & "' and line='" & cbLineNumber.Text & "'"
                Dim dtDoneFT As DataTable = Database.GetData(queryFT)

                If dtDoneFT.Rows.Count = dtDoneFG.Rows.Count Then
                    If txtSubSubPODefective.Text <> "" Then
                        If txtWIPTicketNo.Text <> "" Then
                            Dim split() As String = txtWIPTicketNo.Text.Split(";")
                            Dim query As String = "select code_stock_prod_wip,flow_ticket_no,process,pengali from stock_prod_wip where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and line='" & cbLineNumber.Text & "' and flow_ticket_no='" & split(5) & "' group by code_stock_prod_wip,flow_ticket_no,process,pengali"
                            Dim dt As DataTable = Database.GetData(query)
                            If dt.Rows.Count > 0 Then
                                For i = 0 To dt.Rows.Count - 1
                                    globVar.failPrint = ""
                                    _PrintingWIPOnHold.txt_QR_Code.Text = dt.Rows(i).Item("code_stock_prod_wip") & Environment.NewLine
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
                                RJMessageBox.Show("Dont have WIP Data")
                            End If
                        Else
                            Dim query As String = "select code_stock_prod_wip,flow_ticket_no,process,pengali from stock_prod_wip where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and line='" & cbLineNumber.Text & "' group by code_stock_prod_wip,flow_ticket_no,process,pengali"
                            Dim dt As DataTable = Database.GetData(query)
                            If dt.Rows.Count > 0 Then
                                For i = 0 To dt.Rows.Count - 1
                                    globVar.failPrint = ""
                                    _PrintingWIPOnHold.txt_QR_Code.Text = dt.Rows(i).Item("code_stock_prod_wip") & Environment.NewLine
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
                                RJMessageBox.Show("Dont have WIP Data")
                            End If
                        End If
                    Else
                        RJMessageBox.Show("Sorry please input Sub Sub PO First.")
                    End If
                Else
                    RJMessageBox.Show("cannot print now because the PO is not yet completed.")
                End If
            Catch ex As Exception
                RJMessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnPrintOnhold_Click(sender As Object, e As EventArgs) Handles btnPrintOnhold.Click
        If globVar.view > 0 Then

            Try
                If txtSubSubPODefective.Text <> "" Then
                    If txtOnHoldTicketNo.Text <> "" Then
                        Dim split() As String = txtOnHoldTicketNo.Text.Split(";")
                        Dim query As String = "select code_stock_prod_onhold,flow_ticket_no,process,pengali from stock_prod_onhold where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and line='" & cbLineNumber.Text & "' and flow_ticket_no='" & split(5) & "' group by code_stock_prod_onhold,flow_ticket_no,process,pengali"
                        Dim dt As DataTable = Database.GetData(query)
                        If dt.Rows.Count > 0 Then
                            For i = 0 To dt.Rows.Count - 1

                                globVar.failPrint = ""
                                _PrintingWIPOnHold.txt_QR_Code.Text = dt.Rows(i).Item("code_stock_prod_onhold") & Environment.NewLine
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
                            RJMessageBox.Show("Dont have On Hold Data")
                        End If
                    Else
                        Dim query As String = "select code_stock_prod_onhold,flow_ticket_no,process,pengali from stock_prod_onhold where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg_pn='" & cbFGPN.Text & "' and line='" & cbLineNumber.Text & "' group by code_stock_prod_onhold,flow_ticket_no,process,pengali"
                        Dim dt As DataTable = Database.GetData(query)
                        If dt.Rows.Count > 0 Then
                            For i = 0 To dt.Rows.Count - 1

                                globVar.failPrint = ""
                                _PrintingWIPOnHold.txt_QR_Code.Text = dt.Rows(i).Item("code_stock_prod_onhold") & Environment.NewLine
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
                            RJMessageBox.Show("Dont have On Hold Data")
                        End If
                    End If
                Else
                    RJMessageBox.Show("Sorry please input Sub Sub PO First.")
                End If
            Catch ex As Exception
                RJMessageBox.Show(ex.Message)
            End Try
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
        txtBalanceQty.Clear()
        txtReturnMaterialManual.Clear()
        TextBox2.Clear()
        txtBalanceQty.Enabled = False
        txtBalanceQty.Clear()
        TextBox10.Clear()
        tampungIDMaterialReturnMaterial.Clear()
        txtReturnMaterialPN.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles CheckManualReturnMaterial.Click

        If txtReturnMaterialManual.Text = "" Or ComboBox2.Text = "" Then
            RJMessageBox.Show("Part Number or Lot No Still blank. Please fill first")
            Exit Sub
        End If

        If globVar.view > 0 Then

            Try

                Dim allmanualMaterial As String() = ComboBox2.Text.Split("|")
                Dim lotManualMaterial As String() = allmanualMaterial(0).Split(":")
                Dim icdManualMaterial As String() = allmanualMaterial(1).Split(":")
                Dim traceManualMaterial As String() = allmanualMaterial(2).Split(":")
                Dim batchManualMaterial As String() = allmanualMaterial(3).Split(":")

                Dim queryCheck As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' 
                        and material='" & txtReturnMaterialManual.Text & "' 
                        and status='Production Process' 
                        and department='" & globVar.department & "' 
                        and traceability='" & traceManualMaterial(1) & "'
                        and inv_ctrl_date='" & icdManualMaterial(1) & "'
                        and batch_no='" & batchManualMaterial(1) & "'
                        and lot_no='" & lotManualMaterial(1) & "'"
                Dim dttable As DataTable = Database.GetData(queryCheck)
                If dttable.Rows.Count > 0 Then

                    txtReturnMaterialPN.Text = txtReturnMaterialManual.Text
                    txtBalanceQty.Text = dttable.Rows(0).Item("actual_qty")
                    tampungIDMaterialReturnMaterial.Text = dttable.Rows(0).Item("id")
                    TextBox10.Text = lotManualMaterial(1)
                    txtBalanceQty.Enabled = True

                Else

                    RJMessageBox.Show("This material not exist in Database.")
                    txtBalanceBarcode.Clear()
                    txtBalanceMaterialPN.Clear()

                End If
            Catch ex As Exception
                RJMessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.CheckState = CheckState.Unchecked Then
            TableLayoutPanel19.Enabled = True
            TableLayoutPanel20.Enabled = True
            txtRejectBarcode.Enabled = False
        Else
            TableLayoutPanel19.Enabled = False
            TableLayoutPanel20.Enabled = False
            txtRejectBarcode.Enabled = True
        End If
        txtRejectBarcode.Clear()
        txtRejectMaterialPN.Clear()
        txtRejectQty.Clear()
        txtRejectMaterialManual.Clear()
        ComboBox1.SelectedIndex = -1
        txtRejectQty.Enabled = False
        txtRejectQty.Clear()
        txtRejectMaterialManual.Clear()
        tampungIDMaterial.Clear()
        TextBox4.Clear()
    End Sub

    Private Sub CheckManualRejectMaterial_Click(sender As Object, e As EventArgs) Handles CheckManualRejectMaterial.Click
        Try
            If txtSubSubPODefective.Text <> "" Then
                If ComboBox1.Text <> "" And txtRejectMaterialManual.Text <> "" Then

                    Dim allmanualMaterial As String() = ComboBox1.Text.Split("|")
                    Dim lotManualMaterial As String() = allmanualMaterial(0).Split(":")
                    Dim icdManualMaterial As String() = allmanualMaterial(1).Split(":")
                    Dim traceManualMaterial As String() = allmanualMaterial(2).Split(":")
                    Dim batchManualMaterial As String() = allmanualMaterial(3).Split(":")

                    Dim queryCheck As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' 
                        and material='" & txtRejectMaterialManual.Text & "' 
                        and status='Production Process' 
                        and department='" & globVar.department & "' 
                        and traceability='" & traceManualMaterial(1) & "' 
                        and inv_ctrl_date='" & icdManualMaterial(1) & "' 
                        and batch_no='" & batchManualMaterial(1) & "' 
                        and lot_no='" & lotManualMaterial(1) & "'"
                    Dim dttable As DataTable = Database.GetData(queryCheck)
                    If dttable.Rows.Count > 0 Then
                        loaddgReject(txtRejectMaterialManual.Text)
                        TextBox4.Text = lotManualMaterial(1)
                        txtRejectMaterialPN.Text = txtRejectMaterialManual.Text
                        tampungIDMaterial.Text = dttable.Rows(0).Item("id")
                        txtRejectQty.Enabled = True
                    Else
                        RJMessageBox.Show("This material not exist in Database.")
                        txtRejectMaterialManual.Clear()
                        ComboBox1.SelectedIndex = -1
                        txtRejectQty.Clear()
                    End If
                Else
                    RJMessageBox.Show("Part Number or Lot No Still blank. Please input first")
                End If
            Else

                RJMessageBox.Show("Sorry, please input Sub Sub PO First")
                txtRejectMaterialManual.Clear()
                ComboBox1.SelectedIndex = -1
                txtRejectQty.Clear()

            End If
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRejectSave_Click(sender As Object, e As EventArgs) Handles btnRejectSave.Click
        If globVar.add > 0 Then

            If (CheckBox3.CheckState = CheckState.Checked And txtRejectBarcode.Text <> "" And txtRejectMaterialPN.Text <> "" And txtRejectQty.Text <> "") Or (CheckBox3.CheckState = CheckState.Unchecked And txtRejectMaterialManual.Text <> "" And ComboBox1.Text <> "" And txtRejectQty.Text <> "") Then
                Dim _dQtyReject As Double
                Dim _sPNReject As String

                If CheckBox3.CheckState = CheckState.Checked Then
                    _dQtyReject = txtRejectQty.Text
                    _sPNReject = txtRejectMaterialPN.Text
                Else
                    _dQtyReject = txtRejectQty.Text
                    _sPNReject = txtRejectMaterialManual.Text
                End If

                If txtStatusSubSubPO.Text = "Closed" Then
                    RJMessageBox.Show("Sorry This Sub Sub PO status is closed. Cannot use this menu.")
                    Exit Sub
                End If

                Try

                    Dim result = RJMessageBox.Show("Are you sure for save?.", "Are You Sure?", MessageBoxButtons.YesNo)

                    If result = DialogResult.Yes Then

                        Dim queryCheckQty = "select * from stock_card where id=" & tampungIDMaterial.Text

                        Dim dtCheckQty As DataTable = Database.GetData(queryCheckQty)

                        If dtCheckQty.Rows.Count <= 0 Then

                            RJMessageBox.Show("Sorry this material doesnt exist in database")
                            Exit Sub

                        End If

                        If dtCheckQty.Rows(0).Item("actual_qty") >= Convert.ToDouble(txtRejectQty.Text) Then

                            Dim SaveQtySC = dtCheckQty.Rows(0).Item("actual_qty") - Convert.ToDouble(txtRejectQty.Text)

                            Dim queryUpdateactualQty As String = "update stock_card set actual_qty=" & Convert.ToDouble(SaveQtySC) & " where id=" & tampungIDMaterial.Text
                            Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                            If dtUpdateactualQty.ExecuteNonQuery() Then

                                Dim rCode As String = RejectGenerateCode()

                                Dim sql As String = "INSERT INTO out_prod_reject(CODE_OUT_PROD_REJECT,SUB_SUB_PO,FG_PN,PART_NUMBER,LOT_NO,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,QTY,PO,DEPARTMENT,LINE,SUB_ASSY) 
                                select '" & rCode & "',SUB_SUB_PO,FINISH_GOODS_PN,MATERIAL,LOT_NO,TRACEABILITY,INV_CTRL_DATE,BATCH_NO," & txtRejectQty.Text & ",PO,DEPARTMENT,LINE,'" & txtRejectBarcode.Text & "' from stock_card where id=" & tampungIDMaterial.Text
                                Dim cmd = New SqlCommand(sql, Database.koneksi)
                                If cmd.ExecuteNonQuery() Then

                                    RJMessageBox.Show("Success Save data!!!")
                                    loaddgReject("")
                                    txtRejectBarcode.Clear()
                                    txtRejectMaterialPN.Clear()
                                    txtRejectQty.Clear()
                                    txtRejectMaterialManual.Clear()
                                    ComboBox1.SelectedIndex = -1
                                    TextBox4.Clear()
                                    txtRejectQty.Enabled = False

                                Else

                                    RJMessageBox.Show("Failed Save Reject Material!!!")

                                End If

                            End If

                        Else

                            loaddgReject("")
                            txtRejectBarcode.Clear()
                            txtRejectMaterialPN.Clear()
                            txtRejectQty.Clear()
                            txtRejectMaterialManual.Clear()
                            ComboBox1.SelectedIndex = -1
                            TextBox4.Clear()
                            txtRejectQty.Enabled = False
                            RJMessageBox.Show("Qty for this material is not enough")

                        End If

                    End If

                Catch ex As Exception
                    RJMessageBox.Show("Error Save Reject : " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else
                RJMessageBox.Show("Material Label & Qty cannot be blank.")
            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub btnRejectDelete_Click(sender As Object, e As EventArgs) Handles btnRejectDelete.Click
        If globVar.delete > 0 Then
            If (CheckBox3.CheckState = CheckState.Checked And txtRejectBarcode.Text <> "" And txtRejectMaterialPN.Text <> "") Or (CheckBox3.CheckState = CheckState.Unchecked And txtRejectMaterialManual.Text <> "" And ComboBox1.Text <> "") Then
                Dim _sPNReject As String

                If CheckBox3.CheckState = CheckState.Checked Then
                    _sPNReject = txtRejectMaterialPN.Text
                Else
                    _sPNReject = txtRejectMaterialManual.Text
                End If

                If txtStatusSubSubPO.Text = "Closed" Then
                    RJMessageBox.Show("Sorry This Sub Sub PO status is closed. Cannot use this menu.")
                    Exit Sub
                End If

                Try
                    Dim result = RJMessageBox.Show("Are you sure for delete?.", "Are You Sure?", MessageBoxButtons.YesNo)

                    If result = DialogResult.Yes Then

                        Dim statusSimpan As Integer = 1
                        Dim sumQty As Double = 0

                        Dim queryCheckReject As String = "select * from out_prod_reject where part_number='" & _sPNReject & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and fg_pn='" & cbFGPN.Text & "' and lot_no='" & TextBox4.Text & "'"
                        Dim dtCheckReject As DataTable = Database.GetData(queryCheckReject)

                        If dtCheckReject.Rows.Count > 0 Then
                            Dim queryUpdateQtyReject As String = "delete from out_prod_reject where id=" & dtCheckReject.Rows(0).Item("ID")
                            Dim dtUpdateQtyReject = New SqlCommand(queryUpdateQtyReject, Database.koneksi)
                            If dtUpdateQtyReject.ExecuteNonQuery() Then

                                Dim queryInsertToTemp As String = "insert into stock_card_temporary([MTS_NO], [DEPARTMENT], [MATERIAL], [STATUS], [STANDARD_PACK], [INV_CTRL_DATE], 
                                        [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [DATETIME_INSERT], [SAVE], [QRCODE], [DATETIME_SAVE], 
                                        [QTY], [ACTUAL_QTY], [FIFO], [ID_LEVEL], [LEVEL], [FLOW_TICKET]) 
                                        select [MTS_NO], [DEPARTMENT], [MATERIAL], [STATUS], [STANDARD_PACK], [INV_CTRL_DATE], [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], 
                                        [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [DATETIME_INSERT], [SAVE], [QRCODE], [DATETIME_SAVE], [QTY], [ACTUAL_QTY], [FIFO], [ID_LEVEL], [LEVEL], [FLOW_TICKET] 
                                        from stock_card where status='Production Process' and material='" & _sPNReject & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' AND [LEVEL]='Fresh'"
                                Dim dtInsertTemp = New SqlCommand(queryInsertToTemp, Database.koneksi)
                                If dtInsertTemp.ExecuteNonQuery() Then
                                    Dim queryDeleteStockCard As String = "delete from stock_card where status='Production Process' and material='" & _sPNReject & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' AND [LEVEL]='Fresh'"
                                    Dim dtDeleteStockCard = New SqlCommand(queryDeleteStockCard, Database.koneksi)
                                    If dtDeleteStockCard.ExecuteNonQuery() Then
                                        Dim queryCheckSumUsage As String = "select * from stock_card where status='Production Request' and material='" & _sPNReject & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & TextBox4.Text & "' AND [LEVEL]='Fresh'"
                                        Dim dtCheckSumUsage As DataTable = Database.GetData(queryCheckSumUsage)

                                        sumQty = dtCheckSumUsage.Rows(0).Item("sum_qty") + dtCheckReject.Rows(0).Item("qty")

                                        Dim queryUpdateToStockCardqty As String = "update stock_card set actual_qty=" & sumQty.ToString.Replace(",", ".") & ",sum_qty=" & sumQty.ToString.Replace(",", ".") & " where status='Production Request' and material='" & _sPNReject & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & TextBox4.Text & "' AND [LEVEL]='Fresh'"
                                        Dim dtUpdateToStockCardqty = New SqlCommand(queryUpdateToStockCardqty, Database.koneksi)
                                        If dtUpdateToStockCardqty.ExecuteNonQuery() Then
                                            Dim queryDistintLotNoinTemp As String = "select DISTINCT(lot_no) from stock_card_temporary where status='Production Process' and material='" & _sPNReject & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
                                            Dim dtDistintLotNoinTemp As DataTable = Database.GetData(queryDistintLotNoinTemp)
                                            If dtDistintLotNoinTemp.Rows.Count > 0 Then
                                                For iDistint = 0 To dtDistintLotNoinTemp.Rows.Count - 1
                                                    If dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") = TextBox4.Text Then
                                                        Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & sumQty.ToString.Replace(",", ".") & ",@material='" & _sPNReject & "',@lot_material='" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") & "'"
                                                        Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                                                    Else
                                                        Dim queryUpdateactualQty As String = "update stock_card set actual_qty=sum_qty where status='Production Request' and material='" & _sPNReject & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") & "' AND [LEVEL]='Fresh'"
                                                        Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                                                        If dtUpdateactualQty.ExecuteNonQuery() Then
                                                            Dim sqlQtyLot = "select * from stock_card where status='Production Request' and material='" & _sPNReject & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") & "' AND [LEVEL]='Fresh'"
                                                            Dim dtQtyLot As DataTable = Database.GetData(sqlQtyLot)
                                                            Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtQtyLot.Rows(0).Item("sum_qty").ToString.Replace(",", ".") & ",@material='" & _sPNReject & "',@lot_material='" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") & "'"
                                                            Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
                                                        End If
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
                            End If
                        Else
                            RJMessageBox.Show("Sorry the data not exist in DB. Cannot delete.")
                            loaddgReject("")
                            txtRejectBarcode.Clear()
                            txtRejectMaterialPN.Clear()
                            txtRejectMaterialManual.Clear()
                            ComboBox1.SelectedIndex = -1
                            txtRejectQty.Clear()
                            TextBox4.Clear()
                            Exit Sub
                        End If

                        'Dim queryUpdateStockCardProdReq As String = "update summary_fg set return_out=0 where sub_sub_po='" & txtSubSubPODefective.Text & "' and material='" & _sPNReject & "'"
                        'Dim dtUpdateStockCardProdReq = New SqlCommand(queryUpdateStockCardProdReq, Database.koneksi)
                        'dtUpdateStockCardProdReq.ExecuteNonQuery()

                        If statusSimpan > 0 Then
                            RJMessageBox.Show("Success Delete data!!!")
                            loaddgReject("")
                            txtRejectBarcode.Clear()
                            txtRejectMaterialPN.Clear()
                            txtRejectQty.Clear()
                            txtRejectMaterialManual.Clear()
                            ComboBox1.SelectedIndex = -1
                            TextBox4.Clear()
                        Else
                            RJMessageBox.Show("Fail Delete data!!!")
                            loaddgReject("")
                            txtRejectBarcode.Clear()
                            txtRejectMaterialPN.Clear()
                            txtRejectQty.Clear()
                            txtRejectMaterialManual.Clear()
                            ComboBox1.SelectedIndex = -1
                            TextBox4.Clear()
                        End If

                    End If

                Catch ex As Exception
                    RJMessageBox.Show("Error Delete Reject : " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else
                RJMessageBox.Show("Material Label & Qty cannot be blank.")
            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub txtRejectBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRejectBarcode.KeyDown
        Dim QrcodeValid As Boolean
        If e.KeyCode = Keys.Enter Then
            Try
                If txtSubSubPODefective.Text <> "" Then
                    tampungIDMaterial.Clear()

                    If txtRejectBarcode.Text.StartsWith("B") AndAlso txtRejectBarcode.Text.Length > 1 AndAlso IsNumeric(txtRejectBarcode.Text.Substring(1)) Then 'Balance

                        Dim queryCheck As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and qrcode='" & txtRejectBarcode.Text & "' and status='Production Process' and department='" & globVar.department & "'"
                        Dim dttable As DataTable = Database.GetData(queryCheck)
                        If dttable.Rows.Count > 0 Then
                            txtRejectMaterialPN.Text = dttable.Rows(0).Item("material")
                            TextBox4.Text = dttable.Rows(0).Item("lot_no")
                            loaddgReject(txtRejectMaterialPN.Text)
                            txtRejectQty.Enabled = True
                            txtRejectQty.Select()
                            btnRejectSave.Enabled = True
                            tampungIDMaterial.Text = dttable.Rows(0).Item("id")
                        Else
                            RJMessageBox.Show("This material not exist in Database.")
                            txtRejectBarcode.Clear()
                            txtRejectMaterialPN.Clear()
                            txtRejectQty.Clear()
                        End If

                    ElseIf txtRejectBarcode.Text.StartsWith("SM") AndAlso txtRejectBarcode.Text.Length > 2 AndAlso IsNumeric(txtRejectBarcode.Text.Substring(2)) Then 'Split Material

                        Dim queryCheck As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and qrcode='" & txtRejectBarcode.Text & "' and status='Production Process' and department='" & globVar.department & "'"
                        Dim dttable As DataTable = Database.GetData(queryCheck)
                        If dttable.Rows.Count > 0 Then
                            txtRejectMaterialPN.Text = dttable.Rows(0).Item("material")
                            TextBox4.Text = dttable.Rows(0).Item("lot_no")
                            loaddgReject(txtRejectMaterialPN.Text)
                            txtRejectQty.Enabled = True
                            txtRejectQty.Select()
                            btnRejectSave.Enabled = True
                            tampungIDMaterial.Text = dttable.Rows(0).Item("id")
                        Else
                            RJMessageBox.Show("This material not exist in Database.")
                            txtRejectBarcode.Clear()
                            txtRejectMaterialPN.Clear()
                            txtRejectQty.Clear()
                        End If

                    ElseIf txtRejectBarcode.Text.StartsWith("NQ") AndAlso txtRejectBarcode.Text.Length > 2 AndAlso IsNumeric(txtRejectBarcode.Text.Substring(2)) Then 'New Label

                        Dim queryCheck As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and qrcode_new='" & txtRejectBarcode.Text & "' and status='Production Process' and department='" & globVar.department & "'"
                        Dim dttable As DataTable = Database.GetData(queryCheck)
                        If dttable.Rows.Count > 0 Then
                            txtRejectMaterialPN.Text = dttable.Rows(0).Item("material")
                            TextBox4.Text = dttable.Rows(0).Item("lot_no")
                            loaddgReject(txtRejectMaterialPN.Text)
                            txtRejectQty.Enabled = True
                            txtRejectQty.Select()
                            btnRejectSave.Enabled = True
                            tampungIDMaterial.Text = dttable.Rows(0).Item("id")
                        Else
                            RJMessageBox.Show("This material not exist in Database.")
                            txtRejectBarcode.Clear()
                            txtRejectMaterialPN.Clear()
                            txtRejectQty.Clear()
                        End If

                    ElseIf txtRejectBarcode.Text.StartsWith("MX2D") Then 'Normal

                        QrcodeValid = QRCode.Baca(txtRejectBarcode.Text)

                        If QrcodeValid = False Then
                            RJMessageBox.Show("QRCode Not Valid")
                            Play_Sound.Wrong()
                            txtRejectBarcode.Clear()
                            Exit Sub
                        End If

                        If globVar.QRCode_PN = "" Or globVar.QRCode_lot = "" Or globVar.QRCode_Traceability = "" Or globVar.QRCode_Batch = "" Or globVar.QRCode_Inv = "" Then
                            RJMessageBox.Show("QRCode Not Valid")
                            Play_Sound.Wrong()
                            txtRejectBarcode.Clear()
                            Exit Sub
                        End If

                        Dim queryCheck As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and material='" & globVar.QRCode_PN & "' and lot_no='" & globVar.QRCode_lot & "' and traceability='" & globVar.QRCode_Traceability & "' and inv_ctrl_date='" & globVar.QRCode_Inv & "' and batch_no='" & globVar.QRCode_Batch & "' and status='Production Process' and department='" & globVar.department & "'"
                        Dim dttable As DataTable = Database.GetData(queryCheck)
                        If dttable.Rows.Count > 0 Then
                            txtRejectMaterialPN.Text = dttable.Rows(0).Item("material")
                            TextBox4.Text = dttable.Rows(0).Item("lot_no")
                            loaddgReject(txtRejectMaterialPN.Text)
                            txtRejectQty.Enabled = True
                            txtRejectQty.Select()
                            btnRejectSave.Enabled = True
                            tampungIDMaterial.Text = dttable.Rows(0).Item("id")
                        Else
                            RJMessageBox.Show("This material not exist in Database.")
                            txtRejectBarcode.Clear()
                            txtRejectMaterialPN.Clear()
                            txtRejectQty.Clear()
                        End If

                    ElseIf txtRejectBarcode.Text.StartsWith("SA") AndAlso txtRejectBarcode.Text.Length > 2 AndAlso IsNumeric(txtRejectBarcode.Text.Substring(2)) Then

                        Dim queryCheck As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and qrcode='" & txtRejectBarcode.Text & "' and status='Production Process' and department='" & globVar.department & "'"
                        Dim dttable As DataTable = Database.GetData(queryCheck)
                        If dttable.Rows.Count > 0 Then
                            txtRejectMaterialPN.Text = dttable.Rows(0).Item("material")
                            TextBox4.Text = dttable.Rows(0).Item("lot_no")
                            loaddgReject(txtRejectMaterialPN.Text)
                            txtRejectQty.Enabled = True
                            txtRejectQty.Select()
                            btnRejectSave.Enabled = True
                            tampungIDMaterial.Text = dttable.Rows(0).Item("id")
                        Else
                            RJMessageBox.Show("This material not exist in Database.")
                            txtRejectBarcode.Clear()
                            txtRejectMaterialPN.Clear()
                            txtRejectQty.Clear()
                        End If

                    Else

                        RJMessageBox.Show("QRCode not valid.")
                        Play_Sound.Wrong()
                        txtRejectBarcode.Clear()
                        Exit Sub

                    End If

                Else
                    RJMessageBox.Show("Sorry, please input Sub Sub PO First")
                    txtRejectBarcode.Clear()
                    txtRejectMaterialPN.Clear()
                    txtRejectQty.Clear()
                End If
            Catch ex As Exception
                RJMessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub dgReject_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgReject.CellValueChanged

        If e.RowIndex = -1 Then

            Exit Sub

        End If

        If e.ColumnIndex = -1 Then

            Exit Sub

        End If

        If globVar.update = 0 Then

            RJMessageBox.Show("Your Access cannot execute this action")
            Exit Sub

        End If

        If dgReject.Columns(e.ColumnIndex).Name = "Reject Qty" Then

            Dim sqlcheck As String = "select * from out_prod_reject where ID=" & dgReject.Rows(e.RowIndex).Cells("#").Value
            Dim dtCheck As DataTable = Database.GetData(sqlcheck)
            If dtCheck.Rows.Count > 0 Then

                If dtCheck.Rows(0).Item("qty") <> dgReject.Rows(e.RowIndex).Cells("Reject Qty").Value Then

                    Dim sqlcheckSCProcess As String = "select * from stock_card where sub_sub_po='" & txtSubSubPODefective.Text & "' and material='" & dgReject.Rows(e.RowIndex).Cells("Material").Value & "' and lot_no='" & dgReject.Rows(e.RowIndex).Cells("Lot No.").Value & "' and inv_ctrl_date='" & dgReject.Rows(e.RowIndex).Cells("Inv. Ctrl Date").Value & "' and traceability='" & dgReject.Rows(e.RowIndex).Cells("Traceability").Value & "' and batch_no='" & dgReject.Rows(e.RowIndex).Cells("Batch No.").Value & "' and status='Production Process' order by id"
                    Dim dtCheckSCProcess As DataTable = Database.GetData(sqlcheckSCProcess)

                    If dtCheckSCProcess.Rows(0).Item("actual_qty") - dgReject.Rows(e.RowIndex).Cells("Reject Qty").Value < 0 Then

                        RJMessageBox.Show("cannot change the qty because the addition makes the actual qty smaller than 0")
                        loaddgReject("")
                        Exit Sub

                    End If

                    If dtCheck.Rows(0).Item("qty") > dgReject.Rows(e.RowIndex).Cells("Reject Qty").Value Then

                        Dim SumQty = dtCheck.Rows(0).Item("qty") - dgReject.Rows(e.RowIndex).Cells("Reject Qty").Value

                        Dim queryUpdateactualQty As String = "update stock_card set actual_qty=actual_qty + " & SumQty & " where id=" & dtCheckSCProcess.Rows(0).Item("id")
                        Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                        If dtUpdateactualQty.ExecuteNonQuery() Then

                            Dim queryUpdateReject As String = "update out_prod_reject set qty=" & dgReject.Rows(e.RowIndex).Cells("Reject Qty").Value & " where id=" & dgReject.Rows(e.RowIndex).Cells("#").Value
                            Dim dtUpdateReject = New SqlCommand(queryUpdateReject, Database.koneksi)
                            If dtUpdateReject.ExecuteNonQuery() Then

                                RJMessageBox.Show("Update Qty Success")

                            End If

                        End If

                    Else

                        Dim SumQty = dgReject.Rows(e.RowIndex).Cells("Reject Qty").Value - dtCheck.Rows(0).Item("qty")

                        Dim queryUpdateactualQty As String = "update stock_card set actual_qty=actual_qty - " & SumQty & " where id=" & dtCheckSCProcess.Rows(0).Item("id")
                        Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                        If dtUpdateactualQty.ExecuteNonQuery() Then

                            Dim queryUpdateReject As String = "update out_prod_reject set qty=" & dgReject.Rows(e.RowIndex).Cells("Reject Qty").Value & " where id=" & dgReject.Rows(e.RowIndex).Cells("#").Value
                            Dim dtUpdateReject = New SqlCommand(queryUpdateReject, Database.koneksi)
                            If dtUpdateReject.ExecuteNonQuery() Then

                                RJMessageBox.Show("Update Qty Success")

                            End If

                        End If

                    End If

                Else

                    RJMessageBox.Show("Old and New qty is same")

                End If

            End If

        End If

        'If dgReject.CurrentCell.ColumnIndex = 6 Then
        '    If IsNumeric(dgReject.Rows(e.RowIndex).Cells(6).Value) = False Then
        '        RJMessageBox.Show("Sorry. Only number.")
        '        Exit Sub
        '    End If

        '    If (CheckBox3.CheckState = CheckState.Checked And txtRejectBarcode.Text = "") Or (txtRejectMaterialManual.Text = "" And ComboBox1.Text = "" And CheckBox3.CheckState = CheckState.Unchecked) Then
        '        RJMessageBox.Show("Please Set Label Material First.")
        '        Exit Sub
        '    End If

        '    Dim statusSimpan As Integer = 1
        '    Dim sumQty As Double = 0

        '    If Convert.ToDouble(TextBox5.Text) > Convert.ToDouble(dgReject.Rows(e.RowIndex).Cells(6).Value) Then
        '        Dim queryUpdateQtyReject As String = "update out_prod_reject set qty=" & Convert.ToDouble(dgReject.Rows(e.RowIndex).Cells(6).Value) & " where part_number='" & txtRejectMaterialPN.Text & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and fg_pn='" & cbFGPN.Text & "' and lot_no='" & TextBox4.Text & "'"
        '        Dim dtUpdateQtyReject = New SqlCommand(queryUpdateQtyReject, Database.koneksi)
        '        If dtUpdateQtyReject.ExecuteNonQuery() Then
        '            Dim queryInsertToTemp As String = "insert into stock_card_temporary([MTS_NO], [DEPARTMENT], [MATERIAL], [STATUS], [STANDARD_PACK], [INV_CTRL_DATE], 
        '            [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [DATETIME_INSERT], [SAVE], [QRCODE], [DATETIME_SAVE], 
        '            [QTY], [ACTUAL_QTY], [FIFO], [ID_LEVEL], [LEVEL], [FLOW_TICKET]) 
        '            select [MTS_NO], [DEPARTMENT], [MATERIAL], [STATUS], [STANDARD_PACK], [INV_CTRL_DATE], [TRACEABILITY], [BATCH_NO], [LOT_NO], [FINISH_GOODS_PN], 
        '            [PO], [SUB_PO], [SUB_SUB_PO], [LINE], [DATETIME_INSERT], [SAVE], [QRCODE], [DATETIME_SAVE], [QTY], [ACTUAL_QTY], [FIFO], [ID_LEVEL], [LEVEL], [FLOW_TICKET] 
        '            from stock_card where status='Production Process' and material='" & txtRejectMaterialPN.Text & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
        '            Dim dtInsertTemp = New SqlCommand(queryInsertToTemp, Database.koneksi)
        '            If dtInsertTemp.ExecuteNonQuery() Then
        '                Dim queryDeleteStockCard As String = "delete from stock_card where status='Production Process' and material='" & txtRejectMaterialPN.Text & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
        '                Dim dtDeleteStockCard = New SqlCommand(queryDeleteStockCard, Database.koneksi)
        '                If dtDeleteStockCard.ExecuteNonQuery() Then
        '                    Dim queryCheckSumUsage As String = "select * from stock_card where status='Production Request' and material='" & txtRejectMaterialPN.Text & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & TextBox4.Text & "'"
        '                    Dim dtCheckSumUsage As DataTable = Database.GetData(queryCheckSumUsage)

        '                    sumQty = dtCheckSumUsage.Rows(0).Item("sum_qty") + (Convert.ToDouble(TextBox5.Text) - Convert.ToDouble(dgReject.Rows(e.RowIndex).Cells(6).Value))

        '                    Dim queryUpdateToStockCardqty As String = "update stock_card set actual_qty=" & sumQty.ToString.Replace(",", ".") & ",sum_qty=" & sumQty.ToString.Replace(",", ".") & " where status='Production Request' and material='" & txtRejectMaterialPN.Text & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & TextBox4.Text & "'"
        '                    Dim dtUpdateToStockCardqty = New SqlCommand(queryUpdateToStockCardqty, Database.koneksi)
        '                    If dtUpdateToStockCardqty.ExecuteNonQuery() Then
        '                        Dim queryDistintLotNoinTemp As String = "select DISTINCT(lot_no) from stock_card_temporary where status='Production Process' and material='" & txtRejectMaterialPN.Text & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
        '                        Dim dtDistintLotNoinTemp As DataTable = Database.GetData(queryDistintLotNoinTemp)
        '                        If dtDistintLotNoinTemp.Rows.Count > 0 Then
        '                            For iDistint = 0 To dtDistintLotNoinTemp.Rows.Count - 1
        '                                If dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") = TextBox4.Text Then
        '                                    Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & sumQty.ToString.Replace(",", ".") & ",@material='" & txtRejectMaterialPN.Text & "',@lot_material='" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") & "'"
        '                                    Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
        '                                Else
        '                                    Dim sqlQtyLot = "select * from stock_card where status='Production Request' and material='" & txtRejectMaterialPN.Text & "' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "' and lot_no='" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") & "'"
        '                                    Dim dtQtyLot As DataTable = Database.GetData(sqlQtyLot)
        '                                    Dim sqlExeProcedure As String = "exec pCreateStockCardProdProcess @sub_sub_po='" & txtSubSubPODefective.Text & "', @fg='" & cbFGPN.Text & "',@line='" & cbLineNumber.Text & "',@dept='" & globVar.department & "',@qtyMaterial=" & dtQtyLot.Rows(0).Item("sum_qty").ToString.Replace(",", ".") & ",@material='" & txtRejectMaterialPN.Text & "',@lot_material='" & dtDistintLotNoinTemp.Rows(iDistint).Item("lot_no") & "'"
        '                                    Dim dtExeProcedure As DataTable = Database.GetData(sqlExeProcedure)
        '                                End If
        '                            Next
        '                        End If
        '                    End If
        '                End If
        '            End If

        '            Dim queryDeleteStockCardTemporary As String = "delete from stock_card_temporary where status='Production Process' and DEPARTMENT='" & globVar.department & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and line='" & cbLineNumber.Text & "' and FINISH_GOODS_PN='" & cbFGPN.Text & "'"
        '            Dim dtDeleteStockCardTemporary = New SqlCommand(queryDeleteStockCardTemporary, Database.koneksi)
        '            If dtDeleteStockCardTemporary.ExecuteNonQuery() Then
        '                statusSimpan *= 1
        '            Else
        '                statusSimpan *= 0
        '            End If

        '            If statusSimpan > 0 Then
        '                RJMessageBox.Show("Success save data!!!")
        '                loaddgReject("")
        '                txtRejectBarcode.Clear()
        '                txtRejectMaterialPN.Clear()
        '                txtRejectQty.Clear()
        '            End If
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub PrintDefect_Click(sender As Object, e As EventArgs) Handles PrintDefect.Click
        If globVar.view > 0 Then
            If txtSubSubPODefective.Text <> "" Then

                Dim _formListPrint As New ListPrint("Print Defect", "Defect", txtSubSubPODefective.Text, cbLineNumber.Text, txtDescDefective.Text, txtINV.Text)
                _formListPrint.ShowDialog()

            Else
                RJMessageBox.Show("Sorry, please input Sub Sub PO First.")
            End If
        End If
    End Sub

    Private Sub btnPrintSA_Click(sender As Object, e As EventArgs) Handles btnPrintSA.Click
        If globVar.view > 0 Then
            If txtSubSubPODefective.Text <> "" Then

                Dim _formListPrint As New ListPrint("Print Sub Assy", "Sub Assy", txtSubSubPODefective.Text, cbLineNumber.Text, txtDescDefective.Text, txtINV.Text)
                _formListPrint.ShowDialog()

                'Dim countPrint = 0
                'Dim query As String = "select DISTINCT(CODE_STOCK_PROD_SUB_ASSY),[print],flow_ticket,qty,traceability,inv_ctrl_date,batch_no,lot_no from STOCK_PROD_SUB_ASSY where sub_sub_po='" & txtSubSubPODefective.Text & "' and fg='" & cbFGPN.Text & "' and line='" & cbLineNumber.Text & "'"
                'Dim dtCheckStockSA As DataTable = Database.GetData(query)
                'If dtCheckStockSA.Rows.Count > 0 Then
                '    For i = 0 To dtCheckStockSA.Rows.Count - 1
                '        If dtCheckStockSA.Rows(i).Item("print") = 0 Then
                '            globVar.failPrint = ""
                '            _PrintingSubAssyRawMaterial.txt_jenis_ticket.Text = "Sub Assy"
                '            _PrintingSubAssyRawMaterial.txt_Unique_id.Text = dtCheckStockSA.Rows(i).Item("CODE_STOCK_PROD_SUB_ASSY")
                '            _PrintingSubAssyRawMaterial.txt_part_number.Text = cbFGPN.Text
                '            _PrintingSubAssyRawMaterial.txt_Part_Description.Text = txtDescDefective.Text
                '            _PrintingSubAssyRawMaterial.txt_Qty.Text = dtCheckStockSA.Rows(i).Item("qty")
                '            _PrintingSubAssyRawMaterial.txt_Traceability.Text = dtCheckStockSA.Rows(i).Item("traceability")
                '            _PrintingSubAssyRawMaterial.txt_Inv_crtl_date.Text = dtCheckStockSA.Rows(i).Item("inv_ctrl_date")
                '            _PrintingSubAssyRawMaterial.txt_Batch_no.Text = dtCheckStockSA.Rows(i).Item("batch_no")
                '            _PrintingSubAssyRawMaterial.txt_Lot_no.Text = dtCheckStockSA.Rows(i).Item("lot_no")
                '            _PrintingSubAssyRawMaterial.txt_QR_Code.Text = dtCheckStockSA.Rows(i).Item("CODE_STOCK_PROD_SUB_ASSY") & Environment.NewLine
                '            _PrintingSubAssyRawMaterial.btn_Print_Click(sender, e)

                '            If globVar.failPrint = "No" Then
                '                Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, remark,sub_sub_po,department,flow_ticket,code_print)
                '                    VALUES ('" & cbPONumber.Text & "','" & cbFGPN.Text & "','" & cbLineNumber.Text & "','Sub Assy','" & txtSubSubPODefective.Text & "','" & dept & "','" & dtCheckStockSA.Rows(i).Item("flow_ticket") & "','" & dtCheckStockSA.Rows(i).Item("CODE_STOCK_PROD_SUB_ASSY") & "')"
                '                Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                '                cmdInsertPrintingRecord.ExecuteNonQuery()

                '                Dim sqlupdateDefect As String = "update STOCK_PROD_SUB_ASSY set [print]=1 where CODE_STOCK_PROD_SUB_ASSY='" & dtCheckStockSA.Rows(i).Item("CODE_STOCK_PROD_SUB_ASSY") & "'"
                '                Dim cmdupdateDefect = New SqlCommand(sqlupdateDefect, Database.koneksi)
                '                cmdupdateDefect.ExecuteNonQuery()
                '            End If
                '        Else
                '            countPrint += 1
                '        End If
                '    Next

                '    If dtCheckStockSA.Rows.Count = countPrint Then
                '        Dim result As DialogResult = RJMessageBox.Show("All Sub Assy have been printed. Are you sure you want to print again?", "Attention", MessageBoxButtons.YesNo)
                '        If result = DialogResult.Yes Then
                '            For i = 0 To dtCheckStockSA.Rows.Count - 1
                '                globVar.failPrint = ""
                '                _PrintingSubAssyRawMaterial.txt_jenis_ticket.Text = "Sub Assy"
                '                _PrintingSubAssyRawMaterial.txt_Unique_id.Text = dtCheckStockSA.Rows(i).Item("CODE_STOCK_PROD_SUB_ASSY")
                '                _PrintingSubAssyRawMaterial.txt_part_number.Text = cbFGPN.Text
                '                _PrintingSubAssyRawMaterial.txt_Part_Description.Text = txtDescDefective.Text
                '                _PrintingSubAssyRawMaterial.txt_Qty.Text = dtCheckStockSA.Rows(i).Item("qty")
                '                _PrintingSubAssyRawMaterial.txt_Traceability.Text = dtCheckStockSA.Rows(i).Item("traceability")
                '                _PrintingSubAssyRawMaterial.txt_Inv_crtl_date.Text = dtCheckStockSA.Rows(i).Item("inv_ctrl_date")
                '                _PrintingSubAssyRawMaterial.txt_Batch_no.Text = dtCheckStockSA.Rows(i).Item("batch_no")
                '                _PrintingSubAssyRawMaterial.txt_Lot_no.Text = dtCheckStockSA.Rows(i).Item("lot_no")
                '                _PrintingSubAssyRawMaterial.txt_QR_Code.Text = dtCheckStockSA.Rows(i).Item("CODE_STOCK_PROD_SUB_ASSY") & Environment.NewLine
                '                _PrintingSubAssyRawMaterial.btn_Print_Click(sender, e)

                '                If globVar.failPrint = "No" Then
                '                    Dim sqlInsertPrintingRecord As String = "INSERT INTO record_printing (po, fg, line, remark,sub_sub_po,department,flow_ticket,code_print)
                '                        VALUES ('" & cbPONumber.Text & "','" & cbFGPN.Text & "','" & cbLineNumber.Text & "','Sub Assy (Again)','" & txtSubSubPODefective.Text & "','" & dept & "','" & dtCheckStockSA.Rows(i).Item("flow_ticket") & "','" & dtCheckStockSA.Rows(i).Item("CODE_STOCK_PROD_SUB_ASSY") & "')"
                '                    Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                '                    cmdInsertPrintingRecord.ExecuteNonQuery()
                '                End If
                '            Next
                '        End If
                '    End If
                'Else
                '    RJMessageBox.Show("This Sub Sub PO dont have Reject Data")
                'End If
            Else
                RJMessageBox.Show("Sorry, please input Sub Sub PO First.")
            End If
        End If
    End Sub

    Private Sub dgReject_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgReject.DataBindingComplete
        For i As Integer = 0 To dgReject.RowCount - 1
            If dgReject.Rows(i).Index Mod 2 = 0 Then
                dgReject.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgReject.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub dgWIP_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgWIP.DataBindingComplete
        For i As Integer = 0 To dgWIP.RowCount - 1
            If dgWIP.Rows(i).Index Mod 2 = 0 Then
                dgWIP.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgWIP.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub dgOnHold_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgOnHold.DataBindingComplete
        For i As Integer = 0 To dgOnHold.RowCount - 1
            If dgOnHold.Rows(i).Index Mod 2 = 0 Then
                dgOnHold.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgOnHold.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete
        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub DataGridView3_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView3.DataBindingComplete
        For i As Integer = 0 To DataGridView3.RowCount - 1
            If DataGridView3.Rows(i).Index Mod 2 = 0 Then
                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub dgBalance_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgBalance.DataBindingComplete
        For i As Integer = 0 To dgBalance.RowCount - 1
            If dgBalance.Rows(i).Index Mod 2 = 0 Then
                dgBalance.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgBalance.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i
    End Sub

    Private Sub txtWIPTicketNo_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtWIPTicketNo.PreviewKeyDown
        If (e.KeyData = Keys.Enter) Then
            txtWIPQuantity.Select()
            btnWIPAdd.Enabled = True
        End If
    End Sub

    Private Sub txtOnHoldTicketNo_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtOnHoldTicketNo.PreviewKeyDown
        If (e.KeyData = Keys.Enter) Then
            txtOnHoldQty.Select()
            btnOnHoldSave.Enabled = True
        End If
    End Sub

    Private Sub ckLossQty_CheckedChanged(sender As Object, e As EventArgs) Handles ckLossQty.CheckedChanged
        If ckLossQty.Checked = True Then
            txtLossQty.ReadOnly = False
        Else
            txtLossQty.ReadOnly = True
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 0 Then
            txtRejectBarcode.Select()
        ElseIf TabControl1.SelectedIndex = 3 Then
            If FGkah = True Then
                txtFGLabel.Select()
            Else
                txtSAFlowTicket.Select()
            End If
        ElseIf TabControl1.SelectedIndex = 4 Then
            txtBalanceBarcode.Select()
        End If
    End Sub

    Private Sub btnListPrintOnHold_Click(sender As Object, e As EventArgs) Handles btnListPrintOnHold.Click
        If txtSubSubPODefective.Text <> "" Then
            Dim _formListPrint As New ListPrint("Print On Hold", "ONHOLD", txtSubSubPODefective.Text, cbLineNumber.Text, txtDescDefective.Text, txtINV.Text)
            _formListPrint.ShowDialog()
        Else
            RJMessageBox.Show("Sorry, please input Sub Sub PO First.")
        End If
    End Sub

    Private Sub btnListPrintReturn_Click(sender As Object, e As EventArgs) Handles btnListPrintReturn.Click
        If txtSubSubPODefective.Text <> "" Then
            Dim _formListPrint As New ListPrint("Print Return Material", "Return", txtSubSubPODefective.Text, cbLineNumber.Text, txtDescDefective.Text, txtINV.Text)
            _formListPrint.ShowDialog()
        Else
            RJMessageBox.Show("Sorry, please input Sub Sub PO First.")
        End If
    End Sub

    Private Sub btnListPrintOthers_Click(sender As Object, e As EventArgs) Handles btnListPrintOthers.Click
        If txtSubSubPODefective.Text <> "" Then
            RJMessageBox.Show("On Progress")
            'Dim _formListPrint As New ListPrint("Print Others Part", "Others", txtSubSubPODefective.Text, cbLineNumber.Text, txtDescDefective.Text, txtINV.Text)
            '_formListPrint.ShowDialog()
        Else
            RJMessageBox.Show("Sorry, please input Sub Sub PO First.")
        End If
    End Sub

    Private Sub txtRejectQty_TextChanged(sender As Object, e As EventArgs) Handles txtRejectQty.TextChanged
        If txtRejectQty.Text.StartsWith("0") AndAlso txtRejectQty.Text.Length > 1 Then
            txtRejectQty.Text = txtRejectQty.Text.TrimStart("0"c)
            txtRejectQty.SelectionStart = txtRejectQty.Text.Length
        End If
    End Sub

    Private Sub txtRejectQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRejectQty.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtWIPQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtWIPQuantity.TextChanged
        If txtWIPQuantity.Text.StartsWith("0") AndAlso txtWIPQuantity.Text.Length > 1 Then
            txtWIPQuantity.Text = txtWIPQuantity.Text.TrimStart("0"c)
            txtWIPQuantity.SelectionStart = txtWIPQuantity.Text.Length
        End If
    End Sub

    Private Sub txtWIPQuantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWIPQuantity.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtOnHoldQty_TextChanged(sender As Object, e As EventArgs) Handles txtOnHoldQty.TextChanged
        If txtOnHoldQty.Text.StartsWith("0") AndAlso txtOnHoldQty.Text.Length > 1 Then
            txtOnHoldQty.Text = txtOnHoldQty.Text.TrimStart("0"c)
            txtOnHoldQty.SelectionStart = txtOnHoldQty.Text.Length
        End If
    End Sub

    Private Sub txtOnHoldQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOnHoldQty.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtBalanceQty_TextChanged(sender As Object, e As EventArgs) Handles txtBalanceQty.TextChanged
        If txtBalanceQty.Text.StartsWith("0") AndAlso txtBalanceQty.Text.Length > 1 Then
            txtBalanceQty.Text = txtBalanceQty.Text.TrimStart("0"c)
            txtBalanceQty.SelectionStart = txtBalanceQty.Text.Length
        End If
    End Sub

    Private Sub txtBalanceQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBalanceQty.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles txtRejectMaterialManual.TextChanged
        ComboBox1.Enabled = False
        CheckManualRejectMaterial.Enabled = False
    End Sub

    Private Sub TextBox7_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtRejectMaterialManual.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            tampilDataComboBoxDataManualMaterialCombobox1(txtRejectMaterialManual.Text)
            CheckManualRejectMaterial.Enabled = True
            ComboBox1.Enabled = True
        End If
    End Sub

    Private Sub TextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRejectMaterialManual.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Sub tampilDataComboBoxDataManualMaterialCombobox1(material As String)
        Call Database.koneksi_database()
        Dim dtMaterial As DataTable = Database.GetData("select lot_no, inv_ctrl_date, traceability, batch_no, qty, qrcode from stock_card where department='" & globVar.department & "' and material='" & material & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and status='Production Process' order by datetime_insert")

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

    Sub tampilDataComboBoxDataManualMaterialCombobox2(material As String)
        Call Database.koneksi_database()
        Dim dtMaterial As DataTable = Database.GetData("select lot_no, inv_ctrl_date, traceability, batch_no, qty, qrcode from stock_card where department='" & globVar.department & "' and material='" & material & "' and sub_sub_po='" & txtSubSubPODefective.Text & "' and status='Production Process' order by datetime_insert")

        dtMaterial.Columns.Add("DisplayMember", GetType(String))

        For Each row As DataRow In dtMaterial.Rows
            If row("qrcode").ToString() = "Manual Input" Then
                row("DisplayMember") = "Lot No:" & row("lot_no").ToString() & "|ICD:" & row("inv_ctrl_date").ToString() & "|Trace:" & row("traceability").ToString() & "|Batch:" & row("batch_no").ToString() & "|Qty:" & row("qty").ToString() & "|Manual"
            Else
                row("DisplayMember") = "Lot No:" & row("lot_no").ToString() & "|ICD:" & row("inv_ctrl_date").ToString() & "|Trace:" & row("traceability").ToString() & "|Batch:" & row("batch_no").ToString() & "|Qty:" & row("qty").ToString()
            End If
        Next

        ComboBox2.DataSource = dtMaterial
        ComboBox2.DisplayMember = "DisplayMember"
        ComboBox2.ValueMember = "DisplayMember"
        ComboBox2.SelectedIndex = -1
    End Sub

    Private Sub txtReturnMaterialManual_TextChanged(sender As Object, e As EventArgs) Handles txtReturnMaterialManual.TextChanged
        ComboBox2.Enabled = False
        CheckManualReturnMaterial.Enabled = False
    End Sub

    Private Sub txtReturnMaterialManual_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtReturnMaterialManual.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            tampilDataComboBoxDataManualMaterialCombobox2(txtReturnMaterialManual.Text)
            CheckManualReturnMaterial.Enabled = True
            ComboBox2.Enabled = True
        End If
    End Sub

    Private Sub txtReturnMaterialManual_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtReturnMaterialManual.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub dgReject_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgReject.CellClick

        Try

            If e.RowIndex = -1 Then
                Exit Sub
            End If

            If e.ColumnIndex = -1 Then
                Exit Sub
            End If

            If globVar.delete = 0 Then
                RJMessageBox.Show("Your Access cannot execute this action")
                Exit Sub
            End If

            If dgReject.Columns(e.ColumnIndex).Name = "delete" Then
                Dim queryUpdateactualQty As String = "update stock_card set actual_qty=actual_qty + " & dgReject.Rows(e.RowIndex).Cells("Reject Qty").Value & " where material='" & dgReject.Rows(e.RowIndex).Cells("Material").Value & "' and lot_no='" & dgReject.Rows(e.RowIndex).Cells("Lot No.").Value & "' and inv_ctrl_date='" & dgReject.Rows(e.RowIndex).Cells("Inv. Ctrl Date").Value & "' and traceability='" & dgReject.Rows(e.RowIndex).Cells("Traceability").Value & "' and batch_no='" & dgReject.Rows(e.RowIndex).Cells("Batch No.").Value & "' and status='Production Process'"
                Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                If dtUpdateactualQty.ExecuteNonQuery() Then
                    Dim sqlDelete As String = "delete from out_prod_reject where id=" & dgReject.Rows(e.RowIndex).Cells("#").Value
                    Dim cmdDelete = New SqlCommand(sqlDelete, Database.koneksi)
                    If cmdDelete.ExecuteNonQuery() Then
                        RJMessageBox.Show("Delete Reject Material Success.")

                        loaddgReject("")
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgBalance_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgBalance.CellClick

        Try

            If e.RowIndex = -1 Then
                Exit Sub
            End If

            If e.ColumnIndex = -1 Then
                Exit Sub
            End If

            If globVar.delete = 0 Then
                RJMessageBox.Show("Your Access cannot execute this action")
                Exit Sub
            End If

            If dgBalance.Columns(e.ColumnIndex).Name = "delete" Then
                Dim queryUpdateactualQty As String = "update stock_card set actual_qty=actual_qty + " & dgBalance.Rows(e.RowIndex).Cells("Return Qty").Value & " where material='" & dgBalance.Rows(e.RowIndex).Cells("Material").Value & "' and lot_no='" & dgBalance.Rows(e.RowIndex).Cells("Lot No.").Value & "' and inv_ctrl_date='" & dgBalance.Rows(e.RowIndex).Cells("Inv. Ctrl Date").Value & "' and traceability='" & dgBalance.Rows(e.RowIndex).Cells("Traceability").Value & "' and batch_no='" & dgBalance.Rows(e.RowIndex).Cells("Batch No.").Value & "' and status='Production Process'"
                Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                If dtUpdateactualQty.ExecuteNonQuery() Then
                    Dim sqlDelete As String = "delete from stock_card where id=" & dgBalance.Rows(e.RowIndex).Cells("#").Value
                    Dim cmdDelete = New SqlCommand(sqlDelete, Database.koneksi)
                    If cmdDelete.ExecuteNonQuery() Then
                        RJMessageBox.Show("Delete Return Material Success.")

                        loaddgBalance("")
                    End If
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgOnHold_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgOnHold.CellClick

        Try

            Dim successDelete As Integer

            If e.RowIndex = -1 Then
                Exit Sub
            End If

            If e.ColumnIndex = -1 Then
                Exit Sub
            End If

            If globVar.delete = 0 Then
                RJMessageBox.Show("Your Access cannot execute this action")
                Exit Sub
            End If

            If dgOnHold.Columns(e.ColumnIndex).Name = "delete" Then

                successDelete = 0

                Dim sqlSelectOnHold As String = "select * from stock_prod_onhold where code_stock_prod_onhold='" & dgOnHold.Rows(e.RowIndex).Cells("Code").Value & "'"
                Dim dtSelectOnHold As DataTable = Database.GetData(sqlSelectOnHold)
                For i = 0 To dtSelectOnHold.Rows.Count - 1

                    If IsDBNull(dtSelectOnHold.Rows(i).Item("qrcode")) = False Then

                        Dim queryUpdateactualQty As String = "update stock_card set actual_qty=actual_qty + " & dtSelectOnHold.Rows(i).Item("qty") & " where material='" & dtSelectOnHold.Rows(i).Item("part_number") & "' and lot_no='" & dtSelectOnHold.Rows(i).Item("lot_no") & "' and inv_ctrl_date='" & dtSelectOnHold.Rows(i).Item("inv_ctrl_date") & "' and traceability='" & dtSelectOnHold.Rows(i).Item("traceability") & "' and batch_no='" & dtSelectOnHold.Rows(i).Item("batch_no") & "' and status='Production Process' and qrcode_new='" & dtSelectOnHold.Rows(i).Item("qrcode") & "'"
                        Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                        If dtUpdateactualQty.ExecuteNonQuery() Then
                            Dim sqlDelete As String = "delete from stock_prod_onhold where id=" & dtSelectOnHold.Rows(i).Item("id")
                            Dim cmdDelete = New SqlCommand(sqlDelete, Database.koneksi)
                            If cmdDelete.ExecuteNonQuery() Then

                                successDelete = successDelete + 1

                            End If
                        End If

                    Else

                        Dim queryUpdateactualQty As String = "update stock_card set actual_qty=actual_qty + " & dtSelectOnHold.Rows(i).Item("qty") & " where material='" & dtSelectOnHold.Rows(i).Item("part_number") & "' and lot_no='" & dtSelectOnHold.Rows(i).Item("lot_no") & "' and inv_ctrl_date='" & dtSelectOnHold.Rows(i).Item("inv_ctrl_date") & "' and traceability='" & dtSelectOnHold.Rows(i).Item("traceability") & "' and batch_no='" & dtSelectOnHold.Rows(i).Item("batch_no") & "' and status='Production Process' and qrcode_new is null"
                        Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                        If dtUpdateactualQty.ExecuteNonQuery() Then
                            Dim sqlDelete As String = "delete from stock_prod_onhold where id=" & dtSelectOnHold.Rows(i).Item("id")
                            Dim cmdDelete = New SqlCommand(sqlDelete, Database.koneksi)
                            If cmdDelete.ExecuteNonQuery() Then

                                successDelete = successDelete + 1

                            End If
                        End If

                    End If

                Next

                If successDelete = dtSelectOnHold.Rows.Count Then

                    RJMessageBox.Show("Delete On Hold Success.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    LoaddgOnHold("")

                Else

                    RJMessageBox.Show("Error Delete On Hold", "", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    LoaddgOnHold("")

                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub TabControl1_Click(sender As Object, e As EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 Then
            LoaddgWIP("")
        ElseIf TabControl1.SelectedIndex = 2 Then
            LoaddgOnHold("")
        End If
    End Sub

    Private Sub dgWIP_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgWIP.CellClick

        Try

            Dim successDelete As Integer

            If e.RowIndex = -1 Then
                Exit Sub
            End If

            If e.ColumnIndex = -1 Then
                Exit Sub
            End If

            If globVar.delete = 0 Then
                RJMessageBox.Show("Your Access cannot execute this action")
                Exit Sub
            End If

            If dgWIP.Columns(e.ColumnIndex).Name = "delete" Then

                successDelete = 0

                Dim sqlSelectWIP As String = "select * from stock_prod_wip where code_stock_prod_wip='" & dgWIP.Rows(e.RowIndex).Cells("Code").Value & "'"
                Dim dtSelectWIP As DataTable = Database.GetData(sqlSelectWIP)
                For i = 0 To dtSelectWIP.Rows.Count - 1

                    If IsDBNull(dtSelectWIP.Rows(i).Item("qrcode")) = False Then

                        Dim queryUpdateactualQty As String = "update stock_card set actual_qty=actual_qty + " & dtSelectWIP.Rows(i).Item("qty") & " where material='" & dtSelectWIP.Rows(i).Item("part_number") & "' and lot_no='" & dtSelectWIP.Rows(i).Item("lot_no") & "' and inv_ctrl_date='" & dtSelectWIP.Rows(i).Item("inv_ctrl_date") & "' and traceability='" & dtSelectWIP.Rows(i).Item("traceability") & "' and batch_no='" & dtSelectWIP.Rows(i).Item("batch_no") & "' and status='Production Process' and qrcode_new='" & dtSelectWIP.Rows(i).Item("qrcode") & "'"
                        Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                        If dtUpdateactualQty.ExecuteNonQuery() Then
                            Dim sqlDelete As String = "delete from stock_prod_wip where id=" & dtSelectWIP.Rows(i).Item("id")
                            Dim cmdDelete = New SqlCommand(sqlDelete, Database.koneksi)
                            If cmdDelete.ExecuteNonQuery() Then

                                successDelete = successDelete + 1

                            End If
                        End If

                    Else

                        Dim queryUpdateactualQty As String = "update stock_card set actual_qty=actual_qty + " & dtSelectWIP.Rows(i).Item("qty") & " where material='" & dtSelectWIP.Rows(i).Item("part_number") & "' and lot_no='" & dtSelectWIP.Rows(i).Item("lot_no") & "' and inv_ctrl_date='" & dtSelectWIP.Rows(i).Item("inv_ctrl_date") & "' and traceability='" & dtSelectWIP.Rows(i).Item("traceability") & "' and batch_no='" & dtSelectWIP.Rows(i).Item("batch_no") & "' and status='Production Process' and qrcode_new is null"
                        Dim dtUpdateactualQty = New SqlCommand(queryUpdateactualQty, Database.koneksi)
                        If dtUpdateactualQty.ExecuteNonQuery() Then
                            Dim sqlDelete As String = "delete from stock_prod_wip where id=" & dtSelectWIP.Rows(i).Item("id")
                            Dim cmdDelete = New SqlCommand(sqlDelete, Database.koneksi)
                            If cmdDelete.ExecuteNonQuery() Then

                                successDelete = successDelete + 1

                            End If
                        End If

                    End If

                Next

                If successDelete = dtSelectWIP.Rows.Count Then

                    RJMessageBox.Show("Delete WIP Success.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    LoaddgWIP("")

                Else

                    RJMessageBox.Show("Error Delete WIP", "", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    LoaddgWIP("")

                End If

            End If

        Catch ex As Exception

        End Try

    End Sub
End Class