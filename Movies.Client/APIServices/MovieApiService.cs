using Movies.Client.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace Movies.Client.APIServices
{
    public class MovieApiService : IMovieApiService
    {

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var movies = new List<Movie>()
            {
                new Movie()
                {
                    Id = 1,
                    Title = "Pulp Fiction",
                    Genre = "Crime, Drama",
                    Rating = "8.9",
                    ImageUrl = "images/src",
                    ReleaseDate = DateTime.UtcNow,
                    Owner = "admin"
                }
            };

            return await Task.FromResult(movies);
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