'' An example of a pier to Pier game imports a Socket connection
'' Implementation in VB.NET of Battleships
'' Uses the Previously derived Board Control
'' Also has a threaded model to avoid lock up.
'' Author Tony Grimer
'' For Chapter 10 of Essential guide to .NET
''****************************************************************************

'' Just a Dialog to get the remote IP address ...
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Net.Sockets
Imports System.Net
Imports System.IO

Namespace SimpleSocketsBasedBattleShips
    Public Class GetIp
        Inherits System.Windows.Forms.Form
        Private Mparent As mainform
#Region " Windows Form Designer generated code "

        Public Sub New(ByVal m As mainform)
            MyBase.New()
            InitializeComponent()
            Mparent = m
            IpAdd.Text = Mparent.RemoteIp
            'This call is required by the Windows Form Designer.
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
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        Friend WithEvents cmdOK As System.Windows.Forms.Button
        Friend WithEvents IpAdd As System.Windows.Forms.TextBox
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.cmdOK = New System.Windows.Forms.Button()
            Me.IpAdd = New System.Windows.Forms.TextBox()
            Me.SuspendLayout()
            '
            'cmdOK
            '
            Me.cmdOK.Location = New System.Drawing.Point(264, 29)
            Me.cmdOK.Name = "cmdOK"
            Me.cmdOK.Size = New System.Drawing.Size(56, 48)
            Me.cmdOK.TabIndex = 1
            Me.cmdOK.Text = "OK"
            '
            'IpAdd
            '
            Me.IpAdd.Location = New System.Drawing.Point(32, 40)
            Me.IpAdd.Name = "IpAdd"
            Me.IpAdd.Size = New System.Drawing.Size(184, 20)
            Me.IpAdd.TabIndex = 2
            Me.IpAdd.Text = ""
            '
            'GetIp
            '
            Me.AcceptButton = Me.cmdOK
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(358, 107)
            Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.IpAdd, Me.cmdOK})
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Name = "GetIp"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "IP Address to Play"
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
            Dim temp As IPAddress = IPAddress.Parse(IpAdd.Text)
            Mparent.RemoteIp = IpAdd.Text
            Mparent.RemoteIpAdd = temp
            Me.Close()
        End Sub
    End Class
End Namespace

