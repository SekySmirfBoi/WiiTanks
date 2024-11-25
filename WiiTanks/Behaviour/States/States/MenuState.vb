Public Class MenuState
    Inherits State

    Public Overrides Sub Create(parent As StateManager)
        p_father = parent

        ' Backround needs making

        '

        p_uiManager = New UIManager()

        p_uiManager.AddComponent(New TextBlock(New Point(SharedResources.CentreWindowCoord.X - 30, 100), "title", SharedResources.BlankImage, SharedResources.DEFAULT_FONT))
        p_uiManager.AddComponent(New Button(New Point(SharedResources.CentreWindowCoord.X - 60, 150), "Start", SharedResources.BtnSize,
                                           Function() As Boolean
                                               p_father.ChangeState(New LevelSelectState())
                                           End Function))
        p_uiManager.AddComponent(New Button(New Point(SharedResources.CentreWindowCoord.X - 60, 200), "Settings", SharedResources.BtnSize,
                                           Function() As Boolean
                                               p_father.ChangeState(New SettingsState())
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
