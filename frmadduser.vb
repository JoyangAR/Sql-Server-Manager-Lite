Option Strict Off
Option Explicit On
Friend Class frmadduser
	Inherits System.Windows.Forms.Form
	
	
	Private Sub CancelButton_Renamed_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CancelButton_Renamed.Click
		Me.Close()
	End Sub
	
	Private Sub OKButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OKButton.Click
		
		If txtname.Text = "" Or txtpwd.Text = "" Then
			MsgBox("Fill-up the empty fields", MsgBoxStyle.Exclamation, "")
			Exit Sub
		End If

		Dim err1 As String = ""

		Me.Hide()
		
		frmmain.Wait(True)
		
		If CreateAccount((Me.txtname.Text), (Me.txtpwd.Text), err1) = True Then
			frmmain.Logg("New user added: " & Me.txtname.Text)
			frmmain.LoadUser()
		Else
			frmmain.Logg("Failed to add user: " & err1)
		End If
		
		frmmain.Wait(False)
		
		Me.Close()
		
	End Sub
	
	Private Sub txtname_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtname.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		FilterInput(KeyAscii)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	Private Sub txtpwd_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtpwd.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		FilterInput(KeyAscii)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
End Class