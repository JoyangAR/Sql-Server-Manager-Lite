Option Strict Off
Option Explicit On

Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Net.NetworkInformation
Imports System.Security.Principal
Imports System.Xml
Imports Microsoft.VisualBasic.Compatibility.VB6
Imports Microsoft.Win32

Module Module1
    Public con As New ADODB.Connection
    Public connection As New SqlConnection

    ' Path to the configuration XML file in the application directory
    Public configFileName As String = "SSMLConf.xml"
    Public configFilePath As String = System.IO.Path.Combine(Application.StartupPath, configFileName)
    Public defaultmdf As String
    Public defaultldf As String
    ' Other variables to replace objects from the Std Framework
    Public prov2 As String

    Public inf As Object ' Replace with the appropriate data type
    Public reg1 As Object ' Replace with the appropriate data type
    Public cUser As String
    Public cPwd As String
    Public srv1 As Object ' Replace with the appropriate data type
    Public srvc As Object ' Replace with the appropriate data type

    Enum pShrinkMode
        pReleaseUnused = 0
        pReorganizeFirst = 1
        pEmptyLog = 2
    End Enum

    Enum pRepairMode
        pFast
        pStandard
        pForced
    End Enum

    'UPGRADE_WARNING: Application will terminate when Sub Main() finishes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="E08DDC71-66BA-424F-A612-80AF11498FF8"'
    Public Sub Main()

        If Not IsUserAdministrator() Then
            MsgBox("Please run this program as administrator", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        'If App.PrevInstance = True Then
        '    MsgBox(My.Application.Info.Title & " is alredy running", MsgBoxStyle.Exclamation, "")
        '    End
        'End If

        frmlogin.Show()

    End Sub

    Function IsUserAdministrator() As Boolean
        Dim identity = WindowsIdentity.GetCurrent()
        Dim principal = New WindowsPrincipal(identity)
        Return principal.IsInRole(WindowsBuiltInRole.Administrator)
    End Function

    Function AttachData(ByRef DatabaseName As String, ByRef path As String, Optional ByRef errmsg As String = "") As Boolean
        On Error GoTo ErrorHandler

        System.Windows.Forms.Application.DoEvents()
        If prov2 = "sqloledb" Then
            con.Execute("CREATE DATABASE [" & DatabaseName & "] ON (FILENAME = '" & path & "') FOR ATTACH_REBUILD_LOG;")
        Else
            Using connection As New SqlConnection(frmmain.strlogin)
                connection.Open()

                Using cmd As New SqlCommand("CREATE DATABASE [" & DatabaseName & "] ON (FILENAME = '" & path & "') FOR ATTACH_REBUILD_LOG;", connection)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

        End If
        Return True
        Exit Function
ErrorHandler:
        errmsg = Err.Description
        Return False
    End Function

    Function TabulateDatabase(ByRef bakPath As String, Optional ByRef dbname As String = "", Optional ByRef LogName As String = "", Optional ByRef errmsg As String = "", Optional ByRef datapath As String = "", Optional ByRef logpath As String = "", Optional ByRef mdfid As Integer = 0) As Boolean
        On Error GoTo ErrorHandler
        Dim resultList As New List(Of String)()
        Dim rs As SqlDataReader

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            rs = con.Execute($"RESTORE FILELISTONLY FROM DISK = '{bakPath}'").ExecuteReader()
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String = $"RESTORE FILELISTONLY FROM DISK = '{bakPath}'"
                Using cmd As New SqlCommand(commandText, con)
                    rs = cmd.ExecuteReader()

                    While rs.Read()
                        Dim fileType As String = rs("Type").ToString().Trim().ToLower()
                        If fileType = "d" Then ' "d" for Data
                            dbname = rs("LogicalName").ToString() ' Logical name of the MDF file
                            datapath = rs("PhysicalName").ToString() ' Physical location of the MDF file
                        ElseIf fileType = "l" Then ' "l" for Log
                            LogName = rs("LogicalName").ToString() ' Logical name of the LDF file
                            logpath = rs("PhysicalName").ToString() ' Physical location of the LDF file
                        End If
                    End While
                End Using
            End Using
        End If

        If Not String.IsNullOrEmpty(datapath) And Not String.IsNullOrEmpty(logpath) Then
            TabulateDatabase = True
        Else
            errmsg = "Failed to retrieve complete backup file information."
            TabulateDatabase = False
        End If

        Exit Function

ErrorHandler:
        errmsg = Err.Description
        TabulateDatabase = False
    End Function

    Function CorrectDBname(ByRef dbname As String) As String
        Dim tmp As String

        If LCase(dbname).Contains("_dat") Or LCase(dbname).Contains("_data") Then
            If LCase(dbname).EndsWith("_dat") Then
                tmp = Left(dbname, Len(dbname) - 4)
            ElseIf LCase(dbname).EndsWith("_data") Then
                tmp = Left(dbname, Len(dbname) - 5)
            End If
        Else
            tmp = dbname
        End If

        CorrectDBname = tmp
    End Function

    Function OnlyDirPath(ByVal fullpath As String) As String
        Dim dirPath As String = System.IO.Path.GetDirectoryName(fullpath)
        dirPath = EnsureTrailingBackslash(dirPath)
        Return dirPath
    End Function

    Function CreateAccount(ByRef username As String, ByRef Password As String, Optional ByRef errmsg As String = "") As Boolean
        On Error GoTo ErrorHandler

        System.Windows.Forms.Application.DoEvents()
        If prov2 = "sqloledb" Then
            con.Execute("USE [master]" & vbCrLf & "Create LOGIN [" & username & "] WITH PASSWORD = N'" & Password & "',DEFAULT_Database=[master],CHECK_EXPIRATION=OFF,CHECK_POLICY = OFF" & vbCrLf & "EXEC master..sp_addsrvrolemember @loginame = N'" & username & "', @rolename = N'sysadmin'")
        Else
            Using connection As New SqlConnection(frmmain.strlogin)
                connection.Open()

                Using cmd As New SqlCommand("USE [master]" & vbCrLf & "Create LOGIN [" & username & "] WITH PASSWORD = N'" & Password & "',DEFAULT_Database=[master],CHECK_EXPIRATION=OFF,CHECK_POLICY = OFF" & vbCrLf & "EXEC master..sp_addsrvrolemember @loginame = N'" & username & "', @rolename = N'sysadmin'", connection)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End If
        Return True
        Exit Function

ErrorHandler:
        errmsg = Err.Description
        CreateAccount = False
    End Function

    Function ConnectDB(ByRef str_Renamed As String, ByRef prov1 As String) As Boolean
        prov2 = prov1
        Try

            Debug.Print(str_Renamed)
            If prov1 = "sqloledb" Then
                con.CommandTimeout = 0
                con.CursorLocation = ADODB.CursorLocationEnum.adUseServer
                con.Open(str_Renamed)
            ElseIf prov1 = "integrated" Then
                connection.ConnectionString = str_Renamed
                connection.Open()
            End If

            ConnectDB = True

            Exit Function
        Catch ex As Exception
            MsgBox(ex.Message)
            ConnectDB = False
        End Try
    End Function

    Function DatabaseExists(ByRef dbname As String) As Boolean

        If prov2 = "sqloledb" Then
            con.Execute("use " & dbname & "")
            DatabaseExists = True
        Else
            Try
                Using connection As New SqlConnection(frmmain.strlogin)
                    connection.Open()

                    Using cmd As New SqlCommand("use " & dbname, connection)
                        cmd.ExecuteNonQuery()
                    End Using

                    Return True
                End Using
            Catch ex As Exception
                Return False
            End Try
        End If
        Exit Function

        DatabaseExists = False
    End Function

    Public Function RestoreDatabase2(ByRef bckpath As String, ByRef NewDBDatapath As String, ByRef NewDBLogpath As String, ByRef dbfile As String, ByRef logfile As String, Optional ByRef errmsg As String = "", Optional ByRef MDF_Only As Boolean = False, Optional ByRef MDF_ID As Integer = 0) As Boolean
        On Error GoTo ErrorHandler

        Dim db1 As String
        Dim str_Renamed As String

        db1 = CorrectDBname(dbfile)

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            con.Execute("USE master")
            System.Windows.Forms.Application.DoEvents()

            If Not MDF_Only Then
                str_Renamed = $"RESTORE DATABASE {db1} FROM DISK='{bckpath}' WITH MOVE '{dbfile}' TO '{NewDBDatapath}{db1}.mdf', MOVE '{logfile}' TO '{NewDBLogpath}{db1}_log.ldf'"
            Else
                str_Renamed = $"RESTORE DATABASE {db1} FROM DISK='{bckpath}' WITH FILE=1, RECOVERY, MOVE '{dbfile}' TO '{NewDBDatapath}{db1}.mdf', MOVE '{logfile}' TO '{NewDBLogpath}{db1}_log.ldf'"
            End If
            con.Execute(str_Renamed)

            ' Alter the database to set to single-user mode and back to multi-user mode
            con.Execute($"ALTER DATABASE {db1} SET SINGLE_USER WITH ROLLBACK IMMEDIATE")
            con.Execute($"ALTER DATABASE {db1} SET MULTI_USER")
        ElseIf prov2.ToLower() = "integrated" Then
            Using connnection As New SqlConnection(frmmain.strlogin)
                connnection.Open()

                Dim commandText As String

                If Not MDF_Only Then
                    commandText = $"RESTORE DATABASE [{db1}] FROM DISK='{bckpath}' WITH MOVE '{dbfile}' TO '{NewDBDatapath}{db1}.mdf', MOVE '{logfile}' TO '{NewDBLogpath}{db1}_log.ldf'"
                Else
                    commandText = $"RESTORE DATABASE [{db1}] FROM DISK='{bckpath}' WITH FILE=1, RECOVERY, MOVE '{dbfile}' TO '{NewDBDatapath}{db1}.mdf', MOVE '{logfile}' TO '{NewDBLogpath}{db1}_log.ldf'"
                End If
                Using cmd As New SqlCommand(commandText, connnection)
                    cmd.ExecuteNonQuery()
                End Using

                ' Alter the database to set to single-user mode and back to multi-user mode
                Using cmd As New SqlCommand($"ALTER DATABASE [{db1}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", connnection)
                    cmd.ExecuteNonQuery()
                End Using
                Using cmd As New SqlCommand($"ALTER DATABASE [{db1}] SET MULTI_USER", connnection)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End If

        RestoreDatabase2 = True
        Exit Function

ErrorHandler:
        Debug.Print(str_Renamed)
        errmsg = Err.Description
        RestoreDatabase2 = False
        ' Close the connection if an error occurs
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Function

    Function RestoreDatabase(ByRef bckfile As String, ByRef dbname As String, Optional ByRef errmsg As String = "") As Boolean
        On Error GoTo ErrorHandler

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            con.Execute($"RESTORE DATABASE {dbname} FROM DISK = '{bckfile}' WITH REPLACE")
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String = $"RESTORE DATABASE [{dbname}] FROM DISK = '{bckfile}' WITH REPLACE"

                Using cmd As New SqlCommand(commandText, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End If

        RestoreDatabase = True
        Exit Function

ErrorHandler:
        Debug.Print($"RESTORE DATABASE {dbname} FROM DISK = '{bckfile}' WITH REPLACE")

        errmsg = Err.Description
        RestoreDatabase = False
        ' Close the connection if an error occurs
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Function

    Function ListUsers() As Collection
        Dim col As New Collection
        Dim tmp As String

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            Dim rs1 As New ADODB.Recordset
            rs1.Open("SELECT * FROM sys.server_principals where type='S' and name<>'sa' and name not like '##MS_%'", con, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic)

            Do Until rs1.EOF
                tmp = ""
                tmp = rs1.Fields(0).Value
                col.Add(tmp)
                rs1.MoveNext()
            Loop

            rs1.Close()
        ElseIf prov2.ToLower() = "integrated" Then
            ' New code with System.Data.SqlClient
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim query As String = "SELECT * FROM sys.server_principals WHERE type='S' AND name<>'sa' AND name NOT LIKE '##MS_%'"
                Using cmd As New SqlCommand(query, con)
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    While reader.Read()
                        tmp = ""
                        tmp = reader(0).ToString()
                        col.Add(tmp)
                    End While
                End Using
            End Using
        End If

        Return col

    End Function

    Function ListDatabases() As Collection
        Dim col As New Collection
        Dim tmp As String

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            Dim rs1 As New ADODB.Recordset
            rs1 = con.Execute("sp_databases")

            Do Until rs1.EOF
                tmp = ""
                tmp = rs1.Fields(0).Value

                If LCase(tmp) <> "master" And LCase(tmp) <> "msdb" And LCase(tmp) <> "tempdb" And LCase(tmp) <> "model" Then
                    col.Add(tmp)
                End If

                rs1.MoveNext()
            Loop

            rs1.Close()
        ElseIf prov2.ToLower() = "integrated" Then
            ' New code with System.Data.SqlClient
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim query As String = "SELECT name FROM sys.databases WHERE name NOT IN ('master', 'msdb', 'tempdb', 'model')"
                Using cmd As New SqlCommand(query, con)
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    While reader.Read()
                        tmp = ""
                        tmp = reader("name").ToString()
                        col.Add(tmp)
                    End While
                End Using
            End Using
        End If

        Return col
    End Function

    Function ChangePwd(ByRef username As String, ByRef pwd As String, Optional ByRef errmsg As String = "") As Boolean
        On Error GoTo ErrorHandler

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            con.Execute($"ALTER LOGIN [{username}] WITH PASSWORD=N'{pwd}'")
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String = $"ALTER LOGIN [{username}] WITH PASSWORD=N'{pwd}'"

                Using cmd As New SqlCommand(commandText, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End If

        ChangePwd = True
        Exit Function

ErrorHandler:
        errmsg = Err.Description
        ChangePwd = False
    End Function

    Function DeleteAccount(ByRef username As String, Optional ByRef errmsg As String = "") As Boolean
        On Error GoTo ErrorHandler

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            con.Execute($"DROP LOGIN [{username}]")
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String = $"DROP LOGIN [{username}]"

                Using cmd As New SqlCommand(commandText, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End If

        DeleteAccount = True
        Exit Function

ErrorHandler:
        errmsg = Err.Description
        DeleteAccount = False
    End Function

    Function AccountExists(ByVal username As String) As Boolean
        Dim tmp As Boolean = False

        Try
            If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
                Dim rs1 As New ADODB.Recordset
                rs1.Open($"SELECT * FROM sys.server_principals WHERE name='{username}' AND type='S'", con, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic)

                ' Check if the recordset is empty (EOF)
                If Not rs1.EOF Then
                    tmp = True
                End If

                rs1.Close()
            ElseIf prov2.ToLower() = "integrated" Then
                Using con As New SqlConnection(frmmain.strlogin)
                    con.Open()

                    Dim commandText As String = $"SELECT * FROM sys.server_principals WHERE name='{username}' AND type='S'"
                    Using cmd As New SqlCommand(commandText, con)
                        Using reader As SqlDataReader = cmd.ExecuteReader()
                            ' Check if the recordset is empty
                            If reader.HasRows Then
                                tmp = True
                            End If
                        End Using
                    End Using
                End Using
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "")
        End Try

        Return tmp
    End Function

    Function RepairDB(ByRef dbname As String, Optional ByRef forced As pRepairMode = pRepairMode.pStandard, Optional ByRef errmsg As String = "", Optional ByRef rs As ADODB.Recordset = Nothing) As Boolean
        Dim str_Renamed As String

        On Error GoTo ErrorHandler

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            If forced = pRepairMode.pForced Then
                str_Renamed = $"ALTER DATABASE {dbname} SET EMERGENCY; ALTER DATABASE {dbname} SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DBCC CHECKDB ({dbname}, REPAIR_ALLOW_DATA_LOSS); ALTER DATABASE {dbname} SET MULTI_USER"
            ElseIf forced = pRepairMode.pStandard Then
                str_Renamed = $"ALTER DATABASE {dbname} SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DBCC CHECKDB ({dbname}, REPAIR_REBUILD); ALTER DATABASE {dbname} SET MULTI_USER"
            ElseIf forced = pRepairMode.pFast Then
                str_Renamed = $"ALTER DATABASE {dbname} SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DBCC CHECKDB ({dbname}, REPAIR_FAST); ALTER DATABASE {dbname} SET MULTI_USER"
            End If
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String
                If forced = pRepairMode.pForced Then
                    commandText = $"ALTER DATABASE [{dbname}] SET EMERGENCY; ALTER DATABASE [{dbname}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DBCC CHECKDB ({dbname}, REPAIR_ALLOW_DATA_LOSS); ALTER DATABASE [{dbname}] SET MULTI_USER"
                ElseIf forced = pRepairMode.pStandard Then
                    commandText = $"ALTER DATABASE [{dbname}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DBCC CHECKDB ({dbname}, REPAIR_REBUILD); ALTER DATABASE [{dbname}] SET MULTI_USER"
                ElseIf forced = pRepairMode.pFast Then
                    commandText = $"ALTER DATABASE [{dbname}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DBCC CHECKDB ({dbname}, REPAIR_FAST); ALTER DATABASE [{dbname}] SET MULTI_USER"
                End If

                Using cmd As New SqlCommand(commandText, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End If

        Debug.Print(str_Renamed)

        RepairDB = True
        Exit Function

ErrorHandler:
        Dim errLoop As SqlError

        ' Loop through each Error object in Errors collection.
        If con.Errors.Count > 1 Then
            For Each errLoop In con.Errors
                errmsg = errLoop.Message
                frmmain.Logg(errmsg)
            Next errLoop
        Else
            errmsg = Err.Description
        End If

        RepairDB = False
    End Function

    Function DeleteDatabase(ByRef dbname As String, Optional ByRef errmsg As String = "") As Boolean
        On Error GoTo ErrorHandler

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            con.Execute("DROP DATABASE " & dbname)
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String = $"DROP DATABASE [{dbname}]"

                Using cmd As New SqlCommand(commandText, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End If

        DeleteDatabase = True
        Exit Function

ErrorHandler:
        errmsg = Err.Description
        DeleteDatabase = False
    End Function

    Function DirExists(ByRef DirName As String) As Boolean
        On Error GoTo ErrorHandler
        ' test the directory attribute
        DirExists = GetAttr(DirName) And FileAttribute.Directory
ErrorHandler:
        ' if an error occurs, this function returns False
    End Function

    Function BackupDatabase(ByRef dbname1 As String, ByRef ipath As String, ByRef bckfile As String, Optional ByRef errmsg As String = "") As Boolean
        Dim newbck As String = ""

        On Error GoTo ErrorHandler

        newbck = $"{dbname1}_{Today:MM-dd-yyyy}_{Now:hhmmss}.bak"
        bckfile = newbck
        System.Windows.Forms.Application.DoEvents()

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            con.Execute($"BACKUP DATABASE [{dbname1}] TO DISK = N'{ipath}{newbck}' WITH NOFORMAT, INIT, NAME = N'{dbname1}-Full Database Backup', SKIP, NOREWIND, NOUNLOAD")
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String = $"BACKUP DATABASE [{dbname1}] TO DISK = N'{ipath}{newbck}' WITH NOFORMAT, INIT, NAME = N'{dbname1}-Full Database Backup', SKIP, NOREWIND, NOUNLOAD"

                Using cmd As New SqlCommand(commandText, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End If

        BackupDatabase = True
        Exit Function

ErrorHandler:
        errmsg = Err.Description
        BackupDatabase = False
    End Function

    Sub FilterInput(ByRef KeyAscii As Short)
        KeyAscii = Format(KeyAscii, GetKeyAsciiMode())
    End Sub

    Function GetKeyAsciiMode() As Integer
        ' You can implement logic to determine the KeyAscii mode here
        ' For example, you could use regular expressions to detect SQL injection patterns.
        Dim keyAsciiMode As Integer = 0 ' Default mode
        ' ... additional logic ...
        Return keyAsciiMode
    End Function

    Function GuestAllowed(ByRef dbname As String) As Boolean
        System.Windows.Forms.Application.DoEvents()

        On Error GoTo ErrorHandler

        Dim tmp As Boolean

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            Dim rs1 As New ADODB.Recordset
            con.Execute("use " & dbname)
            rs1.Open("SELECT name, hasdbaccess From sys.sysusers WHERE name = 'guest'", con, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic)

            If rs1.RecordCount = 0 Then
                tmp = False
            Else
                If Val(rs1.Fields("hasdbaccess").Value) = 1 Then
                    tmp = True
                Else
                    tmp = False
                End If
            End If

            rs1.Close()
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim query As String = "SELECT name, hasdbaccess FROM sys.sysusers WHERE name = 'guest'"
                Using cmd As New SqlCommand(query, con)
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    If reader.Read() Then
                        tmp = CBool(reader("hasdbaccess"))
                    Else
                        ' Handle case where no records were found
                        tmp = False
                    End If
                End Using
            End Using
        End If

        GuestAllowed = tmp

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            con.Execute("use master")
        End If

        Exit Function

ErrorHandler:
        GuestAllowed = False
    End Function

    Sub SetGuest(ByRef dbname As String, ByRef Grant As Boolean)

        Dim str_Renamed As String

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            If Grant Then
                str_Renamed = "GRANT CONNECT TO GUEST"
            Else
                str_Renamed = "REVOKE CONNECT FROM GUEST"
            End If

            System.Windows.Forms.Application.DoEvents()
            con.Execute("use " & dbname & " " & str_Renamed)
            str_Renamed = LCase(str_Renamed)
            con.Execute("use " & dbname & " " & str_Renamed)

        ElseIf prov2.ToLower() = "integrated" Then
            Using connection As New SqlConnection(frmmain.strlogin)
                connection.Open()

                Dim commandText As String
                If Grant Then
                    commandText = "USE " & dbname & "; GRANT CONNECT TO GUEST"
                Else
                    commandText = "USE " & dbname & "; REVOKE CONNECT FROM GUEST"
                End If

                Using cmd As New SqlCommand(commandText, connection)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End If

    End Sub

    Function GetDatabasePath() As String

        Dim tmp As String


        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            Dim rs1 As New ADODB.Recordset
            con.Execute("use master")
            rs1.Open("SELECT name, physical_name FROM master.sys.master_files where name='master'", con, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            tmp = rs1.Fields("physical_name").Value
            tmp = Replace(tmp, "master.mdf", "")
            rs1.Close()
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim query As String = "SELECT name, physical_name FROM master.sys.master_files WHERE name='master'"
                Using cmd As New SqlCommand(query, con)
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    If reader.Read() Then
                        tmp = reader("physical_name").ToString()
                        tmp = tmp.Replace("master.mdf", "")
                    Else
                        ' Handle case where no records were found
                        tmp = ""
                    End If
                End Using
            End Using
        End If


        Return tmp

    End Function

    Function GetInstanceVersion(ByRef version As String, ByRef errmsg As String)

        On Error GoTo ErrorHandler

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            Dim rs As New ADODB.Recordset

            rs.Open("SELECT @@VERSION AS version", con, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            If Not rs.EOF Then
                version = rs.Fields("version").Value.ToString()
            End If
            rs.Close()

        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)

                con.Open()
                Dim query As String = "SELECT @@VERSION AS version"
                Using cmd As New SqlCommand(query, con)
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    If reader.Read() Then
                        version = reader("version").ToString()
                    Else
                        ' Handle case where no records were found
                        version = "No version found."
                    End If
                End Using

            End Using
        End If

        Return True
        Return version
        Exit Function

ErrorHandler:
        errmsg = Err.Description
        Return False
    End Function

    Function ChangeDefaultDataLocation(ByRef newDataPath As String) As Boolean
        Dim result As Boolean = False

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            defaultmdf = newDataPath
            result = True
        ElseIf prov2.ToLower() = "integrated" Then
            defaultmdf = newDataPath
            result = True
        End If

        Return result
    End Function

    Function ChangeDefaultLogLocation(ByRef newLogPath As String) As Boolean
        Dim result As Boolean = False

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            defaultldf = newLogPath
            result = True
        ElseIf prov2.ToLower() = "integrated" Then
            defaultldf = newLogPath
            result = True
        End If

        Return result
    End Function

    Function GetDefaultDataAndLogLocations(Optional ByRef DefaultDataPath As String = "", Optional ByRef DefaultLogPath As String = "", Optional ByRef errmsg As String = "") As Boolean

        On Error GoTo ErrorHandler
        ' Check if default data and log paths are already set
        If Not String.IsNullOrEmpty(defaultmdf) Or Not String.IsNullOrEmpty(defaultldf) Then
            ' If default values are already set, return them
            DefaultDataPath = EnsureTrailingBackslash(defaultmdf)
            DefaultLogPath = EnsureTrailingBackslash(defaultldf)
        Else
            If Not frmmain.islocaldb Then
                ' If default values are not set, retrieve them based on the provider type
                If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
                    ' Get the default data and log locations from the registry for SQL Server instance
                    Using regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey($"Software\Microsoft\Microsoft SQL Server\{frmlogin.instance}")
                        If regKey IsNot Nothing Then
                            DefaultDataPath = EnsureTrailingBackslash(CStr(regKey.GetValue("DefaultData")))
                            DefaultLogPath = EnsureTrailingBackslash(CStr(regKey.GetValue("DefaultLog")))
                        End If
                    End Using
                ElseIf prov2.ToLower() = "integrated" Then
                    ' Try to obtain the default data and log locations from SQL Server instance properties
                    Using con As New SqlConnection(frmmain.strlogin)

                        con.Open()
                        Using cmd As New SqlCommand("SELECT SERVERPROPERTY('InstanceDefaultDataPath') AS DefaultDataPath, SERVERPROPERTY('InstanceDefaultLogPath') AS DefaultLogPath", con)
                            Dim reader As SqlDataReader = cmd.ExecuteReader()
                            If reader.Read() Then
                                DefaultDataPath = EnsureTrailingBackslash(If(IsDBNull(reader("DefaultDataPath")), String.Empty, reader("DefaultDataPath").ToString()))
                                DefaultLogPath = EnsureTrailingBackslash(If(IsDBNull(reader("DefaultLogPath")), String.Empty, reader("DefaultLogPath").ToString()))
                                Return True
                            End If
                        End Using

                    End Using
                End If
            Else
                DefaultDataPath = EnsureTrailingBackslash(GetDatabasePath())
                DefaultLogPath = EnsureTrailingBackslash(GetDatabasePath())
            End If

        End If

        Return True

ErrorHandler:
        errmsg = Err.Description
        Return False
    End Function

    Private Function EnsureTrailingBackslash(path As String) As String
        If Not path.EndsWith(System.IO.Path.DirectorySeparatorChar) Then
            path += System.IO.Path.DirectorySeparatorChar
        End If
        Return path
    End Function


    Sub WriteXML(ByRef username As String, ByRef password As String, ByRef instance As String, ByRef provider As String, ByRef driver As String, ByRef trusted As CheckState, ByRef localdb As CheckState, ByRef autologin As CheckState, mdfpath As String, ByRef ldfpath As String)
        Try
            ' Create a new XML configuration file
            Using writer As New XmlTextWriter(configFilePath, Nothing)
                ' Start the XML document
                writer.WriteStartDocument()

                ' Root element <Configuration>
                writer.WriteStartElement("Configuration")

                ' Elements within <Configuration>
                writer.WriteElementString("Username", username)
                writer.WriteElementString("Password", password)
                ' If Len(cPwd) > 0 Then
                '     writer.WriteElementString("Password", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(cPwd)))
                ' End If
                If prov2.ToLower() = "sqloledb" Then
                    writer.WriteElementString("ConnectMode", "OLEDB")
                    writer.WriteElementString("Provider", provider)
                ElseIf prov2.ToLower() = "odbc" Then
                    writer.WriteElementString("ConnectMode", "ODBC")
                    writer.WriteElementString("Driver", driver)
                ElseIf prov2.ToLower() = "integrated" Then
                    writer.WriteElementString("ConnectMode", "Integrated")
                    writer.WriteElementString("Instance", instance)
                End If

                writer.WriteElementString("Trusted", If(trusted = -1, "1", "0"))
                writer.WriteElementString("LocalDB", If(localdb = -1, "1", "0"))
                writer.WriteElementString("AutoLogin", If(autologin = -1, "1", "0"))
                writer.WriteElementString("DefaultMDFPath", mdfpath)
                writer.WriteElementString("DefaultLDFPath", ldfpath)

                ' Close the root element <Configuration>
                writer.WriteEndElement()

                ' Finish the XML document
                writer.WriteEndDocument()
            End Using
        Catch ex As Exception
        End Try
    End Sub
    Function GetBackupPath() As String
        Dim rs1 As ADODB.Recordset
        Dim tmp As String
        con.Execute("use master")
        rs1 = con.Execute("master..xp_instance_regread N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'BackupDirectory'")
        tmp = rs1.Fields(1).Value
        rs1.Close()

        GetBackupPath = tmp & "\"

    End Function

    Function GetSqlServerBinary() As String
        Dim instancesKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Microsoft SQL Server")

        If instancesKey IsNot Nothing Then
            Dim installedInstances As String() = DirectCast(instancesKey.GetValue("InstalledInstances"), String())

            If installedInstances IsNot Nothing AndAlso installedInstances.Length > 0 Then
                Dim instanceName As String = installedInstances(0)
                Dim instanceNamesKey As RegistryKey = instancesKey.OpenSubKey("Instance Names\SQL")

                If instanceNamesKey IsNot Nothing Then
                    Dim instanceSName As String = DirectCast(instanceNamesKey.GetValue(instanceName), String)
                    Dim setupKeyPath As String = $"SOFTWARE\Microsoft\Microsoft SQL Server\{instanceSName}\Setup"

                    Dim setupKey As RegistryKey = Registry.LocalMachine.OpenSubKey(setupKeyPath)

                    If setupKey IsNot Nothing Then
                        Dim sqlBinRoot As String = DirectCast(setupKey.GetValue("SQLBinRoot"), String)
                        Dim sqlServerPath As String = $"{sqlBinRoot}\sqlservr.exe"
                        Return sqlServerPath
                    End If
                End If
            End If
        End If

        Return String.Empty
    End Function

    Function ShrinkLog(ByRef dbname As String, Optional ByRef pMode As pShrinkMode = 0, Optional ByRef ShrinkSize As Integer = 1, Optional ByRef errmsg As String = "") As Boolean
        Dim str_Renamed As String

        On Error GoTo ErrorHandler

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            con.Execute($"USE {dbname}")
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String = $"USE {dbname}"
                Using cmd As New SqlCommand(commandText, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End If

        If pMode = pShrinkMode.pReleaseUnused Then
            str_Renamed = $"DBCC SHRINKFILE (N'{dbname}_log' , 0, TRUNCATEONLY)"
        ElseIf pMode = pShrinkMode.pReorganizeFirst Then
            str_Renamed = $"DBCC SHRINKFILE (N'{dbname}_log' , {ShrinkSize})"
        ElseIf pMode = pShrinkMode.pEmptyLog Then
            str_Renamed = $"DBCC SHRINKFILE (N'{dbname}_log' , EMPTYFILE)"
        End If

        con.Execute(str_Renamed)

        ShrinkLog = True
        Exit Function

ErrorHandler:
        errmsg = Err.Description
        ShrinkLog = False

    End Function

    Function GetDBFile(ByRef dbname As String, Optional ByRef logfilename As String = "") As String
        Dim dbid As Integer
        Dim dbfile As String = ""
        Dim logfile As String = ""

        On Error Resume Next ' Handle errors

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            Using rs As SqlDataReader = con.Execute($"SELECT DB_ID('{dbname}')").ExecuteReader()
                If rs.HasRows Then
                    rs.Read()
                    dbid = rs.GetInt32(0)
                End If
            End Using
        ElseIf prov2.ToLower() = "integrated" Then
            ' Use a new connection for integrated
            Using conIntegrated As New SqlConnection(frmmain.strlogin)
                conIntegrated.Open()

                Using cmd As New SqlCommand($"SELECT DB_ID('{dbname}')", conIntegrated)
                    dbid = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        End If

        If dbid = 0 Then
            GetDBFile = ""
            Exit Function
        End If

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            Using rs As SqlDataReader = con.Execute($"SELECT database_id, type_desc, physical_name FROM sys.master_files WHERE database_id = {dbid} AND type_desc = 'ROWS'").ExecuteReader()
                If rs.HasRows Then
                    rs.Read()
                    dbfile = rs.GetString(2)
                End If
            End Using
        ElseIf prov2.ToLower() = "integrated" Then
            ' Use a new connection for integrated
            Using conIntegrated As New SqlConnection(frmmain.strlogin)
                conIntegrated.Open()

                Using cmd As New SqlCommand($"SELECT database_id, type_desc, physical_name FROM sys.master_files WHERE database_id = {dbid} AND type_desc = 'ROWS'", conIntegrated)
                    Using rs As SqlDataReader = cmd.ExecuteReader()
                        If rs.HasRows Then
                            rs.Read()
                            dbfile = rs.GetString(2)
                        End If
                    End Using
                End Using
            End Using
        End If

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            Using rs As SqlDataReader = con.Execute($"SELECT database_id, type_desc, physical_name FROM sys.master_files WHERE database_id = {dbid} AND type_desc = 'LOG'").ExecuteReader()
                If rs.HasRows Then
                    rs.Read()
                    logfile = rs.GetString(2)
                    logfilename = logfile
                End If
            End Using
        ElseIf prov2.ToLower() = "integrated" Then
            ' Use a new connection for integrated
            Using conIntegrated As New SqlConnection(frmmain.strlogin)
                conIntegrated.Open()

                Using cmd As New SqlCommand($"SELECT database_id, type_desc, physical_name FROM sys.master_files WHERE database_id = {dbid} AND type_desc = 'LOG'", conIntegrated)
                    Using rs As SqlDataReader = cmd.ExecuteReader()
                        If rs.HasRows Then
                            rs.Read()
                            logfile = rs.GetString(2)
                            logfilename = logfile
                        End If
                    End Using
                End Using
            End Using
        End If

        GetDBFile = dbfile
        Exit Function

    End Function

    Function PathDepth(ByVal path As String) As Integer
        Dim tmp As String

        If path.StartsWith("\\") Then
            tmp = path.Replace("\\", "\")
        ElseIf path.Contains(":\") Then
            tmp = path.Substring(3)
        End If

        If IO.Directory.Exists(tmp) Then
            tmp = IO.Path.Combine(tmp, "")
        End If

        Dim t() As String = tmp.Split("\"c)

        Return UBound(t)
    End Function

    Function DetachDatabase(ByRef dbname As String, Optional ByRef errmsg As String = "") As Boolean
        On Error GoTo ErrorHandler

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            con.Execute("sp_detach_db " & dbname)
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String = $"sp_detach_db '{dbname}'"

                Using cmd As New SqlCommand(commandText, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End If

        DetachDatabase = True
        Exit Function

ErrorHandler:
        errmsg = Err.Description
        DetachDatabase = False
    End Function

    Function IsPortInUse(port As Integer) As Boolean
        ' Get information from the connections table
        Dim connections() As TcpConnectionInformation = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections()

        ' Check if the port is in use
        For Each connection As TcpConnectionInformation In connections
            If connection.LocalEndPoint.Port = port Then
                Return True
            End If
        Next

        Return False
    End Function

    Sub AddFirewallException(port As Integer, ruleName As String)
        Try
            ' Create an instance of Netsh
            Dim netsh As New ProcessStartInfo("netsh", $"advfirewall firewall add rule name=""{ruleName}"" dir=in action=allow protocol=TCP localport={port}")
            netsh.WindowStyle = ProcessWindowStyle.Hidden
            netsh.RedirectStandardOutput = True
            netsh.UseShellExecute = False

            ' Execute the Netsh command to add the firewall rule
            Using process As Process = Process.Start(netsh)
                process.WaitForExit()
                Dim output As String = process.StandardOutput.ReadToEnd()
                If process.ExitCode <> 0 Then
                    frmmain.Logg("Failed to add firewall exception. Output: " & output)
                End If
            End Using
        Catch ex As Exception
            frmmain.Logg("Error adding firewall exception: " & ex.Message)
        End Try
    End Sub

    Function KillConnections(ByVal dbName As String, ByRef errmsg As String) As Boolean
        On Error GoTo ErrorHandler

        If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
            ' Not supported for sqloledb provider
            Throw New Exception("KillConnections is not supported for sqloledb provider.")
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String = $"USE master; ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; USE [{dbName}];"
                Using cmd As New SqlCommand(commandText, con)
                    cmd.ExecuteNonQuery()
                End Using

                commandText = $"USE master; ALTER DATABASE [{dbName}] SET MULTI_USER;"
                Using cmd As New SqlCommand(commandText, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End If

        KillConnections = True
        Exit Function

ErrorHandler:
        Dim errLoop As SqlError

        ' Loop through each Error object in Errors collection.
        If con.Errors.Count > 1 Then
            For Each errLoop In con.Errors
                errmsg = errLoop.Message
                frmmain.Logg(errmsg)
            Next errLoop
        Else
            errmsg = Err.Description
        End If

        KillConnections = False
    End Function

    Function GetTableNames(ByVal selectedDatabase As String) As List(Of String)
        Dim tableNames As New List(Of String)

        Try
            If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
                ' Logic to get table names using ADODB for SQL Server
                ' You can adapt the logic according to your needs
                Dim rsTables As New ADODB.Recordset
                con.Execute("USE " & selectedDatabase)

                ' Query to get table names
                rsTables.Open("SELECT table_name FROM information_schema.tables WHERE table_type = 'BASE TABLE'", con, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic)

                Do Until rsTables.EOF
                    tableNames.Add(rsTables.Fields("table_name").Value.ToString())
                    rsTables.MoveNext()
                Loop
                rsTables.Close()
            ElseIf prov2.ToLower() = "integrated" Then
                ' Logic to get table names using SqlConnection for SQL Server
                ' You can adapt the logic according to your needs
                Try
                    Using sqlCon As New SqlConnection(frmmain.strlogin)
                        sqlCon.Open()
                        Dim query As String = $"USE {selectedDatabase} Select TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'"
                        Using cmd As New SqlCommand(query, sqlCon)
                            Dim reader As SqlDataReader = cmd.ExecuteReader()
                            While reader.Read()
                                tableNames.Add(reader("TABLE_NAME").ToString())
                            End While
                        End Using
                    End Using
                Catch ex As Exception
                    Debug.Print("Error in GetTableNames (Integrated): " & ex.Message)
                    Return New List(Of String)()
                End Try
            End If

            ' Sort the list alphabetically
            tableNames.Sort()

            Return tableNames
        Catch ex As Exception
            Debug.Print("Error in GetTableNames: " & ex.Message)
            Return New List(Of String)()
        End Try
    End Function

    Function GetRows(ByVal SelectedDatabase As String, ByVal selectedTable As String, ByVal selectedRows As Integer) As DataTable
        Dim resultTable As New DataTable()

        Try
            ' Get the schema of the table
            Dim schemaName As String = GetTableSchema(SelectedDatabase, selectedTable)
            If String.IsNullOrEmpty(schemaName) Then
                Throw New Exception("Failed to get the schema of the table.")
            End If

            ' Form the SQL query with the obtained schema
            Dim query As String = String.Format("SELECT TOP {0} * FROM [{1}].[{2}].[{3}]", selectedRows, SelectedDatabase, schemaName, selectedTable)

            If prov2.ToLower() = "sqloledb" Or prov2.ToLower() = "odbc" Then
                ' Logic to get rows using ADODB for SQL Server
                Dim rsRows As New ADODB.Recordset
                con.Execute(query, , 1024) ' 1024 is the value of adExecuteNoRecords

                rsRows.Open(query, con, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic)

                For Each field As ADODB.Field In rsRows.Fields
                    resultTable.Columns.Add(field.Name, Type.GetType("System.Object"))
                Next

                Do Until rsRows.EOF
                    Dim newRow As DataRow = resultTable.NewRow()
                    For Each field As ADODB.Field In rsRows.Fields
                        newRow(field.Name) = field.Value
                    Next
                    resultTable.Rows.Add(newRow)
                    rsRows.MoveNext()
                Loop

                rsRows.Close()
            ElseIf prov2.ToLower() = "integrated" Then
                ' Logic to get rows using SqlConnection for SQL Server
                Try
                    Using sqlCon As New SqlConnection(frmmain.strlogin)
                        sqlCon.Open()
                        Using cmd As New SqlCommand(query, sqlCon)
                            Using reader As SqlDataReader = cmd.ExecuteReader()
                                resultTable.Load(reader)
                            End Using
                        End Using
                    End Using
                Catch ex As SqlException
                    Debug.Print("Error in GetRows (Integrated): " & ex.Message)
                    Return New DataTable()
                End Try
            End If

            Return resultTable
        Catch ex As Exception
            Debug.Print("Error in GetRows: " & ex.Message)
            Return New DataTable()
        End Try
    End Function

    Function GetTableSchema(ByVal databaseName As String, ByVal tableName As String) As String
        Dim schemaName As String = String.Empty

        Try
            Using sqlCon As New SqlConnection(frmmain.strlogin)
                sqlCon.Open()
                ' Make sure the database name is included in the connection string or use it in the SQL query.
                Dim query As String = String.Format("SELECT TABLE_SCHEMA FROM {0}.INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName", databaseName)
                Using cmd As New SqlCommand(query, sqlCon)
                    cmd.Parameters.AddWithValue("@TableName", tableName)
                    Dim result As Object = cmd.ExecuteScalar()
                    If result IsNot Nothing Then
                        schemaName = result.ToString()
                    End If
                End Using
            End Using
        Catch ex As SqlException
            Debug.Print("Error getting table schema: " & ex.Message)
        End Try

        Return schemaName
    End Function

End Module