app.controller('ProductFormController', ['$scope', '$stateParams', '$http', function ($scope, $stateParams, $http) {
    var vm = this;
    vm.back = back;
    vm.save = save;
    vm.product = {};
    vm.categories = {};
    vm.id = $stateParams.id;

    function back() {
        history.back();
    }
    $http({
        method: "GET",
        url: "api/ProductCategoriesAPI/ProductCategories",
    }).then(function successCallback(res) {
        vm.categories = res.data.data;
    }, function errorCallback(res) {
        toastr["error"]("Lỗi rồi , ko tải được dữ liệu!");
    })


    function save() {
        if (vm.id) {
            $http({
                method: "Put",
                url: "api/ProductsAPI/Product",
                data: JSON.stringify(vm.product)
            }).then(function (res) {
                toastr["success"]("Chỉnh sửa thành công!");
                vm.back();
            });
        }
        else {
            $http({
                method: "POST",
                //url:"api/Product/AddProduct",
                url: "api/ProductsAPI/Products",
                datatype: "json",
                data: JSON.stringify(vm.product)
            }).then(function (response) {
                debugger;
                vm.product = {};
                toastr["success"]("Thêm thành công!"),
                    vm.back();
            })
        }
    }
    if (vm.id) {
        $http({
            method: "GET",
            url: "api/ProductsAPI/Products?id=" + vm.id,
        }).then(function (res) {
            vm.product = res.data
            debugger;
        })
    }
    vm.price = {
        format: "0,",
        step: 1000
    }

    //config CKeditor
    //vm.editorOptions = {
    //    // settings more at http://docs.ckeditor.com/#!/guide/dev_configuration
    //};

}])