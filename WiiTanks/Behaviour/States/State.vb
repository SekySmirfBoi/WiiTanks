Public MustInherit Class State

    Protected p_uiManager As UIManager

    Public Sub New()
        p_uiManager = New UIManager()
    End Sub

    Public MustOverride Sub Create()
    Public MustOverride Sub Tick()
    Public MustOverride Sub Render(graphics As Graphics)
    Public MustOverride Sub Click()
End Class
