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
        private ObservableCollection<Inventory> _inventories;
        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitWindowCommand { get; set; }
        public ICommand SupplierWindowCommand { get; set; }
        public ICommand CustomerWindowCommand { get; set; }
        public ICommand ObjectWindowCommand { get; set; }
        public ICommand UserWindowCommand { get; set; }
        public ICommand InputWindowCommand { get; set; }
        public ICommand OutputWindowCommand { get; set; }
        public ObservableCollection<Inventory> Inventories 
        { 
            get => _inventories; 
            set 
            { 
                _inventories = value; 
                OnPropertyChanged(); 
            } 
        }

        public MainViewModel()
        {
            LoadInventory();
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
        void LoadInventory()
        {
            Inventories = new ObservableCollection<Inventory>();
            var objectList = DataProvider.Instance.Database.Objects;
            int i = 1;
            foreach(var item in objectList)
            {
                var inputList = DataProvider.Instance.Database.InputInfoes.Where(intput => intput.Id == item.Id);
                var outputList = DataProvider.Instance.Database.OutputInfoes.Where(output => output.Id == item.Id);
                int sumInput =  0;
                int sumOutput = 0;
                if (inputList!=null)
                    sumInput = Convert.ToInt32(inputList.Sum(intput => intput.Count));
                if (outputList != null)
                    sumOutput = Convert.ToInt32(inputList.Sum(output => output.Count));
                Inventory inventory = new Inventory();
                inventory.STT = i;
                inventory.Count = sumInput - sumOutput;
                inventory.Object = item;
                Inventories.Add(inventory);
            }    
        }
    }
}
