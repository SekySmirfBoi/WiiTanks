Public Class Player
    Inherits Bae

    Private _KeyDirection As New Dictionary(Of Keys, Func(Of Boolean))
    Private _MineCooldown As Integer = 0

    Sub New(spawnLocation As Point)
        MyBase.New(spawnLocation, 5)
        p_baseImage = My.Resources.BlueTankBase
        p_turretImage = My.Resources.BlueTankTurret
        _MineCooldown = SharedResources.TickRate
    End Sub

    Public Overrides Sub Tick()
        MyBase.Tick()
        If _MineCooldown > 0 Then
            _MineCooldown -= 1
        End If

        MovePlayerTank()
        p_centreCoord = New Point(p_loc.X + p_size.Width / 2, p_loc.Y + p_size.Height / 2)
    End Sub

    Protected Overrides Sub MovePlayerTank()
        p_xVel = 0
        p_yVel = 0

        For Each rank In _KeyDirection
            If SharedResources.inputKeys.Count - 1 >= rank.Key Then
                If SharedResources.inputKeys(rank.Key) Then
                    rank.Value.Invoke()
                End If
            End If
        Next

        p_baseRotation = If(p_yVel = -1 And p_xVel = -1, 315,
                        If(p_yVel = -1 And p_xVel = 0, 0,
                        If(p_yVel = -1 And p_xVel = 1, 45,
                        If(p_yVel = 0 And p_xVel = -1, 270,
                        If(p_yVel = 0 And p_xVel = 0, p_baseRotation,
                        If(p_yVel = 0 And p_xVel = 1, 90,
                        If(p_yVel = 1 And p_xVel = -1, 225,
                        If(p_yVel = 1 And p_xVel = 0, 180,
                        If(p_yVel = 1 And p_xVel = 1, 135, p_baseRotation)))))))))

        MyBase.AcutallyMoveTheTank()
    End Sub

    Public Sub AssociateKey(inputKey As Keys, direction() As Integer)
        _KeyDirection.Add(inputKey, Function() As Boolean
                                        p_xVel += direction(0)
                                        p_yVel += direction(1)
                                        Return True
                                    End Function)
    End Sub

    Public Sub DisassociateKey(inputKey As Keys)
        _KeyDirection.Remove(inputKey)
    End Sub

    Protected Overrides Function calculateTurret(btmp1 As Bitmap) As Object
        Dim btmp2 As New Bitmap(151, 151)
        Using g = Graphics.FromImage(btmp2)
            g.DrawImage(btmp1, New Point(0, 0))

            Dim myCentreX As Integer = btmp1.Width / 2 + p_loc.X
            Dim myCentreY As Integer = btmp1.Height / 2 + p_loc.Y
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
            g.DrawImage(p_turretImage, New Point(-p_turretImage.Width / 2, -p_turretImage.Height / 2))
            g.TranslateTransform(-btmp1.Width / 2, -btmp1.Height / 2)
        End Using

        Return btmp2
    End Function
End Class
