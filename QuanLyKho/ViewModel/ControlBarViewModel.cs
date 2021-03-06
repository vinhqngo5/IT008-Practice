using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class ControlBarViewModel : BaseViewModel
    {
        #region commands

        public ICommand CloseWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MouseMoveWindowCommand { get; set; }

        #endregion

        public ControlBarViewModel()
        {
            CloseWindowCommand = new RelayCommand<UserControl>(
                parameter => parameter != null,
                parameter =>
                {
                    var parentWindow = Window.GetWindow(parameter);
                    parentWindow?.Close();
                });

            MaximizeWindowCommand = new RelayCommand<UserControl>(
                parameter => parameter != null,
                parameter =>
                {
                    var parentWindow = Window.GetWindow(parameter);
                    if (parentWindow != null)
                        parentWindow.WindowState = parentWindow.WindowState != WindowState.Maximized
                            ? WindowState.Maximized
                            : WindowState.Normal;
                });

            MinimizeWindowCommand = new RelayCommand<UserControl>(
                parameter => parameter != null,
                parameter =>
                {
                    var parentWindow = Window.GetWindow(parameter);
                    if (parentWindow != null)
                        parentWindow.WindowState = parentWindow.WindowState != WindowState.Minimized
                            ? WindowState.Minimized
                            : WindowState.Normal;
                });

            MouseMoveWindowCommand = new RelayCommand<UserControl>(
                parameter => parameter != null,
                parameter =>
                {
                    var parentWindow = Window.GetWindow(parameter);
                    parentWindow?.DragMove();
                });
        }
    }
}