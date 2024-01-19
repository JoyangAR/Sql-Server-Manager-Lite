Option Strict Off
Option Explicit On
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Net.NetworkInformation
Imports System.Security.Principal
Imports Microsoft.Win32

Module Module1
    Public con As New ADODB.Connection
    Public connection As New SqlConnection
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

        System.Windows.Forms.Application.DoEvents()
        If prov2 = "sqloledb" Then
            Try
                con.Execute("CREATE DATABASE [" & DatabaseName & "] ON (FILENAME = '" & path & "') FOR ATTACH_REBUILD_LOG;")
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Else
            Try
                Using connection As New SqlConnection(frmmain.strlogin)
                    connection.Open()

                    Using cmd As New SqlCommand("CREATE DATABASE [" & DatabaseName & "] ON (FILENAME = '" & path & "') FOR ATTACH_REBUILD_LOG;", connection)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End If
        AttachData = True
        Exit Function

        errmsg = Err.Description
        AttachData = False
    End Function

    Function TabulateDatabase(ByRef bakPath As String, Optional ByRef dbname As String = "", Optional ByRef LogName As String = "", Optional ByRef errmsg As String = "", Optional ByRef mdfid As Integer = 0) As Boolean
        On Error GoTo xc
        Dim resultList As New List(Of String)()
        Dim rs As SqlDataReader

        If prov2.ToLower() = "sqloledb" Then
            rs = con.Execute($"RESTORE FILELISTONLY FROM DISK = '{bakPath}'").ExecuteReader()
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String = $"RESTORE FILELISTONLY FROM DISK = '{bakPath}'"
                Using cmd As New SqlCommand(commandText, con)
                    rs = cmd.ExecuteReader()

                    While rs.Read()
                        resultList.Add(rs.GetString(0))
                    End While
                End Using
            End Using
        Else
            Throw New Exception("prov2 Value is not valid!")
        End If

        If resultList.Count >= 2 Then
            dbname = resultList(0)
            LogName = resultList(1)
            TabulateDatabase = True
        Else
            TabulateDatabase = False
        End If

        Exit Function

xc:
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

    Function CreateAccount(ByRef username As String, ByRef Password As String, Optional ByRef errmsg As String = "") As Boolean

        System.Windows.Forms.Application.DoEvents()
        If prov2 = "sqloledb" Then
            Try
                con.Execute("USE [master]" & vbCrLf & "Create LOGIN [" & username & "] WITH PASSWORD = N'" & Password & "',DEFAULT_Database=[master],CHECK_EXPIRATION=OFF,CHECK_POLICY = OFF" & vbCrLf & "EXEC master..sp_addsrvrolemember @loginame = N'" & username & "', @rolename = N'sysadmin'")
                CreateAccount = True
            Catch ex As Exception
            End Try
        Else
            Try
                Using connection As New SqlConnection(frmmain.strlogin)
                    connection.Open()

                    Using cmd As New SqlCommand("USE [master]" & vbCrLf & "Create LOGIN [" & username & "] WITH PASSWORD = N'" & Password & "',DEFAULT_Database=[master],CHECK_EXPIRATION=OFF,CHECK_POLICY = OFF" & vbCrLf & "EXEC master..sp_addsrvrolemember @loginame = N'" & username & "', @rolename = N'sysadmin'", connection)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End If

        Exit Function
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


    Public Function RestoreDatabase2(ByRef bckpath As String, ByRef NewDBpath As String, ByRef dbfile As String, ByRef logfile As String, Optional ByRef errmsg As String = "", Optional ByRef MDF_Only As Boolean = False, Optional ByRef MDF_ID As Integer = 0) As Boolean
        On Error GoTo xc

        Dim db1 As String
        Dim str_Renamed As String

        db1 = CorrectDBname(dbfile)

        If prov2.ToLower() = "sqloledb" Then
            con.Execute("USE master")
            System.Windows.Forms.Application.DoEvents()

            If Not MDF_Only Then
                str_Renamed = $"RESTORE DATABASE {db1} FROM DISK='{bckpath}' WITH MOVE '{dbfile}' TO '{NewDBpath}{db1}.mdf', MOVE '{logfile}' TO '{NewDBpath}{db1}_log.ldf'"
            Else
                str_Renamed = $"RESTORE DATABASE {db1} FROM DISK='{bckpath}' WITH FILE=1, RECOVERY, MOVE '{dbfile}' TO '{NewDBpath}{db1}.mdf', MOVE '{logfile}' TO '{NewDBpath}{db1}_log.ldf'"
            End If

            con.Execute(str_Renamed)
        ElseIf prov2.ToLower() = "integrated" Then
            Using connnection As New SqlConnection(frmmain.strlogin)
                connnection.Open()

                Dim commandText As String

                If Not MDF_Only Then
                    commandText = $"RESTORE DATABASE [{db1}] FROM DISK='{bckpath}' WITH MOVE '{dbfile}' TO '{NewDBpath}{db1}.mdf', MOVE '{logfile}' TO '{NewDBpath}{db1}_log.ldf'"
                Else
                    commandText = $"RESTORE DATABASE [{db1}] FROM DISK='{bckpath}' WITH FILE=1, RECOVERY, MOVE '{dbfile}' TO '{NewDBpath}{db1}.mdf', MOVE '{logfile}' TO '{NewDBpath}{db1}_log.ldf'"
                End If

                Using cmd As New SqlCommand(commandText, connnection)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Else
            Throw New Exception("prov2 Value is not valid!")
        End If

        RestoreDatabase2 = True
        Exit Function

