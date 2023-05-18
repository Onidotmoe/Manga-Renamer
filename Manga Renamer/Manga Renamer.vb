Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Public Class MainWindow

    Public Shared ListBoxValues As New BindingList(Of String)
    Public Shared ListBoxSelection As New List(Of Integer)
    Public Shared PreviousNode As New TreeNode
    Public Shared ListViewValues As New List(Of ListViewItem)
    Public Shared KeyList As New List(Of Key)
    Public Shared BlockSelection As Boolean = False
    Public Shared IgnoreException As Boolean = True
    Public Shared MiddleClickOptions As Boolean = True
    Public Shared SelectionInProgress As Boolean = False
    Public Shared BoolEdit As Boolean = True
    Public Shared BoolRemove As Boolean = True
    Public Shared BoolRemoveOnlyFirst As Boolean = True
    Public Shared BoolPrefix As Boolean = True
    Public Shared BoolSwap As Boolean = True
    Public Shared IsLoaded As Boolean = False
    Public Shared EditValue As Decimal = 0
    Public Shared RemoveStartValue As Decimal = 0
    Public Shared RemoveEndValue As Decimal = 0
    Public Shared Wildcards() As Char = New Char() {CType("?", Char), CType("*", Char)}

    Public Structure Key
        Dim Index As Integer
        Dim Path As String
        Dim Name As String
        Dim Result As String

        Public Sub New(ByVal ListIndex As Integer, ByVal PathValue As String, ByVal NameValue As String, Optional ByVal ResultValue As String = Nothing)
            Index = ListIndex
            Path = PathValue
            Name = NameValue
            Result = ResultValue
        End Sub
    End Structure

    Private Enum ItemType
        Drive
        Folder
        File
    End Enum

    Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lbx_Before.DataSource = ListBoxValues
        Lbx_Before.BindingContext = New BindingContext()
        IsLoaded = True

        Dim DesktopInfo As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        Dim DesktopNode As TreeNode = TreeView1.Nodes.Add(DesktopInfo)
        DesktopNode.Tag = ItemType.Folder
        DesktopNode.Nodes.Add("FILLER")

        For Each Drive As DriveInfo In DriveInfo.GetDrives
            If Not (Drive.DriveType = DriveType.CDRom) Then
                Dim node As TreeNode = TreeView1.Nodes.Add(Drive.Name)
                node.Tag = ItemType.Drive
                node.Nodes.Add("FILLER")
            End If
        Next
    End Sub

    Private Sub TreeView1_BeforeExpand(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs) Handles TreeView1.BeforeExpand
        Dim CurrentNode As TreeNode = e.Node
        CurrentNode.Nodes.Clear()
        TreeView1.BeginUpdate()

        Try
            'Now go get all the files and folders
            Dim fullPathString As String = CurrentNode.FullPath

            'Handle each folder
            For Each folderString As String In Directory.GetDirectories(fullPathString)
                Dim FolderInfo As New System.IO.DirectoryInfo(folderString)

                If ((FolderInfo.Attributes.HasFlag(FileAttributes.Hidden) = False)) Then
                    Dim newNode As TreeNode = CurrentNode.Nodes.Add(Path.GetFileName(folderString))
                    Dim x As String = Path.GetFileName("")
                    newNode.Tag = ItemType.Folder
                    newNode.Nodes.Add("FILLER")
                End If
            Next

            'Handle each file
            'For Each fileString As String In Directory.GetFiles(fullPathString)
            '    'Get just the file name portion (without the path) :
            '    Dim newNode As TreeNode = CurrentNode.Nodes.Add(Path.GetFileName(fileString))
            '    newNode.Tag = ItemType.File
            'Next

        Catch Exception As System.Exception
            e.Node.ForeColor = Color.Red
            TreeView1.SelectedNode.ToolTipText = "Caused an exception."

            If IgnoreException = False Then
                Dim ExceptionBox As New ExceptionBox
                ExceptionBox.Txb_Main.Text = Exception.ToString
                ExceptionBox.ShowDialog()
            End If
        End Try

        TreeView1.EndUpdate()
    End Sub

    Private Sub TreeView1_BeforeSelect(sender As Object, e As TreeViewCancelEventArgs) Handles TreeView1.BeforeSelect
        BlockSelection = True
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        ListViewValues.Clear()
        Lv_After.Items.Clear()
        KeyList.Clear()

        Dim SortingNames As New List(Of String)

        Try
            Dim NewPath As String = e.Node.FullPath

            If Directory.Exists(NewPath) Then
                Dim NewDirectories As New BindingList(Of String)
                Dim i As Integer = 0

                For Each Path As String In Directory.GetDirectories(NewPath)
                    Dim FolderInfo As New System.IO.DirectoryInfo(Path)
                    Dim FolderName As String = FolderInfo.Name

                    If (FolderInfo.Attributes.HasFlag(FileAttributes.Hidden) = False) Then
                        NewDirectories.Add(FolderName)
                        KeyList.Add(New Key(i, Path, FolderName))
                        SortingNames.Add(FolderName)
                        i += 1
                    End If
                Next

                Dim StringArray() As String = NewDirectories.ToArray
                Array.Sort(StringArray, New AlphanumComparator())
                NewDirectories = New BindingList(Of String)(StringArray)

                ListBoxValues = NewDirectories
                Lbx_Before.DataSource = ListBoxValues
            End If

        Catch Exception As Exception
            e.Node.ForeColor = Color.Red
            TreeView1.SelectedNode.ToolTipText = "Caused an exception."

            If IgnoreException = False Then
                Dim ExceptionBox As New ExceptionBox
                ExceptionBox.Txb_Main.Text = Exception.ToString
                ExceptionBox.ShowDialog()
            End If
        End Try

        Dim NameArray() As String = SortingNames.ToArray
        Array.Sort(NameArray, New AlphanumComparator())
        SortingNames = NameArray.ToList

        For i = 0 To SortingNames.Count - 1
            For iKey = 0 To KeyList.Count - 1
                Dim Key As Key = KeyList(iKey)
                If Key.Name = SortingNames(i) Then
                    KeyList(iKey) = New Key(i, Key.Path, Key.Name, Key.Result)
                    Exit For
                End If
            Next
        Next

        BlockSelection = False
        If Equals(PreviousNode, e.Node) Then
            CustomSelection()
            WorkStrings()
        Else
            ListBoxSelection.Clear()
            ListBoxSelection.Add(0)
            PreviousNode = e.Node
        End If
    End Sub

    Private Sub CustomSelection()
        Dim sender As ListBox = Lbx_Before

        If (ListBoxSelection.Count > 0) AndAlso (ListBoxSelection(0) = -2) Then
            Selecting(sender)
        Else
            SelectionInProgress = True
            sender.SelectedItem = Nothing

            sender.BeginUpdate()
            For Each i As Integer In ListBoxSelection
                sender.SetSelected(i, True)
            Next

            SelectionInProgress = False
            sender.TopIndex = 0
            sender.EndUpdate()
        End If
    End Sub

    Private Sub B_Go_Click(sender As Object, e As EventArgs) Handles B_Go.Click
        JumpToPath()
    End Sub

    Private Sub Txb_JumpToPath_KeyDown(sender As Object, e As KeyEventArgs) Handles Txb_JumpToPath.KeyDown
        If (e.KeyData = Keys.Enter) Then
            JumpToPath()
        End If
    End Sub

    Private Sub B_GetPath_Click(sender As Object, e As EventArgs) Handles B_GetPath.Click
        TreeView1.Select()
        Txb_JumpToPath.Text = (TreeView1.SelectedNode.FullPath).Replace("\\", "\")
    End Sub

    Private Sub JumpToPath()
        Dim SelectPath As String = Txb_JumpToPath.Text

        If Not String.IsNullOrWhiteSpace(SelectPath) Then
            If Directory.Exists(SelectPath) Then
                SelectPath = SelectPath.Replace("\\", "\")

                Dim BackSlash As Char = Convert.ToChar("\")
                Dim PathList As String() = SelectPath.Split(BackSlash)

                If PathList(0).Contains(":") AndAlso Not PathList(0).Contains(BackSlash) Then
                    PathList(0) = (PathList(0) + BackSlash)
                End If

                TreeView1.CollapseAll()

                For Each Folder As String In PathList
                    TreeView1.SelectedNode = Nothing
                    TreeView1.SelectedNode = GetNode(Folder, TreeView1.Nodes)
                    TreeView1.Select()
                    TreeView1.SelectedNode.Expand()
                Next

                L_Path.Text = ""
            Else
                L_Path.Text = "Directory doesn't exist."
            End If
        Else
            L_Path.Text = "No path was specified."
        End If
    End Sub

    Private Function GetNode(ByVal text As String, ByVal parentCollection As TreeNodeCollection) As TreeNode
        Dim ReturnNode As TreeNode = Nothing
        Dim Child As TreeNode = Nothing

        For Each Child In parentCollection 'step through the parentcollection
            If Child.Text = text Then
                ReturnNode = Child
            ElseIf Child.GetNodeCount(False) > 0 Then ' if there is child items then call this function recursively
                ReturnNode = GetNode(text, Child.Nodes)
            End If

            If ReturnNode IsNot Nothing Then
                Exit For 'if something was found, exit out of the for loop
            End If
        Next

        Return ReturnNode
    End Function

    Private Sub Txb_JumpToPath_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txb_JumpToPath.KeyPress
        If e.KeyChar = ChrW(1) Then
            DirectCast(sender, TextBox).SelectAll()
            e.Handled = True
        End If
    End Sub

    Private Sub Txb_Validation_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txb_EditString.KeyPress, Txb_RemoveString.KeyPress, Txb_PrefixString.KeyPress, Txb_SwapB.KeyPress, Txb_SwapA.KeyPress
        If e.KeyChar = ChrW(1) Then
            DirectCast(sender, TextBox).SelectAll()
            e.Handled = True
        End If

        '"^[^\\/?%*:|<>\.]+$" backup
        If Not Regex.IsMatch(e.KeyChar, "^[^\\/%:|<>]+$") Then
            e.Handled = True
        End If
    End Sub

    Private Sub Txb_Edit_Validation_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txb_EditString.KeyPress, Txb_PrefixString.KeyPress
        If e.KeyChar = ChrW(1) Then
            DirectCast(sender, TextBox).SelectAll()
            e.Handled = True
        End If

        '"^[^\\/?%*:|<>\.]+$" backup
        If Not Regex.IsMatch(e.KeyChar, "^[^\\/?%*:|<>]+$") Then
            e.Handled = True
        End If
    End Sub

    Private Sub Lbx_Before_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Lbx_Before.KeyPress
        If e.KeyChar = ChrW(1) Then
            Selecting(CType(sender, ListBox))
            e.Handled = True
        End If
    End Sub

    Private Sub Lbx_Before_MouseDown(sender As Object, e As MouseEventArgs) Handles Lbx_Before.MouseDown
        Dim senderLbx As ListBox = CType(sender, ListBox)

        If (e.Button = MouseButtons.Right) Then
            Dim Index As Integer = senderLbx.IndexFromPoint(e.X, e.Y)

            If (Index >= 0) Then
                If senderLbx.GetSelected(Index) Then
                    senderLbx.SetSelected(Index, False)
                Else
                    senderLbx.SelectedIndex = Index
                End If
            End If

        ElseIf (e.Button = MouseButtons.Middle) Then
            Selecting(senderLbx)
        End If
    End Sub

    Private Sub Lbx_Before_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles Lbx_Before.PreviewKeyDown
        If e.KeyCode = Keys.F5 Then
            Dim TreeViewNode As TreeNode = TreeView1.SelectedNode
            TreeView1.SelectedNode = Nothing
            TreeView1.SelectedNode = TreeViewNode
        End If
    End Sub

    Private Sub Lv_After_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles Lv_After.ItemSelectionChanged
        e.Item.Selected = False
    End Sub

    Private Sub Lbx_Before_SelectedValueChanged(sender As Object, e As EventArgs) Handles Lbx_Before.SelectedValueChanged
        If (SelectionInProgress = False) AndAlso (BlockSelection = False) Then
            ListBoxSelection.Clear()
            Dim senderLbx As ListBox = CType(sender, ListBox)

            For Each Item As Object In senderLbx.SelectedItems
                ListBoxSelection.Add(senderLbx.Items.IndexOf(Item))
            Next

            WorkStrings()
        End If
    End Sub

    Private Sub Selecting(sender As ListBox)
        SelectionInProgress = True

        sender.BeginUpdate()
        For i As Integer = 0 To sender.Items.Count - 1
            sender.SetSelected(i, True)

            If i = (sender.Items.Count - 2) Then
                SelectionInProgress = False
            End If
        Next

        sender.TopIndex = 0
        sender.EndUpdate()

        ListBoxSelection.Clear()
        ListBoxSelection.Add(-2)
    End Sub

    Private Sub PopulateListViewValues()
        ListViewValues.Clear()

        Parallel.Invoke(Sub()
                            For Each iString As String In Lbx_Before.SelectedItems
                                ListViewValues.Add(New ListViewItem(iString))
                            Next
                        End Sub)
    End Sub

    Private Sub UpdateListView()
        Lv_After.Items.Clear()

        Me.Invoke(New MethodInvoker(Sub()
                                        Lv_After.BeginUpdate()
                                        Lv_After.Items.AddRange(ListViewValues.ToArray())
                                        Lv_After.EndUpdate()
                                    End Sub))

        Lv_After.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
    End Sub

    Private Sub WorkStrings()
        PopulateListViewValues()
        Dim TempKeyList As New List(Of Key)
        Dim Temp2KeyList As New List(Of Key)

        For Each Item In KeyList
            Temp2KeyList.Add(New Key(Item.Index, Item.Path, Item.Name, Nothing))
        Next

        KeyList = Temp2KeyList

        Parallel.ForEach(ListViewValues, Sub(Item As ListViewItem)
                                             Dim istring As String = Item.Text

                                             If (BoolSwap = True) Then
                                                 Dim SwapA As String = Txb_SwapA.Text
                                                 Dim SwapB As String = Txb_SwapB.Text
                                                 Dim CleanBool As Boolean = True

                                                 If ((Not String.IsNullOrEmpty(SwapA)) AndAlso (Not String.IsNullOrEmpty(SwapB))) Then
                                                     If ((SwapA.Length < istring.Length) AndAlso (SwapB.Length < istring.Length)) Then
                                                         If ((SwapA.Length + SwapB.Length) < istring.Length) Then
                                                             istring = AdvancedSwap(istring, SwapA, SwapB)
                                                             CleanBool = False
                                                         End If
                                                     End If
                                                 End If

                                                 If (CleanBool = True) Then
                                                     Txb_SwapA.BackColor = SystemColors.Window
                                                     Txb_SwapB.BackColor = SystemColors.Window
                                                 End If
                                             End If

                                             If (BoolRemove = True) Then
                                                 Dim RemoveString As String = Txb_RemoveString.Text

                                                 If (String.IsNullOrEmpty(RemoveString) = False) Then
                                                     Dim isLenght As Integer = istring.Length
                                                     Dim TempRemoveStartValue As Integer = Convert.ToInt32(RemoveStartValue)
                                                     Dim TempRemoveEndValue As Integer = Convert.ToInt32(RemoveEndValue)

                                                     If (TempRemoveStartValue > isLenght) Then
                                                         TempRemoveStartValue = isLenght
                                                     End If

                                                     If (TempRemoveEndValue > isLenght) OrElse (TempRemoveEndValue = 0) Then
                                                         TempRemoveEndValue = isLenght
                                                     End If

                                                     If (TempRemoveStartValue < TempRemoveEndValue) Then
                                                         Dim TempRemoveString As String = RemoveString

                                                         If ((isLenght - TempRemoveStartValue) < TempRemoveString.Length) Then
                                                             TempRemoveString = TempRemoveString.Substring(0, (TempRemoveString.Length - (isLenght - TempRemoveEndValue)))
                                                         End If

                                                         Dim TempString As String = istring.Substring(TempRemoveStartValue, (TempRemoveEndValue - TempRemoveStartValue))

                                                         If Not (TempRemoveString.Contains(Wildcards(0)) Or TempRemoveString.Contains(Wildcards(1))) Then
                                                             If (BoolRemoveOnlyFirst = False) Then
                                                                 TempString = Replace(TempString, TempRemoveString, "", StringComparison.CurrentCultureIgnoreCase)
                                                             Else
                                                                 Dim TempRemoveIndex As Integer = TempString.IndexOf(TempRemoveString, StringComparison.CurrentCultureIgnoreCase)

                                                                 If (TempRemoveIndex >= 0) Then
                                                                     TempString = TempString.Remove(TempRemoveIndex, TempRemoveString.Length)
                                                                 End If
                                                             End If
                                                         Else
                                                             TempString = AdvancedRemove(TempString, TempRemoveString)
                                                         End If

                                                         istring = istring.Remove(TempRemoveStartValue, (TempRemoveEndValue - TempRemoveStartValue))
                                                         istring = istring.Insert(TempRemoveStartValue, TempString)
                                                     End If
                                                 End If
                                             End If

                                             If (BoolEdit = True) Then
                                                 Dim EditString As String = Txb_EditString.Text
                                                 Dim TempValue As Integer = Convert.ToInt32(EditValue)

                                                 If String.IsNullOrEmpty(EditString) = False Then
                                                     If (TempValue > istring.Length) Then
                                                         TempValue = istring.Length
                                                     End If

                                                     istring = istring.Insert(TempValue, EditString)
                                                 End If
                                             End If

                                             If (BoolPrefix = True) Then
                                                 Dim PrefixString As String = Txb_PrefixString.Text

                                                 If String.IsNullOrEmpty(PrefixString) = False Then
                                                     istring = istring.Insert(0, PrefixString)
                                                 End If
                                             End If

                                             Dim i As Integer = KeyList.FindIndex(Function(Key) Key.Name = Item.Text)
                                             Dim iKey As Key = KeyList(i)

                                             KeyList(i) = New Key(iKey.Index, iKey.Path, iKey.Name, istring)

                                             SyncLock TempKeyList
                                                 TempKeyList.Add(New Key(iKey.Index, iKey.Path, iKey.Name, istring))
                                             End SyncLock
                                         End Sub)

        TempKeyList.Sort(Function(x, y) x.Index.CompareTo(y.Index))
        Dim TempList As New List(Of ListViewItem)

        For Each Key In TempKeyList
            TempList.Add(New ListViewItem(Key.Result))
        Next

        ListViewValues = TempList
        UpdateListView()
    End Sub

    Public Function AdvancedSwap(ByVal Original As String, ByVal A As String, ByVal B As String) As String
        Dim xChar As Char = Wildcards(0) '?
        Dim yChar As Char = Wildcards(1) '*
        Dim AB As String() = New String() {A, B}

        For iAB As Integer = AB.Count - 1 To 0 Step -1
            Dim TempPattern As String = AB(iAB)
            Dim Wildcount As Integer = TempPattern.Count(Function(f) (f = xChar) OrElse (f = yChar))

            For i = 0 To Wildcount - 1
                Dim xIndex As Integer = TempPattern.IndexOf(xChar)
                Dim yIndex As Integer = TempPattern.IndexOf(yChar)
                Dim xExist As Boolean = If((xIndex < 0), False, True)
                Dim yExist As Boolean = If((yIndex < 0), False, True)
                Dim IndexChar As New Integer
                Dim yIs As New Boolean

                If ((xExist And yExist) AndAlso (xIndex < yIndex)) OrElse (yExist = False) Then
                    IndexChar = xIndex
                    yIs = False
                Else
                    IndexChar = yIndex
                    yIs = True
                End If

                Dim PatternCut As String = TempPattern.Substring(0, IndexChar)
                Dim PatternStart As Integer = Original.IndexOf(PatternCut, 0, StringComparison.CurrentCultureIgnoreCase)

                If (PatternStart < 0) OrElse ((PatternStart + IndexChar) >= Original.Length) Then
                    Return Original
                End If

                If (yIs = False) Then
                    Dim InsertChar As Char = Original.ElementAt(PatternStart + IndexChar)
                    TempPattern = TempPattern.Remove(IndexChar, 1)
                    TempPattern = TempPattern.Insert(IndexChar, InsertChar)

                ElseIf (yIs = True) Then
                    Dim SelectStart As Integer = PatternStart + IndexChar
                    Dim SelectEnd As Integer = PatternStart + IndexChar
                    Dim WildcardChar As Char = (Original.ElementAt(PatternStart + IndexChar))

                    If ((Not Char.IsLetterOrDigit(WildcardChar)) AndAlso (Not Char.IsWhiteSpace(WildcardChar))) Then
                        Return Original
                    End If

                    If (Not Char.IsWhiteSpace(WildcardChar)) Then
                        For ilenght = 1 To (PatternStart + IndexChar - 1)
                            Dim BeforeChar As Char = (Original.ElementAt(PatternStart + IndexChar - ilenght))

                            If Char.IsLetterOrDigit(BeforeChar) Then
                                SelectStart -= 1
                            Else
                                Exit For
                            End If
                        Next

                        For ilenght = 0 To (Original.Length - PatternStart - IndexChar - 1)
                            Dim AfterChar As Char = (Original.ElementAt(PatternStart + IndexChar + ilenght))

                            If Char.IsLetterOrDigit(AfterChar) Then
                                SelectEnd += 1
                            Else
                                Exit For
                            End If
                        Next
                    Else
                        SelectStart = IndexChar
                        SelectEnd = IndexChar + 1
                    End If

                    SelectStart = If((SelectStart < 0), 0, SelectStart)
                    SelectEnd = If((SelectEnd < SelectStart), SelectStart, SelectEnd)

                    Dim InsertString As String = Original.Substring(SelectStart, (SelectEnd - SelectStart))
                    Dim BeyondStart As Integer = 0
                    Dim BeyondEnd As Integer = 1

                    If (InsertString.Length > 0) Then
                        For ilenght = 1 To (IndexChar)
                            If ((InsertString.Length - ilenght) > 0) Then
                                Dim BeforeChar As Char = (InsertString.ElementAt(ilenght - 1))

                                If (Char.ToUpperInvariant(TempPattern.ElementAt(IndexChar - ilenght)) = Char.ToUpperInvariant(BeforeChar)) Then
                                    BeyondStart = 1
                                Else
                                    Exit For
                                End If
                            Else
                                Exit For
                            End If
                        Next

                        For ilenght = 1 To (TempPattern.Length - IndexChar - 1)
                            If ((InsertString.Length - ilenght) > 0) Then
                                Dim NextChar As Char = (InsertString.ElementAt(InsertString.Length - ilenght))

                                If (Char.ToUpperInvariant(TempPattern.ElementAt(IndexChar + ilenght)) = Char.ToUpperInvariant(NextChar)) Then
                                    BeyondEnd += 1
                                Else
                                    Exit For
                                End If
                            Else
                                Exit For
                            End If
                        Next
                    End If

                    TempPattern = TempPattern.Remove((IndexChar - BeyondStart), BeyondEnd + BeyondStart)
                    TempPattern = TempPattern.Insert((IndexChar - BeyondStart), InsertString)
                End If
            Next

            If String.IsNullOrEmpty(TempPattern) Then
                Return Original
            End If

            AB(iAB) = TempPattern
        Next

        A = AB(0)
        B = AB(1)

        Dim AIndex As Integer = Original.IndexOf(A, 0, StringComparison.CurrentCultureIgnoreCase)
        Dim BIndex As Integer = Original.IndexOf(B, 0, StringComparison.CurrentCultureIgnoreCase)

        Dim ALength As Integer = A.Length
        Dim BLength As Integer = B.Length
        Dim ARange As Integer = AIndex + ALength
        Dim BRange As Integer = BIndex + BLength

        Dim CurrentTbx As New TextBox

        If Equals(Me.ActiveControl, Txb_SwapA) Then
            CurrentTbx = Txb_SwapA

        ElseIf Equals(Me.ActiveControl, Txb_SwapB) Then
            CurrentTbx = Txb_SwapB
        End If

        If (AIndex < 0) OrElse (BIndex < 0) Then
            Txb_SwapA.BackColor = SystemColors.Window
            Txb_SwapB.BackColor = SystemColors.Window
            Return Original

        ElseIf (Equals(AIndex, BIndex)) Then
            Txb_SwapA.BackColor = Color.Red
            Txb_SwapB.BackColor = Color.Red
            Return Original

        ElseIf ((AIndex <= BIndex) AndAlso (ARange > BIndex)) OrElse ((BIndex <= AIndex) AndAlso (BRange > AIndex)) Then
            CurrentTbx.BackColor = Color.Red
            Return Original
        End If

        Txb_SwapA.BackColor = SystemColors.Window
        Txb_SwapB.BackColor = SystemColors.Window

        Dim Result As String = Original

        A = Result.Substring(AIndex, ALength)
        B = Result.Substring(BIndex, BLength)

        If (AIndex < BIndex) Then
            Result = ASmallerThanB(Result, AIndex, A, B)
            BIndex = (BIndex - ALength + BLength)

            If (BIndex < 0) Then
                Return Original
            End If

            Result = ASmallerThanB(Result, BIndex, B, A)
        Else
            Result = ASmallerThanB(Result, BIndex, B, A)
            AIndex = (AIndex - BLength + ALength)

            If (AIndex < 0) Then
                Return Original
            End If

            Result = ASmallerThanB(Result, AIndex, A, B)
        End If

        Return Result
    End Function

    Public Shared Function ASmallerThanB(ByVal Original As String, ByVal AIndex As Integer, ByVal ARemove As String, ByVal BInsert As String) As String
        Original = Original.Remove(AIndex, ARemove.Length)
        Original = Original.Insert(AIndex, BInsert)
        Return Original
    End Function

    Public Shared Function AdvancedRemove(ByVal Original As String, ByVal Pattern As String) As String
        Dim Result As New StringBuilder()
        Dim xChar As Char = Wildcards(0) '?
        Dim yChar As Char = Wildcards(1) '*
        Dim TempPattern As String = Pattern
        Dim Wildcount As Integer = TempPattern.Count(Function(f) (f = xChar) OrElse (f = yChar))

        If (Wildcount > Original.Length) Then
            Return Original
        End If

        For i = 0 To Wildcount - 1
            Dim xIndex As Integer = TempPattern.IndexOf(xChar)
            Dim yIndex As Integer = TempPattern.IndexOf(yChar)
            Dim xExist As Boolean = If((xIndex < 0), False, True)
            Dim yExist As Boolean = If((yIndex < 0), False, True)
            Dim IndexChar As New Integer
            Dim yIs As New Boolean

            If ((xExist And yExist) AndAlso (xIndex < yIndex)) OrElse (yExist = False) Then
                IndexChar = xIndex
                yIs = False
            Else
                IndexChar = yIndex
                yIs = True
            End If

            Dim PatternCut As String = TempPattern.Substring(0, IndexChar)
            Dim PatternStart As Integer = Original.IndexOf(PatternCut, 0, StringComparison.CurrentCultureIgnoreCase)

            If (PatternStart < 0) OrElse ((PatternStart + IndexChar) >= Original.Length) Then
                Return Original
            End If

            If (yIs = False) Then
                Dim InsertChar As Char = Original.ElementAt(PatternStart + IndexChar)
                TempPattern = TempPattern.Remove(IndexChar, 1)
                TempPattern = TempPattern.Insert(IndexChar, InsertChar)

            ElseIf (yIs = True) Then
                Dim SelectStart As Integer = PatternStart + IndexChar
                Dim SelectEnd As Integer = PatternStart + IndexChar
                Dim WildcardChar As Char = (Original.ElementAt(PatternStart + IndexChar))

                If ((Not Char.IsLetterOrDigit(WildcardChar)) AndAlso (Not Char.IsWhiteSpace(WildcardChar))) Then
                    Return Original
                End If

                If (Not Char.IsWhiteSpace(WildcardChar)) Then
                    For ilenght = 1 To (PatternStart + IndexChar - 1)
                        Dim BeforeChar As Char = (Original.ElementAt(PatternStart + IndexChar - ilenght))

                        If Char.IsLetterOrDigit(BeforeChar) Then
                            SelectStart -= 1
                        Else
                            Exit For
                        End If
                    Next

                    For ilenght = 0 To (Original.Length - PatternStart - IndexChar - 1)
                        Dim AfterChar As Char = (Original.ElementAt(PatternStart + IndexChar + ilenght))

                        If Char.IsLetterOrDigit(AfterChar) Then
                            SelectEnd += 1
                        Else
                            Exit For
                        End If
                    Next
                Else
                    SelectStart = IndexChar
                    SelectEnd = IndexChar + 1
                End If

                SelectStart = If((SelectStart < 0), 0, SelectStart)
                SelectEnd = If((SelectEnd < SelectStart), SelectStart, SelectEnd)

                Dim InsertString As String = Original.Substring(SelectStart, (SelectEnd - SelectStart))
                Dim BeyondStart As Integer = 0
                Dim BeyondEnd As Integer = 1

                If (InsertString.Length > 0) Then
                    For ilenght = 1 To (IndexChar)
                        If ((InsertString.Length - ilenght) > 0) Then
                            Dim BeforeChar As Char = (InsertString.ElementAt(ilenght - 1))

                            If (Char.ToUpperInvariant(TempPattern.ElementAt(IndexChar - ilenght)) = Char.ToUpperInvariant(BeforeChar)) Then
                                BeyondStart = 1
                            Else
                                Exit For
                            End If
                        Else
                            Exit For
                        End If
                    Next

                    For ilenght = 1 To (TempPattern.Length - IndexChar - 1)
                        If ((InsertString.Length - ilenght) > 0) Then
                            Dim NextChar As Char = (InsertString.ElementAt(InsertString.Length - ilenght))

                            If (Char.ToUpperInvariant(TempPattern.ElementAt(IndexChar + ilenght)) = Char.ToUpperInvariant(NextChar)) Then
                                BeyondEnd += 1
                            Else
                                Exit For
                            End If
                        Else
                            Exit For
                        End If
                    Next
                End If

                TempPattern = TempPattern.Remove((IndexChar - BeyondStart), BeyondEnd + BeyondStart)
                TempPattern = TempPattern.Insert((IndexChar - BeyondStart), InsertString)
            End If
        Next

        If String.IsNullOrEmpty(TempPattern) Then
            Return Original
        End If

        Dim tlPattern As Integer = TempPattern.Length
        Dim iPattern As Integer = -1
        Dim iLast As Integer = 0

        While True
            iPattern = Original.IndexOf(TempPattern, iPattern + 1, StringComparison.CurrentCultureIgnoreCase)

            If iPattern < 0 Then
                If Result.Length < 0 Then
                    Return Original
                End If

                Result.Append(Original, iLast, Original.Length - iLast)
                Exit While
            End If

            If iLast > iPattern Then
                iLast = iPattern
            End If

            Result.Append(Original, iLast, iPattern - iLast)
            iLast = iPattern + tlPattern
        End While

        Return Result.ToString()
    End Function

    Public Shared Function Replace(ByVal Original As String, ByVal Pattern As String, ByVal Replacement As String, ByVal ComparisonType As StringComparison) As String
        'If original Is Nothing Then
        '    Return Nothing
        'End If

        'If [String].IsNullOrEmpty(pattern) Then
        '    Return original
        'End If

        Dim lPattern As Integer = Pattern.Length
        Dim iPattern As Integer = -1
        Dim iLast As Integer = 0
        Dim result As New StringBuilder()

        While True
            iPattern = Original.IndexOf(Pattern, iPattern + 1, ComparisonType)

            If iPattern < 0 Then
                If result.Length < 0 Then
                    Return Original
                End If

                result.Append(Original, iLast, Original.Length - iLast)
                Exit While
            End If

            If iLast > iPattern Then
                iLast = iPattern
            End If

            result.Append(Original, iLast, iPattern - iLast)
            result.Append(Replacement)

            iLast = iPattern + lPattern
        End While

        Return result.ToString()
    End Function

    Private Sub B_Rename_Click(sender As Object, e As EventArgs) Handles B_Rename.Click
        If MessageBox.Show("Are you sure you want to rename?", "Manga Renamer", MessageBoxButtons.OKCancel) = DialogResult.OK Then
            Dim TempList As List(Of Key) = KeyList.GroupBy(Function(perKey) perKey.Result).[Select](Function(g) g.First()).ToList()
            Dim TempPath As String = TreeView1.SelectedNode.FullPath + "\"

            Parallel.ForEach(TempList, Sub(Key)
                                           If ((String.IsNullOrWhiteSpace(Key.Result) = False) AndAlso (Equals(Key.Name, Key.Result) = False) AndAlso (Directory.Exists(TempPath + Key.Result) = False)) Then
                                               My.Computer.FileSystem.RenameDirectory(Key.Path, Key.Result)
                                           End If
                                       End Sub)

            Dim SelectedNode As TreeNode = TreeView1.SelectedNode()
            TreeView1.SelectedNode() = Nothing
            TreeView1.SelectedNode() = SelectedNode
        End If
    End Sub

    Private Sub Txb_EditString_TextChanged(sender As Object, e As EventArgs) Handles Txb_EditString.TextChanged, Txb_PrefixString.TextChanged
        '"^[^\\/?%*:|<>\.]+$" backup
        CType(sender, TextBox).Text = Regex.Replace(CType(sender, TextBox).Text, "^[\\/?%*:|<>]+$", "")
        WorkStrings()
    End Sub

    Private Sub Txb_RemoveString_TextChanged(sender As Object, e As EventArgs) Handles Txb_RemoveString.TextChanged, Txb_SwapB.TextChanged, Txb_SwapA.TextChanged
        WorkStrings()
    End Sub

    Private Sub Txb_EditString_MouseDown(sender As Object, e As MouseEventArgs) Handles Txb_EditString.MouseDown
        If (e.Button = MouseButtons.Middle) AndAlso (MiddleClickOptions = True) Then
            Dim SenderTbx As TextBox = CType(sender, TextBox)
            SenderTbx.Text = "0" + SenderTbx.Text
        End If
    End Sub

    Private Sub Txb_RemoveString_MouseDown(sender As Object, e As MouseEventArgs) Handles Txb_RemoveString.MouseDown
        If (e.Button = MouseButtons.Middle) AndAlso (MiddleClickOptions = True) Then
            Dim SenderTbx As TextBox = CType(sender, TextBox)
            SenderTbx.Text = "*" + SenderTbx.Text
        End If
    End Sub

    Private Sub Txb_PrefixString_MouseDown(sender As Object, e As MouseEventArgs) Handles Txb_PrefixString.MouseDown, Txb_SwapB.MouseDown, Txb_SwapA.MouseDown
        If (e.Button = MouseButtons.Middle) AndAlso (MiddleClickOptions = True) Then
            Dim SenderTbx As TextBox = CType(sender, TextBox)
            SenderTbx.Text = "x" + SenderTbx.Text
        End If
    End Sub

    Private Sub B_Clear_Click(sender As Object, e As EventArgs) Handles B_Clear.Click
        Txb_EditString.Text = Nothing
        Txb_RemoveString.Text = Nothing
        Txb_PrefixString.Text = Nothing
        Txb_SwapA.Text = Nothing
        Txb_SwapB.Text = Nothing
    End Sub

    Private Sub B_Clear_MouseDown(sender As Object, e As MouseEventArgs) Handles B_Clear.MouseDown
        If (e.Button = MouseButtons.Right) Then
            Txb_EditString.Text = Nothing
            Txb_RemoveString.Text = Nothing
            Txb_PrefixString.Text = Nothing
            Cbx_EditString.Checked = True
            Cbx_RemoveString.Checked = True
            Cbx_PrefixString.Checked = True
            Cbx_RemoveFirstOnly.Checked = True
            Nup_EditString.Value = 0
            Nup_RemoveStart.Value = 0
            Nup_RemoveEnd.Value = 0
            Txb_SwapA.Text = Nothing
            Txb_SwapB.Text = Nothing
            Cbx_Swap.Checked = True
        End If
    End Sub

    Private Sub B_GetCurrent_Click(sender As Object, e As EventArgs) Handles B_GetCurrent.Click
        TreeView1.Select()

        If TreeView1.SelectedNode IsNot Nothing Then
            Txb_EditString.Text = TreeView1.SelectedNode.Text + Txb_EditString.Text
        End If
    End Sub

    Private Sub B_GetParent_Click(sender As Object, e As EventArgs) Handles B_GetParent.Click
        TreeView1.Select()

        If TreeView1.SelectedNode.Parent IsNot Nothing Then
            Txb_EditString.Text = TreeView1.SelectedNode.Parent.Text + Txb_EditString.Text
        Else
            TreeView1.SelectedNode.ForeColor = Color.Orange
            TreeView1.SelectedNode.ToolTipText = "Doesn't have a parent."
        End If
    End Sub

    Private Sub B_Next_Click(sender As Object, e As EventArgs) Handles B_Next.Click
        TreeView1.Select()

        If TreeView1.SelectedNode.NextNode() IsNot Nothing Then
            TreeView1.SelectedNode.Collapse()
            TreeView1.SelectedNode = TreeView1.SelectedNode.NextNode()
            TreeView1.Select()
            Selecting(Lbx_Before)
        End If
    End Sub

    Private Sub MItem_IgnoreExceptions_CheckedChanged(sender As Object, e As EventArgs) Handles MItem_IgnoreExceptions.CheckedChanged
        If CType(sender, ToolStripMenuItem).Checked Then
            IgnoreException = True
        Else
            IgnoreException = False
        End If
    End Sub

    Private Sub MItem_TbxMiddleClick_CheckedChanged(sender As Object, e As EventArgs) Handles MItem_MiddleClick.CheckedChanged
        If CType(sender, ToolStripMenuItem).Checked Then
            MiddleClickOptions = True
        Else
            MiddleClickOptions = False
        End If
    End Sub

    Protected Sub OnTitlebarClick(pos As Point)
        Menu_Main.Show(pos)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = &HA4 Then
            Dim pos As New Point(m.LParam.ToInt32() And &HFFFF, m.LParam.ToInt32() >> 16)
            OnTitlebarClick(pos)
            Return
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub Menu_Main_Opening(sender As Object, e As CancelEventArgs) Handles Menu_Main.Opening
        MItem_IgnoreExceptions.Checked = IgnoreException
        MItem_MiddleClick.Checked = MiddleClickOptions
    End Sub

    Private Sub DrawTreeNodeHighlightSelectedEvenWithoutFocus(sender As Object, e As DrawTreeNodeEventArgs) Handles TreeView1.DrawNode
        Dim foreColor As Color

        If e.Node Is DirectCast(sender, TreeView).SelectedNode Then
            ' is selected, draw a HightliteText rectangle under the text
            foreColor = SystemColors.HighlightText
            e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds)
            ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds, foreColor, SystemColors.Highlight)
        Else
            foreColor = If((e.Node.ForeColor = Color.Empty), DirectCast(sender, TreeView).ForeColor, e.Node.ForeColor)
            e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds)
        End If

        TextRenderer.DrawText(e.Graphics, e.Node.Text, If(e.Node.NodeFont, e.Node.TreeView.Font), e.Bounds, foreColor, TextFormatFlags.GlyphOverhangPadding)
    End Sub

    Private Sub Cbx_EditString_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_EditString.CheckedChanged
        BoolEdit = CType(sender, CheckBox).Checked
        CheckIfLoaded()
    End Sub

    Private Sub Cbx_RemoveString_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_RemoveString.CheckedChanged
        BoolRemove = CType(sender, CheckBox).Checked
        CheckIfLoaded()
    End Sub

    Private Sub Cbx_PrefixString_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_PrefixString.CheckedChanged
        BoolPrefix = CType(sender, CheckBox).Checked
        CheckIfLoaded()
    End Sub

    Private Sub Cbx_RemoveFirstOnly_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_RemoveFirstOnly.CheckedChanged
        BoolRemoveOnlyFirst = CType(sender, CheckBox).Checked
        CheckIfLoaded()
    End Sub

    Private Sub Cbx_Swap_CheckedChanged(sender As Object, e As EventArgs) Handles Cbx_Swap.CheckedChanged
        BoolSwap = CType(sender, CheckBox).Checked
        CheckIfLoaded()
    End Sub

    Private Sub CheckIfLoaded()
        If (IsLoaded = True) Then
            WorkStrings()
        End If
    End Sub

    'Private Sub Txb_EditString_MouseHover(sender As Object, e As EventArgs) Handles Txb_EditString.MouseHover, Cbx_EditString.MouseHover, Nup_EditString.MouseHover
    '    Dim sToolTip As New ToolTip
    '    sToolTip.Show("Insert", CType(sender, Control))
    'End Sub

    'Private Sub Txb_RemoveString_MouseHover(sender As Object, e As EventArgs) Handles Txb_RemoveString.MouseHover, Cbx_RemoveString.MouseHover, Nup_RemoveStart.MouseHover, Nup_RemoveEnd.MouseHover, Cbx_RemoveFirstOnly.MouseHover
    '    Dim sToolTip As New ToolTip
    '    sToolTip.Show("Remove", CType(sender, Control))
    'End Sub

    'Private Sub Txb_PrefixString_MouseHover(sender As Object, e As EventArgs) Handles Txb_PrefixString.MouseHover, Cbx_PrefixString.MouseHover
    '    Dim sToolTip As New ToolTip
    '    sToolTip.Show("Prefix", CType(sender, Control))
    'End Sub

    'Private Sub Txb_Swap_MouseHover(sender As Object, e As EventArgs) Handles Txb_SwapA.MouseHover, Txb_SwapB.MouseHover, Cbx_Swap.MouseHover
    '    Dim sToolTip As New ToolTip
    '    sToolTip.Show("Swap", CType(sender, Control))
    'End Sub

    Private Sub Nup_EditString_ValueChanged(sender As Object, e As EventArgs) Handles Nup_EditString.ValueChanged
        EditValue = CType(sender, NumericUpDown).Value
        WorkStrings()
    End Sub

    Private Sub Nup_RemoveStart_ValueChanged(sender As Object, e As EventArgs) Handles Nup_RemoveStart.ValueChanged
        RemoveStartValue = CType(sender, NumericUpDown).Value

        If (RemoveStartValue >= RemoveEndValue) AndAlso (Not (RemoveEndValue = 0)) Then
            CType(sender, NumericUpDown).BackColor = Color.Red
        Else
            CType(sender, NumericUpDown).BackColor = SystemColors.Window
        End If

        WorkStrings()
    End Sub

    Private Sub Nup_RemoveEnd_ValueChanged(sender As Object, e As EventArgs) Handles Nup_RemoveEnd.ValueChanged
        RemoveEndValue = CType(sender, NumericUpDown).Value

        If (RemoveStartValue < RemoveEndValue) OrElse (RemoveEndValue = 0) Then
            Nup_RemoveStart.BackColor = SystemColors.Window
        Else
            Nup_RemoveStart.BackColor = Color.Red
        End If

        WorkStrings()
    End Sub


    Private Sub Nup_RemoveEnd_MouseDown(sender As Object, e As MouseEventArgs) Handles Nup_RemoveStart.MouseDown, Nup_RemoveEnd.MouseDown, Nup_EditString.MouseDown
        If (MiddleClickOptions = True) AndAlso (e.Button = MouseButtons.Right) Then
            CType(sender, NumericUpDown).Value = 0
        End If
    End Sub

    Private Sub ScrollHandlerFunction(sender As Object, e As MouseEventArgs) Handles Nup_EditString.MouseWheel, Nup_RemoveStart.MouseWheel, Nup_RemoveEnd.MouseWheel
        Dim handledArgs As HandledMouseEventArgs = TryCast(e, HandledMouseEventArgs)
        handledArgs.Handled = True

        Dim iTemp As Decimal = CType(sender, NumericUpDown).Value

        If (handledArgs.Delta > 0) Then
            iTemp += 1
        Else
            iTemp += -1
        End If

        If iTemp < 0 Then
            iTemp = 0
        End If

        CType(sender, NumericUpDown).Value = iTemp
    End Sub

