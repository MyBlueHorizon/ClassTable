Imports System.ComponentModel

Public Class UpdateWindow
    ReadOnly Network As New ClassTable.Network
    ReadOnly UpdateFilePath = System.Environment.GetEnvironmentVariable("TEMP") + "\ClassTable\classtable.msi"
    Public Shared ReadOnly LocalVersion As Version = New Version(Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString())
    Public Shared LatestVersion As Version
    Public Shared ReleaseInformation As String
    Private Sub UpdateWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Label_LocalVersion.Content = LocalVersion
        If ReleaseInformation = Nothing Then
            LatestVersion = New Version("0.0.0")
            Label_LatestVersion.Content = "0.0.0"
        Else
            Label_LatestVersion.Content = LatestVersion
            If LatestVersion > LocalVersion Then
                Button_Update.Content = "更新"
            Else
                Button_Update.Content = "刷新"
            End If
        End If
    End Sub
    Private Sub Button_Update_Click(sender As Object, e As RoutedEventArgs) Handles Button_Update.Click
        If Button_Update.Content = "刷新" Then
            ReleaseInformation = Network.GetReleaseInformation
            LatestVersion = New Version(Network.GetLatestVersion(ReleaseInformation))
            Label_LatestVersion.Content = LatestVersion
            If LatestVersion > LocalVersion Then
                Button_Update.Content = "更新"
            Else
                Button_Update.Content = "刷新"
            End If
        Else
            ProgressBar_Download.Value = 50
            If Network.HttpDownload(Network.GetDownloadUrl(ReleaseInformation), UpdateFilePath) = True Then
                ProgressBar_Download.Value = 100
                Process.Start(UpdateFilePath)
                System.Windows.Application.Current.Shutdown()
            Else
                MsgBox("下载更新失败，请重试")
            End If
            ProgressBar_Download.Value = 0
        End If
    End Sub
    Private Sub UpdateWindow_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Hide()
        e.Cancel = True
    End Sub
End Class
