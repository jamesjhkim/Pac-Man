Public Class MainForm
    ' Pacman Project
    ' James Kim
    ' December 16 

#Region "Attributes"
    ' Attributes to describe PacMan
    Dim PacMan(5) As Image
    Dim PacDirection As String = "Stop"
    Dim NextPacDirection As String = ""
    Dim PacSpeed As Short = 10 '1, 2, 5, 10, 25, 50 (Divides by 50) 
    Dim MyPacLeft As Short = 50
    Dim myPacTop As Short = 50

    ' Attritbutes to describe Ghosts
    Dim Ghosts(5) As Image
    Dim GhostTimer As Short = 0

    ' Inky's attributes
    Dim InkyDirection As String = "U"
    Dim InkyNextDirection As String = ""
    Dim InkySpeed As Short = 5
    Dim InkyPowerPelletTime As Short = 0
    Dim InkyLeft As Short = 600
    Dim InkyTop As Short = 350

    ' Pinky's attributes
    Dim PinkyDirection As String = "U"
    Dim PinkyNextDirection As String = ""
    Dim PinkySpeed As Short = 5
    Dim PinkyTimerTime As Short = 20
    Dim PinkyPowerPelletTime As Short = 0
    Dim PinkyLeft As Short = 600
    Dim PinkyTop As Short = 450

    ' Blinky's attributes
    Dim BlinkyDirection As String = "R"
    Dim BlinkyNextDirection As String = ""
    Dim BlinkySpeed As Short = 5
    Dim BlinkyTimerTime As Short = 40
    Dim BlinkyPowerPelletTime As Short = 0
    Dim BlinkyLeft As Short = 550
    Dim BlinkyTop As Short = 450

    ' Clyde's attributes
    Dim ClydeDirection As String = "U"
    Dim ClydeNextDirection As String = ""
    Dim ClydeSpeed As Short = 5
    Dim ClydeTimerTime As Short = 60
    Dim ClydePowerPelletTime As Short = 0
    Dim ClydeLeft As Short = 650
    Dim ClydeTop As Short = 450

    ' Fruits' attributes
    Dim FruitLeft As Short = 600
    Dim FruitTop As Short = 550
    Dim CherryTime As Short = 0
    Dim StrawberryTime As Short = 0
    Dim KeyTime As Short = 0
    Dim BerryTime As Short = 0

    ' Various other variables 
    Dim CurrentLevel As Short = 1
    Dim ResetTime As Short = 20
    Dim ResetTimerTime As Short = 0
    Dim ScoreTime As Short = 0
    Dim Counter As Long = 0
    Dim Wall(-1) As PictureBox
    Dim Edible(-1) As PictureBox
    Dim Score As Short = 0
    Dim MazeCompletion As Short
    Dim PowerUpClock As Short = 0
    Dim WallX(-1), WallY(-1) As Short
    Dim DotX(-1), DotY(-1) As Short

    ' Location variables
    Dim TitleTop As Short = 100
    Dim TitleLeft As Short = 1255
    Dim TunnelLeft As Short = 0
    Dim TunnelRight As Short = 1200
    Dim LivesCount As Short = 3
    Dim StartCount As Short = 0
    Dim ReadyTop As Short = 550
    Dim ReadyLeft As Short = 550
    Dim GameOverLeft As Short = 475
    Dim GameOverTop As Short = 550
    Dim ScoreTop As Short = 200
    Dim ScoreLeft As Short = 1275
    Dim LivesTop As Short = 250
    Dim LivesLeft As Short = 1275
    Dim PacLivesTop As Short = 300
    Dim PacLives1Left As Short = 1275
    Dim PacLives2Left As Short = 1325
    Dim PacLives3Left As Short = 1375
#End Region

#Region "Locations"
    Private Sub TitleReset()
        ' Assigns values of the level title's reset position
        Title.Left = TitleLeft
        Title.Top = TitleTop
    End Sub

    Private Sub ScoreReset()
        ' Assigns values of the score's reset position
        lblScore.Left = ScoreLeft
        lblScore.Top = ScoreTop
    End Sub

    Private Sub LivesReset()
        ' Assigns values of the lives' reset position
        lblLives.Left = LivesLeft
        lblLives.Top = LivesTop
    End Sub

    Private Sub PacLivesReset()
        ' Assigns values of Pacman's lives' reset position
        PacLives1.Left = PacLives1Left
        PacLives1.Top = PacLivesTop
        PacLives2.Left = PacLives2Left
        PacLives2.Top = PacLivesTop
        PacLives3.Left = PacLives3Left
        PacLives3.Top = PacLivesTop
    End Sub

    Private Sub GameOverReset()
        ' Assigns values of the "Game Over" reset position
        GameOver.Left = GameOverLeft
        GameOver.Top = GameOverTop
    End Sub

    Private Sub ReadyReset()
        ' Assigns values of "Ready" reset position
        Ready.Left = ReadyLeft
        Ready.Top = ReadyTop
    End Sub

    Private Sub PacReset()
        ' Assigns values of Pacman's reset position
        MyPac.Left = MyPacLeft
        MyPac.Top = myPacTop
    End Sub

    Private Sub InkyReset()
        ' Assigns values of Inky's reset position
        Inky.Left = InkyLeft
        Inky.Top = InkyTop
    End Sub

    Private Sub PinkyReset()
        ' Assigns values of Pinky's reset position
        Pinky.Left = PinkyLeft
        Pinky.Top = PinkyTop
    End Sub

    Private Sub BlinkyReset()
        ' Assigns values of Blinky's reset position
        Blinky.Left = BlinkyLeft
        Blinky.Top = BlinkyTop
    End Sub

    Private Sub ClydeReset()
        ' Assigns values of Clyde's reset position
        Clyde.Left = ClydeLeft
        Clyde.Top = ClydeTop
    End Sub

    Private Sub CherryReset()
        ' Assigns values of the cherry's reset position
        Cherry.Left = FruitLeft
        Cherry.Top = FruitTop
    End Sub

    Private Sub StrawberryReset()
        ' Assigns values of the strawberry's reset position
        Strawberry.Left = FruitLeft
        Strawberry.Top = FruitTop
    End Sub

    Private Sub KeyReset()
        ' Assigns values of the key's reset position
        Key.Left = FruitLeft
        Key.Top = FruitTop
    End Sub

    Private Sub BerryReset()
        ' Assigns values of the berry's reset position
        BerrySpecial.Left = FruitLeft
        BerrySpecial.Top = FruitTop
    End Sub

    Private Sub GhostReset()
        ' Assigns values of Pacman/ghosts' reset position
        InkyReset()
        PinkyReset()
        BlinkyReset()
        ClydeReset()
    End Sub

    Private Sub TunnelTeleport(ByVal Sprite As PictureBox)
        ' Assigns the tunnel's teleportation locations for all sprites(Pacman/ghosts)
        If Sprite.Left = TunnelRight Then
            Sprite.Left = TunnelLeft
        ElseIf Sprite.Left = TunnelLeft Then
            Sprite.Left = TunnelRight
        End If
    End Sub
