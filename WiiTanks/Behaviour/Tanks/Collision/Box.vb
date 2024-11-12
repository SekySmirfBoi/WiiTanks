Public Class Box
    Inherits Collision


    Sub New(startPoint As Point, endPoint As Point)
        MyBase.New(startPoint, endPoint)
    End Sub

    Public Overrides Sub CheckOverlap(Collision As Collision)
        Throw New NotImplementedException()
    End Sub
End Class
