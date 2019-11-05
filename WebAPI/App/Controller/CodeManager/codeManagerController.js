(function () {
    'use strict';
    app
        
        .controller('codeManagerController', codeManagerController);
    codeManagerController.$inject = ['$location','$http',"$state"];
    function codeManagerController($location, $http, $state) {
        var vm = this;
        vm.codemanager = [{}];
        vm.edit = edit;
        $http({
            method: "GET",
            url: "/odata/CodeManagers",
        }).then(function (res) {
            vm.codemanager = res.data.value;
        }, function errorCallback() {
                toastr["error"]("Can't load data, please try again!")
        })

        function edit(key) {
            $state.go("codemanagerform", {key : key});
        }

    }
})();
