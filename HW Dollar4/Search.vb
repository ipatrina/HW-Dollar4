Public Class Search

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        Try
            MainUI.SearchText = TxtSearch.Text
            Dispose()
        Catch ex As Exception
            Dispose()
        End Try
    End Sub

    Private Sub Search_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            Dispose()
        Catch ex As Exception
            Dispose()
        End Try
    End Sub

    Private Sub Search_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            AcceptButton = BtnSearch
        Catch ex As Exception

        End Try
    End Sub

End Class