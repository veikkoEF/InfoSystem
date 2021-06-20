using MyHome.Helpers;
using System.Collections.Generic;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Xaml.Controls.Maps;

namespace MyHome.ViewModels
{
    public class MapControlViewModel : Observable
    {
        private Geopoint homeLocation;

        public IList<MapLayer> Layers { get; }

        public MapControlViewModel()
        {
            //GetCurrentPosition();
            Layers = new List<MapLayer>();
            AddHomePosition();
        }

        public Geopoint HomeLocation
        {
            get => homeLocation;
            set
            {
                homeLocation = value;
                OnPropertyChanged(nameof(HomeLocation));
            }
        }

        public async void GetCurrentPosition()
        {
            Geoposition position = await LocationService.GetPositionAsync().ConfigureAwait(true);
            if (position != null)
            {
                HomeLocation = new Geopoint(new BasicGeoposition() { Latitude = position.Coordinate.Point.Position.Latitude, Longitude = position.Coordinate.Point.Position.Longitude });
            }
        }

        private void AddHomePosition()
        {
            var MyLandmarks = new List<MapElement>();

            BasicGeoposition snPosition = new BasicGeoposition { Latitude = 50.987474, Longitude = 11.017008 };
            Geopoint snPoint = new Geopoint(snPosition);

            var spaceNeedleIcon = new MapIcon
            {
                Location = snPoint,
                NormalizedAnchorPoint = new Point(0.5, 1.0),
                ZIndex = 0,
                Title = "Zu Hause"
            };

            MyLandmarks.Add(spaceNeedleIcon);

            var LandmarksLayer = new MapElementsLayer
            {
                ZIndex = 1,
                MapElements = MyLandmarks
            };

            HomeLocation = snPoint;
            Layers.Add(LandmarksLayer);
        }
    }
}