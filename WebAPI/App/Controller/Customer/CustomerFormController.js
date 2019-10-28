app.controller('CustomerFormController', ['$scope', '$stateParams', '$http', function ($scope, $stateParams, $http, $state) {
    var vm = this;
    vm.back = back;
    vm.save = save;
    vm.customers = {};
    vm.id = $stateParams.id;

    function back() {
        history.back();

    }
    function save() {
        if (vm.id) {
            $http({
                method: "Put",
                url: "odata/Customers" + "(" + vm.id + ")",
                data: angular.toJson(vm.customers)
            }).then(function (res) {
                toastr["success"]("Chỉnh sửa thành công!")
                vm.back()
            })
        }
        else {
            $http({
                method: "POST",
                //url: "api/Customers/AddCustomer",
                url: "odata/Customers",
                datatype: "json",
                data: angular.toJson(vm.customers)
            }).then(function (response) {
                toastr["success"]("Thêm thành công!")
                vm.back()
            })
        }
    }
    if (vm.id) {
        $http({
            method: "GET",
            url: "odata/Customers" + "(" + vm.id + ")",
        }).then(function (res) {
            vm.customers = res.data;
        })
    }






}]);