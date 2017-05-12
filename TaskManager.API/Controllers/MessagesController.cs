using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngularJSAuthentication.API.Repositories;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/Messages")]
    public class MessagesController : ApiController
    {
        private ProjectRepository projectRepository;


        public MessagesController()
        {
            projectRepository = new ProjectRepository();
        }
        [Route("")]
        [HttpGet]
        public List<ChatMessage> Get()
        {
            return projectRepository.GetGroupMessages(2);
        }
    }
}
