(function () {
   app
        .controller('codeManagerFormController', codeManagerFormController);

    codeManagerFormController.$inject = ["$http","$stateParams"];

    function codeManagerFormController($http, $stateParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.key = $stateParams.key;
        vm.code = {};
        vm.back = back;
        vm.save = save;
        $http({
            method: "GET",
            url:"/odata/CodeManagers"+"("+vm.key+")"
        }).then(function successCallback(res) {
            debugger
            vm.code = res.data;
        }, function errorCallback() {
                toastr["error"]("Có biến rồi đại vương ơi!");
        })

        function back() {
            history.back();
        }
        function save() {
            $http({
                method: "PUT",
                url: "/odata/CodeManagers" + "(" + vm.key + ")",
                data:angular.toJson(vm.code)
            }).then(function successCallback() {
                toastr["success"]("Quá trình chỉnh sửa hoàn thành!")
                back();
            }, function errorCallback() {
                    toastr["error"]("Có biến rồi đại vương ơi!");
            })

        }
    }
})();
