Option Strict Off
Option Explicit On
Imports System.IO
Imports System.Linq
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.ServiceProcess
Imports Microsoft.Win32

Friend Class frmmain
    Inherits System.Windows.Forms.Form
    Public strlogin As String
    Dim windrive As String
    Const vGray As Integer = &H8000000F
    Dim dbpath As String
    Dim bckpath As String


    Private Sub adduser_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles adduser.Click
        VB6.ShowForm(frmadduser, VB6.FormShowConstants.Modal, Me)
    End Sub

    Sub FirewallExcepted(ByRef mode As Boolean)
        Wait(True)
        Logg("Applying firewall exception...")
        Try
            Dim PortInUse As Boolean = IsPortInUse(1433)

            If Not PortInUse Then
                ' Get information from the connections table
                Dim connections = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections()

                ' Check if there is a local connection on port 1433
                Dim isLocalPortInUse = connections.Any(Function(connection) connection.LocalEndPoint.Port = 1433)

                If Not isLocalPortInUse Then
                    ' Add firewall exception for port 1433
                    AddFirewallException(1433, "SQLServerException")
                    Logg("Firewall exception applied for port 1433.")
                Else
                    Logg("Firewall exception not applied. Port 1433 is already in use.")
                End If
            Else
                Logg("Firewall exception not applied. Port 1433 is already in use.")
            End If
        Catch ex As Exception
            Logg("Failed to apply firewall exception: " & ex.Message)
        End Try
    End Sub





    Private Sub chkfirewall_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkfirewall.CheckStateChanged

        If Me.chkfirewall.Checked = False Then
            If MsgBox("Disabling firewall exception for SQL Server means that remote database connection is not allowed. Continue?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.No Then
                Exit Sub
            End If
        End If

        FirewallExcepted(chkfirewall.Checked)

    End Sub




    Private Sub chksqlbrowser_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chksqlbrowser.CheckStateChanged
        ManageSQLBrowser(chksqlbrowser.Checked)
    End Sub

    Sub ManageSQLBrowser(ByRef lmode As Boolean)
        Dim serviceName As String = "SQLBrowser" ' SQL Browser's service name

        If lmode Then
            If IsServiceRunning(serviceName) Then
                Logg("SQL Browser already running")
            Else
                Logg("Starting SQL Browser...")

                If GetServiceStartMode(serviceName) <> ServiceStartMode.Automatic Then
                    SetServiceStartMode(serviceName, ServiceStartMode.Automatic)
                End If

                StartService(serviceName)
                Logg("SQL Browser has been started")
            End If
        Else
            Logg("Stopping SQL Browser...")
            SetServiceStartMode(serviceName, ServiceStartMode.Manual)
            StopService(serviceName)
            Logg("SQL Browser has been stopped")
        End If
    End Sub

    Function IsServiceRunning(ByVal serviceName As String) As Boolean
        Dim controller As New ServiceController(serviceName)
        Return controller.Status = ServiceControllerStatus.Running
    End Function

    Function GetServiceStartMode(ByVal serviceName As String) As ServiceStartMode
        Dim service As New ServiceController(serviceName)
        Dim regKey As Microsoft.Win32.RegistryKey = Nothing

        Try
            regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey($"SYSTEM\CurrentControlSet\Services\{serviceName}")
            Dim startMode As Object = regKey.GetValue("Start")

            If startMode IsNot Nothing Then
                Return DirectCast(startMode, ServiceStartMode)
            End If
        Finally
            regKey?.Close()
        End Try

        Return ServiceStartMode.Manual
    End Function

    Sub SetServiceStartMode(ByVal serviceName As String, ByVal startMode As ServiceStartMode)
        Try
            Dim regKey As RegistryKey = Registry.LocalMachine.OpenSubKey($"SYSTEM\CurrentControlSet\Services\{serviceName}", True)

            If regKey IsNot Nothing Then
                ' Change the start mode
                Select Case startMode
                    Case ServiceStartMode.Automatic
                        regKey.SetValue("Start", 2) ' Value 2 represents automatic start
                    Case ServiceStartMode.Manual
                        regKey.SetValue("Start", 3) ' Value 3 represents manual start
                    Case ServiceStartMode.Disabled
                        regKey.SetValue("Start", 4) ' Value 4 represents that the service is disabled
                End Select

                ' Close the Registry key
                regKey.Close()

                ' Report the successful change
                Logg($"Start mode of the service {serviceName} changed to {startMode}.")
            Else
                ' Registry entry not found
                Logg("Error: Registry entry for the service is not found.")
            End If
        Catch ex As Exception
            ' Another error while changing the service start mode
            Logg("Error changing the service start mode: " & ex.Message)
        End Try
    End Sub


    Sub StartService(ByVal serviceName As String)
        Try
            Using controller As New ServiceController(serviceName)
                If controller.Status = ServiceControllerStatus.Stopped Then
                    ' Start the service
                    controller.Start()
                    ' Wait until the service is in the 'Running' state
                    controller.WaitForStatus(ServiceControllerStatus.Running)
                    Logg($"The service {serviceName} has been started successfully.")
                Else
                    Logg($"The service {serviceName} is already running.")
                End If
            End Using
        Catch ex As InvalidOperationException
            ' The exception occurs if the service does not exist or if the service cannot be controlled (e.g., may require elevated privileges).
            Logg($"Error attempting to start the service {serviceName}: {ex.Message}")
        Catch ex As TimeoutException
            ' The exception occurs if the timeout is exceeded while waiting for the service to reach the 'Running' state.
            Logg($"Timeout error while starting the service {serviceName}: {ex.Message}")
        Catch ex As Exception
            ' Catch other unexpected exceptions
            Logg($"Unexpected error while starting the service {serviceName}: {ex.Message}")
        End Try
    End Sub


    Sub StopService(ByVal serviceName As String)
        Dim controller As New ServiceController(serviceName)

        If controller.Status = ServiceControllerStatus.Running Then
            controller.Stop()
            controller.WaitForStatus(ServiceControllerStatus.Stopped)
        End If
    End Sub


    Private Sub cmdadd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdadd.Click
        Dim str_Renamed As String

        Try
            dlgOpen.Title = "Select backup file"
            dlgOpen.Filter = "SQL Server Database Back-up File (*.bak)|*.bak|SQL Server Database File (*.mdf)|*.mdf"
            dlgOpen.ShowDialog()

            str_Renamed = dlgOpen.FileName

            If str_Renamed = "" Then
                Exit Sub
            End If

            Wait(True)

            If LCase(IO.Path.GetExtension(str_Renamed)) = ".bak" Then
                ProcessUpload(str_Renamed)
            ElseIf LCase(IO.Path.GetExtension(str_Renamed)) = ".mdf" Then
                ProcessUpload2(str_Renamed)
            End If

            Wait(False)
        Catch ex As Exception
            Logg("Upload database cancelled")
            Logg("Reason: " & ex.Message)
        End Try
    End Sub


    Private Sub cmdapply_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdapply.Click
        FirewallExcepted((Me.chkfirewall.Checked))
        ManageSQLBrowser((Me.chksqlbrowser.Checked))
    End Sub

    Private Sub cmdbackup_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdbackup.Click

        If lstdb.SelectedIndex = -1 Then
            MsgBox("Select database", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        Wait(True)
        ProcessBackup(VB6.GetItemString(lstdb, lstdb.SelectedIndex))
        Wait(False)

    End Sub

    Private Sub cmdchange_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdchange.Click
        If lstuser.SelectedIndex = -1 Then
            MsgBox("Select user", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        VB6.ShowForm(frmpwd, VB6.FormShowConstants.Modal, Me)

    End Sub

    Private Sub cmdclear_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdclear.Click
        Me.txtlog.Text = ""
    End Sub

    Private Sub cmddelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmddelete.Click

        Dim err1 As String

        If lstdb.SelectedIndex = -1 Then
            MsgBox("Select database", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        Dim x1 As String
        Dim d1 As String
        If MsgBox("Are you sure do you want to delete the database?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then



            Randomize()

            d1 = Format(Int(Rnd() * 999999), "000000")

            x1 = InputBox("Type this code to continue: " & d1, "Delete database")

            If x1 = "" Or x1 <> d1 Then
                Exit Sub
            End If

            System.Windows.Forms.Application.DoEvents()

            If DeleteDatabase(VB6.GetItemString(Me.lstdb, lstdb.SelectedIndex), err1) = True Then
                Logg("Database " & VB6.GetItemString(Me.lstdb, lstdb.SelectedIndex) & " deleted!")
                Me.LoadDatabase()
            Else
                Logg("Cannot delete database: " & err1)
            End If

        End If

    End Sub

    Private Sub cmddeleteuser_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmddeleteuser.Click
        Dim err1 As String

        If lstuser.SelectedIndex = -1 Then
            MsgBox("Select user", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        Dim x1 As String
        Dim d1 As String
        If MsgBox("Are you sure do you want to delete the user?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then


            Randomize()

            d1 = VB6.Format(Int(Rnd() * 999999), "000000")

            x1 = InputBox("Type this code to continue: " & d1, "Delete user")

            If x1 = "" Or x1 <> d1 Then
                Exit Sub
            End If

            Wait(True)

            System.Windows.Forms.Application.DoEvents()

            If DeleteAccount(VB6.GetItemString(Me.lstuser, lstuser.SelectedIndex), err1) = True Then
                Logg("User " & VB6.GetItemString(Me.lstuser, lstuser.SelectedIndex) & " deleted!")
                Me.LoadUser()
            Else
                Logg("Cannot delete user: " & err1)
            End If

            Wait(False)

        End If

    End Sub

    Private Sub cmdgetsize_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdgetsize.Click
        Dim ldf As String
        Dim mdf As String
        Dim str_Renamed As String
        Dim mdfsize As Long
        Dim ldfsize As Long
        Dim strmdf As String
        Dim strldf As String

        If lstdb.SelectedIndex = -1 Then
            MsgBox("Select database", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        mdf = GetDBFile(lstdb.Items(lstdb.SelectedIndex).ToString(), ldf)
        mdfsize = New System.IO.FileInfo(mdf).Length
        ldfsize = New System.IO.FileInfo(ldf).Length

        Debug.Print(mdfsize & "|" & ldfsize)

        If mdfsize >= (1024 ^ 4) Then
            strmdf = System.Math.Round(mdfsize / (1024 ^ 4), 3) & " Tb"
        ElseIf mdfsize >= (1024 ^ 3) Then
            strmdf = System.Math.Round(mdfsize / (1024 ^ 3), 3) & " Gb"
        ElseIf mdfsize >= (1024 ^ 2) Then
            strmdf = System.Math.Round(mdfsize / (1024 ^ 2), 3) & " Mb"
        ElseIf mdfsize >= 1024 Then
            strmdf = System.Math.Round(mdfsize / 1024, 3) & " kb"
        Else
            strmdf = System.Math.Round(mdfsize, 3) & " B"
        End If

        If ldfsize >= (1024 ^ 4) Then
            strldf = System.Math.Round(ldfsize / (1024 ^ 4), 3) & " Tb"
        ElseIf ldfsize >= (1024 ^ 3) Then
            strldf = System.Math.Round(ldfsize / (1024 ^ 3), 3) & " Gb"
        ElseIf ldfsize >= (1024 ^ 2) Then
            strldf = System.Math.Round(ldfsize / (1024 ^ 2), 3) & " Mb"
        ElseIf ldfsize >= 1024 Then
            strldf = System.Math.Round(ldfsize / 1024, 3) & " kb"
        Else
            strldf = System.Math.Round(ldfsize, 3) & " B"
        End If

        str_Renamed = "DB Name: " & lstdb.Items(lstdb.SelectedIndex).ToString() & vbCrLf & "DB Filesize: " & strmdf & vbCrLf & "Log Filesize: " & strldf

        Logg(str_Renamed)
    End Sub


    Private Sub cmdguest_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdguest.Click

        If lstdb.SelectedIndex = -1 Then
            MsgBox("Select database", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If


        If UCase(cmdguest.Text) = "GUEST ON" Then
            SetGuest(lstdb.Items(lstdb.SelectedIndex).ToString(), False)
            Logg("Guest account for " & lstdb.Items(lstdb.SelectedIndex).ToString() & " disabled")
        ElseIf UCase(cmdguest.Text) = "GUEST OFF" Then
            SetGuest(lstdb.Items(lstdb.SelectedIndex).ToString(), True)
            Logg("Guest account for " & lstdb.Items(lstdb.SelectedIndex).ToString() & " enabled")
        End If

        LookGuest(lstdb.Items(lstdb.SelectedIndex).ToString())


    End Sub

    Private Sub cmdpurge_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdpurge.Click
        If lstdb.SelectedIndex = -1 Then
            MsgBox("Select database", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        If MsgBox("Are you sure do you want to clear the log file for this database?" & vbCrLf & "The database might be less reliable", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
            RebuildLog(VB6.GetItemString(Me.lstdb, Me.lstdb.SelectedIndex))
        End If

        LoadDatabase()

    End Sub

    Private Sub cmdrepairdb_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdrepairdb.Click
        If lstdb.SelectedIndex = -1 Then
            MsgBox("Select database", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        If MsgBox("Are you sure do you want to repair the database?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
            frmdialog.ShowDialog(Me)
        End If


    End Sub

    Private Sub cmdrestore_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdrestore.Click

        If lstdb.SelectedIndex = -1 Then
            MsgBox("Select database", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        Dim str_Renamed As String

        On Error GoTo xc

        dlgOpen.Title = "Select backup file"
        dlgOpen.Filter = "SQL Server Database Back-up File (*.bak)|*.bak"
        dlgOpen.ShowDialog()
        str_Renamed = dlgOpen.FileName

        If str_Renamed = "" Then
            Exit Sub
        End If

        Wait(True)
        ProcessRestore(str_Renamed, lstdb.SelectedItem.ToString())
        Wait(False)

        Exit Sub

xc:
        Debug.Print(Err.Description)
        Logg("Upload database restoration cancelled")
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub




    Private Sub frmmain_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        LoadUser()
        LoadDatabase()
        windrive = System.IO.Path.GetPathRoot(System.Environment.SystemDirectory)
        dbpath = GetDatabasePath()

        ' Check if port 1433 is open
        'chkfirewall.CheckState = IsPortOpen(1433)

        ' Check if the SQLBrowser service is installed and running
        ' Dim serviceName As String = "SQLBrowser"
        ' chksqlbrowser.Enabled = ServiceController.GetServices().Any(Function(s) s.ServiceName = serviceName)

        ' If chksqlbrowser.Enabled Then
        ' Dim serviceController As New ServiceController(serviceName)
        ' chksqlbrowser.CheckState = If(serviceController.Status = ServiceControllerStatus.Running, CheckState.Checked, CheckState.Unchecked)
        ' End If

    End Sub


    Private Function IsPortOpen(port As Integer) As Boolean
        Try
            Using tcpClient As New TcpClient()
                tcpClient.Connect("127.0.0.1", port)
                Return True
            End Using
        Catch
            Return False
        End Try
    End Function

    Public Sub Loadstr(ByVal strlogin1 As String)
        strlogin = strlogin1
    End Sub

    Sub LoadUser()

        lstuser.Items.Clear()

        Dim col1 As Collection
        Dim d As Integer

        col1 = ListUsers()

        For d = 1 To col1.Count()
            lstuser.Items.Add(col1.Item(d))
        Next d

        If lstuser.Items.Count > 0 Then
            KeyUser(True)
        Else
            KeyUser(False)
        End If

    End Sub

    Sub LoadDatabase()

        lstdb.Items.Clear()

        Dim col1 As Collection
        Dim d As Integer

        cmdguest.Text = "Guest"
        cmdguest.BackColor = System.Drawing.ColorTranslator.FromOle(vGray)

        col1 = ListDatabases()

        For d = 1 To col1.Count()
            lstdb.Items.Add(col1.Item(d))
        Next d

        If lstdb.Items.Count > 0 Then
            KeyDb(True)
        Else
            KeyDb(False)
        End If

    End Sub

    Sub ProcessUpload(ByRef bakfile As String)
        Dim newpath As String
        Dim err1 As String
        Dim msg1 As String
        Dim dbfile As String
        Dim dbname As String
        Dim logfile As String
        Dim mdfo As Boolean
        Dim mvf As Boolean
        Dim mdfid As Integer

        mvf = False

        If Not Directory.Exists(windrive) Then
            MsgBox("Admin access required", MsgBoxStyle.Exclamation, "Error!")
            Exit Sub
        End If

        Logg("Copying files...")
        System.Windows.Forms.Application.DoEvents()

        If PathDepth(bakfile) > 1 Then
            newpath = Path.Combine(windrive, Path.GetFileName(bakfile))
            System.IO.File.Copy(bakfile, newpath)
            mvf = True
        Else
            newpath = bakfile
            mvf = False
        End If

        Logg("Tabulating database...")
        Dim tmp1 As String
        If Not TabulateDatabase(newpath, dbfile, logfile, err1) Then
            Logg($"Error Tabulating:{err1}")
        Else
            Debug.Print("dbfile: " & dbfile)

            dbname = CorrectDBname(dbfile)

            Debug.Print("db Name:" & dbname)

            If String.IsNullOrEmpty(dbpath) Then
                tmp1 = Path.Combine(windrive, "SysData\")
                If Not Directory.Exists(tmp1) Then
                    Logg("Creating database directory...")
                    Directory.CreateDirectory(tmp1)
                End If
            Else
                tmp1 = dbpath
            End If

            If DatabaseExists(dbname) Then
                Logg("Cannot upload database: Database already exists")
            Else
                If MsgBox("You will now upload this database. Proceed?" & vbCrLf & vbCrLf & "Database name: " & dbname, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Notice") = MsgBoxResult.Yes Then
                    Logg("Uploading database...")

                    If MsgBox("Include backup log file for uploading? If you select NO then a new log file will be created instead of using the backup log file.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Notice") = MsgBoxResult.Yes Then
                        mdfo = False
                    Else
                        mdfo = True
                    End If

                    If RestoreDatabase2(newpath, tmp1, dbfile, logfile, err1) Then
                        SetGuest(dbname, True)
                        'con.Execute($"USE {dbname} GRANT CONNECT TO GUEST")
                        'con.Execute("USE master")

                        If mdfo Then
                            RebuildLog(dbname)
                        End If

                        Logg($"Database {dbname} is now uploaded")
                    Else
                        Logg($"Database upload of {dbname} failed: {err1}")
                    End If
                Else
                    Logg("Upload database cancelled")
                End If
            End If

            If mvf Then
                File.Delete(newpath)
            End If

            LoadDatabase()
        End If
    End Sub

    Sub ProcessUpload2(ByRef mdffile As String)
        Dim newpath As String
        Dim err1 As String
        Dim msg1 As String
        Dim dbfile As String
        Dim dbname As String
        Dim flname As String
        Dim logfile As String
        Dim dbfname As String

        newpath = Path.Combine(dbpath, Path.GetFileName(mdffile))

        If Not Directory.Exists(windrive) Then
            MsgBox("Admin access required", MsgBoxStyle.Exclamation, "Error!")
            Exit Sub
        End If

        Logg("Copying files...")
        System.Windows.Forms.Application.DoEvents()
        File.Copy(mdffile, newpath)
        Dim tmp1 As String
        If MsgBox("Uploading mdf file means that the log file is missing. Are you sure?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.No Then
            Logg("Upload database cancelled")
            Exit Sub
        Else
            dbfname = Path.GetFileNameWithoutExtension(mdffile)
            dbname = CorrectDBname(dbfname)
            flname = Path.GetFileName(mdffile)

            If DatabaseExists(dbname) Then
                Logg("Cannot upload database: Database already exists")
            Else
                If MsgBox("You will now upload this database. Proceed?" & vbCrLf & vbCrLf & "Database name: " & dbname, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Notice") = MsgBoxResult.Yes Then
                    Logg("Uploading database...")
                    If AttachData(dbname, newpath, err1) Then
                        con.Execute($"USE {dbname} GRANT CONNECT TO GUEST")
                        con.Execute("USE master")
                        Logg($"Database {dbname} is now uploaded")
                    Else
                        Logg($"Database upload of {dbname} failed: {err1}")
                    End If
                Else
                    Logg("Upload database cancelled")
                End If
            End If

            LoadDatabase()
        End If
    End Sub


    Sub Logg(ByRef str_Renamed As String)

        System.Windows.Forms.Application.DoEvents()

        If Len(txtlog.Text) > 200000000 Then
            txtlog.Text = ""
        End If

        If txtlog.Text = "" Then
            Me.txtlog.Text = str_Renamed & vbCrLf & vbCrLf
        Else
            Me.txtlog.Text = Me.txtlog.Text & str_Renamed & vbCrLf & vbCrLf
        End If

        txtlog.SelectionStart = txtlog.TextLength
        txtlog.ScrollToCaret()

    End Sub


    Sub ProcessBackup(ByRef dbname As String)
        Logg("Full database backup started...")

        System.Windows.Forms.Application.DoEvents()

        Dim tmp12 As String
        Dim newbck As String
        Dim err1 As String

        tmp12 = windrive & "SysDataBackup"

        If DirExists(tmp12) = False Then
            Logg("Creating database backup directory...")
            MkDir(tmp12)
        End If


        If BackupDatabase(dbname, tmp12 & "\", newbck, err1) = True Then
            Logg("Database backup file for " & dbname & " created: " & newbck)
            Logg("Database backup file was in " & tmp12 & "\")
            Logg("Backup database for " & dbname & " successful")

            If MsgBox("Backup successful. Open backup location?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
                Shell("explorer.exe " & tmp12, AppWinStyle.NormalFocus)
            End If

        Else
            Logg("Backup database failed: " & err1)
        End If

    End Sub

    Sub ProcessRestore(ByRef bckfile As String, ByRef dbname As String)
        Dim err1 As String
        Dim mvf As Boolean
        Dim bfile As String

        mvf = False

        If MsgBox("Are you sure do you want to rollback the database?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes Then
            Logg("Restore database started...")

            System.Windows.Forms.Application.DoEvents()

            If PathDepth(bckfile) > 1 Then
                bfile = Path.Combine(windrive, Path.GetFileName(bckfile))
                File.Copy(bckfile, bfile)
                mvf = True
            Else
                bfile = bckfile
                mvf = False
            End If

            If RestoreDatabase(bfile, dbname, err1) Then
                Logg($"Restore database for {dbname} successful!")
            Else
                Logg($"Restore database failed: {err1}")
            End If

            If mvf Then
                File.Delete(bfile)
            End If
        End If
    End Sub

    Sub ProcessRepair(ByRef dbname As String, ByVal forced As Module1.pRepairMode)
        Dim err1 As String
        Dim mode1 As String
        Dim rmode As Module1.pRepairMode

        Wait(True)

        If forced = Module1.pRepairMode.pFast Then
            mode1 = "Fast mode"
        ElseIf forced = Module1.pRepairMode.pForced Then
            mode1 = "Forced mode"
        ElseIf forced = Module1.pRepairMode.pStandard Then
            mode1 = "Standard mode"
        End If

        Logg("Repair database " & dbname & " started (" & mode1 & ")...")

        System.Windows.Forms.Application.DoEvents()

        If RepairDB(dbname, forced, err1) = True Then
            Logg("Repair database " & dbname & " successful!")
        ElseIf Len(err1) > 0 Then
            Logg("Repair database failed: " & err1)
        End If

        Wait(False)

    End Sub

    Sub Wait(ByRef d As Boolean)

        If d = True Then
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Else
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End If

    End Sub


    Sub KeyDb(ByRef d As Boolean)
        Me.cmdbackup.Enabled = d
        Me.cmddelete.Enabled = d
        Me.cmdrepairdb.Enabled = d
        Me.cmdrestore.Enabled = d
        Me.cmdguest.Enabled = d
        Me.cmdpurge.Enabled = d
        Me.cmdgetsize.Enabled = d
    End Sub

    Sub KeyUser(ByRef d As Boolean)
        Me.cmddeleteuser.Enabled = d
        Me.cmdchange.Enabled = d
    End Sub

    Private Sub lstdb_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstdb.SelectedIndexChanged

        cmdguest.BackColor = System.Drawing.ColorTranslator.FromOle(vGray)
        cmdguest.Enabled = True
        LookGuest(VB6.GetItemString(lstdb, lstdb.SelectedIndex))
        Debug.Print(GetDBFile(VB6.GetItemString(lstdb, lstdb.SelectedIndex)))

    End Sub

    Sub LookGuest(ByRef dbname As String)

        System.Windows.Forms.Application.DoEvents()

        If GuestAllowed(dbname) = True Then
            cmdguest.BackColor = System.Drawing.Color.Lime
            cmdguest.Text = "Guest ON"
        Else
            cmdguest.BackColor = System.Drawing.Color.Red
            cmdguest.Text = "Guest OFF"
        End If
    End Sub

    Sub RebuildLog(ByRef dbname As String)
        Dim mdf As String
        Dim ldf As String
        Dim err3 As String
        Dim err1 As String

        mdf = GetDBFile(dbname, ldf)

        Debug.Print(ldf)

        If mdf = "" Then
            Logg("Clear log file failed")
            Exit Sub
        End If

        Logg($"Detaching {dbname}...")

        If DetachDatabase(dbname, err1) Then
        Else
            Logg($"Detach Failed Failed: {err1}")
        End If


        If File.Exists(ldf) Then
            Logg("Deleting log file...")
            File.Delete(ldf)
        End If

        Logg($"Reattaching {dbname}...")

        If AttachData(dbname, mdf, err3) Then
            Logg($"Log file for {dbname} cleared")
        Else
            Logg($"Attach Failed: {err3}")
        End If
    End Sub
    Private Sub frmmain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Verifica si frmLogin está abierto y lo cierra
        If Not frmlogin Is Nothing AndAlso Not frmlogin.IsDisposed Then
            frmlogin.Close()
        End If
    End Sub

    Private Sub cmddetach_Click(sender As Object, e As EventArgs) Handles cmddetach.Click

        Dim err1 As String

        If lstdb.SelectedIndex = -1 Then
            MsgBox("Select database", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        Dim x1 As String
        Dim d1 As String
        If MsgBox("Are you sure do you want to detach the database?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then



            Randomize()

            d1 = Format(Int(Rnd() * 999999), "000000")

            x1 = InputBox("Type this code to continue: " & d1, "Detach database")

            If x1 = "" Or x1 <> d1 Then
                Exit Sub
            End If

            System.Windows.Forms.Application.DoEvents()

            Dim selectedDatabase As String = If(lstdb.SelectedIndex <> -1, lstdb.SelectedItem.ToString(), String.Empty)

            If DetachDatabase(selectedDatabase, err1) Then
                Logg("Database " & selectedDatabase & " detached!")
                Me.LoadDatabase()
            Else
                Logg("Cannot detach database: " & err1)
            End If

        End If

    End Sub
End Class