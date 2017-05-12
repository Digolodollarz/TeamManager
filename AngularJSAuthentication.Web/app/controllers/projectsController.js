'use strict';
app.controller('projectsController', ['$scope', 'projectsService', function ($scope, projectsService) {

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

    $scope.addProject = function() {
        console.log($scope.newProject);
        projectsService.addProject($scope.newProject).then(function(response) {
                console.log(response);
            },
            function(error) {
                console.log("Error", error);
            });
    }

}]);