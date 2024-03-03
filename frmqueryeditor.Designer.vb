<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmqueryeditor
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TxtQueryBox = New System.Windows.Forms.RichTextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MenuQueryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadQueryMS = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveQueryMS = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveQueryAsMS = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitMS = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExecuteMS = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CutMS = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyMS = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteMS = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchMS = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReplaceMS = New System.Windows.Forms.ToolStripMenuItem()
        Me.VerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FontMS = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResultBoxMS = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CutRC = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyRC = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteRC = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteRC = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchRC = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReplaceRC = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExecuteRC = New System.Windows.Forms.ToolStripMenuItem()
        Me.TxtResult = New System.Windows.Forms.TextBox()
        Me.MenuStrip1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TxtQueryBox
        '
        Me.TxtQueryBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtQueryBox.Location = New System.Drawing.Point(0, 27)
        Me.TxtQueryBox.Name = "TxtQueryBox"
        Me.TxtQueryBox.Size = New System.Drawing.Size(1099, 596)
        Me.TxtQueryBox.TabIndex = 4
        Me.TxtQueryBox.Text = ""
        Me.TxtQueryBox.WordWrap = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuQueryToolStripMenuItem, Me.EditarToolStripMenuItem, Me.VerToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1099, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MenuQueryToolStripMenuItem
        '
        Me.MenuQueryToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadQueryMS, Me.SaveQueryMS, Me.SaveQueryAsMS, Me.ExitMS, Me.ExecuteMS})
        Me.MenuQueryToolStripMenuItem.Name = "MenuQueryToolStripMenuItem"
        Me.MenuQueryToolStripMenuItem.Size = New System.Drawing.Size(50, 20)
        Me.MenuQueryToolStripMenuItem.Text = "Menu"
        '
        'LoadQueryMS
        '
        Me.LoadQueryMS.Name = "LoadQueryMS"
        Me.LoadQueryMS.Size = New System.Drawing.Size(180, 22)
        Me.LoadQueryMS.Text = "Load Query"
        '
        'SaveQueryMS
        '
        Me.SaveQueryMS.Name = "SaveQueryMS"
        Me.SaveQueryMS.Size = New System.Drawing.Size(180, 22)
        Me.SaveQueryMS.Text = "Save Query"
        '
        'SaveQueryAsMS
        '
        Me.SaveQueryAsMS.Name = "SaveQueryAsMS"
        Me.SaveQueryAsMS.Size = New System.Drawing.Size(180, 22)
        Me.SaveQueryAsMS.Text = "Save Query As"
        '
        'ExitMS
        '
        Me.ExitMS.Name = "ExitMS"
        Me.ExitMS.Size = New System.Drawing.Size(180, 22)
        Me.ExitMS.Text = "Exit"
        '
        'ExecuteMS
        '
        Me.ExecuteMS.Name = "ExecuteMS"
        Me.ExecuteMS.Size = New System.Drawing.Size(180, 22)
        Me.ExecuteMS.Text = "Execute"
        '
        'EditarToolStripMenuItem
        '
        Me.EditarToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CutMS, Me.CopyMS, Me.PasteMS, Me.SearchMS, Me.ReplaceMS})
        Me.EditarToolStripMenuItem.Name = "EditarToolStripMenuItem"
        Me.EditarToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditarToolStripMenuItem.Text = "Edit"
        '
        'CutMS
        '
        Me.CutMS.Name = "CutMS"
        Me.CutMS.Size = New System.Drawing.Size(180, 22)
        Me.CutMS.Text = "Cut"
        '
        'CopyMS
        '
        Me.CopyMS.Name = "CopyMS"
        Me.CopyMS.Size = New System.Drawing.Size(180, 22)
        Me.CopyMS.Text = "Copy"
        '
        'PasteMS
        '
        Me.PasteMS.Name = "PasteMS"
        Me.PasteMS.Size = New System.Drawing.Size(180, 22)
        Me.PasteMS.Text = "Paste"
        '
        'SearchMS
        '
        Me.SearchMS.Name = "SearchMS"
        Me.SearchMS.Size = New System.Drawing.Size(180, 22)
        Me.SearchMS.Text = "Search"
        '
        'ReplaceMS
        '
        Me.ReplaceMS.Name = "ReplaceMS"
        Me.ReplaceMS.Size = New System.Drawing.Size(180, 22)
        Me.ReplaceMS.Text = "Replace"
        '
        'VerToolStripMenuItem
        '
        Me.VerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FontMS, Me.ResultBoxMS})
        Me.VerToolStripMenuItem.Name = "VerToolStripMenuItem"
        Me.VerToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.VerToolStripMenuItem.Text = "View"
        '
        'FontMS
        '
        Me.FontMS.Name = "FontMS"
        Me.FontMS.Size = New System.Drawing.Size(180, 22)
        Me.FontMS.Text = "Font Options"
        '
        'ResultBoxMS
        '
        Me.ResultBoxMS.Name = "ResultBoxMS"
        Me.ResultBoxMS.Size = New System.Drawing.Size(180, 22)
        Me.ResultBoxMS.Text = "Result Box"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CutRC, Me.CopyRC, Me.PasteRC, Me.DeleteRC, Me.SearchRC, Me.ReplaceRC, Me.ExecuteRC})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(116, 158)
        '
        'CutRC
        '
        Me.CutRC.Name = "CutRC"
        Me.CutRC.Size = New System.Drawing.Size(115, 22)
        Me.CutRC.Text = "Cut"
        '
        'CopyRC
        '
        Me.CopyRC.Name = "CopyRC"
        Me.CopyRC.Size = New System.Drawing.Size(115, 22)
        Me.CopyRC.Text = "Copy"
        '
        'PasteRC
        '
        Me.PasteRC.Name = "PasteRC"
        Me.PasteRC.Size = New System.Drawing.Size(115, 22)
        Me.PasteRC.Text = "Paste"
        '
        'DeleteRC
        '
        Me.DeleteRC.Name = "DeleteRC"
        Me.DeleteRC.Size = New System.Drawing.Size(115, 22)
        Me.DeleteRC.Text = "Delete"
        '
        'SearchRC
        '
        Me.SearchRC.Name = "SearchRC"
        Me.SearchRC.Size = New System.Drawing.Size(115, 22)
        Me.SearchRC.Text = "Search"
        '
        'ReplaceRC
        '
        Me.ReplaceRC.Name = "ReplaceRC"
        Me.ReplaceRC.Size = New System.Drawing.Size(115, 22)
        Me.ReplaceRC.Text = "Replace"
        '
        'ExecuteRC
        '
        Me.ExecuteRC.Name = "ExecuteRC"
        Me.ExecuteRC.Size = New System.Drawing.Size(115, 22)
        Me.ExecuteRC.Text = "Execute"
        '
        'TxtResult
        '
        Me.TxtResult.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtResult.Location = New System.Drawing.Point(0, 526)
        Me.TxtResult.Multiline = True
        Me.TxtResult.Name = "TxtResult"
        Me.TxtResult.ReadOnly = True
        Me.TxtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TxtResult.Size = New System.Drawing.Size(1099, 97)
        Me.TxtResult.TabIndex = 5
        Me.TxtResult.Visible = False
        '
        'frmqueryeditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1099, 623)
        Me.Controls.Add(Me.TxtResult)
        Me.Controls.Add(Me.TxtQueryBox)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "frmqueryeditor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmqueryeditor"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtQueryBox As RichTextBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents MenuQueryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadQueryMS As ToolStripMenuItem
    Friend WithEvents SaveQueryMS As ToolStripMenuItem
    Friend WithEvents SaveQueryAsMS As ToolStripMenuItem
    Friend WithEvents ExitMS As ToolStripMenuItem
    Friend WithEvents ExecuteMS As ToolStripMenuItem
    Friend WithEvents EditarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopyMS As ToolStripMenuItem
    Friend WithEvents CutMS As ToolStripMenuItem
    Friend WithEvents PasteMS As ToolStripMenuItem
    Friend WithEvents SearchMS As ToolStripMenuItem
    Friend WithEvents ReplaceMS As ToolStripMenuItem
    Friend WithEvents VerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FontMS As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents CutRC As ToolStripMenuItem
    Friend WithEvents CopyRC As ToolStripMenuItem
    Friend WithEvents PasteRC As ToolStripMenuItem
    Friend WithEvents DeleteRC As ToolStripMenuItem
    Friend WithEvents SearchRC As ToolStripMenuItem
    Friend WithEvents ReplaceRC As ToolStripMenuItem
    Friend WithEvents ExecuteRC As ToolStripMenuItem
    Friend WithEvents TxtResult As TextBox
    Friend WithEvents ResultBoxMS As ToolStripMenuItem
End Class
