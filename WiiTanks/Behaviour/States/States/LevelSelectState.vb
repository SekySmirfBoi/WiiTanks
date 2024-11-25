Public Class LevelSelectState
    Inherits State

    Public Overrides Sub Create(parent As StateManager)
        p_father = parent
        p_uiManager = New UIManager


        p_uiManager.AddComponent(New Button(New Point(SharedResources.CentreWindowCoord.X - 60, 20), "Back", SharedResources.BtnSize,
                                            Function() As Boolean
                                                p_father.ChangeState(New MenuState())
                                            End Function))

        For y As Integer = 0 To 0       ' max is 5
                                                    For x As Integer = 0 To 0   ' max is 8
                                                        p_uiManager.AddComponent(New Button(New Point(x * 130 + 100, y * 130 + 100), "", New Size(SharedResources.BtnSize.Width, SharedResources.BtnSize.Width),
                                                                                    Function() As Boolean
                                                                                        p_father.ChangeState(New GameState(x + y * 8 + 1))
                                                                                    End Function))
                                                    Next
                                                Next

    End Sub

    Public Overrides Sub Tick()
    End Sub

    Public Overrides Sub Render(graphics As Graphics)
        p_uiManager.Render(graphics)
    End Sub

    Public Overrides Sub Click(MouseCoords As Point)
        p_uiManager.Click(MouseCoords)
    End Sub
End Class
