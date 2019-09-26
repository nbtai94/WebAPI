app.controller('ListProductController', function ($scope, $http,$state) {
    var vm = this;
    vm.add = add;
    vm.edit = edit;
    vm.products = {};
    vm.getAllProduct = getAllProduct;
    getAllProduct();
    function getAllProduct() {
        debugger;
        $http({

            method: "get",
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
        $state.go("form", {id:item.Id});
    }


});

