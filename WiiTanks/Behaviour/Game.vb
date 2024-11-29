Imports System.Windows.Forms.VisualStyles

Public Class Game

    Private WithEvents _window As GameWindow
    Private WithEvents _timer As Timer

    Private _inputPlayer As New Dictionary(Of Keys, Player)

    Private _playerTanksCount As Integer = 0
    Private _playerTanks() As Player
    Private _wallCount As Integer = 0
    Private _walls() As BasicWall

    Private _stateManager As StateManager

    Sub New(window As GameWindow)
        _stateManager = New StateManager(New GameState(1))
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
        _stateManager.Tick()
        _window.Invalidate()
    End Sub




    Private Sub CreatePlayer(spawnLocation As Point)
        Dim playerTank As New Player(spawnLocation)
        ReDim Preserve _playerTanks(_playerTanksCount)
        _playerTanks(_playerTanksCount) = playerTank
        _playerTanksCount += 1

        playerTank.AssociateKey(Keys.W, Directions.UP)
        playerTank.AssociateKey(Keys.A, Directions.LEFT)
        playerTank.AssociateKey(Keys.S, Directions.DOWN)
        playerTank.AssociateKey(Keys.D, Directions.RIGHT)
    End Sub
    Private Sub CreateWall(Location As Point)
        Dim wall As New BasicWall(Location, New Size(70, 70))
        ReDim Preserve _walls(_wallCount)
        _walls(_wallCount) = wall
        _wallCount += 1
    End Sub

    Private Sub CreateDestroyableWall(Location As Point)
        Dim wall As New DestroyableWall(Location, New Size(70, 70))
        ReDim Preserve _walls(_wallCount)
        _walls(_wallCount) = wall
        _wallCount += 1
    End Sub

    Private Sub AssociateKeyWithTank(key As Keys, player As Player)
        _inputPlayer.Add(key, player)
    End Sub




    Private Sub paint_event(sender As GameWindow, e As PaintEventArgs) Handles _window.Paint
        'e.Graphics.DrawString("W:" & If(_inputKeys.Count >= Keys.W, Convert.ToString(_inputKeys(Keys.W)), "False"), New Font("Arial", 13), textBrush.Brush, New Point(500, 100))
        'e.Graphics.DrawString("A:" & If(_inputKeys.Count >= Keys.A, Convert.ToString(_inputKeys(Keys.A)), "False"), New Font("Arial", 13), textBrush.Brush, New Point(500, 150))
        'e.Graphics.DrawString("S:" & If(_inputKeys.Count >= Keys.S, Convert.ToString(_inputKeys(Keys.S)), "False"), New Font("Arial", 13), textBrush.Brush, New Point(500, 200))
        'e.Graphics.DrawString("D:" & If(_inputKeys.Count >= Keys.D, Convert.ToString(_inputKeys(Keys.D)), "False"), New Font("Arial", 13), textBrush.Brush, New Point(500, 250))
        'e.Graphics.DrawString("Count:" & _inputKeys.Count - 1, New Font("Arial", 13), SharedResources.TextBrush.Brush, New Point(500, 250))



        'e.Graphics.DrawRectangle(New Pen(Color.Green), New Rectangle(New Point(SharedResources.TileSize), New Size(SharedResources.TileSize.Width * SharedResources.MapSize.Width, SharedResources.TileSize.Height * SharedResources.MapSize.Height)))

        _stateManager.Render(e.Graphics)
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

    Public Sub MouseMove_Event(sender As GameWindow, e As MouseEventArgs) Handles _window.MouseMove
        SharedResources.LastKnownMouseCoords = New Point(e.X, e.Y)
    End Sub

    Public Sub MouseClick_Event(sender As GameWindow, e As MouseEventArgs) Handles _window.Click
        _stateManager.Click()
    End Sub
End Class
