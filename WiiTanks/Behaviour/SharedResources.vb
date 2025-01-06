Public Class SharedResources
    Public Shared TickRate As Integer = 60

    Public Shared RNG As New Random
    Public Shared TileSize As Size = New Size(55, 55)
    Public Shared MapSize As Size = New Size(20, 15)

    Public Shared WindowSize As Size = New Size(TileSize.Width * (MapSize.Width + 2) + 16, TileSize.Height * (MapSize.Height + 2) + 39)
    Public Shared CentreWindowCoord As Point = New Point(WindowSize.Width / 2, WindowSize.Height / 2)
    Public Shared TextBrush As Pen = New Pen(Color.Black, 1)
    Public Shared BlankImage As Image = New Bitmap(1, 1)
    Public Shared BtnSize As Size = New Size(120, 40)
    Public Shared DEFAULT_FONT As Font = New Font("Ariel", 24)

    Public Shared LastKnownMouseCoords As Point = New Point(0, 0)
    Public Shared inputKeys() As Boolean

    Public Shared walls() As BasicWall
    Public Shared playerTanks() As Player
    Public Shared projectiles() As BasicProjectile
    Public Shared playerTanksCount As Integer = 0
    Public Shared projectileCount As Integer = 0
    Public Shared enemyTanksCount As Integer = 0

    Public Shared finishedLoadingMap As Boolean = False


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
        Dim amountToSfit As Integer = 0

        projectileCount -= 1

        For i As Integer = 0 To projectileCount
            If Not projectiles(i).Equals(proj) Then
                projectiles(i - amountToSfit) = projectiles(i)
            Else
                amountToSfit += 1
            End If
        Next
    End Sub
End Class
