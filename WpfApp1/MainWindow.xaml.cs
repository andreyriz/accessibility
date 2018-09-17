using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

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
            main = new MainWindowViewModel(c => this.Cursor = c);
            DataContext = main;
            Button a = new Button();
        }
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            main.OnSelectedItemChanged((Node)e.NewValue);
        }
    }
}