using WebApplication1.ViewModels;

namespace WebApplication1.Services.Users;

public interface IUsersService
{
    Task<ViewUsersVM> GetUsers();
    Task CreateUser(CreateUserVM vm);
}
