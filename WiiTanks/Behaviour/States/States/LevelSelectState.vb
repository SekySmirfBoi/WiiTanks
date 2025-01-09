Public Class LevelSelectState
    Inherits State

    Public Overrides Sub Create()

        p_uiManager.AddComponent(New Button(New Point(SharedResources.CentreWindowCoord.X - 60, 20), "Back", SharedResources.BtnSize,
                                            Function() As Boolean
                                                SharedResources.stateManager.ChangeState(New MenuState())
                                            End Function))

        For y As Integer = 0 To (SharedResources.NumberOfLevels - 1) \ 8               ' max is 5
            For x As Integer = 0 To If(y = (SharedResources.NumberOfLevels - 1) \ 8, SharedResources.NumberOfLevels - y * 8, If(SharedResources.NumberOfLevels > 8, 8, SharedResources.NumberOfLevels)) - 1      ' max is 8

                Dim tempInt As Integer = x + y * 8 + 1
                p_uiManager.AddComponent(New Button(New Point(x * 130 + 100, y * 130 + 100), tempInt,
                                                    New Size(SharedResources.BtnSize.Width, SharedResources.BtnSize.Width),
                                                    Function() As Boolean
                                                        SharedResources.stateManager.ChangeState(New GameState(tempInt))
                                                    End Function))
            Next
        Next

    End Sub

    Public Overrides Sub Tick()
    End Sub

    Public Overrides Sub Render(graphics As Graphics)
        p_uiManager.Render(graphics)
    End Sub

    Public Overrides Sub Click()
        p_uiManager.Click()
    End Sub
End Class
