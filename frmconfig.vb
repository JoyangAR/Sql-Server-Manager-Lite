Public Class frmconfig
    Private Sub cmddataloc_Click(sender As Object, e As EventArgs) Handles cmddataloc.Click
        Dim defaultDataPath As String = String.Empty
        Dim defaultLogPath As String = String.Empty
        If GetDefaultDataAndLogLocations(defaultDataPath, defaultLogPath) Then
            frmfilespath.TxtMDF.Text = defaultDataPath
            frmfilespath.TxtLDF.Text = defaultLogPath
            frmfilespath.ShowDialog()
        End If
    End Sub

    Private Sub CmdApply_Click(sender As Object, e As EventArgs) Handles CmdApply.Click
        autologin = chkautologin.CheckState
        logtofile = chklogtofile.Checked
        colourQE = chkcolourQE.Checked
        disableRND = chkdisablerandom.Checked
        UpdCheck = chkUpdates.Checked
        WriteXML()
    End Sub

    Private Sub frmconfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        chkautologin.Checked = autologin
        chklogtofile.Checked = logtofile
        chkcolourQE.Checked = colourQE
        chkdisablerandom.Checked = disableRND
        chkUpdates.Checked = UpdCheck
    End Sub

    Private Sub cmdCheckUpd_Click(sender As Object, e As EventArgs) Handles cmdCheckUpd.Click
        CheckForUpdates()
    End Sub
End Class