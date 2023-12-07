using IdentityModel.Client;

namespace Movies.Client.HttpHandlers
{
    public class AuthenticationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ClientCredentialsTokenRequest _credentials;

        public AuthenticationDelegatingHandler(IHttpClientFactory httpClientFactory, ClientCredentialsTokenRequest credentials)
        {
            _httpClientFactory = httpClientFactory;
            _credentials = credentials;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("IDPClient");

            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(_credentials);

            if (tokenResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong while requesting the access token");
            }

            request.SetBearerToken(tokenResponse.AccessToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
