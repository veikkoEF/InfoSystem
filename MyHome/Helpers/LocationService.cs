using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace MyHome.Helpers
{
    public static class LocationService
    {
        public static async Task<Geoposition> GetPositionAsync()
        {
            Geoposition geoPosition = null;
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    // notify user: Waiting for update

                    // If DesiredAccuracy or DesiredAccuracyInMeters are not set (or value is 0), DesiredAccuracy.Default is used.
                    Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 1000 };
                    // Carry out the operation
                    geoPosition = await geolocator.GetGeopositionAsync();
                    break;

                case GeolocationAccessStatus.Denied:
                    // notify user: Access to location is denied
                    break;

                case GeolocationAccessStatus.Unspecified:
                    // notify user: Unspecified error
                    break;
            }
            return geoPosition;
        }
    }
}