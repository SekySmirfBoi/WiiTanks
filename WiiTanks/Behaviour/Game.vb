Public Class Game

    Private _window As Form
    Sub New(window As Form)
        _window = window

        Dim testtank As New Bae()
        _window.Controls.Add(testtank)
        _window.BackColor = Color.Black
    End Sub
End Class
