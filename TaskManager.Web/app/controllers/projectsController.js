'use strict';
app.controller('projectsController', ['$scope', 'projectsService', '$rootScope', function ($scope, projectsService, $rootScope) {

    $scope.projects = [];
    $scope.skills = [];

    projectsService.getAllProjects().then(function (results) {

        $scope.projects = results.data;
        console.log(results);
        console.log($scope.projects);
    }, function (error) {
        //alert(error.data.message);
    });

    projectsService.getSkills().then(function (results) {
        $scope.skills = results.data;
        console.log(results);
    }, function (error) {
        //alert(error.data.message);
    });

    projectsService.getMessages().then(function (results) {
        $scope.messages = results.data;
        console.log($scope.messages);
    }, function (error) {
        //alert(error.data.message);
    });

    $scope.addProject = function() {
        console.log($scope.newProject);
        projectsService.addProject($scope.newProject).then(function(response) {
                console.log(response);
            },
            function(error) {
                console.log("Error", error);
            });
    }

    $scope.showChat = function(message) {
        projectsService.getMessages(message.groupId).then(function (results) {
            $scope.messages = results.data;
            console.log($scope.messages);
        }, function (error) {
            //alert(error.data.message);
        });
    }

    $scope.sendMessage = function (groupId, message) {
        projectsService.sendChatMessage("userme", groupId, message).then(function (results) {
            $scope.messages = results.data;
            console.log($scope.messages);
        }, function (error) {
            //alert(error.data.message);
        });
    }


    $scope.assignProject = function(project) {
        console.log("User Asigned", project);

    }
    
}]);