using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private bool _isLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public bool IsLoaded { get => _isLoaded; set => _isLoaded = value; }

        public MainViewModel()
        {

            LoadedWindowCommand = new RelayCommand<object>(p => true, p =>
            {
                IsLoaded = true;
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
            });

        }

    }
}
