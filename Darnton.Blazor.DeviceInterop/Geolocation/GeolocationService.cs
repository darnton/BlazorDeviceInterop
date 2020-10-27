using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Darnton.Blazor.DeviceInterop.Geolocation
{
    /// <summary>
    /// An implementation of <see cref="IGeolocationService"/> that provides 
    /// an interop layer for the device's Geolocation API.
    /// </summary>
    public class GeolocationService : IGeolocationService
    {
        private readonly IJSRuntime _jsRuntime;

        /// <inheritdoc/>
        public event EventHandler<GeolocationEventArgs> WatchPositionReceived;

        /// <summary>
        /// Constructs a <see cref="GeolocationService"/> object.
        /// </summary>
        /// <param name="JSRuntime"></param>
        public GeolocationService(IJSRuntime JSRuntime)
        {
            _jsRuntime = JSRuntime;
        }

        /// <inheritdoc/>
        public async Task<GeolocationResult> GetCurrentPosition(PositionOptions options = null)
        {
            return await _jsRuntime.InvokeAsync<GeolocationResult>("Geolocation.getCurrentPosition", options);
        }

        /// <inheritdoc/>
        public async Task<long?> WatchPosition(PositionOptions options = null)
        {
            var callbackObj = DotNetObjectReference.Create(this);
            return await _jsRuntime.InvokeAsync<int>("Geolocation.watchPosition",
                callbackObj, nameof(SetWatchPosition), options);
        }

        /// <summary>
        /// Invokes the <see cref="WatchPositionReceived"/> event handler.
        /// Invoked by the success and error callbacks of the JavaScript watchPosition() function.
        /// </summary>
        /// <param name="watchResult">A <see cref="GeolocationResult"/> passed back from JavaScript.</param>
        [JSInvokable]
        public void SetWatchPosition(GeolocationResult watchResult)
        {
            WatchPositionReceived?.Invoke(this, new GeolocationEventArgs
            {
                GeolocationResult = watchResult
            });
        }

        /// <inheritdoc/>
        public async Task ClearWatch(long watchId)
        {
            await _jsRuntime.InvokeVoidAsync("Geolocation.clearWatch", watchId);
        }
    }
}
