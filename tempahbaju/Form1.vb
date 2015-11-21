Public Class form1

    Private Sub MetroButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MetroButton2.Click
        Me.Close()
    End Sub

    Private Sub MetroButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MetroButton1.Click
        If MetroTextBox1.Text = "" Or MetroTextBox2.Text = "" Then
            MsgBox("Sila Masukkan Nama Pengguna Dan Kata Laluan.")
        ElseIf MetroTextBox1.Text = "admin" And MetroTextBox2.Text = "admin" Then
            Form2.ShowDialog()
            ' Me.Hide()
        Else
            MsgBox("Nama Pengguna Atau Kata Laluan Salah. Sila Cuba Lagi.")
        End If
    End Sub
End Class
