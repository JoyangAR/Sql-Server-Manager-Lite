Friend Class frmshrinkdialog
	Private Sub CancelButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CancelButton.Click
		Me.Close()
	End Sub

	Private Sub OKButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OKButton.Click
		Dim pmode1 As pRepairMode

		If Me.optrelease.Checked = True Then
			pmode1 = pShrinkMode.pReleaseUnused
		ElseIf Me.optreorganize.Checked = True Then
			pmode1 = pShrinkMode.pReorganizeFirst
		ElseIf Me.optempty.Checked = True Then
			pmode1 = pShrinkMode.pEmptyLog
		End If

		Me.Hide()
		' Ensure that there is a selected item in the ListBox
		If frmmain.lstdb.SelectedIndex <> -1 Then
			Dim selectedDatabase As String = DirectCast(frmmain.lstdb.SelectedItem, Object).DatabaseName
			frmmain.ProcessShrink(selectedDatabase, pmode1)
		End If
		Me.Close()
	End Sub
End Class