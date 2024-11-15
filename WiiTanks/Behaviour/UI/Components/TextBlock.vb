Imports System.CodeDom

Public Class TextBlock
    Inherits UIComponent

    Sub New(position As Point, text As String, backgournd As Image)
        MyBase.New(position)
    End Sub

    Public Overrides Sub Render(graohics As Graphics)
        Throw New NotImplementedException()
    End Sub
End Class
