Public Class Bae


    Protected p_baseImage As Image
    Protected p_turretImage As Image
    Protected _turretRotation As Single = 0

    Protected _image As Image
    Protected _size As Size
    Protected _loc As Point
    Protected _centreCoord As Point

    Public ReadOnly Property Image As Image
        Get
            Return _image
        End Get
    End Property

    Public ReadOnly Property Size As Size
        Get
            Return _size
        End Get
    End Property

    Public ReadOnly Property Location As Point
        Get
            Return _loc
        End Get
    End Property

    Public ReadOnly Property CentreCood As Point
        Get
            Return _centreCoord
        End Get
    End Property

    Sub New()
        _size = New Size(150, 150)
        _loc = New Point(800, 450)

        p_baseImage = My.Resources.BlankTankBase
        p_turretImage = My.Resources.BlankTankTurret

        calculateTankImage(0, New Point(0, 0))
    End Sub

    Public Sub tick(MouseCoords As Point)
        calculateTankImage(0, MouseCoords)
        _centreCoord = New Point(_loc.X + _size.Width / 2, _loc.Y + _size.Height / 2)
    End Sub

    Public Sub calculateTankImage(rotationAmount As Single, MouseCoords As Point)
        _image = calculateTurret(calculateBase(rotationAmount), MouseCoords)
    End Sub

    Private Function calculateBase(rotation As Integer)
        Dim btmp1 As New Bitmap(151, 151)

        Using G = Graphics.FromImage(btmp1)
            G.TranslateTransform(btmp1.Width / 2, btmp1.Height / 2)
            G.RotateTransform(rotation)
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

    Public Function getImage() As Image
        Return _image
    End Function

End Class
