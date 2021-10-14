using QuanLyKho.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private ObservableCollection<TonKho> _tonKhoList;
        public ObservableCollection<TonKho> TonKhoList
        {
            get => _tonKhoList;
            set
            {
                _tonKhoList = value;
                OnPropertyChanged();
            }
        }

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
                        LoadTonKhoData();
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

        void LoadTonKhoData()
        {
            TonKhoList = new ObservableCollection<TonKho>();

            var objectList = DataProvider.Instance.Database.Objects;

            int i = 1;

            foreach (var item in objectList)
            {
                var inputList = DataProvider.Instance.Database.InputInfoes.Where(p => p.IdObject == item.Id);
                var outputList = DataProvider.Instance.Database.OutputInfoes.Where(p => p.IdObject == item.Id);

                int sumInput = 0;
                int sumOutput = 0;

                if (inputList.Count() != 0)
                {
                    sumInput = (int)inputList.Sum(p => p.Count);
                }
                if (outputList.Count() != 0)
                {
                    sumOutput = (int)outputList.Sum(p => p.Count);
                }

                TonKho tonkho = new TonKho();
                tonkho.STT = i;
                tonkho.Count = sumInput - sumOutput;
                tonkho.Object = item;

                TonKhoList.Add(tonkho);

                i++;
            }

        }
    }
}
