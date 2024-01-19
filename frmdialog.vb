Option Strict Off
Option Explicit On
Friend Class frmdialog
	Inherits System.Windows.Forms.Form


	Private Sub CancelButton_Renamed_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CancelButton_Renamed.Click
		Me.Close()
	End Sub

	Private Sub OKButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OKButton.Click
		Dim pmode1 As Module1.pRepairMode

		If Me.optfast.Checked = True Then
			pmode1 = Module1.pRepairMode.pFast
		ElseIf Me.optforce.Checked = True Then
			pmode1 = Module1.pRepairMode.pForced
		ElseIf Me.optnormal.Checked = True Then
			pmode1 = Module1.pRepairMode.pStandard
		End If

		Me.Hide()
		frmmain.ProcessRepair(VB6.GetItemString(frmmain.lstdb, frmmain.lstdb.SelectedIndex), pmode1)
		Me.Close()
	End Sub
End Class