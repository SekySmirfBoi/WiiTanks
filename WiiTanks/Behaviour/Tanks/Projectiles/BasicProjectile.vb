Public Class BasicProjectile
    Protected p_velX As Integer
    Protected p_velY As Integer
    Private _speed As Decimal
    Protected p_angle As Integer

    Private _bounceBuffer As Integer = 0
    Private _bouncesLeft As Integer

    Protected p_loc As Point
    Protected p_centreCoords As Point

    Private _image As Image

    Public ReadOnly Property Image As Image
        Get
            Dim btmp As New Bitmap(_image.Height, _image.Height)
            Using g = Graphics.FromImage(btmp)
                g.TranslateTransform(btmp.Width / 2, btmp.Height / 2)
                g.RotateTransform(p_angle)
                g.DrawImage(_image, New Point(-_image.Width / 2, -_image.Height / 2))
                g.TranslateTransform(-btmp.Width / 2, -btmp.Height / 2)
            End Using

            Return btmp
        End Get
    End Property

    Public ReadOnly Property Location As Point
        Get
            Return p_loc
        End Get
    End Property

    Sub New(angle As Integer, centreloc As Point, speed As Decimal, bouncesLeft As Integer)
        _image = My.Resources.BasicProjectile

        p_centreCoords = centreloc
        p_loc = New Point(p_centreCoords.X - _image.Width / 2, p_centreCoords.Y - _image.Height / 2)

        p_angle = angle
        _speed = speed

        _bouncesLeft = bouncesLeft


        CalculateVelocities()
    End Sub

    Public Sub Tick()
        _bounceBuffer -= 1

        Dim xDisplacement As Integer = p_velX * If(p_velX <> 0 And p_velY <> 0, _speed / Math.Sqrt(2), _speed) * 60 / SharedResources.TickRate
        Dim yDisplacement As Integer = p_velY * If(p_velX <> 0 And p_velY <> 0, _speed / Math.Sqrt(2), _speed) * 60 / SharedResources.TickRate

        Dim newLoc As Point = New Point(p_loc.X + p_velX, p_loc.Y + p_velY)
        Dim newCentre As Point = newLoc + New Point(_image.Height / 2, _image.Width / 2)

        Dim colliSionPoint As New Point(newCentre.X + _speed * Math.Cos((Math.PI / 180) * (p_angle - 90)), newCentre.Y + _speed * Math.Sin((Math.PI / 180) * (p_angle - 90)))

        For Each wall As BasicWall In SharedResources.walls

            If Collision.CheckPointAgainstRectangle(colliSionPoint, wall.rect) Then
                Dim TopLeft As Point = New Point(wall.rect.Location.X, wall.rect.Location.Y)
                Dim TopRight As Point = New Point(wall.rect.Location.X + wall.rect.Width, wall.rect.Location.Y)
                Dim BottomLeft As Point = New Point(wall.rect.Location.X, wall.rect.Location.Y + wall.rect.Height)
                Dim BottomRight As Point = New Point(wall.rect.Location.X + wall.rect.Width, wall.rect.Location.Y + wall.rect.Height)

                Dim disTL As Decimal = Math.Sqrt((colliSionPoint.X - TopLeft.X) ^ 2 + (colliSionPoint.Y - TopLeft.Y) ^ 2)
                Dim disTR As Decimal = Math.Sqrt((colliSionPoint.X - TopRight.X) ^ 2 + (colliSionPoint.Y - TopRight.Y) ^ 2)
                Dim disBL As Decimal = Math.Sqrt((colliSionPoint.X - BottomLeft.X) ^ 2 + (colliSionPoint.Y - BottomLeft.Y) ^ 2)
                Dim disBR As Decimal = Math.Sqrt((colliSionPoint.X - BottomRight.X) ^ 2 + (colliSionPoint.Y - BottomRight.Y) ^ 2)

                If Not (disTL = disBR And disTR = disBL) Then
                    If disTL < disBR Then
                        If disTR < disBL Then
                            ' Hit top wall
                            p_angle = 540 - p_angle
                        Else
                            ' Hit left wall
                            p_angle = 360 - p_angle
                        End If
                    ElseIf disBR < disTL Then
                        If disTR < disBL Then
                            ' Hit right wall
                            p_angle = 360 - p_angle
                        Else
                            ' Hit bottom wall
                            p_angle = 540 - p_angle
                        End If
                    End If
                End If

                If _bounceBuffer <= 0 Then
                    _bouncesLeft -= 1
                    _bounceBuffer = SharedResources.TickRate / 10
                End If

                If _bouncesLeft < 0 Then
                    SharedResources.DestroyProjectile(Me)
                End If
            End If
        Next

        If SharedResources.playerTanksCount > 0 Then
            For Each player As Player In SharedResources.playerTanks
                If Collision.CheckPointAgainstRectangle(colliSionPoint, player.collBox) Then
                    SharedResources.killPlayer(player)
                    SharedResources.DestroyProjectile(Me)
                End If
            Next
        End If

        If SharedResources.enemyTanksCount > 0 Then
            For Each enemy As Bae In SharedResources.enemyTanks
                If Collision.CheckPointAgainstRectangle(colliSionPoint, enemy.collBox) Then
                    SharedResources.killEnemt(enemy)
                    SharedResources.DestroyProjectile(Me)
                End If
            Next
        End If

        CalculateVelocities()

        p_loc = New Point(p_loc.X + p_velX, p_loc.Y + p_velY)
        p_centreCoords = p_loc + New Point(_image.Height / 2, _image.Width / 2)

        If p_loc.X > SharedResources.WindowSize.Width Or
           p_loc.X < 0 Or
           p_loc.Y > SharedResources.WindowSize.Height Or
           p_loc.Y < 0 Then
            SharedResources.DestroyProjectile(Me)
        End If
    End Sub

    Protected Sub CalculateVelocities()
        p_velX = _speed * Math.Cos((Math.PI / 180) * (p_angle - 90))
        p_velY = _speed * Math.Sin((Math.PI / 180) * (p_angle - 90))
    End Sub
End Class
