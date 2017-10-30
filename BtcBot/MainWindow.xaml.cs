using System.Windows;

using BtcBot.Views;

namespace BtcBot {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {
        public MainWindow() {
            InitializeComponent();
            GraphView view = ((BtcBotApp) Application.Current).CreateView<GraphView>();
            view.Show();
            Visibility = Visibility.Hidden;
        }
    }
}