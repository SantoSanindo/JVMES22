Imports System.Runtime.Remoting

Public Class Summary
    Private Sub Summary_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Sub loadDGV()
        With DGSummary
            .DefaultCellStyle.Font = New Font("Tahoma", 14)

            .ColumnCount = 15
            .Columns(0).HeaderText = "No"
            .Columns(0).Width = 50
            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).HeaderText = "Sub Sub PO"
            .Columns(2).HeaderText = "FG"
            .Columns(3).HeaderText = "Mat"
            .Columns(4).HeaderText = "In Fresh"
            .Columns(5).HeaderText = "In Others"
            .Columns(6).HeaderText = "In WIP"
            .Columns(7).HeaderText = "In On Hold"
            .Columns(8).HeaderText = "In Sub Assy"
            .Columns(9).HeaderText = "Out Return"
            .Columns(10).HeaderText = "Out Others"
            .Columns(11).HeaderText = "Out WIP"
            .Columns(12).HeaderText = "Out On Hold"
            .Columns(13).HeaderText = "Out FG"
            .Columns(14).HeaderText = "Out Balance"

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
                For i = 0 To dttable.Rows.Count - 1
                    Dim dtInFresh As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='Fresh'")
                    Dim dtInOthers As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='OT'")
                    Dim dtInWIP As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='WIP'")
                    Dim dtInOnHold As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='OH'")
                    Dim dtInSA As DataTable = Database.GetData("select isnull(sum(qty),0) from stock_card where sub_sub_po='" & txtSummarySubSubPO.Text & "' and material='" & dttable.Rows(i).Item("component") & "' and status='Production Request' and [level]='SA'")

                    Dim dtOutReturn As DataTable = Database.GetData("select isnull(sum(qty),0) from out_prod_reject where sub_sub_po='" & txtSummarySubSubPO.Text & "' and part_number='" & dttable.Rows(i).Item("component") & "'")
                    Dim dtOutOthers As DataTable = Database.GetData("select isnull(sum(q.qty),0) from (select DISTINCT(d.code_out_prod_defect),ot.qty from stock_prod_others ot, out_prod_defect d where d.sub_sub_po='" & txtSummarySubSubPO.Text & "' and d.part_number='" & dttable.Rows(i).Item("component") & "' and d.code_out_prod_defect=ot.code_out_prod_defect) q")
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
                    .Item(9, i).Value = dtOutReturn.Rows(0)(0)
                    .Item(10, i).Value = dtOutOthers.Rows(0)(0)
                    .Item(11, i).Value = dtOutWIP.Rows(0)(0)
                    .Item(12, i).Value = dtOutOnHold.Rows(0)(0)
                    .Item(13, i).Value = dtOutFG.Rows(0)(0)
                    .Item(14, i).Value = dtOutBalance.Rows(0)(0)
                Next
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