Public MustInherit Class State

    Protected p_father As StateManager
    Protected p_uiManager As UIManager
    Public MustOverride Sub Create(parent As StateManager)
    Public MustOverride Sub Tick()
    Public MustOverride Sub Render(graphics As Graphics)
    Public MustOverride Sub Click(MouseCoords As Point)
End Class
