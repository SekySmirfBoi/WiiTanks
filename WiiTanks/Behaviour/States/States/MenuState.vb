Public Class MenuState
    Inherits State

    Public Overrides Sub Create()

        ' Backround needs making

        '


        p_uiManager.AddComponent(New TextBlock(New Point(SharedResources.CentreWindowCoord.X - 30, 100), "title", SharedResources.BlankImage, SharedResources.DEFAULT_FONT))
        p_uiManager.AddComponent(New Button(New Point(SharedResources.CentreWindowCoord.X - 60, 150), "Start", SharedResources.BtnSize,
                                           Function() As Boolean
                                               SharedResources.stateManager.ChangeState(New LevelSelectState())
                                           End Function))
        p_uiManager.AddComponent(New Button(New Point(SharedResources.CentreWindowCoord.X - 60, 200), "Settings", SharedResources.BtnSize,
                                           Function() As Boolean
                                               SharedResources.stateManager.ChangeState(New SettingsState())
                                           End Function))
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
