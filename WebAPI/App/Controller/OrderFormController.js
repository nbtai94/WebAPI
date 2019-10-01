app.controller("OrderFormController", function ($scope, $stateParams, $state, $http) {
    var vm = this;

    vm.products = {};
    vm.customers = {};

    vm.currentPage = 1;
    vm.itemsPerPage = 10;
    vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
    vm.take = vm.itemsPerPage;
    vm.getAllCustomer = getAllCustomer;
    getAllCustomer();
    function getAllCustomer() {
        $http({
            method: "GET",
            url: "api/Customers?skip=" + vm.skip + "&take=" + vm.take
        }).then(function (result) {
            debugger;
            vm.customers = result.data.data;
            vm.total = result.data.total;
        })
    }

    vm.back = back;
    function back() {
        history.back();
    }
});