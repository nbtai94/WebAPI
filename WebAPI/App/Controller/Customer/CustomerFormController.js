(function () {
    'use strict';

    app.controller('customerFormController', controller);

    controller.$inject = ['$scope', '$stateParams', '$http'];

    function controller($scope, $stateParams, $http, $state) {
        var vm = this;
        vm.back = back;
        vm.save = save;
        vm.customer = {};
        vm.id = $stateParams.id;
        
        function back() {
            history.back();
        }
        //SAVE
        function save() {
            if (vm.id) {
                debugger;
                $http({
                    method: "Put",
                    url: "/odata/Customers" + "(" + vm.id + ")",
                    data: angular.toJson(vm.customer)
                }).then(function (res) {
                    toastr["success"]("Chỉnh sửa thành công!")
                    vm.back()
                })
            }
            else {
                $http({
                    method: "POST",
                    //url: "api/Customers/AddCustomer",
                    url: "/odata/Customers",
                    datatype: "json",
                    data: angular.toJson(vm.customer)
                }).then(function (response) {
                    toastr["success"]("Thêm thành công!")
                    vm.back()
                })
            }
        }
        //GET CUSTOMER
        if (vm.id) {
            debugger
            $http({
                method: "GET",
                url: "/odata/Customers" + "(" + vm.id + ")",
            }).then(function (res) {
                vm.customer = res.data;
            })
        }
    }
})();
