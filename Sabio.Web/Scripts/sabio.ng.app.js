﻿/// <reference path="sabio.js" />
/// <reference path="/scripts/ng/angular.js" />

/*
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * DEPRECATED. Only for reference
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */

sabio.ng = {
    app: {
        services: {}
		, controllers: {}
    }
    , controllerInstances: []
	, exceptions: {}
	, examples: {}
    , defaultDependencies: ["ngAnimate", "ngRoute"]
    , getModuleDependencies: function () {
        if (sabio.extraModuleDependencies) {
            var newItems = sabio.ng.defaultDependencies.concat(sabio.extraModuleDependencies);
            return newItems;
        }
        return sabio.ng.defaultDependencies;
    }
};

sabio.ng.app.module = angular.module('sabioApp', sabio.ng.getModuleDependencies());

sabio.ng.app.module.value('$sabio', sabio.page);

//#region - base functions and objects -

sabio.ng.exceptions.argumentException = function (msg) {
    this.message = msg;
    var err = new Error();


    console.error(msg + "\n" + err.stack);
}

sabio.ng.app.services.baseEventServiceFactory = function ($rootScope) {

    var factory = this;

    factory.$rootScope = $rootScope;

    factory.eventService = function (eventName) {

        var thisEventService = this;

        thisEventService.eventName = eventName;

        thisEventService.broadcast = function () {
            factory.$rootScope.$broadcast(thisEventService.eventName, arguments)
        }

        thisEventService.emit = function () {
            factory.$rootScope.$emit(thisEventService.eventName, arguments)
        }

        thisEventService.listen = function (callback) {
            factory.$rootScope.$on(thisEventService.eventName, callback)
        }

    }

    return factory.eventService;
}


sabio.ng.app.services.baseService = function ($win, $loc, $util) {
    /*
        when this function is envoked by Angular, Angular wants an instance of the Service object. 
		
    */

    var getChangeNotifier = function ($scope) {
        /*
        will be called when there is an event outside Angular that has modified
        our data and we need to let Angular know about it.
        */
        var self = this;

        self.scope = $scope;

        return function (fx) {
            self.scope.$apply(fx);//this is the magic right here that cause ng to re-evaluate bindings
        }


    }

    var baseService = {
        $window: $win
        , getNotifier: getChangeNotifier
        , $location: $loc
        , $utils: $util
        , merge: $.extend
    };

    return baseService;
}

sabio.ng.app.controllers.baseController = function ($doc, $logger, $sab, $route, $routeParams) {
    /*
        this is intended to serve as the base controller
    */

    baseControler = {
        $document: $doc
		, $log: $logger
        , $sabio: $sab
        , merge: $.extend


        , setUpCurrentRequest: function (viewModel) {

            viewModel.currentRequest = { originalPath: "/", isTop: true };

            if (viewModel.$route.current) {
                viewModel.currentRequest = viewModel.$route.current;
                viewModel.currentRequest.locals = {};
                viewModel.currentRequest.isTop = false;
            }

            viewModel.$log.log("setUpCurrentRequest firing:");
            viewModel.$log.debug(viewModel.currentRequest);

        }

    };

    return baseControler;
}

//#endregion

//#region  - core ng wrapper functions --

sabio.ng.getControllerInstance = function (jQueryObj) {///used to grab an instance of a controller bound to an Element
    console.log(jQueryObj);
    return angular.element(jQueryObj[0]).controller();
}

sabio.ng.addService = function (ngModule, serviceName, dependencies, factory) {
    /*
    sabio.ng.app.module.service(
    '$baseService', 
    ['$window', '$location', '$utils', sabio.ng.app.services.baseService]);
    */
    if (!ngModule ||
		!serviceName || !factory ||
		!angular.isFunction(factory)) {
        throw new sabio.ng.exceptions.argumentException("Invalid Service Call");
    }

    if (dependencies && !angular.isArray(dependencies)) {
        throw new sabio.ng.exceptions.argumentException("Invalid Service Call [dependencies]");
    }
    else if (!dependencies) {
        dependencies = [];
    }

    dependencies.push(factory);

    ngModule.service(serviceName, dependencies);

}

sabio.ng.registerService = sabio.ng.addService;

sabio.ng.addController = function (ngModule, controllerName, dependencies, factory) {
    if (!ngModule ||
		!controllerName || !factory ||
		!angular.isFunction(factory)) {
        throw new sabio.ng.exceptions.argumentException("Invalid Service defined");
    }

    if (dependencies && !angular.isArray(dependencies)) {
        throw new sabio.ng.exceptions.argumentException("Invalid Service Call [dependencies]");
    }
    else if (!dependencies) {
        dependencies = [];
    }

    dependencies.push(factory);
    ngModule.controller(controllerName, dependencies);

}

sabio.ng.registerController = sabio.ng.addController;


//#endregions


//#region - adding in baseService and baseController

/*
the basic explaination for these three function arguments

name of thing we are creating:'baseService'
array containing the dependancies of the function we will use to create said thing
the last item in this array is invoked to create the object and passed the preceding dependancies.


*/
sabio.ng.addService(sabio.ng.app.module
					, "$baseService"
					, ['$window', '$location']
					, sabio.ng.app.services.baseService);

sabio.ng.addService(sabio.ng.app.module
					, "$eventServiceFactory"
					, ["$rootScope"]
					, sabio.ng.app.services.baseEventServiceFactory);

sabio.ng.addService(sabio.ng.app.module
					, "$baseController"
					, ['$document', '$log', '$sabio', "$route", "$routeParams"]
					, sabio.ng.app.controllers.baseController);


//#endregion

//#region - Examples on how to use the core functions

//***************************************************************************************
//------------------------ Examples -------------------------------------
/*
 * 
 *              How to call the .addService and .addController
 * 
 */




sabio.ng.examples.exampleServices = function ($baseService) {
    /*
		when this function is envoked by Angular,
		Angular wants an instance of the Service object. here
		we reuse the same instance of the JS object we have above
	*/

    /*
		we are using this as an example to demonstrate we can use our existing
		code with this new pattern.
	*/

    var aSabioServiceObject = sabio.services.users;
    var newService = angular.merge(true, {}, aSabioServiceObject, baseService);

    return newService;
}

sabio.ng.examples.exampleController = function ($scope, $baseController, $exampleSvc) {

    var vm = this;
    vm.items = null;
    vm.receiveItems = _receiveItems;
    vm.testTitle = "Get this to show first";

    //-- this line to simulate inheritance
    $baseController.merge(vm, $baseController);

    //You first pass at creating a new controller end here. 
    //go make this work first
    //-----------------------

    //this is a wrapper for our small dependency on $scope
    vm.notify = $exampleSvc.getNotifier($scope);

    function _receiveItems(data) {
        //this receives the data and calls the special
        //notify method that will trigger ng to refresh UI
        vm.notify(function () {
            vm.items = data.items;
        });
    }
}


sabio.ng.addService(sabio.ng.app.module
					, "$exampleSvc"
					, ['$baseService']
					, sabio.ng.examples.exampleServices);

sabio.ng.addController(sabio.ng.app.module
	, 'controllerName'
	, ['$scope', '$baseController', '$exampleSvc']
	, sabio.ng.examples.exampleController
	);


//------------------------ Examples -------------------------------------
//***************************************************************************************


//#endregion
