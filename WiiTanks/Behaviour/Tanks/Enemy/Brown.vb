Public Class Brown
    Inherits Bae

    Sub New(spawnLocation As Point)
        MyBase.New(spawnLocation, 0, New BorwnAI(2), ProjectileTypes.BASIC)
        p_baseImage = My.Resources.BrownTankBase
        p_turretImage = My.Resources.BrownTankTurret
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
