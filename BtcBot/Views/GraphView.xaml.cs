using System.Windows;

using BtcBot.ViewModels;

using Unity.Attributes;

namespace BtcBot.Views {
    /// <summary>
    /// Interaction logic for GraphView.xaml
    /// </summary>
    public partial class GraphView: Window {
        [Dependency]
        public GraphViewModel ViewModel {
            set { DataContext = value; }
        }

        public GraphView() {
            InitializeComponent();
        }
    }
}