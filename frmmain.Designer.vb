<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmmain
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
	Public WithEvents cmdgetsize As System.Windows.Forms.Button
	Public WithEvents cmdclear As System.Windows.Forms.Button
	Public WithEvents txtlog As System.Windows.Forms.TextBox
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents cmdpurge As System.Windows.Forms.Button
	Public WithEvents cmdguest As System.Windows.Forms.Button
	Public WithEvents cmdrestore As System.Windows.Forms.Button
	Public WithEvents cmdadd As System.Windows.Forms.Button
	Public WithEvents cmdbackup As System.Windows.Forms.Button
	Public WithEvents cmddelete As System.Windows.Forms.Button
	Public WithEvents cmdrepairdb As System.Windows.Forms.Button
	Public WithEvents lstdb As System.Windows.Forms.ListBox
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents adduser As System.Windows.Forms.Button
	Public WithEvents cmddeleteuser As System.Windows.Forms.Button
	Public WithEvents cmdchange As System.Windows.Forms.Button
	Public WithEvents lstuser As System.Windows.Forms.ListBox
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public dlgOpen As System.Windows.Forms.OpenFileDialog
	Public WithEvents cmdapply As System.Windows.Forms.Button
	Public WithEvents chkfirewall As System.Windows.Forms.CheckBox
	Public WithEvents chksqlbrowser As System.Windows.Forms.CheckBox
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmmain))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdgetsize = New System.Windows.Forms.Button()
        Me.cmdkillconn = New System.Windows.Forms.Button()
        Me.cmddetach = New System.Windows.Forms.Button()
        Me.cmdpurge = New System.Windows.Forms.Button()
        Me.cmdguest = New System.Windows.Forms.Button()
        Me.cmdrestore = New System.Windows.Forms.Button()
        Me.cmdadd = New System.Windows.Forms.Button()
        Me.cmdbackup = New System.Windows.Forms.Button()
        Me.cmddelete = New System.Windows.Forms.Button()
        Me.cmdrepairdb = New System.Windows.Forms.Button()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.txtlog = New System.Windows.Forms.TextBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.lstdb = New System.Windows.Forms.ListBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.adduser = New System.Windows.Forms.Button()
        Me.cmddeleteuser = New System.Windows.Forms.Button()
        Me.cmdchange = New System.Windows.Forms.Button()
        Me.lstuser = New System.Windows.Forms.ListBox()
        Me.dlgOpen = New System.Windows.Forms.OpenFileDialog()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.cmdapply = New System.Windows.Forms.Button()
        Me.chkfirewall = New System.Windows.Forms.CheckBox()
        Me.chksqlbrowser = New System.Windows.Forms.CheckBox()
        Me.Frame3.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdgetsize
        '
        Me.cmdgetsize.BackColor = System.Drawing.SystemColors.Control
        Me.cmdgetsize.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdgetsize.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdgetsize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdgetsize.Location = New System.Drawing.Point(272, 128)
        Me.cmdgetsize.Name = "cmdgetsize"
        Me.cmdgetsize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdgetsize.Size = New System.Drawing.Size(81, 25)
        Me.cmdgetsize.TabIndex = 20
        Me.cmdgetsize.Text = "Get filesize"
        Me.ToolTip1.SetToolTip(Me.cmdgetsize, "Get file size of ldf/mdf files from selected Databse")
        Me.cmdgetsize.UseVisualStyleBackColor = False
        '
        'cmdkillconn
        '
        Me.cmdkillconn.BackColor = System.Drawing.SystemColors.Control
        Me.cmdkillconn.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdkillconn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdkillconn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdkillconn.Location = New System.Drawing.Point(263, 151)
        Me.cmdkillconn.Name = "cmdkillconn"
        Me.cmdkillconn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdkillconn.Size = New System.Drawing.Size(81, 25)
        Me.cmdkillconn.TabIndex = 21
        Me.cmdkillconn.Text = "Kill conn"
        Me.ToolTip1.SetToolTip(Me.cmdkillconn, "Kill all connections to the database")
        Me.cmdkillconn.UseVisualStyleBackColor = False
        '
        'cmddetach
        '
        Me.cmddetach.BackColor = System.Drawing.SystemColors.Control
        Me.cmddetach.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmddetach.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmddetach.Location = New System.Drawing.Point(176, 152)
        Me.cmddetach.Name = "cmddetach"
        Me.cmddetach.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmddetach.Size = New System.Drawing.Size(81, 25)
        Me.cmddetach.TabIndex = 20
        Me.cmddetach.Text = "Detach"
        Me.ToolTip1.SetToolTip(Me.cmddetach, "Detach Database")
        Me.cmddetach.UseVisualStyleBackColor = False
        '
        'cmdpurge
        '
        Me.cmdpurge.BackColor = System.Drawing.SystemColors.Control
        Me.cmdpurge.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdpurge.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdpurge.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdpurge.Location = New System.Drawing.Point(176, 88)
        Me.cmdpurge.Name = "cmdpurge"
        Me.cmdpurge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdpurge.Size = New System.Drawing.Size(81, 25)
        Me.cmdpurge.TabIndex = 19
        Me.cmdpurge.Text = "Clear Log File"
        Me.ToolTip1.SetToolTip(Me.cmdpurge, "Clear ldf file from selected Database")
        Me.cmdpurge.UseVisualStyleBackColor = False
        '
        'cmdguest
        '
        Me.cmdguest.BackColor = System.Drawing.SystemColors.Control
        Me.cmdguest.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdguest.Enabled = False
        Me.cmdguest.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdguest.Location = New System.Drawing.Point(176, 56)
        Me.cmdguest.Name = "cmdguest"
        Me.cmdguest.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdguest.Size = New System.Drawing.Size(81, 25)
        Me.cmdguest.TabIndex = 14
        Me.cmdguest.Text = "Guest"
        Me.ToolTip1.SetToolTip(Me.cmdguest, "Grant Guest Access to the selected Database")
        Me.cmdguest.UseVisualStyleBackColor = False
        '
        'cmdrestore
        '
        Me.cmdrestore.BackColor = System.Drawing.SystemColors.Control
        Me.cmdrestore.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdrestore.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdrestore.Location = New System.Drawing.Point(264, 56)
        Me.cmdrestore.Name = "cmdrestore"
        Me.cmdrestore.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdrestore.Size = New System.Drawing.Size(81, 25)
        Me.cmdrestore.TabIndex = 10
        Me.cmdrestore.Text = "Restore"
        Me.ToolTip1.SetToolTip(Me.cmdrestore, "Restore a database from a .bak")
        Me.cmdrestore.UseVisualStyleBackColor = False
        '
        'cmdadd
        '
        Me.cmdadd.BackColor = System.Drawing.SystemColors.Control
        Me.cmdadd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdadd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdadd.Location = New System.Drawing.Point(176, 24)
        Me.cmdadd.Name = "cmdadd"
        Me.cmdadd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdadd.Size = New System.Drawing.Size(81, 25)
        Me.cmdadd.TabIndex = 9
        Me.cmdadd.Text = "Add database"
        Me.ToolTip1.SetToolTip(Me.cmdadd, "Atach a database from a .mdf or .bak file")
        Me.cmdadd.UseVisualStyleBackColor = False
        '
        'cmdbackup
        '
        Me.cmdbackup.BackColor = System.Drawing.SystemColors.Control
        Me.cmdbackup.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdbackup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdbackup.Location = New System.Drawing.Point(264, 24)
        Me.cmdbackup.Name = "cmdbackup"
        Me.cmdbackup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdbackup.Size = New System.Drawing.Size(81, 25)
        Me.cmdbackup.TabIndex = 8
        Me.cmdbackup.Text = "Backup"
        Me.ToolTip1.SetToolTip(Me.cmdbackup, "Create a backup of the selected Database")
        Me.cmdbackup.UseVisualStyleBackColor = False
        '
        'cmddelete
        '
        Me.cmddelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmddelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmddelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmddelete.Location = New System.Drawing.Point(176, 120)
        Me.cmddelete.Name = "cmddelete"
        Me.cmddelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmddelete.Size = New System.Drawing.Size(81, 25)
        Me.cmddelete.TabIndex = 7
        Me.cmddelete.Text = "Delete"
        Me.ToolTip1.SetToolTip(Me.cmddelete, "Delete Database")
        Me.cmddelete.UseVisualStyleBackColor = False
        '
        'cmdrepairdb
        '
        Me.cmdrepairdb.BackColor = System.Drawing.SystemColors.Control
        Me.cmdrepairdb.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdrepairdb.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdrepairdb.Location = New System.Drawing.Point(264, 88)
        Me.cmdrepairdb.Name = "cmdrepairdb"
        Me.cmdrepairdb.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdrepairdb.Size = New System.Drawing.Size(81, 25)
        Me.cmdrepairdb.TabIndex = 6
        Me.cmdrepairdb.Text = "Repair "
        Me.ToolTip1.SetToolTip(Me.cmdrepairdb, "Run one of various DBCC checks for the selected Database")
        Me.cmdrepairdb.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdclear)
        Me.Frame3.Controls.Add(Me.txtlog)
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(8, 216)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(641, 217)
        Me.Frame3.TabIndex = 12
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Message"
        '
        'cmdclear
        '
        Me.cmdclear.BackColor = System.Drawing.SystemColors.Control
        Me.cmdclear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdclear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdclear.Location = New System.Drawing.Point(552, 184)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdclear.Size = New System.Drawing.Size(81, 25)
        Me.cmdclear.TabIndex = 21
        Me.cmdclear.Text = "Clear"
        Me.cmdclear.UseVisualStyleBackColor = False
        '
        'txtlog
        '
        Me.txtlog.AcceptsReturn = True
        Me.txtlog.BackColor = System.Drawing.SystemColors.Window
        Me.txtlog.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtlog.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlog.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtlog.Location = New System.Drawing.Point(8, 24)
        Me.txtlog.MaxLength = 0
        Me.txtlog.Multiline = True
        Me.txtlog.Name = "txtlog"
        Me.txtlog.ReadOnly = True
        Me.txtlog.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtlog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtlog.Size = New System.Drawing.Size(625, 154)
        Me.txtlog.TabIndex = 13
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.cmdkillconn)
        Me.Frame2.Controls.Add(Me.cmddetach)
        Me.Frame2.Controls.Add(Me.cmdpurge)
        Me.Frame2.Controls.Add(Me.cmdguest)
        Me.Frame2.Controls.Add(Me.cmdrestore)
        Me.Frame2.Controls.Add(Me.cmdadd)
        Me.Frame2.Controls.Add(Me.cmdbackup)
        Me.Frame2.Controls.Add(Me.cmddelete)
        Me.Frame2.Controls.Add(Me.cmdrepairdb)
        Me.Frame2.Controls.Add(Me.lstdb)
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(8, 8)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(353, 201)
        Me.Frame2.TabIndex = 4
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Database"
        '
        'lstdb
        '
        Me.lstdb.BackColor = System.Drawing.SystemColors.Window
        Me.lstdb.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstdb.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstdb.Location = New System.Drawing.Point(8, 24)
        Me.lstdb.Name = "lstdb"
        Me.lstdb.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstdb.Size = New System.Drawing.Size(161, 160)
        Me.lstdb.TabIndex = 5
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.adduser)
        Me.Frame1.Controls.Add(Me.cmddeleteuser)
        Me.Frame1.Controls.Add(Me.cmdchange)
        Me.Frame1.Controls.Add(Me.lstuser)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(368, 8)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(281, 129)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Users"
        '
        'adduser
        '
        Me.adduser.BackColor = System.Drawing.SystemColors.Control
        Me.adduser.Cursor = System.Windows.Forms.Cursors.Default
        Me.adduser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.adduser.Location = New System.Drawing.Point(184, 24)
        Me.adduser.Name = "adduser"
        Me.adduser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.adduser.Size = New System.Drawing.Size(81, 25)
        Me.adduser.TabIndex = 11
        Me.adduser.Text = "Add user"
        Me.adduser.UseVisualStyleBackColor = False
        '
        'cmddeleteuser
        '
        Me.cmddeleteuser.BackColor = System.Drawing.SystemColors.Control
        Me.cmddeleteuser.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmddeleteuser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmddeleteuser.Location = New System.Drawing.Point(184, 88)
        Me.cmddeleteuser.Name = "cmddeleteuser"
        Me.cmddeleteuser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmddeleteuser.Size = New System.Drawing.Size(81, 25)
        Me.cmddeleteuser.TabIndex = 3
        Me.cmddeleteuser.Text = "Delete"
        Me.cmddeleteuser.UseVisualStyleBackColor = False
        '
        'cmdchange
        '
        Me.cmdchange.BackColor = System.Drawing.SystemColors.Control
        Me.cmdchange.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdchange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdchange.Location = New System.Drawing.Point(184, 56)
        Me.cmdchange.Name = "cmdchange"
        Me.cmdchange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdchange.Size = New System.Drawing.Size(81, 25)
        Me.cmdchange.TabIndex = 2
        Me.cmdchange.Text = "Change pwd"
        Me.cmdchange.UseVisualStyleBackColor = False
        '
        'lstuser
        '
        Me.lstuser.BackColor = System.Drawing.SystemColors.Window
        Me.lstuser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstuser.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstuser.Location = New System.Drawing.Point(8, 24)
        Me.lstuser.Name = "lstuser"
        Me.lstuser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstuser.Size = New System.Drawing.Size(169, 95)
        Me.lstuser.TabIndex = 1
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.cmdapply)
        Me.Frame4.Controls.Add(Me.chkfirewall)
        Me.Frame4.Controls.Add(Me.chksqlbrowser)
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(368, 144)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(281, 65)
        Me.Frame4.TabIndex = 15
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Options"
        '
        'cmdapply
        '
        Me.cmdapply.BackColor = System.Drawing.SystemColors.Control
        Me.cmdapply.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdapply.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdapply.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdapply.Location = New System.Drawing.Point(192, 16)
        Me.cmdapply.Name = "cmdapply"
        Me.cmdapply.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdapply.Size = New System.Drawing.Size(81, 34)
        Me.cmdapply.TabIndex = 18
        Me.cmdapply.Text = "Apply Settings"
        Me.cmdapply.UseVisualStyleBackColor = False
        '
        'chkfirewall
        '
        Me.chkfirewall.BackColor = System.Drawing.SystemColors.Control
        Me.chkfirewall.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkfirewall.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkfirewall.Location = New System.Drawing.Point(8, 40)
        Me.chkfirewall.Name = "chkfirewall"
        Me.chkfirewall.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkfirewall.Size = New System.Drawing.Size(153, 17)
        Me.chkfirewall.TabIndex = 17
        Me.chkfirewall.Text = "Enable firewall exception"
        Me.chkfirewall.UseVisualStyleBackColor = False
        '
        'chksqlbrowser
        '
        Me.chksqlbrowser.BackColor = System.Drawing.SystemColors.Control
        Me.chksqlbrowser.Cursor = System.Windows.Forms.Cursors.Default
        Me.chksqlbrowser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chksqlbrowser.Location = New System.Drawing.Point(8, 24)
        Me.chksqlbrowser.Name = "chksqlbrowser"
        Me.chksqlbrowser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chksqlbrowser.Size = New System.Drawing.Size(145, 17)
        Me.chksqlbrowser.TabIndex = 16
        Me.chksqlbrowser.Text = "Enable SQL Browser"
        Me.chksqlbrowser.UseVisualStyleBackColor = False
        '
        'frmmain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(657, 442)
        Me.Controls.Add(Me.cmdgetsize)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame4)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 25)
        Me.MaximizeBox = False
        Me.Name = "frmmain"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SQL Server Manager Lite"
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents cmddetach As Button
    Public WithEvents cmdkillconn As Button
#End Region
End Class