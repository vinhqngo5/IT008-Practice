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
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();

            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }

        void LoadFoodListBtCategoryId(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryId(id);

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

            float totalPRice = 0;

            foreach (DTO.Menu item in listMenu)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName);
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPRice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }

            CultureInfo culture = new CultureInfo("vi-VN");

            //this will settings for this thread
            //Thread.CurrentThread.CurrentCulture = culture;

            txbTotalPrice.Text = totalPRice.ToString("c", culture);

        }

        #endregion

        #region Events

        private void BtnTable_Click(object sender, EventArgs e)
        {
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(((sender as Button).Tag as Table).Id);
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

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;

            Category selected = cb.SelectedItem as Category;
            id = selected.Id;

            LoadFoodListBtCategoryId(id);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            int idBill = BillDAO.Instance.GetBillIdByTableId(table.Id);
            int foodId = (cbFood.SelectedItem as Food).Id;
            int count = Convert.ToInt32(nmFoodCount.Value);

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.Id);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIdBill(), foodId, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodId, count);
            }
            ShowBill(table.Id);

            LoadTable();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            int idBill = BillDAO.Instance.GetBillIdByTableId(table.Id);

            if (idBill != -1)
            {
                if (MessageBox.Show("Bạn có chắc thanh toán hóa đơn cho bàn " + table.Name, "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.Checkout(idBill);

                    ShowBill(idBill);
                    LoadTable();
                }
            }


        }
        #endregion
    }
}
