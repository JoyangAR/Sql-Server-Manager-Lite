Option Strict Off
Option Explicit On
Friend Class frmpwd
	Inherits System.Windows.Forms.Form
	
	
	Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
		Me.Close()
	End Sub
	
	Private Sub Command2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command2.Click
		
		If txtpwd.Text = "" Or txtrepeat.Text = "" Then
			MsgBox("Fill-up the empty fields", MsgBoxStyle.Exclamation, "")
			Exit Sub
		End If
		
		If txtpwd.Text <> txtrepeat.Text Then
			MsgBox("Password does not match", MsgBoxStyle.Exclamation, "")
			Exit Sub
		End If
		
		Me.Hide()

		Dim err1 As String = ""

		frmmain.Wait(True)

		' Ensure that there is a selected item in the ListBox and the password is not empty
		If frmmain.lstuser.SelectedIndex <> -1 AndAlso Not String.IsNullOrEmpty(Me.txtpwd.Text) Then
			Dim selectedUser As String = frmmain.lstuser.SelectedItem.ToString()

			' Call the ChangePwd function
			If ChangePwd(selectedUser, Me.txtpwd.Text, err1) Then
				frmmain.Logg("Password for user " & selectedUser & " changed!")
			Else
				frmmain.Logg("Failed to change password: " & err1)
			End If
		End If

		frmmain.Wait(False)
		
		Me.Close()
		
	End Sub
	
	Private Sub txtpwd_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtpwd.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		FilterInput(KeyAscii)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	Private Sub txtrepeat_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtrepeat.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		FilterInput(KeyAscii)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
End Class