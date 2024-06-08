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
        Me.components = New System.ComponentModel.Container()
        Me.dtgridtable = New System.Windows.Forms.DataGridView()
        Me.lsttables = New System.Windows.Forms.ListBox()
        Me.txtrows = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CmdRefresh = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SearchRC = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.dtgridtable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtgridtable
        '
        Me.dtgridtable.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtgridtable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgridtable.Location = New System.Drawing.Point(242, 12)
        Me.dtgridtable.Name = "dtgridtable"
        Me.dtgridtable.RowHeadersVisible = False
        Me.dtgridtable.Size = New System.Drawing.Size(650, 464)
        Me.dtgridtable.TabIndex = 1
        '
        'lsttables
        '
        Me.lsttables.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lsttables.FormattingEnabled = True
        Me.lsttables.Location = New System.Drawing.Point(12, 12)
        Me.lsttables.Name = "lsttables"
        Me.lsttables.Size = New System.Drawing.Size(228, 433)
        Me.lsttables.TabIndex = 2
        '
        'txtrows
        '
        Me.txtrows.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtrows.Location = New System.Drawing.Point(134, 451)
        Me.txtrows.Name = "txtrows"
        Me.txtrows.Size = New System.Drawing.Size(65, 20)
        Me.txtrows.TabIndex = 3
        Me.txtrows.Text = "1000"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 454)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Amount of rows to show"
        '
        'CmdRefresh
        '
        Me.CmdRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CmdRefresh.Location = New System.Drawing.Point(205, 448)
        Me.CmdRefresh.Name = "CmdRefresh"
        Me.CmdRefresh.Size = New System.Drawing.Size(28, 25)
        Me.CmdRefresh.TabIndex = 5
        Me.CmdRefresh.Text = "↺"
        Me.ToolTip1.SetToolTip(Me.CmdRefresh, "Refresh")
        Me.CmdRefresh.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SearchRC})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(110, 26)
        '
        'SearchRC
        '
        Me.SearchRC.Name = "SearchRC"
        Me.SearchRC.Size = New System.Drawing.Size(109, 22)
        Me.SearchRC.Text = "Search"
        '
        'frmtableview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(904, 486)
        Me.Controls.Add(Me.CmdRefresh)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtrows)
        Me.Controls.Add(Me.lsttables)
        Me.Controls.Add(Me.dtgridtable)
        Me.MinimumSize = New System.Drawing.Size(820, 498)
        Me.Name = "frmtableview"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Table View"
        CType(Me.dtgridtable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtgridtable As DataGridView
    Friend WithEvents lsttables As ListBox
    Friend WithEvents txtrows As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents CmdRefresh As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SearchRC As ToolStripMenuItem
End Class
