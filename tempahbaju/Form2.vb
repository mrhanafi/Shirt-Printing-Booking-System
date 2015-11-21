Imports System.Drawing.Drawing2D
Imports System.IO

Public Class Form2
    Private lastPT As Point
    Private signature As New GraphicsPath
    Public Shared ds As DataSet
    Public Shared row As Integer
    Public Shared row1 As Integer
    Public Shared row2 As Integer
    Dim totaltempah As Integer
    Dim totalbayar As Integer
    Dim pay As String
    Dim format1 As String = "dd/MM/yyyy"
    Dim today As String
    Public Shared namabaju As String
    Dim imagepath As String
    Public Shared savepath As String = "C:\images\"
    Dim pilihan As Boolean = False
    Dim reka As Boolean = False
    Public Shared duekali As Boolean = False

    Private Sub MetroButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MetroButton1.Click
        Me.Close()
    End Sub

    Private Sub MetroButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MetroButton3.Click
        If ComboBox1.Text = "" Or MetroTextBox1.Text = "" Or MetroTextBox2.Text = "" Or MetroTextBox3.Text = "" Or MetroTextBox4.Text = "" Then
            MsgBox("Sila Isi Perincian Dan Tempahan Pada Kotak Yang Disediakan.")
        Else
            Dim nama As String = MetroTextBox2.Text
            Dim ic As String = MetroTextBox3.Text
            Dim email As String = MetroTextBox4.Text
            Dim size As String = ComboBox1.Text
            Dim qty As Decimal = MetroTextBox1.Text
            Dim dalamproses As String = "Dalam Proses"
            Dim tarikh1 As DateTime
            Dim tarikh11 As String
            tarikh1 = Convert.ToDateTime(DateTimePicker2.Text)
            tarikh11 = Format(tarikh1, "dd/MM/yyyy")

            Dim total As Double
            If ComboBox1.Text = "XS" Then
                total = qty * 15
            ElseIf ComboBox1.Text = "S" Then
                total = qty * 20
            ElseIf ComboBox1.Text = "M" Then
                total = qty * 23
            ElseIf ComboBox1.Text = "L" Then
                total = qty * 26
            ElseIf ComboBox1.Text = "XL" Then
                total = qty * 29
            ElseIf ComboBox1.Text = "XXL" Then
                total = qty * 32
            End If

            Dim oleConn As System.Data.OleDb.OleDbConnection

            oleConn = New System.Data.OleDb.OleDbConnection
            oleConn.ConnectionString = My.Settings.Database2ConnectionString
            Dim cmd As New OleDb.OleDbCommand
            Try
                oleConn.Open()
                cmd.Connection = oleConn
                cmd.CommandText = "insert into customer(nama,ic,email,tarikht,saiz,kuantiti,nilaitempah,nilaibayar,status) values('" + MetroTextBox2.Text + "','" + MetroTextBox3.Text + "','" + MetroTextBox4.Text + "','" + tarikh11 + "','" + ComboBox1.Text + "','" + MetroTextBox1.Text + "','" + total.ToString + "','" + total.ToString + "','" + dalamproses + "')"
                cmd.ExecuteNonQuery()

                oleConn.Close()
                MsgBox("Tempahan Baju Berjaya.")
                If pilihan Then
                    simpan()
                Else

                End If
            Catch ex As Exception
                MsgBox(ErrorToString)
            End Try
        End If


    End Sub
    'Public Sub copyfile()
    '    Dim strPath As String = Application.StartupPath()
    '    Dim np As String = strPath + "\baju\" + namabaju.ToString + ".jpg"
    '    My.Computer.FileSystem.CopyFile(fn, np, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs,
    'Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
    'End Sub
    Public Sub simpan()
        Dim np As String = "C:\images\" + MetroTextBox2.Text + "-" + namabaju + Path.GetExtension(imagepath)
        ' MsgBox(imagepath)
        ' MsgBox(namabaju)
        If imagepath <> np Then
            File.Copy(imagepath, np, True)
            imagepath = np
        End If
    End Sub
    Public Sub readdatashow(ByVal lvw As ListView)
        Dim oleConn As System.Data.OleDb.OleDbConnection
        Dim da As OleDb.OleDbDataAdapter
        ds = New DataSet
        oleConn = New System.Data.OleDb.OleDbConnection
        oleConn.ConnectionString = My.Settings.Database2ConnectionString
        oleConn.Open()
        da = New OleDb.OleDbDataAdapter("SELECT * FROM customer", oleConn)
        da.Fill(ds, "infox")
        oleConn.Close()
        lvw.Clear()
        With lvw
            .Items.Clear()
            .View = View.Details
            .GridLines = True
            .FullRowSelect = True
            .Columns.Add("ID", 0, HorizontalAlignment.Left)
            .Columns.Add("NAMA", 150, HorizontalAlignment.Left)
            .Columns.Add("NO.KP", 150, HorizontalAlignment.Left)
            .Columns.Add("EMAIL", 200, HorizontalAlignment.Left)
            .Columns.Add("TARIKH TEMPAH", 130, HorizontalAlignment.Left)
            .Columns.Add("SAIZ", 50, HorizontalAlignment.Left)
            .Columns.Add("KUANTITI", 100, HorizontalAlignment.Left)
            If lvw Is ListView3 Then
                .Columns.Add("Tempahan (RM)", 0, HorizontalAlignment.Left)
                .Columns.Add("Bayaran (RM)", 0, HorizontalAlignment.Left)
                .Columns.Add("Status", 100, HorizontalAlignment.Left)
            ElseIf lvw Is ListView1 Then
                .Columns.Add("Tempahan (RM)", 130, HorizontalAlignment.Left)
                .Columns.Add("Bayaran (RM)", 0, HorizontalAlignment.Left)
                .Columns.Add("Status", 0, HorizontalAlignment.Left)
            ElseIf lvw Is ListView2 Then
                .Columns.Add("Tempahan (RM)", 0, HorizontalAlignment.Left)
                .Columns.Add("Bayaran (RM)", 130, HorizontalAlignment.Left)
                .Columns.Add("Status", 0, HorizontalAlignment.Left)
            End If
        End With
        For Each row As DataRow In ds.Tables("infox").Rows
            Dim lst As ListViewItem
            lst = lvw.Items.Add(row(0))
            For i As Integer = 1 To ds.Tables("infox").Columns.Count - 1
                lst.SubItems.Add(row(i))
            Next
        Next

    End Sub

    Public Sub readdatashow2(ByVal lvw As ListView)
        Dim oleConn As System.Data.OleDb.OleDbConnection
        Dim da As OleDb.OleDbDataAdapter
        ds = New DataSet
        oleConn = New System.Data.OleDb.OleDbConnection
        oleConn.ConnectionString = My.Settings.Database2ConnectionString
        oleConn.Open()
        da = New OleDb.OleDbDataAdapter("SELECT * FROM customer WHERE status LIKE '%" & "Dalam Proses" & "%'", oleConn)
        da.Fill(ds, "tempah")
        oleConn.Close()
        lvw.Clear()
        With lvw
            .Items.Clear()
            .View = View.Details
            .GridLines = True
            .FullRowSelect = True
            .Columns.Add("ID", 0, HorizontalAlignment.Left)
            .Columns.Add("NAMA", 150, HorizontalAlignment.Left)
            .Columns.Add("NO.KP", 150, HorizontalAlignment.Left)
            .Columns.Add("EMAIL", 200, HorizontalAlignment.Left)
            .Columns.Add("TARIKH TEMPAH", 130, HorizontalAlignment.Left)
            .Columns.Add("SAIZ", 50, HorizontalAlignment.Left)
            .Columns.Add("KUANTITI", 100, HorizontalAlignment.Left)
            .Columns.Add("Tempahan (RM)", 130, HorizontalAlignment.Left)
            .Columns.Add("Bayaran (RM)", 0, HorizontalAlignment.Left)
            .Columns.Add("Status", 0, HorizontalAlignment.Left)

        End With
        For Each row As DataRow In ds.Tables("tempah").Rows
            Dim lst As ListViewItem
            lst = lvw.Items.Add(row(0))
            For i As Integer = 1 To ds.Tables("tempah").Columns.Count - 1
                lst.SubItems.Add(row(i))
            Next
        Next
        row1 = 0
        totaltempah = 0
        For Each huhu As DataRow In ds.Tables("tempah").Rows
            totaltempah = totaltempah + ds.Tables("tempah").Rows(row1)("nilaitempah")
            row1 = row1 + 1
        Next
        MetroTextBox6.Text = totaltempah
    End Sub
    Public Sub readdatashow3(ByVal lvw As ListView)
        Dim oleConn As System.Data.OleDb.OleDbConnection
        Dim da As OleDb.OleDbDataAdapter
        ds = New DataSet
        oleConn = New System.Data.OleDb.OleDbConnection
        oleConn.ConnectionString = My.Settings.Database2ConnectionString
        oleConn.Open()
        da = New OleDb.OleDbDataAdapter("SELECT * FROM customer WHERE status LIKE '%" & "Selesai" & "%'", oleConn)
        da.Fill(ds, "bayar")
        oleConn.Close()
        lvw.Clear()
        With lvw
            .Items.Clear()
            .View = View.Details
            .GridLines = True
            .FullRowSelect = True
            .Columns.Add("ID", 0, HorizontalAlignment.Left)
            .Columns.Add("NAMA", 150, HorizontalAlignment.Left)
            .Columns.Add("NO.KP", 150, HorizontalAlignment.Left)
            .Columns.Add("EMAIL", 200, HorizontalAlignment.Left)
            .Columns.Add("TARIKH TEMPAH", 130, HorizontalAlignment.Left)
            .Columns.Add("SAIZ", 50, HorizontalAlignment.Left)
            .Columns.Add("KUANTITI", 100, HorizontalAlignment.Left)
            .Columns.Add("Tempahan (RM)", 0, HorizontalAlignment.Left)
            .Columns.Add("Bayaran (RM)", 130, HorizontalAlignment.Left)
            .Columns.Add("Status", 0, HorizontalAlignment.Left)
        End With
        For Each row As DataRow In ds.Tables("bayar").Rows
            Dim lst As ListViewItem
            lst = lvw.Items.Add(row(0))
            For i As Integer = 1 To ds.Tables("bayar").Columns.Count - 1
                lst.SubItems.Add(row(i))
            Next
        Next
        row2 = 0
        totalbayar = 0
        For Each haha As DataRow In ds.Tables("bayar").Rows
            totalbayar = totalbayar + ds.Tables("bayar").Rows(row2)("nilaibayar")
            row2 = row2 + 1
        Next
        MetroTextBox11.Text = totalbayar
    End Sub
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' readdatashow()
        today = DateTime.Now.ToString(format1)
        PictureBox2.Visible = False
    End Sub

    Private Sub TabControlAction(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        Try
            If TabControl1.SelectedTab.Text = "REKOD JUALAN" Then
                readdatashow3(ListView2)
                readdatashow2(ListView1)
            ElseIf TabControl1.SelectedTab.Text = "UBAH TEMPAHAN" Then
                readdatashow(ListView3)
            ElseIf TabControl1.SelectedTab.Text = "TEMPAH" Then
                ComboBox1.SelectedIndex = -1
                MetroTextBox1.Text = ""
                MetroTextBox2.Text = ""
                MetroTextBox3.Text = ""
                MetroTextBox4.Text = ""
            ElseIf TabControl1.SelectedTab.Text = "PEMBAYARAN" Then
                readdatashow2(ListView4)
            End If
        Catch ex As Exception
            MsgBox(ErrorToString)
        End Try


    End Sub

    Private Sub ListView3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView3.DoubleClick
        Form3.row = ListView3.SelectedItems(0).Index
        'indx = ListView3.FocusedItem.Index
        Form3.ShowDialog()
    End Sub

    Public Sub searchme()
        Dim oleConn As System.Data.OleDb.OleDbConnection
        Dim da As OleDb.OleDbDataAdapter
        ds = New DataSet
        oleConn = New System.Data.OleDb.OleDbConnection
        oleConn.ConnectionString = My.Settings.Database2ConnectionString
        oleConn.Open()
        ' Dim search As String
        ' search = "SELECT * FROM customer WHERE ic LIKE '%" & MetroTextBox5.Text & "%'"
        da = New OleDb.OleDbDataAdapter("SELECT ID,nama,ic,email,tarikht,saiz,kuantiti,status FROM customer WHERE ic LIKE '%" & MetroTextBox5.Text & "%'", oleConn)
        da.Fill(ds, "infox")
        oleConn.Close()
        ListView3.Clear()
        With ListView3
            .Items.Clear()
            .View = View.Details
            .GridLines = True
            .FullRowSelect = True
            .Columns.Add("ID", 0, HorizontalAlignment.Left)
            .Columns.Add("NAMA", 150, HorizontalAlignment.Left)
            .Columns.Add("NO.KP", 150, HorizontalAlignment.Left)
            .Columns.Add("EMAIL", 200, HorizontalAlignment.Left)
            .Columns.Add("TARIKH TEMPAH", 130, HorizontalAlignment.Left)
            .Columns.Add("SAIZ", 50, HorizontalAlignment.Left)
            .Columns.Add("KUANTITI", 100, HorizontalAlignment.Left)
            .Columns.Add("STATUS", 100, HorizontalAlignment.Left)

        End With
        For Each row As DataRow In ds.Tables("infox").Rows
            Dim lst As ListViewItem
            lst = ListView3.Items.Add(row(0))
            For i As Integer = 1 To ds.Tables("infox").Columns.Count - 1
                lst.SubItems.Add(row(i))
            Next
        Next
    End Sub

    Private Sub MetroTextBox5_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MetroTextBox5.TextChanged
        Try
            searchme()
        Catch ex As Exception
            MsgBox(ErrorToString)
        End Try

        ' readdatashow(ListView3)
    End Sub

    Private Sub ListView4_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView4.DoubleClick
        MetroTextBox9.Text = ""
        MetroTextBox8.Text = ""
        MetroTextBox7.Text = ""
        MetroTextBox12.Text = ""
        MetroTextBox10.Text = ""
        MetroTextBox13.Text = ""
        MetroTextBox14.Text = ""
        row = ListView4.SelectedItems(0).Index

        MetroTextBox9.Text = ds.Tables("tempah").Rows(row)("nama")
        MetroTextBox8.Text = ds.Tables("tempah").Rows(row)("ic")
        MetroTextBox7.Text = ds.Tables("tempah").Rows(row)("email")
        MetroTextBox12.Text = ds.Tables("tempah").Rows(row)("saiz")
        MetroTextBox10.Text = ds.Tables("tempah").Rows(row)("kuantiti")
        MetroTextBox13.Text = ds.Tables("tempah").Rows(row)("status")
        MetroTextBox14.Text = ds.Tables("tempah").Rows(row)("tarikht")
        pay = ds.Tables("tempah").Rows(row)("nilaibayar")

    End Sub

    Private Sub MetroButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MetroButton5.Click
        Dim oleConn As System.Data.OleDb.OleDbConnection
        Dim da As OleDb.OleDbDataAdapter
        Dim add As String = ""
        ds = New DataSet
        oleConn = New System.Data.OleDb.OleDbConnection
        oleConn.ConnectionString = My.Settings.Database2ConnectionString
        oleConn.Open()
        ' Dim search As String
        ' search = "SELECT * FROM customer WHERE ic LIKE '%" & MetroTextBox5.Text & "%'"
        da = New OleDb.OleDbDataAdapter("UPDATE customer SET status = '" & ComboBox2.Text & "' WHERE ic LIKE '%" & MetroTextBox8.Text & "%'", oleConn)
        da.Fill(ds, "infox")
        oleConn.Close()
        readdatashow2(ListView4)
    End Sub

    Private Sub MetroButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MetroButton6.Click
        PrintPreviewDialog1.Document = PrintDocument1 'PrintPreviewDialog associate with PrintDocument.


        DirectCast(DirectCast(PrintPreviewDialog1.Controls(1), ToolStrip).Items(0), ToolStripButton).Enabled = False
        PrintPreviewDialog1.ShowDialog()

        PrintDialog1.Document = PrintDocument1 'PrintDialog associate with PrintDocument.

        If PrintDialog1.ShowDialog() = DialogResult.OK Then

            PrintDocument1.Print()

        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim drawFont As New Font("Arial", 14)
        Dim drawBrush As New SolidBrush(Color.DarkBlue)
        Dim drawFormat As New StringFormat
        Dim blackPen As New Pen(Color.Black, 3)

        e.Graphics.DrawString("RESIT PEMBAYARAN TEMPAHAN BAJU KEDAI KAMI", drawFont, drawBrush, 120.0F, 52.0F, drawFormat)
        e.Graphics.DrawString("Jln 123, Taman Adtech Melaka, 34912", drawFont, drawBrush, 120.0F, 72.0F, drawFormat)
        e.Graphics.DrawString("Melaka", drawFont, drawBrush, 120.0F, 92.0F, drawFormat)

        'horizon line
        e.Graphics.DrawLine(blackPen, 120.0F, 130.0F, 750.0F, 130.0F)
        e.Graphics.DrawLine(blackPen, 120.0F, 160.0F, 750.0F, 160.0F)
        e.Graphics.DrawLine(blackPen, 120.0F, 190.0F, 750.0F, 190.0F)
        e.Graphics.DrawLine(blackPen, 120.0F, 220.0F, 750.0F, 220.0F)
        e.Graphics.DrawLine(blackPen, 120.0F, 250.0F, 750.0F, 250.0F)
        e.Graphics.DrawLine(blackPen, 120.0F, 280.0F, 750.0F, 280.0F)
        e.Graphics.DrawLine(blackPen, 120.0F, 310.0F, 750.0F, 310.0F)
        e.Graphics.DrawLine(blackPen, 120.0F, 340.0F, 750.0F, 340.0F)

        'vertical line
        e.Graphics.DrawLine(blackPen, 120.0F, 130.0F, 120.0F, 340.0F)
        e.Graphics.DrawLine(blackPen, 340.0F, 130.0F, 340.0F, 340.0F)
        e.Graphics.DrawLine(blackPen, 750.0F, 130.0F, 750.0F, 340.0F)

        'penerangan
        e.Graphics.DrawString("NAMA", drawFont, drawBrush, 125.0F, 165.0F, drawFormat)
        e.Graphics.DrawString("EMAIL", drawFont, drawBrush, 125.0F, 195.0F, drawFormat)
        e.Graphics.DrawString("PILIHAN SAIZ", drawFont, drawBrush, 125.0F, 225.0F, drawFormat)
        e.Graphics.DrawString("KUANTITI BAJU", drawFont, drawBrush, 125.0F, 255.0F, drawFormat)
        e.Graphics.DrawString("HARGA SEHELAI", drawFont, drawBrush, 125.0F, 285.0F, drawFormat)
        e.Graphics.DrawString("BAYARAN TEMPAHAN", drawFont, drawBrush, 125.0F, 315.0F, drawFormat)

        'detail
        e.Graphics.DrawString(MetroTextBox9.Text, drawFont, drawBrush, 345.0F, 165.0F, drawFormat)
        e.Graphics.DrawString(MetroTextBox7.Text, drawFont, drawBrush, 345.0F, 195.0F, drawFormat)
        e.Graphics.DrawString(MetroTextBox12.Text, drawFont, drawBrush, 345.0F, 225.0F, drawFormat)
        e.Graphics.DrawString(MetroTextBox10.Text, drawFont, drawBrush, 345.0F, 255.0F, drawFormat)
        If MetroTextBox12.Text = "XS" Then
            e.Graphics.DrawString("RM 15", drawFont, drawBrush, 345.0F, 285.0F, drawFormat)
        ElseIf MetroTextBox12.Text = "S" Then
            e.Graphics.DrawString("RM 20", drawFont, drawBrush, 345.0F, 285.0F, drawFormat)
        ElseIf MetroTextBox12.Text = "M" Then
            e.Graphics.DrawString("RM 23", drawFont, drawBrush, 345.0F, 285.0F, drawFormat)
        ElseIf MetroTextBox12.Text = "L" Then
            e.Graphics.DrawString("RM 26", drawFont, drawBrush, 345.0F, 285.0F, drawFormat)
        ElseIf MetroTextBox12.Text = "XL" Then
            e.Graphics.DrawString("RM 29", drawFont, drawBrush, 345.0F, 285.0F, drawFormat)
        ElseIf MetroTextBox12.Text = "XXL" Then
            e.Graphics.DrawString("RM 32", drawFont, drawBrush, 345.0F, 285.0F, drawFormat)
        End If
        e.Graphics.DrawString("RM " + pay, drawFont, drawBrush, 345.0F, 315.0F, drawFormat)

        'sign
        e.Graphics.DrawString(".................................", drawFont, drawBrush, 125.0F, 350.0F, drawFormat)
        e.Graphics.DrawString("(" + MetroTextBox9.Text + ")", drawFont, drawBrush, 125.0F, 370.0F, drawFormat)
        e.Graphics.DrawString("TARIKH :", drawFont, drawBrush, 125.0F, 390.0F, drawFormat)
        e.Graphics.DrawString(today, drawFont, drawBrush, 205.0F, 390.0F, drawFormat)
    End Sub

    Private Sub MetroButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MetroButton7.Click
        Form4.ShowDialog()
    End Sub

    Private Sub MetroButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MetroButton8.Click
        'Form5.ShowDialog()
        OpenFileDialog1.Title = "Please Select a File"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Filter = "JPEG Image files (*.jpg)|*.jpg|PNG Image files (*.png)|*.png"
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        imagepath = OpenFileDialog1.FileName
        ' MsgBox(OpenFileDialog1.FileName.ToString)
        PictureBox1.Load(imagepath)
        namabaju = Path.GetFileName(imagepath)
        PictureBox1.BackgroundImageLayout = ImageLayout.Stretch
        pilihan = True
    End Sub
End Class