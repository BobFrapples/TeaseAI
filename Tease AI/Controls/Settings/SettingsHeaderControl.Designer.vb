<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SettingsHeaderControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.HeaderTextLabel = New System.Windows.Forms.Label()
        Me.AppsSettingsLoad = New System.Windows.Forms.Button()
        Me.AppsSettingsSave = New System.Windows.Forms.Button()
        Me.LogoPictureBox = New System.Windows.Forms.PictureBox()
        Me.HeaderPanel.SuspendLayout()
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'HeaderPanel
        '
        Me.HeaderPanel.BackColor = System.Drawing.Color.Transparent
        Me.HeaderPanel.Controls.Add(Me.HeaderTextLabel)
        Me.HeaderPanel.Controls.Add(Me.AppsSettingsLoad)
        Me.HeaderPanel.Controls.Add(Me.AppsSettingsSave)
        Me.HeaderPanel.Controls.Add(Me.LogoPictureBox)
        Me.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Size = New System.Drawing.Size(1287, 60)
        Me.HeaderPanel.TabIndex = 0
        '
        'HeaderTextLabel
        '
        Me.HeaderTextLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HeaderTextLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HeaderTextLabel.Location = New System.Drawing.Point(162, 0)
        Me.HeaderTextLabel.Name = "HeaderTextLabel"
        Me.HeaderTextLabel.Size = New System.Drawing.Size(1001, 60)
        Me.HeaderTextLabel.TabIndex = 156
        Me.HeaderTextLabel.Text = "Header Text"
        Me.HeaderTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'AppsSettingsLoad
        '
        Me.AppsSettingsLoad.BackColor = System.Drawing.Color.Transparent
        Me.AppsSettingsLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.AppsSettingsLoad.Dock = System.Windows.Forms.DockStyle.Right
        Me.AppsSettingsLoad.FlatAppearance.BorderSize = 0
        Me.AppsSettingsLoad.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.AppsSettingsLoad.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.AppsSettingsLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AppsSettingsLoad.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppsSettingsLoad.ForeColor = System.Drawing.Color.Black
        Me.AppsSettingsLoad.Image = Global.Tease_AI.My.Resources.Resources.Button_Save
        Me.AppsSettingsLoad.Location = New System.Drawing.Point(1163, 0)
        Me.AppsSettingsLoad.Margin = New System.Windows.Forms.Padding(0)
        Me.AppsSettingsLoad.Name = "AppsSettingsLoad"
        Me.AppsSettingsLoad.Size = New System.Drawing.Size(62, 60)
        Me.AppsSettingsLoad.TabIndex = 155
        Me.AppsSettingsLoad.Text = "Load"
        Me.AppsSettingsLoad.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.AppsSettingsLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.AppsSettingsLoad.UseVisualStyleBackColor = False
        '
        'AppsSettingsSave
        '
        Me.AppsSettingsSave.BackColor = System.Drawing.Color.Transparent
        Me.AppsSettingsSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.AppsSettingsSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.AppsSettingsSave.FlatAppearance.BorderSize = 0
        Me.AppsSettingsSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.AppsSettingsSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.AppsSettingsSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AppsSettingsSave.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppsSettingsSave.ForeColor = System.Drawing.Color.Black
        Me.AppsSettingsSave.Image = Global.Tease_AI.My.Resources.Resources.Button_Export
        Me.AppsSettingsSave.Location = New System.Drawing.Point(1225, 0)
        Me.AppsSettingsSave.Margin = New System.Windows.Forms.Padding(0)
        Me.AppsSettingsSave.Name = "AppsSettingsSave"
        Me.AppsSettingsSave.Size = New System.Drawing.Size(62, 60)
        Me.AppsSettingsSave.TabIndex = 154
        Me.AppsSettingsSave.Text = "Save"
        Me.AppsSettingsSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.AppsSettingsSave.UseVisualStyleBackColor = False
        '
        'LogoPictureBox
        '
        Me.LogoPictureBox.BackColor = System.Drawing.Color.Transparent
        Me.LogoPictureBox.Dock = System.Windows.Forms.DockStyle.Left
        Me.LogoPictureBox.Image = Global.Tease_AI.My.Resources.Resources.TAI_Banner_small
        Me.LogoPictureBox.Location = New System.Drawing.Point(0, 0)
        Me.LogoPictureBox.Name = "LogoPictureBox"
        Me.LogoPictureBox.Size = New System.Drawing.Size(162, 60)
        Me.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.LogoPictureBox.TabIndex = 149
        Me.LogoPictureBox.TabStop = False
        '
        'SettingsHeaderControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.HeaderPanel)
        Me.Name = "SettingsHeaderControl"
        Me.Size = New System.Drawing.Size(1287, 60)
        Me.HeaderPanel.ResumeLayout(False)
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents HeaderPanel As Panel
    Friend WithEvents LogoPictureBox As PictureBox
    Friend WithEvents AppsSettingsLoad As Button
    Friend WithEvents AppsSettingsSave As Button
    Friend WithEvents HeaderTextLabel As Label
End Class
