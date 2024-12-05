Public MustInherit Class BaseAI
    Protected p_hose As Bae
    Protected p_mineCooldown As Integer

    Public p_target As Decimal = 0
    Public p_turretAngle As Decimal = 0
    Protected p_turretTurnSpeed As Decimal = 2

    Public Sub assignCreature(Creature As Bae)
        p_hose = Creature
    End Sub

    Public MustOverride Sub Tick()

    Private Function calculateBase()

        p_hose.BaseRotiation = If(p_hose.yVel = -1 And p_hose.xVel = -1, 315,
                        If(p_hose.yVel = -1 And p_hose.xVel = 0, 0,
                        If(p_hose.yVel = -1 And p_hose.xVel = 1, 45,
                        If(p_hose.yVel = 0 And p_hose.xVel = -1, 270,
                        If(p_hose.yVel = 0 And p_hose.xVel = 0, p_hose.BaseRotiation,
                        If(p_hose.yVel = 0 And p_hose.xVel = 1, 90,
                        If(p_hose.yVel = 1 And p_hose.xVel = -1, 225,
                        If(p_hose.yVel = 1 And p_hose.xVel = 0, 180,
                        If(p_hose.yVel = 1 And p_hose.xVel = 1, 135, p_hose.BaseRotiation)))))))))

        Dim btmp1 As New Bitmap(151, 151)

        Using G = Graphics.FromImage(btmp1)
            G.TranslateTransform(btmp1.Width / 2, btmp1.Height / 2)
            G.RotateTransform(p_hose.BaseRotiation)
            G.DrawImage(p_hose.baseImage, New Point(-p_hose.baseImage.Width / 2, -p_hose.baseImage.Height / 2))
            G.TranslateTransform(-btmp1.Width / 2, -btmp1.Height / 2)
        End Using

        Return btmp1
    End Function

    Protected Overridable Function calculateTurret(btmp1 As Bitmap)
        Dim btmp2 As New Bitmap(151, 151)

        Using g = Graphics.FromImage(btmp2)
            g.DrawImage(btmp1, New Point(0, 0))
            g.TranslateTransform(btmp1.Width / 2, btmp1.Height / 2)
            g.DrawImage(p_hose.TurretImage, New Point(-p_hose.TurretImage.Width / 2, -p_hose.TurretImage.Height / 2))
            g.TranslateTransform(-btmp1.Width / 2, -btmp1.Height / 2)
        End Using

        Return btmp2
    End Function

    Protected Sub changeTurretAngle()
        If If(p_turretAngle > p_target, p_turretAngle - p_target, p_target - p_turretAngle) < p_turretTurnSpeed Then
            p_turretAngle = p_target
        Else
            If p_target <= 180 And p_turretAngle <= 180 Then
                p_turretAngle += If(p_target > p_turretAngle, p_turretTurnSpeed, -p_turretTurnSpeed)
            ElseIf p_target <= 180 And p_turretAngle >= 180 Then
                If p_turretAngle - p_target > 180 Then
                    p_turretAngle += p_turretTurnSpeed
                Else
                    p_turretAngle -= p_turretTurnSpeed
                End If
            ElseIf p_target >= 180 And p_turretAngle <= 180 Then
                If p_target - p_turretAngle > 180 Then
                    p_turretAngle -= p_turretTurnSpeed
                Else
                    p_turretAngle += p_turretTurnSpeed
                End If
            ElseIf p_target >= 180 And p_turretAngle >= 180 Then
                p_turretAngle += If(p_target > p_turretAngle, p_turretTurnSpeed, -p_turretTurnSpeed)
            End If
        End If


        If p_turretAngle > 360 Then
            p_turretAngle = 0
        End If
        If p_turretAngle < 0 Then
            p_turretAngle = 360 - p_turretTurnSpeed
        End If
    End Sub


    Public Function calculateImage()
        Return calculateTurret(calculateBase())
    End Function
End Class
