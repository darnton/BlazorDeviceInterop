using Microsoft.JSInterop;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorDeviceInterop.Components
{
    public class JsRuntimeObjectRef : IAsyncDisposable
    {
        internal IJSRuntime JSRuntime { get; set; }

        [JsonPropertyName("__jsObjRefId")]
        public int JsObjectRefId { get; set; }

        public async ValueTask DisposeAsync()
        {
            await JSRuntime.InvokeVoidAsync("deviceInterop.removeObjectRef", JsObjectRefId);
        }
    }
}
