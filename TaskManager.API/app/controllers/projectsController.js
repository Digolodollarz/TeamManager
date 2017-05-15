'use strict';
app.controller('projectsController', ['$scope', 'projectsService', '$routeParams', 'authService',
    function ($scope, projectsService, $routeParams, authService) {

        $scope.logOut = function () {
            authService.logOut();
            $location.path('/home');
        }

        $scope.authentication = authService.authentication;

        $scope.projects = [];
        $scope.skills = [];
        $scope.project = {};
        $scope.projectId = $routeParams.projectId;
        $scope.managedUserId = $routeParams.managedUserId;

        console.log("route paramas", $routeParams);
        projectsService.getProject($routeParams.projectId).then(function (response) {
            $scope.project = response.data;
            console.log("project", $scope.project);
        }, function (error) {
            console.log("Project Error", error);
        });

        if ($scope.managedUserId) {
            projectsService.getUser($scope.managedUserId).then(function (response) {
                $scope.managedUser = response.data;
                console.log("project", $scope.managedUser);
            }, function (error) {
                console.log("managedUser Error", error);
            });
        }

        projectsService.getAllProjects().then(function (results) {

            $scope.projects = results.data;
            $scope.allocationProjects = results.data;
            console.log("projects: ", $scope.projects);
        }, function (error) {
            //alert(error.data.message);
        });

        projectsService.getSkills().then(function (results) {
            $scope.skills = results.data;
            console.log("Skills", results);
        }, function (error) {
            //alert(error.data.message);
        });

        if($scope.project)
            projectsService.getChatMessages($scope.project.id).then(function (results) {
            $scope.chatMessages = results.data;
            console.log("Chat messages", $scope.chatMessages);
        }, function (error) {
            console.log(error.data);
        });

        projectsService.getMessages().then(function (response) {
            $scope.messages = response.data;
            console.log("messages", $scope.messages);
        }, function (error) {
            console.log(error.data);
        });

        projectsService.getAllUsers().then(function (response) {
            console.log("Users", response);
            $scope.users = response.data;
        }, function (error) {
            console.log("Users Error", error);
        });

        $scope.addProject = function () {
            console.log($scope.newProject);
            projectsService.addProject($scope.newProject).then(function (response) {
                console.log(response);
            },
                function (error) {
                    console.log("Error", error);
                });
        }

        $scope.showChat = function (message) {
            projectsService.getMessages(message.projectId).then(function (results) {
                $scope.chatMessages = results.data;
                console.log("chatMessages", $scope.chatMessages);
            }, function (error) {
                console.log("Error", error);
            });
        }

        $scope.sendMessage = function (groupId, message) {
            projectsService.sendChatMessage(groupId, message)
                .then(function (results) {
                $scope.messages = results.data;
                console.log("Message semt", $scope.messages);
            }, function (error) {
                console.log("Error", error);
            });
        }

        $scope.addUserSkill = function (userId, skillId) {
            projectsService.addUserSkill(userId, skillId).then(function (response) {
                console.log("Add User result", response);
            }, function (error) {
                console.log("Add user Error", error);
            });
        }

        $scope.addUserToProject = function (projectId, userId) {
            projectsService.addUserToProject(projectId, userId).then(function (response) {
                console.log("Add User result", response);
            }, function (error) {
                console.log("Add user Error", error);
            });
        }

        $scope.addProjectSkill = function (userId, skillId) {
            projectsService.addUserSkill(userId, skillId).then(function (result) {
                console.log(result);
            }, function (error) {
                console.log(" Add project Error", error);
            });
        }

        $scope.addTaskSkill = function (projectId, taskId, skillId) {
            console.log("Adding task skiri", projectId, taskId, skillId)
            projectsService.addTaskSkill(projectId, taskId, skillId).then(function (result) {
                console.log("Add Task Skiri", result);
            }, function (error) {
                console.log("Error Task Skiri", error);
            });
        }

        $scope.createSkill = function (skill) {
            console.log("Create Skill", skill);
            projectsService.addSkill($scope.newSkill.skillName).then(function () {
                projectsService.getSkills().then(function (results) {
                    $scope.skills = results.data;
                    console.log("Skills", results);
                }, function (error) {
                    console.log("Error", error);
                });
            });
        }

        $scope.assignProject = function (project) {
            console.log("Asigning project", project);
            $scope.allocationProject = project;
            projectsService.getEligibleUsers(project.id).then(function (result) {
                $scope.eligibleUsers = result.data;
                console.log("Eligble Users", $scope.eligibleUsers);
            },
                function (error) { console.log(error); });
        }

        $scope.addTaskToProject = function (projectId, taskName) {
            console.log("Creating Task", projectId, taskName);

            projectsService.addTaskToProject(projectId, taskName).then(function (result) {

                projectsService.getProject(projectId).then(function (result) {
                    $scope.allocationProject = result.data;
                    $scope.project = result.data;
                    console.log("allocation project", $scope.allocationProject);
                }, function (error) { console.log(error); });

                projectsService.getEligibleUsers(projectId).then(function (result) {
                    $scope.eligibleUsers = result.data;
                    console.log("Eligible Users", $scope.eligibleUsers);
                }, function (error) { console.log(error); });
            }, function (error) { console.log(error); });
        }

    }]);