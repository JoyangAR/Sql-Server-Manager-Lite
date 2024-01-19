<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmpwd
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
	Public WithEvents txtrepeat As System.Windows.Forms.TextBox
	Public WithEvents Command2 As System.Windows.Forms.Button
	Public WithEvents Command1 As System.Windows.Forms.Button
	Public WithEvents txtpwd As System.Windows.Forms.TextBox
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmpwd))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.txtrepeat = New System.Windows.Forms.TextBox
		Me.Command2 = New System.Windows.Forms.Button
		Me.Command1 = New System.Windows.Forms.Button
		Me.txtpwd = New System.Windows.Forms.TextBox
		Me.Label1 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.Text = "Change password"
		Me.ClientSize = New System.Drawing.Size(297, 105)
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
		Me.Name = "frmpwd"
		Me.txtrepeat.AutoSize = False
		Me.txtrepeat.Size = New System.Drawing.Size(185, 25)
		Me.txtrepeat.IMEMode = System.Windows.Forms.ImeMode.Disable
		Me.txtrepeat.Location = New System.Drawing.Point(104, 40)
		Me.txtrepeat.PasswordChar = ChrW(42)
		Me.txtrepeat.TabIndex = 4
		Me.txtrepeat.AcceptsReturn = True
		Me.txtrepeat.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtrepeat.BackColor = System.Drawing.SystemColors.Window
		Me.txtrepeat.CausesValidation = True
		Me.txtrepeat.Enabled = True
		Me.txtrepeat.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtrepeat.HideSelection = True
		Me.txtrepeat.ReadOnly = False
		Me.txtrepeat.Maxlength = 0
		Me.txtrepeat.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtrepeat.MultiLine = False
		Me.txtrepeat.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtrepeat.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtrepeat.TabStop = True
		Me.txtrepeat.Visible = True
		Me.txtrepeat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtrepeat.Name = "txtrepeat"
		Me.Command2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.Command2.Text = "OK"
		Me.Command2.Size = New System.Drawing.Size(81, 25)
		Me.Command2.Location = New System.Drawing.Point(120, 72)
		Me.Command2.TabIndex = 2
		Me.Command2.BackColor = System.Drawing.SystemColors.Control
		Me.Command2.CausesValidation = True
		Me.Command2.Enabled = True
		Me.Command2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Command2.Cursor = System.Windows.Forms.Cursors.Default
		Me.Command2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Command2.TabStop = True
		Me.Command2.Name = "Command2"
		Me.Command1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.Command1.Text = "Cancel"
		Me.Command1.Size = New System.Drawing.Size(81, 25)
		Me.Command1.Location = New System.Drawing.Point(208, 72)
		Me.Command1.TabIndex = 1
		Me.Command1.BackColor = System.Drawing.SystemColors.Control
		Me.Command1.CausesValidation = True
		Me.Command1.Enabled = True
		Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Command1.TabStop = True
		Me.Command1.Name = "Command1"
		Me.txtpwd.AutoSize = False
		Me.txtpwd.Size = New System.Drawing.Size(185, 25)
		Me.txtpwd.IMEMode = System.Windows.Forms.ImeMode.Disable
		Me.txtpwd.Location = New System.Drawing.Point(104, 8)
		Me.txtpwd.PasswordChar = ChrW(42)
		Me.txtpwd.TabIndex = 0
		Me.txtpwd.AcceptsReturn = True
		Me.txtpwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtpwd.BackColor = System.Drawing.SystemColors.Window
		Me.txtpwd.CausesValidation = True
		Me.txtpwd.Enabled = True
		Me.txtpwd.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtpwd.HideSelection = True
		Me.txtpwd.ReadOnly = False
		Me.txtpwd.Maxlength = 0
		Me.txtpwd.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtpwd.MultiLine = False
		Me.txtpwd.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtpwd.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtpwd.TabStop = True
		Me.txtpwd.Visible = True
		Me.txtpwd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtpwd.Name = "txtpwd"
		Me.Label1.Text = "Repeat Password:"
		Me.Label1.Size = New System.Drawing.Size(89, 17)
		Me.Label1.Location = New System.Drawing.Point(8, 40)
		Me.Label1.TabIndex = 5
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label1.BackColor = System.Drawing.SystemColors.Control
		Me.Label1.Enabled = True
		Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label1.UseMnemonic = True
		Me.Label1.Visible = True
		Me.Label1.AutoSize = False
		Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label1.Name = "Label1"
		Me.Label3.Text = "New Password:"
		Me.Label3.Size = New System.Drawing.Size(81, 17)
		Me.Label3.Location = New System.Drawing.Point(8, 8)
		Me.Label3.TabIndex = 3
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label3.BackColor = System.Drawing.SystemColors.Control
		Me.Label3.Enabled = True
		Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label3.UseMnemonic = True
		Me.Label3.Visible = True
		Me.Label3.AutoSize = False
		Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label3.Name = "Label3"
		Me.Controls.Add(txtrepeat)
		Me.Controls.Add(Command2)
		Me.Controls.Add(Command1)
		Me.Controls.Add(txtpwd)
		Me.Controls.Add(Label1)
		Me.Controls.Add(Label3)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class