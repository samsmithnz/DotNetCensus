VERSION 5.00
Object = "{67397AA1-7FB1-11D0-B148-00A0C922E820}#6.0#0"; "MSADODC.OCX"
Begin VB.Form frm_tbl 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Choose DB Type, Table & Cols"
   ClientHeight    =   6630
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   7905
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6630
   ScaleWidth      =   7905
   StartUpPosition =   2  'CenterScreen
   Begin VB.Frame Frame5 
      Caption         =   "Choose Columns"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1575
      Left            =   120
      TabIndex        =   19
      Top             =   4920
      Width           =   7695
      Begin VB.ListBox List3 
         Height          =   1230
         Left            =   120
         TabIndex        =   20
         Top             =   240
         Width           =   7455
      End
   End
   Begin MSAdodcLib.Adodc Adodc1 
      Height          =   330
      Left            =   360
      Top             =   6360
      Visible         =   0   'False
      Width           =   1200
      _ExtentX        =   2117
      _ExtentY        =   582
      ConnectMode     =   0
      CursorLocation  =   3
      IsolationLevel  =   -1
      ConnectionTimeout=   15
      CommandTimeout  =   30
      CursorType      =   3
      LockType        =   3
      CommandType     =   8
      CursorOptions   =   0
      CacheSize       =   50
      MaxRecords      =   0
      BOFAction       =   0
      EOFAction       =   0
      ConnectStringType=   1
      Appearance      =   1
      BackColor       =   -2147483643
      ForeColor       =   -2147483640
      Orientation     =   0
      Enabled         =   -1
      Connect         =   ""
      OLEDBString     =   ""
      OLEDBFile       =   ""
      DataSourceName  =   ""
      OtherAttributes =   ""
      UserName        =   ""
      Password        =   ""
      RecordSource    =   ""
      Caption         =   "Adodc1"
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      _Version        =   393216
   End
   Begin VB.Frame Frame4 
      Caption         =   "Stored Procedures"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1815
      Left            =   3960
      TabIndex        =   18
      Top             =   3000
      Width           =   3855
      Begin VB.ListBox List2 
         Height          =   1230
         Left            =   120
         TabIndex        =   9
         Top             =   360
         Width           =   3615
      End
   End
   Begin VB.Frame Frame3 
      Caption         =   "Tables"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1815
      Left            =   120
      TabIndex        =   17
      Top             =   3000
      Width           =   3855
      Begin VB.ListBox List1 
         Height          =   1230
         Left            =   120
         TabIndex        =   8
         Top             =   360
         Width           =   3615
      End
   End
   Begin VB.Frame Frame2 
      Height          =   2895
      Left            =   3600
      TabIndex        =   11
      Top             =   0
      Width           =   4215
      Begin VB.ComboBox Combo1 
         Height          =   315
         Left            =   1920
         TabIndex        =   3
         Top             =   360
         Width           =   2175
      End
      Begin VB.TextBox Text1 
         Height          =   315
         Left            =   1920
         TabIndex        =   4
         Top             =   840
         Width           =   2175
      End
      Begin VB.TextBox Text2 
         Height          =   315
         IMEMode         =   3  'DISABLE
         Left            =   1920
         PasswordChar    =   "*"
         TabIndex        =   5
         Top             =   1320
         Width           =   2175
      End
      Begin VB.ComboBox Combo2 
         Height          =   315
         Left            =   1920
         TabIndex        =   6
         Top             =   1800
         Width           =   2175
      End
      Begin VB.CommandButton Command1 
         Caption         =   "&Connect"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   495
         Left            =   3120
         TabIndex        =   7
         Top             =   2280
         Width           =   975
      End
      Begin VB.FileListBox File1 
         Height          =   285
         Left            =   5400
         Pattern         =   "*.mdf"
         TabIndex        =   12
         Top             =   1080
         Visible         =   0   'False
         Width           =   135
      End
      Begin VB.Label Label1 
         Caption         =   "Available Servers"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   16
         Top             =   360
         Width           =   1695
      End
      Begin VB.Label Label2 
         Caption         =   "User Name"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   15
         Top             =   840
         Width           =   1695
      End
      Begin VB.Label Label3 
         Caption         =   "Password"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   14
         Top             =   1320
         Width           =   1695
      End
      Begin VB.Label Label4 
         Caption         =   "Available Database"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   13
         Top             =   1800
         Width           =   1695
      End
   End
   Begin VB.Frame Frame1 
      Height          =   2895
      Left            =   120
      TabIndex        =   10
      Top             =   0
      Width           =   3375
      Begin VB.OptionButton Option1 
         Caption         =   "Connect to SQL Server"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   0
         Top             =   240
         Width           =   2535
      End
      Begin VB.OptionButton Option2 
         Caption         =   "Connect to Oracle Server"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   1
         Top             =   600
         Width           =   2535
      End
      Begin VB.OptionButton Option3 
         Caption         =   "Connect to MS Access Database"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   2
         Top             =   960
         Width           =   3135
      End
   End
