(function () {
    app
        .controller('codeManagerFormController', codeManagerFormController);

    codeManagerFormController.$inject = ["$http", "$stateParams"];

    function codeManagerFormController($http, $stateParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.key = $stateParams.key;
        vm.code = {
            DateResetIndex: new Date()
        };
        vm.dropdowns = [
            { field: 1, Name: "Theo tháng (reset ngày 1 hàng tháng)" },
            { field: 2, Name: "Theo năm (reset ngày 1/1 hàng năm)" }
        ]
        vm.back = back;
        vm.save = save;
        $http({
            method: "GET",
            url: "/odata/CodeManagers" + "(" + vm.key + ")"
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
                data: angular.toJson(vm.code)
            }).then(function successCallback() {
                toastr["success"]("Quá trình chỉnh sửa hoàn thành!")
                back();
            }, function errorCallback() {
                toastr["error"]("Có biến rồi đại vương ơi!");
            })

        }
    }
})();
