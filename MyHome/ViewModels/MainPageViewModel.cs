using MyHome.Helpers;
using MyHome.Settings;
using Windows.UI.Xaml;

namespace MyHome.ViewModels
{
    public class MainPageViewModel : Observable
    {
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
