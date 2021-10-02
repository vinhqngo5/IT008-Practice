using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class FormAdmin : Form
    {
        BindingSource foodList = new BindingSource();

        public FormAdmin()
        {
            InitializeComponent();

            Load();
        }

        #region Methods
        void Load()
        {
            dtgvFood.DataSource = foodList;

            LoadDateTimePickerBill();

            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);

            LoadListFood();

            LoadCategoryIntoCombobox(cbFoodCategory);

            AddFoodBinding();
        }

        void LoadListBillByDate(DateTime dateCheckIn, DateTime dateCheckOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetListBillByDate(dateCheckIn, dateCheckOut);
        }

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }

        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name"));

            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Id"));

            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price"));

            //cbFoodCategory.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price"));
        }

        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategories();
            cb.DisplayMember = "Name";
        }

        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        #endregion

        #region Events

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }


        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            if (dtgvFood.SelectedCells.Count > 0)
            {
                int id = Convert.ToInt32(dtgvFood.SelectedCells[0].OwningRow.Cells["IdCategory"].Value);


                Category category = CategoryDAO.Instance.GetCategoryById(id);

                cbFoodCategory.SelectedItem = category;

                int index = -1;
                int i = 0;

                foreach (Category item in cbFoodCategory.Items)
                {
                    if (item.Id == category.Id)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }

                cbFoodCategory.SelectedIndex = index;
            }
        }
        #endregion
    }
}
