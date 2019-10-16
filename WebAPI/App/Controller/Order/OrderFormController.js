
app.controller("OrderFormController", function ($scope, $stateParams, $state, $http) {
    var vm = this;
    vm.id = $stateParams.id;
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
            ProductName: item.Name,
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

    //GET 1 ORDER
    vm.order = {};
    vm.getOrder = getOrder;
    if (vm.id) {
        vm.getOrder();
    }
    function getOrder() {
        $http({
            method: "GET",
            url: "api/Orders/GetOrderDetail?Id=" + vm.id
        }).then(function (res) {
            vm.order = res.data.data;
            vm.listItems = vm.order.Items;
            vm.datepicker = vm.order.DateOrder;
            vm.customer = {
                Id: vm.order.CustomerId,
                Name: vm.order.CustomerName
            }
        })
    };
    vm.orrrder = getOrder();

    function save() {
        debugger;
    
        if (vm.id) {
            vm.totalMoney = getTotal();
            vm.data = {
                Id: vm.id,
                CustomerId: vm.customer.Id,
                TotalMoney: vm.totalMoney,
                DateOrder: vm.datepicker,
                DateCreate: vm.datecreate,
                Status: "",
                Note: "",
                Items: vm.listItems,
            };
                //EDIT
            $http({
                method: 'PUT',
                url: "/api/Orders/EditOrder?Id=" + vm.id,
                datatype: "JSON",
                data: JSON.stringify(vm.data)
            }).then(function successCallback(response) {
                toastr["success"]("Chỉnh sửa thành công!")
                $state.go("order", {});
                // when the response is available
            }, function errorCallback(response) {
                    toastr["error"]("Vui lòng điền đủ thông tin và thử lại!")
            });



        }
        //ADD ORDER
        else {
            vm.totalMoney = getTotal();
            vm.data = {
                Id: vm.id,
                CustomerId: vm.customer.Id,
                TotalMoney: vm.totalMoney,
                DateOrder: vm.datepicker,
                DateCreate: vm.datecreate,
                Items: vm.listItems,
                Status: "",
                Note: "",
            }


            $http({
                method: 'POST',
                url: '/api/Orders/AddOrder',
                datatype: "JSON",
                data:JSON.stringify(vm.data)
            }).then(function successCallback(response) {
                toastr["success"]("Đã thêm đơn hàng!");
                $state.go("order", {});
                // when the response is available
            }, function errorCallback(response) {
                    toastr["error"]("Vui lòng điền đủ thông tin và thử lại!")
            });






        }
        //COMBOBOX KENDO


    }
});