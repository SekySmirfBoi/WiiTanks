Public Class Line
    Inherits Collision

    Sub New(startPoint As Point, endPoint As Point)
        MyBase.New(startPoint, endPoint)
    End Sub

    Public Overrides Function CheckOverlap(Collision As Collision)
        Throw New NotImplementedException()
    End Function
End Class
