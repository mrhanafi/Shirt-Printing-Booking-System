
Public Class Form3
    Public Shared row As Integer

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MetroTextBox2.Text = Form2.ds.Tables("infox").Rows(row)("nama")
        MetroTextBox3.Text = Form2.ds.Tables("infox").Rows(row)("ic")
        MetroTextBox4.Text = Form2.ds.Tables("infox").Rows(row)("email")

        'Dim tarikh1 As DateTime
        'tarikh1 = Convert.ToDateTime(Form2.ds.Tables("infox").Rows(row)("tarikht").ToString)
        'DateTimePicker1.Text = Format(tarikh1, "dd/MM/yyyy")
        DateTimePicker1.Text = Format(Form2.ds.Tables("infox").Rows(row)("tarikht"))

        If Form2.ds.Tables("infox").Rows(row)("saiz") = "XS" Then
            ComboBox1.SelectedIndex = 0
        ElseIf Form2.ds.Tables("infox").Rows(row)("saiz") = "S" Then
            ComboBox1.SelectedIndex = 1
        ElseIf Form2.ds.Tables("infox").Rows(row)("saiz") = "M" Then
            ComboBox1.SelectedIndex = 2
        ElseIf Form2.ds.Tables("infox").Rows(row)("saiz") = "L" Then
            ComboBox1.SelectedIndex = 3
        ElseIf Form2.ds.Tables("infox").Rows(row)("saiz") = "XL" Then
            ComboBox1.SelectedIndex = 4
        ElseIf Form2.ds.Tables("infox").Rows(row)("saiz") = "XXL" Then
            ComboBox1.SelectedIndex = 5
        End If

        MetroTextBox1.Text = Form2.ds.Tables("infox").Rows(row)("kuantiti")
    End Sub

    Private Sub MetroButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MetroButton1.Click
        Me.Close()
    End Sub


    Private Sub MetroButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MetroButton3.Click
        Dim oleConn As System.Data.OleDb.OleDbConnection
        oleConn = New System.Data.OleDb.OleDbConnection
        oleConn.ConnectionString = My.Settings.Database2ConnectionString
        Dim cmd As New OleDb.OleDbCommand
        oleConn.Open()
        cmd.Connection = oleConn
        cmd.CommandText = "DELETE FROM customer WHERE ID = " + (row + 1).ToString
        cmd.ExecuteNonQuery()
        oleConn.Close()

        Me.Close()
        Form2.readdatashow(Form2.ListView3)
    End Sub
End Class