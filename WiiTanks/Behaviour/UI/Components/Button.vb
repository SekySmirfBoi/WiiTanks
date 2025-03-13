Public Class Button
    Inherits UIComponent

    Private _Text As String
    Private _task As Func(Of Boolean)
    Private _extraData As Integer


    Sub New(position As Point, text As String, size As Size, task As Func(Of Boolean), Optional extraDAta As Integer = 0)
        MyBase.New(position, size)
        _Text = text
        _task = task
        _extraData = extraDAta
    End Sub

    Public Overrides Sub Render(graohics As Graphics)
        graohics.FillRectangle(New Pen(Color.Gray).Brush, New Rectangle(p_loc, p_size))
        graohics.DrawString(_Text, SharedResources.DEFAULT_FONT, New Pen(Color.Black).Brush, p_loc)
    End Sub

    Public Overrides Function Click(MouseCords As Point) As Boolean
        If MyBase.Click(MouseCords) Then
            _task.Invoke()
            Return True
        Else
            Return False
        End If
    End Function
End Class
