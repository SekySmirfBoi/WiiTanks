Public Class Brown
    Inherits Bae

    Sub New(spawnLocation As Point)
        MyBase.New(spawnLocation, 0)
        p_baseImage = My.Resources.BrownTankBase
        p_turretImage = My.Resources.BrownTankTurret
    End Sub

    Public Overrides Sub Tick()
        MyBase.Tick()
    End Sub

    Protected Overrides Sub MovePlayerTank()
        MyBase.MovePlayerTank()
    End Sub

    Protected Overrides Sub MoveEnemyTank()
        MyBase.MoveEnemyTank()
    End Sub

    Protected Overrides Function calculateTurret(btmp1 As Bitmap) As Object
        Dim btmp2 As New Bitmap(151, 151)

        Using g = Graphics.FromImage(btmp2)
            g.DrawImage(btmp1, New Point(0, 0))

            If SharedResources.finishedLoadingMap Then
                Dim myCentreX As Integer = btmp1.Width / 2 + p_loc.X
                Dim myCentreY As Integer = btmp1.Height / 2 + p_loc.Y
                Dim dx As Integer = 0
                Dim dy As Integer = 0
                dx = SharedResources.playerTanks(0).CentreCood.X - myCentreX
                dy = SharedResources.playerTanks(0).CentreCood.Y - myCentreY
                Dim angle As Decimal
                If dY <> 0 Then
                    angle = Math.Atan(dX / dY) * 180 / Math.PI
                Else
                    angle = If(dX = 0, 0, If(dX > 0, 90, 270))
                End If

                g.TranslateTransform(btmp1.Width / 2, btmp1.Height / 2)
                g.RotateTransform(-angle + If(SharedResources.playerTanks(0).CentreCood.Y > myCentreY, 180, 0))
                g.DrawImage(p_turretImage, New Point(-p_turretImage.Width / 2, -p_turretImage.Height / 2))
                g.TranslateTransform(-btmp1.Width / 2, -btmp1.Height / 2)
            End If
        End Using

        Return btmp2
    End Function
End Class
