Public Class DestroyableWall
    Inherits BasicWall

    Sub New(Location As Point, size As Size)
        MyBase.New(Location, size)
        p_wallImage = My.Resources.DestroyableWall
    End Sub
End Class
