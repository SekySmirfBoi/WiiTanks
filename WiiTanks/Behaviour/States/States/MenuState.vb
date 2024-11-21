Public Class MenuState
    Inherits State

    Private _uiManager As UIManager

    Public Overrides Sub Create()
        ' Backround needs making

        '

        _uiManager = New UIManager()

        _uiManager.AddComponent(New TextBlock(New Point(SharedResources.CentreWindowCoord.X - 30, 100), "dick", SharedResources.BlankImage))
        _uiManager.AddComponent(New Button(New Point(SharedResources.CentreWindowCoord.X - 40, 150), "ilugylku", New Size(80, 40),
                                           Function() As Boolean
                                               MsgBox("fuck you")
                                           End Function))
    End Sub

    Public Overrides Sub Tick()

    End Sub

    Public Overrides Sub Render(graphics As Graphics)
        _uiManager.Render(graphics)
    End Sub

    Public Overrides Sub Click(MouseCoords As Point)
        _uiManager.Click(MouseCoords)
    End Sub
End Class
