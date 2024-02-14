Option Strict Off
Option Explicit On

Imports System.Configuration
Imports System.Xml

Friend Class frmlogin
    Inherits System.Windows.Forms.Form
    Public username As String = ""
    Public password As String = ""
    Public instance As String = ""
    Public connectMode As String = ""
    Public provider As String = ""
    Public driver As String = ""
    Public trusted As Boolean = False
    Public localdb As Boolean = False
    Public autologin As Boolean = False

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
        Dim prov As String
        Dim connectionString As String
        Dim serverName As String
        If chklocaldb.Checked Then
            serverName = "(localdb)\MSSQLLocalDB"
        Else
            If Not txtsvr.Text = "MSSQLSERVER" Then
                serverName = "localhost\" & txtsvr.Text
            Else
                serverName = "localhost\"
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
        If ConnectDB(connectionString, prov) Then
            cUser = txtname.Text
            cPwd = txtpwd.Text
            instance = txtsvr.Text
            provider = txtprovider.Text
            driver = txtdriver.Text
            trusted = chktrust.CheckState
            localdb = chklocaldb.CheckState
            autologin = chkautologin.CheckState
            WriteXML(cUser, cPwd, instance, provider, driver, trusted, localdb, autologin, defaultmdf, defaultldf)
            frmmain.islocaldb = chklocaldb.Checked
            frmmain.Loadstr(connectionString)
            frmmain.Show()

            Me.Cursor = System.Windows.Forms.Cursors.Default

            Me.Hide()

        End If

        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Sub InputMode(ByRef X As Boolean)
        txtname.Enabled = X
        txtpwd.Enabled = X
    End Sub

    Private Sub frmlogin_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
                                    username = reader.Value
                                Case "Password"
                                    reader.Read()
                                    password = reader.Value
                                Case "ConnectMode"
                                    reader.Read()
                                    connectMode = reader.Value
                                Case "Provider"
                                    reader.Read()
                                    provider = reader.Value
                                Case "Driver"
                                    reader.Read()
                                    driver = reader.Value
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
                                Case "DefaultMDFPath"
                                    reader.Read()
                                    defaultmdf = reader.Value
                                Case "DefaultLDFPath"
                                    reader.Read()
                                    defaultldf = reader.Value
                            End Select
                        End If
                    End While

                    ' Apply the configuration
                    txtname.Text = username
                    txtpwd.Text = password
                    txtsvr.Text = instance
                    chklocaldb.Checked = localdb
                    chktrust.Checked = trusted
                    chkautologin.Checked = autologin
                    If connectMode = "OLEDB" Then
                        optoledb.Checked = True
                    ElseIf connectMode = "ODBC" Then
                        optodbc.Checked = True
                    ElseIf connectMode = "Integrated" Then
                        optintegrated.Checked = True
                    End If
                End Using
            Else
                ' The XML configuration file does not exist
                ' You can handle this according to your needs
            End If
            RefreshObjects()
            If chkautologin.Checked Then
                Timerstart()
            End If
        Catch ex As Exception
            ' Handle exceptions as needed
        End Try
    End Sub

    Private Sub Timerstart()
        ' Configure the timer
        Timer1.Interval = 1000 ' 1 second = 1000 milliseconds
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
        txtsvr.Enabled = optintegrated.Checked
    End Sub

    Private Sub optintegrated_CheckedChanged(sender As Object, e As EventArgs) Handles optintegrated.CheckedChanged
        RefreshObjects()
    End Sub

End Class