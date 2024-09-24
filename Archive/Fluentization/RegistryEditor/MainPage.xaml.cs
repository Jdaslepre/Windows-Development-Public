using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RegistryEditor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private RTreeViewNode selectedItem;

        public event PropertyChangedEventHandler PropertyChanged;

        public RTreeViewNode SelectedItem
        {
            get => selectedItem;
            set
            {
                if (value != selectedItem)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //public RegistryViewModel ViewModel => (RegistryViewModel)DataContext;
        public MainPage()
        {
            InitializeComponent();
            //DataContext = new RegistryViewModel();
            InitializeRegistryTreeView();
        }



        public class RegistryKeyViewModel
        {
            public string KeyName { get; set; }
            public ObservableCollection<RegistryKeyViewModel> SubKeys { get; set; }

            public RegistryKeyViewModel(string keyName)
            {
                KeyName = keyName;
                SubKeys = new ObservableCollection<RegistryKeyViewModel>();
            }
        }

        public RegistryKeyViewModel SelectedRegistryKey
        {
            get { return (RegistryKeyViewModel)regList.SelectedItem; }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.GetWindowForElement(this).Close();
        }


        private async void InitializeRegistryTreeView()
        {
            var PCGlyph = "\uE7F4";
            var FolderGlyph = "\uE8B7";
            RTreeViewNode computerNode = new RTreeViewNode { Content = "Computer", Glyph = PCGlyph };
            computerNode.IsExpanded = true;
            regList.RootNodes.Add(computerNode);
            regList.SelectedNode = computerNode;

            // ClassesRoot
            //RTreeViewNode ClassesRoot = new RTreeViewNode { Content = Registry.ClassesRoot.Name, Glyph = FolderGlyph };
            //computerNode.Children.Add(ClassesRoot);
            //AddChildNodes(Registry.ClassesRoot, ClassesRoot);

            // Current user
            //RTreeViewNode CurrentUser = new RTreeViewNode { Content = Registry.CurrentUser.Name, Glyph = FolderGlyph };
            //computerNode.Children.Add(CurrentUser);
            //AddChildNodes(Registry.CurrentUser, CurrentUser);


            // LocalMachine
            //RTreeViewNode LocalMachine = new RTreeViewNode { Content = Registry.LocalMachine.Name, Glyph = FolderGlyph };
            //computerNode.Children.Add(LocalMachine);
            //AddChildNodes(Registry.LocalMachine, LocalMachine);

            // Users
            //RTreeViewNode Users = new RTreeViewNode { Content = Registry.Users.Name, Glyph = FolderGlyph };
            //computerNode.Children.Add(Users);
            //AddChildNodes(Registry.Users, Users);

            // CurrentConfig
            RTreeViewNode CurrentConfig = new RTreeViewNode { Content = Registry.CurrentConfig.Name, Glyph = FolderGlyph };
            computerNode.Children.Add(CurrentConfig);
            AddChildNodes(Registry.CurrentConfig, CurrentConfig);

        }

        private async Task AddChildNodes(RegistryKey parentKey, RTreeViewNode parentNode)
        {
            string[] subKeyNames = parentKey.GetSubKeyNames();

            for (int i = 0; i < subKeyNames.Length; i++)
            {
                RegistryKey subKey = parentKey.OpenSubKey(subKeyNames[i]);
                RTreeViewNode subNode = new RTreeViewNode { Content = subKeyNames[i], Glyph = "\uE8B7" };

                DispatcherQueue.TryEnqueue(() =>
                {
                    parentNode.Children.Add(subNode);
                });

                if ((i + 1) % 15 == 0)
                {
                    await Task.Delay(10);
                    await AddChildNodes(subKey, subNode);
                }
                else
                {
                    await AddChildNodes(subKey, subNode);
                }
            }
        }

        StringBuilder fullPath = new StringBuilder();

        private async void regList_SelectionChanged(TreeView sender, TreeViewSelectionChangedEventArgs args)
        {
            if (args.AddedItems.Count == 0)
            {
                return;
            }

            RTreeViewNode selectedNode = (RTreeViewNode)args.AddedItems[0];
            SelectedItem = selectedNode;

            string selectedKey = selectedNode.Content.ToString();

            StringBuilder fullPath = new StringBuilder(selectedKey);
            RTreeViewNode parentNode = selectedNode.Parent as RTreeViewNode;
            while (parentNode != null)
            {
                fullPath.Insert(0, parentNode.Content + "\\");
                parentNode = parentNode.Parent as RTreeViewNode;
            }

            string[] segments = fullPath.ToString().Split('\\');

            int index = -1;
            for (int i = 0; i < pathStack.Children.Count; i++)
            {
                UIElement child = pathStack.Children[i];
                if (child is SplitButton splitButton && segments.Length > 1 && (string)splitButton.Content == segments[segments.Length - 2])
                {
                    index = i;
                    break;
                }
            }

            if (index >= 0)
            {
                for (int i = pathStack.Children.Count - 1; i > index; i--)
                {
                    pathStack.Children.RemoveAt(i);
                }
            }
            else
            {
                pathStack.Children.Clear();
            }

            string currentPath = "";
            for (int i = 0; i < segments.Length - 1; i++)
            {
                string segment = segments[i];

                if (string.IsNullOrWhiteSpace(segment))
                {
                    continue;
                }

                currentPath += segment + "\\";

                bool segmentExists = false;
                foreach (UIElement child in pathStack.Children)
                {
                    if (child is SplitButton splitButton && (string)splitButton.Content == segment)
                    {
                        segmentExists = true;
                        break;
                    }
                }

                if (!segmentExists)
                {
                    SplitButton splitButton = new SplitButton();
                    splitButton.Content = segment;
                    splitButton.Tag = currentPath;
                    pathStack.Children.Add(splitButton);
                }
            }

            Button lastSegmentButton = new Button();
            lastSegmentButton.Content = segments[segments.Length - 1];
            pathStack.Children.Add(lastSegmentButton);
        }
       
    }
}
