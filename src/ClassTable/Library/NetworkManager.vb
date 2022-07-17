Imports System.IO
Imports System.Net
Imports Newtonsoft.Json
Public Class NetworkManager

    '网络与更新部分

    '发送GET请求
    Public Shared Function GetHttpResponse(ByVal url As String, ByVal Timeout As Integer) As String
        Try
            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "GET"
            request.ContentType = "application/json;charset=UTF-8"
            request.UserAgent = “Mozilla/5.0”
            request.Timeout = Timeout
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim myResponseStream As Stream = response.GetResponseStream()
            Dim myStreamReader As New StreamReader(myResponseStream, Text.Encoding.GetEncoding("utf-8"))
            Dim retString As String = myStreamReader.ReadToEnd()
            myStreamReader.Close()
            myResponseStream.Close()
            Return retString
        Catch Ex As Exception
            Return "Error"
        End Try
    End Function

    '获取更新信息
    Public Shared Function GetReleaseInformation() As String
        Dim JsonString
        Try
            JsonString = GetHttpResponse("https://api.github.com/repos/mybluehorizon/classtable/releases/latest", 10000)
            Return JsonString
        Catch Ex As Exception
            Return "Error"
        End Try
    End Function

    '解析更新版本
    Public Function GetLatestVersion(ByVal ReleaseInformation)
        Dim LatestVersion
        Try
            LatestVersion = JsonConvert.DeserializeObject(ReleaseInformation)
            Dim sIndex As String = LatestVersion("tag_name")
            If sIndex.StartsWith("v") Then
                Return sIndex.Remove(0, 1)
            Else
                Return sIndex
            End If
        Catch Ex As Exception
            Return "0.0.0"
        End Try
    End Function

    '解析下载链接
    Public Function GetDownloadUrl(ByVal ReleaseInformation)
        Dim DownloadUrl
        Try
            DownloadUrl = JsonConvert.DeserializeObject(ReleaseInformation)
            Dim sIndex As String = DownloadUrl("assets")(0)("browser_download_url")
            Return sIndex
        Catch Ex As Exception
            Return "Error"
        End Try
    End Function
End Class
