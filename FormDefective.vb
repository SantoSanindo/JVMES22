Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Reflection.Emit
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

    Private Sub cbWIPProcess_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbWIPProcess.SelectedIndexChanged
        LoaddgWIP(cbWIPProcess.Text)
    End Sub

    Sub LoaddgWIP(proses As String)
        Dim i As Integer

        Try
            'Call Database.koneksi_database()
            'Dim dtMaterialUsage As DataTable = Database.GetData("select distinct MATERIAL_USAGE from _OLD_MASTER_PROCESS_FLOW where MASTER_PROCESS='" & strKey & "' AND MASTER_FINISH_GOODS_PN='" & cbFGPN.Text & "'")
            ''Dim dtMaterialInfo As DataTable = Database.GetData("select distinct MATERIAL_USAGE from MASTER_PROCESS_FLOW where MASTER_PROCESS='" & strKey & "'")

            'Dim matUsage As String = dtMaterialUsage.Rows(i)(0).ToString()
            'Dim matList() As String = matUsage.Split(";")

            With dgWIP
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

    Private Sub btnWIPAdd_Click(sender As Object, e As EventArgs) Handles btnWIPAdd.Click
        If cbWIPProcess.Text <> "" And txtWIPTicketNo.Text <> "" And txtWIPQuantity.Text <> "" Then

            Try
                Dim Part As String() = Nothing
                Dim i As Integer

                Part = materialList(cbWIPProcess.SelectedIndex).Split(";")

                'diulang sebanyak part number yg ada
                Call Database.koneksi_database()
                For i = 0 To Part.Length - 3
                    Dim sql As String = "INSERT INTO STOCK_PROD_WIP(CODE_STOCK_PROD_WIP,SUB_SUB_PO,FG_PN,PART_NUMBER,LOT_NO,TRACEABILITY,INV_CTRL_DATE,BATCH_NO,PROCESS,QTY,DATETIME_INSERT,PO) VALUES ('" &
                                        WIPGenerateCode() & "','" & WIPGetSubsubPO() & "','" & cbFGPN.Text & "','" & Part(i) & "','" & "" & "','" & "" & "','" & "" & "','" & "" & "','" & cbWIPProcess.Text & "','" & WIPGetQtyperPart(Part(i)) & "','" & "" & "','" & cbPONumber.Text & "')"
                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    If cmd.ExecuteNonQuery() Then
                        MessageBox.Show("Success save data!!!")
                    End If
                Next

            Catch ex As Exception
                MessageBox.Show("Error Insert : " & ex.Message)
            End Try
            Call Database.close_koneksi()
        End If

    End Sub

    ''''''''''''''''''''''''''''''''''''''' WIP FUNCTION
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
        Database.close_koneksi()

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
        Database.close_koneksi()


        Return SubsubPO
    End Function

    Function WIPGetQtyperPart(strComponent As String) As Double
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
        Double.TryParse(txtWIPQuantity.Text, dt)

        qty = qty * dt

        Return qty
    End Function
    '''''''''''''''''''''''''''''''''''''' END WIP FUNCTION
    Private Sub btnWIPEdit_Click(sender As Object, e As EventArgs) Handles btnWIPEdit.Click
        'MessageBox.Show((WIPGetQtyperPart("1717213000") * 10).ToString())
    End Sub
End Class