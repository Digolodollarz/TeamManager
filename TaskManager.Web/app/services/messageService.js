'use strict';
app.factory('messageService', ['$http', function ($http) {

    //var serviceBase = 'http://localhost:26264/';
    var serviceBase = 'http://localhost:26264/';//
    var messageServiceFactory = {};

    var getMessages = function () {

        return $http.get(serviceBase + 'api/Messages').then(function (results) {
            return results;
        });
    };

    var sendMessages = function(data) {
        return $http.post(serviceBase + 'api/Messages/Send?groupId=' + data.groupId +
            "message="+data.message,
            { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
    }

    messageServiceFactory.getMessages = getMessages;
    messageServiceFactory.sendMessages = sendMessages;

    return messageServiceFactory;

}]);