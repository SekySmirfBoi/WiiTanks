﻿Public Class StateManager

    Private _CurrentState As State
    Sub New(StartStart As State)
        _CurrentState = StartStart
        _CurrentState.Create()
    End Sub

    Public Sub Tick()
        _CurrentState.Tick()
    End Sub

    Public Sub Render(graphics As Graphics)
        _CurrentState.Render(graphics)
    End Sub

    Public Sub ChangeState(newState As State)
        _CurrentState = newState
        _CurrentState.Create()
    End Sub

    Public Sub Click()
        _CurrentState.Click()
    End Sub
End Class
