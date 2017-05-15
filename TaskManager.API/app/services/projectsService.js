﻿'use strict';
app.factory('projectsService', ['$http', '$q', function ($http, $q) {

    var serviceBase = 'http://localhost:26264/';
    var ordersServiceFactory = {};

    var getAllProjects = function () {

        return $http.get(serviceBase + 'api/projects').then(function (results) {
            return results;
        });
    };

    var addProject = function (data) {
        return $http.post(serviceBase + 'api/Projects/Create?projectName=' + data.projectName, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
    };

    var deleteProject = function () {
        return $http.post(serviceBase + 'api/Projects/Create?projectName=' + data.projectName, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
    };

    var updateProject = function () {
        return $http.post(serviceBase + 'api/Projects/Create?projectName=' + data.projectName, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
    };

    var addUserToProject = function (projectId, memberId) {
        return $http.post(serviceBase + 'api/Projects/AddUser?projectId=' + projectId + '&memberId=' + memberId,
            { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
    };

    var addTaskToProject = function (projectId, taskName) {
        return $http.post(serviceBase + 'api/Projects/AddTask?projectId=' + projectId + '&taskName=' + taskName,
            { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
    };

    var getProjectRequirements = function (projectId) {
        return $http.get(serviceBase + 'api/Projects/Tasks&projectId='+projectId);
    };


    var getSkills = function () {
        return $http.get(serviceBase + 'api/Projects/Skills').then(function (results) {
            return results;
        });
    };

    var addSkill = function (skillName) {
        return $http.post(serviceBase + 'api/Projects/CreateSkill?skillName=' + skillName).then(function (results) {
            return results;
        });
    };

    ordersServiceFactory.addUserSkill = function (userId, skillId) {
        return $http.post(serviceBase + 'api/Projects/AddUserSkill?userId=' + userId + '&skillId=' + skillId)
            .then(function (results) {
                return results;
            });
    };

    var addTaskSkill = function (projectId, taskId, skillId) {
        return $http.post(serviceBase + 'api/Projects/AddTaskSkill?projectId=' + projectId
            + '&taskId=' + taskId + '&skillId=' + skillId)
            .then(function (results) {
                return results;
            });
    };

    var addTask= function (projectId, skillId) {
        return $http.post(serviceBase + 'api/Projects/AddTask?taskId=' + projectId + '&skillId=' + skillId)
            .then(function (results) {
                return results;
            });
    };

    var getProjectSkills = function (projectId) {
        return $http.get(serviceBase + 'api/Projects/ProjectSkills?projectId=' + projectId).then(function (results) {
            return results;
        });
    };

    var getEligibleUsers = function (projectId) {
        return $http.get(serviceBase + 'api/Projects/GetEleigibleUsers?projectId=' + projectId);
    };

    var getProject = function (projectId) {
        return $http.get(serviceBase + 'api/Projects/Project?projectId=' + projectId).then(function (results) {
            return results;
        });
    };

    var getMessages = function () {
        return $http.get(serviceBase + 'api/Messages/Latest').then(function (results) {
            return results;
        });
    };

    ordersServiceFactory.getUser = function (userId) {
        return $http.get(serviceBase + 'api/Projects/User?memberId=' + userId).then(function (results) {
            return results;
        });
    };
    ordersServiceFactory.getMe = function (userId) {
        return $http.get(serviceBase + 'api/Projects/Me').then(function (results) {
            return results;
        });
    };
    ordersServiceFactory.getAllUsers = function (userId) {
        return $http.get(serviceBase + 'api/Projects/Users');
    };

    var getChatMessages = function (projectId) {
        return $http.get(serviceBase + 'api/Messages/Chat?projectId=' + projectId)
            .then(function (results) {
                return results;
            });
    };

    var sendChatMessage = function (projectId, message) {
        return $http.post(serviceBase + 'api/Messages/Send?projectId=' + projectId
            + '&message=' + message)
            .then(function (results) {
                return results;
            });
    };



    


    ordersServiceFactory.getAllProjects = getAllProjects;
    ordersServiceFactory.getProjectRequirements = getProjectRequirements;
    ordersServiceFactory.getProject = getProject;
    ordersServiceFactory.getEligibleUsers = getEligibleUsers;
    ordersServiceFactory.addUserToProject = addUserToProject;
    ordersServiceFactory.updateProject = updateProject;
    ordersServiceFactory.deleteProject = deleteProject;
    ordersServiceFactory.addProject = addProject;
    ordersServiceFactory.getSkills = getSkills; 
    ordersServiceFactory.addSkill = addSkill;
    ordersServiceFactory.getProjectSkills = getProjectSkills;
    ordersServiceFactory.getMessages = getMessages;
    ordersServiceFactory.sendChatMessage = sendChatMessage;
    ordersServiceFactory.getChatMessages = getChatMessages;
    ordersServiceFactory.addTaskSkill = addTaskSkill;
    ordersServiceFactory.addTask= addTask;
    ordersServiceFactory.addTaskToProject = addTaskToProject;

    return ordersServiceFactory;

}]);