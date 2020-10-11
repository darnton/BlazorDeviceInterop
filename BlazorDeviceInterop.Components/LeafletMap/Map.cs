using Microsoft.JSInterop;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorDeviceInterop.Components.LeafletMap
{
    public class Map : InteropObject
    {
        [JsonIgnore] public string ElementId { get; }
        [JsonIgnore] public MapOptions Options { get; }

        public Map(string elementId, MapOptions options)
        {
            ElementId = elementId;
            Options = options;
        }

        protected override async Task<JsRuntimeObjectRef> CreateJsObjectRef(IJSRuntime jsRuntime)
        {
            return await jsRuntime.InvokeAsync<JsRuntimeObjectRef>("LeafletMap.map", ElementId, Options);
        }
    }
}
