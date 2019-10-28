app.controller('ListProductController', function ($scope, $http, $state) {
    //Khai bao
    var vm = this;
    vm.add = add;
    vm.edit = edit;
    vm.remove = remove;
    vm.search = search;
    vm.products = [{}];
    vm.currentPage = 1;
    vm.itemsPerPage = 5;
    vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
    vm.top = vm.itemsPerPage;
    vm.onChangePagination = onChangePagination;
    vm.getAllProduct = getAllProduct;
    getAllProduct();
    //Get all

    function getAllProduct() {
        $http({
            method: "GET",
            url: "odata/Products?" + "$count=true" + "&$skip=" + vm.skip + "&$top=" + vm.top
        }).then(function (result) {
            vm.products = result.data.value;
            vm.total = result.data["@odata.count"];
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
        vm.top = vm.itemsPerPage;
        $http({
            method: "GET",
            url: "odata/Products?$count=true" + "&$skip=" + vm.skip + "&$top=" + vm.top
        }).then(function (result) {
            vm.products = result.data.value;
            vm.total = result.data["@odata.count"];
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
            //url: "api/ProductsAPI/Delete?key=" + item.Id
            url:"odata/Products"+"("+item.Id+")",
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