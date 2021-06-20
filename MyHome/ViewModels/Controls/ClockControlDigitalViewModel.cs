using MyHome.Helpers;
using MyHome.Settings;
using System;
using System.Globalization;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace MyHome.ViewModels
{
    public class ClockControlDigitalViewModel : Observable
    {
        private DispatcherTimer timer;
        private string time;
        private UISettings uiSettings = new UISettings();

        private string date;

        public string Date
        {
            get => date;
            set => Set(ref date, value);
        }

        public ClockControlDigitalViewModel()
        {
            UpdateUI();
            timer = new DispatcherTimer();
            if (ProgrammSettings.ShowSeconds)
                timer.Interval = new TimeSpan(0, 0, 1);
            else
                timer.Interval = new TimeSpan(0, 0, 10);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (ProgrammSettings.ShowSeconds)
                Time = DateTime.Now.ToLocalTime().ToString("HH:mm:ss");
            else
                Time = DateTime.Now.ToLocalTime().ToString("HH:mm");
            if (ProgrammSettings.ShowDate)
                Date = DateTime.Now.ToLocalTime().Date.ToString("D", CultureInfo.CreateSpecificCulture("de-DE"));
        }

        public Brush Foreground
        {
            get
            {
                if (ProgrammSettings.AutomaticColor)
                    return new SolidColorBrush(uiSettings.GetColorValue(UIColorType.Accent));
                else
                    return new SolidColorBrush(ProgrammSettings.ForegroundColor);
            }
        }

        public Brush Background
        {
            get
            {
                return new SolidColorBrush(uiSettings.GetColorValue(UIColorType.Background));
            }
        }

        public string Time
        {
            get => time;
            set => Set(ref time, value);
        }
    }
}