Imports System.IO

Public Class LevelDesignerState
    Inherits State

    Private _selectedComp As UIComponent = SharedResources.EmptyComp
    Private _textBox As TextBox
    Private _selectedTextBox As Boolean = False

    Private _selectedImage As String = GameMapTiles.BLANK

    Private _TypeImageDick As New Dictionary(Of String, Image)

    Private _SelectGrid(SharedResources.MapSize.Width, SharedResources.MapSize.Height) As ClickablePictureBox
    Private _StringGrid(SharedResources.MapSize.Width, SharedResources.MapSize.Height) As String

    Private _SampleBoxes As New List(Of ClickablePictureBox)

    Private Sub CreateWall(Location As Point)
        Dim wall As New BasicWall(Location, SharedResources.TileSize)
        ReDim Preserve SharedResources.walls(SharedResources.wallCount)
        SharedResources.walls(SharedResources.wallCount) = wall
        SharedResources.wallCount += 1
    End Sub

    Private Sub fixDick()
        _TypeImageDick.Add(GameMapTiles.BLANK, New Bitmap(1, 1))
        _TypeImageDick.Add(GameMapTiles.WALL, My.Resources.BasicWall)
        _TypeImageDick.Add(GameMapTiles.DESTROYABLE_WALL, My.Resources.DestroyableWall)
        _TypeImageDick.Add(GameMapTiles.PLAYER_SPAWN, My.Resources.BlueTankBase)
        _TypeImageDick.Add(GameMapTiles.BROWN_SPAWN, My.Resources.BrownTankBase)




        _TypeImageDick.Add(GameMapTiles.GREEN_SPAWN, My.Resources.GreenTankBase)
    End Sub

    Public Overrides Sub Create()
        SharedResources.ChantWindowSize(New Size(SharedResources.WindowSize.Width + 400, SharedResources.WindowSize.Height))
        p_uiManager.AddComponent(New Button(New Point(SharedResources.WindowSize.Width + 100, 20), "Confirm", SharedResources.BtnSize,
                                            Function() As Boolean
                                                If _textBox.Text = "" Then
                                                    MsgBox("Invalid file name")
                                                    Return True
                                                End If

                                                Dim writer As New StreamWriter(_textBox.Text & ".txt")

                                                For y As Integer = 1 To SharedResources.MapSize.Height
                                                    For x As Integer = 1 To SharedResources.MapSize.Width
                                                        writer.Write(_StringGrid(x, y) & If(x <> SharedResources.MapSize.Width, ",", ""))
                                                    Next
                                                    writer.WriteLine()
                                                Next

                                                writer.Close()

                                                SharedResources.stateManager.ChangeState(New MenuState())
                                            End Function))

        p_uiManager.AddComponent(New Button(New Point(SharedResources.WindowSize.Width + 200, SharedResources.WindowSize.Height - 100), "Clear", SharedResources.BtnSize,
                                            Function()
                                                For y As Integer = 1 To SharedResources.MapSize.Height
                                                    For x As Integer = 1 To SharedResources.MapSize.Width
                                                        p_uiManager.removeCOmponent(_SelectGrid(x, y))

                                                        _StringGrid(x, y) = GameMapTiles.BLANK

                                                        Dim curPicBox As New ClickablePictureBox(New Point(SharedResources.TileSize.Width * x, SharedResources.TileSize.Height * y), SharedResources.TileSize, _TypeImageDick(GameMapTiles.BLANK))
                                                        p_uiManager.AddComponent(curPicBox)
                                                        _SelectGrid(x, y) = curPicBox
                                                    Next
                                                Next
                                            End Function))

        _textBox = New TextBox(New Point(SharedResources.WindowSize.Width + 75, 100), New Size(200, 20), "File name")
        p_uiManager.AddComponent(_textBox)

        For i As Integer = 0 To SharedResources.MapSize.Width + 1
            CreateWall(New Point(i * SharedResources.TileSize.Width, 0))
            CreateWall(New Point(i * SharedResources.TileSize.Width, SharedResources.TileSize.Height * (SharedResources.MapSize.Height + 1)))
        Next
        For i As Integer = 1 To SharedResources.MapSize.Height
            CreateWall(New Point(0, i * SharedResources.TileSize.Height))
            CreateWall(New Point(SharedResources.TileSize.Width * (SharedResources.MapSize.Width + 1), i * SharedResources.TileSize.Height))
        Next
        fixDick()

        For y As Integer = 1 To SharedResources.MapSize.Height
            For x As Integer = 1 To SharedResources.MapSize.Width
                _StringGrid(x, y) = GameMapTiles.BLANK

                Dim curPicBox As New ClickablePictureBox(New Point(SharedResources.TileSize.Width * x, SharedResources.TileSize.Height * y),
                                                         SharedResources.TileSize,
                                                         _TypeImageDick(GameMapTiles.BLANK))
                p_uiManager.AddComponent(curPicBox)
                _SelectGrid(x, y) = curPicBox
            Next
        Next

        Dim AmountDone As Integer = 0
        For Each record In _TypeImageDick
            If record.Key <> GameMapTiles.BLANK Then
                Dim curClcikBox As New ClickablePictureBox(New Point(SharedResources.WindowSize.Width + If(AmountDone < 8, 75, 325), If(AmountDone < 8, AmountDone, AmountDone - 8) * 60 + 150), SharedResources.TileSize, record.Value, record.Key)
                p_uiManager.AddComponent(curClcikBox)
                _SampleBoxes.Add(curClcikBox)

                AmountDone += 1
            End If
        Next
    End Sub

    Public Overrides Sub Tick()
        If SharedResources.inputKeys.Count - 1 >= Keys.Escape Then
            If SharedResources.inputKeys(Keys.Escape) Then
                SharedResources.stateManager.ChangeState(New MenuState())
            End If
        End If
    End Sub

    Public Overrides Sub Click()
        Dim isAPictureBox As Boolean = False
        Dim clickedComp As UIComponent = p_uiManager.Click()


        For y As Integer = 1 To SharedResources.MapSize.Height
            For x As Integer = 1 To SharedResources.MapSize.Width
                If _SelectGrid(x, y).Equals(clickedComp) Then
                    isAPictureBox = True
                    _SelectGrid(x, y).ChangeImage(_TypeImageDick(_selectedImage))
                    _StringGrid(x, y) = _selectedImage
                End If
            Next
        Next

        If Not clickedComp.Equals(SharedResources.EmptyComp) Then
            For Each bx As ClickablePictureBox In _SampleBoxes
                If bx.Equals(clickedComp) Then
                    isAPictureBox = True
                    Dim tempClcikBox As ClickablePictureBox = clickedComp
                    _selectedImage = tempClcikBox.Data
                End If
            Next
        End If

        If (Not clickedComp.Equals(SharedResources.EmptyComp)) And Not isAPictureBox Then
            _selectedComp = clickedComp
        End If

        If _selectedComp.Equals(_textBox) Then
            _selectedTextBox = True
        Else
            _selectedTextBox = False
        End If
    End Sub

    Public Overrides Sub KeyPress(key As Char)
        My.Computer.Clipboard.SetText(key)
        If _selectedTextBox Then
            If key = "" Then
                _textBox.popCar()
            Else
                _textBox.AddCar(key)
            End If
        End If
    End Sub

    Public Overrides Sub Render(graphics As Graphics)
        MyBase.Render(graphics)

        If SharedResources.wallCount > 0 Then
            For Each wall As BasicWall In SharedResources.walls
                graphics.DrawImage(wall.Image, wall.Location)
                'graphics.DrawRectangle(New Pen(Color.Red, 3), wall.rect)
            Next
        End If

        graphics.DrawLine(New Pen(Color.Pink), New Point(SharedResources.WindowSize.Width, 0), New Point(SharedResources.WindowSize.Width, SharedResources.WindowSize.Height))

        For y As Integer = 1 To SharedResources.MapSize.Height
            graphics.DrawLine(New Pen(Color.Black), New Point(SharedResources.TileSize.Width, y * SharedResources.TileSize.Height), New Point(SharedResources.WindowSize.Width - SharedResources.TileSize.Width, y * SharedResources.TileSize.Height))
        Next

        For x As Integer = 1 To SharedResources.MapSize.Width
            graphics.DrawLine(New Pen(Color.Black), New Point(x * SharedResources.TileSize.Width, SharedResources.TileSize.Height), New Point(x * SharedResources.TileSize.Width, SharedResources.WindowSize.Height - SharedResources.TileSize.Height))
        Next
    End Sub

    Public Overrides Sub RightClick()
        _selectedImage = GameMapTiles.BLANK
    End Sub
End Class
