Imports System.Text.RegularExpressions

Public Class Dollar4

    Private Sub BtnD4_Click(sender As Object, e As EventArgs) Handles BtnD4.Click
        Try
            Dim _loc_1 As String = TxtD4.Text.Trim().Replace("[", "").Replace("]", "").Replace(",", "")
            Dim _loc_2 As String = _loc_1
            If _loc_1.Contains(" ") Then
                _loc_2 = ""
                For Each _loc_5 In _loc_1.Split(" ")
                    If _loc_5.Length < 5 Then
                        _loc_2 &= _loc_5.Trim()
                    End If
                Next
            End If

            Dim _loc_3 As String = ""
            For Each _loc_4 As Match In Regex.Matches(_loc_2, "[0-9a-fA-F]")
                _loc_3 &= _loc_4.Value
            Next

            MainUI.D4Password = MainUI.HexToBytes(_loc_3)
            MainUI.D4PasswordLast = MainUI.BytesToHex(MainUI.D4Password)
            Dispose()
        Catch ex As Exception
            Dispose()
        End Try
    End Sub

    Private Sub Dollar4_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            Dispose()
        Catch ex As Exception
            Dispose()
        End Try
    End Sub

    Private Sub Dollar4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            AcceptButton = BtnD4
            If MainUI.D4PasswordLast.Length > 0 Then TxtD4.Text = MainUI.D4PasswordLast
        Catch ex As Exception

        End Try
    End Sub

End Class