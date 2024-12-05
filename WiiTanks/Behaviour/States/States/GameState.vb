Public Class GameState
    Inherits State

    Private _levelNum As Integer
    Private _level As Level
    Private _uiManager As UIManager
    Private _gameMap(,) As String

    Private _playerTanksCount As Integer = 0
    Private _enemyTanksCount As Integer = 0
    Private _enemyTanks() As Bae

    Private _wallCount As Integer = 0

    Sub New(Level As Integer)
        _levelNum = Level
        ReDim SharedResources.playerTanks(0)
    End Sub

    Private Sub CreatePlayer(spawnLocation As Point)
        Dim playerTank As New Player(spawnLocation)
        ReDim Preserve SharedResources.Playertanks(_playerTanksCount)
        SharedResources.playerTanks(_playerTanksCount) = playerTank
        _playerTanksCount += 1

        playerTank.AssociateKey(Keys.W, Directions.UP)
        playerTank.AssociateKey(Keys.A, Directions.LEFT)
        playerTank.AssociateKey(Keys.S, Directions.DOWN)
        playerTank.AssociateKey(Keys.D, Directions.RIGHT)
    End Sub

    Private Sub CreateEnemy(location As Point, type As String)
        Dim enemytank As Bae

        Select Case type
            Case GameMapTiles.BROWN_SPAWN
                enemytank = New Brown(location)
            Case Else
                enemytank = New Player(location)
        End Select

        ReDim Preserve _enemyTanks(_enemyTanksCount)
        _enemyTanks(_enemyTanksCount) = enemytank
        _enemyTanksCount += 1
    End Sub

    Private Sub CreateWall(Location As Point)
        Dim wall As New BasicWall(Location, SharedResources.TileSize)
        ReDim Preserve SharedResources.walls(_wallCount)
        SharedResources.walls(_wallCount) = wall
        _wallCount += 1
    End Sub

    Private Sub CreateDestroyableWall(Location As Point)
        Dim wall As New DestroyableWall(Location, SharedResources.TileSize)
        ReDim Preserve SharedResources.walls(_wallCount)
        SharedResources.walls(_wallCount) = wall
        _wallCount += 1
    End Sub

    Private Sub CreateMapBorder()
        For i As Integer = 0 To SharedResources.MapSize.Width + 1
            CreateWall(New Point(i * SharedResources.TileSize.Width, 0))
            CreateWall(New Point(i * SharedResources.TileSize.Width, SharedResources.TileSize.Height * (SharedResources.MapSize.Height + 1)))
        Next
        For i As Integer = 1 To SharedResources.MapSize.Height
            CreateWall(New Point(0, i * SharedResources.TileSize.Height))
            CreateWall(New Point(SharedResources.TileSize.Width * (SharedResources.MapSize.Width + 1), i * SharedResources.TileSize.Height))
        Next
    End Sub

    Public Overrides Sub Create(parent As StateManager)
        p_father = parent

        _uiManager = New UIManager

        SharedResources.finishedLoadingMap = True

        Select Case _levelNum
            Case 1
                _level = New Level1
            Case Else
                MsgBox(_levelNum)
        End Select

        _gameMap = _level.Create()
        CreateMapBorder()


        Dim playerSpawnCount As Integer = 0
        Dim xCoordPlayerSpawn As New Dictionary(Of Integer, Integer)
        Dim yCoordPlayerSpawn As New Dictionary(Of Integer, Integer)

        For y As Integer = 0 To _gameMap.GetLength(1) - 1
            For x As Integer = 0 To _gameMap.GetLength(0) - 1
                Select Case _gameMap(x, y)
                    Case GameMapTiles.BLANK
                    Case GameMapTiles.WALL
                        CreateWall(New Point((x + 1) * SharedResources.TileSize.Width, (y + 1) * SharedResources.TileSize.Height))
                    Case GameMapTiles.DESTROYABLE_WALL
                        CreateDestroyableWall(New Point((x + 1) * SharedResources.TileSize.Width, (y + 1) * SharedResources.TileSize.Height))
                    Case GameMapTiles.PLAYER_SPAWN
                        xCoordPlayerSpawn.Add(playerSpawnCount, x)
                        yCoordPlayerSpawn.Add(playerSpawnCount, y)
                        playerSpawnCount += 1
                    Case GameMapTiles.BROWN_SPAWN,
                         GameMapTiles.GREY_SPAWN,
                         GameMapTiles.TEAL_SPAWN,
                         GameMapTiles.YELLOW_SPAWN,
                         GameMapTiles.RED_SPAWN,
                         GameMapTiles.GREEN_SPAWN,
                         GameMapTiles.PURPLE_SPAWN,
                         GameMapTiles.WHITE_SPAWN,
                         GameMapTiles.BLACK_SPAWN
                        CreateEnemy(New Point((x + 1) * SharedResources.TileSize.Width, (y + 1) * SharedResources.TileSize.Height), _gameMap(x, y))
                    Case Else
                End Select
            Next
        Next

        Dim rng As New Random
        Dim spawnIndex As Integer = rng.Next(0, playerSpawnCount)

        CreatePlayer(New Point((xCoordPlayerSpawn(spawnIndex) + 1) * SharedResources.TileSize.Width, (yCoordPlayerSpawn(spawnIndex) + 1) * SharedResources.TileSize.Height))
        xCoordPlayerSpawn.Remove(spawnIndex)
        yCoordPlayerSpawn.Remove(spawnIndex)

        SharedResources.finishedLoadingMap = True
    End Sub

    Public Overrides Sub Tick()
        For Each pTank As Player In SharedResources.playerTanks
            pTank.Tick()
        Next

        For Each eTank As Bae In _enemyTanks
            eTank.Tick()
        Next

        If SharedResources.inputKeys.Count - 1 >= Keys.Escape Then
            If SharedResources.inputKeys(Keys.Escape) Then
                p_father.ChangeState(New LevelSelectState())
            End If
        End If
    End Sub

    Public Overrides Sub Render(graphics As Graphics)
        For Each pTank As Player In SharedResources.playerTanks
            graphics.DrawImage(pTank.Image, pTank.Location)
            graphics.DrawRectangle(New Pen(Color.Blue, 3), New Rectangle(pTank.Location, pTank.Size))

            If SharedResources.inputKeys.Length - 1 >= Keys.P Then
                If SharedResources.inputKeys(Keys.P) Then
                    graphics.DrawRectangle(New Pen(Color.Red, 3), pTank.collBox)
                End If
            End If
        Next

        For Each eTank As Bae In _enemyTanks
            graphics.DrawImage(eTank.Image(), eTank.Location)

            For i As Integer = 0 To 1
                Dim x1 As Integer = eTank.CentreCood.X
                Dim y1 As Integer = eTank.CentreCood.Y

                Dim theta As Integer = If(i = 0, eTank.p_AI.p_turretAngle, eTank.p_AI.p_target)
                Dim phi As Integer = (180 - theta) / 2

                Dim thetaRad As Decimal = theta * (Math.PI / 180)
                Dim phiRad As Decimal = phi * (Math.PI / 180)

                Dim a As Decimal = Math.Sqrt(20000 - 20000 * Math.Cos(thetaRad))
                Dim o As Decimal = a * Math.Sin((Math.PI / 2) - phiRad)
                Dim n As Decimal = a * Math.Cos((Math.PI / 2) - phiRad)


                graphics.DrawLine(New Pen(Color.Red, 2), eTank.CentreCood, New Point(x1 + n, y1 - 100 + o))
            Next
        Next

            For Each wall As BasicWall In SharedResources.walls
            graphics.DrawImage(wall.Image, wall.Location)
        Next

        'For i As Integer = 0 To SharedResources.MapSize.Width
        '    For j As Integer = 0 To SharedResources.MapSize.Height
        '        graphics.DrawRectangle(New Pen(Color.Green), New Rectangle(New Point((i + 1) * SharedResources.TileSize.Width, (j + 1) * SharedResources.TileSize.Height), SharedResources.TileSize))
        '    Next
        'Next
    End Sub

    Public Overrides Sub Click()
    End Sub
End Class
