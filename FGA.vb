Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class FGA
    Public Shared menu As String = "FGA"

    Dim sub_sub_po As String
    Dim pn_fg As String
    Dim qty_po As String
    Dim qtyperlot As String
    Dim line As String
    Dim noflowticket As String

    Private Sub FGA_Load(sender As Object, e As EventArgs) Handles Me.Load
        If globVar.view > 0 Then

            tampilDataComboBoxInspector()
            tampilDataComboBoxPacker1()
            tampilDataComboBoxPacker2()
            tampilDataComboBoxPacker3()
            tampilDataComboBoxPacker4()

            ComboBox1.Enabled = True
            ComboBox2.Enabled = False
            ComboBox3.Enabled = False
            ComboBox4.Enabled = False
            ComboBox5.Enabled = False
            Button1.Enabled = False

            txtSAP.Enabled = False
            textFT.Enabled = False


        End If
    End Sub

    Sub tampilDataComboBoxInspector()

        Call Database.koneksi_database()
        Dim dtMasterUsers As DataTable = Database.GetData("select * from users where user_fga=1 and department='" & globVar.department & "'")

        ComboBox1.DataSource = dtMasterUsers
        ComboBox1.DisplayMember = "NAME"
        ComboBox1.ValueMember = "NAME"
        ComboBox1.SelectedIndex = -1

    End Sub

    Sub tampilDataComboBoxPacker1()
        Call Database.koneksi_database()
        Dim dtMasterUsers As DataTable = Database.GetData("select * from users where user_fga=1 and department='" & globVar.department & "'")

        ComboBox2.DataSource = dtMasterUsers
        ComboBox2.DisplayMember = "NAME"
        ComboBox2.ValueMember = "NAME"
        ComboBox2.SelectedIndex = -1
    End Sub

    Sub tampilDataComboBoxPacker2()
        Call Database.koneksi_database()
        Dim dtMasterUsers As DataTable = Database.GetData("select * from users where user_fga=1 and department='" & globVar.department & "'")

        ComboBox3.DataSource = dtMasterUsers
        ComboBox3.DisplayMember = "name"
        ComboBox3.ValueMember = "name"
        ComboBox3.SelectedIndex = -1
    End Sub

    Sub tampilDataComboBoxPacker3()
        Call Database.koneksi_database()
        Dim dtMasterUsers As DataTable = Database.GetData("select * from users where user_fga=1 and department='" & globVar.department & "'")

        ComboBox4.DataSource = dtMasterUsers
        ComboBox4.DisplayMember = "NAME"
        ComboBox4.ValueMember = "NAME"
        ComboBox4.SelectedIndex = -1
    End Sub

    Sub tampilDataComboBoxPacker4()
        Call Database.koneksi_database()
        Dim dtMasterUsers As DataTable = Database.GetData("select * from users where user_fga=1 and department='" & globVar.department & "'")

        ComboBox5.DataSource = dtMasterUsers
        ComboBox5.DisplayMember = "NAME"
        ComboBox5.ValueMember = "NAME"
        ComboBox5.SelectedIndex = -1
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim queryCheck As String = "select * from fga where FLOW_TICKET='" & textFT.Text & "'"
        Dim dtCheck As DataTable = Database.GetData(queryCheck)

        If dtCheck.Rows.Count > 0 Then
            RJMessageBox.Show("Double Scan", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            textFT.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            textFT.Select()

            ComboBox1.Enabled = False
            ComboBox2.Enabled = False
            ComboBox3.Enabled = False
            ComboBox4.Enabled = False
            ComboBox5.Enabled = False
            Button1.Enabled = False

            ComboBox1.SelectedIndex = -1
            ComboBox2.SelectedIndex = -1
            ComboBox3.SelectedIndex = -1
            ComboBox4.SelectedIndex = -1
            ComboBox5.SelectedIndex = -1

            Exit Sub
        End If

        If ComboBox1.Text <> "" And ComboBox2.Text <> "" And textFT.Text <> "" Then
            If globVar.add > 0 Then
                Try
                    Dim sqlInsertPrintingRecord As String = "INSERT INTO fga (flow_ticket, inspector, packer1, packer2, packer3, packer4,sub_sub_po,fg_pn,line,no_flowticket,department)
                                    VALUES ('" & textFT.Text & "','" & ComboBox1.Text & "','" & ComboBox2.Text & "','" & ComboBox3.Text & "','" & ComboBox4.Text & "','" & ComboBox5.Text & "','" & sub_sub_po & "','" & pn_fg & "','" & line & "','" & noflowticket & "','" & globVar.department & "')"
                    Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                    If cmdInsertPrintingRecord.ExecuteNonQuery() Then
                        Dim SqlUpdate As String = "UPDATE summary_traceability SET inspector='" & ComboBox1.Text & "',packer1='" & ComboBox2.Text & "',packer2='" & ComboBox3.Text & "',packer3='" & ComboBox4.Text & "',packer4='" & ComboBox5.Text & "' WHERE sub_sub_po='" & sub_sub_po & "' and lot_no='" & aFlowTicket.Text & "'"
                        Dim cmdUpdate = New SqlCommand(SqlUpdate, Database.koneksi)
                        cmdUpdate.ExecuteNonQuery()

                        'ResetAfterSave()
                    End If
                Catch ex As Exception
                    RJMessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else
                RJMessageBox.Show("Your Access cannot execute this action")
            End If
        End If
    End Sub

    Sub ResetAfterSave()
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        ComboBox3.SelectedIndex = -1
        ComboBox4.SelectedIndex = -1
        ComboBox5.SelectedIndex = -1
        aFlowTicket.Clear()
        aQty.Clear()
        aPO.Clear()
        bFlowTicket.Clear()
        bQty.Clear()
        bPO.Clear()
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        textFT.Clear()
        textFT.Enabled = False
        TextBox2.Clear()
        TextBox3.Clear()
        txtSAP.Clear()
        txtSAP.Enabled = False
        ComboBox2.Enabled = False
        ComboBox3.Enabled = False
        ComboBox4.Enabled = False
        ComboBox5.Enabled = False
    End Sub

    Sub DGV_FGA()
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Dim queryMasterFinishGoods As String = "select id [#],SUB_SUB_PO [Sub Sub PO],FG_PN [Finish Goods], NO_FLOWTICKET [Flow Ticket],INSPECTOR [Inspector],PACKER1 [Packer 1],PACKER2 [Packer 2],PACKER3 [Packer 3],PACKER4 [Packer 4] from FGA where sub_sub_po='" & sub_sub_po & "' and department='" & globVar.department & "'"
        Dim dtMaterialNeed As DataTable = Database.GetData(queryMasterFinishGoods)
        If dtMaterialNeed.Rows.Count > 0 Then
            DataGridView1.DataSource = dtMaterialNeed
        End If
    End Sub

    Private Sub textFT_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles textFT.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) And textFT.Text <> "" Then
            If textFT.Text <> "" Then
                If InStr(textFT.Text, ";") = 0 Then
                    RJMessageBox.Show("QRCode Flow Ticket not valid")
                    textFT.Clear()
                    Exit Sub
                End If

                Dim splitFlowTicket = textFT.Text.Split(New String() {";"}, StringSplitOptions.None)

                aFlowTicket.Text = splitFlowTicket(5).Split(" of ")(0)
                aQty.Text = splitFlowTicket(3)
                aPO.Text = splitFlowTicket(0).Split("-")(0)
                sub_sub_po = splitFlowTicket(0)

                Dim queryMasterFinishGoods As String = "select * from master_finish_goods where fg_part_number='" & splitFlowTicket(1) & "'"
                Dim dtMasterFinishGoods As DataTable = Database.GetData(queryMasterFinishGoods)

                If dtMasterFinishGoods.Rows.Count > 0 Then
                    TextBox2.Text = dtMasterFinishGoods.Rows(0).Item("fg_part_number").ToString
                    TextBox3.Text = dtMasterFinishGoods.Rows(0).Item("description").ToString
                End If

                If bFlowTicket.Text = "" And bQty.Text = "" And bPO.Text = "" Then

                    RJMessageBox.Show("Please scan Label SAP First")
                    txtSAP.Clear()
                    txtSAP.Select()
                    Exit Sub

                End If

                If aFlowTicket.Text = "" And aQty.Text = "" And aPO.Text = "" Then

                    RJMessageBox.Show("Please scan Label Flow Ticket First")
                    textFT.Clear()
                    textFT.Select()
                    Exit Sub

                End If

                If bFlowTicket.Text <> aFlowTicket.Text And bQty.Text <> aQty.Text And bPO.Text <> aPO.Text Then

                    textFT.Clear()
                    textFT.Select()
                    RJMessageBox.Show("Label SAP & Label Flow Ticket is not same")
                    Exit Sub

                End If

                Try

                    Dim queryDOC As String = "SELECT
                                         *
                                        FROM
                                         fga 
                                        WHERE
                                         sub_sub_po = '" & sub_sub_po & "' 
                                         AND department = '" & globVar.department & "'
                                            and flow_ticket = '" & textFT.Text & "'"
                    Dim dtDOC As DataTable = Database.GetData(queryDOC)

                    If dtDOC.Rows.Count = 0 Then

                        Dim sqlInsertPrintingRecord As String = "INSERT INTO fga (flow_ticket, sub_sub_po,fg_pn,line,no_flowticket,department,inspector,packer1,packer2,packer3,packer4)
                                    VALUES ('" & textFT.Text & "','" & sub_sub_po & "','" & splitFlowTicket(1) & "','" & line & "','" & splitFlowTicket(5) & "','" & globVar.department & "','" & ComboBox1.Text & "','" & ComboBox2.Text & "','" & ComboBox3.Text & "','" & ComboBox4.Text & "','" & ComboBox5.Text & "')"
                        Dim cmdInsertPrintingRecord = New SqlCommand(sqlInsertPrintingRecord, Database.koneksi)
                        If cmdInsertPrintingRecord.ExecuteNonQuery() Then

                            Play_Sound.correct()
                            txtSAP.Clear()
                            textFT.Clear()

                            aFlowTicket.Clear()
                            aPO.Clear()
                            aQty.Clear()

                            bFlowTicket.Clear()
                            bPO.Clear()
                            bQty.Clear()

                            TextBox2.Clear()
                            TextBox3.Clear()

                            txtSAP.Enabled = True
                            textFT.Enabled = False

                            txtSAP.Select()

                        End If


                    Else

                        RJMessageBox.Show("This Flow Ticket already set for Inspector and Packer.")

                        txtSAP.Clear()
                        textFT.Clear()

                        aFlowTicket.Clear()
                        aPO.Clear()
                        aQty.Clear()

                        bFlowTicket.Clear()
                        bPO.Clear()
                        bQty.Clear()

                        TextBox2.Clear()
                        TextBox3.Clear()

                        txtSAP.Enabled = True
                        textFT.Enabled = False

                        txtSAP.Select()

                    End If

                Catch ex As Exception

                    RJMessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                End Try

                DGV_FGA()

            End If
        End If
    End Sub

    Private Sub txtSAP_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtSAP.PreviewKeyDown
        If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) And txtSAP.Text <> "" Then
            If txtSAP.Text <> "" And txtSAP.Text.Length > 64 Then

                QRCode.Baca(txtSAP.Text)

                bFlowTicket.Text = globVar.QRCode_lot
                bQty.Text = globVar.QRCode_Qty
                bPO.Text = globVar.QRCode_Traceability

                If bFlowTicket.Text = "" And bQty.Text = "" And bPO.Text = "" Then

                    RJMessageBox.Show("Please scan Label SAP First")
                    txtSAP.Clear()
                    txtSAP.Select()
                    Exit Sub

                End If

                textFT.Enabled = True
                textFT.Select()
                txtSAP.Enabled = False

            End If
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
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ResetAfterSave()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ComboBox2.Enabled = True
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        txtSAP.Enabled = True
        txtSAP.Select()
        ComboBox3.Enabled = True
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        ComboBox4.Enabled = True
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        ComboBox5.Enabled = True
    End Sub
End Class