Public Class HOME
    Private Sub HOME_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnInputStock(sender As Object, e As EventArgs) Handles InputStockBtn.Click

        TabControl1.TabPages.Add(FormInputStock)
        TabControl1.TabPages(FormInputStock).Select()

    End Sub

    Private Sub btnMasterMaterial(sender As Object, e As EventArgs) Handles MasterMaterialBtn.Click

    End Sub
End Class