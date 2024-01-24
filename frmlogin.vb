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
            prov = "sqloledb"
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

            ' Ruta del archivo XML de configuración en el directorio de la aplicación
            Dim configFileName As String = "SSMLConf.xml"
            Dim configFilePath As String = System.IO.Path.Combine(Application.StartupPath, configFileName)

            Try
                ' Create a new XML configuration file
                Using writer As New XmlTextWriter(configFilePath, Nothing)
                    ' Start the XML document
                    writer.WriteStartDocument()

                    ' Root element <Configuration>
                    writer.WriteStartElement("Configuration")

                    ' Elements within <Configuration>
                    writer.WriteElementString("Username", cUser)
                    writer.WriteElementString("Password", cPwd)
                    ' If Len(cPwd) > 0 Then
                    '     writer.WriteElementString("Password", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(cPwd)))
                    ' End If

                    If Me.optoledb.Checked Then
                        writer.WriteElementString("ConnectMode", "OLEDB")
                        writer.WriteElementString("Provider", Me.txtprovider.Text)
                    ElseIf Me.optodbc.Checked Then
                        writer.WriteElementString("ConnectMode", "ODBC")
                        writer.WriteElementString("Driver", Me.txtdriver.Text)
                    ElseIf Me.optintegrated.Checked Then
                        writer.WriteElementString("ConnectMode", "Integrated")
                        writer.WriteElementString("Instance", Me.txtsvr.Text)
                    End If

                    writer.WriteElementString("Trusted", If(chktrust.CheckState = System.Windows.Forms.CheckState.Checked, "1", "0"))
                    writer.WriteElementString("localdb", If(Me.chklocaldb.CheckState = System.Windows.Forms.CheckState.Checked, "1", "0"))

                    ' Close the root element <Configuration>
                    writer.WriteEndElement()

                    ' Finish the XML document
                    writer.WriteEndDocument()
                End Using


            Catch ex As Exception
            End Try
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
        ' Path to the XML configuration file in the application directory
        Dim configFileName As String = "SSMLConf.xml"
        Dim configFilePath As String = System.IO.Path.Combine(Application.StartupPath, configFileName)

        Try
            ' Check if the XML configuration file exists
            If System.IO.File.Exists(configFilePath) Then
                ' Create an XML reader
                Using reader As New XmlTextReader(configFilePath)
                    ' Variables to store the read configuration
                    Dim username As String = ""
                    Dim password As String = ""
                    Dim connectMode As String = ""
                    Dim provider As String = ""
                    Dim driver As String = ""
                    Dim trusted As Boolean = False
                    Dim localdb As Boolean = False

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
                                Case "Trusted"
                                    reader.Read()
                                    trusted = (reader.Value = "1")
                                Case "localdb"
                                    reader.Read()
                                    localdb = (reader.Value = "1")
                            End Select
                        End If
                    End While

                    ' Apply the configuration
                    txtname.Text = username
                    txtpwd.Text = password
                    chklocaldb.Checked = localdb
                    chktrust.Checked = trusted
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
        Catch ex As Exception
            ' Handle exceptions as needed
        End Try
    End Sub


    Private Sub SaveConfiguration()
        Try
            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

            config.AppSettings.Settings("Provider").Value = Me.txtprovider.Text
            config.AppSettings.Settings("Driver").Value = Me.txtdriver.Text
            config.AppSettings.Settings("ConnectMode").Value = If(Me.optoledb.Checked, "OLEDB", If(Me.optodbc.Checked, "ODBC", String.Empty))
            config.AppSettings.Settings("Username").Value = Me.txtname.Text
            config.AppSettings.Settings("Password").Value = If(String.IsNullOrEmpty(Me.txtpwd.Text), String.Empty, Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Me.txtpwd.Text)))
            config.AppSettings.Settings("SQLExpress").Value = If(Me.chklocaldb.CheckState = CheckState.Checked, "1", "0")
            config.AppSettings.Settings("Trusted").Value = If(Me.chktrust.CheckState = CheckState.Checked, "1", "0")

            config.Save(ConfigurationSaveMode.Modified)
            ConfigurationManager.RefreshSection("appSettings")
        Catch ex As Exception
        End Try
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