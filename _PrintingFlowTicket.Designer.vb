<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class _PrintingFlowTicket
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
        Me.btn_Print = New System.Windows.Forms.Button()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.No2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Operator2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ID2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Process2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Check_Mark2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Defect_Qty2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Component_PN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txt_Qty_per_Lot = New System.Windows.Forms.TextBox()
        Me.txt_Line_No = New System.Windows.Forms.TextBox()
        Me.txt_Lot_No = New System.Windows.Forms.TextBox()
        Me.txt_date_code = New System.Windows.Forms.TextBox()
        Me.txt_Quantity_PO = New System.Windows.Forms.TextBox()
        Me.txt_part_description = New System.Windows.Forms.TextBox()
        Me.txt_PO_Number = New System.Windows.Forms.TextBox()
        Me.txt_QR_Code = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_fg_part_number = New System.Windows.Forms.TextBox()
        Me.btn_Refresh = New System.Windows.Forms.Button()
        Me.txt_compress = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_Print
        '
        Me.btn_Print.Location = New System.Drawing.Point(360, 551)
        Me.btn_Print.Name = "btn_Print"
        Me.btn_Print.Size = New System.Drawing.Size(75, 23)
        Me.btn_Print.TabIndex = 24
        Me.btn_Print.Text = "Print..."
        Me.btn_Print.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No2, Me.Operator2, Me.ID2, Me.Process2, Me.Check_Mark2, Me.Defect_Qty2})
        Me.DataGridView2.Location = New System.Drawing.Point(25, 300)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(410, 111)
        Me.DataGridView2.TabIndex = 23
        '
        'No2
        '
        Me.No2.HeaderText = "No"
        Me.No2.Name = "No2"
        '
        'Operator2
        '
        Me.Operator2.HeaderText = "Operator"
        Me.Operator2.Name = "Operator2"
        '
        'ID2
        '
        Me.ID2.HeaderText = "ID"
        Me.ID2.Name = "ID2"
        '
        'Process2
        '
        Me.Process2.HeaderText = "Process"
        Me.Process2.Name = "Process2"
        '
        'Check_Mark2
        '
        Me.Check_Mark2.HeaderText = "Check Mark"
        Me.Check_Mark2.Name = "Check_Mark2"
        '
        'Defect_Qty2
        '
        Me.Defect_Qty2.HeaderText = "Defect Qty"
        Me.Defect_Qty2.Name = "Defect_Qty2"
        '
        'DataGridView1
        '
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.Component_PN, Me.Description})
        Me.DataGridView1.Location = New System.Drawing.Point(25, 158)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(410, 122)
        Me.DataGridView1.TabIndex = 22
        '
        'No
        '
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        '
        'Component_PN
        '
        Me.Component_PN.HeaderText = "Component PN"
        Me.Component_PN.Name = "Component_PN"
        '
        'Description
        '
        Me.Description.HeaderText = "Description"
        Me.Description.Name = "Description"
        '
        'txt_Qty_per_Lot
        '
        Me.txt_Qty_per_Lot.Location = New System.Drawing.Point(335, 81)
        Me.txt_Qty_per_Lot.Name = "txt_Qty_per_Lot"
        Me.txt_Qty_per_Lot.Size = New System.Drawing.Size(100, 20)
        Me.txt_Qty_per_Lot.TabIndex = 20
        '
        'txt_Line_No
        '
        Me.txt_Line_No.Location = New System.Drawing.Point(112, 84)
        Me.txt_Line_No.Name = "txt_Line_No"
        Me.txt_Line_No.Size = New System.Drawing.Size(100, 20)
        Me.txt_Line_No.TabIndex = 19
        '
        'txt_Lot_No
        '
        Me.txt_Lot_No.Location = New System.Drawing.Point(335, 105)
        Me.txt_Lot_No.Name = "txt_Lot_No"
        Me.txt_Lot_No.Size = New System.Drawing.Size(100, 20)
        Me.txt_Lot_No.TabIndex = 18
        '
        'txt_date_code
        '
        Me.txt_date_code.Location = New System.Drawing.Point(112, 108)
        Me.txt_date_code.Name = "txt_date_code"
        Me.txt_date_code.Size = New System.Drawing.Size(100, 20)
        Me.txt_date_code.TabIndex = 17
        '
        'txt_Quantity_PO
        '
        Me.txt_Quantity_PO.Location = New System.Drawing.Point(335, 57)
        Me.txt_Quantity_PO.Name = "txt_Quantity_PO"
        Me.txt_Quantity_PO.Size = New System.Drawing.Size(100, 20)
        Me.txt_Quantity_PO.TabIndex = 16
        '
        'txt_part_description
        '
        Me.txt_part_description.Location = New System.Drawing.Point(112, 60)
        Me.txt_part_description.Name = "txt_part_description"
        Me.txt_part_description.Size = New System.Drawing.Size(100, 20)
        Me.txt_part_description.TabIndex = 21
        '
        'txt_PO_Number
        '
        Me.txt_PO_Number.Location = New System.Drawing.Point(335, 33)
        Me.txt_PO_Number.Name = "txt_PO_Number"
        Me.txt_PO_Number.Size = New System.Drawing.Size(100, 20)
        Me.txt_PO_Number.TabIndex = 15
        '
        'txt_QR_Code
        '
        Me.txt_QR_Code.Location = New System.Drawing.Point(79, 554)
        Me.txt_QR_Code.Name = "txt_QR_Code"
        Me.txt_QR_Code.Size = New System.Drawing.Size(255, 20)
        Me.txt_QR_Code.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(245, 109)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 13)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Lot No"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(172, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(117, 24)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Flow Ticket"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(245, 85)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 13)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "Qty per Lot"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(22, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Date Code"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(245, 61)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Quantity PO"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Line No"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(245, 37)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "PO Number"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Part Description"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 557)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "QR Code"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(22, 37)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(83, 13)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "FG Part Number"
        '
        'txt_fg_part_number
        '
        Me.txt_fg_part_number.Location = New System.Drawing.Point(112, 37)
        Me.txt_fg_part_number.Name = "txt_fg_part_number"
        Me.txt_fg_part_number.Size = New System.Drawing.Size(100, 20)
        Me.txt_fg_part_number.TabIndex = 21
        '
        'btn_Refresh
        '
        Me.btn_Refresh.Location = New System.Drawing.Point(360, 580)
        Me.btn_Refresh.Name = "btn_Refresh"
        Me.btn_Refresh.Size = New System.Drawing.Size(75, 23)
        Me.btn_Refresh.TabIndex = 25
        Me.btn_Refresh.Text = "Refresh"
        Me.btn_Refresh.UseVisualStyleBackColor = True
        '
        'txt_compress
        '
        Me.txt_compress.Location = New System.Drawing.Point(25, 417)
        Me.txt_compress.Multiline = True
        Me.txt_compress.Name = "txt_compress"
        Me.txt_compress.Size = New System.Drawing.Size(410, 123)
        Me.txt_compress.TabIndex = 26
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(259, 580)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 27
        Me.Button1.Text = "Compress"
        Me.Button1.UseVisualStyleBackColor = True
        '
        '_PrintingFlowTicket
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(465, 613)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txt_compress)
        Me.Controls.Add(Me.btn_Refresh)
        Me.Controls.Add(Me.btn_Print)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.txt_Qty_per_Lot)
        Me.Controls.Add(Me.txt_Line_No)
        Me.Controls.Add(Me.txt_Lot_No)
        Me.Controls.Add(Me.txt_date_code)
        Me.Controls.Add(Me.txt_Quantity_PO)
        Me.Controls.Add(Me.txt_fg_part_number)
        Me.Controls.Add(Me.txt_part_description)
        Me.Controls.Add(Me.txt_PO_Number)
        Me.Controls.Add(Me.txt_QR_Code)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Name = "_PrintingFlowTicket"
        Me.Text = "_PrintingFlowTicket"
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_Print As Button
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents No2 As DataGridViewTextBoxColumn
    Friend WithEvents Operator2 As DataGridViewTextBoxColumn
    Friend WithEvents ID2 As DataGridViewTextBoxColumn
    Friend WithEvents Process2 As DataGridViewTextBoxColumn
    Friend WithEvents Check_Mark2 As DataGridViewTextBoxColumn
    Friend WithEvents Defect_Qty2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents No As DataGridViewTextBoxColumn
    Friend WithEvents Component_PN As DataGridViewTextBoxColumn
    Friend WithEvents Description As DataGridViewTextBoxColumn
    Friend WithEvents txt_Qty_per_Lot As TextBox
    Friend WithEvents txt_Line_No As TextBox
    Friend WithEvents txt_Lot_No As TextBox
    Friend WithEvents txt_date_code As TextBox
    Friend WithEvents txt_Quantity_PO As TextBox
    Friend WithEvents txt_part_description As TextBox
    Friend WithEvents txt_PO_Number As TextBox
    Friend WithEvents txt_QR_Code As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txt_fg_part_number As TextBox
    Friend WithEvents btn_Refresh As Button
    Friend WithEvents txt_compress As TextBox
    Friend WithEvents Button1 As Button
End Class
