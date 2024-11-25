Public Class Level1
    Inherits Level
    Public Overrides Sub Create(gameMap(,) As String)
        ReDim gameMap(20, 15)
        gameMap(5, 5) = GameMapTiles.PLAYER_SPAWN
    End Sub
End Class
