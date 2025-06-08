Imports System.Data.SqlClient

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 初期設定
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = 100
        ProgressBar1.Value = 0
        Label1.Text = "0%"
    End Sub

    ' ボタンを押したらDBから値を取得して表示
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Dim progressValue As Integer = GetProgressFromDatabase()

        ' 安全に範囲内で設定
        If progressValue >= ProgressBar1.Minimum AndAlso progressValue <= ProgressBar1.Maximum Then
            SetProgress(progressValue)
        Else
            MessageBox.Show("取得した値が範囲外です。")
        End If
    End Sub

    ' 進捗バーとラベルを更新する関数
    Private Sub SetProgress(value As Integer)
        ProgressBar1.Value = value
        Label1.Text = value.ToString() & "%"
    End Sub

End Class
