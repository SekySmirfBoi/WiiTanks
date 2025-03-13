Public Class EmptyComponent
    Inherits UIComponent
    Public Sub New()
        MyBase.New(New Point(0, 0), New Size(0, 0))
    End Sub

    Public Overrides Sub Render(graohics As Graphics)
        Throw New NotImplementedException()
    End Sub
End Class
