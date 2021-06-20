using MyHome.Settings;
using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace MyHome.Views
{
    public sealed partial class ClockControlAnalog : UserControl
    {
        private Brush _background = new SolidColorBrush(ProgrammSettings.BackgroundColor);

        private double _diameter;

        private Canvas _face = new Canvas();

        private Brush _foreground = new SolidColorBrush(ProgrammSettings.ForegroundColor);

        private Rectangle _hoursHand;

        private int _hoursHeight;

        private int _hoursWidth = 8;

        private Canvas _markers = new Canvas();

        private Rectangle _minutesHand;

        private int _minutesHeight;

        private int _minutesWidth = 5;

        private Rectangle _secondsHand;

        private int _secondsHeight;

        private int _secondsWidth = 1;

        private DispatcherTimer _timer = new DispatcherTimer();

        public ClockControlAnalog()
        {
            this.InitializeComponent();
            UISettings _uiSettings = new UISettings();
            if (ProgrammSettings.AutomaticColor)
            {
                _foreground = new SolidColorBrush(_uiSettings.GetColorValue(UIColorType.Background));
                _background = new SolidColorBrush(_uiSettings.GetColorValue(UIColorType.Accent));
            }
            else
            {
                _background = new SolidColorBrush(ProgrammSettings.BackgroundColor);
                _foreground = new SolidColorBrush(ProgrammSettings.ForegroundColor);
            }
        }

        public bool Enabled
        {
            get { return _timer.IsEnabled; }
            set
            {
                if (_timer.IsEnabled)
                {
                    _timer.Stop();
                }
                else
                {
                    _timer.Start();
                }
            }
        }

        public bool IsRealTime { get; set; } = true;

        public Brush RimBackground
        {
            get { return _background; }
            set
            {
                _background = value;
                Layout(ref Display);
            }
        }

        public Brush RimForeground
        {
            get { return _foreground; }
            set
            {
                _foreground = value;
                Layout(ref Display);
            }
        }

        public bool ShowHours { get; set; } = true;

        public bool ShowMinutes { get; set; } = true;

        public bool ShowSeconds { get; set; } = true;

        public DateTime Time { get; set; } = DateTime.Now;

        private void _timer_Tick(object sender, object e)
        {
            UpdateClockView();
        }

        private void AddHand(ref Rectangle hand)
        {
            if (!_face.Children.Contains(hand))
            {
                _face.Children.Add(hand);
            }
        }

        private void Display_Loaded(object sender, RoutedEventArgs e)
        {
            Layout(ref Display);
            UpdateClockView();
            if (ProgrammSettings.ShowSeconds)
                _timer.Interval = TimeSpan.FromSeconds(1);
            else
                _timer.Interval = TimeSpan.FromMinutes(1);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private Rectangle Hand(double width, double height, double radiusX, double radiusY, double thickness)
        {
            Rectangle hand = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = _background,
                StrokeThickness = thickness,
                RadiusX = radiusX,
                RadiusY = radiusY
            };
            return hand;
        }

        private void HourHand(int hours, int minutes, int seconds)
        {
            RemoveHand(ref _hoursHand);
            if (ShowHours)
            {
                _hoursHand = Hand(_hoursWidth, _hoursHeight, 3, 3, 0.6);
                _hoursHand.RenderTransform = TransformGroup(30 * hours + minutes / 2 + seconds / 120,
                -_hoursWidth / 2, -_hoursHeight + 4.25);
                AddHand(ref _hoursHand);
            }
        }

        private void Layout(ref Canvas canvas)
        {
            canvas.Children.Clear();
            _diameter = canvas.Width;
            double inner = _diameter - 15;
            Ellipse rim = new Ellipse
            {
                Height = _diameter,
                Width = _diameter,
                Stroke = RimBackground,
                StrokeThickness = 20
            };
            canvas.Children.Add(rim);
            _markers.Children.Clear();
            _markers.Width = inner;
            _markers.Height = inner;
            for (int i = 0; i < 60; i++)
            {
                Rectangle marker =
                    new Rectangle
                    {
                        Fill = RimForeground
                    };
                if ((i % 5) == 0)
                {
                    marker.Width = 3;
                    marker.Height = 8;
                    marker.RenderTransform = TransformGroup(i * 6, -(marker.Width / 2),
                    -(marker.Height * 2 + 4.5 - rim.StrokeThickness / 2 - inner / 2 - 6));
                }
                else
                {
                    marker.Width = 1;
                    marker.Height = 4;
                    marker.RenderTransform = TransformGroup(i * 6, -(marker.Width / 2),
                    -(marker.Height * 2 + 12.75 - rim.StrokeThickness / 2 - inner / 2 - 8));
                }
                _markers.Children.Add(marker);
            }
            canvas.Children.Add(_markers);
            _face.Width = _diameter;
            _face.Height = _diameter;
            canvas.Children.Add(_face);
            _secondsHeight = (int)_diameter / 2 - 20;
            _minutesHeight = (int)_diameter / 2 - 40;
            _hoursHeight = (int)_diameter / 2 - 60;
        }

        private void MinuteHand(int minutes, int seconds)
        {
            RemoveHand(ref _minutesHand);
            if (ShowMinutes)
            {
                _minutesHand = Hand(_minutesWidth, _minutesHeight, 2, 2, 0.6);
                _minutesHand.RenderTransform = TransformGroup(6 * minutes + seconds / 10,
                -_minutesWidth / 2, -_minutesHeight + 4.25);
                AddHand(ref _minutesHand);
            }
        }

        private void RemoveHand(ref Rectangle hand)
        {
            if (hand != null && _face.Children.Contains(hand))
            {
                _face.Children.Remove(hand);
            }
        }

        private void SecondHand(int seconds)
        {
            RemoveHand(ref _secondsHand);
            if (ShowSeconds)
            {
                _secondsHand = Hand(_secondsWidth, _secondsHeight, 0, 0, 0);
                _secondsHand.RenderTransform = TransformGroup(seconds * 6,
                -_secondsWidth / 2, -_secondsHeight + 4.25);
                AddHand(ref _secondsHand);
            }
        }

        private TransformGroup TransformGroup(double angle, double x, double y)
        {
            TransformGroup transformGroup = new TransformGroup();
            TranslateTransform firstTranslate = new TranslateTransform
            {
                X = x,
                Y = y
            };
            transformGroup.Children.Add(firstTranslate);
            RotateTransform rotateTransform = new RotateTransform
            {
                Angle = angle
            };
            transformGroup.Children.Add(rotateTransform);
            TranslateTransform secondTranslate = new TranslateTransform
            {
                X = _diameter / 2,
                Y = _diameter / 2
            };
            transformGroup.Children.Add(secondTranslate);
            return transformGroup;
        }

        private void UpdateClockView()
        {
            if (IsRealTime) Time = DateTime.Now;
            if (ProgrammSettings.ShowSeconds)
                SecondHand(Time.Second);
            MinuteHand(Time.Minute, Time.Second);
            HourHand(Time.Hour, Time.Minute, Time.Second);
        }
    }
}