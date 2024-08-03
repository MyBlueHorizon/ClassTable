Public Class FunctionWindow
    Public Sub ChangeToSetting()
        MainFrame.Navigate(New Uri("Page/SettingPage.xaml", UriKind.Relative))
    End Sub
    Public Sub ChangeToAbout()
        MainFrame.Navigate(New Uri("Page/AboutPage.xaml", UriKind.Relative))
    End Sub
    Public Sub ChangeToUpdate()
        MainFrame.Navigate(New Uri("Page/UpdatePage.xaml", UriKind.Relative))
    End Sub
    Public Sub HideFunctionWindow()
        Hide()
    End Sub
    Private Sub FunctionWindow_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles Me.Closing
        Hide()
        e.Cancel = True
    End Sub
    Private Sub LegacySettingWindow_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseLeftButtonDown
        DragMove()
    End Sub
End Class
