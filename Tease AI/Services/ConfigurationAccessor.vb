Option Strict On
Option Infer Off

Imports TeaseAI.Common
Imports TeaseAI.Common.Data
Imports TeaseAI.Common.Interfaces.Accessors

Public Class ConfigurationAccessor
    Implements IConfigurationAccessor

    Public Function GetBaseFolder() As String Implements IConfigurationAccessor.GetBaseFolder
        Return Application.StartupPath
    End Function

    Public Function GetApplicationConfiguration() As ApplicationConfiguration Implements IConfigurationAccessor.GetApplicationConfiguration
        Throw New NotImplementedException()
    End Function

    Public Function SaveApplicationConfiguration(applicationConfiguration As ApplicationConfiguration) As Result Implements IConfigurationAccessor.SaveApplicationConfiguration
        Throw New NotImplementedException()
    End Function
End Class
