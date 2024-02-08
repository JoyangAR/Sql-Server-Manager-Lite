<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmlogin
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
	Public WithEvents txtparam As System.Windows.Forms.TextBox
	Public WithEvents txtdriver As System.Windows.Forms.TextBox
	Public WithEvents optodbc As System.Windows.Forms.RadioButton
	Public WithEvents optoledb As System.Windows.Forms.RadioButton
	Public WithEvents txtprovider As System.Windows.Forms.TextBox
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents chklocaldb As System.Windows.Forms.CheckBox
	Public WithEvents txtname As System.Windows.Forms.TextBox
	Public WithEvents txtpwd As System.Windows.Forms.TextBox
	Public WithEvents chktrust As System.Windows.Forms.CheckBox
	Public WithEvents cmdcancel As System.Windows.Forms.Button
	Public WithEvents cmdconnect As System.Windows.Forms.Button
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmlogin))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.optintegrated = New System.Windows.Forms.RadioButton()
        Me.txtparam = New System.Windows.Forms.TextBox()
        Me.txtdriver = New System.Windows.Forms.TextBox()
        Me.optodbc = New System.Windows.Forms.RadioButton()
        Me.optoledb = New System.Windows.Forms.RadioButton()
        Me.txtprovider = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chklocaldb = New System.Windows.Forms.CheckBox()
        Me.txtname = New System.Windows.Forms.TextBox()
        Me.txtpwd = New System.Windows.Forms.TextBox()
        Me.chktrust = New System.Windows.Forms.CheckBox()
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.cmdconnect = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtsvr = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkautologin = New System.Windows.Forms.CheckBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.optintegrated)
        Me.Frame1.Controls.Add(Me.txtparam)
        Me.Frame1.Controls.Add(Me.txtdriver)
        Me.Frame1.Controls.Add(Me.optodbc)
        Me.Frame1.Controls.Add(Me.optoledb)
        Me.Frame1.Controls.Add(Me.txtprovider)
        Me.Frame1.Controls.Add(Me.Label5)
        Me.Frame1.Controls.Add(Me.Label4)
        Me.Frame1.Controls.Add(Me.Label1)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(8, 139)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(273, 232)
        Me.Frame1.TabIndex = 8
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Advanced Settings"
        '
        'optintegrated
        '
        Me.optintegrated.BackColor = System.Drawing.SystemColors.Control
        Me.optintegrated.Cursor = System.Windows.Forms.Cursors.Default
        Me.optintegrated.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optintegrated.Location = New System.Drawing.Point(8, 26)
        Me.optintegrated.Name = "optintegrated"
        Me.optintegrated.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optintegrated.Size = New System.Drawing.Size(81, 17)
        Me.optintegrated.TabIndex = 17
        Me.optintegrated.TabStop = True
        Me.optintegrated.Text = "Integrated"
        Me.optintegrated.UseVisualStyleBackColor = False
        '
        'txtparam
        '
        Me.txtparam.AcceptsReturn = True
        Me.txtparam.BackColor = System.Drawing.SystemColors.Window
        Me.txtparam.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtparam.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtparam.Location = New System.Drawing.Point(117, 187)
        Me.txtparam.MaxLength = 0
        Me.txtparam.Name = "txtparam"
        Me.txtparam.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtparam.Size = New System.Drawing.Size(148, 20)
        Me.txtparam.TabIndex = 15
        '
        'txtdriver
        '
        Me.txtdriver.AcceptsReturn = True
        Me.txtdriver.BackColor = System.Drawing.SystemColors.Window
        Me.txtdriver.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtdriver.Enabled = False
        Me.txtdriver.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtdriver.Location = New System.Drawing.Point(80, 145)
        Me.txtdriver.MaxLength = 0
        Me.txtdriver.Name = "txtdriver"
        Me.txtdriver.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtdriver.Size = New System.Drawing.Size(185, 20)
        Me.txtdriver.TabIndex = 14
        '
        'optodbc
        '
        Me.optodbc.BackColor = System.Drawing.SystemColors.Control
        Me.optodbc.Cursor = System.Windows.Forms.Cursors.Default
        Me.optodbc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optodbc.Location = New System.Drawing.Point(8, 121)
        Me.optodbc.Name = "optodbc"
        Me.optodbc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optodbc.Size = New System.Drawing.Size(81, 17)
        Me.optodbc.TabIndex = 12
        Me.optodbc.TabStop = True
        Me.optodbc.Text = "ODBC"
        Me.optodbc.UseVisualStyleBackColor = False
        '
        'optoledb
        '
        Me.optoledb.BackColor = System.Drawing.SystemColors.Control
        Me.optoledb.Checked = True
        Me.optoledb.Cursor = System.Windows.Forms.Cursors.Default
        Me.optoledb.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optoledb.Location = New System.Drawing.Point(8, 65)
        Me.optoledb.Name = "optoledb"
        Me.optoledb.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optoledb.Size = New System.Drawing.Size(81, 17)
        Me.optoledb.TabIndex = 11
        Me.optoledb.TabStop = True
        Me.optoledb.Text = "OLE DB"
        Me.optoledb.UseVisualStyleBackColor = False
        '
        'txtprovider
        '
        Me.txtprovider.AcceptsReturn = True
        Me.txtprovider.BackColor = System.Drawing.SystemColors.Window
        Me.txtprovider.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtprovider.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtprovider.Location = New System.Drawing.Point(80, 89)
        Me.txtprovider.MaxLength = 0
        Me.txtprovider.Name = "txtprovider"
        Me.txtprovider.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtprovider.Size = New System.Drawing.Size(185, 20)
        Me.txtprovider.TabIndex = 10
        Me.txtprovider.Text = "SQLOLEDB"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(4, 190)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(111, 29)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Custom Parameters:"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(32, 148)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(81, 17)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Driver:"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(32, 92)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(81, 17)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Provider:"
        '
        'chklocaldb
        '
        Me.chklocaldb.BackColor = System.Drawing.SystemColors.Control
        Me.chklocaldb.Cursor = System.Windows.Forms.Cursors.Default
        Me.chklocaldb.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chklocaldb.Location = New System.Drawing.Point(72, 95)
        Me.chklocaldb.Name = "chklocaldb"
        Me.chklocaldb.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chklocaldb.Size = New System.Drawing.Size(177, 17)
        Me.chklocaldb.TabIndex = 7
        Me.chklocaldb.Text = "LocalDB"
        Me.chklocaldb.UseVisualStyleBackColor = False
        '
        'txtname
        '
        Me.txtname.AcceptsReturn = True
        Me.txtname.BackColor = System.Drawing.SystemColors.Window
        Me.txtname.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtname.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtname.Location = New System.Drawing.Point(72, 8)
        Me.txtname.MaxLength = 0
        Me.txtname.Name = "txtname"
        Me.txtname.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtname.Size = New System.Drawing.Size(209, 20)
        Me.txtname.TabIndex = 0
        '
        'txtpwd
        '
        Me.txtpwd.AcceptsReturn = True
        Me.txtpwd.BackColor = System.Drawing.SystemColors.Window
        Me.txtpwd.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtpwd.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtpwd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtpwd.Location = New System.Drawing.Point(72, 40)
        Me.txtpwd.MaxLength = 0
        Me.txtpwd.Name = "txtpwd"
        Me.txtpwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpwd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtpwd.Size = New System.Drawing.Size(209, 20)
        Me.txtpwd.TabIndex = 1
        '
        'chktrust
        '
        Me.chktrust.BackColor = System.Drawing.SystemColors.Control
        Me.chktrust.Cursor = System.Windows.Forms.Cursors.Default
        Me.chktrust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chktrust.Location = New System.Drawing.Point(72, 119)
        Me.chktrust.Name = "chktrust"
        Me.chktrust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chktrust.Size = New System.Drawing.Size(177, 17)
        Me.chktrust.TabIndex = 2
        Me.chktrust.Text = "Use Trusted Connection"
        Me.chktrust.UseVisualStyleBackColor = False
        '
        'cmdcancel
        '
        Me.cmdcancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdcancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdcancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdcancel.Location = New System.Drawing.Point(200, 379)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdcancel.Size = New System.Drawing.Size(81, 25)
        Me.cmdcancel.TabIndex = 4
        Me.cmdcancel.Text = "Cancel"
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'cmdconnect
        '
        Me.cmdconnect.BackColor = System.Drawing.SystemColors.Control
        Me.cmdconnect.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdconnect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdconnect.Location = New System.Drawing.Point(112, 379)
        Me.cmdconnect.Name = "cmdconnect"
        Me.cmdconnect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdconnect.Size = New System.Drawing.Size(81, 25)
        Me.cmdconnect.TabIndex = 3
        Me.cmdconnect.Text = "Connect"
        Me.cmdconnect.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(65, 17)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Username:"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(8, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(57, 17)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Password:"
        '
        'txtsvr
        '
        Me.txtsvr.AcceptsReturn = True
        Me.txtsvr.BackColor = System.Drawing.SystemColors.Window
        Me.txtsvr.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtsvr.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtsvr.Location = New System.Drawing.Point(72, 69)
        Me.txtsvr.MaxLength = 0
        Me.txtsvr.Name = "txtsvr"
        Me.txtsvr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtsvr.Size = New System.Drawing.Size(209, 20)
        Me.txtsvr.TabIndex = 9
        Me.txtsvr.Text = "MSSQLSERVER"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(8, 69)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(65, 17)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Instance:"
        '
        'chkautologin
        '
        Me.chkautologin.BackColor = System.Drawing.SystemColors.Control
        Me.chkautologin.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkautologin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkautologin.Location = New System.Drawing.Point(8, 379)
        Me.chkautologin.Name = "chkautologin"
        Me.chkautologin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkautologin.Size = New System.Drawing.Size(98, 25)
        Me.chkautologin.TabIndex = 11
        Me.chkautologin.Text = "Auto Login"
        Me.chkautologin.UseVisualStyleBackColor = False
        '
        'frmlogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(289, 411)
        Me.Controls.Add(Me.chkautologin)
        Me.Controls.Add(Me.txtsvr)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.chklocaldb)
        Me.Controls.Add(Me.txtname)
        Me.Controls.Add(Me.txtpwd)
        Me.Controls.Add(Me.chktrust)
        Me.Controls.Add(Me.cmdcancel)
        Me.Controls.Add(Me.cmdconnect)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmlogin"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Connect to Server"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents optintegrated As RadioButton
    Public WithEvents txtsvr As TextBox
    Public WithEvents Label6 As Label
    Public WithEvents chkautologin As CheckBox
    Friend WithEvents Timer1 As Timer
#End Region
End Class