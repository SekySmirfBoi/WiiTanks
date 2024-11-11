Public Class StateManager

    Private _CurrentState As State

    Sub New(StartStart As State)
        _CurrentState = StartStart
    End Sub

    Public Sub Tick()
        _CurrentState.Tick()
    End Sub
End Class
