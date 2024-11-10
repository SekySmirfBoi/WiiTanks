Public Class Collision

    Private _StartPoint As Point
    Private _EndPoint As Point

    Public ReadOnly Property StartPoint As Point
        Get
            Return _StartPoint
        End Get
    End Property

    Public ReadOnly Property EndPoint As Point
        Get
            Return _EndPoint
        End Get
    End Property

    Sub New(StartPoint As Point, EndPoint As Point)
        _StartPoint = StartPoint
        _EndPoint = EndPoint
    End Sub

    Public Sub updateCollision(newStartPoint As Point, newEndPoint As Point)
        _StartPoint = newStartPoint
        _EndPoint = newEndPoint
    End Sub
End Class
