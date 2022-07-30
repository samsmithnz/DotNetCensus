'' An example of a pier to Pier game imports a Socket connection
'' Implementation in C# of Battleships
'' Uses the Previously derived Board Control
'' This VB version uses the C# Board control to denonstarte compatability.
'' Also has a threaded model to avoid lock up.
'' Author Tony Grimer
'' For Chapter 10 of Essential guide to .NET
''****************************************************************************
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Net.Sockets
Imports System.Net
Imports System.IO
Imports System.Threading

Namespace SimpleSocketsBasedBattleShips
    Public Class mainform
        Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "
#Region "Class data"
        Public RemoteIp As String = "127.0.0.1"
        Public RemoteIpAdd As IPAddress
        Private Shared ipAddress As ipAddress = Dns.Resolve("localhost").AddressList(0)
        Private Shared tcpListener As tcpListener = New tcpListener(5000)
        Private GameWriter As BinaryWriter
        Private GameReader As BinaryReader
        Private SSocket As Socket '' Server type socket
        Private CSocket As TcpClient  '' Client type socket
        Private SStream As NetworkStream  '' Server stream
        Private CStream As NetworkStream  '' Client Stream
        Private Server As Boolean = False ''is this instance a Server or a client
        Private Connected As Boolean = False '' holds the play thread until connection
        Private MyTurn As Boolean = False  ''Indicate whose turn..
        Private StartUp As Thread    '' One of thread to get connections
        Private Play As Thread      '' Thread we play the game within
        Private Player As Integer    '' Identify the player Number
        Private LastMove As Integer  ''last move sent.
