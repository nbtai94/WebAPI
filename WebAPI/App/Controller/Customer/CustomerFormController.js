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
        debugger;
        if (vm.id) {
            debugger;
            $http({
                method: "Put",
                url: "api/Customers/EditCustomer?id=" + vm.id,
                data: JSON.stringify(vm.customers)
            }).then(function (res) {
                toastr["success"]("Chỉnh sửa thành công!")
                debugger;

            })
        }
        else {
            debugger;
            $http({
                method: "POST",
                //url:"api/Product/AddProduct",
                url: "api/Customers/AddCustomer",
                datatype: "json",
                data: JSON.stringify(vm.customers)
            }).then(function (response) {
                debugger;
                vm.customers = {};
                toastr["success"]("Thêm thành công!")
                $state.go("customer");
            })
        }
    }
    if (vm.id) {
        debugger;
        $http({
            method: "GET",
            url: "api/Customers/GetCustomer?id=" + vm.id,
        }).then(function (res) {
            vm.customers = res.data;
        })
    }


    



}])