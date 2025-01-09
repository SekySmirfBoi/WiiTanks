Public Class Green
    Inherits Bae

    Public Sub New(spawnLocation As Point)
        MyBase.New(spawnLocation, 0, New GreenAI(100, 0), ProjectileTypes.ADVANCED_2)

    End Sub
End Class
