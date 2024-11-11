Public Class Button
    Inherits UIComponent

    Private _Text As String
    Private _task As Func(Of Boolean)

    Sub New(position As Point, text As String, task As Func(Of Boolean))
        MyBase.New(position)
        _Text = text
        _task = task
    End Sub

    Public Overrides Sub Render(graohics As graphics)

    End Sub

    Public Sub ExecuteTask()
        _task.Invoke()
    End Sub
End Class
