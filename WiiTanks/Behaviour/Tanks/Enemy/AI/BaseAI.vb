Public MustInherit Class BaseAI
    Protected p_hose As Bae
    Protected p_mineCooldown As Integer
    Protected p_baseRotation As Integer
    Sub New(Creature As Bae)
        p_hose = Creature
    End Sub

    Public MustOverride Sub Tick()

    Private Function calculateBase()
        Dim btmp1 As New Bitmap(151, 151)

        Using G = Graphics.FromImage(btmp1)
            G.TranslateTransform(btmp1.Width / 2, btmp1.Height / 2)
            G.RotateTransform(p_baseRotation)
            G.DrawImage(p_hose.baseImage, New Point(-p_hose.baseImage.Width / 2, -p_hose.baseImage.Height / 2))
            G.TranslateTransform(-btmp1.Width / 2, -btmp1.Height / 2)
        End Using

        Return btmp1
    End Function

    Protected MustOverride Function calculateTurret(btmp1 As Bitmap)
End Class
