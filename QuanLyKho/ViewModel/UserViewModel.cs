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
    public class UserViewModel : BaseViewModel
    {
        private ObservableCollection<User> _list;
        public ObservableCollection<User> List { get => _list; set { _list = value; OnPropertyChanged(); } }
        private User _selectedItem;
        public User SelectedItem
        {
            get => _selectedItem; 
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    UserName = SelectedItem.UserName;
                    SelectedRole = SelectedItem.UserRole;
                }
            }
        }
        private ObservableCollection<UserRole> _role;
        public ObservableCollection<UserRole> Role { get => _role; set { _role = value; OnPropertyChanged(); } }

        private string _displayName;
        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }

        private string _userName;
        public string UserName { get => _userName; set { _userName = value; OnPropertyChanged(); } }
        private UserRole _selectedRole;
        public UserRole SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }

        public UserViewModel()
        {
            List = new ObservableCollection<User>(DataProvider.Instance.Database.Users);
            Role = new ObservableCollection<UserRole>(DataProvider.Instance.Database.UserRoles);
            AddCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (string.IsNullOrEmpty(UserName))
                        return false;
                    var displayUser = DataProvider.Instance.Database.Users.Where(x => x.UserName == UserName);
                    if (displayUser == null || displayUser.Count() != 0)
                        return false;
                    return true;
                },
                (p) =>
                {
                    var unit = new User() { DisplayName = DisplayName, UserName = UserName, UserRole = SelectedRole, PassWord ="1" };
                    DataProvider.Instance.Database.Users.Add(unit);
                    DataProvider.Instance.Database.SaveChanges();
                    List.Add(unit);
                });
            EditCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (string.IsNullOrEmpty(UserName) || SelectedItem == null)
                        return false;
                    return true;
                },
                (p) =>
                {
                    var user = DataProvider.Instance.Database.Users.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                    user.DisplayName = DisplayName;
                    user.UserName = UserName;
                    user.UserRole = SelectedRole;
                    
                    DataProvider.Instance.Database.SaveChanges();
                });
            ChangePasswordCommand = new RelayCommand<object>(
                (p) =>
                {
                    return true;
                },
                (p) =>
                {
                    _ = new ChangePassWordWindow().ShowDialog();
                });
        }
    }
}

