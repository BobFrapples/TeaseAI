Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Diagnostics
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Windows.Forms.Design


<ToolboxItemFilter("RandomizerApp.LabelColor", ToolboxItemFilterType.Require)>
<ToolboxItemFilter("RandomizerApp.BackColor", ToolboxItemFilterType.Require)>
<ToolboxItemFilter("RandomizerApp.ButtonForegroundColor", ToolboxItemFilterType.Require)>
<ToolboxItemFilter("RandomizerApp.ButtonBackgroundColor", ToolboxItemFilterType.Require)>
<Security.Permissions.PermissionSet(Security.Permissions.SecurityAction.Demand)>
Public Class RandomizerAppDesigner
    Inherits DocumentDesigner
    Public Sub New()
        Trace.WriteLine("MarqueeControlRootDesigner ctor")
    End Sub
End Class
