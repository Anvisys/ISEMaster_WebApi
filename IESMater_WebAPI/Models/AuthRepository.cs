using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace IESMater_WebAPI.Models
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

         //Method will help to Register User
	        public async Task<IdentityResult> RegisterUser(UserModel userModel)
	        {
	            IdentityUser user = new IdentityUser
	            {
	                UserName = userModel.UserName
	            };
	
	            var result = await _userManager.CreateAsync(user, userModel.Password);
	
	            return result;
	        }
	          //Method used to Get User details will be used by
	          //Authentication Provider class in Steps below
	        public async Task<IdentityUser> FindUser(string userName, string password)
	        {
	            IdentityUser user = await _userManager.FindAsync(userName, password);
	
	            return user;
            }


        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        {
            IdentityUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        //public async Task<IdentityRole> FindClient(String ClientID)
        //{
        //    IdentityRole user = await _userManager.FindByIdAsync(ClientID);

        //    return user;
        //}

        public async Task<IdentityResult> CreateAsync(IdentityUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }



        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }
    }
}