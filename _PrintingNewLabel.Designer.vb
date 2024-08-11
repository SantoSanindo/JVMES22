<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class _PrintingNewLabel
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btn_Print = New System.Windows.Forms.Button()
        Me.txt_QR_Code = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btn_refresh = New System.Windows.Forms.Button()
        Me.txt_Unique_id = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(110, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 24)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "New Label"
        '
        'btn_Print
        '
        Me.btn_Print.Location = New System.Drawing.Point(257, 64)
        Me.btn_Print.Name = "btn_Print"
        Me.btn_Print.Size = New System.Drawing.Size(75, 23)
        Me.btn_Print.TabIndex = 31
        Me.btn_Print.Text = "Print..."
        Me.btn_Print.UseVisualStyleBackColor = True
        '
        'txt_QR_Code
        '
        Me.txt_QR_Code.Location = New System.Drawing.Point(114, 187)
        Me.txt_QR_Code.Name = "txt_QR_Code"
        Me.txt_QR_Code.Size = New System.Drawing.Size(218, 20)
        Me.txt_QR_Code.TabIndex = 33
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(25, 190)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 13)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "QR Code"
        '
        'btn_refresh
        '
        Me.btn_refresh.Location = New System.Drawing.Point(257, 113)
        Me.btn_refresh.Name = "btn_refresh"
        Me.btn_refresh.Size = New System.Drawing.Size(75, 23)
        Me.btn_refresh.TabIndex = 34
        Me.btn_refresh.Text = "Refresh"
        Me.btn_refresh.UseVisualStyleBackColor = True
        '
        'txt_Unique_id
        '
        Me.txt_Unique_id.Location = New System.Drawing.Point(114, 64)
        Me.txt_Unique_id.Name = "txt_Unique_id"
        Me.txt_Unique_id.Size = New System.Drawing.Size(100, 20)
        Me.txt_Unique_id.TabIndex = 28
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(24, 64)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(55, 13)
        Me.Label10.TabIndex = 24
        Me.Label10.Text = "Unique ID"
        '
        '_PrintingNewLabel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(349, 231)
        Me.Controls.Add(Me.btn_refresh)
        Me.Controls.Add(Me.txt_QR_Code)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btn_Print)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_Unique_id)
        Me.Controls.Add(Me.Label10)
        Me.Name = "_PrintingNewLabel"
        Me.Text = "_PrintingNewLabel"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As Label
    Friend WithEvents btn_Print As Button
    Friend WithEvents txt_QR_Code As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents btn_refresh As Button
    Friend WithEvents txt_Unique_id As TextBox
    Friend WithEvents Label10 As Label
End Class
