app.controller('ListProductController', function ($scope, $http, $state) {
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
        $http({
            method: "GET",
            url: "api/ProductsAPI/Products?skip=" + vm.skip + "&take=" + vm.take
        }).then(function (result) {
            vm.products = result.data.data;
            vm.total = result.data.total;
        })
    }
    //tim kiem
    function search() {
        $http({
            method: "GET",
            url: "api/ProductsAPI/SearchProduct?k=" + vm.k 
        }).then(function (result) {
            vm.products = result.data.data;
            vm.total = result.data.total;
        })
    }

    //Phan trang
    function onChangePagination() {
        vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
        vm.take = vm.itemsPerPage;
        $http({
            method: "GET",
            url: "api/ProductsAPI/Products?skip=" + vm.skip + "&take=" + vm.take
        }).then(function (result) {
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
        if (!confirm("Bạn có chắc muốn xóa sản phẩm này!")) {
            return false;
        }
        $http({
            method: 'delete',
            url: "api/ProductsAPI/Delete?key=" + item.Id
        }).then(function (response) {
            toastr["success"]("Xóa thành công!")
            getAllProduct();
        }, function (error) {
                toastr["error"]("Không thể xóa sản phẩm đã có trong đơn hàng!")
        });
    }


    //Sap xep
    vm.sortBy = sortBy;
    vm.sortColumn = 'Id';
    vm.reverse = false;
    function sortBy(col, reverse, show) {
        switch (col) {
      
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