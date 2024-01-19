<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmdialog
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents optfast As System.Windows.Forms.RadioButton
	Public WithEvents optforce As System.Windows.Forms.RadioButton
	Public WithEvents optnormal As System.Windows.Forms.RadioButton
	Public WithEvents CancelButton_Renamed As System.Windows.Forms.Button
	Public WithEvents OKButton As System.Windows.Forms.Button
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmdialog))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.optfast = New System.Windows.Forms.RadioButton
		Me.optforce = New System.Windows.Forms.RadioButton
		Me.optnormal = New System.Windows.Forms.RadioButton
		Me.CancelButton_Renamed = New System.Windows.Forms.Button
		Me.OKButton = New System.Windows.Forms.Button
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.Text = "Select Repair Mode"
		Me.ClientSize = New System.Drawing.Size(249, 121)
		Me.Location = New System.Drawing.Point(184, 250)
		Me.ControlBox = False
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.Enabled = True
		Me.KeyPreview = False
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "dialogfrm"
		Me.optfast.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.optfast.Text = "Fast mode"
		Me.optfast.Size = New System.Drawing.Size(209, 25)
		Me.optfast.Location = New System.Drawing.Point(16, 8)
		Me.optfast.TabIndex = 4
		Me.optfast.Checked = True
		Me.optfast.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.optfast.BackColor = System.Drawing.SystemColors.Control
		Me.optfast.CausesValidation = True
		Me.optfast.Enabled = True
		Me.optfast.ForeColor = System.Drawing.SystemColors.ControlText
		Me.optfast.Cursor = System.Windows.Forms.Cursors.Default
		Me.optfast.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.optfast.Appearance = System.Windows.Forms.Appearance.Normal
		Me.optfast.TabStop = True
		Me.optfast.Visible = True
		Me.optfast.Name = "optfast"
		Me.optforce.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.optforce.Text = "Forced mode (some data maybe lost)"
		Me.optforce.Size = New System.Drawing.Size(209, 25)
		Me.optforce.Location = New System.Drawing.Point(16, 56)
		Me.optforce.TabIndex = 3
		Me.optforce.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.optforce.BackColor = System.Drawing.SystemColors.Control
		Me.optforce.CausesValidation = True
		Me.optforce.Enabled = True
		Me.optforce.ForeColor = System.Drawing.SystemColors.ControlText
		Me.optforce.Cursor = System.Windows.Forms.Cursors.Default
		Me.optforce.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.optforce.Appearance = System.Windows.Forms.Appearance.Normal
		Me.optforce.TabStop = True
		Me.optforce.Checked = False
		Me.optforce.Visible = True
		Me.optforce.Name = "optforce"
		Me.optnormal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.optnormal.Text = "Normal mode"
		Me.optnormal.Size = New System.Drawing.Size(209, 25)
		Me.optnormal.Location = New System.Drawing.Point(16, 32)
		Me.optnormal.TabIndex = 2
		Me.optnormal.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.optnormal.BackColor = System.Drawing.SystemColors.Control
		Me.optnormal.CausesValidation = True
		Me.optnormal.Enabled = True
		Me.optnormal.ForeColor = System.Drawing.SystemColors.ControlText
		Me.optnormal.Cursor = System.Windows.Forms.Cursors.Default
		Me.optnormal.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.optnormal.Appearance = System.Windows.Forms.Appearance.Normal
		Me.optnormal.TabStop = True
		Me.optnormal.Checked = False
		Me.optnormal.Visible = True
		Me.optnormal.Name = "optnormal"
		Me.CancelButton_Renamed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.CancelButton_Renamed.Text = "Cancel"
		Me.CancelButton_Renamed.Size = New System.Drawing.Size(81, 25)
		Me.CancelButton_Renamed.Location = New System.Drawing.Point(160, 88)
		Me.CancelButton_Renamed.TabIndex = 1
		Me.CancelButton_Renamed.BackColor = System.Drawing.SystemColors.Control
		Me.CancelButton_Renamed.CausesValidation = True
		Me.CancelButton_Renamed.Enabled = True
		Me.CancelButton_Renamed.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CancelButton_Renamed.Cursor = System.Windows.Forms.Cursors.Default
		Me.CancelButton_Renamed.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CancelButton_Renamed.TabStop = True
		Me.CancelButton_Renamed.Name = "CancelButton_Renamed"
		Me.OKButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.OKButton.Text = "OK"
		Me.OKButton.Size = New System.Drawing.Size(81, 25)
		Me.OKButton.Location = New System.Drawing.Point(72, 88)
		Me.OKButton.TabIndex = 0
		Me.OKButton.BackColor = System.Drawing.SystemColors.Control
		Me.OKButton.CausesValidation = True
		Me.OKButton.Enabled = True
		Me.OKButton.ForeColor = System.Drawing.SystemColors.ControlText
		Me.OKButton.Cursor = System.Windows.Forms.Cursors.Default
		Me.OKButton.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.OKButton.TabStop = True
		Me.OKButton.Name = "OKButton"
		Me.Controls.Add(optfast)
		Me.Controls.Add(optforce)
		Me.Controls.Add(optnormal)
		Me.Controls.Add(CancelButton_Renamed)
		Me.Controls.Add(OKButton)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class