End Class

''' <summary>
''' Sorts alphanumerically.
''' </summary>
Public Class AlphanumComparator
    Implements IComparer

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare

        ' [1] Validate the arguments.
        Dim s1 As String = CType(x, String)
        If s1 = Nothing Then
            Return 0
        End If

        Dim s2 As String = CType(y, String)
        If s2 = Nothing Then
            Return 0
        End If

        Dim len1 As Integer = s1.Length
        Dim len2 As Integer = s2.Length
        Dim marker1 As Integer = 0
        Dim marker2 As Integer = 0

        ' [2] Loop over both Strings.
        While marker1 < len1 And marker2 < len2

            ' [3] Get Chars.
            Dim ch1 As Char = s1(marker1)
            Dim ch2 As Char = s2(marker2)

            Dim space1(len1) As Char
            Dim loc1 As Integer = 0
            Dim space2(len2) As Char
            Dim loc2 As Integer = 0

            ' [4] Collect digits for String one.
            Do
                space1(loc1) = ch1
                loc1 += 1
                marker1 += 1

                If marker1 < len1 Then
                    ch1 = s1(marker1)
                Else
                    Exit Do
                End If
            Loop While Char.IsDigit(ch1) = Char.IsDigit(space1(0))

            ' [5] Collect digits for String two.
            Do
                space2(loc2) = ch2
                loc2 += 1
                marker2 += 1

                If marker2 < len2 Then
                    ch2 = s2(marker2)
                Else
                    Exit Do
                End If
            Loop While Char.IsDigit(ch2) = Char.IsDigit(space2(0))

            ' [6] Convert to Strings.
            Dim str1 = New String(space1)
            Dim str2 = New String(space2)

            ' [7] Parse Strings into Integers.
            Dim result As Integer
            If Char.IsDigit(space1(0)) And Char.IsDigit(space2(0)) Then
                Dim thisNumericChunk = Integer.Parse(str1)
                Dim thatNumericChunk = Integer.Parse(str2)
                result = thisNumericChunk.CompareTo(thatNumericChunk)
            Else
                result = str1.CompareTo(str2)
            End If

            ' [8] Return result if not equal.
            If Not result = 0 Then
                Return result
            End If
        End While

        ' [9] Compare lengths.
        Return len1 - len2
    End Function
End Class
