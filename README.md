    # statusLabelProject


```vbnet
Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    If Panel1.Visible Then
        Panel1.Visible = False
        C1FlexGrid1.Top = Panel1.Top ' 上に詰める
        C1FlexGrid1.Height += Panel1.Height ' 高さも拡張する場合
    Else
        Panel1.Visible = True
        C1FlexGrid1.Top = Panel1.Top + Panel1.Height ' 下に移動
        C1FlexGrid1.Height -= Panel1.Height ' 高さを縮める場合
    End If
End Sub
```

```vbnet

Private Sub C1FlexGrid1_DoubleClick(sender As Object, e As EventArgs) Handles C1FlexGrid1.DoubleClick
    ' 1. 選択行・列の取得
    Dim rowIndex As Integer = C1FlexGrid1.Row
    Dim colIndex As Integer = C1FlexGrid1.Col
    Dim originalValue As String = C1FlexGrid1(rowIndex, colIndex).ToString()

    ' 2. 入力画面を表示（モーダルで）
    Dim inputForm As New InputForm()
    inputForm.InputValue = originalValue
    If inputForm.ShowDialog() <> DialogResult.OK Then
        Exit Sub
    End If

    Dim newValue As String = inputForm.InputValue

    ' 3. 値が変更されていない場合は中断
    If originalValue = newValue Then
        MessageBox.Show("値に変更がありません。処理を中止します。")
        Exit Sub
    End If

    ' 4. PLSQL パッケージを呼び出し（OracleCommand使用など）
    Dim conn As New OracleConnection("接続文字列")
    Dim tran As OracleTransaction = Nothing
    Try
        conn.Open()
        tran = conn.BeginTransaction()

        Dim cmd As New OracleCommand("パッケージ名.プロシージャ名", conn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("p_value", OracleDbType.Varchar2).Value = newValue
        cmd.Parameters.Add("p_code", OracleDbType.Int32).Direction = ParameterDirection.Output

        cmd.ExecuteNonQuery()

        Dim resultCode As Integer = Convert.ToInt32(cmd.Parameters("p_code").Value)

        ' 5. 処理コード = 10 の場合は専用画面を表示
        If resultCode = 10 Then
            Dim specialForm As New SpecialForm()
            specialForm.ShowDialog()
            tran.Rollback()
            Exit Sub
        End If

        ' 6. 正常終了 → コミット
        tran.Commit()

        ' 7. 効果音を再生
        My.Computer.Audio.Play("success.wav", AudioPlayMode.Background)

        ' 8. グリッドの選択行を強調
        C1FlexGrid1.Select(rowIndex, colIndex)

        MessageBox.Show("更新が完了しました。")

    Catch ex As Exception
        tran?.Rollback()
        MessageBox.Show("エラーが発生しました：" & ex.Message)
    Finally
        conn.Close()
    End Try
End Sub
```

```vbnet
Public Class InputForm

    ' 入力値を親画面に渡すプロパティ
    Public Property InputValue As String

    Private Sub InputForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 初期値を TextBox に設定
        TextBox1.Text = InputValue
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        ' 入力値をプロパティにセットして終了
        InputValue = TextBox1.Text
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class
```

```vbnet
If resultCode = 10 Then
    Dim errForm As New ErrorForm()
    errForm.ErrorCode = resultCode
    errForm.ErrorMessage = "更新処理でエラーが発生しました。内容を確認してください。"
    errForm.ShowDialog()

    tran.Rollback()
    Exit Sub
End If

Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    ' メッセージボックスフォーム作成
    Dim confirmForm As New ConfirmForm()
    confirmForm.MessageTitle = "終了"
    confirmForm.MessageBody = "確認"

    ' 表示して結果を取得
    If confirmForm.ShowDialog() = DialogResult.OK AndAlso confirmForm.Result = MsgBoxResult.Ok Then
        ' OK の場合：閉じる
    Else
        ' No の場合：キャンセル
        e.Cancel = True
    End If
End Sub




Public Class FormConfirm
    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        Me.DialogResult = DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        Me.DialogResult = DialogResult.No
        Me.Close()
    End Sub
End Class



' チェックボックスの状態が変更されたときのイベント
Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
    If CheckBox1.Checked Then
        CheckBox1.Text = "ON"
    Else
        CheckBox1.Text = "OFF"
    End If
End Sub


If CheckBox1.Checked Then
    ' チェックされている場合の処理
    MessageBox.Show("チェックされています")
Else
    ' チェックされていない場合の処理
    MessageBox.Show("チェックされていません")
End If
```



