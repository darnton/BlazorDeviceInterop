using Microsoft.JSInterop;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorDeviceInterop.Components.LeafletMap
{
    public class Marker : InteractiveLayer
    {
        [JsonIgnore] public LatLng LatLng { get; }
        [JsonIgnore] public MarkerOptions Options { get; }

        public Marker(LatLng latlng, MarkerOptions options)
        {
            LatLng = latlng;
            Options = options;
        }
        protected override async Task<JsRuntimeObjectRef> CreateJsObjectRef(IJSRuntime jsRuntime)
        {
            return await jsRuntime.InvokeAsync<JsRuntimeObjectRef>("LeafletMap.marker", LatLng, Options);
        }
    }
}
