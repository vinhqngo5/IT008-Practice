﻿using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            txbTotalPrice.Text = totalPrice.ToString("C0", _culture);
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
        #endregion

        private void cbCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Category selectedCategory = cbCategory.SelectedItem as Category;
            LoadFoodByCategoryId(selectedCategory.Id);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            int idBill = BillDAO.Instance.GetBillIdByTableId(table.Id);
            int idFood = (cbFood.SelectedItem as Food).Id;
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
        }
    }
}
