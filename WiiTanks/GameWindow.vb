Public Class GameWindow
    Private _game As Game


    Private Sub GameWindow_Load(sender As Form, e As EventArgs) Handles MyBase.Load
        SetupWindow()
        _game = New Game(Me)
    End Sub

    Private Sub SetupWindow()
        Me.Size = New Size(1600, 900)
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.CenterToScreen()

        Me.DoubleBuffered = True

        Me.Text = "Wii Tonks"
        'Me.BackColor = Color.Black
    End Sub
End Class
