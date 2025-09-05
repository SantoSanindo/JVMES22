Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.Office.Interop

Public Class MasterDepartment
    Public Shared menu As String = "Master Department"

    Dim oleCon As OleDbConnection
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If globVar.add > 0 Then
            If txt_department.Text <> "" Then
                Try
                    Dim sql As String = "
                        IF NOT EXISTS (SELECT 1 FROM DEPARTMENT WHERE upper(DEPARTMENT) = '" & Trim(txt_department.Text.ToUpper) & "')
                            BEGIN
                                INSERT INTO DEPARTMENT (DEPARTMENT, INSERT_BY) VALUES ('" & Trim(txt_department.Text) & "','" & globVar.username & "')
                            END
                        ELSE
                            BEGIN
                                RAISERROR('Data already exists', 16, 1)
                            END"

                    Dim cmd = New SqlCommand(sql, Database.koneksi)
                    cmd.ExecuteNonQuery()

                    txt_department.Select()

                    dgv_masterprocess.DataSource = Nothing
                    DGV_MasterDepartment()
                    txt_department.Clear()

                Catch ex As Exception
                    RJMessageBox.Show("Error Master Department - 1 =>" & ex.Message)
                End Try
            End If
        Else
            RJMessageBox.Show("Your Access cannot execute this action")
        End If
    End Sub

    Private Sub MasterDepartment_Load(sender As Object, e As EventArgs) Handles Me.Load
        If globVar.view > 0 Then

            txt_department.Select()
            DGV_MasterDepartment()

        End If
    End Sub

    Private Sub DGV_MasterDepartment()
        dgv_masterprocess.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_masterprocess.DataSource = Nothing
        dgv_masterprocess.Rows.Clear()
        dgv_masterprocess.Columns.Clear()
        Call Database.koneksi_database()
        Dim dtMasterMaterial As DataTable = Database.GetData("select department [Department], insert_by [Created By], datetime_insert [Date Save] from department order by department")

        dgv_masterprocess.DataSource = dtMasterMaterial

        Dim delete As DataGridViewButtonColumn = New DataGridViewButtonColumn
        delete.Name = "delete"
        delete.HeaderText = "Delete"
        delete.Width = 100
        delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        delete.Text = "Delete"
        delete.UseColumnTextForButtonValue = True
        dgv_masterprocess.Columns.Insert(3, delete)

    End Sub

    Private Sub dgv_masterprocess_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_masterprocess.CellClick
        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If e.ColumnIndex = -1 Then
            Exit Sub
        End If

        If dgv_masterprocess.Columns(e.ColumnIndex).Name = "delete" Then
            If globVar.delete > 0 Then

                Dim queryCek As String = "SELECT * FROM dbo.stock_card where department='" & dgv_masterprocess.Rows(e.RowIndex).Cells("Department").Value & "'"
                Dim dsexist = New DataSet
                Dim adapterexist = New SqlDataAdapter(queryCek, Database.koneksi)
                adapterexist.Fill(dsexist)

                If dsexist.Tables(0).Rows.Count > 0 Then
                    RJMessageBox.Show("Cannot delete. Because this department already use")
                    Exit Sub
                End If

                Dim result = RJMessageBox.Show("Are you sure delete this data?", "Warning", MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then
                    Try
                        Dim sql As String = "delete from department where department='" & dgv_masterprocess.Rows(e.RowIndex).Cells("Department").Value & "'"
                        Dim cmd = New SqlCommand(sql, Database.koneksi)
                        If cmd.ExecuteNonQuery() Then
                            RJMessageBox.Show("Delete Success")
                        End If
                        dgv_masterprocess.DataSource = Nothing
                        DGV_MasterDepartment()
                    Catch ex As Exception
                        RJMessageBox.Show("Error Master Department - 2 =>" & ex.Message)
                    End Try
                End If
            Else
                RJMessageBox.Show("Your Access cannot execute this action")
            End If
        End If
    End Sub

    Private Sub dgv_material_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgv_masterprocess.DataBindingComplete
        For i As Integer = 0 To dgv_masterprocess.RowCount - 1
            If dgv_masterprocess.Rows(i).Index Mod 2 = 0 Then
                dgv_masterprocess.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                dgv_masterprocess.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With dgv_masterprocess
            .DefaultCellStyle.Font = New Font("Tahoma", 14)

            For i As Integer = 0 To .ColumnCount - 1
                .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

            .EnableHeadersVisualStyles = False
            With .ColumnHeadersDefaultCellStyle
                .BackColor = Color.Navy
                .ForeColor = Color.White
                .Font = New Font("Tahoma", 13, FontStyle.Bold)
                .Alignment = HorizontalAlignment.Center
                .Alignment = ContentAlignment.MiddleCenter
            End With

            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

End Class