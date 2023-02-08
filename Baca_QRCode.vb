Public Module QRCode

    Public Sub Baca(Input_QRCode As String)
        If Input_QRCode.Length >= 68 Then
            globVar.QRCode_PN = Input_QRCode.Substring(6, 10)
            If globVar.QRCode_PN.StartsWith("0") Then
                globVar.QRCode_PN = globVar.QRCode_PN.Remove(0, 1)
            End If
            globVar.QRCode_Qty = Input_QRCode.Substring(17, 12)
            globVar.QRCode_Traceability = Input_QRCode.Substring(30, 12)
            globVar.QRCode_lot = Input_QRCode.Substring(45, 4)
            globVar.QRCode_Batch = Input_QRCode.Substring(50, 10)
            globVar.QRCode_Inv = Input_QRCode.Substring(63, 8)
            globVar.QRCode_Country = Input_QRCode.Substring(73)
        End If
    End Sub
End Module
