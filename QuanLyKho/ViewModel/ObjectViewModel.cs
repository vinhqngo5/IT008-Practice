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
    public class ObjectViewModel : BaseViewModel
    {
        private ObservableCollection<Model.Object> _list;
        public ObservableCollection<Model.Object> List { get => _list; set { _list = value; OnPropertyChanged(); } }
        private ObservableCollection<Supplier> _supplier;
        public ObservableCollection<Supplier> Suppliers { get => _supplier; set { _supplier = value; OnPropertyChanged(); } }
        private ObservableCollection<Unit> _unit;
        public ObservableCollection<Unit> Unit { get => _unit; set { _unit = value; OnPropertyChanged(); } }
        private Model.Object _selectedItem;
        public Model.Object SelectedItem
        {
            get => _selectedItem; set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    QRCode = SelectedItem.QrCode;
                    BarCode = SelectedItem.BarCode;
                    SelectedUnit = SelectedItem.Unit;
                    SelectedSupllier = SelectedItem.Supplier;
                }
            }
        }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        private Unit _selectedUnit;
        public Unit SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                _selectedUnit = value;
                OnPropertyChanged();
            }
        }
        private Supplier _selectedSupplier;
        public Supplier SelectedSupllier
        {
            get => _selectedSupplier;
            set
            {
                _selectedSupplier = value;
                OnPropertyChanged();
            }
        }

        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }

        public string QRCode { get => _qRCode; set { _qRCode = value; OnPropertyChanged(); } }

        public string BarCode { get => _barCode; set { _barCode = value; OnPropertyChanged(); } }


        private string _displayName;

        private string _qRCode;

        private string _barCode;


        public ObjectViewModel()
        {
            List = new ObservableCollection<Model.Object>(DataProvider.Instance.Database.Objects);
            Unit = new ObservableCollection<Unit>(DataProvider.Instance.Database.Units);
            Suppliers = new ObservableCollection<Supplier>(DataProvider.Instance.Database.Suppliers);
            AddCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (string.IsNullOrEmpty(DisplayName))
                        return false;
                    return true;
                },
                (p) =>
                {
                    var tmp = new Model.Object() { DisplayName = DisplayName, Supplier = SelectedSupllier, Unit = SelectedUnit, QrCode = QRCode, BarCode = BarCode, Id = Guid.NewGuid().ToString()  };
                    DataProvider.Instance.Database.Objects.Add(tmp);
                    DataProvider.Instance.Database.SaveChanges();
                    List.Add(tmp);
                });
            EditCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (string.IsNullOrEmpty(DisplayName) || SelectedItem == null)
                        return false;

                    return true;
                },
                (p) =>
                {
                    var tmp = DataProvider.Instance.Database.Objects.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                    tmp.DisplayName = DisplayName;
                    tmp.QrCode = QRCode;
                    tmp.BarCode = BarCode;
                    tmp.Unit = SelectedUnit;
                    tmp.Supplier = SelectedSupllier;
                    DataProvider.Instance.Database.SaveChanges();
                });
        }
    }
}
