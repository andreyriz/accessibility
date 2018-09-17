using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    [Serializable]
    public class Node
    {
        #region Field
        public BitmapImage Image { get; set; }
        public String Name { get; set; }
        public ObservableCollection<Node> Nodes { get; set; }
        #endregion
        public Node()
        {
            Nodes = new ObservableCollection<Node>();
        }
    }
}