#End Region

#Region "Other Procedures"
    Private Sub Map1Coordinates()
        ' Assigns x/y coordinates values of the wall locations and dot locations (Levels 1/2) 
        WallX = {0, 50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000, 1050, 1100, 1150, 1200, 0, 0, 0, 0, 0, 50, 100, 150, 150, 150, 100, 50, 0, 0, 50, 100, 150, 150, 150, 100, 50, 0, 0, 0, 0, 0, 50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000, 1050, 1100, 1150, 1200, 1200, 1200, 1200, 1200, 1150, 1100, 1050, 1050, 1050, 1100, 1150, 1200, 1200, 1150, 1100, 1050, 1050, 1050, 1100, 1150, 1200, 1200, 1200, 1200, 1200, 100, 100, 150, 150, 550, 600, 650, 250, 250, 450, 400, 350, 250, 250, 250, 250, 250, 250, 350, 400, 450, 350, 400, 450, 1100, 1050, 1050, 1100, 950, 950, 750, 800, 850, 850, 800, 750, 550, 600, 650, 550, 600, 650, 750, 800, 850, 950, 950, 950, 950, 950, 950, 550, 500, 500, 500, 550, 600, 650, 700, 700, 700, 650, 550, 600, 650, 350, 400, 450, 750, 800, 850, 100, 150, 350, 350, 350, 300, 250, 400, 350, 350, 400, 400, 800, 800, 850, 850, 400, 400, 800, 850, 800, 850, 800, 850, 900, 950, 1050, 1100, 500, 500, 550, 700, 700, 650, 650, 550}
        WallY = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 100, 150, 200, 250, 250, 250, 250, 300, 350, 350, 350, 350, 450, 450, 450, 450, 500, 550, 550, 550, 550, 600, 650, 700, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 700, 650, 600, 550, 550, 550, 550, 500, 450, 450, 450, 450, 350, 350, 350, 350, 300, 250, 250, 250, 250, 200, 150, 100, 50, 100, 150, 100, 150, 50, 50, 50, 100, 150, 100, 100, 100, 250, 300, 350, 450, 500, 550, 150, 150, 150, 300, 300, 300, 100, 100, 150, 150, 100, 150, 100, 100, 100, 150, 150, 150, 150, 150, 150, 200, 200, 200, 300, 300, 300, 250, 300, 350, 450, 500, 550, 400, 400, 450, 500, 500, 500, 500, 500, 450, 400, 400, 300, 300, 300, 250, 250, 250, 250, 250, 250, 650, 650, 550, 600, 650, 650, 650, 650, 400, 450, 450, 400, 400, 450, 450, 400, 550, 600, 550, 550, 600, 600, 650, 650, 650, 650, 650, 650, 600, 650, 600, 650, 600, 600, 650, 650}
        DotX = {611, 61, 1161, 61, 1161, 63, 63, 63, 113, 163, 213, 213, 213, 213, 163, 113, 263, 313, 313, 313, 313, 263, 363, 413, 463, 513, 513, 513, 513, 463, 413, 363, 213, 213, 213, 213, 213, 213, 213, 213, 163, 113, 63, 63, 113, 163, 213, 213, 313, 313, 313, 313, 263, 313, 313, 313, 313, 263, 263, 313, 363, 413, 463, 463, 463, 513, 563, 613, 663, 713, 613, 613, 763, 763, 763, 813, 863, 913, 963, 1013, 1063, 1113, 1163, 1163, 1113, 1063, 1013, 1013, 963, 913, 1013, 1013, 1013, 1013, 1013, 1013, 1013, 1013, 1013, 1013, 1013, 1063, 1113, 1163, 1163, 1163, 1113, 1063, 563, 613, 663, 713, 763, 813, 863, 913, 963, 713, 713, 713, 763, 813, 863, 913, 913, 913, 963, 913, 913, 913, 913, 913, 913, 913, 963, 513, 563, 613, 663, 713, 513, 713}
        DotY = {561, 161, 161, 661, 661, 63, 113, 213, 213, 213, 213, 163, 113, 63, 63, 63, 63, 63, 113, 163, 213, 213, 63, 63, 63, 63, 113, 163, 213, 213, 213, 213, 263, 313, 363, 413, 463, 513, 563, 613, 613, 613, 613, 713, 713, 713, 713, 663, 263, 313, 363, 413, 413, 463, 513, 563, 613, 613, 713, 713, 713, 713, 613, 663, 713, 713, 713, 713, 713, 713, 663, 613, 613, 663, 713, 713, 713, 713, 713, 713, 713, 713, 713, 613, 613, 613, 613, 663, 613, 613, 563, 513, 463, 413, 363, 313, 263, 213, 163, 113, 63, 63, 63, 63, 113, 213, 213, 213, 113, 113, 113, 63, 63, 63, 63, 63, 63, 113, 163, 213, 213, 213, 213, 113, 163, 213, 213, 263, 313, 363, 413, 463, 513, 563, 413, 263, 263, 263, 263, 263, 313, 313}
    End Sub

    Private Sub Map2Coordinates()
        ' Assigns x/y coordinates values of the wall locations and dot locations (Levels 3/4)
        WallX = {0, 50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000, 1050, 1100, 1150, 1200, 0, 0, 0, 0, 0, 50, 100, 150, 200, 200, 200, 150, 100, 50, 0, 0, 50, 100, 150, 200, 200, 200, 150, 100, 50, 0, 0, 0, 0, 0, 50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000, 1050, 1100, 1150, 1200, 1200, 1200, 1200, 1200, 1150, 1100, 1050, 1000, 1000, 1000, 1050, 1100, 1150, 1200, 1200, 1150, 1100, 1050, 1000, 1000, 1000, 1050, 1100, 1150, 1200, 1200, 1200, 1200, 1200, 700, 650, 600, 550, 500, 500, 500, 550, 700, 700, 650, 100, 150, 300, 300, 300, 250, 300, 350, 400, 400, 500, 550, 600, 650, 700, 600, 500, 700, 800, 800, 850, 900, 950, 1050, 1100, 400, 400, 400, 800, 800, 800, 900, 900, 900, 600, 500, 550, 650, 700, 600, 300, 400, 400, 400, 350, 300, 800, 800, 800, 850, 900, 900, 100, 100, 150, 200, 250, 200, 300, 300, 1000, 900, 900, 950, 1000, 1050, 1100, 1100, 600, 600, 600, 400, 400, 450, 500, 500, 500, 700, 700, 750, 800, 800, 700}
        WallY = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 100, 150, 200, 250, 250, 250, 250, 250, 300, 350, 350, 350, 350, 350, 450, 450, 450, 450, 450, 500, 550, 550, 550, 550, 550, 600, 650, 700, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 700, 650, 600, 550, 550, 550, 550, 550, 500, 450, 450, 450, 450, 450, 350, 350, 350, 350, 350, 300, 250, 250, 250, 250, 250, 200, 150, 100, 50, 500, 500, 500, 500, 500, 450, 400, 400, 450, 400, 400, 650, 650, 450, 500, 550, 650, 650, 650, 650, 600, 600, 600, 600, 600, 600, 650, 700, 700, 600, 650, 650, 650, 650, 650, 650, 450, 500, 550, 550, 500, 450, 550, 500, 450, 300, 300, 300, 300, 300, 250, 350, 350, 300, 250, 250, 250, 350, 300, 250, 250, 250, 350, 100, 150, 150, 150, 150, 50, 100, 150, 50, 100, 150, 150, 150, 150, 150, 100, 150, 100, 50, 100, 150, 150, 150, 50, 200, 200, 150, 150, 150, 100, 50}
        DotX = {611, 61, 1161, 61, 1161, 63, 63, 113, 163, 163, 213, 263, 263, 313, 363, 363, 363, 363, 313, 263, 213, 163, 113, 63, 413, 463, 463, 513, 563, 563, 563, 563, 613, 663, 663, 663, 663, 713, 763, 763, 813, 863, 913, 963, 963, 1013, 1063, 1063, 1113, 1163, 1163, 1163, 1113, 1063, 1013, 963, 913, 863, 863, 863, 813, 763, 413, 463, 263, 263, 263, 263, 263, 263, 263, 263, 213, 163, 113, 63, 63, 113, 163, 213, 213, 313, 363, 363, 363, 363, 363, 313, 363, 363, 313, 263, 313, 363, 413, 463, 463, 463, 463, 763, 763, 763, 763, 513, 563, 563, 613, 663, 663, 713, 813, 863, 913, 963, 1013, 1063, 1113, 1163, 1163, 1113, 1063, 1013, 1013, 963, 913, 863, 863, 863, 863, 863, 863, 863, 913, 963, 963, 963, 963, 963, 963, 963, 913, 463, 513, 563, 663, 713, 763, 463, 763, 413, 813, 463, 763}
        DotY = {561, 161, 161, 661, 661, 113, 63, 63, 63, 113, 113, 113, 63, 63, 63, 113, 163, 213, 213, 213, 213, 213, 213, 213, 63, 63, 113, 113, 63, 113, 163, 213, 213, 213, 163, 113, 63, 113, 113, 63, 63, 63, 63, 63, 113, 113, 113, 63, 63, 63, 113, 213, 213, 213, 213, 213, 213, 213, 163, 113, 213, 213, 213, 213, 263, 313, 363, 413, 463, 513, 563, 613, 613, 613, 613, 613, 713, 713, 713, 713, 663, 613, 613, 563, 513, 463, 413, 413, 363, 313, 313, 713, 713, 713, 713, 713, 663, 613, 563, 563, 613, 663, 713, 663, 663, 713, 713, 713, 663, 663, 713, 713, 713, 713, 713, 713, 713, 713, 613, 613, 613, 613, 663, 613, 613, 613, 563, 513, 463, 413, 363, 313, 313, 263, 313, 363, 413, 463, 513, 563, 413, 263, 263, 263, 263, 263, 263, 313, 313, 413, 413, 363, 363}
    End Sub

    Private Sub StartLocations()
        ' Assigns start locations for all characters
        MyPac.Image = PacMan(4)
        PacManRemove()
        GhostReset()
        CherryReset()
        ReadyReset()
        ScoreReset()
        LivesReset()
        TitleReset()
        PacLivesReset()
        GameOverRemove()
        GhostScoreRemove()
        CherryScoreRemove()
        StrawberryRemove()
        KeyRemove()
        BerryRemove()
    End Sub

    Private Sub ClocksFalse()
        ' Turns off all of the clocks that involve Pacman/ghosts
        PacClock.Enabled = False
        InkyClock.Enabled = False
        BlinkyClock.Enabled = False
        PinkyClock.Enabled = False
        ClydeClock.Enabled = False
    End Sub

    Private Sub CherryScoreDisplay()
        ' Displays the score increase where the cherry has been eaten 
        CherryScore.Top = 560
        CherryScore.Left = 575
        FruitTimer.Enabled = True
    End Sub

    Private Sub GhostScoreDisplay(ByVal Sprite As PictureBox)
        ' Displays the score increase where the ghost has been eaten 
        GhostScore.Top = Sprite.Top
        GhostScore.Left = Sprite.Left
        ScoreTimer.Enabled = True
    End Sub

    Private Function GhostsTargeted(ByVal Sprite As PictureBox, ByVal DirectionsAvailable As String)
        ' Activates flee behaviour for ghosts in "Vulnerable mode" 
        ' GhostsTarget: Determines whether the ghosts will attack Pacman or flee 
        Dim DirectionsTargeted As String = ""
        Dim UniqueDirection As String = ""
        Dim AbovePac As Boolean = Nothing
        Dim LeftofPac As Boolean = Nothing

        If Sprite.Left > Me.MyPac.Left Then
            LeftofPac = False
        Else
            LeftofPac = True
        End If
        If Sprite.Top > Me.MyPac.Top Then
            AbovePac = False
        Else
            AbovePac = True
        End If

        ' Flee/Attack behaviour 
        If PowerPelletClock.Enabled Then
            If LeftofPac = False Then
                UniqueDirection += "R"
            ElseIf LeftofPac = True Then
                UniqueDirection += "L"
            End If
            If AbovePac = False Then
                UniqueDirection += "D"
            ElseIf AbovePac = True Then
                UniqueDirection += "U"
            End If
        Else
            If LeftofPac = False Then
                UniqueDirection += "L"
            ElseIf LeftofPac = True Then
                UniqueDirection += "R"
            End If
            If AbovePac = False Then
                UniqueDirection += "U"
            ElseIf AbovePac = True Then
                UniqueDirection += "D"
            End If
        End If
        For index = 0 To UniqueDirection.Length - 1
            If UniqueDirection.Substring(index, 1) Like DirectionsAvailable Then
                DirectionsTargeted += UniqueDirection.Substring(index, 1)
            End If
        Next
        Return DirectionsTargeted
    End Function
