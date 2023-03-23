Public Module QRCode

    Public Sub Baca(Input_QRCode As String)
        If Input_QRCode.Length >= 68 Then
            globVar.QRCode_PN = Input_QRCode.Substring(6, 10).TrimStart("0"c)
            globVar.QRCode_Qty = Input_QRCode.Substring(17, 12).TrimStart("0"c)
            globVar.QRCode_Traceability = Input_QRCode.Substring(30, 12).TrimStart("0"c)
            globVar.QRCode_lot = Input_QRCode.Substring(45, 4).TrimStart("0"c)
            globVar.QRCode_Batch = Input_QRCode.Substring(50, 10).TrimStart("0"c)
            globVar.QRCode_Inv = Input_QRCode.Substring(63, 8).TrimStart("0"c)
            globVar.QRCode_Country = Input_QRCode.Substring(73)
        End If
    End Sub
End Module
