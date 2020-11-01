<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GlitterSettingsControl
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
        Me.GlitterSettingsGroupBox = New System.Windows.Forms.GroupBox()
        Me.ClearImageDirectoryButton = New System.Windows.Forms.Button()
        Me.SetImageDirectoryButton = New System.Windows.Forms.Button()
        Me.GlitterImageDirectory = New System.Windows.Forms.TextBox()
        Me.GlitterNameColorButton = New System.Windows.Forms.Button()
        Me.GlitterChatPreview = New System.Windows.Forms.Label()
        Me.ResponseFrequencySliderLabel = New System.Windows.Forms.Label()
        Me.ResponseFrequencySlider = New System.Windows.Forms.TrackBar()
        Me.GlitterEnableCheckbox = New System.Windows.Forms.CheckBox()
        Me.GlitterContactName = New System.Windows.Forms.TextBox()
        Me.GlitterAvatarImage = New System.Windows.Forms.PictureBox()
        Me.GlitterSettingsGroupBox.SuspendLayout()
        CType(Me.ResponseFrequencySlider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GlitterAvatarImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GlitterSettingsGroupBox
        '
        Me.GlitterSettingsGroupBox.Controls.Add(Me.ClearImageDirectoryButton)
        Me.GlitterSettingsGroupBox.Controls.Add(Me.SetImageDirectoryButton)
        Me.GlitterSettingsGroupBox.Controls.Add(Me.GlitterImageDirectory)
        Me.GlitterSettingsGroupBox.Controls.Add(Me.GlitterNameColorButton)
        Me.GlitterSettingsGroupBox.Controls.Add(Me.GlitterChatPreview)
        Me.GlitterSettingsGroupBox.Controls.Add(Me.ResponseFrequencySliderLabel)
        Me.GlitterSettingsGroupBox.Controls.Add(Me.ResponseFrequencySlider)
        Me.GlitterSettingsGroupBox.Controls.Add(Me.GlitterEnableCheckbox)
        Me.GlitterSettingsGroupBox.Controls.Add(Me.GlitterContactName)
        Me.GlitterSettingsGroupBox.Controls.Add(Me.GlitterAvatarImage)
        Me.GlitterSettingsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GlitterSettingsGroupBox.Location = New System.Drawing.Point(0, 0)
        Me.GlitterSettingsGroupBox.Name = "GlitterSettingsGroupBox"
        Me.GlitterSettingsGroupBox.Size = New System.Drawing.Size(350, 150)
        Me.GlitterSettingsGroupBox.TabIndex = 0
        Me.GlitterSettingsGroupBox.TabStop = False
        Me.GlitterSettingsGroupBox.Text = "GroupBox1"
        '
        'ClearImageDirectoryButton
        '
        Me.ClearImageDirectoryButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ClearImageDirectoryButton.BackColor = System.Drawing.Color.LightGray
        Me.ClearImageDirectoryButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClearImageDirectoryButton.ForeColor = System.Drawing.Color.Black
        Me.ClearImageDirectoryButton.Location = New System.Drawing.Point(177, 89)
        Me.ClearImageDirectoryButton.Name = "ClearImageDirectoryButton"
        Me.ClearImageDirectoryButton.Size = New System.Drawing.Size(39, 22)
        Me.ClearImageDirectoryButton.TabIndex = 191
        Me.ClearImageDirectoryButton.Text = "Clear"
        Me.ClearImageDirectoryButton.UseVisualStyleBackColor = False
        '
        'SetImageDirectoryButton
        '
        Me.SetImageDirectoryButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SetImageDirectoryButton.BackColor = System.Drawing.Color.LightGray
        Me.SetImageDirectoryButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SetImageDirectoryButton.ForeColor = System.Drawing.Color.Black
        Me.SetImageDirectoryButton.Location = New System.Drawing.Point(12, 89)
        Me.SetImageDirectoryButton.Name = "SetImageDirectoryButton"
        Me.SetImageDirectoryButton.Size = New System.Drawing.Size(160, 22)
        Me.SetImageDirectoryButton.TabIndex = 190
        Me.SetImageDirectoryButton.Text = "My Images Directory"
        Me.SetImageDirectoryButton.UseVisualStyleBackColor = False
        '
        'GlitterImageDirectory
        '
        Me.GlitterImageDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GlitterImageDirectory.BackColor = System.Drawing.SystemColors.Control
        Me.GlitterImageDirectory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GlitterImageDirectory.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GlitterImageDirectory.ForeColor = System.Drawing.Color.Black
        Me.GlitterImageDirectory.Location = New System.Drawing.Point(12, 117)
        Me.GlitterImageDirectory.MinimumSize = New System.Drawing.Size(204, 17)
        Me.GlitterImageDirectory.Name = "GlitterImageDirectory"
        Me.GlitterImageDirectory.ReadOnly = True
        Me.GlitterImageDirectory.Size = New System.Drawing.Size(204, 20)
        Me.GlitterImageDirectory.TabIndex = 189
        '
        'GlitterNameColorButton
        '
        Me.GlitterNameColorButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GlitterNameColorButton.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.GlitterNameColorButton.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GlitterNameColorButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GlitterNameColorButton.ForeColor = System.Drawing.Color.Black
        Me.GlitterNameColorButton.Location = New System.Drawing.Point(223, 19)
        Me.GlitterNameColorButton.Name = "GlitterNameColorButton"
        Me.GlitterNameColorButton.Size = New System.Drawing.Size(115, 24)
        Me.GlitterNameColorButton.TabIndex = 188
        Me.GlitterNameColorButton.Text = "Choose Name Color"
        Me.GlitterNameColorButton.UseVisualStyleBackColor = False
        '
        'GlitterChatPreview
        '
        Me.GlitterChatPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GlitterChatPreview.BackColor = System.Drawing.SystemColors.Control
        Me.GlitterChatPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GlitterChatPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GlitterChatPreview.Location = New System.Drawing.Point(223, 53)
        Me.GlitterChatPreview.Name = "GlitterChatPreview"
        Me.GlitterChatPreview.Size = New System.Drawing.Size(115, 23)
        Me.GlitterChatPreview.TabIndex = 187
        Me.GlitterChatPreview.Text = "Preview"
        Me.GlitterChatPreview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ResponseFrequencySliderLabel
        '
        Me.ResponseFrequencySliderLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ResponseFrequencySliderLabel.BackColor = System.Drawing.Color.Transparent
        Me.ResponseFrequencySliderLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ResponseFrequencySliderLabel.ForeColor = System.Drawing.Color.Black
        Me.ResponseFrequencySliderLabel.Location = New System.Drawing.Point(223, 92)
        Me.ResponseFrequencySliderLabel.Name = "ResponseFrequencySliderLabel"
        Me.ResponseFrequencySliderLabel.Size = New System.Drawing.Size(115, 19)
        Me.ResponseFrequencySliderLabel.TabIndex = 186
        Me.ResponseFrequencySliderLabel.Text = "Response Frequency"
        Me.ResponseFrequencySliderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ResponseFrequencySlider
        '
        Me.ResponseFrequencySlider.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ResponseFrequencySlider.AutoSize = False
        Me.ResponseFrequencySlider.LargeChange = 1
        Me.ResponseFrequencySlider.Location = New System.Drawing.Point(223, 114)
        Me.ResponseFrequencySlider.Maximum = 9
        Me.ResponseFrequencySlider.Minimum = 1
        Me.ResponseFrequencySlider.Name = "ResponseFrequencySlider"
        Me.ResponseFrequencySlider.Size = New System.Drawing.Size(115, 25)
        Me.ResponseFrequencySlider.TabIndex = 185
        Me.ResponseFrequencySlider.Value = 1
        '
        'GlitterEnableCheckbox
        '
        Me.GlitterEnableCheckbox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GlitterEnableCheckbox.AutoSize = True
        Me.GlitterEnableCheckbox.Checked = True
        Me.GlitterEnableCheckbox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.GlitterEnableCheckbox.ForeColor = System.Drawing.Color.Black
        Me.GlitterEnableCheckbox.Location = New System.Drawing.Point(94, 24)
        Me.GlitterEnableCheckbox.Name = "GlitterEnableCheckbox"
        Me.GlitterEnableCheckbox.Size = New System.Drawing.Size(122, 17)
        Me.GlitterEnableCheckbox.TabIndex = 184
        Me.GlitterEnableCheckbox.Text = "Enable This Contact"
        Me.GlitterEnableCheckbox.UseVisualStyleBackColor = True
        '
        'GlitterContactName
        '
        Me.GlitterContactName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GlitterContactName.BackColor = System.Drawing.Color.White
        Me.GlitterContactName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GlitterContactName.ForeColor = System.Drawing.Color.Black
        Me.GlitterContactName.Location = New System.Drawing.Point(82, 53)
        Me.GlitterContactName.Name = "GlitterContactName"
        Me.GlitterContactName.Size = New System.Drawing.Size(134, 23)
        Me.GlitterContactName.TabIndex = 182
        Me.GlitterContactName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GlitterAvatarImage
        '
        Me.GlitterAvatarImage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GlitterAvatarImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GlitterAvatarImage.Location = New System.Drawing.Point(12, 19)
        Me.GlitterAvatarImage.Name = "GlitterAvatarImage"
        Me.GlitterAvatarImage.Size = New System.Drawing.Size(64, 64)
        Me.GlitterAvatarImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.GlitterAvatarImage.TabIndex = 183
        Me.GlitterAvatarImage.TabStop = False
        '
        'GlitterSettingsControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.GlitterSettingsGroupBox)
        Me.MinimumSize = New System.Drawing.Size(350, 150)
        Me.Name = "GlitterSettingsControl"
        Me.Size = New System.Drawing.Size(350, 150)
        Me.GlitterSettingsGroupBox.ResumeLayout(False)
        Me.GlitterSettingsGroupBox.PerformLayout()
        CType(Me.ResponseFrequencySlider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GlitterAvatarImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GlitterSettingsGroupBox As GroupBox
    Friend WithEvents ClearImageDirectoryButton As Button
    Friend WithEvents SetImageDirectoryButton As Button
    Friend WithEvents GlitterImageDirectory As TextBox
    Friend WithEvents GlitterNameColorButton As Button
    Friend WithEvents GlitterChatPreview As Label
    Friend WithEvents ResponseFrequencySliderLabel As Label
    Friend WithEvents ResponseFrequencySlider As TrackBar
    Friend WithEvents GlitterEnableCheckbox As CheckBox
    Friend WithEvents GlitterContactName As TextBox
    Friend WithEvents GlitterAvatarImage As PictureBox
End Class