#End Region

#Region "Off Screen Locations"
    Private Sub PacManRemove()
        ' Removes Pacman off the screen (at start of game)
        MyPac.Top = -100
        MyPac.Left = -100
    End Sub

    Private Sub GameOverRemove()
        ' Removes the "Game Over" sign off the screen
        GameOver.Top = -100
        GameOver.Left = -100
    End Sub

    Private Sub GhostScoreRemove()
        ' Removes the score increase (ghosts) off the screen
        GhostScore.Top = -100
        GhostScore.Left = -100
        ScoreTime = 0
        ScoreTimer.Enabled = False
    End Sub

    Private Sub CherryScoreRemove()
        ' Removes the score increase (cherry) off the screen
        CherryScore.Top = -100
        CherryScore.Left = -100
        CherryTime = 0
        FruitTimer.Enabled = False
    End Sub

    Private Sub CherryRemove()
        ' Removes the cherry fruit special off the screen
        Cherry.Top = -100
        Cherry.Left = -100
    End Sub

    Private Sub StrawberryRemove()
        ' Removes the strawberry fruit special off the screen
        Strawberry.Top = -100
        Strawberry.Left = -100
    End Sub

    Private Sub KeyRemove()
        ' Removes the cherry fruit special off the screen
        Key.Top = -100
        Key.Left = -100
    End Sub

    Private Sub BerryRemove()
        ' Removes the berry fruit special off the screen
        BerrySpecial.Top = -100
        BerrySpecial.Left = -100
    End Sub
    Private Sub GhostsRemove()
        ' Removes all ghosts off the screen
        Inky.Top = -100
        Inky.Left = -100
        Pinky.Top = -100
        Pinky.Left = -100
        Blinky.Top = -100
        Blinky.Left = -100
        Clyde.Top = -100
        Clyde.Left = -100
    End Sub
