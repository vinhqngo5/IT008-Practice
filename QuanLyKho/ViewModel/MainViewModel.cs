using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            LoadedWindowCommand = new RelayCommand<object>(
                (p) => { return false; },
                (p) =>
                {
                    Isloaded = true;
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.ShowDialog();
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
