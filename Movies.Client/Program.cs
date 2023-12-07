using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Net.Http.Headers;
using Movies.Client.APIServices;
using Movies.Client.HttpHandlers;

namespace Movies.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IMovieApiService, MovieApiService>();

            builder.Services.AddAuthentication(
                options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.Authority = "https://localhost:5005";

                        options.ClientId = "movies_mvc_client";
                        options.ClientSecret = "secret";
                        options.ResponseType = "code";

                        options.Scope.Add("openid");
                        options.Scope.Add("profile");

                        options.SaveTokens = true;

                        options.GetClaimsFromUserInfoEndpoint = true;
                    });

            builder.Services.AddTransient<AuthenticationDelegatingHandler>();

            builder.Services.AddHttpClient(
                "MovieAPIClient",
                client =>
                {
                    client.BaseAddress = new Uri("https://localhost:5001");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
                }).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

            builder.Services.AddHttpClient(
                "IDPClient",
                client =>
                {
                    client.BaseAddress = new Uri("https://localhost:5005");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
                });

            builder.Services.AddSingleton(new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:5005/connect/token",
                ClientId = "movieClient",
                ClientSecret = "secret",
                Scope = "movieAPI"
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}