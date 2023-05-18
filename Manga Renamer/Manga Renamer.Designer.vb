<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainWindow
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainWindow))
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.Txb_EditString = New System.Windows.Forms.TextBox()
        Me.B_Rename = New System.Windows.Forms.Button()
        Me.B_Next = New System.Windows.Forms.Button()
        Me.Lv_After = New System.Windows.Forms.ListView()
        Me.RenamePreview = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Lbx_Before = New System.Windows.Forms.ListBox()
        Me.Txb_JumpToPath = New System.Windows.Forms.TextBox()
        Me.L_JumpToPath = New System.Windows.Forms.Label()
        Me.B_Go = New System.Windows.Forms.Button()
        Me.B_GetParent = New System.Windows.Forms.Button()
        Me.Menu_Main = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MItem_IgnoreExceptions = New System.Windows.Forms.ToolStripMenuItem()
        Me.MItem_MiddleClick = New System.Windows.Forms.ToolStripMenuItem()
        Me.B_GetCurrent = New System.Windows.Forms.Button()
        Me.B_Clear = New System.Windows.Forms.Button()
        Me.Txb_RemoveString = New System.Windows.Forms.TextBox()
        Me.Txb_PrefixString = New System.Windows.Forms.TextBox()
        Me.Cbx_EditString = New System.Windows.Forms.CheckBox()
        Me.Cbx_RemoveString = New System.Windows.Forms.CheckBox()
        Me.Cbx_PrefixString = New System.Windows.Forms.CheckBox()
        Me.Nup_EditString = New System.Windows.Forms.NumericUpDown()
        Me.Nup_RemoveStart = New System.Windows.Forms.NumericUpDown()
        Me.Nup_RemoveEnd = New System.Windows.Forms.NumericUpDown()
        Me.Cbx_RemoveFirstOnly = New System.Windows.Forms.CheckBox()
        Me.B_GetPath = New System.Windows.Forms.Button()
        Me.L_Path = New System.Windows.Forms.Label()
        Me.Txb_SwapA = New System.Windows.Forms.TextBox()
        Me.Txb_SwapB = New System.Windows.Forms.TextBox()
        Me.Cbx_Swap = New System.Windows.Forms.CheckBox()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Menu_Main.SuspendLayout()
        CType(Me.Nup_EditString, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Nup_RemoveStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Nup_RemoveEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Left
        Me.TreeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText
        Me.TreeView1.HideSelection = False
        Me.TreeView1.HotTracking = True
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.ShowNodeToolTips = True
        Me.TreeView1.Size = New System.Drawing.Size(260, 353)
        Me.TreeView1.TabIndex = 10
        '
        'Txb_EditString
        '
        Me.Txb_EditString.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Txb_EditString.Location = New System.Drawing.Point(279, 12)
        Me.Txb_EditString.Name = "Txb_EditString"
        Me.Txb_EditString.Size = New System.Drawing.Size(344, 22)
        Me.Txb_EditString.TabIndex = 20
        '
        'B_Rename
        '
        Me.B_Rename.Location = New System.Drawing.Point(279, 124)
        Me.B_Rename.Name = "B_Rename"
        Me.B_Rename.Size = New System.Drawing.Size(75, 23)
        Me.B_Rename.TabIndex = 60
        Me.B_Rename.Text = "Rename"
        Me.B_Rename.UseVisualStyleBackColor = True
        '
        'B_Next
        '
        Me.B_Next.Location = New System.Drawing.Point(360, 124)
        Me.B_Next.Name = "B_Next"
        Me.B_Next.Size = New System.Drawing.Size(75, 23)
        Me.B_Next.TabIndex = 70
        Me.B_Next.Text = "Next"
        Me.B_Next.UseVisualStyleBackColor = True
        '
        'Lv_After
        '
        Me.Lv_After.BackColor = System.Drawing.Color.LightGray
        Me.Lv_After.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.RenamePreview})
        Me.Lv_After.Dock = System.Windows.Forms.DockStyle.Right
        Me.Lv_After.ForeColor = System.Drawing.Color.Green
        Me.Lv_After.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.Lv_After.LabelWrap = False
        Me.Lv_After.Location = New System.Drawing.Point(922, 0)
        Me.Lv_After.MultiSelect = False
        Me.Lv_After.Name = "Lv_After"
        Me.Lv_After.Size = New System.Drawing.Size(260, 353)
        Me.Lv_After.TabIndex = 140
        Me.Lv_After.UseCompatibleStateImageBehavior = False
        Me.Lv_After.View = System.Windows.Forms.View.Details
        '
        'RenamePreview
        '
        Me.RenamePreview.Text = "Rename Preview"
        Me.RenamePreview.Width = 255
        '
        'Lbx_Before
        '
        Me.Lbx_Before.Dock = System.Windows.Forms.DockStyle.Right
        Me.Lbx_Before.FormattingEnabled = True
        Me.Lbx_Before.HorizontalScrollbar = True
        Me.Lbx_Before.ItemHeight = 16
        Me.Lbx_Before.Location = New System.Drawing.Point(662, 0)
        Me.Lbx_Before.Name = "Lbx_Before"
        Me.Lbx_Before.ScrollAlwaysVisible = True
        Me.Lbx_Before.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.Lbx_Before.Size = New System.Drawing.Size(260, 353)
        Me.Lbx_Before.TabIndex = 130
        '
        'Txb_JumpToPath
        '
        Me.Txb_JumpToPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Txb_JumpToPath.Location = New System.Drawing.Point(279, 321)
        Me.Txb_JumpToPath.Name = "Txb_JumpToPath"
        Me.Txb_JumpToPath.Size = New System.Drawing.Size(364, 22)
        Me.Txb_JumpToPath.TabIndex = 120
        '
        'L_JumpToPath
        '
        Me.L_JumpToPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.L_JumpToPath.AutoSize = True
        Me.L_JumpToPath.Location = New System.Drawing.Point(279, 298)
        Me.L_JumpToPath.Name = "L_JumpToPath"
        Me.L_JumpToPath.Size = New System.Drawing.Size(91, 17)
        Me.L_JumpToPath.TabIndex = 5
        Me.L_JumpToPath.Text = "Jump to Path"
        '
        'B_Go
        '
        Me.B_Go.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.B_Go.Location = New System.Drawing.Point(568, 295)
        Me.B_Go.Name = "B_Go"
        Me.B_Go.Size = New System.Drawing.Size(75, 23)
        Me.B_Go.TabIndex = 110
        Me.B_Go.Text = "Go"
        Me.B_Go.UseVisualStyleBackColor = True
        '
        'B_GetParent
        '
        Me.B_GetParent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.B_GetParent.Location = New System.Drawing.Point(553, 153)
        Me.B_GetParent.Name = "B_GetParent"
        Me.B_GetParent.Size = New System.Drawing.Size(90, 23)
        Me.B_GetParent.TabIndex = 100
        Me.B_GetParent.Text = "Get Parent"
        Me.B_GetParent.UseVisualStyleBackColor = True
        '
        'Menu_Main
        '
        Me.Menu_Main.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.Menu_Main.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MItem_IgnoreExceptions, Me.MItem_MiddleClick})
        Me.Menu_Main.Name = "Menu_Main"
        Me.Menu_Main.Size = New System.Drawing.Size(223, 56)
        '
        'MItem_IgnoreExceptions
        '
        Me.MItem_IgnoreExceptions.Checked = True
        Me.MItem_IgnoreExceptions.CheckOnClick = True
        Me.MItem_IgnoreExceptions.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MItem_IgnoreExceptions.Name = "MItem_IgnoreExceptions"
        Me.MItem_IgnoreExceptions.Size = New System.Drawing.Size(222, 26)
        Me.MItem_IgnoreExceptions.Text = "Ignore Exceptions"
        '
        'MItem_MiddleClick
        '
        Me.MItem_MiddleClick.Checked = True
        Me.MItem_MiddleClick.CheckOnClick = True
        Me.MItem_MiddleClick.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MItem_MiddleClick.Name = "MItem_MiddleClick"
        Me.MItem_MiddleClick.Size = New System.Drawing.Size(222, 26)
        Me.MItem_MiddleClick.Text = "Middle Click Options"
        '
        'B_GetCurrent
        '
        Me.B_GetCurrent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.B_GetCurrent.Location = New System.Drawing.Point(553, 124)
        Me.B_GetCurrent.Name = "B_GetCurrent"
        Me.B_GetCurrent.Size = New System.Drawing.Size(90, 23)
        Me.B_GetCurrent.TabIndex = 90
        Me.B_GetCurrent.Text = "Get Current"
        Me.B_GetCurrent.UseVisualStyleBackColor = True
        '
        'B_Clear
        '
        Me.B_Clear.Location = New System.Drawing.Point(279, 153)
        Me.B_Clear.Name = "B_Clear"
        Me.B_Clear.Size = New System.Drawing.Size(75, 23)
        Me.B_Clear.TabIndex = 80
        Me.B_Clear.Text = "Clear"
        Me.B_Clear.UseVisualStyleBackColor = True
        '
        'Txb_RemoveString
        '
        Me.Txb_RemoveString.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Txb_RemoveString.Location = New System.Drawing.Point(279, 40)
        Me.Txb_RemoveString.Name = "Txb_RemoveString"
        Me.Txb_RemoveString.Size = New System.Drawing.Size(344, 22)
        Me.Txb_RemoveString.TabIndex = 30
        '
        'Txb_PrefixString
        '
        Me.Txb_PrefixString.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Txb_PrefixString.Location = New System.Drawing.Point(279, 68)
        Me.Txb_PrefixString.Name = "Txb_PrefixString"
        Me.Txb_PrefixString.Size = New System.Drawing.Size(344, 22)
        Me.Txb_PrefixString.TabIndex = 40
        '
        'Cbx_EditString
        '
        Me.Cbx_EditString.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cbx_EditString.Checked = True
        Me.Cbx_EditString.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Cbx_EditString.Location = New System.Drawing.Point(629, 12)
        Me.Cbx_EditString.Name = "Cbx_EditString"
        Me.Cbx_EditString.Size = New System.Drawing.Size(26, 24)
        Me.Cbx_EditString.TabIndex = 41
        Me.Cbx_EditString.UseVisualStyleBackColor = True
        '
        'Cbx_RemoveString
        '
        Me.Cbx_RemoveString.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cbx_RemoveString.Checked = True
        Me.Cbx_RemoveString.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Cbx_RemoveString.Location = New System.Drawing.Point(629, 40)
        Me.Cbx_RemoveString.Name = "Cbx_RemoveString"
        Me.Cbx_RemoveString.Size = New System.Drawing.Size(26, 24)
        Me.Cbx_RemoveString.TabIndex = 42
        Me.Cbx_RemoveString.UseVisualStyleBackColor = True
        '
        'Cbx_PrefixString
        '
        Me.Cbx_PrefixString.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cbx_PrefixString.Checked = True
        Me.Cbx_PrefixString.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Cbx_PrefixString.Location = New System.Drawing.Point(629, 68)
        Me.Cbx_PrefixString.Name = "Cbx_PrefixString"
        Me.Cbx_PrefixString.Size = New System.Drawing.Size(26, 24)
        Me.Cbx_PrefixString.TabIndex = 43
        Me.Cbx_PrefixString.UseVisualStyleBackColor = True
        '
        'Nup_EditString
        '
        Me.Nup_EditString.Location = New System.Drawing.Point(279, 96)
        Me.Nup_EditString.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.Nup_EditString.Name = "Nup_EditString"
        Me.Nup_EditString.Size = New System.Drawing.Size(75, 22)
        Me.Nup_EditString.TabIndex = 50
        '
        'Nup_RemoveStart
        '
        Me.Nup_RemoveStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Nup_RemoveStart.Location = New System.Drawing.Point(467, 96)
        Me.Nup_RemoveStart.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.Nup_RemoveStart.Name = "Nup_RemoveStart"
        Me.Nup_RemoveStart.Size = New System.Drawing.Size(75, 22)
        Me.Nup_RemoveStart.TabIndex = 85
        '
        'Nup_RemoveEnd
        '
        Me.Nup_RemoveEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Nup_RemoveEnd.Location = New System.Drawing.Point(548, 96)
        Me.Nup_RemoveEnd.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.Nup_RemoveEnd.Name = "Nup_RemoveEnd"
        Me.Nup_RemoveEnd.Size = New System.Drawing.Size(75, 22)
        Me.Nup_RemoveEnd.TabIndex = 86
        '
        'Cbx_RemoveFirstOnly
        '
        Me.Cbx_RemoveFirstOnly.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cbx_RemoveFirstOnly.AutoSize = True
        Me.Cbx_RemoveFirstOnly.Checked = True
        Me.Cbx_RemoveFirstOnly.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Cbx_RemoveFirstOnly.Location = New System.Drawing.Point(629, 96)
        Me.Cbx_RemoveFirstOnly.Name = "Cbx_RemoveFirstOnly"
        Me.Cbx_RemoveFirstOnly.Size = New System.Drawing.Size(18, 17)
        Me.Cbx_RemoveFirstOnly.TabIndex = 87
        Me.Cbx_RemoveFirstOnly.UseVisualStyleBackColor = True
        '
        'B_GetPath
        '
        Me.B_GetPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.B_GetPath.Location = New System.Drawing.Point(487, 295)
        Me.B_GetPath.Name = "B_GetPath"
        Me.B_GetPath.Size = New System.Drawing.Size(75, 23)
        Me.B_GetPath.TabIndex = 110
        Me.B_GetPath.Text = "Get Path"
        Me.B_GetPath.UseVisualStyleBackColor = True
        '
        'L_Path
        '
        Me.L_Path.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.L_Path.AutoEllipsis = True
        Me.L_Path.AutoSize = True
        Me.L_Path.Location = New System.Drawing.Point(279, 275)
        Me.L_Path.Name = "L_Path"
        Me.L_Path.Size = New System.Drawing.Size(0, 17)
        Me.L_Path.TabIndex = 142
        Me.L_Path.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Txb_SwapA
        '
        Me.Txb_SwapA.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Txb_SwapA.Location = New System.Drawing.Point(3, 3)
        Me.Txb_SwapA.Name = "Txb_SwapA"
        Me.Txb_SwapA.Size = New System.Drawing.Size(178, 22)
        Me.Txb_SwapA.TabIndex = 105
        '
        'Txb_SwapB
        '
        Me.Txb_SwapB.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Txb_SwapB.Location = New System.Drawing.Point(187, 3)
        Me.Txb_SwapB.Name = "Txb_SwapB"
        Me.Txb_SwapB.Size = New System.Drawing.Size(150, 22)
        Me.Txb_SwapB.TabIndex = 106
        '
        'Cbx_Swap
        '
        Me.Cbx_Swap.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cbx_Swap.AutoSize = True
        Me.Cbx_Swap.Checked = True
        Me.Cbx_Swap.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Cbx_Swap.Location = New System.Drawing.Point(347, 6)
        Me.Cbx_Swap.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.Cbx_Swap.Name = "Cbx_Swap"
        Me.Cbx_Swap.Size = New System.Drawing.Size(18, 17)
        Me.Cbx_Swap.TabIndex = 107
        Me.Cbx_Swap.UseVisualStyleBackColor = True
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FlowLayoutPanel1.AutoSize = True
        Me.FlowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.FlowLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(279, 211)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(0, 0)
        Me.FlowLayoutPanel1.TabIndex = 143
        Me.FlowLayoutPanel1.WrapContents = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Txb_SwapB, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Txb_SwapA, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cbx_Swap, 2, 0)
        Me.TableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(279, 182)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(368, 29)
        Me.TableLayoutPanel1.TabIndex = 104
        '
        'MainWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1182, 353)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Controls.Add(Me.L_Path)
        Me.Controls.Add(Me.Cbx_RemoveFirstOnly)
        Me.Controls.Add(Me.Nup_RemoveEnd)
        Me.Controls.Add(Me.Nup_RemoveStart)
        Me.Controls.Add(Me.Nup_EditString)
        Me.Controls.Add(Me.Cbx_PrefixString)
        Me.Controls.Add(Me.Cbx_RemoveString)
        Me.Controls.Add(Me.Cbx_EditString)
        Me.Controls.Add(Me.L_JumpToPath)
        Me.Controls.Add(Me.Lbx_Before)
        Me.Controls.Add(Me.B_GetPath)
        Me.Controls.Add(Me.B_Go)
        Me.Controls.Add(Me.B_GetCurrent)
        Me.Controls.Add(Me.B_GetParent)
        Me.Controls.Add(Me.B_Next)
        Me.Controls.Add(Me.B_Clear)
        Me.Controls.Add(Me.B_Rename)
        Me.Controls.Add(Me.Txb_JumpToPath)
        Me.Controls.Add(Me.Txb_PrefixString)
        Me.Controls.Add(Me.Txb_RemoveString)
        Me.Controls.Add(Me.Txb_EditString)
        Me.Controls.Add(Me.Lv_After)
        Me.Controls.Add(Me.TreeView1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(1095, 335)
        Me.Name = "MainWindow"
        Me.Text = "Manga Renamer"
        Me.Menu_Main.ResumeLayout(False)
        CType(Me.Nup_EditString, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Nup_RemoveStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Nup_RemoveEnd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents Txb_EditString As TextBox
    Friend WithEvents B_Rename As Button
    Friend WithEvents B_Next As Button
    Friend WithEvents Lv_After As ListView
    Friend WithEvents Lbx_Before As ListBox
    Friend WithEvents Txb_JumpToPath As TextBox
    Friend WithEvents L_JumpToPath As Label
    Friend WithEvents B_Go As Button
    Friend WithEvents B_GetParent As Button
    Friend WithEvents Menu_Main As ContextMenuStrip
    Friend WithEvents MItem_IgnoreExceptions As ToolStripMenuItem
    Friend WithEvents B_GetCurrent As Button
    Friend WithEvents B_Clear As Button
    Friend WithEvents Txb_RemoveString As TextBox
    Friend WithEvents Txb_PrefixString As TextBox
    Friend WithEvents Cbx_EditString As CheckBox
    Friend WithEvents Cbx_RemoveString As CheckBox
    Friend WithEvents Cbx_PrefixString As CheckBox
    Friend WithEvents Nup_EditString As NumericUpDown
    Friend WithEvents Nup_RemoveStart As NumericUpDown
    Friend WithEvents Nup_RemoveEnd As NumericUpDown
    Friend WithEvents Cbx_RemoveFirstOnly As CheckBox
    Friend WithEvents B_GetPath As Button
    Friend WithEvents L_Path As Label
    Friend WithEvents MItem_MiddleClick As ToolStripMenuItem
    Friend WithEvents Txb_SwapA As TextBox
    Friend WithEvents Txb_SwapB As TextBox
    Friend WithEvents Cbx_Swap As CheckBox
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents RenamePreview As ColumnHeader
End Class
