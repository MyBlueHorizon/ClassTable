Imports System.IO
Imports System.Net
Imports Newtonsoft.Json
Public Class Network

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
            Dim myStreamReader As StreamReader = New StreamReader(myResponseStream, Text.Encoding.GetEncoding("utf-8"))
            Dim retString As String = myStreamReader.ReadToEnd()
            myStreamReader.Close()
            myResponseStream.Close()
            Return retString
        Catch Ex As Exception
            Return ("Error")
        End Try
    End Function

    '获取更新信息
    Public Shared Function GetReleaseInformation() As String
        Dim JsonString
        Try
            JsonString = GetHttpResponse("https://api.github.com/repos/mybluehorizon/classtable/releases/latest", 10000)
            Return (JsonString)
        Catch Ex As Exception
            Return ("Error")
        End Try
    End Function

    '解析更新版本
    Public Function GetLatestVersion(ByVal ReleaseInformation)
        Dim LatestVersion
        Try
            LatestVersion = JsonConvert.DeserializeObject(ReleaseInformation)
            Dim sIndex As String = LatestVersion("tag_name")
            Return sIndex
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
            Return ("Error")
        End Try
    End Function

    '进行下载
    Public Function HttpDownload(ByVal url As String, ByVal path As String) As Boolean
        Dim tempPath As String = System.Environment.GetEnvironmentVariable("TEMP") & "\ClassTable"
        System.IO.Directory.CreateDirectory(tempPath)
        Dim tempFile As String = tempPath & "\" & System.IO.Path.GetFileName(path) & ".temp"
        If System.IO.File.Exists(path) Then
            System.IO.File.Delete(path)
        End If
        If System.IO.File.Exists(tempFile) Then
            System.IO.File.Delete(tempFile)
        End If
        If url = "False" Then
            Return ("False")
        Else
            Try
                Dim fs As FileStream = New FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite)
                Dim request As HttpWebRequest = TryCast(WebRequest.Create(url), HttpWebRequest)
                Dim response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
                Dim responseStream As Stream = response.GetResponseStream()
                Dim bArr As Byte() = New Byte(1023) {}
                Dim size As Integer = responseStream.Read(bArr, 0, CInt(bArr.Length))

                While size > 0
                    fs.Write(bArr, 0, size)
                    size = responseStream.Read(bArr, 0, CInt(bArr.Length))
                End While

                fs.Close()
                responseStream.Close()
                System.IO.File.Move(tempFile, path)
                Return (True)
            Catch ex As Exception
                Return (False)
            End Try
        End If
    End Function
End Class
