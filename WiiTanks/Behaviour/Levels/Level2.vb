Public Class Level2
    Inherits Level
    Public Overrides Function Create()
        Dim gameMap(19, 14) As String

        gameMap(0, 0) = GameMapTiles.PLAYER_SPAWN
        gameMap(19, 14) = GameMapTiles.GREEN_SPAWN

        Return gameMap
    End Function
End Class
