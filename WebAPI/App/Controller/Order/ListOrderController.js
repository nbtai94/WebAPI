app.controller("ListOrderController", function ($scope, $stateParams, $state, $http) {
    var vm = this;
    vm.orders = [{}];
    vm.add = add;
    vm.getAllOrder = getAllOrder;
    vm.currentPage=1;
    vm.itemsPerPage = 10    ;
    vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
    vm.take = vm.itemsPerPage;

    vm.getAllOrder();
    //Get All
    function getAllOrder() {
        $http({
            method: "GET",
            url: "api/OrdersAPI/Orders?skip=" + vm.skip + "&take=" + vm.take
        }).then(function (result) {
            vm.orders = result.data.data;
            vm.total = result.data.total;
        })
    }
    //Redirect sang form
    function add() {
        $state.go("orderform", {});
    }
    vm.edit = edit;
    function edit(item) {
        $state.go("orderform", { id: item.Id });
    }

    vm.info = info;
    function info(item) {
        $state.go("orderdetail", {id:item.Id});
    }
    //Tim kiem
    vm.search = search;

    function search() {
        $http({
            method: "GET",
            url: "api/OrdersAPI/Orders?key=" + vm.k
        }).then(function (result) {
            vm.orders = result.data.data;
            vm.total = result.data.total;
        })
    }
    //Phan trang
    vm.onChangePagination = onChangePagination;
    function onChangePagination() {
        vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
        vm.take = vm.itemsPerPage;
        $http({
            method: "GET",
            url: "api/OrdersAPI/Orders?skip=" + vm.skip + "&take=" + vm.take
        }).then(function (result) {
            vm.orders = result.data.data;
            vm.total = result.data.total;
        })
    }
    //Sap xep
    vm.sortBy = sortBy;
    vm.sortColumn = 'Id';
    vm.reverse = false;
    function sortBy(col, reverse) {
        switch (col) {
          
            case "CustomerName": {
                vm.sortColumn = 'CustomerName'; break;
            }
            case "CustomerAddress": {
                vm.sortColumn = 'CustomerAddress'; break;
            }
            case "CustomerPhone": {
                vm.sortColumn = 'CustomerPhone'; break;
            }
            case "TotalMoney": {
                vm.sortColumn = 'TotalMoney'; break;
            }
            case "DateOrder": {
                vm.sortColumn = 'TotalMoney'; break;
            }
            case "DateCreated": {
                vm.sortColumn = 'TotalMoney'; break;
            }
                
        }
        vm.reverse = !reverse;
    }
    //Remove & Redirect
    vm.remove = remove;
    function remove(item) {
        if (!confirm("Bạn có chắc muốn xóa đơn hàng này!")) {
            return false;
        }
        $http({
            method: "DELETE",
            url: "api/OrdersAPI/Delete?Id=" + item.Id,

        }).then(function (res) {
            toastr["success"]("Đã xóa đơn hàng!")
            getAllOrder();
        });
    }
    //

});