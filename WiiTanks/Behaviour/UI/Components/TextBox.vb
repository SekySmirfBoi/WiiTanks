Public Class TextBox
    Inherits UIComponent

    Private _text As String = ""
    Private _faintText As String

    Public ReadOnly Property Text As String
        Get
            Return _text
        End Get
    End Property

    Sub New(pos As Point, size As Size, Optional FaintText As String = "Type here")
        MyBase.New(pos, size)
        _faintText = FaintText
    End Sub

    Public Overrides Sub Render(graohics As Graphics)
        graohics.DrawRectangle(New Pen(Color.Gray), New Rectangle(p_loc, p_size))
        graohics.DrawString(If(_text.Length > 0, _text, _faintText), SharedResources.TXTBX_FONT, SharedResources.TextBrush.Brush, p_loc)
    End Sub

    Public Sub AddCar(car As Char)
        _text &= car
    End Sub

    Public Sub popCar()
        _text = _text.Substring(0, _text.Length - 1)
    End Sub
End Class
