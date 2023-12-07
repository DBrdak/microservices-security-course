using Movies.Client.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace Movies.Client.APIServices
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public MovieApiService(IHttpClientFactory httpClientFactory/*, IHttpContextAccessor httpContextAccessor*/)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            //_httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "/api/Movies");

            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var movieList = JsonConvert.DeserializeObject<List<Movie>>(content);
            return movieList;
        }

        public async Task<Movie> GetMovie(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteMovie(int id)
        {
            throw new NotImplementedException();
        }
    }
}