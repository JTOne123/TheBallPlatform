/**
 * Created by Kalle on 6.9.2015.
 */

/// <reference path="../../typings/angularjs/angular.d.ts" />

module application {
  export class ConnectionService {
    public getHelloWorld():any {
      var me = this;
      return this.promiseCache({
        promise: function() {
          return me.$http.get('/data/ConnectionHosts.json');
        }
      });
    }

    constructor(private $http, $location, private promiseCache) {

    }
  }


  (<any>window).appModule.factory('ConnectionService', ["$http", "$location", "promiseCache", ($http, $location, promiseCache)
    => new ConnectionService($http, $location, promiseCache)]);

}

