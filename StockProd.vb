Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports ZXing

Public Class StockProd
    Private Sub StockProd_Load(sender As Object, e As EventArgs) Handles Me.Load
        FreshMaterial()
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

    Sub FreshMaterial()
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Call Database.koneksi_database()
        Dim querystockProdFreshMaterial As String = "select ID, po [PO],sub_sub_po [SUB SUB PO],fg_pn [FINISH GOODS],part_number [MATERIAL],qty [QTY],actual_qty [ACTUAL QTY] from stock_prod_material order by actual_qty desc"
        Dim dtFreshMaterial As DataTable = Database.GetData(querystockProdFreshMaterial)
        DataGridView1.DataSource = dtFreshMaterial
    End Sub

    Private Sub RadioButton1_Click(sender As Object, e As EventArgs) Handles RadioButton1.Click
        FreshMaterial()
    End Sub
End Class