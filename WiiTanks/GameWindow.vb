Public Class GameWindow

    Private _game As Game

    Private Sub GameWindow_Load(sender As Form, e As EventArgs) Handles MyBase.Load
        SetupWindow()
        _game = New Game(Me)
    End Sub

    Private Sub SetupWindow()
        Me.Size = New Size(1920, 1080)
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        Me.Text = "Wii Tonks"
        Me.BackColor = Color.Black
    End Sub

    Private Sub KeyDown_Event(sender As Form, e As KeyEventArgs) Handles Me.KeyDown
        _game.KeyDown_Event(e)
    End Sub

    Private Sub KeyUp_Event(sender As Form, e As KeyEventArgs) Handles Me.KeyUp
        _game.KeyUp_Event(e)
    End Sub

    Private Sub MouseMove_Event(sender As Form, e As MouseEventArgs) Handles Me.MouseMove
        _game.MouseMove_Event(e)
    End Sub
End Class
