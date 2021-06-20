using MyHome.Helpers;
using MyHome.Settings;
using Windows.ApplicationModel.Core;

namespace MyHome.ViewModels
{
    public class MainPageViewModel : Observable
    {
        public MainPageViewModel()
        {
            ShutDownCommand = new RelayCommand(OnShutDownCommandExecute);
        }

        private void OnShutDownCommandExecute()
        {
            ProgrammSettings.Save();
            CoreApplication.Exit();
        }

        public RelayCommand ShutDownCommand { get; set; }

        public bool AutoChangeContentSections
        {
            get
            {
                return ProgrammSettings.AutoChangeContentSections;
            }
            set
            {
                ProgrammSettings.AutoChangeContentSections = value;
                OnPropertyChanged(nameof(AutoChangeContentSections));
            }
        }
    }
}
