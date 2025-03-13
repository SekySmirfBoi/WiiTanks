Public Class UIManager

    Private _components As New List(Of UIComponent)

    Public Sub AddComponent(component As UIComponent)
        _components.Add(component)
    End Sub

    Public Function removeCOmponent(comp As UIComponent)
        Return _components.Remove(comp)
    End Function

    Public Sub Render(graphics As Graphics)
        For Each curCom As UIComponent In _components
            curCom.Render(graphics)
        Next
    End Sub

    Public Function Click() As UIComponent
        For Each com As UIComponent In _components
            If com.Click(SharedResources.LastKnownMouseCoords) Then
                Return com
            End If
        Next

        Return SharedResources.EmptyComp
    End Function
End Class
