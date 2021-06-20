using MyHome.Helpers;
using MyHome.Settings;

namespace MyHome.ViewModels
{
    public class HomePageViewModel : Observable
    {
        public string Message
        {
            get => ProgrammSettings.Message;
            set
            {
                ProgrammSettings.Message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
    }
}