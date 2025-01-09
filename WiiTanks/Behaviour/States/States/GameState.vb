Imports System.Threading

Public Class GameState
    Inherits State

    Private _levelNum As Integer
    Private _level As Level
    Private _gameMap(,) As String

    Private _wallCount As Integer = 0

    Private threads As New List(Of Thread)

    Sub New(Level As Integer)
        SharedResources.gameEnded = False
        _levelNum = Level
        ReDim SharedResources.playerTanks(0)
    End Sub

    Private Sub CreatePlayer(spawnLocation As Point)
        Dim playerTank As New Player(spawnLocation)
        ReDim Preserve SharedResources.playerTanks(SharedResources.playerTanksCount)
        SharedResources.playerTanks(SharedResources.playerTanksCount) = playerTank
        SharedResources.playerTanksCount += 1

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
            Case GameMapTiles.GREEN_SPAWN
                enemytank = New Brown(location)
            Case Else
                enemytank = New Player(location)
        End Select

        ReDim Preserve SharedResources.enemyTanks(SharedResources.enemyTanksCount)
        SharedResources.enemyTanks(SharedResources.enemyTanksCount) = enemytank
        SharedResources.enemyTanksCount += 1
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

    Public Overrides Sub Create()
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
        If SharedResources.playerTanksCount > 0 Then
            For Each pTank As Player In SharedResources.playerTanks
                SharedResources.createThread(New ThreadStart(AddressOf pTank.Tick))
                'pTank.Tick()
            Next
        End If

        If SharedResources.enemyTanksCount > 0 Then
            For Each eTank As Bae In SharedResources.enemyTanks
                SharedResources.createThread(New ThreadStart(AddressOf eTank.Tick))
                'eTank.Tick()
            Next
        End If

        If SharedResources.projectileCount > 0 Then
            For Each proj As BasicProjectile In SharedResources.projectiles
                SharedResources.createThread(New ThreadStart(AddressOf proj.Tick))
                'proj.Tick()
            Next
        End If

        If SharedResources.inputKeys.Count - 1 >= Keys.Escape Then
            If SharedResources.inputKeys(Keys.Escape) Then
                SharedResources.stateManager.ChangeState(New LevelSelectState())
            End If
        End If

        SharedResources.WaitForThreads()

        If SharedResources.gameEnded Then
            If SharedResources.playerTanksCount > 0 Then
                SharedResources.stateManager.ChangeState(New GameWinState(_levelNum))
            Else
                SharedResources.stateManager.ChangeState(New GameLossState(_levelNum))
            End If
        End If
    End Sub

    Public Overrides Sub Render(graphics As Graphics)
        If SharedResources.playerTanksCount > 0 Then
            For Each pTank As Player In SharedResources.playerTanks
                graphics.DrawImage(pTank.Image, pTank.Location)
                graphics.DrawRectangle(New Pen(Color.Blue, 3), New Rectangle(pTank.Location, pTank.Size))

                If SharedResources.inputKeys.Length - 1 >= Keys.P Then
                    If SharedResources.inputKeys(Keys.P) Then
                        graphics.DrawRectangle(New Pen(Color.Red, 3), pTank.collBox)
                    End If
                End If

                'graphics.DrawString("X:" & pTank.Location.X, SharedResources.DEFAULT_FONT, SharedResources.TextBrush.Brush, New Point(800, 200))
                'graphics.DrawString("Y:" & pTank.Location.Y, SharedResources.DEFAULT_FONT, SharedResources.TextBrush.Brush, New Point(800, 250))
            Next
        End If

        If SharedResources.enemyTanksCount > 0 Then
            For Each eTank As Bae In SharedResources.enemyTanks
                graphics.DrawImage(eTank.Image(), eTank.Location)
            Next
        End If

        If SharedResources.projectileCount > 0 Then
            For Each proj As BasicProjectile In SharedResources.projectiles
                graphics.DrawImage(proj.Image, proj.Location)
            Next
        End If

        For Each wall As BasicWall In SharedResources.walls
            graphics.DrawImage(wall.Image, wall.Location)
        Next

        'For y As Integer = 0 To 20
        '    graphics.DrawLine(New Pen(Color.Green), New Point(0, y * SharedResources.TileSize.Height), New Point(SharedResources.WindowSize.Width, y * SharedResources.TileSize.Height))
        'Next
        '
        'For x As Integer = 0 To 20
        '    graphics.DrawLine(New Pen(Color.Green), New Point(x * SharedResources.TileSize.Width, 0), New Point(x * SharedResources.TileSize.Width, SharedResources.WindowSize.Height))
        'Next
    End Sub

    Public Overrides Sub Click()
        If SharedResources.finishedLoadingMap Then
            If SharedResources.playerTanksCount > 0 Then
                SharedResources.playerTanks(0).Shoot()
            End If
        End If
    End Sub
End Class
