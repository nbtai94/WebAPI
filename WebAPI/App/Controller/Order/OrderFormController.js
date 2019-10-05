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
            vm.customers = result.data.data;
        })
    }
    function getAllProduct() {
        $http({
            method: "GET",
            url: "api/Products/GetAllProducts"
        }).then(function (result) {
            vm.products = result.data.data;
            vm.total = result.data.total;
        })
    }

    vm.select = select;
    function select(item) {
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
            //++isExist.Quantity;
        }
    }
    vm.getTotal = getTotal;
    function getTotal() {
        var sum = 0;
        for (var i = 0; i < vm.listItems.length; i++) {
            sum += vm.listItems[i].Price * vm.listItems[i].Quantity;
        }
        return sum;
    }

    vm.back = back;
    function back() {
        history.back();
    }
    vm.remove = remove;
    function remove(index) {
        vm.listItems.splice(index, 1);
    }
    vm.customer;
    vm.datepicker;
    vm.save = save;
 
    function save() {
        debugger;
        vm.totalMoney = getTotal();
        vm.data = {
            CustomerId: vm.customerId,
            TotalMoney: vm.totalMoney,
            DateOrder: vm.datepicker,
            DateCreate: vm.datecreate,         
            Items: vm.listItems,
        }
        $http({
            method: "POST",
            url: "api/Orders/AddOrder",
            datatype: "Json",
            data: JSON.stringify(vm.data)
        }).then(function (res) {
            alert("Đã thêm thành công");
            vm.listItems = {};
        });
    }
});