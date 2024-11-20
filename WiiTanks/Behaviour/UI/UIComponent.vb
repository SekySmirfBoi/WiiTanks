Public MustInherit Class UIComponent
    Protected p_loc As Point

    Public ReadOnly Property Location As Point
        Get
            Return p_loc
        End Get
    End Property

    Sub New(position As Point)
        p_loc = position
    End Sub

    Public MustOverride Sub Render(graohics As Graphics)

    Public MustOverride Sub Click(MouseCords As Point)
End Class
