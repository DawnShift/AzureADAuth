using AzureADAuth.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzureADAuth
{
    public partial class AzureAuth : IAzureAuth
    {
        private readonly AzureADconnectionSettings settings;
        public AzureAuth(AzureADconnectionSettings settings)
        {
            this.settings = settings;
        }

        public async Task<List<AzureUser>> GetAllAzureADUsers()
        {
            settings.AuthToken = settings.GetAuthToken();
            HttpClient client = new HttpClient();
            string uri = $"{settings.Resource}{settings.Tenant}/users?{settings.APIVersion}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", settings.AuthToken);
            var getResult = await client.GetAsync(uri);
            if (getResult.IsSuccessStatusCode)
            {
                return getResult.Content.ReadAsAsync<AzureUserList>().Result.UserList;
            }
            return new List<AzureUser>();
        }

        public async Task<List<AzureUser>> SearchAzureADUsers(string searchTerm)
        {
            settings.AuthToken = settings.GetAuthToken();
            HttpClient client = new HttpClient();
            string uri = $"{settings.Resource}{settings.Tenant}/users?$filter=startswith(displayName,'{searchTerm}')&{settings.APIVersion}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", settings.AuthToken);
            var getResult = await client.GetAsync(uri);
            if (getResult.IsSuccessStatusCode)
            {
                return getResult.Content.ReadAsAsync<AzureUserList>().Result.UserList;
            }
            return new List<AzureUser>();
        }

        public async Task<ADUserViewModel> ADUserAuthorization(LoginModel user)
        {
            ADUserViewModel model = new ADUserViewModel();
            HttpClient client = new HttpClient();
            string tokenEndpoint = settings.Authority + settings.OAuth;
            var body = $"resource={settings.ClientId}&client_id={settings.ClientId}&client_secret={settings.AppKey}&grant_type=password&username={user.Email}&password={user.Password}";
            var stringContent = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
            var result = await client.PostAsync(tokenEndpoint, stringContent);
            if (result.IsSuccessStatusCode)
            {
                model.IsSuccess = true;
                model.UserDetails = await result.Content.ReadAsAsync<ADUserLoginSuccess>();
            }
            else
            {
                model.IsSuccess = false;
                model.ErrorDetails = await result.Content.ReadAsAsync<ADUserLoginError>();
            }
            return model;
        }
    }
}
