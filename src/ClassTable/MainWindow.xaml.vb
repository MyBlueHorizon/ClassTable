﻿Imports System.Windows.Forms
Imports System.Drawing
Public Class MainWindow
    ReadOnly Core As New ClassTable.Core
    ReadOnly Excel As New ClassTable.Excel
    ReadOnly Network As New ClassTable.Network
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
                Left = Screen.PrimaryScreen.WorkingArea.Width / (ScreenDPI.dpix / 96) - 48
        End Select
        '设置颜色
        Background = Core.GetWindowBrush(My.Settings.BackgroundColor_Red, My.Settings.BackgroundColor_Green, My.Settings.BackgroundColor_Blue, My.Settings.BackgroundColor_Alpha)
        MorWeekday.Foreground = Core.GetWindowBrush(My.Settings.ForegroundColor_Red, My.Settings.ForegroundColor_Green, My.Settings.ForegroundColor_Blue, My.Settings.ForegroundColor_Alpha)
        '设置日期
        If My.Settings.Mdate = "No" Then
        Else
            NowWeekday = My.Settings.Mdate
            My.Settings.Mdate = "No"
            My.Settings.Save()
        End If
    End Sub
    '显示设置窗口
    Private Event ShowSettingWindow(ByVal sender As Object, ByVal e As EventArgs)
    ReadOnly MySettingWindow As New SettingWindow
    Private Sub MainWindow_ShowSettingWindow(sender As Object, e As EventArgs) Handles Me.ShowSettingWindow
        MySettingWindow.ShowDialog()
    End Sub
    '显示更新窗口
    Private Event ShowUpdateWindow(ByVal sender As Object, ByVal e As EventArgs)
    ReadOnly MyUpdateWindow As New UpdateWindow
    Private Sub MainWindow_ShowUpdateWindow(sender As Object, e As EventArgs) Handles Me.ShowUpdateWindow
        MyUpdateWindow.ShowDialog()
    End Sub
    ReadOnly ScreenDPI = Graphics.FromHwnd(IntPtr.Zero)
    ReadOnly Apppart = AppDomain.CurrentDomain.BaseDirectory
    Public Shared NowWeekday = Weekday(DateValue(DateString))
    Public Event LoadTable(ByVal sender As Object, ByVal e As EventArgs)
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        RaiseEvent InitializeNotifyIcon(Me, New EventArgs)
        RaiseEvent ReadConfigInformation(Me, New EventArgs)
        '载入课表
        RaiseEvent LoadTable(Me, New EventArgs)
        UpdateWindow.ReleaseInformation = Network.GetReleaseInformation
        UpdateWindow.LatestVersion = New Version(Network.GetLatestVersion(UpdateWindow.ReleaseInformation))
        If UpdateWindow.LatestVersion > UpdateWindow.LocalVersion Then
            RaiseEvent ShowUpdateWindow(Me, New EventArgs)
        End If
    End Sub
    Private Sub MainWindow_LoadTable(sender As Object, e As EventArgs) Handles Me.LoadTable
        '判断文字长度
        Select Case Width
            Case 96
                MorWeekday.Text = Core.AddSpace(Core.GetChineseFullWeekName(NowWeekday), 1, 1)
            Case 48
                MorWeekday.Text = Core.AddSpace(Core.GetChineseWeekLiteName(NowWeekday), 1, 5)
        End Select
        '开始载入
        Dim WeekSheet = Core.GetChineseFullWeekName(NowWeekday)
        Dim ClassJsonString As String = Excel.GetTableInformation(WeekSheet)
        MorRead.Text = Core.AddSpace(Excel.GetClassName(ClassJsonString, "RangeA"), 1, 1)
        MorA.Text = Core.AddSpace(Excel.GetClassName(ClassJsonString, "RangeB"), 1, 1)
        MorB.Text = Core.AddSpace(Excel.GetClassName(ClassJsonString, "RangeC"), 1, 1)
        MorC.Text = Core.AddSpace(Excel.GetClassName(ClassJsonString, "RangeD"), 1, 1)
        MorD.Text = Core.AddSpace(Excel.GetClassName(ClassJsonString, "RangeE"), 1, 1)
        MorE.Text = Core.AddSpace(Excel.GetClassName(ClassJsonString, "RangeF"), 1, 1)
        MorF.Text = Core.AddSpace(Excel.GetClassName(ClassJsonString, "RangeG"), 1, 1)
        MorG.Text = Core.AddSpace(Excel.GetClassName(ClassJsonString, "RangeH"), 1, 1)
        MorH.Text = Core.AddSpace(Excel.GetClassName(ClassJsonString, "RangeI"), 1, 1)
        MorI.Text = Core.AddSpace(Excel.GetClassName(ClassJsonString, "RangeJ"), 1, 1)
        MorJ.Text = Core.AddSpace(Excel.GetClassName(ClassJsonString, "RangeK"), 1, 1)
        MorStudy.Text = Core.AddSpace(Excel.GetClassName(ClassJsonString, "RangeL"), 1, 1)
    End Sub
    '调整窗口大小
    Private Sub MainWindow_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseDoubleClick
        If Width = 48 Then
            Width = 96
            Left = Screen.PrimaryScreen.WorkingArea.Width / (ScreenDPI.dpix / 96) - 96
            MorWeekday.Text = Core.AddSpace(Core.GetChineseFullWeekName(NowWeekday), 1, 1)
        Else
            Width = 48
            Left = Screen.PrimaryScreen.WorkingArea.Width / (ScreenDPI.dpix / 96) - 48
            MorWeekday.Text = Core.AddSpace(Core.GetChineseWeekLiteName(NowWeekday), 1, 5)
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
    ReadOnly MyNotifyIcon As NotifyIcon = New NotifyIcon
    Private Event InitializeNotifyIcon(ByVal sender As Object, ByVal e As EventArgs)
    Dim WithEvents NotifyIconContextMenuStrip As ContextMenuStrip = New ContextMenuStrip
    Dim WithEvents NotifyIconToolStripMenuItemQuickly As ToolStripMenuItem = New ToolStripMenuItem
    Dim WithEvents NotifyIconToolStripMenuItemEdit As ToolStripMenuItem = New ToolStripMenuItem
    Dim WithEvents NotifyIconToolStripMenuItemUp As ToolStripMenuItem = New ToolStripMenuItem
    Dim WithEvents NotifyIconToolStripMenuItemAbout As ToolStripMenuItem = New ToolStripMenuItem
    Dim WithEvents NotifyIconToolStripMenuItemSetting As ToolStripMenuItem = New ToolStripMenuItem
    Dim WithEvents NotifyIconToolStripMenuItemHide As ToolStripMenuItem = New ToolStripMenuItem
    Dim WithEvents NotifyIconToolStripMenuItemTop As ToolStripMenuItem = New ToolStripMenuItem
    Dim WithEvents NotifyIconToolStripMenuItemExit As ToolStripMenuItem = New ToolStripMenuItem
    Private Sub MainWindow_InitializeNotifyIcon(sender As Object, e As EventArgs) Handles Me.InitializeNotifyIcon

        '初始化通知图标
        MyNotifyIcon.Icon = My.Resources.Icon
        MyNotifyIcon.Visible = True
        MyNotifyIcon.Text = "ClassTable"
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
        'NotifyIconToolStripMenuItemHide.Text = "隐藏"
        'NotifyIconContextMenuStrip.Items.Add(NotifyIconToolStripMenuItemHide)
        NotifyIconToolStripMenuItemExit.Text = "关闭"
        NotifyIconContextMenuStrip.Items.Add(NotifyIconToolStripMenuItemExit)

    End Sub
    Private Sub NotifyIconContextMenuStrip_Click(sender As Object, e As EventArgs) Handles NotifyIconContextMenuStrip.Click
        Show()
        NotifyIconToolStripMenuItemHide.Checked = True
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
        MsgBox("本产品使用的第三方库:" & vbCrLf & "Newtonsoft.Json" & vbCrLf & "ClosedXML" & vbCrLf & "访问 mybluehorizon.github.io/classtable 获得支持" & vbCrLf & "© ATHS Studio 2016 - 2020", Title:="ClassTable 桌面课表", Buttons:=MsgBoxStyle.Information)
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
    'Private Sub NotifyIconToolStripMenuItemHide_Click(sender As Object, e As EventArgs) Handles NotifyIconToolStripMenuItemHide.Click
    '    If NotifyIconToolStripMenuItemHide.Checked = True Then
    '        NotifyIconToolStripMenuItemHide.Checked = False
    '        Show()
    '    Else
    '        Hide()
    '        NotifyIconToolStripMenuItemHide.Checked = True
    '    End If
    'End Sub
    Private Sub NotifyIconToolStripMenuItemExit_Click(sender As Object, e As EventArgs) Handles NotifyIconToolStripMenuItemExit.Click
        System.Windows.Application.Current.Shutdown()
    End Sub
End Class