#End Region

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' 1) Pre load all the ghost and pacman images
        ' 2) Call LoadLevel and tell it to load level 1 

        ' Pre load the ghost images
        Ghosts(0) = Image.FromFile("GhostVulnerable.png")
        Ghosts(1) = Image.FromFile("Inky.png")
        Ghosts(2) = Image.FromFile("Blinky.png")
        Ghosts(3) = Image.FromFile("Pinky.png")
        Ghosts(4) = Image.FromFile("Clyde.png")
        Ghosts(5) = Image.FromFile("GhostVulnerable2.png")
        Inky.Image = Ghosts(1)
        Blinky.Image = Ghosts(2)
        Pinky.Image = Ghosts(3)
        Clyde.Image = Ghosts(4)

        ' Pre load all of our pac images
        PacMan(0) = Image.FromFile("B.png") 'Base Pac 
        PacMan(1) = Image.FromFile("U.png") 'Up
        PacMan(2) = Image.FromFile("D.png") 'Down
        PacMan(3) = Image.FromFile("L.png") 'Left
        PacMan(4) = Image.FromFile("R.png") 'Right

        ' Pre load all of the "fruit" images 
        Cherry.Image = Image.FromFile("Cherry.png") ' +1000 to Score
        Strawberry.Image = Image.FromFile("Strawberry.png") ' Stops all ghosts in "vulnerable" mode 
        BerrySpecial.Image = Image.FromFile("BerrySpecial.png") ' Increases Pac's speed 
        Key.Image = Image.FromFile("Key.png") ' +extra life to Pac's lives

        ' Pre load all of Pacmans' lives' images
        PacLives1.Image = Image.FromFile("Lives.png")
        PacLives2.Image = Image.FromFile("Lives.png")
        PacLives3.Image = Image.FromFile("Lives.png")

        ' Pre load the "Game Over" image
        GameOver.Image = Image.FromFile("GameOver.png")

        ' Pre load the "Scores" images
        GhostScore.Image = Image.FromFile("GhostScore.png")
        CherryScore.Image = Image.FromFile("CherryScore.png")

        LoadLevel(1)
    End Sub

    Private Sub LoadLevel(ByVal shtLevel As Short)
        ' For every maze level we load we need to do each of the following:
        ' 1. Load up the x and y coordinates for each wall piece
        ' 2. Load up the x and y coordinates for each edible (dots, powerpellets, fruit)
        ' 3. Place our Pacman at his start location
        ' 4. Place our ghost(s) at their start location(s) 
        ' 5. Then place all of the wall titles and edibles on the form based on the .left

        If shtLevel = 1 Then
            Title.Image = Image.FromFile("Level1.png")
            My.Computer.Audio.Play(My.Resources.pacman_beginning, AudioPlayMode.Background)
        ElseIf shtLevel = 2 Then
            Title.Image = Image.FromFile("Level2.png")
            My.Computer.Audio.Play(My.Resources.pacman_intermission, AudioPlayMode.Background)
        ElseIf shtLevel = 3 Then
            Title.Image = Image.FromFile("Level3.png")
            My.Computer.Audio.Play(My.Resources.pacman_intermission, AudioPlayMode.Background)
        ElseIf shtLevel = 4 Then
            Title.Image = Image.FromFile("Level4.png")
            My.Computer.Audio.Play(My.Resources.pacman_intermission, AudioPlayMode.Background)
        End If

        If shtLevel > 2 Then
            InkySpeed = 10
            PinkySpeed = 10
            BlinkySpeed = 10
            ClydeSpeed = 10
        End If

        If shtLevel > 1 Then
            GhostsRemove()
            CherryRemove()
            For index = 0 To WallX.Length() - 1
                Me.Controls.Remove(Wall(index))
            Next
            ReDim Wall(-1)

            For index = 0 To DotX.Length() - 1
                Me.Controls.Remove(Edible(index))
            Next
            ReDim Edible(-1)
        End If

        If shtLevel = 1 Or shtLevel = 2 Then
            ' Level 1/ Level 2 
            Map1Coordinates()
            StartLocations()
        ElseIf shtLevel = 3 Or shtLevel = 4 Then
            ' Level 3
            ' Assigns x/y coordinates values of the wall locations and dot locations
            ' Assigns start locations for all characters
            Map2Coordinates()
            StartLocations()
        End If

        ' The following loop is run once for every wall piece you need.
        ' Create a new wall piece with all the properties of a wall piece
        ' Give it a location, colour, size and so on
        ' Place it on the screen based on the coordinates above in WallX() and WallY() 

        For index = 0 To WallX.Length() - 1
            Dim NewWallPiece As New PictureBox
            NewWallPiece.Width = 50
            NewWallPiece.Height = 50
            NewWallPiece.Left = WallX(index)
            NewWallPiece.Top = WallY(index)
            NewWallPiece.BackColor = Color.Blue

            ReDim Preserve Wall(Wall.Length())
            Wall(index) = NewWallPiece
            Me.Controls.Add(Wall(index))
        Next

        For index = 0 To DotX.Length() - 1
            Dim NewDot As New PictureBox
            If (DotX(index) = 61 And DotY(index) = 161) Or
                (DotX(index) = 1161 And DotY(index) = 161) Or
                (DotX(index) = 61 And DotY(index) = 661) Or
                (DotX(index) = 1161 And DotY(index) = 661) Then
                ' Power Pellet 
                NewDot.BackColor = Color.AliceBlue
                NewDot.Name = "PowerPellet"
                NewDot.Width = 20
                NewDot.Height = 20
                NewDot.Left = DotX(index) + 6
                NewDot.Top = DotY(index) + 6
            ElseIf (DotX(index) = 611 And DotY(index) = 561) Then
                ' Fruit Special 
                NewDot.BackColor = Color.Red
                NewDot.Name = "Fruit"
                NewDot.Width = 20
                NewDot.Height = 20
                NewDot.Left = DotX(index) + 6
                NewDot.Top = DotY(index) + 6
            Else
                ' Regular Dots 
                NewDot.BackColor = Color.White
                NewDot.Name = "Dot"
                NewDot.Width = 5
                NewDot.Height = 5
                NewDot.Left = DotX(index) + 12
                NewDot.Top = DotY(index) + 12
            End If

            ReDim Preserve Edible(Edible.Length())
            Edible(index) = NewDot
            Me.Controls.Add(Edible(index))
        Next
        StartCount = 0
        GhostTimer = 0
        InkyPowerPelletTime = 0
        PinkyPowerPelletTime = 0
        BlinkyPowerPelletTime = 0
        ClydePowerPelletTime = 0
        PacDirection = ""
    End Sub

    Private Sub Main_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        ' keyvalues to set nextpacdirection
        ' when it is set then we use it ot see if the move is valid 
        If e.KeyValue = Keys.Up Then
            NextPacDirection = "U"
        ElseIf e.KeyValue = Keys.Down Then
            NextPacDirection = "D"
        ElseIf e.KeyValue = Keys.Right Then
            NextPacDirection = "R"
        ElseIf e.KeyValue = Keys.Left Then
            NextPacDirection = "L"
        Else
            NextPacDirection = "Stop"
        End If
    End Sub

    Private Sub MovePac(ByVal PacDirection As String, ByVal Counter As Long)
        'This simply moves our pacman in the direction stored in
        ' PacDirection and animates the chomping motion based on the # stored in Counter
        ' if Counter is an even # it will load the open mouth image for the correct direction
        ' if counter is odd it will load the closed mouth pacman 

        If PacDirection = "R" Then
            Me.MyPac.Left += PacSpeed
            If Counter Mod 2 = 0 Then
                MyPac.Image = PacMan(4)
            Else
                MyPac.Image = PacMan(0)
            End If
        ElseIf PacDirection = "L" Then
            Me.MyPac.Left -= PacSpeed
            If Counter Mod 2 = 0 Then
                MyPac.Image = PacMan(3)
            Else
                MyPac.Image = PacMan(0)
            End If
        ElseIf PacDirection = "U" Then
            Me.MyPac.Top -= PacSpeed
            If Counter Mod 2 = 0 Then
                MyPac.Image = PacMan(1)
            Else
                MyPac.Image = PacMan(0)
            End If
        ElseIf PacDirection = "D" Then
            Me.MyPac.Top += PacSpeed
            If Counter Mod 2 = 0 Then
                MyPac.Image = PacMan(2)
            Else
                MyPac.Image = PacMan(0)
            End If
        End If
    End Sub

    Private Function Eats(ByVal PacMan As PictureBox)
        ' loop through all the dots to see if pac man has hit it. If the bounds of 
        ' the Pacman picturebox
        ' touches the bounds of the dot or other object, return its name.
        ' so if this function returns a "Dot" pac has hit a dot. If it returns
        ' a "Pellet" pacman has hit a pellet
        ' and if it returns a "Fruit" Pacman has eaten a special. If it returns
        ' nothing then no collision has occured
        ' We also move the dots off screen to make it look like they disappeared 
        ' We also count up the eaten items to see if maze is complete 

        For index = 0 To Edible.Length - 1
            If MazeCompletion = 3 Then
                Return "Complete"
            ElseIf PacMan.Bounds.IntersectsWith(Edible(index).Bounds) Then
                MazeCompletion += 1
                Edible(index).Left = -1000
                Return Edible(index).Name
            End If
        Next
        Return ""
    End Function

    Private Function HitGhost(ByVal Pacman As PictureBox, ByVal Ghost As PictureBox)
        Dim GhostDistance As Short = Math.Sqrt((MyPac.Left - Ghost.Left) ^ 2 _
                                     + (MyPac.Top - Ghost.Top) ^ 2)

        If GhostDistance <= 49 And Me.PowerPelletClock.Enabled = False Then
            Return "Dead"
        ElseIf GhostDistance <= 49 And Me.PowerPelletClock.Enabled = True Then
            Return "Ghost Score"
        Else
            Return ""
        End If
    End Function

    Private Function HitWalls(ByVal Sprite As PictureBox, ByVal NextDirection As String, ByVal Speed As Short)
        ' We use his function to check to see if our Pacman or Ghost's current direction
        ' will make him crash into a wall on the next move 
        ' We do this by creating a copy of the sprite (either Pacman or Ghost) and copying
        ' over its . left, .top, .height, and .width properties to our SpriteCopy. 
        ' Then we update the SpriteCopy with the next direction values.
        ' if the new position of the SpireCopy is detected as a collison we return TRUE otherise function
        ' So in easyspeak, if the next move of the spirte will be a collision this function will say 
        ' So in the calling function we can then move the sprite if HItwalls = FALSE and not move him

        ' Create a replica of the sprite
        Dim SpriteCopy = New PictureBox()
        Dim XVal, YVal, Width, Height As Short
        XVal = Sprite.Left
        YVal = Sprite.Top
        Width = Sprite.Width
        Height = Sprite.Height
        SpriteCopy.Left = XVal
        SpriteCopy.Top = YVal
        SpriteCopy.Width = Width
        SpriteCopy.Height = Height

        ' Change its position based on current value of NextDirection 
        If NextDirection = "R" Then
            SpriteCopy.Left += Speed
        ElseIf NextDirection = "L" Then
            SpriteCopy.Left -= Speed
        ElseIf NextDirection = "U" Then
            SpriteCopy.Top -= Speed
        ElseIf NextDirection = "D" Then
            SpriteCopy.Top += Speed
        End If

        ' Check for collision based on the location of this temporary sprite and return either TRUE or FALSE
        For index = 0 To Wall.Length() - 1
            If SpriteCopy.Bounds.IntersectsWith(Wall(index).Bounds) Then
                Return True
            End If
        Next
        Return False
    End Function


