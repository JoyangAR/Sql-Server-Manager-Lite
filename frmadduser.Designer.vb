<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmadduser
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
	Public WithEvents txtpwd As System.Windows.Forms.TextBox
	Public WithEvents txtname As System.Windows.Forms.TextBox
	Public WithEvents CancelButton_Renamed As System.Windows.Forms.Button
	Public WithEvents OKButton As System.Windows.Forms.Button
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmadduser))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.txtpwd = New System.Windows.Forms.TextBox
		Me.txtname = New System.Windows.Forms.TextBox
		Me.CancelButton_Renamed = New System.Windows.Forms.Button
		Me.OKButton = New System.Windows.Forms.Button
		Me.Label3 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.Text = "Add user"
		Me.ClientSize = New System.Drawing.Size(281, 105)
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
		Me.Name = "frmadduser"
		Me.txtpwd.AutoSize = False
		Me.txtpwd.Size = New System.Drawing.Size(209, 25)
		Me.txtpwd.IMEMode = System.Windows.Forms.ImeMode.Disable
		Me.txtpwd.Location = New System.Drawing.Point(64, 40)
		Me.txtpwd.PasswordChar = ChrW(42)
		Me.txtpwd.TabIndex = 1
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
		Me.txtname.AutoSize = False
		Me.txtname.Size = New System.Drawing.Size(209, 25)
		Me.txtname.Location = New System.Drawing.Point(64, 8)
		Me.txtname.TabIndex = 0
		Me.txtname.AcceptsReturn = True
		Me.txtname.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtname.BackColor = System.Drawing.SystemColors.Window
		Me.txtname.CausesValidation = True
		Me.txtname.Enabled = True
		Me.txtname.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtname.HideSelection = True
		Me.txtname.ReadOnly = False
		Me.txtname.Maxlength = 0
		Me.txtname.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtname.MultiLine = False
		Me.txtname.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtname.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtname.TabStop = True
		Me.txtname.Visible = True
		Me.txtname.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtname.Name = "txtname"
		Me.CancelButton_Renamed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.CancelButton_Renamed.Text = "Cancel"
		Me.CancelButton_Renamed.Size = New System.Drawing.Size(81, 25)
		Me.CancelButton_Renamed.Location = New System.Drawing.Point(192, 72)
		Me.CancelButton_Renamed.TabIndex = 3
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
		Me.OKButton.Location = New System.Drawing.Point(104, 72)
		Me.OKButton.TabIndex = 2
		Me.OKButton.BackColor = System.Drawing.SystemColors.Control
		Me.OKButton.CausesValidation = True
		Me.OKButton.Enabled = True
		Me.OKButton.ForeColor = System.Drawing.SystemColors.ControlText
		Me.OKButton.Cursor = System.Windows.Forms.Cursors.Default
		Me.OKButton.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.OKButton.TabStop = True
		Me.OKButton.Name = "OKButton"
		Me.Label3.Text = "Password:"
		Me.Label3.Size = New System.Drawing.Size(57, 17)
		Me.Label3.Location = New System.Drawing.Point(8, 40)
		Me.Label3.TabIndex = 5
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
		Me.Label2.Text = "Username:"
		Me.Label2.Size = New System.Drawing.Size(57, 17)
		Me.Label2.Location = New System.Drawing.Point(8, 8)
		Me.Label2.TabIndex = 4
		Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label2.BackColor = System.Drawing.SystemColors.Control
		Me.Label2.Enabled = True
		Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label2.UseMnemonic = True
		Me.Label2.Visible = True
		Me.Label2.AutoSize = False
		Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label2.Name = "Label2"
		Me.Controls.Add(txtpwd)
		Me.Controls.Add(txtname)
		Me.Controls.Add(CancelButton_Renamed)
		Me.Controls.Add(OKButton)
		Me.Controls.Add(Label3)
		Me.Controls.Add(Label2)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class