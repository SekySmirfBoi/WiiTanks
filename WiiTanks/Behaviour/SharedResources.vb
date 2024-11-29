Public Class SharedResources
    Public Shared TileSize As Size = New Size(55, 55)
    Public Shared MapSize As Size = New Size(20, 15)
    Public Shared WindowSize As Size = New Size(TileSize.Width * (MapSize.Width + 2) + 17, TileSize.Height * (MapSize.Height + 2) + 40)
    Public Shared CentreWindowCoord As Point = New Point(WindowSize.Width / 2, WindowSize.Height / 2)
    Public Shared TextBrush As Pen = New Pen(Color.Black, 1)
    Public Shared BlankImage As Image = New Bitmap(1, 1)
    Public Shared BtnSize As Size = New Size(120, 40)
    Public Shared DEFAULT_FONT As Font = New Font("Ariel", 24)
    Public Shared TickRate As Integer = 60
    Public Shared LastKnownMouseCoords As Point = New Point(0, 0)
    Public Shared inputKeys() As Boolean
    Public Shared walls() As BasicWall
    Public Shared playerTanks() As Player
    Public Shared finishedLoadingMap As Boolean = False
End Class
