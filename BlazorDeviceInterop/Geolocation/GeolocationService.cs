using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorDeviceInterop.Geolocation
{
    public class GeolocationService : IGeolocationService
    {
        private readonly IJSRuntime _jsRuntime;

        public event EventHandler<GeolocationEventArgs> WatchPositionReceived;

        public GeolocationService(IJSRuntime JSRuntime)
        {
            _jsRuntime = JSRuntime;
        }

        public async Task<GeolocationResult> GetCurrentPosition(PositionOptions options = null)
        {
            return await _jsRuntime.InvokeAsync<GeolocationResult>("Geolocation.getCurrentPosition", options);
        }

        public async Task<long?> WatchPosition(PositionOptions options = null)
        {
            var callbackObj = DotNetObjectReference.Create(this);
            return await _jsRuntime.InvokeAsync<int>("Geolocation.watchPosition",
                callbackObj, nameof(SetWatchPosition), options);
        }

        [JSInvokable]
        public void SetWatchPosition(GeolocationResult watchResult)
        {
            WatchPositionReceived?.Invoke(this, new GeolocationEventArgs
            {
                GeolocationResult = watchResult
            });
        }

        public async Task ClearWatch(long watchId)
        {
            await _jsRuntime.InvokeVoidAsync("Geolocation.clearWatch", watchId);
        }
    }
}
