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
        $http({
            method: "GET",
            url: "api/Customers?skip=" + vm.skip + "&take=" + vm.take
        }).then(function (result) {
            vm.customers = result.data.data;
            vm.total = result.data.total;
        })
    }
    //Serach
    function search() {
        $http({
            method: "GET",
            url: "api/Customers/SearchCustomer?key=" + vm.k
        }).then(function (result) {
            vm.customers = result.data.data;
            vm.total = result.data.total;
        })
    }
    vm.reload = reload;
    function reload() {
        getAllCustomer();
    }

    //Phan trang
    function onChangePagination() {
        vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
        vm.take = vm.itemsPerPage;
        $http({
            method: "GET",
            url: "api/Customers?skip=" + vm.skip + "&take=" + vm.take
        }).then(function (result) {
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
        if (!confirm("Bạn có chắc muốn xóa khách hàng này!")) {
            return false;
        }

        $http({
            method: 'delete',
            url: "api/Customers/RemoveCustomer?Id=" + item.Id
        }).then(function (response) {
        
            toastr["success"]("Xóa thành công!")
            getAllCustomer();
        }, function (error) {
                toastr["error"]("Không thể xóa khách đã đặt hàng!")
        });
    }

    //Sap xep
    vm.sortBy = sortBy;
    vm.sortColumn = 'Id';
    vm.reverse = false;
    function sortBy(col,reverse) {
        
        switch (col) {
        
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