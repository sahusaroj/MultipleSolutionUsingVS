using DotNetLibrariesC.LookUps;
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
    public partial class Form2 : Form
    {
        SortableBindingList<Data> lstContractList = new SortableBindingList<Data>();

        public Form2()
        {
            InitializeComponent();
        }

        private void dgvContractList_KeyDown(object sender, KeyEventArgs e)
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
                PasteClipboard();
                //BindListViewItems();
            }
        }
        private void PasteClipboard()
        {
            DataObject dObject = (DataObject)Clipboard.GetDataObject();
            if (dObject.GetDataPresent(DataFormats.Text))
            {
                string[] pastedRows = Regex.Split(dObject.GetData(DataFormats.Text).ToString().TrimEnd("\r\n".ToCharArray()), "\r\n");
                SortableBindingList<Data> contractList = new SortableBindingList<Data>();
                foreach (string pastedRow in pastedRows)
                {
                    string[] pastedRowCells = pastedRow.Split(new char[] { '\t' });

                    if (pastedRowCells.Length > 0)
                        contractList.Add(new Data { Contract = pastedRowCells[0] }); //, Status = pastedRowCells[1]

                }

                //list of status
                SortableBindingList<Data> itemStatus = new SortableBindingList<Data>()
                {
                    new Data { Contract = "1", Status = "Completed" }, new Data { Contract = "4", Status = "Closed" }, new Data { Contract = "2", Status = "Pre-close" },
                    new Data { Contract = "3", Status = "INVALID" },
                };

                foreach (var i in itemStatus)
                {
                    var itemToChange = contractList.First(c => c.Contract == i.Contract).Status = i.Status;
                }

                LoadList(contractList);
            }

        }
        private void LoadList(SortableBindingList<Data> item)
        {
            if (lstContractList.Count > 0)
                lstContractList = item;
            else
                lstContractList.AddNew();
                
            var list = new BindingList<Data>(item);
            dgvContractList.DataSource = list;
            //SortableBindingList<Data> items = (SortableBindingList<Data>)dgvContractList.DataSource;
            //items.ToList().AddRange(item);
            //ContractsBindingSource.DataSource = items;
            //dgvContractList.DataSource = ContractsBindingSource;
            var clist = new List<Data>()
                {
                    new Data { Contract = "Joe", },
                    new Data { Contract = "Misha", },
                };
            var bindingList = new BindingList<Data>(clist);
            var source = new BindingSource(bindingList, null);
            dgvContractList.DataSource = source;

        }

    }
}
