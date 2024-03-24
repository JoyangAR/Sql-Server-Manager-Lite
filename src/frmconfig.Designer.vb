<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it with the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.chkautologin = New System.Windows.Forms.CheckBox()
        Me.grpSettings = New System.Windows.Forms.GroupBox()
        Me.chkUpdates = New System.Windows.Forms.CheckBox()
        Me.cmdCheckUpd = New System.Windows.Forms.Button()
        Me.chkdisablerandom = New System.Windows.Forms.CheckBox()
        Me.chklogtofile = New System.Windows.Forms.CheckBox()
        Me.grpQE = New System.Windows.Forms.GroupBox()
        Me.chkcolourQE = New System.Windows.Forms.CheckBox()
        Me.CmdApply = New System.Windows.Forms.Button()
        Me.cmddataloc = New System.Windows.Forms.Button()
        Me.grpSettings.SuspendLayout()
        Me.grpQE.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkautologin
        '
        Me.chkautologin.AutoSize = True
        Me.chkautologin.Location = New System.Drawing.Point(6, 19)
        Me.chkautologin.Name = "chkautologin"
        Me.chkautologin.Size = New System.Drawing.Size(78, 17)
        Me.chkautologin.TabIndex = 0
        Me.chkautologin.Text = "Auto LogIn"
        Me.chkautologin.UseVisualStyleBackColor = True
        '
        'grpSettings
        '
        Me.grpSettings.Controls.Add(Me.chkUpdates)
        Me.grpSettings.Controls.Add(Me.cmdCheckUpd)
        Me.grpSettings.Controls.Add(Me.chkdisablerandom)
        Me.grpSettings.Controls.Add(Me.chklogtofile)
        Me.grpSettings.Controls.Add(Me.chkautologin)
        Me.grpSettings.Location = New System.Drawing.Point(12, 12)
        Me.grpSettings.Name = "grpSettings"
        Me.grpSettings.Size = New System.Drawing.Size(236, 132)
        Me.grpSettings.TabIndex = 1
        Me.grpSettings.TabStop = False
        Me.grpSettings.Text = "Settings"
        '
        'chkUpdates
        '
        Me.chkUpdates.AutoSize = True
        Me.chkUpdates.Location = New System.Drawing.Point(6, 88)
        Me.chkUpdates.Name = "chkUpdates"
        Me.chkUpdates.Size = New System.Drawing.Size(148, 17)
        Me.chkUpdates.TabIndex = 29
        Me.chkUpdates.Text = "Check for updates at start"
        Me.chkUpdates.UseVisualStyleBackColor = True
        '
        'cmdCheckUpd
        '
        Me.cmdCheckUpd.Location = New System.Drawing.Point(155, 84)
        Me.cmdCheckUpd.Name = "cmdCheckUpd"
        Me.cmdCheckUpd.Size = New System.Drawing.Size(75, 23)
        Me.cmdCheckUpd.TabIndex = 30
        Me.cmdCheckUpd.Text = "Check now"
        Me.cmdCheckUpd.UseVisualStyleBackColor = True
        '
        'chkdisablerandom
        '
        Me.chkdisablerandom.AutoSize = True
        Me.chkdisablerandom.Location = New System.Drawing.Point(6, 42)
        Me.chkdisablerandom.Name = "chkdisablerandom"
        Me.chkdisablerandom.Size = New System.Drawing.Size(127, 17)
        Me.chkdisablerandom.TabIndex = 28
        Me.chkdisablerandom.Text = "Disable RND security"
        Me.chkdisablerandom.UseVisualStyleBackColor = True
        '
        'chklogtofile
        '
        Me.chklogtofile.AutoSize = True
        Me.chklogtofile.Location = New System.Drawing.Point(6, 65)
        Me.chklogtofile.Name = "chklogtofile"
        Me.chklogtofile.Size = New System.Drawing.Size(72, 17)
        Me.chklogtofile.TabIndex = 26
        Me.chklogtofile.Text = "Log to file"
        Me.chklogtofile.UseVisualStyleBackColor = True
        '
        'grpQE
        '
        Me.grpQE.Controls.Add(Me.chkcolourQE)
        Me.grpQE.Location = New System.Drawing.Point(12, 151)
        Me.grpQE.Name = "grpQE"
        Me.grpQE.Size = New System.Drawing.Size(236, 53)
        Me.grpQE.TabIndex = 2
        Me.grpQE.TabStop = False
        Me.grpQE.Text = "QE Options"
        '
        'chkcolourQE
        '
        Me.chkcolourQE.AutoSize = True
        Me.chkcolourQE.Location = New System.Drawing.Point(6, 19)
        Me.chkcolourQE.Name = "chkcolourQE"
        Me.chkcolourQE.Size = New System.Drawing.Size(183, 17)
        Me.chkcolourQE.TabIndex = 27
        Me.chkcolourQE.Text = "Colour text in QE (To be repaired)"
        Me.chkcolourQE.UseVisualStyleBackColor = True
        '
        'CmdApply
        '
        Me.CmdApply.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.CmdApply.Location = New System.Drawing.Point(167, 209)
        Me.CmdApply.Name = "CmdApply"
        Me.CmdApply.Size = New System.Drawing.Size(81, 25)
        Me.CmdApply.TabIndex = 3
        Me.CmdApply.Text = "Apply"
        Me.CmdApply.UseVisualStyleBackColor = True
        '
        'cmddataloc
        '
        Me.cmddataloc.Location = New System.Drawing.Point(12, 209)
        Me.cmddataloc.Name = "cmddataloc"
        Me.cmddataloc.Size = New System.Drawing.Size(81, 25)
        Me.cmddataloc.TabIndex = 4
        Me.cmddataloc.Text = "Data location"
        Me.cmddataloc.UseVisualStyleBackColor = True
        '
        'frmconfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(260, 243)
        Me.Controls.Add(Me.cmddataloc)
        Me.Controls.Add(Me.CmdApply)
        Me.Controls.Add(Me.grpQE)
        Me.Controls.Add(Me.grpSettings)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmconfig"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configuration"
        Me.grpSettings.ResumeLayout(False)
        Me.grpSettings.PerformLayout()
        Me.grpQE.ResumeLayout(False)
        Me.grpQE.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents chkautologin As CheckBox
    Friend WithEvents grpSettings As GroupBox
    Friend WithEvents chkUpdates As CheckBox
    Friend WithEvents cmdCheckUpd As Button
    Friend WithEvents chkdisablerandom As CheckBox
    Friend WithEvents chklogtofile As CheckBox
    Friend WithEvents grpQE As GroupBox
    Friend WithEvents chkcolourQE As CheckBox
    Friend WithEvents CmdApply As Button
    Friend WithEvents cmddataloc As Button

End Class