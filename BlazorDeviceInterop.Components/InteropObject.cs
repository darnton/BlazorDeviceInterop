using Microsoft.JSInterop;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorDeviceInterop.Components
{
    public abstract class InteropObject
    {
        protected JsRuntimeObjectRef _jsObjRef;

        [JsonPropertyName("__jsObjRefId")]
        public int JsObjectRefId { get { return _jsObjRef.JsObjectRefId; } }

        public async Task BindToJsRuntime(IJSRuntime jsRuntime)
        {
            _jsObjRef = await CreateJsObjectRef(jsRuntime);
            _jsObjRef.JSRuntime = jsRuntime;
        }

        protected abstract Task<JsRuntimeObjectRef> CreateJsObjectRef(IJSRuntime jsRuntime);
    }
}
