app.controller("OrderFormController", function ($scope, $stateParams, $state, $http) {
    var vm = this;

    vm.products = {};
    vm.customers = {};
    vm.listItems = [];

    vm.getAllCustomer = getAllCustomer;
    vm.getAllProduct = getAllProduct;
    getAllCustomer();
    getAllProduct();
    function getAllCustomer() {
        $http({
            method: "GET",
            url: "api/Customers/GetAllCustomers"
        }).then(function (result) {
            debugger;
            vm.customers = result.data.data;
        })
    }
    function getAllProduct() {
        debugger;
        $http({
            method: "GET",
            url: "api/Products/GetAllProducts"
        }).then(function (result) {
            debugger;
            vm.products = result.data.data;
            vm.total = result.data.total;
        })
    }
    
    vm.select = select;
    function select(item) {
        debugger;
        var data = {
            Id: item.Id,
            Name: item.Name,
            Price: item.Price,
            Quantity: 1
        }
        //debugger;
        //var a = (0 == '0')
        //var b = (0 === '0')
        var isExist = vm.listItems.find(x => x.Id === item.Id);

        if (!isExist) {
            vm.listItems.push(data);
        }
        else {
            isExist.Quantity++;
            ++isExist.Quantity;
        } 
    }


 
    

    vm.back = back;
    function back() {
        debugger;
        history.back();
    }
    vm.remove = remove;
    function remove(index) {
        debugger;
        vm.listItems.splice(index, 1);

    }








});