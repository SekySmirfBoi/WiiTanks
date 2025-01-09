Public Class Green
    Inherits Bae

    Public Sub New(spawnLocation As Point)
        MyBase.New(spawnLocation, 0, New GreenAI(100), ProjectileTypes.ADVANCED_2)
        p_baseImage = My.Resources.GreenTankBase
        p_turretImage = My.Resources.GreenTankTurret
    End Sub

    Public Overrides Sub Tick()
        MyBase.Tick()
    End Sub

    Protected Overrides Sub MovePlayerTank()
        MyBase.MovePlayerTank()
    End Sub

    Protected Overrides Sub MoveEnemyTank()

    End Sub
End Class
