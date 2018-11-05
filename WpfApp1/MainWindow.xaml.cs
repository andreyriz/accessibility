using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Automation;

using Gma.System.MouseKeyHook.HotKeys;
using Gma.System.MouseKeyHook.Implementation;
using Gma.System.MouseKeyHook.WinApi;
using Gma.System.MouseKeyHook;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        MainWindowViewModel main;
        public MainWindow()
        { 
            InitializeComponent();
            main = new MainWindowViewModel(c => this.Cursor = c, this);
            DataContext = main;
            Closing += main.OnMainWindowClosing;
        }
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            main.OnSelectedItemChanged((Node)e.NewValue);
        }
    }
}