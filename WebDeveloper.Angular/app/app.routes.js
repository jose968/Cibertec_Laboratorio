﻿(function () {
    'use strict';

    angular.module('app')
        .config(routeConfig);

    routeConfig.$inject = ['$stateProvider', '$urlRouterProvider'];

    function routeConfig($stateProvider, $urlRouterProvider) {
        $stateProvider
        .state("home",
        {
            url: "/home",
            templateUrl: "app/home.html"
        })

        .state("person",
        {
            url: "/person",
            templateUrl: "app/private/person/person.html"
        })

        .state("otherwise",
        {
            url: "path*",
            templateUrl: "app/home.html"
        });
    }

})();