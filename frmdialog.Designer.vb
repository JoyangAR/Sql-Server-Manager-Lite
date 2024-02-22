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
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.optfast = New System.Windows.Forms.RadioButton()
        Me.optforce = New System.Windows.Forms.RadioButton()
        Me.optnormal = New System.Windows.Forms.RadioButton()
        Me.CancelButton_Renamed = New System.Windows.Forms.Button()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'optfast
        '
        Me.optfast.BackColor = System.Drawing.SystemColors.Control
        Me.optfast.Checked = True
        Me.optfast.Cursor = System.Windows.Forms.Cursors.Default
        Me.optfast.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optfast.Location = New System.Drawing.Point(16, 8)
        Me.optfast.Name = "optfast"
        Me.optfast.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optfast.Size = New System.Drawing.Size(209, 25)
        Me.optfast.TabIndex = 4
        Me.optfast.TabStop = True
        Me.optfast.Text = "Fast mode"
        Me.optfast.UseVisualStyleBackColor = False
        '
        'optforce
        '
        Me.optforce.BackColor = System.Drawing.SystemColors.Control
        Me.optforce.Cursor = System.Windows.Forms.Cursors.Default
        Me.optforce.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optforce.Location = New System.Drawing.Point(16, 56)
        Me.optforce.Name = "optforce"
        Me.optforce.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optforce.Size = New System.Drawing.Size(209, 25)
        Me.optforce.TabIndex = 3
        Me.optforce.TabStop = True
        Me.optforce.Text = "Forced mode (some data maybe lost)"
        Me.optforce.UseVisualStyleBackColor = False
        '
        'optnormal
        '
        Me.optnormal.BackColor = System.Drawing.SystemColors.Control
        Me.optnormal.Cursor = System.Windows.Forms.Cursors.Default
        Me.optnormal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optnormal.Location = New System.Drawing.Point(16, 32)
        Me.optnormal.Name = "optnormal"
        Me.optnormal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optnormal.Size = New System.Drawing.Size(209, 25)
        Me.optnormal.TabIndex = 2
        Me.optnormal.TabStop = True
        Me.optnormal.Text = "Normal mode"
        Me.optnormal.UseVisualStyleBackColor = False
        '
        'CancelButton_Renamed
        '
        Me.CancelButton_Renamed.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton_Renamed.Cursor = System.Windows.Forms.Cursors.Default
        Me.CancelButton_Renamed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CancelButton_Renamed.Location = New System.Drawing.Point(160, 88)
        Me.CancelButton_Renamed.Name = "CancelButton_Renamed"
        Me.CancelButton_Renamed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CancelButton_Renamed.Size = New System.Drawing.Size(81, 25)
        Me.CancelButton_Renamed.TabIndex = 1
        Me.CancelButton_Renamed.Text = "Cancel"
        Me.CancelButton_Renamed.UseVisualStyleBackColor = False
        '
        'OKButton
        '
        Me.OKButton.BackColor = System.Drawing.SystemColors.Control
        Me.OKButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.OKButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OKButton.Location = New System.Drawing.Point(72, 88)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OKButton.Size = New System.Drawing.Size(81, 25)
        Me.OKButton.TabIndex = 0
        Me.OKButton.Text = "OK"
        Me.OKButton.UseVisualStyleBackColor = False
        '
        'frmdialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(249, 121)
        Me.ControlBox = False
        Me.Controls.Add(Me.optfast)
        Me.Controls.Add(Me.optforce)
        Me.Controls.Add(Me.optnormal)
        Me.Controls.Add(Me.CancelButton_Renamed)
        Me.Controls.Add(Me.OKButton)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(184, 250)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmdialog"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Repair Mode"
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class