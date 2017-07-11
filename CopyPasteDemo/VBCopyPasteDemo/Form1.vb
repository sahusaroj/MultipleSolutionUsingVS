Imports System.Data.SqlClient
Imports System.Text
Imports System.Text.RegularExpressions

Public Class frmContract

    Dim contractFilteredList As New List(Of ContractStatus)()

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If lstContractList.SelectedItems.Count > 0 Then
            'Dim confirmation = MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            'If confirmation = DialogResult.Yes Then
            For Each item As ListViewItem In lstContractList.SelectedItems
                lstContractList.Items.Remove(item)
                'for (int i = lstContractList.SelectedItems.Count - 1; i >= 0; i--)
                '{
                '    lstContractList.Items[lstContractList.SelectedItems[i].Index].Remove();
                '}
            Next
            'End If
            'Else
            '    MessageBox.Show("Contract not selected")
        End If
    End Sub

    Private Sub btnRemoveAll_Click(sender As Object, e As EventArgs) Handles btnRemoveAll.Click
        lstContractList.Items.Clear()
    End Sub

    Private Sub btnPreClose_Click(sender As Object, e As EventArgs) Handles btnPreClose.Click
        Dim count As Integer = contractFilteredList.FindAll(Function(x) x.Status = "Completed").Count
        If count > 0 Then
            MsgBox("There there are " + count.ToString() + " number of contracts that cannot be pre-closed")
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'get the status and bind
        Dim contractDataList As New List(Of ContractStatus)()
        GetContractList(txtContract.Text.Trim(), contractDataList)
        LoadList(contractDataList)
        contractFilteredList.AddRange(contractDataList)
        'lstContractList.Items.Add(New ListViewItem(txtContract.Text))
        txtContract.Clear()
    End Sub

    Private Sub frmContract_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeListView()
        AddContextMenu()
        'AddContextMenuAndItems()
    End Sub

    Public Sub AddContextMenu()
        Dim menuStrip As New ContextMenuStrip()
        Dim menuItem As New ToolStripMenuItem("Exit")
        menuItem.Name = "Exit"
        'menuItem.ShortcutKeys = Keys.Alt|Keys.
        ' Associate an event handler with an event.
        AddHandler menuItem.Click, AddressOf Me.menuItem_Click
        menuStrip.Items.Add(menuItem)
        Me.ContextMenuStrip = menuStrip
        Dim menuItem1 As New ToolStripMenuItem("Remove")
        menuItem1.Name = "Remove"
        AddHandler menuItem1.Click, AddressOf Me.menuItem_Click
        menuStrip.Items.Add(menuItem1)
        Me.ContextMenuStrip = menuStrip
        Dim menuItem2 As New ToolStripMenuItem("Paste")
        menuItem2.Name = "Paste"
        AddHandler menuItem2.Click, AddressOf Me.menuItem_Click
        menuStrip.Items.Add(menuItem2)
        Me.ContextMenuStrip = menuStrip
    End Sub

    Protected Sub menuItem_Click(sender As Object, e As EventArgs)
        Dim menuItem As ToolStripItem = DirectCast(sender, ToolStripItem)
        If menuItem.Name = "Exit" Then
            Application.[Exit]()
        End If
    End Sub

    Public Sub AddContextMenuAndItems()
        Dim mnuContextMenu As New ContextMenu()
        Me.ContextMenu = mnuContextMenu
        Dim mnuItemRemove As New MenuItem()
        mnuItemRemove.Text = "&Remove"
        mnuContextMenu.MenuItems.Add(mnuItemRemove)

        Dim mnuItemRemoveAll As New MenuItem()
        mnuItemRemoveAll.Text = "Remove &All"
        mnuContextMenu.MenuItems.Add(mnuItemRemoveAll)

        Dim mnuItemRemoveInValid As New MenuItem()
        mnuItemRemoveInValid.Text = "Remove &Invalid"
        mnuContextMenu.MenuItems.Add(mnuItemRemoveInValid)

        Dim mnuItemPaste As New MenuItem()
        mnuItemPaste.Text = "&Paste"
        mnuContextMenu.MenuItems.Add(mnuItemPaste)

        lstContractList.ContextMenu = mnuContextMenu

    End Sub

    Private Sub InitializeListView()

        ' Set the view to show details.
        lstContractList.View = View.Details

        ' Allow the user to edit item text.
        lstContractList.LabelEdit = True

        ' Allow the user to rearrange columns.
        lstContractList.AllowColumnReorder = True

        ' Select the item And subitems when selection Is made.
        lstContractList.FullRowSelect = True

        ' Display grid lines.
        lstContractList.GridLines = True

        'lstContractList.

        ' Sort the items in the list in ascending order.
        lstContractList.Sorting = SortOrder.Ascending

        ' Attach Subitems to the ListView
        lstContractList.Columns.Add("Contract", 170, HorizontalAlignment.Left)
        lstContractList.Columns.Add("Status", 185, HorizontalAlignment.Left)

    End Sub

    Private Sub lstContractList_KeyDown(sender As Object, e As KeyEventArgs) Handles lstContractList.KeyDown
        'ctrl-c
        If e.Control And e.KeyCode = Keys.C Then
            ' add your processing code, this is an example 
        End If
        'ctrl-v
        If e.Control And e.KeyCode = Keys.V Then
            PasteClipboard(lstContractList)
        End If
    End Sub


    Private Sub PasteClipboard(myListView As ListView)
        'Get the copied item to be paste
        Dim dObject As DataObject = DirectCast(Clipboard.GetDataObject(), DataObject)
        If dObject.GetDataPresent(DataFormats.Text) Then
            Dim pastedRows As String() = Regex.Split(dObject.GetData(DataFormats.Text.Trim).ToString().TrimEnd(vbCrLf.ToCharArray()), vbCrLf)
            Dim contractList As New List(Of ContractStatus)()
            Dim contractID As String = String.Empty
            Dim contractDataList As New List(Of ContractStatus)()
            For Each pastedRow As String In pastedRows
                Dim pastedRowCells As String() = pastedRow.Split(New Char() {ControlChars.Tab})

                If pastedRowCells.Length > 0 Then
                    contractList.Add(New ContractStatus() With {.Contract = pastedRowCells(0)})   ', Status = pastedRowCells[1]
                    contractID += pastedRowCells(0) + ","
                End If
            Next

            contractID = contractID.Remove(contractID.Length - 1)
            'remove the last comma from the stringbuilder
            'sbContractID.Length = sbContractID.Length - 1
            'sbContractID.Remove(sbContractID.Length - 1, 1)
            'sbContractID.ToString().TrimEnd(',')
            GetContractList(contractID, contractDataList)


            '    'list of status should get from DB by passing the contract
            '    Dim itemStatus As New List(Of Data)() From {
            '    New Data() With {
            '        .Contract = "1",
            '        .Status = "Completed"
            '    },
            '    New Data() With {
            '        .Contract = "4",
            '        .Status = "Closed"
            '    },
            '    New Data() With {
            '        .Contract = "2",
            '        .Status = "Pre-close"
            '    },
            '    New Data() With {
            '        .Contract = "3",
            '        .Status = "INVALID"
            '    }
            '}

            '    For Each i As Data In itemStatus
            '        contractList.First(Function(c) c.Contract = i.Contract).Status = i.Status
            '    Next

            'LoadList(contractList)
            LoadList(contractDataList)
            contractFilteredList.AddRange(contractDataList)
        End If
    End Sub

    Private Shared Sub GetContractList(contractID As String, contractDataList As List(Of ContractStatus))
        Dim connetionString As String
        Dim connection As SqlConnection
        Dim command As New SqlCommand
        Dim param As SqlParameter
        'Dim sql As String
        connetionString = "Data Source=SAROJSAHU-PC;Initial Catalog=FSL2UK02;User ID=sa;Password=Exch3qu3r#1" 'Windows Auth-"Server= localhost; Database= employeedetails;Integrated Security=SSPI;"
        'sql = "SELECT CP.[ContractID] As Contract,CS.ContractStatus AS Status FROM [dbo].[tblContract_PROTECTED] CP INNER JOIN [dbo].tblContractStatus CS ON CP.ContractStatusID = CS.[ContractStatusID] 
        '            WHERE [ContractID] IN ( SELECT t.c.value('.', 'VARCHAR(20)')
        '	            FROM (SELECT x = CAST('<t>' + REPLACE('" + contractID + "', ',', '</t><t>') + '</t>' AS XML)
        '                                          ) a
        '                                  CROSS APPLY x.nodes('/t') t(c))"
        'sql = "SELECT * INTO #TempContract FROM dbo.SplitString('" + contractID + "',',') 
        '    SELECT t.Item [Contract], ISNULL((SELECT cs.ContractStatus FROM tblContractStatus AS cs WHERE cs.ContractStatusID = c.ContractStatusID),'INVALID') [Status]
        '    FROM [tblContract_PROTECTED] AS c
        '    RIGHT JOIN #TempContract AS t ON c.ContractID=t.Item 
        '    DROP TABLE #TempContract"
        'Setup SQL Command
        connection = New SqlConnection(connetionString)
        Try
            connection.Open()
            'command = New SqlCommand(sql, connection)

            ' Create the command with the sproc name and add the parameter required
            command.Connection = connection
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = "fsspGetContractStatusByContractID"
            param = New SqlParameter("@ContractID", contractID)
            param.Direction = ParameterDirection.Input
            param.DbType = DbType.String
            command.Parameters.Add(param)

            'command to create an SqlDataReader on the result of the sproc'
            Dim sqlReader As SqlDataReader = command.ExecuteReader()
            While sqlReader.Read()
                ' Get the first and second field frm the reader'
                contractDataList.Add(New ContractStatus() With {.Contract = sqlReader(0).ToString(), .Status = sqlReader(1).ToString()})
            End While
            sqlReader.Close()
            command.Dispose()
            connection.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadList(item As List(Of ContractStatus))

        'Create two ImageList objects.
        Dim imageListSmall As New ImageList()
        ' Initialize the ImageList objects with bitmaps.
        imageListSmall.Images.Add(Bitmap.FromFile("C:\Working\Exchequer Bespoke\Bespoke\White House Products Ltd\Advanced.Bespoke.Whitehouse\Icons\Error.png"))
        imageListSmall.Images.Add(Bitmap.FromFile("C:\Working\Exchequer Bespoke\Bespoke\White House Products Ltd\Advanced.Bespoke.Whitehouse\Icons\Ready.png"))
        'Assign the ImageList objects to the ListView.
        lstContractList.SmallImageList = imageListSmall

        Dim lvi As ListViewItem
        'Add each Row as a ListViewItem 
        For Each data As ContractStatus In item
            lvi = New ListViewItem(data.Contract)
            lvi.SubItems.Add(data.Status)
            lvi.Tag = data

            ' Sets the first list item to display the 4th image.

            lstContractList.Items.Add(lvi)
            If data.Status = "INVALID" Then
                lstContractList.Items(lstContractList.Items.Count - 1).ImageIndex = 0
            Else
                lstContractList.Items(lstContractList.Items.Count - 1).ImageIndex = 1

            End If
            ' Add the ListView to the control collection.
            'Me.Controls.Add(lstContractList)
        Next

        'lstContractList

    End Sub



    Private Sub lstContractList_AfterLabelEdit(sender As Object, e As LabelEditEventArgs) Handles lstContractList.AfterLabelEdit
        ' Determine if label is changed by checking to see if it is equal to Nothing.
        If e.Label Is Nothing Then
            Return
        End If
        ' ASCIIEncoding is used to determine if a number character has been entered.
        Dim AE As New ASCIIEncoding()
        ' Convert the new label to a character array.
        Dim temp As Char() = e.Label.ToCharArray()

        'lstContractList.FindItemWithText(lstContractList.)


        ' Check each character in the new label to determine if it is a number.
        Dim x As Integer
        For x = 0 To temp.Length - 1
            ' Encode the character from the character array to its ASCII code.
            Dim bc As Byte() = AE.GetBytes(temp(x).ToString())

            ' Determine if the ASCII code is within the valid range of numerical values.
            If bc(0) > 47 And bc(0) < 58 Then
                ' Cancel the event and return the lable to its original state.
                e.CancelEdit = True
                ' Display a MessageBox alerting the user that numbers are not allowed.
                MessageBox.Show("The text for the item cannot contain numerical values.")
                ' Break out of the loop and exit.
                Return
            End If
        Next x
    End Sub
End Class


Class ContractStatus
    Public Property Contract() As String
        Get
            Return m_Contract
        End Get
        Set
            m_Contract = Value
        End Set
    End Property
    Private m_Contract As String
    Public Property Status() As String
        Get
            Return m_Status
        End Get
        Set
            m_Status = Value
        End Set
    End Property
    Private m_Status As String
End Class
