using BlazorDeviceInterop.Components.LeafletMap;
using BlazorDeviceInterop.Geolocation;

namespace BlazorDeviceTestRig.Geolocation
{
    public static class GeolocationPositionExtension
    {
        public static LatLng ToLeafletLatLng(this GeolocationPosition position)
        {
            var coords = position.Coords;
            return new LatLng(coords.Latitude, coords.Longitude);
        }
    }
}
