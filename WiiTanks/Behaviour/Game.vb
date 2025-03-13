Public Class Game

    Private WithEvents _window As GameWindow
    Private WithEvents _timer As Timer

    Private _inputPlayer As New Dictionary(Of Keys, Player)

    Private _playerTanksCount As Integer = 0
    Private _playerTanks() As Player
    Private _wallCount As Integer = 0
    Private _walls() As BasicWall

    Sub New(window As GameWindow)
        SharedResources.window = window
        'SharedResources.stateManager = New StateManager(New LevelDesignerState)
        'SharedResources.stateManager = New StateManager(New MenuState)
        SharedResources.stateManager = New StateManager(New GameState(My.Resources.Level1))
        setupVariables(window)
    End Sub




    Private Sub setupVariables(window As GameWindow)
        _window = window

        ReDim Preserve SharedResources.inputKeys(0)
        SharedResources.LastKnownMouseCoords = New Point(0, 0)


        _timer = New Timer With {.Interval = 1000 / SharedResources.TickRate}
        _timer.Start()
    End Sub




    Private Sub Tick(sender As Timer, e As EventArgs) Handles _timer.Tick
        SharedResources.stateManager.Tick()
        _window.Invalidate()
    End Sub

    Private Sub paint_event(sender As GameWindow, e As PaintEventArgs) Handles _window.Paint
        SharedResources.stateManager.Render(e.Graphics)
    End Sub

    Public Sub KeyDown_Event(sender As GameWindow, e As KeyEventArgs) Handles _window.KeyDown
        If SharedResources.inputKeys.Count - 1 < e.KeyCode Then
            ReDim Preserve SharedResources.inputKeys(e.KeyCode)
        End If
        SharedResources.inputKeys(e.KeyCode) = True
    End Sub

    Public Sub KeyUp_Event(sender As GameWindow, e As KeyEventArgs) Handles _window.KeyUp
        If SharedResources.inputKeys.Count - 1 < e.KeyCode Then
            ReDim Preserve SharedResources.inputKeys(e.KeyCode)
        End If
        SharedResources.inputKeys(e.KeyCode) = False
    End Sub
    Public Sub KeyPress_Event(sender As GameWindow, e As KeyPressEventArgs) Handles _window.KeyPress
        SharedResources.stateManager.KeyPress(e.KeyChar)
    End Sub

    Public Sub MouseMove_Event(sender As GameWindow, e As MouseEventArgs) Handles _window.MouseMove
        SharedResources.LastKnownMouseCoords = New Point(e.X, e.Y)
    End Sub

    Public Sub MouseClick_Event(sender As GameWindow, e As MouseEventArgs) Handles _window.Click
        If e.Button = MouseButtons.Left Then
            SharedResources.stateManager.Click()
        ElseIf e.Button = MouseButtons.Right Then
            SharedResources.stateManager.RightClick()
        End If
    End Sub
End Class
