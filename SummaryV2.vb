Public Class SummaryV2
    Public Shared menu As String = "Summary"

    Private Sub SummaryV2_Load(sender As Object, e As EventArgs) Handles Me.Load
        tampilDataComboBoxLine()
        ComboBox1.SelectedIndex = -1
    End Sub

    Sub tampilDataComboBoxLine()
        Call Database.koneksi_database()
        Dim dtMasterLine As DataTable = Database.GetData("select line from SUB_SUB_PO where status='Open' order by line")

        ComboBox1.DataSource = dtMasterLine
        ComboBox1.DisplayMember = "line"
        ComboBox1.ValueMember = "line"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class