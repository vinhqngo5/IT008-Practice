using System.Windows;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitWindowCommand { get; set; }
        public ICommand SupplierWindowCommand { get; set; }
        public ICommand CustomerWindowCommand { get; set; }
        public ICommand ObjectWindowCommand { get; set; }
        public ICommand UserWindowCommand { get; set; }
        public ICommand InputWindowCommand { get; set; }
        public ICommand OutputWindowCommand { get; set; }

        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>(
                (p) => { return true; },
                (p) =>
                {
                    Isloaded = true;
                    if (p == null)
                        return;
                    p.Hide();
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.ShowDialog();

                    if (loginWindow.DataContext == null)
                        return;
                    var loginVM = loginWindow.DataContext as LoginViewModel;

                    if (loginVM.Islogin)
                    {
                        p.Show();
                    }
                    else
                    {
                        p.Close();
                    }
                });

            UnitWindowCommand = new RelayCommand<object>(
                (p) => { return true; },
                (p) =>
                {
                    _ = new UnitWindow().ShowDialog();
                });

            SupplierWindowCommand = new RelayCommand<object>(
                (p) => { return true; },
                (p) =>
                {
                    _ = new SupplierWindow().ShowDialog();
                });

            CustomerWindowCommand = new RelayCommand<object>(
                (p) => { return true; },
                (p) =>
                {
                    _ = new CustomerWindow().ShowDialog();
                });

            ObjectWindowCommand = new RelayCommand<object>(
                (p) => { return true; },
                (p) =>
                {
                    _ = new ObjectWindow().ShowDialog();
                });

            UserWindowCommand = new RelayCommand<object>(
                (p) => { return true; },
                (p) =>
                {
                    _ = new UserWindow().ShowDialog();
                });

            InputWindowCommand = new RelayCommand<object>(
                (p) => { return true; },
                (p) =>
                {
                    _ = new InputWindow().ShowDialog();
                });

            OutputWindowCommand = new RelayCommand<object>(
                (p) => { return true; },
                (p) =>
                {
                    _ = new OutputWindow().ShowDialog();
                });
        }
    }
}
