using Microsoft.JSInterop;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorDeviceInterop.Components.LeafletMap
{
    public class TileLayer : Layer
    {
        [JsonIgnore] public string UrlTemplate { get; }
        [JsonIgnore] public TileLayerOptions Options { get; }

        public TileLayer(string urlTemplate, TileLayerOptions options)
        {
            UrlTemplate = urlTemplate;
            Options = options;
        }

        protected override async Task<JsRuntimeObjectRef> CreateJsObjectRef(IJSRuntime jsRuntime)
        {
            return await jsRuntime.InvokeAsync<JsRuntimeObjectRef>("LeafletMap.tileLayer", UrlTemplate, Options);
        }
    }
}
