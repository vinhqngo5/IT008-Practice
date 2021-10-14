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
    public class UnitViewModel: BaseViewModel
    {
        private ObservableCollection<Unit> _list;
        private Unit _selectedItem;
        private string _displayName;

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public ObservableCollection<Unit> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }

        public Unit SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                    DisplayName = SelectedItem.DisplayName;
            }
        }

        public string DisplayName
        {
            get => _displayName;
            set
            {
                _displayName = value;
                OnPropertyChanged();
            }
        }

        public UnitViewModel()
        {
            _list = new ObservableCollection<Unit>(DataProvider.Instance.Database.Units);

            AddCommand = new RelayCommand<object>(
                (p) => 
                {
                    if (string.IsNullOrEmpty(DisplayName))
                        return false;
                    var displayList = DataProvider.Instance.Database.Units.Where(unit => unit.DisplayName == DisplayName);
                    if (displayList == null || displayList.Count() > 0)
                        return false;
                    return true;
                },
                (p) =>
                {
                    var unit = new Unit() { DisplayName = DisplayName };
                    DataProvider.Instance.Database.Units.Add(unit);
                    DataProvider.Instance.Database.SaveChanges();
                    List.Add(unit);
                });
            EditCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (string.IsNullOrEmpty(DisplayName))
                        return false;
                    var displayList = DataProvider.Instance.Database.Units.Where(unit => unit.DisplayName == DisplayName);
                    if (displayList == null || displayList.Count() > 0)
                        return false;
                    return true;
                },
                (p) =>
                {
                    var unit = DataProvider.Instance.Database.Units.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                    unit.DisplayName = DisplayName;
                    DataProvider.Instance.Database.SaveChanges();
                    SelectedItem.DisplayName = DisplayName;
                });
        }
    }
}
