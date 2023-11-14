Imports System.Data.Odbc
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1
    Dim Conn As OdbcConnection
    Dim cmd As OdbcCommand
    Dim Ds As DataSet
    Dim Da As OdbcDataAdapter
    Dim Rd As OdbcDataReader
    Dim MyDB As String

    Sub Connection()
        MyDB = "Driver={MySQL ODBC 8.1 ANSI Driver};Database=apotekerdb;server=localhost;uid=root"
        Conn = New OdbcConnection(MyDB)
        If Conn.State = ConnectionState.Closed Then Conn.Open()
    End Sub

    Sub KondisiAwal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""

        ComboBox1.Text = ""

        Call Connection()
        Da = New OdbcDataAdapter("select * from apoteker", Conn)
        Ds = New DataSet
        Da.Fill(Ds, "apoteker")
        DataGridView1.DataSource = Ds.Tables("apoteker")
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call Connection()
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or DateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") = "" Or ComboBox1.Text = "" Then
            MessageBox.Show("Silahkan Mengisi data Secara Lengkap")
        Else
            Dim InputData As String = " Insert into apoteker value ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "','" & TextBox3.Text & "','" & DateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") & "')"
            cmd = New OdbcCommand(InputData, Conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Input Data Berhasil")
            Call KondisiAwal()
        End If
    End Sub
    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call Connection()
            cmd = New OdbcCommand("select * from apoteker where KodeObat= '" & TextBox1.Text & "'", Conn)

            Rd = cmd.ExecuteReader
            Rd.Read()
            If Rd.HasRows Then
                TextBox2.Text = Rd.Item("NamaObat")
                ComboBox1.Text = Rd.Item("JenisObat")
                TextBox3.Text = Rd.Item("Dosis")
                DateTimePicker1.Value = Rd.Item("ExpireDate")
            Else
                MsgBox("Data tidak ditemukan")
            End If
        End If
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        End
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call Connection()
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or DateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") = "" Or ComboBox1.Text = "" Then
            MessageBox.Show("Silahkan Mengisi data Secara Lengkap")
        Else
            Dim DeleteData As String = "delete from apoteker where KodeObat='" & TextBox1.Text & "'"
            cmd = New OdbcCommand(DeleteData, Conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Hapus Data Berhasil")
            Call KondisiAwal()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or DateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") = "" Or ComboBox1.Text = "" Then
            MessageBox.Show("Silahkan Mengisi data Secara Lengkap")
        Else
            Call Connection()
            Dim EditData As String = "update apoteker Set NamaObat='" & TextBox2.Text & "',JenisObat='" & ComboBox1.Text & "',Dosis='" & TextBox3.Text & "',ExpireDate='" & DateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") & "' where KodeObat='" & TextBox1.Text & "'"
            cmd = New OdbcCommand(EditData, Conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Edit Data Berhasil")
            Call KondisiAwal()
        End If

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub
End Class