#Region "Clocks"
    Private Sub StartClock_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartClock.Tick
        ' Begins the count to start the game
        StartCount += 1

        If StartCount Mod 6 = 1 Then
            Ready.Image = Nothing
        Else
            Ready.Image = Image.FromFile("Ready.png")
        End If
        ' Display Pacman (with 2 lives), and begins the sprites' movement 

        If StartCount = 35 Then
            Ready.Top = -100
            Ready.Left = -100
            PacReset()
            PacLives3.Top = -100
            PacLives3.Top = -100
        ElseIf StartCount = 36 Then
            PacClock.Enabled = True
            GhostClock.Enabled = True
            InkyClock.Enabled = True
        End If
    End Sub

    Private Sub PacClock_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PacClock.Tick
        'This function deals with all of the Pacman move sutff
        '  It does all of the following ...
        '1) Updates Counter so we can animate our Pacman from open mouth to close
        '   mouth to make him "Munch". Odd is open and even is closed.
        '2) Use HitWalls to see if next move will be a collision. If it is set
        '   PacDirection to Stop. If it isn't make NextPacDirection and PacDirection the same.
        '3) Check to see what edibles Pacman has collided with and update score and 
        '   play sound files accordingly
        '4) Check to see if Pacman has moved through the secret passage and move 
        '   him to the other side of hte maze.

        ' Check for collison with walls then move or stop Pacman accordingly.
        Counter += 1

        ' Next position is a collision with walls
        If HitWalls(MyPac, NextPacDirection, PacSpeed) = True Then

            If NextPacDirection = PacDirection Or HitWalls(MyPac, NextPacDirection, PacSpeed) Then
                PacDirection = "Stop"
            End If
        Else
            PacDirection = NextPacDirection
        End If

        MovePac(PacDirection, Counter)
        TunnelTeleport(MyPac)

        If HitGhost(MyPac, Inky) = "Dead" Or HitGhost(MyPac, Pinky) = "Dead" Or
            HitGhost(MyPac, Blinky) = "Dead" Or HitGhost(MyPac, Clyde) = "Dead" Then
            My.Computer.Audio.Play(My.Resources.pacman_death, AudioPlayMode.Background)
            ClocksFalse()
            ResetClock.Enabled = True
            LivesCount -= 1
            If LivesCount = 0 Then
                GameOverReset()
                ResetClock.Enabled = False
            End If
        ElseIf (HitGhost(MyPac, Inky) = "Ghost Score") Then
            Score += 200
            GhostScoreDisplay(Inky)
            My.Computer.Audio.Play(My.Resources.pacman_eatghost, AudioPlayMode.Background)
            InkyReset()
            If InkyPowerPelletTime <= 10 Then
                InkyClock.Enabled = False
            Else
                InkyClock.Enabled = True
            End If
        ElseIf (HitGhost(MyPac, Pinky) = "Ghost Score") Then
            Score += 200
            GhostScoreDisplay(Pinky)
            My.Computer.Audio.Play(My.Resources.pacman_eatghost, AudioPlayMode.Background)
            PinkyReset()
            If PinkyPowerPelletTime <= 10 Then
                PinkyClock.Enabled = False
            Else
                PinkyClock.Enabled = True
            End If
        ElseIf (HitGhost(MyPac, Blinky) = "Ghost Score") Then
            Score += 200
            GhostScoreDisplay(Blinky)
            My.Computer.Audio.Play(My.Resources.pacman_eatghost, AudioPlayMode.Background)
            BlinkyReset()
            If BlinkyPowerPelletTime <= 10 Then
                BlinkyClock.Enabled = False
            Else
                BlinkyClock.Enabled = True
            End If
        ElseIf (HitGhost(MyPac, Clyde) = "Ghost Score") Then
            Score += 200
            GhostScoreDisplay(Clyde)
            My.Computer.Audio.Play(My.Resources.pacman_eatghost, AudioPlayMode.Background)
            ClydeReset()
            If ClydePowerPelletTime <= 10 Then
                ClydeClock.Enabled = False
            Else
                ClydeClock.Enabled = True
            End If
        End If

        ' See what edible Pacman has collided with and play correct sounds and update score
        Dim Eatable As String = Eats(MyPac)

        If Eatable = "Dot" Then
            Score += 10
            My.Computer.Audio.Play(My.Resources.pacman_chomp, AudioPlayMode.Background)
        ElseIf Eatable = "PowerPellet" Then
            Me.PowerPelletClock.Enabled = True
            Score += 100
            My.Computer.Audio.Play(My.Resources.pacman_chomp, AudioPlayMode.Background)
        ElseIf Eatable = "Fruit" Then
            CherryRemove()
            Score += 1000
            CherryScoreDisplay()
            My.Computer.Audio.Play(My.Resources.pacman_eatfruit, AudioPlayMode.Background)
        ElseIf Eatable = "Complete" Then
            ClocksFalse()
            GhostClock.Enabled = False
            ' load up next game board 
            If CurrentLevel = 4 Then
                MessageBox.Show("Game Complete!!!")
                ClocksFalse()
            Else
                MessageBox.Show("Level Complete")
                CurrentLevel += 1
                LoadLevel(CurrentLevel)
                MazeCompletion = 0
            End If
        End If

        Me.lblScore.Text = "Score: " + Score.ToString
    End Sub

    Private Sub GhostClock_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GhostClock.Tick

        GhostTimer += 1
        If (GhostTimer = PinkyTimerTime) And (PinkyClock.Enabled = False) Then PinkyClock.Enabled = True

        If (GhostTimer = BlinkyTimerTime) And (BlinkyClock.Enabled = False) Then BlinkyClock.Enabled = True

        If (GhostTimer = ClydeTimerTime) And (ClydeClock.Enabled = False) Then ClydeClock.Enabled = True

    End Sub

    Private Sub InkyClock_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InkyClock.Tick
        ' Exiting "ghost's den"  
        If Inky.Left = 600 And Inky.Top = 350 Then
            InkyDirection = "R"
        Else
            InkyDirection = GhostChooseDirection(Inky, InkySpeed, InkyDirection)
        End If

        ' Direction/speed relations
        If InkyDirection = "L" Then
            Inky.Left -= InkySpeed
        ElseIf InkyDirection = "R" Then
            Inky.Left += InkySpeed
        ElseIf InkyDirection = "U" Then
            Inky.Top -= InkySpeed
        ElseIf InkyDirection = "D" Then
            Inky.Top += InkySpeed
        End If

        ' Tunnel teleportation
        TunnelTeleport(Inky)

    End Sub

    Private Sub PinkyClock_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PinkyClock.Tick
        ' Exiting "ghost's den" 
        If Pinky.Left = 600 And Pinky.Top = 450 Then
            PinkyDirection = "U"
        ElseIf Pinky.Left = 600 And Pinky.Top = 400 Then
            PinkyDirection = "U"
        ElseIf Pinky.Left = 600 And Pinky.Top = 350 Then
            PinkyDirection = "L"
        Else
            PinkyDirection = GhostChooseDirection(Pinky, PinkySpeed, PinkyDirection)
        End If

        ' Direction/speed relations
        If PinkyDirection = "L" Then
            Pinky.Left -= PinkySpeed
        ElseIf PinkyDirection = "R" Then
            Pinky.Left += PinkySpeed
        ElseIf PinkyDirection = "U" Then
            Pinky.Top -= PinkySpeed
        ElseIf PinkyDirection = "D" Then
            Pinky.Top += PinkySpeed
        End If

        ' Tunnel teleportation
        TunnelTeleport(Pinky)
    End Sub

    Private Sub BlinkyClock_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BlinkyClock.Tick
        ' Exiting "ghost's den"  
        If Blinky.Left = 550 And Blinky.Top = 450 Then
            BlinkyDirection = "R"
        ElseIf Blinky.Left = 600 And Blinky.Top = 450 Then
            BlinkyDirection = "U"
        ElseIf Blinky.Left = 600 And Blinky.Top = 350 Then
            BlinkyDirection = "R"
        Else
            BlinkyDirection = GhostChooseDirection(Blinky, BlinkySpeed, BlinkyDirection)
        End If

        ' Direction/speed relations
        If BlinkyDirection = "L" Then
            Blinky.Left -= BlinkySpeed
        ElseIf BlinkyDirection = "R" Then
            Blinky.Left += BlinkySpeed
        ElseIf BlinkyDirection = "U" Then
            Blinky.Top -= BlinkySpeed
        ElseIf BlinkyDirection = "D" Then
            Blinky.Top += BlinkySpeed
        End If

        ' Tunnel teleportation
        TunnelTeleport(Blinky)
    End Sub

    Private Sub ClydeClock_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClydeClock.Tick
        ' Exiting "ghost's den" 
        If Clyde.Left = 650 And Clyde.Top = 450 Then
            ClydeDirection = "L"
        ElseIf Clyde.Left = 600 And Clyde.Top = 450 Then
            ClydeDirection = "U"
        ElseIf Clyde.Left = 600 And Clyde.Top = 350 Then
            ClydeDirection = "L"
        Else
            ClydeDirection = GhostChooseDirection(Clyde, ClydeSpeed, ClydeDirection)
        End If

        ' Direction/speed relations
        If ClydeDirection = "L" Then
            Clyde.Left -= ClydeSpeed
        ElseIf ClydeDirection = "R" Then
            Clyde.Left += ClydeSpeed
        ElseIf ClydeDirection = "U" Then
            Clyde.Top -= ClydeSpeed
        ElseIf ClydeDirection = "D" Then
            Clyde.Top += ClydeSpeed
        End If

        ' Tunnel teleportation
        TunnelTeleport(Clyde)
    End Sub

    Private Sub PowerPelletClock_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PowerPelletClock.Tick
        ' Calculates time before Inky is released from "ghost's den" after being eaten by Pacman 
        If InkyClock.Enabled = False Then
            InkyPowerPelletTime += 1
        Else
            InkyPowerPelletTime = 0
        End If

        ' Calculates time before Pinky is released from "ghost's den" after being eaten by Pacman 
        If PinkyClock.Enabled = False Then
            PinkyPowerPelletTime += 1
        Else
            PinkyPowerPelletTime = 0
        End If

        ' Calculates time before Blinky is released from "ghost's den" after being eaten by Pacman 
        If BlinkyClock.Enabled = False Then
            BlinkyPowerPelletTime += 1
        Else
            BlinkyPowerPelletTime = 0
        End If

        ' Calculates time before Clyde is released from "ghost's den" after being eaten by Pacman 
        If ClydeClock.Enabled = False Then
            ClydePowerPelletTime += 1
        Else
            ClydePowerPelletTime = 0
        End If

        ' Triggers the "PowerUpClock" and 
        ' assigns the "vulnerable" images to the ghosts
        PowerUpClock += 1
        Me.Inky.Image = Ghosts(0)
        Me.Pinky.Image = Ghosts(0)
        Me.Blinky.Image = Ghosts(0)
        Me.Clyde.Image = Ghosts(0)

        ' When the clock's time has about 4 seconds left,
        ' the ghosts will flash between their vulnerable and normal images
        If PowerUpClock >= 50 Then
            If PowerUpClock Mod 2 = 1 Then
                Me.Inky.Image = Ghosts(0)
                Me.Pinky.Image = Ghosts(0)
                Me.Blinky.Image = Ghosts(0)
                Me.Clyde.Image = Ghosts(0)
            Else
                Me.Inky.Image = Ghosts(5)
                Me.Pinky.Image = Ghosts(5)
                Me.Blinky.Image = Ghosts(5)
                Me.Clyde.Image = Ghosts(5)
            End If
        End If

        ' When the clock's time is up, the ghosts will return to normal 
        ' from their "vulnerable" images
        If PowerUpClock = 80 Then
            Me.PowerPelletClock.Enabled = False
            If InkyClock.Enabled = False Then
                InkyClock.Enabled = True
            End If
            If PinkyClock.Enabled = False Then
                PinkyClock.Enabled = True
            End If
            If BlinkyClock.Enabled = False Then
                BlinkyClock.Enabled = True
            End If
            If ClydeClock.Enabled = False Then
                ClydeClock.Enabled = True
            End If
            PowerUpClock = 0
            Me.Inky.Image = Ghosts(1)
            Me.Pinky.Image = Ghosts(2)
            Me.Blinky.Image = Ghosts(3)
            Me.Clyde.Image = Ghosts(4)
        End If
    End Sub

    Private Sub ResetClock_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetClock.Tick
        ' Triggers the reset timer 
        ResetTimerTime += 1

        ' Resets all sprites and clocks to normal positions once Pacman has died 
        If ResetTime = ResetTimerTime Then
            GhostReset()
            PacReset()
            GhostTimer = 0
            ResetTimerTime = 0
            ResetClock.Enabled = False
            PacClock.Enabled = True
            InkyClock.Enabled = True
            If LivesCount = 2 Then
                PacLives2.Top = -100
                PacLives2.Left = -100
            ElseIf LivesCount = 1 Then
                PacLives1.Top = -100
                PacLives1.Left = -100
            End If
        End If
    End Sub

    Private Sub ScoreTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScoreTimer.Tick
        ScoreTime += 1

        If ScoreTime = 3 Then
            GhostScoreRemove()
        End If
    End Sub

    Private Sub FruitTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FruitTimer.Tick
        CherryTime += 1

        If CherryTime = 3 Then
            CherryScoreRemove()
        End If
    End Sub
