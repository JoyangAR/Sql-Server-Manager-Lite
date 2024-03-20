Option Strict Off
Option Explicit On

Imports System.Configuration
Imports System.Xml

Friend Class frmlogin
    Inherits System.Windows.Forms.Form

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
            servername = "(localdb)\MSSQLLocalDB"
        Else
            If Not txtinst.Text = "MSSQLSERVER" Then
                serverName = If(Not String.IsNullOrEmpty(txtsvr.Text), $"{txtsvr.Text}\{txtinst.Text}", $"localhost\{txtinst.Text}")
            Else
                serverName = If(Not String.IsNullOrEmpty(txtsvr.Text), $"{txtsvr.Text}\", "localhost\")
            End If
        End If
        If optoledb.Checked Then
            ' OLEDB
            connectionString = $"Provider={txtprovider.Text};Data Source={serverName};Initial Catalog=master;User Id={txtname.Text};Password={txtpwd.Text};"
            prov = "sqloledb"
        ElseIf optodbc.Checked Then
            ' ODBC
            connectionString = $"Driver={txtdriver.Text};Server={serverName};Uid={txtname.Text};Pwd={txtpwd.Text};"
            prov = "odbc"
        ElseIf optintegrated.Checked Then
            prov = "integrated"
            If chktrust.Checked Then
                ' Use Windows authentication
                connectionString = $"Data Source={serverName};Initial Catalog=master;Integrated Security=True;"
            Else
                ' Use SQL Server authentication
                connectionString = $"Data Source={serverName};Initial Catalog=master;User ID={txtname.Text};Password={txtpwd.Text};"
            End If
        End If
        If ConnectDB(connectionString) Then
            cUser = txtname.Text
            cPwd = txtpwd.Text
            server = If(String.IsNullOrEmpty(txtsvr.Text), "localhost", txtsvr.Text)
            instance = If(String.IsNullOrEmpty(txtinst.Text), "MSSQLSERVER", txtinst.Text)
            provider = txtprovider.Text
            driver = txtdriver.Text
            trusted = chktrust.CheckState
            localdb = chklocaldb.CheckState
            autologin = chkautologin.CheckState
            WriteXML()
            frmmain.islocaldb = chklocaldb.Checked
            frmmain.Loadstr(connectionString)
            frmmain.Show()
            Me.Cursor = System.Windows.Forms.Cursors.Default
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
            If System.IO.File.Exists(configFilePath) Then
                ' Create an XML reader
                Using reader As New XmlTextReader(configFilePath)
                    ' Read the XML file
                    While reader.Read()
                        If reader.NodeType = XmlNodeType.Element Then
                            Select Case reader.Name
                                Case "Username"
                                    reader.Read()
                                    cUser = reader.Value
                                Case "Password"
                                    reader.Read()
                                    cPwd = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(reader.Value))
                                Case "ConnectMode"
                                    reader.Read()
                                    connectMode = reader.Value
                                Case "Provider"
                                    reader.Read()
                                    provider = reader.Value
                                Case "Driver"
                                    reader.Read()
                                    driver = reader.Value
                                Case "Server"
                                    reader.Read()
                                    server = reader.Value
                                Case "Instance"
                                    reader.Read()
                                    instance = reader.Value
                                Case "Trusted"
                                    reader.Read()
                                    trusted = (reader.Value = "1")
                                Case "LocalDB"
                                    reader.Read()
                                    localdb = (reader.Value = "1")
                                Case "AutoLogin"
                                    reader.Read()
                                    autologin = (reader.Value = "1")
                                Case "LogToFile"
                                    reader.Read()
                                    logtofile = (reader.Value = "1")
                                Case "ColourQE"
                                    reader.Read()
                                    colourQE = (reader.Value = "1")
                                Case "DisableRND"
                                    reader.Read()
                                    disableRND = (reader.Value = "1")
                                Case "AutoCheckforUpd"
                                    reader.Read()
                                    UpdCheck = (reader.Value = "1")
                                Case "DefaultMDFPath"
                                    reader.Read()
                                    mdfpath = reader.Value
                                Case "DefaultLDFPath"
                                    reader.Read()
                                    ldfpath = reader.Value
                            End Select
                        End If
                    End While

                    ' Apply the configuration
                    txtname.Text = cUser
                    txtpwd.Text = cPwd
                    txtsvr.Text = server
                    txtinst.Text = instance
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
                End Using
            Else
                ' The XML configuration file does not exist
                ' You can handle this according to your needs
            End If
            RefreshObjects()
            If UpdCheck Then CheckForUpdates()
            If chkautologin.Checked Then Timerstart()

        Catch ex As Exception
            ' Handle exceptions as needed
        End Try
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
        chklocaldb.Enabled = optintegrated.Checked
        txtinst.Enabled = optintegrated.Checked
        txtsvr.Enabled = optintegrated.Checked
        If Not chkautologin.Checked Then Me.Enabled = True
    End Sub

    Private Sub optintegrated_CheckedChanged(sender As Object, e As EventArgs) Handles optintegrated.CheckedChanged
        RefreshObjects()
    End Sub

End Class