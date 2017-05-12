using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngularJSAuthentication.API.Repositories;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/Projects")]
    public class ProjectController : ApiController
    {
        private ProjectRepository projectRepository;


        public ProjectController()
        {
            projectRepository = new ProjectRepository();
        }

        [Route("")]
        [HttpGet]
        public List<Project> Get()
        {
            return projectRepository.GetAllProjects();
        }

        [Route("Skills")]
        [HttpGet]
        public List<Skill> Skills()
        {
            return projectRepository.GetAllSkills();
        }

        [Route("UserProjects")]
        [HttpGet]
        public List<Project> GetUserProjects(string userId)
        {
            return projectRepository.GetProjectsByUserId(userId);
        }

        [Route("Create")]
        [HttpPost]
        public IHttpActionResult CreateProject(string projectName)
        {
            projectRepository.CreateProject(projectName);
            return Ok();
        }

        [Route("RemoveUser")]
        [HttpPost]
        public IHttpActionResult RemoveFromProject(string userName, int projectId)
        {
            projectRepository.RemoveUserFromProject(userName, projectId);
            return Ok();
        }

        [Route("AddUser")]
        [HttpPost]
        public IHttpActionResult AddToProject(string userName, int projectId)
        {
            projectRepository.AddUserToProject(userName, projectId);
            return Ok();
        }

        [Route("SetManager")]
        [HttpPost]
        public IHttpActionResult SetManager([FromBody] ProjectManager manager)
        {
            if (projectRepository.SetManager(manager)) return Ok();
            return BadRequest("Manager");
        }

    }
}
