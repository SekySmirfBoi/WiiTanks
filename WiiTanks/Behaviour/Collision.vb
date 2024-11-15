Public Class Collision

    Public Shared Function CheckPointAgainstRectangle(coords As Point, rect As Rectangle)
        Return coords.X > rect.Location.X And coords.X < rect.Location.X + rect.Width And coords.Y > rect.Location.Y And coords.Y < rect.Location.Y + rect.Height
    End Function

    Public Shared Function CheckRectangleCollision(rect1 As Rectangle, rect2 As Rectangle)
        Return CheckPointAgainstRectangle(rect1.Location, rect2) Or CheckPointAgainstRectangle(rect1.Location + rect1.Size, rect2) Or CheckPointAgainstRectangle(New Point(rect1.X, rect1.Y + rect1.Height), rect2) Or CheckPointAgainstRectangle(New Point(rect1.X + rect1.Width, rect1.Y), rect2)
    End Function
End Class
