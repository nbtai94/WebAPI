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
        debugger;
        if (vm.id) {
            debugger;
            $http({
                method: "Put",
                url: "api/Products?id=" + vm.id,
                data: JSON.stringify(vm.product)
            }).then(function (res) {
                alert("Chỉnh sửa thành công!")
            })
        }
        else {
            debugger;
            $http({
                method: "POST",
                //url:"api/Product/AddProduct",
                url: "api/Products",
                datatype: "json",
                data: JSON.stringify(vm.product)
            }).then(function (response) {
                debugger;
                alert("Thêm thành công!");
                vm.product = {};
            })
        }
    }
    if (vm.id) {
        debugger;
        $http({
            method: "GET",
            url: "api/Products?id=" + vm.id,
        }).then(function (res) {
            vm.product = res.data
        })
    }
}])