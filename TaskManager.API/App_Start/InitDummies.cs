using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AngularJSAuthentication.API.Models;
using AngularJSAuthentication.API.Repositories;

namespace AngularJSAuthentication.API
{
    public static class InitDummies
    {

        public static void Initialise()
        {
            var projectRepository = new ProjectRepository();
            var userRepository = new AuthRepository();
            var projectsContext = new ProjectContext();


            #region Create Users

            for (int i = 1; i <= 50; i++)
            {
                var user = userRepository.CreateUser(new UserModel
                {
                    UserName = "User" + i,
                    Password = "#Pass123",
                    ConfirmPassword = "#Pass123"
                });

                if (user.Succeeded)
                {
                    Debug.WriteLine("created");
                }
                else
                {
                    foreach (var userError in user.Errors)
                    {
                        Debug.WriteLine(userError);
                    }
                }

            }
            #endregion

            userRepository.CreateRole("Admin");
            userRepository.CreateRole("Manager");
            userRepository.CreateRole("Superman");

            var randomiser = new Random();


            //for (int i = 1; i < 11; i++)
            //    projectRepository.CreateSkill("Skill " + i);


            //for (int i = 1; i < 11; i++)
            //{
            //    projectRepository.CreateProject("Project " + i);
            //}

            //for (int i = 1; i < 11; i++)
            //{
            //    var manager = userRepository.FindUserByUserName("User" + randomiser.Next(1, 50));
            //    var project = projectRepository.FindProjectByName("Project " + i);
            //    projectsContext.ProjectManagers.Add(new ProjectManager
            //    {
            //        ProjectId = project.Id,
            //        UserId = manager.Id
            //    });
            //    if(project.Skills==null)  project.Skills = new List<Skill>();
            //    for (int j = 0; j < 10; j++)
            //    {
            //        project.Skills.Add(projectsContext.Skills.Find(randomiser.Next(0,9)));
            //        projectsContext.ProjectMessages.Add(new ChatMessage
            //        {
            //            SenderId = manager.Id,
            //            GroupId = project.Id,
            //            Text = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8)
            //    });
            //    }
            //}



        }
    }
}