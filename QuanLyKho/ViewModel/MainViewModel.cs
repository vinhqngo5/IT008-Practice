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
        private ObservableCollection<Inventory> _inventoryList;
        public ObservableCollection<Inventory> InventoryList { get => _inventoryList; set { _inventoryList = value; OnPropertyChanged(); } }
        private Inventory _selectedItem;
        public Inventory SelectedItem
        {
            get => _selectedItem; set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    SumIntput = SelectedItem.SumInput;
                    SumOutput = SelectedItem.SumOutput;
                    Count = SelectedItem.Count;
                }
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
        public int SumIntput { get => _sumInput; set { _sumInput = value; OnPropertyChanged(); } }
        public int SumOutput { get => _sumOutput; set { _sumOutput = value; OnPropertyChanged(); } }

        public int Count { get => _count; set { _count = value; OnPropertyChanged(); } }

        private int _sumInput;
        private int _sumOutput;
        private int _count;

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
                        LoadInventoryData();
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
        void LoadInventoryData()
        {
            InventoryList = new ObservableCollection<Inventory>();
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
                    sumInput = Convert.ToInt32(inputList.Sum(p => p.Count));
                }
                if (outputList.Count() != 0)
                {
                    sumOutput = Convert.ToInt32(outputList.Sum(p => p.Count));
                }
                Inventory inventory = new Inventory();
                inventory.STT = i;
                inventory.SumInput = sumInput;
                inventory.SumOutput = sumOutput;
                inventory.Count = sumInput - sumOutput;
                inventory.Object = item;
                InventoryList.Add(inventory);
                i++;
            }
        }
    }
}
