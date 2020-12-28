using MyHome.Settings;
using MyHome.ViewModels;
using System;
using System.Linq;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace MyHome.Views
{
    public sealed partial class MainPage : Page
    {
        private ContentType type;
        private DispatcherTimer uiTimer = new DispatcherTimer();

        public MainPage()
        {
            this.InitializeComponent();
            Application.Current.Suspending += new SuspendingEventHandler(App_Suspending);
            uiTimer.Interval = new TimeSpan(0, 0, 5);
            uiTimer.Start();
            uiTimer.Tick += UpdateContent;
            type = ContentType.Home;
        }

        public MainPageViewModel ViewModel { get; } = new MainPageViewModel();

        private void App_Suspending(object sender, SuspendingEventArgs e)
        {
            ProgrammSettings.Save();
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            uiTimer.Stop();
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingsPage), null, new DrillInNavigationTransitionInfo());
                // nach dem Aufrufen der Settingseite ist der Cache für Seiten zu löschen
                var cacheSize = ContentFrame.CacheSize;
                ContentFrame.CacheSize = 0;
                ContentFrame.CacheSize = cacheSize;
                NavView.PaneDisplayMode = NavigationViewPaneDisplayMode.LeftCompact;
            }
            else
            {
                if (ProgrammSettings.AutoChangeContentSections)
                    uiTimer.Start();
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavView_Navigate(item as NavigationViewItem);
                NavView.PaneDisplayMode = NavigationViewPaneDisplayMode.LeftMinimal;
            }
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            // set the initial SelectedItem
            ContentFrame.Navigate(typeof(HomePage), null, new DrillInNavigationTransitionInfo());
        }

        private void NavView_Navigate(NavigationViewItem item)
        {
            switch (item.Tag)
            {
                case "home":
                    type = ContentType.Home;
                    break;

                case "weather":
                    type = ContentType.Weather;
                    break;

                case "news":
                    type = ContentType.News;
                    break;

                case "pictures":
                    type = ContentType.Picture;
                    break;

                case "clock":
                    type = ContentType.Clock;
                    break;

                case "map":
                    type = ContentType.Map;
                    break;

                case "message":
                    type = ContentType.Message;
                    break;

                case "canvas":
                    type = ContentType.Canvas;
                    break;
            }
            UpdateContent();
        }

        private void UpdateContent(object sender, object e)
        {
            UpdateContent();
        }

        private void UpdateContent()
        {
            switch (type)
            {
                case ContentType.Weather:
                    type = ContentType.News;
                    if (ProgrammSettings.WeatherIsActiv)
                    {
                        ContentFrame.Navigate(typeof(WeatherPage), null, new DrillInNavigationTransitionInfo());
                        uiTimer.Interval = new TimeSpan(0, 0, ProgrammSettings.ShowDurationWeather);
                    }
                    else
                        uiTimer.Interval = new TimeSpan(0, 0, 1);
                    break;

                case ContentType.News:
                    type = ContentType.Picture;
                    if (ProgrammSettings.NewsIsActiv)
                    {
                        ContentFrame.Navigate(typeof(NewsPage), null, new DrillInNavigationTransitionInfo());
                        uiTimer.Interval = new TimeSpan(0, 0, ProgrammSettings.ShowDurationNews);
                    }
                    else
                        uiTimer.Interval = new TimeSpan(0, 0, 1);
                    break;

                case ContentType.Picture:
                    type = ContentType.Clock;
                    if (ProgrammSettings.PictureIsActiv)
                    {
                        ContentFrame.Navigate(typeof(PictureShowPage), null, new DrillInNavigationTransitionInfo());
                        uiTimer.Interval = new TimeSpan(0, 0, ProgrammSettings.ShowDurationPictures);
                    }
                    else
                        uiTimer.Interval = new TimeSpan(0, 0, 1);
                    break;

                case ContentType.Clock:
                    type = ContentType.Map;
                    if (ProgrammSettings.ClockIsActiv)
                    {
                        ContentFrame.Navigate(typeof(ClockPage), null, new DrillInNavigationTransitionInfo());
                        uiTimer.Interval = new TimeSpan(0, 0, ProgrammSettings.ShowDurationClock);
                    }
                    else
                        uiTimer.Interval = new TimeSpan(0, 0, 1);
                    break;

                case ContentType.Map:
                    type = ContentType.Message;
                    if (ProgrammSettings.MapIsActiv)
                    {
                        ContentFrame.Navigate(typeof(MapPage), null, new DrillInNavigationTransitionInfo());
                        uiTimer.Interval = new TimeSpan(0, 0, ProgrammSettings.ShowDurationMap);
                    }
                    else
                        uiTimer.Interval = new TimeSpan(0, 0, 1);
                    break;

                case ContentType.Message:
                    type = ContentType.Canvas;
                    if (ProgrammSettings.MessageIsActiv)
                    {
                        ContentFrame.Navigate(typeof(MessagePage), null, new DrillInNavigationTransitionInfo());
                        uiTimer.Interval = new TimeSpan(0, 0, ProgrammSettings.ShowDurationMessage);
                    }
                    else
                        uiTimer.Interval = new TimeSpan(0, 0, 1);
                    break;

                case ContentType.Canvas:
                    type = ContentType.Home;
                    if (ProgrammSettings.CanvasIsActiv)
                    {
                        ContentFrame.Navigate(typeof(CanvasPage), null, new DrillInNavigationTransitionInfo());
                        uiTimer.Interval = new TimeSpan(0, 0, ProgrammSettings.ShowDurationCanvas);
                    }
                    else
                        uiTimer.Interval = new TimeSpan(0, 0, 1);
                    break;


                case ContentType.Home:
                default:
                    type = ContentType.Weather;
                    if (ProgrammSettings.HomeIsActiv)
                    {
                        ContentFrame.Navigate(typeof(HomePage), null, new DrillInNavigationTransitionInfo());
                        uiTimer.Interval = new TimeSpan(0, 0, ProgrammSettings.ShowDurationHome);
                    }
                    else
                        uiTimer.Interval = new TimeSpan(0, 0, 1);
                    break;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1801:Nicht verwendete Parameter überprüfen", Justification = "<Ausstehend>")]
        private void NavView_PaneOpened(NavigationView sender, object args)
        {
            // Menüeinträge modifizieren
            HomeItem.Visibility = ProgrammSettings.HomeIsActiv ? Visibility.Visible : Visibility.Collapsed;
            WeatherItem.Visibility = ProgrammSettings.WeatherIsActiv ? Visibility.Visible : Visibility.Collapsed;
            NewsItem.Visibility = ProgrammSettings.NewsIsActiv ? Visibility.Visible : Visibility.Collapsed;
            PictureItem.Visibility = ProgrammSettings.PictureIsActiv ? Visibility.Visible : Visibility.Collapsed;
            ClockItem.Visibility = ProgrammSettings.ClockIsActiv ? Visibility.Visible : Visibility.Collapsed;
            MapItem.Visibility = ProgrammSettings.MapIsActiv ? Visibility.Visible : Visibility.Collapsed;
            MessageItem.Visibility = ProgrammSettings.MessageIsActiv ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
