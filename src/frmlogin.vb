Option Strict Off
Option Explicit On

Imports System.Xml

Friend Class frmlogin
    Inherits System.Windows.Forms.Form
    Public sh As Boolean
    Private Sub chktrust_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chktrust.CheckStateChanged
        InputMode(Not chktrust.Checked)
    End Sub

    Private Sub cmdcancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub cmdconnect_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdconnect.Click
        Connect()
    End Sub

    Sub Connect()
        Cursor = System.Windows.Forms.Cursors.WaitCursor
        Dim connectionString As String = ""
        If chklocaldb.Checked Then
            fullsvr = "(localdb)\MSSQLLocalDB"
        Else
            If Not txtinst.Text = "MSSQLSERVER" Then
                fullsvr = If(Not String.IsNullOrEmpty(txtsvr.Text), $"{txtsvr.Text}\{txtinst.Text}", $"localhost\{txtinst.Text}")
            Else
                fullsvr = If(Not String.IsNullOrEmpty(txtsvr.Text), $"{txtsvr.Text}", "localhost")
            End If
        End If
        If optoledb.Checked Then
            ' OLEDB
            prov = 1
            If chktrust.Checked Then
                ' Use Windows authentication
                connectionString = $"Provider={txtprovider.Text};Data Source={fullsvr};Initial Catalog=master;Integrated Security=SSPI;{txtparam.Text}"
            Else
                ' Use SQL Server authentication
                connectionString = $"Provider={txtprovider.Text};Data Source={fullsvr};Initial Catalog=master;User Id={txtname.Text};Password={txtpwd.Text};{txtparam.Text}"
            End If
        ElseIf optodbc.Checked Then
            ' ODBC
            prov = 2
            connectionString = $"Driver={txtdriver.Text};Server={fullsvr};Uid={txtname.Text};Pwd={txtpwd.Text};{txtparam.Text}"
        ElseIf optintegrated.Checked Then
            ' System.Data.SqlClient
            prov = 3
            If chktrust.Checked Then
                ' Use Windows authentication
                connectionString = $"Data Source={fullsvr};Initial Catalog=master;Integrated Security=True;{txtparam.Text}"
            Else
                ' Use SQL Server authentication
                connectionString = $"Data Source={fullsvr};Initial Catalog=master;User ID={txtname.Text};Password={txtpwd.Text};{txtparam.Text}"
            End If
        End If
        Debug.WriteLine(connectionString)
        If ConnectToDatabaseEngine(connectionString) Then
            cUser = txtname.Text
            cPwd = txtpwd.Text
            servername = If(String.IsNullOrEmpty(txtsvr.Text), "localhost", txtsvr.Text)
            instance = If(String.IsNullOrEmpty(txtinst.Text), "MSSQLSERVER", txtinst.Text)
            provider = txtprovider.Text
            driver = txtdriver.Text
            trusted = chktrust.CheckState
            localdb = chklocaldb.CheckState
            autologin = chkautologin.CheckState
            WriteConfigurationToIni()
            frmmain.islocaldb = chklocaldb.Checked
            frmmain.Loadstr(connectionString)
            frmmain.Show()
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Me.Enabled = True
            Me.Hide()
        End If
        Me.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Sub InputMode(ByRef X As Boolean)
        txtname.Enabled = X
        txtpwd.Enabled = X
    End Sub

    Private Sub frmlogin_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Me.Enabled = False
        Try
            ' Check if the XML configuration file exists
            If System.IO.File.Exists(configFileName) Then
                GetConfigValues()
                ' Apply the configuration
                txtname.Text = cUser
                txtpwd.Text = cPwd
                txtsvr.Text = servername
                txtinst.Text = instance
                txtprovider.Text = provider
                txtdriver.Text = driver
                chklocaldb.Checked = localdb
                chktrust.Checked = trusted
                chkautologin.Checked = autologin
                If connectMode = "Integrated" Then
                    optintegrated.Checked = True
                ElseIf connectMode = "ODBC" Then
                    optodbc.Checked = True
                ElseIf connectMode = "OLEDB" Then
                    optoledb.Checked = True
                End If
            Else
                ' The Ini configuration file does not exist
            End If
            RefreshObjects()
            sh = True
            If UpdCheck Then CheckForUpdates()
            sh = False
            If chkautologin.Checked Then Timerstart()

        Catch ex As Exception
            ' Failed to read the configuration file or apply settings
        End Try
    End Sub

    Private Sub GetConfigValues()
        cUser = configFile.ReadString("Login", "UserName", "")
        cPwd = configFile.ReadString("Login", "Password", "")
        servername = configFile.ReadString("Login", "Server", "")
        instance = configFile.ReadString("Login", "Instance", "")
        connectMode = configFile.ReadString("Login", "ConnectMode", "Integrated")
        provider = configFile.ReadString("Login", "Provider", "MSOLEDBSQL")
        driver = configFile.ReadString("Login", "Driver", "SQL Server")
        trusted = configFile.ReadBoolean("Login", "Trusted", False)
        localdb = configFile.ReadBoolean("Login", "LocalDB", False)
        autologin = configFile.ReadBoolean("Login", "AutoLogin", False)
        logtofile = configFile.ReadBoolean("Settings", "LogToFile", False)
        colourQE = configFile.ReadBoolean("Settings", "ColourQE", False)
        disableRND = configFile.ReadBoolean("Settings", "DisableRND", False)
        UpdCheck = configFile.ReadBoolean("Settings", "AutoCheckforUpd", False)
        mdfpath = configFile.ReadString("Settings", "DefaultMDFPath", "")
        ldfpath = configFile.ReadString("Settings", "DefaultLDFPath", "")
    End Sub

    Private Sub Timerstart()
        ' Configure the timer
        Timer1.Interval = 100 ' 1 second = 1000 milliseconds
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = True ' Start the timer
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs)
        ' Stop the timer to prevent the AutoLogin method from executing more than once
        Timer1.Enabled = False

        ' Call the AutoLogin method
        Connect()
    End Sub

    Private Sub optodbc_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optodbc.CheckedChanged
        If eventSender.Checked Then
            RefreshObjects()
        End If
    End Sub

    Private Sub optoledb_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optoledb.CheckedChanged
        If eventSender.Checked Then
            RefreshObjects()
        End If
    End Sub

    Sub RefreshObjects()
        Me.txtprovider.Enabled = Me.optoledb.Checked
        Me.txtdriver.Enabled = Me.optodbc.Checked
        If Not chkautologin.Checked Then Me.Enabled = True
        txtsvr.Enabled = Not chklocaldb.Checked
        txtinst.Enabled = Not chklocaldb.Checked
    End Sub

    Private Sub optintegrated_CheckedChanged(sender As Object, e As EventArgs) Handles optintegrated.CheckedChanged
        RefreshObjects()
    End Sub

    Private Sub chklocaldb_CheckedChanged(sender As Object, e As EventArgs) Handles chklocaldb.CheckedChanged
        RefreshObjects()
    End Sub
End Class