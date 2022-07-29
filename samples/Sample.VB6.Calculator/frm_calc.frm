VERSION 5.00
Begin VB.Form frm_calc 
   Caption         =   "Advanced Calculator 1.0"
   ClientHeight    =   4380
   ClientLeft      =   60
   ClientTop       =   630
   ClientWidth     =   7395
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   ScaleHeight     =   4380
   ScaleWidth      =   7395
   StartUpPosition =   2  'CenterScreen
   Begin VB.Frame Frame4 
      Height          =   615
      Left            =   120
      TabIndex        =   9
      Top             =   2640
      Width           =   7215
      Begin VB.CommandButton Command2 
         Caption         =   "?"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   495
         Left            =   5760
         TabIndex        =   13
         Top             =   120
         Width           =   615
      End
      Begin VB.CommandButton Command1 
         Caption         =   "="
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   495
         Left            =   6480
         TabIndex        =   11
         Top             =   120
         Width           =   615
      End
      Begin VB.CheckBox Check1 
         Caption         =   "Apply Comma Seperator"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Left            =   120
         TabIndex        =   10
         Top             =   240
         Value           =   1  'Checked
         Width           =   2415
      End
   End
   Begin VB.Frame Frame3 
      Caption         =   "Output"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   855
      Left            =   120
      TabIndex        =   8
      Top             =   3360
      Width           =   7215
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         BorderStyle     =   1  'Fixed Single
         Caption         =   "0"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   495
         Left            =   120
         TabIndex        =   12
         Top             =   240
         Width           =   6975
      End
   End
   Begin VB.Frame Frame2 
      Caption         =   "Enter Expressions"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2535
      Left            =   1800
      TabIndex        =   6
      Top             =   120
      Width           =   5535
      Begin VB.TextBox Text1 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   2055
         Left            =   120
         TabIndex        =   7
         Top             =   360
         Width           =   5295
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Type"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2535
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   1575
      Begin VB.OptionButton Option5 
         Caption         =   "Mixed"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   5
         Top             =   1920
         Width           =   1215
      End
      Begin VB.OptionButton Option4 
         Caption         =   "/"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   4
         Top             =   1440
         Width           =   1215
      End
      Begin VB.OptionButton Option3 
         Caption         =   "*"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   3
         Top             =   1080
         Width           =   1215
      End
      Begin VB.OptionButton Option2 
         Caption         =   "-"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   2
         Top             =   720
         Width           =   1215
      End
      Begin VB.OptionButton Option1 
         Caption         =   "+"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   1
         Top             =   360
         Value           =   -1  'True
         Width           =   1215
      End
   End
   Begin VB.Menu file 
      Caption         =   "&File"
      Begin VB.Menu new 
         Caption         =   "&New"
         Shortcut        =   ^N
      End
      Begin VB.Menu open1 
         Caption         =   "&Import Text File"
         Shortcut        =   ^T
      End
      Begin VB.Menu open2 
         Caption         =   "&Import DB Table Coloumns"
         Shortcut        =   ^O
      End
   End
   Begin VB.Menu about 
      Caption         =   "&About"
   End
End
Attribute VB_Name = "frm_calc"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim str1(100000) As String
Dim i, j, k, p, tot As Long
Public temp, tmp As Double
Public st, results, type_op As String

Private Sub about_Click()
n = MsgBox("DESIGNED, CODED AND INTEGRATED BY SAYANTAN, M.C.A!" & vbNewLine & "WORKING AT SOFTWARE COMPANY, KOLKATA." & vbNewLine & "EMAIL : sentu_now@yahoo.co.in")
End Sub

Private Sub Check1_Click()
If Check1.Value = 1 Then
    Label1.Caption = Format(tmp, "0,000.00")
Else
    Label1.Caption = tmp
End If
End Sub

Private Sub Command1_Click()
On Error GoTo l1
If Text1.Text = "" Then
    Exit Sub
End If
If Mid(Text1.Text, Len(Text1.Text), 1) <> type_op Then
Text1.Text = Text1.Text & type_op
End If
    For i = 1 To Len(Text1.Text)
        If Mid(Text1.Text, i, 1) = type_op Then
            If st = "" Then
                str1(j) = Mid(Text1.Text, 1, i - 1)
                st = "1"
                k = i
                j = j + 1
                p = 0
                tot = tot + 1
            Else
                str1(j) = Mid(Text1.Text, k + 1, p - 1)
                k = i
                j = j + 1
                p = 0
                tot = tot + 1
            End If
        End If
        p = p + 1
    Next i
    If type_op = "+" Then
        For j = 0 To tot - 1
            temp = temp + CDbl(str1(j))
        Next j
    ElseIf type_op = "-" Then
        For j = 0 To tot - 1
            temp = temp - CDbl(str1(j))
        Next j
    ElseIf type_op = "*" Then
        temp = 1
        For j = 0 To tot - 1
            temp = temp * CDbl(str1(j))
        Next j
    ElseIf type_op = "/" Then
        For j = 0 To tot - 1
            If str1(j + 1) = "" Then
                Exit For
            End If
            temp = CDbl(str1(j + 1))
            temp = CDbl(str1(j)) / temp
        Next j
    End If
    If Check1.Value = 1 Then
        Label1.Caption = Format(temp, "0,000.00")
    Else
        Label1.Caption = temp
    End If
    tmp = temp
    'clear values
    For j = 0 To tot - 1
        str1(j) = ""
    Next j
    i = 0
    j = 0
    k = 0
    p = 0
    tot = 0
    temp = 0
    st = ""
Exit Sub
l1:
n = MsgBox("The System couldn't evaluate the expression! Check the expression!", vbExclamation, "Advanced Calculator 1.0")
    i = 0
    j = 0
    k = 0
    p = 0
    tot = 0
    temp = 0
    st = ""
End Sub

Private Sub Command2_Click()
n = MsgBox("EXPRESSION SHOULD BE LIKE : 2+2 OR 34.5+78+78", vbInformation, "INFORMATION")
End Sub

Private Sub Form_Load()
n = MsgBox("THIS IS AN DEMO VERSION! ONLY THE <ADD> FEATURES HAS BEEN ADDED TILL DATE" & vbNewLine & "ADD CODE FOR OTHER MODULE AS PER REQUIREMENT" & vbNewLine & "FEEL FREE TO CONTACT ME AT <sentu_now@yahoo.co.in>", vbExclamation, "INFORMATION")

type_op = "+"
Label1.Caption = Format(Label1.Caption, "0,000.00")
End Sub

Private Sub open2_Click()
frm_tbl.Show vbModal
End Sub

Private Sub Option1_Click()
If Option1.Value = True Then
    type_op = "+"
End If
End Sub

Private Sub Option2_Click()
If Option2.Value = True Then
    type_op = "-"
End If
End Sub

Private Sub Option3_Click()
If Option3.Value = True Then
    type_op = "*"
End If
End Sub

Private Sub Option4_Click()
If Option4.Value = True Then
    type_op = "/"
End If
End Sub

Private Sub Option5_Click()
If Option5.Value = True Then
    type_op = "M"
End If
End Sub

Private Sub Text1_KeyPress(KeyAscii As Integer)
If KeyAscii >= 48 And KeyAscii <= 57 Or KeyAscii = 43 Or KeyAscii = 46 Or KeyAscii = Asc("-") Or KeyAscii = Asc("*") Or KeyAscii = Asc("/") Or KeyAscii = 8 Then
Else
KeyAscii = 0
End If
End Sub

Private Sub Timer1_Timer()

End Sub
