using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

            LoadCategory();
            
        }

        #region Methods
        void LoadCategory()
        {
            List<Category> listcategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listcategory;
            cbCategory.DisplayMember = "Name";
        }
        void LoadFoodListByCategoryId(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetListFoodByIdCategory(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }
        void LoadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();
            foreach (Table table in tableList)
            {
                Button btnTable = new Button()
                {
                    Width = TableDAO.TableWidth,
                    Height = TableDAO.TableHeight,
                    Text = table.Name + "\n" + (table.Status ? "Có người" : "Trống"),
                    BackColor = table.Status ? Color.LightPink : Color.LightGreen,
                    Tag = table,

                };

                btnTable.Click += BtnTable_Click;

                flpTable.Controls.Add(btnTable);
            }
        }

        void ShowBill(int idTable)
        {
            lsvBill.Items.Clear();
            List<DTO.Menu> listMenu = MenuDAO.Instance.GetListMenuByTable(idTable);
            float totalPrice = 0;
            foreach (DTO.Menu item in listMenu)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName);
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            //Thread.CurrentThread.CurrentCulture = (culture);
            txbTotalPrice.Text = totalPrice.ToString("c",culture);
        }

        #endregion

        #region Events

        private void BtnTable_Click(object sender, EventArgs e)
        {
            ShowBill(((sender as Button).Tag as Table).Id);
            lsvBill.Tag = (sender as Button).Tag;
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

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            id = (cb.SelectedItem as Category).Id;
            LoadFoodListByCategoryId(id);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            int billID = BillDAO.Instance.GetBillIdByTableId(table.Id);
            int foodId = (cbFood.SelectedItem as Food).Id;
            int count = Convert.ToInt32(nmFoodCount.Value);
            if (billID == -1)
            {
                BillDAO.Instance.InsertBill(table.Id);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIdBill(), foodId, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(billID, foodId, count);
            }
            ShowBill(table.Id);
            LoadTable();
              
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            int billId = BillDAO.Instance.GetBillIdByTableId(table.Id);
            if (billId!=-1)
            {
                if(MessageBox.Show("Bạn có chắc thanh toán hoá đơn cho bàn " + table.Name, "Thông báo",MessageBoxButtons.OKCancel)==DialogResult.OK)
                {
                    BillDAO.Instance.Checkout(billId);
                    ShowBill(table.Id);
                    LoadTable();
                }    
            }
            
        }
    }
}
