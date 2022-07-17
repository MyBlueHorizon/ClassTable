Imports System.Drawing
Imports System.Windows.Forms
Imports ClassTable.AppCore
Public Class LegacySidebarWindow
    '读取配置文件
    Private Event ReadConfigInformation(ByVal sender As Object, ByVal e As EventArgs)
    Private Sub MainWindow_ReadConfigInformation(sender As Object, e As EventArgs) Handles Me.ReadConfigInformation
        '设置窗口大小
        Select Case My.Settings.WindowMode
            Case "Wide"
                Width = 96
                Left = Screen.PrimaryScreen.WorkingArea.Width / (ScreenDPI.dpix / 96) - 96
            Case "Narrow"
                Width = 48
                MorWeekday.Width = 96
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
                Left = Screen.PrimaryScreen.WorkingArea.Width / (ScreenDPI.dpix / 96) - 48
        End Select
        '设置颜色
        Background = AppCore.GetWindowBrush(My.Settings.BackgroundColor_Red, My.Settings.BackgroundColor_Green, My.Settings.BackgroundColor_Blue, My.Settings.BackgroundColor_Alpha)
        MorWeekday.Foreground = GetWindowBrush(My.Settings.ForegroundColor_Red, My.Settings.ForegroundColor_Green, My.Settings.ForegroundColor_Blue, My.Settings.ForegroundColor_Alpha)
    End Sub
    '显示设置窗口
    Private Event ShowSettingWindow(ByVal sender As Object, ByVal e As EventArgs)
    ReadOnly MySettingWindow As New LegacySettingWindow
    Private Sub MainWindow_ShowSettingWindow(sender As Object, e As EventArgs) Handles Me.ShowSettingWindow
        MySettingWindow.ShowDialog()
    End Sub
    '显示更新窗口
    Private Event ShowUpdateWindow(ByVal sender As Object, ByVal e As EventArgs)
    ReadOnly MyUpdateWindow As New LegacyUpdateWindow
    Private Sub MainWindow_ShowUpdateWindow(sender As Object, e As EventArgs) Handles Me.ShowUpdateWindow
        MyUpdateWindow.ShowDialog()
    End Sub
    '显示关于窗口
    ReadOnly MyAboutWindow As New LegacyAboutWindow
    Private Event ShowAboutWindow(ByVal sender As Object, ByVal e As EventArgs)
    Private Sub LegacySidebarWindow_ShowAboutWindow(sender As Object, e As EventArgs) Handles Me.ShowAboutWindow
        MyAboutWindow.ShowDialog()
    End Sub
    ReadOnly ScreenDPI = Graphics.FromHwnd(IntPtr.Zero)
    ReadOnly Apppart = AppDomain.CurrentDomain.BaseDirectory
    Public Shared NowWeekday = Weekday(DateValue(DateString))
    Public Event LoadTable(ByVal sender As Object, ByVal e As EventArgs)
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        RaiseEvent ReadConfigInformation(Me, New EventArgs)
        RaiseEvent LoadTable(Me, New EventArgs)
        RaiseEvent InitializeNotifyIcon(Me, New EventArgs)
        '载入课表
        Dim task = CheckUpdate()
        GC.Collect()
    End Sub
    Private Async Function CheckUpdate() As Task
        Await Task.Run(action:=Sub()
                                   LegacyUpdateWindow.ReleaseInformation = NetworkManager.GetReleaseInformation()
                                   LegacyUpdateWindow.LatestVersion = New Version(NetworkManager.GetLatestVersion(LegacyUpdateWindow.ReleaseInformation))
                               End Sub)
        If LegacyUpdateWindow.LatestVersion > LegacyUpdateWindow.LocalVersion Then
            MyNotifyIcon.BalloonTipIcon = ToolTipIcon.Info
            MyNotifyIcon.BalloonTipText = "发现可用的版本更新，为确保安全性与可用性请及时更新。" + vbLf +
                                       "如果未弹出更新窗口，请 右击/长按通知图标-快捷设置-更新 进入更新页面。"
            MyNotifyIcon.BalloonTipTitle = "发现可用更新"
            MyNotifyIcon.ShowBalloonTip(20)
            RaiseEvent ShowUpdateWindow(Me, New EventArgs)
        End If
    End Function

    ReadOnly WeekSheet = GetChineseFullWeekName(NowWeekday)
    ReadOnly ClassJsonString As String = ExcelManager.GetTableInformation(WeekSheet)
    Private Sub MainWindow_LoadTable(sender As Object, e As EventArgs) Handles Me.LoadTable
        '设置日期
        If My.Settings.Mdate = "No" Then
        Else
            NowWeekday = My.Settings.Mdate
            My.Settings.Mdate = "No"
            My.Settings.Save()
        End If
        '判断文字长度
        Select Case My.Settings.WindowMode
            Case "Wide"
                MorWeekday.Text = AddSpace(GetChineseFullWeekName(NowWeekday), 1, 1)
                '开始载入
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
            Case "Narrow"
                MorWeekday.Width = 48
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
        End Select
        '开始载入
    End Sub
    '调整窗口大小
    Private Sub MainWindow_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseDoubleClick
        If Width = 48 Then
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
            Left = Screen.PrimaryScreen.WorkingArea.Width / (ScreenDPI.dpix / 96) - 96
            MorWeekday.Text = AddSpace(GetChineseFullWeekName(NowWeekday), 1, 1)
        Else
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
            Left = Screen.PrimaryScreen.WorkingArea.Width / (ScreenDPI.dpix / 96) - 48
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
        End If
    End Sub
    '设置/取消窗口置顶
    Private Sub MorWeekday_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles MorWeekday.MouseDown
        If Topmost = True Then
            Topmost = False
            NotifyIconToolStripMenuItemTop.Checked = False
        Else
            Topmost = True
            NotifyIconToolStripMenuItemTop.Checked = True
        End If
    End Sub
    '启动设置窗口
    Private Sub MorWeekday_MouseRightButtonDown(sender As Object, e As MouseButtonEventArgs) Handles MorWeekday.MouseRightButtonDown
        RaiseEvent ShowSettingWindow(Me, New EventArgs)
    End Sub
    Private Sub MainWindow_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        '存储窗口大小设定
        If Width = 48 Then
            My.Settings.WindowMode = "Narrow"
        Else
            My.Settings.WindowMode = "Wide"
        End If
        My.Settings.Save()
    End Sub
    '通知区域
    ReadOnly MyNotifyIcon As New NotifyIcon
    Private Event InitializeNotifyIcon(ByVal sender As Object, ByVal e As EventArgs)
    Dim WithEvents NotifyIconContextMenuStrip As New ContextMenuStrip
    Dim WithEvents NotifyIconToolStripMenuItemQuickly As New ToolStripMenuItem
    Dim WithEvents NotifyIconToolStripMenuItemEdit As New ToolStripMenuItem
    Dim WithEvents NotifyIconToolStripMenuItemUp As New ToolStripMenuItem
    Dim WithEvents NotifyIconToolStripMenuItemAbout As New ToolStripMenuItem
    Dim WithEvents NotifyIconToolStripMenuItemSetting As New ToolStripMenuItem
    Dim WithEvents NotifyIconToolStripMenuItemTop As New ToolStripMenuItem
    Dim WithEvents NotifyIconToolStripMenuItemExit As New ToolStripMenuItem
    Private Sub MainWindow_InitializeNotifyIcon(sender As Object, e As EventArgs) Handles Me.InitializeNotifyIcon

        '初始化通知图标
        MyNotifyIcon.Icon = My.Resources.AppIcon
        MyNotifyIcon.Visible = True
        MyNotifyIcon.Text = "ClassTable - 桌面课表"
        MyNotifyIcon.ContextMenuStrip = NotifyIconContextMenuStrip

        '初始化右键菜单
        NotifyIconToolStripMenuItemQuickly.Text = "快捷设置"
        NotifyIconContextMenuStrip.Items.Add(NotifyIconToolStripMenuItemQuickly)

        NotifyIconToolStripMenuItemSetting.Text = "高级设置"
        NotifyIconToolStripMenuItemQuickly.DropDown.Items.Add(NotifyIconToolStripMenuItemSetting)
        NotifyIconToolStripMenuItemEdit.Text = "编辑"
        NotifyIconToolStripMenuItemQuickly.DropDown.Items.Add(NotifyIconToolStripMenuItemEdit)
        NotifyIconToolStripMenuItemUp.Text = "更新"
        NotifyIconToolStripMenuItemQuickly.DropDown.Items.Add(NotifyIconToolStripMenuItemUp)
        NotifyIconToolStripMenuItemAbout.Text = "关于"
        NotifyIconToolStripMenuItemQuickly.DropDown.Items.Add(NotifyIconToolStripMenuItemAbout)

        NotifyIconToolStripMenuItemTop.Text = "置顶"
        NotifyIconContextMenuStrip.Items.Add(NotifyIconToolStripMenuItemTop)
        NotifyIconToolStripMenuItemTop.Checked = Topmost <> False
        NotifyIconToolStripMenuItemExit.Text = "关闭"
        NotifyIconContextMenuStrip.Items.Add(NotifyIconToolStripMenuItemExit)

    End Sub
    Private Sub NotifyIconToolStripMenuItemSetting_Click(sender As Object, e As EventArgs) Handles NotifyIconToolStripMenuItemSetting.Click
        RaiseEvent ShowSettingWindow(Me, New EventArgs)
    End Sub
    Private Sub NotifyIconToolStripMenuItemEdit_Click(sender As Object, e As EventArgs) Handles NotifyIconToolStripMenuItemEdit.Click
        Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\ATHS Studio\ClassTable\tablefile.xlsx")
    End Sub
    Private Sub NotifyIconToolStripMenuItemUp_Click(sender As Object, e As EventArgs) Handles NotifyIconToolStripMenuItemUp.Click
        RaiseEvent ShowUpdateWindow(Me, New EventArgs)
    End Sub
    Private Sub NotifyIconToolStripMenuItemAbout_Click(sender As Object, e As EventArgs) Handles NotifyIconToolStripMenuItemAbout.Click
        RaiseEvent ShowAboutWindow(Me, New EventArgs)
    End Sub
    Private Sub NotifyIconToolStripMenuItemTop_Click(sender As Object, e As EventArgs) Handles NotifyIconToolStripMenuItemTop.Click
        If NotifyIconToolStripMenuItemTop.Checked = True Then
            NotifyIconToolStripMenuItemTop.Checked = False
            Topmost = False
        Else
            Topmost = True
            NotifyIconToolStripMenuItemTop.Checked = True
        End If
    End Sub
    Private Sub NotifyIconToolStripMenuItemExit_Click(sender As Object, e As EventArgs) Handles NotifyIconToolStripMenuItemExit.Click
        Windows.Application.Current.Shutdown()
    End Sub
End Class
