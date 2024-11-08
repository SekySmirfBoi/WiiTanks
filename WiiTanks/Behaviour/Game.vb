Public Class Game

    Private WithEvents _window As GameWindow
    Private WithEvents _timer As Timer
    Private _tickRate As Integer = 60

    Private _LastKnownMouseCoords As Point

    Private WithEvents _backImage As Image
    Private _graohics As Graphics

    Private _inputPlayer As New Dictionary(Of Keys, Player)
    Private _inputKeys() As Boolean

    Private _playerTanksCount As Integer = 0
    Private _playerTanks() As Player

    Public Property BackImage As Image
        Get
            Return _backImage
        End Get
        Set(value As Image)
            _backImage = value
        End Set
    End Property

    Public ReadOnly Property Graphics As Graphics
        Get
            Return _graohics
        End Get
    End Property

    Sub New(window As GameWindow)
        _window = window
        _backImage = New Bitmap(_window.Width, _window.Height)
        _window.BackgroundImage = _backImage
        _graohics = Graphics.FromImage(_backImage)

        ReDim Preserve _inputKeys(0)
        _LastKnownMouseCoords = New Point(0, 0)

        _timer = New Timer With {.Interval = 1000 / _tickRate}
        _timer.Start()


        CreatePlayer(New Point(300, 150))
    End Sub

    Private Sub Tick(sender As Timer, e As EventArgs) Handles _timer.Tick
        For Each pTank As Player In _playerTanks
            pTank.Tick(_LastKnownMouseCoords, _inputKeys)
        Next
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

    Private Sub AssociateKeyWithTank(key As Keys, player As Player)

    End Sub

    Private Sub paint_event(sender As GameWindow, e As PaintEventArgs) Handles _window.Paint
        'e.Graphics.DrawImage(_playerTanks(0).getImage(), _playerTanks(0).Location)
        For Each pTank As Player In _playerTanks
            e.Graphics.DrawImage(pTank.getImage(), pTank.Location)
            e.Graphics.DrawLine(New Pen(Color.Red, 3), New Point(0, 0), pTank.CentreCood)
        Next
    End Sub

    Public Sub KeyDown_Event(e As KeyEventArgs)
        If _inputKeys.Count - 1 < e.KeyCode Then
            ReDim Preserve _inputKeys(e.KeyCode)
        End If
        _inputKeys(e.KeyCode) = True
    End Sub

    Public Sub KeyUp_Event(e As KeyEventArgs)
        If _inputKeys.Count - 1 < e.KeyCode Then
            ReDim Preserve _inputKeys(e.KeyCode)
        End If
        _inputKeys(e.KeyCode) = False
    End Sub

    Public Sub MouseMove_Event(e As MouseEventArgs)
        _LastKnownMouseCoords = New Point(e.X, e.Y)
    End Sub
End Class
