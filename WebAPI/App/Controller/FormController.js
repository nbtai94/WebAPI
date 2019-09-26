app.controller('FormController', ['$scope', '$stateParams', '$http', function ($scope, $stateParams, $http) {
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

        }
        else {
            debugger;
            $http({
                method: "POST",
                url: "api/Product/AddProduct",
                datatype: "json",
                data: JSON.stringify(vm.product)
            }).then(function (response) {
                debugger;
                alert(response.data);
                vm.product = {};
            })
        }
    }




}])