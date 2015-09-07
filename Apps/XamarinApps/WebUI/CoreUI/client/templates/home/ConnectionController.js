///<reference path="..\..\services\OperationService.ts"/>
/// <reference path="../../../typings/angularjs/angular.d.ts" />
/// <reference path="../../services/ConnectionService.ts"/>
var application;
(function (application) {
    var ConnectionController = (function () {
        function ConnectionController($scope, connectionService, operationService) {
            this.operationService = operationService;
            this.hosts = [];
            this.connections = [];
            $scope.vm = this;
            this.currentHost = this.hosts[2];
            var me = this;
            connectionService.getConnectionPrefillData().then(function (result) {
                var data = result.data;
                me.email = data.email;
                me.hosts = data.hosts;
            });
            connectionService.getConnectionData().then(function (result) {
                var data = result.data;
                me.connections = data.connections;
            });
        }
        ConnectionController.prototype.hasConnections = function () {
            return this.connections.length > 0;
        };
        ConnectionController.prototype.isCreateFirstConnectionMode = function () {
            return !this.hasConnections();
        };
        ConnectionController.prototype.isManageConnectionsMode = function () {
            return this.hasConnections();
        };
        ConnectionController.prototype.CreateConnection = function () {
            this.operationService.executeOperation("TheBall.LocalApp.CreateConnection", {
                "host": this.currentHost.hostname,
                "email": this.email
            });
        };
        ConnectionController.prototype.DeleteConnection = function (connectionID) {
            this.operationService.executeOperation("TheBall.LocalApp.DeleteConnection", { "connectionID": connectionID });
        };
        ConnectionController.$inject = ['$scope'];
        return ConnectionController;
    })();
    window.appModule.controller("ConnectionController", ["$scope", "ConnectionService", "OperationService", function ($scope, connectionService, operationService) { return new ConnectionController($scope, connectionService, operationService); }]);
})(application || (application = {}));
//# sourceMappingURL=ConnectionController.js.map