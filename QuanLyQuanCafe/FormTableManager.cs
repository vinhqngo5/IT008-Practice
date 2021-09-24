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
                flpTable.Controls.Add(btn);

            }
        }
        #endregion


        #region Events
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
