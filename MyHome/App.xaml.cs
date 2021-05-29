#pragma warning disable CA2007 // Aufruf von "ConfigureAwait" für erwarteten Task erwägen
using MyHome.Helpers;
using MyHome.Settings;
using System;
using Windows.ApplicationModel.Activation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace MyHome
{
    public sealed partial class App : Application
    {
        private readonly Lazy<ActivationService> _activationService;
        private DispatcherTimer appTimer;

        public void StartAppTimer()
        {
            appTimer.Start();
        }

        public void StopAppTimer()
        {
            appTimer.Stop();
        }

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDQ2ODA5QDMxMzkyZTMxMmUzMG9RYXI5QnRydUI0cUpNaDdmUFF4Wm5oalc4ZXRCMlAwaDVKb0lXbGgrdjQ9");

            // Deferred execution until used. Check https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);

            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
            ProgrammSettings.Load();
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.MainPage));
        }
    }
}
