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
    
        //var deferred = $q.defer();

        //$http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/json' } }).success(function (response) {

        //    deferred.resolve(response);

        //}).error(function (err, status) {
        //    deferred.reject(err);
        //});

        //return deferred.promise;

        //return $http.post(serviceBase + 'api/Projects/Create', data, { headers: { 'Content-Type': 'application/json' } });

        return $http({
            method: 'POST',
            url: serviceBase + 'api/Projects/Create',
            data: JSON.stringify(data)
        });

    };

    var deleteProject = function () {

        return $http.get(serviceBase + 'api/projects').then(function (results) {
            return results;
        });
    };

    var updateProject = function () {

        return $http.get(serviceBase + 'api/projects').then(function (results) {
            return results;
        });
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

    ordersServiceFactory.getAllProjects = getAllProjects;
    ordersServiceFactory.getProjectRequirements = getProjectRequirements;
    ordersServiceFactory.getProject = getProject;
    ordersServiceFactory.addUserToProject = addUserToProject;
    ordersServiceFactory.updateProject = updateProject;
    ordersServiceFactory.deleteProject = deleteProject;
    ordersServiceFactory.addProject = addProject;
    ordersServiceFactory.getSkills = getSkills;

    return ordersServiceFactory;

}]);