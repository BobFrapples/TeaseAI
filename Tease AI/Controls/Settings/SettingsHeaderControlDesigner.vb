Option Strict On
Option Infer Off

Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Diagnostics
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Windows.Forms.Design

<ToolboxItemFilter("SettingsHeaderControl.SettingsTitle", ToolboxItemFilterType.Require)>
<Security.Permissions.PermissionSet(Security.Permissions.SecurityAction.Demand)>
Public Class SettingsHeaderControlDesigner
    Inherits DocumentDesigner
    Public Sub New()
        Trace.WriteLine("SettingsHeaderControlDesigner ctor")
    End Sub
End Class
