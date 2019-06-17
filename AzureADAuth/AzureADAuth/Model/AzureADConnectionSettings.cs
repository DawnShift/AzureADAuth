using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace AzureADAuth.Model
{
    public class AzureADconnectionSettings
    {
        private const string addInstance = "https://login.microsoftonline.com/{0}";

        private const string resource = "https://graph.windows.net/";

        private const string oAuth = "/oauth2/token";

        private const string apiversion = "api-version=1.6";

        public string ClientId { get; set; }

        public string Tenant { get; set; }

        public string AppKey { get; set; }

        public string Authority { get { return String.Format(CultureInfo.InvariantCulture, addInstance, this.Tenant); } }

        public string AuthToken { get; set; }

        public string Resource { get { return resource; } }

        public string APIVersion { get { return apiversion; } }

        public string OAuth { get { return oAuth; } }

        public async Task<string> ConnectToServerAsync()
        {
            AuthenticationContext context = null;
            ClientCredential credentials = null;
            context = new AuthenticationContext(this.Authority);
            credentials = new ClientCredential(this.ClientId, this.AppKey);
            AuthenticationResult result = null;
            result = await context.AcquireTokenAsync(resource, credentials);
            return result.AccessToken;
        }

        public string GetAuthToken()
        {
            Task<string> authToken = ConnectToServerAsync();
            authToken.Wait();
            return authToken.Result;
        }
    }
}
