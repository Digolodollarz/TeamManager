using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AngularJSAuthentication.API.Repositories
{
    public class ProjectRepository
    {
        private ProjectContext ctx;
        //private String currentUserId;
        private Member CurrentMember;

        public ProjectRepository()
        {
            this.ctx = new ProjectContext();

            //var user = System.Web.HttpContext.Current.User.Identity;
            //this.currentUserId = user.GetUserId();
            //if (currentUserId == null)
            //    this.currentUserId = "diggie";
            this.CurrentMember = ctx.Members.FirstOrDefault(m => m.Name.Equals(System.Web.HttpContext.Current.User.Identity.Name));
        }


        #region Messages
        public List<ChatMessage> GetLatest()
        {
            return ctx.Projects
                .Where(p => p.Members.Select(m => m.IdentityUserId).Contains(CurrentMember.IdentityUserId))
                    .Select(p => p.ChatMessages.OrderByDescending(m => m.DateCreated).FirstOrDefault()).ToList();
        }

        public List<ChatMessage> GetGroupMessages(int groupId)
        {
            return
                ctx.Projects.FirstOrDefault(p => p.Id == groupId)?
                    .ChatMessages
                    .OrderByDescending(m => m.DateCreated)
                    .Take(50)
                    .ToList();
        }
        #endregion

        public List<Project> GetAllProjects()
        {
            return ctx.Projects.Take(50).ToList();
        }

        public List<Skill> GetAllSkills()
        {
            return ctx.Skills.Take(50).ToList();
        }

        public List<Project> GetProjectsByUserId(string userId)
        {
            var member = ctx.Members.FirstOrDefault(m => m.IdentityUserId.Equals(userId));
            return ctx.Projects.Where(p => p.Members.Contains(member)).ToList();
        }

        public int CreateProject(string projectName)
        {
            ctx.Projects.Add(new Project { Name = projectName });
            if (ctx.SaveChanges() > 0)
            {
                var project = ctx.Projects.FirstOrDefault(p => p.Name.Equals(projectName));
                if (project.ChatMessages == null)
                {
                    project.ChatMessages = new List<ChatMessage>();
                    ctx.SaveChanges();
                }
                SendMessage(project.Id, "Project Created " + projectName);
            }
            return 0;
        }

        public int RemoveUserFromProject(string memberId, int projectId)
        {
            var project = ctx.Projects.FirstOrDefault(p => p.Id == projectId);
            var member = ctx.Members.FirstOrDefault(m => m.IdentityUserId == memberId);
            if (project == null || member == null) return 0;
            project.Members.Remove(member);
            return ctx.SaveChanges();
        }

        public int AddUserToProject(string memberId, int projectId)
        {
            var project = ctx.Projects.FirstOrDefault(p => p.Id == projectId);
            var member = ctx.Members.FirstOrDefault(m => m.IdentityUserId == memberId);
            if (project == null || member == null) return 0;
            project.Members.Add(member);
            return ctx.SaveChanges();
        }

        public bool SetManager(String memberId, int projectId)
        {
            var project = ctx.Projects.FirstOrDefault(p => p.Id == projectId);
            var member = ctx.Members.FirstOrDefault(m => m.IdentityUserId == memberId);
            if (project == null) throw new ArgumentException("Invalid Project Id");
            project.ProjectManager = new ProjectManager
            {
                Member = member,
                Project = project
            };

            return ctx.SaveChanges() > 0;
        }

        public int SendMessage(int groupId, string message, string attachmentUrl = null)
        {
            var userId = CurrentMember.IdentityUserId;
            var project = ctx.Projects.FirstOrDefault(p=>p.Id==groupId);
            project.ChatMessages.Add(new ChatMessage
            {
                DateCreated = DateTime.UtcNow,
                Text = message,
                AttachmentUrl = attachmentUrl,
                MemberId = userId
            });
            //ctx.ChatMessages.Add(new ChatMessage
            //{
            //    MemberId = userId,
            //    ProjectId = groupId,
            //    Text = message,
            //    DateCreated = DateTime.Now
            //});
            return ctx.SaveChanges();
        }
    }

    public class ProjectContext : AuthContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Member> Members { get; set; }
        //public DbSet<ChatMessage> ChatMessages { get; set; }

    }

    public class Member
    {
        [Key]
        public string IdentityUserId { get; set; }
        public string Name { get; set; }
        [ForeignKey("IdentityUserId")]
        public virtual IdentityUser IdentityUser { get; set; }

        public virtual List<Skill> Skills { get; set; }
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

        public virtual Skill Skill { get; set; }
    }

    public class Task
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Skill> Skills { get; set; }

        //public List<Member> Members { get; set; }
    }

    public class ProjectFile
    {
        [Key]
        public int FileId { get; set; }
        public string Location { get; set; }
    }

    public class ProjectManager
    {
        [Key, ForeignKey("Project")]
        public int Id { get; set; }
        public virtual Member Member { get; set; }
        public virtual Project Project { get; set; }
    }

    public class Project
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        //public int ProjectManagerId { get; set; }
        public virtual List<Task> Tasks { get; set; }
        public virtual List<Member> Members { get; set; }
        public virtual List<ProjectFile> ProjectFiles { get; set; }
        public virtual List<ChatMessage> ChatMessages { get; set; }
        //[ForeignKey("ProjectManagerId")]
        public virtual ProjectManager ProjectManager { get; set; }

    }

    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public string AttachmentUrl { get; set; }

        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }


        //public int ProjectId { get; set; }


        public string MemberId { get; set; }

        //[ForeignKey("ProjectId")]
        //public virtual Project Project { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }
    }
}