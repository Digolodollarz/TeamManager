
var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/orders", {
        controller: "ordersController",
        templateUrl: "/app/views/orders.html"
    });

    $routeProvider.when("/projects", {
        controller: "projectsController",
        templateUrl: "/app/views/projects/projects.html"
    });

    $routeProvider.when("/viewProject/:projectId", {
        controller: "projectsController",
        templateUrl: "/app/views/projects/project.html" //TODO: resolve this shit
    });

    $routeProvider.when("/allocateProject", {
        controller: "projectsController",
        templateUrl: "/app/views/projects/allocate-project.html"
    });

    $routeProvider.when("/createProject", {
        controller: "projectsController",
        templateUrl: "/app/views/projects/create-project.html"
    });

    $routeProvider.when("/admin/users", {
        controller: "userController",
        templateUrl: "/app/views/users/all-users.html"
    });

    $routeProvider.when("/admin/users/create", {
        controller: "userController",
        templateUrl: "/app/views/users/create-user.html"
    });

    $routeProvider.when("/admin/roles", {
        controller: "projectsController",
        templateUrl: "/app/views/users/create-role.html"
    });

    $routeProvider.when("/admin/skills", {
        controller: "userController",
        templateUrl: "/app/views/users/skills.html"
    });

    $routeProvider.when("/messages", {
        controller: "projectsController",
        templateUrl: "/app/views/messages.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });

});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


