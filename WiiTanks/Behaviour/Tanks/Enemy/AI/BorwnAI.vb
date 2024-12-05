Public Class BorwnAI
    Inherits BaseAI

    Public Overrides Sub Tick()
        If p_turretAngle = p_target Then
            Dim rng As New Random
            p_target = rng.Next(0, 360)
        End If

        changeTurretAngle()
    End Sub

    Protected Overrides Function calculateTurret(btmp1 As Bitmap)
        Dim btmp2 As New Bitmap(151, 151)

        Using g = Graphics.FromImage(btmp2)
            g.DrawImage(btmp1, New Point(0, 0))

            If SharedResources.finishedLoadingMap Then
                g.DrawString(p_target, New Font("Ariel", 13), SharedResources.TextBrush.Brush, New Point(0, 0))
                g.DrawString(p_turretAngle, New Font("Ariel", 13), SharedResources.TextBrush.Brush, New Point(0, 50))

                g.TranslateTransform(btmp1.Width / 2, btmp1.Height / 2)
                g.RotateTransform(p_turretAngle)
                g.DrawImage(p_hose.TurretImage, New Point(-p_hose.TurretImage.Width / 2, -p_hose.Size.Height / 2))
                g.TranslateTransform(-btmp1.Width / 2, -btmp1.Height / 2)
            End If
        End Using

        Return btmp2
    End Function
End Class
