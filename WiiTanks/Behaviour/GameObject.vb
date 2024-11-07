Public MustInherit Class GameObject

    Private _loc As Point
    Private _xVel As Decimal
    Private _yVel As Decimal
    Private _ticksAlive As Integer
    Private _tickRate As Integer
    Private _image As Image

    Sub New(location As Point, xVel As Decimal, yVel As Decimal, tickrate As Integer, image As Image)
        _loc = location
        _xVel = xVel
        _yVel = yVel
        _tickRate = tickrate
        _ticksAlive = 0
        _image = image
    End Sub

    Public MustOverride Sub Tick()
End Class

