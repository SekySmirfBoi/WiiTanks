Public Class GameState
    Inherits State

    Private _level As Level
    Private _uiManager As UIManager

    Sub New(Level As Level)
        _uiManager = New UIManager
        _level = Level
        _level.Create(_uiManager)
    End Sub

    Public Overrides Sub Create()
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub Tick()
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub Render(graphics As Graphics)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub Click(MouseCoords As Point)
        Throw New NotImplementedException()
    End Sub
End Class
