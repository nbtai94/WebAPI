app.controller('ListProductController', function ($scope, $http, $state, toaster) {

    var vm = this;
    vm.add = add;
    vm.edit = edit;
    vm.remove = remove;
    vm.search = search;
    vm.products = {};   
    vm.currentPage = 1;
    vm.itemsPerPage = 10;
    vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
    vm.take = vm.itemsPerPage;
    vm.onChangePagination = onChangePagination;
    vm.getAllProduct = getAllProduct;
    getAllProduct();
    
    vm.changeItemPerPage = changeItemPerPage;
    
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


    function search() {
        debugger;
        $http({
            method: "GET",
            url: "api/Products/SearchProduct?key=" + vm.k
        }).then(function (result) {
            vm.products = result.data;;
            vm.total = result.data.total;
        })
    } 



    function changeItemPerPage() {
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
});

