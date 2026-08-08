<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainUI
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainUI))
        Me.LblVersion = New System.Windows.Forms.Label()
        Me.BtnCopy = New System.Windows.Forms.Button()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnOpen = New System.Windows.Forms.Button()
        Me.TxtMain = New System.Windows.Forms.TextBox()
        Me.OfdBoardinfo = New System.Windows.Forms.OpenFileDialog()
        Me.BtnParse = New System.Windows.Forms.Button()
        Me.SFDBoardinfo = New System.Windows.Forms.SaveFileDialog()
        Me.RadV3 = New System.Windows.Forms.RadioButton()
        Me.RadV5 = New System.Windows.Forms.RadioButton()
        Me.RadV6 = New System.Windows.Forms.RadioButton()
        Me.RadD4 = New System.Windows.Forms.RadioButton()
        Me.CboD2 = New System.Windows.Forms.ComboBox()
        Me.RadD1 = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'LblVersion
        '
        Me.LblVersion.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LblVersion.ForeColor = System.Drawing.Color.LightCyan
        Me.LblVersion.Location = New System.Drawing.Point(244, 9)
        Me.LblVersion.Name = "LblVersion"
        Me.LblVersion.Size = New System.Drawing.Size(297, 31)
        Me.LblVersion.TabIndex = 200
        Me.LblVersion.Text = "HW Dollar4 版本: 1.0.0 (2020.01)"
        Me.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnCopy
        '
        Me.BtnCopy.BackColor = System.Drawing.Color.Violet
        Me.BtnCopy.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.BtnCopy.ForeColor = System.Drawing.Color.White
        Me.BtnCopy.Location = New System.Drawing.Point(547, 8)
        Me.BtnCopy.Name = "BtnCopy"
        Me.BtnCopy.Size = New System.Drawing.Size(110, 32)
        Me.BtnCopy.TabIndex = 201
        Me.BtnCopy.Text = "复制 (&C)"
        Me.BtnCopy.UseVisualStyleBackColor = False
        '
        'BtnSave
        '
        Me.BtnSave.BackColor = System.Drawing.Color.MediumBlue
        Me.BtnSave.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.BtnSave.ForeColor = System.Drawing.Color.White
        Me.BtnSave.Location = New System.Drawing.Point(12, 8)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(110, 32)
        Me.BtnSave.TabIndex = 101
        Me.BtnSave.Text = "保存 (&S)"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'BtnOpen
        '
        Me.BtnOpen.BackColor = System.Drawing.Color.MediumSlateBlue
        Me.BtnOpen.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.BtnOpen.ForeColor = System.Drawing.Color.White
        Me.BtnOpen.Location = New System.Drawing.Point(128, 8)
        Me.BtnOpen.Name = "BtnOpen"
        Me.BtnOpen.Size = New System.Drawing.Size(110, 32)
        Me.BtnOpen.TabIndex = 111
        Me.BtnOpen.Text = "打开 (&O)"
        Me.BtnOpen.UseVisualStyleBackColor = False
        '
        'TxtMain
        '
        Me.TxtMain.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TxtMain.Location = New System.Drawing.Point(12, 46)
        Me.TxtMain.MaxLength = 2147483647
        Me.TxtMain.Multiline = True
        Me.TxtMain.Name = "TxtMain"
        Me.TxtMain.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TxtMain.Size = New System.Drawing.Size(760, 471)
        Me.TxtMain.TabIndex = 801
        '
        'OfdBoardinfo
        '
        Me.OfdBoardinfo.Filter = "所有文件|*.*"
        '
        'BtnParse
        '
        Me.BtnParse.BackColor = System.Drawing.Color.Orchid
        Me.BtnParse.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.BtnParse.ForeColor = System.Drawing.Color.White
        Me.BtnParse.Location = New System.Drawing.Point(662, 8)
        Me.BtnParse.Name = "BtnParse"
        Me.BtnParse.Size = New System.Drawing.Size(110, 32)
        Me.BtnParse.TabIndex = 211
        Me.BtnParse.Text = "计算 (&P)"
        Me.BtnParse.UseVisualStyleBackColor = False
        '
        'SFDBoardinfo
        '
        Me.SFDBoardinfo.Filter = "所有文件|*.*"
        '
        'RadV3
        '
        Me.RadV3.AutoSize = True
        Me.RadV3.Checked = True
        Me.RadV3.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.RadV3.ForeColor = System.Drawing.Color.White
        Me.RadV3.Location = New System.Drawing.Point(252, 525)
        Me.RadV3.Name = "RadV3"
        Me.RadV3.Size = New System.Drawing.Size(81, 26)
        Me.RadV3.TabIndex = 921
        Me.RadV3.TabStop = True
        Me.RadV3.Text = "V&3模式"
        Me.RadV3.UseVisualStyleBackColor = True
        '
        'RadV5
        '
        Me.RadV5.AutoSize = True
        Me.RadV5.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.RadV5.ForeColor = System.Drawing.Color.White
        Me.RadV5.Location = New System.Drawing.Point(352, 525)
        Me.RadV5.Name = "RadV5"
        Me.RadV5.Size = New System.Drawing.Size(81, 26)
        Me.RadV5.TabIndex = 931
        Me.RadV5.Text = "V&5模式"
        Me.RadV5.UseVisualStyleBackColor = True
        '
        'RadV6
        '
        Me.RadV6.AutoSize = True
        Me.RadV6.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.RadV6.ForeColor = System.Drawing.Color.White
        Me.RadV6.Location = New System.Drawing.Point(452, 525)
        Me.RadV6.Name = "RadV6"
        Me.RadV6.Size = New System.Drawing.Size(81, 26)
        Me.RadV6.TabIndex = 941
        Me.RadV6.Text = "V&6模式"
        Me.RadV6.UseVisualStyleBackColor = True
        '
        'RadD4
        '
        Me.RadD4.AutoSize = True
        Me.RadD4.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.RadD4.ForeColor = System.Drawing.Color.White
        Me.RadD4.Location = New System.Drawing.Point(552, 525)
        Me.RadD4.Name = "RadD4"
        Me.RadD4.Size = New System.Drawing.Size(80, 26)
        Me.RadD4.TabIndex = 951
        Me.RadD4.Text = "$&4模式"
        Me.RadD4.UseVisualStyleBackColor = True
        '
        'CboD2
        '
        Me.CboD2.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CboD2.FormattingEnabled = True
        Me.CboD2.Items.AddRange(New Object() {"OS", "BOARDINFO", "OMCI"})
        Me.CboD2.Location = New System.Drawing.Point(12, 523)
        Me.CboD2.Name = "CboD2"
        Me.CboD2.Size = New System.Drawing.Size(110, 28)
        Me.CboD2.TabIndex = 901
        Me.CboD2.Text = "OS"
        '
        'RadD1
        '
        Me.RadD1.AutoSize = True
        Me.RadD1.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.RadD1.ForeColor = System.Drawing.Color.White
        Me.RadD1.Location = New System.Drawing.Point(152, 525)
        Me.RadD1.Name = "RadD1"
        Me.RadD1.Size = New System.Drawing.Size(80, 26)
        Me.RadD1.TabIndex = 911
        Me.RadD1.Text = "$&1模式"
        Me.RadD1.UseVisualStyleBackColor = True
        '
        'MainUI
        '
        Me.AllowDrop = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.MediumVioletRed
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.RadD1)
        Me.Controls.Add(Me.CboD2)
        Me.Controls.Add(Me.RadD4)
        Me.Controls.Add(Me.RadV6)
        Me.Controls.Add(Me.RadV5)
        Me.Controls.Add(Me.RadV3)
        Me.Controls.Add(Me.BtnParse)
        Me.Controls.Add(Me.LblVersion)
        Me.Controls.Add(Me.BtnCopy)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.BtnOpen)
        Me.Controls.Add(Me.TxtMain)
        Me.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.Name = "MainUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HW Dollar4"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LblVersion As Label
    Friend WithEvents BtnCopy As Button
    Friend WithEvents BtnSave As Button
    Friend WithEvents BtnOpen As Button
    Friend WithEvents TxtMain As TextBox
    Friend WithEvents OfdBoardinfo As OpenFileDialog
    Friend WithEvents BtnParse As Button
    Friend WithEvents SFDBoardinfo As SaveFileDialog
    Friend WithEvents RadV3 As RadioButton
    Friend WithEvents RadV5 As RadioButton
    Friend WithEvents RadV6 As RadioButton
    Friend WithEvents RadD4 As RadioButton
    Friend WithEvents CboD2 As ComboBox
    Friend WithEvents RadD1 As RadioButton
End Class
