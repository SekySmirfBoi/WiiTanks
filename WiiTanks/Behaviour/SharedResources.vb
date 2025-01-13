Imports System.Threading

Public Class SharedResources
    Public Shared TickRate As Integer = 60

    Public Shared RNG As New Random
    Public Shared TileSize As Size = New Size(55, 55)
    Public Shared MapSize As Size = New Size(16, 14)
    Public Shared NumberOfLevels As Integer = 17

    Public Shared window As Form
    'Public Shared WindowSize As Size = New Size(TileSize.Width * (MapSize.Width + 2) + 16, TileSize.Height * (MapSize.Height + 2) + 39)
    Public Shared WindowSize As Size = New Size(400, 400)
    Public Shared CentreWindowCoord As Point = New Point(WindowSize.Width / 2, WindowSize.Height / 2)
    Public Shared TextBrush As Pen = New Pen(Color.Black, 1)
    Public Shared BlankImage As Image = New Bitmap(1, 1)
    Public Shared BtnSize As Size = New Size(150, 40)
    Public Shared DEFAULT_FONT As Font = New Font("Ariel", 24)

    Public Shared LastKnownMouseCoords As Point = New Point(0, 0)
    Public Shared inputKeys() As Boolean

    Public Shared walls() As BasicWall
    Public Shared playerTanks() As Player
    Public Shared projectiles() As BasicProjectile
    Public Shared enemyTanks() As Bae
    Public Shared playerTanksCount As Integer = 0
    Public Shared projectileCount As Integer = 0
    Public Shared enemyTanksCount As Integer = 0

    Public Shared finishedLoadingMap As Boolean = False
    Public Shared gameEnded As Boolean = False

    Public Shared stateManager As StateManager
    Public Shared threads As New List(Of Thread)


    Public Shared Function CalculateBtnPos(btnNum As Integer, NumOfBtns As Integer) As Point
        Return CalculateBtnPos(btnNum, NumOfBtns, BtnSize)
    End Function

    Public Shared Function CalculateBtnPos(btnNum As Integer, NumOfBtns As Integer, btnSize As Size) As Point
        Return New Point(CentreWindowCoord.X - btnSize.Width / 2, CentreWindowCoord.Y + 5 - (NumOfBtns * 25) + (50 * (btnNum - 1)))
    End Function

    Public Shared Sub createThread(func As ThreadStart)
        Dim curthread As New Thread(func)
        curthread.Start()
        threads.Add(curthread)
    End Sub

    Public Shared Sub WaitForThreads()
        Dim running As Boolean = True

        While running
            running = False
            Dim deadThreads As New List(Of Thread)
            For Each thr As Thread In threads
                If thr.IsAlive() Then
                    running = True
                Else
                    deadThreads.Add(thr)
                End If
            Next

            For Each thr As Thread In deadThreads
                threads.Remove(thr)
            Next
        End While

        Return
    End Sub

    Public Shared Sub DeleteAll()
        playerTanksCount = 0
        projectileCount = 0
        enemyTanksCount = 0

        ReDim playerTanks(0)
        ReDim projectiles(0)
        ReDim enemyTanks(0)
    End Sub

    Public Shared Sub CreateProjectile(Location As Point, angle As Integer, type As String)
        Dim proj As BasicProjectile
        If type = ProjectileTypes.BASIC Then
            proj = New BasicProjectile(angle, Location, 2 * Math.Sqrt(18), 2)
        Else
            proj = New BasicProjectile(angle, Location, Math.Sqrt(18), 2)
        End If
        ReDim Preserve projectiles(projectileCount)
        projectiles(projectileCount) = proj
        projectileCount += 1
    End Sub

    Public Shared Sub DestroyProjectile(proj As BasicProjectile)
        If Not gameEnded Then
            Dim amountToSfit As Integer = 0

            projectileCount -= 1

            For i As Integer = 0 To projectileCount
                If Not projectiles(i).Equals(proj) Then
                    projectiles(i - amountToSfit) = projectiles(i)
                Else
                    amountToSfit += 1
                End If
            Next

            If projectileCount > 0 Then
                ReDim Preserve projectiles(projectileCount - 1)
            Else
                ReDim projectiles(0)
            End If
        End If
    End Sub

    Public Shared Sub killPlayer(player As Player)
        If Not gameEnded Then
            Dim amountToSfit As Integer = 0

            playerTanksCount -= 1

            For i As Integer = 0 To playerTanksCount
                If Not playerTanks(i).Equals(player) Then
                    playerTanks(i - amountToSfit) = playerTanks(i)
                Else
                    amountToSfit += 1
                End If
            Next

            If playerTanksCount > 0 Then
                ReDim Preserve playerTanks(playerTanksCount - 1)
            Else
                ReDim playerTanks(0)
            End If

            If playerTanksCount <= 0 Then
                gameEnded = True
            End If
        End If
    End Sub

    Public Shared Sub killEnemt(enemebr As Bae)
        If Not gameEnded Then
            Dim amountToSfit As Integer = 0

            enemyTanksCount -= 1

            For i As Integer = 0 To enemyTanksCount
                If Not enemyTanks(i).Equals(enemebr) Then
                    enemyTanks(i - amountToSfit) = enemyTanks(i)
                Else
                    amountToSfit += 1
                End If
            Next

            If enemyTanksCount > 0 Then
                ReDim Preserve enemyTanks(enemyTanksCount - 1)
            Else
                ReDim enemyTanks(0)
            End If

            If enemyTanksCount <= 0 Then
                gameEnded = True
            End If
        End If
    End Sub
End Class
