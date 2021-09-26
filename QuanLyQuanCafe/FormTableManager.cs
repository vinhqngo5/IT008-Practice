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
        void LoadTable()
        {
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            foreach (Table item in tableList)
            {
                Button btn = new Button()
                {
                    Text = item.Name + Environment.NewLine + (item.Status ? "Đầy" : "Trống"),
                    BackColor = item.Status ? Color.LightPink : Color.Aqua,
                    Width = TableDAO.TableWidth,
                    Height = TableDAO.TableHeight
                };

                btn.Click += btn_Click;
                btn.Tag = item;

                flpTable.Controls.Add(btn);

            }
        }

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenyByTable(BillDAO.Instance.GetUncheckBillIdByTableID(id));

            foreach (DTO.Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add((item.Count.ToString()));
                lsvItem.SubItems.Add((item.Price.ToString()));
                lsvItem.SubItems.Add((item.TotalPrice.ToString()));
                lsvBill.Items.Add(lsvItem);
            }
        }

        #endregion


        #region Events
        private void btn_Click(object sender, EventArgs e)
        {
            int tableId = ((sender as Button).Tag as Table).Id;
            ShowBill(tableId);
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
