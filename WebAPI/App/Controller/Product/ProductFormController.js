app.controller('ProductFormController', ['$scope', '$stateParams', '$http', function ($scope, $stateParams, $http) {
    var vm = this;
    vm.back = back;
    vm.save = save;
    vm.product = {};
    vm.id = $stateParams.id;

    function back() {
        history.back();
    }
    function save() {
        if (vm.id) {
            $http({
                method: "Put",
                url: "api/Products?id=" + vm.id,
                data: JSON.stringify(vm.product)
            }).then(function (res) {
                toastr["success"]("Chỉnh sửa thành công!"),
                    $state.go("list")
            })
        }
        else {
            $http({
                method: "POST",
                //url:"api/Product/AddProduct",
                url: "api/Products",
                datatype: "json",
                data: JSON.stringify(vm.product)
            }).then(function (response) {
                debugger;
                vm.product = {};
                toastr["success"]("Thêm thành công!"),

                    $state.go("list")
            })
        }
    }
    if (vm.id) {
        $http({
            method: "GET",
            url: "api/Products?id=" + vm.id,
        }).then(function (res) {
            vm.product = res.data
        })
    }
}])