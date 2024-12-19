Imports System.Net

Public Class BasicProjectile
    Protected p_velX As Integer
    Protected p_velY As Integer
    Private _speed As Decimal
    Protected p_angle As Integer

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

    Sub New(angle As Integer, centreloc As Point, speed As Decimal)
        _image = My.Resources.BasicProjectile

        p_centreCoords = centreloc
        p_loc = New Point(p_centreCoords.X - _image.Width / 2, p_centreCoords.Y - _image.Height / 2)

        p_angle = angle
        _speed = speed
        'p_loc = Loc()


        CalculateVelocities()
    End Sub

    Public Sub Tick()
        Dim xDisplacement As Integer = p_velX * If(p_velX <> 0 And p_velY <> 0, _speed / Math.Sqrt(2), _speed) * 60 / SharedResources.TickRate
        Dim yDisplacement As Integer = p_velY * If(p_velX <> 0 And p_velY <> 0, _speed / Math.Sqrt(2), _speed) * 60 / SharedResources.TickRate

        For Each wall As BasicWall In SharedResources.walls

        Next

        CalculateVelocities()

        p_loc = New Point(p_loc.X + p_velX, p_loc.Y + p_velY)
    End Sub

    Protected Sub CalculateVelocities()
        p_velX = _speed * Math.Cos((Math.PI / 180) * (p_angle - 90))
        p_velY = _speed * Math.Sin((Math.PI / 180) * (p_angle - 90))
    End Sub
End Class
