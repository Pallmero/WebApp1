using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Persistance;
using WebApplication1.ViewModels;

namespace WebApplication1.Services.Users;

public class UsersService : IUsersService
{
    private readonly ApplicationDbContext _db;

    public UsersService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ViewUsersVM> GetUsers()
    {
        var users = await _db.Users.AsNoTracking().ToListAsync();

        var vm = new ViewUsersVM
        {
            Users = new List<User>()
        };

        foreach (var user in users)
            vm.Users.Add(new User { Id = user.Id, Name = user.Name, Email = user.Email });

        return vm;
    }

    public Task CreateUser(CreateUserVM vm)
    {
        throw new NotImplementedException();
    }
}
