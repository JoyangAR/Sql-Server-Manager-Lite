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
    Dim windrive As String
    Const vGray As Integer = &H8000000F
    Dim dbpath As String
    Dim bckpath As String
    Public islocaldb As Boolean


    Private Sub adduser_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles adduser.Click
        ' Replace VB6.ShowForm
        frmadduser.ShowDialog(Me)
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
                Dim isLocalPortInUse As Boolean = False

                For Each connection As TcpConnectionInformation In connections
                    If connection.LocalEndPoint.Port = 1433 Then
                        isLocalPortInUse = True
                        Exit For
                    End If
                Next

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

                If StartService(serviceName) Then
                    Logg("SQL Browser has been started")
                End If
            End If
        Else
            Logg("Stopping SQL Browser...")
            SetServiceStartMode(serviceName, ServiceStartMode.Manual)

            If StopService(serviceName) Then
                Logg("SQL Browser has been stopped")
            End If
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


    Function StartService(ByVal serviceName As String)
        Try
            Using controller As New ServiceController(serviceName)
                If controller.Status = ServiceControllerStatus.Stopped Then
                    ' Start the service
                    controller.Start()
                    ' Wait until the service is in the 'Running' state
                    controller.WaitForStatus(ServiceControllerStatus.Running)
                    Logg($"The service {serviceName} has been started successfully.")
                    Return True
                Else
                    Logg($"The service {serviceName} is already running.")
                    Return True
                End If
            End Using
        Catch ex As InvalidOperationException
            ' The exception occurs if the service does not exist or if the service cannot be controlled (e.g., may require elevated privileges).
            Logg($"Error attempting to start the service {serviceName}: {ex.Message}")
            Return False
        Catch ex As TimeoutException
            ' The exception occurs if the timeout is exceeded while waiting for the service to reach the 'Running' state.
            Logg($"Timeout error while starting the service {serviceName}: {ex.Message}")
            Return False
        Catch ex As Exception
            ' Catch other unexpected exceptions
            Logg($"Unexpected error while starting the service {serviceName}: {ex.Message}")
            Return False
        End Try
    End Function


    Function StopService(ByVal serviceName As String)
        Dim controller As New ServiceController(serviceName)

        If controller.Status = ServiceControllerStatus.Running Then
            Try
                controller.Stop()
                controller.WaitForStatus(ServiceControllerStatus.Stopped)
                Return True
            Catch ex As InvalidOperationException
                ' The exception occurs if the service does not exist or if the service cannot be controlled (e.g., may require elevated privileges).
                Logg($"Error attempting to stopping the service {serviceName}: {ex.Message}")
                Return False
            Catch ex As TimeoutException
                ' The exception occurs if the timeout is exceeded while waiting for the service to reach the 'Running' state.
                Logg($"Timeout error while stopping the service {serviceName}: {ex.Message}")
                Return False
            Catch ex As Exception
                ' Catch other unexpected exceptions
                Logg($"Unexpected error while stopping the service {serviceName}: {ex.Message}")
                Return False
            End Try
        End If
        Return True
    End Function


    Private Sub cmdadd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdadd.Click
        Dim SelectedFile As String

        Try
            dlgOpen.Title = "Select backup file"
            dlgOpen.Filter = "SQL Server Database Back-up File (*.bak)|*.bak|SQL Server Database File (*.mdf)|*.mdf"
            dlgOpen.ShowDialog()

            SelectedFile = dlgOpen.FileName

            If SelectedFile = "" Then
                Exit Sub
            End If

            Wait(True)

            If LCase(IO.Path.GetExtension(SelectedFile)) = ".bak" Then
                ProcessUpload(SelectedFile)
            ElseIf LCase(IO.Path.GetExtension(SelectedFile)) = ".mdf" Then
                ProcessUpload2(SelectedFile)
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
        ' Ensure that there is a selected item in the ListBox
        If lstdb.SelectedIndex <> -1 Then
            Dim selectedItem As String = lstdb.SelectedItem.ToString()
            ProcessBackup(selectedItem)
        End If
        Wait(False)

    End Sub

    Private Sub cmdchange_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdchange.Click
        If lstuser.SelectedIndex = -1 Then
            MsgBox("Select user", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        ' Replace VB6.ShowForm
        frmpwd.ShowDialog(Me)

    End Sub

    Private Sub cmdclear_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdclear.Click
        Me.txtlog.Text = ""
    End Sub

    Private Sub cmddelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmddelete.Click

        Dim err1 As String = ""

        If lstdb.SelectedIndex = -1 Then
            MsgBox("Select database", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        If MsgBox("Are you sure do you want to delete the database?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then

            If RNDSecurity("Delete database") Then
                System.Windows.Forms.Application.DoEvents()

                ' Ensure that there is a selected item in the ListBox
                If lstdb.SelectedIndex <> -1 Then
                    Dim selectedDatabase As String = lstdb.Items(lstdb.SelectedIndex).ToString()

                    ' Call the DeleteDatabase function and handle the result
                    If DeleteDatabase(selectedDatabase, err1) Then
                        Logg("Database " & selectedDatabase & " deleted!")
                        LoadDatabase()
                    Else
                        Logg("Cannot delete database: " & err1)
                    End If
                End If
            Else
                Logg("Random check not passed")
            End If

        End If

    End Sub

    Private Sub cmddeleteuser_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmddeleteuser.Click
        Dim err1 As String = ""

        If lstuser.SelectedIndex = -1 Then
            MsgBox("Select user", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        If MsgBox("Are you sure do you want to delete the user?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then

            If RNDSecurity("Delete user") Then
                Wait(True)

                System.Windows.Forms.Application.DoEvents()

                ' Ensure that there is a selected item in the ListBox
                If lstuser.SelectedIndex <> -1 Then
                    Dim selectedUser As String = lstuser.Items(lstuser.SelectedIndex).ToString()

                    ' Call the DeleteAccount function and handle the result
                    If DeleteAccount(selectedUser, err1) Then
                        Logg("User " & selectedUser & " deleted!")
                        LoadUser()
                    Else
                        Logg("Cannot delete user: " & err1)
                    End If
                End If


                Wait(False)
            Else
                Logg("Random check not passed")
            End If

        End If

    End Sub

    Private Sub cmdgetsize_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdgetsize.Click
        Dim ldf As String = ""
        Dim mdf As String = ""
        Dim msg1 As String
        Dim mdfsize As Long
        Dim ldfsize As Long
        Dim mdfloc As String = ""
        Dim ldfloc As String = ""
        Dim strmdf As String
        Dim strldf As String

        If lstdb.SelectedIndex = -1 Then
            MsgBox("Select database", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        GetDBFilesLocation(lstdb.Items(lstdb.SelectedIndex).ToString(), mdf, ldf)
        mdfsize = New System.IO.FileInfo(mdf).Length
        ldfsize = New System.IO.FileInfo(ldf).Length

        GetDBFilesLocation(lstdb.Items(lstdb.SelectedIndex).ToString(), mdfloc, ldfloc)

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

        msg1 = "DB Name: " & lstdb.Items(lstdb.SelectedIndex).ToString() & vbCrLf & "DB Filesize: " & strmdf & vbCrLf & "Log Filesize: " & strldf & vbCrLf & "DB Data: " & mdfloc & vbCrLf & "DB Log: " & ldfloc
        Logg(msg1)
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
            ' Ensure that there is a selected item in the ListBox
            If lstdb.SelectedIndex <> -1 Then
                Dim selectedDatabase As String = lstdb.SelectedItem.ToString()

                ' Call the RebuildLog function
                RebuildLog(selectedDatabase)
            End If

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

        Dim RstFile As String

        On Error GoTo xc

        dlgOpen.Title = "Select backup file"
        dlgOpen.Filter = "SQL Server Database Back-up File (*.bak)|*.bak"
        dlgOpen.ShowDialog()
        RstFile = dlgOpen.FileName

        If RstFile = "" Then
            Exit Sub
        End If

        Wait(True)
        ProcessRestore(RstFile, lstdb.SelectedItem.ToString())
        Wait(False)

        Exit Sub

xc:
        Debug.Print(Err.Description)
        Logg("Upload database restoration cancelled")
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub


    Private Sub cmddetach_Click(sender As Object, e As EventArgs) Handles cmddetach.Click

        Dim err1 As String = ""

        If lstdb.SelectedIndex = -1 Then
            MsgBox("Select database", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        If MsgBox("Are you sure do you want to detach the database?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then

            If RNDSecurity("Detach database") = True Then
                System.Windows.Forms.Application.DoEvents()

                Dim selectedDatabase As String = If(lstdb.SelectedIndex <> -1, lstdb.SelectedItem.ToString(), String.Empty)

                If DetachDatabase(selectedDatabase, err1) Then
                    Logg("Database " & selectedDatabase & " detached!")
                    Me.LoadDatabase()
                Else
                    Logg("Cannot detach database: " & err1)
                End If
            Else
                Logg("Random check not passed")
            End If

        End If

    End Sub

    Private Sub cmdkillconn_Click(sender As Object, e As EventArgs) Handles cmdkillconn.Click

        Dim err1 As String = ""

        If lstdb.SelectedIndex = -1 Then
            MsgBox("Select database", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        If MsgBox("Are you sure do you want to kill all connections with the database?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then

            If RNDSecurity("Kill connections") = True Then
                System.Windows.Forms.Application.DoEvents()
                Dim selectedDatabase As String = If(lstdb.SelectedIndex <> -1, lstdb.SelectedItem.ToString(), String.Empty)


                If KillConnections(selectedDatabase, err1) Then
                    Logg("Existing connections to the database " & selectedDatabase & " killed!")
                Else
                    Logg("Cannot kill connections to the database: " & err1)
                End If
            Else
                Logg("Random check not passed")
            End If

        End If
    End Sub

    Private Sub cmdtables_Click(sender As Object, e As EventArgs) Handles cmdtables.Click
        ' Check if a database is selected in lstdb
        If lstdb.SelectedIndex = -1 Then
            MsgBox("Select a database first.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        ' Create an instance of frmtables
        Dim tablesForm As New frmtableview()

        ' Show frmtables
        tablesForm.Text = lstdb.SelectedItem
        tablesForm.Icon = Me.Icon
        tablesForm.Show()
    End Sub

    Private Sub frmmain_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        LoadUser()
        LoadDatabase()
        windrive = System.IO.Path.GetPathRoot(System.Environment.SystemDirectory)
        If Not islocaldb Then
            dbpath = GetDefaultDataAndLogLocations()
        Else
            dbpath = GetDatabasePath()
        End If

        'Check If port 1433 Is open
        If Not islocaldb Then chkfirewall.CheckState = IsPortOpen(1433)
        If islocaldb Then chkfirewall.Enabled = False

        'Check If the SQLBrowser service Is installed And running
        Dim serviceName As String = "SQLBrowser"
        chksqlbrowser.Enabled = ServiceController.GetServices().Any(Function(s) s.ServiceName = serviceName)

        If chksqlbrowser.Enabled Then
            Dim serviceController As New ServiceController(serviceName)
            chksqlbrowser.CheckState = If(serviceController.Status = ServiceControllerStatus.Running, CheckState.Checked, CheckState.Unchecked)
        End If
        Dim errmsg As String = ""
        Dim version As String = ""
        If GetInstanceVersion(version, errmsg) Then
            'Display SQL Server version
            Logg($"SQL Server version: {version}")
        Else
            Logg(errmsg)
        End If
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
        Dim newpath As String = "" ' Temporal .bak path
        Dim err1 As String = "" ' Where error messages will be store
        Dim dbfile As String = "" ' Database mdf file name
        Dim dbname As String ' Database name
        Dim logfile As String = "" ' Database ldf file name
        Dim datapath As String = "" ' Database mdf path
        Dim logpath As String = "" ' Database ldf path
        Dim mdfo As Boolean ' MDF Only
        Dim mvf As Boolean = False

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

        Dim restoreOptionsForm As New frmbakdir()
            If restoreOptionsForm.ShowDialog() = DialogResult.OK Then
                ' Check which option was selected and proceed accordingly
                If restoreOptionsForm.optPrevious.Checked Then
                ' Use the paths from TabulateDatabase for restoration
                Logg("Tabulating database...")
                If Not TabulateDatabase(newpath, dbfile, logfile, err1, datapath, logpath) Then
                    Logg($"Error Tabulating:{err1}")
                Else
                End If
            ElseIf restoreOptionsForm.optDefault.Checked Then
                    ' Use the default locations
                    Dim defaultDataPath As String = ""
                    Dim defaultLogPath As String = ""
                    GetDefaultDataAndLogLocations(defaultDataPath, defaultLogPath)
                datapath = defaultDataPath
                logpath = defaultLogPath

            ElseIf restoreOptionsForm.optNew.Checked Then
                    ' Ask if the user wants to select MDF and LDF locations separately (if needed)
                    ' And then open a FolderBrowserDialog(s) to choose the new location(s)
                    If MessageBox.Show("Do you want to select MDF and LDF locations separately?", "Select Location", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        ' Prompt for MDF file location
                        Using fbdMdf As New FolderBrowserDialog()
                            fbdMdf.Description = "Select the location for the MDF file"
                            If fbdMdf.ShowDialog() = DialogResult.OK Then
                                Dim mdfPathSelected = fbdMdf.SelectedPath
                            Else
                                ' User cancelled the selection
                                MessageBox.Show("You did not select a location for the MDF file. Operation cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Return
                            End If
                        End Using

                        ' Prompt for LDF file location
                        Using fbdLdf As New FolderBrowserDialog()
                            fbdLdf.Description = "Select the location for the LDF file"
                            If fbdLdf.ShowDialog() = DialogResult.OK Then
                                Dim ldfPathSelected = fbdLdf.SelectedPath
                            Else
                                ' User cancelled the selection
                                MessageBox.Show("You did not select a location for the LDF file. Operation cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Return
                            End If
                        End Using
                    Else
                        ' Logic for single selection (both MDF and LDF in the same location)
                        Using fbd As New FolderBrowserDialog()
                            If fbd.ShowDialog() = DialogResult.OK Then
                                ' Use the selected path for both MDF and LDF
                                datapath = fbd.SelectedPath
                                logpath = fbd.SelectedPath
                            End If
                        End Using
                    End If
                End If
            Else
                ' Handle the case where the user cancels the form
                MessageBox.Show("Operation cancelled by the user.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

        If Not TabulateDatabase(newpath, dbfile, logfile, err1) Then
            Logg($"Error Tabulating:{err1}")
        Else

            dbname = CorrectDBname(dbfile)
            datapath = OnlyDirPath(datapath)
            logpath = OnlyDirPath(logpath)

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
                    Debug.Print(newpath)
                    If RestoreDatabase2(newpath, datapath, logpath, dbfile, logfile, err1) Then
                        'SetGuest(dbname, True)
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
        Dim err1 As String = ""
        Dim dbname As String
        Dim flname As String
        Dim mdfo As Boolean = False ' MDF Only

        If Not Directory.Exists(windrive) Then
            MsgBox("Admin access required", MsgBoxStyle.Exclamation, "Error!")
            Exit Sub
        End If

        ' Define the file name and database name
        flname = Path.GetFileName(mdffile)
        dbname = CorrectDBname(Path.GetFileNameWithoutExtension(mdffile))

        If DatabaseExists(dbname) Then
            Logg("Cannot upload database: Database already exists")
            Exit Sub
        End If

        Logg("Copying files...")
        System.Windows.Forms.Application.DoEvents()

        ' Ask the user about the location to attach the database
        Dim userChoice As MsgBoxResult = MsgBox("Do you want to attach the database using the default location or the location of the MDF file?" & vbCrLf & "Yes: Default Location" & vbCrLf & "No: Location of the MDF file", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel, "Select Location")

        ' Cancel if the user chooses Cancel
        If userChoice = MsgBoxResult.Cancel Then
            Logg("Upload database cancelled")
            Exit Sub
        End If

        ' Logic to determine the new path based on the user's choice
        If userChoice = MsgBoxResult.Yes Then
            ' Retrieve and use the default location
            Dim defaultDataPath As String = ""
            Dim defaultLogPath As String = ""
            GetDefaultDataAndLogLocations(defaultDataPath, defaultLogPath, err1)
            If String.IsNullOrEmpty(err1) AndAlso Not String.IsNullOrEmpty(defaultDataPath) Then
                newpath = Path.Combine(defaultDataPath, flname)
                ' Ensure copying the file to the default location
                If Not String.Equals(mdffile, newpath, StringComparison.OrdinalIgnoreCase) Then
                    File.Copy(mdffile, newpath, True)
                End If
            Else
                Logg($"Failed to retrieve default locations: {err1}")
                Exit Sub
            End If
        Else
            ' Use the MDF file's location for attaching
            newpath = mdffile ' No need to copy, use it directly as it's already in the desired location
        End If

        ' Confirm upload after ensuring the file is not in use
        If MsgBox("You will now upload this database. Proceed?" & vbCrLf & vbCrLf & "Database name: " & dbname, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Notice") = MsgBoxResult.Yes Then
            Logg("Uploading database...")
            If AttachData(dbname, newpath, mdfo, err1) Then
                Logg($"Database {dbname} is now uploaded")
            Else
                Logg($"Database upload of {dbname} failed: {err1}")
            End If
        Else
            Logg("Upload database cancelled")
        End If

        LoadDatabase()
    End Sub

    Sub Logg(ByRef msgtw As String)
        System.Windows.Forms.Application.DoEvents()

        ' Clear the log text if it exceeds a certain size to prevent overflow
        If Len(txtlog.Text) > 200000000 Then
            txtlog.Text = ""
        End If

        ' Add the new log entry to the text box, with double line breaks for readability
        If txtlog.Text = "" Then
            Me.txtlog.Text = msgtw & vbCrLf & vbCrLf
        Else
            Me.txtlog.Text = Me.txtlog.Text & msgtw & vbCrLf & vbCrLf
        End If

        ' Ensure the latest log entry is visible in the text box
        txtlog.SelectionStart = txtlog.TextLength
        txtlog.ScrollToCaret()

        ' If logging to a file is enabled, write the log entry to the appropriate log file
        If logtofile Then
            Try
                ' Construct the log file name using the current date
                Dim logFileName As String = $"SSML_{DateTime.Now:yyyy-MM-dd}.log"
                ' Build the path to the "SSML Logs" folder within the application directory
                Dim logFolderPath As String = Path.Combine(Application.StartupPath, "SSML Logs")
                ' Ensure the folder exists
                If Not Directory.Exists(logFolderPath) Then
                    Directory.CreateDirectory(logFolderPath)
                End If
                ' Construct the full path to the log file
                Dim logFilePath As String = Path.Combine(logFolderPath, logFileName)
                ' Use StreamWriter to append text to the log file; the second parameter as True means we're appending to the existing file
                Using writer As New StreamWriter(logFilePath, True)
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {msgtw}")
                End Using
            Catch ex As Exception
                ' Optional error handling, e.g., you might want to log the error elsewhere or simply ignore it
                MessageBox.Show("Error writing to log file: " & ex.Message)
            End Try
        End If
    End Sub

    Sub ProcessBackup(ByRef dbname As String)
        Logg("Full database backup started...")

        System.Windows.Forms.Application.DoEvents()

        Dim tmp12 As String
        Dim newbck As String = ""
        Dim err1 As String = ""

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
        Dim err1 As String = ""
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
        Dim err1 As String = ""
        Dim mode1 As String = ""

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
        If Not prov = "integrated" Then Me.cmdguest.Enabled = d
        Me.cmdpurge.Enabled = d
        Me.cmdgetsize.Enabled = d
        Me.cmddetach.Enabled = d
        If Not prov = "sqloledb" Then Me.cmdkillconn.Enabled = d
        Me.cmdtables.Enabled = d
    End Sub

    Sub KeyUser(ByRef d As Boolean)
        Me.cmddeleteuser.Enabled = d
        Me.cmdchange.Enabled = d
    End Sub

    Private Sub lstdb_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstdb.SelectedIndexChanged

        If Not prov = "integrated" Then cmdguest.BackColor = System.Drawing.ColorTranslator.FromOle(vGray)
        If Not prov = "integrated" Then cmdguest.Enabled = True
        ' Ensure that there is a selected item in the ListBox
        If lstdb.SelectedIndex <> -1 Then
            Dim selectedDatabase As String = lstdb.SelectedItem.ToString()

            ' Call the LookGuest function if islocaldb is False
            If Not prov = "integrated" Then LookGuest(selectedDatabase)
        End If


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
        Dim mdf As String = ""
        Dim ldf As String = ""
        Dim err3 As String = ""
        Dim err1 As String = ""
        Dim mdfo As Boolean = True ' MDF Only
        GetDBFilesLocation(dbname, mdf, ldf)

        Debug.Print(ldf)

        If mdf = "" Then
            Logg("Clear log file failed")
            Exit Sub
        End If

        Logg($"Detaching {dbname}...")

        If DetachDatabase(dbname, err1) Then
        Else
            Logg($"Detach database Failed: {err1}")
        End If


        If File.Exists(ldf) Then
            Logg("Deleting log file...")
            File.Delete(ldf)
        End If

        Logg($"Reattaching {dbname}...")

        If AttachData(dbname, mdf, mdfo, err3) Then
            Logg($"Log file for {dbname} cleared")
        Else
            Logg($"Attach Failed: {err3}")
        End If
    End Sub
    Private Sub frmmain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Check if frmLogin is open and close it
        If Not frmlogin Is Nothing AndAlso Not frmlogin.IsDisposed Then
            frmlogin.Close()
        End If
    End Sub

    Private Sub cmdQueryEditor_Click(sender As Object, e As EventArgs) Handles cmdQueryEditor.Click
        frmqueryeditor.Icon = Me.Icon
        frmqueryeditor.Text = "New Query"
        frmqueryeditor.Show()
    End Sub

    Private Sub cmdConfig_Click(sender As Object, e As EventArgs) Handles cmdConfig.Click
        frmconfig.Icon = Me.Icon
        frmconfig.ShowDialog()
    End Sub
End Class