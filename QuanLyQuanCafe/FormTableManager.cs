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

            LoadComboBoxTable(cbSwitchTable);
        }

        #region Methods


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
        
        void LoadComboBoxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";
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
            float totalPrice = Convert.ToSingle(txbTotalPrice.Text.Split(' ')[0]);
            float finalTotalPrice = totalPrice - (totalPrice / 100) * discount;
            if (idBill != -1 && MessageBox.Show(string.Format("Bạn có muốn thanh toán hóa đơn cho {0}?\nTổng tiền phai thanh toan sau khi giam gia la {1}.000 VNĐ", table.Name, finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                BillDAO.Instance.CheckOut(idBill, discount);
                ShowBill(table.Id);
                LoadTable();
            }
        }
        

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            int idTable1 = (lsvBill.Tag as Table).Id;
            int idTable2 = (cbSwitchTable.SelectedItem as Table).Id;
            if (MessageBox.Show(string.Format("Bạn có thật sự muốn chuyển bàn {0} qua bàn {1}?", idTable1+1, idTable2+1), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(idTable1, idTable2);
                LoadTable();
            }
                
        }
        #endregion
    }
}
