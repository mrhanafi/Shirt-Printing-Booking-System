Imports System.Drawing.Drawing2D
Public Class Form4
    Private lastPT As Point
    Private signature As New GraphicsPath

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If Not IsNothing(signature) Then
            If e.Button = Windows.Forms.MouseButtons.Left Then
                lastPT = New Point(e.X, e.Y)
            End If
        End If
    End Sub
    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        If Not IsNothing(signature) Then
            If e.Button = Windows.Forms.MouseButtons.Left Then
                Dim curPt As New Point(e.X, e.Y)
                signature.AddLine(lastPT, curPt)
                lastPT = curPt
                PictureBox1.Refresh()
            End If
        End If
    End Sub
    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        If Not IsNothing(signature) Then
            If e.Button = Windows.Forms.MouseButtons.Left Then
                signature.StartFigure()
            End If
        End If
    End Sub
    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        If Not IsNothing(signature) Then
            e.Graphics.DrawPath(Pens.Black, signature)
        End If
    End Sub

    Private Sub MetroButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MetroButton7.Click
        signature.Reset()
        PictureBox1.Refresh()
    End Sub

    Private Sub MetroButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MetroButton8.Click
        If TextBox1.Text = "" Then
            MsgBox("Sila Isi Nama Pada Ruangan Yang Disediakan")
        Else
            If Form2.duekali = False Then

                Dim bmp1 As New System.Drawing.Bitmap(PictureBox1.Width, PictureBox1.Height)
                ' Dim bmp2 As New System.Drawing.Bitmap(PictureBox2.Width, PictureBox2.Height)
                PictureBox1.DrawToBitmap(bmp1, PictureBox1.ClientRectangle)
                ' PictureBox2.DrawToBitmap(bmp2, PictureBox2.ClientRectangle)
                ' bmp.Save(System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, TextBox1.Text + ".bmp"), System.Drawing.Imaging.ImageFormat.Bmp)
                ' bmp2.Save(System.IO.Path.Combine(Form2.savepath, TextBox1.Text + "-back.jpeg"), System.Drawing.Imaging.ImageFormat.Jpeg)
                If RadioButton1.Checked = True Then
                    bmp1.Save(System.IO.Path.Combine(Form2.savepath, TextBox1.Text + "-front.jpeg"), System.Drawing.Imaging.ImageFormat.Jpeg)
                    'Form2.namabaju = "front.jpeg"
                ElseIf RadioButton2.Checked = True Then
                    bmp1.Save(System.IO.Path.Combine(Form2.savepath, TextBox1.Text + "-back.jpeg"), System.Drawing.Imaging.ImageFormat.Jpeg)
                    ' Form2.namabaju = "back.jpeg"
                End If

                Form2.PictureBox1.Image = bmp1
                ' Form2.PictureBox2.Image = bmp2
                Form2.duekali = True
            Else
                Form2.PictureBox2.Visible = True
                Dim bmp1 As New System.Drawing.Bitmap(PictureBox1.Width, PictureBox1.Height)
                ' Dim bmp2 As New System.Drawing.Bitmap(PictureBox2.Width, PictureBox2.Height)
                PictureBox1.DrawToBitmap(bmp1, PictureBox1.ClientRectangle)
                ' PictureBox2.DrawToBitmap(bmp2, PictureBox2.ClientRectangle)
                ' bmp.Save(System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, TextBox1.Text + ".bmp"), System.Drawing.Imaging.ImageFormat.Bmp)
                ' bmp1.Save(System.IO.Path.Combine(Form2.savepath, TextBox1.Text + "-front.jpeg"), System.Drawing.Imaging.ImageFormat.Jpeg)
                ' bmp2.Save(System.IO.Path.Combine(Form2.savepath, TextBox1.Text + "-back.jpeg"), System.Drawing.Imaging.ImageFormat.Jpeg)
                If RadioButton1.Checked = True Then
                    bmp1.Save(System.IO.Path.Combine(Form2.savepath, TextBox1.Text + "-front.jpeg"), System.Drawing.Imaging.ImageFormat.Jpeg)
                    ' Form2.namabaju = "front.jpeg"
                ElseIf RadioButton2.Checked = True Then
                    bmp1.Save(System.IO.Path.Combine(Form2.savepath, TextBox1.Text + "-back.jpeg"), System.Drawing.Imaging.ImageFormat.Jpeg)
                    ' Form2.namabaju = "back.jpeg"
                End If
                Form2.PictureBox2.Image = bmp1
                ' Form2.PictureBox2.Image = bmp2
            End If



            Me.Close()
        End If
        
    End Sub
    Private Sub PictureBox2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If Not IsNothing(signature) Then
            If e.Button = Windows.Forms.MouseButtons.Left Then
                lastPT = New Point(e.X, e.Y)
            End If
        End If
    End Sub
   
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        PictureBox1.Image = My.Resources.front
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        PictureBox1.Image = My.Resources.back
    End Sub

    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RadioButton1.Checked = True
    End Sub
End Class