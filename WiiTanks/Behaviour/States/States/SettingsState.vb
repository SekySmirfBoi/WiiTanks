Public Class SettingsState
    Inherits State
    Public Overrides Sub Create()
        SharedResources.window.Size = SharedResources.WindowSize
        Dim numOfBtns As Integer = 2
        p_uiManager.AddComponent(New Button(SharedResources.CalculateBtnPos(1, numOfBtns), "test", SharedResources.BtnSize,
                                            Function() As Boolean
                                                MsgBox("hey it wokrs")
                                            End Function))
        p_uiManager.AddComponent(New Button(SharedResources.CalculateBtnPos(2, numOfBtns), "Back", SharedResources.BtnSize,
                                            Function() As Boolean
                                                SharedResources.stateManager.ChangeState(New MenuState())
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
