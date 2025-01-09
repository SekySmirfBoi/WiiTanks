Public Class BorwnAI
    Inherits BaseAI

    Sub New(turretVelocity)
        MyBase.New(turretVelocity, 1)
    End Sub

    Public Overrides Sub Tick()
        MyBase.Tick()
        If p_turretAngle = p_target Then
            p_target = SharedResources.RNG.Next(0, 360)
            If SharedResources.projectileCount < 4 Then
                Shoot()
            End If
        End If
        changeTurretAngle()
    End Sub
End Class
