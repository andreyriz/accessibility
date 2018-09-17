using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;

namespace WpfApp1
{
    class MainWindowViewModel : BindableBase
    {
        #region margins
        Brush borderBrush;
        Action<Cursor> setCursor;
        Int32 GroupCount;
        Int32 controlCount;
        Node child;
        Single fontSize;
        Single height;
        #endregion
        #region properties
        public ObservableCollection<Node> TNodes { get; set; }
        public Brush BorderBrush { get { return borderBrush; } set { borderBrush = value; OnPropertyChanged("BorderBrush"); } }
        public Single FontSize { get { return fontSize; } set { fontSize = value; OnPropertyChanged("FontSize"); } }
        public Single Height { get { return height; } set { height = value;OnPropertyChanged("Height"); } }
        #endregion
        #region constructors
        public MainWindowViewModel(Action<Cursor> cursor)
        {
            Height = 15;
            BorderBrush = Brushes.White;
            setCursor = cursor;
            GroupCount = 1;
            controlCount = 1;
            TNodes = new ObservableCollection<Node>();
            ChangeFontSize();
        }
        #endregion
        #region methods
        private void ChangeFontSize()
        {
            if (SystemParameters.PrimaryScreenHeight <= 600 && SystemParameters.PrimaryScreenWidth <= 800)
            {
                FontSize = 8;
                Height = 15;
            }
            else
            if (SystemParameters.PrimaryScreenHeight == 768 && SystemParameters.PrimaryScreenWidth == 1024)
            {
                FontSize = 8.5f;
                Height = 15.5f;
            }
            else
            if (SystemParameters.PrimaryScreenHeight == 600 && SystemParameters.PrimaryScreenWidth == 1280)
            {
                FontSize = 9;
                Height = 16;
            }
            else
            if (SystemParameters.PrimaryScreenHeight == 720 && SystemParameters.PrimaryScreenWidth == 1280)
            {
                FontSize = 9.5f;
                Height = 16.5f;
            }
            else
            if (SystemParameters.PrimaryScreenHeight == 768 && SystemParameters.PrimaryScreenWidth == 1280)
            {
                FontSize = 10;
                Height = 17;
            }
            else
            if (SystemParameters.PrimaryScreenHeight == 768 && SystemParameters.PrimaryScreenWidth == 1360)
            {
                FontSize = 10.5f;
                Height = 17.5f;
            }
            else
            if (SystemParameters.PrimaryScreenHeight == 768 && SystemParameters.PrimaryScreenWidth == 1366)
            {
                FontSize = 11;
                Height = 18;
            }
            else
            {
                FontSize = 15;
                Height = 23;
            }
        }
        private void OnChangeBrush(object a)
        {
            if (BorderBrush == Brushes.White)
            {
                BorderBrush = Brushes.Red;
            }
            else
            {
                BorderBrush = Brushes.White;
            }
        }
        private void OnAddGroup(object a)
        {
            if (TNodes.Count == 0 || child == null)
            {
                TNodes.Add(new Node() { Name = "Group" + GroupCount, Image = new BitmapImage(new Uri("Resources/directory.bmp", UriKind.Relative)) });
                GroupCount++;
            }
            else
            {
                child.Nodes.Add(new Node() { Name = "Group" + GroupCount, Image = new BitmapImage(new Uri("Resources/directory.bmp", UriKind.Relative)) });
                GroupCount++;
            }
        }
        private void OnAddControl(object a)
        {
            if (TNodes.Count == 0 || child == null)
            {
                TNodes.Add(new ControlClass() { Name = "Control" + controlCount, FirstName = "Jon Do", role = role.sysadmin, value = 1, Image = new BitmapImage(new Uri("Resources/fileTxt.png", UriKind.Relative)) });
                controlCount++;
            }
            else
            {
                child.Nodes.Add(new ControlClass() { Name = "Control" + controlCount, FirstName = "Jon Do", role = role.sysadmin, value = 1, Image = new BitmapImage(new Uri("Resources/fileTxt.png", UriKind.Relative)) });
                controlCount++;
            }
            setCursor(Cursors.Cross);
        }
        private void RemoveEl(Node i)
        {
            if (i.Nodes.Count != 0)
            {
                foreach (var z in i.Nodes)
                {
                    if (z.Equals(child))
                    {
                        i.Nodes.Remove(z);
                        break;
                    }
                    else
                    {
                        RemoveEl(z);
                    }
                }
            }
        }
        private void OnRemoveElement(object a)
        {
            if (child != null)
            {
                TNodes.Remove(child);
                if (child != null)
                {
                    foreach (var z in TNodes)
                    {
                        RemoveEl(z);
                    }
                }
            }
        }
        private void OnResetCursor(object a)
        {
            setCursor(Cursors.Arrow);
        }
        internal void OnSelectedItemChanged(Node newValue)
        {
            child = newValue;
        }
        #endregion
        #region Commands
        public ICommand ChangeColor { get { return new RelayCommand(OnChangeBrush); } }
        public ICommand AddGroupCommand { get { return new RelayCommand(OnAddGroup); } }
        public ICommand AddControlCommand { get { return new RelayCommand(OnAddControl); } }
        public ICommand RemoveElementCommand { get { return new RelayCommand(OnRemoveElement); } }
        public ICommand ResetCursorCommand { get { return new RelayCommand(OnResetCursor); } }
        #endregion
    }
}