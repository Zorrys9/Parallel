using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvilInsultGeneratorApi.Models
{
    public class JokeViewModel
    {
        public ICollection<Joke> Jokes { get; set; }
        public string Time { get; set; }
    }
}
