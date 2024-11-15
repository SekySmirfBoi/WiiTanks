Public MustInherit Class Bae

    Protected p_baseImage As Image
    Protected p_turretImage As Image

    Protected p_baseRotation As Integer

    Protected p_ticksAlive As Integer
    Protected p_tickRate As Integer
    Protected p_turretCooldown As Integer
    Protected p_turretCooldownSeconds As Decimal

    Protected p_image As Image
    Protected p_size As Size
    Protected p_loc As Point
    Protected p_centreCoord As Point

    Protected p_xVel As Integer
    Protected p_yVel As Integer
    Protected p_movementVelocity As Decimal

    Protected p_collisionBox As Rectangle

    Public ReadOnly Property Image As Image
        Get
            Return p_image
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

    Sub New(spawnLocation As Point, tickRate As Integer, movementVelocity As Decimal)
        p_size = New Size(150, 150)
        p_loc = spawnLocation
        p_centreCoord = New Point(p_loc.X + p_size.Width / 2, p_loc.Y + p_size.Height / 2)

        p_ticksAlive = 0
        p_tickRate = tickRate
        p_turretCooldown = 1 * p_tickRate
        p_turretCooldownSeconds = 0

        p_movementVelocity = movementVelocity

        p_baseImage = My.Resources.BlankTankBase
        p_turretImage = My.Resources.BlankTankTurret


        CalculateTankImage(New Point(0, 0))
    End Sub

    Public Overridable Sub Tick()
        p_ticksAlive += 1
        If p_turretCooldown < 0 Then
            p_turretCooldown = 0
        End If
    End Sub

    Protected Overridable Sub MovePlayerTank(inputKeys() As Boolean, walls() As BasicWall)

    End Sub

    Protected Overridable Sub MoveEnemyTank(walls() As BasicWall)

    End Sub

    Protected Sub AcutallyMoveTheTank(walls() As BasicWall)
        Dim xDisplacement As Integer = p_xVel * p_movementVelocity * 60 / p_tickRate
        Dim yDisplacement As Integer = p_yVel * p_movementVelocity * 60 / p_tickRate
        Dim canMoveInX As Boolean = True
        Dim canMoveInY As Boolean = True

        For Each wall As BasicWall In walls
            'If Collision.CheckPointAgainstRectangle(p_centreCoord + New Point(xDisplacement, 0), wall.rect) Then
            '    canMoveInX = False
            'End If
            'If Collision.CheckPointAgainstRectangle(p_centreCoord + New Point(0, yDisplacement), wall.rect) Then
            '    canMoveInY = False
            'End If

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


    Public Sub CalculateTankImage(MouseCoords As Point)
        p_image = calculateTurret(calculateBase(), MouseCoords)
    End Sub

    Private Function calculateBase()
        Dim btmp1 As New Bitmap(151, 151)

        Using G = Graphics.FromImage(btmp1)
            G.TranslateTransform(btmp1.Width / 2, btmp1.Height / 2)
            G.RotateTransform(p_baseRotation)
            G.DrawImage(p_baseImage, New Point(-p_baseImage.Width / 2, -p_baseImage.Height / 2))
            G.TranslateTransform(-btmp1.Width / 2, -btmp1.Height / 2)
        End Using

        Return btmp1
    End Function

    Protected Overridable Function calculateTurret(btmp1 As Bitmap, MouseCoords As Point)
        Dim btmp2 As New Bitmap(151, 151)

        Using g = Graphics.FromImage(btmp2)
            g.DrawImage(btmp1, New Point(0, 0))
            g.TranslateTransform(btmp1.Width / 2, btmp1.Height / 2)
            g.DrawImage(p_turretImage, New Point(-p_turretImage.Width / 2, -p_turretImage.Height / 2))
            g.TranslateTransform(-btmp1.Width / 2, -btmp1.Height / 2)
        End Using

        Return btmp2
    End Function

    Public Function getImage(MouseCoords As Point) As Image
        CalculateTankImage(MouseCoords)
        Return p_image
    End Function

End Class
