using System;
using System.Text.Json.Serialization;

namespace BlazorDeviceInterop.Geolocation
{
    [Serializable]
    /// <summary>
    /// Geolocation Position, based on https://developer.mozilla.org/en-US/docs/Web/API/GeolocationPosition.
    /// </summary>
    public class GeolocationPosition
    {
        /// <summary>
        /// The coordinates defining the current location
        /// </summary>
        public GeolocationCoordinates Coords { get; set; }

        /// <summary>
        /// The time the coordinates were taken, in milliseconds since the Unix epoch.
        /// </summary>
        public long Timestamp { get; set; }

        private const long UnixEpochTicks = 621355968000000000;
        private const long TicksPerMillisecond = 10000;
        [JsonIgnore]
        public DateTime DateTime => new DateTime(Timestamp * TicksPerMillisecond + UnixEpochTicks);
    }
}
