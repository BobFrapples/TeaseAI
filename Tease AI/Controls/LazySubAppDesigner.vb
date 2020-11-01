Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Diagnostics
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Windows.Forms.Design


<ToolboxItemFilter("LazySubApp.ButtonForegroundColor", ToolboxItemFilterType.Require)>
<ToolboxItemFilter("LazySubApp.ButtonBackgroundColor", ToolboxItemFilterType.Require)>
<ToolboxItemFilter("LazySubApp.LabelColor", ToolboxItemFilterType.Require)>
<ToolboxItemFilter("LazySubApp.BackColor", ToolboxItemFilterType.Require)>
<Security.Permissions.PermissionSet(Security.Permissions.SecurityAction.Demand)>
Public Class LazySubAppDesigner
    Inherits DocumentDesigner
    Public Sub New()
        Trace.WriteLine("LazySubAppDesigner ctor")
    End Sub
End Class

