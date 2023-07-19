using Movies.Client.Models;

namespace Movies.Client.APIServices
{
    public class MovieApiService : IMovieApiService
    {
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            throw new NotImplementedException();
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