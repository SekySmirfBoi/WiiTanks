Public Class UIManager

    Private _components As New List(Of UIComponent)

    Public Sub AddComponent(component As UIComponent)
        _components.Add(component)
    End Sub

    Public Sub Render(graphics As Graphics)
        For Each curCom As UIComponent In _components
            ' cum
        Next
    End Sub
End Class
