Public MustInherit Class State

    Protected p_uiManager As UIManager
    Public MustOverride Sub Create()
    Public MustOverride Sub Tick()
    Public MustOverride Sub Render(graphics As Graphics)
    Public MustOverride Sub Click()
End Class
