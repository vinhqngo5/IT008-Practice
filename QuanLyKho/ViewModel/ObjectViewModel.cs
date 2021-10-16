using QuanLyKho.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Object = QuanLyKho.Model.Object;

namespace QuanLyKho.ViewModel
{
    public class ObjectViewModel : BaseViewModel
    {
        private ObservableCollection<Object> _list;
        public ObservableCollection<Object> List { get => _list; set { _list = value; OnPropertyChanged(); } }

        private ObservableCollection<Unit> _unit;
        public ObservableCollection<Unit> Unit { get => _unit; set { _unit = value; OnPropertyChanged(); } }

        private ObservableCollection<Supplier> _supplier;
        public ObservableCollection<Supplier> Supplier { get => _supplier; set { _supplier = value; OnPropertyChanged(); } }
        private Object _selectedItem;
        public Object SelectedItem
        {
            get => _selectedItem; set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    QrCode = SelectedItem.QrCode;
                    BarCode = SelectedItem.BarCode;
                    SelectedUnit = SelectedItem.Unit;
                    SelectedSupplier = SelectedItem.Supplier;
                }
            }
        }
        private Unit _selectedUnit;
        public Unit SelectedUnit
        {
            get => _selectedUnit; set
            {
                _selectedUnit = value;
                OnPropertyChanged();
            }
        }

        private Supplier _selectedSupplier;
        public Supplier SelectedSupplier
        {
            get => _selectedSupplier; set
            {
                _selectedSupplier = value;
                OnPropertyChanged();
            }
        }
        private string _displayName;
        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }

        private string _qrCode;
        public string QrCode { get => _qrCode; set { _qrCode = value; OnPropertyChanged(); } }
        private string _barCode;
        public string BarCode { get => _barCode; set { _barCode = value; OnPropertyChanged(); } }

        private Nullable<double> _outputPrice;
        public Nullable<double> OutputPrice { get => _outputPrice; set { _outputPrice = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public ObjectViewModel()
        {
            List = new ObservableCollection<Object>(DataProvider.Instance.Database.Objects);
            Unit = new ObservableCollection<Unit>(DataProvider.Instance.Database.Units);
            Supplier = new ObservableCollection<Supplier>(DataProvider.Instance.Database.Suppliers);
            AddCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (SelectedSupplier == null || SelectedUnit == null)
                        return false;
                    return true;
                },
                (p) =>
                {
                    var obj = new Object() { DisplayName = DisplayName, BarCode = BarCode, QrCode = QrCode, IdSupplier = SelectedSupplier.Id, IdUnit = SelectedUnit.Id, Id = Guid.NewGuid().ToString()};
                    DataProvider.Instance.Database.Objects.Add(obj);
                    DataProvider.Instance.Database.SaveChanges();
                    List.Add(obj);
                });
            EditCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (SelectedItem == null || SelectedSupplier == null || SelectedUnit == null)
                        return false;
                    /*                    var displayList = DataProvider.Instance.Database.Objects.Where(x => x.Id == SelectedItem.Id);
                                        if (displayList == null || displayList.Count() != 0)
                                            return false;*/
                    return true;
                },
                (p) =>
                {
                    var obj = DataProvider.Instance.Database.Objects.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                    obj.DisplayName = DisplayName;
                    obj.BarCode = BarCode;
                    obj.QrCode = QrCode;
                    obj.IdSupplier = SelectedSupplier.Id;
                    obj.IdUnit = SelectedUnit.Id;
                    DataProvider.Instance.Database.SaveChanges();
                });
        }
    }
}

