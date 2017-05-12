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

        [Route("Chat")]
        [HttpGet]
        public List<ChatMessage> GetForGroup(int groupId)
        {
            return projectRepository.GetGroupMessages(groupId);
        }

        [Route("Latest")]
        [HttpGet]
        public List<ChatMessage> GetLatest()
        {
            return projectRepository.GetLatest();
        }

        [Route("Send")]
        [HttpPost]
        public IHttpActionResult Send(string userId, int groupId, string message)
        {
            if (projectRepository.AddMessage(new ChatMessage
            {
                SenderId = userId,
                GroupId = groupId,
                Text = message
            }) > 0) return Ok();
            return BadRequest();
        }
    }
}
