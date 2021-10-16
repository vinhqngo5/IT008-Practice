using QuanLyKho.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {
        private ObservableCollection<Customer> _list;
        public ObservableCollection<Customer> List { get => _list; set { _list = value; OnPropertyChanged(); } }
        private Customer _selectedItem;
        public Customer SelectedItem
        {
            get => _selectedItem; set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    Phone = SelectedItem.Phone;
                    Email = SelectedItem.Email;
                    Address = SelectedItem.Address;
                    MoreInfo = SelectedItem.Address;
                    ContractDate = SelectedItem.ContractDate;
                }
            }
        }
        private string _displayName;
        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }

        private string _address;
        public string Address { get => _address; set { _address = value; OnPropertyChanged(); } }

        private string _phone;
        public string Phone { get => _phone; set { _phone = value; OnPropertyChanged(); } }

        private string _email;
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }

        private string _moreInfo;
        public string MoreInfo { get => _moreInfo; set { _moreInfo = value; OnPropertyChanged(); } }

        private DateTime? _contractDate;
        public DateTime? ContractDate { get => _contractDate; set { _contractDate = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public CustomerViewModel()
        {
            List = new ObservableCollection<Customer>(DataProvider.Instance.Database.Customers);
            AddCommand = new RelayCommand<object>(
                (p) =>
                {
                    return true;
                },
                (p) =>
                {
                    var customer = new Customer() { DisplayName = DisplayName, Address = Address, Phone = Phone, Email = Email, MoreInfo = MoreInfo, ContractDate = ContractDate };
                    DataProvider.Instance.Database.Customers.Add(customer);
                    DataProvider.Instance.Database.SaveChanges();
                    List.Add(customer);
                });
            EditCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (SelectedItem == null)
                        return false;
                    /*                    var displayList = DataProvider.Instance.Database.Customers.Where(x => x.Id == SelectedItem.Id);
                                        if (displayList == null || displayList.Count() != 0)
                                            return false;*/
                    return true;
                },
                (p) =>
                {
                    var customer = DataProvider.Instance.Database.Customers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                    customer.DisplayName = DisplayName;
                    customer.Address = Address;
                    customer.Phone = Phone;
                    customer.Email = Email;
                    customer.MoreInfo = MoreInfo;
                    customer.ContractDate = ContractDate;
                    DataProvider.Instance.Database.SaveChanges();
                });
        }
    }
}