xc:
        Debug.Print(str_Renamed)
        errmsg = Err.Description
        RestoreDatabase2 = False
    End Function

    Function RestoreDatabase(ByRef bckfile As String, ByRef dbname As String, Optional ByRef errmsg As String = "") As Boolean
        On Error GoTo xc

        If prov2.ToLower() = "sqloledb" Then
            con.Execute($"RESTORE DATABASE {dbname} FROM DISK = '{bckfile}' WITH REPLACE")
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String = $"RESTORE DATABASE [{dbname}] FROM DISK = '{bckfile}' WITH REPLACE"

                Using cmd As New SqlCommand(commandText, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Else
            Throw New Exception("prov2 Value is not valid!")
        End If

        RestoreDatabase = True
        Exit Function

xc:
        Debug.Print($"RESTORE DATABASE {dbname} FROM DISK = '{bckfile}' WITH REPLACE")

        errmsg = Err.Description
        RestoreDatabase = False
    End Function

    Function ListUsers() As Collection
        Dim col As New Collection
        Dim tmp As String

        If prov2.ToLower() = "sqloledb" Then
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
        Else
            Throw New Exception("prov2 Value is not valid!")
        End If

        Return col

    End Function


    Function ListDatabases() As Collection
        Dim col As New Collection
        Dim tmp As String

        If prov2.ToLower() = "sqloledb" Then
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
        Else
            Throw New Exception("prov2 Value is not valid!")
        End If

        Return col
    End Function



    Function ChangePwd(ByRef username As String, ByRef pwd As String, Optional ByRef errmsg As String = "") As Boolean
        On Error GoTo xc

        If prov2.ToLower() = "sqloledb" Then
            con.Execute($"ALTER LOGIN [{username}] WITH PASSWORD=N'{pwd}'")
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String = $"ALTER LOGIN [{username}] WITH PASSWORD=N'{pwd}'"

                Using cmd As New SqlCommand(commandText, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Else
            Throw New Exception("prov2 Value is not valid!")
        End If

        ChangePwd = True
        Exit Function

xc:
        errmsg = Err.Description
        ChangePwd = False
    End Function


    Function DeleteAccount(ByRef username As String, Optional ByRef errmsg As String = "") As Boolean
        On Error GoTo xc

        If prov2.ToLower() = "sqloledb" Then
            con.Execute($"DROP LOGIN [{username}]")
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String = $"DROP LOGIN [{username}]"

                Using cmd As New SqlCommand(commandText, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Else
            Throw New Exception("prov2 Value is not valid!")
        End If

        DeleteAccount = True
        Exit Function

xc:
        errmsg = Err.Description
        DeleteAccount = False
    End Function


    Function AccountExists(ByVal username As String) As Boolean
        Dim tmp As Boolean = False

        Try
            If prov2.ToLower() = "sqloledb" Then
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
            Else
                Throw New Exception("prov2 Value is not valid!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "")
        End Try

        Return tmp
    End Function



    Function RepairDB(ByRef dbname As String, Optional ByRef forced As pRepairMode = pRepairMode.pStandard, Optional ByRef errmsg As String = "", Optional ByRef rs As ADODB.Recordset = Nothing) As Boolean
        Dim str_Renamed As String

        On Error GoTo xc

        If prov2.ToLower() = "sqloledb" Then
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
        Else
            Throw New Exception("prov2 Value is not valid!")
        End If

        Debug.Print(str_Renamed)

        RepairDB = True
        Exit Function

xc:
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
        On Error GoTo xc

        If prov2.ToLower() = "sqloledb" Then
            con.Execute("DROP DATABASE " & dbname)
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String = $"DROP DATABASE [{dbname}]"

                Using cmd As New SqlCommand(commandText, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Else
            Throw New Exception("prov2 Value is not valid!")
        End If

        DeleteDatabase = True
        Exit Function

xc:
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

        On Error GoTo xc

        newbck = $"{dbname1}_{Today:MM-dd-yyyy}_{Now:hhmmss}.bak"
        bckfile = newbck
        System.Windows.Forms.Application.DoEvents()

        If prov2.ToLower() = "sqloledb" Then
            con.Execute($"BACKUP DATABASE [{dbname1}] TO DISK = N'{ipath}{newbck}' WITH NOFORMAT, INIT, NAME = N'{dbname1}-Full Database Backup', SKIP, NOREWIND, NOUNLOAD")
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String = $"BACKUP DATABASE [{dbname1}] TO DISK = N'{ipath}{newbck}' WITH NOFORMAT, INIT, NAME = N'{dbname1}-Full Database Backup', SKIP, NOREWIND, NOUNLOAD"

                Using cmd As New SqlCommand(commandText, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Else
            Throw New Exception("prov2 Value is not valid!")
        End If

        BackupDatabase = True
        Exit Function

xc:
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

        On Error GoTo xc

        Dim tmp As Boolean

        If prov2.ToLower() = "sqloledb" Then
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
        Else
            Throw New Exception("prov2 Value is not valid!")
        End If

        GuestAllowed = tmp

        If prov2.ToLower() = "sqloledb" Then
            con.Execute("use master")
        End If

        Exit Function

xc:
        GuestAllowed = False
    End Function


    Sub SetGuest(ByRef dbname As String, ByRef Grant As Boolean)

        Dim str_Renamed As String

        If prov2.ToLower() = "sqloledb" Then
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
        Else
            Throw New Exception("prov2 Value is not valid!")
        End If

    End Sub

    Function GetDatabasePath() As String

        Dim tmp As String

        If prov2.ToLower() = "sqloledb" Then
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
        Else
            Throw New Exception("prov2 Value is not valid!")
        End If

        Return tmp

    End Function

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

        On Error GoTo xc

        If prov2.ToLower() = "sqloledb" Then
            con.Execute($"USE {dbname}")
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String = $"USE {dbname}"
                Using cmd As New SqlCommand(commandText, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Else
            Throw New Exception("prov2 Value is not valid!")
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

xc:
        errmsg = Err.Description
        ShrinkLog = False

    End Function

    Function GetDBFile(ByRef dbname As String, Optional ByRef logfilename As String = "") As String
        Dim dbid As Integer
        Dim dbfile As String = ""
        Dim logfile As String = ""

        On Error Resume Next ' Handle errors

        If prov2.ToLower() = "sqloledb" Then
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
        Else
            Throw New Exception("prov2 Value is not valid!")
        End If

        If dbid = 0 Then
            GetDBFile = ""
            Exit Function
        End If

        If prov2.ToLower() = "sqloledb" Then
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

        If prov2.ToLower() = "sqloledb" Then
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
        On Error GoTo xc

        If prov2.ToLower() = "sqloledb" Then
            con.Execute("sp_detach_db " & dbname)
        ElseIf prov2.ToLower() = "integrated" Then
            Using con As New SqlConnection(frmmain.strlogin)
                con.Open()

                Dim commandText As String = $"sp_detach_db '{dbname}'"

                Using cmd As New SqlCommand(commandText, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Else
            Throw New Exception("prov2 Value is not valid!")
        End If

        DetachDatabase = True
        Exit Function

xc:
        errmsg = Err.Description
        DetachDatabase = False
    End Function

    Function IsPortInUse(port As Integer) As Boolean
        ' Get information from the connections table
        Dim connections = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections()

        ' Check if the port is in use
        Return connections.Any(Function(connection) connection.LocalEndPoint.Port = port)
    End Function

    Sub AddFirewallException(port As Integer, ruleName As String)
        Try
            ' Create an instance of the Netsh object
            Dim netsh As New ProcessStartInfo("netsh", $"advfirewall firewall add rule name=""{ruleName}"" dir=in action=allow protocol=TCP localport={port}")
            netsh.WindowStyle = ProcessWindowStyle.Hidden

            ' Execute the Netsh command to add the firewall rule
            Using process As Process = Process.Start(netsh)
                process.WaitForExit()
            End Using
        Catch ex As Exception
        End Try
    End Sub
End Module