Imports System.IO

Public Class LevelSelectState
    Inherits State

    Public Overrides Sub Create()
        SharedResources.ChantWindowSize(SharedResources.WindowSize)
        p_uiManager.AddComponent(New Button(New Point(SharedResources.CentreWindowCoord.X - 60, 20), "Back", SharedResources.BtnSize,
                                            Function() As Boolean
                                                SharedResources.stateManager.ChangeState(New MenuState())
                                            End Function))

        For y As Integer = 0 To (SharedResources.NumberOfLevels - 1) \ 7               ' max is 5
            For x As Integer = 0 To If(y = (SharedResources.NumberOfLevels - 1) \ 7, SharedResources.NumberOfLevels - y * 7, If(SharedResources.NumberOfLevels > 7, 7, SharedResources.NumberOfLevels)) - 1      ' max is 8

                Dim tempInt As Integer = x + y * 7 + 1
                Dim file As String = ""

                Dim isDebug As Boolean = False

                Select Case tempInt
                    Case 1
                        file = My.Resources.Level1

                    Case 17
                        file = My.Resources.Level17
                    Case Else
                        isDebug = True
                        file = My.Resources.debug
                End Select

                p_uiManager.AddComponent(New Button(New Point(x * 130 + 50, y * 130 + 100), tempInt,
                                                    New Size(120, 120),
                                                    Function() As Boolean
                                                        SharedResources.AIEnabled = Not isDebug
                                                        SharedResources.stateManager.ChangeState(New GameState(file))
                                                    End Function))
            Next
        Next

        p_uiManager.AddComponent(New Button(New Point(SharedResources.WindowSize.Width - 300, SharedResources.WindowSize.Height - 100), "Load custom", SharedResources.BtnSize,
                                            Function()
                                                Dim fd As New OpenFileDialog
                                                fd.Title = "Custom map"
                                                fd.Filter = "Text files (*.txt)|*.txt"

                                                Dim reader As StreamReader
                                                If fd.ShowDialog() = DialogResult.OK Then
                                                    reader = New StreamReader(fd.FileName)
                                                End If

                                                SharedResources.stateManager.ChangeState(New GameState(reader.ReadToEnd()))
                                            End Function))

    End Sub

    Public Overrides Sub Tick()
    End Sub

    Public Overrides Sub Click()
        p_uiManager.Click()
    End Sub
End Class
