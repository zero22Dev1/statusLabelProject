# statusLabelProject


'''vb.net
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
'''


