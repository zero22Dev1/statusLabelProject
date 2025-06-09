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
    Dim rowIndex As Integer = C1FlexGrid1.Row
    Dim colIndex As Integer = C1FlexGrid1.Col

    ' ヘッダ行は無視（行0がヘッダの場合）
    If rowIndex < C1FlexGrid1.Rows.Fixed Then Exit Sub

    ' たとえば、「商品コード列」が3列目（インデックス2）だった場合のみ処理
    If colIndex = 2 Then
        Dim cellValue As String = C1FlexGrid1(rowIndex, colIndex).ToString()

        ' 子フォームに値を渡して表示
        Dim detailForm As New FormDetail(cellValue)
        detailForm.ShowDialog()
    End If
End Sub

Public Class FormDetail
    Private itemCode As String

    ' コンストラクタで受け取る
    Public Sub New(code As String)
        InitializeComponent()
        itemCode = code
    End Sub

    Private Sub FormDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = "商品コード: " & itemCode
        ' 他、必要であればデータベース検索など
    End Sub
End Class

```



