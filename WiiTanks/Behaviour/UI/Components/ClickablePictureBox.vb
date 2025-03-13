Public Class ClickablePictureBox
    Inherits UIComponent

    Private _image As Image
    Private _extraData As String

    Public ReadOnly Property Data As String
        Get
            Return _extraData
        End Get
    End Property

    Public Sub New(position As Point, size As Size, startingImage As Image, Optional extra As String = "")
        MyBase.New(position, size)
        _image = startingImage
        _extraData = extra
    End Sub

    Public Overrides Sub Render(graohics As Graphics)
        graohics.DrawImage(_image, p_loc)
    End Sub

    Public Sub ChangeImage(newImage As Image)
        _image = newImage
    End Sub
End Class
