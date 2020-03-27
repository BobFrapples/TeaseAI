Option Explicit On
Option Infer Off
Imports Tease_AI.My
Imports TeaseAI.Common.Constants
Imports TeaseAI.Common.Data

Public Class ApplicationConfigMarshalling
    Public Function GetApplicationConfiguration() As ApplicationConfiguration
        Dim appConfig As ApplicationConfiguration = New ApplicationConfiguration()
        Return appConfig
    End Function

End Class
