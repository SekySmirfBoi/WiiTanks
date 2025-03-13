Public Class GreenAI
    Inherits BaseAI

    Private _directShots As New List(Of Integer)
    Private _targetAcquired As Boolean = False

    Public Sub New(turretVelocity As Integer)
        MyBase.New(turretVelocity, 2)
    End Sub

    Public Overrides Sub Tick()
        MyBase.Tick()

        For i As Integer = 0 To 359
            If SharedResources.playerTanksCount > 0 Then
                For Each play As Player In SharedResources.playerTanks
                    Dim speed As Decimal = 2 * Math.Sqrt(18)
                    Dim xVel As Decimal = speed * Math.Cos((Math.PI / 180) * (i - 90))
                    Dim yVel As Decimal = speed * Math.Sin((Math.PI / 180) * (i - 90))

                    Dim xDisplace As Decimal = 76 * Math.Cos((Math.PI / 180) * (i - 90))
                    Dim yDisplace As Decimal = 76 * Math.Sin((Math.PI / 180) * (i - 90))
                    Dim spawnCoord As Point = New Point(p_hose.CentreCood.X + xDisplace, p_hose.CentreCood.Y + yDisplace)

                    If Collision.willPathIntersectWithRect(spawnCoord, xVel, yVel, play.collBox) Then
                        _directShots.Add(i)
                    End If
                Next
            End If
        Next

        If _directShots.Count > 0 Then
            If Not _directShots.Contains(p_target) Then
                p_target = _directShots(SharedResources.RNG.Next(0, _directShots.Count))
            End If
        End If

        If p_turretAngle = p_target Then
            If SharedResources.projectileCount < 2 And _targetAcquired Then
                Shoot()
            End If
        End If
        changeTurretAngle()
    End Sub
End Class
