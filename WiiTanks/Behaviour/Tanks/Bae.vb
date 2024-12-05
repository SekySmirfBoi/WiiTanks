Public MustInherit Class Bae

    Protected p_baseImage As Image
    Protected p_turretImage As Image

    Protected p_baseRotation As Integer

    Protected p_ticksAlive As Integer
    Protected p_turretCooldown As Integer
    Protected p_turretCooldownSeconds As Decimal
    Private _projectileType As BasicProjectile

    Protected p_image As Image
    Protected p_size As Size
    Protected p_loc As Point
    Protected p_centreCoord As Point

    Protected p_xVel As Integer
    Protected p_yVel As Integer
    Protected p_movementVelocity As Decimal

    Protected p_collisionBox As Rectangle

    Public p_AI As BaseAI

    Public ReadOnly Property xVel As Integer
        Get
            Return p_xVel
        End Get
    End Property

    Public ReadOnly Property yVel As Integer
        Get
            Return p_yVel
        End Get
    End Property

    Public Property BaseRotiation As Integer
        Get
            Return p_baseRotation
        End Get
        Set(value As Integer)
            p_baseRotation = value
        End Set
    End Property

    Public ReadOnly Property Image As Image
        Get
            p_image = p_AI.calculateImage()
            Return p_image
        End Get
    End Property

    Public ReadOnly Property TurretImage As Image
        Get
            Return p_turretImage
        End Get
    End Property

    Public ReadOnly Property baseImage As Image
        Get
            Return p_baseImage
        End Get
    End Property

    Public ReadOnly Property Size As Size
        Get
            Return p_size
        End Get
    End Property

    Public ReadOnly Property Location As Point
        Get
            Return p_loc
        End Get
    End Property

    Public ReadOnly Property CentreCood As Point
        Get
            Return p_centreCoord
        End Get
    End Property

    Public ReadOnly Property collBox As Rectangle
        Get
            Return New Rectangle(CentreCood - New Point(20, 20), New Size(40, 40))
        End Get
    End Property

    Sub New(spawnLocation As Point, movementVelocity As Decimal, AI As BaseAI)
        p_size = New Size(150, 150)
        p_loc = spawnLocation
        p_centreCoord = New Point(spawnLocation.X + SharedResources.TileSize.Width / 2, spawnLocation.Y + SharedResources.TileSize.Height / 2)
        p_loc = New Point(p_centreCoord.X - p_size.Width / 2, p_centreCoord.Y - p_size.Height / 2)

        p_ticksAlive = 0
        p_turretCooldown = 1 * SharedResources.TickRate
        p_turretCooldownSeconds = 0

        p_movementVelocity = movementVelocity

        p_baseImage = My.Resources.BlankTankBase
        p_turretImage = My.Resources.BlankTankTurret

        p_AI = AI
        p_AI.assignCreature(Me)
    End Sub

    Public Overridable Sub Tick()
        p_ticksAlive += 1
        If p_turretCooldown < 0 Then
            p_turretCooldown = 0
        End If
        p_AI.Tick()
    End Sub

    Protected Overridable Sub MovePlayerTank()

    End Sub

    Protected Overridable Sub MoveEnemyTank()

    End Sub

    Protected Sub AcutallyMoveTheTank()
        Dim xDisplacement As Integer = p_xVel * If(p_xVel <> 0 And p_yVel <> 0, p_movementVelocity / Math.Sqrt(2), p_movementVelocity) * 60 / SharedResources.TickRate
        Dim yDisplacement As Integer = p_yVel * If(p_xVel <> 0 And p_yVel <> 0, p_movementVelocity / Math.Sqrt(2), p_movementVelocity) * 60 / SharedResources.TickRate
        Dim canMoveInX As Boolean = True
        Dim canMoveInY As Boolean = True

        For Each wall As BasicWall In SharedResources.walls
            Dim rectXOffsetted As Rectangle = collBox
            Dim rectYOffsetted As Rectangle = collBox

            rectXOffsetted.X = rectXOffsetted.X + xDisplacement
            rectYOffsetted.Y = rectYOffsetted.Y + yDisplacement

            If Collision.CheckRectangleCollision(rectXOffsetted, wall.rect) Then
                canMoveInX = False
            End If
            If Collision.CheckRectangleCollision(rectYOffsetted, wall.rect) Then
                canMoveInY = False
            End If
        Next

        p_loc = New Point(p_loc.X + If(canMoveInX, xDisplacement, 0), p_loc.Y + If(canMoveInY, yDisplacement, 0))
        p_centreCoord = New Point(p_loc.X + p_size.Width / 2, p_loc.Y + p_size.Height / 2)
    End Sub

    'Public Function calculateBase() As Image
    '    Dim btmp1 As New Bitmap(151, 151)
    '
    '    Using G = Graphics.FromImage(btmp1)
    '        G.TranslateTransform(btmp1.Width / 2, btmp1.Height / 2)
    '        G.RotateTransform(p_baseRotation)
    '        G.DrawImage(p_baseImage, New Point(-p_baseImage.Width / 2, -p_baseImage.Height / 2))
    '        G.TranslateTransform(-btmp1.Width / 2, -btmp1.Height / 2)
    '    End Using
    '
    '    Return btmp1
    'End Function
End Class
