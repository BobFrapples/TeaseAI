Option Infer Off
Option Strict On
Imports System.IO
Imports TeaseAI.Common
Imports TeaseAI.Common.Data
Imports TeaseAI.Common.Interfaces

Public Class ScriptAccessor
    Implements IScriptAccessor
    ''' <summary>
    ''' Get Any scripts available for the Domme based on the current submissive personality(Currently only in chastity is used)
    ''' </summary>
    ''' <param name="domme"></param>
    ''' <param name="submissive"></param>
    ''' <param name="type">one of Stroke or Interrupt </param>
    ''' <param name="stage">This varies.</param>
    ''' <returns></returns>
    Public Function GetAvailableScripts(domme As DommePersonality, submissive As SubPersonality, type As String, stage As String) As Result(Of List(Of ScriptMetaData)) Implements IScriptAccessor.GetAvailableScripts
        Dim searchSuffix As String = GetSearchSuffix(submissive)
        Dim scriptList As New List(Of ScriptMetaData)

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath + "\Scripts\" + domme.PersonalityName + "\" + type + "\" + stage + "\", FileIO.SearchOption.SearchTopLevelOnly, searchSuffix)
            Dim scriptName As String = Path.GetFileName(foundFile).Replace(".txt", "")

            Dim getScripts As Result(Of List(Of String)) = Result.Ok(GetAvailableScripts(domme, type, stage)) _
                .Ensure(Function(sl) sl.Count > 0, "No available scripts") _
                .OnSuccess(Sub(availableScripts)
                               For x As Integer = 0 To availableScripts.Count - 1
                                   'TODO: Figure out the BEG requirement
                                   If submissive.InChastity OrElse submissive.IsOrgasmRestricted Then
                                       If availableScripts(x) = scriptName Then
                                           scriptList.Add(CreateScriptMetaData(foundFile))
                                       End If
                                   Else
                                       If availableScripts(x) = scriptName AndAlso Not scriptName.Contains("_CHASTITY") AndAlso
                                           Not scriptName.Contains("_RESTRICTED") AndAlso
                                           Not scriptName.Contains("_BEG") Then
                                           scriptList.Add(CreateScriptMetaData(foundFile))
                                       End If
                                   End If
                               Next
                           End Sub)
        Next
        Return Result.Ok(scriptList)
    End Function

    Private Function GetSearchSuffix(submissive As SubPersonality) As String
        If (submissive.InChastity) Then
            Return "*_CHASTITY.txt"
        ElseIf submissive.IsOrgasmRestricted Then
            Return "*_RESTRICTED.txt"
            ' *_BEG.txt should show up somehow, but I'm not sure how
        End If
        Return "*.txt"
    End Function

    Public Function GetScript(metaData As ScriptMetaData) As Result(Of Script) Implements IScriptAccessor.GetScript
        Try
            Return Result.Ok(New Script(metaData, File.ReadAllLines(metaData.Key).ToList()))
        Catch ex As Exception
            Return Result.Fail(Of Script)(ex.Message)
        End Try
    End Function

    Public Function GetScript(domme As DommePersonality, fileName As String) As Result(Of Script) Implements IScriptAccessor.GetScript
        Try
            Dim fullName As String = Application.StartupPath + "\Scripts\" + domme.PersonalityName + "\" + fileName
            If (Not File.Exists(fullName)) Then
                Return Result.Fail(Of Script)(fileName + "does not exist please try again.")
            End If

            Dim metaData As ScriptMetaData = CreateScriptMetaData(fullName)
            Return Result.Ok(New Script(metaData, File.ReadAllLines(metaData.Key).ToList()))
        Catch ex As Exception
            Return Result.Fail(Of Script)(ex.Message)
        End Try
    End Function

    Public Function GetFallbackMetaData(session As Session, stage As String) As Result(Of ScriptMetaData) Implements IScriptAccessor.GetFallbackMetaData
        Dim path As String = Application.StartupPath + "\Scripts\" + session.Domme.PersonalityName + "\System\Scripts\" + stage + ".txt"
        If (session.Sub.InChastity) Then
            path = Application.StartupPath + "\Scripts\" + session.Domme.PersonalityName + "\System\Scripts\" + stage + "_CHASTITY.txt"
        End If

        If (Not File.Exists(path)) Then
            Return Result.Fail(Of ScriptMetaData)("Fallback script not found")
        End If
        Return Result.Ok(CreateScriptMetaData(path))
    End Function

    Private Function GetAvailableScripts(domme As DommePersonality, type As String, stage As String) As List(Of String)
        Dim baseDir As String = Application.StartupPath + "\Scripts\" + domme.PersonalityName + "\"
        Dim scriptDir As String = baseDir + type + "\" + stage + "\"
        Dim checkList As String = baseDir + "System\" + stage + "CheckList.cld"

        Dim returnValue As List(Of String) = New List(Of String)()
        Using fs As New FileStream(checkList, FileMode.Open), binRead As New BinaryReader(fs)
            Do While fs.Position < fs.Length
                Dim fileName As String = binRead.ReadString()
                Dim enabled As Boolean = binRead.ReadBoolean()
                Dim fullFilePath As String = scriptDir + fileName + ".txt"

                If File.Exists(fullFilePath) AndAlso enabled Then
                    returnValue.Add(fileName)
                End If
            Loop
        End Using
        Return returnValue.Distinct().ToList()
    End Function

    Private Function CreateScriptMetaData(fileName As String) As ScriptMetaData
        Dim script As ScriptMetaData = New ScriptMetaData()
        script.Name = Path.GetFileName(fileName).Replace(".txt", "")
        script.Key = fileName

        Dim data As List(Of String) = Txt2List(script.Key)
        Dim info As String = data.FirstOrDefault(Function(line) line.StartsWith("@Info"))
        If (Not String.IsNullOrWhiteSpace(info)) Then
            info = info.Replace("@Info", "")
        End If
        Return script
    End Function
End Class
