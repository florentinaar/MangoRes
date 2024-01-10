using static Mango.Web.SD;

namespace Mango.Web.Models
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; } //the data can be generic(we dont know what type) so we leave it as object
        public string AccessToken { get; set; }
    }
}
