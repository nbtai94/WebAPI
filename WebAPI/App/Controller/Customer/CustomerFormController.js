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
                url: "api/Customers/EditCustomer?id=" + vm.id,
                data: JSON.stringify(vm.customers)
            }).then(function (res) {
                toastr["success"]("Chỉnh sửa thành công!")

            })
        }
        else {
            $http({
                method: "POST",
                //url:"api/Product/AddProduct",
                url: "api/Customers/AddCustomer",
                datatype: "json",
                data: JSON.stringify(vm.customers)
            }).then(function (response) {
                vm.customers = {};
                toastr["success"]("Thêm thành công!")
                $state.go("customer");
            })
        }
    }
    if (vm.id) {
        $http({
            method: "GET",
            url: "api/Customers/GetCustomer?id=" + vm.id,
        }).then(function (res) {
            vm.customers = res.data;
        })
    }


    



}]);