app.controller("OrderController", ['$scope', '$stateParams', '$http'], function ($scope, $stateParams, $http) {
    var vm = this;
    vm.orders = {};
    vm.getAllOrder = getAllOrder;
 
    function getAllOrder() {
        $http({
            method: "GET",
            url:"api/GetOrders"
        }).then(function (res) {
            vm.Orders = res.data;
        })
    }
});