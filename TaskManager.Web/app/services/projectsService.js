'use strict';
app.factory('projectsService', ['$http', '$q', function ($http, $q) {

    var serviceBase = 'http://localhost:26264/';
    var ordersServiceFactory = {};

    var getAllProjects = function () {

        return $http.get(serviceBase + 'api/projects').then(function (results) {
            return results;
        });
        //var deferred = $q.defer();
        //var data = [
        //    {
        //        Name: "Writing Code",
        //        Status: "In Progress",
        //        Manager: "Le Super Man",
        //        Members: [
        //        {
        //            Name: "Le Batman", Id: "0x14"
        //        },
        //        {
        //            Name: "Le Batman", Id: "0x14"
        //        },
        //        {
        //            Name: "Le Batman", Id: "0x14"
        //        },
        //        {
        //            Name: "Le Batman", Id: "0x14"
        //        }]
        //    }, {
        //        Name: "Writing Code",
        //        Status: "In Progress",
        //        Manager: "Le Super Man",
        //        Members: [
        //        {
        //            Name: "Le Batman", Id: "0x14"
        //        },
        //        {
        //            Name: "Le Batman", Id: "0x14"
        //        },
        //        {
        //            Name: "Le Batman", Id: "0x14"
        //        },
        //        {
        //            Name: "Le Batman", Id: "0x14"
        //        }]
        //    }, {
        //        Name: "Writing Code",
        //        Status: "In Progress",
        //        Manager: "Le Super Man",
        //        Members: [
        //        {
        //            Name: "Le Batman", Id: "0x14"
        //        },
        //        {
        //            Name: "Le Batman", Id: "0x14"
        //        },
        //        {
        //            Name: "Le Batman", Id: "0x14"
        //        },
        //        {
        //            Name: "Le Batman", Id: "0x14"
        //        }]
        //    }, {
        //        Name: "Writing Code",
        //        Status: "In Progress",
        //        Manager: "Le Super Man",
        //        Members: [
        //        {
        //            Name: "Le Batman", Id: "0x14"
        //        },
        //        {
        //            Name: "Le Batman", Id: "0x14"
        //        },
        //        {
        //            Name: "Le Batman", Id: "0x14"
        //        },
        //        {
        //            Name: "Le Batman", Id: "0x14"
        //        }]
        //    }
        //];
        //var results={data:data}
        //setTimeout((function () {
        //    setTimeout(function () {
        //        deferred.resolve(results);
        //    }, 1000);
        //}));
        //return deferred.promise;
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

    var addUserToProject = function () {

        return $http.get(serviceBase + 'api/projects').then(function (results) {
            return results;
        });
    };

    var getProjectRequirements = function () {

        return $http.get(serviceBase + 'api/projects').then(function (results) {
            return results;
        });
    };


    var getSkills = function () {
        return $http.get(serviceBase + 'api/Projects/Skills').then(function (results) {
            return results;
        });
    };

    var getProject = function () {
        return $http.get(serviceBase + 'api/Projects').then(function (results) {
            return results;
        });
    };

    var getMessages = function () {
        return $http.get(serviceBase + 'api/Messages').then(function (results) {
            return results;
        });
    };

    var getChatMessages = function (groupId) {
        return $http.get(serviceBase + 'api/Messages/Chat?groupId=' + groupId)
            .then(function (results) {
                return results;
            });
    };

    var sendChatMessage = function (userId, groupId, message) {
        return $http.get(serviceBase + 'api/Messages/Send?userId=' + userId + '&groupId=' + groupId
            + '&message=' + message)
            .then(function (results) {
                return results;
            });
    };






    ordersServiceFactory.getAllProjects = getAllProjects;
    ordersServiceFactory.getProjectRequirements = getProjectRequirements;
    ordersServiceFactory.getProject = getProject;
    ordersServiceFactory.addUserToProject = addUserToProject;
    ordersServiceFactory.updateProject = updateProject;
    ordersServiceFactory.deleteProject = deleteProject;
    ordersServiceFactory.addProject = addProject;
    ordersServiceFactory.getSkills = getSkills;
    ordersServiceFactory.getMessages = getMessages;
    ordersServiceFactory.sendChatMessage = sendChatMessage;
    ordersServiceFactory.getChatMessages = getChatMessages;

    return ordersServiceFactory;

}]);