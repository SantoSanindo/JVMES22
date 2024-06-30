Imports System.Text.RegularExpressions

Public Module QRCode

    'Public Sub Baca(Input_QRCode As String)
    '    globVar.QRCode_PN = ""
    '    globVar.QRCode_Inv = ""
    '    globVar.QRCode_Batch = ""
    '    globVar.QRCode_Country = ""
    '    globVar.QRCode_lot = ""
    '    globVar.QRCode_Qty = ""
    '    globVar.QRCode_Traceability = ""

    '    If Input_QRCode.Length > 100 Then
    '        RJMessageBox.Show("QRCode Not Valid")
    '        Exit Sub
    '    End If

    '    If IsValidQRCode(Input_QRCode) Then
    '        Dim part1P As String = RemoveLeadingZeros(ExtractPart(Input_QRCode, "1P(\d{10})", 1))
    '        Dim partQ1 As String = RemoveLeadingZeros(ExtractPart(Input_QRCode, "Q(\d{12})", 1))
    '        Dim partS As String = RemoveLeadingZeros(ExtractPart(Input_QRCode, "S(\d{14})", 1))
    '        Dim partQ2 As String = RemoveLeadingZeros(ExtractPart(Input_QRCode, "Q(\d{4})", 2))
    '        Dim partB As String = RemoveLeadingZeros(ExtractPart(Input_QRCode, "B(\w{7,12})", 1))
    '        Dim part12D As String = RemoveLeadingZeros(ExtractPart(Input_QRCode, "12D(\d{8})", 1))
    '        Dim part4L As String = RemoveLeadingZeros(ExtractPart(Input_QRCode, "4L\s*(\w+)$", 1))

    '        globVar.QRCode_PN = part1P
    '        globVar.QRCode_Qty = partQ1
    '        globVar.QRCode_Traceability = partS
    '        globVar.QRCode_lot = partQ2
    '        globVar.QRCode_Batch = partB
    '        globVar.QRCode_Inv = part12D
    '        globVar.QRCode_Country = part4L

    '        'globVar.QRCode_PN = Input_QRCode.Substring(6, 10).TrimStart("0"c)
    '        'globVar.QRCode_Qty = Input_QRCode.Substring(17, 12).TrimStart("0"c)
    '        'globVar.QRCode_Traceability = Input_QRCode.Substring(30, 12).TrimStart("0"c)
    '        'globVar.QRCode_lot = Input_QRCode.Substring(45, 4).TrimStart("0"c)
    '        'globVar.QRCode_Batch = Input_QRCode.Substring(50, 10).TrimStart("0"c)
    '        'globVar.QRCode_Inv = Input_QRCode.Substring(63, 8).TrimStart("0"c)
    '        'globVar.QRCode_Country = Input_QRCode.Substring(73)
    '    End If
    'End Sub

    Public Function Baca(Input_QRCode As String)
        globVar.QRCode_PN = ""
        globVar.QRCode_Inv = ""
        globVar.QRCode_Batch = ""
        globVar.QRCode_Country = ""
        globVar.QRCode_lot = ""
        globVar.QRCode_Qty = ""
        globVar.QRCode_Traceability = ""

        If Input_QRCode.Length > 100 Then
            Return False
        End If

        If IsValidQRCode(Input_QRCode) Then
            Dim part1P As String = RemoveLeadingZeros(ExtractPart(Input_QRCode, "1P(\d{10})", 1))
            Dim partQ1 As String = RemoveLeadingZeros(ExtractPart(Input_QRCode, "Q(\d{12})", 1))
            Dim partS As String = RemoveLeadingZeros(ExtractPart(Input_QRCode, "S(\d{14})", 1))
            Dim partQ2 As String = RemoveLeadingZeros(ExtractPart(Input_QRCode, "Q(\d{4})", 2))
            Dim partB As String = RemoveLeadingZeros(ExtractPart(Input_QRCode, "B(\w{7,12})", 1))
            Dim part12D As String = RemoveLeadingZeros(ExtractPart(Input_QRCode, "12D(\d{8})", 1))
            Dim part4L As String = RemoveLeadingZeros(ExtractPart(Input_QRCode, "4L\s*(\w+)$", 1))

            globVar.QRCode_PN = part1P
            globVar.QRCode_Qty = partQ1
            globVar.QRCode_Traceability = partS
            globVar.QRCode_lot = partQ2
            globVar.QRCode_Batch = partB
            globVar.QRCode_Inv = part12D
            globVar.QRCode_Country = part4L

            Return True

            'globVar.QRCode_PN = Input_QRCode.Substring(6, 10).TrimStart("0"c)
            'globVar.QRCode_Qty = Input_QRCode.Substring(17, 12).TrimStart("0"c)
            'globVar.QRCode_Traceability = Input_QRCode.Substring(30, 12).TrimStart("0"c)
            'globVar.QRCode_lot = Input_QRCode.Substring(45, 4).TrimStart("0"c)
            'globVar.QRCode_Batch = Input_QRCode.Substring(50, 10).TrimStart("0"c)
            'globVar.QRCode_Inv = Input_QRCode.Substring(63, 8).TrimStart("0"c)
            'globVar.QRCode_Country = Input_QRCode.Substring(73)
        Else
            Return False
        End If
    End Function

    Function IsValidQRCode(qrCode As String) As Boolean
        Try
            Dim pattern As New Regex("1P.*Q.*S.*Q.*B.*12D.*4L")
            Return pattern.IsMatch(qrCode)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function ExtractPart(input As String, pattern As String, occurrence As Integer) As String
        Dim match As MatchCollection = Regex.Matches(input, pattern)
        If match.Count >= occurrence Then
            Return match(occurrence - 1).Groups(1).Value.Trim()
        End If
        Return String.Empty
    End Function

    Function RemoveLeadingZeros(input As String) As String
        Return input.TrimStart("0"c)
    End Function
End Module
