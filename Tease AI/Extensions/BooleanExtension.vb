Option Infer Off
Option Strict On
Imports System.Runtime.CompilerServices

Module BooleanExtension
    ''' <summary>
    ''' Convert a boolean to green or red
    ''' </summary>
    ''' <param name="isTrue"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function ToColor(ByVal isTrue As Boolean) As Color
        Return If(isTrue, Color.Green, Color.Red)
    End Function


    ''' <summary>
    ''' Convert a boolean to ON or OFF
    ''' </summary>
    ''' <param name="isTrue"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function ToOnOff(ByVal isTrue As Boolean) As String
        Return If(isTrue, "ON", "OFF")
    End Function
End Module
