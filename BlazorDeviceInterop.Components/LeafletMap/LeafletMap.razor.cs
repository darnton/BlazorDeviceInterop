using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorDeviceInterop.Components.LeafletMap
{
    public class LeafletMapBase : ComponentBase
    {
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Parameter] public Map Map { get; set; }
        [Parameter] public TileLayer TileLayer { get; set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Map.BindToJsRuntime(JSRuntime);
                await TileLayer.BindToJsRuntime(JSRuntime);
                await TileLayer.AddTo(Map);
            }
        }
    }
}
