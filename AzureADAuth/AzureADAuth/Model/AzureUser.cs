using Newtonsoft.Json;
using System.Collections.Generic;

namespace AzureADAuth.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AzureUser
    {
        [JsonProperty(PropertyName = "objectId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "givenName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "surname")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "sipProxyAddress")]
        public string Email { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class AzureUserList
    {
        [JsonProperty(PropertyName = "value")]
        public List<AzureUser> UserList { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class ADUserLoginError
    {

        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public string Description { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class ADUserLoginSuccess
    {
        [JsonProperty(PropertyName = "access_token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string Type { get; set; }
    }

    public class ADUserViewModel
    {
        public bool IsSuccess { get; set; }

        public ADUserLoginSuccess UserDetails { get; set; }

        public ADUserLoginError ErrorDetails { get; set; }

    }
}
