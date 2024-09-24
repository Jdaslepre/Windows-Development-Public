using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

namespace RegistryEditor
{
    public class RTreeViewNode : TreeViewNode, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string glyph;
        public string Glyph
        {
            get { return glyph; }
            set
            {
                glyph = value;
                OnPropertyChanged(nameof(Glyph));
            }
        }

        private string content;
        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                OnPropertyChanged(nameof(Content));
            }
        }
    }
}
