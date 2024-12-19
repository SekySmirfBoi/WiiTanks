Public Class Player
    Inherits Bae

    Private _KeyDirection As New Dictionary(Of Keys, Func(Of Boolean))
    Private _MineCooldown As Integer = 0

    Sub New(spawnLocation As Point)
        MyBase.New(spawnLocation, 5, New PlayerAI, ProjectileTypes.BASIC)
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

    Public Sub Shoot()
        p_AI.Shoot()
    End Sub
End Class
