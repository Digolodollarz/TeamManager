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
        //[Route("")]
        //[HttpGet]
        //public List<ChatMessage> Get()
        //{
        //    return projectRepository.GetGroupMessages(2);
        //}

        [Route("Chat")]
        [HttpGet]
        public List<ChatMessage> GetForGroup(int projectId)
        {
            return projectRepository.GetGroupMessages(projectId);
        }

        [Route("Latest")]
        [HttpGet]
        public List<ChatMessage> GetLatest()
        {
            return projectRepository.GetLatest();
        }

        [Route("Send")]
        [HttpPost]
        public IHttpActionResult Send(int projectId, string message)
        {
            projectRepository.SendMessage(projectId, message);
            return BadRequest();
        }
    }
}
