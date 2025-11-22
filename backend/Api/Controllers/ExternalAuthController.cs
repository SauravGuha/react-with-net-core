

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Api.Models;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Api.Controllers
{
    public class ExternalAuthController : BaseController
    {
        private string githubclientId;
        private string githubsecretId;
        private string applicationUrl;
        private HttpClient client;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ExternalAuthController(IConfiguration configuration, IHttpClientFactory httpClientFactory,
        UserManager<User> userManager, SignInManager<User> signInManager,
        IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
            this.githubclientId = configuration.GetSection("Authentication:Github_ClientId")?.Value!;
            this.githubsecretId = configuration.GetSection("Authentication:Github_ClientSecret")?.Value!;
            this.applicationUrl = configuration.GetSection("ApplicationUrl")?.Value!;
            if (string.IsNullOrWhiteSpace(githubclientId)
            || string.IsNullOrWhiteSpace(githubsecretId)
            || string.IsNullOrWhiteSpace(applicationUrl))
                throw new ArgumentNullException("Github_ClientId or Github_ClientSecret or applicationUrl is empty");

            this.client = httpClientFactory.CreateClient();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn()
        {
            //currently only for github
            var authorizeUrl = $"https://github.com/login/oauth/authorize?client_id={githubclientId}&redirect_uri={Uri.EscapeDataString(applicationUrl + "/api/externalauth/callback")}&scopes=read:user%20user:email";
            return Redirect(authorizeUrl);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CallBack([FromQuery] string code)
        {
            var accessTokenUrl = "https://github.com/login/oauth/access_token";
            var body = new FormUrlEncodedContent(new[]{
              new KeyValuePair<string,string>("client_id", githubclientId),
              new KeyValuePair<string,string>("client_secret", githubsecretId),
              new KeyValuePair<string,string>("code", code),
              new KeyValuePair<string,string>("redirect_uri", applicationUrl+"/api/externalauth/callback")
            });
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Reactivity/1.0");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var accessTokenResponse = await client.PostAsync(accessTokenUrl, body);
            if (!accessTokenResponse.IsSuccessStatusCode)
                return Redirect("notfound");
            else
            {
                var authentcationResponse = await accessTokenResponse.Content.ReadFromJsonAsync<GithubAuthenticationResponse>();
                if (authentcationResponse == null)
                    return Redirect("notfound");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentcationResponse.Access_Token);
                var userProfileResponse = await client.GetAsync("https://api.github.com/user/emails");
                if (!userProfileResponse.IsSuccessStatusCode)
                    return Redirect("notfound");
                var userProfileResult = await userProfileResponse.Content.ReadFromJsonAsync<List<GithubUser>>();
                if (userProfileResult == null
                || (userProfileResult != null && !userProfileResult.Any()))
                    return Redirect("notfound");

                var user = await userManager.FindByEmailAsync(userProfileResult!.FirstOrDefault()!.Email);
                if (user == null)
                {
                    user = new User
                    {
                        UserName = userProfileResult!.FirstOrDefault()!.Email,
                        Email = userProfileResult!.FirstOrDefault()!.Email,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(user);
                }

                await signInManager.SignInAsync(user, false);
            }
            if (webHostEnvironment.IsDevelopment())
                return Redirect("http://localhost:5000");

            return Redirect("/activities");
        }
    }
}