Imports Newtonsoft.Json
Imports ClosedXML.Excel
Imports System.IO
Public Class Excel

    '读取表格部分

    '将表格转换为Json
    Public Function GetTableInformation(ByVal Sheet As String)
        Dim fileStream = New FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\ATHS Studio\ClassTable\tablefile.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
        Dim TableWorkBook = New XLWorkbook(fileStream)
        Dim TableSheet As IXLWorksheet = TableWorkBook.Worksheet(Sheet)
        Dim TableString As String
        TableString =
    "{" + "RangeA" + ":" + Chr(34) & TableSheet.Cell("A3").Value & Chr(34) + "," + "RangeB" + ":" + Chr(34) & TableSheet.Cell("A4").Value & Chr(34) + "," + "RangeC" + ":" + Chr(34) & TableSheet.Cell("A5").Value & Chr(34) + "," + "RangeD" + ":" + Chr(34) & TableSheet.Cell("A6").Value & Chr(34) + "," + "RangeE" + ":" + Chr(34) & TableSheet.Cell("A7").Value & Chr(34) + "," + "RangeF" + ":" + Chr(34) & TableSheet.Cell("A8").Value & Chr(34) + "," + "RangeG" + ":" + Chr(34) & TableSheet.Cell("A10").Value & Chr(34) + "," + "RangeH" + ":" + Chr(34) & TableSheet.Cell("A11").Value & Chr(34) + "," + "RangeI" + ":" + Chr(34) & TableSheet.Cell("A12").Value & Chr(34) + "," + "RangeJ" + ":" + Chr(34) & TableSheet.Cell("A13").Value & Chr(34) + "," + "RangeK" + ":" + Chr(34) & TableSheet.Cell("A14").Value & Chr(34) + "," + "RangeL" + ":" + Chr(34) & TableSheet.Cell("A15").Value & Chr(34) + "}"
        Return TableString
    End Function

    '解析 Json 数据
    Public Function GetClassName(ByVal ClassJsonString As String, ByVal ClassBlock As String)
        Dim GetName
        GetName = JsonConvert.DeserializeObject(ClassJsonString)
        Dim ClassName As String = GetName(ClassBlock)
        Return ClassName
    End Function
End Class
