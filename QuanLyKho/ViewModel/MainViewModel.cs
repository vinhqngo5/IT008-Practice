using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private bool _isLoaded = false;
        public MainViewModel()
        {
            LoginWindow loginWindow = new LoginWindow();

            if (!this.IsLoaded)
            {
                this.IsLoaded = true;
                loginWindow.ShowDialog();
            }

        }

        public bool IsLoaded { get => _isLoaded; set => _isLoaded = value; }
    }
}
