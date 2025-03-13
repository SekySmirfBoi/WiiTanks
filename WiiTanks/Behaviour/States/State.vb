Imports System.IO

Public MustInherit Class State

    Protected p_uiManager As UIManager

    Public Sub New()
        p_uiManager = New UIManager()
    End Sub

    Public MustOverride Sub Create()
    Public MustOverride Sub Tick()
    Public Overridable Sub Render(graphics As Graphics)
        p_uiManager.Render(graphics)
    End Sub
    Public MustOverride Sub Click()

    Public Overridable Sub RightClick()
    End Sub

    Public Overridable Sub KeyPress(key As Char)
    End Sub
End Class
