<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Policy
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Policy))
        Me.BtnPolicy = New System.Windows.Forms.Button()
        Me.TxtPolicy = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'BtnPolicy
        '
        Me.BtnPolicy.BackColor = System.Drawing.Color.DeepPink
        Me.BtnPolicy.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.BtnPolicy.ForeColor = System.Drawing.Color.White
        Me.BtnPolicy.Location = New System.Drawing.Point(117, 158)
        Me.BtnPolicy.Name = "BtnPolicy"
        Me.BtnPolicy.Size = New System.Drawing.Size(110, 35)
        Me.BtnPolicy.TabIndex = 201
        Me.BtnPolicy.Text = "确定 (&K)"
        Me.BtnPolicy.UseVisualStyleBackColor = False
        '
        'TxtPolicy
        '
        Me.TxtPolicy.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TxtPolicy.Location = New System.Drawing.Point(12, 11)
        Me.TxtPolicy.MaxLength = 65535
        Me.TxtPolicy.Multiline = True
        Me.TxtPolicy.Name = "TxtPolicy"
        Me.TxtPolicy.Size = New System.Drawing.Size(320, 140)
        Me.TxtPolicy.TabIndex = 101
        '
        'Policy
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.MediumVioletRed
        Me.ClientSize = New System.Drawing.Size(344, 201)
        Me.Controls.Add(Me.BtnPolicy)
        Me.Controls.Add(Me.TxtPolicy)
        Me.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Policy"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "下载配置文件密钥 (ASCII)"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnPolicy As Button
    Friend WithEvents TxtPolicy As TextBox
End Class
