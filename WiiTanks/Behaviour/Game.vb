Imports System.Windows.Forms.VisualStyles

Public Class Game

    Private WithEvents _window As GameWindow
    Private WithEvents _timer As Timer
    Private _tickRate As Integer = 60

    Private _LastKnownMouseCoords As Point

    Private _inputPlayer As New Dictionary(Of Keys, Player)
    Private _inputKeys() As Boolean

    Private _playerTanksCount As Integer = 0
    Private _playerTanks() As Player
    Private _wallCount As Integer = 0
    Private _walls() As BasicWall

    Private _stateManager As StateManager

    Sub New(window As GameWindow)
        ' each level will be in a grid 20 wide 15 high
        ' each tile 70 x 70 pixles
        setupVariables(window)

        CreatePlayer(New Point(300, 150))
        CreateWall(New Point(500, 600))
    End Sub




    Private Sub setupVariables(window As GameWindow)
        _window = window

        ReDim Preserve _inputKeys(0)
        _LastKnownMouseCoords = New Point(0, 0)

        _stateManager = New StateManager(New MenuState())

        _timer = New Timer With {.Interval = 1000 / _tickRate}
        _timer.Start()
    End Sub




    Private Sub Tick(sender As Timer, e As EventArgs) Handles _timer.Tick
        For Each pTank As Player In _playerTanks
            pTank.Tick(_inputKeys, _walls)
        Next
        _stateManager.Tick()
        _window.Invalidate()
    End Sub




    Private Sub CreatePlayer(spawnLocation As Point)
        Dim playerTank As New Player(spawnLocation, _tickRate)
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

    Private Sub AssociateKeyWithTank(key As Keys, player As Player)
        _inputPlayer.Add(key, player)
    End Sub




    Private Sub paint_event(sender As GameWindow, e As PaintEventArgs) Handles _window.Paint
        Dim textBrush As New Pen(Color.Black, 1)
        'e.Graphics.DrawString("W:" & If(_inputKeys.Count >= Keys.W, Convert.ToString(_inputKeys(Keys.W)), "False"), New Font("Arial", 13), textBrush.Brush, New Point(500, 100))
        'e.Graphics.DrawString("A:" & If(_inputKeys.Count >= Keys.A, Convert.ToString(_inputKeys(Keys.A)), "False"), New Font("Arial", 13), textBrush.Brush, New Point(500, 150))
        'e.Graphics.DrawString("S:" & If(_inputKeys.Count >= Keys.S, Convert.ToString(_inputKeys(Keys.S)), "False"), New Font("Arial", 13), textBrush.Brush, New Point(500, 200))
        'e.Graphics.DrawString("D:" & If(_inputKeys.Count >= Keys.D, Convert.ToString(_inputKeys(Keys.D)), "False"), New Font("Arial", 13), textBrush.Brush, New Point(500, 250))

        For Each pTank As Player In _playerTanks
            e.Graphics.DrawImage(pTank.getImage(_LastKnownMouseCoords), pTank.Location)
            e.Graphics.DrawLine(New Pen(Color.Red, 3), New Point(0, 0), pTank.CentreCood)
            e.Graphics.DrawLine(New Pen(Color.Lime, 3), _LastKnownMouseCoords, pTank.CentreCood)
            e.Graphics.DrawRectangle(New Pen(Color.Blue, 3), New Rectangle(pTank.Location, pTank.Size))
            e.Graphics.DrawRectangle(New Pen(Color.Red, 3), pTank.collBox)
        Next

        For Each wall As BasicWall In _walls
            e.Graphics.DrawImage(wall.Image, wall.Location)
            e.Graphics.DrawRectangle(New Pen(Color.Red, 3), wall.rect)
        Next

        _stateManager.Render(e.Graphics)
    End Sub

    Public Sub KeyDown_Event(sender As GameWindow, e As KeyEventArgs) Handles _window.KeyDown
        If _inputKeys.Count - 1 < e.KeyCode Then
            ReDim Preserve _inputKeys(e.KeyCode)
        End If
        _inputKeys(e.KeyCode) = True
    End Sub

    Public Sub KeyUp_Event(sender As GameWindow, e As KeyEventArgs) Handles _window.KeyUp
        If _inputKeys.Count - 1 < e.KeyCode Then
            ReDim Preserve _inputKeys(e.KeyCode)
        End If
        _inputKeys(e.KeyCode) = False
    End Sub

    Public Sub MouseMove_Event(sender As GameWindow, e As MouseEventArgs) Handles _window.MouseMove
        _LastKnownMouseCoords = New Point(e.X, e.Y)
    End Sub

    Public Sub MouseClick_Event(sender As GameWindow, e As MouseEventArgs) Handles _window.Click

    End Sub
End Class
