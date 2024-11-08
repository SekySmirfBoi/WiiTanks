Public Class Player
    Inherits Bae

    Sub New()
        MyBase.New()
        _loc = New Point(400, 200)
        p_baseImage = My.Resources.BlueTankBase
        p_turretImage = My.Resources.BlueTankTurret
    End Sub

    Protected Overrides Function calculateTurret(btmp1 As Bitmap, MouseCoords As Point) As Object
        Dim btmp2 As New Bitmap(151, 151)
        Using g = Graphics.FromImage(btmp2)
            g.DrawImage(btmp1, New Point(0, 0))

            Dim myCentreX As Integer = btmp1.Width / 2 + _loc.X
            Dim myCentreY As Integer = btmp1.Height / 2 + _loc.Y
            Dim dX As Integer = MouseCoords.X - myCentreX
            Dim dY As Integer = MouseCoords.Y - myCentreY
            Dim angle As Decimal = Math.Atan(dX / dY) * 180 / Math.PI

            g.TranslateTransform(btmp1.Width / 2, btmp1.Height / 2)
            g.RotateTransform(-angle + If(MouseCoords.Y > myCentreY, 180, 0))
            g.DrawImage(p_turretImage, New Point(-p_turretImage.Width / 2, -p_turretImage.Height / 2))
            g.TranslateTransform(-btmp1.Width / 2, -btmp1.Height / 2)
        End Using

        Return btmp2
    End Function
End Class
