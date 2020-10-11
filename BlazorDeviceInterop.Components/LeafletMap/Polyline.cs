using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorDeviceInterop.Components.LeafletMap
{
    public class Polyline : Path
    {
        [JsonIgnore] public IEnumerable<LatLng> LatLngs { get; }
        [JsonIgnore] public PolylineOptions Options { get; }

        public Polyline(IEnumerable<LatLng> latLngs, PolylineOptions options)
        {
            LatLngs = latLngs;
            Options = options;
        }

        protected override async Task<JsRuntimeObjectRef> CreateJsObjectRef(IJSRuntime jsRuntime)
        {
            return await jsRuntime.InvokeAsync<JsRuntimeObjectRef>("LeafletMap.polyline", LatLngs.ToArray(), Options);
        }

        public async Task<Polyline> AddLatLng(LatLng latLng)
        {
            await _jsObjRef.JSRuntime.InvokeVoidAsync("LeafletMap.Polyline.addLatLng", this, latLng);
            return this;
        }
    }
}
