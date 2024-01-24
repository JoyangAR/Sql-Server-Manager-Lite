Imports System.Collections.Generic

Public Class frmtableview

    Private selectedDatabase As String = frmmain.lstdb.SelectedItem.ToString()

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
        Dim selectedTable As String = lsttables.SelectedItem.ToString()
        Dim numRows As Integer

        If Not Integer.TryParse(txtrows.Text, numRows) Then
            MessageBox.Show("Please enter a valid number for the number of rows.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Get the rows and display them in the DataGridView
        Dim dataTable As DataTable = GetRows(selectedTable, numRows)
        dtgridtable.DataSource = dataTable
    End Sub
End Class
