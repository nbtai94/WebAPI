﻿app.controller("OrderDetailController", function ($scope, $stateParams, $state, $http) {
    var vm = this;
   
    vm.id = $stateParams.id;

    vm.order = {
    };
    vm.getOrderDetail = getOrderDetail;
    getOrderDetail();
    vm.back = back;
    function back() {
        history.back();
    }


    function getOrderDetail() {
        debugger;
        $http({
            method: "GET",
            url: "api/Orders/GetOrderDetail?Id=" + vm.id
        }).then(function (res) {
            debugger;
            vm.order = res.data.data;

        })
    };




});