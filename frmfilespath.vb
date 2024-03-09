Public Class frmfilespath
    Dim newmdfpath As String = ""
    Dim newldfpath As String = ""

    Private Sub cmdmdfpath_Click(sender As Object, e As EventArgs) Handles cmdmdfpath.Click
        ' Create an instance of FolderBrowserDialog
        Using folderBrowser As New FolderBrowserDialog()
            ' Configure the FolderBrowserDialog if necessary
            folderBrowser.Description = "Select the default location for MDF files"

            ' Show the FolderBrowserDialog
            Dim result As DialogResult = folderBrowser.ShowDialog()

            ' Check if the user selected a folder and clicked OK
            If result = DialogResult.OK AndAlso Not String.IsNullOrWhiteSpace(folderBrowser.SelectedPath) Then
                ' Save the selected path in the variable newmdfpath
                newmdfpath = folderBrowser.SelectedPath
                frmmain.Logg("Selected path: " & newmdfpath)
                If ChangeDefaultDataLocation(newmdfpath) Then
                    TxtMDF.Text = newmdfpath
                End If
            Else
                ' Optional handling if the user cancels or doesn't select a folder
                MessageBox.Show("No folder selected.")
            End If
        End Using
    End Sub

    Private Sub cmdldfpath_Click(sender As Object, e As EventArgs) Handles cmdldfpath.Click
        ' Create an instance of FolderBrowserDialog
        Using folderBrowser As New FolderBrowserDialog()
            ' Configure the FolderBrowserDialog if necessary
            folderBrowser.Description = "Select the default location for LDF files"

            ' Show the FolderBrowserDialog
            Dim result As DialogResult = folderBrowser.ShowDialog()

            ' Check if the user selected a folder and clicked OK
            If result = DialogResult.OK AndAlso Not String.IsNullOrWhiteSpace(folderBrowser.SelectedPath) Then
                ' Save the selected path in the variable newldfpath
                newldfpath = folderBrowser.SelectedPath
                frmmain.Logg("Selected path: " & newldfpath)
                If ChangeDefaultLogLocation(newldfpath) Then
                    TxtLDF.Text = newldfpath
                End If
            Else
                ' Optional handling if the user cancels or doesn't select a folder
                MessageBox.Show("No folder selected.")
            End If
        End Using
    End Sub

    Private Sub cmdapply_Click(sender As Object, e As EventArgs) Handles cmdapply.Click
        If Not String.IsNullOrEmpty(newmdfpath) Then
            mdfpath = newmdfpath
            WriteXML()
            frmmain.Logg("Default MDF path set to: " & newmdfpath)
        End If
        If Not String.IsNullOrEmpty(newldfpath) Then
            ldfpath = newldfpath
            WriteXML()
            frmmain.Logg("Default LDF path set to: " & newldfpath)
        End If
        Me.Close()
    End Sub
End Class