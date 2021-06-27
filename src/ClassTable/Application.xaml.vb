Class Application

    ' 应用程序级事件(例如 Startup、Exit 和 DispatcherUnhandledException)
    ' 可以在此文件中进行处理。

    '启用Aero2样式
    Private Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        Dim Uri = New Uri("/PresentationFramework.Aero2, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35;component/themes/aero2.normalcolor.xaml", UriKind.Relative)
        Application.Current.Resources.Source = Uri
    End Sub

End Class
