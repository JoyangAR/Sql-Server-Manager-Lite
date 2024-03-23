<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmbakdir
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.optprevious = New System.Windows.Forms.RadioButton()
        Me.optnew = New System.Windows.Forms.RadioButton()
        Me.optdefault = New System.Windows.Forms.RadioButton()
        Me.CancelButton_Renamed = New System.Windows.Forms.Button()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'optprevious
        '
        Me.optprevious.BackColor = System.Drawing.SystemColors.Control
        Me.optprevious.Checked = True
        Me.optprevious.Cursor = System.Windows.Forms.Cursors.Default
        Me.optprevious.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optprevious.Location = New System.Drawing.Point(16, 8)
        Me.optprevious.Name = "optprevious"
        Me.optprevious.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optprevious.Size = New System.Drawing.Size(209, 25)
        Me.optprevious.TabIndex = 9
        Me.optprevious.TabStop = True
        Me.optprevious.Text = "Previous location"
        Me.optprevious.UseVisualStyleBackColor = False
        '
        'optnew
        '
        Me.optnew.BackColor = System.Drawing.SystemColors.Control
        Me.optnew.Cursor = System.Windows.Forms.Cursors.Default
        Me.optnew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optnew.Location = New System.Drawing.Point(16, 56)
        Me.optnew.Name = "optnew"
        Me.optnew.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optnew.Size = New System.Drawing.Size(209, 25)
        Me.optnew.TabIndex = 8
        Me.optnew.TabStop = True
        Me.optnew.Text = "New location"
        Me.optnew.UseVisualStyleBackColor = False
        '
        'optdefault
        '
        Me.optdefault.BackColor = System.Drawing.SystemColors.Control
        Me.optdefault.Cursor = System.Windows.Forms.Cursors.Default
        Me.optdefault.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optdefault.Location = New System.Drawing.Point(16, 32)
        Me.optdefault.Name = "optdefault"
        Me.optdefault.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optdefault.Size = New System.Drawing.Size(209, 25)
        Me.optdefault.TabIndex = 7
        Me.optdefault.TabStop = True
        Me.optdefault.Text = "Default location"
        Me.optdefault.UseVisualStyleBackColor = False
        '
        'CancelButton_Renamed
        '
        Me.CancelButton_Renamed.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton_Renamed.Cursor = System.Windows.Forms.Cursors.Default
        Me.CancelButton_Renamed.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelButton_Renamed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CancelButton_Renamed.Location = New System.Drawing.Point(160, 88)
        Me.CancelButton_Renamed.Name = "CancelButton_Renamed"
        Me.CancelButton_Renamed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CancelButton_Renamed.Size = New System.Drawing.Size(81, 25)
        Me.CancelButton_Renamed.TabIndex = 6
        Me.CancelButton_Renamed.Text = "Cancel"
        Me.CancelButton_Renamed.UseVisualStyleBackColor = False
        '
        'OKButton
        '
        Me.OKButton.BackColor = System.Drawing.SystemColors.Control
        Me.OKButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.OKButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OKButton.Location = New System.Drawing.Point(72, 88)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OKButton.Size = New System.Drawing.Size(81, 25)
        Me.OKButton.TabIndex = 5
        Me.OKButton.Text = "OK"
        Me.OKButton.UseVisualStyleBackColor = False
        '
        'frmbakdir
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(249, 121)
        Me.ControlBox = False
        Me.Controls.Add(Me.optprevious)
        Me.Controls.Add(Me.optnew)
        Me.Controls.Add(Me.optdefault)
        Me.Controls.Add(Me.CancelButton_Renamed)
        Me.Controls.Add(Me.OKButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmbakdir"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Add Mode"
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents optprevious As RadioButton
    Public WithEvents optnew As RadioButton
    Public WithEvents optdefault As RadioButton
    Public WithEvents CancelButton_Renamed As Button
    Public WithEvents OKButton As Button
End Class
