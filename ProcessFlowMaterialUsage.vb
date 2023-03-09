Imports System.Data.SqlClient
'Imports ZXing

Public Class ProcessFlowMaterialUsage

    Dim lastSelectedLevel1 As Integer
    Dim lastSelectedLevel2 As Integer
    Private Sub ProcessFlowMaterialUsage_Load(sender As Object, e As EventArgs) Handles Me.Load
        treeView_show()
    End Sub

    Private Sub treeView_show()
        TreeView1.Nodes.Clear()
        Dim queryFinishGoodsMaterialUsage As String = "SELECT distinct master_finish_goods_pn FROM master_process_flow order by master_finish_goods_pn"
        Dim dtFinishGoodsMaterialUsage As DataTable = Database.GetData(queryFinishGoodsMaterialUsage)

        If dtFinishGoodsMaterialUsage.Rows.Count > 0 Then
            For i = 0 To dtFinishGoodsMaterialUsage.Rows.Count - 1
                TreeView1.Nodes.Add(dtFinishGoodsMaterialUsage.Rows(i).Item("MASTER_FINISH_GOODS_PN").ToString, "FG PN : " & dtFinishGoodsMaterialUsage.Rows(i).Item("MASTER_FINISH_GOODS_PN").ToString)

                Dim queryProcess As String = "SELECT id,master_process,master_process_number,material_usage FROM master_process_flow where master_finish_goods_pn='" & dtFinishGoodsMaterialUsage.Rows(i).Item("MASTER_FINISH_GOODS_PN").ToString & "' and master_process is not null order by id"
                Dim dtProcess As DataTable = Database.GetData(queryProcess)

                If dtProcess.Rows.Count > 0 Then
                    For j = 0 To dtProcess.Rows.Count - 1
                        Dim level3 = TreeView1.Nodes(i).Nodes.Add(dtProcess.Rows(j).Item("ID").ToString, dtProcess.Rows(j).Item("master_process_number").ToString & " - " & dtProcess.Rows(j).Item("master_process").ToString)

                        If dtProcess.Rows(j).Item("material_usage").ToString IsNot Nothing Then
                            Dim stringsplit() As String = dtProcess.Rows(j).Item("material_usage").ToString.Split(";")

                            For Each resultstring In stringsplit
                                If resultstring <> "" Then
                                    level3.Nodes.Add(dtProcess.Rows(j).Item("ID").ToString, "Component Usage - " & resultstring)
                                End If
                            Next

                        End If
                    Next
                End If
            Next
        End If
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        If (e.Node.Parent IsNot Nothing) Then
            CheckedListBox1.Items.Clear()
            Dim resultSplit() As String = e.Node.Parent.Text.ToString.Split(":")

            Dim position = InStr(e.Node.Parent.Text.ToString, ":")

            lastSelectedLevel2 = e.Node.Index
            lastSelectedLevel1 = e.Node.Parent.Index

            If position > 0 Then
                Dim queryMaterial As String = "SELECT * FROM material_usage_finish_goods where fg_part_number='" & resultSplit(1).Trim & "' order by component"
                Dim dtMaterial As DataTable = Database.GetData(queryMaterial)

                If dtMaterial.Rows.Count > 0 Then
                    For i = 0 To dtMaterial.Rows.Count - 1
                        CheckedListBox1.Items.Add(dtMaterial.Rows(i).Item("COMPONENT").ToString & " - " & dtMaterial.Rows(i).Item("DESCRIPTION").ToString)
                    Next
                End If

                For Each childNode As TreeNode In TreeView1.SelectedNode.Nodes
                    For j = 0 To CheckedListBox1.Items.Count - 1
                        Dim resultCompSplit() As String = CheckedListBox1.Items(j).ToString.Split("-")
                        Dim resultchildSplit() As String = childNode.Text.ToString.Split("-")

                        If resultchildSplit(1).Trim = resultCompSplit(0).Trim Then
                            CheckedListBox1.SetItemChecked(j, True)
                        End If
                    Next
                Next
            End If
        End If
    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedIndexChanged
        If CheckedListBox1.Items.Count > 0 Then
            Dim cheq As New System.Text.StringBuilder
            Dim gas As Integer = 0

            For i = 0 To CheckedListBox1.Items.Count - 1
                If (CheckedListBox1.GetItemChecked(i)) = True Then
                    Dim resultCompSplit() As String = CheckedListBox1.Items(i).ToString.Split("-")
                    cheq.Append(resultCompSplit(0).ToString.Trim)
                    cheq.Append(";")
                Else
                    gas = gas + 1
                End If
            Next

            If gas = CheckedListBox1.Items.Count Then
                Dim Sql As String = "update master_process_flow set material_usage=null where id='" & TreeView1.SelectedNode.Name & "'"
                Dim cmd = New SqlCommand(Sql, Database.koneksi)
                If cmd.ExecuteNonQuery() Then
                    refreshAll()
                End If
            Else
                If cheq.ToString <> "" Then
                    If TreeView1.SelectedNode.Name Is Nothing Then
                        RJMessageBox.Show("Please Select process First")
                        Exit Sub
                    End If

                    If TreeView1.SelectedNode.Name = "" Then
                        RJMessageBox.Show("Please Select process First")
                        Exit Sub
                    End If

                    If IsNumeric(TreeView1.SelectedNode.Name) Then
                        Dim Sql As String = "update master_process_flow set material_usage='" & cheq.ToString.Trim & "' where id='" & TreeView1.SelectedNode.Name & "'"
                        Dim cmd = New SqlCommand(Sql, Database.koneksi)
                        If cmd.ExecuteNonQuery() Then
                            refreshAll()
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Sub refreshAll()
        CheckedListBox1.Items.Clear()
        treeView_show()
        TreeView1.SelectedNode = TreeView1.Nodes(lastSelectedLevel1).Nodes(lastSelectedLevel2)
        TreeView1.Nodes(lastSelectedLevel1).Nodes(lastSelectedLevel2).Expand()
        TreeView1.Select()
    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            Dim FoundTreeview As Boolean = False
            For Each nd As TreeNode In TreeView1.Nodes
                If String.Compare(nd.Name, TextBox1.Text, True) = 0 Then
                    TreeView1.SelectedNode = nd
                    TreeView1.Select()
                    FoundTreeview = True
                    Exit For
                End If
            Next
            If FoundTreeview = False Then
                RJMessageBox.Show("Data not Found")
            End If
        End If
    End Sub
End Class