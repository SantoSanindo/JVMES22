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

    Private Sub btnMainPoSubPO(sender As Object, e As EventArgs) Handles MainPOSubPOBtn.Click

        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(MainPOSubPO)
        TabControl1.TabPages(MainPOSubPO).Select()

    End Sub

    Private Sub btnProductionRequest(sender As Object, e As EventArgs) Handles ProductionRequestBtn.Click

        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(ProductionRequest)
        TabControl1.TabPages(ProductionRequest).Select()

    End Sub

    Private Sub btnMaterialUsageFinishGoods(sender As Object, e As EventArgs) Handles MaterialUsageFinishGoodsBtn.Click

        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(MaterialUsageFinishGoods)
        TabControl1.TabPages(MaterialUsageFinishGoods).Select()

    End Sub

    Private Sub btnMasterFinishGoods(sender As Object, e As EventArgs) Handles MasterFinishGoodsBtn.Click

        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(MasterFinishGoods)
        TabControl1.TabPages(MasterFinishGoods).Select()

    End Sub

    Private Sub BtnStockMinistore(sender As Object, e As EventArgs) Handles StockMinistoreBtn.Click
        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(StockMinistore)
        TabControl1.TabPages(StockMinistore).Select()
    End Sub

    Private Sub BtnProduction(sender As Object, e As EventArgs) Handles ProductionBtn.Click
        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(Production)
        TabControl1.TabPages(Production).Select()
    End Sub

    Private Sub BtnStockProd(sender As Object, e As EventArgs) Handles StockProdBtn.Click
        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(StockProd)
        TabControl1.TabPages(StockProd).Select()
    End Sub

    Private Sub BtnUsers(sender As Object, e As EventArgs) Handles UsersBtn.Click
        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(Users)
        TabControl1.TabPages(Users).Select()
    End Sub
End Class