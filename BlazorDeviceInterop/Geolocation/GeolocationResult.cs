using System;
using System.Text.Json.Serialization;

namespace BlazorDeviceInterop.Geolocation
{
    [Serializable]
    public class GeolocationResult
    {
        public GeolocationPosition Position { get; set; }
        public GeolocationPositionError Error { get; set; }

        [JsonIgnore]
        public bool IsSuccess => !(Position is null);
    }
}
