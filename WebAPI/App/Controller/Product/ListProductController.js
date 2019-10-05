﻿app.controller('ListProductController', function ($scope, $http, $state) {
    //Khai bao
    var vm = this;
    vm.add = add;
    vm.edit = edit;
    vm.remove = remove;
    vm.search = search;
    vm.products = [{}];
    vm.currentPage = 1;
    vm.itemsPerPage = 8;
    vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
    vm.take = vm.itemsPerPage;
    vm.onChangePagination = onChangePagination;
    vm.getAllProduct = getAllProduct;
    getAllProduct();
    //Get all

    function getAllProduct() {
        debugger;
        $http({
            method: "GET",
            url: "api/Products?skip=" + vm.skip + "&take=" + vm.take
        }).then(function (result) {
            debugger;
            vm.products = result.data.data;
            vm.total = result.data.total;
        })
    }
    //tim kiem
    function search() {
        debugger;
        $http({
            method: "GET",
            url: "api/Products/SearchProduct?k=" + vm.k
        }).then(function (result) {
            vm.products = result.data.data;
            vm.total = result.data.total;
        })
    }

    //Phan trang
    function onChangePagination() {
        debugger;
        vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
        vm.take = vm.itemsPerPage;
        $http({
            method: "GET",
            url: "api/Products?skip=" + vm.skip + "&take=" + vm.take
        }).then(function (result) {
            debugger;
            vm.products = result.data.data;
            vm.total = result.data.total;
        })
    }
    function add() {
        $state.go("form", {});
    }
    function edit(item) {
        $state.go("form", { id: item.Id });
    }
    //Xoa
    function remove(item) {
        if (!confirm("Bạn có chắc muốn xóa!")) {
            return false;
        }

        $http({
            method: 'delete',
            url: "api/Products?key=" + item.Id
        }).then(function (response) {
            debugger;
            alert("Đã xóa thành công!");
            getAllProduct();
        }, function (error) {
        });
    }


    //Sap xep
    vm.sortBy = sortBy;
    vm.sortColumn = 'Id';
    vm.reverse = false;
    function sortBy(col, reverse) {
        debugger;
        switch (col) {
            case "Id": {
                vm.sortColumn = 'Id'; break;
            }
            case "Name": {
                vm.sortColumn = 'Name'; break;
            }
            case "Category": {
                vm.sortColumn = 'Category'; break;
            }
            case "Price": {
                vm.sortColumn = 'Price'; break;
            }

        }
        vm.reverse = !reverse;
    }
});