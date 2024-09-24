using Microsoft.UI.Xaml.Controls;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace RegistryEditor.Content.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<TreeViewNode> RootNodes { get; set; }

        public MainViewModel()
        {
            RootNodes = new ObservableCollection<TreeViewNode>();
        }

        public void LoadRegistry()
        {
            TreeViewNode computerNode = new TreeViewNode { Content = "Computer" };
            computerNode.IsExpanded = true;
            RootNodes.Add(computerNode);

            // LocalMachine
            TreeViewNode LocalMachine = new TreeViewNode { Content = Registry.LocalMachine.Name };
            computerNode.Children.Add(LocalMachine);
            AddChildNodes(Registry.CurrentUser, LocalMachine);
        }

        private void AddChildNodes(RegistryKey parentKey, TreeViewNode parentNode)
        {
            string[] subKeyNames = parentKey.GetSubKeyNames();

            foreach (string subKeyName in subKeyNames)
            {
                RegistryKey subKey = parentKey.OpenSubKey(subKeyName);
                TreeViewNode subNode = new TreeViewNode { Content = subKeyName, };

                parentNode.Children.Add(subNode);

                AddChildNodes(subKey, subNode);
            }
        }
    }
}
