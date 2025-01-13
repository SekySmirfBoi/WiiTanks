Public Class Level1
    Inherits Level
    Public Overrides Function Create()
        Dim gameMap(19, 14) As String
        gameMap(10, 10) = GameMapTiles.PLAYER_SPAWN

        For x As Integer = 3 To SharedResources.MapSize.Width
            gameMap(x, 5) = GameMapTiles.WALL
        Next

        gameMap(2, 2) = GameMapTiles.BROWN_SPAWN
        gameMap(8, 8) = GameMapTiles.BROWN_SPAWN
        Return gameMap
    End Function
End Class
