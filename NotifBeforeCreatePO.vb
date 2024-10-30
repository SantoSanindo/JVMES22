Public Class NotifBeforeCreatePO
    Public Sub New(ByVal finishgoods As String)

        InitializeComponent()

        showdatagridview(finishgoods)

    End Sub

    Sub showdatagridview(ByVal finishgoods As String)
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Dim queryMasterFinishGoods As String = "select FG_PART_NUMBER [FG Part Number],DESCRIPTION [Desc],FAMILY [Family],COMPONENT [Component],USAGE [Usage] from MATERIAL_USAGE_FINISH_GOODS where fg_part_number='" & finishgoods & "' order by COMPONENT"
        Dim dtMasterFinishGoods As DataTable = Database.GetData(queryMasterFinishGoods)
        If dtMasterFinishGoods.Rows.Count > 0 Then
            DataGridView1.DataSource = dtMasterFinishGoods
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
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
End Class