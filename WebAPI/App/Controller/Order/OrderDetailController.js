app.controller("OrderDetailController", function ($scope, $stateParams, $state, $http) {
    var vm = this;
   
    vm.id = $stateParams.id;

    vm.order = {
    };
    vm.getOrderDetail = getOrderDetail;
    vm.back = back;
    vm.print = print;
    getOrderDetail();

    function back() {
        history.back();
    }
    function getOrderDetail() {
        $http({
            method: "GET",
            url: "/odata/Orders" + "(" + vm.id + ")" + "?$expand=Items",
        }).then(function (res) {
            vm.order = res.data;
        })
    };
    function print() {
        window.print();
    }
});