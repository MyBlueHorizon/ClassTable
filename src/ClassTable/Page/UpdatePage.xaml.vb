Imports System.ComponentModel
Imports System.Net
Imports ClassTable.AppCore
Public Class UpdatePage
    ReadOnly MyWebClient As New Net.WebClient()
    ReadOnly UpdateFilePath = System.Environment.GetEnvironmentVariable("TEMP") + "\ClassTableInstall.msi"
    Public WithEvents DownloadEvents As WebClient = MyWebClient
    ReadOnly BackgroundRedValue = My.Settings.BackgroundColor_Red
    ReadOnly BackgroundBlueValue = My.Settings.BackgroundColor_Blue
    ReadOnly BackgroundGreenValue = My.Settings.BackgroundColor_Green
    ReadOnly BackgroundAlphaValue = My.Settings.BackgroundColor_Alpha
    ReadOnly ForegroundRedValue = My.Settings.ForegroundColor_Red
    ReadOnly ForegroundBlueValue = My.Settings.ForegroundColor_Blue
    ReadOnly ForegroundGreenValue = My.Settings.ForegroundColor_Green
    ReadOnly ForegroundAlphaValue = My.Settings.ForegroundColor_Alpha
    Public Shared ReadOnly LocalVersion As New Version(Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString())
    Public Shared LatestVersion As Version
    Public Shared ReleaseInformation As String
    Private Sub Client_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs) Handles DownloadEvents.DownloadProgressChanged
        Label_DownloadBit.Content = String.Format("{0} 字节 / {1} 字节", e.BytesReceived, e.TotalBytesToReceive)
        ProgressBar_Download.Value = e.ProgressPercentage
        Label_Progress.Content = Str(e.ProgressPercentage) + " %"
    End Sub
    Private Sub Client_DownloadFileCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles DownloadEvents.DownloadFileCompleted
        Process.Start(UpdateFilePath)
        Windows.Application.Current.Shutdown()
    End Sub
    Private Sub UpdateWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Background = GetWindowBrush(BackgroundRedValue, BackgroundGreenValue, BackgroundBlueValue, BackgroundAlphaValue)
        TextBlock_Name.Foreground = GetWindowBrush(ForegroundRedValue, ForegroundGreenValue, ForegroundBlueValue, ForegroundAlphaValue)
        Label_LocalVersion.Content = LocalVersion
        If ReleaseInformation = Nothing Then
            LatestVersion = New Version("0.0.0.0")
            Label_LatestVersion.Content = "尚未检查更新/获取版本失败"
        Else
            Label_LatestVersion.Content = LatestVersion
            If LatestVersion > LocalVersion Then
                Button_Update.Content = "更  新"
            Else
                Button_Update.Content = "刷  新"
            End If
        End If
    End Sub
    Private Sub Button_Update_Click(sender As Object, e As RoutedEventArgs) Handles Button_Update.Click
        If Button_Update.Content = "刷  新" Then
            ReleaseInformation = NetworkManager.GetReleaseInformation
            LatestVersion = New Version(NetworkManager.GetLatestVersion(ReleaseInformation))
            Label_LatestVersion.Content = LatestVersion
            If LatestVersion > LocalVersion Then
                Button_Update.Content = "更  新"
            Else
                Button_Update.Content = "刷  新"
            End If
        Else
            MyWebClient.DownloadFileAsync(New Uri(NetworkManager.GetDownloadUrl(ReleaseInformation)), UpdateFilePath)
            Button_Update.IsEnabled = False
        End If
    End Sub
    Private Sub Button_Back_Click(sender As Object, e As RoutedEventArgs) Handles Button_Back.Click
        LegacySidebarWindow.MyFunctionWindow.HideFunctionWindow()
    End Sub
End Class
