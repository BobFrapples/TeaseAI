Option Strict On
Option Infer Off
Imports System.ComponentModel
Imports System.Windows.Forms.Design

<ToolboxItemFilter("GlitterSettingsControl.GlitterLabel", ToolboxItemFilterType.Require)>
<ToolboxItemFilter("GlitterSettingsControl.EnabledLabel", ToolboxItemFilterType.Require)>
<Security.Permissions.PermissionSet(Security.Permissions.SecurityAction.Demand)>
Public Class GlitterSettingsControlDesigner
    Inherits DocumentDesigner

End Class
