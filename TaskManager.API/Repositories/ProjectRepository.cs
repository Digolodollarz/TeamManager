using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AngularJSAuthentication.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace AngularJSAuthentication.API.Repositories
{
    public class ProjectRepository
    {
        private ProjectContext ctx;
        private String currentUserId;

        public ProjectRepository()
        {
            this.ctx = new ProjectContext();
            this.currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }


        public List<Skill> GetProjectSkills()
        {
            return ctx.Projects.Find(1)?.Skills;
        }

        public void CreateProject(string projectName)
        {
            ctx.Projects.Add(new Project { Name = projectName });
            ctx.SaveChanges();
        }

        public void AddUserToProject(string userName, int projectId)
        {
            ctx.UserProjects.Add(new UserProject { UserId = userName, ProjectId = projectId });
            ctx.SaveChanges();
        }

        public void RemoveUserFromProject(string userId, int projectId)
        {
            ctx.UserProjects.Remove(new UserProject { UserId = userId, ProjectId = projectId });
            ctx.SaveChanges();
        }

        public List<Project> GetProjectsByUserId(string userId)
        {
            var userProjects = ctx.UserProjects.Where(p => p.UserId.Equals(userId)).ToList();
            var projects = new List<Project>();
            foreach (var userProject in userProjects)
            {
                projects.Add(ctx.Projects.FirstOrDefault(p => p.Id == userProject.ProjectId));
            }
            return projects;
        }

        public List<Project> GetAllProjects()
        {
            return ctx.Projects.Take(50)?.ToList();
        }


        public List<TeamMember> GetTeamMembers(int projectId)
        {
            var authRepo = new AuthRepository();
            var members = new List<TeamMember>();
            var userProjects = ctx.UserProjects.Where(p => p.ProjectId == projectId);
            foreach (var userProject in userProjects)
            {
                members.Add(new TeamMember
                {
                    Name = authRepo.FindUserById(userProject.UserId).UserName,
                    UserId = userProject.UserId,
                    Skills = ctx.UserSkills
                            .Where(s => s.UserId.Equals(userProject.UserId))
                            .Select(u => ctx.Skills.Find(u.SkillId))
                            .ToList()
                });
            }

            return members;
        }

        public bool SetManager(ProjectManager manager)
        {
            ctx.ProjectManagers.Add(manager);
            return ctx.SaveChanges() > 0;
        }

        public List<Skill> GetAllSkills()
        {
            return ctx.Skills.Take(50)?.ToList();
        }

        public List<ChatMessage> GetLatest()
        {
            List<Project> projects = GetProjectsByUserId(currentUserId);
            return ctx.ProjectMessages.Where(m => projects.Select(p => p.Id).Contains(m.GroupId)).ToList();
        }

        public int AddMessage(ChatMessage message)
        {
            ctx.ProjectMessages.Add(message);
            return ctx.SaveChanges();
        }

        public List<ChatMessage> GetGroupMessages(int groupId)
        {
            return ctx.ProjectMessages.Where(m => m.GroupId == groupId).ToList();
        }
    }

    public class ProjectContext : AuthContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskSkill> TaskSkills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<ProjectManager> ProjectManagers { get; set; }
        public DbSet<ChatMessage> ProjectMessages { get; set; }
    }

    public class Project
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Skill> Skills { get; set; }

    }

    public class TeamMember
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public List<Skill> Skills { get; set; }

    }


    public class ProjectManager
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string UserId { get; set; }
    }


    public class UserProject
    {
        [Key]
        public int Id { get; set; }
        //[ForeignKey("UserModel")]
        public String UserId { get; set; }
        //[ForeignKey("Project")]
        public int ProjectId { get; set; }
    }


    public class Skill
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserSkill
    {
        [Key]
        public int Id { get; set; }

        public int SkillId { get; set; }

        public string UserId { get; set; }
    }

    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        public string SenderId { get; set; }
        public int GroupId { get; set; }

        public string Text { get; set; }
    }


    public class TaskSkill
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Task")]
        public int TaskId { get; set; }
        [ForeignKey("Skill")]
        public int SkillId { get; set; }

        //  [JsonIgnore]
        public virtual Task Task { get; set; }
        // [JsonIgnore]
        public virtual Skill Skill { get; set; }

    }
}