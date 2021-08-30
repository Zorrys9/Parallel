using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvilInsultGeneratorApi.Models
{

    public class JokeResponse
    {
        [JsonProperty("attachments")]
        public IList<Joke> JokeBody { get; set; }
    }

    public class Joke
    {

        [JsonProperty("text")]
        public string Content { get; set; }
        public int NumberIterator { get; set; }
    }
}
