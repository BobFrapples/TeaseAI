<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingsDescriptionControl
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
        Me.DescriptionGroupBox = New System.Windows.Forms.GroupBox()
        Me.AppsSettingsDescriptionLabel = New System.Windows.Forms.Label()
        Me.DescriptionGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'DescriptionGroupBox
        '
        Me.DescriptionGroupBox.BackColor = System.Drawing.Color.Transparent
        Me.DescriptionGroupBox.Controls.Add(Me.AppsSettingsDescriptionLabel)
        Me.DescriptionGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DescriptionGroupBox.ForeColor = System.Drawing.Color.Black
        Me.DescriptionGroupBox.Location = New System.Drawing.Point(0, 0)
        Me.DescriptionGroupBox.Name = "DescriptionGroupBox"
        Me.DescriptionGroupBox.Size = New System.Drawing.Size(980, 165)
        Me.DescriptionGroupBox.TabIndex = 68
        Me.DescriptionGroupBox.TabStop = False
        Me.DescriptionGroupBox.Text = "Description"
        '
        'AppsSettingsDescriptionLabel
        '
        Me.AppsSettingsDescriptionLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AppsSettingsDescriptionLabel.BackColor = System.Drawing.Color.Transparent
        Me.AppsSettingsDescriptionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppsSettingsDescriptionLabel.ForeColor = System.Drawing.Color.Black
        Me.AppsSettingsDescriptionLabel.Location = New System.Drawing.Point(9, 16)
        Me.AppsSettingsDescriptionLabel.Name = "AppsSettingsDescriptionLabel"
        Me.AppsSettingsDescriptionLabel.Size = New System.Drawing.Size(964, 144)
        Me.AppsSettingsDescriptionLabel.TabIndex = 62
        Me.AppsSettingsDescriptionLabel.Text = "Hover over any setting in the menu for a more detailed description of its functio" &
    "n."
        Me.AppsSettingsDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SettingsDescriptionControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.DescriptionGroupBox)
        Me.Name = "SettingsDescriptionControl"
        Me.Size = New System.Drawing.Size(980, 165)
        Me.DescriptionGroupBox.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DescriptionGroupBox As GroupBox
    Friend WithEvents AppsSettingsDescriptionLabel As Label
End Class
