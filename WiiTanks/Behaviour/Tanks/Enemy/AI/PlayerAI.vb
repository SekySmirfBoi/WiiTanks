Public Class PlayerAI
    Inherits BaseAI

    Sub New()
        MyBase.New(0, 4)
    End Sub

    Public Overrides Sub Tick()
        MyBase.Tick()
    End Sub

    Protected Overrides Function calculateTurret(btmp1 As Bitmap) As Object
        Dim btmp2 As New Bitmap(151, 151)
        Using g = Graphics.FromImage(btmp2)
            g.DrawImage(btmp1, New Point(0, 0))

            Dim dX As Integer = SharedResources.LastKnownMouseCoords.X - p_hose.CentreCood.X
            Dim dY As Integer = SharedResources.LastKnownMouseCoords.Y - p_hose.CentreCood.Y
            If dY <> 0 Then
                p_turretAngle = Math.Atan(dX / dY) * 180 / Math.PI
            Else
                p_turretAngle = If(dX = 0, 0, If(dX > 0, 90, 270))
            End If

            p_turretAngle = -p_turretAngle + If(SharedResources.LastKnownMouseCoords.Y > p_hose.CentreCood.Y, 180, 0)

            g.TranslateTransform(btmp1.Width / 2, btmp1.Height / 2)
            g.RotateTransform(p_turretAngle)
            g.DrawImage(p_hose.TurretImage, New Point(-p_hose.TurretImage.Width / 2, -p_hose.TurretImage.Height / 2))
            g.TranslateTransform(-btmp1.Width / 2, -btmp1.Height / 2)
        End Using

        Return btmp2
    End Function
End Class
