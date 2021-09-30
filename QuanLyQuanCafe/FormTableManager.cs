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
        private readonly CultureInfo _culture = new CultureInfo("vi-VN");

        public FormTableManager()
        {
            InitializeComponent();

            LoadTable();

            LoadCategories();
        }

        #region Methods

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

        void LoadCategories()
        {
            List<Category> listCategories = CategoryDAO.Instance.GetListCategories();
            cbCategory.DataSource = listCategories;
            cbCategory.DisplayMember = "Name";
            cbCategory.SelectedItem = null;
            cbCategory.Text = "-- Chọn danh mục --";
            cbFood.SelectedItem = null;
            cbFood.Text = "-- Chọn món --";
        }

        void LoadFoodByCategoryId(int idCategory)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryId(idCategory);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }

        void LoadSwitchTable(int idTable)
        {
            List<Table> listTable = TableDAO.Instance.LoadTableList();
            listTable.RemoveAll(table => table.Id == idTable);
            cbSwitchTable.DataSource = listTable;
            cbSwitchTable.DisplayMember = "Name";
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
                lsvItem.SubItems.Add(item.Price.ToString("C0", _culture));
                lsvItem.SubItems.Add(item.TotalPrice.ToString("C0", _culture));
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }

            //this will settings for this thread
            //Thread.CurrentThread.CurrentCulture = _culture;
            
            txbTotalPrice.Text = totalPrice.ToString("C0", _culture);
        }

        #endregion

        #region Events

        private void BtnTable_Click(object sender, EventArgs e)
        {
            lsvBill.Tag = (sender as Button).Tag;
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


        private void cbCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Category selectedCategory = cbCategory.SelectedItem as Category;
            LoadFoodByCategoryId(selectedCategory.Id);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Vui lòng chọn bàn để thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idBill = BillDAO.Instance.GetBillIdByTableId(table.Id);
            Food selectedFood = cbFood.SelectedItem as Food;
            if (selectedFood == null)
            {
                MessageBox.Show("Vui lòng chọn món để thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idFood = (selectedFood).Id;
            int count = Convert.ToInt32(nmFoodCount.Value);

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.Id);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetBillIdByTableId(table.Id), idFood, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, idFood, count);
            }

            ShowBill(table.Id);
            LoadTable();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Vui lòng chọn bàn thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idBill = BillDAO.Instance.GetBillIdByTableId(table.Id);
            int discount = Convert.ToInt32(nmDisCount.Value);
            float totalPrice = float.Parse(txbTotalPrice.Text, NumberStyles.Currency, _culture);
            float finalTotalPrice = totalPrice * (1 - Convert.ToSingle(discount) / 100);

            if (idBill != -1)
            {
                DialogResult notif = MessageBox.Show
                    (
                    string.Format("{0}:\nTổng tiền: {1}\nGiảm giá: {2}%\nTổng tiền cần thanh toán: {3}", table.Name, txbTotalPrice.Text, discount, finalTotalPrice.ToString("C0", _culture)),
                    "Xác nhận thanh toán hóa đơn",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);
                if (notif == DialogResult.Cancel)
                {
                    return;
                }
                BillDAO.Instance.CheckOut(idBill, discount);
                ShowBill(table.Id);
                LoadTable();
            }
        }
        private void cbSwitchTable_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Vui lòng chọn bàn cần chuyển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LoadSwitchTable(table.Id);
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Vui lòng chọn bàn cần chuyển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Table tableSwitchedTo = cbSwitchTable.SelectedItem as Table;
            if (MessageBox.Show(string.Format("Bạn có muốn chuyển từ {0} qua {1}", table.Name, tableSwitchedTo.Name), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(table.Id, tableSwitchedTo.Id);
                LoadTable();
            }
        }

        #endregion
    }
}
