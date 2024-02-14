<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmfilespath
    Inherits System.Windows.Forms.Form

    ' Form replaces Dispose to clean up the list of components.
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

    ' Required by the Windows Forms Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The Windows Forms Designer requires the following procedure
    ' It can be modified using the Windows Forms Designer.  
    ' Do not modify it with the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TxtMDF = New System.Windows.Forms.TextBox()
        Me.TxtLDF = New System.Windows.Forms.TextBox()
        Me.cmdmdfpath = New System.Windows.Forms.Button()
        Me.cmdldfpath = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TxtMDF
        '
        Me.TxtMDF.Location = New System.Drawing.Point(12, 30)
        Me.TxtMDF.Name = "TxtMDF"
        Me.TxtMDF.Size = New System.Drawing.Size(542, 20)
        Me.TxtMDF.TabIndex = 0
        '
        'TxtLDF
        '
        Me.TxtLDF.Location = New System.Drawing.Point(12, 69)
        Me.TxtLDF.Name = "TxtLDF"
        Me.TxtLDF.Size = New System.Drawing.Size(542, 20)
        Me.TxtLDF.TabIndex = 1
        '
        'cmdmdfpath
        '
        Me.cmdmdfpath.Location = New System.Drawing.Point(560, 30)
        Me.cmdmdfpath.Name = "cmdmdfpath"
        Me.cmdmdfpath.Size = New System.Drawing.Size(32, 20)
        Me.cmdmdfpath.TabIndex = 2
        Me.cmdmdfpath.Text = "..."
        Me.cmdmdfpath.UseVisualStyleBackColor = True
        '
        'cmdldfpath
        '
        Me.cmdldfpath.Location = New System.Drawing.Point(560, 68)
        Me.cmdldfpath.Name = "cmdldfpath"
        Me.cmdldfpath.Size = New System.Drawing.Size(32, 20)
        Me.cmdldfpath.TabIndex = 3
        Me.cmdldfpath.Text = "..."
        Me.cmdldfpath.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "MDF Default Path"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "LDF Default Path"
        '
        'frmfilespath
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(604, 132)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdldfpath)
        Me.Controls.Add(Me.cmdmdfpath)
        Me.Controls.Add(Me.TxtLDF)
        Me.Controls.Add(Me.TxtMDF)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "frmfilespath"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Data Files Path"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtMDF As TextBox
    Friend WithEvents TxtLDF As TextBox
    Friend WithEvents cmdmdfpath As Button
    Friend WithEvents cmdldfpath As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
End Class
