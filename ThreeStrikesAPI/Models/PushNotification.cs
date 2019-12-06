using Newtonsoft.Json;

namespace ThreeStrikesAPI.Models
{
    public class PushNotification
    {
        [JsonProperty(PropertyName = "to")]
        public string To { get; set; }
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }
    }
}
