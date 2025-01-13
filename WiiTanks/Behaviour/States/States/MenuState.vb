Public Class MenuState
    Inherits State

    Public Overrides Sub Create()
        SharedResources.window.Size = SharedResources.WindowSize

        ' Backround needs making

        '


        p_uiManager.AddComponent(New TextBlock(New Point(SharedResources.CentreWindowCoord.X - 30, 100), "title", SharedResources.BlankImage, SharedResources.DEFAULT_FONT))
        Dim numOfBtns As Integer = 3
        p_uiManager.AddComponent(New Button(SharedResources.CalculateBtnPos(1, numOfBtns), "Start", SharedResources.BtnSize,
                                           Function() As Boolean
                                               SharedResources.stateManager.ChangeState(New LevelSelectState())
                                           End Function))
        p_uiManager.AddComponent(New Button(SharedResources.CalculateBtnPos(2, numOfBtns), "Settings", SharedResources.BtnSize,
                                           Function() As Boolean
                                               SharedResources.stateManager.ChangeState(New SettingsState())
                                           End Function))
        p_uiManager.AddComponent(New Button(SharedResources.CalculateBtnPos(3, numOfBtns), "Exit", SharedResources.BtnSize,
                                   Function() As Boolean
                                       SharedResources.window.Close()
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
