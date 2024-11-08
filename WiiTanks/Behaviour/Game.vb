Public Class Game

    Private WithEvents _window As GameWindow
    Private WithEvents _timer As Timer
    Private _tickRate As Integer = 60

    Private _LastKnownMouseCoords As Point

    Private WithEvents _backImage As Image
    Private _graohics As Graphics

    Private _inputFunction As New Dictionary(Of Keys, Func(Of Boolean)) ''''''''''''''''''''''''''''''''''''' WTF THIS DOESNT CAUSE AN ERROR IMMIDIETELY

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

    Private _teswt = 0
    'Private _testTank As Bae
    Sub New(window As GameWindow)
        _window = window
        _backImage = New Bitmap(_window.Width, _window.Height)
        _window.BackgroundImage = _backImage
        _graohics = Graphics.FromImage(_backImage)

        ReDim Preserve _playerTanks(0)          ' Redim Preserve array allows for the size of the array to be altered and the contents of the array will remain
        _LastKnownMouseCoords = New Point(0, 0)

        _timer = New Timer With {.Interval = 1000 / _tickRate}
        _timer.Start()


        Dim testtank As New Player
        '_testTank = testtank
        _playerTanks(0) = testtank
    End Sub

    Private Sub tick(sender As Timer, e As EventArgs) Handles _timer.Tick
        _playerTanks(0).tick(_LastKnownMouseCoords)
        _window.Invalidate()
    End Sub

    Private Sub paint_event(sender As GameWindow, e As PaintEventArgs) Handles _window.Paint
        e.Graphics.DrawImage(_playerTanks(0).getImage(), _playerTanks(0).Location)
        e.Graphics.DrawLine(New Pen(Color.Red, 3), New Point(0, 0), _playerTanks(0).CentreCood)
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
