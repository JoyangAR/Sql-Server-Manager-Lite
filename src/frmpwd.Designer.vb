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
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtrepeat = New System.Windows.Forms.TextBox()
        Me.Command2 = New System.Windows.Forms.Button()
        Me.Command1 = New System.Windows.Forms.Button()
        Me.txtpwd = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtrepeat
        '
        Me.txtrepeat.AcceptsReturn = True
        Me.txtrepeat.BackColor = System.Drawing.SystemColors.Window
        Me.txtrepeat.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtrepeat.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtrepeat.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtrepeat.Location = New System.Drawing.Point(113, 40)
        Me.txtrepeat.MaxLength = 0
        Me.txtrepeat.Name = "txtrepeat"
        Me.txtrepeat.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtrepeat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtrepeat.Size = New System.Drawing.Size(176, 20)
        Me.txtrepeat.TabIndex = 4
        '
        'Command2
        '
        Me.Command2.BackColor = System.Drawing.SystemColors.Control
        Me.Command2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command2.Location = New System.Drawing.Point(120, 72)
        Me.Command2.Name = "Command2"
        Me.Command2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command2.Size = New System.Drawing.Size(81, 25)
        Me.Command2.TabIndex = 2
        Me.Command2.Text = "OK"
        Me.Command2.UseVisualStyleBackColor = False
        '
        'Command1
        '
        Me.Command1.BackColor = System.Drawing.SystemColors.Control
        Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command1.Location = New System.Drawing.Point(208, 72)
        Me.Command1.Name = "Command1"
        Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command1.Size = New System.Drawing.Size(81, 25)
        Me.Command1.TabIndex = 1
        Me.Command1.Text = "Cancel"
        Me.Command1.UseVisualStyleBackColor = False
        '
        'txtpwd
        '
        Me.txtpwd.AcceptsReturn = True
        Me.txtpwd.BackColor = System.Drawing.SystemColors.Window
        Me.txtpwd.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtpwd.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtpwd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtpwd.Location = New System.Drawing.Point(113, 8)
        Me.txtpwd.MaxLength = 0
        Me.txtpwd.Name = "txtpwd"
        Me.txtpwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpwd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtpwd.Size = New System.Drawing.Size(176, 20)
        Me.txtpwd.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(99, 20)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Repeat Password:"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(8, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(99, 20)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "New Password:"
        '
        'frmpwd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(297, 105)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtrepeat)
        Me.Controls.Add(Me.Command2)
        Me.Controls.Add(Me.Command1)
        Me.Controls.Add(Me.txtpwd)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(184, 250)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmpwd"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change password"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
End Class