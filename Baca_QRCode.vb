Public Module QRCode

    Public Sub Baca(Input_QRCode As String)
        If Input_QRCode.Length = 99 Then
            globVar.QRCode_modul_sub_assy_part_Number = Input_QRCode.Substring(0, 16)
            globVar.QRCode_Qty = Input_QRCode.Substring(16, 13)
            globVar.QRCode_inv_ctrl_Dste = Input_QRCode.Substring(29, 15)
            globVar.QRCode_Traceability = Input_QRCode.Substring(44, 6)
            globVar.QRCode_Batch_No = Input_QRCode.Substring(60, 11)
            globVar.QRCode_last_char = Input_QRCode.Substring(71, 28)
        End If
    End Sub

End Module
