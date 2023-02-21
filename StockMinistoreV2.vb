Public Class StockMinistoreV2
    Private Sub StockMinistoreV2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'JOVANDataSet.STOCK_CARD' table. You can move, or remove it, as needed.
        Me.STOCK_CARDTableAdapter.Fill(Me.JOVANDataSet.STOCK_CARD)

    End Sub
End Class