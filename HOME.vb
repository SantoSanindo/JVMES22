Public Class HOME
    Private Sub HOME_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(FormLogin)
        TabControl1.TabPages(FormLogin).Select()
    End Sub

    'Open Menu Master
    Private Sub btnMasterMaterial(sender As Object, e As EventArgs) Handles MasterMaterialBtn.Click
        If globVar.hakAkses = "ADMIN" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MasterMaterial)
            TabControl1.TabPages(MasterMaterial).Select()
        Else
            MessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnMasterProcess(sender As Object, e As EventArgs) Handles MasterProcessBtn.Click
        If globVar.hakAkses = "ADMIN" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MasterProcess)
            TabControl1.TabPages(MasterProcess).Select()
        Else
            MessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnMasterProcessFlow(sender As Object, e As EventArgs) Handles MasterProcessFlowBtn.Click
        If globVar.hakAkses = "ADMIN" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MasterProcessFlow)
            TabControl1.TabPages(MasterProcessFlow).Select()
        Else
            MessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnProcessFlowMaterialUsage(sender As Object, e As EventArgs) Handles ProcessFlowMaterialUsageBtn.Click
        If globVar.hakAkses = "ADMIN" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(ProcessFlowMaterialUsage)
            TabControl1.TabPages(ProcessFlowMaterialUsage).Select()
        Else
            MessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnMaterialUsageFinishGoods(sender As Object, e As EventArgs) Handles MaterialUsageFinishGoodsBtn.Click
        If globVar.hakAkses = "ADMIN" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MaterialUsageFinishGoods)
            TabControl1.TabPages(MaterialUsageFinishGoods).Select()
        Else
            MessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnUsers(sender As Object, e As EventArgs) Handles UsersBtn.Click
        If globVar.hakAkses = "ADMIN" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(Users)
            TabControl1.TabPages(Users).Select()
        Else
            MessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnMasterFinishGoods(sender As Object, e As EventArgs) Handles MasterFinishGoodsBtn.Click
        If globVar.hakAkses = "ADMIN" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MasterFinishGoods)
            TabControl1.TabPages(MasterFinishGoods).Select()
        Else
            MessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnMasterLine(sender As Object, e As EventArgs) Handles MasterLineBtn.Click
        If globVar.hakAkses = "ADMIN" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MasterLine)
            TabControl1.TabPages(MasterLine).Select()
        Else
            MessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    'Close Menu Master

    'Open Menu Ministore
    Private Sub btnInputStock(sender As Object, e As EventArgs) Handles InputStockBtn.Click
        If globVar.hakAkses = "OPERATOR_MINISTORE" Or globVar.hakAkses = "ADMIN" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(FormInputStock)
            TabControl1.TabPages(FormInputStock).Select()
        Else
            MessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnReturnStock(sender As Object, e As EventArgs) Handles ReturnStockBtn.Click
        If globVar.hakAkses = "OPERATOR_MINISTORE" Or globVar.hakAkses = "ADMIN" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(FormReturnStock)
            TabControl1.TabPages(FormReturnStock).Select()
        Else
            MessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnProductionRequest(sender As Object, e As EventArgs) Handles ProductionRequestBtn.Click
        If globVar.hakAkses = "OPERATOR_MINISTORE" Or globVar.hakAkses = "ADMIN" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(ProductionRequest)
            TabControl1.TabPages(ProductionRequest).Select()
        Else
            MessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnStockMinistore(sender As Object, e As EventArgs) Handles StockMinistoreBtn.Click
        If globVar.hakAkses = "OPERATOR_MINISTORE" Or globVar.hakAkses = "ADMIN" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(StockMinistore)
            TabControl1.TabPages(StockMinistore).Select()
        Else
            MessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    'Close Menu Ministore

    'Open Menu Production

    Private Sub btnMainPoSubPO(sender As Object, e As EventArgs) Handles MainPOSubPOBtn.Click
        If globVar.hakAkses = "OPERATOR" Or globVar.hakAkses = "ADMIN" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(MainPOSubPO)
            TabControl1.TabPages(MainPOSubPO).Select()
        Else
            MessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnProduction(sender As Object, e As EventArgs) Handles ProductionBtn.Click
        If globVar.hakAkses = "OPERATOR" Or globVar.hakAkses = "ADMIN" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(Production)
            TabControl1.TabPages(Production).Select()
        Else
            MessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnStockProd(sender As Object, e As EventArgs) Handles StockProdBtn.Click
        If globVar.hakAkses = "OPERATOR" Or globVar.hakAkses = "ADMIN" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(StockProd)
            TabControl1.TabPages(StockProd).Select()
        Else
            MessageBox.Show("Cannot Access This Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    'Close Menu Production

    Private Sub Login_Btn(sender As Object, e As EventArgs) Handles RibbonTab3.PressedChanged
        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(FormLogin)
        TabControl1.TabPages(FormLogin).Select()
    End Sub

    '>>>>>>> Arif
End Class