(function() {
  'use strict';

  angular.module('application', [
    'ui.router',
    'ngAnimate',

    //foundation
    'foundation',
    'foundation.dynamicRouting',
    'foundation.dynamicRouting.animations'
  ])
    .config(config).run(run)
  ;

  config.$inject = ['$urlRouterProvider', '$locationProvider', '$controllerProvider'];

  function config($urlProvider, $locationProvider, $controllerProvider) {
    $urlProvider.otherwise('/');

    $locationProvider.html5Mode({
      enabled:false,
      requireBase: false
    });

    $locationProvider.hashPrefix('!');

    $controllerProvider.allowGlobals();
  }

  function run() {
    FastClick.attach(document.body);
  }

})();

/**
 * Created by Kalle on 1.9.2015.
 */
/// <reference path="../../../typings/angularjs/angular.d.ts" />
var HomeController = (function () {
    function HomeController($scope) {
        this.hosts = [
            { displayName: "test.theball.me", value: "test.theball.me" },
            { displayName: "beta.diosphere.org", value: "beta.diosphere.org" },
            { displayName: "localhost", value: "localhost" },
        ];
        this.connections = [];
        $scope.vm = this;
        this.currentHost = this.hosts[2];
    }
    HomeController.prototype.hasConnections = function () {
        return this.connections.length > 0;
    };
    HomeController.prototype.isCreateFirstConnectionMode = function () {
        return !this.hasConnections();
    };
    HomeController.prototype.isManageConnectionsMode = function () {
        return this.hasConnections();
    };
    HomeController.prototype.CreateConnection = function () {
        alert(this.email + " for host: " + this.currentHost.value);
    };
    HomeController.$inject = ['$scope'];
    return HomeController;
})();
//# sourceMappingURL=HomeController.js.map