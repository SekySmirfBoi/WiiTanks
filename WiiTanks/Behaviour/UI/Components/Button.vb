Public Class Button
    Inherits UIComponent

    Private _Text As String
    Private _task As Func(Of Boolean)
    Private _size As Size


    Sub New(position As Point, text As String, size As Size, task As Func(Of Boolean))
        MyBase.New(position)
        _Text = text
        _task = task
        _size = size
    End Sub

    Public Overrides Sub Render(graohics As graphics)
        graohics.FillRectangle(New Pen(Color.Gray).Brush, New Rectangle(p_loc, _size))
        graohics.DrawString(_Text, SharedResources.DEFAULT_FONT, New Pen(Color.Black).Brush, p_loc)
    End Sub

    Public Overrides Sub Click(MouseCords As Point)
        If MouseCords.X >= p_loc.X And MouseCords.X <= p_loc.X + _size.Width And
                MouseCords.Y >= p_loc.Y And MouseCords.Y <= p_loc.Y + _size.Height Then
            _task.Invoke
        End If
    End Sub
End Class
