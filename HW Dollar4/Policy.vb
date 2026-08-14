Public Class Policy

    Private Sub BtnPolicy_Click(sender As Object, e As EventArgs) Handles BtnPolicy.Click
        Try
            MainUI.PolicyPassword = TxtPolicy.Text
            Dispose()
        Catch ex As Exception
            Dispose()
        End Try
    End Sub

    Private Sub Policy_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            Dispose()
        Catch ex As Exception
            Dispose()
        End Try
    End Sub

    Private Sub Policy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            AcceptButton = BtnPolicy
        Catch ex As Exception

        End Try
    End Sub

End Class