#End Region
        Public Sub New()
            MyBase.New()
            InitializeComponent()
            Dim StartUpThread As ThreadStart = New ThreadStart(AddressOf Me.Setup)
            Dim PlayThread As ThreadStart = New ThreadStart(AddressOf Me.PlayGame)
            '' Sort out opponents IP address
            '' set up my Home Grid...
            '' Five ships... for this simple demo..
            Dim Dialog As GetIp
            Dialog = New GetIp(Me)
            Dialog.ShowDialog()
            CreateShips()
            Info.Items.Add("Initialising Game")
            StartUp = New Thread(StartUpThread)
            Play = New Thread(PlayThread)
            StartUp.Start()

            'Add any initialization after the InitializeComponent() call

        End Sub

        'Form overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified imports the Windows Form Designer.  
        'Do not modify it imports the code editor.
        Friend WithEvents mainMenu1 As System.Windows.Forms.MainMenu
        Friend WithEvents FileAction As System.Windows.Forms.MenuItem
        Friend WithEvents FileExit As System.Windows.Forms.MenuItem
        Friend WithEvents HelpAction As System.Windows.Forms.MenuItem
        Friend WithEvents HelpAbout As System.Windows.Forms.MenuItem
        Friend WithEvents MoveI As System.Windows.Forms.Label
        Friend WithEvents label1 As System.Windows.Forms.Label
        Friend WithEvents label2 As System.Windows.Forms.Label
        Friend WithEvents HomeGrid As GamesGridCntrl.GamesGrid
        Friend WithEvents Info As System.Windows.Forms.ListBox
        Friend WithEvents Away As GamesGridCntrl.GamesGrid
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.mainMenu1 = New System.Windows.Forms.MainMenu()
            Me.FileAction = New System.Windows.Forms.MenuItem()
            Me.FileExit = New System.Windows.Forms.MenuItem()
            Me.HelpAction = New System.Windows.Forms.MenuItem()
            Me.HelpAbout = New System.Windows.Forms.MenuItem()
            Me.MoveI = New System.Windows.Forms.Label()
            Me.label1 = New System.Windows.Forms.Label()
            Me.label2 = New System.Windows.Forms.Label()
            Me.Away = New GamesGridCntrl.GamesGrid()
            Me.HomeGrid = New GamesGridCntrl.GamesGrid()
            Me.Info = New System.Windows.Forms.ListBox()
            Me.SuspendLayout()
            '
            'mainMenu1
            '
            Me.mainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.FileAction, Me.HelpAction})
            '
            'FileAction
            '
            Me.FileAction.Index = 0
            Me.FileAction.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.FileExit})
            Me.FileAction.Text = "File"
            '
            'FileExit
            '
            Me.FileExit.Index = 0
            Me.FileExit.Text = "Exit"
            '
            'HelpAction
            '
            Me.HelpAction.Index = 1
            Me.HelpAction.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.HelpAbout})
            Me.HelpAction.Text = "Help"
            '
            'HelpAbout
            '
            Me.HelpAbout.Index = 0
            Me.HelpAbout.Text = "About"
            '
            'MoveI
            '
            Me.MoveI.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.MoveI.Location = New System.Drawing.Point(200, 0)
            Me.MoveI.Name = "MoveI"
            Me.MoveI.Size = New System.Drawing.Size(216, 40)
            Me.MoveI.TabIndex = 7
            Me.MoveI.Text = "Your Move"
            Me.MoveI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'label1
            '
            Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.label1.Location = New System.Drawing.Point(32, 40)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(168, 24)
            Me.label1.TabIndex = 8
            Me.label1.Text = "My Ships"
            '
            'label2
            '
            Me.label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.label2.Location = New System.Drawing.Point(376, 40)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(168, 24)
            Me.label2.TabIndex = 9
            Me.label2.Text = "Opponents Ships"
            '
            'Away
            '
            Me.Away.ColoumsOnBoard = 8
            Me.Away.DarkSquare = System.Drawing.Color.Red
            Me.Away.LightSquare = System.Drawing.Color.White
            Me.Away.Location = New System.Drawing.Point(352, 80)
            Me.Away.Name = "Away"
            Me.Away.RowsOnBoard = 8
            Me.Away.Size = New System.Drawing.Size(288, 208)
            Me.Away.StandardBoard = False
            Me.Away.TabIndex = 11
            '
            'HomeGrid
            '
            Me.HomeGrid.ColoumsOnBoard = 8
            Me.HomeGrid.DarkSquare = System.Drawing.Color.Red
            Me.HomeGrid.LightSquare = System.Drawing.Color.White
            Me.HomeGrid.Location = New System.Drawing.Point(24, 80)
            Me.HomeGrid.Name = "HomeGrid"
            Me.HomeGrid.RowsOnBoard = 8
            Me.HomeGrid.Size = New System.Drawing.Size(288, 208)
            Me.HomeGrid.StandardBoard = False
            Me.HomeGrid.TabIndex = 12
            '
            'Info
            '
            Me.Info.Location = New System.Drawing.Point(112, 320)
            Me.Info.Name = "Info"
            Me.Info.Size = New System.Drawing.Size(440, 56)
            Me.Info.TabIndex = 13
            '
            'mainform
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(728, 409)
            Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Info, Me.HomeGrid, Me.Away, Me.label2, Me.label1, Me.MoveI})
            Me.Menu = Me.mainMenu1
            Me.Name = "mainform"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "VB.NET Version Simple Pier to Pier Game of Battleships"
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private Sub FileExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileExit.Click
            '' stop the threads
            '' strange but only way to check for <> Nothing !!
            If StartUp Is Nothing Then
            Else
                StartUp.Abort()
            End If
            If Play Is Nothing Then
            Else
                Play.Abort()
            End If
            If Connected Then
                GameReader.Close()
                GameWriter.Close()
                If (Server) Then
                    SSocket.Close()
                    SStream.Close()
                Else
                    CSocket.Close()
                    CStream.Close()

                End If
            Else
                If (Server) Then
                    tcpListener.Stop()
                End If
            End If
            Application.Exit()
        End Sub

        Private Sub HelpAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpAbout.Click
            MessageBox.Show(" CopyRight Tony Grimer" + vbCrLf + " Date June2004" + vbCrLf + " Example of a Sockets (Multi-Player) Based Game of BattleShips " + vbCrLf + " For Chapter 10 Students Essential Guide to .NET" + vbCrLf + " Version 1.0.0", "VB.NET Examples About")
        End Sub
        Private Sub CreateShips()
            Dim randomNumber As Random = New Random()
            Dim i As Integer = 0
            Dim j As Integer
            While i < 5
                j = randomNumber.Next(63)
                If HomeGrid.GetSQColour(j).ToString() <> System.Drawing.Color.Yellow.ToString Then
                    i = i + 1
                    HomeGrid.SetSqColour(j, System.Drawing.Color.Yellow)
                    HomeGrid.SetSqString(j, "Ship")
                End If
            End While
        End Sub
        Public Sub Setup()
            Try
                '' if this try works we act as a client.
                CSocket = New TcpClient()
                CSocket.Connect(Me.RemoteIpAdd, 5000)
                CStream = CSocket.GetStream()
                GameWriter = New BinaryWriter(CStream)
                GameReader = New BinaryReader(CStream)
                Info.Items.Add("Found another Player Connected")
                Player = 2  '' Set the player Number
                Connected = True
            Catch
                ''If we get no joy as client then set up to wait as a Server
                Info.Items.Add("Failed to find another Player Connected")
                CSocket = Nothing  '' kill the client socket...
                CStream = Nothing
                Server = True
                Player = 1
                Info.Items.Add("Waiting for another Player to Connect")
                tcpListener.Start()
                SSocket = tcpListener.AcceptSocket()
                Connected = True
                SStream = New NetworkStream(SSocket)
                GameWriter = New BinaryWriter(SStream)
                GameReader = New BinaryReader(SStream)
                Info.Items.Add("Another Player has Connected")
                MyTurn = True
            End Try
            '' We can let this thread exit now as we have got a connection sorted 
            Me.Text = Me.Text + "  Player  No  " + Player.ToString()
            If Player = 1 Then
                MoveI.Visible = True
            Else
                MoveI.Visible = False
            End If
            Play.Start()
        End Sub
        Private Sub PlayGame()
            ''thread that plays the game via the socket connection
            If Connected Then
                Try
                    While True
                        ProcessMessage(GameReader.ReadString())
                    End While
                Catch
                    MessageBox.Show("Other Player has Disconnected", "Error on Socket")
                End Try
            End If
        End Sub
        Private Sub ProcessMessage(ByVal Message As String)
            Dim Value As String
            Dim HitMiss As String
            If Message.StartsWith("Target = ") Then
                '' they are trying to hit my taget
                MoveI.Visible = Not MoveI.Visible
                MyTurn = Not MyTurn  '' ensures play alternates
                Value = Message.Substring(Message.IndexOf("=", 0) + 2)
                HitMiss = HomeGridPosition(Integer.Parse(Value))
                GameWriter.Write("Result = " + HitMiss)
            Else
                Away.SetSqString(LastMove, Message.Substring(Message.IndexOf("=", 0) + 2))

            End If
        End Sub
        Private Function HomeGridPosition(ByVal Position As Integer) As String
            If (HomeGrid.GetSQText(Position) = "") Then

                HomeGrid.SetSqColour(Position, System.Drawing.Color.Blue)
                Return "Miss"
            Else
                HomeGrid.SetSqColour(Position, System.Drawing.Color.Green)
                Return "Hit"
            End If
        End Function

        Private Sub Away_SquareClicked(ByVal sender As Object, ByVal e As GamesGridCntrl.GamesGrid.SquareEvent) Handles Away.SquareClicked
            '' This is where the users clicks
            If MyTurn Then
                If (Away.GetSQColour(e.Number).ToString() <> System.Drawing.Color.Red.ToString()) Then
                    Away.SetSqColour(e.Number, System.Drawing.Color.Red)
                    GameWriter.Write("Target = " + e.Number.ToString())
                    LastMove = e.Number
                    MyTurn = Not MyTurn
                    MoveI.Visible = Not MoveI.Visible
                End If
            End If
        End Sub
    End Class
End Namespace
