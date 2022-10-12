using Newtonsoft.Json;

namespace Session2._2_Homework
{

    public class PetModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("category")]
        public Category Category { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("photoUrls")]
        public string[] PhotoUrls { get; set; }
        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Tag
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
