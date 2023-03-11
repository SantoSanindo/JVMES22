Module Play_Sound
    Public Sub correct()
        Try
            'buat instance dari Windows Media Player
            Dim player As New WMPLib.WindowsMediaPlayer

            'Atur sumber file MP3 yang akan diputar
            player.URL = Application.StartupPath & "\MP3\Correct.mp3"

            player.controls.play()
            'Mulai memutar file MP3
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Wrong()
        Try
            'buat instance dari Windows Media Player
            Dim player As New WMPLib.WindowsMediaPlayer

            'Atur sumber file MP3 yang akan diputar
            player.URL = Application.StartupPath & "\MP3\Wrong.mp3"

            'Mulai memutar file MP3
            player.controls.play()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Double_scan()
        Try
            'buat instance dari Windows Media Player
            Dim player As New WMPLib.WindowsMediaPlayer

            'Atur sumber file MP3 yang akan diputar
            player.URL = Application.StartupPath & "\MP3\Double_Scan.mp3"

            'Mulai memutar file MP3
            player.controls.play()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub not_in_database()
        Try
            'buat instance dari Windows Media Player
            Dim player As New WMPLib.WindowsMediaPlayer

            'Atur sumber file MP3 yang akan diputar
            player.URL = Application.StartupPath & "\MP3\not_in_database.mp3"

            'Mulai memutar file MP3
            player.controls.play()
        Catch ex As Exception

        End Try
    End Sub
End Module
