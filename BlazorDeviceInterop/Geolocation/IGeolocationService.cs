using System;
using System.Threading.Tasks;

namespace BlazorDeviceInterop.Geolocation
{
    public interface IGeolocationService
    {
        Task<GeolocationResult> GetCurrentPosition(PositionOptions options = null);
        Task<long?> WatchPosition(PositionOptions options = null);
        void SetWatchPosition(GeolocationResult watchResult);
        event EventHandler<GeolocationEventArgs> WatchPositionReceived;
        Task ClearWatch(long watchId);
    }
}
