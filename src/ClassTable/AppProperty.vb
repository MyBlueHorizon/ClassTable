Public Class AppProperty
    '应用版本
    Public Property AppVersion As Version = New Version(Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString())
    Public Property AppVersionName As String = My.Settings.AppVersionName
    Public Property AppVersionString As String = AppVersion.ToString + " （" + AppVersionName + "）"
    '颜色相关
    Public Property AppForegroundColorBrush As Brush = AppCore.GetWindowBrush(My.Settings.ForegroundColor_Red, My.Settings.ForegroundColor_Green, My.Settings.ForegroundColor_Blue, My.Settings.ForegroundColor_Alpha)
    Public Property AppBackgroundColorBrush As Brush = AppCore.GetWindowBrush(My.Settings.BackgroundColor_Red, My.Settings.BackgroundColor_Green, My.Settings.BackgroundColor_Blue, My.Settings.BackgroundColor_Alpha)
End Class
