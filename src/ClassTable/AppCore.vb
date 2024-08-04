Public Class AppCore

    '字符串转换
    '日期单字转换
    Public Shared Function GetChineseWeekLiteName(ByVal NowWeekday)
        Dim ChineseWeekName = "期"
        Select Case NowWeekday
            Case 1
                ChineseWeekName = "日"
            Case 2
                ChineseWeekName = "一"
            Case 3
                ChineseWeekName = "二"
            Case 4
                ChineseWeekName = "三"
            Case 5
                ChineseWeekName = "四"
            Case 6
                ChineseWeekName = "五"
            Case 7
                ChineseWeekName = "六"
        End Select
        Return ChineseWeekName
    End Function

    '日期双字转换
    Public Shared Function GetChineseFullWeekName(ByVal NowWeekday)
        Dim ChineseWeekName
        ChineseWeekName = WeekdayName(NowWeekday, FirstDayOfWeekValue:=FirstDayOfWeek.Sunday, Abbreviate:=True)
        Return ChineseWeekName
    End Function
    Public Shared Function OneChar(ByVal OriginStr)
        Dim ReturnString
        If OriginStr = "" Then
            ReturnString = ""
        Else
            ReturnString = OriginStr.ToString.Chars(0)
        End If
        Return ReturnString
    End Function

    '空格添加
    Public Shared Function AddSpace(ByVal OriginStr, ByVal SpaceS, ByVal SpaceN)
        Dim ReturnString
        Dim SpaceString
        If OriginStr = "" Then
            ReturnString = ""
        Else
            SpaceString = Space（SpaceN）
            ReturnString = OriginStr.InSert(SpaceS, SpaceString)
        End If
        Return ReturnString
    End Function

    '程序UI
    '获取窗口笔刷
    Public Shared Function GetWindowBrush(ByVal R As String, ByVal G As String, ByVal B As String, ByVal A As String)
        Dim WindowBrush As Brush = New SolidColorBrush(Color.FromArgb(A, R, G, B))
        Return WindowBrush
    End Function
End Class