Public Class PlayerAI
    Inherits BaseAI

    Sub New()
        MyBase.New(0)
    End Sub

    Public Overrides Sub Tick()
    End Sub

    Protected Overrides Function calculateTurret(btmp1 As Bitmap) As Object
        Dim btmp2 As New Bitmap(151, 151)
        Using g = Graphics.FromImage(btmp2)
            g.DrawImage(btmp1, New Point(0, 0))

            Dim myCentreX As Integer = btmp1.Width / 2 + p_hose.Location.X
            Dim myCentreY As Integer = btmp1.Height / 2 + p_hose.Location.Y
            Dim dX As Integer = SharedResources.LastKnownMouseCoords.X - myCentreX
            Dim dY As Integer = SharedResources.LastKnownMouseCoords.Y - myCentreY
            Dim angle As Decimal
            If dY <> 0 Then
                angle = Math.Atan(dX / dY) * 180 / Math.PI
            Else
                angle = If(dX = 0, 0, If(dX > 0, 90, 270))
            End If

            g.TranslateTransform(btmp1.Width / 2, btmp1.Height / 2)
            g.RotateTransform(-angle + If(SharedResources.LastKnownMouseCoords.Y > myCentreY, 180, 0))
            g.DrawImage(p_hose.TurretImage, New Point(-p_hose.TurretImage.Width / 2, -p_hose.TurretImage.Height / 2))
            g.TranslateTransform(-btmp1.Width / 2, -btmp1.Height / 2)
        End Using

        Return btmp2
    End Function
End Class
