Imports System.Collections.Generic

Public Class frmtableview
    Private selectedDatabase As String = DirectCast(frmmain.lstdb.SelectedItem, Object).DatabaseName

    Private Sub frmtableview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Get table names for the selected database
        Dim tableNames As List(Of String) = GetTableNames(selectedDatabase)

        ' Populate the ListView with table names
        lsttables.Items.Clear()
        For Each tableName As String In tableNames
            lsttables.Items.Add(tableName)
        Next
    End Sub

    Private Sub lsttables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lsttables.SelectedIndexChanged
        ' Get the selected table and the number of rows from the controls
        If Not lsttables.SelectedItem = Nothing Then
            Dim selectedTable As String = lsttables.SelectedItem.ToString()

            Dim numRows As Integer

            If Not Integer.TryParse(txtrows.Text, numRows) Then
                MessageBox.Show("Please enter a valid number for the number of rows.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Get the rows and display them in the DataGridView
            Dim dataTable As DataTable = GetRows(selectedDatabase, selectedTable, numRows)
            dtgridtable.DataSource = dataTable
        End If
    End Sub

    Private Sub CmdRefresh_Click(sender As Object, e As EventArgs) Handles CmdRefresh.Click
        lsttables_SelectedIndexChanged(sender, e)
    End Sub
    Private Sub dtgridtable_MouseClick(sender As Object, e As MouseEventArgs) Handles dtgridtable.MouseClick
        ' Checks if the right mouse button was pressed
        If e.Button = MouseButtons.Right Then
            ' Displays the ContextMenuStrip at the cursor position
            ContextMenuStrip1.Show(dtgridtable, e.Location)
        End If
    End Sub

    Private Sub CmdSearch(sender As Object, e As EventArgs) Handles SearchRC.Click
        ' Displays a custom dialog box to enter the text to search for and the replacement text
        Using SearchForm As New Form()
            SearchForm.Text = "Search"
            SearchForm.Size = New Size(300, 150)
            SearchForm.StartPosition = FormStartPosition.CenterScreen

            Dim searchLabel As New Label()
            searchLabel.Text = "Search:"
            searchLabel.Location = New Point(10, 10)

            Dim searchTextBox As New TextBox()
            searchTextBox.Location = New Point(120, 10)
            searchTextBox.Size = New Size(150, 20)

            Dim columnLabel As New Label()
            columnLabel.Text = "Column"
            columnLabel.Location = New Point(10, 40)

            Dim columnComboBox As New ComboBox()
            columnComboBox.Location = New Point(120, 40)
            columnComboBox.Size = New Size(150, 20)

            Dim acceptButton As New Button()
            acceptButton.Text = "Ok"
            acceptButton.Location = New Point(120, 70)
            acceptButton.DialogResult = DialogResult.OK

            SearchForm.ShowIcon = False
            SearchForm.FormBorderStyle = FormBorderStyle.Fixed3D
            SearchForm.Controls.AddRange(New Control() {searchLabel, searchTextBox, columnLabel, columnComboBox, acceptButton})

            If lsttables.SelectedItem IsNot Nothing Then
                Dim selectedTable As String = lsttables.SelectedItem.ToString()
                Dim columnNames As List(Of String) = GetColumnNames(selectedDatabase, selectedTable)
                columnComboBox.Items.AddRange(columnNames.ToArray())
            End If

            If SearchForm.ShowDialog() = DialogResult.OK Then
                If lsttables.SelectedItem IsNot Nothing Then
                    Dim selectedTable As String = lsttables.SelectedItem.ToString()
                    Dim numRows As Integer
                    Dim searchQuery As String = searchTextBox.Text
                    Dim whereCondition As String = columnComboBox.SelectedItem?.ToString()

                    If String.IsNullOrEmpty(whereCondition) Then
                        MessageBox.Show("Please select a column.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If

                    If Not Integer.TryParse(txtrows.Text, numRows) Then
                        MessageBox.Show("Please enter a valid number for the number of rows.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If

                    ' Get the rows and display them in the DataGridView
                    Dim dataTable As DataTable = GetSearchResults(selectedDatabase, selectedTable, searchQuery, whereCondition)
                    dtgridtable.DataSource = dataTable
                End If
            End If

        End Using
    End Sub
End Class
