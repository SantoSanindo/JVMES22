<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormDefectiveV2
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtFTQty = New System.Windows.Forms.TextBox()
        Me.txtSAPQty = New System.Windows.Forms.TextBox()
        Me.txtFTLot = New System.Windows.Forms.TextBox()
        Me.txtSAPLot = New System.Windows.Forms.TextBox()
        Me.btnListPrintOthers = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.btnListPrintReturn = New System.Windows.Forms.Button()
        Me.btnListPrintOnHold = New System.Windows.Forms.Button()
        Me.PrintDefect = New System.Windows.Forms.Button()
        Me.btnPrintSA = New System.Windows.Forms.Button()
        Me.btnListPrintWIP = New System.Windows.Forms.Button()
        Me.txtStatusSubSubPO = New System.Windows.Forms.TextBox()
        Me.txtSPQ = New System.Windows.Forms.TextBox()
        Me.cbFGPN = New System.Windows.Forms.TextBox()
        Me.txtDescDefective = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.btnPrintFGDefect = New System.Windows.Forms.Button()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtSubSubPODefective = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cbPONumber = New System.Windows.Forms.TextBox()
        Me.txtTampungFlow = New System.Windows.Forms.TextBox()
        Me.txtTampungLabel = New System.Windows.Forms.TextBox()
        Me.txtINV = New System.Windows.Forms.TextBox()
        Me.txtBatchno = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbLineNumber = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.btnWIPAdd = New System.Windows.Forms.Button()
        Me.txtWIPQuantity = New System.Windows.Forms.TextBox()
        Me.txtWIPTicketNo = New System.Windows.Forms.TextBox()
        Me.cbWIPProcess = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tpRejectMaterial = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel16 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel17 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel18 = New System.Windows.Forms.TableLayoutPanel()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.txtRejectBarcode = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel19 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtRejectMaterialManual = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel20 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.CheckManualRejectMaterial = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.txtRejectQty = New System.Windows.Forms.TextBox()
        Me.btnRejectSave = New System.Windows.Forms.Button()
        Me.txtRejectMaterialPN = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.btnRejectDelete = New System.Windows.Forms.Button()
        Me.tampungIDMaterial = New System.Windows.Forms.TextBox()
        Me.dgReject = New System.Windows.Forms.DataGridView()
        Me.tpWIP = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgWIP = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnPrintWIP = New System.Windows.Forms.Button()
        Me.TextBox11 = New System.Windows.Forms.TextBox()
        Me.btnWIPDelete = New System.Windows.Forms.Button()
        Me.tpOnHold = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel13 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgOnHold = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel15 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtOnHoldQty = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtOnHoldTicketNo = New System.Windows.Forms.TextBox()
        Me.cbOnHoldProcess = New System.Windows.Forms.ComboBox()
        Me.btnPrintOnhold = New System.Windows.Forms.Button()
        Me.TextBox12 = New System.Windows.Forms.TextBox()
        Me.btnOnHoldDelete = New System.Windows.Forms.Button()
        Me.btnOnHoldSave = New System.Windows.Forms.Button()
        Me.tpFinishGoods = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.DataGridView3 = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnResetFG = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtFGLabel = New System.Windows.Forms.TextBox()
        Me.txtFGFlowTicket = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.CheckBoxFGDefect = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnResetSA = New System.Windows.Forms.Button()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtSAFlowTicket = New System.Windows.Forms.TextBox()
        Me.txtSABatchNo = New System.Windows.Forms.TextBox()
        Me.ckLossQty = New System.Windows.Forms.CheckBox()
        Me.txtLossQty = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSaveFGDefect = New System.Windows.Forms.Button()
        Me.btnSaveFG = New System.Windows.Forms.Button()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSaveSADefect = New System.Windows.Forms.Button()
        Me.btnSaveSA = New System.Windows.Forms.Button()
        Me.TableLayoutPanel23 = New System.Windows.Forms.TableLayoutPanel()
        Me.rbFG = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel24 = New System.Windows.Forms.TableLayoutPanel()
        Me.rbSA = New System.Windows.Forms.RadioButton()
        Me.tpBalance = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBox10 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnBalanceAdd = New System.Windows.Forms.Button()
        Me.txtBalanceQty = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtBalanceBarcode = New System.Windows.Forms.TextBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.CheckManualReturnMaterial = New System.Windows.Forms.Button()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel14 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtReturnMaterialManual = New System.Windows.Forms.TextBox()
        Me.txtBalanceMaterialPN = New System.Windows.Forms.TextBox()
        Me.txtReturnMaterialPN = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel22 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnBalanceEdit = New System.Windows.Forms.Button()
        Me.btnBalanceDelete = New System.Windows.Forms.Button()
        Me.btnPrintBalance = New System.Windows.Forms.Button()
        Me.tampungIDMaterialReturnMaterial = New System.Windows.Forms.TextBox()
        Me.dgBalance = New System.Windows.Forms.DataGridView()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tpRejectMaterial.SuspendLayout()
        Me.TableLayoutPanel16.SuspendLayout()
        Me.TableLayoutPanel17.SuspendLayout()
        Me.TableLayoutPanel18.SuspendLayout()
        Me.TableLayoutPanel19.SuspendLayout()
        Me.TableLayoutPanel20.SuspendLayout()
        CType(Me.dgReject, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpWIP.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.dgWIP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.tpOnHold.SuspendLayout()
        Me.TableLayoutPanel13.SuspendLayout()
        CType(Me.dgOnHold, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel15.SuspendLayout()
        Me.tpFinishGoods.SuspendLayout()
        Me.TableLayoutPanel12.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        Me.TableLayoutPanel11.SuspendLayout()
        Me.TableLayoutPanel23.SuspendLayout()
        Me.TableLayoutPanel24.SuspendLayout()
        Me.tpBalance.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel14.SuspendLayout()
        Me.TableLayoutPanel22.SuspendLayout()
        CType(Me.dgBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtFTQty)
        Me.GroupBox1.Controls.Add(Me.txtSAPQty)
        Me.GroupBox1.Controls.Add(Me.txtFTLot)
        Me.GroupBox1.Controls.Add(Me.txtSAPLot)
        Me.GroupBox1.Controls.Add(Me.btnListPrintOthers)
        Me.GroupBox1.Controls.Add(Me.TextBox2)
        Me.GroupBox1.Controls.Add(Me.btnListPrintReturn)
        Me.GroupBox1.Controls.Add(Me.btnListPrintOnHold)
        Me.GroupBox1.Controls.Add(Me.PrintDefect)
        Me.GroupBox1.Controls.Add(Me.btnPrintSA)
        Me.GroupBox1.Controls.Add(Me.btnListPrintWIP)
        Me.GroupBox1.Controls.Add(Me.txtStatusSubSubPO)
        Me.GroupBox1.Controls.Add(Me.txtSPQ)
        Me.GroupBox1.Controls.Add(Me.cbFGPN)
        Me.GroupBox1.Controls.Add(Me.txtDescDefective)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.btnPrintFGDefect)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.txtSubSubPODefective)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.cbPONumber)
        Me.GroupBox1.Controls.Add(Me.txtTampungFlow)
        Me.GroupBox1.Controls.Add(Me.txtTampungLabel)
        Me.GroupBox1.Controls.Add(Me.txtINV)
        Me.GroupBox1.Controls.Add(Me.txtBatchno)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cbLineNumber)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1924, 169)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtFTQty
        '
        Me.txtFTQty.Location = New System.Drawing.Point(1705, 62)
        Me.txtFTQty.Name = "txtFTQty"
        Me.txtFTQty.Size = New System.Drawing.Size(100, 20)
        Me.txtFTQty.TabIndex = 36
        Me.txtFTQty.Visible = False
        '
        'txtSAPQty
        '
        Me.txtSAPQty.Location = New System.Drawing.Point(1579, 62)
        Me.txtSAPQty.Name = "txtSAPQty"
        Me.txtSAPQty.Size = New System.Drawing.Size(100, 20)
        Me.txtSAPQty.TabIndex = 35
        Me.txtSAPQty.Visible = False
        '
        'txtFTLot
        '
        Me.txtFTLot.Location = New System.Drawing.Point(1705, 19)
        Me.txtFTLot.Name = "txtFTLot"
        Me.txtFTLot.Size = New System.Drawing.Size(100, 20)
        Me.txtFTLot.TabIndex = 34
        Me.txtFTLot.Visible = False
        '
        'txtSAPLot
        '
        Me.txtSAPLot.Location = New System.Drawing.Point(1579, 19)
        Me.txtSAPLot.Name = "txtSAPLot"
        Me.txtSAPLot.Size = New System.Drawing.Size(100, 20)
        Me.txtSAPLot.TabIndex = 33
        Me.txtSAPLot.Visible = False
        '
        'btnListPrintOthers
        '
        Me.btnListPrintOthers.BackColor = System.Drawing.Color.SkyBlue
        Me.btnListPrintOthers.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnListPrintOthers.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnListPrintOthers.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnListPrintOthers.Location = New System.Drawing.Point(1529, 112)
        Me.btnListPrintOthers.Name = "btnListPrintOthers"
        Me.btnListPrintOthers.Size = New System.Drawing.Size(199, 41)
        Me.btnListPrintOthers.TabIndex = 32
        Me.btnListPrintOthers.Text = "Print Others Part"
        Me.btnListPrintOthers.UseVisualStyleBackColor = False
        Me.btnListPrintOthers.Visible = False
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TextBox2.Location = New System.Drawing.Point(1370, 71)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(179, 20)
        Me.TextBox2.TabIndex = 3
        Me.TextBox2.Visible = False
        '
        'btnListPrintReturn
        '
        Me.btnListPrintReturn.BackColor = System.Drawing.Color.SkyBlue
        Me.btnListPrintReturn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnListPrintReturn.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnListPrintReturn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnListPrintReturn.Location = New System.Drawing.Point(1274, 112)
        Me.btnListPrintReturn.Name = "btnListPrintReturn"
        Me.btnListPrintReturn.Size = New System.Drawing.Size(240, 41)
        Me.btnListPrintReturn.TabIndex = 31
        Me.btnListPrintReturn.Text = "Print Return Material"
        Me.btnListPrintReturn.UseVisualStyleBackColor = False
        '
        'btnListPrintOnHold
        '
        Me.btnListPrintOnHold.BackColor = System.Drawing.Color.SkyBlue
        Me.btnListPrintOnHold.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnListPrintOnHold.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnListPrintOnHold.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnListPrintOnHold.Location = New System.Drawing.Point(865, 112)
        Me.btnListPrintOnHold.Name = "btnListPrintOnHold"
        Me.btnListPrintOnHold.Size = New System.Drawing.Size(172, 41)
        Me.btnListPrintOnHold.TabIndex = 30
        Me.btnListPrintOnHold.Text = "Print On Hold"
        Me.btnListPrintOnHold.UseVisualStyleBackColor = False
        '
        'PrintDefect
        '
        Me.PrintDefect.BackColor = System.Drawing.Color.SkyBlue
        Me.PrintDefect.Enabled = False
        Me.PrintDefect.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.PrintDefect.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PrintDefect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.PrintDefect.Location = New System.Drawing.Point(1345, 16)
        Me.PrintDefect.Name = "PrintDefect"
        Me.PrintDefect.Size = New System.Drawing.Size(199, 41)
        Me.PrintDefect.TabIndex = 29
        Me.PrintDefect.Text = "Print Defect"
        Me.PrintDefect.UseVisualStyleBackColor = False
        '
        'btnPrintSA
        '
        Me.btnPrintSA.BackColor = System.Drawing.Color.SkyBlue
        Me.btnPrintSA.Enabled = False
        Me.btnPrintSA.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPrintSA.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintSA.Location = New System.Drawing.Point(1057, 112)
        Me.btnPrintSA.Name = "btnPrintSA"
        Me.btnPrintSA.Size = New System.Drawing.Size(199, 41)
        Me.btnPrintSA.TabIndex = 8
        Me.btnPrintSA.Text = "Print Sub Assy"
        Me.btnPrintSA.UseVisualStyleBackColor = False
        '
        'btnListPrintWIP
        '
        Me.btnListPrintWIP.BackColor = System.Drawing.Color.SkyBlue
        Me.btnListPrintWIP.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnListPrintWIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnListPrintWIP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnListPrintWIP.Location = New System.Drawing.Point(696, 112)
        Me.btnListPrintWIP.Name = "btnListPrintWIP"
        Me.btnListPrintWIP.Size = New System.Drawing.Size(151, 41)
        Me.btnListPrintWIP.TabIndex = 6
        Me.btnListPrintWIP.Text = "Print WIP"
        Me.btnListPrintWIP.UseVisualStyleBackColor = False
        '
        'txtStatusSubSubPO
        '
        Me.txtStatusSubSubPO.Location = New System.Drawing.Point(14, 147)
        Me.txtStatusSubSubPO.Name = "txtStatusSubSubPO"
        Me.txtStatusSubSubPO.Size = New System.Drawing.Size(100, 20)
        Me.txtStatusSubSubPO.TabIndex = 26
        Me.txtStatusSubSubPO.Visible = False
        '
        'txtSPQ
        '
        Me.txtSPQ.Location = New System.Drawing.Point(120, 147)
        Me.txtSPQ.Name = "txtSPQ"
        Me.txtSPQ.Size = New System.Drawing.Size(100, 20)
        Me.txtSPQ.TabIndex = 24
        Me.txtSPQ.Visible = False
        '
        'cbFGPN
        '
        Me.cbFGPN.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFGPN.Location = New System.Drawing.Point(943, 19)
        Me.cbFGPN.Name = "cbFGPN"
        Me.cbFGPN.ReadOnly = True
        Me.cbFGPN.Size = New System.Drawing.Size(359, 35)
        Me.cbFGPN.TabIndex = 23
        '
        'txtDescDefective
        '
        Me.txtDescDefective.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescDefective.Location = New System.Drawing.Point(943, 62)
        Me.txtDescDefective.Name = "txtDescDefective"
        Me.txtDescDefective.ReadOnly = True
        Me.txtDescDefective.Size = New System.Drawing.Size(359, 35)
        Me.txtDescDefective.TabIndex = 22
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(902, 65)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(19, 29)
        Me.Label25.TabIndex = 21
        Me.Label25.Text = ":"
        '
        'btnPrintFGDefect
        '
        Me.btnPrintFGDefect.BackColor = System.Drawing.Color.SkyBlue
        Me.btnPrintFGDefect.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPrintFGDefect.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintFGDefect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnPrintFGDefect.Location = New System.Drawing.Point(1709, 113)
        Me.btnPrintFGDefect.Name = "btnPrintFGDefect"
        Me.btnPrintFGDefect.Size = New System.Drawing.Size(199, 39)
        Me.btnPrintFGDefect.TabIndex = 5
        Me.btnPrintFGDefect.Text = "Print Defect"
        Me.btnPrintFGDefect.UseVisualStyleBackColor = False
        Me.btnPrintFGDefect.Visible = False
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(691, 65)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(135, 29)
        Me.Label26.TabIndex = 20
        Me.Label26.Text = "Description"
        '
        'txtSubSubPODefective
        '
        Me.txtSubSubPODefective.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubSubPODefective.Location = New System.Drawing.Point(285, 19)
        Me.txtSubSubPODefective.Name = "txtSubSubPODefective"
        Me.txtSubSubPODefective.Size = New System.Drawing.Size(359, 35)
        Me.txtSubSubPODefective.TabIndex = 19
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(244, 107)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(19, 29)
        Me.Label17.TabIndex = 18
        Me.Label17.Text = ":"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(33, 22)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(146, 29)
        Me.Label24.TabIndex = 17
        Me.Label24.Text = "Sub Sub PO"
        '
        'cbPONumber
        '
        Me.cbPONumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPONumber.Location = New System.Drawing.Point(285, 62)
        Me.cbPONumber.Name = "cbPONumber"
        Me.cbPONumber.ReadOnly = True
        Me.cbPONumber.Size = New System.Drawing.Size(359, 35)
        Me.cbPONumber.TabIndex = 16
        '
        'txtTampungFlow
        '
        Me.txtTampungFlow.Location = New System.Drawing.Point(544, 147)
        Me.txtTampungFlow.Name = "txtTampungFlow"
        Me.txtTampungFlow.Size = New System.Drawing.Size(100, 20)
        Me.txtTampungFlow.TabIndex = 15
        Me.txtTampungFlow.Visible = False
        '
        'txtTampungLabel
        '
        Me.txtTampungLabel.Location = New System.Drawing.Point(438, 147)
        Me.txtTampungLabel.Name = "txtTampungLabel"
        Me.txtTampungLabel.Size = New System.Drawing.Size(100, 20)
        Me.txtTampungLabel.TabIndex = 14
        Me.txtTampungLabel.Visible = False
        '
        'txtINV
        '
        Me.txtINV.Location = New System.Drawing.Point(332, 147)
        Me.txtINV.Name = "txtINV"
        Me.txtINV.Size = New System.Drawing.Size(100, 20)
        Me.txtINV.TabIndex = 13
        Me.txtINV.Visible = False
        '
        'txtBatchno
        '
        Me.txtBatchno.Location = New System.Drawing.Point(226, 147)
        Me.txtBatchno.Name = "txtBatchno"
        Me.txtBatchno.Size = New System.Drawing.Size(100, 20)
        Me.txtBatchno.TabIndex = 12
        Me.txtBatchno.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(902, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(19, 29)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = ":"
        '
        'cbLineNumber
        '
        Me.cbLineNumber.Enabled = False
        Me.cbLineNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbLineNumber.FormattingEnabled = True
        Me.cbLineNumber.Items.AddRange(New Object() {"Line 1", "Line 2", "Line 3"})
        Me.cbLineNumber.Location = New System.Drawing.Point(285, 104)
        Me.cbLineNumber.Name = "cbLineNumber"
        Me.cbLineNumber.Size = New System.Drawing.Size(359, 37)
        Me.cbLineNumber.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(244, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(19, 29)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = ":"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(244, 65)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(19, 29)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = ":"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(691, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(196, 29)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Finish Goods PN"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(33, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(179, 29)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "PO/SA Number"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(33, 107)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(152, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Line Number"
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Checked = True
        Me.CheckBox5.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBox5.Enabled = False
        Me.CheckBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox5.Location = New System.Drawing.Point(678, 126)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(265, 37)
        Me.CheckBox5.TabIndex = 28
        Me.CheckBox5.Text = "Auto Print Sub Assy"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'btnWIPAdd
        '
        Me.btnWIPAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnWIPAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnWIPAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnWIPAdd.Location = New System.Drawing.Point(813, 122)
        Me.btnWIPAdd.Name = "btnWIPAdd"
        Me.btnWIPAdd.Size = New System.Drawing.Size(162, 48)
        Me.btnWIPAdd.TabIndex = 7
        Me.btnWIPAdd.Text = "Save"
        Me.btnWIPAdd.UseVisualStyleBackColor = False
        '
        'txtWIPQuantity
        '
        Me.txtWIPQuantity.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtWIPQuantity.Location = New System.Drawing.Point(153, 128)
        Me.txtWIPQuantity.Name = "txtWIPQuantity"
        Me.txtWIPQuantity.Size = New System.Drawing.Size(519, 35)
        Me.txtWIPQuantity.TabIndex = 6
        '
        'txtWIPTicketNo
        '
        Me.txtWIPTicketNo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtWIPTicketNo.Location = New System.Drawing.Point(153, 72)
        Me.txtWIPTicketNo.Name = "txtWIPTicketNo"
        Me.txtWIPTicketNo.Size = New System.Drawing.Size(519, 35)
        Me.txtWIPTicketNo.TabIndex = 5
        '
        'cbWIPProcess
        '
        Me.cbWIPProcess.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbWIPProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbWIPProcess.FormattingEnabled = True
        Me.cbWIPProcess.Location = New System.Drawing.Point(153, 11)
        Me.cbWIPProcess.Name = "cbWIPProcess"
        Me.cbWIPProcess.Size = New System.Drawing.Size(519, 37)
        Me.cbWIPProcess.TabIndex = 4
        '
        'Label12
        '
        Me.Label12.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(3, 15)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(101, 29)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Process"
        '
        'Label13
        '
        Me.Label13.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(3, 75)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(138, 29)
        Me.Label13.TabIndex = 1
        Me.Label13.Text = "Flow Ticket"
        '
        'Label14
        '
        Me.Label14.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(3, 131)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(100, 29)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "Quantity"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TabControl1)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 169)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1924, 642)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tpRejectMaterial)
        Me.TabControl1.Controls.Add(Me.tpWIP)
        Me.TabControl1.Controls.Add(Me.tpOnHold)
        Me.TabControl1.Controls.Add(Me.tpFinishGoods)
        Me.TabControl1.Controls.Add(Me.tpBalance)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(3, 16)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1918, 623)
        Me.TabControl1.TabIndex = 0
        '
        'tpRejectMaterial
        '
        Me.tpRejectMaterial.Controls.Add(Me.TableLayoutPanel16)
        Me.tpRejectMaterial.Location = New System.Drawing.Point(4, 38)
        Me.tpRejectMaterial.Name = "tpRejectMaterial"
        Me.tpRejectMaterial.Size = New System.Drawing.Size(1910, 581)
        Me.tpRejectMaterial.TabIndex = 5
        Me.tpRejectMaterial.Text = "Reject Material"
        Me.tpRejectMaterial.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel16
        '
        Me.TableLayoutPanel16.ColumnCount = 1
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel16.Controls.Add(Me.TableLayoutPanel17, 0, 0)
        Me.TableLayoutPanel16.Controls.Add(Me.dgReject, 0, 1)
        Me.TableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel16.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel16.Name = "TableLayoutPanel16"
        Me.TableLayoutPanel16.RowCount = 2
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.97824!))
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.02176!))
        Me.TableLayoutPanel16.Size = New System.Drawing.Size(1910, 581)
        Me.TableLayoutPanel16.TabIndex = 0
        '
        'TableLayoutPanel17
        '
        Me.TableLayoutPanel17.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanel17.ColumnCount = 7
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.0!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.59951!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.39072!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.197802!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.31502!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.09768!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.68987!))
        Me.TableLayoutPanel17.Controls.Add(Me.Label23, 1, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.Label27, 1, 1)
        Me.TableLayoutPanel17.Controls.Add(Me.TableLayoutPanel18, 2, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.TableLayoutPanel19, 4, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.TableLayoutPanel20, 5, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.txtRejectQty, 2, 1)
        Me.TableLayoutPanel17.Controls.Add(Me.btnRejectSave, 4, 1)
        Me.TableLayoutPanel17.Controls.Add(Me.txtRejectMaterialPN, 0, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.TextBox4, 0, 1)
        Me.TableLayoutPanel17.Controls.Add(Me.TextBox5, 3, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.btnRejectDelete, 5, 1)
        Me.TableLayoutPanel17.Controls.Add(Me.tampungIDMaterial, 3, 1)
        Me.TableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel17.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel17.Name = "TableLayoutPanel17"
        Me.TableLayoutPanel17.RowCount = 2
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel17.Size = New System.Drawing.Size(1904, 121)
        Me.TableLayoutPanel17.TabIndex = 0
        '
        'Label23
        '
        Me.Label23.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(40, 15)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(165, 29)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "Material Label"
        '
        'Label27
        '
        Me.Label27.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(40, 76)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(175, 29)
        Me.Label27.TabIndex = 1
        Me.Label27.Text = "Quantity Reject"
        '
        'TableLayoutPanel18
        '
        Me.TableLayoutPanel18.ColumnCount = 2
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel18.Controls.Add(Me.CheckBox3, 1, 0)
        Me.TableLayoutPanel18.Controls.Add(Me.txtRejectBarcode, 0, 0)
        Me.TableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel18.Location = New System.Drawing.Point(260, 3)
        Me.TableLayoutPanel18.Name = "TableLayoutPanel18"
        Me.TableLayoutPanel18.RowCount = 1
        Me.TableLayoutPanel18.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel18.Size = New System.Drawing.Size(381, 54)
        Me.TableLayoutPanel18.TabIndex = 2
        '
        'CheckBox3
        '
        Me.CheckBox3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Checked = True
        Me.CheckBox3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox3.Location = New System.Drawing.Point(193, 10)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(133, 33)
        Me.CheckBox3.TabIndex = 0
        Me.CheckBox3.Text = "QR Code"
        Me.CheckBox3.UseVisualStyleBackColor = True
        Me.CheckBox3.Visible = False
        '
        'txtRejectBarcode
        '
        Me.txtRejectBarcode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtRejectBarcode.Location = New System.Drawing.Point(3, 9)
        Me.txtRejectBarcode.Name = "txtRejectBarcode"
        Me.txtRejectBarcode.Size = New System.Drawing.Size(154, 35)
        Me.txtRejectBarcode.TabIndex = 1
        '
        'TableLayoutPanel19
        '
        Me.TableLayoutPanel19.ColumnCount = 2
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel19.Controls.Add(Me.Label28, 0, 0)
        Me.TableLayoutPanel19.Controls.Add(Me.txtRejectMaterialManual, 1, 0)
        Me.TableLayoutPanel19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel19.Location = New System.Drawing.Point(688, 3)
        Me.TableLayoutPanel19.Name = "TableLayoutPanel19"
        Me.TableLayoutPanel19.RowCount = 1
        Me.TableLayoutPanel19.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel19.Size = New System.Drawing.Size(341, 54)
        Me.TableLayoutPanel19.TabIndex = 3
        '
        'Label28
        '
        Me.Label28.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(3, 12)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(78, 29)
        Me.Label28.TabIndex = 0
        Me.Label28.Text = "Comp"
        Me.Label28.Visible = False
        '
        'txtRejectMaterialManual
        '
        Me.txtRejectMaterialManual.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtRejectMaterialManual.Location = New System.Drawing.Point(105, 9)
        Me.txtRejectMaterialManual.Name = "txtRejectMaterialManual"
        Me.txtRejectMaterialManual.Size = New System.Drawing.Size(233, 35)
        Me.txtRejectMaterialManual.TabIndex = 1
        Me.txtRejectMaterialManual.Visible = False
        '
        'TableLayoutPanel20
        '
        Me.TableLayoutPanel20.ColumnCount = 3
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.10309!))
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.10309!))
        Me.TableLayoutPanel20.Controls.Add(Me.Label29, 0, 0)
        Me.TableLayoutPanel20.Controls.Add(Me.CheckManualRejectMaterial, 2, 0)
        Me.TableLayoutPanel20.Controls.Add(Me.ComboBox1, 1, 0)
        Me.TableLayoutPanel20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel20.Location = New System.Drawing.Point(1035, 3)
        Me.TableLayoutPanel20.Name = "TableLayoutPanel20"
        Me.TableLayoutPanel20.RowCount = 1
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel20.Size = New System.Drawing.Size(565, 54)
        Me.TableLayoutPanel20.TabIndex = 4
        '
        'Label29
        '
        Me.Label29.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(3, 12)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(84, 29)
        Me.Label29.TabIndex = 0
        Me.Label29.Text = "Lot No"
        Me.Label29.Visible = False
        '
        'CheckManualRejectMaterial
        '
        Me.CheckManualRejectMaterial.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckManualRejectMaterial.Location = New System.Drawing.Point(397, 3)
        Me.CheckManualRejectMaterial.Name = "CheckManualRejectMaterial"
        Me.CheckManualRejectMaterial.Size = New System.Drawing.Size(140, 48)
        Me.CheckManualRejectMaterial.TabIndex = 2
        Me.CheckManualRejectMaterial.Text = "Check"
        Me.CheckManualRejectMaterial.UseVisualStyleBackColor = True
        Me.CheckManualRejectMaterial.Visible = False
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.DropDownWidth = 800
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(115, 16)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(276, 37)
        Me.ComboBox1.TabIndex = 3
        Me.ComboBox1.Visible = False
        '
        'txtRejectQty
        '
        Me.txtRejectQty.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtRejectQty.Location = New System.Drawing.Point(260, 73)
        Me.txtRejectQty.Name = "txtRejectQty"
        Me.txtRejectQty.Size = New System.Drawing.Size(321, 35)
        Me.txtRejectQty.TabIndex = 5
        '
        'btnRejectSave
        '
        Me.btnRejectSave.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnRejectSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRejectSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnRejectSave.Location = New System.Drawing.Point(688, 66)
        Me.btnRejectSave.Name = "btnRejectSave"
        Me.btnRejectSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnRejectSave.Size = New System.Drawing.Size(162, 49)
        Me.btnRejectSave.TabIndex = 6
        Me.btnRejectSave.Text = "Save"
        Me.btnRejectSave.UseVisualStyleBackColor = False
        '
        'txtRejectMaterialPN
        '
        Me.txtRejectMaterialPN.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtRejectMaterialPN.Location = New System.Drawing.Point(3, 12)
        Me.txtRejectMaterialPN.Name = "txtRejectMaterialPN"
        Me.txtRejectMaterialPN.Size = New System.Drawing.Size(26, 35)
        Me.txtRejectMaterialPN.TabIndex = 8
        Me.txtRejectMaterialPN.Visible = False
        '
        'TextBox4
        '
        Me.TextBox4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TextBox4.Location = New System.Drawing.Point(3, 73)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(26, 35)
        Me.TextBox4.TabIndex = 9
        Me.TextBox4.Visible = False
        '
        'TextBox5
        '
        Me.TextBox5.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TextBox5.Location = New System.Drawing.Point(647, 12)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(26, 35)
        Me.TextBox5.TabIndex = 10
        Me.TextBox5.Visible = False
        '
        'btnRejectDelete
        '
        Me.btnRejectDelete.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnRejectDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnRejectDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnRejectDelete.Location = New System.Drawing.Point(1035, 66)
        Me.btnRejectDelete.Name = "btnRejectDelete"
        Me.btnRejectDelete.Size = New System.Drawing.Size(160, 49)
        Me.btnRejectDelete.TabIndex = 8
        Me.btnRejectDelete.Text = "Delete"
        Me.btnRejectDelete.UseVisualStyleBackColor = False
        Me.btnRejectDelete.Visible = False
        '
        'tampungIDMaterial
        '
        Me.tampungIDMaterial.Location = New System.Drawing.Point(647, 63)
        Me.tampungIDMaterial.Name = "tampungIDMaterial"
        Me.tampungIDMaterial.Size = New System.Drawing.Size(35, 35)
        Me.tampungIDMaterial.TabIndex = 11
        Me.tampungIDMaterial.Visible = False
        '
        'dgReject
        '
        Me.dgReject.AllowUserToAddRows = False
        Me.dgReject.AllowUserToDeleteRows = False
        Me.dgReject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgReject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgReject.Location = New System.Drawing.Point(3, 130)
        Me.dgReject.Name = "dgReject"
        Me.dgReject.Size = New System.Drawing.Size(1904, 448)
        Me.dgReject.TabIndex = 1
        '
        'tpWIP
        '
        Me.tpWIP.BackColor = System.Drawing.SystemColors.Control
        Me.tpWIP.Controls.Add(Me.TableLayoutPanel5)
        Me.tpWIP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.tpWIP.Location = New System.Drawing.Point(4, 38)
        Me.tpWIP.Name = "tpWIP"
        Me.tpWIP.Size = New System.Drawing.Size(1910, 581)
        Me.tpWIP.TabIndex = 3
        Me.tpWIP.Text = "WIP"
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.dgWIP, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.TableLayoutPanel6, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 2
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.84291!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.15709!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(1910, 581)
        Me.TableLayoutPanel5.TabIndex = 0
        '
        'dgWIP
        '
        Me.dgWIP.AllowUserToAddRows = False
        Me.dgWIP.AllowUserToDeleteRows = False
        Me.dgWIP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgWIP.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgWIP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgWIP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgWIP.Location = New System.Drawing.Point(3, 182)
        Me.dgWIP.Name = "dgWIP"
        Me.dgWIP.Size = New System.Drawing.Size(1904, 396)
        Me.dgWIP.TabIndex = 1
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 6
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.31339!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.354226!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.30678!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.96195!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.06365!))
        Me.TableLayoutPanel6.Controls.Add(Me.Label12, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.txtWIPQuantity, 1, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.Label13, 0, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.Label14, 0, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.txtWIPTicketNo, 1, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.cbWIPProcess, 1, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.btnPrintWIP, 5, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.TextBox11, 2, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.btnWIPDelete, 4, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.btnWIPAdd, 3, 2)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 3
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.68208!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.10405!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.63584!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(1904, 173)
        Me.TableLayoutPanel6.TabIndex = 4
        '
        'btnPrintWIP
        '
        Me.btnPrintWIP.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnPrintWIP.BackColor = System.Drawing.Color.SkyBlue
        Me.btnPrintWIP.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPrintWIP.Location = New System.Drawing.Point(1739, 122)
        Me.btnPrintWIP.Name = "btnPrintWIP"
        Me.btnPrintWIP.Size = New System.Drawing.Size(162, 48)
        Me.btnPrintWIP.TabIndex = 9
        Me.btnPrintWIP.Text = "Print"
        Me.btnPrintWIP.UseVisualStyleBackColor = False
        '
        'TextBox11
        '
        Me.TextBox11.Location = New System.Drawing.Point(772, 3)
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.Size = New System.Drawing.Size(29, 35)
        Me.TextBox11.TabIndex = 11
        Me.TextBox11.Visible = False
        '
        'btnWIPDelete
        '
        Me.btnWIPDelete.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnWIPDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnWIPDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnWIPDelete.Location = New System.Drawing.Point(1169, 5)
        Me.btnWIPDelete.Name = "btnWIPDelete"
        Me.btnWIPDelete.Size = New System.Drawing.Size(162, 49)
        Me.btnWIPDelete.TabIndex = 10
        Me.btnWIPDelete.Text = "Delete"
        Me.btnWIPDelete.UseVisualStyleBackColor = False
        Me.btnWIPDelete.Visible = False
        '
        'tpOnHold
        '
        Me.tpOnHold.BackColor = System.Drawing.SystemColors.Control
        Me.tpOnHold.Controls.Add(Me.TableLayoutPanel13)
        Me.tpOnHold.Location = New System.Drawing.Point(4, 38)
        Me.tpOnHold.Name = "tpOnHold"
        Me.tpOnHold.Size = New System.Drawing.Size(1910, 581)
        Me.tpOnHold.TabIndex = 4
        Me.tpOnHold.Text = "On Hold"
        '
        'TableLayoutPanel13
        '
        Me.TableLayoutPanel13.ColumnCount = 1
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel13.Controls.Add(Me.dgOnHold, 0, 1)
        Me.TableLayoutPanel13.Controls.Add(Me.TableLayoutPanel15, 0, 0)
        Me.TableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel13.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel13.Name = "TableLayoutPanel13"
        Me.TableLayoutPanel13.RowCount = 2
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.84291!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.15709!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel13.Size = New System.Drawing.Size(1910, 581)
        Me.TableLayoutPanel13.TabIndex = 1
        '
        'dgOnHold
        '
        Me.dgOnHold.AllowUserToAddRows = False
        Me.dgOnHold.AllowUserToDeleteRows = False
        Me.dgOnHold.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgOnHold.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgOnHold.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgOnHold.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgOnHold.Location = New System.Drawing.Point(3, 182)
        Me.dgOnHold.Name = "dgOnHold"
        Me.dgOnHold.Size = New System.Drawing.Size(1904, 396)
        Me.dgOnHold.TabIndex = 1
        '
        'TableLayoutPanel15
        '
        Me.TableLayoutPanel15.ColumnCount = 6
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.29412!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.352942!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.49731!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.8172!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.25269!))
        Me.TableLayoutPanel15.Controls.Add(Me.Label9, 0, 0)
        Me.TableLayoutPanel15.Controls.Add(Me.txtOnHoldQty, 1, 2)
        Me.TableLayoutPanel15.Controls.Add(Me.Label10, 0, 1)
        Me.TableLayoutPanel15.Controls.Add(Me.Label15, 0, 2)
        Me.TableLayoutPanel15.Controls.Add(Me.txtOnHoldTicketNo, 1, 1)
        Me.TableLayoutPanel15.Controls.Add(Me.cbOnHoldProcess, 1, 0)
        Me.TableLayoutPanel15.Controls.Add(Me.btnPrintOnhold, 5, 2)
        Me.TableLayoutPanel15.Controls.Add(Me.TextBox12, 2, 0)
        Me.TableLayoutPanel15.Controls.Add(Me.btnOnHoldDelete, 4, 0)
        Me.TableLayoutPanel15.Controls.Add(Me.btnOnHoldSave, 3, 2)
        Me.TableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel15.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel15.Name = "TableLayoutPanel15"
        Me.TableLayoutPanel15.RowCount = 3
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.10405!))
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.94798!))
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.36994!))
        Me.TableLayoutPanel15.Size = New System.Drawing.Size(1904, 173)
        Me.TableLayoutPanel15.TabIndex = 4
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(3, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(101, 29)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Process"
        '
        'txtOnHoldQty
        '
        Me.txtOnHoldQty.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtOnHoldQty.Location = New System.Drawing.Point(153, 127)
        Me.txtOnHoldQty.Name = "txtOnHoldQty"
        Me.txtOnHoldQty.Size = New System.Drawing.Size(518, 35)
        Me.txtOnHoldQty.TabIndex = 6
        '
        'Label10
        '
        Me.Label10.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 73)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(138, 29)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "Flow Ticket"
        '
        'Label15
        '
        Me.Label15.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(3, 130)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(100, 29)
        Me.Label15.TabIndex = 2
        Me.Label15.Text = "Quantity"
        '
        'txtOnHoldTicketNo
        '
        Me.txtOnHoldTicketNo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtOnHoldTicketNo.Location = New System.Drawing.Point(153, 70)
        Me.txtOnHoldTicketNo.Name = "txtOnHoldTicketNo"
        Me.txtOnHoldTicketNo.Size = New System.Drawing.Size(518, 35)
        Me.txtOnHoldTicketNo.TabIndex = 5
        '
        'cbOnHoldProcess
        '
        Me.cbOnHoldProcess.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbOnHoldProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbOnHoldProcess.FormattingEnabled = True
        Me.cbOnHoldProcess.Location = New System.Drawing.Point(153, 11)
        Me.cbOnHoldProcess.Name = "cbOnHoldProcess"
        Me.cbOnHoldProcess.Size = New System.Drawing.Size(518, 37)
        Me.cbOnHoldProcess.TabIndex = 4
        '
        'btnPrintOnhold
        '
        Me.btnPrintOnhold.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnPrintOnhold.BackColor = System.Drawing.Color.SkyBlue
        Me.btnPrintOnhold.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPrintOnhold.Location = New System.Drawing.Point(1739, 120)
        Me.btnPrintOnhold.Name = "btnPrintOnhold"
        Me.btnPrintOnhold.Size = New System.Drawing.Size(162, 49)
        Me.btnPrintOnhold.TabIndex = 9
        Me.btnPrintOnhold.Text = "Print"
        Me.btnPrintOnhold.UseVisualStyleBackColor = False
        '
        'TextBox12
        '
        Me.TextBox12.Location = New System.Drawing.Point(770, 3)
        Me.TextBox12.Name = "TextBox12"
        Me.TextBox12.Size = New System.Drawing.Size(28, 35)
        Me.TextBox12.TabIndex = 11
        Me.TextBox12.Visible = False
        '
        'btnOnHoldDelete
        '
        Me.btnOnHoldDelete.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnOnHoldDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnOnHoldDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnOnHoldDelete.Location = New System.Drawing.Point(1169, 5)
        Me.btnOnHoldDelete.Name = "btnOnHoldDelete"
        Me.btnOnHoldDelete.Size = New System.Drawing.Size(162, 49)
        Me.btnOnHoldDelete.TabIndex = 10
        Me.btnOnHoldDelete.Text = "Delete"
        Me.btnOnHoldDelete.UseVisualStyleBackColor = False
        Me.btnOnHoldDelete.Visible = False
        '
        'btnOnHoldSave
        '
        Me.btnOnHoldSave.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnOnHoldSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnOnHoldSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnOnHoldSave.Location = New System.Drawing.Point(811, 120)
        Me.btnOnHoldSave.Name = "btnOnHoldSave"
        Me.btnOnHoldSave.Size = New System.Drawing.Size(162, 49)
        Me.btnOnHoldSave.TabIndex = 7
        Me.btnOnHoldSave.Text = "Save"
        Me.btnOnHoldSave.UseVisualStyleBackColor = False
        '
        'tpFinishGoods
        '
        Me.tpFinishGoods.BackColor = System.Drawing.SystemColors.Control
        Me.tpFinishGoods.Controls.Add(Me.TableLayoutPanel12)
        Me.tpFinishGoods.Location = New System.Drawing.Point(4, 38)
        Me.tpFinishGoods.Name = "tpFinishGoods"
        Me.tpFinishGoods.Padding = New System.Windows.Forms.Padding(3)
        Me.tpFinishGoods.Size = New System.Drawing.Size(1910, 581)
        Me.tpFinishGoods.TabIndex = 1
        Me.tpFinishGoods.Text = "Finish Goods / Sub Assy"
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.ColumnCount = 2
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel12.Controls.Add(Me.DataGridView1, 0, 2)
        Me.TableLayoutPanel12.Controls.Add(Me.DataGridView3, 1, 2)
        Me.TableLayoutPanel12.Controls.Add(Me.TableLayoutPanel8, 0, 1)
        Me.TableLayoutPanel12.Controls.Add(Me.TableLayoutPanel9, 1, 1)
        Me.TableLayoutPanel12.Controls.Add(Me.TableLayoutPanel10, 0, 3)
        Me.TableLayoutPanel12.Controls.Add(Me.TableLayoutPanel11, 1, 3)
        Me.TableLayoutPanel12.Controls.Add(Me.TableLayoutPanel23, 0, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.TableLayoutPanel24, 1, 0)
        Me.TableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 4
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.0!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(1904, 575)
        Me.TableLayoutPanel12.TabIndex = 0
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 232)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(946, 293)
        Me.DataGridView1.TabIndex = 1
        '
        'DataGridView3
        '
        Me.DataGridView3.AllowUserToAddRows = False
        Me.DataGridView3.AllowUserToDeleteRows = False
        Me.DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView3.Location = New System.Drawing.Point(955, 232)
        Me.DataGridView3.Name = "DataGridView3"
        Me.DataGridView3.Size = New System.Drawing.Size(946, 293)
        Me.DataGridView3.TabIndex = 2
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 4
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.btnResetFG, 3, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.Label20, 1, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.Label21, 1, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.txtFGLabel, 2, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.txtFGFlowTicket, 2, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.TextBox3, 2, 2)
        Me.TableLayoutPanel8.Controls.Add(Me.Label30, 1, 2)
        Me.TableLayoutPanel8.Controls.Add(Me.CheckBoxFGDefect, 3, 2)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(3, 60)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 3
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(946, 166)
        Me.TableLayoutPanel8.TabIndex = 6
        '
        'btnResetFG
        '
        Me.btnResetFG.BackColor = System.Drawing.SystemColors.Info
        Me.btnResetFG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnResetFG.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnResetFG.Location = New System.Drawing.Point(711, 3)
        Me.btnResetFG.Name = "btnResetFG"
        Me.btnResetFG.Size = New System.Drawing.Size(232, 49)
        Me.btnResetFG.TabIndex = 6
        Me.btnResetFG.Text = "Reset"
        Me.btnResetFG.UseVisualStyleBackColor = False
        '
        'Label20
        '
        Me.Label20.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(50, 68)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(216, 29)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "   Scan Flow Ticket"
        '
        'Label21
        '
        Me.Label21.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(50, 13)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(151, 29)
        Me.Label21.TabIndex = 1
        Me.Label21.Text = "   Scan Label"
        '
        'txtFGLabel
        '
        Me.txtFGLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFGLabel.Location = New System.Drawing.Point(333, 10)
        Me.txtFGLabel.Name = "txtFGLabel"
        Me.txtFGLabel.Size = New System.Drawing.Size(271, 35)
        Me.txtFGLabel.TabIndex = 2
        '
        'txtFGFlowTicket
        '
        Me.txtFGFlowTicket.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFGFlowTicket.Location = New System.Drawing.Point(333, 65)
        Me.txtFGFlowTicket.Name = "txtFGFlowTicket"
        Me.txtFGFlowTicket.Size = New System.Drawing.Size(271, 35)
        Me.txtFGFlowTicket.TabIndex = 3
        '
        'TextBox3
        '
        Me.TextBox3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TextBox3.Location = New System.Drawing.Point(333, 120)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(271, 35)
        Me.TextBox3.TabIndex = 4
        '
        'Label30
        '
        Me.Label30.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(50, 123)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(156, 29)
        Me.Label30.TabIndex = 7
        Me.Label30.Text = "   Laser Code"
        '
        'CheckBoxFGDefect
        '
        Me.CheckBoxFGDefect.AutoSize = True
        Me.CheckBoxFGDefect.Checked = True
        Me.CheckBoxFGDefect.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxFGDefect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBoxFGDefect.Location = New System.Drawing.Point(711, 113)
        Me.CheckBoxFGDefect.Name = "CheckBoxFGDefect"
        Me.CheckBoxFGDefect.Size = New System.Drawing.Size(232, 50)
        Me.CheckBoxFGDefect.TabIndex = 8
        Me.CheckBoxFGDefect.Text = "Auto Print Defect"
        Me.CheckBoxFGDefect.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 4
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.57505!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.54123!))
        Me.TableLayoutPanel9.Controls.Add(Me.btnResetSA, 3, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.Label31, 1, 2)
        Me.TableLayoutPanel9.Controls.Add(Me.Label22, 1, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.TextBox6, 2, 2)
        Me.TableLayoutPanel9.Controls.Add(Me.Label8, 1, 1)
        Me.TableLayoutPanel9.Controls.Add(Me.txtSAFlowTicket, 2, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.CheckBox5, 3, 3)
        Me.TableLayoutPanel9.Controls.Add(Me.txtSABatchNo, 2, 1)
        Me.TableLayoutPanel9.Controls.Add(Me.ckLossQty, 1, 3)
        Me.TableLayoutPanel9.Controls.Add(Me.txtLossQty, 2, 3)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(955, 60)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 4
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(946, 166)
        Me.TableLayoutPanel9.TabIndex = 7
        '
        'btnResetSA
        '
        Me.btnResetSA.BackColor = System.Drawing.SystemColors.Info
        Me.btnResetSA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnResetSA.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnResetSA.Location = New System.Drawing.Point(678, 3)
        Me.btnResetSA.Name = "btnResetSA"
        Me.btnResetSA.Size = New System.Drawing.Size(265, 35)
        Me.btnResetSA.TabIndex = 7
        Me.btnResetSA.Text = "Reset"
        Me.btnResetSA.UseVisualStyleBackColor = False
        '
        'Label31
        '
        Me.Label31.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(50, 88)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(156, 29)
        Me.Label31.TabIndex = 1
        Me.Label31.Text = "   Laser Code"
        '
        'Label22
        '
        Me.Label22.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(50, 6)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(216, 29)
        Me.Label22.TabIndex = 5
        Me.Label22.Text = "   Scan Flow Ticket"
        '
        'TextBox6
        '
        Me.TextBox6.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TextBox6.Location = New System.Drawing.Point(333, 85)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(271, 35)
        Me.TextBox6.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(50, 47)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(129, 29)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "   Batch No"
        '
        'txtSAFlowTicket
        '
        Me.txtSAFlowTicket.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtSAFlowTicket.Location = New System.Drawing.Point(333, 3)
        Me.txtSAFlowTicket.Name = "txtSAFlowTicket"
        Me.txtSAFlowTicket.Size = New System.Drawing.Size(271, 35)
        Me.txtSAFlowTicket.TabIndex = 5
        '
        'txtSABatchNo
        '
        Me.txtSABatchNo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtSABatchNo.Location = New System.Drawing.Point(333, 44)
        Me.txtSABatchNo.Name = "txtSABatchNo"
        Me.txtSABatchNo.Size = New System.Drawing.Size(271, 35)
        Me.txtSABatchNo.TabIndex = 9
        '
        'ckLossQty
        '
        Me.ckLossQty.AutoSize = True
        Me.ckLossQty.Location = New System.Drawing.Point(50, 126)
        Me.ckLossQty.Name = "ckLossQty"
        Me.ckLossQty.Size = New System.Drawing.Size(125, 33)
        Me.ckLossQty.TabIndex = 10
        Me.ckLossQty.Text = "Loss Qty"
        Me.ckLossQty.UseVisualStyleBackColor = True
        '
        'txtLossQty
        '
        Me.txtLossQty.Location = New System.Drawing.Point(333, 126)
        Me.txtLossQty.Name = "txtLossQty"
        Me.txtLossQty.Size = New System.Drawing.Size(271, 35)
        Me.txtLossQty.TabIndex = 11
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 3
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel10.Controls.Add(Me.btnSaveFGDefect, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.btnSaveFG, 1, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(3, 531)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(946, 41)
        Me.TableLayoutPanel10.TabIndex = 8
        '
        'btnSaveFGDefect
        '
        Me.btnSaveFGDefect.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnSaveFGDefect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSaveFGDefect.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSaveFGDefect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSaveFGDefect.Location = New System.Drawing.Point(3, 3)
        Me.btnSaveFGDefect.Name = "btnSaveFGDefect"
        Me.btnSaveFGDefect.Size = New System.Drawing.Size(309, 35)
        Me.btnSaveFGDefect.TabIndex = 4
        Me.btnSaveFGDefect.Text = "Save Defect"
        Me.btnSaveFGDefect.UseVisualStyleBackColor = False
        Me.btnSaveFGDefect.Visible = False
        '
        'btnSaveFG
        '
        Me.btnSaveFG.BackColor = System.Drawing.Color.Tan
        Me.btnSaveFG.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSaveFG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSaveFG.Location = New System.Drawing.Point(318, 3)
        Me.btnSaveFG.Name = "btnSaveFG"
        Me.btnSaveFG.Size = New System.Drawing.Size(309, 35)
        Me.btnSaveFG.TabIndex = 5
        Me.btnSaveFG.Text = "Save Finish Goods"
        Me.btnSaveFG.UseVisualStyleBackColor = False
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 3
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel11.Controls.Add(Me.btnSaveSADefect, 0, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.btnSaveSA, 1, 0)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(955, 531)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 1
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(946, 41)
        Me.TableLayoutPanel11.TabIndex = 9
        '
        'btnSaveSADefect
        '
        Me.btnSaveSADefect.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnSaveSADefect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSaveSADefect.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSaveSADefect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSaveSADefect.Location = New System.Drawing.Point(3, 3)
        Me.btnSaveSADefect.Name = "btnSaveSADefect"
        Me.btnSaveSADefect.Size = New System.Drawing.Size(309, 35)
        Me.btnSaveSADefect.TabIndex = 5
        Me.btnSaveSADefect.Text = "Save Defect"
        Me.btnSaveSADefect.UseVisualStyleBackColor = False
        Me.btnSaveSADefect.Visible = False
        '
        'btnSaveSA
        '
        Me.btnSaveSA.BackColor = System.Drawing.Color.Tan
        Me.btnSaveSA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSaveSA.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSaveSA.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSaveSA.Location = New System.Drawing.Point(318, 3)
        Me.btnSaveSA.Name = "btnSaveSA"
        Me.btnSaveSA.Size = New System.Drawing.Size(309, 35)
        Me.btnSaveSA.TabIndex = 6
        Me.btnSaveSA.Text = "Save Sub Assy"
        Me.btnSaveSA.UseVisualStyleBackColor = False
        '
        'TableLayoutPanel23
        '
        Me.TableLayoutPanel23.ColumnCount = 2
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel23.Controls.Add(Me.rbFG, 0, 0)
        Me.TableLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel23.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel23.Name = "TableLayoutPanel23"
        Me.TableLayoutPanel23.RowCount = 1
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel23.Size = New System.Drawing.Size(946, 51)
        Me.TableLayoutPanel23.TabIndex = 7
        '
        'rbFG
        '
        Me.rbFG.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbFG.AutoSize = True
        Me.rbFG.Location = New System.Drawing.Point(3, 9)
        Me.rbFG.Name = "rbFG"
        Me.rbFG.Size = New System.Drawing.Size(266, 33)
        Me.rbFG.TabIndex = 0
        Me.rbFG.TabStop = True
        Me.rbFG.Text = "Finish Goods Material"
        Me.rbFG.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel24
        '
        Me.TableLayoutPanel24.ColumnCount = 2
        Me.TableLayoutPanel24.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel24.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel24.Controls.Add(Me.rbSA, 0, 0)
        Me.TableLayoutPanel24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel24.Location = New System.Drawing.Point(955, 3)
        Me.TableLayoutPanel24.Name = "TableLayoutPanel24"
        Me.TableLayoutPanel24.RowCount = 1
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51.0!))
        Me.TableLayoutPanel24.Size = New System.Drawing.Size(946, 51)
        Me.TableLayoutPanel24.TabIndex = 10
        '
        'rbSA
        '
        Me.rbSA.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbSA.AutoSize = True
        Me.rbSA.Location = New System.Drawing.Point(3, 9)
        Me.rbSA.Name = "rbSA"
        Me.rbSA.Size = New System.Drawing.Size(222, 33)
        Me.rbSA.TabIndex = 3
        Me.rbSA.TabStop = True
        Me.rbSA.Text = "Sub Assy Material"
        Me.rbSA.UseVisualStyleBackColor = True
        '
        'tpBalance
        '
        Me.tpBalance.BackColor = System.Drawing.SystemColors.Control
        Me.tpBalance.Controls.Add(Me.TableLayoutPanel2)
        Me.tpBalance.Location = New System.Drawing.Point(4, 38)
        Me.tpBalance.Name = "tpBalance"
        Me.tpBalance.Size = New System.Drawing.Size(1910, 581)
        Me.tpBalance.TabIndex = 2
        Me.tpBalance.Text = "Return Material"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.dgBalance, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.22222!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.77778!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1910, 581)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 7
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.23321!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.26862!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.014652!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.25397!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.99878!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.52137!))
        Me.TableLayoutPanel3.Controls.Add(Me.TextBox10, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label11, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label16, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btnBalanceAdd, 4, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.txtBalanceQty, 2, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel4, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel7, 5, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel14, 4, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.txtBalanceMaterialPN, 3, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.txtReturnMaterialPN, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel22, 5, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.btnPrintBalance, 6, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.tampungIDMaterialReturnMaterial, 3, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1904, 123)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'TextBox10
        '
        Me.TextBox10.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TextBox10.Location = New System.Drawing.Point(3, 74)
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New System.Drawing.Size(26, 35)
        Me.TextBox10.TabIndex = 25
        Me.TextBox10.Visible = False
        '
        'Label11
        '
        Me.Label11.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(40, 77)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(177, 29)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Quantity Return"
        '
        'Label16
        '
        Me.Label16.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(40, 16)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(165, 29)
        Me.Label16.TabIndex = 3
        Me.Label16.Text = "Material Label"
        '
        'btnBalanceAdd
        '
        Me.btnBalanceAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBalanceAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnBalanceAdd.Location = New System.Drawing.Point(675, 64)
        Me.btnBalanceAdd.Name = "btnBalanceAdd"
        Me.btnBalanceAdd.Size = New System.Drawing.Size(162, 49)
        Me.btnBalanceAdd.TabIndex = 9
        Me.btnBalanceAdd.Text = "Save"
        Me.btnBalanceAdd.UseVisualStyleBackColor = False
        '
        'txtBalanceQty
        '
        Me.txtBalanceQty.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtBalanceQty.Location = New System.Drawing.Point(253, 74)
        Me.txtBalanceQty.Name = "txtBalanceQty"
        Me.txtBalanceQty.Size = New System.Drawing.Size(321, 35)
        Me.txtBalanceQty.TabIndex = 12
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.13497!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.86503!))
        Me.TableLayoutPanel4.Controls.Add(Me.txtBalanceBarcode, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.CheckBox2, 1, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(253, 3)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(378, 55)
        Me.TableLayoutPanel4.TabIndex = 13
        '
        'txtBalanceBarcode
        '
        Me.txtBalanceBarcode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtBalanceBarcode.Location = New System.Drawing.Point(3, 10)
        Me.txtBalanceBarcode.Name = "txtBalanceBarcode"
        Me.txtBalanceBarcode.Size = New System.Drawing.Size(176, 35)
        Me.txtBalanceBarcode.TabIndex = 4
        '
        'CheckBox2
        '
        Me.CheckBox2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Checked = True
        Me.CheckBox2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox2.Location = New System.Drawing.Point(215, 11)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(133, 33)
        Me.CheckBox2.TabIndex = 5
        Me.CheckBox2.Text = "QR Code"
        Me.CheckBox2.UseVisualStyleBackColor = True
        Me.CheckBox2.Visible = False
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 3
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.1957!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.96574!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.97859!))
        Me.TableLayoutPanel7.Controls.Add(Me.Label19, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.CheckManualReturnMaterial, 2, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.ComboBox2, 1, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(1021, 3)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(544, 55)
        Me.TableLayoutPanel7.TabIndex = 14
        '
        'Label19
        '
        Me.Label19.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(3, 13)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(84, 29)
        Me.Label19.TabIndex = 1
        Me.Label19.Text = "Lot No"
        Me.Label19.Visible = False
        '
        'CheckManualReturnMaterial
        '
        Me.CheckManualReturnMaterial.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckManualReturnMaterial.Location = New System.Drawing.Point(383, 3)
        Me.CheckManualReturnMaterial.Name = "CheckManualReturnMaterial"
        Me.CheckManualReturnMaterial.Size = New System.Drawing.Size(135, 49)
        Me.CheckManualReturnMaterial.TabIndex = 16
        Me.CheckManualReturnMaterial.Text = "Check"
        Me.CheckManualReturnMaterial.UseVisualStyleBackColor = True
        Me.CheckManualReturnMaterial.Visible = False
        '
        'ComboBox2
        '
        Me.ComboBox2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.DropDownWidth = 800
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(123, 9)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(254, 37)
        Me.ComboBox2.TabIndex = 17
        Me.ComboBox2.Visible = False
        '
        'TableLayoutPanel14
        '
        Me.TableLayoutPanel14.ColumnCount = 2
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel14.Controls.Add(Me.Label18, 0, 0)
        Me.TableLayoutPanel14.Controls.Add(Me.txtReturnMaterialManual, 1, 0)
        Me.TableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel14.Location = New System.Drawing.Point(675, 3)
        Me.TableLayoutPanel14.Name = "TableLayoutPanel14"
        Me.TableLayoutPanel14.RowCount = 1
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel14.Size = New System.Drawing.Size(340, 55)
        Me.TableLayoutPanel14.TabIndex = 15
        '
        'Label18
        '
        Me.Label18.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(3, 13)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(78, 29)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Comp"
        Me.Label18.Visible = False
        '
        'txtReturnMaterialManual
        '
        Me.txtReturnMaterialManual.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtReturnMaterialManual.Location = New System.Drawing.Point(105, 10)
        Me.txtReturnMaterialManual.Name = "txtReturnMaterialManual"
        Me.txtReturnMaterialManual.Size = New System.Drawing.Size(196, 35)
        Me.txtReturnMaterialManual.TabIndex = 2
        Me.txtReturnMaterialManual.Visible = False
        '
        'txtBalanceMaterialPN
        '
        Me.txtBalanceMaterialPN.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtBalanceMaterialPN.Location = New System.Drawing.Point(637, 13)
        Me.txtBalanceMaterialPN.Name = "txtBalanceMaterialPN"
        Me.txtBalanceMaterialPN.Size = New System.Drawing.Size(26, 35)
        Me.txtBalanceMaterialPN.TabIndex = 6
        Me.txtBalanceMaterialPN.Visible = False
        '
        'txtReturnMaterialPN
        '
        Me.txtReturnMaterialPN.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtReturnMaterialPN.Location = New System.Drawing.Point(3, 13)
        Me.txtReturnMaterialPN.Name = "txtReturnMaterialPN"
        Me.txtReturnMaterialPN.Size = New System.Drawing.Size(26, 35)
        Me.txtReturnMaterialPN.TabIndex = 16
        Me.txtReturnMaterialPN.Visible = False
        '
        'TableLayoutPanel22
        '
        Me.TableLayoutPanel22.ColumnCount = 2
        Me.TableLayoutPanel22.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.23769!))
        Me.TableLayoutPanel22.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.76231!))
        Me.TableLayoutPanel22.Controls.Add(Me.btnBalanceEdit, 1, 0)
        Me.TableLayoutPanel22.Controls.Add(Me.btnBalanceDelete, 0, 0)
        Me.TableLayoutPanel22.Location = New System.Drawing.Point(1021, 64)
        Me.TableLayoutPanel22.Name = "TableLayoutPanel22"
        Me.TableLayoutPanel22.RowCount = 1
        Me.TableLayoutPanel22.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel22.Size = New System.Drawing.Size(467, 56)
        Me.TableLayoutPanel22.TabIndex = 26
        '
        'btnBalanceEdit
        '
        Me.btnBalanceEdit.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnBalanceEdit.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.btnBalanceEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnBalanceEdit.Location = New System.Drawing.Point(317, 3)
        Me.btnBalanceEdit.Name = "btnBalanceEdit"
        Me.btnBalanceEdit.Size = New System.Drawing.Size(147, 49)
        Me.btnBalanceEdit.TabIndex = 10
        Me.btnBalanceEdit.Text = "SUB (-)"
        Me.btnBalanceEdit.UseVisualStyleBackColor = False
        Me.btnBalanceEdit.Visible = False
        '
        'btnBalanceDelete
        '
        Me.btnBalanceDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnBalanceDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnBalanceDelete.Location = New System.Drawing.Point(3, 3)
        Me.btnBalanceDelete.Name = "btnBalanceDelete"
        Me.btnBalanceDelete.Size = New System.Drawing.Size(147, 49)
        Me.btnBalanceDelete.TabIndex = 11
        Me.btnBalanceDelete.Text = "Delete"
        Me.btnBalanceDelete.UseVisualStyleBackColor = False
        Me.btnBalanceDelete.Visible = False
        '
        'btnPrintBalance
        '
        Me.btnPrintBalance.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnPrintBalance.BackColor = System.Drawing.Color.SkyBlue
        Me.btnPrintBalance.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPrintBalance.Location = New System.Drawing.Point(1739, 6)
        Me.btnPrintBalance.Name = "btnPrintBalance"
        Me.btnPrintBalance.Size = New System.Drawing.Size(162, 49)
        Me.btnPrintBalance.TabIndex = 11
        Me.btnPrintBalance.Text = "Print"
        Me.btnPrintBalance.UseVisualStyleBackColor = False
        Me.btnPrintBalance.Visible = False
        '
        'tampungIDMaterialReturnMaterial
        '
        Me.tampungIDMaterialReturnMaterial.Location = New System.Drawing.Point(637, 64)
        Me.tampungIDMaterialReturnMaterial.Name = "tampungIDMaterialReturnMaterial"
        Me.tampungIDMaterialReturnMaterial.Size = New System.Drawing.Size(32, 35)
        Me.tampungIDMaterialReturnMaterial.TabIndex = 27
        Me.tampungIDMaterialReturnMaterial.Visible = False
        '
        'dgBalance
        '
        Me.dgBalance.AllowUserToAddRows = False
        Me.dgBalance.AllowUserToDeleteRows = False
        Me.dgBalance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgBalance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgBalance.Location = New System.Drawing.Point(3, 132)
        Me.dgBalance.Name = "dgBalance"
        Me.dgBalance.Size = New System.Drawing.Size(1904, 446)
        Me.dgBalance.TabIndex = 1
        '
        'FormDefectiveV2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1924, 811)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FormDefectiveV2"
        Me.Text = "Result Production"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.tpRejectMaterial.ResumeLayout(False)
        Me.TableLayoutPanel16.ResumeLayout(False)
        Me.TableLayoutPanel17.ResumeLayout(False)
        Me.TableLayoutPanel17.PerformLayout()
        Me.TableLayoutPanel18.ResumeLayout(False)
        Me.TableLayoutPanel18.PerformLayout()
        Me.TableLayoutPanel19.ResumeLayout(False)
        Me.TableLayoutPanel19.PerformLayout()
        Me.TableLayoutPanel20.ResumeLayout(False)
        Me.TableLayoutPanel20.PerformLayout()
        CType(Me.dgReject, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpWIP.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        CType(Me.dgWIP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        Me.tpOnHold.ResumeLayout(False)
        Me.TableLayoutPanel13.ResumeLayout(False)
        CType(Me.dgOnHold, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel15.ResumeLayout(False)
        Me.TableLayoutPanel15.PerformLayout()
        Me.tpFinishGoods.ResumeLayout(False)
        Me.TableLayoutPanel12.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.TableLayoutPanel9.PerformLayout()
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel11.ResumeLayout(False)
        Me.TableLayoutPanel23.ResumeLayout(False)
        Me.TableLayoutPanel23.PerformLayout()
        Me.TableLayoutPanel24.ResumeLayout(False)
        Me.TableLayoutPanel24.PerformLayout()
        Me.tpBalance.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.TableLayoutPanel14.ResumeLayout(False)
        Me.TableLayoutPanel14.PerformLayout()
        Me.TableLayoutPanel22.ResumeLayout(False)
        CType(Me.dgBalance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cbLineNumber As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents tpFinishGoods As TabPage
    Friend WithEvents tpBalance As TabPage
    Friend WithEvents tpWIP As TabPage
    Friend WithEvents tpOnHold As TabPage
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Label11 As Label
    Friend WithEvents dgBalance As DataGridView
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents cbWIPProcess As ComboBox
    Friend WithEvents txtWIPTicketNo As TextBox
    Friend WithEvents txtWIPQuantity As TextBox
    Friend WithEvents btnWIPAdd As Button
    Friend WithEvents TableLayoutPanel12 As TableLayoutPanel
    Friend WithEvents dgWIP As DataGridView
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel13 As TableLayoutPanel
    Friend WithEvents dgOnHold As DataGridView
    Friend WithEvents TableLayoutPanel15 As TableLayoutPanel
    Friend WithEvents btnOnHoldSave As Button
    Friend WithEvents Label9 As Label
    Friend WithEvents txtOnHoldQty As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents txtOnHoldTicketNo As TextBox
    Friend WithEvents cbOnHoldProcess As ComboBox
    Friend WithEvents btnBalanceEdit As Button
    Friend WithEvents btnBalanceAdd As Button
    Friend WithEvents Label16 As Label
    Friend WithEvents txtBalanceBarcode As TextBox
    Friend WithEvents txtBalanceMaterialPN As TextBox
    Friend WithEvents txtTampungFlow As TextBox
    Friend WithEvents txtTampungLabel As TextBox
    Friend WithEvents txtINV As TextBox
    Friend WithEvents txtBatchno As TextBox
    Friend WithEvents rbFG As RadioButton
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents DataGridView3 As DataGridView
    Friend WithEvents rbSA As RadioButton
    Friend WithEvents btnSaveFGDefect As Button
    Friend WithEvents btnSaveSADefect As Button
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents Label20 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents txtFGLabel As TextBox
    Friend WithEvents txtFGFlowTicket As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TableLayoutPanel9 As TableLayoutPanel
    Friend WithEvents Label22 As Label
    Friend WithEvents txtSAFlowTicket As TextBox
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents cbPONumber As TextBox
    Friend WithEvents txtSubSubPODefective As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents cbFGPN As TextBox
    Friend WithEvents txtDescDefective As TextBox
    Friend WithEvents Label25 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents txtSPQ As TextBox
    Friend WithEvents btnPrintBalance As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents txtSABatchNo As TextBox
    Friend WithEvents TableLayoutPanel10 As TableLayoutPanel
    Friend WithEvents btnPrintFGDefect As Button
    Friend WithEvents TableLayoutPanel11 As TableLayoutPanel
    Friend WithEvents btnListPrintWIP As Button
    Friend WithEvents btnResetFG As Button
    Friend WithEvents btnResetSA As Button
    Friend WithEvents btnPrintWIP As Button
    Friend WithEvents btnPrintOnhold As Button
    Friend WithEvents txtBalanceQty As TextBox
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents Label19 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TableLayoutPanel14 As TableLayoutPanel
    Friend WithEvents Label18 As Label
    Friend WithEvents txtReturnMaterialManual As TextBox
    Friend WithEvents CheckManualReturnMaterial As Button
    Friend WithEvents tpRejectMaterial As TabPage
    Friend WithEvents TableLayoutPanel16 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel17 As TableLayoutPanel
    Friend WithEvents dgReject As DataGridView
    Friend WithEvents Label23 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents TableLayoutPanel19 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel20 As TableLayoutPanel
    Friend WithEvents txtRejectBarcode As TextBox
    Friend WithEvents txtRejectQty As TextBox
    Friend WithEvents Label28 As Label
    Friend WithEvents Label29 As Label
    Friend WithEvents txtRejectMaterialManual As TextBox
    Friend WithEvents CheckManualRejectMaterial As Button
    Friend WithEvents txtRejectMaterialPN As TextBox
    Friend WithEvents TableLayoutPanel18 As TableLayoutPanel
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents txtReturnMaterialPN As TextBox
    Friend WithEvents btnRejectSave As Button
    Friend WithEvents btnRejectDelete As Button
    Friend WithEvents btnWIPDelete As Button
    Friend WithEvents btnOnHoldDelete As Button
    Friend WithEvents TableLayoutPanel22 As TableLayoutPanel
    Friend WithEvents btnBalanceDelete As Button
    Friend WithEvents TextBox11 As TextBox
    Friend WithEvents TextBox12 As TextBox
    Friend WithEvents TextBox10 As TextBox
    Friend WithEvents btnSaveFG As Button
    Friend WithEvents btnSaveSA As Button
    Friend WithEvents txtStatusSubSubPO As TextBox
    Friend WithEvents TableLayoutPanel23 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel24 As TableLayoutPanel
    Friend WithEvents btnPrintSA As Button
    Friend WithEvents CheckBox5 As CheckBox
    Friend WithEvents PrintDefect As Button
    Friend WithEvents Label30 As Label
    Friend WithEvents Label31 As Label
    Friend WithEvents ckLossQty As CheckBox
    Friend WithEvents txtLossQty As TextBox
    Friend WithEvents btnListPrintOthers As Button
    Friend WithEvents btnListPrintReturn As Button
    Friend WithEvents btnListPrintOnHold As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents tampungIDMaterial As TextBox
    Friend WithEvents tampungIDMaterialReturnMaterial As TextBox
    Friend WithEvents CheckBoxFGDefect As CheckBox
    Friend WithEvents txtFTQty As TextBox
    Friend WithEvents txtSAPQty As TextBox
    Friend WithEvents txtFTLot As TextBox
    Friend WithEvents txtSAPLot As TextBox
End Class
