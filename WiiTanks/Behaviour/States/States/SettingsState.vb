Public Class SettingsState
    Inherits State
    Public Overrides Sub Create(parent As StateManager)
        p_father = parent
        p_uiManager = New UIManager()



        p_uiManager.AddComponent(New Button(New Point(SharedResources.CentreWindowCoord.X - 60, 150), "test", SharedResources.BtnSize,
                                            Function() As Boolean
                                                MsgBox("hey it wokrs")
                                            End Function))
        p_uiManager.AddComponent(New Button(New Point(SharedResources.CentreWindowCoord.X - 60, 20), "Back", SharedResources.BtnSize,
                                            Function() As Boolean
                                                p_father.ChangeState(New MenuState())
                                            End Function))
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
