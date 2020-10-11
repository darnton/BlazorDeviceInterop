namespace BlazorDeviceInterop.Components.LeafletMap
{
    public class LatLng
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public LatLng(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }
    }
}
