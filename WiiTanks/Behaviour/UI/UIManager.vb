﻿Public Class UIManager

    Private _components As New List(Of UIComponent)

    Public Sub AddComponent(component As UIComponent)
        _components.Add(component)
    End Sub

    Public Sub Render(graphics As Graphics)
        For Each curCom As UIComponent In _components
            curCom.Render(graphics)
        Next
    End Sub

    Public Sub Click()
        For Each com As UIComponent In _components
            com.Click(SharedResources.LastKnownMouseCoords)
        Next
    End Sub
End Class
