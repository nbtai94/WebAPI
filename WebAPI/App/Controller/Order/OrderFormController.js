app.controller("OrderFormController", function ($scope, $stateParams, $state, $http) {
    var vm = this;
    vm.id = $stateParams.id;
    vm.products = [{}];
    //vm.getAllCustomer = getAllCustomer;
    vm.getAllProduct = getAllProduct;
    //getAllCustomer();
    getAllProduct();
    vm.save = save;
    vm.back = back;
    vm.remove = remove;
    vm.getTotal = getTotal;
    vm.order = {
        Items: [], DateOrder: new Date(),TotalMoney:0
    };
    vm.order.DateOrder = kendo.parseDate(vm.order.DateOrder, "s");
    vm.getOrder = getOrder;
    vm.select = select;
    vm.clear = clear;

    //function getAllCustomer() {
    //    $http({
    //        method: "GET",
    //        url: "odata/Customers"
    //    }).then(function (result) {
    //        vm.customers = result.data.value;
    //    })
    //}
    vm.customerDataSource = {
        type: "odata-v4",
        serverFiltering: true,
        transport: {
            read: {
                url: "/odata/Customers",
            }
        }
    };
    function getAllProduct() {
        $http({
            method: "GET",
            url: "/odata/Products"
        }).then(function (result) {
            vm.products = result.data.value;
            vm.total = result.data.total;
        })
    }
    function select(item) {
        data = {
            ProductId: item.Id,
            ProductName: item.Name,
            Price: item.Price,
            Quantity: 1
        }
        var isExist = vm.order.Items.find(x => x.ProductId === item.Id);

        if (!isExist) {
            vm.order.Items.push(data);
        }
        else {
            isExist.Quantity++;
            //++isExist.Quantity;
        }
        vm.order.TotalMoney= vm.getTotal()
        
    }
    function getTotal() {
        debugger;
        var sum = 0;
        //for (var i = 0; i < vm.listItems.length; i++) {                       //For
        //    sum += vm.listItems[i].Price * vm.listItems[i].Quantity;
        //}
        //vm.order.Items.forEach(function (value) {                         //Foreach JavaScript
        //    sum+=value.Quantity*value.Price
        //})
        angular.forEach(vm.order.Items, function (value) {                  //Foreach Angular
            sum += value.Quantity * value.Price
        })
        return sum;
    }
    

    function back() {
        history.back();
    }
    function remove(index) {
        vm.order.Items.splice(index, 1);
        vm.order.TotalMoney = vm.getTotal()
    }
    function clear() {
        vm.order.Items = [];
        vm.order.TotalMoney = 0;
    }
    //GET 1 ORDER
    if (vm.id) {
        vm.getOrder();
    }
    function getOrder() {
        $http({
            method: "GET",
            url: "/odata/Orders" + "(" + vm.id + ")" + "?$expand=Items",
        }).then(function (res) {
            vm.order = res.data;
        })
    };
    function save() {
        if (vm.id) {
            //EDIT
            $http({
                method: 'PUT',
                url: "/odata/Orders" + "(" + vm.id + ")",
                datatype: "JSON",
                data: angular.toJson(vm.order)
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
            $http({
                method: 'POST',
                url: '/odata/Orders',
                datatype: "JSON",
                data: angular.toJson(vm.order)
            }).then(function successCallback(response) {
                toastr["success"]("Đã thêm đơn hàng!");
                $state.go("order", {});
                // when the response is available
            }, function errorCallback(response) {
                toastr["error"]("Vui lòng điền đủ thông tin và thử lại!")
            });
        }
    }
    //FORTMAT NUMERIC QUANTITY KENDO
    vm.quantity = {
        format: "#",
        decimals: 0
    }
    //FORTMAT NUMERIC PRICE KENDO
    vm.price = {
        format: "0,",
        step: 1000
    }
});