using System.Threading;
using System.Threading.Tasks;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Example.Auth0.AuthenticationApi.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Example.Auth0.AuthenticationApi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public IPagedList<User> Users { get; private set; }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            Users = await _userService.GetUsersAsync(new GetUsersRequest(), new PaginationInfo(), cancellationToken);
        }
    }
}
