Public MustInherit Class State
    Public MustOverride Sub Create()
    Public MustOverride Sub Tick()
    Public MustOverride Sub Render(graphics As Graphics)
    Public MustOverride Sub Click(MouseCoords As Point)
    'Public MustOverride Sub Create()
End Class
