<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmtableview
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

    ' NOTE: The Windows Forms Designer needs the following procedure
    ' It can be modified using the Windows Forms Designer.  
    ' Do not modify it with the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmtableview))
        Me.dtgridtable = New System.Windows.Forms.DataGridView()
        Me.lsttables = New System.Windows.Forms.ListBox()
        Me.txtrows = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.dtgridtable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtgridtable
        '
        Me.dtgridtable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgridtable.Location = New System.Drawing.Point(165, 12)
        Me.dtgridtable.Name = "dtgridtable"
        Me.dtgridtable.Size = New System.Drawing.Size(623, 433)
        Me.dtgridtable.TabIndex = 1
        '
        'lsttables
        '
        Me.lsttables.FormattingEnabled = True
        Me.lsttables.Location = New System.Drawing.Point(12, 12)
        Me.lsttables.Name = "lsttables"
        Me.lsttables.Size = New System.Drawing.Size(147, 407)
        Me.lsttables.TabIndex = 2
        '
        'txtrows
        '
        Me.txtrows.Location = New System.Drawing.Point(98, 425)
        Me.txtrows.Name = "txtrows"
        Me.txtrows.Size = New System.Drawing.Size(61, 20)
        Me.txtrows.TabIndex = 3
        Me.txtrows.Text = "1000"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 428)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Amount of rows"
        '
        'frmtableview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 455)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtrows)
        Me.Controls.Add(Me.lsttables)
        Me.Controls.Add(Me.dtgridtable)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(820, 498)
        Me.MinimumSize = New System.Drawing.Size(820, 498)
        Me.Name = "frmtableview"
        Me.ShowIcon = False
        Me.Text = "Table View"
        CType(Me.dtgridtable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtgridtable As DataGridView
    Friend WithEvents lsttables As ListBox
    Friend WithEvents txtrows As TextBox
    Friend WithEvents Label1 As Label
End Class
