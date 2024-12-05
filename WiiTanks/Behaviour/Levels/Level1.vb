Public Class Level1
    Inherits Level
    Public Overrides Function Create()
        Dim gameMap(20, 15) As String
        gameMap(10, 10) = GameMapTiles.PLAYER_SPAWN

        gameMap(2, 2) = GameMapTiles.BROWN_SPAWN

        gameMap(8, 8) = GameMapTiles.BROWN_SPAWN
        Return gameMap
    End Function
End Class
