using Darnton.Blazor.Leaflet.LeafletMap;
using Darnton.Blazor.DeviceInterop.Geolocation;
using BlazorDeviceTestRig.Geolocation;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceTestRig.Pages
{
    public class GeolocationBase : ComponentBase, IDisposable
    {
        [Inject] public IGeolocationService GeolocationService { get; set; }

        protected Map PositionMap;
        protected TileLayer PositionTileLayer;
        protected Marker CurrentPositionMarker;

        protected Map WatchMap;
        protected TileLayer WatchTileLayer;
        protected Polyline WatchPath;
        protected List<Marker> WatchMarkers;

        protected GeolocationResult CurrentPositionResult { get; set; }
        protected string CurrentLatitude => CurrentPositionResult?.Position?.Coords?.Latitude.ToString("F2");
        protected string CurrentLongitude => CurrentPositionResult?.Position?.Coords?.Longitude.ToString("F2");
        protected bool ShowCurrentPositionError => CurrentPositionResult?.Error != null;

        private bool isWatching => WatchHandlerId.HasValue;
        protected long? WatchHandlerId { get; set; }
        protected GeolocationResult LastWatchPositionResult { get; set; }
        protected string LastWatchLatitude => LastWatchPositionResult?.Position?.Coords?.Latitude.ToString("F2");
        protected string LastWatchLongitude => LastWatchPositionResult?.Position?.Coords?.Longitude.ToString("F2");
        protected string LastWatchTimestamp => LastWatchPositionResult?.Position?.DateTimeOffset.ToString();
        protected string ToggleWatchCommand => isWatching ? "Stop watching" : "Start watching";

        public GeolocationBase() : base()
        {
            PositionMap = new Map("geolocationPointMap", new MapOptions //Centred on New Zealand
            {
                Center = new LatLng(-42, 175),
                Zoom = 4
            });
            PositionTileLayer = new TileLayer(
                "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
                new TileLayerOptions
                {
                    Attribution = @"Map data &copy; <a href=""https://www.openstreetmap.org/"">OpenStreetMap</a> contributors, " +
                        @"<a href=""https://creativecommons.org/licenses/by-sa/2.0/"">CC-BY-SA</a>"
                }
            );
            WatchMap = new Map("geolocationWatchMap", new MapOptions //Centred on New Zealand
            {
                Center = new LatLng(-42, 175),
                Zoom = 4
            });
            WatchTileLayer = new TileLayer(
                "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
                new TileLayerOptions
                {
                    Attribution = @"Map data &copy; <a href=""https://www.openstreetmap.org/"">OpenStreetMap</a> contributors, " +
                        @"<a href=""https://creativecommons.org/licenses/by-sa/2.0/"">CC-BY-SA</a>"
                }
            );
        }

        public async void ShowCurrentPosition()
        {
            if (CurrentPositionMarker != null)
            {
                await CurrentPositionMarker.Remove();
            }
            CurrentPositionResult = await GeolocationService.GetCurrentPosition();
            if (CurrentPositionResult.IsSuccess)
            {
                CurrentPositionMarker = new Marker(
                        CurrentPositionResult.Position.ToLeafletLatLng(), null
                    );
                await CurrentPositionMarker.AddTo(PositionMap);
            }
            StateHasChanged();
        }

        public async void TogglePositionWatch()
        {
            if (isWatching)
            {
                await StopWatching();
                WatchHandlerId = null;
                foreach (var marker in WatchMarkers)
                {
                    await marker.Remove();
                }
                WatchMarkers.Clear();
                await WatchPath.Remove();
                WatchPath = null;
            }
            else
            {
                GeolocationService.WatchPositionReceived += HandleWatchPositionReceived;
                WatchHandlerId = await GeolocationService.WatchPosition();
            }
            StateHasChanged();
        }

        private async Task StopWatching()
        {
            GeolocationService.WatchPositionReceived -= HandleWatchPositionReceived;
            await GeolocationService.ClearWatch(WatchHandlerId.Value);
        }

        private async void HandleWatchPositionReceived(object sender, GeolocationEventArgs e)
        {
            LastWatchPositionResult = e.GeolocationResult;
            if (LastWatchPositionResult.IsSuccess)
            {
                var latlng = LastWatchPositionResult.Position.ToLeafletLatLng();
                var marker = new Marker(latlng, null);
                if (WatchPath is null)
                {
                    WatchMarkers = new List<Marker> { marker };
                    WatchPath = new Polyline(WatchMarkers.Select(m => m.LatLng), new PolylineOptions());
                    await WatchPath.AddTo(WatchMap);
                }
                else
                {
                    WatchMarkers.Add(marker);
                    await WatchPath.AddLatLng(latlng);
                }
                await marker.AddTo(WatchMap);
            }
            StateHasChanged();
        }

        public async void Dispose()
        {
            if (isWatching)
            {
                await StopWatching();
            }
        }
    }
}
