```vbnet
For col As Integer = C1FlexGrid1.Cols.Fixed To C1FlexGrid1.Cols.Count - 1
    If C1FlexGrid1(0, col).ToString() = "カテゴリ" Then
        Dim style = C1FlexGrid1.Styles.Add("HeaderCat")
        style.BackColor = Color.Yellow
        C1FlexGrid1.SetCellStyle(0, col, style)
    End If

    If C1FlexGrid1(1, col).ToString() = "商品名" Then
        Dim style = C1FlexGrid1.Styles.Add("HeaderItem")
        style.BackColor = Color.Pink
        C1FlexGrid1.SetCellStyle(1, col, style)
    End If
Next
```