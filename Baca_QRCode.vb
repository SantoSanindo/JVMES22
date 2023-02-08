Public Module QRCode

    Public Sub Baca(Input_QRCode As String)
        If Input_QRCode.Length >= 68 Then
            globVar.QRCode_modul_sub_assy_part_Number = Input_QRCode.Substring(6, 10)
            globVar.QRCode_Qty = Input_QRCode.Substring(17, 12)
            globVar.QRCode_inv_ctrl_Date = Input_QRCode.Substring(30, 12)
            globVar.QRCode_Traceability = Input_QRCode.Substring(45, 4)
            globVar.QRCode_Batch_No = Input_QRCode.Substring(50, 8)
            globVar.QRCode_last_char = Input_QRCode.Substring(74)
        End If
    End Sub

End Module
