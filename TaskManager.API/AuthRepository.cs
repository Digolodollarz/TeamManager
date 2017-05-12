using AngularJSAuthentication.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AngularJSAuthentication.API.Repositories;

namespace AngularJSAuthentication.API
{

    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;

        private UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);


            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public string GetUserId(string userName) => _userManager.FindByName(userName).Id;

        public bool AddToRole(string userName, string roleName)
        {
            var user = _userManager.FindByName(userName);
            if (_ctx.Roles.FirstOrDefault(role => role.Name == roleName) == null) return false;
            _userManager.AddToRole(user.Id, roleName);
            return true;
        }

        public void RemoveFromRole(string userName, string roleName)
        {
            var user = _userManager.FindByName(userName);
            if (user == null) throw new ArgumentException("No Such User", userName);
            if (_ctx.Roles.FirstOrDefault(role => role.Name == roleName) == null) throw new ArgumentException("No Such Role", roleName); ;
            _userManager.RemoveFromRole(user.Id, roleName);
        }

        public bool CreateRole(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_ctx));
            if (roleManager.RoleExists(roleName)) return true;
            var role = new IdentityRole { Name = roleName };
            roleManager.Create(role);
            return true;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }

        public List<IdentityUser> FindUsers()
        {
            var users = from usersPeeps in _ctx.Users
                        select usersPeeps;
            return users.ToList();
        }

        public IdentityUser FindUserById(string userProjectUserId)
        {
            return _ctx.Users.Find(userProjectUserId);
        }
    }
}