using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
  class MainViewModel : BaseViewModel
  {
    private bool _isLoaded = false;
    public ICommand LoadedWindowCommand { get; set; }
    public MainViewModel()
    {
        LoginWindow loginWindow = new LoginWindow();
        LoadedWindowCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
        {
            this.IsLoaded = true;
            loginWindow.ShowDialog();
        });

    }

    public bool IsLoaded { get => _isLoaded; set => _isLoaded = value; }
  }
}
