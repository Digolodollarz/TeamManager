using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngularJSAuthentication.API.Repositories;
using Microsoft.AspNet.Identity;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/Projects")]
    public class ProjectController : ApiController
    {
        private ProjectRepository projectRepository;
        private ProjectContext projectContext;
        private string currentUserId;

        public ProjectController()
        {
            projectRepository = new ProjectRepository();
            projectContext = new ProjectContext();
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                currentUserId = projectContext.Members.FirstOrDefault(m=>m.Name.Equals(System.Web.HttpContext.Current.User.Identity.Name))?.IdentityUserId;
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
        public IHttpActionResult AddToProject(string memberId, int projectId)
        {
            projectRepository.AddUserToProject(memberId, projectId);
            return Ok();
        }

        [Route("AddTask")]
        [HttpPost]
        public IHttpActionResult AddTaskToProject(int projectId, string taskName)
        {
            var project = projectContext.Projects.FirstOrDefault(p => p.Id == projectId);
            //var task = projectContext.
            if (project == null) return BadRequest("project");
            project.Tasks.Add(new Task { Name = taskName });
            if(projectContext.SaveChanges()>0)
            return Ok();
            return BadRequest("ProjectId");
        }

        [Route("Tasks")]
        [HttpGet]
        public List<Task> GetProjectRequirements(int projectId)
        {
            return projectContext.Projects.FirstOrDefault(p => p.Id == projectId)?.Tasks;
        }

        [Route("AddTaskSkill")]
        [HttpPost]
        public IHttpActionResult AddTaskSkill(int projectId, int taskId, int skillId)
        {
            var project = projectContext.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project == null) return BadRequest("project");
            var task = project.Tasks.FirstOrDefault(t => t.Id == taskId);
            var skill = projectContext.Skills.FirstOrDefault(s => s.Id == skillId);
            task.Skills.Add(skill);
            projectContext.SaveChanges();
            return Ok();
        }


        [Route("AddUserSkill")]
        [HttpPost]
        public IHttpActionResult AddUserSkill(string userId, int skillId)
        {
            var user = projectContext.Members.FirstOrDefault(m => m.IdentityUserId.Equals(userId));
            if (user == null) return BadRequest("User");
            var skill = projectContext.Skills.FirstOrDefault(s => s.Id == skillId);
            user.Skills.Add(skill);
            projectContext.SaveChanges();
            return Ok();
        }

        [Route("CreateSkill")]
        [HttpPost]
        public IHttpActionResult CreateSkill(string skillName)
        {
            projectContext.Skills.Add(new Skill { Name = skillName });
            //var task = projectContext.
            if (projectContext.SaveChanges() > 0)
                return Ok();
            return InternalServerError();
        }

        [Route("ProjectSkills")]
        [HttpGet]
        public List<Skill> GetProjectSkills(int projectId)
        {
            List<Skill> projectSkills = new List<Skill>();
            var pskill = projectContext.Projects.FirstOrDefault(p => p.Id == projectId)?.Tasks;
            foreach (var task in pskill)
            {
                foreach (var taskSkill in task.Skills)
                {
                    projectSkills.Add(taskSkill);
                }
            }
            return projectSkills;
        }


        [Route("GetEleigibleUsers")]
        [HttpGet]
        public List<TempMember> GetEleigibleUsers(int projectId)
        {
            var allMembers = projectContext.Members;
            Project project = projectContext.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project == null) return null;
            List<Skill> projectSkills = GetProjectSkills(projectId);
            List<Task> projectTasks = project.Tasks;

            List<TempMember> projecTempMembers = new List<TempMember>(); 
            foreach (var projectTask in projectTasks)
            {
                var taskTempMembers = new List<TempMember>();
                foreach (var projectTaskSkill in projectTask.Skills)
                {
                    var members = allMembers.Where(m => m.Skills.Select(s=>s.Id).Contains(projectTaskSkill.Id)).ToList();
                    foreach (var tempMember in members)
                    {
                        var member = taskTempMembers
                            .FirstOrDefault(m => m.IdentityUserId == tempMember.IdentityUserId);
                        if (member != null)
                        {
                            member.Strength++;
                        }
                        else
                        {
                            taskTempMembers.Add(new TempMember
                            {
                                IdentityUserId = tempMember.IdentityUserId,
                                SkillName = projectTask.Name,
                                Strength = 1,
                                Name = allMembers.Find(tempMember.IdentityUserId).Name
                            });
                        }
                    }
                }
                foreach (var tempMember in taskTempMembers.Take(2).OrderByDescending(m=>m.Strength))
                {
                    projecTempMembers.Add(tempMember);
                }
            }

            return projecTempMembers;
        }

        [Route("Project")]
        [HttpGet]
        public Project GetProject(int projectId)
        {
            return projectContext.Projects.FirstOrDefault(p => p.Id == projectId);
        }


        [Route("User")]
        [HttpGet]
        public Member GetUser(string memberId)
        {
            return projectContext.Members.FirstOrDefault(p => p.IdentityUserId.Equals(memberId));
        }


        [Route("Me")]
        [HttpGet]
        public Member GetMe()
        {
            return projectContext.Members.FirstOrDefault(p => p.IdentityUserId.Equals(
                System.Web.HttpContext.Current.User.Identity.Name
                ));
        }


        [Route("Users")]
        [HttpGet]
        public List<Member> GetAllUsers()
        {
            return projectContext.Members.Take(50).ToList();
        }

        [Route("SetManager")]
        [HttpPost]
        public IHttpActionResult SetManager(string managerId, int projectId)
        {
            if (projectRepository.SetManager(managerId, projectId)) return Ok();
            return BadRequest("Manager");
        }

    }

    public class TempMember
    {
        public string IdentityUserId { get; set; }
        public int Strength { get; set; }
        public string SkillName { get; set; }
        public string Name { get; set; }
    }
}
