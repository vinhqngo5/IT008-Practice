﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            CloseWindowCommand = new RelayCommand<UserControl>(p => p == null ? false : true, p =>
            {
                FrameworkElement window = GetParentWindow(p);
                (window as Window)?.Close();
            });

            MaximizeWindowCommand = new RelayCommand<UserControl>(p => p == null ? false : true, p =>
             {
                 FrameworkElement window = GetParentWindow(p);
                 var w = window as Window;

                 if (w?.WindowState != WindowState.Maximized)
                 {
                     w.WindowState = WindowState.Maximized;
                 }
                 else
                 {
                     w.WindowState = WindowState.Normal;
                 }
             });

            MinimizeWindowCommand = new RelayCommand<UserControl>(p => p == null ? false : true, p =>
             {
                 FrameworkElement window = GetParentWindow(p);
                 var w = window as Window;

                 if (w?.WindowState != WindowState.Minimized)
                 {
                     w.WindowState = WindowState.Minimized;
                 }
                 else
                 {
                     w.WindowState = WindowState.Normal;
                 }
             });

            MouseMoveWindowCommand = new RelayCommand<UserControl>(p => p == null ? false : true, p =>
            {
                FrameworkElement window = GetParentWindow(p);
                (window as Window)?.DragMove();
            });
        }

        FrameworkElement GetParentWindow(UserControl p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }
    }
}
