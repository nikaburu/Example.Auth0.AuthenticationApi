using System.Threading;
using System.Threading.Tasks;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;

namespace Example.Auth0.AuthenticationApi.Services
{
    public interface IUserService
    {
        Task<IPagedList<User>> GetUsersAsync(GetUsersRequest request, PaginationInfo paginationInfo,
            CancellationToken cancellationToken);
    }
}