using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class FormTableManager : Form
    {
        public FormTableManager()
        {
            InitializeComponent();
            LoadTable();
        }
        #region Method
        private void LoadTable()
        {
            List<Table> tableList = TableDAO.Instance.LoadTableList();
            foreach(Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = item.Name + Environment.NewLine;
                btn.Click += Btn_Click;
                btn.Tag = item;
                if (item.Status)
                {
                    btn.Text += "Có người";
                    btn.BackColor = Color.LightPink;
                }    
                else
                {
                    btn.Text += "Trống";
                    btn.BackColor = Color.Aqua;
                }    
                flpTable.Controls.Add(btn); 

            }    
        }
        private void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<DTO.Menu> listMenu = MenuDAO.Instance.GetListMenuByTable(id);
            foreach(DTO.Menu menu in listMenu )
            {
                ListViewItem lsvItem = new ListViewItem(menu.FoodName.ToString());
                lsvItem.SubItems.Add(menu.Count.ToString());
                lsvItem.SubItems.Add(menu.Price.ToString());
                lsvItem.SubItems.Add(menu.TotalPrice.ToString());
                lsvBill.Items.Add(lsvItem);
            }    
        }


        #endregion
        #region Events
        private void Btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).Id;
            ShowBill(tableID);
        }
        private void TsmiLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TsmiAbout_Click(object sender, EventArgs e)
        {
            FormAccountProfile f = new FormAccountProfile();
            f.ShowDialog();
        }

        private void TsmiAdmin_Click(object sender, EventArgs e)
        {
            FormAdmin f = new FormAdmin();
            f.ShowDialog();
        }
        #endregion
    }
}
