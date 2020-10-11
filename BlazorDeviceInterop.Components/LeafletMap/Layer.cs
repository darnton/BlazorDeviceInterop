using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorDeviceInterop.Components.LeafletMap
{
    public abstract class Layer : InteropObject
    {
        public async Task<Layer> AddTo(Map map)
        {
            await _jsObjRef.JSRuntime.InvokeVoidAsync("LeafletMap.Layer.addTo", this, map);
            return this;
        }

        public async Task<Layer> Remove()
        {
            await _jsObjRef.JSRuntime.InvokeVoidAsync("LeafletMap.Layer.remove", this);
            return this;
        }
    }
}
