using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using gma.System.Windows;
using UIAutomationClient;
using System.ComponentModel;
using System.Windows.Threading;

namespace WpfApp1
{
    class MainWindowViewModel : BindableBase
    {
        #region margins
        Window focusingAttention;
        UserActivityHook actHook;
        Brush borderBrush;
        Action<Cursor> setCursor;
        Int32 GroupCount;
        Int32 controlCount;
        Node child;
        Single fontSize;
        Single height;
        Boolean isClick;
        Boolean isClose;
        tagRECT selectedItem;
        IUIAutomation UIA;
        tagPOINT points;
        IUIAutomationElement element;
        DispatcherTimer timer;
        #endregion
        #region properties
        public ObservableCollection<Node> TNodes { get; set; }
        public Brush BorderBrush { get { return borderBrush; } set { borderBrush = value; OnPropertyChanged("BorderBrush"); } }
        public Single FontSize { get { return fontSize; } set { fontSize = value; OnPropertyChanged("FontSize"); } }
        public Single Height { get { return height; } set { height = value; OnPropertyChanged("Height"); } }
        #endregion
        #region constructors
        public MainWindowViewModel(Action<Cursor> cursor, Window mainWindow)
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateRect);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            UIA = new CUIAutomation();
            points = new tagPOINT();
            actHook = new UserActivityHook();
            actHook.OnMouseActivity += new System.Windows.Forms.MouseEventHandler(MyKeyDown);
            Height = 15;
            BorderBrush = Brushes.Gray;
            setCursor = cursor;
            GroupCount = 1;
            controlCount = 1;
            TNodes = new ObservableCollection<Node>();
            isClick = false;
            isClose = false;
            focusingAttention = new Window()
            {
                ShowInTaskbar = false,
                Topmost = true,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                Background = Brushes.Transparent,
                ResizeMode = ResizeMode.NoResize,
                BorderThickness = new Thickness(3)
            };
            focusingAttention.Closing += OnWindowClosing;
            ChangeFontSize();
        }
        #endregion
        #region methods
        private void MyKeyDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (BorderBrush.Equals(Brushes.Yellow))
            {
                if (selectedItem.top != focusingAttention.Top || selectedItem.left != focusingAttention.Left
                    || focusingAttention.Width > selectedItem.right - selectedItem.left || focusingAttention.Width < selectedItem.right - selectedItem.left
                    || focusingAttention.Height > selectedItem.bottom - selectedItem.top || focusingAttention.Height < selectedItem.bottom - selectedItem.top)
                {
                    ShowFocusingAttention(selectedItem);
                }
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Left && isClick)
            {
                try
                {
                    points.x = e.X;
                    points.y = e.Y;
                    element = UIA.ElementFromPoint(points);
                    if (BorderBrush.Equals(Brushes.Yellow))
                    {
                        selectedItem = element.CurrentBoundingRectangle;
                        ShowFocusingAttention(selectedItem);
                    }

                    if (TNodes.Count == 0 || child == null)
                    {
                        TNodes.Add(new ControlClass() { Name = "Control" + controlCount + "(" + element.CurrentName + " " + element.CurrentAriaRole + " " + element.CurrentFrameworkId + ")", FirstName = element.CurrentName, role = element.CurrentAriaRole, value = element.CurrentFrameworkId, Image = new BitmapImage(new Uri("Resources/fileTxt.png", UriKind.Relative)) });
                        controlCount++;
                    }
                    else
                    {
                        child.Nodes.Add(new ControlClass() { Name = "Control" + controlCount + "(" + element.CurrentName + " " + element.CurrentAriaRole + " " + element.CurrentFrameworkId + ")", FirstName = element.CurrentName, role = element.CurrentAriaRole, value = element.CurrentFrameworkId, Image = new BitmapImage(new Uri("Resources/fileTxt.png", UriKind.Relative)) });
                        controlCount++;
                    }
                    OnResetCursor(null);
                }
                catch (Exception) { }
            }
        }
        public void OnMainWindowClosing(object sender, CancelEventArgs e)
        {
            isClose = true;
            focusingAttention.Close();
        }
        private void UpdateRect(object sender, EventArgs e)
        {
            try
            {
                selectedItem = element.CurrentBoundingRectangle;
            }
            catch (Exception) { }
        }
        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            try
            {
                if (isClose == true)
                {
                    focusingAttention.Close();
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception) { }
        }
        private void ShowFocusingAttention(tagRECT a)
        {
            try
            {
                focusingAttention.Width = a.right - a.left;
                focusingAttention.Height = a.bottom - a.top;
                focusingAttention.BorderBrush = Brushes.Yellow;
                focusingAttention.Top = a.top;
                focusingAttention.Left = a.left;
                focusingAttention.Show();
            }
            catch (Exception) { }
        }
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
            if (BorderBrush.Equals(Brushes.Gray))
            {
                BorderBrush = Brushes.Yellow;
            }
            else
            {
                BorderBrush = Brushes.Gray;
            }
            setCursor(Cursors.Arrow);
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
            setCursor(Cursors.Arrow);
        }
        private void OnAddControl(object a)
        {
            isClick = true;
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
            setCursor(Cursors.Arrow);
        }
        private void OnResetCursor(object a)
        {
            isClick = false;
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