app.controller("ListOrderController", function ($scope, $stateParams, $state, $http) {
    var vm = this;
    vm.orders = [{}];
    vm.add = add;
    vm.getAllOrder = getAllOrder;
    vm.currentPage = 1;
    vm.itemsPerPage = 10;
    vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
    vm.take = vm.itemsPerPage;

    vm.getAllOrder();
    //Get All
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
    //Redirect sang form
    function add() {
        $state.go("orderform", {});
    }
    vm.info = info;
    function info(item) {
        debugger;
        $state.go("orderdetail", {id:item.Id});
    }
    //Tim kiem
    vm.search = search;

    function search() {
        debugger;
        $http({
            method: "GET",
            url: "api/Orders/SearchOrder?key=" + vm.k
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
        debugger;
        switch (col) {
            case "Id": {
                vm.sortColumn = 'Id'; break;
            }
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

});