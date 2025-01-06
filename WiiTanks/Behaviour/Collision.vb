Public Class Collision

    Public Shared Function CheckPointAgainstRectangle(coords As Point, rect As Rectangle)
        Return coords.X > rect.Location.X And coords.X < rect.Location.X + rect.Width And coords.Y > rect.Location.Y And coords.Y < rect.Location.Y + rect.Height
    End Function

    Public Shared Function CheckRectangleCollision(rect1 As Rectangle, rect2 As Rectangle)
        Return CheckPointAgainstRectangle(rect1.Location, rect2) Or CheckPointAgainstRectangle(rect1.Location + rect1.Size, rect2) Or CheckPointAgainstRectangle(New Point(rect1.X, rect1.Y + rect1.Height), rect2) Or CheckPointAgainstRectangle(New Point(rect1.X + rect1.Width, rect1.Y), rect2)
    End Function

    Public Shared Function CheckCircleInLine(cirlceCentre As Point, radius As Integer, from As Point, [to] As Point, depth As Integer)
        Dim curStart As Point = from
        Dim curEnd As Point = [to]

        If (curStart.X - cirlceCentre.X) ^ 2 + (curStart.Y - cirlceCentre.Y) ^ 2 < radius ^ 2 Or
           (curEnd.X - cirlceCentre.X) ^ 2 + (curEnd.Y - cirlceCentre.Y) ^ 2 < radius ^ 2 Then
            Return True
        ElseIf depth > 0 Then
            Dim changeInX As Integer = curEnd.X - curStart.X
            Dim changeInY As Integer = curEnd.Y - curStart.Y
            Return CheckCircleInLine(cirlceCentre, radius, curStart, curEnd - New Point(changeInX / 2, changeInY / 2), depth - 1) Or
                CheckCircleInLine(cirlceCentre, radius, curStart + New Point(changeInX / 2, changeInY / 2), curEnd, depth - 1)
        End If

        Return false
    End Function

    Public Shared Function CheckCircleInRect(circleCentre As Point, radius As Integer, rect As Rectangle)
        Dim TopLeft As Point = New Point(rect.Location.X, rect.Location.Y)
        Dim TopRight As Point = New Point(rect.Location.X + rect.Width, rect.Location.Y)
        Dim BottomLeft As Point = New Point(rect.Location.X, rect.Location.Y + rect.Height)
        Dim BottomRight As Point = New Point(rect.Location.X + rect.Width, rect.Location.Y + rect.Height)

        Return CheckCircleInLine(circleCentre, radius, TopLeft, TopRight, 5) Or
            CheckCircleInLine(circleCentre, radius, BottomLeft, BottomRight, 5) Or
            CheckCircleInLine(circleCentre, radius, TopLeft, BottomLeft, 5) Or
            CheckCircleInLine(circleCentre, radius, TopRight, BottomRight, 5)
    End Function
End Class
