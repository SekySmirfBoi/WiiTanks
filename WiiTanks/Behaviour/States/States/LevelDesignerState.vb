Public Class LevelDesignerState
    Inherits State

    Private _selectedComp As UIComponent

    Public Overrides Sub Create()
        SharedResources.window.Size = SharedResources.WindowSize + New Size(400, 0)

        p_uiManager.AddComponent(New Button(New Point(SharedResources.WindowSize.Width + 100, 20), "Confirm", SharedResources.BtnSize,
                                            Function() As Boolean

                                            End Function))
        p_uiManager.AddComponent(New TextBox(New Point(100, 100), New Size(100, 20)))
    End Sub

    Public Overrides Sub Tick()
    End Sub

    Public Overrides Sub Click()

    End Sub
End Class
