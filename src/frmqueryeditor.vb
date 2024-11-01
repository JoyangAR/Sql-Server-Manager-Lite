Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions

Public Class frmqueryeditor

    ' Define the patterns for SQL keywords and their corresponding colors
    Private sqlKeywordsPatterns As Dictionary(Of String, Color) = New Dictionary(Of String, Color) From {
    {"\b(SELECT|FROM|WHERE|JOIN|INNER|LEFT|RIGHT|OUTER|FULL|CROSS)\b", Color.Blue},
    {"\b(ALTER|INDEX|CREATE|DROP|REBUILD|WITH|REORGANIZE)\b", Color.Brown},
    {"\b(AND|OR|NOT|IN|AS|ON|IS|NULL)\b", Color.Green},
    {"\b(GO|SET|USE|DELETE|UPDATE|INSERT|INTO|VALUES|CREATE|TABLE|VIEW|PROCEDURE|FUNCTION|TRIGGER)\b", Color.Red},
    {"\b(BEGIN|END|DECLARE|INT|VARCHAR|DATE|DATETIME|BOOLEAN|FLOAT|DOUBLE|DECIMAL)\b", Color.Purple},
    {"\b(CASE|WHEN|THEN|ELSE|END)\b", Color.Magenta},
    {"\b(GETDATE|DATEPART|DATEDIFF|DATEADD|CONVERT|CAST)\b", Color.DarkMagenta},
    {"\b(TOP|LIMIT|GROUP|BY|ORDER|HAVING|DISTINCT)\b", Color.Orange},
    {"\b(COMMIT|ROLLBACK|TRANSACTION|SAVE)\b", Color.DarkOrange},
    {"\b(UNION|ALL|INTERSECT|EXCEPT)\b", Color.DarkCyan},
    {"\b(EXEC|EXECUTE)\b", Color.Gray},
    {"\b(COUNT|SUM|AVG|MIN|MAX)\b", Color.Teal},
    {"\b(UPDATE STATISTICS|DBCC|RECONFIGURE)\b", Color.Sienna},
    {"\b(BACKUP|DATABASE|RESTORE)\b", Color.DarkSalmon},
    {"--.*|/\*[\s\S]*?\*/", Color.DarkGreen} ' For single-line and multi-line comments
}

    Private loadedFile As String
    Private allowClose As Boolean = True
    Private suspendColouring As Boolean = False

    Private Sub ColourLoadedText()
        suspendColouring = True
        If colourQE Then

            ' Disable the RichTextBox to prevent visual changes
            TxtQueryBox.Enabled = False

            ' Apply a default style to all text
            TxtQueryBox.SelectAll()
            TxtQueryBox.SelectionColor = TxtQueryBox.ForeColor

            ' Iterate through each group of keywords and apply highlighting
            For Each kvp As KeyValuePair(Of String, Color) In sqlKeywordsPatterns
                Dim regex As New Regex(kvp.Key, RegexOptions.IgnoreCase)
                Dim matches As MatchCollection = regex.Matches(TxtQueryBox.Text)

                For Each match As Match In matches
                    TxtQueryBox.Select(match.Index, match.Length)
                    TxtQueryBox.SelectionColor = kvp.Value
                Next
            Next

            ' Re-enable the RichTextBox
            TxtQueryBox.Enabled = True
        End If
        suspendColouring = False
    End Sub

    Private Sub TxtQueryBox_TextChanged(sender As Object, e As EventArgs) Handles TxtQueryBox.TextChanged
        If colourQE AndAlso Not suspendColouring Then
            Dim currentText As String = TxtQueryBox.Text
            Dim currentPos As Integer = TxtQueryBox.SelectionStart

            ' Check if the last entered character is a space or Enter and that it is not the first character
            If currentPos > 1 AndAlso (currentText(currentPos - 1) = " " OrElse currentText(currentPos - 1) = Chr(10)) Then
                ' Find the start of the last word and its length in one step
                Dim lastWordStart As Integer = currentText.LastIndexOfAny({" "c, Chr(10)}, currentPos - 2) + 1
                Dim wordLength As Integer = currentPos - lastWordStart

                ' Extract the last word and ensure it is not empty
                Dim lastWord As String = currentText.Substring(lastWordStart, wordLength).Trim()
                If String.IsNullOrWhiteSpace(lastWord) Then Exit Sub

                ' Identify the line where the last word is located
                Dim lineStart As Integer
                If lastWordStart > 0 Then
                    lineStart = currentText.LastIndexOf(Chr(10), lastWordStart - 1) + 1
                Else
                    lineStart = 0 ' For the very first line
                End If

                Dim lineText As String = currentText.Substring(lineStart)

                ' If the line starts with '--', set the entire line to green and skip keyword checks
                If lineText.StartsWith("--") Then
                    Dim lineEnd As Integer = currentText.IndexOf(Chr(10), lineStart)
                    If lineEnd = -1 Then lineEnd = currentText.Length
                    TxtQueryBox.Select(lineStart, lineEnd - lineStart)
                    TxtQueryBox.SelectionColor = Color.Green
                ElseIf lastWord.Equals("*/") Then
                    ' Call ColourLoadedText if the last word is */
                    ColourLoadedText()
                Else
                    ' If not a comment line, apply keyword coloring to the last word
                    TxtQueryBox.Select(lastWordStart, wordLength)
                    TxtQueryBox.SelectionColor = Color.Black

                    For Each kvp As KeyValuePair(Of String, Color) In sqlKeywordsPatterns
                        If Regex.IsMatch(lastWord, kvp.Key, RegexOptions.IgnoreCase) Then
                            TxtQueryBox.Select(lastWordStart, wordLength)
                            TxtQueryBox.SelectionColor = kvp.Value
                            Exit For
                        End If
                    Next
                End If
            End If

            ' Restore the cursor position
            TxtQueryBox.Select(currentPos, 0)
            TxtQueryBox.SelectionColor = Color.Black
            TxtQueryBox.Focus()
        End If
        allowClose = False
    End Sub

    Private Sub CmdFont(sender As Object, e As EventArgs) Handles FontMS.Click
        ' Gets the current font size
        Dim currentFont As Font = TxtQueryBox.SelectionFont

        ' If no text is selected, use the default font size
        If currentFont Is Nothing Then
            currentFont = TxtQueryBox.Font
        End If

        ' Displays a dialog box to select the new font size
        Using fontDialog As New FontDialog()
            fontDialog.Font = currentFont
            If fontDialog.ShowDialog() = DialogResult.OK Then
                ' Gets the new font size from the dialog box
                Dim newFont As Font = fontDialog.Font

                ' Changes the font size of the selected text (or the entire text box if no selection)
                TxtQueryBox.SelectionFont = newFont
            End If
        End Using
    End Sub

    Private Sub CmdSearch(sender As Object, e As EventArgs) Handles SearchMS.Click, SearchRC.Click
        suspendColouring = True
        Dim selectedText As String = If(Not String.IsNullOrEmpty(TxtQueryBox.SelectedText), TxtQueryBox.SelectedText, "")
        ' Displays a dialog box for the user to enter the search word
        Dim searchWord As String = InputBox("Search", "Search Input", selectedText)

        ' Checks if a word was entered
        If Not String.IsNullOrEmpty(searchWord) Then

            ' Clears previous highlighting
            TxtQueryBox.SelectAll()
            TxtQueryBox.SelectionBackColor = TxtQueryBox.BackColor
            TxtQueryBox.DeselectAll()

            ' Starts the search
            Dim startIndex As Integer = 0
            While startIndex < TxtQueryBox.TextLength
                Dim wordIndex As Integer = TxtQueryBox.Find(searchWord, startIndex, RichTextBoxFinds.None)
                If wordIndex = -1 Then Exit While

                ' Highlights the matching word
                TxtQueryBox.Select(wordIndex, searchWord.Length)
                TxtQueryBox.SelectionBackColor = Color.Yellow

                ' Moves the start index to continue the search
                startIndex = wordIndex + searchWord.Length
            End While
            suspendColouring = False
        End If
    End Sub

    Private Sub CmdCut(sender As Object, e As EventArgs) Handles CutMS.Click, CutRC.Click
        ' Checks if there is selected text in the RichTextBox
        If TxtQueryBox.SelectedText.Length > 0 Then
            ' Cuts the selected text
            TxtQueryBox.Cut()
            allowClose = False
        End If
    End Sub

    Private Sub CmdCopy(sender As Object, e As EventArgs) Handles CopyMS.Click, CopyRC.Click
        ' Checks if there is selected text in the RichTextBox
        If TxtQueryBox.SelectedText.Length > 0 Then
            ' Cuts the selected text
            TxtQueryBox.Copy()
        End If
    End Sub

    Private Sub CmdPaste(sender As Object, e As EventArgs) Handles PasteMS.Click, PasteRC.Click
        ' Check if there is text in the clipboard
        If Clipboard.ContainsText() Then
            ' Get the text from the clipboard
            Dim clipboardText As String = Clipboard.GetText()

            ' Save the current cursor position
            Dim originalSelectionStart As Integer = TxtQueryBox.SelectionStart

            ' Paste the text at the current cursor position, replacing any selected text
            TxtQueryBox.SelectedText = clipboardText

            ' Apply coloring to the newly pasted text
            If colourQE = True Then
                ColourLoadedText()
            End If

            ' Calculate the new cursor position
            Dim newSelectionStart As Integer = originalSelectionStart + clipboardText.Length

            ' Ensure it doesn't exceed the limits of the text
            If newSelectionStart > TxtQueryBox.Text.Length Then
                newSelectionStart = TxtQueryBox.Text.Length
            End If

            ' Adjust the cursor position considering line breaks
            Dim lineCount As Integer = clipboardText.Count(Function(c) c = vbLf) ' Count line breaks

            ' Restore the cursor position just after the pasted text
            TxtQueryBox.SelectionStart = newSelectionStart - lineCount
            TxtQueryBox.SelectionLength = 0 ' Ensure no text is selected

            ' Set focus back to the RichTextBox
            TxtQueryBox.Focus()

            allowClose = False
        End If
    End Sub

    Private Sub CmdDelete(sender As Object, e As EventArgs) Handles DeleteRC.Click
        ' Deletes the selected text in TxtQueryBox, if any
        If Not String.IsNullOrEmpty(TxtQueryBox.SelectedText) Then
            ' Sets the selected text to an empty string, effectively deleting it
            TxtQueryBox.SelectedText = ""
            allowClose = False
        End If
    End Sub

    Private Sub CmdReplace(sender As Object, e As EventArgs) Handles ReplaceMS.Click, ReplaceRC.Click
        ' Displays a custom dialog box to enter the text to search for and the replacement text
        Using replacementForm As New Form()
            replacementForm.Text = "Replace"
            replacementForm.Size = New Size(300, 150)
            replacementForm.StartPosition = FormStartPosition.CenterScreen

            Dim searchLabel As New Label()
            searchLabel.Text = "Search:"
            searchLabel.Location = New Point(10, 10)

            Dim searchTextBox As New TextBox()
            searchTextBox.Location = New Point(120, 10)
            searchTextBox.Size = New Size(150, 20)

            ' Pre-fills searchTextBox with the selected text in TxtQueryBox, if any
            If Not String.IsNullOrEmpty(TxtQueryBox.SelectedText) Then
                searchTextBox.Text = TxtQueryBox.SelectedText
            End If

            Dim replaceLabel As New Label()
            replaceLabel.Text = "Replace with:"
            replaceLabel.Location = New Point(10, 40)

            Dim replaceTextBox As New TextBox()
            replaceTextBox.Location = New Point(120, 40)
            replaceTextBox.Size = New Size(150, 20)

            Dim acceptButton As New Button()
            acceptButton.Text = "Ok"
            acceptButton.Location = New Point(120, 70)
            acceptButton.DialogResult = DialogResult.OK

            replacementForm.ShowIcon = False
            replacementForm.FormBorderStyle = FormBorderStyle.Fixed3D
            replacementForm.Controls.AddRange(New Control() {searchLabel, searchTextBox, replaceLabel, replaceTextBox, acceptButton})

            ' Displays the dialog box
            Dim result As DialogResult = replacementForm.ShowDialog()

            ' Performs the replacement if "Ok" was clicked
            If result = DialogResult.OK Then
                ' Obtains the text to search for and the replacement text
                Dim searchText As String = searchTextBox.Text
                Dim replaceText As String = replaceTextBox.Text

                ' Performs the replacement in the RichTextBox
                TxtQueryBox.Text = TxtQueryBox.Text.Replace(searchText, replaceText)
                allowClose = False
            End If
        End Using
    End Sub

    Private Sub CmdLoadQuery(sender As Object, e As EventArgs) Handles LoadQueryMS.Click
        ' Configures the OpenFileDialog
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Title = "Select SQL or TXT Files"
        openFileDialog.Filter = "SQL Files (*.sql)|*.sql|Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        openFileDialog.Multiselect = False ' Allows selecting only one file at a time

        ' Displays the dialog box and checks if a file was selected
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            ' Gets the name of the selected file
            Dim selectedFile As String = openFileDialog.FileName
            LoadQuery(selectedFile)
            allowClose = True
            ' Set starting point at the start of the document
            TxtQueryBox.SelectionStart = 0
            TxtQueryBox.Focus()
        End If
    End Sub

    Private Sub CmdSaveQuery(sender As Object, e As EventArgs) Handles SaveQueryMS.Click
        ' If a file has already been loaded, saves changes to the same file
        If Not String.IsNullOrEmpty(loadedFile) Then
            Try
                ' Saves the current content of the RichTextBox to the existing file
                File.WriteAllText(loadedFile, TxtQueryBox.Text)
                MessageBox.Show("Successfully saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information)
                allowClose = True
            Catch ex As Exception
                MessageBox.Show("Error saving file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            ' If no file has been previously loaded, allows the user to select the location
            ' and name of the file to save
            Dim saveFileDialog As New SaveFileDialog()
            saveFileDialog.Title = "Save File"
            saveFileDialog.Filter = "SQL Files (*.sql)|*.sql|Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            saveFileDialog.DefaultExt = ".sql"

            ' Displays the dialog box and checks if the user made a selection
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                ' Gets the full path of the selected file
                Dim filePath As String = saveFileDialog.FileName

                Try
                    ' Saves the current content of the RichTextBox to the new file
                    File.WriteAllText(filePath, TxtQueryBox.Text)
                    MessageBox.Show("Successfully saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Updates the name of the loaded file
                    loadedFile = filePath
                    allowClose = True
                Catch ex As Exception
                    MessageBox.Show("Error saving file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

    Private Sub CmdSaveQueryAs(sender As Object, e As EventArgs) Handles SaveQueryAsMS.Click
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Title = "Save File"
        saveFileDialog.Filter = "SQL Files (*.sql)|*.sql|Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        saveFileDialog.DefaultExt = ".sql"

        ' Displays the dialog box and checks if the user made a selection
        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            ' Gets the full path of the selected file
            Dim filePath As String = saveFileDialog.FileName

            Try
                ' Saves the current content of the RichTextBox to the new file
                File.WriteAllText(filePath, TxtQueryBox.Text)
                MessageBox.Show("Successfully saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Updates the name of the loaded file
                loadedFile = filePath

                allowClose = True
            Catch ex As Exception
                MessageBox.Show("Error saving file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub CmdExit(sender As Object, e As EventArgs) Handles ExitMS.Click
        Me.Close()
    End Sub

    Private Sub CmdExecute(sender As Object, e As EventArgs) Handles ExecuteMS.Click, ExecuteRC.Click
        Dim errMessage As String = ""
        Dim queryResult As String = ""
        If ExecuteQuery(TxtQueryBox.Text, queryResult, errMessage) Then
            If Not String.IsNullOrEmpty(TxtQueryBox.Text) Then
                TxtResult.Text = ""
            End If
            TxtResult.Text = queryResult
            If logtofile Then frmmain.Logg(queryResult)
        Else
            TxtResult.Text = errMessage
            If logtofile Then frmmain.Logg(errMessage)
        End If
        Const adjustmentHeight As Integer = 96 ' The height to adjust TxtQueryBox by
        If Not TxtResult.Visible = True Then TxtQueryBox.Height -= adjustmentHeight
        TxtResult.Visible = True
        frmmain.LoadDatabase()
    End Sub

    Private Sub QueryEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CenterToScreen()
        TxtQueryBox.ContextMenuStrip = ContextMenuStrip1
    End Sub

    Private Sub GotKeyDown(sender As Object, e As KeyEventArgs) Handles TxtQueryBox.KeyDown
        ' Check if the Ctrl key is pressed along with another key
        If e.Control Then
            Select Case e.KeyCode
                Case Keys.E
                    ' Call the CmdExecute method
                    CmdExecute(sender, e)
                Case Keys.V
                    ' Call the CmdPaste method
                    CmdPaste(sender, e)
                Case Keys.C
                    ' Call the CmdCopy method
                    CmdCopy(sender, e)
                Case Keys.F
                    ' Call the CmdSearch method
                    CmdSearch(sender, e)
                Case Keys.X
                    ' Call the CmdCut method
                    CmdCut(sender, e)
                Case Keys.R
                    ' Call the CmdReplace method
                    CmdReplace(sender, e)
                Case Keys.S
                    ' Call the CmdSaveQuery method
                    CmdSaveQuery(sender, e)
                Case Keys.Z
                    ' Disable Undo & Redo if colour text enabled
                    If Not colourQE Then
                        If Not e.Shift Then TxtQueryBox.Undo()  Else TxtQueryBox.Redo()
                    End If
            End Select

            ' Optionally: Prevent the event from propagating further
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub TxtQueryBox_MouseClick(sender As Object, e As MouseEventArgs) Handles TxtQueryBox.MouseClick
        ' Checks if the right mouse button was pressed
        If e.Button = MouseButtons.Right Then
            ' Displays the ContextMenuStrip at the cursor position
            ContextMenuStrip1.Show(TxtQueryBox, e.Location)
        End If
    End Sub

    Public Sub QueryInput_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not allowClose = True Then
            Dim confirmation As DialogResult = MessageBox.Show("Are you sure you want to exit? If you press 'No', the 'Save As' window will open to save any unsaved changes.", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If confirmation = DialogResult.Yes Then
                allowClose = True
                Me.Close()
            Else
                e.Cancel = True
                CmdSaveQueryAs(sender, e)
            End If
        End If
    End Sub

    Private Sub CmdShowHideResult() Handles ResultBoxMS.Click
        Const adjustmentHeight As Integer = 96 ' The height to adjust TxtQueryBox by

        If TxtResult.Visible Then
            ' If TxtResult is visible, hide it and increase the height of TxtQueryBox
            TxtResult.Visible = False
            TxtQueryBox.Height += adjustmentHeight
        Else
            ' If TxtResult is hidden, show it and decrease the height of TxtQueryBox
            TxtResult.Visible = True
            TxtQueryBox.Height -= adjustmentHeight
        End If
    End Sub

    Private Sub LoadQuery(FileToLoad As String)
        ' Tries to read the file content
        Try
            Dim fileContent As String = File.ReadAllText(FileToLoad)

            ' Checks the length of the file content
            If fileContent.Length > 2147483647 Then ' Int32.MaxValue
                MessageBox.Show("The file is too large to be loaded.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                loadedFile = FileToLoad
                ' Places the content in the RichTextBox if it doesn't exceed the limit
                TxtQueryBox.Text = fileContent
                Me.Text = System.IO.Path.GetFileName(FileToLoad)
                ColourLoadedText()
            End If
        Catch ex As Exception
            MessageBox.Show("Error reading file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' This event handles the action of dropping a file onto the control
    Private Sub TxtQueryBox_DragDrop(sender As Object, e As DragEventArgs) Handles TxtQueryBox.DragDrop
        ' Check if the dragged data is of file type
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            ' Get the list of dragged files (can be multiple)
            Dim files As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())

            ' Check if more than one file was selected
            If files.Length > 1 Then
                MessageBox.Show("Multiple files cannot be dragged. Please select only one.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                If files(0).EndsWith(".sql", StringComparison.OrdinalIgnoreCase) Then
                    ' Load query with first line of file
                    LoadQuery(files(0))
                    allowClose = True
                End If
            End If
        End If
    End Sub

    ' This event allows file dragging
    Private Sub TxtQueryBox_DragEnter(sender As Object, e As DragEventArgs) Handles TxtQueryBox.DragEnter
        ' Check if the data format is file; if so, allow the drag operation
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub


End Class