using System;

namespace BlazorDeviceInterop.Geolocation
{
    [Serializable]
    /// <summary>
    /// The reason for a Geolocation error, based on https://developer.mozilla.org/en-US/docs/Web/API/GeolocationPositionError.
    /// </summary>
    public class GeolocationPositionError
    {
        /// <summary>
        /// The code for the error
        /// </summary>
        public GeolocationPositionErrorCode Code { get; set; }

        /// <summary>
        /// Details of the error. Intended for debugging rather than display to the user.
        /// </summary>
        public string Message { get; set; }
    }
}
