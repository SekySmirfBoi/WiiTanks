Public Class Box
    Inherits CollisionOld


    Sub New(startPoint As Point, endPoint As Point)
        MyBase.New(startPoint, endPoint)
    End Sub

    Public Overrides Function CheckOverlap(Collision As CollisionOld)
        Dim bottomLeft As New Point(StartPoint.X, EndPoint.Y)
        Dim topRight As New Point(EndPoint.X, StartPoint.Y)
        Dim otherBottomLeft As New Point(Collision.StartPoint.X, Collision.EndPoint.Y)
        Dim otherTopRight As New Point(Collision.EndPoint.X, Collision.StartPoint.Y)

        If StartPoint.X >= Collision.StartPoint.X And StartPoint.X <= Collision.EndPoint.X And StartPoint.Y >= Collision.StartPoint.Y And StartPoint.Y <= Collision.EndPoint.Y Or
           topRight.X >= Collision.StartPoint.X And topRight.X <= Collision.EndPoint.X And topRight.Y >= Collision.StartPoint.Y And topRight.Y <= Collision.EndPoint.Y Or
           bottomLeft.X >= Collision.StartPoint.X And bottomLeft.X <= Collision.EndPoint.X And bottomLeft.Y >= Collision.StartPoint.Y And bottomLeft.Y <= Collision.EndPoint.Y Or
           EndPoint.X >= Collision.StartPoint.X And EndPoint.X <= Collision.EndPoint.X And EndPoint.Y >= Collision.StartPoint.Y And EndPoint.Y <= Collision.EndPoint.Y Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
