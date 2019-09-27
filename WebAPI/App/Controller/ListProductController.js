app.controller('ListProductController', function ($scope, $http, $state) {
    var vm = this;
    vm.add = add;
    vm.edit = edit;
    vm.remove = remove;
    vm.products = {};
    vm.currentPage = 1;
    vm.itemsPerPage = 5;
    vm.skip = (vm.currentPage - 1) * vm.itemsPerPage;
    vm.take = vm.itemsPerPage;
    vm.onChangePagination = onChangePagination;

    vm.getAllProduct = getAllProduct;
    getAllProduct();
    vm.changeItemPerPage =changeItemPerPage;
    function getAllProduct() {
        debugger;
        $http({
            method: "GET",
            url: "api/Products?skip=" + vm.skip + "&take=" + vm.take        
        }).then(function (result){
            debugger;
            vm.products = result.data.data;
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
        debugger;
        $state.go("form", {});
    }
    function edit(item) {
        debugger;
        $state.go("form", { id: item.Id });
    }
    function remove(item) {
        debugger;
        $http({
            method: 'delete',
            url:"api/Products/"+item.Id
        }).then(function (response) {

            alert(response.data);
            getAllProduct();
        }, function (error) {
        });
    }
});

