Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Public Class Form1
    Public ReadOnly Version As String = "0.1.0beta"
    Private Sub RichTextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RichTextBox1.TextChanged

        If RichTextBox1.TextLength > -1 Then 'Make sure there is some text in the RichTextBox first

            Highlight("function", Color.Red)
            Highlight("if", Color.Green)
            Highlight("else", Color.Green)
            Highlight("var", Color.Green)

        End If

    End Sub

    Private Sub Highlight(ByVal word As String, ByVal clr As Color)
        Dim reg As Regex = New Regex("\b" & word & "\b", RegexOptions.Multiline Or RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace)
        Dim m_ As MatchCollection = reg.Matches(RichTextBox1.Text)
        Dim pos As Integer = RichTextBox1.SelectionStart
        For Each m As Match In m_
            RichTextBox1.SelectionStart = m.Index
            RichTextBox1.SelectionLength = m.Length
            RichTextBox1.SelectionColor = clr
        Next
        RichTextBox1.SelectionStart = pos
        RichTextBox1.SelectionLength = 0
        RichTextBox1.SelectionColor = RichTextBox1.ForeColor
    End Sub

    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click
        RichTextBox1.Undo()
    End Sub

    Private Sub RedoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RedoToolStripMenuItem.Click
        RichTextBox1.Redo()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        RichTextBox1.Copy()
    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        RichTextBox1.Cut()
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        RichTextBox1.Paste()
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        RichTextBox1.SelectAll()
    End Sub

    Private Sub DeselectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeselectAllToolStripMenuItem.Click
        RichTextBox1.DeselectAll()
    End Sub

    Private Sub SaveCurrentFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveCurrentFileToolStripMenuItem.Click
        Dim sfd As New SaveFileDialog
        If sfd.ShowDialog = DialogResult.OK Then
            NewFileWriter(sfd.FileName)
        Else
            MessageBox.Show("Save file operation aborted. Can't save file.", "Save file operation aborted.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Async Sub NewFileWriter(filename As String)
        Try
            Dim sb As StringBuilder = New StringBuilder()
            sb.Append(RichTextBox1.Text)

            Using outfile As StreamWriter = New StreamWriter(filename + ".cpmf", True)
                Await outfile.WriteAsync(sb.ToString())
            End Using

            MessageBox.Show("File successfully saved!", "File saved!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("An error occured. Error: " & ex.Message & " - Please contact the developer for help and informations!", "An error occured while saving an file!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IO.Directory.Exists("C:\Craftbyte Mod IDE for MCPE") = False Then
            IO.Directory.CreateDirectory("C:\Craftbyte Mod IDE for MCPE")
            IO.Directory.CreateDirectory("C:\Craftbyte Mod IDE for MCPE\Files")
        End If
        ToolStripStatusLabel1.Text = "Version " & Version
        Me.Text = "Craftbyte Mod IDE for Minecraft Pocket Edition - Version " & Version
    End Sub

    Private Sub OpenFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFileToolStripMenuItem.Click
        Try
            Dim ofd As New OpenFileDialog
            If ofd.ShowDialog = DialogResult.OK Then
                Using reader As New IO.StreamReader(ofd.FileName)
                    RichTextBox1.Text = reader.ReadToEnd()
                    reader.Close()
                End Using
            End If
            Label4.Text = ofd.FileName
        Catch ex As Exception
            MessageBox.Show("An error occured. Error: " & ex.Message & " - Please contact the developer for help and informations!", "An error occured while saving an file!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NewFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewFileToolStripMenuItem.Click
        Dim frm As New Form1
        frm.Show()
    End Sub

    Private Async Sub CompileFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompileFileToolStripMenuItem.Click
        Dim sfd As New SaveFileDialog
        If sfd.ShowDialog = DialogResult.OK Then
            Dim sb As StringBuilder = New StringBuilder()
            sb.Append(RichTextBox1.Text)
            Try
                Using outfile As StreamWriter = New StreamWriter(sfd.FileName + ".js", True)
                    Await outfile.WriteAsync(sb.ToString())
                End Using
                Label4.Text = sfd.FileName
                MessageBox.Show("Successfully saved your Mod!", "Mod saved!", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("An error occured. Error: " & ex.Message & " - Please contact the developer for help and informations!", "An error occured while saving an file!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

    End Sub

    Private Sub FileManagerToolStripMenuItem_Click(sender As Object, e As EventArgs)
        MessageBox.Show("Coming soon! :)", "Coming soon...", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        MessageBox.Show("Craftbyte Mod IDE for Minecraft Pocket Edition - (C)2015 Craftbyte Developments" & Environment.NewLine & "Version " & Version, "About", MessageBoxButtons.OK, MessageBoxIcon.None)
    End Sub

    Private Sub RichTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles RichTextBox1.KeyDown

    End Sub
End Class
