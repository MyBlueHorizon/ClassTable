Imports System.ComponentModel
Public Class LegacySettingWindow
    ReadOnly Core As New LegacyCore
    Dim BackgroundRedValue = My.Settings.BackgroundColor_Red
    Dim BackgroundBlueValue = My.Settings.BackgroundColor_Blue
    Dim BackgroundGreenValue = My.Settings.BackgroundColor_Green
    Dim BackgroundAlphaValue = My.Settings.BackgroundColor_Alpha
    Dim ForegroundRedValue = My.Settings.ForegroundColor_Red
    Dim ForegroundBlueValue = My.Settings.ForegroundColor_Blue
    Dim ForegroundGreenValue = My.Settings.ForegroundColor_Green
    Dim ForegroundAlphaValue = My.Settings.ForegroundColor_Alpha
    Dim SetDate = LegacySidebarWindow.NowWeekday
    Private Sub Slider_AlphaBar_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double)) Handles Slider_AlphaBar.ValueChanged
        If Radio_Back.IsChecked = True Then
            BackgroundAlphaValue = Slider_AlphaBar.Value
            Background = Core.GetWindowBrush(BackgroundRedValue, BackgroundGreenValue, BackgroundBlueValue, BackgroundAlphaValue)
        Else
            ForegroundAlphaValue = Slider_AlphaBar.Value
            TextBlock_Weekday.Foreground = Core.GetWindowBrush(ForegroundRedValue, ForegroundGreenValue, ForegroundBlueValue, ForegroundAlphaValue)
        End If
    End Sub
    Private Sub Slider_RedBar_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double)) Handles Slider_RedBar.ValueChanged
        If Radio_Back.IsChecked = True Then
            BackgroundRedValue = Slider_RedBar.Value
            Background = Core.GetWindowBrush(BackgroundRedValue, BackgroundGreenValue, BackgroundBlueValue, BackgroundAlphaValue)
        Else
            ForegroundRedValue = Slider_RedBar.Value
            TextBlock_Weekday.Foreground = Core.GetWindowBrush(ForegroundRedValue, ForegroundGreenValue, ForegroundBlueValue, ForegroundAlphaValue)
        End If
    End Sub
    Private Sub Slider_GreenBar_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double)) Handles Slider_GreenBar.ValueChanged
        If Radio_Back.IsChecked = True Then
            BackgroundGreenValue = Slider_GreenBar.Value
            Background = Core.GetWindowBrush(BackgroundRedValue, BackgroundGreenValue, BackgroundBlueValue, BackgroundAlphaValue)
        Else
            ForegroundGreenValue = Slider_GreenBar.Value
            TextBlock_Weekday.Foreground = Core.GetWindowBrush(ForegroundRedValue, ForegroundGreenValue, ForegroundBlueValue, ForegroundAlphaValue)
        End If
    End Sub
    Private Sub Slider_BlueBar_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double)) Handles Slider_BlueBar.ValueChanged
        If Radio_Back.IsChecked = True Then
            BackgroundBlueValue = Slider_BlueBar.Value
            Background = Core.GetWindowBrush(BackgroundRedValue, BackgroundGreenValue, BackgroundBlueValue, BackgroundAlphaValue)
        Else
            ForegroundBlueValue = Slider_BlueBar.Value
            TextBlock_Weekday.Foreground = Core.GetWindowBrush(ForegroundRedValue, ForegroundGreenValue, ForegroundBlueValue, ForegroundAlphaValue)
        End If
    End Sub
    Private Sub Button_Edit_Click(sender As Object, e As RoutedEventArgs) Handles Button_Edit.Click
        Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\ATHS Studio\ClassTable\tablefile.xlsx")
    End Sub
    Private Sub SettingWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Slider_AlphaBar.Value = BackgroundAlphaValue
        Slider_RedBar.Value = BackgroundRedValue
        Slider_GreenBar.Value = BackgroundGreenValue
        Slider_BlueBar.Value = BackgroundBlueValue
        Background = Core.GetWindowBrush(BackgroundRedValue, BackgroundGreenValue, BackgroundBlueValue, BackgroundAlphaValue)
        TextBlock_Weekday.Foreground = Core.GetWindowBrush(ForegroundRedValue, ForegroundGreenValue, ForegroundBlueValue, ForegroundAlphaValue)
        TextBlock_Weekday.Text = Core.GetChineseWeekLiteName(SetDate)
    End Sub
    Private Sub Button_Back_Click(sender As Object, e As RoutedEventArgs) Handles Button_Back.Click
        Hide()
    End Sub
    Private Sub ChangeData_Click(sender As Object, e As RoutedEventArgs) Handles ChangeData.Click
        If SetDate = 7 Then
            SetDate = 1
        Else
            SetDate += 1
        End If
        TextBlock_Weekday.Text = Core.GetChineseWeekLiteName(SetDate)
    End Sub
    Private Sub Button_Save_Click(sender As Object, e As RoutedEventArgs) Handles Button_Save.Click
        My.Settings.BackgroundColor_Red = BackgroundRedValue
        My.Settings.BackgroundColor_Green = BackgroundGreenValue
        My.Settings.BackgroundColor_Blue = BackgroundBlueValue
        My.Settings.BackgroundColor_Alpha = BackgroundAlphaValue
        My.Settings.ForegroundColor_Red = ForegroundRedValue
        My.Settings.ForegroundColor_Green = ForegroundGreenValue
        My.Settings.ForegroundColor_Blue = ForegroundBlueValue
        My.Settings.ForegroundColor_Alpha = ForegroundAlphaValue
        My.Settings.Mdate = SetDate
        My.Settings.Save()
        Process.Start(Application.ResourceAssembly.Location)
        Application.Current.Shutdown()
    End Sub
    Private Sub Radio_Back_Checked(sender As Object, e As RoutedEventArgs) Handles Radio_Back.Checked
        Slider_AlphaBar.Value = BackgroundAlphaValue
        Slider_RedBar.Value = BackgroundRedValue
        Slider_GreenBar.Value = BackgroundGreenValue
        Slider_BlueBar.Value = BackgroundBlueValue
    End Sub
    Private Sub Radio_Foce_Checked(sender As Object, e As RoutedEventArgs) Handles Radio_Foce.Checked
        Slider_AlphaBar.Value = ForegroundAlphaValue
        Slider_RedBar.Value = ForegroundRedValue
        Slider_GreenBar.Value = ForegroundGreenValue
        Slider_BlueBar.Value = ForegroundBlueValue
    End Sub
    Private Sub SettingWindow_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Hide()
        e.Cancel = True
    End Sub
    Private Sub LegacySettingWindow_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseLeftButtonDown
        DragMove()
    End Sub
End Class
