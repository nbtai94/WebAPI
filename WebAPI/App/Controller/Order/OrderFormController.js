
app.controller("OrderFormController", function ($scope, $stateParams, $state, $http) {
   
    var vm = this;
    vm.id = $stateParams.id;
    vm.products = [{}];
    vm.listItems = [];
    vm.getAllCustomer = getAllCustomer;
    vm.getAllProduct = getAllProduct;
    getAllCustomer();
    getAllProduct();
    vm.save = save;
    vm.back = back;
    vm.remove = remove;
    vm.getTotal = getTotal;
   
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
        debugger
        var data = {
            ProductId: item.Id,
            ProductName: item.Name,
            Price: item.Price,
            Quantity: 1
        }    
            vm.order.Items.push(data);

    }
    function getTotal() {
        var sum = 0;
        //for (var i = 0; i < vm.listItems.length; i++) {  //Không sử dụng for
        //    sum += vm.listItems[i].Price * vm.listItems[i].Quantity;
        //}


        angular.forEach(vm.order.Items, function (value) {
            sum+=value.Quantity*value.Price
        })
        return sum;
    }

    function back() {
        history.back();
    }
    function remove(index) {
        vm.order.Items.splice(index, 1);
    }


    //GET 1 ORDER
    vm.order = {};
    vm.getOrder = getOrder;
    if (vm.id) {
        vm.getOrder();
    }
    function getOrder() {
        $http({
            method: "GET",
            url: "api/Orders/GetOrder?Id=" + vm.id
        }).then(function (res) {
            vm.order = res.data.data;
            //vm.listItems = vm.order.Items;  // không sử dụng vm.listItems?
            //vm.datepicker = vm.order.DateOrder; //Không sử dụng vm.datepicker?
            //vm.customer = {   //Không sử dụng vm.customer ?
            //    Id: vm.order.CustomerId,
            //    Name: vm.order.CustomerName
            //}
        })
    };

    function save() {
        debugger;
    
        if (vm.id) {
            vm.totalMoney = getTotal();
  
            vm.data = { //Không sử dụng vm.data được không ?
                Id:  vm.id,
                CustomerId: vm.order.CustomerId,
                TotalMoney:vm.totalMoney,
                DateOrder: vm.order.DateOrder,
                DateCreate:vm.DateCreate,
                Status:"",
                Note:"",
                Items:vm.order.Items,
            }
            //EDIT
            $http({
                method: 'PUT',
                url: "/api/Orders/EditOrder?Id=" + vm.id,
                datatype: "JSON",
                data: angular.toJson(vm.data)
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
                CustomerId: vm.order.CustomerId,
                TotalMoney: vm.totalMoney,
                DateOrder: vm.order.DateOrder,
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
 
    }

    //FORTMAT NUMERIC QUANTITY KENDO
    vm.quantity = {
        format: "#",
        decimals: 0
    }
     //FORTMAT NUMERIC PRICE KENDO
    vm.price = {
        format: "0,",
        step:1000
    }

});