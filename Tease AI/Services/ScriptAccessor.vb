Option Infer Off
Option Strict On
Imports System.IO
Imports TeaseAI.Common
Imports TeaseAI.Common.Constants
Imports TeaseAI.Common.Data
Imports TeaseAI.Common.Interfaces
Imports TeaseAI.Common.Interfaces.Accessors

Public Class ScriptAccessor
    Implements IScriptAccessor

    Public Sub New(cldAccessor As CldAccessor)
        myCldAccessor = cldAccessor
    End Sub

    ''' <summary>
    ''' Get Any scripts available for the Domme based on the current submissive personality(Currently only in chastity is used)
    ''' </summary>
    ''' <param name="domme"></param>
    ''' <param name="submissive"></param>
    ''' <param name="type">one of Stroke or Interrupt </param>
    ''' <param name="stage">This varies.</param>
    ''' <returns></returns>
    Public Function GetAvailableScripts(domme As DommePersonality, submissive As SubPersonality, type As String, stage As SessionPhase) As Result(Of List(Of ScriptMetaData)) Implements IScriptAccessor.GetAvailableScripts
        Dim searchSuffix As String = GetSearchSuffix(submissive)
        Dim scriptList As New List(Of ScriptMetaData)

        Dim availableScripts As List(Of String) = GetAvailableScripts(domme.PersonalityName, type, stage)

        Dim dirPath As String = GetDirPath(domme.PersonalityName, type, stage)
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(dirPath, FileIO.SearchOption.SearchTopLevelOnly, searchSuffix)
            Dim scriptName As String = Path.GetFileName(foundFile).Replace(".txt", "")

            Dim getScripts As Result(Of List(Of String)) = Result.Ok(availableScripts) _
                .Ensure(Function(sl) sl.Count > 0, "No available scripts") _
                .OnSuccess(Sub(ascripts)
                               For x As Integer = 0 To ascripts.Count - 1
                                   'TODO: Figure out the BEG requirement
                                   If submissive.InChastity OrElse submissive.IsOrgasmRestricted Then
                                       If ascripts(x) = scriptName Then
                                           scriptList.Add(CreateScriptMetaData(foundFile))
                                       End If
                                   Else
                                       If ascripts(x) = scriptName AndAlso Not scriptName.Contains("_CHASTITY") AndAlso
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

    Public Function GetAllScripts(dommePersonalityName As String, type As String, stage As SessionPhase, isDefaultEnabled As Boolean) As List(Of ScriptMetaData) Implements IScriptAccessor.GetAllScripts
        Dim baseDir As String = GetDommeBaseDir(dommePersonalityName)
        Dim scriptDir As String = GetDirPath(dommePersonalityName, type, stage)
        Dim checkListFile As String = baseDir + "System\" + stage.ToString() + "CheckList.cld"

        Dim cldData As List(Of ScriptMetaData) = myCldAccessor.ReadCld(scriptDir, checkListFile).GetResultOrDefault(New List(Of ScriptMetaData))
        cldData.ForEach(Function(cld) cld.Key = scriptDir & cld.Name & ".txt")

        Dim finalCld As List(Of ScriptMetaData) = cldData.Where(Function(cld) File.Exists(cld.Key)).ToList()
        finalCld.ForEach(Function(cld) cld.Info = GetScriptInfo(cld.Key))

        Dim fileData As List(Of ScriptMetaData) = GetScriptFiles(dommePersonalityName, type, stage, isDefaultEnabled)
        fileData = fileData.Where(Function(file) Not finalCld.Any(Function(cld) cld.Key = file.Key)).ToList()
        finalCld.AddRange(fileData)

        Return finalCld.OrderBy(Function(cld) cld.Name).ToList()
    End Function

    ''' <summary>
    ''' Get MetaData for files added to the folder without being in the checklist file
    ''' </summary>
    ''' <param name="dommePersonalityName"></param>
    ''' <param name="type"></param>
    ''' <param name="stage"></param>
    ''' <param name="isEnabled"></param>
    ''' <returns></returns>
    Private Function GetScriptFiles(dommePersonalityName As String, type As String, stage As SessionPhase, isEnabled As Boolean) As List(Of ScriptMetaData)
        Dim scriptDir As String = GetDirPath(dommePersonalityName, type, stage)
        If Not Directory.Exists(scriptDir) Then Directory.CreateDirectory(scriptDir)

        Dim returnValue As List(Of ScriptMetaData) = New List(Of ScriptMetaData)()
        For Each scriptFile As String In Directory.GetFiles(scriptDir, "*.txt", SearchOption.TopDirectoryOnly)
            Dim scriptName As String = Path.GetFileNameWithoutExtension(scriptFile)

            Dim cld As ScriptMetaData = New ScriptMetaData With {
                .Key = scriptFile,
                .Name = scriptName,
                .IsEnabled = isEnabled,
                .Info = GetScriptInfo(scriptFile)
            }
            returnValue.Add(cld)
        Next

        Return returnValue
    End Function

    Private Function GetAvailableScripts(dommePersonalityName As String, type As String, stage As SessionPhase) As List(Of String)
        Dim baseDir As String = Application.StartupPath + "\Scripts\" + dommePersonalityName + "\"
        Dim scriptDir As String = GetDirPath(dommePersonalityName, type, stage)
        Dim stageName As String = stage.ToString()

        ' This if clause is required because of inconsistency in naming within the program. 
        If stage = SessionPhase.Modules Then
            stageName = "Module"
        End If
        Dim checkList As String = baseDir + "System\" + stageName + "CheckList.cld"

        If Not File.Exists(checkList) Then
            Throw New FileNotFoundException(checkList + " was not found, please report this to the script writer.")
        End If
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

    Public Function GetFallbackMetaData(session As Session, stage As SessionPhase) As Result(Of ScriptMetaData) Implements IScriptAccessor.GetFallbackMetaData
        Dim fallbackFileName As String = stage.ToString()
        If stage = SessionPhase.Modules Then
            fallbackFileName = "module"
        End If
        Dim path As String = Application.StartupPath + "\Scripts\" + session.Domme.PersonalityName + "\System\Scripts\" + fallbackFileName + ".txt"
        If (session.Sub.InChastity) Then
            path = Application.StartupPath + "\Scripts\" + session.Domme.PersonalityName + "\System\Scripts\" + fallbackFileName + "_CHASTITY.txt"
        End If
        If (session.Sub.IsEdging) Then
            path = Application.StartupPath + "\Scripts\" + session.Domme.PersonalityName + "\System\Scripts\" + fallbackFileName + "_EDGING.txt"
        End If

        If (Not File.Exists(path)) Then
            Return Result.Fail(Of ScriptMetaData)("No script available for " + stage.ToString())
        End If
        Return Result.Ok(CreateScriptMetaData(path))
    End Function

    Private Function CreateScriptMetaData(fileName As String) As ScriptMetaData
        Dim script As ScriptMetaData = New ScriptMetaData With {
            .Name = Path.GetFileName(fileName).Replace(".txt", ""),
            .Key = fileName,
            .Info = GetScriptInfo(fileName)
        }

        Return script
    End Function

    Private Function GetScriptInfo(filename As String) As String
        Dim data As List(Of String) = Txt2List(filename)
        Dim info As String = data.FirstOrDefault(Function(line) line.StartsWith("@Info"))
        If (Not String.IsNullOrWhiteSpace(info)) Then
            info = info.Replace("@Info", "")
        End If
        Return info
    End Function

    Public Sub Save(scripts As List(Of ScriptMetaData), dommePersonalityName As String, type As String, stage As SessionPhase) Implements IScriptAccessor.Save
        Dim baseDir As String = GetDommeBaseDir(dommePersonalityName)
        Dim checkListFile As String = baseDir + "System\" + stage.ToString() + "CheckList.cld"
        myCldAccessor.WriteCld(scripts, checkListFile)
    End Sub

    Public Function Save(script As Script) As Result Implements IScriptAccessor.Save
        Throw New NotImplementedException()
    End Function

#Region "File location information"
    Private Function GetDirPath(dommePersonalityName As String, type As String, stage As SessionPhase) As String
        Dim baseDir As String = GetDommeBaseDir(dommePersonalityName)
        If stage = SessionPhase.Modules Then
            baseDir += "Modules\"
        Else
            baseDir += type + "\" + stage.ToString() + "\"
        End If

        Return baseDir
    End Function

    ''' <summary>
    ''' Get the backslash terminated base directory for <paramref name="dommePersonalityName"/> 
    ''' </summary>
    ''' <param name="dommePersonalityName"></param>
    ''' <returns></returns>
    Private Function GetDommeBaseDir(dommePersonalityName As String) As String
        Return Application.StartupPath + "\Scripts\" + dommePersonalityName + "\"
    End Function

    Public Function GetAllScripts(dommePersonalityName As String) As Result(Of List(Of ScriptMetaData)) Implements IScriptAccessor.GetAllScripts
        Throw New NotImplementedException()
    End Function

    Public Function GetScript(id As String) As Result(Of Script) Implements IScriptAccessor.GetScript
        Try
            If (Not File.Exists(id)) Then
                Return Result.Fail(Of Script)(id + "does not exist please try again.")
            End If

            Dim metaData As ScriptMetaData = CreateScriptMetaData(id)
            Return Result.Ok(New Script(metaData, File.ReadAllLines(metaData.Key).ToList()))
        Catch ex As Exception
            Return Result.Fail(Of Script)(ex.Message)
        End Try
    End Function

#End Region
    Private myCldAccessor As ICldAccessor
End Class
