app.controller('ListCustomer', function ($scope, $http, $state) {
    //Khai bao
    var vm = this;
    vm.add = add;
    vm.edit = edit;
    vm.remove = remove;
    vm.search = search;
    vm.customers = [{}];
    vm.currentPage = 1;
    vm.itemsPerPage = 10;
    vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
    vm.take = vm.itemsPerPage;
    vm.onChangePagination = onChangePagination;
    vm.getAllCustomer = getAllCustomer;
  
    //Get All Product
    getAllCustomer();
    function getAllCustomer() {
        debugger;
        $http({
            method: "GET",
            url: "api/Customers?skip=" + vm.skip + "&take=" + vm.take
        }).then(function (result) {
            debugger;
            vm.customers = result.data.data;
            vm.total = result.data.total;
        })
    }
    //Serach
    function search() {
        debugger;
        $http({
            method: "GET",
            url: "api/Customers/SearchCustomer?key=" + vm.k
        }).then(function (result) {
            vm.customers = result.data.data;
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
            url: "api/Customers?skip=" + vm.skip + "&take=" + vm.take
        }).then(function (result) {
            debugger;
            vm.customers = result.data.data;
            vm.total = result.data.total;
        })
    }
    //Redirect sang form
    function add() {
        $state.go("cusForm", {});
    }
    function edit(item) {
        $state.go("cusForm", { id: item.Id });
    }

    //Xoa
    function remove(item) {
        if (!confirm("Bạn có chắc muốn xóa!")) {
            return false;
        }

        $http({
            method: 'delete',
            url: "api/Customers/RemoveCustomer?Id=" + item.Id
        }).then(function (response) {
            debugger;
            alert("Đã xóa thành công!");
            getAllCustomer();
        }, function (error) {
        });
    }

    //Sap xep
    vm.sortBy = sortBy;
    vm.sortColumn = 'Id';
    vm.reverse = false;
    function sortBy(col,reverse) {
        debugger;
        switch (col) {
            case "Id": {
                vm.sortColumn = 'Id'; break;
            }
            case "Name": {
                vm.sortColumn = 'Name'; break;
            }
            case "Address": {
                vm.sortColumn = 'Address'; break;
            }
            case "Email": {
                vm.sortColumn = 'Email'; break;
            }
            case "Phone": {
                vm.sortColumn = 'Email'; break;
            }
        }
        vm.reverse = !reverse;
    }


});