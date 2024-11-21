Public Class TextBlock
    Inherits UIComponent

    Private _backImage As Image
    Private _text As String
    Private _font As Font

    Sub New(position As Point, text As String, backgournd As Image, fon As Font)
        MyBase.New(position)
        _text = text
        _backImage = backgournd
        _font = fon
    End Sub

    Public Overrides Sub Render(graohics As Graphics)
        graohics.DrawImage(_backImage, p_loc)
        graohics.DrawString(_text, _font, New Pen(Color.Gray).Brush, p_loc)
    End Sub

    Public Overrides Sub Click(MouseCords As Point)
    End Sub
End Class
