using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Persistance;
using WebApplication1.Services.Users;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUsersService _usersService;
        public UsersController(ApplicationDbContext db, IUsersService usersService)
        {
            _db = db; 
            _usersService = usersService;
        }

        /* public async Task<IActionResult> ViewUser()
         {
             if (!ModelState.IsValid) { }
             var user = await _db.Users.FirstOrDefaultAsync();
             var vm = new ViewUserVM { Name = user.Name, Email = user.Email };
             return View(vm);

         }*/

        public async Task<IActionResult> ViewUsers()
        {
            var vm = await _usersService.GetUsers();
            return View(vm);
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(CreateUser), vm);
            }

            var user = new User() { Name = vm.Name, Email = vm.Email };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(ViewUsers));
        }

        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _db.Users.AsNoTracking().Where(u => u.Id == id).FirstOrDefaultAsync();

            if (user == null)
                return View(nameof(ViewError));

            var vm = new EditUserVM
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserVM vm)
        {
            if (vm.Name == vm.Email)
            {
                ModelState.AddModelError("name", "takie same");
            }

            if (!ModelState.IsValid) { }

            var user = await _db.Users.Where(u => u.Id == vm.Id).FirstOrDefaultAsync();

            if (user is null)
                return View(nameof(ViewError));

            user.Name = vm.Name;
            user.Email = vm.Email;

            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(ViewUsers));
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _db.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

            if (user is null) return View(nameof(ViewError));

            _db.Remove(user);
            await _db.SaveChangesAsync();

            return View();
        }

        public IActionResult ViewError()
        {
            return View();
        }
    }
}
