app.controller("OrderController", function ($scope, $stateParams, $state, $http) {
    var vm = this;
    vm.orders = {};
    vm.add = add;
    vm.getAllOrder = getAllOrder;

    vm.currentPage = 1;
    vm.itemsPerPage = 10;
    vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
    vm.take = vm.itemsPerPage;

    vm.getAllOrder();

    function getAllOrder() {
        debugger;
        $http({
            method: "GET",
            url: "api/Orders?skip=" + vm.skip + "&take=" + vm.take
        }).then(function (result) {
            debugger;
            vm.orders = result.data.data;
            vm.total = result.data.total;
        })
    }

    function add() {
        $state.go("orderform", {});
    }
});