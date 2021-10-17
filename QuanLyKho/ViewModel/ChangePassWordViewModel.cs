using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    class ChangePassWordViewModel : BaseViewModel
    {
        public ICommand ConfirmCommand { get; set; }
        public ICommand ClosingCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand NewPasswordChangedCommand { get; set; }
        public ICommand ReNewPasswordChangedCommand { get; set; }

        private string _username = "";

        private string _password = "";
        private string _newPassword = "";
        private string _reNewPassword = "";

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged();
            }
        }
        public string ReNewPassword
        {
            get => _reNewPassword;
            set
            {
                _reNewPassword = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public ChangePassWordViewModel()
        {
            LoginWindow loginWindow = new LoginWindow();
            if (loginWindow.DataContext == null)
                return;
            var loginVM = loginWindow.DataContext as LoginViewModel;
            Username = loginVM.Username;
            PasswordChangedCommand = new RelayCommand<PasswordBox>(
                (p) => { return true; },
                (p) =>
                {
                    Password = p.Password;
                });
            NewPasswordChangedCommand = new RelayCommand<PasswordBox>(
                (p) => { return true; },
                (p) =>
                {
                    NewPassword = p.Password;
                });
            ReNewPasswordChangedCommand = new RelayCommand<PasswordBox>(
                (p) => { return true; },
                (p) =>
                {
                    ReNewPassword = p.Password;
                });
            ConfirmCommand = new RelayCommand<Window>(
                (p) => { return true; },
                (p) =>
                {
                    Confirm(p);
                });

            ClosingCommand = new RelayCommand<Window>(
                (p) => { return true; },
                (p) =>
                {
                    p.Close();
                });
        }

        void Confirm(Window p)
        {
            if (p == null)
                return;

            User accCount = DataProvider.Instance.Database.Users.Where(user => user.UserName == Username && user.PassWord == Password).FirstOrDefault();

            if (accCount != null)
            {
                if (NewPassword.Equals(ReNewPassword) && !string.IsNullOrEmpty(NewPassword))
                {
                    MessageBox.Show("Thay đổi mật khẩu thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    accCount.PassWord = NewPassword;
                    DataProvider.Instance.Database.SaveChanges();
                    p.Close();
                }    
                else
                    MessageBox.Show("Mật khẩu nhập lại không trùng khớp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {

               MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        public static string Base64Encode(string plainText)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }



        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
