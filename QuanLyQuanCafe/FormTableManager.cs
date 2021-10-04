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
        private Account _loginAccount;
        public FormTableManager()
        {
            InitializeComponent();
            LoadTable();

            LoadCategories();
        }

        #region Methods

        void UpdateAccountInfo(string displayName)
        {
            tsmiAccountInfo.Text = "Thông tin tài khoản (" + displayName + ")";
        }

        public void LoadAccount(string userName)
        {
            _loginAccount = AccountDAO.Instance.GetAccountByUserName(userName);
            tsmiAdmin.Enabled = _loginAccount.Type == true;
            UpdateAccountInfo(_loginAccount.DisplayName);
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
                    Tag = table
                };

                btnTable.Click += BtnTable_Click;

                flpTable.Controls.Add(btnTable);
            }
        }

        void ReloadTable()
        {
            List<Table> newTableList = TableDAO.Instance.LoadTableList();
            List<Button> currentTableList = flpTable.Controls.OfType<Button>().ToList();
            Table selectedTable = lsvBill.Tag as Table;

            foreach (Button button in currentTableList)
            {
                Table tableOfButton = button.Tag as Table;
                Table newTable = newTableList.Find(table => table.Id == tableOfButton.Id);
                button.Text = newTable.Name + "\n" + (newTable.Status ? "Có người" : "Trống");
                if (selectedTable.Id == tableOfButton.Id)
                {
                    button.BackColor = Color.Yellow;
                    continue;
                }
                button.BackColor = newTable.Status ? Color.LightPink : Color.LightGreen;
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
            Button buttonTable = sender as Button;
            lsvBill.Tag = buttonTable.Tag;
            ReloadTable();
            ShowBill((lsvBill.Tag as Table).Id);
        }

        private void TsmiLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TsmiAbout_Click(object sender, EventArgs e)
        {
            FormAccountProfile formAccount = new FormAccountProfile();
            formAccount.LoadAccount(_loginAccount);
            formAccount.UpdateAccount += f_UpdateAccount;
            formAccount.ShowDialog();
        }

        private void f_UpdateAccount(object sender, AccountEvent e)
        {
            tsmiAccountInfo.Text =  "Thông tin tài khoản (" + e.Account.DisplayName + ")";
        }

        private void TsmiAdmin_Click(object sender, EventArgs e)
        {
            FormAdmin f = new FormAdmin();
            f.InsertFood += F_InsertFood;
            f.UpdateFood += F_UpdateFood;
            f.DeleteFood += F_DeleteFood;
            f.LoadAccount(_loginAccount);
            f.UpdateAccount += F_UpdateAccount;
            f.ShowDialog();
        }

        private void F_UpdateAccount(object sender, AccountEvent e)
        {
            tsmiAccountInfo.Text = "Thông tin tài khoản (" + e.Account.DisplayName + ")";
            tsmiAdmin.Enabled = e.Account.Type== true;
        }

        private void F_UpdateFood(object sender, EventArgs e)
        {
            if (cbCategory.SelectedItem != null) 
                LoadFoodByCategoryId((cbCategory.SelectedItem as Category).Id);
            ShowBill(((lsvBill).Tag as Table).Id);
        }

        private void F_DeleteFood(object sender, EventArgs e)
        {
            ReloadTable();
            if (cbCategory.SelectedItem != null) 
                LoadFoodByCategoryId((cbCategory.SelectedItem as Category).Id);
            ShowBill(((lsvBill).Tag as Table).Id);
        }

        private void F_InsertFood(object sender, EventArgs e)
        {
            if (cbCategory.SelectedItem != null) 
                LoadFoodByCategoryId((cbCategory.SelectedItem as Category).Id);
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
            ReloadTable();
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
                BillDAO.Instance.CheckOut(idBill, discount, finalTotalPrice);
                ShowBill(table.Id);
                ReloadTable();
            }
        }

        private void cbSwitchTable_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Chọn bàn để chuyển đi chứ!?", "Nhắc hoài z tr", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LoadSwitchTable(table.Id);
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            Table tableSwitchedTo = cbSwitchTable.SelectedItem as Table;
            if (tableSwitchedTo == null)
            {
                MessageBox.Show("Chưa chọn bàn chuyển tới mà bạn ơi!??", "Éo chuyển được đâu bạn ơi :))", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(string.Format("Chuyển từ {0} qua {1} hử?", table.Name, tableSwitchedTo.Name), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(table.Id, tableSwitchedTo.Id);
                lsvBill.Tag = tableSwitchedTo;
                ReloadTable();
            }
        }

        #endregion
    }
}
