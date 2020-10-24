Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Diagnostics
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Windows.Forms.Design


<ToolboxItemFilter("RandomizerAppPanel.LabelColor", ToolboxItemFilterType.Require)>
<ToolboxItemFilter("RandomizerAppPanel.BackColor", ToolboxItemFilterType.Require)>
<ToolboxItemFilter("RandomizerAppPanel.ButtonForegroundColor", ToolboxItemFilterType.Require)>
<ToolboxItemFilter("RandomizerAppPanel.ButtonBackgroundColor", ToolboxItemFilterType.Require)>
<Security.Permissions.PermissionSet(Security.Permissions.SecurityAction.Demand)>
Public Class RandomizerAppPanelDesigner
    Inherits DocumentDesigner
    Public Sub New()
        Trace.WriteLine("MarqueeControlRootDesigner ctor")
    End Sub
End Class
