using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class SupplierViewModel : BaseViewModel
    {
        private ObservableCollection<Supplier> _list;
        public ObservableCollection<Supplier> List { get => _list; set { _list = value; OnPropertyChanged(); } }
        private Supplier _selectedItem;
        public Supplier SelectedItem
        {
            get => _selectedItem; 
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    Address = SelectedItem.Address;
                    Phone = SelectedItem.Phone;
                    Email = SelectedItem.Email;
                    MoreInfo = SelectedItem.MoreInfo;
                    ContractDate = SelectedItem.ContractDate;
                }
            }
        }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }


        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }

        public string Address { get => _address; set { _address = value; OnPropertyChanged(); } }

        public string Phone { get => _phone; set { _phone = value; OnPropertyChanged(); } }

        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }

        public string MoreInfo { get => _moreInfo; set { _moreInfo = value; OnPropertyChanged(); } }

        public DateTime? ContractDate { get => _contractDate; set { _contractDate = value; OnPropertyChanged(); } }

        private string _address;

        private string _displayName;

        private string _phone;

        private string _email;

        private string _moreInfo;

        private DateTime? _contractDate;

        public SupplierViewModel()
        {
            List = new ObservableCollection<Supplier>(DataProvider.Instance.Database.Suppliers);
            AddCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (string.IsNullOrEmpty(DisplayName))
                        return false;
                    var displayList = DataProvider.Instance.Database.Suppliers.Where(x => x.DisplayName == DisplayName);
                    if (displayList == null || displayList.Count() != 0)
                        return false;
                    return true;
                },
                (p) =>
                {
                    var supplier = new Supplier() { DisplayName = DisplayName, Address = Address, Phone = Phone, Email = Email , MoreInfo = MoreInfo, ContractDate = ContractDate};
                    DataProvider.Instance.Database.Suppliers.Add(supplier);
                    DataProvider.Instance.Database.SaveChanges();
                    List.Add(supplier);
                });
            EditCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (string.IsNullOrEmpty(DisplayName) || SelectedItem == null)
                        return false;
                    var displayList = DataProvider.Instance.Database.Suppliers.Where(x => x.DisplayName == DisplayName);
                    if (displayList == null || displayList.Count() != 0)
                        return false;
                    return true;
                },
                (p) =>
                {
                    var supplier = DataProvider.Instance.Database.Suppliers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                    supplier.DisplayName = DisplayName;
                    supplier.Address = Address;
                    supplier.Phone = Phone;
                    supplier.Email = Email;
                    supplier.MoreInfo = MoreInfo;
                    supplier.ContractDate = ContractDate;
                    DataProvider.Instance.Database.SaveChanges();
                });
        }
    }
}
