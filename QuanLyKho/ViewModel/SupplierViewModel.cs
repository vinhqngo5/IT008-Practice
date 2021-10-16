using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    MoreInfo = SelectedItem.MoreInfo;
                    ContractDate = SelectedItem.ContractDate;
                }
            }
        }
        private string _displayName;
        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }


        private string _phone;
        public string Phone { get => _phone; set { _phone = value; OnPropertyChanged(); } }


        private string _address;
        public string Address { get => _address; set { _address = value; OnPropertyChanged(); } }


        private string _email;
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }


        private string _moreInfo;
        public string MoreInfo { get => _moreInfo; set { _moreInfo = value; OnPropertyChanged(); } }


        private DateTime? _contractDate;
        public DateTime? ContractDate { get => _contractDate; set { _contractDate = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public SupplierViewModel()
        {
            List = new ObservableCollection<Supplier>(DataProvider.Instance.Database.Suppliers);
            AddCommand = new RelayCommand<object>(
                (p) =>
                {
                    return true;
                },
                (p) =>
                {
                    var supplier = new Supplier()
                    {
                        DisplayName = DisplayName,
                        Phone = Phone,
                        Email = Email,
                        Address = Address,
                        MoreInfo = MoreInfo,
                        ContractDate = ContractDate
                    };
                    DataProvider.Instance.Database.Suppliers.Add(supplier);
                    DataProvider.Instance.Database.SaveChanges();
                    List.Add(supplier);
                });
            EditCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (SelectedItem == null)
                        return false;
                    var displayList = DataProvider.Instance.Database.Suppliers.Where(x => x.Id == SelectedItem.Id);
                    if (displayList != null && displayList.Count() != 0)
                        return true;
                    return false;
                },
                (p) =>
                {
                    var supplier = DataProvider.Instance.Database.Suppliers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                    supplier.DisplayName = DisplayName;
                    supplier.Phone = Phone; ;
                    supplier.Email = Email;
                    supplier.Address = Address;
                    supplier.MoreInfo = MoreInfo;
                    supplier.ContractDate = ContractDate;
                    DataProvider.Instance.Database.SaveChanges();
                });
        }
    }
}
