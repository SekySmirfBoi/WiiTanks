Public Class GreenAI
    Inherits BaseAI

    Public Sub New(turretVelocity As Integer)
        MyBase.New(turretVelocity, 2)
    End Sub

    Public Overrides Sub Tick()
        MyBase.Tick()
    End Sub
End Class