End
Attribute VB_Name = "frm_tbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Combo1_GotFocus()
Combo2.Clear
End Sub

Private Sub Combo2_GotFocus()
On Error GoTo l1:
If Combo1.Text = "" Then
    Exit Sub
End If
Me.MousePointer = vbHourglass
dbusername = Text1.Text
dbpswd = Text2.Text
If Combo1.Text = "." Then
machine_name = "TEAPACK"
ElseIf Combo1.Text = "." Then
machine_name = ""
Else
machine_name = Combo1.Text
End If
Adodc1.ConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=" & dbusername & ";PWD=" & dbpswd & ";Initial Catalog=" & "master" & ";Data Source=" & machine_name
Adodc1.RecordSource = "select * from sysdatabases"
Adodc1.Refresh
While Not Adodc1.Recordset.EOF = True
Combo2.AddItem Adodc1.Recordset.Fields("name")
Adodc1.Recordset.MoveNext
Wend
Combo2.Text = Combo2.List(0)
Me.MousePointer = vbNormal
Exit Sub
l1:
n = MsgBox(Err.Description, vbCritical)
Me.MousePointer = vbNormal
End Sub

Private Sub Command1_Click()
On Error GoTo l1
'
dbusername = ""
dbpswd = ""
db_name = ""
machine_name = ""
'
dbusername = Text1.Text
dbpswd = Text2.Text
db_name = Combo2.Text
machine_name = Combo1.Text
'
Adodc1.ConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=" & dbusername & ";PWD=" & dbpswd & ";Initial Catalog=" & db_name & ";Data Source=" & machine_name
    Adodc1.RecordSource = "select * from sysobjects where xtype = " & "'u'" & " order by name"
    Adodc1.Refresh
'
Me.MousePointer = vbHourglass
If Option1.Value = True Then
    iscon = True
End If
Me.MousePointer = vbNormal
'
If iscon = True Then
    Adodc1.RecordSource = "select * from sysobjects where xtype = " & "'u'" & " order by name"
    Adodc1.Refresh
    While Not Adodc1.Recordset.EOF = True
        List1.AddItem Adodc1.Recordset.Fields(0)
        Adodc1.Recordset.MoveNext
    Wend
    '
    Adodc1.RecordSource = "select * from sysobjects where xtype = " & "'p'" & " order by name"
    Adodc1.Refresh
    While Not Adodc1.Recordset.EOF = True
        List2.AddItem Adodc1.Recordset.Fields(0)
        Adodc1.Recordset.MoveNext
    Wend
    Me.Caption = "Object of Database: " & db_name & " of Server: " & " TEAPACK"
End If
'
Exit Sub
l1:
n = MsgBox(Err.Description, vbCritical, "Connection Error!")
Me.MousePointer = vbNormal
End Sub

Private Sub Form_Activate()
n = MsgBox("ONLY THE SQL SERVER DATABASE ACCESSING OPTIONS IS AVILABLE NOW!" & vbNewLine & "APPLY REQUIRED CODE FOR OTHER DATABASE", vbInformation, "INFORMATION")
End Sub

Private Sub List1_Click()
    List3.Clear
    If Not List1.ListCount <= 0 Then
        Adodc1.RecordSource = "select * from " & List1.Text
        Adodc1.Refresh
        For i = 0 To Adodc1.Recordset.Fields.Count - 1
            List3.AddItem Adodc1.Recordset.Fields(i).Name
        Next i
    End If
End Sub

Private Sub List3_Click()
If Not List3.ListCount <= 0 Then
    n = MsgBox("Want to Choose this field for summation?", vbExclamation + vbYesNo, "Confirmation")
    If n = 6 Then
        If Adodc1.Recordset.EOF = True Then
            MsgBox ("No Records exists")
            Exit Sub
        End If
        Me.MousePointer = vbHourglass
        While Not Adodc1.Recordset.EOF
            frm_calc.Text1.Text = frm_calc.Text1.Text & Adodc1.Recordset.Fields(List3.Text) & "+"
            Adodc1.Recordset.MoveNext
        Wend
        Me.MousePointer = vbNormal
        Unload Me
    End If
End If
End Sub

Private Sub Option2_Click()
n = MsgBox("IMPLEMENT AS PER REQUIREMENT", vbInformation, "")
End Sub

Private Sub Option3_Click()
n = MsgBox("IMPLEMENT AS PER REQUIREMENT", vbInformation, "")
End Sub
