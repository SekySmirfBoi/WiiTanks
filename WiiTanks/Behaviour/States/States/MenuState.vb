Public Class MenuState
    Inherits State

    Private _uiManager As UIManager

    Public Overrides Sub Create()
        ' Backround needs making

        '

        _uiManager = New UIManager()

        _uiManager.AddComponent(New TextBlock(New Point(100, 100), "dick", ComponentResources.BlankImage))
        _uiManager.AddComponent(New Button(New Point(100, 200), "ilugylku", New Size(80, 40),
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
