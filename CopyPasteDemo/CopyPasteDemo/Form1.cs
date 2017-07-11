using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyPasteDemo
{
    public partial class frmContractList : Form
    {
        public frmContractList()
        {
            InitializeComponent();
        }

        // Initialize ListView
        private void InitializeListView()
        {
            // Set the view to show details.
            lstContractList.View = View.Details;

            // Allow the user to edit item text.
            lstContractList.LabelEdit = true;

            // Allow the user to rearrange columns.
            lstContractList.AllowColumnReorder = true;

            // Select the item and subitems when selection is made.
            lstContractList.FullRowSelect = true;

            // Display grid lines.
            lstContractList.GridLines = true;

            //lstContractList.

            // Sort the items in the list in ascending order.
            lstContractList.Sorting = SortOrder.Ascending;

            // Attach Subitems to the ListView
            lstContractList.Columns.Add("Contract", 120, HorizontalAlignment.Left);
            lstContractList.Columns.Add("Logo", 40, HorizontalAlignment.Left);
            lstContractList.Columns.Add("Status", 185, HorizontalAlignment.Left);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeListView();
            // Set the view to show details.
            //lstContractList.View = View.Details;
            //lstContractList.Columns.Add("Contract", 170, HorizontalAlignment.Left);
            //lstContractList.Columns.Add("Stautus", 185, HorizontalAlignment.Left);
            AddItem(lstContractList, "102-01", Properties.Resources.Ready, "Completed");
            AddItem(lstContractList, "102", Properties.Resources.Error, "Error");
            AddItem(lstContractList, "102-02", Properties.Resources.Warning, "Invalid");
        }


        // Make a server status item.
        private void AddItem(ListView lvw, string server, Image logo, string desc)//Color status,
        {
            // Make the item.
            ListViewItem item = new ListViewItem(server);

            // Save the ServeStatus item in the Tag property.
            Data server_status = new Data(server, logo, desc);// status,
            item.Tag = server_status;
            item.SubItems[0].Name = "Contract";

            // Add subitems so they can draw.
            item.SubItems.Add("Logo");
            item.SubItems.Add("Status");
            //item.SubItems.Add("Desc");

            // Add the item to the ListView.
            lvw.Items.Add(item);
        }

        private void lstContractList_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void lstContractList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                // add your processing code, this is an example 
                MessageBox.Show("You have pressed ctrl-c!");
            }
            if (e.Control && e.KeyCode == Keys.V)
            {
                // add your processing code, this is an example 
                MessageBox.Show("You have pressed ctrl-v!");
                PasteClipboard(lstContractList);
                //BindListViewItems();
            }
        }

        private void BindListViewItems()
        {
            //lstContractList.Items.Clear();
            List<Data> item = new List<Data>()
            {
                new Data { Contract = "1", Status = "Completed" }, new Data { Contract = "2", Status = "Closed" }, new Data { Contract = "3", Status = "Pre-close" },
                };
            LoadList(item);
        }

        private void PasteClipboard(ListView myListView)
        {
            DataObject dObject = (DataObject)Clipboard.GetDataObject();
            if (dObject.GetDataPresent(DataFormats.Text))
            {
                string[] pastedRows = Regex.Split(dObject.GetData(DataFormats.Text).ToString().TrimEnd("\r\n".ToCharArray()), "\r\n");
                List<Data> contractList = new List<Data>();
                foreach (string pastedRow in pastedRows)
                {
                    string[] pastedRowCells = pastedRow.Split(new char[] { '\t' });

                    if (pastedRowCells.Length > 0)
                        contractList.Add(new Data { Contract = pastedRowCells[0] }); //, Status = pastedRowCells[1]

                }

                //list of status
                List<Data> itemStatus = new List<Data>()
                {
                    new Data { Contract = "1", Logo=Properties.Resources.Error, Status = "Error" }, new Data { Contract = "4", Logo=Properties.Resources.Error, Status = "Closed" }, new Data { Contract = "2", Logo=Properties.Resources.Ready, Status = "Completed" },
                    new Data { Contract = "3", Logo=Properties.Resources.Warning, Status = "INVALID" },
                };

                foreach (var i in itemStatus)
                {
                    var itemToChange = contractList.First(c => c.Contract == i.Contract).Status = i.Status;
                    var logo = contractList.First(c => c.Contract == i.Contract).Logo = i.Logo;
                }

                LoadList(contractList);
            }
        }
       
        private void LoadList(List<Data> item)
        {

            ListViewItem lvi;
            //Add each Row as a ListViewItem 
            foreach (Data data in item)
            {
                lvi = new ListViewItem(data.Contract);
                Data dataItem = new Data(data.Contract, data.Logo, data.Status);
                lvi.Tag = data;
                lvi.SubItems[0].Name = "Contract";
                // Add subitems so they can draw.
                lvi.SubItems.Add("Contract");
                lvi.SubItems.Add("Status");
                //lvi.SubItems.Add(data.Logo);
                //lvi.SubItems.Add(data.Status);
                //lvi.Tag = data;
                lstContractList.Items.Add(lvi);
            }
        }

        private void lstContractList_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            lstContractList.Items.Add(new ListViewItem(txtContract.Text));
            txtContract.Clear();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstContractList.SelectedItems.Count > 0)
            {
                var confirmation = MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation == DialogResult.Yes)
                {
                    foreach (ListViewItem item in lstContractList.SelectedItems)
                    {
                        lstContractList.Items.Remove(item);
                    }
                    //for (int i = lstContractList.SelectedItems.Count - 1; i >= 0; i--)
                    //{
                    //    lstContractList.Items[lstContractList.SelectedItems[i].Index].Remove();
                    //}
                }
            }
            else
                MessageBox.Show("Contract not selected");
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            lstContractList.Items.Clear();
        }

        private void btnPreClose_Click(object sender, EventArgs e)
        {

        }

        private void lstContractList_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (StringFormat string_format = new StringFormat())
            {
                string_format.Alignment = StringAlignment.Center;
                string_format.LineAlignment = StringAlignment.Center;

                string text = lstContractList.Columns[e.ColumnIndex].Text;
                switch (e.ColumnIndex)
                {
                    case 0:
                        e.Graphics.DrawString(text, lstContractList.Font, Brushes.Black, e.Bounds);
                        break;
                    case 1:
                        e.Graphics.DrawString(text, lstContractList.Font, Brushes.Black, e.Bounds);
                        break;
                    case 2:
                        e.Graphics.DrawString(text, lstContractList.Font, Brushes.Black, e.Bounds);
                        break;
                    //case 3:
                    //    e.Graphics.DrawString(text, lstContractList.Font, Brushes.Black, e.Bounds);
                    //    break;
                }
            }
        }

        private void lstContractList_DrawItem(object sender, DrawListViewItemEventArgs e)
        {           
            // Get the ListView item and the ServerStatus object.
            ListViewItem item = e.Item;
            Data server_status = item.Tag as Data;

            // Clear.
            e.DrawBackground();


            // Draw the image.
            e.Graphics.DrawImage(server_status.Logo, 0, 0);

            // Draw the focus rectangle if appropriate.
            e.Graphics.ResetTransform();
            e.DrawFocusRectangle();
        }

        private void lstContractList_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            // Get the ListView item and the ServerStatus object.
            ListViewItem item = e.Item;
            Data server_status = item.Tag as Data;

            // Draw.
            switch (e.ColumnIndex)
            {
                case 0:
                    // Draw the server's name.
                    e.Graphics.DrawString(server_status.Contract,
                        lstContractList.Font, Brushes.Black, e.Bounds);
                    break;
                case 1:
                    // Draw the server's logo.
                    float scale = e.Bounds.Height / (float)server_status.Logo.Height;
                    e.Graphics.ScaleTransform(scale, scale);
                    e.Graphics.TranslateTransform(
                        e.Bounds.Left,
                        e.Bounds.Top + (e.Bounds.Height - server_status.Logo.Height * scale) / 2,
                        System.Drawing.Drawing2D.MatrixOrder.Append);
                    e.Graphics.DrawImage(server_status.Logo, 0, 0);
                    break;
                case 2:
                    // Draw the server's name.
                    e.Graphics.DrawString(server_status.Status,
                        lstContractList.Font, Brushes.Black, e.Bounds);
                    break;
            }

            // Draw the focus rectangle if appropriate.
            e.Graphics.ResetTransform();
            ListView lvw = e.Item.ListView;
            if (lvw.FullRowSelect)
            {
                e.DrawFocusRectangle(e.Item.Bounds);
            }
            else if (e.SubItem.Name == "Server")
            {
                e.DrawFocusRectangle(e.Bounds);
            }
        }
    }

    class Data
    {
        public string Contract { get; set; }
        public Image Logo;
        public string Status { get; set; }
        public Data()
        { }

        public Data(string contract, Image logo, string status)
        {
            Contract = contract;
            Logo = logo;
            Status = status;
        }
    }
}
