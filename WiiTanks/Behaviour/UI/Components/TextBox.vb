Public Class TextBox
    Inherits UIComponent

    Private _text As String
    Private _faintText As String
    Private _size As Size

    Sub New(pos As Point, size As Size, Optional FaintText As String = "Type here")
        MyBase.New(pos)
        _faintText = FaintText
        _size = size
    End Sub

    Public Overrides Sub Render(graohics As Graphics)
        graohics.DrawRectangle(New Pen(Color.Gray), New Rectangle(p_loc, _size))
    End Sub

    Public Overrides Sub Click(MouseCords As Point)
    End Sub

    Public Sub AddCar(car As Char)
        _text &= car
    End Sub

    Public Sub popCar()
        _text = _text.Substring(0, _text.Length - 1)
    End Sub
End Class
