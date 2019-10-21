Option Strict On
Option Infer Off

Imports TeaseAI.Common.Interfaces.Accessors

Public Class ConfigurationAccessor
    Implements IConfigurationAccessor

    Public Function GetBaseFolder() As String Implements IConfigurationAccessor.GetBaseFolder
        Return Application.StartupPath
    End Function
End Class
