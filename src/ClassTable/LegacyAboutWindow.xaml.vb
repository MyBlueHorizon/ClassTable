Imports System.ComponentModel
Public Class LegacyAboutWindow
    Private Sub Button_Back_Click(sender As Object, e As RoutedEventArgs) Handles Button_Back.Click
        Hide()
    End Sub
    Private Sub LegacyAboutWindow_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseLeftButtonDown
        DragMove()
    End Sub
    Private Sub LegacyAboutWindow_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        Hide()
    End Sub
End Class
