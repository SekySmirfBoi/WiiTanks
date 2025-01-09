Public Class GreenAI
    Inherits BaseAI

    Public Sub New(turretVelocity As Integer, maxFLurry As Integer)
        MyBase.New(turretVelocity, maxFLurry)
    End Sub

    Public Overrides Sub Tick()
        MyBase.Tick()
    End Sub

    Protected Overrides Function calculateTurret(btmp1 As Bitmap) As Object
        Return MyBase.calculateTurret(btmp1)
    End Function
End Class
