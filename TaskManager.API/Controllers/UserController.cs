using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngularJSAuthentication.API.Repositories;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("admin/Users")]
    public class UserController : ApiController
    {
        private AuthRepository userRepository;

        public UserController()
        {
            userRepository = new AuthRepository();
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(userRepository.FindUsers());
        }

        [Route("AddToRole/{userId}/{roleName}/")]
        [HttpPost]
        public IHttpActionResult AddUserToRole(string userId, string roleName)
        {
            if (userRepository.AddToRole(userId, roleName))
                return Ok();
            return BadRequest();
        }


    }
}
