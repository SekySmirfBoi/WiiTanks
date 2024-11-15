Public Class BasicWall

    Protected p_wallImage As Image
    Protected p_loc As Point
    Protected p_size As Size

    Public ReadOnly Property rect As Rectangle
        Get
            Return New Rectangle(p_loc, p_size)
        End Get
    End Property

    Public ReadOnly Property Location As Point
        Get
            Return p_loc
        End Get
    End Property

    Public ReadOnly Property Image As Image
        Get
            Return p_wallImage
        End Get
    End Property

    Public ReadOnly Property Size As Size
        Get
            Return p_size
        End Get
    End Property

    Sub New(Locationa As Point, size As Size)
        p_loc = Locationa
        p_wallImage = My.Resources.BasicWall
        p_size = size
    End Sub
End Class
