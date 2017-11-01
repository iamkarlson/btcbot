using System.Net;
using System.Windows;

using BtcBot.Adapters.Cexio;
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

            #region hacks
            // fix ssl issues
            // refs: 
            // https://stackoverflow.com/questions/18947373/restsharp-could-not-create-ssl-tls-secure-channel
            // https://stackoverflow.com/questions/2859790/the-request-was-aborted-could-not-create-ssl-tls-secure-channel
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            #endregion

            IPollerService poller = new BasicPollerService();
            poller.PushApi(new CexioApiAdapter());
            container.RegisterInstance<IPollerService>(poller);
            container.RegisterType<GraphViewModel>();
            MainWindow mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        public T CreateView<T>() {
            return container.Resolve<T>();
        }
    }
}