Imports System.Runtime.Remoting

Public Class Summary
    Private Sub Summary_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Sub loadDGV()
        With DGSummary
            .DefaultCellStyle.Font = New Font("Tahoma", 14)

            .ColumnCount = 17
            .Columns(0).HeaderText = "No"
            .Columns(0).Width = 50
            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).HeaderText = "Sub Sub PO"
            .Columns(2).HeaderText = "FG"
            .Columns(3).HeaderText = "Mat"
            .Columns(4).HeaderText = "Fresh"
            .Columns(5).HeaderText = "Others"
            .Columns(6).HeaderText = "WIP"
            .Columns(7).HeaderText = "On Hold"
            .Columns(8).HeaderText = "Sub Assy"
            .Columns(9).HeaderText = "Sum Input"
            .Columns(10).HeaderText = "Return"
            .Columns(11).HeaderText = "Others"
            .Columns(12).HeaderText = "WIP"
            .Columns(13).HeaderText = "On Hold"
            .Columns(14).HeaderText = "FG"
            .Columns(15).HeaderText = "Balance"
            .Columns(16).HeaderText = "Sum Output"

            For i As Integer = 0 To .ColumnCount - 1
                .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

            Dim sqlStr As String = "select mp.fg_pn, mufg.component from material_usage_finish_goods mufg, main_po mp, sub_sub_po ssp where mufg.fg_part_number=mp.fg_pn and mp.id=ssp.main_po and ssp.sub_sub_po='" & txtSummarySubSubPO.Text & "'"

            .EnableHeadersVisualStyles = False
            With .ColumnHeadersDefaultCellStyle
                .BackColor = Color.Navy
                .ForeColor = Color.White
                .Font = New Font("Tahoma", 13, FontStyle.Bold)
                .Alignment = HorizontalAlignment.Center
                .Alignment = ContentAlignment.MiddleCenter
            End With

            Dim dttable As DataTable = Database.GetData(sqlStr)

            If dttable.Rows.Count > 0 Then
                .Rows.Clear()

                For i = 0 To dttable.Rows.Count - 1
                    Dim dtInFresh As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='Fresh'")
                    Dim dtInOthers As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='OT'")
                    Dim dtInWIP As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='WIP'")
                    Dim dtInOnHold As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='OH'")
                    Dim dtInSA As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='SA'")

                    Dim dtOutReturn As DataTable = Database.GetData("select isnull(sum(qty),0) from out_prod_reject where sub_sub_po='" & txtSummarySubSubPO.Text & "' and part_number='" & dttable.Rows(i).Item("component") & "'")
                    Dim dtOutOthers As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_prod_others where code_out_prod_defect in (select DISTINCT(code_out_prod_defect) from out_prod_defect where sub_sub_po='" & txtSummarySubSubPO.Text & "') and part_number='" & dttable.Rows(i).Item("component") & "'")
                    Dim dtOutWIP As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_prod_wip where sub_sub_po='" & txtSummarySubSubPO.Text & "' and part_number='" & dttable.Rows(i).Item("component") & "'")
                    Dim dtOutOnHold As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_prod_onhold where sub_sub_po='" & txtSummarySubSubPO.Text & "' and part_number='" & dttable.Rows(i).Item("component") & "'")
                    Dim dtOutFG As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Result'")
                    Dim dtOutBalance As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Return To Mini Store'")

                    .Rows.Add(1)
                    .Item(0, i).Value = (i + 1).ToString()
                    .Item(1, i).Value = txtSummarySubSubPO.Text
                    .Item(2, i).Value = dttable.Rows(i).Item("fg_pn")
                    .Item(3, i).Value = dttable.Rows(i).Item("component")
                    .Item(4, i).Value = dtInFresh.Rows(0)(0)
                    .Item(5, i).Value = dtInOthers.Rows(0)(0)
                    .Item(6, i).Value = dtInWIP.Rows(0)(0)
                    .Item(7, i).Value = dtInOnHold.Rows(0)(0)
                    .Item(8, i).Value = dtInSA.Rows(0)(0)
                    .Item(9, i).Value = dtInFresh.Rows(0)(0) + dtInOthers.Rows(0)(0) + dtInWIP.Rows(0)(0) + dtInOnHold.Rows(0)(0) + dtInSA.Rows(0)(0)
                    .Item(9, i).Style.BackColor = Color.Green
                    .Item(10, i).Value = dtOutReturn.Rows(0)(0)
                    .Item(11, i).Value = dtOutOthers.Rows(0)(0)
                    .Item(12, i).Value = dtOutWIP.Rows(0)(0)
                    .Item(13, i).Value = dtOutOnHold.Rows(0)(0)
                    .Item(14, i).Value = dtOutFG.Rows(0)(0)
                    .Item(15, i).Value = dtOutBalance.Rows(0)(0)
                    .Item(16, i).Value = dtOutReturn.Rows(0)(0) + dtOutOthers.Rows(0)(0) + dtOutWIP.Rows(0)(0) + dtOutOnHold.Rows(0)(0) + dtOutFG.Rows(0)(0) + dtOutBalance.Rows(0)(0)
                    .Item(16, i).Style.BackColor = Color.Green
                Next
            Else
                .Rows.Clear()
            End If

            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Sub txtSummarySubSubPO_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtSummarySubSubPO.PreviewKeyDown
        If (e.KeyData = Keys.Enter) Then
            If txtSummarySubSubPO.Text <> "" Then
                loadDGV()
            Else
                MsgBox("Sorry please fill the sub sub po")
            End If
        End If
    End Sub
End Class