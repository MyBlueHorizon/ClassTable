Imports System.Drawing
Imports ClassTable.AppCore
Imports Hardcodet.Wpf.TaskbarNotification

Public Class MainWindow
    Private Property WindowMode As String = My.Settings.WindowMode
    ReadOnly ScreenDPI = Graphics.FromHwnd(IntPtr.Zero)
    ReadOnly Apppart = AppDomain.CurrentDomain.BaseDirectory
    Public Shared NowWeekday = Weekday(DateValue(DateString))
    '读取配置文件
    Private Sub ConfigFormSetting()
        If My.Settings.Mdate = "No" Then
        Else
            NowWeekday = My.Settings.Mdate
            My.Settings.Mdate = "No"
            My.Settings.Save()
        End If
        '设置窗口大小
        SetWindowMode()
        '设置颜色
        SetWindowColor()
        MenuItem_Topmost.IsChecked = Topmost
    End Sub
    '显示设置窗口
    Private Event ShowSettingWindow(ByVal sender As Object, ByVal e As EventArgs)
    Public Shared ReadOnly MyFunctionWindow As New FunctionWindow
    Private Sub MainWindow_ShowSettingWindow(sender As Object, e As EventArgs) Handles Me.ShowSettingWindow
        MyFunctionWindow.ChangeToSetting()
        If MyFunctionWindow.IsVisible = False Then
            MyFunctionWindow.ShowDialog()
        End If
    End Sub
    '显示更新窗口
    Private Event ShowUpdateWindow(ByVal sender As Object, ByVal e As EventArgs)
    Private Sub MainWindow_ShowUpdateWindow(sender As Object, e As EventArgs) Handles Me.ShowUpdateWindow
        MyFunctionWindow.ChangeToUpdate()
        If MyFunctionWindow.IsVisible = False Then
            MyFunctionWindow.ShowDialog()
        End If
    End Sub
    '显示关于窗口
    Private Event ShowAboutWindow(ByVal sender As Object, ByVal e As EventArgs)
    Private Sub LegacySidebarWindow_ShowAboutWindow(sender As Object, e As EventArgs) Handles Me.ShowAboutWindow
        MyFunctionWindow.ChangeToAbout()
        If MyFunctionWindow.IsVisible = False Then
            MyFunctionWindow.ShowDialog()
        End If
    End Sub
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ConfigFormSetting()
        SetClassBlock()
        '载入课表
        Dim task = CheckUpdate()
        GC.Collect()
    End Sub
    Private Async Function CheckUpdate() As Task
        Await Task.Run(action:=Sub()
                                   UpdatePage.ReleaseInformation = NetworkManager.GetReleaseInformation()
                                   UpdatePage.LatestVersion = New Version(NetworkManager.GetLatestVersion(UpdatePage.ReleaseInformation))
                               End Sub)
        If UpdatePage.LatestVersion > UpdatePage.LocalVersion Then
            MyTaskbarIcon.ShowBalloonTip("发现可用更新", "发现可用的版本更新，为确保安全性与可用性请及时更新。" + vbLf +
                                         "如果未弹出更新窗口，请 右击/长按通知图标-快捷设置-更新 进入更新页面。", BalloonIcon.Info)
            RaiseEvent ShowUpdateWindow(Me, New EventArgs)
        End If
    End Function
    ReadOnly WeekSheet = GetChineseFullWeekName(NowWeekday)
    ReadOnly ClassJsonString As String = ExcelManager.GetTableInformation(WeekSheet)
    '调整窗口大小
    Private Sub MainWindow_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseDoubleClick
        Select Case WindowMode
            Case "Wide"
                WindowMode = "Narrow"
            Case "Narrow"
                WindowMode = "Wide"
            Case Else
                Exit Select
        End Select
        SetWindowMode()
        SetClassBlock()
    End Sub
    '设置/取消窗口置顶
    Private Sub MorWeekday_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles MorWeekday.MouseDown
        If Topmost = True Then
            Topmost = False
        Else
            Topmost = False
        End If
        MenuItem_Topmost.IsChecked = Topmost
    End Sub
    '启动设置窗口
    Private Sub MorWeekday_MouseRightButtonDown(sender As Object, e As MouseButtonEventArgs) Handles MorWeekday.MouseRightButtonDown
        RaiseEvent ShowSettingWindow(Me, New EventArgs)
    End Sub
    Private Sub MainWindow_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        '存储窗口大小设定
        Select Case WindowMode
            Case "Narrow"
                My.Settings.WindowMode = "Narrow"
            Case "Wide"
                My.Settings.WindowMode = "Wide"
            Case Else
                Exit Select
        End Select
        My.Settings.Save()
    End Sub
    '通知区域
    Private Sub EditTableFile_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\ATHS Studio\ClassTable\tablefile.xlsx")
    End Sub
    Private Sub ShowSetting_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        RaiseEvent ShowSettingWindow(Me, New EventArgs)
    End Sub
    Private Sub ShowUpdate_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        RaiseEvent ShowUpdateWindow(Me, New EventArgs)
    End Sub
    Private Sub ShowAbout_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        RaiseEvent ShowAboutWindow(Me, New EventArgs)
    End Sub
    Private Sub WindowTopmost_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Topmost = True
    End Sub
    Private Sub WindowTopmost_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Topmost = False
    End Sub
    Private Sub AppExit_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Windows.Application.Current.Shutdown()
    End Sub
    '窗口模式与颜色
    Private Sub SetWindowMode()
        Select Case WindowMode
            Case "Wide"
                Width = 96
                MorWeekday.Width = 96
                MorRead.Width = 96
                MorA.Width = 96
                MorB.Width = 96
                MorC.Width = 96
                MorD.Width = 96
                MorE.Width = 96
                MorF.Width = 96
                MorG.Width = 96
                MorH.Width = 96
                MorI.Width = 96
                MorJ.Width = 96
                MorStudy.Width = 96
                Left = Forms.Screen.PrimaryScreen.WorkingArea.Width / (ScreenDPI.dpix / 96) - 96
            Case "Narrow"
                Width = 48
                MorWeekday.Width = 48
                MorRead.Width = 48
                MorA.Width = 48
                MorB.Width = 48
                MorC.Width = 48
                MorD.Width = 48
                MorE.Width = 48
                MorF.Width = 48
                MorG.Width = 48
                MorH.Width = 48
                MorI.Width = 48
                MorJ.Width = 48
                MorStudy.Width = 48
                Left = Forms.Screen.PrimaryScreen.WorkingArea.Width / (ScreenDPI.dpix / 96) - 48
        End Select
    End Sub
    Private Sub SetWindowColor()
        Background = GetWindowBrush(My.Settings.BackgroundColor_Red, My.Settings.BackgroundColor_Green, My.Settings.BackgroundColor_Blue, My.Settings.BackgroundColor_Alpha)
        MorWeekday.Foreground = GetWindowBrush(My.Settings.ForegroundColor_Red, My.Settings.ForegroundColor_Green, My.Settings.ForegroundColor_Blue, My.Settings.ForegroundColor_Alpha)
    End Sub
    Private Sub SetClassBlock()
        Select Case WindowMode
            Case "Wide"
                MorRead.Text = AddSpace(ExcelManager.GetClassName(ClassJsonString, "RangeA"), 1, 1)
                MorA.Text = AddSpace(ExcelManager.GetClassName(ClassJsonString, "RangeB"), 1, 1)
                MorB.Text = AddSpace(ExcelManager.GetClassName(ClassJsonString, "RangeC"), 1, 1)
                MorC.Text = AddSpace(ExcelManager.GetClassName(ClassJsonString, "RangeD"), 1, 1)
                MorD.Text = AddSpace(ExcelManager.GetClassName(ClassJsonString, "RangeE"), 1, 1)
                MorE.Text = AddSpace(ExcelManager.GetClassName(ClassJsonString, "RangeF"), 1, 1)
                MorF.Text = AddSpace(ExcelManager.GetClassName(ClassJsonString, "RangeG"), 1, 1)
                MorG.Text = AddSpace(ExcelManager.GetClassName(ClassJsonString, "RangeH"), 1, 1)
                MorH.Text = AddSpace(ExcelManager.GetClassName(ClassJsonString, "RangeI"), 1, 1)
                MorI.Text = AddSpace(ExcelManager.GetClassName(ClassJsonString, "RangeJ"), 1, 1)
                MorJ.Text = AddSpace(ExcelManager.GetClassName(ClassJsonString, "RangeK"), 1, 1)
                MorStudy.Text = AddSpace(ExcelManager.GetClassName(ClassJsonString, "RangeL"), 1, 1)
                MorWeekday.Text = AddSpace(GetChineseFullWeekName(NowWeekday), 1, 1)
            Case "Narrow"
                MorWeekday.Text = GetChineseWeekLiteName(NowWeekday)
                MorRead.Text = OneChar(ExcelManager.GetClassName(ClassJsonString, "RangeA"))
                MorA.Text = OneChar(ExcelManager.GetClassName(ClassJsonString, "RangeB"))
                MorB.Text = OneChar(ExcelManager.GetClassName(ClassJsonString, "RangeC"))
                MorC.Text = OneChar(ExcelManager.GetClassName(ClassJsonString, "RangeD")）
                MorD.Text = OneChar(ExcelManager.GetClassName(ClassJsonString, "RangeE")）
                MorE.Text = OneChar(ExcelManager.GetClassName(ClassJsonString, "RangeF")）
                MorF.Text = OneChar(ExcelManager.GetClassName(ClassJsonString, "RangeG")）
                MorG.Text = OneChar(ExcelManager.GetClassName(ClassJsonString, "RangeH")）
                MorH.Text = OneChar(ExcelManager.GetClassName(ClassJsonString, "RangeI")）
                MorI.Text = OneChar(ExcelManager.GetClassName(ClassJsonString, "RangeJ")）
                MorJ.Text = OneChar(ExcelManager.GetClassName(ClassJsonString, "RangeK")）
                MorStudy.Text = OneChar(ExcelManager.GetClassName(ClassJsonString, "RangeL")）
            Case Else
                Exit Select
        End Select
    End Sub
End Class