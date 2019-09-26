app.controller('ListProductController', function ($scope, $http, $state) {
    var vm = this;
    vm.add = add;
    vm.edit = edit;
    vm.remove = remove;
    vm.products = {};
    vm.getAllProduct = getAllProduct;
    getAllProduct();
    function getAllProduct() {
        debugger;
        $http({

            method: "GET",
            //url: "api/Product/GetAllProduct"
            url:"api/Products"
        }).then(function (result) {
            vm.products = result.data;

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
            //url: 'api/Product/RemoveProduct?id=' + item.Id,
            url:"api/Products/"+item.Id
        }).then(function (response) {

            alert(response.data);
            getAllProduct();
        }, function (error) {
        });
    }




});

