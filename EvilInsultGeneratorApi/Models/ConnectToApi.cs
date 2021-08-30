using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static EvilInsultGeneratorApi.Controllers.GeneratorController;

namespace EvilInsultGeneratorApi.Models
{
    public class ConnectToApi
    {
     
        public JokeResponse GetJoke()
        {
            HttpClient client = new HttpClient();
            string httpPath = $"https://icanhazdadjoke.com/slack";

            var result = client.GetStringAsync(httpPath).Result;
            var joke = JsonConvert.DeserializeObject<JokeResponse>(result);
            return joke;
        }

        public async Task<JokeResponse> GetJokeAsync()
        {
            HttpClient client = new HttpClient();
            string httpPath = $"https://icanhazdadjoke.com/slack";

            var result = await client.GetStringAsync(httpPath);
            var joke = JsonConvert.DeserializeObject<JokeResponse>(result);
            return joke;
        }

    }
}
