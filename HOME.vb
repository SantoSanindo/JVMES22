Public Class HOME
    Private Sub HOME_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnInputStock(sender As Object, e As EventArgs) Handles InputStockBtn.Click

        TabControl1.TabPages.Add(FormInputStock)
        TabControl1.TabPages(FormInputStock).Select()

    End Sub

    Private Sub btnMasterMaterial(sender As Object, e As EventArgs) Handles MasterMaterialBtn.Click

        TabControl1.TabPages.Add(MasterMaterial)
        TabControl1.TabPages(MasterMaterial).Select()

    End Sub

    Private Sub btnMasterProcess(sender As Object, e As EventArgs) Handles MasterProcessBtn.Click

        TabControl1.TabPages.Add(MasterProcess)
        TabControl1.TabPages(MasterProcess).Select()

    End Sub
End Class