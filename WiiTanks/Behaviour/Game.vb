Public Class Game

    Private _window As Form
    Private WithEvents _timer As Timer
    Private _tickRate As Integer = 60

    Private _LastKnownMouseCoords As Point

    Private _teswt = 0
    Private _testTank As Bae
    Sub New(window As Form)
        _window = window
        _LastKnownMouseCoords = New Point(0, 0)

        _timer = New Timer With {.Interval = 1000 / _tickRate}
        _timer.Start()


        Dim testtank As New Bae
        _testTank = testtank
        _window.Controls.Add(_testTank)
    End Sub

    Private Sub tick(sender As Timer, e As EventArgs) Handles _timer.Tick
        _testTank.tick(_LastKnownMouseCoords)
    End Sub

    Public Sub KeyDown_Event(e As KeyEventArgs)
        If e.KeyCode = Keys.W Then
            _teswt = 0
        ElseIf e.KeyCode = Keys.S Then
            _teswt = 180
        ElseIf e.KeyCode = Keys.A Then
            _teswt = 270
        ElseIf e.KeyCode = Keys.D Then
            _teswt = 90
        End If
    End Sub

    Public Sub KeyUp_Event(e As KeyEventArgs)
    End Sub

    Public Sub MouseMove_Event(e As MouseEventArgs)
        _LastKnownMouseCoords = New Point(e.X, e.Y)
    End Sub
End Class