#End Region


#Region "Directions" 
    Private Function GhostChooseDirection(ByVal Ghost As PictureBox, ByVal GhostSpeed As Short, ByVal GhostDirection As String)
        Dim DirectionsAvailable As String = ""
        Dim CheckDirection As String = ""

        If HitWalls(Ghost, "U", GhostSpeed) = False And GhostDirection <> "D" Then
            DirectionsAvailable += "U"
        End If
        If HitWalls(Ghost, "D", GhostSpeed) = False And GhostDirection <> "U" Then
            DirectionsAvailable += "D"
        End If
        If HitWalls(Ghost, "L", GhostSpeed) = False And GhostDirection <> "R" Then
            DirectionsAvailable += "L"
        End If
        If HitWalls(Ghost, "R", GhostSpeed) = False And GhostDirection <> "L" Then
            DirectionsAvailable += "R"
        End If
        ' Fix 
        'If CurrentLevel >= 3 Then
        '    CheckDirection = GhostsTargeted(Ghost, DirectionsAvailable)
        '    If CheckDirection <> "" Then
        '        DirectionsAvailable = CheckDirection
        '    End If
        'End If

        Randomize()
        GhostDirection = DirectionsAvailable.Substring(Int(Rnd() * DirectionsAvailable.Length), 1)

        Return GhostDirection
    End Function
#End Region


End Class
