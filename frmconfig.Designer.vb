<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmconfig
    Inherits System.Windows.Forms.Form
    'Form overrides Dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Forms Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The Windows Forms Designer requires the following procedure
    'It can be modified using the Windows Forms Designer.  
    'Do not modify it with the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.chkautologin = New System.Windows.Forms.CheckBox()
        Me.cmddataloc = New System.Windows.Forms.Button()
        Me.CmdApply = New System.Windows.Forms.Button()
        Me.chklogtofile = New System.Windows.Forms.CheckBox()
        Me.chkcolourQE = New System.Windows.Forms.CheckBox()
        Me.chkdisablerandom = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'chkautologin
        '
        Me.chkautologin.AutoSize = True
        Me.chkautologin.Location = New System.Drawing.Point(12, 12)
        Me.chkautologin.Name = "chkautologin"
        Me.chkautologin.Size = New System.Drawing.Size(78, 17)
        Me.chkautologin.TabIndex = 0
        Me.chkautologin.Text = "Auto LogIn"
        Me.chkautologin.UseVisualStyleBackColor = True
        '
        'cmddataloc
        '
        Me.cmddataloc.BackColor = System.Drawing.SystemColors.Control
        Me.cmddataloc.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmddataloc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmddataloc.Location = New System.Drawing.Point(96, 7)
        Me.cmddataloc.Name = "cmddataloc"
        Me.cmddataloc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmddataloc.Size = New System.Drawing.Size(81, 25)
        Me.cmddataloc.TabIndex = 24
        Me.cmddataloc.Text = "Data location"
        Me.cmddataloc.UseVisualStyleBackColor = False
        '
        'CmdApply
        '
        Me.CmdApply.BackColor = System.Drawing.SystemColors.Control
        Me.CmdApply.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdApply.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.CmdApply.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdApply.Location = New System.Drawing.Point(51, 114)
        Me.CmdApply.Name = "CmdApply"
        Me.CmdApply.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdApply.Size = New System.Drawing.Size(81, 25)
        Me.CmdApply.TabIndex = 25
        Me.CmdApply.Text = "Apply"
        Me.CmdApply.UseVisualStyleBackColor = False
        '
        'chklogtofile
        '
        Me.chklogtofile.AutoSize = True
        Me.chklogtofile.Location = New System.Drawing.Point(12, 35)
        Me.chklogtofile.Name = "chklogtofile"
        Me.chklogtofile.Size = New System.Drawing.Size(72, 17)
        Me.chklogtofile.TabIndex = 26
        Me.chklogtofile.Text = "Log to file"
        Me.chklogtofile.UseVisualStyleBackColor = True
        '
        'chkcolourQE
        '
        Me.chkcolourQE.AutoSize = True
        Me.chkcolourQE.Location = New System.Drawing.Point(12, 58)
        Me.chkcolourQE.Name = "chkcolourQE"
        Me.chkcolourQE.Size = New System.Drawing.Size(105, 17)
        Me.chkcolourQE.TabIndex = 27
        Me.chkcolourQE.Text = "Colour text in QE"
        Me.chkcolourQE.UseVisualStyleBackColor = True
        '
        'chkdisablerandom
        '
        Me.chkdisablerandom.AutoSize = True
        Me.chkdisablerandom.Location = New System.Drawing.Point(12, 81)
        Me.chkdisablerandom.Name = "chkdisablerandom"
        Me.chkdisablerandom.Size = New System.Drawing.Size(127, 17)
        Me.chkdisablerandom.TabIndex = 28
        Me.chkdisablerandom.Text = "Disable RND security"
        Me.chkdisablerandom.UseVisualStyleBackColor = True
        '
        'frmconfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(189, 146)
        Me.Controls.Add(Me.chkdisablerandom)
        Me.Controls.Add(Me.chkcolourQE)
        Me.Controls.Add(Me.chklogtofile)
        Me.Controls.Add(Me.CmdApply)
        Me.Controls.Add(Me.cmddataloc)
        Me.Controls.Add(Me.chkautologin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmconfig"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configuration"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents chkautologin As CheckBox
    Public WithEvents cmddataloc As Button
    Public WithEvents CmdApply As Button
    Friend WithEvents chklogtofile As CheckBox
    Friend WithEvents chkcolourQE As CheckBox
    Friend WithEvents chkdisablerandom As CheckBox
End Class
