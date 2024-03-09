Imports System.Collections.Generic
Imports System.IO
Imports System.Text.RegularExpressions

Public Class frmqueryeditor

    ' Define the patterns for SQL keywords and their corresponding colors
    Private sqlKeywordsPatterns As Dictionary(Of String, Color) = New Dictionary(Of String, Color) From {
    {"\b(SELECT|FROM|WHERE|JOIN|INNER JOIN|LEFT JOIN|RIGHT JOIN|OUTER JOIN|FULL JOIN|CROSS JOIN)\b", Color.Blue},
    {"\b(AND|OR|NOT|IN|AS|ON|IS|NULL)\b", Color.Green},
    {"\b(GO|SET|USE|DELETE|UPDATE|INSERT|INTO|VALUES|CREATE|TABLE|VIEW|PROCEDURE|FUNCTION|TRIGGER)\b", Color.Red},
    {"\b(BEGIN|END|DECLARE|INT|VARCHAR|DATE|DATETIME|BOOLEAN|FLOAT|DOUBLE|DECIMAL)\b", Color.Purple},
    {"\b(CASE|WHEN|THEN|ELSE|END)\b", Color.Magenta},
    {"\b(TOP|LIMIT|GROUP BY|ORDER BY|HAVING|DISTINCT)\b", Color.Orange},
    {"\b(UNION|UNION ALL|INTERSECT|EXCEPT)\b", Color.DarkCyan},
    {"\b(EXEC|EXECUTE)\b", Color.Gray},
    {"\b(COUNT|SUM|AVG|MIN|MAX)\b", Color.Teal},
    {"--.*|/\*[\s\S]*?\*/", Color.DarkGreen} ' For single-line and multi-line comments
}

    Private loadedFile As String
    Private endNow As Boolean
    Private suspended As Boolean = False

    Private Sub ColourLoadedText()
        suspended = True
        If colourQE = True Then
            ' Saves the current cursor position
            Dim originalSelectionStart As Integer = TxtQueryBox.SelectionStart
            Dim originalSelectionLength As Integer = TxtQueryBox.SelectionLength

            ' Applies a default style to all text
            TxtQueryBox.SelectAll()
            TxtQueryBox.SelectionColor = TxtQueryBox.ForeColor ' Sets the default color

            ' Iterates through each group of keywords and applies highlighting
            For Each kvp As KeyValuePair(Of String, Color) In sqlKeywordsPatterns
                Dim regex As New Regex(kvp.Key, RegexOptions.IgnoreCase)
                Dim matches As MatchCollection = regex.Matches(TxtQueryBox.Text)

                For Each match As Match In matches
                    TxtQueryBox.Select(match.Index, match.Length)
                    TxtQueryBox.SelectionColor = kvp.Value
                Next
            Next

            ' Restores the original cursor position and selection
            TxtQueryBox.Select(originalSelectionStart, originalSelectionLength)
            TxtQueryBox.SelectionColor = TxtQueryBox.ForeColor

            suspended = False
        End If
    End Sub

    Private Sub TxtQueryBox_TextChanged(sender As Object, e As EventArgs) Handles TxtQueryBox.TextChanged
        If suspended = False Then
            ' Defines the range for highlighting
            Const HighlightRange As Integer = 50 ' Number of characters to check before and after the current point

            ' Saves the current cursor position
            Dim originalSelectionStart As Integer = TxtQueryBox.SelectionStart
            Dim originalSelectionLength As Integer = TxtQueryBox.SelectionLength

            ' Recalculates the text range based on the current cursor position
            Dim startRange As Integer = Math.Max(0, originalSelectionStart - HighlightRange)
            Dim endRange As Integer = Math.Min(TxtQueryBox.TextLength, originalSelectionStart + HighlightRange + 1) ' +1 to include the last character
            Dim textRange As String = TxtQueryBox.Text.Substring(startRange, endRange - startRange)

            ' First, reset the color of the changed range to the default color to avoid unintentional coloring
            TxtQueryBox.Select(startRange, textRange.Length)
            TxtQueryBox.SelectionColor = Color.Black ' Or any default color of your choice

            ' Iterates through each group of keywords and comments to apply highlighting
            For Each kvp As KeyValuePair(Of String, Color) In sqlKeywordsPatterns
                Dim regex As New Regex(kvp.Key, RegexOptions.IgnoreCase)
                Dim matches As MatchCollection = regex.Matches(textRange)

                For Each match As Match In matches
                    ' Select and color only the matching text
                    TxtQueryBox.Select(startRange + match.Index, match.Length)
                    TxtQueryBox.SelectionColor = kvp.Value
                Next
            Next

            ' Restore the original cursor position and selection without changing the text color
            TxtQueryBox.Select(originalSelectionStart, originalSelectionLength)
            TxtQueryBox.SelectionColor = Color.Black

        End If
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
        suspended = True
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
            suspended = False
        End If
    End Sub

    Private Sub CmdCut(sender As Object, e As EventArgs) Handles CutMS.Click, CutRC.Click
        ' Checks if there is selected text in the RichTextBox
        If TxtQueryBox.SelectedText.Length > 0 Then
            ' Cuts the selected text
            TxtQueryBox.Cut()
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
        ' Checks if there is text in the clipboard
        If Clipboard.ContainsText() Then
            ' Gets the text from the clipboard
            Dim clipboardText As String = Clipboard.GetText()

            ' Pastes the text at the current cursor position replacing the selected text
            TxtQueryBox.SelectedText = clipboardText

            ' Applies coloring to the newly pasted text
            ' Note: This is a simplified approach and may require adjustments to optimize performance.
            ColourLoadedText()
        End If
    End Sub

    Private Sub CmdDelete(sender As Object, e As EventArgs) Handles DeleteRC.Click
        ' Deletes the selected text in TxtQueryBox, if any
        If Not String.IsNullOrEmpty(TxtQueryBox.SelectedText) Then
            ' Sets the selected text to an empty string, effectively deleting it
            TxtQueryBox.SelectedText = ""
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
            loadedFile = selectedFile
            ' Tries to read the file content
            Try
                Dim fileContent As String = File.ReadAllText(selectedFile)

                ' Checks the length of the file content
                If fileContent.Length > 2147483647 Then ' Int32.MaxValue
                    MessageBox.Show("The file is too large to be loaded.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    ' Places the content in the RichTextBox if it doesn't exceed the limit
                    TxtQueryBox.Text = fileContent
                    Me.Text = System.IO.Path.GetFileName(selectedFile)
                    ColourLoadedText()
                End If
            Catch ex As Exception
                MessageBox.Show("Error reading file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub CmdSaveQuery(sender As Object, e As EventArgs) Handles SaveQueryMS.Click
        ' If a file has already been loaded, saves changes to the same file
        If Not String.IsNullOrEmpty(loadedFile) Then
            Try
                ' Saves the current content of the RichTextBox to the existing file
                File.WriteAllText(loadedFile, TxtQueryBox.Text)
                MessageBox.Show("Successfully saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        Else
            TxtResult.Text = errMessage
        End If
        Const adjustmentHeight As Integer = 96 ' The height to adjust TxtQueryBox by
        TxtResult.Visible = True
        TxtQueryBox.Height -= adjustmentHeight
    End Sub

    Private Sub QueryEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CenterToScreen()
        TxtQueryBox.ContextMenuStrip = ContextMenuStrip1
    End Sub

    Private Sub TxtQueryBox_MouseClick(sender As Object, e As MouseEventArgs) Handles TxtQueryBox.MouseClick
        ' Checks if the right mouse button was pressed
        If e.Button = MouseButtons.Right Then
            ' Displays the ContextMenuStrip at the cursor position
            ContextMenuStrip1.Show(TxtQueryBox, e.Location)
        End If
    End Sub

    Public Sub QueryInput_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not endNow = True Then
            Dim confirmation As DialogResult = MessageBox.Show("Are you sure you want to exit? If you press 'No', the 'Save As' window will open to save any unsaved changes.", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If confirmation = DialogResult.Yes Then
                endNow = True
                Me.Close()
            Else
                e.Cancel = True
                CmdSaveQueryAs(sender, e)
            End If
        Else
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


End Class