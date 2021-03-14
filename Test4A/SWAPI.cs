using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Test4A.Models;

namespace Test4A
{
    public interface ISWAPI
    {
        Task<List<FilmModel>> List();
        Task<FilmModel> Details(int episode_id);
    }
    public class SWAPI : ISWAPI
    {
        private readonly ILogger<SWAPI> _logger;
        private readonly HttpClient httpClient;

        public SWAPI(ILogger<SWAPI> logger, HttpClient client)
        {
            _logger = logger;
            httpClient = client;
            httpClient.Timeout = TimeSpan.FromSeconds(5);
            httpClient.BaseAddress = new Uri("https://swapi.dev/api/");
        }


        public async Task<List<FilmModel>> List()
        {
            var response = await httpClient.GetAsync("films/");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            var d = JsonDocument.Parse(content);
            var list = JsonSerializer.Deserialize<List<FilmModel>>(d.RootElement.GetProperty("results").GetRawText());
            int index = 1;
            list.ForEach(i => i.index = index++);
            return list;
        }
        public async Task<FilmModel> Details(int index)
        {
            var response = await httpClient.GetAsync($"films/{index}/");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            var film = JsonSerializer.Deserialize<FilmModel>(content);
            film.index = index;
            return film;
        }

    }
}
