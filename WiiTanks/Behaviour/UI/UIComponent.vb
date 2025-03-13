Imports System.Threading

Public MustInherit Class UIComponent
    Protected p_loc As Point
    Protected p_size As Size

    Public ReadOnly Property Location As Point
        Get
            Return p_loc
        End Get
    End Property

    Public ReadOnly Property Size As Size
        Get
            Return p_size
        End Get
    End Property

    Sub New(position As Point, size As Size)
        p_loc = position
        p_size = size
    End Sub

    Public MustOverride Sub Render(graohics As Graphics)

    Public Overridable Function Click(MouseCords As Point) As Boolean
        If MouseCords.X >= p_loc.X And MouseCords.X <= p_loc.X + p_size.Width And
                MouseCords.Y >= p_loc.Y And MouseCords.Y <= p_loc.Y + p_size.Height Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
