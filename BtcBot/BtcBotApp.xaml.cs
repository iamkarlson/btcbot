using System.Windows;

using BtcBot.Services;
using BtcBot.ViewModels;

using Unity;

namespace BtcBot {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class BtcBotApp: Application {
        IUnityContainer container = new UnityContainer();

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            container.RegisterType<IPollerService, BasicPollerService>();
            container.RegisterType<GraphViewModel>();
            MainWindow mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        public T CreateView<T>() {
            return container.Resolve<T>();
        }
    }
}