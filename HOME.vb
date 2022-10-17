Public Class HOME
    Private Sub HOME_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnInputStock(sender As Object, e As EventArgs) Handles InputStockBtn.Click

        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(FormInputStock)
        TabControl1.TabPages(FormInputStock).Select()

    End Sub

    Private Sub btnMasterMaterial(sender As Object, e As EventArgs) Handles MasterMaterialBtn.Click

        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(MasterMaterial)
        TabControl1.TabPages(MasterMaterial).Select()

    End Sub

    Private Sub btnMasterProcess(sender As Object, e As EventArgs) Handles MasterProcessBtn.Click

        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(MasterProcess)
        TabControl1.TabPages(MasterProcess).Select()

    End Sub

    Private Sub btnMasterFinishGoods(sender As Object, e As EventArgs) Handles MasterFinishGoodsBtn.Click

        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(MasterFinishGoods)
        TabControl1.TabPages(MasterFinishGoods).Select()

    End Sub

    Private Sub btnMasterProcessFlow(sender As Object, e As EventArgs) Handles MasterProcessFlowBtn.Click

        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(MasterProcessFlow)
        TabControl1.TabPages(MasterProcessFlow).Select()

    End Sub

    Private Sub btnProcessFlowMaterialUsage(sender As Object, e As EventArgs) Handles ProcessFlowMaterialUsageBtn.Click

        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(ProcessFlowMaterialUsage)
        TabControl1.TabPages(ProcessFlowMaterialUsage).Select()

    End Sub
End Class