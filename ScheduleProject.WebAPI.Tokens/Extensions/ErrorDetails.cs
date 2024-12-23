using Newtonsoft.Json;

namespace TokenService.Extentions
{
    internal class ErrorDetails
    {
        public string ErrorMessage { get; set; }
        public string Source { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
