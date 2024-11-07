Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging

Public Class Bae
    Inherits PictureBox

    Protected p_baseImage As Image
    Protected p_turretImage As Image
    Private _turretRotation As Single = 0

    Sub New()
        Me.Size = New Size(150, 150)
        Me.Location = New Point(960, 540)

        p_baseImage = My.Resources.BlankTankBase
        p_turretImage = My.Resources.BlankTankTurret

        calculateTankImage(0, New Point(0, 0))
    End Sub

    Public Sub tick(MouseCoords As Point)
        calculateTankImage(0, MouseCoords)
    End Sub

    Public Sub calculateTankImage(rotationAmount As Single, MouseCoords As Point)
        Dim btmp1 As New Bitmap(151, 151)

        Using G = Graphics.FromImage(btmp1)
            G.TranslateTransform(btmp1.Width / 2, btmp1.Height / 2)
            G.RotateTransform(rotationAmount)
            G.DrawImage(p_baseImage, New Point(-p_baseImage.Width / 2, -p_baseImage.Height / 2))
            'G.DrawImage(p_baseImage, New Point(0, 0))
            G.TranslateTransform(-btmp1.Width / 2, -btmp1.Height / 2)
        End Using

        Dim btmp2 As New Bitmap(151, 151)
        Using g = Graphics.FromImage(btmp2)
            g.DrawImage(btmp1, New Point(0, 0))

            Dim myCentreX As Integer = btmp1.Width / 2 + Me.Location.X
            Dim myCentreY As Integer = btmp1.Height / 2 + Me.Location.Y
            Dim dX As Integer = MouseCoords.X - myCentreX
            Dim dY As Integer = MouseCoords.Y - myCentreY
            Dim angle As Decimal = Math.Atan(dX / dY) * 180 / Math.PI

            g.TranslateTransform(btmp1.Width / 2, btmp1.Height / 2)
            g.RotateTransform(-angle + If(MouseCoords.Y > myCentreY, 180, 0))
            g.DrawImage(p_turretImage, New Point(-p_turretImage.Width / 2, -p_turretImage.Height / 2 + 10))
            g.TranslateTransform(-btmp1.Width / 2, -btmp1.Height / 2)
            'g.DrawImage(p_turretImage, New Point(0, 0))
        End Using

        Me.Image = btmp2
    End Sub
End Class
