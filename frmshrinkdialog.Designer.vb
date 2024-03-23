<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmshrinkdialog
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
        Me.optrelease = New System.Windows.Forms.RadioButton()
        Me.optempty = New System.Windows.Forms.RadioButton()
        Me.optreorganize = New System.Windows.Forms.RadioButton()
        Me.CancelButton = New System.Windows.Forms.Button()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'optrelease
        '
        Me.optrelease.BackColor = System.Drawing.SystemColors.Control
        Me.optrelease.Checked = True
        Me.optrelease.Cursor = System.Windows.Forms.Cursors.Default
        Me.optrelease.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optrelease.Location = New System.Drawing.Point(16, 8)
        Me.optrelease.Name = "optrelease"
        Me.optrelease.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optrelease.Size = New System.Drawing.Size(209, 25)
        Me.optrelease.TabIndex = 9
        Me.optrelease.TabStop = True
        Me.optrelease.Text = "Release Unused"
        Me.optrelease.UseVisualStyleBackColor = False
        '
        'optempty
        '
        Me.optempty.BackColor = System.Drawing.SystemColors.Control
        Me.optempty.Cursor = System.Windows.Forms.Cursors.Default
        Me.optempty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optempty.Location = New System.Drawing.Point(16, 56)
        Me.optempty.Name = "optempty"
        Me.optempty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optempty.Size = New System.Drawing.Size(209, 25)
        Me.optempty.TabIndex = 8
        Me.optempty.TabStop = True
        Me.optempty.Text = "Empty Log"
        Me.optempty.UseVisualStyleBackColor = False
        '
        'optreorganize
        '
        Me.optreorganize.BackColor = System.Drawing.SystemColors.Control
        Me.optreorganize.Cursor = System.Windows.Forms.Cursors.Default
        Me.optreorganize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optreorganize.Location = New System.Drawing.Point(16, 32)
        Me.optreorganize.Name = "optreorganize"
        Me.optreorganize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optreorganize.Size = New System.Drawing.Size(209, 25)
        Me.optreorganize.TabIndex = 7
        Me.optreorganize.TabStop = True
        Me.optreorganize.Text = "Reorganize First"
        Me.optreorganize.UseVisualStyleBackColor = False
        '
        'CancelButton
        '
        Me.CancelButton.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.CancelButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CancelButton.Location = New System.Drawing.Point(160, 88)
        Me.CancelButton.Name = "CancelButton"
        Me.CancelButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CancelButton.Size = New System.Drawing.Size(81, 25)
        Me.CancelButton.TabIndex = 6
        Me.CancelButton.Text = "Cancel"
        Me.CancelButton.UseVisualStyleBackColor = False
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
        Me.OKButton.TabIndex = 5
        Me.OKButton.Text = "OK"
        Me.OKButton.UseVisualStyleBackColor = False
        '
        'frmshrinkdialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(249, 121)
        Me.Controls.Add(Me.optrelease)
        Me.Controls.Add(Me.optempty)
        Me.Controls.Add(Me.optreorganize)
        Me.Controls.Add(Me.CancelButton)
        Me.Controls.Add(Me.OKButton)
        Me.Name = "frmshrinkdialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Shrink Mode"
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents optrelease As RadioButton
    Public WithEvents optempty As RadioButton
    Public WithEvents optreorganize As RadioButton
    Public WithEvents CancelButton As Button
    Public WithEvents OKButton As Button
End Class
