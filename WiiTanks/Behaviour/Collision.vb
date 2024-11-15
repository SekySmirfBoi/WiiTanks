Public Class Collision

    Public Shared Function CheckPointAgainstRectangle(coords As Point, rect As Rectangle)
        Return coords.X >= rect.Location.X And coords.X <= rect.Location.X + rect.Width And coords.Y >= rect.Location.Y And coords.Y <= rect.Location.Y + rect.Height
    End Function
End Class
