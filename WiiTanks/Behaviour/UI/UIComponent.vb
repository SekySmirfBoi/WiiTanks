Public MustInherit Class UIComponent
    Private _loc As Point

    Public ReadOnly Property Location As Point
        Get
            Return _loc
        End Get
    End Property

    Sub New(position As Point)
        _loc = position
    End Sub

    Public MustOverride Sub Render(graohics As Graphics)
End Class
