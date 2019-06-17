using AzureADAuth.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureADAuth
{
    public interface IAzureAuth
    {
        Task<List<AzureUser>> GetAllAzureADUsers();
        Task<List<AzureUser>> SearchAzureADUsers(string searchTerm);
        Task<ADUserViewModel> ADUserAuthorization(LoginModel user);
    }
}
