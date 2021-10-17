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
    class ObjectViewModel : BaseViewModel
    {

        private ObservableCollection<Model.Object> _list;
        public ObservableCollection<Model.Object> List { get => _list; set { _list = value; OnPropertyChanged(); } }


        private ObservableCollection<Unit> _unit;
        public ObservableCollection<Unit> Unit { get => _unit; set { _unit = value; OnPropertyChanged(); } }


        private ObservableCollection<Supplier> _supplier;
        public ObservableCollection<Supplier> Supplier { get => _supplier; set { _supplier = value; OnPropertyChanged(); } }


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
                    QrCode = SelectedItem.QrCode;
                    BarCode = SelectedItem.BarCode;
                    SelectedUnit = SelectedItem.Unit;
                    SelectedSupplier = SelectedItem.Supplier;
                }
            }
        }


        private Model.Unit _selectedUnit;
        public Model.Unit SelectedUnit
        {
            get => _selectedUnit; set
            {
                _selectedUnit = value;
                OnPropertyChanged();
            }
        }


        private Model.Supplier _selectedSupplier;
        public Model.Supplier SelectedSupplier
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




        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public ObjectViewModel()
        {
            List = new ObservableCollection<Model.Object>(DataProvider.Instance.Database.Objects);
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
                    var supplier = new Model.Object()
                    {
                        Id = Guid.NewGuid().ToString(),
                        DisplayName = DisplayName,
                        BarCode = BarCode,
                        QrCode = QrCode,
                        IdSupplier = SelectedSupplier.Id,
                        IdUnit = SelectedUnit.Id

                    };
                    DataProvider.Instance.Database.Objects.Add(supplier);
                    DataProvider.Instance.Database.SaveChanges();
                    List.Add(supplier);
                });
            EditCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (SelectedItem == null || SelectedUnit == null || SelectedSupplier == null)
                        return false;
                    var displayList = DataProvider.Instance.Database.Objects.Where(x => x.Id == SelectedItem.Id);
                    if (displayList != null && displayList.Count() != 0)
                        return true;
                    return false;
                },
                (p) =>
                {
                    var objectInstance = DataProvider.Instance.Database.Objects.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                    objectInstance.DisplayName = DisplayName;
                    objectInstance.BarCode = BarCode;
                    objectInstance.QrCode = QrCode;
                    objectInstance.IdSupplier = SelectedSupplier.Id;
                    objectInstance.IdUnit = SelectedUnit.Id;
                    DataProvider.Instance.Database.SaveChanges();
                });
        }
    }
}
