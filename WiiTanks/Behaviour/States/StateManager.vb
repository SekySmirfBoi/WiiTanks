Public Class StateManager

    Private _CurrentState As State

    Sub New(StartStart As State)
        _CurrentState = StartStart
    End Sub

    Public Sub Tick()
        _CurrentState.Tick()
    End Sub

    Public Sub Render(graphics As Graphics)
        _CurrentState.Render(graphics)
    End Sub

    Public Sub Click(MouseCoord As Point)
        _CurrentState.Click(MouseCoord)
    End Sub
End Class
