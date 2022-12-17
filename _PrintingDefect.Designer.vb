<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class _PrintingDefect
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.txt_Part_Description = New System.Windows.Forms.TextBox()
        Me.txt_Traceability = New System.Windows.Forms.TextBox()
        Me.txt_Unique_id = New System.Windows.Forms.TextBox()
        Me.txt_part_number = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_Inv_crtl_date = New System.Windows.Forms.TextBox()
        Me.btn_Print = New System.Windows.Forms.Button()
        Me.txt_QR_Code = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btn_refresh = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txt_Part_Description
        '
        Me.txt_Part_Description.Location = New System.Drawing.Point(114, 111)
        Me.txt_Part_Description.Name = "txt_Part_Description"
        Me.txt_Part_Description.Size = New System.Drawing.Size(100, 20)
        Me.txt_Part_Description.TabIndex = 27
        '
        'txt_Traceability
        '
        Me.txt_Traceability.Location = New System.Drawing.Point(114, 135)
        Me.txt_Traceability.Name = "txt_Traceability"
        Me.txt_Traceability.Size = New System.Drawing.Size(100, 20)
        Me.txt_Traceability.TabIndex = 26
        '
        'txt_Unique_id
        '
        Me.txt_Unique_id.Location = New System.Drawing.Point(114, 64)
        Me.txt_Unique_id.Name = "txt_Unique_id"
        Me.txt_Unique_id.Size = New System.Drawing.Size(100, 20)
        Me.txt_Unique_id.TabIndex = 28
        '
        'txt_part_number
        '
        Me.txt_part_number.Location = New System.Drawing.Point(114, 87)
        Me.txt_part_number.Name = "txt_part_number"
        Me.txt_part_number.Size = New System.Drawing.Size(100, 20)
        Me.txt_part_number.TabIndex = 29
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(24, 139)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 13)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "Traceability"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 115)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Part Description"
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
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Part Number"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(83, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(131, 24)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Defect Ticket"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 165)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Inv Ctrl Date"
        '
        'txt_Inv_crtl_date
        '
        Me.txt_Inv_crtl_date.Location = New System.Drawing.Point(114, 161)
        Me.txt_Inv_crtl_date.Name = "txt_Inv_crtl_date"
        Me.txt_Inv_crtl_date.Size = New System.Drawing.Size(100, 20)
        Me.txt_Inv_crtl_date.TabIndex = 26
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
        Me.btn_refresh.Location = New System.Drawing.Point(257, 115)
        Me.btn_refresh.Name = "btn_refresh"
        Me.btn_refresh.Size = New System.Drawing.Size(75, 23)
        Me.btn_refresh.TabIndex = 34
        Me.btn_refresh.Text = "Refresh"
        Me.btn_refresh.UseVisualStyleBackColor = True
        '
        '_PrintingDefect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(349, 231)
        Me.Controls.Add(Me.btn_refresh)
        Me.Controls.Add(Me.txt_QR_Code)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btn_Print)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_Part_Description)
        Me.Controls.Add(Me.txt_Inv_crtl_date)
        Me.Controls.Add(Me.txt_Traceability)
        Me.Controls.Add(Me.txt_Unique_id)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_part_number)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label3)
        Me.Name = "_PrintingDefect"
        Me.Text = "_PrintingDefect"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txt_Part_Description As TextBox
    Friend WithEvents txt_Traceability As TextBox
    Friend WithEvents txt_Unique_id As TextBox
    Friend WithEvents txt_part_number As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_Inv_crtl_date As TextBox
    Friend WithEvents btn_Print As Button
    Friend WithEvents txt_QR_Code As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents btn_refresh As Button
End Class
