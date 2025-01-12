Public Class Level17
    Inherits Level
    Public Overrides Function Create() As Object
        Dim gameMap(SharedResources.MapSize.Width, SharedResources.MapSize.Height) As String

        gameMap(1, 3) = GameMapTiles.PLAYER_SPAWN

        For i As Integer = 0 To SharedResources.MapSize.Width ' Bottom horizontal
            Select Case i
                Case 0 To 3, 12
                    gameMap(i, 9) = GameMapTiles.WALL
                Case 4 To 5, 10 To 11
                    gameMap(i, 9) = GameMapTiles.DESTROYABLE_WALL
                Case Else
            End Select
        Next

        For i As Integer = 0 To SharedResources.MapSize.Width

        Next

        Return gameMap
    End Function
End Class
