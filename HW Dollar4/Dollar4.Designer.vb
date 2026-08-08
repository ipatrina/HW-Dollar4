<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Dollar4
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Dollar4))
        Me.TxtD4 = New System.Windows.Forms.TextBox()
        Me.BtnD4 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TxtD4
        '
        Me.TxtD4.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TxtD4.Location = New System.Drawing.Point(12, 11)
        Me.TxtD4.MaxLength = 65535
        Me.TxtD4.Multiline = True
        Me.TxtD4.Name = "TxtD4"
        Me.TxtD4.Size = New System.Drawing.Size(320, 140)
        Me.TxtD4.TabIndex = 101
        '
        'BtnD4
        '
        Me.BtnD4.BackColor = System.Drawing.Color.DeepPink
        Me.BtnD4.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.BtnD4.ForeColor = System.Drawing.Color.White
        Me.BtnD4.Location = New System.Drawing.Point(117, 158)
        Me.BtnD4.Name = "BtnD4"
        Me.BtnD4.Size = New System.Drawing.Size(110, 35)
        Me.BtnD4.TabIndex = 201
        Me.BtnD4.Text = "确定 (&K)"
        Me.BtnD4.UseVisualStyleBackColor = False
        '
        'Dollar4
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.MediumVioletRed
        Me.ClientSize = New System.Drawing.Size(344, 201)
        Me.Controls.Add(Me.BtnD4)
        Me.Controls.Add(Me.TxtD4)
        Me.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Dollar4"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "UnVisible密钥 (Hex)"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtD4 As TextBox
    Friend WithEvents BtnD4 As Button
End Class
