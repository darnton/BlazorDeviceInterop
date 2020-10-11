using System;

namespace BlazorDeviceInterop.Geolocation
{
    public class GeolocationEventArgs : EventArgs
    {
        public GeolocationResult GeolocationResult { get; set; }
    }
}
