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
            set {
                DataContext = value;
                PricesListView.ItemsSource = value.Prices;
                value.PropertyChanged += (sender, args) => {
                    PricesListView.UpdateLayout();
                    Refresh(PricesListView);
                };
            }
        }

        public GraphView() {
            InitializeComponent();
        }

        public static void Refresh(DependencyObject obj) {
            obj.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, (NoArgDelegate) delegate { });
        }

        private delegate void NoArgDelegate();
    }
}