<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GlitterApp
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GlitterHistory = New System.Windows.Forms.WebBrowser()
        Me.SuspendLayout()
        '
        'GlitterHistory
        '
        Me.GlitterHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GlitterHistory.Location = New System.Drawing.Point(0, 0)
        Me.GlitterHistory.MinimumSize = New System.Drawing.Size(20, 20)
        Me.GlitterHistory.Name = "GlitterHistory"
        Me.GlitterHistory.Size = New System.Drawing.Size(240, 612)
        Me.GlitterHistory.TabIndex = 0
        '
        'GlitterApp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GlitterHistory)
        Me.Name = "GlitterApp"
        Me.Size = New System.Drawing.Size(240, 612)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GlitterHistory As WebBrowser
End Class
