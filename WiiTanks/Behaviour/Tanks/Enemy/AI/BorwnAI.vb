Public Class BorwnAI
    Inherits BaseAI

    Sub New(Creature As Bae)
        MyBase.New(Creature)
    End Sub

    Public Overrides Sub Tick()
        Dim btmp2 As New Bitmap(151, 151)

        Using g = Graphics.FromImage(btmp2)
            g.DrawImage(btmp1, New Point(0, 0))

            If SharedResources.finishedLoadingMap Then
                Dim myCentreX As Integer = btmp1.Width / 2 + p_hose.Location.X
                Dim myCentreY As Integer = btmp1.Height / 2 + p_hose.Location.Y
                Dim dx As Integer = 0
                Dim dy As Integer = 0
                dx = SharedResources.playerTanks(0).CentreCood.X - myCentreX
                dy = SharedResources.playerTanks(0).CentreCood.Y - myCentreY
                Dim angle As Decimal
                If dy <> 0 Then
                    angle = Math.Atan(dx / dy) * 180 / Math.PI
                Else
                    angle = If(dx = 0, 0, If(dx > 0, 90, 270))
                End If

                g.TranslateTransform(btmp1.Width / 2, btmp1.Height / 2)
                g.RotateTransform(-angle + If(SharedResources.playerTanks(0).CentreCood.Y > myCentreY, 180, 0))
                g.DrawImage(p_hose.TurretImage, New Point(-p_hose.TurretImage.Width / 2, -p_turretImage.Height / 2))
                g.TranslateTransform(-btmp1.Width / 2, -btmp1.Height / 2)
            End If
        End Using
    End Sub
End Class
