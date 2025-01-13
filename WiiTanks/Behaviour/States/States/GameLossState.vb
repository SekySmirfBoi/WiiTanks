Imports System.Threading

Public Class GameLossState
    Inherits State

    Private _level As String

    Public Sub New(level As String)
        MyBase.New()
        _level = level
    End Sub

    Public Overrides Sub Create()
        SharedResources.window.Size = SharedResources.WindowSize
        p_uiManager.AddComponent(New Button(SharedResources.CentreWindowCoord + New Point(-60, 0), "Try Again!", SharedResources.BtnSize,
                                            Function() As Boolean
                                                SharedResources.DeleteAll()
                                                SharedResources.stateManager.ChangeState(New GameState(_level))
                                            End Function))

        p_uiManager.AddComponent(New Button(SharedResources.CentreWindowCoord + New Point(-60, 50), "Main menu", SharedResources.BtnSize,
                                    Function() As Boolean
                                        SharedResources.DeleteAll()
                                        SharedResources.stateManager.ChangeState(New MenuState)
                                    End Function))
    End Sub

    Public Overrides Sub Tick()
        If SharedResources.playerTanksCount > 0 Then
            For Each pTank As Player In SharedResources.playerTanks
                SharedResources.createThread(New ThreadStart(AddressOf pTank.Tick))
                'pTank.Tick()
            Next
        End If

        If SharedResources.enemyTanksCount > 0 Then
            For Each eTank As Bae In SharedResources.enemyTanks
                SharedResources.createThread(New ThreadStart(AddressOf eTank.Tick))
                'eTank.Tick()
            Next
        End If

        If SharedResources.projectileCount > 0 Then
            For Each proj As BasicProjectile In SharedResources.projectiles
                SharedResources.createThread(New ThreadStart(AddressOf proj.Tick))
                'proj.Tick()
            Next
        End If

        If SharedResources.inputKeys.Count - 1 >= Keys.Escape Then
            If SharedResources.inputKeys(Keys.Escape) Then
                SharedResources.stateManager.ChangeState(New LevelSelectState())
            End If
        End If

        SharedResources.WaitForThreads()
    End Sub

    Public Overrides Sub Render(graphics As Graphics)
        If SharedResources.playerTanksCount > 0 Then
            For Each pTank As Player In SharedResources.playerTanks
                graphics.DrawImage(pTank.Image, pTank.Location)
                graphics.DrawRectangle(New Pen(Color.Blue, 3), New Rectangle(pTank.Location, pTank.Size))

                If SharedResources.inputKeys.Length - 1 >= Keys.P Then
                    If SharedResources.inputKeys(Keys.P) Then
                        graphics.DrawRectangle(New Pen(Color.Red, 3), pTank.collBox)
                    End If
                End If

                'graphics.DrawString("X:" & pTank.Location.X, SharedResources.DEFAULT_FONT, SharedResources.TextBrush.Brush, New Point(800, 200))
                'graphics.DrawString("Y:" & pTank.Location.Y, SharedResources.DEFAULT_FONT, SharedResources.TextBrush.Brush, New Point(800, 250))
            Next
        End If

        If SharedResources.enemyTanksCount > 0 Then
            For Each eTank As Bae In SharedResources.enemyTanks
                graphics.DrawImage(eTank.Image(), eTank.Location)
            Next
        End If

        If SharedResources.projectileCount > 0 Then
            For Each proj As BasicProjectile In SharedResources.projectiles
                graphics.DrawImage(proj.Image, proj.Location)
            Next
        End If

        For Each wall As BasicWall In SharedResources.walls
            graphics.DrawImage(wall.Image, wall.Location)
        Next


        p_uiManager.Render(graphics)
    End Sub

    Public Overrides Sub Click()
        p_uiManager.Click()
    End Sub
End Class
