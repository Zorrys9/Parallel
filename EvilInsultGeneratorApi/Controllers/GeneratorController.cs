using EvilInsultGeneratorApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EvilInsultGeneratorApi.Controllers
{
    [Route("Jokes")]
    [ApiController]
    public class GeneratorController : Controller
    {
        [HttpPost("{count}")]
        public JokeViewModel Get([FromRoute] int count)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            JokeViewModel viewModel = new JokeViewModel();
            ConnectToApi connectToApi = new ConnectToApi();
            viewModel.Jokes = new List<Joke>();
            
            for (var i = 0; i < count; i++)
            {
                var j = i;
                var joke = connectToApi.GetJoke();
                foreach (var jok in joke.JokeBody)
                {
                    jok.NumberIterator = j;
                    viewModel.Jokes.Add(jok);
                }
            }

            stopwatch.Stop();
            viewModel.Time = stopwatch.ElapsedMilliseconds.ToString();
            return viewModel;
        }

        [HttpPost("Parallel/{count}")]
        public JokeViewModel GetParallel([FromRoute] int count)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            JokeViewModel viewModel = new JokeViewModel();
            ConnectToApi connectToApi = new ConnectToApi();
            viewModel.Jokes = new List<Joke>();
            List<Action> actions = new List<Action>();
            for (var i = 0; i < count; i++)
            {
                var j = i;
                actions.Add(() =>
                {
                    var joke = connectToApi.GetJoke();
                    foreach (var jok in joke.JokeBody)
                    {
                        jok.NumberIterator = j; 
                        viewModel.Jokes.Add(jok);
                    }
                });
            }

            Parallel.Invoke(actions.ToArray());

            stopwatch.Stop();
            viewModel.Time = stopwatch.ElapsedMilliseconds.ToString();
            return viewModel;
        }

        [HttpPost("Async/{count}")]
        public async Task<JokeViewModel> GetAsync([FromRoute] int count)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            JokeViewModel viewModel = new JokeViewModel();
            ConnectToApi connectToApi = new ConnectToApi();
            viewModel.Jokes = new List<Joke>();
            ICollection<Task<JokeResponse>> jokes = new List<Task<JokeResponse>>();

            for (var i = 0; i < count; i++)
            {
                jokes.Add(connectToApi.GetJokeAsync());
            }
            foreach (var jok in jokes)
            {
                var result = await jok;
                foreach (var res in result.JokeBody)
                {
                    viewModel.Jokes.Add(res);
                }
            }

            stopwatch.Stop();
            viewModel.Time = stopwatch.ElapsedMilliseconds.ToString();
            return viewModel;
        }
    }
}
